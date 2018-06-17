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

public partial class WareHouse_Job_StorageTemp : System.Web.UI.Page
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
            }
            else if (Request.QueryString["no"] == null || Request.QueryString["no"] == "0")
            {
                string sql = string.Format(@"select count(*) from JobInfo where JobType='SRTemp'");
                int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                if (cnt == 0)
                {
                    this.grid_Issue.AddNewRow();
                }
                else
                {
                    Session["SoWhere"] = "JobType='SRTemp'";
                    dsIssue.FilterExpression = Session["SoWhere"].ToString();
                }

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
        e.NewValues["JobType"] = "SRTemp";
        e.NewValues["JobStage"] = "Site Survery";
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["DateTime1"] = DateTime.Now;
        e.NewValues["ExpectedDate"] = DateTime.Today;
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.000000;
        e.NewValues["PayTerm"] = "30DAYS";
        e.NewValues["IncoTerm"] = "EXW";
        e.NewValues["WareHouseId"] = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];
		return;
        e.NewValues["Attention1"] = @"<p>You can insure separately at a premium of 1.50% of total value.(Min. Premium is $100.00)
Please be advised that completed insurance application must be submitted to our office prior to pack and move dates. There is no liability on Collin’s Movers upon the decline of insurance. All items in the shipment must be insured. Any policy that covers only a limited number of items in the shipment may be subject to rejection by insurance company.</p>
<p>This Quotation is based on us providing a normal weekday(Monday to Saturday)services unless otherwise stated. Any substantial variation in the volume will affect our charges. The validity of this quotation is 30 days from the date of issue.</p>
";
        e.NewValues["Attention2"] = @"<p>*Preparation : Delivery of cartons and tapes for self packing.</p>
