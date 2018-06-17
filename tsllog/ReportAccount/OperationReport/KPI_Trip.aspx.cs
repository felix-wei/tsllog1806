using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class ReportAccount_OperationReport_KPI_Trip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dtime = DateTime.Now;
            search_from.Date = dtime;
            search_to.Date = dtime;
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql_where = "";
        string sql_where2 = "";
        if (!search_driver.Text.Equals(""))
        {
            sql_where = "and DriverCode=@DriverCode";
            sql_where2 = "and DriverCode2=@DriverCode";
        }
//        string sql = string.Format(@"with tb_trips as (
//select Id,DriverCode,TripCode,
//(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(16,2)) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
//(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(16,2)) from job_cost where TripNo=det2.Id and LineType='CL') as Claim 
//from CTM_JobDet2 as det2 
//where Statuscode='C' and datediff(day,ToDate,@DateFrom)<=0 and datediff(day,ToDate,@DateTo)>=0 {0}
//)
//select DriverCode,sum(case when TripCode='SHF' then 0 else 1 end) as Trips,sum(case when TripCode='SHF' then 1 else 0 end) as shf,
//sum(Incentive) as Incentive,sum(Claim) as Claim 
//from tb_trips 
//group by DriverCode
//order by DriverCode", sql_where);
        string sql = string.Format(@"select DriverCode,sum(case when TripCode='SHF' then 0 else 1 end) as Trips,sum(case when TripCode='SHF' then 1 else 0 end) as shf,
sum(Incentive) as Incentive,sum(Claim) as Claim 
from (
select Id,DriverCode,TripCode,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='DP' and c.DriverCode=t.DriverCode),0) as Incentive,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='CL'),0) as Claim 
from CTM_JobDet2 as t 
where Statuscode='C' and datediff(day,FromDate,@DateFrom)<=0 and datediff(day,FromDate,@DateTo)>=0 {0}
union all
select Id,DriverCode2 as DriverCode,TripCode,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='DP' and c.DriverCode=t.DriverCode2),0) as Incentive,
0 as Claim 
from CTM_JobDet2 as t 
where Statuscode='C' and datediff(day,FromDate,@DateFrom)<=0 and datediff(day,FromDate,@DateTo)>=0 {1}
) as temp 
where DriverCode<>''
group by DriverCode
order by DriverCode", sql_where, sql_where2);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", search_driver.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", SafeValue.SafeDate(search_from.Date, new DateTime(1990, 1, 1)).ToString("yyyyMMdd"), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", SafeValue.SafeDate(search_to.Date, DateTime.Now).ToString("yyyyMMdd"), SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        rp.DataSource = dt;
        rp.DataBind();
    }

    private int Trips = 0;
    private int shf = 0;
    private decimal Incentive = 0;
    private decimal Claim = 0;

    protected void rp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            Trips += SafeValue.SafeInt(dr["Trips"], 0);
            shf += SafeValue.SafeInt(dr["shf"], 0);
            Incentive += SafeValue.SafeDecimal(dr["Incentive"]);
            Claim += SafeValue.SafeDecimal(dr["Claim"]);
        }
        else
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                System.Web.UI.WebControls.Label lb_datefrom = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_datefrom");
                System.Web.UI.WebControls.Label lb_dateto = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_dateto");
                lb_datefrom.Text = search_from.Date.ToString("dd/MM/yyyy");
                lb_dateto.Text = search_to.Date.ToString("dd/MM/yyyy");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                System.Web.UI.WebControls.Label lb_Trips = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_Trips");
                System.Web.UI.WebControls.Label lb_shf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_shf");
                System.Web.UI.WebControls.Label lb_Incentive = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_Incentive");
                System.Web.UI.WebControls.Label lb_Claim = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_Claim");

                lb_Trips.Text = Trips.ToString();
                lb_shf.Text = shf.ToString();
                lb_Incentive.Text = Incentive.ToString();
                lb_Claim.Text = Claim.ToString();
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
        string sql_where = "";
        string sql_where2 = "";
        if (!search_driver.Text.Equals(""))
        {
            sql_where = "and DriverCode=@DriverCode";
            sql_where2 = "and DriverCode2=@DriverCode";
        }
