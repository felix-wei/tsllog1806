using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class PagesContTrucking_Report_TripReport_Incentive : System.Web.UI.Page
{
	public int count=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
            search_DateTo.Date = DateTime.Now.AddDays(8);
            cb_TripStatus_All.Checked = true;
        }
        if (cb_TripStatus_All.Checked)
        {
            locked.Attributes.Add("class", "hidden");
            unlocked.Attributes.Add("class", "hidden");
            paid.Attributes.Add("class", "hidden");
            unpaid.Attributes.Add("class", "hidden");
        }
        else
        {
            if (cb_UnLocked.Checked)
            {
                unlocked.Attributes.Remove("class");
                locked.Attributes.Add("class", "hidden");
            }
            else
            {
                unlocked.Attributes.Add("class", "hidden");
                locked.Attributes.Remove("class");
            }
        }
        count = this.grid_Transport.VisibleRowCount;
        OnLoad();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string JobNo = txt_search_jobNo.Text;
        if (search_DateFrom.Date < new DateTime(1900, 1, 1))
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
        }
        if (search_DateTo.Date < new DateTime(1900, 1, 1))
        {
            search_DateTo.Date = DateTime.Now.AddDays(8);
        }
        bool v_tripStatusAll = cb_TripStatus_All.Checked;
        bool unLocked = cb_UnLocked.Checked;
        bool unPaid = cb_UnPaid.Checked;
        string status = "";
        if (unLocked)
            status = "LOCKED";
        else if (unPaid)
            status = "PAID";
        string sql_columns = string.Format(@"
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip' and (DriverCode=det2.DriverCode or isnull(DriverCode,'')='')) as Incentive1,
(select isnull(sum(isnull(Qty * Price, 0)), 0) from job_cost where TripNo = det2.Id and LineType = 'DP' and ChgCode = 'Trip' and DriverCode = det2.DriverCode2) as Incentive1_a,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime' and (DriverCode=det2.DriverCode or isnull(DriverCode,'')='')) as Incentive2,
(select isnull(sum(isnull(Qty * Price, 0)), 0) from job_cost where TripNo = det2.Id and LineType = 'DP' and ChgCode = 'OverTime' and DriverCode = det2.DriverCode2) as Incentive2_a,
");
        string where = "";
        if (v_tripStatusAll)
        {
            where = string.Format(@" where det2.Statuscode='C' and isnull(TripStatus,'')<>'PAID' and DATEDIFF(d,det2.ToDate,'{0}')<=0 and DATEDIFF(d,det2.ToDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date);
        }else
        {
            where = string.Format(@" where det2.Statuscode='C' and isnull(TripStatus,'')='{2}' and DATEDIFF(d,det2.ToDate,'{0}')<=0 and DATEDIFF(d,det2.ToDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date, status);
        }

        if (JobNo.Length > 0)
        {
            where += string.Format(@" and det2.JobNo like '%{0}%'",JobNo);
        }
        if (search_Driver.Text.Trim().Length > 0)
        {
            where += string.Format(@" and (det2.DriverCode='{0}' or det2.DriverCode2='{0}')", search_Driver.Text);
            sql_columns = string.Format(@"
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip' and det2.DriverCode='{0}' and(DriverCode=det2.DriverCode or isnull(DriverCode,'')='')) as Incentive1,
(select isnull(sum(isnull(Qty * Price, 0)), 0) from job_cost where TripNo = det2.Id and LineType = 'DP' and ChgCode = 'Trip' and DriverCode = det2.DriverCode2 and DriverCode='{0}') as Incentive1_a,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime' and det2.DriverCode='{0}' and(DriverCode=det2.DriverCode or isnull(DriverCode,'')='')) as Incentive2x,
(select isnull(sum(isnull(Qty * Price, 0)), 0) from job_cost where TripNo = det2.Id and LineType = 'DP' and ChgCode = 'OverTime' and DriverCode = det2.DriverCode2 and DriverCode='{0}') as Incentive2_a,


", search_Driver.Text);
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
        string sql = string.Format(@"with pri as (
select * from ctm_MastData where [Type]='tripcode'
),
tb1 as (
select det2.JobNo,det2.ContainerNo,det2.Id,det2.TripStatus,
{2}
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
isnull((select sum(isnull(Qty*Price,0)) from job_cost 
where DATEDIFF(d,EventDate,'{0}')<=0 and DATEDIFF(d,EventDate,'{1}')>=0 AND DriverCode=det2.DriverCode and LineType='DC' and JobNo=det2.JobNo AND TripNo=det2.TripIndex),0) as Adjustment, 
det1.SealNo,det1.ContainerType,det1.ScheduleDate,det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.TripCode,det2.TowheadCode,det2.Overtime,det2.OverDistance,
det2.ChessisCode,det2.FromCode,det2.ToCode,job.JobType,det2.ParkingZone,det2.DriverCode,det2.DriverCode2,det2.SubCon_Code,det2.RequestTrailerType,(select top 1 Name from XXParty where PartyId=job.PartyId) as Client,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.TripCode),0) as TripCodePrice,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.Overtime),0) as OverTimePrice,
case when det2.OverDistance='Y' then isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code='OJ'),0) else 0 end as QJPrice
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id 
left outer join CTM_Job as job on job.jobNo=det2.JobNo
left outer join CTM_Driver as dri on dri.Code=det2.DriverCode", search_DateFrom.Date, search_DateTo.Date,sql_columns);
        string sql_part1 = string.Format(@")
select *,TripCodePrice+OverTimePrice+QJPrice as Total,isnull(Incentive1,0)+isnull(Incentive1_a,0)+isnull(Incentive2,0)+isnull(Incentive3,0)+isnull(Incentive4,0) as TotalIncentive from tb1");

		//throw new Exception(sql + where + sql_part1);
        grid_Transport.DataSource = ConnectSql.GetTab(sql + where + sql_part1);
		
        grid_Transport.DataBind();
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("TripReport_Incentive" + (search_Driver.Text.Length > 0 ? "_" + search_Driver.Text : ""), true);
    }
	protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
		string par = e.Parameters;
		if(par=="Locked"){
		   if (list.Count > 0)
           {
		      for(int i=0;i<list.Count;i++){
				  int id=list[i].id;
				  string sql=string.Format(@"update ctm_jobdet2 set TripStatus='LOCKED' where Id={0}",id);
				  
				  ConnectSql_mb.ExecuteScalar(sql);
			  }
			  e.Result ="Success";
		   }
		}
       else if (par == "UnLocked")
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i].id;
                    string sql = string.Format(@"update ctm_jobdet2 set TripStatus='' where Id={0}", id);

                    ConnectSql_mb.ExecuteScalar(sql);
                }
                e.Result = "Success";
            }
        }
        else if (par == "Paid")
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i].id;
                    string sql = string.Format(@"update ctm_jobdet2 set TripStatus='PAID' where Id={0}", id);

                    ConnectSql_mb.ExecuteScalar(sql);
                }
                e.Result = "Success";
            }
        }
        else if (par == "UnPaid")
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i].id;
                    string sql = string.Format(@"update ctm_jobdet2 set TripStatus='' where Id={0}", id);

                    ConnectSql_mb.ExecuteScalar(sql);
                }
                e.Result = "Success";
            }
        }
    }
	List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;

        public Record(int _id)
        {
            id = _id;
        }
    }
	    private void OnLoad()
    {
        int start = 0;
        int end = 1000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Id"], "txt_Id") as ASPxTextBox;

            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0)));
            }
            else if (id == null)
                break;
        }
    }
}