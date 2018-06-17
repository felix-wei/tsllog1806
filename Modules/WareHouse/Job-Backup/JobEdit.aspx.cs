using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using System.Xml;
using System.IO;
using DevExpress.Web.ASPxHtmlEditor;
public partial class WareHouse_Job_JobEdit : System.Web.UI.Page
{
	protected void Page_Init()
	{
		SetFilter(); //this.dsIssue.FilterExpression = "QuotationNo='" + Request.QueryString["no"] + "'";
		//throw new Exception(this.dsIssue.FilterExpression);
		if (this.grid_Issue.GetRow(0) != null)
			this.grid_Issue.StartEdit(0);
	}

	public void SetFilter()
	{
		string no = Request.QueryString["no"] ?? "";
		string rno = Request.QueryString["refN"] ?? "";
		if(no != "")
			this.dsIssue.FilterExpression = "QuotationNo='" + no + "'";
		if(rno != "")
			this.dsIssue.FilterExpression = "JobNo='" + rno + "'";
	}

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	#region JO
    protected void grid_Issue_DataSelect(object sender, EventArgs e)
	{
	}

	protected void grid_Issue_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobInfo));
		}
	}

	protected void grid_Issue_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		string userId = HttpContext.Current.User.Identity.Name;
		e.NewValues["JobNo"] = "NEW";
		e.NewValues["JobDate"] = DateTime.Now;

		e.NewValues["JobStage"] = "Customer Inquir";
		e.NewValues["CreateBy"] = userId;
		e.NewValues["CreateDateTime"] = DateTime.Today;
		e.NewValues["DateTime1"] = DateTime.Now;
		e.NewValues["ExpectedDate"] = DateTime.Today;
		e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
		e.NewValues["ExRate"] = 1.000000;
		e.NewValues["PayTerm"] = "30DAYS";
		e.NewValues["IncoTerm"] = "EXW";
		//e.NewValues["WareHouseId"] = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];
	}

	protected string SaveJob()
	{
		try
		{
			ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
			ASPxTextBox DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
			ASPxTextBox txt_Id = grid_Issue.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
			string pId = SafeValue.SafeString(txt_Id.Text, "");
			Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(JobInfo), "Id='" + pId + "'");
			JobInfo job = C2.Manager.ORManager.GetObject(query) as JobInfo;
			ASPxDateEdit issueDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
			bool isNew = false;
			//const string runType = "DOOUT";
			string issueN = "";
			if (job == null)
			{
				job = new JobInfo();
				isNew = true;
				issueN = C2Setup.GetNextNo("", "Quotation", issueDate.Date);
				job.JobDate = issueDate.Date;
			}

			ASPxDateEdit doDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
			if (doDate != null)
				job.JobDate = doDate.Date;

			ASPxComboBox cmb_Status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
			if (cmb_Status != null)
				job.JobStage = SafeValue.SafeString(cmb_Status.Value);
			ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
			if (cmb_JobType != null)
				job.JobType = SafeValue.SafeString(cmb_JobType.Value);
			//Main Info
            ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
			if (txt_CustomerId != null)
				job.CustomerId = txt_CustomerId.Text;
			ASPxTextBox txt_CustomerName = grid_Issue.FindEditFormTemplateControl("txt_CustomerName") as ASPxTextBox;
			if (txt_CustomerName != null)
				job.CustomerName = txt_CustomerName.Text;
			ASPxMemo memo_Address = grid_Issue.FindEditFormTemplateControl("memo_Address") as ASPxMemo;
			if (memo_Address != null)
				job.CustomerAdd = memo_Address.Text;

			ASPxTextBox txt_PostalCode = grid_Issue.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
			if (txt_PostalCode != null)
				job.Postalcode = txt_PostalCode.Text;
			ASPxTextBox txt_Contact = grid_Issue.FindEditFormTemplateControl("txt_Contact") as ASPxTextBox;
			if (txt_Contact != null)
				job.Contact = txt_Contact.Text;
			ASPxTextBox txt_Tel = grid_Issue.FindEditFormTemplateControl("txt_Tel") as ASPxTextBox;
			if (txt_Tel != null)
				job.Tel = txt_Tel.Text;
			ASPxTextBox txt_Email = grid_Issue.FindEditFormTemplateControl("txt_Email") as ASPxTextBox;
			if (txt_Email != null)
				job.Email = txt_Email.Text;
			ASPxTextBox txt_Fax = grid_Issue.FindEditFormTemplateControl("txt_Fax") as ASPxTextBox;
			if (txt_Fax != null)
				job.Fax = txt_Fax.Text;
			ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
			if (remark != null)
				job.Remark = remark.Text;

			ASPxComboBox payTerm = grid_Issue.FindEditFormTemplateControl("cmb_PayTerm") as ASPxComboBox;
			if (payTerm != null)
				job.PayTerm = payTerm.Text;
			ASPxDateEdit date_ExpiryDate = grid_Issue.FindEditFormTemplateControl("date_ExpiryDate") as ASPxDateEdit;
			if (date_ExpiryDate != null)
				job.ExpiryDate = date_ExpiryDate.Date;

			ASPxMemo txt_PackRemark = grid_Issue.FindEditFormTemplateControl("txt_PackRemark") as ASPxMemo;
			if (txt_PackRemark != null)
				job.PackRmk = txt_PackRemark.Text;
			ASPxMemo txt_MoveRemark = grid_Issue.FindEditFormTemplateControl("txt_MoveRemark") as ASPxMemo;
			if (txt_MoveRemark != null)
				job.MoveRmk = txt_MoveRemark.Text;

			//ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
			//if (txt_WareHouseId != null)
			//   job.WareHouseId = txt_WareHouseId.Text;
			ASPxButtonEdit btn_OriginPort = grid_Issue.FindEditFormTemplateControl("btn_OriginPort") as ASPxButtonEdit;
			if (btn_OriginPort != null)
				job.OriginPort = btn_OriginPort.Text;
			ASPxButtonEdit btn_DestinationPort = grid_Issue.FindEditFormTemplateControl("btn_DestinationPort") as ASPxButtonEdit;
			if (btn_DestinationPort != null)
				job.DestinationPort = btn_DestinationPort.Text;
			ASPxMemo memo_Address1 = grid_Issue.FindEditFormTemplateControl("memo_Address1") as ASPxMemo;
			if (memo_Address1 != null)
				job.OriginAdd = memo_Address1.Text;
			ASPxMemo memo_Address2 = grid_Issue.FindEditFormTemplateControl("memo_Address2") as ASPxMemo;
			if (memo_Address2 != null)
				job.DestinationAdd = memo_Address2.Text;
			ASPxSpinEdit spin_Volumne = grid_Issue.FindEditFormTemplateControl("spin_Volumne") as ASPxSpinEdit;
			if (spin_Volumne != null)
				job.Volumne = SafeValue.SafeDecimal(spin_Volumne.Value);
			ASPxMemo memo_Description = grid_Issue.FindEditFormTemplateControl("memo_Description") as ASPxMemo;
			if (memo_Description != null)
				job.ItemDes = memo_Description.Text;
			ASPxTextBox memo_Volumn = grid_Issue.FindEditFormTemplateControl("memo_Volumn") as ASPxTextBox;
			if (memo_Volumn != null)
				job.VolumneRmk = memo_Volumn.Text;
			ASPxSpinEdit spin_HeadCount = grid_Issue.FindEditFormTemplateControl("spin_HeadCount") as ASPxSpinEdit;
			if (spin_HeadCount != null)
				job.HeadCount = SafeValue.SafeInt(spin_HeadCount.Value, 0);

			ASPxDateEdit date_Pack = grid_Issue.FindEditFormTemplateControl("date_Pack") as ASPxDateEdit;
			if (date_Pack != null)
				job.PackDate = date_Pack.Date;
			ASPxComboBox cmb_Via = grid_Issue.FindEditFormTemplateControl("cmb_Via") as ASPxComboBox;
			if (cmb_Via != null)
				job.ViaWh = cmb_Via.Text;

			ASPxComboBox cmb_PrintTotal = pageControl.FindControl("cmb_PrintTotal") as ASPxComboBox;
			if (cmb_PrintTotal != null)
				job.Note16 = cmb_PrintTotal.Text;

			ASPxDateEdit date_time1 = pageControl.FindControl("date_DateTime1") as ASPxDateEdit;
			if (date_time1 != null)
				job.DateTime1 = date_time1.Date;

			ASPxButtonEdit currency = pageControl.FindControl("txt_Currency") as ASPxButtonEdit;
			if (currency != null)
				job.Currency = currency.Text;
			ASPxSpinEdit exRate = pageControl.FindControl("spin_ExRate") as ASPxSpinEdit;
			if (exRate != null)
				job.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);

			ASPxDateEdit date_StorageStartDate = grid_Issue.FindEditFormTemplateControl("date_StorageStartDate") as ASPxDateEdit;
			if (date_StorageStartDate != null)
				job.StorageStartDate = date_StorageStartDate.Date;
			ASPxSpinEdit spin_StorageFreeDays = grid_Issue.FindEditFormTemplateControl("spin_StorageFreeDays") as ASPxSpinEdit;
			if (spin_StorageFreeDays != null)
				job.StorageFreeDays = SafeValue.SafeInt(spin_StorageFreeDays.Value, 0);
			ASPxSpinEdit spin_STotalDays = grid_Issue.FindEditFormTemplateControl("spin_STotalDays") as ASPxSpinEdit;
			if (spin_STotalDays != null)
				job.StorageTotalDays = Helper.Safe.SafeInt(spin_STotalDays.Value);
			ASPxTextBox txt_TripNo = grid_Issue.FindEditFormTemplateControl("txt_TripNo") as ASPxTextBox;
			if (txt_TripNo != null)
				job.TripNo = txt_TripNo.Text;
			ASPxDateEdit date_MoveDate = grid_Issue.FindEditFormTemplateControl("date_MoveDate") as ASPxDateEdit;
			if (date_MoveDate != null)
				job.MoveDate = date_MoveDate.Date;
			ASPxSpinEdit spin_Charges = grid_Issue.FindEditFormTemplateControl("spin_Charges") as ASPxSpinEdit;
			if (spin_Charges != null)
				job.Charges = SafeValue.SafeDecimal(spin_Charges.Value);
			ASPxButtonEdit btn_PortOfEntry = grid_Issue.FindEditFormTemplateControl("btn_PortOfEntry") as ASPxButtonEdit;
			if (btn_PortOfEntry != null)
				job.EntryPort = btn_PortOfEntry.Text;

			ASPxButtonEdit btn_Surveyer = grid_Issue.FindEditFormTemplateControl("btn_Surveyer") as ASPxButtonEdit;
			if (btn_Surveyer != null)
				job.Value2 = btn_Surveyer.Text;

			ASPxComboBox txt_Mode = grid_Issue.FindEditFormTemplateControl("cmb_Mode") as ASPxComboBox;
			if (txt_Mode != null)
				job.Mode = txt_Mode.Text;
			ASPxDateEdit date_Eta = grid_Issue.FindEditFormTemplateControl("date_Eta") as ASPxDateEdit;
			if (date_Eta != null)
				job.Eta = date_Eta.Date;
			ASPxDateEdit date_EtaDest = grid_Issue.FindEditFormTemplateControl("date_EtaDest") as ASPxDateEdit;
			if (date_EtaDest != null)
				job.EtaDest = date_EtaDest.Date;
			ASPxDateEdit date_Etd = grid_Issue.FindEditFormTemplateControl("date_Etd") as ASPxDateEdit;
			if (date_Etd != null)
				job.Etd = date_Eta.Date;
			ASPxTextBox txt_TruckNo = grid_Issue.FindEditFormTemplateControl("txt_TruckNo") as ASPxTextBox;
			if (txt_TruckNo != null)
				job.TruckNo = txt_TruckNo.Text;
			ASPxButtonEdit btn_OriginCity = grid_Issue.FindEditFormTemplateControl("btn_OriginCity") as ASPxButtonEdit;
			if (btn_OriginCity != null)
				job.OriginCity = btn_OriginCity.Text;
			ASPxButtonEdit btn_DestCity = grid_Issue.FindEditFormTemplateControl("btn_DestCity") as ASPxButtonEdit;
			if (btn_DestCity != null)
				job.DestCity = btn_DestCity.Text;
			ASPxTextBox txt_OriginPostal = grid_Issue.FindEditFormTemplateControl("txt_OriginPostal") as ASPxTextBox;
			if (txt_OriginPostal != null)
				job.OriginPostal = txt_OriginPostal.Text;
			ASPxTextBox txt_DestPostal = grid_Issue.FindEditFormTemplateControl("txt_DestPostal") as ASPxTextBox;
			if (txt_DestPostal != null)
				job.DestPostal = txt_DestPostal.Text;
			ASPxComboBox cmb_ServiceType = grid_Issue.FindEditFormTemplateControl("cmb_ServiceType") as ASPxComboBox;
			if (cmb_ServiceType != null)
				job.ServiceType = cmb_ServiceType.Text;
			//Additional

            ASPxComboBox cmb_FullPacking = pageControl.FindControl("cmb_FullPacking") as ASPxComboBox;
			if (cmb_FullPacking != null)
				job.Item1 = SafeValue.SafeString(cmb_FullPacking.Value);
			ASPxComboBox cmb_UnFull = pageControl.FindControl("cmb_UnFull") as ASPxComboBox;
			if (cmb_UnFull != null)
				job.Item2 = SafeValue.SafeString(cmb_UnFull.Value);
			ASPxTextBox txt_Details = pageControl.FindControl("txt_Details") as ASPxTextBox;
			if (txt_Details != null)
				job.ItemDetail1 = txt_Details.Text;
			ASPxTextBox txt_UnpackDetails = pageControl.FindControl("txt_UnpackDetails") as ASPxTextBox;
			if (txt_UnpackDetails != null)
				job.ItemDetail2 = txt_UnpackDetails.Text;

			ASPxComboBox cmb_Insurance = pageControl.FindControl("cmb_Insurance") as ASPxComboBox;
			if (cmb_Insurance != null)
				job.Item3 = SafeValue.SafeString(cmb_Insurance.Value);
			ASPxTextBox txt_Percentage = pageControl.FindControl("txt_Percentage") as ASPxTextBox;
			if (txt_Percentage != null)
				job.ItemValue3 = txt_Percentage.Text;
			ASPxTextBox txt_Value = pageControl.FindControl("txt_Value") as ASPxTextBox;
			if (txt_Value != null)
				job.ItemData3 = txt_Value.Text;
			ASPxSpinEdit txt_Premium = pageControl.FindControl("txt_Premium") as ASPxSpinEdit;
			if (txt_Premium != null)
				job.ItemPrice3 = SafeValue.SafeDecimal(txt_Premium.Text);

			ASPxComboBox cmb_PianoApply = pageControl.FindControl("cmb_PianoApply") as ASPxComboBox;
			if (cmb_PianoApply != null)
				job.Item4 = SafeValue.SafeString(cmb_PianoApply.Value);
			ASPxTextBox txt_PainoDetails = pageControl.FindControl("txt_PainoDetails") as ASPxTextBox;
			if (txt_PainoDetails != null)
				job.ItemDetail4 = txt_PainoDetails.Text;
			ASPxSpinEdit spin_Charges1 = pageControl.FindControl("spin_Charges1") as ASPxSpinEdit;
			if (spin_Charges1 != null)
				job.ItemPrice4 = SafeValue.SafeDecimal(spin_Charges1.Value);

			ASPxComboBox cmb_Safe = pageControl.FindControl("cmb_Safe") as ASPxComboBox;
			if (cmb_Safe != null)
				job.Item5 = cmb_Safe.Text;
			ASPxTextBox txt_Brand = pageControl.FindControl("txt_Brand") as ASPxTextBox;
			if (txt_Brand != null)
				job.ItemValue5 = SafeValue.SafeString(txt_Brand.Value);
			ASPxSpinEdit spin_Weight = pageControl.FindControl("spin_Weight") as ASPxSpinEdit;
			if (spin_Weight != null)
				job.ItemPrice5 = SafeValue.SafeDecimal(spin_Weight.Value);

			ASPxComboBox cmb_Crating = pageControl.FindControl("cmb_Crating") as ASPxComboBox;
			if (cmb_Crating != null)
				job.Item6 = SafeValue.SafeString(cmb_Crating.Value);
			ASPxTextBox txt_Details1 = pageControl.FindControl("txt_Details1") as ASPxTextBox;
			if (txt_Details1 != null)
				job.ItemDetail6 = txt_Details1.Text;
			ASPxSpinEdit spin_Charges2 = pageControl.FindControl("spin_Charges2") as ASPxSpinEdit;
			if (spin_Charges2 != null)
				job.ItemPrice6 = SafeValue.SafeDecimal(spin_Charges2.Value);

			ASPxComboBox cmb_Handyman = pageControl.FindControl("cmb_Handyman") as ASPxComboBox;
			if (cmb_Handyman != null)
				job.Item7 = SafeValue.SafeString(cmb_Handyman.Value);
			ASPxComboBox cmb_Complimentory = pageControl.FindControl("cmb_Complimentory") as ASPxComboBox;
			if (cmb_Complimentory != null)
				job.ItemValue7 = SafeValue.SafeString(cmb_Complimentory.Value);
			ASPxTextBox txt_Details2 = pageControl.FindControl("txt_Details2") as ASPxTextBox;
			if (txt_Details2 != null)
				job.ItemDetail7 = txt_Details2.Text;
			ASPxSpinEdit spin_Charges3 = pageControl.FindControl("spin_Charges3") as ASPxSpinEdit;
			if (spin_Charges3 != null)
				job.ItemPrice7 = SafeValue.SafeDecimal(spin_Charges3.Value);

			ASPxComboBox cmb_Maid = pageControl.FindControl("cmb_Maid") as ASPxComboBox;
			if (cmb_Maid != null)
				job.Item8 = SafeValue.SafeString(cmb_Maid.Value);
			ASPxComboBox cmb_Complimentory1 = pageControl.FindControl("cmb_Complimentory1") as ASPxComboBox;
			if (cmb_Complimentory1 != null)
				job.ItemValue8 = SafeValue.SafeString(cmb_Complimentory1.Value);
			ASPxTextBox txt_Details3 = pageControl.FindControl("txt_Details3") as ASPxTextBox;
			if (txt_Details3 != null)
				job.ItemDetail8 = txt_Details3.Text;
			ASPxSpinEdit spin_Charges4 = pageControl.FindControl("spin_Charges4") as ASPxSpinEdit;
			if (spin_Charges4 != null)
				job.ItemPrice8 = SafeValue.SafeDecimal(spin_Charges4.Value);

			ASPxComboBox cmb_Shifting = pageControl.FindControl("cmb_Shifting") as ASPxComboBox;
			if (cmb_Shifting != null)
				job.Item9 = SafeValue.SafeString(cmb_Shifting.Value);
			ASPxComboBox cmb_Complimentory2 = pageControl.FindControl("cmb_Complimentory2") as ASPxComboBox;
			if (cmb_Complimentory2 != null)
				job.ItemValue9 = SafeValue.SafeString(cmb_Complimentory2.Value);
			ASPxTextBox txt_Details4 = pageControl.FindControl("txt_Details4") as ASPxTextBox;
			if (txt_Details4 != null)
				job.ItemDetail9 = txt_Details4.Text;
			ASPxSpinEdit spin_Charges5 = pageControl.FindControl("spin_Charges5") as ASPxSpinEdit;
			if (spin_Charges5 != null)
				job.ItemPrice9 = SafeValue.SafeDecimal(spin_Charges5.Value);

			ASPxComboBox cmb_Disposal = pageControl.FindControl("cmb_Disposal") as ASPxComboBox;
			if (cmb_Disposal != null)
				job.Item10 = cmb_Disposal.Text;
			ASPxComboBox cmb_Complimentory3 = pageControl.FindControl("cmb_Complimentory3") as ASPxComboBox;
			if (cmb_Complimentory3 != null)
				job.ItemValue10 = cmb_Complimentory3.Text;
			ASPxTextBox txt_Details5 = pageControl.FindControl("txt_Details5") as ASPxTextBox;
			if (txt_Details5 != null)
				job.ItemDetail10 = txt_Details5.Text;
			ASPxSpinEdit spin_Charges6 = pageControl.FindControl("spin_Charges6") as ASPxSpinEdit;
			if (spin_Charges6 != null)
				job.ItemPrice10 = SafeValue.SafeDecimal(spin_Charges6.Value);

			ASPxComboBox cmb_PickUp = pageControl.FindControl("cmb_PickUp") as ASPxComboBox;
			if (cmb_PickUp != null)
				job.Item11 = SafeValue.SafeString(cmb_PickUp.Value);
			ASPxTextBox txt_Details6 = pageControl.FindControl("txt_Details6") as ASPxTextBox;
			if (txt_Details6 != null)
				job.ItemDetail11 = txt_Details6.Text;

			ASPxComboBox cmb_Additional = pageControl.FindControl("cmb_Additional") as ASPxComboBox;
			if (cmb_Additional != null)
				job.Item12 = cmb_Additional.Text;
			ASPxTextBox txt_Details7 = pageControl.FindControl("txt_Details7") as ASPxTextBox;
			if (txt_Details7 != null)
				job.ItemDetail12 = txt_Details7.Text;

			ASPxComboBox cmb_BadAccess = pageControl.FindControl("cmb_BadAccess") as ASPxComboBox;
			if (cmb_BadAccess != null)
				job.Item13 = SafeValue.SafeString(cmb_BadAccess.Value);
			ASPxComboBox cmb_Origin = pageControl.FindControl("cmb_Origin") as ASPxComboBox;
			if (cmb_Origin != null)
				job.ItemValue13 = cmb_Origin.Text;
			ASPxComboBox cmb_Destination = pageControl.FindControl("cmb_Destination") as ASPxComboBox;
			if (cmb_Destination != null)
				job.ItemData13 = SafeValue.SafeString(cmb_Destination.Value);

			ASPxComboBox cmb_Storage = pageControl.FindControl("cmb_Storage") as ASPxComboBox;
			if (cmb_Storage != null)
				job.Item14 = SafeValue.SafeString(cmb_Storage.Value);
			ASPxComboBox cmb_Complimentory4 = pageControl.FindControl("cmb_Complimentory4") as ASPxComboBox;
			if (cmb_Complimentory4 != null)
				job.ItemValue14 = SafeValue.SafeString(cmb_Complimentory4.Value);
			ASPxTextBox txt_Details8 = pageControl.FindControl("txt_Details8") as ASPxTextBox;
			if (txt_Details8 != null)
				job.ItemDetail14 = txt_Details8.Text;
			ASPxSpinEdit spin_Charges7 = pageControl.FindControl("spin_Charges7") as ASPxSpinEdit;
			if (spin_Charges7 != null)
				job.ItemPrice14 = SafeValue.SafeDecimal(spin_Charges7.Value);

			ASPxTextBox txt_How = pageControl.FindControl("txt_How") as ASPxTextBox;
			if (txt_How != null)
				job.Answer1 = txt_How.Text;
			ASPxTextBox txt_Other = pageControl.FindControl("txt_Other") as ASPxTextBox;
			if (txt_Other != null)
				job.Answer2 = txt_Other.Text;
			ASPxTextBox txt_Move = pageControl.FindControl("txt_Move") as ASPxTextBox;
			if (txt_Move != null)
				job.Answer3 = txt_Move.Text;
			ASPxTextBox txt_UnsuccessRemark = pageControl.FindControl("txt_UnsuccessRemark") as ASPxTextBox;
			if (txt_UnsuccessRemark != null)
				job.Answer4 = txt_UnsuccessRemark.Text;
			ASPxComboBox cmb_WorkStatus = grid_Issue.FindEditFormTemplateControl("cmb_WorkStatus") as ASPxComboBox;
			if (cmb_WorkStatus != null)
				job.WorkStatus = SafeValue.SafeString(cmb_WorkStatus.Value);

			//Quotation
            ASPxHtmlEditor txt_Attention1 = pageControl.FindControl("txt_Attention1") as ASPxHtmlEditor;
			if (txt_Attention1 != null)
				job.Attention1 = txt_Attention1.Html;
			ASPxHtmlEditor txt_Attention2 = pageControl.FindControl("txt_Attention2") as ASPxHtmlEditor;
			if (txt_Attention2 != null)
				job.Attention2 = txt_Attention2.Html;
			ASPxHtmlEditor txt_Attention3 = pageControl.FindControl("txt_Attention3") as ASPxHtmlEditor;
			if (txt_Attention3 != null)
				job.Attention3 = txt_Attention3.Html;
			ASPxHtmlEditor txt_Attention4 = pageControl.FindControl("txt_Attention4") as ASPxHtmlEditor;
			if (txt_Attention4 != null)
				job.Attention4 = txt_Attention4.Html;
			ASPxHtmlEditor txt_Attention5 = pageControl.FindControl("txt_Attention5") as ASPxHtmlEditor;
			if (txt_Attention5 != null)
				job.Attention5 = txt_Attention5.Html;
			ASPxHtmlEditor txt_Attention6 = pageControl.FindControl("txt_Attention6") as ASPxHtmlEditor;
			if (txt_Attention6 != null)
				job.Attention6 = txt_Attention6.Html;
			ASPxHtmlEditor txt_Attention7 = pageControl.FindControl("txt_Attention7") as ASPxHtmlEditor;
			if (txt_Attention7 != null)
				job.Attention7 = txt_Attention7.Html;

			if (cmb_Status.Text == "Job Confirmation")
			{
				job.WorkStatus = "Pending";
			}

			ASPxComboBox cmb_SalesId = grid_Issue.FindEditFormTemplateControl("cmb_SalesId") as ASPxComboBox;
			if (cmb_SalesId != null)
				job.Value4 = cmb_SalesId.Text;
			ASPxComboBox cmb_Survey = grid_Issue.FindEditFormTemplateControl("cmb_SurveyId") as ASPxComboBox;
			if (cmb_Survey != null)
				job.Value2 = cmb_Survey.Text;
			ASPxButtonEdit cmb_Super = grid_Issue.FindEditFormTemplateControl("btn_Supervisor") as ASPxButtonEdit;
			if (cmb_Super != null)
				job.Value7 = cmb_Super.Text;

			if (cmb_Status.Text == "Job Confirmation")
			{
				job.WorkStatus = "Pending";
			}

			if (job.JobStage == "Customer Inquiry")
			{
				job.DateTime1 = DateTime.Now;
			}

			if (job.JobStage == "Costing")
			{
				job.DateTime3 = DateTime.Now;
				string title = "Quotation";
				string type = job.JobType;
				string content = "";
				string sql = string.Format("select count(*) from Job_Message where RefNo='{0}'", job.JobNo);
				int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
				if (cnt == 0)
				{
					sql = string.Format(@"insert into Job_Message(RefNo,MTitle,MType,MBody,JobStage,IsRead,CreatedBy,CreateDateTime) 
values('{0}','{1}','{2}','{3}','{4}','N','{5}',getdate())", job.QuotationNo, title, type, content, job.JobStage, EzshipHelper.GetUserName());
					C2.Manager.ORManager.ExecuteCommand(sql);
				}
			}

			if (job.JobStage == "Quotation")
			{
				job.DateTime4 = DateTime.Now;
			}

			if (job.JobStage == "Job Confirmation")
			{
				job.DateTime5 = DateTime.Now;
			}

			if (job.JobStage == "Billing")
			{
				job.DateTime6 = DateTime.Now;
			}

			if (job.JobStage == "Schedule")
			{
				job.DateTime7 = DateTime.Now;
			}

			if (job.JobStage == "Close")
			{
				job.DateTime8 = DateTime.Now;
			}

			ASPxDateEdit date_DateTime2 = grid_Issue.FindEditFormTemplateControl("date_DateTime2") as ASPxDateEdit;
			if (date_DateTime2 != null)
			{
				if (!date_DateTime2.Date.IsDaylightSavingTime())
					job.DateTime2 = date_DateTime2.Date;
				else
					job.DateTime2 = DateTime.Now;
			}

			ASPxMemo memo_Notes = pageControl.FindControl("memo_Notes") as ASPxMemo;
			if (memo_Notes != null)
				job.Notes = memo_Notes.Text;
			ASPxMemo txt_Note1 = pageControl.FindControl("txt_Note1") as ASPxMemo;
			if (txt_Note1 != null)
				job.Note1 = txt_Note1.Text;
			ASPxMemo txt_Note2 = pageControl.FindControl("txt_Note2") as ASPxMemo;
			if (txt_Note2 != null)
				job.Note2 = txt_Note2.Text;
			ASPxMemo txt_Note3 = pageControl.FindControl("txt_Note3") as ASPxMemo;
			if (txt_Note3 != null)
				job.Note3 = txt_Note3.Text;
			ASPxMemo txt_Note4 = pageControl.FindControl("txt_Note4") as ASPxMemo;
			if (txt_Note4 != null)
				job.Note4 = txt_Note4.Text;
			ASPxMemo txt_Note5 = pageControl.FindControl("txt_Note5") as ASPxMemo;
			if (txt_Note5 != null)
				job.Note5 = txt_Note5.Text;
			ASPxMemo txt_Note6 = pageControl.FindControl("txt_Note6") as ASPxMemo;
			if (txt_Note6 != null)
				job.Note6 = txt_Note6.Text;
			ASPxMemo txt_Note7 = pageControl.FindControl("txt_Note7") as ASPxMemo;
			if (txt_Note7 != null)
				job.Note7 = txt_Note7.Text;
			ASPxMemo txt_Note8 = pageControl.FindControl("txt_Note8") as ASPxMemo;
			if (txt_Note8 != null)
				job.Note8 = txt_Note8.Text;
			ASPxMemo txt_Note9 = pageControl.FindControl("txt_Note9") as ASPxMemo;
			if (txt_Note9 != null)
				job.Note9 = txt_Note9.Text;
			ASPxMemo txt_Note10 = pageControl.FindControl("txt_Note10") as ASPxMemo;
			if (txt_Note10 != null)
				job.Note10 = txt_Note10.Text;
			ASPxMemo txt_Note11 = pageControl.FindControl("txt_Note11") as ASPxMemo;
			if (txt_Note11 != null)
				job.Note11 = txt_Note11.Text;
			ASPxMemo txt_Note12 = pageControl.FindControl("txt_Note12") as ASPxMemo;
			if (txt_Note12 != null)
				job.Note12 = txt_Note12.Text;
			ASPxMemo txt_Note13 = pageControl.FindControl("txt_Note13") as ASPxMemo;
			if (txt_Note13 != null)
				job.Note13 = txt_Note13.Text;

			ASPxTextBox txt_note14 = grid_Issue.FindEditFormTemplateControl("txt_note14") as ASPxTextBox;
			if (txt_note14 != null)
				job.Note14 = txt_note14.Text;

			ASPxComboBox cmb_note15 = grid_Issue.FindEditFormTemplateControl("cmb_note15") as ASPxComboBox;
			if (cmb_note15 != null)
				job.Note15 = cmb_note15.Text;

			ASPxMemo txt_Note16 = pageControl.FindControl("txt_Note16") as ASPxMemo;
			if (txt_Note16 != null)
				job.Note16 = txt_Note16.Text;

			ASPxTextBox txt_Note17 = grid_Issue.FindEditFormTemplateControl("txt_note17") as ASPxTextBox;
			if (txt_Note17 != null)
				job.Note17 = txt_Note17.Text;

			ASPxTextBox txt_Note18 = grid_Issue.FindEditFormTemplateControl("txt_note18") as ASPxTextBox;
			if (txt_Note18 != null)
				job.Note18 = txt_Note18.Text;

			ASPxTextBox txt_Note19 = grid_Issue.FindEditFormTemplateControl("txt_note19") as ASPxTextBox;
			if (txt_Note19 != null)
				job.Note19 = txt_Note19.Text;

			ASPxTextBox txt_Note20 = grid_Issue.FindEditFormTemplateControl("txt_note20") as ASPxTextBox;
			if (txt_Note20 != null)
				job.Note20 = txt_Note20.Text;
			ASPxTextBox txt_PoNo = grid_Issue.FindEditFormTemplateControl("txt_PoNo") as ASPxTextBox;
			if (txt_PoNo != null)
				job.Note21 = txt_PoNo.Text;
			ASPxTextBox txt_Note22 = grid_Issue.FindEditFormTemplateControl("txt_Note22") as ASPxTextBox;
			if (txt_Note22 != null)
				job.Note22 = txt_Note22.Text;
			ASPxTextBox txt_Note23 = grid_Issue.FindEditFormTemplateControl("txt_Note23") as ASPxTextBox;
			if (txt_Note23 != null)
				job.Note23 = txt_Note23.Text;
			ASPxTextBox txt_Note24 = grid_Issue.FindEditFormTemplateControl("txt_Note24") as ASPxTextBox;
			if (txt_Note24 != null)
				job.Note24 = txt_Note24.Text;
			ASPxTextBox txt_Note25 = grid_Issue.FindEditFormTemplateControl("txt_Note25") as ASPxTextBox;
			if (txt_Note25 != null)
				job.Note25 = txt_Note25.Text;
			ASPxTextBox txt_Note26 = grid_Issue.FindEditFormTemplateControl("txt_Note26") as ASPxTextBox;
			if (txt_Note26 != null)
				job.Note26 = txt_Note26.Text;
			ASPxTextBox txt_Note27 = grid_Issue.FindEditFormTemplateControl("txt_Note27") as ASPxTextBox;
			if (txt_Note27 != null)
				job.Note27 = txt_Note27.Text;
			ASPxTextBox txt_Note28 = grid_Issue.FindEditFormTemplateControl("txt_Note28") as ASPxTextBox;
			if (txt_Note28 != null)
				job.Note28 = txt_Note28.Text;
			ASPxTextBox txt_Note29 = grid_Issue.FindEditFormTemplateControl("txt_Note29") as ASPxTextBox;
			if (txt_Note29 != null)
				job.Note29 = txt_Note29.Text;
			ASPxTextBox txt_Note30 = grid_Issue.FindEditFormTemplateControl("txt_Note30") as ASPxTextBox;
			if (txt_Note30 != null)
				job.Note30 = txt_Note30.Text;
			string userId = HttpContext.Current.User.Identity.Name;
			if (isNew)
			{
				job.CreateBy = userId;
				job.CreateDateTime = DateTime.Now;
				job.UpdateBy = userId;
				job.UpdateDateTime = DateTime.Now;
				job.QuotationNo = issueN;
				Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
				Manager.ORManager.PersistChanges(job);
				C2Setup.SetNextNo("", "Quotation", issueN, issueDate.Date);
			}
			else
			{
				job.UpdateBy = userId;
				job.UpdateDateTime = DateTime.Now;
				bool isAddLog = false;
				if (job.JobStatus == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select JobStatus from JobInfo where JobNo='" + job.JobNo + "'")))
				{
				}
				else
				{
					isAddLog = true;
				}

				Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
				Manager.ORManager.PersistChanges(job);
				if (isAddLog)
					EzshipLog.Log(job.JobNo, "", "JO", job.JobStatus);
			}

			this.dsIssue.FilterExpression = "QuotationNo='" + job.QuotationNo + "'";
			if (this.grid_Issue.GetRow(0) != null)
				this.grid_Issue.StartEdit(0);

			return job.QuotationNo;
		}
		catch(Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
		return "";
	}

	private string SaveDoOut(string poNo, string wh)
	{
		string issueN = "";
		try
		{
			ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
			ASPxTextBox DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
			ASPxTextBox txt_Id = grid_Issue.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
			string pId = "";
			Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "Id='" + pId + "'");
			WhDo whDo = C2.Manager.ORManager.GetObject(query) as WhDo;
			ASPxDateEdit issueDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
			bool isNew = false;

			if (whDo == null)
			{
				whDo = new WhDo();
				isNew = true;
				whDo.DoDate = DateTime.Today;
				issueN = C2Setup.GetNextNo("", "DOOUT", whDo.DoDate);
			}

			//Main Info
            ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
			whDo.PartyId = txt_CustomerId.Text;
			ASPxTextBox txt_CustomerName = grid_Issue.FindEditFormTemplateControl("txt_CustomerName") as ASPxTextBox;
			whDo.PartyName = txt_CustomerName.Text;
			ASPxMemo memo_Address = grid_Issue.FindEditFormTemplateControl("memo_Address") as ASPxMemo;
			whDo.PartyAdd = memo_Address.Text;

			ASPxTextBox txt_PostalCode = grid_Issue.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
			whDo.PartyPostalcode = txt_PostalCode.Text;

			ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
			whDo.Remark = remark.Text;

			//ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
			//whDo.WareHouseId = txt_WareHouseId.Text;
			whDo.Priority = "EXPORT";

			ASPxButtonEdit btn_DestinationPort = grid_Issue.FindEditFormTemplateControl("btn_DestinationPort") as ASPxButtonEdit;
			whDo.DeliveryTo = btn_DestinationPort.Text;
			//whDo.WareHouseId = wh;

			whDo.PoNo = DoNo.Text;
			whDo.PoDate = issueDate.Date;
			string userId = HttpContext.Current.User.Identity.Name;
			if (isNew)
			{
				whDo.StatusCode = "USE";
				whDo.CreateBy = userId;
				whDo.CreateDateTime = DateTime.Now;
				whDo.DoNo = issueN;
				whDo.DoType = "OUT";
				Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
				Manager.ORManager.PersistChanges(whDo);
				C2Setup.SetNextNo("", "DOOUT", issueN, whDo.DoDate);
			}
			else
			{
				whDo.UpdateBy = userId;
				whDo.UpdateDateTime = DateTime.Now;
				Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Updated);
				Manager.ORManager.PersistChanges(whDo);
			}
		}
		catch { }
		return issueN;
	}

	private string SaveDoIn(string poNo)
	{
		string doNo = "";
		try
		{
			ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
			ASPxDateEdit txt_Date = this.grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
			WhDo whDo = null;
			bool action = false;
			if (whDo == null)
			{
				action = true;
				whDo = new WhDo();
				doNo = C2Setup.GetNextNo("", "DOIN", txt_Date.Date);
			}

			whDo.DoDate = DateTime.Today;
			whDo.Priority = "IMPORT";
			ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
			whDo.PartyId = txt_CustomerId.Text;
			ASPxTextBox txt_CustomerName = grid_Issue.FindEditFormTemplateControl("txt_CustomerName") as ASPxTextBox;
			whDo.PartyName = txt_CustomerName.Text;
			ASPxMemo memo_Address = grid_Issue.FindEditFormTemplateControl("memo_Address") as ASPxMemo;
			whDo.PartyAdd = memo_Address.Text;

			ASPxTextBox txt_PostalCode = grid_Issue.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
			whDo.PartyPostalcode = txt_PostalCode.Text;

			ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
			whDo.Remark = remark.Text;

			//ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
			//whDo.WareHouseId = txt_WareHouseId.Text;

			ASPxButtonEdit btn_OriginPort = grid_Issue.FindEditFormTemplateControl("btn_OriginPort") as ASPxButtonEdit;
			whDo.CollectFrom = btn_OriginPort.Text;
			if (action)
			{
				whDo.DoNo = doNo;
				whDo.DoType = "IN";
				whDo.StatusCode = "USE";
				whDo.CreateBy = EzshipHelper.GetUserName();
				whDo.CreateDateTime = DateTime.Today;
				whDo.PoNo = poNo;
				whDo.PoDate = txt_Date.Date;
				Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
				Manager.ORManager.PersistChanges(whDo);
				C2Setup.SetNextNo("", "DOIN", doNo, whDo.DoDate);
			}
			else
			{
				whDo.UpdateBy = EzshipHelper.GetUserName();
				whDo.UpdateDateTime = DateTime.Now;
				Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Updated);
				Manager.ORManager.PersistChanges(whDo);
			}
		}
		catch { }
		return doNo;
	}

	private string ViaJobType(string jobNo) {
		return SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select JobType from JobInfo where QuotationNo='{0}'", jobNo)));
	}

	protected void grid_Issue_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
	{
		if (this.grid_Issue.EditingRowVisibleIndex > -1)
		{
			ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
			ASPxTextBox txt_Quotation = grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
			ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
			ASPxComboBox cmb_Status = this.grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
			ASPxComboBox cmb_JobType = this.grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
			ASPxButton btn_AddSch = pageControl.FindControl("btn_AddSch") as ASPxButton;
			ASPxComboBox cmb_Mode = this.grid_Issue.FindEditFormTemplateControl("cmb_Mode") as ASPxComboBox;

			//ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
			//ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
			//agentName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "AgentId" }));
			//notifyName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "NotifyId" }));
			string oid = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
			if (ViaJobType(SafeValue.SafeString(Request.QueryString["no"])) == "Storage")
			{
				TabPage tab = pageControl.TabPages.FindByName("Inventory") as TabPage;
				tab.Visible = true;
				TabPage survery = pageControl.TabPages.FindByName("Survery") as TabPage;
				survery.Visible = false;
				TabPage item = pageControl.TabPages.FindByName("Item List") as TabPage;
				item.Visible = false;
				TabPage mcst = pageControl.TabPages.FindByName("MCST") as TabPage;
				mcst.Visible = true;
				TabPage costing = pageControl.TabPages.FindByName("Costing") as TabPage;
				costing.Visible = false;
			}

			if (ViaJobType(SafeValue.SafeString(Request.QueryString["no"])) == "Inbound")
			{
				TabPage survery = pageControl.TabPages.FindByName("Survery") as TabPage;
				survery.Visible = false;
				TabPage item = pageControl.TabPages.FindByName("Item List") as TabPage;
				item.Visible = false;
			}

			if (ViaJobType(SafeValue.SafeString(Request.QueryString["no"])) == "Outbound")
			{
				//cmb_Mode.Items.RemoveAt(5);
			}

			if (ViaJobType(SafeValue.SafeString(Request.QueryString["no"])) == "Local Move")
			{
				TabPage costing = pageControl.TabPages.FindByName("Costing") as TabPage;
				costing.Visible = false;
			}

			if (ViaJobType(SafeValue.SafeString(Request.QueryString["no"])) == "Office Move")
			{
				TabPage costing = pageControl.TabPages.FindByName("Costing") as TabPage;
				costing.Visible = false;
			}

			if (oid.Length > 0)
			{
				string sql = string.Format("select JobStatus from JobInfo where Id='{0}'", oid);
				string jobStatus = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 

                ASPxButton btn = grid_Issue.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
				ASPxButton btn_Void = grid_Issue.FindEditFormTemplateControl("btn_Void") as ASPxButton;
				ASPxButton btn_Inquir = grid_Issue.FindEditFormTemplateControl("btn_Inquir") as ASPxButton;
				ASPxButton btn_Survey = grid_Issue.FindEditFormTemplateControl("btn_Survey") as ASPxButton;
				ASPxButton btn_Costing = grid_Issue.FindEditFormTemplateControl("btn_Costing") as ASPxButton;
				ASPxButton btn_Quotation = grid_Issue.FindEditFormTemplateControl("btn_Quotation") as ASPxButton;
				ASPxButton btn_Confirmation = grid_Issue.FindEditFormTemplateControl("btn_Confirmation") as ASPxButton;
				ASPxButton btn_Completion = grid_Issue.FindEditFormTemplateControl("btn_Completion") as ASPxButton;
				ASPxButton btn_Billing = grid_Issue.FindEditFormTemplateControl("btn_Billing") as ASPxButton;
				ASPxButton btn_Close = grid_Issue.FindEditFormTemplateControl("btn_Close") as ASPxButton;

				sql = string.Format("select QuotationNo from JobInfo where Id='{0}'", oid);
				string quotationNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                sql = string.Format(@"select count(*) from XaArInvoice where MastRefNo='{0}' and DocType='IV'", quotationNo);
				int cnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
				//if (jobStatus == "Closed")
				//{
				//    btn.Text = "Open Job";
				//}
				sql = string.Format("select JobStage from JobInfo where Id='{0}'", oid);
				string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                if (cnt > 0 && closeInd == "Quotation")
				{
					ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobStage='Billing',DateTime6=getdate(),Value6='{1}' where QuotationNo='{0}'", quotationNo, EzshipHelper.GetUserName()));
					EzshipLog.Log(txt_DoNo.Text, "", "JO", "Billing");
					closeInd = "Billing";
				}
			}

			//string status = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "JobStatus" }));
			//EzshipHelper_Authority.Bind_Authority(pageControl, status);
		}
	}

	protected void grid_Issue_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
	{
		//string s = e.Parameters;
		//if (s == "Save")
		//{
		//    SaveSo();
		//}
	}

	protected void grid_Issue_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
	{
		ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
		ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		//ASPxTextBox txt_How0 = grid_Issue.FindEditFormTemplateControl("txt_How") as ASPxTextBox;
		ASPxTextBox txt_Quotation = grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		//ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
		ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
		ASPxDateEdit issueDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
		ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
		ASPxComboBox cmb_Status = this.grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
		string id = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
		string sql_jobNo = string.Format(@"select JobNo from JobInfo where Id='{0}'", id);
		string refNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_jobNo));
		string s = e.Parameters;
		if (s == "Save")
		{
			#region Save
            if (txt_CustomerId.Text.Trim() == "")
			{
				e.Result = "Fail! Please enter the Customer";
				return;
			}

			ASPxTextBox txt_How0 = pageControl.FindControl("txt_How") as ASPxTextBox;
			string how0 = "";
			if (txt_How0 != null)
				how0 = txt_How0.Text;
			string job_type = cmb_JobType.Text;
			if (how0.Trim() == "" && (job_type == "Outbound" || job_type == "Office Move" || job_type == "Local Move"))
			{
				e.Result = "Failed save, Please indicate how customer knows our company (under Survey Form)";
				return;
			}

			//ASPxComboBox doStatus = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
			//if (doStatus.Text == "Job Confirmation")
			//{
			//    //check purchase price and sell price
			//    string sql = string.Format(@"select count(SequenceId) from Cost where RefNo='{0}'", txt_DoNo.Text);
			//    if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
			//    {
			//        e.Result = "Fail!No Costing";
			//        return;
			//    }
			//}
			if (txt_Quotation.Text.Length > 4)
			{
				SaveJob();
				e.Result = "";//update old one
			}
			else
				e.Result = SaveJob();// new one
            #endregion
		}
		else if (s == "Close")
		{
			#region Close
            ASPxComboBox closeIndStr = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
			string sql = "select JobStatus from JobInfo where JobNo='" + txt_DoNo.Text + "'";
			string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "Closed")
			{
				sql = string.Format("update JobInfo set JobStatus='USE',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				int res = Manager.ORManager.ExecuteCommand(sql);
				if (res > 0)
				{
					EzshipLog.Log(txt_DoNo.Text, "", "JO", "Open Job");
					e.Result = "Success";
				}
				else
				{
					e.Result = "Fail";
				}
			}
			else
			{
				bool closeByEst = true;
				if (closeByEst)
				{
					sql = string.Format("update JobInfo set JobStatus='Closed',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

					int res = Manager.ORManager.ExecuteCommand(sql);
					if (res > 0)
					{
						EzshipLog.Log(txt_DoNo.Text, "", "JO", "Closed");
						e.Result = "Success";
					}
					else
						e.Result = "Fail";
				}
				else
					e.Result = "NoMatch";
			}

			#endregion
		}

		if (s == "JobSch")
		{
			#region Schedule
			//string sql = string.Format(@"select Count from JobCount where  CONVERT(varchar(100), DateFrom, 23)= '{0}'", issueDate.Date.ToString("yyyy-MM-dd"));
			//int count = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);

			//sql = string.Format(@"select Count(JobNo) from JobSchedule where  CONVERT(varchar(100), JobDate, 23) ='{0}'", issueDate.Date.ToString("yyyy-MM-dd"));
			//int totalCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);

			//if (totalCnt < count)
			//{
			string shcNo = AddNewSch(refNo);
			if (shcNo.Length >4)
			{
				string sql = string.Format(@"update JobInfo set JobStage='Schedule',DateTime5=getdate(),Value5='{1}'where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName());
				C2.Manager.ORManager.ExecuteCommand(sql);
				EzshipLog.Log(txt_Quotation.Text, "", "JO", "Job Schedule");
				e.Result = shcNo;
			}

			//}
			//else
			//{
			//    e.Result = "Fail! Today can not create new Job";
			//}
			//}
			//else
			//    e.Result = "Fail!Can not Add Job Schedule";
			#endregion
		}

		if (s == "AddDoIn")
		{
			#region Do IN
            string sql = string.Format(@" select ItemQty from JobItemList where RefNo='{0}'", SafeValue.SafeString(txt_DoNo.Text));
			DataTable tab = ConnectSql.GetTab(sql);
			int cnt = 0;
			for (int i = 0; i < tab.Rows.Count; i++)
			{
				cnt += SafeValue.SafeInt(tab.Rows[i][0], 0);
			}

			if (cnt > 0)
			{
				string jobNo = SafeValue.SafeString(txt_DoNo.Text);
				string wareHouse = ""; //SafeValue.SafeString(txt_WareHouseId.Text);
				string doNo = "";
				sql = @"Insert Into wh_DoDet2(DoNo,DoType,Product,Price,Qty1,Des1,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime])";
				sql += string.Format(@" select * from (select '{1}'as DoNo, 'IN' as DoType,ItemName as Product,ItemValue as Price
,ItemQty as Qty1,ItemMark as Des1,'ADMIN' as CreateBy,getdate() as CreateDateTime,'ADMIN' as UpdateBy,getdate() as UpdateDateTime from JobItemList where RefNo='{0}'
 ) as tab_aa where Qty1>0", txt_DoNo.Text, doNo, EzshipHelper.GetUserName());

				cnt = C2.Manager.ORManager.ExecuteCommand(sql);
				if (cnt > -1)
				{
					e.Result = doNo;
				}
				else
					e.Result = "Fail";
			}
			else
				e.Result = "No Balance Qty!";
			#endregion
		}

		if (s == "AddDoOut")
		{
			#region Do OUT

            string soNo = SafeValue.SafeString(txt_DoNo.Text);
			string wareHouse = ""; //SafeValue.SafeString(txt_WareHouseId.Text);
			string sql1 = string.Format("select doNo from wh_do where DoType='OUT' and PoNo='{0}'", soNo);
			DataTable tab_do = ConnectSql.GetTab(sql1);
			string where = "(1=0";
			for (int i = 0; i < tab_do.Rows.Count; i++)
			{
				where += string.Format(" or DoNo='{0}' ", tab_do.Rows[i][0]);
			}

			where += ")";

			int cnt = 0;
			string sql = string.Format(@" select ItemQty from JobItemList where RefNo='{0}'", txt_DoNo.Text);
			DataTable tab = ConnectSql.GetTab(sql);
			for (int i = 0; i < tab.Rows.Count; i++)
			{
				cnt += SafeValue.SafeInt(tab.Rows[i][0], 0);
			}

			if (cnt >= 0)
			{
				string doNo = SaveDoOut(soNo, "");
				sql = @"Insert Into wh_DoDet2(DoNo,DoType,Product,Price,Qty1,Des1,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime])";
				sql += string.Format(@" select * from (select '{1}'as DoNo, 'OUT' as DoType,ItemName as Product,ItemValue as Price
,ItemQty as Qty1,ItemMark as Des1,'ADMIN' as CreateBy,getdate() as CreateDateTime,'ADMIN' as UpdateBy,getdate() as UpdateDateTime from JobItemList where RefNo='{0}'
 ) as tab_aa where Qty1>0", txt_DoNo.Text, doNo, EzshipHelper.GetUserName());

				cnt = C2.Manager.ORManager.ExecuteCommand(sql);
				if (cnt > -1)
				{
					e.Result = doNo;
				}
				else
					e.Result = "Fail";
			}
			else
				e.Result = "No Balance Qty or No Lot No!";

			#endregion
		}
		else if (s == "Confirm")
		{
			#region Confirm
            ASPxLabel closeIndStr = grid_Issue.FindEditFormTemplateControl("lb_JobStatus") as ASPxLabel;
			string sql = "select JobStage from JobInfo where JobNo='" + txt_DoNo.Text + "'";
			string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "Job Confirmation")
			{
				sql = string.Format("update JobInfo set JobStage='Quotation',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				int res = Manager.ORManager.ExecuteCommand(sql);
				if (res > 0)
				{
					EzshipLog.Log(txt_DoNo.Text, "", "JO", "Quotation");
					e.Result = "Success";
				}
				else
				{
					e.Result = "Fail";
				}
			}
			else
			{
				bool closeByEst = true;//EzshipHelper.GetCloseEstInd(refN.Text, refType);
				if (closeByEst)
				{
					sql = string.Format("update JobInfo set JobStage='Job Confirmation',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

					int res = Manager.ORManager.ExecuteCommand(sql);
					if (res > 0)
					{
						EzshipLog.Log(txt_DoNo.Text, "", "JO", "Job Confirmation");
						e.Result = "Success";
					}
					else
						e.Result = "Fail";
				}
				else
					e.Result = "NoMatch";
			}

			#endregion
		}
		else if (s == "Material")
		{
			#region Material
            String[] materials = { "Brown Paper", "Bubble Park", "Cardboard", "Cartons File", "Cartons Small", "Cartons Medium",
                                     "Cartons Large", "Cartons Flat", "Corner Edge Board", "Newsprint",
                                     "Padding", "Polythene Sheet",
									 "Pre-Printed Colour - Black", "Pre-Printed Colour - Blue", "Pre-Printed Colour - Brown",
									 "Pre-Printed Colour - Green", "Pre-Printed Colour - Orange", "Pre-Printed Colour - Pink",
									 "Pre-Printed Colour - Purple", "Pre-Printed Colour - Red", "Pre-Printed Colour - Yellow",
									 "Silical Gel",
                                 "Strap:Nylon", "Strap:Steel", "StrawBoard", "Stretch Film", "Styrofoam",
                                 "Tape:Masking", "Tape:P.V.C.(OPP)", "Tissue Paper", "W/Proof Paper", "Wardrobe", "Trolley", "Others" };
			String[] shortCode = { "Brown", "B/P", "C/B", "C/F", "C/S", "C/M",
                                     "C/L", "C/F", "CEB", "N/P",
                                     "PAD", "P/S",
									 "PPC-BK", "PPC-BL", "PPC-BR",
									 "PPC-GR", "PPC-OR", "PPC-PK",
									 "PPC-PP", "PPC-RE", "PPC-YE",
									 "S/G",
                                 "S/N", "S/S", "S/B", "S/M", "S/F",
                                 "T/M", "OPP", "TP", "WPP", "W/D", "TLY", "OTH" };
			String[] units = { "Rm", "Roll", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Rm", "Ctn", "Roll",
                            "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Bag", "Kgs", "Kgs", "Roll", "Roll", "Pcs", "Roll", "Roll", "RM", "Rm", "Pcs", "Pcs", "" };
			string result = "";
			for (int i = 0; i < materials.Length; i++)
			{
				string code = SafeValue.SafeString(materials[i]);
				string shortName = SafeValue.SafeString(shortCode[i]);
				string unit = SafeValue.SafeString(units[i]);
				string sql = string.Format(@"select count(*) from Materials where RefNo='{0}' and Description='{1}'", txt_Quotation.Text, code);
				int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
				if (cnt == 0)
				{
					Material m = new Material();
					m.Description = code;
					m.Code = shortName;
					m.RequisitionNew = 0;
					m.RequisitionUsed = 0;
					m.AdditionalNew = 0;
					m.AdditionalUsed = 0;
					m.ReturnedNew = 0;
					m.ReturnedUsed = 0;
					m.TotalNew = 0;
					m.TotalUsed = 0;
					m.Unit = unit;
					m.RefNo = txt_Quotation.Text;
					Manager.ORManager.StartTracking(m, Wilson.ORMapper.InitialState.Inserted);
					Manager.ORManager.PersistChanges(m);
				}
				else
				{
					result += code + ",";
				}
			}

			if(result.Length>0) {
				e.Result = "Fail!Material";
			}

			#endregion
		}
		else if (s == "ITEM")
		{
			#region ITEM
            string sql = "";

			if (cmb_JobType.Text == "Local Move" || cmb_JobType.Text == "Outbound")
			{
				#region Local Move / OutBound
                sql = string.Format(@"SELECT Id, JobNo, ItemType, ItemName, ItemQty, ItemValue, ItemMark, ItemNote, CreateBy, CreateDateTime, UpdateBy, UpdateDateTime
FROM JobItem where JobNo='HOME'");
				DataTable dt = ConnectSql.GetTab(sql);
				for (int i = 0; i < dt.Rows.Count;i++)
				{
					JobItemList list = new JobItemList();

					list.RefNo = txt_Quotation.Text;
					list.ItemName = SafeValue.SafeString(dt.Rows[i]["ItemName"]);

					list.ItemNote = SafeValue.SafeString(dt.Rows[i]["ItemNote"]);
					list.ItemMark = SafeValue.SafeString(dt.Rows[i]["ItemMark"]);
					list.ItemQty = 0;
					list.ItemValue = 0;
					list.ItemType = SafeValue.SafeString(dt.Rows[i]["ItemType"]);
					list.CreateBy = EzshipHelper.GetUserName();
					list.CreateDateTime = DateTime.Now;
					list.JobNo = SafeValue.SafeString(dt.Rows[i]["JobNo"]);
					if (ViaItemName(list.ItemName, list.ItemType, SafeValue.SafeString(txt_Quotation.Text)) > 0)
					{
					}
					else
					{
						Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
						Manager.ORManager.PersistChanges(list);
					}
				}

				#endregion
			}
			else if (cmb_JobType.Text == "Office Move")
			{
				#region Office Move
                sql = string.Format(@"SELECT Id, JobNo, ItemType, ItemName, ItemQty, ItemValue, ItemMark, ItemNote, CreateBy, CreateDateTime, UpdateBy, UpdateDateTime
FROM JobItem where JobNo='OFFICE'");
				DataTable dt = ConnectSql.GetTab(sql);
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					JobItemList list = new JobItemList();

					list.RefNo = txt_Quotation.Text;
					list.ItemName = SafeValue.SafeString(dt.Rows[i]["ItemName"]);
					list.ItemNote = SafeValue.SafeString(dt.Rows[i]["ItemNote"]);
					list.ItemMark = SafeValue.SafeString(dt.Rows[i]["ItemMark"]);
					list.ItemQty = 0;
					list.ItemValue = 0;
					list.ItemType = SafeValue.SafeString(dt.Rows[i]["ItemType"]);
					list.CreateBy = EzshipHelper.GetUserName();
					list.CreateDateTime = DateTime.Now;
					list.JobNo = SafeValue.SafeString(dt.Rows[i]["JobNo"]);

					if (ViaItemName(list.ItemName, list.ItemType, SafeValue.SafeString(txt_Quotation.Text)) > 0)
					{
					}
					else
					{
						Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
						Manager.ORManager.PersistChanges(list);
					}
				}

				#endregion
			}
			else
			{
				e.Result = "Fail!No data";
			}

			#endregion
		}
		else if (s == "CreateJobNo")
		{
			#region Create Job No
            string jobNo = "";
			if (txt_DoNo.Text == "")
			{
				if (cmb_JobType.Text == "Storage")
				{
					jobNo = C2Setup.GetNextNo("", "Storage", DateTime.Today);
					jobNo = "SR-" + jobNo;
					C2Setup.SetNextNo("", "Storage", jobNo, DateTime.Today);
				}

				if (cmb_JobType.Text == "Outbound")
				{
					jobNo = C2Setup.GetNextNo("", "Outbound", DateTime.Today);
					jobNo = "OB-" + jobNo;
					C2Setup.SetNextNo("", "Outbound", jobNo, DateTime.Today);
				}

				if (cmb_JobType.Text == "Local Move")
				{
					jobNo = C2Setup.GetNextNo("", "Local Move", DateTime.Today);
					jobNo = "LM-" + jobNo;
					C2Setup.SetNextNo("", "Local Move", jobNo, DateTime.Today);
				}

				if (cmb_JobType.Text == "Office Move")
				{
					jobNo = C2Setup.GetNextNo("", "Office Move", DateTime.Today);
					jobNo = "OM-" + jobNo;
					C2Setup.SetNextNo("", "Office Move", jobNo, DateTime.Today);
				}

				if (cmb_JobType.Text == "Inbound")
				{
					jobNo = C2Setup.GetNextNo("", "Inbound", DateTime.Today);
					jobNo = "IB-" + jobNo;
					C2Setup.SetNextNo("", "Inbound", jobNo, DateTime.Today);
				}

				if (cmb_JobType.Text == "Air")
				{
					jobNo = C2Setup.GetNextNo("", "Air", DateTime.Today);
					jobNo = "AR-" + jobNo;
					C2Setup.SetNextNo("", "Air", jobNo, DateTime.Today);
				}

				ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobNo='{1}' where QuotationNo='{0}'", txt_Quotation.Text, jobNo));
				EzshipLog.Log(txt_Quotation.Text, "", "Job", "Create Job No");
				e.Result = jobNo;
			}

			#endregion
		}

		string jobStage = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select JobStage from JobInfo where QuotationNo='{0}'", txt_Quotation.Text)));

		#region Site Survey
        if (s == "Site Survey")
		{
			if (jobStage == "Site Survey")
			{
				e.Result = "Fail!This Job Stage is Site Survey";
				return;
			}

			if (jobStage != "Customer Inquiry" && jobStage != "Site Survey")
			{
				e.Result = "Fail!Can not Change the Job Stage";
				return;
			}
			else
			{
				ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobStage='Site Survey',DateTime2=getdate(),Value2='{1}' where QuotationNo='{0}'", txt_Quotation.Text, EzshipHelper.GetUserName()));
				EzshipLog.Log(txt_Quotation.Text, "", "JO", "Site Survey");
				e.Result = "Success";
				return;
			}
		}

		#endregion
        #region Costing
        if (s == "Costing")
		{
			if (jobStage == "Costing")
			{
				e.Result = "Fail!This Job Stage is Costing";
				return;
			}

			//if (jobStage != "Costing" && jobStage != "Site Survey")
			//{
			//    e.Result = "Fail!Can not Change the Job Stage";
			//    return;
			//}
			else
			{
				ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobStage='Costing',DateTime3=getdate(),Value3='{1}' where QuotationNo='{0}'", txt_Quotation.Text, EzshipHelper.GetUserName()));
				EzshipLog.Log(txt_Quotation.Text, "", "JO", "Costing");
				e.Result = "Success";
			}
		}

		#endregion
        #region Quotation
        if (s == "Quotation")
		{
			if (jobStage == "Quotation")
			{
				e.Result = "Fail!This Job Stage is Quotation";
				return;
			}

			//if (jobStage != "Costing"&&jobStage != "Quotation")
			//{
			//    e.Result = "Fail!Can not Change the Job Stage";
			//    return;
			//}
			else
			{
				ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobStage='Quotation',DateTime4=getdate(),Value4='{1}' where QuotationNo='{0}'", txt_Quotation.Text, EzshipHelper.GetUserName()));
				EzshipLog.Log(txt_Quotation.Text, "", "JO", "Quotation");
				e.Result = "Success";
				return;
			}
		}

		#endregion
        #region Billing
        if (s == "Billing")
		{
			if (jobStage == "Billing")
			{
				e.Result = "Fail!This Job Stage is Billing";
				return;
			}

			//if (jobStage != "Billing" && jobStage != "Quotation")
			//{
			//    e.Result = "Fail!Can not Change the Job Stage";
			//    return;
			//}
			else
			{
				ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobStage='Billing',DateTime6=getdate(),Value6='{1}' where QuotationNo='{0}'", txt_Quotation.Text, EzshipHelper.GetUserName()));
				EzshipLog.Log(txt_DoNo.Text, "", "JO", "Billing");
				e.Result = "Success";
				return;
			}
		}

		#endregion
        #region Job Schedule
        if (s == "Schedule")
		{
			if (jobStage == "Schedule")
			{
				e.Result = "Fail!This Job Stage is Job Schedule";
				return;
			}

			//if (jobStage != "Billing" && jobStage != "Job Schedule")
			//{
			//    e.Result = "Fail!Can not Change the Job Stage";
			//    return;
			//}
			else
			{
				ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobStage='Schedule',DateTime5=getdate(),Value5='{1}'where QuotationNo='{0}'", txt_Quotation.Text, EzshipHelper.GetUserName()));
				EzshipLog.Log(txt_Quotation.Text, "", "JO", "Job Schedule");
				e.Result = "Success";
				return;
			}
		}

		#endregion
        #region Job Completion
        if (s == "Close")
		{
			if (jobStage == "Close")
			{
				string sql = string.Format("update JobInfo set JobStatus='USE',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

				int res = Manager.ORManager.ExecuteCommand(sql);
				ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobStage='Schedule',DateTime6=getdate(),Value6='{1}' where QuotationNo='{0}'", txt_Quotation.Text, EzshipHelper.GetUserName()));
				if (res > 0)
				{
					EzshipLog.Log(txt_DoNo.Text, "", "JO", "Open Job");
					e.Result = "Success";
				}
				else
				{
					e.Result = "Fail";
				};
			}
			else
			{
				ConnectSql.ExecuteScalar(string.Format(@"update JobInfo set JobStage='Close',DateTime7=getdate(),Value7='{1}' where QuotationNo='{0}'", txt_Quotation.Text, EzshipHelper.GetUserName()));
				Manager.ORManager.ExecuteCommand(string.Format("update JobInfo set JobStatus='Closed',UpdateBy='{1}',UpdateDateTime='{2}' where QuotationNo='{0}'", txt_Quotation.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
				EzshipLog.Log(txt_DoNo.Text, "", "JO", "Closed");
				e.Result = "Success";
			}
		}

		#endregion
	}

	#endregion
    protected int ViaItemName(string name, string type, string doNo)
	{
		string sql = string.Format(@"select Count(*) from  JobItemList where RefNo='{0}' and ItemName='{1}' and ItemType='{2}'", doNo, name, type);
		int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
		return cnt;
	}

	#region Billing
    protected void Grid_Invoice_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
		string sql = "select QuotationNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
		this.dsInvoice.FilterExpression = "MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' ";
	}

	#endregion

    #region photo
       protected void grd_Photo_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobAttachment));
		}
	}

	protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		this.dsPhoto.FilterExpression = "RefNo='" + refN.Text + "' and FileType='Image'";
	}

	protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
		e.NewValues["UpdateDateTime"] = DateTime.Now;
	}

	protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
	}

	#endregion
    #region log
    protected void grid_Log_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		this.dsLog.FilterExpression = String.Format("RefNo='{0}'", refN.Text);//
	}

	#endregion
    #region DO out/In
    protected void grid_DoIn_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		string sql = "select JobNo from JobInfo where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
		this.dsDoIn.FilterExpression = "DoType='IN' and PoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
	}

	protected void grid_DoOut_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		string sql = "select JobNo from JobInfo where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
		this.dsDoOut.FilterExpression = " DoType='OUT' and PoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
	}

	#endregion

    protected void cmb_Status_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
	{
		ASPxComboBox cmb_Status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
		ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		string doNo = SafeValue.SafeString(txt_DoNo.Text);
		string sql = string.Format(@"select JobStage from JobInfo where JobNo='{0}'", doNo);
		string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
		if (status == "Job Confirmation")
		{
			cmb_Status.Text = "Job Confirmation";
		}

		if (status == "Customer Inquiry")
		{
			cmb_Status.Text = "Customer Inquiry";
		}

		if (status == "Job Close")
		{
			cmb_Status.Text = "Job Close";
		}

		if (status == "Canceled")
		{
			cmb_Status.Text = "Canceled";
		}
	}

	protected void cmb_WorkStatus_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
	{
		ASPxComboBox cmb_WorkStatus = grid_Issue.FindEditFormTemplateControl("cmb_WorkStatus") as ASPxComboBox;
		ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		string doNo = SafeValue.SafeString(txt_DoNo.Text);
		string sql = string.Format(@"select JobStage from JobInfo where JobNo='{0}'", doNo);
		string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
		if (status == "Job Completion")
		{
			cmb_WorkStatus.Text = "COMPLETE";
		}
	}

	protected void grid_Payment_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		this.dsReceipt.FilterExpression = String.Format("SoNo='{0}'", refN.Text);//
	}

	#region Costing
    protected void grid_Cost_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.Cost));
		}
	}

	protected void grid_Cost_DataSelect(object sender, EventArgs e)
	{
		ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
		if (grd.GetMasterRowKeyValue() != null)
		{
			string sql = "select QuotationNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
			this.dsCosting.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
		}
	}

	protected void grid_Cost_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		e.NewValues["CostQty"] = 1;
		e.NewValues["CostPrice"] = 0;
		e.NewValues["CostLocAmt"] = 0;
		e.NewValues["CostGst"] = 0;
		e.NewValues["ChgCode"] = " ";
		e.NewValues["Remark"] = " ";
		e.NewValues["CostCurrency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
		e.NewValues["CostExRate"] = 1;
	}

	protected void grid_Cost_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
	{
		ASPxTextBox txt_Quotation = grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		e.NewValues["JobType"] = "JO";
		e.NewValues["RefNo"] = txt_Quotation.Text;
		e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
		e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
		e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
		e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
		e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
	}

	protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
	{
	}

	protected void grid_Cost_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
		e.NewValues["Marking"] = SafeValue.SafeString(e.NewValues["Marking"]);
		e.NewValues["Profitmargin"] = SafeValue.SafeDecimal(e.NewValues["Profitmargin"], 0);
		e.NewValues["Amount"] = SafeValue.ChinaRound((1 + SafeValue.SafeDecimal(e.NewValues["Profitmargin"], 0)) * SafeValue.SafeDecimal(e.OldValues["Amount2"], 0), 2);
	}

	protected void grid_Cost_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
	}

	protected void grid_Cost_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
	{
	}

	protected void grid_Cost_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
	{
	}

	protected void grid_Cost_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
	{
		string id = "";
		string sql = "";
		ASPxGridView grd = sender as ASPxGridView;
		if (e.Parameters.Contains("Copy"))
		{
			id = e.Parameters.Replace("Copy", "");
			sql = string.Format("INSERT INTO Cost(Version,CostIndex,RefNo,JobNo,RefType,Amount,Amount2,Profitmargin,Amt1,Amt2,Amt3,Amt4,ExRate1,ExRate2,Curr1,Curr2,Value1,Value2,Value3,Value4,Value5,Value6,Value7,[Status],CreateBy,CreateDateTime) (SELECT 1,(select Cast(max(CostIndex) as Int)+1 from Cost where RefNo=m.RefNo),RefNo,JobNo,RefType,Amount,Amount2,Profitmargin,Amt1,Amt2,Amt3,Amt4,ExRate1,ExRate2,Curr1,Curr2,Value1,Value2,Value3,Value4,Value5,Value6,Value7,[Status],'{1}','{2}' FROM cost m WHERE SequenceId='{0}')", id, EzshipHelper.GetUserName(), DateTime.Now);
			C2.Manager.ORManager.ExecuteCommand(sql);
			sql = string.Format(@"INSERT INTO CostDet(ParentId,JobNo,CostDate,RowCreateUser,RowCreateTime,JobType,VendorId,ChgCode,ChgCodeDes,SaleQty,SalePrice,Unit,SaleCurrency,SaleExRate,SaleDocAmt,SaleLocAmt,CostPrice,CostDocAmt,CostLocAmt,SplitType,CostCurrency,costExRate,CostQty,SaleGst,CostGst,PayInd,VerifryInd,DocNo,Salesman,Status1,Status2,Status3,Status4,Date1,Date2,Date3,Date4) 
                  (SELECT (select max(SequenceId) from Cost),JobNo,CostDate,'{1}','{2}',JobType,VendorId,ChgCode,ChgCodeDes,SaleQty,SalePrice,Unit,SaleCurrency,SaleExRate,SaleDocAmt,SaleLocAmt,CostPrice,CostDocAmt,CostLocAmt,SplitType,CostCurrency,costExRate,CostQty,SaleGst,CostGst,PayInd,VerifryInd,DocNo,Salesman,Status1,Status2,Status3,Status4,Date1,Date2,Date3,Date4 FROM CostDet WHERE ParentId='{0}')", id, EzshipHelper.GetUserName(), DateTime.Now);
			C2.Manager.ORManager.ExecuteCommand(sql);
		}
		else if (e.Parameters.Contains("Revise"))
		{
			id = e.Parameters.Replace("Revise", "");
			sql = string.Format("INSERT INTO Cost(Version,CostIndex,RefNo,JobNo,RefType,Amount,[Status],CreateBy,CreateDateTime) (SELECT CAST(Version AS INT)+1,CostIndex,RefNo,JobNo,RefType,Amount,[Status],'{1}','{2}' FROM cost WHERE SequenceId='{0}')", id, EzshipHelper.GetUserName(), DateTime.Now);
			C2.Manager.ORManager.ExecuteCommand(sql);
			sql = string.Format(@"INSERT INTO CostDet(ParentId,JobNo,CostDate,RowCreateUser,RowCreateTime,JobType,VendorId,ChgCode,ChgCodeDes,SaleQty,SalePrice,Unit,SaleCurrency,SaleExRate,SaleDocAmt,SaleLocAmt,CostPrice,CostDocAmt,CostLocAmt,SplitType,CostCurrency,costExRate,CostQty,SaleGst,CostGst,PayInd,VerifryInd,DocNo,Salesman,Status1,Status2,Status3,Status4,Date1,Date2,Date3,Date4) 
                  (SELECT (select max(SequenceId) from Cost),JobNo,CostDate,'{1}','{2}',JobType,VendorId,ChgCode,ChgCodeDes,SaleQty,SalePrice,Unit,SaleCurrency,SaleExRate,SaleDocAmt,SaleLocAmt,CostPrice,CostDocAmt,CostLocAmt,SplitType,CostCurrency,costExRate,CostQty,SaleGst,CostGst,PayInd,VerifryInd,DocNo,Salesman,Status1,Status2,Status3,Status4,Date1,Date2,Date3,Date4 FROM CostDet WHERE ParentId='{0}')", id, EzshipHelper.GetUserName(), DateTime.Now);
			C2.Manager.ORManager.ExecuteCommand(sql);
			sql = string.Format("Update Cost set [Status]='OLD' where SequenceId='{0}'", id);
			C2.Manager.ORManager.ExecuteCommand(sql);
		}
		else if (e.Parameters.Contains("Void"))
		{
			id = e.Parameters.Replace("Void", "");
			sql = string.Format("Update Cost set [Status]='VOID' where SequenceId='{0}'", id);
			C2.Manager.ORManager.ExecuteCommand(sql);
		}
		else if (e.Parameters.Contains("Unvoid"))
		{
			id = e.Parameters.Replace("Unvoid", "");
			sql = string.Format("Update Cost set [Status]='USE' where SequenceId='{0}'", id);
			C2.Manager.ORManager.ExecuteCommand(sql);
		}
		else if (e.Parameters.Contains("AddNew"))
		{
			try {
				ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
				ASPxSpinEdit txt_Volumne = pageControl.FindControl("spin_Volumne") as ASPxSpinEdit;
				ASPxComboBox cmbCostMode = pageControl.FindControl("cmb_CostMode") as ASPxComboBox;
				ASPxTextBox doNo = grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
				ASPxComboBox doType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;

				string mode = "LOCAL";
				string convalue = "0";
				if(doType.Text == "Outbound")
					mode = cmbCostMode.Text;
				if(mode == "20CON")
					convalue = "25";
				if(mode == "40CON")
					convalue = "50";

				sql = string.Format("select Cast(max(CostIndex) as Int) from Cost where RefNo='{0}'", doNo.Text);
				int costIndex = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0) + 1;
				sql = string.Format("INSERT INTO Cost(Version,CostIndex,RefNo,JobNo,RefType,Amount,[Status],CreateBy,CreateDateTime,Amount2,ProfitMargin,ExRate1,ExRate2,Curr1,Curr2,Value1,Value2,Value3,Value4,Value5,Value6) Values('1','{2}','{0}','{5}','{1}','0','USE','{3}','{4}',0,0,1,1,'SGD','SGD',0,0,0,0,0,{6})", doNo.Text, doType.Text, costIndex, EzshipHelper.GetUserName(), DateTime.Now, mode, convalue);
				C2.Manager.ORManager.ExecuteCommand(sql);
				sql = string.Format(@"INSERT INTO CostDet(ParentId,RowCreateUser,RowCreateTime,RowUpdateUser,RowUpdateTime,JobType,ChgCode,ChgCodeDes,SaleQty,SalePrice,Unit,SaleCurrency,SaleExRate) 
                  (SELECT (select max(SequenceId) from Cost),'{1}','{2}','{1}','{2}','{0}',ChgCode,x.ChgcodeDes,case when isnull(Unit,'')='VOL' then '{3}' else '1' end,'0',Unit,Currency,'1' FROM SeaQuoteDet1 d left outer join XXChgCode x on x.ChgcodeId=d.ChgCode WHERE Status1='{0}' and  QuoteId='-1')", doType.Text, EzshipHelper.GetUserName(), DateTime.Now, SafeValue.SafeDecimal(0, 1));
				if (true)
					sql = string.Format(@"INSERT INTO CostDet(ParentId,RowCreateUser,RowCreateTime,RowUpdateUser,RowUpdateTime,JobType,ChgCode,ChgCodeDes,SalePrice,SaleQty,Unit,SaleCurrency,SaleExRate, Status1,CostPrice) 
                  (SELECT (select max(SequenceId) from Cost),'{1}','{2}','{1}','{2}','{0}',ChgCode,x.ChgcodeDes, d.Price  ,'1.00',x.ChgUnit,Currency,'1',GroupTitle,MinAmt FROM SeaQuoteDet1 d left outer join XXChgCode x on x.ChgcodeId=d.ChgCode WHERE Status1='{0}' and  QuoteId='-1' and Status2='{4}')", doType.Text, EzshipHelper.GetUserName(), DateTime.Now, SafeValue.SafeDecimal(0, 1), mode);
				C2.Manager.ORManager.ExecuteCommand(sql);
				e.Result = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select max(SequenceId) from Cost"));
				//ClientScriptManager cs = Page.ClientScript;
				//cs.RegisterStartupScript(this.GetType(), "", "<script type=\"text/javascript\">popubCtr1.SetHeaderText('Costing Edit');popubCtr1.SetContentUrl('CostingView.aspx?id='" + maxId+");popubCtr1.Show();</script>");
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + ex.StackTrace);
			}
		}
		else if (e.Parameters.Contains("UpdateCostDes"))
		{
			int rowIndex = SafeValue.SafeInt(e.Parameters.Replace("UpdateCostDes", ""), -1);
			if (rowIndex < 0) return;
			ASPxTextBox txtDes = grd.FindRowCellTemplateControl(rowIndex, null, "txt_Description") as ASPxTextBox;
			ASPxTextBox txtId = grd.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
			sql = string.Format("Update Cost set Description='{1}',CreateBy='{2}',CreateDateTime='{3}' where SequenceId='{0}'", txtId.Text, txtDes.Text, EzshipHelper.GetUserName(), DateTime.Now);
			C2.Manager.ORManager.ExecuteCommand(sql);
		}
	}

	protected void btn_CostEdit_Init(object sender, EventArgs e)
	{
		ASPxButton btnCostEdit = sender as ASPxButton;
		GridViewDataItemTemplateContainer container = btnCostEdit.NamingContainer as GridViewDataItemTemplateContainer;

		btnCostEdit.ClientInstanceName = String.Format("btnCostEdit{0}", container.VisibleIndex);
		btnCostEdit.ClientSideEvents.Click = String.Format("function (s, e) {{ OnCostEditClick(txtId{0}.GetText()); }}", container.VisibleIndex);
	}

	protected void txt_Id_Init(object sender, EventArgs e)
	{
		ASPxTextBox txtId = sender as ASPxTextBox;
		GridViewDataItemTemplateContainer container = txtId.NamingContainer as GridViewDataItemTemplateContainer;

		txtId.ClientInstanceName = String.Format("txtId{0}", container.VisibleIndex);
	}

	protected void btn_CostCopy_Init(object sender, EventArgs e)
	{
		ASPxButton btnCostCopy = sender as ASPxButton;
		GridViewDataItemTemplateContainer container = btnCostCopy.NamingContainer as GridViewDataItemTemplateContainer;

		btnCostCopy.ClientInstanceName = String.Format("btnCostCopy{0}", container.VisibleIndex);
		btnCostCopy.ClientSideEvents.Click = String.Format("function (s, e) {{ OnCostCopyClick(txtId{0}.GetText()); }}", container.VisibleIndex);
	}

	protected void btn_CostRevise_Init(object sender, EventArgs e)
	{
		ASPxButton btnCostRevise = sender as ASPxButton;
		GridViewDataItemTemplateContainer container = btnCostRevise.NamingContainer as GridViewDataItemTemplateContainer;

		btnCostRevise.ClientInstanceName = String.Format("btnCostRevise{0}", container.VisibleIndex);
		btnCostRevise.ClientSideEvents.Click = String.Format("function (s, e) {{ OnCostReviseClick(txtId{0}.GetText()); }}", container.VisibleIndex);
	}

	protected void btn_CostVoid_Init(object sender, EventArgs e)
	{
		ASPxButton btnCostVoid = sender as ASPxButton;
		GridViewDataItemTemplateContainer container = btnCostVoid.NamingContainer as GridViewDataItemTemplateContainer;

		btnCostVoid.ClientInstanceName = String.Format("btnCostVoid{0}", container.VisibleIndex);
		btnCostVoid.ClientSideEvents.Click = String.Format("function (s, e) {{ OnCostVoidClick(btnCostVoid{0},txtId{0}.GetText()); }}", container.VisibleIndex);
	}

	protected void btn_CostHistory_Init(object sender, EventArgs e)
	{
		ASPxButton btnCostHistory = sender as ASPxButton;
		ASPxTextBox doNo = grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		GridViewDataItemTemplateContainer container = btnCostHistory.NamingContainer as GridViewDataItemTemplateContainer;

		btnCostHistory.ClientInstanceName = String.Format("btnCostHistory{0}", container.VisibleIndex);
		btnCostHistory.ClientSideEvents.Click = String.Format("function (s, e) {{ ShowCostHistory(txtId{0}.GetText()); }}", container.VisibleIndex);
	}

	protected void btn_CostPrint_Init(object sender, EventArgs e)
	{
		ASPxButton btnCostPrint = sender as ASPxButton;
		ASPxTextBox doNo = grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		GridViewDataItemTemplateContainer container = btnCostPrint.NamingContainer as GridViewDataItemTemplateContainer;

		btnCostPrint.ClientInstanceName = String.Format("btnCostHistory{0}", container.VisibleIndex);
		btnCostPrint.ClientSideEvents.Click = String.Format("function (s, e) {{ PrintCost(txtId{0}.GetText()); }}", container.VisibleIndex);
	}

	protected void grid_Cost_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (e.Parameters.Contains("UpdateCost"))
		{
			int rowIndex = SafeValue.SafeInt(e.Parameters.Replace("UpdateCost", ""), -1);
			if (rowIndex < 0) return;
			ASPxTextBox txtRmk = grd.FindRowCellTemplateControl(rowIndex, null, "txt_Remark") as ASPxTextBox;
			ASPxTextBox txtId = grd.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
			string sql = string.Format("Update Cost set Marking='{1}',CreateBy='{2}',CreateDateTime='{3}' where SequenceId='{0}'", txtId.Text, txtRmk.Text, EzshipHelper.GetUserName(), DateTime.Now);
			C2.Manager.ORManager.ExecuteCommand(sql);
		}
	}

	#endregion

    #region Attachment

       protected void grd_Attach_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		this.dsAttachment.FilterExpression = "RefNo='" + refN.Text + "' and FileType='File'";
	}

	protected void grd_Attach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void grd_Attach_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobAttachment));
		}
	}

	protected void grd_Attach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
		e.NewValues["UpdateDateTime"] = DateTime.Now;
	}

	#endregion

    protected void cmb_JobType_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
	{
		ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
	}

	#region Item List
    protected void Grid_Packing_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
		if (grd.GetMasterRowKeyValue() != null)
		{
			string sql = "select QuotationNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
			this.dsJobItemList.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
		}
	}

	protected void Grid_Packing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		e.NewValues["ItemName"] = SafeValue.SafeString(e.NewValues["ItemName"]);
		e.NewValues["ItemQty"] = SafeValue.SafeInt(e.NewValues["ItemQty"], 0);
		e.NewValues["ItemValue"] = SafeValue.SafeDecimal(e.NewValues["ItemValue"], 0);
		e.NewValues["ItemMark"] = SafeValue.SafeString(e.NewValues["ItemMark"]);
		e.NewValues["ItemNote"] = SafeValue.SafeString(e.NewValues["ItemNote"]);

		e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
		e.NewValues["UpdateDateTime"] = DateTime.Now;
	}

	protected void Grid_Packing_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		e.NewValues["JobNo"] = "";
		e.NewValues["RefNo"] = refN.Text;
	}

	protected void Grid_Packing_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void Grid_Packing_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		e.NewValues["ItemQty"] = 0;
	}

	protected void Grid_Packing_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobItemList));
			if (!IsPostBack)
			{
				ApplyLayout(0, grd);
			}
		}
	}

	protected void Grid_Packing_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		ApplyLayout(Int32.Parse(e.Parameters), grd);
	}

	protected void ApplyLayout(int layoutIndex, ASPxGridView grid)
	{
		if (grid != null)
		{
			grid.BeginUpdate();
			try
			{
				grid.ClearSort();
				switch (layoutIndex)
				{
					case 0:
                        grid.GroupBy(grid.Columns["ItemType"]);
					break;
					case 1:
                        grid.GroupBy(grid.Columns["ItemType"]);
					grid.GroupBy(grid.Columns["ItemName"]);
					break;
					case 2:
                        grid.GroupBy(grid.Columns["ItemQty"]);
					break;
				}
			}

			finally
			{
				grid.EndUpdate();
			}

			grid.ExpandAll();
		}
	}

	#endregion

	#region Material
    protected void grid_Material_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.Material));
		}
	}

	protected void grid_Material_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		this.dsMaterial.FilterExpression = "RefNo='" + refN.Text + "'";
	}

	protected void grid_Material_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
	{
		int a1 = SafeValue.SafeInt(e.NewValues["RequisitionNew"], 0);
		int b1 = SafeValue.SafeInt(e.NewValues["RequisitionUsed"], 0);

		int a1_1 = SafeValue.SafeInt(e.NewValues["RequisitionNew1"], 0);
		int b1_1 = SafeValue.SafeInt(e.NewValues["RequisitionUsed1"], 0);

		int a1_2 = SafeValue.SafeInt(e.NewValues["RequisitionNew2"], 0);
		int b1_2 = SafeValue.SafeInt(e.NewValues["RequisitionUsed2"], 0);

		int a2 = SafeValue.SafeInt(e.NewValues["AdditionalNew"], 0);
		int b2 = SafeValue.SafeInt(e.NewValues["AdditionalUsed"], 0);

		int a2_1 = SafeValue.SafeInt(e.NewValues["AdditionalNew1"], 0);
		int b2_1 = SafeValue.SafeInt(e.NewValues["AdditionalUsed1"], 0);

		int a2_2 = SafeValue.SafeInt(e.NewValues["AdditionalNew2"], 0);
		int b2_2 = SafeValue.SafeInt(e.NewValues["AdditionalUsed2"], 0);

		int a3 = SafeValue.SafeInt(e.NewValues["ReturnedNew"], 0);
		int b3 = SafeValue.SafeInt(e.NewValues["ReturnedUsed"], 0);
		e.NewValues["TotalNew"] = SafeValue.SafeInt((a1 + a1_1 + a1_2) + (a2 + a2_1 + a2_2) - a3, 0);
		e.NewValues["TotalUsed"] = SafeValue.SafeInt((b1 + b1_1 + b1_2) + (b2 + b2_1 + b2_2) - b3, 0);
	}

	protected void grid_Material_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void grid_Material_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		int a1 = SafeValue.SafeInt(e.NewValues["RequisitionNew"], 0);
		int b1 = SafeValue.SafeInt(e.NewValues["RequisitionUsed"], 0);

		int a1_1 = SafeValue.SafeInt(e.NewValues["RequisitionNew1"], 0);
		int b1_1 = SafeValue.SafeInt(e.NewValues["RequisitionUsed1"], 0);

		int a1_2 = SafeValue.SafeInt(e.NewValues["RequisitionNew2"], 0);
		int b1_2 = SafeValue.SafeInt(e.NewValues["RequisitionUsed2"], 0);

		int a2 = SafeValue.SafeInt(e.NewValues["AdditionalNew"], 0);
		int b2 = SafeValue.SafeInt(e.NewValues["AdditionalUsed"], 0);

		int a2_1 = SafeValue.SafeInt(e.NewValues["AdditionalNew1"], 0);
		int b2_1 = SafeValue.SafeInt(e.NewValues["AdditionalUsed1"], 0);

		int a2_2 = SafeValue.SafeInt(e.NewValues["AdditionalNew2"], 0);
		int b2_2 = SafeValue.SafeInt(e.NewValues["AdditionalUsed2"], 0);

		int a3 = SafeValue.SafeInt(e.NewValues["ReturnedNew"], 0);
		int b3 = SafeValue.SafeInt(e.NewValues["ReturnedUsed"], 0);
		e.NewValues["TotalNew"] = SafeValue.SafeInt((a1 + a1_1 + a1_2) + (a2 + a2_1 + a2_2) - a3, 0);
		e.NewValues["TotalUsed"] = SafeValue.SafeInt((b1 + b1_1 + b1_2) + (b2 + b2_1 + b2_2) - b3, 0);
	}

	protected void grid_Material_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
	}

	#endregion
    #region MCST
    protected void grid_Mcst_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		this.dsJobMCST.FilterExpression = "RefNo='" + refN.Text + "'";
	}

	protected void grid_Mcst_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobMCST));
		}
	}

	protected void grid_Mcst_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		e.NewValues["RefNo"] = refN.Text;
		e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
		e.NewValues["CreateDateTime"] = DateTime.Now;
	}

	protected void grid_Mcst_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		e.NewValues["RefNo"] = refN.Text;
		e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
		e.NewValues["CreateDateTime"] = DateTime.Now;
	}

	protected void grid_Mcst_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void grid_Mcst_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
		e.NewValues["UpdateDateTime"] = DateTime.Now;
	}

	#endregion
	#region Message

    protected void grid_Message_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		this.dsMessage.FilterExpression = "RefNo='" + refN.Text + "'";
	}

	protected void grid_Message_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobMessage));
		}
	}

	protected void grid_Message_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void grid_Message_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		e.NewValues["MTitle"] = "";
		e.NewValues["MBody"] = "";
		e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
		e.NewValues["CreateDateTime"] = DateTime.Now;
	}

	protected void grid_Message_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
	{
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		e.NewValues["RefNo"] = refN.Text;
		e.NewValues["MTitle"] = SafeValue.SafeString(e.NewValues["MTitle"]);
		e.NewValues["MBody"] = SafeValue.SafeString(e.NewValues["MBody"]);
		e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
		e.NewValues["CreateDateTime"] = DateTime.Now;
	}

	#endregion

	#region Job Sch
    protected string AddNewSch(string jobNo)
	{
		try
		{
			Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(JobInfo), "JobNo='" + jobNo + "'");
			JobInfo jobInfo = C2.Manager.ORManager.GetObject(query) as JobInfo;
			string issueN = "";
			JobSchedule job = new JobSchedule();
			DateTime dt = DateTime.Now;
			issueN = C2Setup.GetNextSchNo("", "Schedule", dt);
			job.JobDate = dt;
			job.JobType = jobInfo.JobType;
			if (job.JobType == "Storage")
			{
				issueN = "SR-" + issueN;
				job.ViaWh = "Normal";
			}

			if (job.JobType == "Outbound")
			{
				issueN = "OB-" + issueN;
				job.Mode = "Lcl";
			}

			if (job.JobType == "Local Move")
			{
				issueN = "LM-" + issueN;
			}

			if (job.JobType == "Office Move")
			{
				issueN = "OM-" + issueN;
			}

			if (job.JobType == "Inbound")
			{
				issueN = "IB-" + issueN;
			}

			if (job.JobType == "Air")
			{
				issueN = "AR-" + issueN;
			}

			//Main Info
            job.WorkStatus = jobInfo.WorkStatus;

			job.DateTime7 = jobInfo.DateTime7;

			job.Value7 = jobInfo.Value7;

			job.Value1 = "";
			job.Value2 = "";
			job.Value3 = "";
			job.Note1 = "";
			job.Note2 = "";
			string userId = HttpContext.Current.User.Identity.Name;

			job.CreateBy = userId;
			job.CreateDateTime = DateTime.Now;
			job.UpdateBy = userId;
			job.UpdateDateTime = DateTime.Now;
			job.RefNo = jobInfo.JobNo;
			job.JobNo = issueN;
			job.CustomerName = jobInfo.CustomerName;
			job.OriginAdd = jobInfo.OriginAdd;
			job.DestinationAdd = jobInfo.DestinationAdd;
			job.PackRmk = jobInfo.PackRmk;
			job.VolumneRmk = jobInfo.VolumneRmk;
			job.MoveDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd")+" 09:00");
			job.Contact = SafeValue.SafeString(jobInfo.Contact +"/"+ jobInfo.Tel);
			Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
			Manager.ORManager.PersistChanges(job);
			C2Setup.SetNextNo("", "Schedule", issueN, DateTime.Now);

			#region Materials
            string sql = string.Format(@"SELECT Id, Description, Unit, RequisitionNew, RequisitionUsed, AdditionalNew, AdditionalUsed, AdditionalNew1, 
                AdditionalUsed1, AdditionalNew2, AdditionalUsed2, ReturnedNew, ReturnedUsed, TotalNew, TotalUsed, RefNo, 
                RequisitionNew1, RequisitionUsed1, RequisitionNew2, RequisitionUsed2 FROM Materials where RefNo='{0}'", job.RefNo);
			DataTable tab_material = ConnectSql.GetTab(sql);
			for (int i = 0; i < tab_material.Rows.Count; i++)
			{
				Material list = new Material();
				list.RefNo = issueN;
				list.Description = SafeValue.SafeString(tab_material.Rows[i]["Description"]);
				list.Unit = SafeValue.SafeString(tab_material.Rows[i]["Unit"]);
				list.RequisitionNew = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew"], 0);
				list.RequisitionUsed = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed"], 0);
				list.AdditionalNew = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew"], 0);
				list.AdditionalUsed = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed"], 0);
				list.AdditionalNew1 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew1"], 0);
				list.AdditionalUsed1 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed1"], 0);
				list.AdditionalNew2 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew2"], 0);
				list.AdditionalUsed2 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed2"], 0);
				list.ReturnedNew = SafeValue.SafeInt(tab_material.Rows[i]["ReturnedNew"], 0);
				list.ReturnedUsed = SafeValue.SafeInt(tab_material.Rows[i]["ReturnedUsed"], 0);
				list.RequisitionNew1 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew1"], 0);
				list.RequisitionUsed1 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed1"], 0);
				list.RequisitionNew2 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew2"], 0);
				list.RequisitionUsed2 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed2"], 0);
				list.TotalNew = SafeValue.SafeInt(tab_material.Rows[i]["TotalNew"], 0);
				list.TotalUsed = SafeValue.SafeInt(tab_material.Rows[i]["TotalUsed"], 0);
				Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
				Manager.ORManager.PersistChanges(list);
			}

			#endregion
            return job.JobNo;
		}
		catch { }
		return "";
	}

	protected void grid_sch_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
	{
		string id = "";
		string sql = "";
		string s = e.Parameters;
		if (s.Contains("Copy"))
		{
			id = e.Parameters.Replace("Copy", "");
			CopyNewSch(id);
			e.Result = "Success!";
		}
	}

	protected void btn_CopySch_Init(object sender, EventArgs e)
	{
		ASPxButton btn_CopySch = sender as ASPxButton;
		GridViewDataItemTemplateContainer container = btn_CopySch.NamingContainer as GridViewDataItemTemplateContainer;

		btn_CopySch.ClientInstanceName = String.Format("btn_CopySch{0}", container.VisibleIndex);
		btn_CopySch.ClientSideEvents.Click = String.Format("function (s, e) {{ OnCopyClick(txt_SchId{0}.GetText()); }}", container.VisibleIndex);
	}

	protected void txt_SchId_Init(object sender, EventArgs e)
	{
		ASPxTextBox txt_SchId = sender as ASPxTextBox;
		GridViewDataItemTemplateContainer container = txt_SchId.NamingContainer as GridViewDataItemTemplateContainer;

		txt_SchId.ClientInstanceName = String.Format("txt_SchId{0}", container.VisibleIndex);
	}

	protected void grid_sch_BeforePerformDataSelect(object sender, EventArgs e)
	{
		string id = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
		string sql_jobNo = string.Format(@"select JobNo from JobInfo where Id='{0}'", id);
		string refNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_jobNo));
		//ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		//throw new Exception(refN.Text);
		dsSchedule.FilterExpression = string.Format("RefNo='{0}' and len(RefNo)>0", refNo);
	}

	protected string CopyNewSch(string id)
	{
		try
		{
			Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(JobSchedule), "Id='" + id + "'");
			JobSchedule jobSch = C2.Manager.ORManager.GetObject(query) as JobSchedule;
			string issueN = "";
			JobSchedule job = new JobSchedule();
			DateTime dt = DateTime.Now;
			issueN = C2Setup.GetNextSchNo("", "Schedule", dt);
			job.JobDate = dt;

			job.JobStage = jobSch.JobStage;
			job.JobType = jobSch.JobType;
			job.Mode = jobSch.Mode;
			job.ViaWh = jobSch.ViaWh;
			job.StorageStartDate = new DateTime(1900, 1, 1);
			job.StorageFreeDays = jobSch.StorageFreeDays; //new DateTime(1900,1,1);
			if (job.JobType == "Storage")
			{
				issueN = "SR-" + issueN;
				job.ViaWh = "Normal";
			}

			if (job.JobType == "Outbound")
			{
				issueN = "OB-" + issueN;
				job.Mode = "Lcl";
			}

			if (job.JobType == "Local Move")
			{
				issueN = "LM-" + issueN;
			}

			if (job.JobType == "Office Move")
			{
				issueN = "OM-" + issueN;
			}

			if (job.JobType == "Inbound")
			{
				issueN = "IB-" + issueN;
			}

			if (job.JobType == "Air")
			{
				issueN = "AR-" + issueN;
			}

			//Main Info

            job.TruckNo = jobSch.TruckNo;

			//Additional

            job.Value1 = jobSch.Value1;
			job.Value2 = jobSch.Value2;
			job.Value3 = jobSch.Value3;

			string userId = HttpContext.Current.User.Identity.Name;
			job.RefNo = jobSch.RefNo;
			job.CreateBy = userId;
			job.CreateDateTime = DateTime.Now;
			job.UpdateBy = userId;
			job.UpdateDateTime = DateTime.Now;
			job.JobNo = issueN;
			job.DateTime1 = DateTime.Now;
			job.CustomerName = jobSch.CustomerName;
			job.OriginAdd = jobSch.OriginAdd;
			job.DestinationAdd = jobSch.DestinationAdd;
			job.PackRmk = jobSch.PackRmk;
			job.VolumneRmk = jobSch.VolumneRmk;
			job.Contact = jobSch.Contact;
			job.MoveDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd") + " 09:00");
			Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
			Manager.ORManager.PersistChanges(job);
			C2Setup.SetNextNo("", "Schedule", issueN, DateTime.Now);

			#region Item List
            string sql = string.Format(@"SELECT  Id, RefNo, JobNo, ItemType, ItemName, ItemQty, ItemValue, ItemMark, ItemNote, CreateBy, CreateDateTime, 
                UpdateBy, UpdateDateTime FROM JobItemList where RefNo='{0}'", jobSch.JobNo);
			DataTable tab = ConnectSql.GetTab(sql);
			for (int i = 0; i < tab.Rows.Count; i++)
			{
				JobItemList list = new JobItemList();

				list.RefNo = job.JobNo;
				list.ItemName = SafeValue.SafeString(tab.Rows[i]["ItemName"]);

				list.ItemNote = SafeValue.SafeString(tab.Rows[i]["ItemNote"]);
				list.ItemMark = SafeValue.SafeString(tab.Rows[i]["ItemMark"]);
				list.ItemQty = SafeValue.SafeInt(tab.Rows[i]["ItemQty"], 0);
				list.ItemValue = SafeValue.SafeDecimal(tab.Rows[i]["ItemValue"]);
				list.ItemType = SafeValue.SafeString(tab.Rows[i]["ItemType"]);
				list.CreateBy = EzshipHelper.GetUserName();
				list.CreateDateTime = DateTime.Now;
				list.JobNo = SafeValue.SafeString(tab.Rows[i]["JobNo"]);
				Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
				Manager.ORManager.PersistChanges(list);
			}

			#endregion
            #region Cost
            sql = string.Format(@"SELECT  SequenceId, Version, CostIndex, RefNo, JobNo, RefType, Amount, CreateBy, CreateDateTime, UpdateBy, 
                UpdateDateTime, Marking, Description, Status, Amount2, Profitmargin FROM  Cost where RefNo='{0}'", jobSch.JobNo);
			DataTable tab_cost = ConnectSql.GetTab(sql);
			for (int i = 0; i < tab_cost.Rows.Count; i++)
			{
				Cost list = new Cost();

				list.RefNo = job.JobNo;

				list.Version = SafeValue.SafeInt(tab_cost.Rows[i]["Version"], 0);
				list.CostIndex = SafeValue.SafeInt(tab_cost.Rows[i]["CostIndex"], 0);
				list.JobNo = SafeValue.SafeString(tab_cost.Rows[i]["JobNo"]);
				list.RefType = SafeValue.SafeString(tab_cost.Rows[i]["RefType"]);
				list.Amount = SafeValue.SafeDecimal(tab_cost.Rows[i]["ItemName"]);
				list.CreateBy = SafeValue.SafeString(tab_cost.Rows[i]["CreateBy"]);
				list.CreateDateTime = SafeValue.SafeDate(tab_cost.Rows[i]["CreateDateTime"], DateTime.Now);
				list.UpdateBy = SafeValue.SafeString(tab_cost.Rows[i]["UpdateBy"]);
				list.UpdateDateTime = SafeValue.SafeDate(tab_cost.Rows[i]["UpdateDateTime"], DateTime.Now);
				list.Marking = SafeValue.SafeString(tab_cost.Rows[i]["Marking"]);
				list.Description = SafeValue.SafeString(tab_cost.Rows[i]["Description"]);
				list.Status = SafeValue.SafeString(tab_cost.Rows[i]["Status"]);
				list.Amount2 = SafeValue.SafeDecimal(tab_cost.Rows[i]["Amount2"]);
				list.Profitmargin = SafeValue.SafeDecimal(tab_cost.Rows[i]["Profitmargin"]);
				Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
				Manager.ORManager.PersistChanges(list);
			}

			#endregion
            #region JobMCST
            sql = string.Format(@"SELECT Id, RefNo, McstNo, McstDate1, McstDate2, States, McstRemark1, McstRemark2, Amount1, Amount2, CondoTel, 
                McstRemark3, CreateBy, CreateDateTime, UpdateBy, UpdateDateTime FROM JobMCST where RefNo='{0}'", jobSch.JobNo);
			DataTable tab_mcst = ConnectSql.GetTab(sql);
			for (int i = 0; i < tab_mcst.Rows.Count; i++)
			{
				JobMCST list = new JobMCST();
				list.RefNo = job.JobNo;
				list.McstNo = SafeValue.SafeString(tab_mcst.Rows[i]["McstNo"]);
				list.McstDate1 = SafeValue.SafeDate(tab_mcst.Rows[i]["McstDate1"], DateTime.Now);
				list.McstDate2 = SafeValue.SafeDate(tab_mcst.Rows[i]["McstDate2"], DateTime.Now);
				list.States = SafeValue.SafeString(tab_mcst.Rows[i]["States"]);
				list.McstRemark1 = SafeValue.SafeString(tab_mcst.Rows[i]["McstRemark1"]);
				list.McstRemark2 = SafeValue.SafeString(tab_mcst.Rows[i]["McstRemark2"]);
				list.CreateBy = SafeValue.SafeString(tab_mcst.Rows[i]["CreateBy"]);
				list.CreateDateTime = SafeValue.SafeDate(tab_mcst.Rows[i]["CreateDateTime"], DateTime.Now);
				list.UpdateBy = SafeValue.SafeString(tab_mcst.Rows[i]["UpdateBy"]);
				list.UpdateDateTime = SafeValue.SafeDate(tab_mcst.Rows[i]["UpdateDateTime"], DateTime.Now);
				list.CondoTel = SafeValue.SafeString(tab_mcst.Rows[i]["CondoTel"]);
				list.McstRemark3 = SafeValue.SafeString(tab_mcst.Rows[i]["McstRemark3"]);
				list.Amount1 = SafeValue.SafeDecimal(tab_mcst.Rows[i]["Amount1"]);
				list.Amount2 = SafeValue.SafeDecimal(tab_mcst.Rows[i]["Amount2"]);
				Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
				Manager.ORManager.PersistChanges(list);
			}

			#endregion
            #region Materials
            sql = string.Format(@"SELECT Id, Description, Unit, RequisitionNew, RequisitionUsed, AdditionalNew, AdditionalUsed, AdditionalNew1, 
                AdditionalUsed1, AdditionalNew2, AdditionalUsed2, ReturnedNew, ReturnedUsed, TotalNew, TotalUsed, RefNo, 
                RequisitionNew1, RequisitionUsed1, RequisitionNew2, RequisitionUsed2 FROM Materials where RefNo='{0}'", jobSch.JobNo);
			DataTable tab_material = ConnectSql.GetTab(sql);
			for (int i = 0; i < tab_material.Rows.Count; i++)
			{
				Material list = new Material();
				list.RefNo = job.JobNo;
				list.Description = SafeValue.SafeString(tab_material.Rows[i]["Description"]);
				list.Unit = SafeValue.SafeString(tab_material.Rows[i]["Unit"]);
				list.RequisitionNew = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew"], 0);
				list.RequisitionUsed = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed"], 0);
				list.AdditionalNew = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew"], 0);
				list.AdditionalUsed = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed"], 0);
				list.AdditionalNew1 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew1"], 0);
				list.AdditionalUsed1 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed1"], 0);
				list.AdditionalNew2 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew2"], 0);
				list.AdditionalUsed2 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed2"], 0);
				list.ReturnedNew = SafeValue.SafeInt(tab_material.Rows[i]["ReturnedNew"], 0);
				list.ReturnedUsed = SafeValue.SafeInt(tab_material.Rows[i]["ReturnedUsed"], 0);
				list.RequisitionNew1 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew1"], 0);
				list.RequisitionUsed1 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed1"], 0);
				list.RequisitionNew2 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew2"], 0);
				list.RequisitionUsed2 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed2"], 0);
				list.TotalNew = SafeValue.SafeInt(tab_material.Rows[i]["TotalNew"], 0);
				list.TotalUsed = SafeValue.SafeInt(tab_material.Rows[i]["TotalUsed"], 0);
				Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
				Manager.ORManager.PersistChanges(list);
			}

			#endregion
            return job.JobNo;
		}
		catch { }
		return "";
	}

	protected void grid_sch_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobSchedule));
		}
	}

	protected void grid_sch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
		 
		e.NewValues["TruckNo"] = SafeValue.SafeString(e.NewValues["TruckNo"]);
		e.NewValues["CustomerName"] = SafeValue.SafeString(e.NewValues["CustomerName"]);
		e.NewValues["Value1"] = SafeValue.SafeString(e.NewValues["Value1"]);
		e.NewValues["Value2"] = SafeValue.SafeString(e.NewValues["Value2"]);
		e.NewValues["Value3"] = SafeValue.SafeString(e.NewValues["Value3"]);
		e.NewValues["WorkStatus"] = SafeValue.SafeString(e.NewValues["WorkStatus"]);
		e.NewValues["JobDate"] = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
		e.NewValues["PackRmk"] = SafeValue.SafeString(e.NewValues["PackRmk"]);
		e.NewValues["MoveRmk"] = SafeValue.SafeString(e.NewValues["MoveRmk"]);
		e.NewValues["TruckNo"] = SafeValue.SafeString(e.NewValues["TruckNo"]);
		e.NewValues["VolumneRmk"] = SafeValue.SafeString(e.NewValues["VolumneRmk"]);
		e.NewValues["Volumne"] = SafeValue.SafeDecimal(e.NewValues["Volumne"]);
		e.NewValues["TripNo"] = SafeValue.SafeString(e.NewValues["TripNo"]);
		e.NewValues["HeadCount"] = SafeValue.SafeInt(e.NewValues["HeadCount"], 0);
		e.NewValues["ViaWh"] = SafeValue.SafeString(e.NewValues["ViaWh"]);
		e.NewValues["StorageFreeDays"] = SafeValue.SafeInt(e.NewValues["StorageFreeDays"], 0);
		e.NewValues["StorageStartDate"] = SafeValue.SafeDate(e.NewValues["StorageStartDate"], DateTime.Now);
		e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
		e.NewValues["Note2"] = SafeValue.SafeString(e.NewValues["Note2"]);
		e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
		e.NewValues["UpdateDateTime"] = DateTime.Now;
		DateTime jobDate = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
		string sql = string.Format(@"select Count from JobCount where CONVERT(varchar(100), DateFrom, 23)= '{0}'", jobDate.Date.ToString("yyyy-MM-dd"));
		int count = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);

		sql = string.Format(@"select Count(*) from JobSchedule where CONVERT(varchar(100), JobDate, 23)='{0}' and WorkStatus<>'Cancel' and Id<>{1}", jobDate.Date.ToString("yyyy-MM-dd"), SafeValue.SafeInt(e.NewValues["Id"] , 0));
		int totalCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);

		if (totalCnt > count)
		{
			throw new Exception("Fail! The Day can not create new Job ("+totalCnt.ToString() + "/" + count.ToString()+")");
			return;
		}

		ASPxTimeEdit date_Time = grid.FindEditRowCellTemplateControl(null, "date_Time") as ASPxTimeEdit;
		string jobTime = SafeValue.SafeDateStr(e.NewValues["JobDate"]);
		DateTime moveDate = DateTime.Parse(jobDate.ToString("yyyy-MM-dd") + " " + date_Time.Text);
		e.NewValues["MoveDate"] = SafeValue.SafeDate(moveDate, DateTime.Now);
	}

	protected void grid_sch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	#endregion

