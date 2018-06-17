using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using C2;
using Wilson.ORMapper;

public partial class BatchPost : BasePage
{
    public string site = "SqlConnectString1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
            cbo_DocType.Text = "IV";
        }
    }
    #region search
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string docType = this.cbo_DocType.Text;
        string doc1 = this.txt_Doc1.Text.Trim();
        string doc2 = this.txt_Doc1.Text.Trim();
        string date1 = this.txt_from.Date.ToString("yyyy-MM-dd");
        string date2 = this.txt_end.Date.ToString("yyyy-MM-dd");

        string sql_base = "";
        string where = " where ExportInd='N' and isnull(CancelInd,'N')='N' And DocType='" + docType + "' ";
        if (doc1 != "" && doc2 != "")
            where = where + " AND Doc>='" + doc1 + "' AND DocNo <= '" + doc2 + "' ";
        else
            where = where + string.Format(" AND DocDate >= '{0:yyyy-MM-dd}' AND DocDate <= '{1:yyyy-MM-dd}' ", date1, date2);

        if (docType == "IV" || docType == "DN" || docType == "CN")
            sql_base = "select SequenceId, DocNo, DocType, DocDate, AcCode, ' ' as OtherDoc, CurrencyId as Currency, ExRate, MastRefNo as RefNo, JobRefNo as JobNo, MastType as JobType, DocAmt, LocAmt from XaArInvoice ";
        if (docType == "RE")
            sql_base = "select SequenceId, DocNo, DocType, DocDate, AcCode, ' ' as OtherDoc, DocCurrency as Currency, DocExRate as ExRate, ' ' as RefNo, ' ' as JobNo, ' ' as JobType, DocAmt, LocAmt from XaArReceipt ";
        if (docType == "PL" || docType == "SD" || docType == "SC" || docType == "VO")
            sql_base = "select SequenceId, DocNo, DocType, DocDate, AcCode, SupplierBillNo as OtherDoc,CurrencyId as Currency, ExRate, MastRefNo as RefNo, JobRefNo as JobNo, MastType as JobType, DocAmt, LocAmt from XaApPayable ";
        if (docType == "PS")
            sql_base = "select SequenceId, DocNo, DocType, DocDate, AcCode, ' ' as OtherDoc,CurrencyId as Currency, ExRate, ' ' as RefNo, ' ' as JobNo, ' ' as JobType, DocAmt, LocAmt from XaApPayment ";

        //throw new Exception(sql_base + where);
        DataTable master = ConnectSql.GetTab(sql_base + where);
        this.grd.DataSource = master;
        this.grd.DataBind();

    }
    #endregion
    protected void grd_Det_BeforePerformDataSelect(object sender, EventArgs e)
    {

    }

    protected void grd_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string trxNo = e.Parameters;
        if (trxNo.Length > 2 && (trxNo.Substring(0, 2) == "BP"))
        {
            string doctype = trxNo.Substring(2, 2);
            string docnos = trxNo.Substring(4);

            string ret = PostBatch(doctype, docnos);
            e.Result = ret;
        }

    }

    public string PostBatch(string doctype, string docnos)
    {
        string[] docs = docnos.Split(new char[] { ',' });
        int done = 0;
        int all = 0;
        for (int i = 0; i < docs.Length; i++)
        {
            string _doc = docs[i].Trim();
            if (_doc != "")
            {
                all++;
                string _ret = PostOne(doctype, _doc);
                if (_ret == "")
                    done++;
            }
        }
        return string.Format("Total Posted ({2}) : {0} / {1}", done, all, doctype);
    }

    private string PostOne(string doctype, string docno)
    {
        string s = "";
        if (doctype == "IV" || doctype == "DN" || doctype == "CN")
        {
            s = PostArIv(docno);
        }
        else if (doctype == "PL" || doctype == "SD" || doctype == "SC")
        {
            s = PostApInv(docno);
        }
        else if (doctype == "VO")
        {
            s = PostApVo(docno);
        }
        else if (doctype == "RE")
        {
            s = PostArRec(docno);
        }
        else if (doctype == "PS")
        {
            s = PostApPay(docno);
        }
        return s;
    }

    private string PostArIv(string docId)
    {
        string sql = @"SELECT  AcYear, AcPeriod, AcCode, AcSource, DocType, DocNo, DocDate, DocDueDate, PartyTo, MastType, CurrencyId, ExRate, 
                      Term, Description, DocAmt, LocAmt
FROM         XAArInvoice";
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
        }
        string _err = "";
        string sqlDet = string.Format("select count(SequenceId) from XAArInvoiceDet where DocId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            _err = docN + ": No Detail";
            return _err;
        }
        else
        {

            string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
            string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
            if (closeInd == "")
            {
                _err = docN + ": Invalid Account Period";
                return _err;
            }
            else if (closeInd == "Y")
            {
                _err = docN + ": Account Period Closed";
                return _err;
            }

            if (_err == "")
            {
                sql = "select SUM(LocAmt) from XAArInvoiceDet where AcSource='CR' and DocId='" + docId + "'";
                decimal amt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                sql = "select SUM(LocAmt) from XAArInvoiceDet where AcSource='DB' and DocId='" + docId + "'";
                amt_det -= SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);

                if (docAmt != amt_det)
                {
                    _err = docN + ": Amount Mismatch, Please Resave";
                    return _err;
                }
                else
                {
                    //delete old post data
                    sql = string.Format("SELECT SequenceId from XAGlEntry WHERE DocNo='{0}' and DocType='{1}'", docN, docType);
                    int glOldOid = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
                    if (glOldOid > 0)
                    {
                        DeleteGl(glOldOid);
                    }

                    //Insert into gl entry
                    int glOid = 0;
                    try
                    {
                        C2.XAGlEntry gl = new XAGlEntry();
                        gl.AcPeriod = acPeriod;
                        gl.AcYear = acYear;
                        gl.ArApInd = "AR";
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
                        gl.SupplierBillNo = "";
                        gl.SupplierBillDate = new DateTime(1900, 1, 1);
                        Manager.ORManager.StartTracking(gl, InitialState.Inserted);
                        Manager.ORManager.PersistChanges(gl);
                        glOid = gl.SequenceId;

                        OPathQuery query = new OPathQuery(typeof(XAArInvoiceDet), "DocId='" + docId + "'");
                        ObjectSet set = Manager.ORManager.GetObjectSet(query);
                        int index = 1;
                        XAGlEntryDet det1 = new XAGlEntryDet();

                        det1.AcCode = acCode;
                        det1.ArApInd = "AR";
                        det1.AcPeriod = acPeriod;
                        det1.AcSource = acSource;
                        det1.AcYear = acYear;
                        det1.CrAmt = 0;
                        det1.CurrencyCrAmt = 0;
                        det1.DbAmt = docAmt;
                        det1.CurrencyDbAmt = locAmt;
                        det1.CurrencyId = currency;
                        det1.DocNo = docN;
                        det1.DocType = docType;
                        det1.ExRate = exRate;
                        det1.GlLineNo = index;
                        det1.GlNo = glOid;//gl.GlNo;
                        det1.Remark = remarks;


                        Manager.ORManager.StartTracking(det1, InitialState.Inserted);
                        Manager.ORManager.PersistChanges(det1);
                        decimal gstCrAmt = 0;
                        decimal gstDbAmt = 0;
                        string gstAcc = SafeValue.SafeString(Manager.ORManager.ExecuteScalar("SELECT AcCode FROM XXGstAccount where GstSrc='AR'"), "2036");
                        for (int i = 0; i < set.Count; i++)
                        {
                            try
                            {
                                index++;
                                XAArInvoiceDet invDet = set[i] as XAArInvoiceDet;
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
                                    det.ArApInd = "AR";
                                    det.AcPeriod = acPeriod;
                                    det.AcSource = invDet.AcSource;
                                    det.AcYear = acYear;
                                    if (invDet.AcSource == "CR")
                                    {
                                        det.CrAmt = invDet.LocAmt - invDet.GstAmt;//SafeValue.ChinaRound(SafeValue.ChinaRound(invDet.Qty * invDet.Price, 2) * invDet.ExRate, 2);
                                        det.CurrencyCrAmt = invDet.LineLocAmt - invDet.GstAmt;//SafeValue.ChinaRound(det.CrAmt * exRate, 2);
                                        det.DbAmt = 0;
                                        det.CurrencyDbAmt = 0;
                                        gstCrAmt += invDet.GstAmt;
                                    }
                                    else
                                    {
                                        det.DbAmt = invDet.LocAmt - invDet.GstAmt;//SafeValue.ChinaRound(SafeValue.ChinaRound(invDet.Qty * invDet.Price, 2) * invDet.ExRate, 2);
                                        det.CurrencyDbAmt = invDet.LineLocAmt - invDet.GstAmt;//SafeValue.ChinaRound(det.DbAmt * exRate, 2);
                                        det.CrAmt = 0;
                                        det.CurrencyCrAmt = 0;
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
                                _err = docN + ": Posting Error";
                                DeleteGl(glOid);
                                return _err;
                            }
                        }
                        if (gstCrAmt - gstDbAmt != 0)
                        {
                            XAGlEntryDet det = new XAGlEntryDet();

                            det.AcCode = gstAcc;
                            det.ArApInd = "AR";
                            det.AcPeriod = acPeriod;
                            det.AcSource = "CR";
                            det.AcYear = acYear;
                            det.CrAmt = gstCrAmt - gstDbAmt;
                            det.CurrencyCrAmt = gstCrAmt - gstDbAmt;
                            det.DbAmt = 0;
                            det.CurrencyDbAmt = 0;
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
                        UpdatePost("XaArInvoice", docId);
                    }
                    catch (Exception ex)
                    {
                        _err = docN + ": Posting Error";
                        DeleteGl(glOid);
                        return _err;
                    }
                }
            }
        }
        return _err;
    }


    private string PostArRec(string docId)
    {
        string sql = @"SELECT AcYear, AcPeriod, DocType, DocNo, DocDate, DocCurrency, DocExRate, PartyTo, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, ExportInd, 
                      BankName, Remark FROM  XAArReceipt";
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
        string chqNo = "";
        string _err = "";
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
            exRate = SafeValue.SafeDecimal(dt.Rows[0]["DocExRate"].ToString(), 0);
            currency = dt.Rows[0]["DocCurrency"].ToString();
            docDt = SafeValue.SafeDate(dt.Rows[0]["DocDate"], new DateTime(1900, 1, 1));
            // partyId = dt.Rows[0][""].ToString();
            remarks = dt.Rows[0]["Remark"].ToString();
            partyTo = dt.Rows[0]["PartyTo"].ToString();
            chqNo = dt.Rows[0]["ChqNo"].ToString();
        }
        string sqlDet = string.Format("select count(SequenceId) from XAArReceiptDet where RepId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            _err = "No Detail";
            return _err;
        }
        else
        {
            sqlDet = string.Format("select max(docDate) from XAArReceiptDet where RepId='{0}'", docId);
            DateTime maxLineDocDate = SafeValue.SafeDate(Manager.ORManager.ExecuteScalar(sqlDet), new DateTime(1900, 1, 1));
            if (maxLineDocDate > new DateTime(2000, 1, 1) && maxLineDocDate > docDt)
            {
                _err = "The bill's Date can't be greater than receipt date.";
                return _err;
            }
        }

        //check account period
        if (acYear < 1 || acPeriod < 1)
        {
            _err = "Account year or Period Invalid!";
            return _err;
        }
        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
        if (closeInd == "")
        {
            _err = "Can't find this account period!";
            return _err;
        }
        else if (closeInd == "Y")
        {
            _err = "The account period is closed!";
            return _err;
        }

        sql = string.Format("select sum(locamt) from XAArReceiptDet where RepId='{0}' and AcSource='CR'", docId);
        decimal amt_detCr = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("select sum(locamt) from XAArReceiptDet where RepId='{0}' and AcSource='DB'", docId);
        decimal amt_detDb = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        if (locAmt != amt_detCr - amt_detDb)
        {
            _err = "Amount can't match, can't post!";
            return _err;
        }
        sql = "select count(LocAmt) from XAArReceiptDet where AcCode='' and RepId='" + docId + "'";
        detCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (detCnt > 0)
        {
            _err = "Some Item's Accode is blank, pls check!";
            return _err;
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
            gl.ArApInd = "AR";
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
            gl.ChqNo = chqNo;
            gl.SupplierBillNo = "";
            gl.SupplierBillDate = new DateTime(1900, 1, 1);
            Manager.ORManager.StartTracking(gl, InitialState.Inserted);
            Manager.ORManager.PersistChanges(gl);
            glOid = gl.SequenceId;

            //insert Detail
            OPathQuery query = new OPathQuery(typeof(XAArReceiptDet), "RepId='" + docId + "'");
            ObjectSet set = Manager.ORManager.GetObjectSet(query);
            int index = 1;
            XAGlEntryDet det1 = new XAGlEntryDet();

            det1.AcCode = acCode;
            det1.ArApInd = "AR";
            det1.AcPeriod = acPeriod;
            det1.AcSource = acSource;
            det1.AcYear = acYear;
            det1.CrAmt = 0;
            det1.CurrencyCrAmt = 0;
            det1.DbAmt = docAmt;
            det1.CurrencyDbAmt = locAmt;
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
                    XAArReceiptDet invDet = set[i] as XAArReceiptDet;
                    XAGlEntryDet det = new XAGlEntryDet();

                    det.AcCode = invDet.AcCode;
                    det.ArApInd = "AR";
                    det.AcPeriod = acPeriod;
                    det.AcSource = invDet.AcSource;
                    det.AcYear = acYear;
                    if (det.AcSource == "DB")
                    {
                        det.DbAmt = invDet.DocAmt;
                        det.CurrencyDbAmt = invDet.LocAmt;
                        det.CrAmt = 0;
                        det.CurrencyCrAmt = 0;
                    }
                    else if (det.AcSource == "CR")
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
                catch (Exception ex)
                {
                    _err = "Posting Error, Please retry!";
                    DeleteGl(glOid);
                    return _err;
                }
            }
            UpdatePost("XaArReceipt", docId);
        }
        catch (Exception ex)
        {
            _err = "Posting Error, Please retry!";
            DeleteGl(glOid);
            return _err;
        }
        return _err;
    }

    private string PostApInv(string docId)
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
        string _err = "";
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

        string sqlDet = string.Format("select count(SequenceId) from XAApPayableDet where DocId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            _err = docN + ": No Detail";
            return _err;
        }
        //check account period
        if (acYear < 1 || acPeriod < 1)
        {
            _err = docN + ": Account year or Period Invalid!";
            return _err;
        }

        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
        if (closeInd == "")
        {
            _err = docN + ": Can't find this account period!";
            return _err;
        }
        else if (closeInd == "Y")
        {
            _err = docN + ": The account period is closed!";
            return _err;
        }

        sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='DB' and DocId='" + docId + "'";
        decimal amt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='CR' and DocId='" + docId + "'";
        amt_det -= SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (docType == "SC")
            amt_det = -amt_det;

        if (docAmt != amt_det)
        {
            _err = docN + ": Loc Amount can't match, can't post,Please first resave it!";
            return _err;
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
                            det.CrAmt = invDet.LocAmt - invDet.GstAmt;
                            det.CurrencyCrAmt = SafeValue.ChinaRound(det.CrAmt * exRate, 2);
                            det.DbAmt = 0;
                            det.CurrencyDbAmt = 0;
                            gstCrAmt += invDet.GstAmt;
                        }
                        else
                        {
                            det.CrAmt = 0;
                            det.CurrencyCrAmt = 0;
                            det.DbAmt = invDet.LocAmt - invDet.GstAmt;
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
                    _err = docN + ": Posting Error";
                    DeleteGl(glOid);
                    return _err;
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
            UpdatePost("XaApPayable",docId);
        }
        catch (Exception ex)
        {
            _err = docN + ": Posting Error";
            DeleteGl(glOid);
            return _err;
        }
        return _err;
    }
    private string PostApVo(string docId)
    {
        string sql = @"SELECT AcYear, AcPeriod, AcCode, AcSource, DocType, DocNo, DocDate,  PartyTo, OtherPartyName, MastType, CurrencyId, ExRate, Term, Description, LocAmt, 
                      DocAmt,ChqNo,SupplierBillNo,ChqDate as SupplierBillDate FROM XAApPayable";
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
        string chequeNo = "";
        string _err = "";
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
            otherPartyName = SafeValue.SafeString(dt.Rows[0]["OtherPartyName"], "");
            supplierBillNo = dt.Rows[0]["SupplierBillNo"].ToString();
            supplierBillDate = SafeValue.SafeDate(dt.Rows[0]["SupplierBillDate"], new DateTime(1900, 1, 1));
            chequeNo = dt.Rows[0]["ChqNo"].ToString();
        }

        if (chequeNo.Length == 0)
        {
            _err = docN + ": No Cheque No, Can't Post";
            return _err;
        }
        string sqlDet = string.Format("select count(SequenceId) from XAApPayableDet where DocId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            _err = docN + ": No Detail, Can't Post";
            return _err;
        }

        //check account period
        if (acYear < 1 || acPeriod < 1)
        {
            _err = docN + ": Account year or Period Invalid!";
            return _err;
        }

        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
        if (closeInd == "")
        {
            _err = docN + ": Can't find this account period!";
            return _err;
        }
        else if (closeInd == "Y")
        {
            _err = docN + ": The account period is closed!";
            return _err;
        }
        //sql = string.Format("select sum(locamt) from XAApPayableDet where DocId='{0}'", docId);
        //decimal amt_det = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='DB' and DocId='" + docId + "'";
        decimal amt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='CR' and DocId='" + docId + "'";
        amt_det -= SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (docAmt != amt_det)
        {
            _err = docN + ": Loc Amount can't match, can't post,Please first resave it!";
            return _err;
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
            gl.ChqNo = chequeNo;
            gl.ChqDate = supplierBillDate;
            gl.SupplierBillNo = supplierBillNo;
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
                            det.DbAmt = invDet.LocAmt - invDet.GstAmt;
                            det.CurrencyDbAmt = SafeValue.ChinaRound(det.DbAmt * exRate, 2);
                            gstDbAmt += invDet.GstAmt;
                        }
                        else
                        {
                            det.DbAmt = 0;
                            det.CurrencyDbAmt = 0;
                            det.CrAmt = invDet.LocAmt - invDet.GstAmt;
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
                catch (Exception ex)
                {
                    _err = docN + ": Posting Error";
                    DeleteGl(glOid);
                    return _err;
                }
            }
            if (gstDbAmt - gstCrAmt != 0)
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
            UpdatePost("XaApPayable",docId);
        }
        catch (Exception ex)
        {
            DeleteGl(glOid);
            _err = docN + ": Posting Error";
            return _err;
        }
        return _err;
    }
    private string PostApPay(string docId)
    {
        string sql = @"SELECT AcYear, AcPeriod, DocType, DocNo, DocDate, CurrencyId, ExRate, PartyTo, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, CloseInd, ExportInd, 
                      BankRec, BankDate, Remark,GenerateInd FROM XAApPayment";
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
        string generateInd = "";
        string _err = "";
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
            generateInd = SafeValue.SafeString(dt.Rows[0]["GenerateInd"]);
        }
        if (generateInd != "Y")
        {
            _err = docN + ": Have not generate no, Can't Post";
            return _err;
        }
        string sqlDet = string.Format("select count(SequenceId) from XAApPaymentDet where PayId='{0}'", docId);
        int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
        if (detCnt == 0)
        {
            _err = docN + ": No Detail";
            return _err;
        }
        sqlDet = string.Format("select max(docDate) from XAApPaymentDet where PayId='{0}'", docId);
        DateTime maxLineDocDate = SafeValue.SafeDate(Manager.ORManager.ExecuteScalar(sqlDet), new DateTime(1900, 1, 1));
        if (maxLineDocDate > new DateTime(2000, 1, 1) && maxLineDocDate > docDt)
        {
            _err = docN + ": The bill's Date can't be greater than payment date.";
            return _err;
        }

        //check account period
        if (acYear < 1 || acPeriod < 1)
        {
            _err = docN + ": Account year or Period Invalid!";
            return _err;
        }
        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
        if (closeInd == "")
        {
            _err = docN + ": Can't find this account period!";
            return _err;
        }
        else if (closeInd == "Y")
        {
            _err = docN + ": The account period is closed!";
            return _err;
        }
        sql = string.Format("select sum(locamt) from XAApPaymentDet where (AcSource = 'DB') AND (PayId = '{0}')", docId);
        decimal amt_detDb = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("select sum(locamt) from XAApPaymentDet where(AcSource = 'CR') AND (PayId = '{0}')", docId);
        decimal amt_detCr = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
        if (locAmt != amt_detDb - amt_detCr)
        {
            _err = docN + ": Amount can't match, can't post";
            return _err;
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
                catch (Exception ex)
                {
                    _err = docN + ": Posting Error";
                    DeleteGl(glOid);
                    return _err;
                }
            }
            UpdatePost("XaApPayment",docId);
        }
        catch (Exception ex)
        {
            _err = docN + ": Posting Error";
            DeleteGl(glOid);
            return _err;
        }
        return _err;
    }


    public void UpdatePost(string table, string docid)
    {
        try
        {
            string sql = string.Format("Update {0} Set ExportInd='Y' where SequenceId={1}", table, docid);
            ConnectSql.ExecuteSql(sql);
        }
        catch { }
    }

    private void DeleteGl(int glOid)
    {
        string sql_delete = "delete from XAGlEntry where SequenceId= '" + glOid + "'";
        int m = Manager.ORManager.ExecuteCommand(sql_delete);
        sql_delete = "delete from XAGlEntryDet where GlNo= '" + glOid + "'";
        m = Manager.ORManager.ExecuteCommand(sql_delete);
    }
}
