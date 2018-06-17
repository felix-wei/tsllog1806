using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesTpt_Local_Viewer_DriverViewLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
            string driver = SafeValue.SafeString(Request.QueryString["driver"]);
            this.ASPxLabel1.Text = SafeValue.SafeString(Request.QueryString["date1"]);
            this.ASPxLabel2.Text = SafeValue.SafeString(Request.QueryString["date2"]);
            this.txt_Driver.Text = driver;
            this.txt_JobNo.Text = jobNo;
            this.date_TptDate.Date = DateTime.Today;
            this.txt_TptTime.Text = DateTime.Now.ToString("HH:mm");
            BindData();
        }
    }
    private void BindData()
    {
        string sql = "select JobNo,Driver,Status,LogDate,LogTime,Remark from ctm_triplog";
        sql += string.Format(" Where JobNo='{0}' and Driver='{1}' and JobType='Local'",this.txt_JobNo.Text,this.txt_Driver.Text);
        sql += " order by LogDate,LogTime,UpdateDateTime";
        this.Repeater1.DataSource = ConnectSql.GetTab(sql);
        this.Repeater1.DataBind();
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        string jobNo = this.txt_JobNo.Text;
        string driver = this.txt_Driver.Text;
        string status = this.cmb_JobStatus.Text;
        DateTime logDate = this.date_TptDate.Date;
        string logTime = this.txt_TptTime.Text;
        string rmk = this.txt_Rmk.Text;
        int flag= InsertTripLog(jobNo, "Local", driver, status, logDate, logTime, rmk);
        if (flag > 0)
        {
            UpdateTptStatus(jobNo, status, logTime, logDate);
            this.cmb_JobStatus.Text = "";
            this.date_TptDate.Date = DateTime.Today;
            this.txt_TptTime.Text = DateTime.Now.ToString("HH:mm");
            this.txt_Rmk.Text = "";
            BindData();
        }
    }
    private int InsertTripLog(string jobNo,string jobType,string driver,string status,DateTime logDate,string logTime,string rmk)
    {
        string sql = string.Format(@"INSERT INTO [CTM_TripLog]([JobNo],[JobType],[Driver],[Status],[LogDate],[LogTime],[Remark],[CreateDateTime],[CreateBy],[UpdateDateTime],[UpdateBy])
     VALUES('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}'  ,'{8}' ,'{7}' ,'{8}' ,'{7}')", jobNo, jobType, driver, status, logDate.ToString("yyyy-MM-dd"), logTime, rmk, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        return ConnectSql.ExecuteSql(sql);
    }
    private void UpdateTptStatus(string jobNo, string jobStatus,string time,DateTime date)
    {
        string sql = string.Format("Update tpt_job set JobProgress='{1}',tptTime='{2}',tptDate='{3}' where JobNo='{0}'", jobNo, jobStatus, time, date);
        ConnectSql.ExecuteSql(sql);
    }
}