﻿using System;
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
		if (!IsPostBack)
		{
			this.txt_DocType.Text = "PL";
			Session["PvEditWhere"] = null;
			if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
			{
				string docNo = Request.QueryString["no"].ToString();
				string billNo = SafeValue.SafeString(Request.QueryString["billNo"], "");
				this.txtSchNo.Text = docNo;
				this.txtBillNo.Text = billNo;
				if (Request.QueryString["type"] != null)
					this.txt_DocType.Text = Request.QueryString["type"].ToString();
				if (docNo.Length > 0)
					Session["PvEditWhere"] = "DocNo='" + docNo + "' and DocType='" + this.txt_DocType.Text + "'";
				else
				{
					Session["PvEditWhere"] = "SupplierBillNo='" + billNo + "' and DocType='" + this.txt_DocType.Text + "'";
				}
			}
			else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
			{
				if (Session["PvEditWhere"] == null)
				{
					this.ASPxGridView1.AddNewRow();
				}
			}
			else
				this.dsApPayable.FilterExpression = "1=0";
		}

		if (Session["PvEditWhere"] != null)
		{
			this.dsApPayable.FilterExpression = Session["PvEditWhere"].ToString();
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
		e.NewValues["MastType"] = "I";
		string type = Request.QueryString["type"].ToString();
		e.NewValues["DocType"] = type;
		if (type == "SC")
			e.NewValues["AcSource"] = "DB";
		else
			e.NewValues["AcSource"] = "CR";
		e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
		e.NewValues["ExRate"] = new decimal(1.0);
		e.NewValues["Term"] = "CASH";
		e.NewValues["SupplierBillDate"] = DateTime.Today;
		e.NewValues["Eta"] = new DateTime(1900, 1, 1);
	}

	protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
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
		ASPxDateEdit eta = this.ASPxGridView1.FindEditFormTemplateControl("txt_Eta") as ASPxDateEdit;
		string invN = docN.Text;
		C2.XAApPayable inv = Manager.ORManager.GetObject(typeof(XAApPayable), SafeValue.SafeInt(oidCtr.Text, 0)) as XAApPayable;
		bool isNew = false;
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

			isNew = true;
			inv = new XAApPayable();
			//invN = C2Setup.GetNextNo("AP-PAYABLE");
			string counterType = "AP-PAYABLE";
			invN = C2Setup.GetNextNo(inv.DocType, counterType, supplierBillDate.Date);

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
			inv.AcCode = EzshipHelper.GetAccApCode(inv.PartyTo, inv.CurrencyId);
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

			inv.MastRefNo = "0";
			inv.JobRefNo = "0";
			inv.MastType = "";
			inv.Eta = eta.Date;
			string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

			inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
			inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
			try
			{
				C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
				C2.Manager.ORManager.PersistChanges(inv);
				//C2Setup.SetNextNo("AP-PAYABLE", invN);
                C2Setup.SetNextNo("", counterType, invN, inv.DocDate);

			}
			catch(Exception ex)
			{
throw new Exception(ex.Message + ex.StackTrace);
			}
		}
		else
		{
			if (supplierBillNo.Text.Trim().Length > 0)
			{
				string sqlCnt = string.Format("select DocNo from XaApPayable where SupplierBillNo='{0}' and PartyTo='{1}' and DocType='{2}' and DocNo!='{3}' AND CancelInd='N'", supplierBillNo.Text.Trim(), SafeValue.SafeString(partyTo.Value, ""), docType.Value.ToString(), inv.DocNo);
				string billNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sqlCnt), "");
				if (billNo.Length > 0)
				{
					throw new Exception(string.Format("Have this Supplier Bill No In {0}({1})", billNo, docType.Value.ToString()));
					return;
				}
			}

			inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
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

		Session["PvEditWhere"] = "SequenceId=" + inv.SequenceId;
		this.dsApPayable.FilterExpression = Session["PvEditWhere"].ToString();
		if (isNew)
		{
			if (this.ASPxGridView1.GetRow(0) != null)
				this.ASPxGridView1.StartEdit(0);
		}
	}

	#endregion

    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
	{
        if (this.ASPxGridView1.EditingRowVisibleIndex > -1)
        {
            //ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
            PayTab(SafeValue.SafeInt(ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "SequenceId"), 0));
        }
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
        if (SafeValue.SafeString(docType.Value, "") == "SC")
            e.NewValues["AcSource"] = "CR";
        else
            e.NewValues["AcSource"] = "DB";
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

        string sql_detCnt = "select count(*) from XAApPayableDet where DocId='" + oidCtr.Text + "'";
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        //e.NewValues["CostingId"] = "";
        e.NewValues["DocId"] = SafeValue.SafeInt(oidCtr.Text, 0);
        e.NewValues["DocLineNo"] = lineNo;
        ASPxTextBox billN = this.ASPxGridView1.FindEditFormTemplateControl("txt_SupplierBillNo") as ASPxTextBox;
		string billNo = S.Text(billN.Text).Trim();
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
		string _jobno = S.Text(e.NewValues["MastRefNo"]).Trim();
		string _contno = S.Text(e.NewValues["JobRefNo"]).Trim();
        e.NewValues["MastRefNo"] = _jobno;
		e.NewValues["JobRefNo"] = _contno;
		
		if(_jobno.Length >5 && _contno.Length > 5) {
			string sqlPsa = string.Format("update psa_bill set Job_No='{0}' where [Bill Number]='{1}' and [Bill Item Number]='{2}' and [Container Number]='{3}'",
			_jobno, billNo, e.NewValues["DocLineNo"], 
			_contno
			);
			D.Exec(sqlPsa);
		}
		
        //e.NewValues["MastType"] = "";
        //e.NewValues["MastRefNo"] = "0";
        //e.NewValues["JobRefNo"] = "0";
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox billN = this.ASPxGridView1.FindEditFormTemplateControl("txt_SupplierBillNo") as ASPxTextBox;
		string billNo = S.Text(billN.Text).Trim();
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

		string _jobno = S.Text(e.NewValues["MastRefNo"]).Trim();
		string _contno = S.Text(e.NewValues["JobRefNo"]).Trim();
        e.NewValues["MastRefNo"] = _jobno;
		e.NewValues["JobRefNo"] = _contno;
		
		if(_jobno.Length >5 && _contno.Length > 5) {
			string sqlPsa = string.Format("update psa_bill set Job_No='{0}' where [Bill Number]='{1}' and [Bill Item Number]='{2}' and [Container Number]='{3}'",
			_jobno, billNo, e.NewValues["DocLineNo"], 
			_contno
			);
			//throw new Exception(sqlPsa);
			D.Exec(sqlPsa);
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
        string sql = string.Format("update XaApPayableDet set LineLocAmt=locAmt* (select ExRate from XaApPayable where SequenceId=XaApPayableDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt=0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAApPayableDet where DocId='{0}'", docId);
        DataTable tab = Helper.Sql.List(sql);
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
        //locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

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

    #region pay detail
    protected void PayTab(int siId)
    {
        DevExpress.Web.ASPxTabControl.ASPxPageControl pageCtr = this.ASPxGridView1.FindEditFormTemplateControl("pageControl") as DevExpress.Web.ASPxTabControl.ASPxPageControl;

        ASPxGridView grd = pageCtr.FindControl("grid_PayDet") as ASPxGridView;
        DataTable tab_repDet = Helper.Sql.List(string.Format(@"SELECT  mast.DocNo ,mast.DocDate, mast.DocType, det.LocAmt, det.DocAmt,mast.ExportInd
FROM  XAApPaymentDet AS det INNER JOIN XAApPayment AS mast ON det.PayId  = mast.SequenceId
WHERE (det.DocId = '{0}')and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", siId));
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
            //if (tab_repDet.Rows[i]["DocType"] == "SR")
            //    row["LocAmt"] = tab_repDet.Rows[i]["mLocAmt"];

            row["PostInd"] = tab_repDet.Rows[i]["ExportInd"];
            tab.Rows.Add(row);
        }
        int cnt = tab.Rows.Count;
        //from receipt
        DataTable tab_repDet1 = Helper.Sql.List(string.Format(@"SELECT mast.DocNo, mast.DocType, mast.DocDate,det.DocAmt, det.LocAmt, mast.ExportInd
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}')and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", siId));
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
        grd.DataSource = tab;
        grd.DataBind();
    }
    #endregion

    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string p = e.Parameters;
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        //get informations from arinvoice
        if (p.ToUpper() == "P")
        {
            #region Post
            string sql = @"SELECT AcYear, AcPeriod, AcCode, AcSource, DocType, DocNo, DocDate, PartyTo, MastType, CurrencyId, ExRate, Term, Description, LocAmt, 
                      DocAmt,SupplierBillNo, SupplierBillDate FROM XAApPayable";
            sql += " WHERE SequenceId='" + oidCtr.Text + "'";
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
            string partyTo = "";
            string supplierBillNo = "";
            DateTime supplierBillDate = new DateTime(1900, 1, 1);
            DateTime docDt = DateTime.Today;
            string remarks = "";
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
            else
            {
                e.Result = "Can't find the Payable!";
                return;
            }

            string sqlDet = string.Format("select count(SequenceId) from XAApPayableDet where DocId='{0}'", oidCtr.Text);
            int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
            if (detCnt == 0)
            {
                e.Result = "No Detail, Can't Post";
                return;
            }

            //check account period
            if (acYear < 1 || acPeriod < 1)
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

            sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='DB' and DocId='" + oidCtr.Text + "'";
            decimal amt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            sql = "select SUM(LocAmt) from XAApPayableDet where AcSource='CR' and DocId='" + oidCtr.Text + "'";
            amt_det -= SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            if (docType == "SC")
                amt_det = -amt_det;

            if (docAmt != amt_det)
            {
                e.Result = "Amount can't match, can't post,Please first resave it,";
                return;
            }
            sql = "select count(LocAmt) from XAApPayableDet where AcCode='' and DocId='" + oidCtr.Text + "'";
            detCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            if (detCnt >0)
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
                OPathQuery query = new OPathQuery(typeof(XAApPayableDet), "DocId='" + oidCtr.Text + "'");
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
                    catch
                    {
                        e.Result = "Posting Error, Please repost!";
                        DeleteGl(glOid);
                    }
                }
                if (gstDbAmt - gstCrAmt != 0)
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
                UpdateArInv(oidCtr.Text);
                e.Result = "Post completely!";
            }
            catch
            {
                e.Result = "Posting Error, Please repost!";
                DeleteGl(glOid);
            }
            #endregion
        }
        else if (p == "V")
        {
            string sql = string.Format(@"SELECT  count(*) FROM XAApPaymentDet WHERE (DocId = '{0}')", oidCtr.Text);
            int cnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
            if (cnt > 0)
            {
                e.Result = "Have Pay this bill, can't void it.";
                return;
            }
            else
            {
                sql = string.Format("update XAApPayable set CancelInd='Y',Description=Description+' Void by {1}' where Sequenceid='{0}'", oidCtr.Text, HttpContext.Current.User.Identity.Name);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";
            }

        }
    }

    private void DeleteGl(int glOid)
    {
        string sql_delete = "delete from XAGlEntry where SequenceId= '" + glOid + "'";
        int m = Manager.ORManager.ExecuteCommand(sql_delete);
        sql_delete = "delete from XAGlEntryDet where GlNo= '" + glOid + "'";
        m = Manager.ORManager.ExecuteCommand(sql_delete);
    }
    private void UpdateArInv(string docId)
    {
        string sql_invoice = "update XAApPayable set ExportInd='Y' where SequenceId='" + docId + "'";
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }


    #region popub
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] Code = new object[grid.VisibleRowCount];
        object[] Des = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            Code[i] = grid.GetRowValues(i, "Code");
            Des[i] = grid.GetRowValues(i, "AcDesc");
        }
        e.Properties["cpCode"] = Code;
        e.Properties["cpDes"] = Des;
    }
    protected void gridPopGst_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] GstCode = new object[grid.VisibleRowCount];
        object[] GstValue = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            GstCode[i] = grid.GetRowValues(i, "Code");
            GstValue[i] = grid.GetRowValues(i, "GstValue");
        }
        e.Properties["cpGstCode"] = GstCode;
        e.Properties["cpGstValue"] = GstValue;
    }
    #endregion

}
