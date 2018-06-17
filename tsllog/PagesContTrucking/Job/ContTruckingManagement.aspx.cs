using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_ContTruckingManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_Using_From.Date = DateTime.Now.AddDays(-15);
            this.date_Using_To.Date = DateTime.Now.AddDays(8);
            btn_Using_Search_Click(null, null);
        }
    }
    protected void btn_TripList_Search_Click(object sender, EventArgs e)
    {
        //ASPxComboBox StatusCode = this.pageControl.FindControl("cbb_Trip_StatusCode") as ASPxComboBox;
        //ASPxTextBox JobNo = this.pageControl.FindControl("txt_TripList_JobNo") as ASPxTextBox;
        //ASPxTextBox ContNo = this.pageControl.FindControl("txt_TripList_ContNo") as ASPxTextBox;
        string where = "job.StatusCode='USE'";
        string status = this.cbb_Trip_StatusCode.Text.Trim();
        string jobno = this.txt_TripList_JobNo.Text.Trim();
        string contno = this.txt_TripList_ContNo.Text.Trim();
        if (status.Length > 0)
        {
            where += " and Det2.StatusCode='" + status + "'";
        }
        if (jobno.Length > 0)
        {
            where += " and Det2.JobNo like '%" + jobno + "%'";
        }
        if (contno.Length > 0)
        {
            where += " and Det2.ContainerNo like '%" + contno + "%'";
        }
        //this.dsTripList.FilterExpression = where;
        string sql = string.Format(@"select det2.JobNo,ContainerNo,DriverCode,FromDate,ToDate 
from CTM_JobDet2 as Det2 left outer join CTM_Job as job on job.JobNo=Det2.JobNo");
        sql = sql + " where " + where;
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_TripList.DataSource = dt;
        this.grid_TripList.DataBind();
    }
    protected void btn_UnAssign_Search_Click(object sender, EventArgs e)
    {
        //ASPxTextBox JobNo = this.pageControl.FindControl("txt_UnAssign_JobNo") as ASPxTextBox;
        //ASPxTextBox ContNo = this.pageControl.FindControl("txt_UnAssign_ContNo") as ASPxTextBox;
        string where = "job.StatusCode='USE' and isnull(Det2.DriverCode,'')=''";
        string jobno = txt_UnAssign_JobNo.Text.Trim();
        string contno = txt_UnAssign_ContNo.Text.Trim();
        if (jobno.Length > 0)
        {
            where += " and Det2.JobNo like '%" + jobno + "%'";
        }
        if (contno.Length > 0)
        {
            where += " and Det2.ContainerNo like '%" + contno + "%'";
        }
        //this.dsUnAssignTrip.FilterExpression = where;
        string sql = string.Format(@"select det2.JobNo,ContainerNo,DriverCode,FromDate,ToDate 
from CTM_JobDet2 as Det2 left outer join CTM_Job as job on job.JobNo=Det2.JobNo");
        sql = sql + " where " + where;
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_UnAssign_Trip.DataSource = dt;
        this.grid_UnAssign_Trip.DataBind();
    }
    protected void txt_Driver_Search_Click(object sender, EventArgs e)
    {
        //ASPxButtonEdit DriverCode = this.pageControl.FindControl("btn_DriverCode") as ASPxButtonEdit;
        //ASPxTextBox JobNo = this.pageControl.FindControl("txt_Driver_JobNo") as ASPxTextBox;
        //ASPxTextBox ContNo = this.pageControl.FindControl("txt_Driver_ContNo") as ASPxTextBox;
        string where = "job.StatusCode='USE' and isnull(Det2.DriverCode,'')<>''";
        string driver = btn_DriverCode.Text.Trim();
        string contno = txt_Driver_ContNo.Text.Trim();
        if (driver.Length > 0)
        {
            where += " and Det2.DriverCode='" + driver + "'";
        }
        if (contno.Length > 0)
        {
            where += " and Det2.ContainerNo like '%" + contno + "%'";
        }
        //this.dsDriverTrip.FilterExpression = where;
        string sql = string.Format(@"select det2.JobNo,ContainerNo,DriverCode,FromDate,ToDate 
from CTM_JobDet2 as Det2 left outer join CTM_Job as job on job.JobNo=Det2.JobNo");
        sql = sql + " where " + where;
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_DriverTrip.DataSource = dt;
        this.grid_DriverTrip.DataBind();
    }
    protected void btn_Using_Search_Click(object sender, EventArgs e)
    {
        //ASPxTextBox JobNo = this.pageControl.FindControl("txt_Using_JobNo") as ASPxTextBox;
        //ASPxDateEdit JobDate_From = this.pageControl.FindControl("date_Using_From") as ASPxDateEdit;
        //ASPxDateEdit JobDate_To = this.pageControl.FindControl("date_Using_To") as ASPxDateEdit;
        string jobno = txt_Using_JobNo.Text.Trim();
        string From = date_Using_From.Date.ToString();
        string To = date_Using_To.Date.ToString();
        string where = "StatusCode='USE'";
        if (jobno.Length > 0)
        {
            where += " and JobNo like '%" + jobno + "%'";
        }
        if (From.Length > 0)
        {
            where += " and JobDate>='" + From + "'";
        }
        if (To.Length > 0)
        {
            where += " and JobDate<='" + To + "'";
        }
        //this.dsJob.FilterExpression = where;
        string sql = string.Format(@"select job.Id,job.JobNo
,JobDate
,EtaDate
,EtdDate
,cast(cast(isnull(cast(det2.ComAmt as numeric(18,2))/det2.Amt*100,0) as numeric(18,2)) as nvarchar)+'%' as PercentAmt
,case  when Det2.Amt=det2.ComAmt then 'complete' else cast(isnull(det2.ComAmt,0) as nvarchar)+'/'+cast(isnull(Det2.Amt,0) as nvarchar) end as PercentAmt2
from  CTM_Job as job
left outer join (select jobno,sum(1) as Amt,sum(case when Statuscode='Completed' then 1 else 0 end) as ComAmt from CTM_JobDet2 group by JobNo) as Det2
on job.JobNo=Det2.JobNo");
        sql = sql + " where " + where;
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_usingJob.DataSource = dt;
        this.grid_usingJob.DataBind();
    }
}