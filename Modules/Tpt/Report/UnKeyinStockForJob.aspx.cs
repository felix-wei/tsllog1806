using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Tpt_Report_UnKeyinStockForJob : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_search_dateFrom.Date = DateTime.Now.AddDays(-7);
            txt_search_dateTo.Date = DateTime.Now;

            //btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = " JobStatus='Confirmed'";
        string sql = string.Format(@"select * from(select * from(select distinct job.StatusCode,job.Id,job.JobNo,job.QuoteNo,job.JobStatus,job.JobDate,job.YardRef as Depot,job.ClientRefNo,job.ClientId,
job.PermitNo,job.Remark,job.SpecialInstruction,job.EtaDate,job.EtaTime,job.EtdDate,job.IsTrucking,IsWarehouse,IsLocal,IsAdhoc,job.PartyId,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,job.ShowInvoice_Ind,
(select top 1 Name from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,
(select count(*) from XAArInvoice where MastRefNo=job.JobNo) as billed,inv.DocNo,
(select count(*) as Cnt from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where mast.MastRefNo=job.JobNo ) as Cnt
,(select count(*) from ctm_jobdet1 where JobNo=job.JobNo) as contsCnt,(select count(*) from ctm_jobdet2 where JobNo=job.JobNo) as tripsCnt,
(select count(*) from ctm_job where BillingRefNo=job.JobNo) as jobsCnt,(select count(*) from ctm_jobdet1 det1 inner join CTM_Job mast on det1.JobNo=mast.JobNo where BillingRefNo=job.JobNo) as subContsCnt,
(select count(*) from ctm_jobdet2 det2 inner join CTM_Job mast on det2.JobNo=mast.JobNo where BillingRefNo=job.JobNo) as subTripsCnt,isnull(tab_s.CntSt,0) CntSt,isnull(tab_c.CntC,0) as CntC,
isnull(tab_in.Cnt,0) InCnt,tab_in.BookingItem InBookingItem,tab_in.Qty InQty,tab_in.Weight InWeight,tab_in.Volume InVolume,tab_in.Location InLocation,
isnull(tab_out.Cnt,0) OutCnt,tab_out.BookingItem OutBookingItem,tab_out.Qty OutQty,tab_out.Weight OutWeight,tab_out.Volume OutVolume,tab_out.Location OutLocation
from CTM_Job as job 
left join(select count(Id) Cnt, max(BookingItem) BookingItem,max(SkuCode) SkuCode,sum(Qty) Qty,JobNo,sum(Weight) Weight,sum(Volume) Volume,max(Location) Location from job_house where CargoType='IN' group by JobNo) as tab_in on tab_in.JobNo=job.JobNo and JobStatus='Confirmed'
left join(select count(Id) Cnt,max(BookingItem) BookingItem,max(SkuCode) SkuCode,sum(Qty) Qty,JobNo,sum(Weight) Weight,sum(Volume) Volume,max(Location) Location from job_house where CargoType='OUT' group by JobNo) as tab_out on tab_out.JobNo=job.JobNo
left join(select DocNo,MastRefNo,MastType,LocAmt from XAArInvoice) as inv on inv.MastRefNo=job.JobNo
left join (select count(*) as CntSt,JobNo from job_house where OpsType='Storage' group by JobNo) as tab_s on tab_s.JobNo=job.JobNo
left join(select count(*) as CntC,JobNo from job_cost where LineType='STORAGE' group by JobNo) as tab_c on tab_c.JobNo=job.JobNo
) as tab where (InCnt>=0 and OutCnt=0) or (InCnt=0 and OutCnt>=0)) as tab ");//
        if (txt_search_jobNo.Text.Trim() != "")
        {
            where = GetWhere(where, string.Format(@"JobNo like '%{0}%'", txt_search_jobNo.Text.Replace("'", "").Trim()));
        }
        else
        {

            if (txt_search_dateFrom.Date > new DateTime(1900, 1, 1))
            {
                where = GetWhere(where, " datediff(d,'" + txt_search_dateFrom.Date + "',JobDate)>=0");
            }

            if (txt_search_dateTo.Date > new DateTime(1900, 1, 1))
            {
                where = GetWhere(where, " datediff(d,'" + txt_search_dateTo.Date + "',JobDate)<=0");
            }

            //if (cbb_StatusCode.Text != "All")
            //{
            //    where = GetWhere(where, " StatusCode='" + cbb_StatusCode.Text.Trim() + "'");
            //}

            if (btn_ClientId.Text.Length > 0)
            {
                where = GetWhere(where, " PartyId='" + btn_ClientId.Text.Replace("'", "") + "'");
            }
        }

        if (where.Length > 0)
        {
            sql += " where " + where + " and IsWarehouse='Yes'  order by EtaDate,JobNo,Id desc, JobDate asc";
        }
        //and  JobType in ('IMP','EXP','LOC','WGR','WDO','TPT','CRA','FRT','ROS','LI','LE','CT','LT','RE')
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_Transport.DataSource = dt;
        this.grid_Transport.DataBind();


    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }

    protected void grid_Transport_PageSizeChanged(object sender, EventArgs e)
    {
        btn_search_Click(null,null);
    }

    protected void grid_Transport_PageIndexChanged(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }

    protected void btn_SaveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("UnStockJob_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"), true);
    }
}