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

public partial class Account_ApPayableEdit : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
            this.form1.Focus();
            Session["SeaPvEditWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["SeaPvEditWhere"] = "DocType!='VO' and DocNo='" + Request.QueryString["no"] + "'";
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["SeaPvEditWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsApPayable.FilterExpression = "1=0";
        }
        if (Session["SeaPvEditWhere"] != null)
        {
            this.dsApPayable.FilterExpression = Session["SeaPvEditWhere"].ToString();
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
            grd.ForceDataRowType(typeof(C2.XAApPayable));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string[] currentPeriod = EzshipHelper.GetAccPeriod(DateTime.Today);
        string acYear = currentPeriod[1];
        string acPeriod = currentPeriod[0];

        e.NewValues["AcYear"] = acYear;
        e.NewValues["AcPeriod"] = acPeriod;
        e.NewValues["DocDate"] = DateTime.Today;
        e.NewValues["SupplierBillDate"] = DateTime.Today;
        e.NewValues["MastType"] = "SOT";
        string type = "PL"; //Request.QueryString["type"].ToString();
        e.NewValues["DocType"] = type;
        if (type == "SC")
            e.NewValues["AcSource"] = "DB";
        else
            e.NewValues["AcSource"] = "CR";
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = new decimal(1.0);
        e.NewValues["Term"] = "CASH";
        if (Request.QueryString["JobType"] != null && Request.QueryString["RefN"] != null && Request.QueryString["JobN"] != null)
        {
            e.NewValues["MastType"] = Request.QueryString["JobType"].ToString();
            e.NewValues["MastRefNo"] = Request.QueryString["RefN"].ToString();
            string houseNo = Request.QueryString["JobN"].ToString();
            e.NewValues["JobRefNo"] = houseNo;
        }
    }

    #endregion


    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    { 
    }

    #region invoice det
    protected void grid_InvDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.XAApPayableDet));
    }
    protected void grid_InvDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsApPayableDet.FilterExpression = "DocId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_InvDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DocLineNo"] = 0;
        ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        e.NewValues["Currency"] = docCurr.Text;
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["Qty"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Gst"] = 0;
        e.NewValues["GstType"] = "Z";
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        if (docType.Value == "SC")
            e.NewValues["AcSource"] = "CR";
        else
            e.NewValues["AcSource"] = "DB";
        e.NewValues["SplitType"] = "WtM3";
        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxComboBox jobType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocCate") as ASPxComboBox;
        e.NewValues["MastType"] = jobType.Text;
        e.NewValues["MastRefNo"] = mastRefNCtr.Text;
        e.NewValues["JobRefNo"] = jobRefNCtr.Text;
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["AcCode"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the charge code");
        }
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

        string sql_detCnt = "select count(*) from XAApPayableDet where DocId='" + oidCtr.Text + "'";
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        e.NewValues["CostingId"] = "";
        e.NewValues["DocId"] = SafeValue.SafeInt(oidCtr.Text, 0);
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
        if (SafeValue.SafeString(e.NewValues["JobRefNo"]).Length > 1)
        {
            e.NewValues["SplitType"] = "SET";
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
            e.NewValues["SplitType"] = "SET";
        }
    }
    protected void grid_InvDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_InvDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    protected void grid_InvDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    protected void grid_InvDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    private void UpdateMaster(string docId)
    {
        string sql = string.Format("update XAApPayableDet set LineLocAmt=locAmt* (select ExRate from XAApPayable where SequenceId=XAApPayableDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAApPayableDet where DocId='{0}'", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (tab.Rows[i]["AcSource"].ToString() == "DB")
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
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        if (docType.Value.ToString() == "SC")
        {
            docAmt = -docAmt;
            locAmt = -locAmt;
        }



        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", docId)), 0);
        balAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM XAArReceiptDet AS det INNER JOIN  XAArInvoice AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", docId)), 0);

        sql = string.Format("Update XAApPayable set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion

    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
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
    private void SaveAndUpdate()
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        ASPxComboBox partyTo = this.ASPxGridView1.FindEditFormTemplateControl("cmb_PartyTo") as ASPxComboBox;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        ASPxMemo remark = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remarks1") as ASPxMemo;
        ASPxComboBox termId = this.ASPxGridView1.FindEditFormTemplateControl("txt_TermId") as ASPxComboBox;
        ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocExRate") as ASPxSpinEdit;
        ASPxTextBox acCode = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcCode") as ASPxTextBox;
        ASPxTextBox acSource = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcSource") as ASPxTextBox;

        ASPxTextBox supplierBillNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_SupplierBillNo") as ASPxTextBox;
        ASPxDateEdit supplierBillDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_SupplierBillDate") as ASPxDateEdit;
        ASPxDateEdit docDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
        ASPxDateEdit eta = this.ASPxGridView1.FindEditFormTemplateControl("txt_Eta") as ASPxDateEdit;
        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxComboBox jobType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocCate") as ASPxComboBox;
        string invN = docN.Text;
        C2.XAApPayable inv = Manager.ORManager.GetObject(typeof(XAApPayable), SafeValue.SafeInt(oidCtr.Text, 0)) as XAApPayable;
        if (invN.Length < 1)// first insert invoice
        {
            if (supplierBillNo.Text.Trim().Length > 0)
            {
                string sqlCnt = string.Format("select DocNo from XaApPayable where SupplierBillNo='{0}' and PartyTo='{1}' and DocType='{2}' AND CancelInd='N'", supplierBillNo.Text.Trim(), SafeValue.SafeString(partyTo.Value, ""), docType.Value.ToString());
                string billNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sqlCnt), "");
                if (billNo.Length > 0)
                {
                    throw new Exception(string.Format("Have this Supplier Bill No In {0}({1})", billNo, docType.Value.ToString()));
                    return;
                }
            }
            inv = new XAApPayable();
            invN = C2Setup.GetNextNo("","AP-PAYABLE",docDt.Date);
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
            inv.MastType = "";//SafeValue.SafeString(docCate.Value, "");
            inv.DocType = docType.Value.ToString();
            inv.DocNo = invN.ToString();
            inv.Term = termId.Text;
            inv.Description = remark.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;
            inv.AcCode = EzshipHelper.GetAccApCode(inv.PartyTo,inv.CurrencyId);
            inv.AcSource = acSource.Text;

            inv.ExportInd = "N";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.EntryDate = DateTime.Now;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";

            inv.SupplierBillNo = supplierBillNo.Text;
            inv.SupplierBillDate = SafeValue.SafeDate(supplierBillDate.Date, DateTime.Today);
            inv.DocDate = inv.SupplierBillDate;
            inv.ChqNo = "";
            inv.ChqDate = new DateTime(1900, 1, 1);

            inv.Eta = eta.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
            inv.MastType = SafeValue.SafeString(jobType.Value, "I");
            inv.MastRefNo = mastRefNCtr.Text;
            inv.JobRefNo = jobRefNCtr.Text;
            if (inv.DocType == "SC")
                inv.AcSource = "DB";
            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo("","AP-PAYABLE", invN,inv.DocDate);
            }
            catch
            {
            }
        }
        else
        {
            if (supplierBillNo.Text.Trim().Length > 0)
            {
                string sqlCnt = string.Format("select DocNo from XaApPayable where SupplierBillNo='{0}' and PartyTo='{1}' and DocType='{2}' and DocNo!='{3}'", supplierBillNo.Text.Trim(), SafeValue.SafeString(partyTo.Value, ""), docType.Value.ToString(), inv.DocNo);
                string billNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sqlCnt), "");
                if (billNo.Length > 0)
                {
                    throw new Exception(string.Format("Have this Supplier Bill No In {0}({1})", billNo, docType.Value.ToString()));
                    return;
                }
            }
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
            // inv.DocDate = SafeValue.SafeDate(docDate.Text, DateTime.Now);
            inv.Term = termId.Text;
            inv.Description = remark.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;
            inv.AcCode = EzshipHelper.GetAccApCode(inv.PartyTo, inv.CurrencyId);
            inv.AcSource = acSource.Text;

            inv.SupplierBillNo = supplierBillNo.Text;
            inv.SupplierBillDate = SafeValue.SafeDate(supplierBillDate.Date, DateTime.Now);
            inv.DocDate = inv.SupplierBillDate;
            inv.Eta = eta.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);


            inv.MastType = SafeValue.SafeString(jobType.Value, "I");
            inv.MastRefNo = mastRefNCtr.Text;
            inv.JobRefNo = jobRefNCtr.Text;
            try
            {
                Manager.ORManager.StartTracking(inv, InitialState.Updated);
                Manager.ORManager.PersistChanges(inv);
                UpdateMaster(inv.SequenceId.ToString());
            }
            catch
            {
            }

        }
        Session["SeaPvEditWhere"] = "DocNo='" + invN + "'";
        this.dsApPayable.FilterExpression = "DocNo='" + invN + "'";
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);

    }
}
