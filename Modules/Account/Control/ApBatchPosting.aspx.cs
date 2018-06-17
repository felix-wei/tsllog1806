using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using Wilson.ORMapper;
using System.Data.SqlClient;

public partial class PagesAccount_ApBatchPosting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.rbt.SelectedIndex = 0;
            int year = DateTime.Today.AddMonths(-1).Year;
            int mth = DateTime.Today.AddMonths(-1).Month;

            this.cmb_Year.Text = year.ToString();
            this.cmb_Period.Text = mth.ToString();
            this.date_From.Date = new DateTime(year, mth, 1);
            this.date_End.Date = this.date_From.Date.AddMonths(1).AddDays(-1);
        }
    }
    protected void btn_Post_click(object sender, EventArgs e)
    {
        string rbtValue = this.rbt.Value.ToString();
        string where = "ExportInd='N' and ";
        if (rbtValue == "0")//post by date
        {
            if (this.date_From.Value != null && this.date_End.Value != null)
                where += string.Format("DocDate>='{0}' and DocDate<'{1}'", date_From.Date.ToString("yyyy-MM-dd"), date_End.Date.AddDays(1).ToString("yyyy-MM-dd"));
        }
        else if (rbtValue == "1") //post by no
        {
            if (this.no_From.Text.Trim().Length > 0 && this.no_End.Text.Trim().Length > 0)
                where += string.Format("DocNo>='{0}' and DocNo<='{1}'", no_From.Text.Trim(), no_End.Text.Trim());
        }
        else if (rbtValue == "2")//post by peroid
        {
            if (this.cmb_Year.Text.Trim().Length > 0 && this.cmb_Period.Text.Trim().Length > 0)
                where += string.Format("AcYear='{0}' and AcPeriod='{1}'", this.cmb_Year.Text, this.cmb_Period.Text);
        }
        if (where.Length > 0)
        {
            bool apSi = this.cb_ApSi.Checked;
            if (apSi)
            {
                PostApSiList(where + " and (DocType='PL' or DocType='SC' or DocType='SD')");
            }
            bool apPaySi = this.cb_ApPaySi.Checked;
            if (apPaySi)
            {
               PostApPaySiList(where + " and DocType='PS'");
               PostApSrList(where + " and DocType='SR'");
            }
            bool apVoucher = this.cb_ApVoucher.Checked;
            if (apVoucher)
            {
                PostApVoList(where + " and DocType='VO'");
            }
            Response.Write("AP Post Complete");
            Response.Write("</br><input type='button' value='Go Back' onclick='history.back(-1)' />");
            Response.Flush();
            Response.End();
        }
        
    }

    #region Post Supply Invoice
    private void PostApSiList(string where)
    {
        string sql = "Select SequenceId,DocNo,DocType,ExportInd from XAApPayable where " + where;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docId = reader["SequenceId"].ToString();
            string postInd = SafeValue.SafeString(reader["ExportInd"], "N");
            if (postInd == "N")
                PostApSi(docId);
        }

        // Call Close when done reading.
        reader.Close();
        con.Close();
    }
    private void PostApSi(string docId)
    {
        string sql = @"SELECT AcYear, AcPeriod, AcCode, AcSource, DocType, DocNo, DocDate, SupplierBillNo, SupplierBillDate,PartyTo, MastType, CurrencyId, ExRate, Term, Description, LocAmt, 
                      DocAmt FROM XAApPayable";
        sql += " WHERE SequenceId='" + docId + "'";
        DataTable dt = Manager.ORManager.GetDataSet(sql).Tables[0];
        int acYear = 0;
        int acPeriod = 0;
        string docN = "";
        string docType = "";
        string acSource = "";
        string acCode = "";
        decimal locAmt = 0;
        decimal docAmt = 0;
        decimal exRate = 0;
        string currency = "";
        string partyTo = "";
        DateTime docDt = DateTime.Today;
        string remarks = "";
        string supplierBillNo = "";
        DateTime supplierBillDate = new DateTime(1900, 1, 1);
        if (dt.Rows.Count == 1)
        {
            acYear = SafeValue.SafeInt(dt.Rows[0]["AcYear"], 0);
            acPeriod = SafeValue.SafeInt(dt.Rows[0]["AcPeriod"], 0);
            acSource = dt.Rows[0]["AcSource"].ToString();
            acCode = dt.Rows[0]["AcCode"].ToString();
            docN = dt.Rows[0]["DocNo"].ToString();
            docType = dt.Rows[0]["DocType"].ToString();
            locAmt = SafeValue.SafeDecimal(dt.Rows[0]["LocAmt"].ToString(), 0);
            docAmt = SafeValue.SafeDecimal(dt.Rows[0]["DocAmt"].ToString(), 0);
            exRate = SafeValue.SafeDecimal(dt.Rows[0]["ExRate"].ToString(), 0);
            currency = dt.Rows[0]["CurrencyId"].ToString();
            docDt = SafeValue.SafeDate(dt.Rows[0]["DocDate"], new DateTime(1900, 1, 1));
            // partyId = dt.Rows[0][""].ToString();
            remarks = dt.Rows[0]["Description"].ToString();
            partyTo = dt.Rows[0]["PartyTo"].ToString();
            supplierBillNo = dt.Rows[0]["SupplierBillNo"].ToString();
            supplierBillDate = SafeValue.SafeDate(dt.Rows[0]["SupplierBillDate"], new DateTime(1900, 1, 1));
        }
        Response.Write(string.Format("</br>Begin Post:Bill No:{0} Bill Type:{1}", docN, docType));
        Response.Flush();

        string sqlDet = string.Format("select count(SequenceId) from XAApPayableDet where DocId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;No Detail, Can't Post");
            Response.Flush();
            Response.End();
        }
        //check account period
        if (acYear < 1 || acPeriod < 1)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Account year or Period Invalid!");
            Response.Flush();
            Response.End();
        }

        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
        if (closeInd == "")
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Can't find this account period!");
            Response.Flush();
            Response.End();
        }
        else if (closeInd == "Y")
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;The account period is closed!");
            Response.Flush();
            Response.End();
        }
        //sql = string.Format("select sum(locamt) from XAApPayableDet where DocId='{0}'", docId);
        //decimal amt_det = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);

        sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='DB' and DocId='" + docId + "'";
        decimal amt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='CR' and DocId='" + docId + "'";
        amt_det -= SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (docType == "SC")
            amt_det = -amt_det;

        if (docAmt != amt_det)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Loc Amount can't match, can't post,Please first resave it!");
            Response.Flush();
            Response.End();
        }
        sql = "select count(LocAmt) from XAApPayableDet where AcCode='' and DocId='" + docId + "'";
        detCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (detCnt > 0)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Some Item's Accode is blank, pls check!");
            Response.Flush();
            Response.End();
        }
        //delete old post data
        sql = string.Format("SELECT SequenceId from XAGlEntry WHERE DocNo='{0}' and DocType='{1}'", docN, docType);
        int glOldOid = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
        if (glOldOid > 0)
        {
            DeleteGl(glOldOid);
        }

        int glOid = 0;
        try
        {
            C2.XAGlEntry gl = new XAGlEntry();
            gl.AcPeriod = acPeriod;
            gl.AcYear = acYear;
            gl.ArApInd = "AP";
            gl.DocType = docType;
            gl.DocDate = docDt;
            gl.DocNo = docN;
            gl.CrAmt = docAmt;
            gl.DbAmt = docAmt;
            gl.CurrencyCrAmt = locAmt;
            gl.CurrencyDbAmt = locAmt;
            gl.CurrencyId = currency;
            gl.EntryDate = DateTime.Now;
            gl.ExRate = exRate;
            gl.PostDate = DateTime.Now;
            gl.PostInd = "N";
            gl.Remark = remarks;
            gl.UserId = HttpContext.Current.User.Identity.Name;
            gl.CancelInd = "N";
            gl.CancelDate = new DateTime(1900, 1, 1);
            gl.PartyTo = partyTo;
            gl.ChqNo = "";
            gl.SupplierBillNo = supplierBillNo;
            gl.SupplierBillDate = supplierBillDate;
            Manager.ORManager.StartTracking(gl, InitialState.Inserted);
            Manager.ORManager.PersistChanges(gl);
            glOid = gl.SequenceId;
            //insert Detail
            OPathQuery query = new OPathQuery(typeof(XAApPayableDet), "DocId='" + docId + "'");
            ObjectSet set = Manager.ORManager.GetObjectSet(query);
            int index = 1;
            XAGlEntryDet det1 = new XAGlEntryDet();

            det1.AcCode = acCode;
            det1.ArApInd = "AP";
            det1.AcPeriod = acPeriod;
            det1.AcSource = acSource;
            det1.AcYear = acYear;
            if (docType == "SC")
            {
                det1.CrAmt = 0;
                det1.CurrencyCrAmt = 0;
                det1.DbAmt = docAmt;
                det1.CurrencyDbAmt = locAmt;
            }
            else
            {
                det1.CrAmt = docAmt;
                det1.CurrencyCrAmt = locAmt;
                det1.DbAmt = 0;
                det1.CurrencyDbAmt = 0;
            }
            det1.CurrencyId = currency;
            det1.DocNo = docN;
            det1.DocType = docType;
            det1.ExRate = exRate;
            det1.GlLineNo = index;
            det1.GlNo = gl.SequenceId;
            det1.Remark = remarks;


            Manager.ORManager.StartTracking(det1, InitialState.Inserted);
            Manager.ORManager.PersistChanges(det1);
            decimal gstCrAmt = 0;
            decimal gstDbAmt = 0;
            string gstAcc = SafeValue.SafeString(Manager.ORManager.ExecuteScalar("SELECT AcCode FROM XXGstAccount where GstSrc='AP'"), "4053");
            for (int i = 0; i < set.Count; i++)
            {
                try
                {
                    index++;
                    XAApPayableDet invDet = set[i] as XAApPayableDet;
                    XAGlEntryDet det = new XAGlEntryDet();

                    if (invDet.AcCode == gstAcc)
                    {
                        if (invDet.AcSource == "DB")
                            gstDbAmt += invDet.LocAmt;
                        else
                            gstCrAmt += invDet.LocAmt;
                    }
                    else
                    {
                        det.AcCode = invDet.AcCode;
                        det.ArApInd = "AP";
                        det.AcPeriod = acPeriod;
                        det.AcSource = invDet.AcSource;
                        det.AcYear = acYear;
                        if (invDet.AcSource == "CR")
                        {
                            det.CrAmt = SafeValue.ChinaRound(SafeValue.ChinaRound(invDet.Qty * invDet.Price, 2) * invDet.ExRate, 2);
                            det.CurrencyCrAmt = SafeValue.ChinaRound(det.CrAmt * exRate, 2);
                            det.DbAmt = 0;
                            det.CurrencyDbAmt = 0;
                            gstCrAmt += invDet.GstAmt;
                        }
                        else
                        {
                            det.CrAmt = 0;
                            det.CurrencyCrAmt = 0;
                            det.DbAmt = SafeValue.ChinaRound(SafeValue.ChinaRound(invDet.Qty * invDet.Price, 2) * invDet.ExRate, 2);
                            det.CurrencyDbAmt = SafeValue.ChinaRound(det.DbAmt * exRate, 2);
                            gstDbAmt += invDet.GstAmt;
                        }
                        det.CurrencyId = invDet.Currency;
                        det.DocNo = docN;
                        det.DocType = docType;
                        det.ExRate = invDet.ExRate;
                        det.GlLineNo = index;
                        det.GlNo = gl.SequenceId;
                        det.Remark = invDet.ChgCode;

                        Manager.ORManager.StartTracking(det, InitialState.Inserted);
                        Manager.ORManager.PersistChanges(det);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("</br>&nbsp;&nbsp;&nbsp;Posting Error, Please retry!");
                    Response.Write("</br>&nbsp;&nbsp;&nbsp;Error:" + ex.Message);
                    Response.Flush();
                    DeleteGl(glOid);
                    Response.End();
                }
            }
            if (gstCrAmt - gstDbAmt != 0)
            {
                XAGlEntryDet det = new XAGlEntryDet();

                det.AcCode = gstAcc;
                det.ArApInd = "AP";
                det.AcPeriod = acPeriod;
                det.AcYear = acYear;
                if (docType == "SC")
                {
                    det.AcSource = "CR";
                    det.CrAmt = gstCrAmt - gstDbAmt;
                    det.CurrencyCrAmt = gstCrAmt - gstDbAmt;
                    det.DbAmt = 0;
                    det.CurrencyDbAmt = 0;
                }
                else
                {
                    det.AcSource = "DB";
                    det.CrAmt = 0;
                    det.CurrencyCrAmt = 0;
                    det.CurrencyDbAmt = gstDbAmt - gstCrAmt;
                    det.DbAmt = gstDbAmt - gstCrAmt;
                }

                det.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                det.DocNo = docN;
                det.DocType = docType;
                det.ExRate = 1;
                det.GlLineNo = index + 1;
                det.GlNo = gl.SequenceId;
                det.Remark = "GST";

                Manager.ORManager.StartTracking(det, InitialState.Inserted);
                Manager.ORManager.PersistChanges(det);
            }
            UpdateAp(docId);
        }
        catch (Exception ex)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Posting Error, Please retry!");
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Error:" + ex.Message);
            Response.Flush();
            DeleteGl(glOid);
            Response.End();
        }
        Response.Write("</br>End Post.");
        Response.Flush();
    }
    #endregion

    #region Post Voucher
    private void PostApVoList(string where)
    {
        string sql = "Select SequenceId, DocNo,DocType,ExportInd from XAApPayable where " + where;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docId=reader["SequenceId"].ToString();
            string postInd = SafeValue.SafeString(reader["ExportInd"], "N");
            if (postInd == "N")
                PostApVo(docId);
        }

        // Call Close when done reading.
        reader.Close();
        con.Close();
    }
    private void PostApVo(string docId)
    {
        string sql = @"SELECT AcYear, AcPeriod, AcCode, AcSource, DocType, DocNo, DocDate,  PartyTo, OtherPartyName, MastType, CurrencyId, ExRate, Term, Description, LocAmt, 
                      DocAmt,ChqNo as SupplierBillNo,ChqDate as SupplierBillDate FROM XAApPayable";
        sql += " WHERE SequenceId='" + docId + "'";
        DataTable dt = Manager.ORManager.GetDataSet(sql).Tables[0];
        int acYear = 0;
        int acPeriod = 0;
        string docN = "";
        string docType = "";
        string acSource = "";
        string acCode = "";
        decimal locAmt = 0;
        decimal docAmt = 0;
        decimal exRate = 0;
        string currency = "";
        DateTime docDt = DateTime.Today;
        string remarks = "";
        string partyTo = "";
        string supplierBillNo = "";
        string otherPartyName = "";
        DateTime supplierBillDate = new DateTime(1900, 1, 1);
        if (dt.Rows.Count == 1)
        {
            acYear = SafeValue.SafeInt(dt.Rows[0]["AcYear"], 0);
            acPeriod = SafeValue.SafeInt(dt.Rows[0]["AcPeriod"], 0);
            acSource = dt.Rows[0]["AcSource"].ToString();
            acCode = dt.Rows[0]["AcCode"].ToString();
            docN = dt.Rows[0]["DocNo"].ToString();
            docType = dt.Rows[0]["DocType"].ToString();
            locAmt = SafeValue.SafeDecimal(dt.Rows[0]["LocAmt"].ToString(), 0);
            docAmt = SafeValue.SafeDecimal(dt.Rows[0]["DocAmt"].ToString(), 0);
            exRate = SafeValue.SafeDecimal(dt.Rows[0]["ExRate"].ToString(), 0);
            currency = dt.Rows[0]["CurrencyId"].ToString();
            docDt = SafeValue.SafeDate(dt.Rows[0]["DocDate"], new DateTime(1900, 1, 1));
            remarks = dt.Rows[0]["Description"].ToString();
            partyTo = dt.Rows[0]["PartyTo"].ToString();
            otherPartyName = SafeValue.SafeString(dt.Rows[0]["OtherPartyName"],"");
            supplierBillNo = dt.Rows[0]["SupplierBillNo"].ToString();
            supplierBillDate = SafeValue.SafeDate(dt.Rows[0]["SupplierBillDate"], new DateTime(1900, 1, 1));
        }
        Response.Write(string.Format("</br>Begin Post:Bill No:{0} Bill Type:{1}", docN, docType));
        Response.Flush();

        string sqlDet = string.Format("select count(SequenceId) from XAApPayableDet where DocId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;No Detail, Can't Post");
            Response.Flush();
            Response.End();
        }

        //check account period
        if (acYear < 1 || acPeriod < 1)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Account year or Period Invalid!");
            Response.Flush();
            Response.End();
        }

        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
        if (closeInd == "")
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Can't find this account period!");
            Response.Flush();
            Response.End();
        }
        else if (closeInd == "Y")
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;The account period is closed!");
            Response.Flush();
            Response.End();
        }
        //sql = string.Format("select sum(locamt) from XAApPayableDet where DocId='{0}'", docId);
        //decimal amt_det = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='DB' and DocId='" + docId + "'";
        decimal amt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='CR' and DocId='" + docId + "'";
        amt_det -= SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (docAmt != amt_det)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Loc Amount can't match, can't post,Please first resave it!");
            Response.Flush();
            Response.End();
        }
        sql = "select count(LocAmt) from XAApPayableDet where AcCode='' and DocId='" + docId + "'";
        detCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (detCnt > 0)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Some Item's Accode is blank, pls check!");
            Response.Flush();
            Response.End();
        }
        //delete old post data
        sql = string.Format("SELECT SequenceId from XAGlEntry WHERE DocNo='{0}' and DocType='{1}'", docN, docType);
        int glOldOid = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
        if (glOldOid > 0)
        {
            DeleteGl(glOldOid);
        }
     
        int glOid = 0;
        try
        {
            C2.XAGlEntry gl = new XAGlEntry();
            gl.AcPeriod = acPeriod;
            gl.AcYear = acYear;
            gl.ArApInd = "AP";
            gl.DocType = docType;
            gl.DocDate = docDt;
            gl.DocNo = docN;
            gl.CrAmt = docAmt;
            gl.DbAmt = docAmt;
            gl.CurrencyCrAmt = locAmt;
            gl.CurrencyDbAmt = locAmt;
            gl.CurrencyId = currency;
            gl.EntryDate = DateTime.Now;
            gl.ExRate = exRate;
            gl.PostDate = DateTime.Now;
            gl.PostInd = "N";
            gl.Remark = remarks;
            gl.UserId = HttpContext.Current.User.Identity.Name;
            gl.CancelInd = "N";
            gl.CancelDate = new DateTime(1900, 1, 1);
            gl.PartyTo = partyTo;
            gl.OtherPartyName = otherPartyName;
            gl.ChqNo = supplierBillNo;
            gl.ChqDate = supplierBillDate;
            gl.SupplierBillNo = "";
            gl.SupplierBillDate = new DateTime(1900, 1, 1);
            Manager.ORManager.StartTracking(gl, InitialState.Inserted);
            Manager.ORManager.PersistChanges(gl);
            glOid = gl.SequenceId;
            //insert Detail
            OPathQuery query = new OPathQuery(typeof(XAApPayableDet), "DocId='" + docId + "'");
            ObjectSet set = Manager.ORManager.GetObjectSet(query);
            int index = 1;
            XAGlEntryDet det1 = new XAGlEntryDet();

            det1.AcCode = acCode;
            det1.ArApInd = "AP";
            det1.AcPeriod = acPeriod;
            det1.AcSource = acSource;
            det1.AcYear = acYear;
            det1.CrAmt = docAmt;
            det1.CurrencyCrAmt = locAmt;
            det1.DbAmt = 0;
            det1.CurrencyDbAmt = 0;
            det1.CurrencyId = currency;
            det1.DocNo = docN;
            det1.DocType = docType;
            det1.ExRate = exRate;
            det1.GlLineNo = index;
            det1.GlNo = gl.SequenceId;
            det1.Remark = remarks;


            Manager.ORManager.StartTracking(det1, InitialState.Inserted);
            Manager.ORManager.PersistChanges(det1);
            decimal gstCrAmt = 0;
            decimal gstDbAmt = 0;
            string gstAcc = SafeValue.SafeString(Manager.ORManager.ExecuteScalar("SELECT AcCode FROM XXGstAccount where GstSrc='AP'"), "4053");
            for (int i = 0; i < set.Count; i++)
            {
                try
                {
                    index++;
                    XAApPayableDet invDet = set[i] as XAApPayableDet;
                    XAGlEntryDet det = new XAGlEntryDet();

                    if (invDet.AcCode == gstAcc)
                    {
                        if (invDet.AcSource == "DB")
                            gstDbAmt += invDet.LocAmt;
                        else
                            gstCrAmt += invDet.LocAmt;
                    }
                    else
                    {
                        det.AcCode = invDet.AcCode;
                        det.ArApInd = "AP";
                        det.AcPeriod = acPeriod;
                        det.AcSource = invDet.AcSource;
                        det.AcYear = acYear;
                        if (invDet.AcSource == "DB")
                        {
                            det.CrAmt = 0;
                            det.CurrencyCrAmt = 0;
                            det.DbAmt = SafeValue.ChinaRound(SafeValue.ChinaRound(invDet.Qty * invDet.Price, 2) * invDet.ExRate, 2);
                            det.CurrencyDbAmt = SafeValue.ChinaRound(det.DbAmt * exRate, 2);
                            gstDbAmt += invDet.GstAmt;
                        }
                        else
                        {
                            det.DbAmt = 0;
                            det.CurrencyDbAmt = 0;
                            det.CrAmt = SafeValue.ChinaRound(SafeValue.ChinaRound(invDet.Qty * invDet.Price, 2) * invDet.ExRate, 2);
                            det.CurrencyCrAmt = SafeValue.ChinaRound(det.CrAmt * exRate, 2);
                            gstCrAmt += invDet.GstAmt;
                        }
                        det.CurrencyId = invDet.Currency;
                        det.DocNo = docN;
                        det.DocType = docType;
                        det.ExRate = invDet.ExRate;
                        det.GlLineNo = index;
                        det.GlNo = gl.SequenceId;
                        det.Remark = invDet.ChgCode;

                        Manager.ORManager.StartTracking(det, InitialState.Inserted);
                        Manager.ORManager.PersistChanges(det);
                    }
                }
                catch(Exception ex)
                {
                    Response.Write("</br>&nbsp;&nbsp;&nbsp;Posting Error, Please retry!");
                    Response.Write("</br>&nbsp;&nbsp;&nbsp;Error:" + ex.Message);
                    Response.Flush();
                    DeleteGl(glOid);
                    Response.End();
                }
            }
            if (gstDbAmt-gstCrAmt!= 0)
            {
                XAGlEntryDet det = new XAGlEntryDet();

                det.AcCode = gstAcc;
                det.ArApInd = "AP";
                det.AcPeriod = acPeriod;
                det.AcSource = "DB";
                det.AcYear = acYear;
                det.DbAmt = gstDbAmt - gstCrAmt;
                det.CurrencyDbAmt = gstDbAmt - gstCrAmt;
                det.CrAmt = 0;
                det.CurrencyCrAmt = 0;
                det.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                det.DocNo = docN;
                det.DocType = docType;
                det.ExRate = 1;
                det.GlLineNo = index + 1;
                det.GlNo = gl.SequenceId;
                det.Remark = "GST";
                Manager.ORManager.StartTracking(det, InitialState.Inserted);
                Manager.ORManager.PersistChanges(det);
            }
            UpdateAp(docId);
        }
        catch(Exception ex)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Posting Error, Please retry!");
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Error:" + ex.Message);
            Response.Flush();
            DeleteGl(glOid);
            Response.End();
        }
        Response.Write("</br>End Post.");
        Response.Flush();
    }
    #endregion

    #region Post Payment supply invoice
    private void PostApPaySiList(string where)
    {
        string sql = "Select SequenceId,DocNo,DocType,ExportInd from XAApPayment where " + where;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docId = reader["SequenceId"].ToString();
            string postInd = SafeValue.SafeString(reader["ExportInd"], "N");
            if (postInd == "N")
                PostApPaySi(docId);
        }

        // Call Close when done reading.
        reader.Close();
        con.Close();
    }
    private void PostApPaySi(string docId)
    {
        string sql = @"SELECT AcYear, AcPeriod, DocType, DocNo, DocDate, CurrencyId, ExRate, PartyTo, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, CloseInd, ExportInd, 
                      BankRec, BankDate, Remark FROM XAApPayment";
        sql += " WHERE SequenceId='" + docId + "'";
        DataTable dt = Manager.ORManager.GetDataSet(sql).Tables[0];
        int acYear = 0;
        int acPeriod = 0;
        string docN = "";
        string docType = "";
        string acSource = "";
        string acCode = "";
        decimal locAmt = 0;
        decimal docAmt = 0;
        decimal exRate = 0;
        string currency = "";
        DateTime docDt = DateTime.Today;
        string partyTo = "";
        string remarks = "";
        string chqNo = "";
        if (dt.Rows.Count == 1)
        {
            acYear = SafeValue.SafeInt(dt.Rows[0]["AcYear"], 0);
            acPeriod = SafeValue.SafeInt(dt.Rows[0]["AcPeriod"], 0);
            acSource = dt.Rows[0]["AcSource"].ToString();
            acCode = dt.Rows[0]["AcCode"].ToString();
            docN = dt.Rows[0]["DocNo"].ToString();
            docType = dt.Rows[0]["DocType"].ToString();
            locAmt = SafeValue.SafeDecimal(dt.Rows[0]["LocAmt"].ToString(), 0);
            docAmt = SafeValue.SafeDecimal(dt.Rows[0]["DocAmt"].ToString(), 0);
            exRate = SafeValue.SafeDecimal(dt.Rows[0]["ExRate"].ToString(), 0);
            currency = dt.Rows[0]["CurrencyId"].ToString();
            docDt = SafeValue.SafeDate(dt.Rows[0]["DocDate"], new DateTime(1900, 1, 1));
            // partyId = dt.Rows[0][""].ToString();
            remarks = dt.Rows[0]["Remark"].ToString();
            partyTo = dt.Rows[0]["PartyTo"].ToString();
            chqNo = dt.Rows[0]["ChqNo"].ToString();
        }
        Response.Write(string.Format("</br>Begin Post:Bill No:{0} Bill Type:{1}", docN, docType));
        Response.Flush();
        string sqlDet = string.Format("select count(SequenceId) from XAApPaymentDet where PayId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;No Detail, Can't Post");
            Response.Flush();
            Response.End();
        }
        sqlDet = string.Format("select max(docDate) from XAApPaymentDet where PayId='{0}'", docId);
        DateTime maxLineDocDate = SafeValue.SafeDate(Manager.ORManager.ExecuteScalar(sqlDet), new DateTime(1900, 1, 1));
        if (maxLineDocDate > new DateTime(2000, 1, 1) && maxLineDocDate > docDt)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;The bill's Date can't be greater than payment date.");
            Response.Flush();
            Response.End();
        }

        //check account period
        if (acYear < 1 || acPeriod < 1)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Account year or Period Invalid!");
            Response.Flush();
            Response.End();
        }
        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
        if (closeInd == "")
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Can't find this account period!");
            Response.Flush();
            Response.End();
        }
        else if (closeInd == "Y")
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;The account period is closed!");
            Response.Flush();
            Response.End();
        }
        sql = string.Format("select sum(locamt) from XAApPaymentDet where (AcSource = 'DB') AND (PayId = '{0}')",docId);
        decimal amt_detDb = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("select sum(locamt) from XAApPaymentDet where(AcSource = 'CR') AND (PayId = '{0}')", docId);
        decimal amt_detCr = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        if (locAmt != amt_detDb - amt_detCr)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Amount can't match, can't post");
            Response.Flush();
            Response.End();
        }
        sql = "select count(*) from XAApPaymentDet where AcCode='' and PayId='" + docId + "'";
        detCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (detCnt > 0)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Some Item's Accode is blank, pls check!");
            Response.Flush();
            Response.End();
        }
        //delete old post data
        sql = string.Format("SELECT SequenceId from XAGlEntry WHERE DocNo='{0}' and DocType='{1}'", docN, docType);
        int glOldOid = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
        if (glOldOid > 0)
        {
            DeleteGl(glOldOid);
        }

        int glOid = 0;
        try
        {
            C2.XAGlEntry gl = new XAGlEntry();

            gl.AcPeriod = acPeriod;
            gl.AcYear = acYear;
            gl.ArApInd = "AP";
            gl.DocType = docType;
            gl.DocDate = docDt;
            gl.DocNo = docN;
            gl.CrAmt = docAmt;
            gl.CurrencyCrAmt = locAmt;
            gl.DbAmt = docAmt;
            gl.CurrencyDbAmt = locAmt;
            gl.CurrencyId = currency;
            gl.EntryDate = DateTime.Now;
            gl.ExRate = exRate;
            gl.PostDate = DateTime.Now;
            gl.PostInd = "N";
            gl.Remark = remarks;
            gl.UserId = HttpContext.Current.User.Identity.Name;
            gl.CancelInd = "N";
            gl.CancelDate = new DateTime(1900, 1, 1);
            gl.PartyTo = partyTo;
            gl.ChqNo = chqNo;
            gl.SupplierBillNo = "";
            gl.SupplierBillDate = new DateTime(1900, 1, 1);
            Manager.ORManager.StartTracking(gl, InitialState.Inserted);
            Manager.ORManager.PersistChanges(gl);
            glOid = gl.SequenceId;

            //insert Detail
            OPathQuery query = new OPathQuery(typeof(XAApPaymentDet), "PayId='" + docId + "'");
            ObjectSet set = Manager.ORManager.GetObjectSet(query);
            int index = 1;
            XAGlEntryDet det1 = new XAGlEntryDet();

            det1.AcCode = acCode;
            det1.ArApInd = "AP";
            det1.AcPeriod = acPeriod;
            det1.AcSource = acSource;
            det1.AcYear = acYear;
            det1.CrAmt = docAmt;
            det1.CurrencyCrAmt = locAmt;
            det1.DbAmt = 0;
            det1.CurrencyDbAmt = 0;
            det1.CurrencyId = currency;
            det1.DocNo = docN;
            det1.DocType = docType;
            det1.ExRate = exRate;
            det1.GlLineNo = index;
            det1.GlNo = gl.SequenceId;
            det1.Remark = remarks;


            Manager.ORManager.StartTracking(det1, InitialState.Inserted);
            Manager.ORManager.PersistChanges(det1);
            for (int i = 0; i < set.Count; i++)
            {
                try
                {
                    index++;
                    XAApPaymentDet invDet = set[i] as XAApPaymentDet;
                    XAGlEntryDet det = new XAGlEntryDet();

                    det.AcCode = invDet.AcCode;
                    det.ArApInd = "AP";
                    det.AcPeriod = acPeriod;
                    det.AcSource = invDet.AcSource;
                    det.AcYear = acYear;

                    if (invDet.AcSource == "DB")
                    {
                        det.CrAmt = 0;
                        det.CurrencyCrAmt = 0;
                        det.DbAmt = invDet.DocAmt;
                        det.CurrencyDbAmt = invDet.LocAmt;
                    }
                    else //if (invDet.AcSource == "CR")
                    {
                        det.CrAmt = invDet.DocAmt;
                        det.CurrencyCrAmt = invDet.LocAmt;
                        det.DbAmt = 0;
                        det.CurrencyDbAmt = 0;
                    }
                    det.CurrencyId = invDet.Currency;
                    det.DocNo = docN;
                    det.DocType = docType;
                    det.ExRate = invDet.ExRate;
                    det.GlLineNo = index;
                    det.GlNo = gl.SequenceId;
                    det.Remark = invDet.Remark1;


                    Manager.ORManager.StartTracking(det, InitialState.Inserted);
                    Manager.ORManager.PersistChanges(det);
                }
                catch(Exception ex)
                {
                    Response.Write("</br>&nbsp;&nbsp;&nbsp;Posting Error, Please retry!");
                    Response.Write("</br>&nbsp;&nbsp;&nbsp;Error:" + ex.Message);
                    Response.Flush();
                    DeleteGl(glOid);
                    Response.End();
                }
            }
            UpdateApPayment(docId);
        }
        catch(Exception ex)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Posting Error, Please retry!");
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Error:" + ex.Message);
            Response.Flush();
            DeleteGl(glOid);
            Response.End();
        }
        Response.Write("</br>End Post.");
        Response.Flush();
    }
    #endregion

    #region Post Payment Supply Refund
    private void PostApSrList(string where)
    {
        string sql = "Select SequenceId,DocNo,DocType,ExportInd from XAApPayment where " + where;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docId = reader["SequenceId"].ToString();
            string postInd = SafeValue.SafeString(reader["ExportInd"], "N");
            if (postInd == "N")
                PostApSr(docId);
        }

        // Call Close when done reading.
        reader.Close();
        con.Close();
    }
    private void PostApSr(string docId)
    {
        string sql = @"SELECT AcYear, AcPeriod, DocType, DocNo, DocDate, CurrencyId, ExRate, PartyTo, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, CloseInd, ExportInd, 
                      BankRec, BankDate, Remark FROM XAApPayment";
        sql += " WHERE SequenceId='" + docId + "'";
        DataTable dt = Manager.ORManager.GetDataSet(sql).Tables[0];
        int acYear = 0;
        int acPeriod = 0;
        string docN = "";
        string docType = "";
        string acSource = "";
        string acCode = "";
        decimal locAmt = 0;
        decimal docAmt = 0;
        decimal exRate = 0;
        string currency = "";
        DateTime docDt = DateTime.Today;
        string partyTo = "";
        string remarks = "";
        string chqNo = "";
        if (dt.Rows.Count == 1)
        {
            acYear = SafeValue.SafeInt(dt.Rows[0]["AcYear"], 0);
            acPeriod = SafeValue.SafeInt(dt.Rows[0]["AcPeriod"], 0);
            acSource = dt.Rows[0]["AcSource"].ToString();
            acCode = dt.Rows[0]["AcCode"].ToString();
            docN = dt.Rows[0]["DocNo"].ToString();
            docType = dt.Rows[0]["DocType"].ToString();
            locAmt = SafeValue.SafeDecimal(dt.Rows[0]["LocAmt"].ToString(), 0);
            docAmt = SafeValue.SafeDecimal(dt.Rows[0]["DocAmt"].ToString(), 0);
            exRate = SafeValue.SafeDecimal(dt.Rows[0]["ExRate"].ToString(), 0);
            currency = dt.Rows[0]["CurrencyId"].ToString();
            docDt = SafeValue.SafeDate(dt.Rows[0]["DocDate"], new DateTime(1900, 1, 1));
            remarks = dt.Rows[0]["Remark"].ToString();
            partyTo = dt.Rows[0]["PartyTo"].ToString();
            chqNo = dt.Rows[0]["ChqNo"].ToString();
        }
        Response.Write(string.Format("</br>Begin Post:Bill No:{0} Bill Type:{1}", docN, docType));
        Response.Flush();
        string sqlDet = string.Format("select count(SequenceId) from XAApPaymentDet where PayId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;No Detail, Can't Post");
            Response.Flush();
            Response.End();
        }
        sqlDet = string.Format("select max(docDate) from XAApPaymentDet where PayId='{0}'", docId);
        DateTime maxLineDocDate = SafeValue.SafeDate(Manager.ORManager.ExecuteScalar(sqlDet), new DateTime(1900, 1, 1));
        if (maxLineDocDate > new DateTime(2000, 1, 1) && maxLineDocDate > docDt)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;The bill's Date can't be greater than payment date.");
            Response.Flush();
            Response.End();
        }

        //check account period
        if (acYear < 1 || acPeriod < 1)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Account year or Period Invalid!");
            Response.Flush();
            Response.End();
        }
        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
        if (closeInd == "")
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Can't find this account period!");
            Response.Flush();
            Response.End();
        }
        else if (closeInd == "Y")
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;The account period is closed!");
            Response.Flush();
            Response.End();
        }
        //sql = string.Format("select sum(locamt) from XAApPaymentDet where PayId='{0}'", docId);
        //decimal amt_det = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("select sum(locamt) from XAApPaymentDet where (AcSource = 'CR') AND (PayId = '{0}')", docId);
        decimal amt_det = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("select sum(locamt) from XAApPaymentDet where(AcSource = 'DB') AND (PayId = '{0}')", docId);
        amt_det -= SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        if (locAmt != amt_det)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Amount can't match, can't post");
            Response.Flush();
            Response.End();
        }
        sql = "select count(*) from XAApPaymentDet where AcCode='' and PayId='" + docId + "'";
        detCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (detCnt > 0)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Some Item's Accode is blank, pls check!");
            Response.Flush();
            Response.End();
        }
        //delete old post data
        sql = string.Format("SELECT SequenceId from XAGlEntry WHERE DocNo='{0}' and DocType='{1}'", docN, docType);
        int glOldOid = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
        if (glOldOid > 0)
        {
            DeleteGl(glOldOid);
        }

        int glOid = 0;
        try
        {
            C2.XAGlEntry gl = new XAGlEntry();

            gl.AcPeriod = acPeriod;
            gl.AcYear = acYear;
            gl.ArApInd = "AP";
            gl.DocType = docType;
            gl.DocDate = docDt;
            gl.DocNo = docN;
            gl.CrAmt = docAmt;
            gl.CurrencyCrAmt = locAmt;
            gl.DbAmt = docAmt;
            gl.CurrencyDbAmt = locAmt;
            gl.CurrencyId = currency;
            gl.EntryDate = DateTime.Now;
            gl.ExRate = exRate;
            gl.PostDate = DateTime.Now;
            gl.PostInd = "N";
            gl.Remark = remarks;
            gl.UserId = HttpContext.Current.User.Identity.Name;
            gl.CancelInd = "N";
            gl.CancelDate = new DateTime(1900, 1, 1);
            gl.PartyTo = partyTo;
            gl.ChqNo = chqNo;
            gl.SupplierBillNo = "";
            gl.SupplierBillDate = new DateTime(1900, 1, 1);
            Manager.ORManager.StartTracking(gl, InitialState.Inserted);
            Manager.ORManager.PersistChanges(gl);
            glOid = gl.SequenceId;

            //insert Detail
            OPathQuery query = new OPathQuery(typeof(XAApPaymentDet), "PayId='" + docId + "'");
            ObjectSet set = Manager.ORManager.GetObjectSet(query);
            int index = 1;
            XAGlEntryDet det1 = new XAGlEntryDet();

            det1.AcCode = acCode;
            det1.ArApInd = "AP";
            det1.AcPeriod = acPeriod;
            det1.AcSource = acSource;
            det1.AcYear = acYear;
            det1.DbAmt = docAmt;
            det1.CurrencyDbAmt = locAmt;
            det1.CrAmt = 0;
            det1.CurrencyCrAmt = 0;
            det1.CurrencyId = currency;
            det1.DocNo = docN;
            det1.DocType = docType;
            det1.ExRate = exRate;
            det1.GlLineNo = index;
            det1.GlNo = gl.SequenceId;
            det1.Remark = remarks;


            Manager.ORManager.StartTracking(det1, InitialState.Inserted);
            Manager.ORManager.PersistChanges(det1);
            for (int i = 0; i < set.Count; i++)
            {
                try
                {
                    index++;
                    XAApPaymentDet invDet = set[i] as XAApPaymentDet;
                    XAGlEntryDet det = new XAGlEntryDet();

                    det.AcCode = invDet.AcCode;
                    det.ArApInd = "AP";
                    det.AcPeriod = acPeriod;
                    det.AcSource = invDet.AcSource;
                    det.AcYear = acYear;
                    if (invDet.AcSource == "DB")
                    {
                        det.CrAmt = 0;
                        det.CurrencyCrAmt = 0;
                        det.DbAmt = invDet.DocAmt;
                        det.CurrencyDbAmt = invDet.LocAmt;
                    }
                    else
                    {
                        det.DbAmt = 0;
                        det.CurrencyDbAmt = 0;
                        det.CrAmt = invDet.DocAmt;
                        det.CurrencyCrAmt = invDet.LocAmt;
                    }
                    det.CurrencyId = invDet.Currency;
                    det.DocNo = docN;
                    det.DocType = docType;
                    det.ExRate = invDet.ExRate;
                    det.GlLineNo = index;
                    det.GlNo = gl.SequenceId;
                    det.Remark = invDet.Remark1;


                    Manager.ORManager.StartTracking(det, InitialState.Inserted);
                    Manager.ORManager.PersistChanges(det);
                }
                catch(Exception ex)
                {
                    Response.Write("</br>&nbsp;&nbsp;&nbsp;Posting Error, Please retry!");
                    Response.Write("</br>&nbsp;&nbsp;&nbsp;Error:" + ex.Message);
                    Response.Flush();
                    DeleteGl(glOid);
                    Response.End();
                }
            }
            UpdateApPayment(docId);
        }
        catch(Exception ex)
        {
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Posting Error, Please retry!");
            Response.Write("</br>&nbsp;&nbsp;&nbsp;Error:" + ex.Message);
            Response.Flush();
            DeleteGl(glOid);
            Response.End();
        }
        Response.Write("</br>End Post.");
        Response.Flush();
    }
    #endregion
    private void DeleteGl(int glOid)
    {
        string sql_delete = "delete from XAGlEntry where SequenceId= '" + glOid + "'";
        int m = Manager.ORManager.ExecuteCommand(sql_delete);
        sql_delete = "delete from XAGlEntryDet where GlNo= '" + glOid + "'";
        m = Manager.ORManager.ExecuteCommand(sql_delete);
    }
    private void UpdateAp(string docId)
    {
        string sql_invoice = "update XAApPayable set ExportInd='Y' where SequenceId='" + docId + "'";
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }
    private void UpdateApPayment(string docId)
    {
        string sql_invoice = "update XAApPayment set ExportInd='Y' where SequenceId='" + docId + "'";
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }
}
