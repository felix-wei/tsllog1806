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
            ASPxComboBox incoTerm = grid_Issue.FindEditFormTemplateControl("cmb_IncoTerm") as ASPxComboBox;
            job.IncoTerm = incoTerm.Text;



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
            ASPxTextBox txt_TripNo = pageControl.FindControl("txt_TripNo") as ASPxTextBox;
            job.TripNo = txt_TripNo.Text;
            ASPxDateEdit date_MoveDate = pageControl.FindControl("date_MoveDate") as ASPxDateEdit;
            job.MoveDate = date_MoveDate.Date;
            ASPxSpinEdit spin_Charges = pageControl.FindControl("spin_Charges") as ASPxSpinEdit;
            job.Charges =SafeValue.SafeDecimal(spin_Charges.Value);
            ASPxButtonEdit btn_PortOfEntry = pageControl.FindControl("btn_PortOfEntry") as ASPxButtonEdit;
            job.EntryPort= btn_PortOfEntry.Text;
            ASPxTextBox txt_Mode = pageControl.FindControl("txt_Mode") as ASPxTextBox;
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
            if (cmb_Status.Text == "Job Confirmation")
            {
                job.WorkStatus = "PENDING";
            }
            ASPxComboBox cmb_SalesId = grid_Issue.FindEditFormTemplateControl("cmb_SalesId") as ASPxComboBox;
            job.Value2 = cmb_SalesId.Text;
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
        catch { }
        return "";
    }
    protected string SaveNewJob()
    {
        try
        {

            ASPxDateEdit issueDate = ASPxPopupControl1.FindControl("date_IssueDate") as ASPxDateEdit;
            bool isNew = false;
            //const string runType = "DOOUT";
            string issueN = "";
            JobInfo job = new JobInfo();
            isNew = true;
            issueN = C2Setup.GetNextNo("", "JobOrder", issueDate.Date);
            job.JobDate = issueDate.Date;

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
            e.Result = SaveNewJob();
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
        #region void
        //if (s == "Void")
        //{
        //   
        //    //billing
        //    string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='WH' and MastRefNo='{0}'", txt_DoNo.Text);
        //    int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
        //    if (cnt > 0)
        //    {
        //        e.Result = "Billing";
        //        return;
        //    }
        //    ASPxLabel closeIndStr = grid_Issue.FindEditFormTemplateControl("lb_JobStatus") as ASPxLabel;
        //    string sql = "select DoStatus from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
        //    string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
        //    if (closeInd == "Canceled")
        //    {
        //        sql = string.Format("update Wh_Trans set DoStatus='Draft',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //        int res = Manager.ORManager.ExecuteCommand(sql);
        //        if (res > 0)
        //        {
        //            EzshipLog.Log(txt_DoNo.Text, "", "OUT", "Unvoid");
        //            e.Result = "Success";
        //        }
        //        else
        //        {
        //            e.Result = "Fail";
        //        }
        //    }
        //    else
        //    {
        //        bool closeByEst = true;//EzshipHelper.GetCloseEstInd(refNo.Text, refType);
        //        if (closeByEst)
        //        {
        //            sql = string.Format("update Wh_Trans set DoStatus='Canceled',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        //            int res = Manager.ORManager.ExecuteCommand(sql);
        //            if (res > 0)
        //            {
        //                EzshipLog.Log(txt_DoNo.Text, "", "OUT", "Canceled");
        //                e.Result = "Success";
        //            }
        //            else
        //                e.Result = "Fail";
        //        }
        //        else
        //            e.Result = "NoMatch";
        //    }
        //   
        //}
        #endregion
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
        if (s == "AddDoOut")
        {
            #region Transfer to Do Out

            string soNo = SafeValue.SafeString(txt_DoNo.Text);
            string wareHouse = SafeValue.SafeString(txt_WareHouseId.Text);
            string sql1 = string.Format("select doNo from wh_do where DoType='Out' and PoNo='{0}'", soNo);
            DataTable tab_do = ConnectSql.GetTab(sql1);
            string where = "(1=0";
            for (int i = 0; i < tab_do.Rows.Count; i++)
            {
                where += string.Format(" or DoNo='{0}' ", tab_do.Rows[i][0]);

            }
            where += ")";

            int cnt = 0;
            string sql = string.Format("select count(*) from wh_transDet where DoNo='{0}' and DoType='JO' and isnull(LotNo,'')='' ", txt_DoNo.Text);
            cnt = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
            if (cnt > 0)
            {
                e.Result = "No Balance Qty or No Lot No!";
                return;
            }
            sql = string.Format(@" select LotNo,
Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {1} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0)
 from Wh_TransDet where DoNo='{0}' and DoType='JO' and Qty1>0", txt_DoNo.Text, where);
            DataTable tab = ConnectSql.GetTab(sql);
            for (int i = 0; i < tab.Rows.Count; i++)
            {

                cnt += SafeValue.SafeInt(tab.Rows[i][1], 0);
            }
            if (cnt >= 0)
            {
                sql = string.Format(@"select LocationCode from Wh_TransDet where DoNo='{0}' group by LocationCode", txt_DoNo.Text);
                DataTable tab_wh = ConnectSql.GetTab(sql);
                for (int i = 0; i < tab_wh.Rows.Count; i++)
                {
                    wareHouse = SafeValue.SafeString(tab_wh.Rows[i]["LocationCode"]);
                    string doNo = SaveDoOut(soNo, wareHouse);
                    sql = @"Insert Into wh_DoDet(JobStatus,DoNo, DoType,ProductCode,ExpiredDate,Price,Qty1,Qty2,Qty3,Qty4,Qty5,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
                    sql += string.Format(@" select * from (select 'Pending' as JobStatus,'{1}'as DoNo, 'Out' as DoType,ProductCode,ExpiredDate,Price
,0 as Qty1
,0 as Qty2
,0 as Qty3
,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {3} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty4
,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {3} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty5
,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,'{2}' as CreateBy,getdate() as CreateDateTime,'{2}' as UpdateBy,getdate() as UpdateDateTime,Id as DoInId from Wh_TransDet where DoNo='{0}' and DoType='JO' and LocationCode='{4}'
 ) as tab_aa where qty4>0  ", txt_DoNo.Text, doNo, EzshipHelper.GetUserName(), where, wareHouse);


                    cnt = C2.Manager.ORManager.ExecuteCommand(sql);
                    if (cnt > -1)
                    {

                       
                        e.Result += doNo + " ";
                    }
                    else
                        e.Result = "Fail";
                }
            }
            else
                e.Result = "No Balance Qty or No Lot No!";

            #endregion
        }
        if (s == "CreatePO")
        {
            #region Create PO
            string sql = string.Format(@"select Count(*) from Wh_TransDet where DoNo='{0}' and DoType='JO' and ISNULL(LotNo,'')=''", txt_DoNo.Text);
            int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (cnt > 0)
            {
                string pId = "";
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTrans), "Id='" + pId + "'");
                WhTrans whTrans = C2.Manager.ORManager.GetObject(query) as WhTrans;
                ASPxDateEdit txt_Date = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
                ASPxButtonEdit txt_SupplierId = grid_Issue.FindEditFormTemplateControl("txt_SupplierId") as ASPxButtonEdit;
                ASPxTextBox txt_SupplierName = grid_Issue.FindEditFormTemplateControl("txt_SupplierName") as ASPxTextBox;
                whTrans = new WhTrans();
                string poNo = C2Setup.GetNextNo("", "PurchaseOrders", txt_Date.Date);
                whTrans.DoNo = poNo;
                whTrans.DoType = "PO";
                whTrans.PartyId = SafeValue.SafeString(txt_SupplierId.Text);
                whTrans.PartyName = SafeValue.SafeString(txt_SupplierName.Text);
                whTrans.CreateBy = EzshipHelper.GetUserName();
                whTrans.CreateDateTime = DateTime.Now;
                whTrans.DoDate = txt_Date.Date;
                whTrans.ExpectedDate = DateTime.Today.AddDays(14);
                whTrans.Currency = "SGD";
                whTrans.DoStatus = "Draft";
                whTrans.ExRate = SafeValue.SafeDecimal(1.000000);
                whTrans.WareHouseId = txt_WareHouseId.Text;
                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whTrans);
                C2Setup.SetNextNo("", "PurchaseOrders", poNo, txt_Date.Date);
                sql = string.Format(@"select * from Wh_TransDet where DoNo='{0}' and DoType='JO' and ISNULL(LotNo,'')=''", txt_DoNo.Text);
                DataTable tab = ConnectSql.GetTab(sql);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    string doNo = SafeValue.SafeString(txt_DoNo.Text);
                    string sku = SafeValue.SafeString(tab.Rows[i]["ProductCode"]);
                    string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
                    int qty = SafeValue.SafeInt(tab.Rows[i]["Qty1"], 0);
                    decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"]);
                    sql = @"Insert Into Wh_PoRequest(SoNo,Product,Qty1,Qty2,Qty3,Price,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,PoNo)";
                    sql += string.Format(@"select '{0}'as DoNo, '{1}' as Sku,'{2}' as Qty1,0 as Qty2,0 as Qty3,'{3}',p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase,'{4}' as CreateBy,getdate() as CreateDateTime,
'{4}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9,p.Description,p.Att1,'{5}'
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", doNo, sku, qty, price, EzshipHelper.GetUserName(), poNo);
                    cnt = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
                    if (cnt > 0)
                    {
                        sql = @"Insert Into Wh_TransDet(DoNo,ProductCode,DoType,Qty1,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing)";
                        sql += string.Format(@"select '{0}'as DoNo, '{1}' as Sku,'PO','{2}' as Qty1,'{3}','{5}',p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase,'{4}' as CreateBy,getdate() as CreateDateTime,
'{4}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9,p.Description,p.Att1 
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", poNo, sku, qty, price, EzshipHelper.GetUserName(), lotNo);
                        ConnectSql.ExecuteSql(sql);
                    }
                }
                e.Result = "Success";
            }
            else
            {
                e.Result = "Fail";
            }
            #endregion
        }
        if (s == "Costing")
        {
            #region Costing
            try
            {
                string sql = string.Format(@"select count(*) from Wh_Costing where RefNo='{0}'", txt_DoNo.Text);
                int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (cnt == 0)
                {
                    C2.WhCosting cost = new C2.WhCosting();
                    cost.RefNo = txt_DoNo.Text;
                    cost.JobType = "JO";

                    sql = string.Format(@"select * from XAArInvoice where MastRefNo='{0}' and DocType='IV'", txt_DoNo.Text);
                    //string sku = "";
                    //string des = "";
                    //decimal price = 0;
                    string cur = "";
                    //decimal gst = 0;
                    decimal docAmt = 0;
                    DataTable tab = ConnectSql.GetTab(sql);
                    if (tab.Rows.Count > 0)
                    {
                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            docAmt = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"]);
                            cur = SafeValue.SafeString(tab.Rows[i]["CurrencyId"]);
                        }
                        //cost.ChgCode = sku;
                        //cost.ChgCodeDes = des;

                        //cost.CostPrice = price;
                        //cost.Unit = "";
                        cost.CostCurrency = cur;
                        cost.CostExRate = SafeValue.SafeDecimal(10);
                        //cost.CostGst = gst;
                        //if (cost.CostExRate == 0)
                        //    cost.CostExRate = 1;
                        //decimal amt = SafeValue.ChinaRound(cost.CostQty * cost.CostPrice, 2);
                        //decimal gstAmt = SafeValue.ChinaRound((amt * cost.CostGst), 2);
                        decimal costDocAmt = docAmt;
                        decimal costLocAmt = SafeValue.ChinaRound(costDocAmt * (cost.CostExRate / 100), 2);
                        cost.CostDocAmt = docAmt;
                        cost.CostLocAmt = costLocAmt;
                        C2.Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(cost);

                        sql = string.Format("update Wh_Trans set DoStatus='Costing',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        int res = Manager.ORManager.ExecuteCommand(sql);
                        if (res > 0)
                        {
                            EzshipLog.Log(txt_DoNo.Text, "", "JO", "Costing");
                        }
                        e.Result = "Success";
                    }
                    else
                        e.Result = "Fail! No Invoice";
                }
                else
                {
                    e.Result = "Fail! Had Costing";
                }
            }
            catch
            {
            }
            #endregion
        }
    }

    #endregion

    #region SKULine
    protected void grid_SKULine_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhTransDet));
        }
    }
    protected void grid_SKULine_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsIssueDet.FilterExpression = "DoType= 'JO' and DoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_SKULine_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty1"] = 1;
        e.NewValues["Qty2"] = 0;
        e.NewValues["Qty3"] = 0;
        e.NewValues["Qty4"] = 0;
        e.NewValues["Qty5"] = 0;
        e.NewValues["QtyPackWhole"] = 1;
        e.NewValues["QtyWholeLoose"] = 1;
        e.NewValues["QtyLooseBase"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["Gst"] = 0;
        e.NewValues["ExRate"] = 1.000000;
        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        e.NewValues["LocationCode"] = SafeValue.SafeString(txt_WareHouseId.Text);
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["GstType"] = "Z";
    }
    protected void grid_SKULine_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues["ProductCode"] == null || e.NewValues["ProductCode"].ToString().Trim() == "")
        {
            throw new Exception("Product not be null !!!");
            return;
        }
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["DoNo"] = refN.Text;
        e.NewValues["DoType"] = "JO";

        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        //e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        //e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);

        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["LocationCode"] = SafeValue.SafeString(e.NewValues["LocationCode"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;

        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["Currency"] = SafeValue.SafeString(e.NewValues["Currency"]);
        e.NewValues["ExRate"] = SafeValue.SafeDecimal(e.NewValues["ExRate"], 0);
        e.NewValues["Gst"] = SafeValue.SafeDecimal(e.NewValues["Gst"], 0);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["ExpiredDate"] = SafeValue.SafeDate(e.NewValues["ExpiredDate"], DateTime.Today);

        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty1"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;


    }
    protected void grid_SKULine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (e.NewValues["ProductCode"] == null || e.NewValues["ProductCode"].ToString().Trim() == "")
        {
            throw new Exception("Product not be null !!!");
            return;
        }
        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);

        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;

        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);


        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["Currency"] = SafeValue.SafeString(e.NewValues["Currency"]);
        e.NewValues["ExRate"] = SafeValue.SafeDecimal(e.NewValues["ExRate"], 0);
        e.NewValues["Gst"] = SafeValue.SafeDecimal(e.NewValues["Gst"], 0);
        e.NewValues["GstType"] = SafeValue.SafeString(e.NewValues["GstType"]);
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Today;
        e.NewValues["ExpiredDate"] = SafeValue.SafeDate(e.NewValues["ExpiredDate"], DateTime.Today);

        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty1"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
    }
    protected void grid_SKULine_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_SKULine_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        //int transId = SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format("select max(id) from wh_transDet where DoNo='{0}' and DoType='JO'",refN.Text)), 0);
        //UpdatePoDetBalQty(transId);
    }
    protected void grid_SKULine_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {

    }
    private void UpdatePoDetBalQty(int transId)
    {
        string sql = string.Format(@"update Wh_TransDet set BalQty=Qty-isnull((select sum(Qty) from Wh_DoDet where DoInId={0}),0) where Id='{0}'", transId);
        C2.Manager.ORManager.ExecuteScalar(sql);
    }
    private int AddNewSkuLine(int qty, string doNo, string product, string lotNo, string des, string uom1, int pkg, int unit, string att1
        , string att2, string att3, string att4, string att5, string att6, int stk, string uom2, string uom3, decimal price, string packing, string wh)
    {
        string sql = string.Format(@"select tab_hand.HandQty-isnull(tab_Reserved.ReservedQty,0) as BalQty
from (select product,LotNo,Packing ,sum(isnull(Case when det.DoType='In' then Qty1 else -Qty1 end,0)) as HandQty from wh_dodet2 det inner join  wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode!='CNL' group by product,LotNo,Packing) as tab_hand
left join (select productCode as Product,LotNo,sum(Qty5) as ReservedQty from wh_doDet det inner join  wh_do mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' group by productCode,LotNo) as tab_Reserved on tab_Reserved.product=tab_hand.product and tab_Reserved.LotNo=tab_hand.LotNo 
where tab_hand.Product='{0}'", product, lotNo);
        int balQty = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (qty > balQty && balQty != 0)
        {
            sql = @"Insert Into wh_TransDet(DoNo, DoType,ProductCode,Qty1,Qty2,Qty3,Qty4,Qty5,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,DocAmt,LocationCode)";
            sql += string.Format(@"select '{0}'as DoNo, 'JO' as DoType,'{1}' as Sku,'{2}' as Qty1,0 as Qty2,0 as Qty3,0 as Qty4,0 as Qty5,'{3}' as Price,'{4}' as LotNo
,'{5}' as Uom1,'{6}' as Uom2,'{7}' as Uom3,'' as Uom4
,'{8}' as QtyPackWhole,'{9}' as QtyWholeLoose,'{10}' as QtyLooseBase
,'{11}' as CreateBy,getdate() as CreateDateTime,'{11}' as UpdateBy,getdate() as UpdateDateTime
,'{12}' as att1,'{13}' as att2,'{14}' as att3,'{15}' as att4,'{16}' as att5,'{17}' as att6,'{18}' as Des1,'{19}' as Packing,({2}*{3}) as DocAmt,'{20}' as LocationCode
from (select '{1}' as Sku) as tab", doNo, product, (qty - balQty), price, lotNo, uom1, uom2, uom3, pkg, unit, stk, EzshipHelper.GetUserName(), att1, att2, att3, att4, att5, att6, des, packing, wh);
            ConnectSql.ExecuteSql(sql);
        }
        else
        {
            balQty = qty;
        }
        return balQty;
    }
    #endregion

    #region Billing
    protected void Grid_Invoice_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsInvoice.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void Grid_Payable_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsVoucher.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
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
    #region po request
    protected void grid_PoRequest_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhPORequest));
        }
    }
    protected void grid_PoRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsPoRequest.FilterExpression = "SoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_PoRequest_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty1"] = 1;
        e.NewValues["Qty2"] = 0;
        e.NewValues["Qty3"] = 0;
        e.NewValues["Qty4"] = 0;
        e.NewValues["Qty5"] = 0;
        e.NewValues["QtyPackWhole"] = 1;
        e.NewValues["QtyWholeLoose"] = 1;
        e.NewValues["QtyLooseBase"] = 1;
        e.NewValues["Price"] = 0;
    }
    protected void grid_PoRequest_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["SoNo"] = refN.Text;


        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Uom3"] = SafeValue.SafeString(e.NewValues["Uom3"]);
        e.NewValues["Uom4"] = SafeValue.SafeString(e.NewValues["Uom4"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;
        //ASPxComboBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxComboBox;
        //ASPxComboBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxComboBox;
        //ASPxComboBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxComboBox;
        //ASPxComboBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxComboBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        //e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        //e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        //e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        //e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        //e.NewValues["Att7"] = SafeValue.SafeString(e.NewValues["Att7"]);
        //e.NewValues["Att8"] = SafeValue.SafeString(e.NewValues["Att8"]);
        //e.NewValues["Att9"] = SafeValue.SafeString(e.NewValues["Att9"]);
        //e.NewValues["Att10"] = SafeValue.SafeString(e.NewValues["Att10"]);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_PoRequest_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Uom3"] = SafeValue.SafeString(e.NewValues["Uom3"]);
        e.NewValues["Uom4"] = SafeValue.SafeString(e.NewValues["Uom4"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;
        //ASPxComboBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxComboBox;
        //ASPxComboBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxComboBox;
        //ASPxComboBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxComboBox;
        //ASPxComboBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxComboBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        //e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        //e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        //e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        //e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        //e.NewValues["Att7"] = SafeValue.SafeString(e.NewValues["Att7"]);
        //e.NewValues["Att8"] = SafeValue.SafeString(e.NewValues["Att8"]);
        //e.NewValues["Att9"] = SafeValue.SafeString(e.NewValues["Att9"]);
        //e.NewValues["Att10"] = SafeValue.SafeString(e.NewValues["Att10"]);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            if (e.NewValues[i] == null)
            {
                e.NewValues[i] = "";
            }
        }
    }
    protected void grid_PoRequest_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_PoRequest_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {

    }
    protected void grid_PoRequest_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
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
            grd.ForceDataRowType(typeof(C2.JobCosting));
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
        ASPxButtonEdit doNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobType"] = "JO";
        e.NewValues["RefNo"] = doNo.Text;
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
}