#region warehouse schedule
    protected void grid_warehouse_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		//ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
		//ASPxLabel jobNo = grid.FindRowCellTemplateControl(grid., (DevExpress.Web.ASPxGridView.GridViewDataColumn)grid.Columns["ItemNo"], "lbl_RItemId") as ASPxLabel;
		string sql = "select JobNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
		string jobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		this.dsInventory.FilterExpression = "JobNo='" + jobNo + "' and (DoType='WI' OR DoType='WO')";
	}

	protected void grid_warehouse_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobInventory));
		}
	}

	protected void grid_warehouse_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		e.NewValues["DoType"] = "WI";
		e.NewValues["Status1"] = "Pending";
		e.NewValues["Qty"] = 0;
		e.NewValues["Date1"] = DateTime.Now;

		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		DataTable dt = D.List("select * from JobInfo where JobNo='"+refN.Text+"'");
		try{
			DataRow dr = dt.Rows[0];
			e.NewValues["Note1"] = S.Text(dr["Note17"]);
			e.NewValues["Note2"] = S.Text(dr["Note19"]);
			e.NewValues["Note3"] = S.Text(dr["Note20"]);
			e.NewValues["Note4"] = "";
			e.NewValues["Note5"] = S.Text(dr["Note18"]);
			e.NewValues["Note6"] = S.Text(dr["EntryPort"]);
			}catch {}
	}

	protected void grid_warehouse_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
	{
		ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		e.NewValues["JobNo"] = refN.Text;
		e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
		e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
		e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
		e.NewValues["CreateDateTime"] = DateTime.Now;
		e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
		e.NewValues["PalletNo"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
		e.NewValues["Unit"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
		e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
		e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
		e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
		e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], DateTime.Now);
	}

	protected void grid_warehouse_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		//e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void grid_warehouse_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		e.NewValues["JobNo"] = refN.Text;
		e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
		e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
		e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
		e.NewValues["CreateDateTime"] = DateTime.Now;
		e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
		e.NewValues["DoType"] = SafeValue.SafeString(e.NewValues["DoType"]);
		e.NewValues["Unit"] = SafeValue.SafeString(e.NewValues["Unit"]);
		e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
		e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
		e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
		e.NewValues["PalletNo"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
		e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
		e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Now);

		e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
		e.NewValues["Note2"] = SafeValue.SafeString(e.NewValues["Note2"]);
		e.NewValues["Note3"] = SafeValue.SafeString(e.NewValues["Note3"]);
		e.NewValues["Note4"] = SafeValue.SafeString(e.NewValues["Note4"]);
		e.NewValues["Note5"] = SafeValue.SafeString(e.NewValues["Note5"]);
		e.NewValues["Note6"] = SafeValue.SafeString(e.NewValues["Note6"]);
	}

	#endregion

    #region Inventory Received
    protected void grid_Inventory_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		//ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
		//ASPxLabel jobNo = grid.FindRowCellTemplateControl(grid., (DevExpress.Web.ASPxGridView.GridViewDataColumn)grid.Columns["ItemNo"], "lbl_RItemId") as ASPxLabel;
		string sql = "select JobNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
		string jobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
		this.dsInventory.FilterExpression = "JobNo='" + jobNo + "' and DoType='IN'";
	}

	protected void grid_Inventory_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.JobInventory));
		}
	}

	protected void grid_Inventory_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		e.NewValues["DoType"] = "IN";
		e.NewValues["Qty"] = 0;
		e.NewValues["DoDate"] = DateTime.Now;
	}

	protected void grid_Inventory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
	{
		ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		e.NewValues["JobNo"] = refN.Text;
		e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
		e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
		e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
		e.NewValues["CreateDateTime"] = DateTime.Now;
		e.NewValues["DoType"] = "IN";
		e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
		e.NewValues["PalletNo"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
		e.NewValues["Unit"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
		e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
		e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
		e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
		e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Now);
	}

	protected void grid_Inventory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void grid_Inventory_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
	{
		ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
		ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
		e.NewValues["JobNo"] = refN.Text;
		e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
		e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
		e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
		e.NewValues["CreateDateTime"] = DateTime.Now;
		e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
		e.NewValues["DoType"] = SafeValue.SafeString(e.NewValues["DoType"]);
		e.NewValues["Unit"] = SafeValue.SafeString(e.NewValues["Unit"]);
		e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
		e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
		e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
		e.NewValues["PalletNo"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
		e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
		e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Now);
	}

	public string GetImgUrl(string id, string jobNo)
	{
		string url = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format(@"select FilePath from Job_Attachment where JobNo='{0}' and RefNo='{1}'", id, jobNo))).Replace("\\", "/");
        return "/Photos/" + url;
    }
    #endregion
    #region Inventory Released
    protected void grid_Released_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        string sql = "select JobNo from JobInfo where Id = '" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string jobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        this.dsReleased.FilterExpression = "JobNo = '" + jobNo + "' and DoType = 'OUT'";
    }
    protected void grid_Released_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobInventory));
        }
    }
    protected void grid_Released_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DoType"] = "OUT";
        e.NewValues["DoDate"] = DateTime.Now;
    }
    protected void grid_Released_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["JobNo"] = refN.Text;
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["DoType"] = "OUT";
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"],DateTime.Now);
        e.NewValues["PalletNo"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
    }
    protected void grid_Released_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int qty = SafeValue.SafeInt(e.Values["Qty"], 0);
        int relaId = SafeValue.SafeInt(e.Values["RelaId"], 0);
        UpdateBalQty(relaId, 0);
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Released_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["JobNo"] = refN.Text;
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        int qty=SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["Qty"] = qty;
        e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["DoType"] = SafeValue.SafeString(e.NewValues["DoType"]);
        e.NewValues["Unit"] = SafeValue.SafeString(e.NewValues["Unit"]);
        e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
        e.NewValues["PalletNo"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);

        e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Now);
        int relaId = SafeValue.SafeInt(e.NewValues["RelaId"],0);
        UpdateBalQty(relaId, qty);
    }
    protected void grid_Released_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_Released_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
    }
    protected void grid_Released_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {

    }
    public void UpdateBalQty(int relaId,int qty)
    {
        string sql = string.Format(@"update JobInventory set BalQty = Qty- { 1 } where Id = { 0 }", relaId, qty);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion

    #region Item Released Photo
    protected void grid_img_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsItemImg.FilterExpression = "RefNo = '" + refN.Text + "'and JobNo = '" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "' and FileType = 'Image'";
        
    }
    protected void grid_img_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_img_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobAttachment));
        }
    }
    protected void grid_img_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void grid_img_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    #endregion

    #region Item Warehouse Photo
    protected void grid_img_W_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsItemRImg.FilterExpression = "RefNo = '" + refN.Text + "'and JobNo = '" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "' and FileType = 'Image'";}
		
    protected void grid_img_W_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
			//throw new Exception("Error Delete");
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_img_W_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobAttachment));
        }
    }
    protected void grid_img_W_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e){


    }
    protected void grid_img_W_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    #endregion
	
    #region Item Received Photo
    protected void grid_img_R_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsItemRImg.FilterExpression = "RefNo='" + refN.Text + "'and JobNo='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "' and FileType='Image'";
    }
    protected void grid_img_R_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_img_R_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobAttachment));
        }
    }
    protected void grid_img_R_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void grid_img_R_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    #endregion

    public string GetMaterials(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}'", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "NO MAT";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int totalNew = SafeValue.SafeInt(dt.Rows[i]["TotalNew"], 0);
            int totalUsed = SafeValue.SafeInt(dt.Rows[i]["TotalUsed"], 0);
            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            //string code = SafeValue.SafeString(dt.Rows[i]["Code"]);
            string code = D.Text("select top 1 code from materials where description='"+des+"' and refno='JO0001010001'");

            if (totalNew > 0)
            {
                result += totalNew + code + "(N)" + "<br />";
            }
            if (totalUsed > 0)
            {
                result += totalUsed + code + "(U)" + "<br />";
            }
 
        }
        return result;
    }
    public string GetCrews(string jobNo)
    {
        string sql = string.Format(@"select * from JobCrews where RefNo='{0}'", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        string result = "";
        string super = "";
        string ValueStr = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string status = SafeValue.SafeString(dt.Rows[i]["Status"]);
            string name = SafeValue.SafeString(dt.Rows[i]["Remark"]);

            if (status == "Supervisor")
            {
                super = name;
            }
            if (status != "Supervisor")
            {
                result += name + ",";
            }
            if (super.Length > 0)
            {
                ValueStr = super + " / " + result;
            }
            else
            {
                ValueStr = result;
            }
        }

        return ValueStr;
    }

    #region container
    protected void Grid_RefCont_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobCost));
        }
    }
    protected void Grid_RefCont_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select QuotationNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsJobCost.FilterExpression = "JobNo='0' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void Grid_RefCont_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
		e.NewValues["SalePrice"] = 0;
		e.NewValues["Remark"] = "";
        string typ = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "JobType" }));
		if(typ=="Office Move") {	
			e.NewValues["ChgCode"] = "OM";
			e.NewValues["ChgCodeDe"] = "Office move charges";
		}
		if(typ=="Local Move") {	
			e.NewValues["ChgCode"] = "LM";
			e.NewValues["ChgCodeDe"] = "Local move charges";
		}
		if(typ=="Storage") {	
			e.NewValues["ChgCode"] = "STO";
			e.NewValues["ChgCodeDe"] = "Storage charges";
		}
		if(typ=="Inbound") {	
			e.NewValues["ChgCode"] = "IBS";
			e.NewValues["ChgCodeDe"] = "Inbound service charges";
		}
		if(typ=="Outbound") {	
			e.NewValues["ChgCode"] = "DTD";
			e.NewValues["ChgCodeDe"] = "Door to door charges";
		}
    }
    protected void Grid_RefCont_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string id = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
        string sql_jobNo = string.Format(@"select QuotationNo from JobInfo where Id='{0}'", id);
        string refNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_jobNo));
		//string refno = Request.QueryString["no"] ?? "";
        e.NewValues["RefNo"] = refNo;
        e.NewValues["JobNo"] = "0";
    }
    protected void Grid_RefCont_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDe"] = SafeValue.SafeString(e.NewValues["ChgCodeDe"]);
        e.NewValues["SalePrice"] = SafeValue.SafeDecimal(e.NewValues["SalePrice"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void Grid_RefCont_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string oid = e.Values["SequenceId"].ToString();
        if (oid.Length > 0)
        {
            string sql = string.Format("delete from JobCost where SequenceId='{0}'", oid);
            int res = Manager.ORManager.ExecuteCommand(sql);
            e.Cancel = true;
        }
    }

	
	
	 protected void Grid_RefContRev_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobCost));
        }
    }
    protected void Grid_RefContRev_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select QuotationNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
		string exp = "ChgCode='-' AND RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
		//throw new Exception(exp);
        this.dsJobCostRev.FilterExpression = exp;
    }
    protected void Grid_RefContRev_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
		e.NewValues["SalePrice"] = 0;
		e.NewValues["Remark"] = "";
        e.NewValues["JobNo"] = "-";
		e.NewValues["ChgCode"] = "-";
    }
    protected void Grid_RefContRev_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string id = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
        string sql_jobNo = string.Format(@"select QuotationNo from JobInfo where Id='{0}'", id);
        string refNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_jobNo));
		//string refno = Request.QueryString["no"] ?? "";
        e.NewValues["RefNo"] = refNo;
        e.NewValues["JobNo"] = string.Format("{0:yyMMddHHmmss}",DateTime.Now);
		e.NewValues["ChgCode"] = "-";
    }
    protected void Grid_RefContRev_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgCode"] = "-"; ///SafeValue.SafeString(e.NewValues["ChgCode"]);
        //e.NewValues["SalePrice"] = 0; //SafeValue.SafeDecimal(e.NewValues["SalePrice"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void Grid_RefContRev_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string oid = e.Values["SequenceId"].ToString();
        if (oid.Length > 0)
        {
            string sql = string.Format("delete from JobCost where SequenceId='{0}'", oid);
            int res = Manager.ORManager.ExecuteCommand(sql);
            e.Cancel = true;
        }
    }
	
    #endregion


}