<p>*Packing Materials : Provide required quantity of packing materials such as various sizes of cartons boxes, Bubble wraps, Tapes etc. Our service includes one time collection of empty cartons after the completion of the move.</p>
<p>*Pack and Unpack: Wrapping and packing of all fragile to include china, glassware, paintings, ornaments, books files, kitchenware and miscellaneous item and unpacking on destination if not specified.</p>
<p>*Moving: Provide skilled and experienced movers to pack and move your house hold items.</p>
<p>*Supervisor : Provide highly experienced and skilled team supervisor during the entire move.</p>
<p>*Transportation: Provide Collins Movers 22ft covered truck equipped with hydraulic tail gate system. All trucks are equipped with GPS tracking system to monitor the truck movements.</p>
<p>*MCST Process: Removal permit application and refundable cheque deposit if applicable for your condominium move.</p>
";
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
                job.JobType = "SRTemp";
                //issueN = C2Setup.GetNextNo("", "JobOrder", issueDate.Date);
                issueN = "SRTemp-01";
                job.JobDate = issueDate.Date;
            }
            job.Attribute = "Temp";

            ASPxDateEdit doDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            job.JobDate = doDate.Date;

            ASPxComboBox cmb_Status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            job.JobStage = SafeValue.SafeString(cmb_Status.Value);
            ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
            job.JobType = SafeValue.SafeString(cmb_JobType.Value);
            //Main Info

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


            ASPxMemo txt_PackRemark = grid_Issue.FindEditFormTemplateControl("txt_PackRemark") as ASPxMemo;
            job.PackRmk = txt_PackRemark.Text;
            ASPxMemo txt_MoveRemark = grid_Issue.FindEditFormTemplateControl("txt_MoveRemark") as ASPxMemo;
            job.MoveRmk = txt_MoveRemark.Text;

            ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            job.WareHouseId = txt_WareHouseId.Text;

            ASPxSpinEdit spin_Volumne = grid_Issue.FindEditFormTemplateControl("spin_Volumne") as ASPxSpinEdit;
            job.Volumne = SafeValue.SafeDecimal(spin_Volumne.Value);
            ASPxMemo memo_Description = grid_Issue.FindEditFormTemplateControl("memo_Description") as ASPxMemo;
            job.ItemDes = memo_Description.Text;
            ASPxTextBox memo_Volumn = grid_Issue.FindEditFormTemplateControl("memo_Volumn") as ASPxTextBox;
            job.VolumneRmk = memo_Description.Text;
            ASPxSpinEdit spin_HeadCount = grid_Issue.FindEditFormTemplateControl("spin_HeadCount") as ASPxSpinEdit;
            job.HeadCount = SafeValue.SafeInt(spin_HeadCount.Value, 0);

            ASPxDateEdit date_Pack = grid_Issue.FindEditFormTemplateControl("date_Pack") as ASPxDateEdit;
            job.PackDate = date_Pack.Date;
            ASPxComboBox cmb_Via = grid_Issue.FindEditFormTemplateControl("cmb_Via") as ASPxComboBox;
            job.ViaWh = cmb_Via.Text;
            ASPxDateEdit date_StorageStartDate = grid_Issue.FindEditFormTemplateControl("date_StorageStartDate") as ASPxDateEdit;
            job.StorageStartDate = date_StorageStartDate.Date;
            ASPxSpinEdit spin_StorageFreeDays = grid_Issue.FindEditFormTemplateControl("spin_StorageFreeDays") as ASPxSpinEdit;
            job.StorageFreeDays = SafeValue.SafeInt(spin_StorageFreeDays.Value, 0);
            ASPxSpinEdit spin_STotalDays = grid_Issue.FindEditFormTemplateControl("spin_STotalDays") as ASPxSpinEdit;
            job.StorageTotalDays = Helper.Safe.SafeInt(spin_STotalDays.Value);
            ASPxTextBox txt_TripNo = grid_Issue.FindEditFormTemplateControl("txt_TripNo") as ASPxTextBox;
            job.TripNo = txt_TripNo.Text;
            ASPxDateEdit date_MoveDate = grid_Issue.FindEditFormTemplateControl("date_MoveDate") as ASPxDateEdit;
            job.MoveDate = date_MoveDate.Date;
            ASPxSpinEdit spin_Charges = grid_Issue.FindEditFormTemplateControl("spin_Charges") as ASPxSpinEdit;
            job.Charges = SafeValue.SafeDecimal(spin_Charges.Value);
            ASPxButtonEdit btn_PortOfEntry = grid_Issue.FindEditFormTemplateControl("btn_PortOfEntry") as ASPxButtonEdit;
            job.EntryPort = btn_PortOfEntry.Text;
            ASPxDateEdit date_Eta = grid_Issue.FindEditFormTemplateControl("date_Eta") as ASPxDateEdit;
            job.Eta = date_Eta.Date;
            ASPxTextBox txt_TruckNo = grid_Issue.FindEditFormTemplateControl("txt_TruckNo") as ASPxTextBox;
            job.TruckNo = txt_TruckNo.Text;
            //Additional

            
            ASPxComboBox cmb_WorkStatus = grid_Issue.FindEditFormTemplateControl("cmb_WorkStatus") as ASPxComboBox;
            job.WorkStatus = SafeValue.SafeString(cmb_WorkStatus.Value);



            //Quotation
            ASPxHtmlEditor txt_Attention1 = pageControl.FindControl("txt_Attention1") as ASPxHtmlEditor;
			if(txt_Attention1 != null)
				job.Attention1 = txt_Attention1.Html;
            ASPxHtmlEditor txt_Attention2 = pageControl.FindControl("txt_Attention2") as ASPxHtmlEditor;
			if(txt_Attention2 != null)
				job.Attention2 = txt_Attention2.Html;
            ASPxHtmlEditor txt_Attention3 = pageControl.FindControl("txt_Attention3") as ASPxHtmlEditor;
			if(txt_Attention3 != null)
				job.Attention3 = txt_Attention3.Html;
            ASPxHtmlEditor txt_Attention4 = pageControl.FindControl("txt_Attention4") as ASPxHtmlEditor;
			if(txt_Attention4 != null)
				job.Attention4 = txt_Attention4.Html;

            job.WorkStatus = "Pending";

            ASPxComboBox cmb_SalesId = grid_Issue.FindEditFormTemplateControl("cmb_SalesId") as ASPxComboBox;
            job.Value4 = cmb_SalesId.Text;

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
                // C2Setup.SetNextNo("", "JobOrder", issueN, issueDate.Date);
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
            Session["SoWhere"] = "JobNo='" + job.JobNo + "'";
            this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);

            return job.JobNo;
        }
        catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
        return "";
    }

    protected void grid_Issue_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Issue.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxComboBox cmb_Status = this.grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            ASPxComboBox cmb_JobType = this.grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
        }
    }
    protected void grid_Issue_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
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

            if (txt_DoNo.Text.Length > 4)
            {
                SaveJob();
                e.Result = "";//update old one
            }
            else
                e.Result = SaveJob();// new one
            #endregion
        }

    }
    protected void cmb_JobType_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
    }
    protected void cmb_Status_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
    }
    protected void cmb_WorkStatus_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {

    }
    #endregion
}