using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportAccount_OperationReport_KPI_TEU : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dtime = DateTime.Now;
            search_from.Date = dtime.AddDays(1 - dtime.Day);
            search_to.Date = dtime;
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"
with tb_today as(
select *,datediff(day,day0,day1)+1 as ds from (select @DateFrom as day0,@DateTo day1) as temp
),
tb_days as(
select top 360 ROW_NUMBER()over(order by Id) as Id from sysobjects
),
tb_days1 as (
select Id,DATEADD(day,Id-1,day0) as dd from tb_days
left outer join tb_today on 1=1
 where ds>=Id
),
tb_days2 as (
select Id,convert(varchar,dd,112) as dd from tb_days1
),
tb_trips as (
select det2.Id,convert(varchar,ToDate,112) as FromDate,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim 
from CTM_JobDet2 as det2
left outer join tb_today on 1=1
where det2.Statuscode='C' and datediff(day,tb_today.day0,ToDate)>=0 and datediff(day,tb_today.day1,ToDate)<=0
),
tb1 as (
select FromDate,sum(Incentive) as Incentive,sum(Claim) as Claim,count(Id) as Trips
from tb_trips group by FromDate
),
tb_inv as (
select convert(varchar,docdate,112) as docdate,locamt from xaarinvoice
left outer join tb_today on 1=1 
where datediff(day,tb_today.day0,docdate)>=0 and datediff(day,tb_today.day1,docdate)<=0
),
tb2 as(
select sum(locamt) as amt,docdate from tb_inv group by docdate
),
tb_cont_trips as (
select * from CTM_JobDet2 where datediff(day,(select day0 from tb_today),FromDate)>=0 and datediff(day,(select day1 from tb_today),FromDate)<=0
),
tb_cont as(
select * from (
select convert(varchar,(select top 1 FromDate from tb_cont_trips where det1Id=det1.Id and JobNo=job.JobNo and TripCode=(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'LOC' end) order by FromDate),112) as eta,
det1.ContainerType,isnull(substring(det1.ContainerType,1,1),'') as FCContType,(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'OTS' end) as JobType,
case when det1.StatusCode='Completed' then
(select sum(Price) from job_cost as jc where LineType='CONT' and jc.JobNo=det1.JobNo)
else 0 end as fee
from CTM_JobDet1 as det1
left outer join CTM_Job as job on job.jobno=det1.jobno
) as temp
where datediff(day,(select day0 from tb_today),eta)>=0 and datediff(day,(select day1 from tb_today),eta)<=0
),
tb3 as (
select eta,
sum(case when JobType='IMP' and FCContType='2' then 1 else 0 end) as i20,
sum(case when JobType='IMP' and FCContType='4' then 1 else 0 end) as i40,
sum(case when JobType='EXP' and FCContType='2' then 1 else 0 end) as e20,
sum(case when JobType='EXP' and FCContType='4' then 1 else 0 end) as e40,
sum(case when JobType='OTS' and FCContType='2' then 1 else 0 end) as o20,
sum(case when JobType='OTS' and FCContType='4' then 1 else 0 end) as o40,
sum(case when FCContType='2' or FCContType='4' then 0 else 1 end) as oe,
sum(fee) as fee
from tb_cont group by eta
),
tb_psaRB as (
select convert(varchar,[BILL DATE] ,112) as billdate,isnull(amount,0) as amount 
from psa_bill 
left outer join tb_today on 1=1 
where datediff(day,tb_today.day0,[BILL DATE] )>=0 and datediff(day,tb_today.day1,[BILL DATE] )<=0 and amount < 0
),
tb4 as (
select sum(amount) as amt,billdate from tb_psaRB group by billdate
),
tb_show as (
select dd,isnull(tb2.amt,0) as inv,isnull(tb4.amt,0) as psaRB,
cast(isnull(tb1.Incentive,0) as decimal(16,2)) as Incentive,
cast(isnull(tb1.Claim,0) as decimal(16,2)) as Claim, isnull(tb1.Trips,0) as Trips,
isnull(tb3.i20,0) as i20,isnull(tb3.i40,0) as i40,isnull(tb3.e20,0) as e20,isnull(tb3.e40,0) as e40,
isnull(tb3.o20,0) as o20,isnull(tb3.o40,0) as o40,isnull(tb3.oe,0) as oe,
cast(isnull(tb3.fee,0) as decimal(16,2)) as fee
from tb_days2
left outer join tb1 on dd=tb1.FromDate
left outer join tb2 on dd=tb2.docdate
left outer join tb3 on dd=tb3.eta
left outer join tb4 on dd=tb4.billdate
)
select *,(i20+e20+o20)+(i40+e40+o40)*2 as teu from tb_show order by dd");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        //list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", Driver, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", SafeValue.SafeDate(search_from.Date, new DateTime(1990, 1, 1)).ToString("yyyyMMdd"), SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", SafeValue.SafeDate(search_to.Date, DateTime.Now).ToString("yyyyMMdd"), SqlDbType.NVarChar, 8));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        rp.DataSource = dt;
        rp.DataBind();
    }
    private int i20 = 0;
    private int i40 = 0;
    private int e20 = 0;
    private int e40 = 0;
    private int o20 = 0;
    private int o40 = 0;
    private int oe = 0;
    private int t20 = 0;
    private int t40 = 0;
    private int teu = 0;
    private decimal fee = 0;
    private int Trips = 0;
    private decimal Incentive = 0;
    private decimal Claim = 0;
    private decimal inv = 0;
    private decimal psaRB = 0;
    protected void rp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            i20 += SafeValue.SafeInt(dr["i20"], 0);
            i40 += SafeValue.SafeInt(dr["i40"], 0);
            e20 += SafeValue.SafeInt(dr["e20"], 0);
            e40 += SafeValue.SafeInt(dr["e40"], 0);
            o20 += SafeValue.SafeInt(dr["o20"], 0);
            o40 += SafeValue.SafeInt(dr["o40"], 0);
            oe += SafeValue.SafeInt(dr["oe"], 0);
            t20 += SafeValue.SafeInt(dr["i20"], 0) + SafeValue.SafeInt(dr["e20"], 0) + SafeValue.SafeInt(dr["o20"], 0);
            t40 += SafeValue.SafeInt(dr["i40"], 0) + SafeValue.SafeInt(dr["e40"], 0) + SafeValue.SafeInt(dr["o40"], 0);
            teu += SafeValue.SafeInt(dr["teu"], 0);
            fee += SafeValue.SafeDecimal(dr["fee"], 0);
            Trips += SafeValue.SafeInt(dr["Trips"], 0);
            Incentive += SafeValue.SafeDecimal(dr["Incentive"]);
            Claim += SafeValue.SafeDecimal(dr["Claim"]);
            inv += SafeValue.SafeDecimal(dr["inv"]);
            psaRB += SafeValue.SafeDecimal(dr["psaRB"]);
        }
        else
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                System.Web.UI.WebControls.Label lb_i20 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_i20");
                System.Web.UI.WebControls.Label lb_i40 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_i40");
                System.Web.UI.WebControls.Label lb_e20 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_e20");
                System.Web.UI.WebControls.Label lb_e40 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_e40");
                System.Web.UI.WebControls.Label lb_o20 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_o20");
                System.Web.UI.WebControls.Label lb_o40 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_o40");
                System.Web.UI.WebControls.Label lb_oe = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_oe");
                System.Web.UI.WebControls.Label lb_t20 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_t20");
                System.Web.UI.WebControls.Label lb_t40 = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_t40");
                System.Web.UI.WebControls.Label lb_te = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_te");
                System.Web.UI.WebControls.Label lb_teu = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_teu");
                System.Web.UI.WebControls.Label lb_fee = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_fee");
                System.Web.UI.WebControls.Label lb_Trips = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_Trips");
                System.Web.UI.WebControls.Label lb_Incentive = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_Incentive");
                System.Web.UI.WebControls.Label lb_Claim = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_Claim");
                System.Web.UI.WebControls.Label lb_inv = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_inv");
                System.Web.UI.WebControls.Label lb_psaRB = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_psaRB");

                lb_i20.Text = i20.ToString();
                lb_i40.Text = i40.ToString();
                lb_e20.Text = e20.ToString();
                lb_e40.Text = e40.ToString();
                lb_o20.Text = o20.ToString();
                lb_o40.Text = o40.ToString();
                lb_oe.Text = oe.ToString();
                lb_t20.Text = t20.ToString();
                lb_t40.Text = t40.ToString();
                lb_te.Text = oe.ToString();
                lb_teu.Text = teu.ToString();
                lb_fee.Text = fee.ToString();
                lb_Trips.Text = Trips.ToString();
                lb_Incentive.Text = Incentive.ToString();
                lb_Claim.Text = Claim.ToString();
                lb_inv.Text = inv.ToString();
                lb_psaRB.Text = psaRB.ToString();
            }
        }
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\Aspose.lic"));

        Workbook workbook = new Workbook();

        Worksheet worksheet = workbook.Worksheets[0];

        #region sql
        string sql = string.Format(@"
with tb_today as(
select *,datediff(day,day0,day1)+1 as ds from (select @DateFrom as day0,@DateTo day1) as temp
),
tb_days as(
select top 360 ROW_NUMBER()over(order by Id) as Id from sysobjects
),
tb_days1 as (
select Id,DATEADD(day,Id-1,day0) as dd from tb_days
left outer join tb_today on 1=1
 where ds>=Id
),
tb_days2 as (
select Id,convert(varchar,dd,112) as dd from tb_days1
),
tb_trips as (
select det2.Id,convert(varchar,ToDate,112) as FromDate,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim 
from CTM_JobDet2 as det2
left outer join tb_today on 1=1
where det2.Statuscode='C' and datediff(day,tb_today.day0,ToDate)>=0 and datediff(day,tb_today.day1,ToDate)<=0
),
tb1 as (
select FromDate,sum(Incentive) as Incentive,sum(Claim) as Claim,count(Id) as Trips
from tb_trips group by FromDate
),
tb_inv as (
select convert(varchar,docdate,112) as docdate,locamt from xaarinvoice
left outer join tb_today on 1=1 
where datediff(day,tb_today.day0,docdate)>=0 and datediff(day,tb_today.day1,docdate)<=0
),
tb2 as(
select sum(locamt) as amt,docdate from tb_inv group by docdate
),
tb_cont_trips as (
select * from CTM_JobDet2 where datediff(day,(select day0 from tb_today),FromDate)>=0 and datediff(day,(select day1 from tb_today),FromDate)<=0
),
tb_cont as(
select * from (
select convert(varchar,(select top 1 FromDate from tb_cont_trips where det1Id=det1.Id and JobNo=job.JobNo and TripCode=(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'LOC' end) order by FromDate),112) as eta,
det1.ContainerType,isnull(substring(det1.ContainerType,1,1),'') as FCContType,(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'OTS' end) as JobType,
case when det1.StatusCode='Completed' then
isnull(fee1,0)+isnull(fee2,0)+isnull(fee3,0)+isnull(fee4,0)+isnull(fee5,0)+isnull(fee6,0)+isnull(fee7,0)+isnull(fee8,0)+isnull(fee9,0)+isnull(fee10,0)
+isnull(fee11,0)+isnull(fee12,0)+isnull(fee13,0)+isnull(fee14,0)+isnull(fee15,0)+isnull(fee16,0)+isnull(fee17,0)+isnull(fee18,0)+isnull(fee19,0)+isnull(fee20,0)
+isnull(fee21,0)+isnull(fee22,0)+isnull(fee23,0)+isnull(fee24,0)+isnull(fee25,0)+isnull(fee26,0)+isnull(fee27,0)+isnull(fee28,0)+isnull(fee29,0)+isnull(fee30,0)
+isnull(fee31,0)+isnull(fee32,0)+isnull(fee33,0)+isnull(fee34,0)+isnull(fee35,0)+isnull(fee36,0)+isnull(fee37,0)+isnull(fee38,0)+isnull(fee39,0)+isnull(fee40,0)
else 0 end as fee
from CTM_JobDet1 as det1
left outer join CTM_Job as job on job.jobno=det1.jobno
) as temp
where datediff(day,(select day0 from tb_today),eta)>=0 and datediff(day,(select day1 from tb_today),eta)<=0
),
tb3 as (
select eta,
sum(case when JobType='IMP' and FCContType='2' then 1 else 0 end) as i20,
sum(case when JobType='IMP' and FCContType='4' then 1 else 0 end) as i40,
sum(case when JobType='EXP' and FCContType='2' then 1 else 0 end) as e20,
sum(case when JobType='EXP' and FCContType='4' then 1 else 0 end) as e40,
sum(case when JobType='OTS' and FCContType='2' then 1 else 0 end) as o20,
sum(case when JobType='OTS' and FCContType='4' then 1 else 0 end) as o40,
sum(case when FCContType='2' or FCContType='4' then 0 else 1 end) as oe,
sum(fee) as fee
from tb_cont group by eta
),
tb_psaRB as (
select convert(varchar,[BILL DATE] ,112) as billdate,isnull(amount,0) as amount 
from psa_bill 
left outer join tb_today on 1=1 
where datediff(day,tb_today.day0,[BILL DATE] )>=0 and datediff(day,tb_today.day1,[BILL DATE] )<=0 and amount < 0
),
tb4 as (
select sum(amount) as amt,billdate from tb_psaRB group by billdate
),
tb_show as (
select dd,isnull(tb2.amt,0) as inv,isnull(tb4.amt,0) as psaRB,
isnull(tb1.Incentive,0) as Incentive,isnull(tb1.Claim,0) as Claim, isnull(tb1.Trips,0) as Trips,
isnull(tb3.i20,0) as i20,isnull(tb3.i40,0) as i40,isnull(tb3.e20,0) as e20,isnull(tb3.e40,0) as e40,
isnull(tb3.o20,0) as o20,isnull(tb3.o40,0) as o40,isnull(tb3.oe,0) as oe,isnull(tb3.fee,0) as fee
from tb_days2
left outer join tb1 on dd=tb1.FromDate
left outer join tb2 on dd=tb2.docdate
left outer join tb3 on dd=tb3.eta
left outer join tb4 on dd=tb4.billdate
)
select *,(i20+e20+o20)+(i40+e40+o40)*2 as teu from tb_show order by dd");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        //list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", Driver, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", SafeValue.SafeDate(search_from.Date, new DateTime(1990, 1, 1)).ToString("yyyyMMdd"), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", SafeValue.SafeDate(search_to.Date, DateTime.Now).ToString("yyyyMMdd"), SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        #endregion

        #region Style
        Cells cells = worksheet.Cells;
        Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
        Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()];
        Aspose.Cells.Style style2 = workbook.Styles[workbook.Styles.Add()];
        style.Font.Name = "Arial";//文字字体 ,宋体
        style.Font.Size = 10;//文字大小  
        style.Font.IsBold = true;//粗体
        style.Font.Color = Color.White;         
        style.ForegroundColor = System.Drawing.Color.Gray;
        style.Pattern = BackgroundType.Solid;
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
        style1.ForegroundColor = System.Drawing.Color.WhiteSmoke;
        style1.Pattern = BackgroundType.Solid;
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
        #endregion

        #region Cell
        cells[0, 0].PutValue("DateFrom:" );

        cells[0, 1].PutValue(search_from.Date.ToString("dd/MM/yyyy"));
        cells[0, 3].PutValue("To: " + search_to.Date.ToString("dd/MM/yyyy"));

        cells.Merge(1,0,2,1);
        cells.Merge(1,1,2,1);
        cells[1, 0].PutValue("#");
        cells[1, 1].PutValue("Date");

        cells.Merge(1, 2, 1, 2);
        cells[1, 2].PutValue("Import");
        cells[2, 2].PutValue("20");
        cells[2, 3].PutValue("40");

        cells.Merge(1, 4, 1, 2);
        cells[1, 4].PutValue("Export");
        cells[2, 4].PutValue("20");
        cells[2, 5].PutValue("40");

        cells.Merge(1, 6, 1, 3);
        cells[1, 6].PutValue("Others");
        cells[2, 6].PutValue("20");
        cells[2, 7].PutValue("40");
        cells[2, 8].PutValue("Empty");

        cells.Merge(1, 9, 1, 3);
        cells[1, 9].PutValue("Total");
        cells[2, 9].PutValue("20");
        cells[2, 10].PutValue("40");
        cells[2, 11].PutValue("Empty");

        cells.Merge(1,12,2,1);
        cells[1, 12].PutValue("TEU");
        cells.Merge(1, 13, 2, 1);
        cells[1, 13].PutValue("EST-REV");
        cells.Merge(1, 14, 2, 1);
        cells[1, 14].PutValue("Trips");
        cells.Merge(1, 15, 2, 1);
        cells[1, 15].PutValue("Incentive");
        cells.Merge(1, 16, 2, 1);
        cells[1, 16].PutValue("Claim");
        cells.Merge(1, 17, 2, 1);
        cells[1, 17].PutValue("Invoice");
        cells.Merge(1, 18, 2, 1);
        cells[1, 18].PutValue("PSA RB");

        #endregion

        #region Set Style
        for (int n = 1; n < 3; n++)
        {
            cells[n, 0].SetStyle(style);
            cells[n, 1].SetStyle(style);
            cells[n, 2].SetStyle(style);
            cells[n, 3].SetStyle(style);
            cells[n, 4].SetStyle(style);
            cells[n, 5].SetStyle(style);
            cells[n, 6].SetStyle(style);
            cells[n, 7].SetStyle(style);
            cells[n, 8].SetStyle(style);
            cells[n, 9].SetStyle(style);
            cells[n, 10].SetStyle(style);
            cells[n, 11].SetStyle(style);
            cells[n, 12].SetStyle(style);
            cells[n, 13].SetStyle(style);
            cells[n, 14].SetStyle(style);
            cells[n, 15].SetStyle(style);
            cells[n, 16].SetStyle(style);
            cells[n, 17].SetStyle(style);
            cells[n, 18].SetStyle(style);
        }

        cells.SetColumnWidth(0, 10);
        cells.SetColumnWidth(1, 10);
        cells.SetColumnWidth(2, 8);
        cells.SetColumnWidth(3, 8);
        cells.SetColumnWidth(4, 8);
        cells.SetColumnWidth(5, 8);
        cells.SetColumnWidth(6, 8);
        cells.SetColumnWidth(7, 8);
        cells.SetColumnWidth(8, 8);
        cells.SetColumnWidth(8, 8);
        cells.SetColumnWidth(9, 8);
        cells.SetColumnWidth(11, 8);
        cells.SetColumnWidth(12, 10);
        cells.SetColumnWidth(13, 10);
        cells.SetColumnWidth(14, 10);
        cells.SetColumnWidth(15, 10);
        cells.SetColumnWidth(16, 10);
        cells.SetColumnWidth(17, 10);
        cells.SetColumnWidth(18, 10);
        #endregion

        int row = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            #region
            DataRow dr = dt.Rows[i];
            i20 += SafeValue.SafeInt(dr["i20"], 0);
            i40 += SafeValue.SafeInt(dr["i40"], 0);
            e20 += SafeValue.SafeInt(dr["e20"], 0);
            e40 += SafeValue.SafeInt(dr["e40"], 0);
            o20 += SafeValue.SafeInt(dr["o20"], 0);
            o40 += SafeValue.SafeInt(dr["o40"], 0);
            oe += SafeValue.SafeInt(dr["oe"], 0);
            t20 += SafeValue.SafeInt(dr["i20"], 0) + SafeValue.SafeInt(dr["e20"], 0) + SafeValue.SafeInt(dr["o20"], 0);
            t40 += SafeValue.SafeInt(dr["i40"], 0) + SafeValue.SafeInt(dr["e40"], 0) + SafeValue.SafeInt(dr["o40"], 0);
            teu += SafeValue.SafeInt(dr["teu"], 0);
            fee += SafeValue.SafeDecimal(dr["fee"], 0);
            Trips += SafeValue.SafeInt(dr["Trips"], 0);
            Incentive += SafeValue.SafeDecimal(dr["Incentive"]);
            Claim += SafeValue.SafeDecimal(dr["Claim"]);
            inv += SafeValue.SafeDecimal(dr["inv"]);
            psaRB += SafeValue.SafeDecimal(dr["psaRB"]);
            cells[i + 3, 0].PutValue(i + 1);
            cells[i + 3, 1].PutValue(dr["dd"]);
            cells[i + 3, 2].PutValue(dr["i20"]);
            cells[i + 3, 3].PutValue(dr["i40"]);
            cells[i + 3, 4].PutValue(dr["e20"]);
            cells[i + 3, 5].PutValue(dr["e40"]);
            cells[i + 3, 6].PutValue(dr["o20"]);
            cells[i + 3, 7].PutValue(dr["o40"]);
            cells[i + 3, 8].PutValue(dr["oe"]);
            cells[i + 3, 9].PutValue(SafeValue.SafeInt(dr["i20"], 0) + SafeValue.SafeInt(dr["e20"], 0) + SafeValue.SafeInt(dr["o20"], 0));
            cells[i + 3, 10].PutValue(SafeValue.SafeInt(dr["i40"], 0) + SafeValue.SafeInt(dr["e40"], 0) + SafeValue.SafeInt(dr["o40"], 0));
            cells[i + 3, 11].PutValue(dr["oe"]);
            cells[i + 3, 12].PutValue(SafeValue.SafeDecimal(dr["teu"]));
            cells[i + 3, 13].PutValue(SafeValue.SafeDecimal(dr["fee"]));
            cells[i + 3, 14].PutValue(SafeValue.SafeDecimal(dr["Trips"]));
            cells[i + 3, 15].PutValue(SafeValue.SafeDecimal(dr["Incentive"]));
            cells[i + 3, 16].PutValue(SafeValue.SafeDecimal(dr["Claim"]));
            cells[i + 3, 17].PutValue(SafeValue.SafeDecimal(dr["inv"]));
            cells[i + 3, 18].PutValue(SafeValue.SafeDecimal(dr["psaRB"]));

            cells[i + 3, 0].SetStyle(style1);
            cells[i + 3, 1].SetStyle(style1);
            cells[i + 3, 2].SetStyle(style1);
            cells[i + 3, 3].SetStyle(style1);
            cells[i + 3, 4].SetStyle(style1);
            cells[i + 3, 5].SetStyle(style1);
            cells[i + 3, 6].SetStyle(style1);
            cells[i + 3, 7].SetStyle(style1);
            cells[i + 3, 8].SetStyle(style1);
            cells[i + 3, 9].SetStyle(style1);
            cells[i + 3, 10].SetStyle(style1);
            cells[i + 3, 11].SetStyle(style1);
            cells[i + 3, 12].SetStyle(style1);
            cells[i + 3, 13].SetStyle(style1);
            cells[i + 3, 14].SetStyle(style1);
            cells[i + 3, 15].SetStyle(style1);
            cells[i + 3, 16].SetStyle(style1);
            cells[i + 3, 17].SetStyle(style1);
            cells[i + 3, 18].SetStyle(style1);
            #endregion
            row = i;
        }

        #region Total
        row = row + 4;
        cells[row, 2].PutValue(i20);
        cells[row, 3].PutValue(i40);
        cells[row, 4].PutValue(e20);
        cells[row, 5].PutValue(e40);
        cells[row, 6].PutValue(o20);
        cells[row, 7].PutValue(o40);
        cells[row, 8].PutValue(oe);
        cells[row, 9].PutValue(t20);
        cells[row, 10].PutValue(t40);
        cells[row, 11].PutValue(oe);
        cells[row, 12].PutValue(teu);
        cells[row, 13].PutValue(fee);
        cells[row, 14].PutValue(Trips);
        cells[row, 15].PutValue(Incentive);
        cells[row, 16].PutValue(Claim);
        cells[row, 17].PutValue(inv);
        cells[row, 18].PutValue(psaRB);

        cells[row, 0].SetStyle(style);
        cells[row, 1].SetStyle(style);
        cells[row, 2].SetStyle(style);
        cells[row, 3].SetStyle(style);
        cells[row, 4].SetStyle(style);
        cells[row, 5].SetStyle(style);
        cells[row, 6].SetStyle(style);
        cells[row, 7].SetStyle(style);
        cells[row, 8].SetStyle(style);
        cells[row, 9].SetStyle(style);
        cells[row, 10].SetStyle(style);
        cells[row, 11].SetStyle(style);
        cells[row, 12].SetStyle(style);
        cells[row, 13].SetStyle(style);
        cells[row, 14].SetStyle(style);
        cells[row, 15].SetStyle(style);
        cells[row, 16].SetStyle(style);
        cells[row, 17].SetStyle(style);
        cells[row, 18].SetStyle(style);

        #endregion
        string path0 = string.Format("~/Excel/KPI_TEU_{0:yyyyMMdd}.xlsx",
        DateTime.Now.ToString("yyyyMMdd-HHmmss") ?? "01-01-2014"); //Request.QueryString["d"]
        string path = HttpContext.Current.Server.MapPath(path0); //POD_RECORD
        //workbook.Save(path);
        System.IO.MemoryStream ms = workbook.SaveToStream(); //生成数据流 
        byte[] bt = ms.ToArray();
        workbook.Save(path);
        Response.Redirect(path0.Substring(1));
    }
}