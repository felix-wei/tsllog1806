using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Cells;
using System.Drawing;

public partial class ReportAccount_OperationReport_KPI_CUS : System.Web.UI.Page
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
        string sql = string.Format(@"with tb_trips as (
select det2.Id,convert(varchar,ToDate,112) as FromDate,isnull(job.ClientId,'') as ClientId,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim 
from CTM_JobDet2 as det2 
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where det2.Statuscode='C' and datediff(day,@DateFrom,ToDate)>=0 and datediff(day,@DateTo,ToDate)<=0 and job.ClientId<>''
),
tb1 as (
select ClientId,sum(Incentive) as Incentive,sum(Claim) as Claim,count(Id) as Trips
from tb_trips group by ClientId
),
tb_inv as (
select convert(varchar,docdate,112) as docdate,locamt,isnull(PartyTo,'') as PartyTo from xaarinvoice
where datediff(day,@DateFrom,docdate)>=0 and datediff(day,@DateTo,docdate)<=0 and ExportInd='Y'
),
tb2 as(
select sum(locamt) as amt,PartyTo from tb_inv group by PartyTo
),
tb_cont as(
select * from (
select convert(varchar,job.EtaDate,112) as eta1,det1.ContainerType,isnull(substring(det1.ContainerType,1,1),'') as FCContType,isnull(job.ClientId,'') as ClientId,
  convert(varchar,(select top 1 FromDate from ctm_jobdet2 where det1Id=det1.Id and JobNo=job.JobNo and TripCode=(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'LOC' end) order by FromDate),112) as eta,
(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'OTS' end) as JobType,
isnull((select sum(isnull(psa.amount,0)) from psa_bill as psa where psa.[CONTAINER NUMBER]=det1.ContainerNo),0) as amount, 
isnull((select sum(isnull(psa.amount,0)) from psa_bill as psa where psa.amount<0 and psa.[CONTAINER NUMBER]=det1.ContainerNo),0) as amountRB 
from CTM_JobDet1 as det1
left outer join CTM_Job as job on job.jobno=det1.jobno)
as temp
where datediff(day,@DateFrom,eta)>=0 and datediff(day,@DateTo,eta)<=0
),
tb3 as (
select ClientId,
sum(case when JobType='IMP' and FCContType='2' then 1 else 0 end) as i20,
sum(case when JobType='IMP' and FCContType='4' then 1 else 0 end) as i40,
sum(case when JobType='EXP' and FCContType='2' then 1 else 0 end) as e20,
sum(case when JobType='EXP' and FCContType='4' then 1 else 0 end) as e40,
sum(case when JobType='OTS' and FCContType='2' then 1 else 0 end) as o20,
sum(case when JobType='OTS' and FCContType='4' then 1 else 0 end) as o40,
sum(case when FCContType='2' or FCContType='4' then 0 else 1 end) as oe,
sum(amountRB) as psaRB
from tb_cont group by ClientId
),
tb_show as (
select xp.Name as ClientId,isnull(tb2.amt,0) as inv,isnull(tb3.psaRB,0) as psaRB,
cast(isnull(tb1.Incentive,0) as decimal(16,2)) as Incentive,
cast(isnull(tb1.Claim,0) as decimal(16,2)) as Claim, isnull(tb1.Trips,0) as Trips,
isnull(tb3.i20,0) as i20,isnull(tb3.i40,0) as i40,isnull(tb3.e20,0) as e20,isnull(tb3.e40,0) as e40,
isnull(tb3.o20,0) as o20,isnull(tb3.o40,0) as o40,isnull(tb3.oe,0) as oe
from  tb1
left outer join tb2 on tb1.ClientId=tb2.PartyTo
left outer join tb3 on tb1.ClientId=tb3.ClientId
left outer join XXParty as xp on tb1.ClientId=xp.PartyId
)
select *,(i20+e20+o20)+(i40+e40+o40)*2 as teu from tb_show order by ClientId
");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        //list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", Driver, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", SafeValue.SafeDate(search_from.Date, new DateTime(1990, 1, 1)).ToString("yyyyMMdd"), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", SafeValue.SafeDate(search_to.Date, DateTime.Now).ToString("yyyyMMdd"), SqlDbType.NVarChar, 100));
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
    private int teu = 0;
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
            teu += SafeValue.SafeInt(dr["teu"], 0);
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
                System.Web.UI.WebControls.Label lb_teu = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_teu");
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
                lb_teu.Text = teu.ToString();
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

        #region SQL
        string sql = string.Format(@"with tb_trips as (
select det2.Id,convert(varchar,ToDate,112) as FromDate,isnull(job.ClientId,'') as ClientId,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim 
from CTM_JobDet2 as det2 
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where det2.Statuscode='C' and datediff(day,@DateFrom,ToDate)>=0 and datediff(day,@DateTo,ToDate)<=0 and job.ClientId<>''
),
tb1 as (
select ClientId,sum(Incentive) as Incentive,sum(Claim) as Claim,count(Id) as Trips
from tb_trips group by ClientId
),
tb_inv as (
select convert(varchar,docdate,112) as docdate,locamt,isnull(PartyTo,'') as PartyTo from xaarinvoice
where datediff(day,@DateFrom,docdate)>=0 and datediff(day,@DateTo,docdate)<=0 and ExportInd='Y'
),
tb2 as(
select sum(locamt) as amt,PartyTo from tb_inv group by PartyTo
),
tb_cont as(
select convert(varchar,job.EtaDate,112) as eta,det1.ContainerType,isnull(substring(det1.ContainerType,1,1),'') as FCContType,isnull(job.ClientId,'') as ClientId,
(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'OTS' end) as JobType,
isnull((select sum(isnull(psa.amount,0)) from psa_bill as psa where psa.[CONTAINER NUMBER]=det1.ContainerNo),0) as amount 
from CTM_JobDet1 as det1
left outer join CTM_Job as job on job.jobno=det1.jobno
where datediff(day,@DateFrom,job.EtaDate)>=0 and datediff(day,@DateTo,job.EtaDate)<=0
),
tb3 as (
select ClientId,
sum(case when JobType='IMP' and FCContType='2' then 1 else 0 end) as i20,
sum(case when JobType='IMP' and FCContType='4' then 1 else 0 end) as i40,
sum(case when JobType='EXP' and FCContType='2' then 1 else 0 end) as e20,
sum(case when JobType='EXP' and FCContType='4' then 1 else 0 end) as e40,
sum(case when JobType='OTS' and FCContType='2' then 1 else 0 end) as o20,
sum(case when JobType='OTS' and FCContType='4' then 1 else 0 end) as o40,
sum(case when FCContType='2' or FCContType='4' then 0 else 1 end) as oe,
sum(amount) as psaRB
from tb_cont group by ClientId
),
tb_show as (
select xp.Name as ClientId,isnull(tb2.amt,0) as inv,isnull(tb3.psaRB,0) as psaRB,
isnull(tb1.Incentive,0) as Incentive,isnull(tb1.Claim,0) as Claim, isnull(tb1.Trips,0) as Trips,
isnull(tb3.i20,0) as i20,isnull(tb3.i40,0) as i40,isnull(tb3.e20,0) as e20,isnull(tb3.e40,0) as e40,
isnull(tb3.o20,0) as o20,isnull(tb3.o40,0) as o40,isnull(tb3.oe,0) as oe
from  tb1
left outer join tb2 on tb1.ClientId=tb2.PartyTo
left outer join tb3 on tb1.ClientId=tb3.ClientId
left outer join XXParty as xp on tb1.ClientId=xp.PartyId
)
select *,(i20+e20+o20)+(i40+e40+o40)*2 as teu from tb_show order by ClientId
");
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
        cells[0, 0].PutValue("DateFrom:");
        cells[0, 1].PutValue(search_from.Date.ToString("dd/MM/yyyy"));
        cells[0, 2].PutValue("To: " + search_to.Date.ToString("dd/MM/yyyy"));

        cells.Merge(1, 0, 2, 1);
        cells.Merge(1, 1, 2, 1);
        cells[1, 0].PutValue("#");
        cells[1, 1].PutValue("Client");

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

        cells.Merge(1, 9, 2, 1);
        cells[1, 9].PutValue("TEU");
        cells.Merge(1, 10, 2, 1);
        cells[1, 10].PutValue("Trips");
        cells.Merge(1, 11, 2, 1);
        cells[1, 11].PutValue("Incentive");
        cells.Merge(1, 12, 2, 1);
        cells[1, 12].PutValue("Claim");
        cells.Merge(1, 13, 2, 1);
        cells[1, 13].PutValue("Invoice");
        cells.Merge(1, 14, 2, 1);
        cells[1, 14].PutValue("PSA RB");

        cells[0, 0].SetStyle(style);
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
        }

        cells.SetColumnWidth(0, 10);
        cells.SetColumnWidth(1, 30);
        cells.SetColumnWidth(2, 8);
        cells.SetColumnWidth(3, 8);
        cells.SetColumnWidth(4, 8);
        cells.SetColumnWidth(5, 8);
        cells.SetColumnWidth(6, 8);
        cells.SetColumnWidth(7, 8);
        cells.SetColumnWidth(8, 8);
        cells.SetColumnWidth(8, 8);
        cells.SetColumnWidth(9, 10);
        cells.SetColumnWidth(11, 10);
        cells.SetColumnWidth(12, 10);
        cells.SetColumnWidth(13, 10);
        cells.SetColumnWidth(14, 10);
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
            teu += SafeValue.SafeInt(dr["teu"], 0);
            Trips += SafeValue.SafeInt(dr["Trips"], 0);
            Incentive += SafeValue.SafeDecimal(dr["Incentive"]);
            Claim += SafeValue.SafeDecimal(dr["Claim"]);
            inv += SafeValue.SafeDecimal(dr["inv"]);
            psaRB += SafeValue.SafeDecimal(dr["psaRB"]);
            cells[i + 3, 0].PutValue(i + 1);
            cells[i + 3, 1].PutValue(dr["ClientId"]);
            cells[i + 3, 2].PutValue(dr["i20"]);
            cells[i + 3, 3].PutValue(dr["i40"]);
            cells[i + 3, 4].PutValue(dr["e20"]);
            cells[i + 3, 5].PutValue(dr["e40"]);
            cells[i + 3, 6].PutValue(dr["o20"]);
            cells[i + 3, 7].PutValue(dr["o40"]);
            cells[i + 3, 8].PutValue(dr["oe"]);
            cells[i + 3, 9].PutValue(dr["teu"]);
            cells[i + 3, 10].PutValue(dr["Trips"]);
            cells[i + 3, 11].PutValue(dr["Incentive"]);
            cells[i + 3, 12].PutValue(dr["Claim"]);
            cells[i + 3, 13].PutValue(dr["inv"]);
            cells[i + 3, 14].PutValue(dr["psaRB"]);

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
            #endregion

            row = i;
        }
        row = row + 4;
        #region Total
        cells[row, 2].PutValue(i20);
        cells[row, 3].PutValue(i40);
        cells[row, 4].PutValue(e20);
        cells[row, 5].PutValue(e40);
        cells[row, 6].PutValue(o20);
        cells[row, 7].PutValue(o40);
        cells[row, 8].PutValue(oe);
        cells[row, 9].PutValue(teu);
        cells[row, 10].PutValue(Trips);
        cells[row, 11].PutValue(Incentive);
        cells[row, 12].PutValue(Claim);
        cells[row, 13].PutValue(inv);
        cells[row, 14].PutValue(psaRB);

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
        #endregion

        string path0 = string.Format("~/Excel/KPI_CUS_{0:yyyyMMdd}.xlsx",
        DateTime.Now.ToString("yyyyMMdd-HHmmss") ?? "01-01-2014"); //Request.QueryString["d"]
        string path = HttpContext.Current.Server.MapPath(path0); //POD_RECORD
        //workbook.Save(path);
        System.IO.MemoryStream ms = workbook.SaveToStream(); //生成数据流 
        byte[] bt = ms.ToArray();
        workbook.Save(path);
        Response.Redirect(path0.Substring(1));
    }
}