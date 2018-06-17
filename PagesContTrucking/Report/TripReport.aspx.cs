using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Report_TripReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
            search_DateTo.Date = DateTime.Now.AddDays(8);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"with pri as (
select * from ctm_MastData where [Type]='tripcode'
),
tb1 as (
select det2.JobNo,det2.ContainerNo,det2.Incentive1,det2.Incentive2,det2.Incentive3,det2.Incentive4,det2.Charge1,det2.Charge2,det2.Charge3,det2.Charge4,det2.Charge5,det2.Charge6,det2.Charge7,det2.Charge8,det2.Charge9,det1.SealNo,det1.ContainerType,det1.ScheduleDate,det2.FromTime,det2.ToTime,det2.TripCode,det2.TowheadCode,det2.Overtime,det2.OverDistance,
det2.ChessisCode,det2.FromCode,det2.ToCode,job.JobType,det2.ParkingZone,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.TripCode),0) as TripCodePrice,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.Overtime),0) as OverTimePrice,
case when det2.OverDistance='Y' then isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code='OJ'),0) else 0 end as QJPrice
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on job.jobNo=det2.JobNo
left outer join CTM_Driver as dri on dri.Code=det2.DriverCode ");
        string sql_part1 = string.Format(@")
select *,TripCodePrice+OverTimePrice+QJPrice as Total from tb1");
        if (search_DateFrom.Date < new DateTime(1900, 1, 1))
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
        }
        if (search_DateTo.Date < new DateTime(1900, 1, 1))
        {
            search_DateTo.Date = DateTime.Now.AddDays(8);
        }

        string where = string.Format(@" where det2.Statuscode<>'Cancel' and job.StatusCode<>'CNL' and DATEDIFF(d,det1.ScheduleDate,'{0}')<=0 and DATEDIFF(d,det1.ScheduleDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date);
        if (search_Driver.Text.Trim().Length > 0)
        {
            where += string.Format(@" and det2.DriverCode='{0}'", search_Driver.Text);
        }
        if (cbb_Trip_TripCode.Text.Trim() != "")
        {
            where += string.Format(@" and det2.TripCode='{0}'", cbb_Trip_TripCode.Text);
        }
        if (cbb_zone.Text.Trim() != "")
        {
            where += string.Format(@" and det2.ParkingZone='{0}'", cbb_zone.Text);
        }
        grid_Transport.DataSource = ConnectSql.GetTab(sql + where + sql_part1);
        grid_Transport.DataBind();
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("TripReport" + (search_Driver.Text.Length > 0 ? "_" + search_Driver.Text : ""), true);
    }
}