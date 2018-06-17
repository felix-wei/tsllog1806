using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_DispatchPlanner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (date_searchDate.Text=="")
        {
            date_searchDate.Date = DateTime.Now;
        }
        btn_Refresh_Click(null, null);
    }
    
    protected void btn_Refresh_Click(object sender, EventArgs e)
    {
        string[] drivers = new string[] { "BULL", "SHANKAR", "AH LEK", "AH TOH", "TIONG", "RAJU" };
        if (date_searchDate.Date < new DateTime(1900, 1, 1))
        {
            date_searchDate.Date = DateTime.Now;
        }
        #region driver1
        string sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det1.ScheduleDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.ContainerNo,det2.FromTime,det2.Id", drivers[0],date_searchDate.Date);
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_driver1.DataSource = dt;
        this.grid_driver1.DataBind();
        #endregion

        #region driver2
        sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det1.ScheduleDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.ContainerNo,det2.FromTime,det2.Id", drivers[1], date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        this.grid_driver2.DataSource = dt;
        this.grid_driver2.DataBind();
        #endregion

        #region driver3
        sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det1.ScheduleDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.ContainerNo,det2.FromTime,det2.Id", drivers[2], date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        this.grid_driver3.DataSource = dt;
        this.grid_driver3.DataBind();
        #endregion

        #region driver4
        sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det1.ScheduleDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.ContainerNo,det2.FromTime,det2.Id", drivers[3], date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        this.grid_driver4.DataSource = dt;
        this.grid_driver4.DataBind();
        #endregion

        #region driver5
        sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det1.ScheduleDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.ContainerNo,det2.FromTime,det2.Id", drivers[4], date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        this.grid_driver5.DataSource = dt;
        this.grid_driver5.DataBind();
        #endregion

        #region driver6
        sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det1.ScheduleDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.ContainerNo,det2.FromTime,det2.Id", drivers[5], date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        this.grid_driver6.DataSource = dt;
        this.grid_driver6.DataBind();
        #endregion

        #region Container Status
//        sql = string.Format(@"select det1.Id,det1.JobNo,det1.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode 
//from CTM_JobDet1 as det1
//left outer join CTM_JobDet2 as det2 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
//left outer join (
//select det2.ContainerNo,MAX(FromTime) as MaxTime
//from CTM_JobDet2 as det2 
//left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
//where datediff(d,'{0}',det1.ScheduleDate )=0
//group by det2.ContainerNo
//) as det2MaxTime on det2.ContainerNo=det2MaxTime.ContainerNo and det2.FromTime=det2MaxTime.MaxTime
//where datediff(d,'{0}',det1.ScheduleDate)=0 and (det2MaxTime.MaxTime is not null or det2.Id is null)
//order by det2.FromTime",date_searchDate.Date);
        sql = string.Format(@"select det1.Id,det1.JobNo,det1.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode 
from CTM_JobDet1 as det1
left outer join(select * From CTM_JobDet2 where Id in(
select max(det2.Id) From (
select det2.Det1Id,MAX(FromTime) as MaxTime
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where datediff(d,'{0}',det1.ScheduleDate )=0
group by det2.Det1Id) as MaxTime
left outer join CTM_JobDet2 as det2 on det2.Det1Id=MaxTime.Det1Id and det2.FromTime=MaxTime.MaxTime
group by det2.Det1Id
)) as det2 on det1.Id=det2.Det1Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{0}',det1.ScheduleDate )=0 and job.StatusCode<>'CNL'
order by det1.ContainerNo", date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        this.grid_Container.DataSource = dt;
        this.grid_Container.DataBind();
        #endregion

        #region Driver Status
        sql = string.Format(@"select log.Id,det2.ContainerNo,det2.ContainerType,det2.ChessisCode,log.Driver as DriverCode,det2.FromCode,det2.TowheadCode,det2.ToCode,det2.TripCode,FromTime,ToTime,Statuscode from CTM_DriverLog as log
left outer join (
select det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.TowheadCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode 
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.id=det2.det1Id
 where det2.Id in(
select MAX(Id) as Id from(
select det2.Id,det2.DriverCode
from (
	select det2.DriverCode,MAX(FromTime) as MaxTime
	from CTM_JobDet2 as det2 
	left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
	where datediff(d,'{0}',det1.ScheduleDate )=0 and isnull(det2.DriverCode,'')<>''
	group by det2.DriverCode) as det2MaxTime
left outer join CTM_JobDet2 as det2  on det2.DriverCode=det2MaxTime.DriverCode and det2.FromTime=det2MaxTime.MaxTime 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{0}',det1.ScheduleDate )=0 and job.StatusCode<>'CNL'
) as temp group by DriverCode)
) as det2 on log.Driver=det2.DriverCode
where DATEDIFF(d,'{0}',log.Date)=0", date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        grid_Driver.DataSource = dt;
        grid_Driver.DataBind();
        #endregion

        #region Trailer
        sql = string.Format(@"select Mast.Code as ChessisCode,temp.Id,temp.ContainerNo,temp.ContainerType,temp.DriverCode,temp.FromCode,temp.ToCode,temp.TripCode,temp.FromTime,temp.ToTime,temp.Statuscode 
from CTM_MastData as Mast
left outer join (
select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode  
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where det2.Id in(
select MAX(Id) from(
select det2.Id,det2MaxTime.ChessisCode
from (
select det2.ChessisCode,MAX(FromTime) as MaxTime
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where datediff(d,'{0}',det1.ScheduleDate )=0 and ISNULL(det2.ChessisCode,'')<>''
group by det2.ChessisCode
) as det2MaxTime 
left outer join CTM_JobDet2 as det2 on det2.ChessisCode=det2MaxTime.ChessisCode and det2.FromTime=det2MaxTime.MaxTime
left outer join CTM_JobDet1 as det1 on det2.det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where DATEDIFF(d,'{0}',det1.ScheduleDate )=0 and job.StatusCode<>'CNL'
) as temp group by ChessisCode)
) as temp on temp.ChessisCode=Mast.Code 
where Mast.Type='chessis'
order by Id desc,ChessisCode", date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        this.grid_Trailer.DataSource = dt;
        this.grid_Trailer.DataBind();
        #endregion
    }

    protected void grid_driver_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        try
        {
            if (e.DataColumn.FieldName == "Statuscode")  // start, pending, complete, cancel
            {
                string _status = string.Format("{0}", e.CellValue);
                if (_status == "Start" || _status == "S")
                    e.Cell.BackColor = System.Drawing.Color.LightBlue;
                else if (_status == "Doing" || _status == "D")
                    e.Cell.BackColor = System.Drawing.Color.BurlyWood;
                else if (_status == "Pending" || _status == "P")
                    e.Cell.BackColor = System.Drawing.Color.Yellow;
                else if (_status == "Completed" || _status == "C")
                    e.Cell.BackColor = System.Drawing.Color.LightGreen;
                else if (_status == "Cancel" || _status == "X")
                    e.Cell.BackColor = System.Drawing.Color.Red;
                else if (_status == "Waiting" || _status == "W")
                    e.Cell.BackColor = System.Drawing.Color.Orange;
            }
        }
        catch { }
    }
}