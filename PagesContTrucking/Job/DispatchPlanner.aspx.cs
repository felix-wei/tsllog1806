using Aspose.Cells;
using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_DispatchPlanner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (date_searchDate.Text == "")
        {
            date_searchDate.Date = DateTime.Now;
        }
        btn_Refresh_Click(null, null);

    }

    protected void btn_Refresh_Click(object sender, EventArgs e)
    {
        string teamNo = cbb_TeamNo.Text;
        //string driver = "";
        string sql_where = "";
        if (!teamNo.Equals(""))
        {
            sql_where = "and TeamNo='" + teamNo + "'";
        }
        string sql_driver = string.Format(@"select Code,ServiceLevel from CTM_Driver where StatusCode='Active' {0} order by Code", sql_where);

        DataTable dt_driver = ConnectSql.GetTab(sql_driver);
        List<string> drivers = new List<string>();
        //int cnt = dt_driver.Rows.Count;
        for (int i = 0; i < dt_driver.Rows.Count; i++)
        {
            drivers.Add(dt_driver.Rows[i]["Code"].ToString());
        }
        if (date_searchDate.Date < new DateTime(1900, 1, 1))
        {
            date_searchDate.Date = DateTime.Now;
        }
        if (cbb_TeamNo.Text.Trim() != "")
        {
            teamNo = SafeValue.SafeString(cbb_TeamNo.Text.Trim());
            for (int a = 0; a < drivers.Count; a++)
            {
                ReturnDisplay(drivers[a], teamNo);
            }
        }


        #region driver1
        string sql = "";
        DataTable dt = null;
        if (drivers.Count >= 1)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[0], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver1.DataSource = dt;
            this.grid_driver1.DataBind();
            Label1.Text = drivers[0];
            tb1.Style.Add("display", "block");
        }
        else
        {
            tb1.Style.Add("display", "none");
        }
        #endregion

        #region driver2
        if (drivers.Count >= 2)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode,det1.SealNo,det2.JobNo,det2.Det1Id  
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[1], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver2.DataSource = dt;
            this.grid_driver2.DataBind();
            Label2.Text = drivers[1];
            tb2.Style.Add("display", "block");
        }
        else
        {
            tb2.Style.Add("display", "none");
        }
        #endregion

        #region driver3
        if (drivers.Count >= 3)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode,det1.SealNo,det2.JobNo,det2.Det1Id  
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[2], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver3.DataSource = dt;
            this.grid_driver3.DataBind();
            Label3.Text = drivers[2];
            tb3.Style.Add("display", "block");
        }
        else
        {
            tb3.Style.Add("display", "none");
        }
        #endregion

        #region driver4
        if (drivers.Count >= 4)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[3], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver4.DataSource = dt;
            this.grid_driver4.DataBind();
            Label4.Text = drivers[3];
            tb4.Style.Add("display", "block");
        }
        else
        {
            tb4.Style.Add("display", "none");
        }

        #endregion

        #region driver5
        if (drivers.Count >= 5)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[4], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver5.DataSource = dt;
            this.grid_driver5.DataBind();
            Label5.Text = drivers[4];
            tb5.Style.Add("display", "block");
        }
        else
        {
            tb5.Style.Add("display", "none");
        }
        #endregion

        #region driver6
        if (drivers.Count >= 6)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[5], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver6.DataSource = dt;
            this.grid_driver6.DataBind();
            Label6.Text = drivers[5];
            tb6.Style.Add("display", "block");
        }
        else
        {
            tb6.Style.Add("display", "none");
        }
        #endregion

        #region driver7
        if (drivers.Count >= 7)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[6], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver7.DataSource = dt;
            this.grid_driver7.DataBind();
            Label7.Text = drivers[6];
            tb7.Style.Add("display", "block");
        }
        else
        {
            tb7.Style.Add("display", "none");
        }
        #endregion

        #region driver8
        if (drivers.Count >= 8)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[7], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver8.DataSource = dt;
            this.grid_driver8.DataBind();
            Label8.Text = drivers[7];
            tb8.Style.Add("display", "block");
        }
        else
        {
            tb8.Style.Add("display", "none");
        }
        #endregion

        #region driver9
        if (drivers.Count >= 9)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[8], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver9.DataSource = dt;
            this.grid_driver9.DataBind();
            Label9.Text = drivers[8];
            tb9.Style.Add("display", "block");
        }
        else
        {
            tb9.Style.Add("display", "none");
        }
        #endregion

        #region driver10
        if (drivers.Count >= 10)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[9], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver10.DataSource = dt;
            this.grid_driver10.DataBind();
            Label10.Text = drivers[9];
            tb10.Style.Add("display", "block");
        }
        else
        {
            tb10.Style.Add("display", "none");
        }
        #endregion

        #region driver11
        if (drivers.Count >= 11)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[10], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver11.DataSource = dt;
            this.grid_driver11.DataBind();
            Label11.Text = drivers[10];
            tb11.Style.Add("display", "block");
        }
        else
        {
            tb11.Style.Add("display", "none");
        }
        #endregion

        #region driver12
        if (drivers.Count >= 12)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[11], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver12.DataSource = dt;
            this.grid_driver12.DataBind();
            Label12.Text = drivers[11];
            tb12.Style.Add("display", "block");
        }
        else
        {
            tb12.Style.Add("display", "none");
        }
        #endregion

        #region driver13
        if (drivers.Count >= 13)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[12], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver13.DataSource = dt;
            this.grid_driver13.DataBind();
            Label13.Text = drivers[12];
            tb13.Style.Add("display", "block");
        }
        else
        {
            tb13.Style.Add("display", "none");
        }
        #endregion

        #region driver14
        if (drivers.Count >= 14)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[13], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver14.DataSource = dt;
            this.grid_driver14.DataBind();
            Label14.Text = drivers[13];
            tb14.Style.Add("display", "block");
        }
        else
        {
            tb14.Style.Add("display", "none");
        }
        #endregion

        #region driver15
        if (drivers.Count >= 15)
        {
            sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Statuscode ,det1.SealNo,det2.JobNo,det2.Det1Id 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.FromTime", drivers[14], date_searchDate.Date);
            dt = ConnectSql.GetTab(sql);
            this.grid_driver15.DataSource = dt;
            this.grid_driver15.DataBind();
            Label15.Text = drivers[14];
            tb15.Style.Add("display", "block");
        }
        else
        {
            tb15.Style.Add("display", "none");
        }
        #endregion

        #region Container Status
        //        sql = string.Format(@"select det1.Id,det1.JobNo,det1.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode 
        //from CTM_JobDet1 as det1
        //left outer join CTM_JobDet2 as det2 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
        //left outer join (
        //select det2.ContainerNo,MAX(FromTime) as MaxTime
        //from CTM_JobDet2 as det2 
        //left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
        //where datediff(d,'{0}',det2.FromDate )=0
        //group by det2.ContainerNo
        //) as det2MaxTime on det2.ContainerNo=det2MaxTime.ContainerNo and det2.FromTime=det2MaxTime.MaxTime
        //where datediff(d,'{0}',det2.FromDate)=0 and (det2MaxTime.MaxTime is not null or det2.Id is null)
        //order by det2.FromTime",date_searchDate.Date);
        sql = string.Format(@"select det1.Id,det1.JobNo,det1.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode,job.ClientId,job.WarehouseAddress 
from CTM_JobDet1 as det1
left outer join(select * From CTM_JobDet2 where Id in(
select max(det2.Id) From (
select det2.Det1Id,MAX(FromTime) as MaxTime
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where datediff(d,'{0}',det2.FromDate )=0
group by det2.Det1Id) as MaxTime
left outer join CTM_JobDet2 as det2 on det2.Det1Id=MaxTime.Det1Id and det2.FromTime=MaxTime.MaxTime
group by det2.Det1Id
)) as det2 on det1.Id=det2.Det1Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{0}',det2.FromDate )=0 and job.StatusCode<>'CNL'
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
	where datediff(d,'{0}',det2.FromDate )=0 and isnull(det2.DriverCode,'')<>''
	group by det2.DriverCode) as det2MaxTime
