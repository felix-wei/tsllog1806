using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Report_TripReport_Incentive : System.Web.UI.Page
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
select det2.JobNo,det2.ContainerNo,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as Incentive1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as Incentive2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Standby') as Incentive3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='PSA') as Incentive4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as Charge1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as Charge2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as Charge3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as Charge4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as Charge5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as Charge6,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as Charge7,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as Charge8,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as Charge9,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as Charge10,
det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.TripCode,det2.TowheadCode,det2.Overtime,det2.OverDistance,
det2.ChessisCode,det2.FromCode,det2.ToCode,job.JobType,det2.ParkingZone,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.TripCode),0) as TripCodePrice,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.Overtime),0) as OverTimePrice,
case when det2.OverDistance='Y' then isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code='OJ'),0) else 0 end as QJPrice
from CTM_JobDet2 as det2
left outer join CTM_Job as job on job.jobNo=det2.JobNo
left outer join CTM_Driver as dri on dri.Code=det2.DriverCode");
        string sql_part1 = string.Format(@")
select *,TripCodePrice+OverTimePrice+QJPrice as Total,isnull(Incentive1,0)+isnull(Incentive2,0)+isnull(Incentive3,0)+isnull(Incentive4,0) as TotalIncentive from tb1");
        if (search_DateFrom.Date < new DateTime(1900, 1, 1))
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
        }
        if (search_DateTo.Date < new DateTime(1900, 1, 1))
        {
            search_DateTo.Date = DateTime.Now.AddDays(8);
        }

        string where = string.Format(@" where det2.Statuscode='C' and job.JobType='CRA' and DATEDIFF(d,det2.ToDate,'{0}')<=0 and DATEDIFF(d,det2.ToDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date);
        if (search_Driver.Text.Trim().Length > 0)
        {
            where += string.Format(@" and det2.DriverCode='{0}'", search_Driver.Text);
        }
        if (search_TowheadCode.Text.Trim().Length > 0)
        {
            where += string.Format(@" and det2.TowheadCode='{0}'", search_TowheadCode.Text);
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
        gridExport.WriteXlsToResponse("TripReport_Incentive" + (search_Driver.Text.Length > 0 ? "_" + search_Driver.Text : ""), true);
    }
}