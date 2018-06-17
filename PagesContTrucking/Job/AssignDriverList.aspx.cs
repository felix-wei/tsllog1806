using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_AssignDriverList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            date_SearchFromDate.Date = DateTime.Now.AddDays(-15);
            date_SearchToDate.Date = DateTime.Now.AddDays(8);
            btn_Search_Click(null, null);
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select det2.JobNo,det2.ContainerNo,job.JobType,DriverCode,TowheadCode,ChessisCode,job.Vessel,job.Voyage,job.CarrierBkgNo,job.EtaDate,job.EtdDate
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det1.StatusCode<>'Completed' and det1.StatusCode<>'Canceled' and det2.Statuscode<>'Completed' and det2.Statuscode<>'Cancel' and isnull(DriverCode,'')='' and DriverCode like '%{0}%' and det2.ContainerNo like '%{1}%'", btn_SearchDriveCode.Text.Trim(), btn_SearchContNo.Text.Trim());
        if (date_SearchFromDate.Date > new DateTime(1900, 1, 1))
        {
            sql += " and DATEDIFF(d,'" + date_SearchFromDate.Date + "',ScheduleDate)>=0";
        }
        if (date_SearchToDate.Date > new DateTime(1900, 1, 1))
        {
            sql += " and DATEDIFF(d,'" + date_SearchToDate.Date + "',ScheduleDate)<=0";
        }
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_Transport.DataSource = dt;
        this.grid_Transport.DataBind();
    }
}