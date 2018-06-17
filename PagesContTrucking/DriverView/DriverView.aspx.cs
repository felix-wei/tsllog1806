using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_DriverView_DriverView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_From.Date = DateTime.Now.AddDays(-15);
            search_To.Date = DateTime.Now.AddDays(8);
            if (Request.QueryString["driver"] != null && Request.QueryString["date1"] != null && Request.QueryString["date2"] != null)
            {
                search_Driver.Text = Request.QueryString["driver"].ToString();
                search_From.Text = Request.QueryString["date1"].ToString();
                search_To.Text = Request.QueryString["date2"].ToString();
            }
            string sql = "select Code from CTM_driver where statuscode='Active' order by Code";
            DataTable dt = ConnectSql.GetTab(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                search_Driver.Items.Add(dt.Rows[i][0].ToString());
                if (i == 0)
                {
                    search_Driver.Text = dt.Rows[0][0].ToString();
                }
            }
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (search_Driver.Text.Length == 0)
        {
            return;
        }
        if (search_From.Date < new DateTime(1900, 1, 1))
            search_From.Date = DateTime.Now.AddDays(-15);
        if (search_To.Date < new DateTime(1900, 1, 1))
            search_To.Date = DateTime.Now.AddDays(8);
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det1.ContainerType,det2.FromTime,det2.FromCode,det2.ToCode,det2.Statuscode,det1.SealNo,det2.DriverCode,det1.ScheduleDate  
from ctm_jobDet2 AS det2
left outer join ctm_jobdet1 as det1 on det1.Id=det2.det1Id 
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where DriverCode='{0}' and det2.statuscode<>'C' and det2.statuscode<>'X' and scheduleDate  between '{1}' and '{2}' and job.StatusCode<>'CNL'", search_Driver.Text, search_From.Date, search_To.Date);
        this.Repeater1.DataSource = ConnectSql.GetTab(sql);
        this.Repeater1.DataBind();
    }
}