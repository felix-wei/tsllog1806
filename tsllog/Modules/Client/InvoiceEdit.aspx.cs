using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class Modules_Client_InvoiceEdit : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["InvoiceEditWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                string docType = "IV";
                if (Request.QueryString["type"] != null)
                    docType = Request.QueryString["type"].ToString();
                this.txt_DocType.Text = docType;
                Session["InvoiceEditWhere"] = "DocType='" + docType + "' and DocNo='" + Request.QueryString["no"] + "'";
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                string docType = "IV";
                if (Request.QueryString["type"] != null)
                    docType = Request.QueryString["type"].ToString();
                this.txt_DocType.Text = docType;
                if (Session["InvoiceEditWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsArInvoice.FilterExpression = "1=0";
        }
        if (Session["InvoiceEditWhere"] != null)
        {
            this.dsArInvoice.FilterExpression = Session["InvoiceEditWhere"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region invoice
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.XAArInvoice));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string[] currentPeriod = EzshipHelper.GetAccPeriod(DateTime.Today);
        string acYear = currentPeriod[1];
        string acPeriod = currentPeriod[0];

        e.NewValues["AcYear"] = acYear;
        e.NewValues["AcPeriod"] = acPeriod;
        e.NewValues["DocDate"] = DateTime.Today;
        e.NewValues["DocDueDate"] = DateTime.Today;
        e.NewValues["DocType"] = this.txt_DocType.Text;
        e.NewValues["MastType"] = "I";
        e.NewValues["AcSource"] = "DB";
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = new decimal(1.0);
        e.NewValues["Term"] = "CASH";
        e.NewValues["Eta"] = new DateTime(1900, 1, 1);
    }

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        Save();
    }
    private void Save()
    {
        ASPxTextBox invNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

        ASPxComboBox partyTo = this.ASPxGridView1.FindEditFormTemplateControl("cmb_PartyTo") as ASPxComboBox;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        ASPxDateEdit docDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
        ASPxMemo remarks1 = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remarks1") as ASPxMemo;
        ASPxComboBox termId = this.ASPxGridView1.FindEditFormTemplateControl("txt_TermId") as ASPxComboBox;
        ASPxDateEdit dueDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_DueDt") as ASPxDateEdit;
        ASPxButtonEdit docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
        ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocExRate") as ASPxSpinEdit;
        ASPxTextBox acCode = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcCode") as ASPxTextBox;
        ASPxComboBox acSource = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcSource") as ASPxComboBox;
        ASPxTextBox txt_MastRefNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxComboBox cbo_DocCate = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocCate") as ASPxComboBox;
        string invN = docN.Text;
        C2.XAArInvoice inv = Manager.ORManager.GetObject(typeof(XAArInvoice), SafeValue.SafeInt(invNCtr.Text, 0)) as XAArInvoice;
        if (inv == null)// first insert invoice
        {
            string counterType = "AR-IV";
            if (docType.Value.ToString() == "DN")
                counterType = "AR-DN";

            inv = new XAArInvoice();
            inv.DocType = docType.Value.ToString();
            inv.DocDate = docDate.Date;
            invN = C2Setup.GetNextNo(inv.DocType, counterType, inv.DocDate);
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
            inv.DocNo = invN.ToString();
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);
            inv.Term = termId.Text;
            //
            int dueDay = SafeValue.SafeInt(termId.Text.ToUpper().Replace("DAYS", "").Trim(), 0);
            inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
            inv.Description = remarks1.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;

            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId); ;
            inv.AcSource = acSource.Value.ToString();

            inv.MastRefNo = "0";
            inv.JobRefNo = "0";
            inv.MastType = "";
            inv.ExportInd = "N";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.EntryDate = DateTime.Now;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";
            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo("", counterType, invN, inv.DocDate);
            }
            catch
            {
            }
        }
        else
        {
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");

            inv.Term = termId.Text;
            inv.DocDate = docDate.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);

            int dueDay = SafeValue.SafeInt(termId.Text.ToUpper().Replace("DAYS", "").Trim(), 0);
            inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
            inv.Description = remarks1.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;

            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
            inv.AcSource = acSource.Text;
            inv.MastRefNo = txt_MastRefNo.Text;
            inv.MastType = SafeValue.SafeString(cbo_DocCate.Value);
            try
            {
                Manager.ORManager.StartTracking(inv, InitialState.Updated);
                Manager.ORManager.PersistChanges(inv);
            }
            catch
            {
            }
            UpdateMaster(inv.SequenceId);
        }
        Session["InvoiceEditWhere"] = "SequenceId=" + inv.SequenceId;
        this.dsArInvoice.FilterExpression = Session["InvoiceEditWhere"].ToString();
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);
    }
    #endregion

    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.ASPxGridView1.EditingRowVisibleIndex > -1)
        {
            string oid = SafeValue.SafeString(this.ASPxGridView1.GetRowValues(this.ASPxGridView1.EditingRowVisibleIndex, new string[] { "SequenceId" }));
            PayTab(SafeValue.SafeInt(oid, 0));

            string sql = string.Format("select CancelInd from XAArInvoice  where SequenceId='{0}'", oid);
            string cancelInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
            ASPxButton btn_void = this.ASPxGridView1.FindEditFormTemplateControl("btn_void") as ASPxButton;
            if (cancelInd == "N")
            {
                btn_void.Text = "Void";
            }
            else
            {
                btn_void.Text = "Unvoid";
            }

        }
    }

    #region invoice det
    protected void grid_InvDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.XAArInvoiceDet));
    }
    protected void grid_InvDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        this.dsArInvoiceDet.FilterExpression = "DocId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_InvDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DocLineNo"] = 0;
        ASPxButtonEdit docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
        e.NewValues["Currency"] = docCurr.Text;
        //e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["Qty"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["GstType"] = "Z";
        e.NewValues["AcSource"] = "CR";
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["AcCode"]) == "")
            throw new Exception("Please enter the account code info");
        ASPxGridView grd = sender as ASPxGridView;
        string sql_detCnt = "select count(DocId) from XAArInvoiceDet where DocId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        //e.NewValues["CostingId"] = "";
        e.NewValues["DocId"] = SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        e.NewValues["DocLineNo"] = lineNo;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        e.NewValues["DocNo"] = docN.Text;
        e.NewValues["DocType"] = docType.Text;
        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;
        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        e.NewValues["MastType"] = "";
        e.NewValues["MastRefNo"] = "0";
        e.NewValues["JobRefNo"] = "0";
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;

        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
    }
    protected void grid_InvDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_InvDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxTextBox docId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(SafeValue.SafeInt(docId.Text, 0));
    }
    protected void grid_InvDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxTextBox docId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(SafeValue.SafeInt(docId.Text, 0));
    }
    protected void grid_InvDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxTextBox docId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(SafeValue.SafeInt(docId.Text, 0));
    }
    private void UpdateMaster(int docId)
    {
        C2.XAArInvoice.update_invoice_mast(docId);
    }
    #endregion

    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string filter = e.Parameters;
        string[] _filter = filter.Split(',');
        string p = _filter[0];
        string s = _filter[1];
        ASPxTextBox docId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

        //get informations from arinvoice
        if (p.ToUpper() == "P")
        {
            #region Post
            string sql = @"SELECT  AcYear, AcPeriod, AcCode, AcSource, DocType, DocNo, DocDate, DocDueDate, PartyTo, MastType, CurrencyId, ExRate, 
                      Term, Description, DocAmt, LocAmt
FROM XAArInvoice";
            sql += " WHERE SequenceId='" + docId.Text + "'";
            DataTable dt = Helper.Sql.List(sql);
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
                docAmt = SafeValue.SafeDecimal(dt.Rows[0]["DocAmt"].ToString(), 0);
                locAmt = SafeValue.SafeDecimal(dt.Rows[0]["LocAmt"].ToString(), 0);
                exRate = SafeValue.SafeDecimal(dt.Rows[0]["ExRate"].ToString(), 0);
                currency = dt.Rows[0]["CurrencyId"].ToString();
                docDt = SafeValue.SafeDate(dt.Rows[0]["DocDate"], new DateTime(1900, 1, 1));
                remarks = dt.Rows[0]["Description"].ToString();
                partyTo = dt.Rows[0]["PartyTo"].ToString();
            }
            else
            {
                e.Result = "Can't find the Ar Invoice!";
                return;
            }
            string sqlDet = string.Format("select count(SequenceId) from XAArInvoiceDet where DocId='{0}'", docId.Text);
            int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
            if (detCnt == 0)
            {
                e.Result = "No Detail, Can't Post";
                return;
            }

            //check account period
            if (acYear < 2000 || acPeriod < 1)
            {
                e.Result = "Account year or Period Invalid!";
                return;
            }

            string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
            string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
            if (closeInd == "")
            {
                e.Result = "Can't find this account period!";
                return;
            }
            else if (closeInd == "Y")
            {
                e.Result = "The account period is closed!";
                return;
            }
            sql = "select SUM(LocAmt) from XAArInvoiceDet where AcSource='CR' and DocId='" + docId.Text + "'";
            decimal amt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            sql = "select SUM(LocAmt) from XAArInvoiceDet where AcSource='DB' and DocId='" + docId.Text + "'";
            amt_det -= SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            if (docAmt != amt_det)
            {
                e.Result = "Loc Amount can't match, can't post,Please first resave it,";
                return;
            }
            sql = "select count(LocAmt) from XAArInvoiceDet where AcCode='' and DocId='" + docId.Text + "'";
            detCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            if (detCnt > 0)
            {
                e.Result = "Some Item's Accode is blank, pls check";
                return;
            }
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
                //gl.GlNo = GetNo("GLENTRY");
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
                // SetNo(gl.GlNo, "GLENTRY");
                //insert Detail
                OPathQuery query = new OPathQuery(typeof(XAArInvoiceDet), "DocId='" + docId.Text + "'");
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
                string gstAcc = SafeValue.SafeString(Manager.ORManager.ExecuteScalar("SELECT AcCode FROM XXGstAccount where GstSrc='AR'"), "2033");
                for (int i = 0; i < set.Count; i++)
                {
                    try
                    {
                        index++;
                        XAArInvoiceDet invDet = set[i] as XAArInvoiceDet;
                        XAGlEntryDet det = new XAGlEntryDet();
                        if (invDet.AcCode == gstAcc)
                        {
                            if (invDet.AcSource == "CR")
                                gstCrAmt += invDet.LocAmt;
                            else
                                gstDbAmt += invDet.LocAmt;
                        }
                        else
                        {

                            det.AcCode = invDet.AcCode;
                            det.ArApInd = "AR";
                            det.AcPeriod = acPeriod;
                            det.AcSource = invDet.AcSource;
                            det.AcYear = acYear;
                            if (det.AcSource == "CR")
                            {
                                det.CrAmt = SafeValue.ChinaRound(SafeValue.ChinaRound(invDet.Qty * invDet.Price, 2) * invDet.ExRate, 2);
                                det.CurrencyCrAmt = SafeValue.ChinaRound(det.CrAmt * exRate, 2);
                                det.DbAmt = 0;
                                det.CurrencyDbAmt = 0;
                                gstCrAmt += invDet.GstAmt;
                            }
                            else
                            {
                                det.DbAmt = SafeValue.ChinaRound(SafeValue.ChinaRound(invDet.Qty * invDet.Price, 2) * invDet.ExRate, 2);
                                det.CurrencyDbAmt = SafeValue.ChinaRound(det.DbAmt * exRate, 2);
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
                    catch
                    {
                        e.Result = "Posting Error, Please repost!";
                        DeleteGl(glOid);
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
                UpdateArInv(docId.Text);
                //EzshipLog.Log(docN, "", docType, "Post");
                e.Result = "Post completely!";
            }
            catch(Exception ex)
            {
                e.Result = "Posting Error, Please repost!";
                DeleteGl(glOid);
            }
            #endregion
        }
       
	   if (p.ToUpper() == "V")
        {
            #region Void
            string sql = "update XAArInvoice set CancelInd=case when CancelInd='N' then 'Y' else 'N' end where SequenceId=" + docId.Text + " and DocAmt=BalanceAmt";
            int res = Manager.ORManager.ExecuteCommand(sql);
            if (res > 0)
            {
                ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
                ASPxComboBox DocType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
                ASPxButton action = this.ASPxGridView1.FindEditFormTemplateControl("btn_void") as ASPxButton;
                //EzshipLog.Log(docN.Text, "", DocType.Text, action.Text);
                e.Result = "Success";
            }
            else
            {
                e.Result = "Already payment, can not void! ";
            }
            #endregion
        }
    }
    #region pay detail
    protected void PayTab(int siId)
    {
        DevExpress.Web.ASPxTabControl.ASPxPageControl pageCtr = this.ASPxGridView1.FindEditFormTemplateControl("pageControl") as DevExpress.Web.ASPxTabControl.ASPxPageControl;

        ASPxGridView grd = pageCtr.FindControl("grid_PayDet") as ASPxGridView;
        DataTable tab_repDet = Helper.Sql.List(string.Format(@"SELECT mast.DocNo, mast.DocType, mast.DocDate,det.DocAmt, det.LocAmt, mast.ExportInd
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", siId));
        DataTable tab = new DataTable();
        tab.Columns.Add("SequenceId");
        tab.Columns.Add("RepNo");
        tab.Columns.Add("RepType");
        tab.Columns.Add("RepDate");
        tab.Columns.Add("DocAmt");
        tab.Columns.Add("LocAmt");
        tab.Columns.Add("PostInd");
        for (int i = 0; i < tab_repDet.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["SequenceId"] = i;
            row["RepNo"] = tab_repDet.Rows[i]["DocNo"];
            row["RepDate"] = SafeValue.SafeDate(tab_repDet.Rows[i]["DocDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["RepType"] = tab_repDet.Rows[i]["DocType"];
            row["DocAmt"] = tab_repDet.Rows[i]["DocAmt"];
            row["LocAmt"] = tab_repDet.Rows[i]["LocAmt"];

            row["PostInd"] = tab_repDet.Rows[i]["ExportInd"];
            tab.Rows.Add(row);
        }
        //from ap payment
        int cnt = tab.Rows.Count;
        DataTable tab_repDet1 = Helper.Sql.List(string.Format(@"SELECT  mast.DocNo ,mast.DocDate, mast.DocType, det.LocAmt, det.DocAmt,mast.ExportInd
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", siId));
        for (int i = 0; i < tab_repDet1.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["SequenceId"] = cnt + i;
            row["RepNo"] = tab_repDet1.Rows[i]["DocNo"];
            row["RepDate"] = SafeValue.SafeDate(tab_repDet1.Rows[i]["DocDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["RepType"] = tab_repDet1.Rows[i]["DocType"];
            row["DocAmt"] = tab_repDet1.Rows[i]["DocAmt"];
            row["LocAmt"] = tab_repDet1.Rows[i]["LocAmt"];
            row["PostInd"] = tab_repDet1.Rows[i]["ExportInd"];
            tab.Rows.Add(row);
        }
        //grd.DataSource = tab;
        //grd.DataBind();
    }
    #endregion

    private void DeleteGl(int glOid)
    {
        string sql_delete = "delete from XAGlEntry where SequenceId= '" + glOid + "'";
        int m = Manager.ORManager.ExecuteCommand(sql_delete);
        sql_delete = "delete from XAGlEntryDet where GlNo= '" + glOid + "'";
        m = Manager.ORManager.ExecuteCommand(sql_delete);
    }
    private void UpdateArInv(string docId)
    {
        string sql_invoice = "update XAArInvoice set ExportInd='Y' where SequenceId='" + docId + "'";
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }
}