left outer join CTM_JobDet2 as det2  on det2.DriverCode=det2MaxTime.DriverCode and det2.FromTime=det2MaxTime.MaxTime 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{0}',det2.FromDate )=0 and job.StatusCode<>'CNL'
) as temp group by DriverCode)
) as det2 on log.Driver=det2.DriverCode
where DATEDIFF(d,'{0}',log.Date)=0", date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        grid_Driver.DataSource = dt;
        grid_Driver.DataBind();
        #endregion

        #region Trailer
        sql = string.Format(@"select Mast.Code as ChessisCode,Mast.Remark as Size,temp.Id,temp.ContainerNo,temp.ContainerType,temp.DriverCode,temp.FromCode,temp.ToCode,temp.TripCode,temp.FromTime,temp.ToTime,temp.Statuscode,temp.JobNo,temp.ContId 
from CTM_MastData as Mast
left outer join (
select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode,det1.JobNo,det1.Id as ContId  
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where det2.Id in(
select MAX(Id) from(
select det2.Id,det2MaxTime.ChessisCode
from (
select det2.ChessisCode,MAX(FromTime) as MaxTime
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where datediff(d,'{0}',det2.FromDate )=0 and ISNULL(det2.ChessisCode,'')<>''
group by det2.ChessisCode
) as det2MaxTime 
left outer join CTM_JobDet2 as det2 on det2.ChessisCode=det2MaxTime.ChessisCode and det2.FromTime=det2MaxTime.MaxTime
left outer join CTM_JobDet1 as det1 on det2.det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where DATEDIFF(d,'{0}',det2.FromDate )=0 and job.StatusCode<>'CNL'
) as temp group by ChessisCode)
) as temp on temp.ChessisCode=Mast.Code 
where Mast.Type='chessis'
order by Id desc,ChessisCode", date_searchDate.Date);
        dt = ConnectSql.GetTab(sql);
        this.grid_Trailer.DataSource = dt;
        this.grid_Trailer.DataBind();
        #endregion

        #region PackingLot
//        sql = string.Format(@"select mast.Id,det.DriverCode,det.Trailer,det.JobType,
//det.CreateDate,mast.Code,mast.Address,ISNULL(Size20-ISNULL(tab_20_in.Qty,0),0) as Size20,
//ISNULL(Size40-ISNULL(tab_40_in.Qty,0),0) as Size40
//from PackingLot mast left outer join PackingLotItem det on det.PLCode=mast.Code 
//left outer join(select count(det.Id) as Qty,max(PLCode) as Code from PackingLotItem det inner join PackingLot mast on det.PLCode=mast.Code where JobType='in' group by PLCode,Size20) as tab_20_in on tab_20_in.Code=det.PLCode
//left outer join(select count(det.Id) as Qty,max(PLCode) as Code from PackingLotItem det inner join PackingLot mast on det.PLCode=mast.Code where JobType='in' group by PLCode,Size40) as tab_40_in on tab_40_in.Code=det.PLCode
//left outer join CTM_JobDet2 det2 on det2.Id=det.TripId 
//left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id 
//left outer join CTM_MastData dt on dt.Code=det2.ChessisCode and dt.Type='chessis'");
        sql = string.Format(@"with trip1 as (
select FromParkingLot as Code,isnull(FromParkingLotType,0) as t,isnull(ChessisCode,'') as trailer,isnull(ContainerNo,'') as container,Id as TripId 
from CTM_JobDet2 where datediff(day,FromDate,getdate())>=0 and (Statuscode='S' or Statuscode='C') and isnull(FromParkingLotType,0)<>0
),
trip2 as (
select ToParkingLot as Code,isnull(ToParkingLotType,0) as t,isnull(ChessisCode,'') as trailer,isnull(ContainerNo,'') as container,Id as TripId 
from CTM_JobDet2 where datediff(day,ToDate,getdate())>=0 and Statuscode='C' and isnull(ToParkingLotType,0)<>0
),
trips as(
select * from trip1
union all
select * from trip2
),
trailer_ex as(
select Code,trailer,sum(t) as tl_ex,max(tripId) as maxId
From trips group by Code,trailer
),
parkinglot_ex as(
select *,sum(tl_ex)over(partition by Code) as pl_ex,
(select ContainerNo from CTM_JobDet2 where Id=maxId) as ContainerNo
From trailer_ex
where tl_ex>0
),
tb_trailer as (
select Id,Code,cast(isnull(Remark,0) as int) as size from CTM_MastData where [Type]='chessis'
),
parkinglot_ex1 as (
select pl.*,tl.size,sum(isnull(tl.size,0))over(partition by pl.Code) as pl_ex_size 
from parkinglot_ex as pl
left outer join tb_trailer as tl on pl.trailer=tl.Code
)
select pl.Code,pl.Address,pl.Name,pl.Size40,isnull(pl_ex,0) as pl_ex,isnull(pl_ex_size,0) as pl_ex_size,pl.Size40-isnull(pl_ex_size,0) as balance,
(select trailer+'['+ContainerNo+'],' from parkinglot_ex1 where Code=pl.Code  for xml path('')) as Trailer
from PackingLot as pl
left outer join (select distinct Code,pl_ex,pl_ex_size from parkinglot_ex1 )as pl1 on pl.Code=pl1.Code");
        dt = ConnectSql.GetTab(sql);
        grid_parkingLot.DataSource = dt;
        grid_parkingLot.DataBind();
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
    protected void grid_packingLot_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {

    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        //btn_Refresh_Click(null, null);
        //gridExport1.WriteXlsToResponse("Driver", true);
        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\Aspose.lic"));

        Workbook workbook = new Workbook();

        string sql_driver = string.Format(@"select Code from CTM_Driver");
        DataTable tab_driver = ConnectSql.GetTab(sql_driver);
        for (int i = 0; i < tab_driver.Rows.Count; i++)
        {
            string code = SafeValue.SafeString(tab_driver.Rows[i]["Code"]);
            if (date_searchDate.Date < new DateTime(1900, 1, 1))
            {
                date_searchDate.Date = DateTime.Now;
            }
            string sql = string.Format(@"select det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.FromCode,det2.ToCode,det2.FromTime,det2.Incentive1,det2.Incentive2,det2.Incentive3,det2.Statuscode 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.ContainerNo=det1.ContainerNo
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,'{1}',det2.FromDate)=0 and det2.DriverCode='{0}' and job.StatusCode<>'CNL'
order by det2.ContainerNo,det2.FromTime,det2.Id", code, date_searchDate.Date);//(select top(1) * from {0}) UNION (select * from {0} where rownum == (select count(*) from {0}))  select top(10) * from {0} where rownum=1
            DataTable dtTableName = ConnectSql.GetTab(sql);
            //throw  new Exception(dtTableName.Rows.Count.ToString());
            Worksheet worksheet = workbook.Worksheets[0]; ;
             if (i != 0)
             {
                 worksheet = workbook.Worksheets.Add(code);

             }
               worksheet.Name = code;
             
                Cells cells = worksheet.Cells;
                Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
                Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()];
                Aspose.Cells.Style style2 = workbook.Styles[workbook.Styles.Add()];
                style.Font.Name = "Arial";//文字字体 ,宋体
                style.Font.Size = 10;//文字大小  
                style.Font.IsBold = true;//粗体
                style.Borders[BorderType.TopBorder].Color = Color.Black;
                style.Borders[BorderType.BottomBorder].Color = Color.Black;
                style.Borders[BorderType.LeftBorder].Color = Color.Black;
                style.Borders[BorderType.RightBorder].Color = Color.Black;
                style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style.HorizontalAlignment = TextAlignmentType.Center;//文字居中  

                style1.Font.Name = "Arial";//文字字体 ,宋体
                style1.Font.Size = 10;//文字大小  
                style1.Font.IsBold = false;//粗体
                style1.Borders[BorderType.TopBorder].Color = Color.Black;
                style1.Borders[BorderType.BottomBorder].Color = Color.Black;
                style1.Borders[BorderType.LeftBorder].Color = Color.Black;
                style1.Borders[BorderType.RightBorder].Color = Color.Black;
                style1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style1.HorizontalAlignment = TextAlignmentType.Center;//文字居中  
                style1.BackgroundColor = Color.FromArgb(0xff, 0xff, 0x00);

                style2.Font.Name = "Arial";//文字字体 ,宋体
                style2.Font.Size = 10;//文字大小  
                style2.Font.IsBold = false;//粗体
                style2.Borders[BorderType.TopBorder].Color = Color.Black;
                style2.Borders[BorderType.BottomBorder].Color = Color.Black;
                style2.Borders[BorderType.LeftBorder].Color = Color.Black;
                style2.Borders[BorderType.RightBorder].Color = Color.Black;
                style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style2.HorizontalAlignment = TextAlignmentType.Left;//文字居中  
                

                cells[0, 0].PutValue("S/N");
                cells[0, 1].PutValue("ContainerNo");
                cells[0, 2].PutValue("ContainerType");
                cells[0, 3].PutValue("Trailer");
                cells[0, 4].PutValue("From");
                cells[0, 5].PutValue("To");
                cells[0, 6].PutValue("Time");
                cells[0, 7].PutValue("Incentive");
                cells[0, 8].PutValue("Overtime");
                cells[0, 9].PutValue("Standby");
                cells[0, 10].PutValue("Status");

                cells[0, 0].SetStyle(style);
                cells[0, 1].SetStyle(style);
                cells[0, 2].SetStyle(style);
                cells[0, 3].SetStyle(style);
                cells[0, 4].SetStyle(style);
                cells[0, 5].SetStyle(style);
                cells[0, 6].SetStyle(style);
                cells[0, 7].SetStyle(style);
                cells[0, 8].SetStyle(style);
                cells[0, 9].SetStyle(style);
                cells[0, 10].SetStyle(style);

                cells.SetColumnWidth(0, 5);
                cells.SetColumnWidth(1, 20);
                cells.SetColumnWidth(2, 20);
                cells.SetColumnWidth(3, 20);
                cells.SetColumnWidth(4, 20);
                cells.SetColumnWidth(5, 20);
                cells.SetColumnWidth(6, 10);
                cells.SetColumnWidth(7, 10);
                cells.SetColumnWidth(8, 10);
                cells.SetColumnWidth(9, 10);
                cells.SetColumnWidth(10, 5);

                for (int n = 0; n < dtTableName.Rows.Count; n++)
                {
                    cells[n + 1, 0].PutValue(n + 1);                    
                    cells[n + 1, 1].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["ContainerNo"]));
                    cells[n + 1, 2].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["ContainerType"]));
                    cells[n + 1, 3].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["ChessisCode"]));
                    cells[n + 1, 4].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["FromCode"]));
                    cells[n + 1, 5].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["ToCode"]));
                    cells[n + 1, 6].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["FromTime"]));
                    cells[n + 1, 7].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Incentive1"]));
                    cells[n + 1, 8].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Incentive2"]));
                    cells[n + 1, 9].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Incentive3"]));
                    cells[n + 1, 10].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Statuscode"]));

                    cells[n + 1, 0].SetStyle(style1);
                    cells[n + 1, 1].SetStyle(style2);
                    cells[n + 1, 2].SetStyle(style2);
                    cells[n + 1, 3].SetStyle(style2);
                    cells[n + 1, 4].SetStyle(style2);
                    cells[n + 1, 5].SetStyle(style2);
                    cells[n + 1, 6].SetStyle(style2);
                    cells[n + 1, 7].SetStyle(style2);
                    cells[n + 1, 8].SetStyle(style2);
                    cells[n + 1, 9].SetStyle(style2);
                    cells[n + 1, 10].SetStyle(style2);
                }

            }
        string path0 = string.Format("~/Excel/Driver_Trip_{0:yyyyMMdd}.xlsx",
        DateTime.Now.ToString("yyyyMMdd-HHmmss") ?? "01-01-2014"); //Request.QueryString["d"]
        string path = HttpContext.Current.Server.MapPath(path0); //POD_RECORD
        //workbook.Save(path);
        System.IO.MemoryStream ms = workbook.SaveToStream(); //生成数据流 
        byte[] bt = ms.ToArray();
        workbook.Save(path);
        Response.Redirect(path0.Substring(1));

    }
    
    public string ReturnDisplay(string driver,string teamNo) {
        string value = "";
        if (teamNo == "")
        {
            value = "block";
        }
        else
        {
            string sql_driver = string.Format(@"select count(*) from CTM_Driver where TeamNo='{0}' and Code='{1}'", teamNo,driver);
            int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_driver),0);
            if (n > 0)
            {
                value = "block";
            }
            else
            {
                value = "none";
            }
            
        }
        return value;
    }
    protected void grid_Container_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {

    }
}