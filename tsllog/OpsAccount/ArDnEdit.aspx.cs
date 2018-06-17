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
using System.IO;
using System.Xml;

public partial class Account_ArDnEdit : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
            this.txtSchNo.Focus();
            Session["SeaDnEditWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                Session["SeaDnEditWhere"] = "DocType='DN' and DocNo='" +Request.QueryString["no"]+"'";
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["SeaDnEditWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsArInvoice.FilterExpression = "1=0";
        }
        if (Session["SeaDnEditWhere"] != null)
        {
            this.dsArInvoice.FilterExpression = Session["SeaDnEditWhere"].ToString();
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
        e.NewValues["DocType"] = "DN";
        e.NewValues["MastType"] = "I";
        e.NewValues["AcSource"] = "DB";
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = new decimal(1.0);
        e.NewValues["Term"] = "CASH";
        if (Request.QueryString["JobType"] != null && Request.QueryString["RefN"] != null && Request.QueryString["JobN"] != null)
        {
            string refNo = Request.QueryString["RefN"].ToString();
            string houseNo = Request.QueryString["JobN"].ToString();
            string jobType = Request.QueryString["JobType"].ToString();
            e.NewValues["MastRefNo"] = refNo;
            e.NewValues["JobRefNo"] = houseNo;
            e.NewValues["MastType"] = jobType;
            string sql = "SELECT Cust FROM  TPT_Job WHERE (JobNo= '" + refNo + "')";
            if (Request.QueryString["JobType"].ToString() == "TPT")
            {
                e.NewValues["PartyTo"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");
                e.NewValues["PartyName"] = EzshipHelper.GetPartyName(e.NewValues["PartyTo"]);
                e.NewValues["Term"] = EzshipHelper.GetTerm(e.NewValues["PartyTo"].ToString());
            }
            if (houseNo.Length > 1)
            {
                if (jobType == "SI")
                    sql = "SELECT CustomerID FROM  SeaImport WHERE (JobNo= '" + houseNo + "')";
                if (jobType == "SE")
                    sql = "SELECT CustomerID FROM  SeaExport WHERE (JobNo= '" + houseNo + "')";
                if (jobType == "AI" || jobType == "AE" || jobType == "ACT")
                    sql = "SELECT CustomerID FROM  air_job WHERE (JobNo= '" + houseNo + "')";

                e.NewValues["PartyTo"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");
                e.NewValues["PartyName"] = EzshipHelper.GetPartyName(e.NewValues["PartyTo"]);
                e.NewValues["Term"] = EzshipHelper.GetTerm(e.NewValues["PartyTo"].ToString());

                if (Request.QueryString["JobType"].ToString() == "SI")
                {
                    sql = "SELECT CollectCurrency, CollectExRate FROM  SeaImport WHERE (JobNo= '" + houseNo + "')";
                    DataTable tab_cur = Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab_cur.Rows.Count == 1)
                    {
                        e.NewValues["CurrencyId"] = tab_cur.Rows[0][0].ToString();
                        e.NewValues["ExRate"] = SafeValue.SafeDecimal(tab_cur.Rows[0][1], 0);
                    }
                }
            }
        }
    }
    #endregion


    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.ASPxGridView1.EditingRowVisibleIndex > -1)
        {
            string oid = SafeValue.SafeString(this.ASPxGridView1.GetRowValues(this.ASPxGridView1.EditingRowVisibleIndex, new string[] { "SequenceId" }));
            if (oid.Length > 0)
            {
                ASPxDateEdit DocDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
                DocDt.BackColor = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                DocDt.ReadOnly = true;
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
        ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        e.NewValues["Currency"] = docCurr.Text;
        //e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["Gst"] = 0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["Qty"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["GstType"] = "Z";
        e.NewValues["AcSource"] = "CR";
        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxComboBox jobType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocCate") as ASPxComboBox;
        e.NewValues["MastType"] = jobType.Text;
        e.NewValues["MastRefNo"] = mastRefNCtr.Text;
        e.NewValues["JobRefNo"] = jobRefNCtr.Text;
        e.NewValues["SplitType"] = "Set";
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["AcCode"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the charge code");
        }
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
        if (SafeValue.SafeString(e.NewValues["JobRefNo"]).Length > 1)
        {
            e.NewValues["SplitType"] = "Set";
        }
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["AcCode"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the charge code");
        }
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
        if (SafeValue.SafeString(e.NewValues["JobRefNo"]).Length > 1)
        {
            e.NewValues["SplitType"] = "Set";
        }
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
        string sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAArInvoiceDet where DocId='{0}'", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (tab.Rows[i]["AcSource"].ToString() == "CR")
            {
                docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }
            else
            {
                docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }
        }



        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        balAmt += SafeValue.SafeDecimal(Manager.ORManager.GetDataSet(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion

    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }
    private void SaveAndUpdate()
    {
        ASPxTextBox invNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        ASPxComboBox partyTo = this.ASPxGridView1.FindEditFormTemplateControl("cmb_PartyTo") as ASPxComboBox;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        ASPxDateEdit docDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
        ASPxMemo remarks1 = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remarks1") as ASPxMemo;
        ASPxComboBox termId = this.ASPxGridView1.FindEditFormTemplateControl("txt_TermId") as ASPxComboBox;
        ASPxDateEdit dueDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_DueDt") as ASPxDateEdit;
        ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocExRate") as ASPxSpinEdit;
        ASPxTextBox acCode = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcCode") as ASPxTextBox;
        ASPxComboBox acSource = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcSource") as ASPxComboBox;
        ASPxTextBox specialNote = this.ASPxGridView1.FindEditFormTemplateControl("txt_SpecialNote") as ASPxTextBox;


        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxComboBox jobType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocCate") as ASPxComboBox;

        string invN = docN.Text;
        C2.XAArInvoice inv = Manager.ORManager.GetObject(typeof(XAArInvoice), SafeValue.SafeInt(invNCtr.Text, 0)) as XAArInvoice;
        if (inv == null)// first insert invoice
        {
            invN = C2Setup.GetNextNo("", "AR-DN", docDate.Date);
            inv = new XAArInvoice();
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
            inv.DocType = docType.Value.ToString();
            inv.DocNo = invN.ToString();
            inv.DocDate = docDate.Date;
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
            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
            inv.AcSource = acSource.Value.ToString();
            inv.SpecialNote = specialNote.Text;

            inv.MastType = SafeValue.SafeString(jobType.Value, "I");
            inv.MastRefNo = mastRefNCtr.Text;
            inv.JobRefNo = jobRefNCtr.Text;
            inv.ExportInd = "N";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.EntryDate = DateTime.Now;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";
            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo("","AR-DN", invN,inv.DocDate);
            }
            catch
            {
            }
			DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("ArDnEdit.aspx?no=" + inv.DocNo);

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
            inv.SpecialNote = specialNote.Text;

            inv.MastType = SafeValue.SafeString(jobType.Value, "I");
            inv.MastRefNo = mastRefNCtr.Text;
            inv.JobRefNo = jobRefNCtr.Text;
            try
            {
                Manager.ORManager.StartTracking(inv, InitialState.Updated);
                Manager.ORManager.PersistChanges(inv);
                UpdateMaster(inv.SequenceId);
            }
            catch
            {
            }

        }
        Session["SeaDnEditWhere"] = "Doctype='DN' and DocNo='" + invN + "'";
        this.dsArInvoice.FilterExpression = "Doctype='DN' and DocNo='" + invN + "'";
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        SaveAndUpdate();
        e.Cancel = true;
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        SaveAndUpdate();
        e.Cancel = true;
    }
}
