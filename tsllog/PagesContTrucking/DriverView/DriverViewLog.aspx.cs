using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_DriverView_DriverViewLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string tripId = SafeValue.SafeString(Request.QueryString["tripId"].ToString());
            this.ASPxLabel1.Text = SafeValue.SafeString(Request.QueryString["date1"]);
            this.ASPxLabel2.Text = SafeValue.SafeString(Request.QueryString["date2"]);
            lb_tripId.Text = tripId;
            string sql = "select JobNo,ContainerNo,DriverCode from ctm_jobDet2 where Id='" + tripId + "'";
            DataTable dt = ConnectSql.GetTab(sql);
            if (dt.Rows.Count > 0)
            {
                lb_JobNo.Text = dt.Rows[0]["JobNo"].ToString();
                lb_ContNo.Text = dt.Rows[0]["ContainerNo"].ToString();
                lb_Driver.Text = dt.Rows[0]["DriverCode"].ToString();
            }
            this.cmb_JobStatus.SelectedIndex = 0;
            this.date_Date.Date = DateTime.Today;
            this.txt_Time.Text = DateTime.Now.ToString("HH:mm");
            BindData();
        }
    }

    private void BindData()
    {
        string sql = string.Format(@"select JobNo,Driver,Status,LogDate,LogTime,Remark from ctm_triplog 
where tripid='{0}' and Driver='{1}' and JobType='HS'
order by LogDate,LogTime,UpdateDateTime", lb_tripId.Text, lb_Driver.Text);
        DataTable dt = ConnectSql.GetTab(sql);
        this.Repeater1.DataSource = dt;
        this.Repeater1.DataBind();
    }

    protected void ASPxButton21_Click(object sender, EventArgs e)
    {
        string jobNo = this.lb_JobNo.Text;
        string driver = this.lb_Driver.Text;
        string status = SafeValue.SafeString(this.cmb_JobStatus.Value);
        DateTime logDate = this.date_Date.Date;
        string logTime = this.txt_Time.Text;
        string rmk = this.txt_Rmk.Text;
        if (check_Trip_Status(lb_tripId.Text, driver, status))
        {
            Response.Write("<script>alert('This status is exist for "+driver+"');</script>");
            return;
        }
        int flag = InsertTripLog(jobNo, "HS", driver, status, logDate, logTime, rmk,lb_tripId.Text);
        if (flag > 0)
        {
            UpdateTptStatus(lb_tripId.Text, status, logTime);
            this.cmb_JobStatus.SelectedIndex = 0;
            this.date_Date.Date = DateTime.Today;
            this.txt_Time.Text = DateTime.Now.ToString("HH:mm");
            this.txt_Rmk.Text = "";
            BindData();
        }
    }
    private int InsertTripLog(string jobNo, string jobType, string driver, string status, DateTime logDate, string logTime, string rmk,string tripId)
    {
        string sql = string.Format(@"INSERT INTO [CTM_TripLog]([JobNo],[JobType],[Driver],[Status],[LogDate],[LogTime],[Remark],[CreateDateTime],[CreateBy],[UpdateDateTime],[UpdateBy],[tripId])
     VALUES('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}'  ,'{8}' ,'{7}' ,'{8}' ,'{7}','{9}')", jobNo, jobType, driver, status, logDate.ToString("yyyy-MM-dd"), logTime, rmk, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),tripId);
        return ConnectSql.ExecuteSql(sql);
    }
    private void UpdateTptStatus(string tripId, string jobStatus,string Time)
    {
        string sql = string.Format("Update CTM_jobDet2 set statuscode='{1}',FromTime='{2}' where Id='{0}'", tripId, jobStatus,Time);
        ConnectSql.ExecuteSql(sql);
    }
    private bool check_Trip_Status(string id, string driverCode, string status)
    {
        if (status == "S" || status == "P")
        {
            string sql = string.Format(@"select COUNT(*) from CTM_JobDet2 where DriverCode='{0}' and Statuscode='{2}' and Id<>'{1}'", driverCode, id, status);
            int result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 0);
            if (result > 0)
            {
                return true;
            }
        }
        return false;
    }
}