//        string sql = string.Format(@"with tb_trips as (
//select Id,DriverCode,TripCode,
//(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
//(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim 
//from CTM_JobDet2 as det2 
//where Statuscode='C' and datediff(day,ToDate,@DateFrom)<=0 and datediff(day,ToDate,@DateTo)>=0 {0}
//)
//select DriverCode,sum(case when TripCode='SHF' then 0 else 1 end) as Trips,sum(case when TripCode='SHF' then 1 else 0 end) as shf,
//sum(Incentive) as Incentive,sum(Claim) as Claim 
//from tb_trips 
//group by DriverCode
//order by DriverCode", sql_where);
        string sql = string.Format(@"select DriverCode,sum(case when TripCode='SHF' then 0 else 1 end) as Trips,sum(case when TripCode='SHF' then 1 else 0 end) as shf,
sum(Incentive) as Incentive,sum(Claim) as Claim 
from (
select Id,DriverCode,TripCode,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='DP' and c.DriverCode=t.DriverCode),0) as Incentive,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='CL'),0) as Claim 
from CTM_JobDet2 as t 
where Statuscode='C' and datediff(day,FromDate,@DateFrom)<=0 and datediff(day,FromDate,@DateTo)>=0 {0}
union all
select Id,DriverCode2 as DriverCode,TripCode,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='DP' and c.DriverCode=t.DriverCode2),0) as Incentive,
0 as Claim 
from CTM_JobDet2 as t 
where Statuscode='C' and datediff(day,FromDate,@DateFrom)<=0 and datediff(day,FromDate,@DateTo)>=0 {1}
) as temp 
where DriverCode<>''
group by DriverCode
order by DriverCode", sql_where, sql_where2);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", search_driver.Text, SqlDbType.NVarChar, 100));
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
        cells.Merge(0, 0, 1, 6);
        cells[0, 0].PutValue(search_from.Date.ToString("dd/MM/yyyy") + "-" + search_to.Date.ToString("dd/MM/yyyy"));

        cells[1, 0].PutValue("#");
        cells[1, 1].PutValue("Driver");
        cells[1, 2].PutValue("Trips");
        cells[1, 3].PutValue("SHF");
        cells[1, 4].PutValue("Incentive");
        cells[1, 5].PutValue("Claim");

        cells[0, 0].SetStyle(style);

        cells[1, 0].SetStyle(style);
        cells[1, 1].SetStyle(style);
        cells[1, 2].SetStyle(style);
        cells[1, 3].SetStyle(style);
        cells[1, 4].SetStyle(style);
        cells[1, 5].SetStyle(style);


        cells.SetColumnWidth(0, 5);
        cells.SetColumnWidth(1, 20);
        cells.SetColumnWidth(2, 20);
        cells.SetColumnWidth(3, 20);
        cells.SetColumnWidth(4, 20);
        cells.SetColumnWidth(5, 20);

        int row = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            Trips += SafeValue.SafeInt(dr["Trips"], 0);
            shf += SafeValue.SafeInt(dr["shf"], 0);
            Incentive += SafeValue.SafeDecimal(dr["Incentive"]);
            Claim += SafeValue.SafeDecimal(dr["Claim"]);
            cells[i + 2, 0].PutValue(i + 1);
            cells[i + 2, 1].PutValue(dr["DriverCode"]);
            cells[i + 2, 2].PutValue(dr["Trips"]);
            cells[i + 2, 3].PutValue(dr["shf"]);
            cells[i + 2, 4].PutValue(SafeValue.SafeDecimal(dr["Incentive"]));
            cells[i + 2, 5].PutValue(SafeValue.SafeDecimal(dr["Claim"]));


            cells[i + 2, 0].SetStyle(style1);
            cells[i + 2, 1].SetStyle(style1);
            cells[i + 2, 2].SetStyle(style1);
            cells[i + 2, 3].SetStyle(style1);
            cells[i + 2, 4].SetStyle(style1);
            cells[i + 2, 5].SetStyle(style1);

            row = i;
        }
        #endregion

        #region row
        row = row + 3;
        cells[row, 2].PutValue(Trips);
        cells[row, 3].PutValue(shf);
        cells[row, 4].PutValue(Incentive);
        cells[row, 5].PutValue(Claim);
        cells[row, 0].SetStyle(style);
        cells[row, 1].SetStyle(style);
        cells[row, 2].SetStyle(style);
        cells[row, 3].SetStyle(style);
        cells[row, 4].SetStyle(style);
        cells[row, 5].SetStyle(style);
        #endregion

        string path0 = string.Format("~/Excel/KPI_Trip_{0:yyyyMMdd}.xlsx",
        DateTime.Now.ToString("yyyyMMdd-HHmmss") ?? "01-01-2014"); //Request.QueryString["d"]
        string path = HttpContext.Current.Server.MapPath(path0); //POD_RECORD
        //workbook.Save(path);
        System.IO.MemoryStream ms = workbook.SaveToStream(); //生成数据流 
        byte[] bt = ms.ToArray();
        workbook.Save(path);
        Response.Redirect(path0.Substring(1));
    }
}