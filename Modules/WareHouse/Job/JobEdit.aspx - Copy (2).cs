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
public partial class WareHouse_Job_JobEdit : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            Session["SoWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"] != "0")
            {
                Session["SoWhere"] = "JobNo='" + Request.QueryString["no"] + "'";
                this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
                this.txt_SchRefNo.Text = Request.QueryString["no"];
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"] == "0")
            {
                this.grid_Issue.AddNewRow();
            }
            else
                this.dsIssue.FilterExpression = "1=0";

        }
        if (Session["SoWhere"] != null)
        {
            this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);
        }

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
        e.NewValues["WareHouseId"] = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];
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
                issueN = C2Setup.GetNextNo("", "JobOrder", issueDate.Date);
                job.JobDate = issueDate.Date;
            }

            ASPxDateEdit doDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            job.JobDate = doDate.Date;

            ASPxComboBox cmb_Status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            job.JobStage = SafeValue.SafeString(cmb_Status.Value);
            ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
            job.JobType = SafeValue.SafeString(cmb_JobType.Value);
            //Main Info
            ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
            job.CustomerId = txt_CustomerId.Text;
            ASPxTextBox txt_CustomerName = grid_Issue.FindEditFormTemplateControl("txt_CustomerName") as ASPxTextBox;
            job.CustomerName = txt_CustomerName.Text;
            ASPxMemo memo_Address = grid_Issue.FindEditFormTemplateControl("memo_Address") as ASPxMemo;
            job.CustomerAdd = memo_Address.Text;

            ASPxTextBox txt_PostalCode = grid_Issue.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
            job.Postalcode = txt_PostalCode.Text;
            ASPxTextBox txt_Contact = grid_Issue.FindEditFormTemplateControl("txt_Contact") as ASPxTextBox;
            job.Contact = txt_Contact.Text;
            ASPxTextBox txt_Tel = grid_Issue.FindEditFormTemplateControl("txt_Tel") as ASPxTextBox;
            job.Tel = txt_Tel.Text;
            ASPxTextBox txt_Email = grid_Issue.FindEditFormTemplateControl("txt_Email") as ASPxTextBox;
            job.Email = txt_Email.Text;
            ASPxTextBox txt_Fax = grid_Issue.FindEditFormTemplateControl("txt_Fax") as ASPxTextBox;
            job.Fax = txt_Fax.Text;
            ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            job.Remark = remark.Text;
            ASPxButtonEdit currency = grid_Issue.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
            job.Currency = currency.Text;
            ASPxSpinEdit exRate = grid_Issue.FindEditFormTemplateControl("spin_ExRate") as ASPxSpinEdit;
            job.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            ASPxComboBox payTerm = grid_Issue.FindEditFormTemplateControl("cmb_PayTerm") as ASPxComboBox;
            job.PayTerm = payTerm.Text;
            ASPxDateEdit date_ExpiryDate = grid_Issue.FindEditFormTemplateControl("date_ExpiryDate") as ASPxDateEdit;
            job.ExpiryDate = date_ExpiryDate.Date;



            //ASPxButtonEdit carrier = grid_Issue.FindEditFormTemplateControl("txt_Transport") as ASPxButtonEdit;
            //job.Carrier = carrier.Text;


            //ASPxMemo txt_Remark1 = grid_Issue.FindEditFormTemplateControl("txt_Remark1") as ASPxMemo;
            //job.Remark1 = txt_Remark1.Text;

            ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            job.WareHouseId = txt_WareHouseId.Text;


            //Party
            //ASPxButtonEdit agentCode = pageControl.FindControl("txt_AgentCode") as ASPxButtonEdit;
            //job.AgentId = agentCode.Text;
            //ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            //job.AgentName = agentName.Text;
            //ASPxMemo agentAdd = pageControl.FindControl("memo_AgentAddress") as ASPxMemo;
            //job.AgentAdd = agentAdd.Text;
            //ASPxTextBox agentPostalCode = pageControl.FindControl("txt_AgentPostalCode") as ASPxTextBox;
            //job.AgentZip = agentPostalCode.Text;
            //ASPxButtonEdit agentCountry = pageControl.FindControl("txt_AgentCountry") as ASPxButtonEdit;
            //job.AgentCountry = agentCountry.Text;
            //ASPxButtonEdit agentCity = pageControl.FindControl("txt_AgentCity") as ASPxButtonEdit;
            //job.AgentCity = agentCity.Text;
            //ASPxTextBox agentTel = pageControl.FindControl("txt_AgentTelexFax") as ASPxTextBox;
            //job.AgentTel = agentTel.Text;
            //ASPxTextBox agentContact = pageControl.FindControl("txt_AgentContact") as ASPxTextBox;
            //job.AgentContact = agentContact.Text;

            //ASPxButtonEdit notifyCode = pageControl.FindControl("txt_NotifyCode") as ASPxButtonEdit;
            //job.NotifyId = notifyCode.Text;
            //ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            //job.NotifyName = notifyName.Text;
            //ASPxMemo notifyAdd = pageControl.FindControl("memo_NotifyAddress") as ASPxMemo;
            //job.NotifyAdd = notifyAdd.Text;
            //ASPxTextBox notifyPostalCode = pageControl.FindControl("txt_NotifyPostalCode") as ASPxTextBox;
            //job.NotifyZip = notifyPostalCode.Text;
            //ASPxButtonEdit notifyCountry = pageControl.FindControl("txt_NotifyCountry") as ASPxButtonEdit;
            //job.NotifyCountry = notifyCountry.Text;
            //ASPxButtonEdit notifyCity = pageControl.FindControl("txt_NotifyCity") as ASPxButtonEdit;
            //job.NotifyCity = notifyCity.Text;
            //ASPxTextBox notifyTel = pageControl.FindControl("txt_NotifyTelexFax") as ASPxTextBox;
            //job.NotifyTel = notifyTel.Text;
            //ASPxTextBox notifyContact = pageControl.FindControl("txt_NotifyContact") as ASPxTextBox;
            //job.NotifyContact = notifyContact.Text;

            //Booking Details
            ASPxButtonEdit btn_OriginPort = grid_Issue.FindEditFormTemplateControl("btn_OriginPort") as ASPxButtonEdit;
            job.OriginPort= btn_OriginPort.Text;
            ASPxButtonEdit btn_DestinationPort = grid_Issue.FindEditFormTemplateControl("btn_DestinationPort") as ASPxButtonEdit;
            job.DestinationPort = btn_DestinationPort.Text;
            ASPxMemo memo_Address1 = grid_Issue.FindEditFormTemplateControl("memo_Address1") as ASPxMemo;
            job.OriginAdd = memo_Address1.Text;
            ASPxMemo memo_Address2 = grid_Issue.FindEditFormTemplateControl("memo_Address2") as ASPxMemo;
            job.DestinationAdd= memo_Address2.Text;
            ASPxTextBox txt_Volumne = pageControl.FindControl("txt_Volumne") as ASPxTextBox;
            job.Volumne= txt_Volumne.Text;
            ASPxMemo memo_Description = pageControl.FindControl("memo_Description") as ASPxMemo;
            job.ItemDes = memo_Description.Text;

            ASPxDateEdit date_Pack = pageControl.FindControl("date_Pack") as ASPxDateEdit;
            job.PackDate = date_Pack.Date;
            ASPxComboBox cmb_Via = pageControl.FindControl("cmb_Via") as ASPxComboBox;
            job.ViaWh = cmb_Via.Text;
            ASPxDateEdit date_StorageStartDate = pageControl.FindControl("date_StorageStartDate") as ASPxDateEdit;
            job.StorageStartDate = date_StorageStartDate.Date;
            ASPxTextBox txt_StorageFreeDays = pageControl.FindControl("txt_StorageFreeDays") as ASPxTextBox;
            job.StorageFreeDays = Helper.Safe.SafeInt(txt_StorageFreeDays.Text);
            ASPxTextBox txt_TripNo = pageControl.FindControl("txt_TripNo") as ASPxTextBox;
            job.TripNo = txt_TripNo.Text;
            ASPxDateEdit date_MoveDate = pageControl.FindControl("date_MoveDate") as ASPxDateEdit;
            job.MoveDate = date_MoveDate.Date;
            ASPxSpinEdit spin_Charges = pageControl.FindControl("spin_Charges") as ASPxSpinEdit;
            job.Charges =SafeValue.SafeDecimal(spin_Charges.Value);
            ASPxButtonEdit btn_PortOfEntry = pageControl.FindControl("btn_PortOfEntry") as ASPxButtonEdit;
            job.EntryPort= btn_PortOfEntry.Text;
            ASPxComboBox txt_Mode = pageControl.FindControl("cmb_Mode") as ASPxComboBox;
            job.Mode = txt_Mode.Text;

            ASPxComboBox cmb_FullPacking = pageControl.FindControl("cmb_FullPacking") as ASPxComboBox;
            job.Item1= SafeValue.SafeString(cmb_FullPacking.Value);
            ASPxComboBox cmb_UnFull = pageControl.FindControl("cmb_UnFull") as ASPxComboBox;
            job.Item2 = SafeValue.SafeString(cmb_UnFull.Value);
            ASPxTextBox txt_Details = pageControl.FindControl("txt_Details") as ASPxTextBox;
            job.ItemDetail1 = txt_Details.Text;
            ASPxTextBox txt_UnpackDetails = pageControl.FindControl("txt_UnpackDetails") as ASPxTextBox;
            job.ItemDetail2= txt_UnpackDetails.Text;

            ASPxComboBox cmb_Insurance = pageControl.FindControl("cmb_Insurance") as ASPxComboBox;
            job.Item3 = SafeValue.SafeString(cmb_Insurance.Value);
            ASPxTextBox txt_Percentage = pageControl.FindControl("txt_Percentage") as ASPxTextBox;
            job.ItemValue3= txt_Percentage.Text;
            ASPxTextBox txt_Value = pageControl.FindControl("txt_Value") as ASPxTextBox;
            job.ItemData3= txt_Value.Text;
            ASPxSpinEdit txt_Premium = pageControl.FindControl("txt_Premium") as ASPxSpinEdit;
            job.ItemPrice3=SafeValue.SafeDecimal( txt_Premium.Text);

            ASPxComboBox cmb_PianoApply = pageControl.FindControl("cmb_PianoApply") as ASPxComboBox;
            job.Item4= SafeValue.SafeString(cmb_PianoApply.Value);
            ASPxTextBox txt_PainoDetails = pageControl.FindControl("txt_PainoDetails") as ASPxTextBox;
            job.ItemDetail4= txt_PainoDetails.Text;
            ASPxSpinEdit spin_Charges1 = pageControl.FindControl("spin_Charges1") as ASPxSpinEdit;
            job.ItemPrice4 = SafeValue.SafeDecimal(spin_Charges1.Value);

            ASPxComboBox cmb_Safe = pageControl.FindControl("cmb_Safe") as ASPxComboBox;
            job.Item5 = cmb_Safe.Text;
            ASPxTextBox txt_Brand = pageControl.FindControl("txt_Brand") as ASPxTextBox;
            job.ItemValue5 = SafeValue.SafeString(txt_Brand.Value);
            ASPxSpinEdit spin_Weight = pageControl.FindControl("spin_Weight") as ASPxSpinEdit;
            job.ItemPrice5 = SafeValue.SafeDecimal(spin_Weight.Value);

            ASPxComboBox cmb_Crating = pageControl.FindControl("cmb_Crating") as ASPxComboBox;
            job.Item6 = SafeValue.SafeString(cmb_Crating.Value);
            ASPxTextBox txt_Details1 = pageControl.FindControl("txt_Details1") as ASPxTextBox;
            job.ItemDetail6 = txt_Details1.Text;
            ASPxSpinEdit spin_Charges2 = pageControl.FindControl("spin_Charges2") as ASPxSpinEdit;
            job.ItemPrice6 = SafeValue.SafeDecimal(spin_Charges2.Value);

            ASPxComboBox cmb_Handyman = pageControl.FindControl("cmb_Handyman") as ASPxComboBox;
            job.Item7 = SafeValue.SafeString(cmb_Handyman.Value);
            ASPxComboBox cmb_Complimentory = pageControl.FindControl("cmb_Complimentory") as ASPxComboBox;
            job.ItemValue7 = SafeValue.SafeString(cmb_Complimentory.Value);
            ASPxTextBox txt_Details2 = pageControl.FindControl("txt_Details2") as ASPxTextBox;
            job.ItemDetail7 = txt_Details2.Text;
            ASPxSpinEdit spin_Charges3 = pageControl.FindControl("spin_Charges3") as ASPxSpinEdit;
            job.ItemPrice7 = SafeValue.SafeDecimal(spin_Charges3.Value);

            ASPxComboBox cmb_Maid = pageControl.FindControl("cmb_Maid") as ASPxComboBox;
            job.Item8 = SafeValue.SafeString(cmb_Maid.Value);
            ASPxComboBox cmb_Complimentory1 = pageControl.FindControl("cmb_Complimentory1") as ASPxComboBox;
            job.ItemValue8 = SafeValue.SafeString(cmb_Complimentory1.Value);
            ASPxTextBox txt_Details3 = pageControl.FindControl("txt_Details3") as ASPxTextBox;
            job.ItemDetail8 = txt_Details3.Text;
            ASPxSpinEdit spin_Charges4 = pageControl.FindControl("spin_Charges4") as ASPxSpinEdit;
            job.ItemPrice8 = SafeValue.SafeDecimal(spin_Charges4.Value);

            ASPxComboBox cmb_Shifting = pageControl.FindControl("cmb_Shifting") as ASPxComboBox;
            job.Item9 = SafeValue.SafeString(cmb_Shifting.Value);
            ASPxComboBox cmb_Complimentory2 = pageControl.FindControl("cmb_Complimentory2") as ASPxComboBox;
            job.ItemValue9 = SafeValue.SafeString(cmb_Complimentory2.Value);
            ASPxTextBox txt_Details4 = pageControl.FindControl("txt_Details4") as ASPxTextBox;
            job.ItemDetail9 = txt_Details4.Text;
            ASPxSpinEdit spin_Charges5 = pageControl.FindControl("spin_Charges5") as ASPxSpinEdit;
            job.ItemPrice9= SafeValue.SafeDecimal(spin_Charges5.Value);

            ASPxComboBox cmb_Disposal = pageControl.FindControl("cmb_Disposal") as ASPxComboBox;
            job.Item10 = cmb_Disposal.Text;
            ASPxComboBox cmb_Complimentory3 = pageControl.FindControl("cmb_Complimentory3") as ASPxComboBox;
            job.ItemValue10 = cmb_Complimentory3.Text;
            ASPxTextBox txt_Details5 = pageControl.FindControl("txt_Details5") as ASPxTextBox;
            job.ItemDetail10 = txt_Details5.Text;
            ASPxSpinEdit spin_Charges6 = pageControl.FindControl("spin_Charges6") as ASPxSpinEdit;
            job.ItemPrice10 = SafeValue.SafeDecimal(spin_Charges6.Value);

            ASPxComboBox cmb_PickUp = pageControl.FindControl("cmb_PickUp") as ASPxComboBox;
            job.Item11 = SafeValue.SafeString(cmb_PickUp.Value);
            ASPxTextBox txt_Details6 = pageControl.FindControl("txt_Details6") as ASPxTextBox;
            job.ItemDetail11 = txt_Details6.Text;

            ASPxComboBox cmb_Additional = pageControl.FindControl("cmb_Additional") as ASPxComboBox;
            job.Item12 = cmb_Additional.Text;
            ASPxTextBox txt_Details7 = pageControl.FindControl("txt_Details7") as ASPxTextBox;
            job.ItemDetail12 = txt_Details7.Text;

            ASPxComboBox cmb_BadAccess = pageControl.FindControl("cmb_BadAccess") as ASPxComboBox;
            job.Item13 = SafeValue.SafeString(cmb_BadAccess.Value);
            ASPxComboBox cmb_Origin = pageControl.FindControl("cmb_Origin") as ASPxComboBox;
            job.ItemValue13 = cmb_Origin.Text;
            ASPxComboBox cmb_Destination = pageControl.FindControl("cmb_Destination") as ASPxComboBox;
            job.ItemData13 = SafeValue.SafeString(cmb_Destination.Value);

            ASPxComboBox cmb_Storage = pageControl.FindControl("cmb_Storage") as ASPxComboBox;
            job.Item14 = SafeValue.SafeString(cmb_Storage.Value);
            ASPxComboBox cmb_Complimentory4 = pageControl.FindControl("cmb_Complimentory4") as ASPxComboBox;
            job.ItemValue14 = SafeValue.SafeString(cmb_Complimentory4.Value);
            ASPxTextBox txt_Details8 = pageControl.FindControl("txt_Details8") as ASPxTextBox;
            job.ItemDetail14 = txt_Details8.Text;
            ASPxSpinEdit spin_Charges7 = pageControl.FindControl("spin_Charges7") as ASPxSpinEdit;
            job.ItemPrice14 = SafeValue.SafeDecimal(spin_Charges7.Value);

            ASPxTextBox txt_How = pageControl.FindControl("txt_How") as ASPxTextBox;
            job.Answer1 = txt_How.Text;
            ASPxTextBox txt_Other = pageControl.FindControl("txt_Other") as ASPxTextBox;
            job.Answer2= txt_Other.Text;
            ASPxTextBox txt_Move = pageControl.FindControl("txt_Move") as ASPxTextBox;
            job.Answer3 = txt_Move.Text;

            ASPxComboBox cmb_WorkStatus = grid_Issue.FindEditFormTemplateControl("cmb_WorkStatus") as ASPxComboBox;
            job.WorkStatus =SafeValue.SafeString(cmb_WorkStatus.Value);
			
			//Mcst

            ASPxTextBox txt_McstNo1 = pageControl.FindControl("txt_McstNo1") as ASPxTextBox;
            job.McstNo1= txt_McstNo1.Text;
            ASPxDateEdit date_McstDate1 = pageControl.FindControl("date_McstDate1") as ASPxDateEdit;
            job.McstDate1 = date_McstDate1.Date;
            ASPxTextBox txt_States1 = pageControl.FindControl("txt_States1") as ASPxTextBox;
            job.States1 = txt_States1.Text;
            ASPxMemo txt_McstRemark1 = pageControl.FindControl("txt_McstRemark1") as ASPxMemo;
            job.McstRemark1 = txt_McstRemark1.Text;
            ASPxSpinEdit spin_Amount1 = pageControl.FindControl("spin_Amount1") as ASPxSpinEdit;
            job.Amount1 = SafeValue.SafeDecimal(spin_Amount1.Value);

            ASPxTextBox txt_McstNo2 = pageControl.FindControl("txt_McstNo2") as ASPxTextBox;
            job.McstNo2 = txt_McstNo2.Text;
            ASPxDateEdit date_McstDate2 = pageControl.FindControl("date_McstDate2") as ASPxDateEdit;
            job.McstDate2 = date_McstDate1.Date;
            ASPxTextBox txt_States2 = pageControl.FindControl("txt_States2") as ASPxTextBox;
            job.States2 = txt_States1.Text;
            ASPxMemo txt_McstRemark2 = pageControl.FindControl("txt_McstRemark2") as ASPxMemo;
            job.McstRemark2 = txt_McstRemark2.Text;
            ASPxSpinEdit spin_Amount2 = pageControl.FindControl("spin_Amount2") as ASPxSpinEdit;
            job.Amount2 = SafeValue.SafeDecimal(spin_Amount2.Value);
			
			            //Quotation
            ASPxMemo txt_Attention1 = pageControl.FindControl("txt_Attention1") as ASPxMemo;
            job.Attention1 = txt_Attention1.Text;
            ASPxMemo txt_Attention2 = pageControl.FindControl("txt_Attention2") as ASPxMemo;
            job.Attention2 = txt_Attention2.Text;
            ASPxMemo txt_Attention3 = pageControl.FindControl("txt_Attention3") as ASPxMemo;
            job.Attention3 = txt_Attention3.Text;
            ASPxMemo txt_Attention4 = pageControl.FindControl("txt_Attention4") as ASPxMemo;
            job.Attention4 = txt_Attention4.Text;
            ASPxMemo txt_Attention5 = pageControl.FindControl("txt_Attention5") as ASPxMemo;
            job.Attention5 = txt_Attention5.Text;
			
            if (cmb_Status.Text == "Job Confirmation")
            {
                job.WorkStatus = "Pending";
            }
            ASPxComboBox cmb_SalesId = grid_Issue.FindEditFormTemplateControl("cmb_SalesId") as ASPxComboBox;
            job.Value4 = cmb_SalesId.Text;
            //if (job.WorkStatus == "COMPLETE")
            //{
            //    job.JobStage= "Job Completion";

            //}
            //if (job.JobStage == "Job Confirmation")
            //{
            //    job.JobStatus = "Confirmed";

            //}
            //if (job.WorkStatus == "Job Close")
            //{
            //    job.JobStage = "Closed";

            //}

            ASPxMemo memo_Notes = pageControl.FindControl("memo_Notes") as ASPxMemo;
            job.Notes = memo_Notes.Text;

            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                job.CreateBy = userId;
                job.CreateDateTime = DateTime.Now;
                job.UpdateBy = userId;
                job.UpdateDateTime = DateTime.Now;
                job.JobNo = issueN;
                Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(job);
                C2Setup.SetNextNo("", "JobOrder", issueN, issueDate.Date);
            }
            else
            {
                job.UpdateBy = userId;
                job.UpdateDateTime = DateTime.Now;
                bool isAddLog = false;
                if (job.JobStatus== SafeValue.SafeString(ConnectSql.ExecuteScalar("Select JobStatus from JobInfo where JobNo='" + job.JobNo + "'")))
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
            Session["SoWhere"] = "JobNo='" + job.JobNo + "'";
            this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);

            return job.JobNo;
        }
        catch(Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
        return "";
    }
    protected string SaveNewJob()
    {
        try
        {

            ASPxDateEdit issueDate = ASPxPopupControl1.FindControl("date_IssueDate") as ASPxDateEdit;
            //const string runType = "DOOUT";
            string issueN = "";
            JobInfo job = new JobInfo();
            issueN = C2Setup.GetNextNo("", "JobOrder", issueDate.Date);
            job.JobDate =DateTime.Now;

            job.JobStage = "Customer Inquir";
            ASPxComboBox cmb_JobType = ASPxPopupControl1.FindControl("cmb_JobType") as ASPxComboBox;
            job.JobType = SafeValue.SafeString(cmb_JobType.Value);

            //Main Info
            ASPxButtonEdit txt_CustId = ASPxPopupControl1.FindControl("txt_CustId") as ASPxButtonEdit;
            job.CustomerId = txt_CustId.Text;
            ASPxTextBox txt_CustName = ASPxPopupControl1.FindControl("txt_CustName") as ASPxTextBox;
            job.CustomerName = txt_CustName.Text;
            ASPxMemo memo_Address = ASPxPopupControl1.FindControl("memo_NewAddress") as ASPxMemo;
            job.CustomerAdd = memo_Address.Text;

            ASPxTextBox txt_PostalCode = ASPxPopupControl1.FindControl("txt_NewPostalCode") as ASPxTextBox;
            job.Postalcode = txt_PostalCode.Text;
            ASPxTextBox txt_Contact = ASPxPopupControl1.FindControl("txt_NewContact") as ASPxTextBox;
            job.Contact = txt_Contact.Text;
            ASPxTextBox txt_Tel = ASPxPopupControl1.FindControl("txt_NewTel") as ASPxTextBox;
            job.Tel = txt_Tel.Text;
            ASPxTextBox txt_Email = ASPxPopupControl1.FindControl("txt_NewEmail") as ASPxTextBox;
            job.Email = txt_Email.Text;
            ASPxTextBox txt_Fax = ASPxPopupControl1.FindControl("txt_NewFax") as ASPxTextBox;
            job.Fax = txt_Fax.Text;
            ASPxMemo remark = ASPxPopupControl1.FindControl("txt_NewRemark") as ASPxMemo;
            job.Remark = remark.Text;
            job.WorkStatus = "PENDING";


            string userId = HttpContext.Current.User.Identity.Name;

            job.CreateBy = userId;
            job.CreateDateTime = DateTime.Now;
            job.UpdateBy = userId;
            job.UpdateDateTime = DateTime.Now;
            job.JobNo = issueN;
            Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(job);
            C2Setup.SetNextNo("", "JobOrder", issueN, issueDate.Date);

            return job.JobNo;
        }
        catch { }
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
            ASPxButtonEdit ConsigneeCode = grid_Issue.FindEditFormTemplateControl("txt_ConsigneeCode") as ASPxButtonEdit;
            whDo.PartyId = ConsigneeCode.Text;
            ASPxTextBox ConsigneeName = grid_Issue.FindEditFormTemplateControl("txt_ConsigneeName") as ASPxTextBox;
            whDo.PartyName = ConsigneeName.Text;
            ASPxButtonEdit consigneeCountry = grid_Issue.FindEditFormTemplateControl("txt_Country") as ASPxButtonEdit;
            whDo.PartyCountry = consigneeCountry.Text;
            ASPxButtonEdit consigneeCity = grid_Issue.FindEditFormTemplateControl("txt_City") as ASPxButtonEdit;
            whDo.PartyCity = consigneeCity.Text;
            ASPxMemo consigneeAdd = grid_Issue.FindEditFormTemplateControl("memo_Address") as ASPxMemo;
            whDo.PartyAdd = consigneeAdd.Text;
            ASPxTextBox consigneeZip = grid_Issue.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
            whDo.PartyPostalcode = consigneeZip.Text;
            ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            whDo.Remark = remark.Text;
            whDo.Priority = "EXPORT";

            //Issue Info
            ASPxTextBox vessel = pageControl.FindControl("txt_Vessel") as ASPxTextBox;
            whDo.Vessel = vessel.Text;
            ASPxTextBox voyage = pageControl.FindControl("txt_Voyage") as ASPxTextBox;
            whDo.Voyage = voyage.Text;
            ASPxTextBox hbl = pageControl.FindControl("txt_HBL") as ASPxTextBox;
            whDo.Hbl = hbl.Text;
            ASPxTextBox obl = pageControl.FindControl("txt_OBL") as ASPxTextBox;
            whDo.Obl = obl.Text;
            ASPxDateEdit etaDest = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            whDo.EtaDest = etaDest.Date;
            ASPxTextBox carrier = pageControl.FindControl("txt_DriveName") as ASPxTextBox;
            whDo.Carrier = carrier.Text;
            ASPxTextBox vehicle = pageControl.FindControl("txt_Vehno") as ASPxTextBox;
            whDo.Vehicle = vehicle.Text;
            ASPxButtonEdit pol = pageControl.FindControl("txt_POL") as ASPxButtonEdit;
            whDo.Pol = pol.Text;
            ASPxButtonEdit pod = pageControl.FindControl("txt_POD") as ASPxButtonEdit;
            whDo.Pod = pod.Text;
            ASPxDateEdit etdPol = pageControl.FindControl("txt_EtdPol") as ASPxDateEdit;
            whDo.Etd = etdPol.Date;
            ASPxDateEdit etaPod = pageControl.FindControl("txt_EtaPod") as ASPxDateEdit;
            whDo.Eta = etaPod.Date;

            //Party
            ASPxButtonEdit agentCode = pageControl.FindControl("txt_AgentCode") as ASPxButtonEdit;
            whDo.AgentId = agentCode.Text;
            ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            whDo.AgentName = agentName.Text;
            ASPxMemo agentAdd = pageControl.FindControl("memo_AgentAddress") as ASPxMemo;
            whDo.AgentAdd = agentAdd.Text;
            ASPxTextBox agentPostalCode = pageControl.FindControl("txt_AgentPostalCode") as ASPxTextBox;
            whDo.AgentZip = agentPostalCode.Text;
            ASPxButtonEdit agentCountry = pageControl.FindControl("txt_AgentCountry") as ASPxButtonEdit;
            whDo.AgentCountry = agentCountry.Text;
            ASPxButtonEdit agentCity = pageControl.FindControl("txt_AgentCity") as ASPxButtonEdit;
            whDo.AgentCity = agentCity.Text;
            ASPxTextBox agentTel = pageControl.FindControl("txt_AgentTelexFax") as ASPxTextBox;
            whDo.AgentTel = agentTel.Text;
            ASPxTextBox agentContact = pageControl.FindControl("txt_AgentContact") as ASPxTextBox;
            whDo.AgentContact = agentContact.Text;

            ASPxButtonEdit notifyCode = pageControl.FindControl("txt_NotifyCode") as ASPxButtonEdit;
            whDo.NotifyId = notifyCode.Text;
            ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            whDo.NotifyName = notifyName.Text;
            ASPxMemo notifyAdd = pageControl.FindControl("memo_NotifyAddress") as ASPxMemo;
            whDo.NotifyAdd = notifyAdd.Text;
            ASPxTextBox notifyPostalCode = pageControl.FindControl("txt_NotifyPostalCode") as ASPxTextBox;
            whDo.NotifyZip = notifyPostalCode.Text;
            ASPxButtonEdit notifyCountry = pageControl.FindControl("txt_NotifyCountry") as ASPxButtonEdit;
            whDo.NotifyCountry = notifyCountry.Text;
            ASPxButtonEdit notifyCity = pageControl.FindControl("txt_NotifyCity") as ASPxButtonEdit;
            whDo.NotifyCity = notifyCity.Text;
            ASPxTextBox notifyTel = pageControl.FindControl("txt_NotifyTelexFax") as ASPxTextBox;
            whDo.NotifyTel = notifyTel.Text;
            ASPxTextBox notifyContact = pageControl.FindControl("txt_NotifyContact") as ASPxTextBox;
            whDo.NotifyContact = notifyContact.Text;
            ASPxMemo txt_CollectFrom = grid_Issue.FindEditFormTemplateControl("txt_CollectFrom") as ASPxMemo;
            whDo.CollectFrom = txt_CollectFrom.Text;
            ASPxMemo txt_DeliveryTo = grid_Issue.FindEditFormTemplateControl("txt_DeliveryTo") as ASPxMemo;
            whDo.DeliveryTo = txt_DeliveryTo.Text;
            whDo.WareHouseId = wh;

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
    protected void grid_Issue_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Issue.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxComboBox cmb_Status = this.grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            ASPxComboBox cmb_JobType = this.grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
            //ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            //ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            //agentName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "AgentId" }));
            //notifyName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "NotifyId" }));
            string oid = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                string sql = string.Format("select JobStage from JobInfo where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = grid_Issue.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_Void = grid_Issue.FindEditFormTemplateControl("btn_Void") as ASPxButton;
                ASPxLabel lbl_Inquir = grid_Issue.FindEditFormTemplateControl("lbl_Inquir") as ASPxLabel;
                ASPxLabel lbl_Survey = grid_Issue.FindEditFormTemplateControl("lbl_Survey") as ASPxLabel;
                ASPxLabel lbl_Costing = grid_Issue.FindEditFormTemplateControl("lbl_Costing") as ASPxLabel;
                ASPxLabel lbl_Quotation = grid_Issue.FindEditFormTemplateControl("lbl_Quotation") as ASPxLabel;
                ASPxLabel lbl_Confirmation = grid_Issue.FindEditFormTemplateControl("lbl_Confirmation") as ASPxLabel;
                ASPxLabel lbl_Completion = grid_Issue.FindEditFormTemplateControl("lbl_Completion") as ASPxLabel;
                ASPxLabel lbl_Billing = grid_Issue.FindEditFormTemplateControl("lbl_Billing") as ASPxLabel;
                ASPxLabel lbl_Close = grid_Issue.FindEditFormTemplateControl("lbl_Close") as ASPxLabel;

                sql = string.Format(@"select count(*) from XaArInvoice where MastRefNo='{0}' and DocType='IV'", txt_DoNo.Text);
                int cnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);

                if (closeInd == "Customer Inquir")
                {
                    lbl_Inquir.BackColor = System.Drawing.Color.Goldenrod;
                    lbl_Survey.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Costing.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Quotation.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Confirmation.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Completion.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Billing.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Close.BackColor = System.Drawing.Color.SkyBlue;
                }
                else if (closeInd == "Site Survey")
                {
                    lbl_Inquir.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Survey.BackColor = System.Drawing.Color.Goldenrod;
                    lbl_Costing.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Quotation.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Confirmation.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Completion.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Billing.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Close.BackColor = System.Drawing.Color.SkyBlue;
                }
                else if (closeInd == "Costing")
                {
                    lbl_Inquir.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Survey.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Costing.BackColor = System.Drawing.Color.Goldenrod;
                    lbl_Quotation.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Confirmation.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Completion.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Billing.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Close.BackColor = System.Drawing.Color.SkyBlue;
                }
                else if (closeInd == "Quotation")
                {
                    lbl_Inquir.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Survey.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Costing.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Quotation.BackColor = System.Drawing.Color.Goldenrod;
                    lbl_Confirmation.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Completion.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Billing.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Close.BackColor = System.Drawing.Color.SkyBlue;
                }
                else if (closeInd == "Job Confirmation")
                {
                    lbl_Inquir.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Survey.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Costing.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Quotation.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Confirmation.BackColor = System.Drawing.Color.Goldenrod;
                    lbl_Completion.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Billing.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Close.BackColor = System.Drawing.Color.SkyBlue;
                    cmb_JobType.ReadOnly = true;
                }
                else if (closeInd == "Job Completion")
                {
                    lbl_Inquir.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Survey.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Costing.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Quotation.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Confirmation.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Completion.BackColor = System.Drawing.Color.Goldenrod;
                    lbl_Billing.BackColor = System.Drawing.Color.SkyBlue;
                    lbl_Close.BackColor = System.Drawing.Color.SkyBlue;
                    cmb_JobType.ReadOnly = true;
                    
                }
                else if (cnt > 0 && closeInd == "Billing")
                {
                    lbl_Inquir.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Survey.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Costing.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Quotation.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Confirmation.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Completion.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Billing.BackColor = System.Drawing.Color.Goldenrod;
                    lbl_Close.BackColor = System.Drawing.Color.SkyBlue;
                    cmb_JobType.ReadOnly = true;
                    
                }
                else if (closeInd == "Job Close")
                {
                    btn.Text = "Open Job";
                    lbl_Inquir.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Survey.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Costing.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Quotation.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Confirmation.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Completion.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Billing.BackColor = System.Drawing.Color.Aquamarine;
                    lbl_Close.BackColor = System.Drawing.Color.Goldenrod;
                    cmb_JobType.ReadOnly = true;
                    
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
        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
        string s = e.Parameters;
        if (s == "Save")
        {
            #region Save
            if (txt_CustomerId.Text.Trim() == "")
            {
                e.Result = "Fail! Please enter the Customer";
                return;
            }
            ASPxComboBox doStatus = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            if (doStatus.Text == "Job Confirmation")
            {
                //check purchase price and sell price
                string sql = string.Format(@"select count(id) from JobCosting 
 where JobNo='{0}'", txt_DoNo.Text);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
                {
                    e.Result = "NO Costing";
                    return;
                }
            }
            if (txt_DoNo.Text.Length > 4)
            {
                SaveJob();
                e.Result = "";//update old one
            }
            else
                e.Result = SaveJob();// new one
            #endregion
        }
        if (s == "OK")
        {
            #region OK
            ASPxComboBox cmb_JobType = ASPxPopupControl1.FindControl("cmb_JobType") as ASPxComboBox;
            ASPxButtonEdit txt_CustId = ASPxPopupControl1.FindControl("txt_CustId") as ASPxButtonEdit;
            if (txt_CustId.Text.Trim() == "")
            {
                e.Result = "Fail! Please enter the Customer";
                return;
            }
            if (cmb_JobType.Text.Trim() == "")
            {
                e.Result = "Fail! Please enter the JobType";
                return;
            }

            else
            {
                e.Result = SaveNewJob();
            }
            #endregion
        }
        else if (s == "Close")
        {
            #region Close
            ASPxComboBox closeIndStr = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            string sql = "select JobStage from JobInfo where JobNo='" + txt_DoNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "Job Close")
            {
                sql = string.Format("update JobInfo set JobStage='Billing',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                bool closeByEst = true;//EzshipHelper.GetCloseEstInd(refN.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update JobInfo set JobStage='Job Close',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    sql = string.Format("update JobInfo set WorkStatus='COMPLETE',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(txt_DoNo.Text, "", "JO", "COMPLETE");
                        EzshipLog.Log(txt_DoNo.Text, "", "JO", "Job Close");
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
                                     "Padding", "Polythene Sheet", "Pre-Printed Colour", "Silical Gel",
                                 "Strap:Nylon","Strap:Steel","StrawBoard","Stretch Film","Styrofoam",
                                 "Tape:Masking","Tape:P.V.C.(OPP)","Tissue Paper","W/Proof Paper","Wardrobe","Others"};
			String[] units = { "Rm", "Roll", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Pcs", "Rm", "Ctn", "Roll",
                            "Pcs","Bag","Kgs","Kgs","Roll","Roll","Pcs","Roll","Roll","RM","Rm","Pcs",""};
            string result = "";
            for (int i = 0; i < materials.Length; i++)
            {

                string code = SafeValue.SafeString(materials[i]);
				string unit = SafeValue.SafeString(units[i]);
                string sql = string.Format(@"select count(*) from Materials where RefNo='{0}' and Description='{1}'",txt_DoNo.Text,code);
                int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (cnt == 0)
                {
                    Material m = new Material();
                    m.Description = code;
                    m.RequisitionNew = 0;
                    m.RequisitionUsed = 0;
                    m.AdditionalNew = 0;
                    m.AdditionalUsed = 0;
                    m.ReturnedNew = 0;
                    m.ReturnedUsed = 0;
                    m.TotalNew = 0;
                    m.TotalUsed = 0;
					m.Unit = unit;
                    m.RefNo = txt_DoNo.Text;
                    Manager.ORManager.StartTracking(m, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(m);
                }
                else
                {
                    result += code + ",";
                }
            } 
            if(result.Length>0){
                e.Result = "Fail!Material";
            }
            #endregion
        }
    }

    #endregion



    #region Billing
    protected void Grid_Invoice_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsInvoice.FilterExpression = "MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    #endregion

    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsIssuePhoto.FilterExpression = "RefNo='" + refN.Text + "'";
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
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsLog.FilterExpression = String.Format("RefNo='{0}'", refN.Text);//
    }
    #endregion
    #region DO out
    protected void grid_DoIn_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsDoIn.FilterExpression = "DoType='Out' and PoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
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
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select JobNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Cost));
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
        ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["JobType"] = "JO";
        e.NewValues["RefNo"] = txt_DoNo.Text;
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
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
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
        string sql ="";
        ASPxGridView grd = sender as ASPxGridView;
        if (e.Parameters.Contains("Copy"))
        {
            id=e.Parameters.Replace("Copy", "");
            sql = string.Format("INSERT INTO Cost(Version,CostIndex,RefNo,JobNo,RefType,Amount,[Status],CreateBy,CreateDateTime) (SELECT 1,(select Cast(max(CostIndex) as Int)+1 from Cost),RefNo,JobNo,RefType,Amount,[Status],'{1}','{2}' FROM cost WHERE SequenceId='{0}')", id, EzshipHelper.GetUserName(), DateTime.Now);
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
            ASPxTextBox doNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
            ASPxComboBox doType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
            sql = string.Format("select Cast(max(CostIndex) as Int) from Cost where RefNo='{0}'", doNo.Text);
            int costIndex = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0) + 1;
            sql = string.Format("INSERT INTO Cost(Version,CostIndex,RefNo,JobNo,RefType,Amount,[Status],CreateBy,CreateDateTime) Values('1','{2}','{0}','','{1}','0','USE','{3}','{4}')", doNo.Text, doType.Text,costIndex, EzshipHelper.GetUserName(), DateTime.Now);
            C2.Manager.ORManager.ExecuteCommand(sql);
            e.Result = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select max(SequenceId) from Cost"));
            //ClientScriptManager cs = Page.ClientScript;
            //cs.RegisterStartupScript(this.GetType(), "", "<script type=\"text/javascript\">popubCtr1.SetHeaderText('Costing Edit');popubCtr1.SetContentUrl('CostingView.aspx?id='" + maxId+");popubCtr1.Show();</script>");
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
    #endregion

    #region Attachment
	
    protected void grd_Attach_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsIssuePhoto.FilterExpression = "RefNo='" + refN.Text + "'";
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
            grd.ForceDataRowType(typeof(C2.WhAttachment));
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
        ASPxButtonEdit doNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        GridViewDataItemTemplateContainer container = btnCostHistory.NamingContainer as GridViewDataItemTemplateContainer;

        btnCostHistory.ClientInstanceName = String.Format("btnCostHistory{0}", container.VisibleIndex);
        btnCostHistory.ClientSideEvents.Click = String.Format("function (s, e) {{ ShowCostHistory(txtId{0}.GetText()); }}", container.VisibleIndex);
    }
    protected void btn_CostPrint_Init(object sender, EventArgs e)
    {
        ASPxButton btnCostPrint = sender as ASPxButton;
        ASPxButtonEdit doNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        GridViewDataItemTemplateContainer container = btnCostPrint.NamingContainer as GridViewDataItemTemplateContainer;

        btnCostPrint.ClientInstanceName = String.Format("btnCostHistory{0}", container.VisibleIndex);
        btnCostPrint.ClientSideEvents.Click = String.Format("function (s, e) {{ PrintCost(txtId{0}.GetText()); }}", container.VisibleIndex);
    }
    protected void txt_Description_Init(object sender, EventArgs e)
    {
        ASPxTextBox txtDes = sender as ASPxTextBox;
        GridViewDataItemTemplateContainer container = txtDes.NamingContainer as GridViewDataItemTemplateContainer;
        txtDes.ClientSideEvents.TextChanged = String.Format("function (s, e) {{ UpdateCost({0}); }}", container.VisibleIndex);
    }
    protected void txt_Remark_Init(object sender, EventArgs e)
    {
        ASPxTextBox txtRmk = sender as ASPxTextBox;
        GridViewDataItemTemplateContainer container = txtRmk.NamingContainer as GridViewDataItemTemplateContainer;
        txtRmk.ClientSideEvents.TextChanged = String.Format("function (s, e) {{ UpdateCost({0}); }}", container.VisibleIndex);
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
    #region Packing
	
    protected void Grid_Packing_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select JobNo from JobInfo where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsWhPacking.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void Grid_Packing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobNo"] = "";
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["StatusCode"] = "USE";
        //string jobType = EzshipHelper.GetJobType("SI", refN.Text);
        //if (jobType == "FCL")
        //    e.NewValues["MkgType"] = "Cont";
        //else
        //    e.NewValues["MkgType"] = "BL";
    }
    protected void Grid_Packing_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobNo"] = "";
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["StatusCode"] = "USE";
    }
    protected void Grid_Packing_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void Grid_Packing_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Volume"] = 0;
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["PackageType"] = " ";
        e.NewValues["MkgType"] = "Cont";
        e.NewValues["ContainerType"] = " ";
        e.NewValues["RefStatusCode"] = "USE";
        e.NewValues["StatusCode"] = "USE";
        ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;

    }
    protected void Grid_Packing_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhPacking));
        }
    }
    #endregion

	#region Crews
    protected void grid_Crews_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsCrews.FilterExpression = "RefNo='" + refN.Text + "'";
    }
    protected void grid_Crews_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobCrews));
        }
    }
    protected void grid_Crews_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }
    protected void grid_Crews_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Crews_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
    protected void grid_Crews_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

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
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsMaterial.FilterExpression = "RefNo='" + refN.Text + "'";
    }
    protected void grid_Material_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int a1=SafeValue.SafeInt(e.NewValues["RequisitionNew"],0);
        int a2=SafeValue.SafeInt(e.NewValues["AdditionalNew"],0);
        int a2_1 = SafeValue.SafeInt(e.NewValues["AdditionalNew1"], 0);
        int a2_2 = SafeValue.SafeInt(e.NewValues["AdditionalNew2"], 0);
        int a3=SafeValue.SafeInt(e.NewValues["ReturnedNew"],0);
        int b1=SafeValue.SafeInt(e.NewValues["RequisitionUsed"],0);
        int b2=SafeValue.SafeInt(e.NewValues["AdditionalUsed"],0);
        int b2_1 = SafeValue.SafeInt(e.NewValues["AdditionalUsed"], 0);
        int b2_2 = SafeValue.SafeInt(e.NewValues["AdditionalUsed"], 0);
        int b3=SafeValue.SafeInt(e.NewValues["ReturnedUsed"],0);
        e.NewValues["TotalNew"] = SafeValue.SafeInt(a1+(a2+a2_1+a2_2)+a3,0);
        e.NewValues["TotalUsed"] = SafeValue.SafeInt(b1 + (b2+b2_1+b2_2) +b3, 0);
    }
    protected void grid_Material_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Material_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int a1=SafeValue.SafeInt(e.NewValues["RequisitionNew"],0);
        int a2=SafeValue.SafeInt(e.NewValues["AdditionalNew"],0);
        int a2_1 = SafeValue.SafeInt(e.NewValues["AdditionalNew1"], 0);
        int a2_2 = SafeValue.SafeInt(e.NewValues["AdditionalNew2"], 0);
        int a3=SafeValue.SafeInt(e.NewValues["ReturnedNew"],0);
        int b1=SafeValue.SafeInt(e.NewValues["RequisitionUsed"],0);
        int b2=SafeValue.SafeInt(e.NewValues["AdditionalUsed"],0);
        int b2_1 = SafeValue.SafeInt(e.NewValues["AdditionalUsed"], 0);
        int b2_2 = SafeValue.SafeInt(e.NewValues["AdditionalUsed"], 0);
        int b3=SafeValue.SafeInt(e.NewValues["ReturnedUsed"],0);
        e.NewValues["TotalNew"] = SafeValue.SafeInt(a1+(a2+a2_1+a2_2)+a3,0);
        e.NewValues["TotalUsed"] = SafeValue.SafeInt(b1 + (b2+b2_1+b2_2) +b3, 0);
    }
    protected void grid_Material_BeforePerformDataSelect1(object sender, EventArgs e)
    {

    }

    protected void grid_Material_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    #endregion
}
