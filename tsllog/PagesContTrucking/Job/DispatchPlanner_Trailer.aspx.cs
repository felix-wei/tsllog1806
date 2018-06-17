using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class PagesContTrucking_Job_DispatchPlanner_Trailer : System.Web.UI.Page
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
        //string driver = "";
        string sql_where = "";
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



        #region Trailer
        //        string sql = string.Format(@"select Mast.Code as ChessisCode,Mast.Remark as Size,temp.Id,temp.ContainerNo,temp.ContainerType,temp.DriverCode,temp.FromCode,temp.ToCode,temp.TripCode,temp.FromTime,temp.ToTime,temp.Statuscode,temp.JobNo,temp.ContId 
        //from CTM_MastData as Mast
        //left outer join (
        //select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode,det1.JobNo,det1.Id as ContId  
        //from CTM_JobDet2 as det2
        //left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
        //where det2.Id in(
        //select MAX(Id) from(
        //select det2.Id,det2MaxTime.ChessisCode
        //from (
        //select det2.ChessisCode,MAX(FromTime) as MaxTime
        //from CTM_JobDet2 as det2 
        //left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
        //where det2.FromDate < '{0:yyyy-MM-dd}' and ISNULL(det2.ChessisCode,'')<>''
        //group by det2.ChessisCode
        //) as det2MaxTime 
        //left outer join CTM_JobDet2 as det2 on det2.ChessisCode=det2MaxTime.ChessisCode and det2.FromTime=det2MaxTime.MaxTime
        //left outer join CTM_JobDet1 as det1 on det2.det1Id=det1.Id
        //left outer join CTM_Job as job on det2.JobNo=job.JobNo
        //where det2.FromDate < '{0:yyyy-MM-dd}' and job.StatusCode<>'CNL'
        //) as temp group by ChessisCode)
        //) as temp on temp.ChessisCode=Mast.Code 
        //where Mast.Type='chessis'
        //order by ChessisCode", date_searchDate.Date.AddDays(1));

        string sql = string.Format(@"select Mast.Code as ChessisCode,Mast.Note2, Mast.Note4, Mast.Date6, Mast.Date7, Mast.Remark as Size,temp.Id,temp.ContainerNo,temp.ContainerType,temp.DriverCode,temp.FromCode,temp.ToCode,temp.ToParkingLot, temp.TripCode,temp.FromTime,temp.ToTime,temp.Statuscode,temp.JobNo,temp.ContId,(Case isnull(CurContStatus,'MT') when '' then 'MT' else isnull(CurContStatus,'MT') end)  as CurContStatus
from CTM_MastData as Mast
left outer join (
select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.ToParkingLot, det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode,det1.JobNo,det1.Id as ContId,
case det2.TripCode when 'IMP' then 'CONT-LD' when 'RET' then 'MT' when 'COL' then 'CONT-MT' when 'EXP' then 'MT' when 'SMT' then 'CONT-MT' when 'SLD' then 'CONT-LD' else 'MT' end as CurContStatus  
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where det2.Id in(
select Id from (
select det2.Id,det2.ChessisCode,ROW_NUMBER()over(partition by det2.ChessisCode order by FromDate desc,FromTime desc ) as rowId
from CTM_JobDet2 as det2 
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.FromDate < '{0:yyyy-MM-dd}' and job.StatusCode<>'CNL' and ISNULL(det2.ChessisCode,'')<>'' and det2.Statuscode='C'
) as temp where rowId=1)
) as temp on temp.ChessisCode=Mast.Code 
where Mast.Type='chessis' and Mast.Type1='Active'
order by  CurContStatus Desc,Size, ChessisCode", date_searchDate.Date.AddDays(1));
//order by  (case when isnull(curContStatus,'')='' then 1 when CurContStatus='MT' then 2 when CurContStatus='CONT-MT' then 3 when CurContStatus='CONT-LD' then 4 else 5 end),Size, ChessisCode", date_searchDate.Date.AddDays(1));
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_Trailer.DataSource = dt;
        this.grid_Trailer.DataBind();
        #endregion

    }
	
    protected void grid0_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string p = e.Parameters;
        //get informations from arinvoice
		e.Result = "";
        if (p == "B")
        {
			string tr_no = S.Text(txt_TrailerNo.Text).Trim(); //.Trim();
			string br_from = S.Text(txt_BorrowFrom.Text).Trim();
			DateTime dt1 = date_BorrowDate.Date;
			DateTime dt2 = date_ReturnBy.Date;
			
			C2.CtmMastData rec = new C2.CtmMastData();
                        rec.Type = "Chessis";
                        rec.Type1 = "Active";
                        rec.Code = S.Text(txt_TrailerNo.Text);
                        rec.Name = rec.Code;
                        rec.Remark = S.Text(cmb_TrailerSize.Value);
                        rec.Note1 = "";
                        rec.Note2 = "N";
                        rec.Note3 = "BORROW";
                        rec.Note4 = S.Text(txt_BorrowFrom.Value);
                        rec.Note5 = S.Text(txt_BorrowRemark.Value);;
                        rec.Note6 = "";
                        rec.Note7 = "";
                        rec.Note8 = "";
                        rec.Date6 = dt1;
                        rec.Date7 = dt2;
                        rec.Date1 = new DateTime(1990,1,1);
                        rec.Date2 = new DateTime(1990,1,1);
                        rec.Date3 = new DateTime(1990,1,1);
                        rec.Date4 = new DateTime(1990,1,1);
                        rec.Date5 = new DateTime(1990,1,1);
                        rec.Date8 = new DateTime(1990,1,1);
                        C2.Manager.ORManager.StartTracking(rec, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(rec);
						e.Result = "Trailer Borrowed";
		}
        if (p == "R")
        {
			string tr_no = S.Text(txt_TrailerNo2.Text).Trim();
			string br_from = S.Text(txt_BorrowFrom2.Text).Trim();
			string rtn_Remark = S.Text(txt_ReturnRemark.Text).Replace("'","");
			DateTime dt1 = date_BorrowDate2.Date;
			DateTime dt2 = date_ReturnBy2.Date;
			DateTime dt3 = date_ReturnDate.Date;
			string ret_sql = string.Format(@"Update Ctm_MastData set Date8='{2:yyyy-MM-dd}', Type1='InActive', Note6='{3}' where Code='{0}' and Date6='{1:yyyy-MM-dd}' and Type1='Active' ", tr_no, dt1, dt3, rtn_Remark);
			D.Exec(ret_sql);
			
						e.Result = "Trailer Returned";
			// set it
			
		}
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