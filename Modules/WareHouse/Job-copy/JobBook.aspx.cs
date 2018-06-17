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

public partial class WareHouse_Job_JobBook : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["no"] != null && Request.QueryString["no"] == "0")
            {
                this.grid_Issue.AddNewRow();
            }
            else
                this.dsIssue.FilterExpression = "1=0";

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region JO

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

            job.JobStage = "Customer Inquir";
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
            job.WorkStatus = "PENDING";


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
                if (job.JobStatus == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select JobStatus from JobInfo where DoNo='" + job.JobNo + "'")))
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
            return job.JobNo;
        }
        catch { }
        return "";
    }
    protected void grid_Issue_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Issue.EditingRowVisibleIndex > -1)
        {
           
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
            if (txt_CustomerId.Text.Trim() == "")
            {
                e.Result = "Fail! Please enter the Customer";
                return;
            }
            else
            {
                e.Result = SaveJob();
            }
            #endregion
        }
    }
    protected void cmb_Status_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {

    }
    #endregion
}