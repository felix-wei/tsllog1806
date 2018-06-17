using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportAccount_OperationReport_KPI_RateCT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = string.Format(@"select ChgcodeId as c,ChgcodeDes as d from xxchgcode
union all 
select '',''
order by ChgcodeId");
            DataTable dt = ConnectSql_mb.GetDataTable(sql);
            search_chgcode.DataSource = dt;
            search_chgcode.DataBind();

            sql = string.Format(@"select PartyId as c from xxparty where IsCustomer='true' and PartyId<>''
union all 
select ''
order by PartyId");
            dt = ConnectSql_mb.GetDataTable(sql);
            search_client.DataSource = dt;
            search_client.DataBind();

            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string ChgCode = search_chgcode.Text;
        string Client = search_client.Text;
        string sql_where = "";
        if (ChgCode.Length > 0)
        {
            sql_where += " and chgcode=@ChgCode";
        }
        if (Client.Length > 0)
        {
            sql_where += " and status5=@Client";
        }
        string sql = string.Format(@"select status5, (select chgcodedes from xxchgcode where chgcodeid=det1.chgcode) as des, unit, price 
from seaquotedet1 as det1 
where quoteid='-1' and status1='BILL' {0}", sql_where);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", ChgCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        rp.DataSource = dt;
        rp.DataBind();
    }
    protected void rp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

        }
        else
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {

            }
        }
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        string ChgCode = search_chgcode.Text;
        string Client = search_client.Text;
        string sql_where = "";
        if (ChgCode.Length > 0)
        {
            sql_where += " and chgcode=@ChgCode";
        }
        if (Client.Length > 0)
        {
            sql_where += " and status5=@Client";
        }
        string sql = string.Format(@"select status5, (select chgcodedes from xxchgcode where chgcodeid=det1.chgcode) as des, unit, price 
from seaquotedet1 as det1 
where quoteid='-1' and status1='BILL' {0}", sql_where);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", ChgCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\Aspose.lic"));

        Workbook workbook = new Workbook();

        Worksheet worksheet = workbook.Worksheets[0];


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

        cells[0, 0].PutValue("ChgCode:");
        cells[0, 1].PutValue(search_chgcode.Text);
        cells[0, 2].PutValue("Client:  " + search_client.Text);

        cells[1, 0].PutValue("#");
        cells[1, 1].PutValue("ChgCode");
        cells[1, 2].PutValue("Des");
        cells[1, 3].PutValue("Unit");
        cells[1, 4].PutValue("Price");


        cells[1, 0].SetStyle(style);
        cells[1, 1].SetStyle(style);
        cells[1, 2].SetStyle(style);
        cells[1, 3].SetStyle(style);
        cells[1, 4].SetStyle(style);


        cells.SetColumnWidth(0, 20);
        cells.SetColumnWidth(1, 20);
        cells.SetColumnWidth(2, 40);
        cells.SetColumnWidth(3, 20);
        cells.SetColumnWidth(4, 20);
        for (int i = 0; i < dt.Rows.Count; i++) {
            cells[i+2, 0].PutValue(i+1);
            cells[i + 2, 1].PutValue(dt.Rows[i]["status5"]);
            cells[i + 2, 2].PutValue(dt.Rows[i]["des"]);
            cells[i + 2, 3].PutValue(dt.Rows[i]["unit"]);
            cells[i + 2, 4].PutValue(dt.Rows[i]["price"]);

            cells[i + 2, 0].SetStyle(style1);
            cells[i + 2, 1].SetStyle(style1);
            cells[i + 2, 2].SetStyle(style1);
            cells[i + 2, 3].SetStyle(style1);
            cells[i + 2, 4].SetStyle(style1);
        }
        string path0 = string.Format("~/Excel/KPI_RateCT_{0:yyyyMMdd}.xlsx",
            DateTime.Now.ToString("yyyyMMdd-HHmmss") ?? "01-01-2014"); //Request.QueryString["d"]
        string path = HttpContext.Current.Server.MapPath(path0); //POD_RECORD
        //workbook.Save(path);
        System.IO.MemoryStream ms = workbook.SaveToStream(); //生成数据流 
        byte[] bt = ms.ToArray();
        workbook.Save(path);
        Response.Redirect(path0.Substring(1));
 
    }
}