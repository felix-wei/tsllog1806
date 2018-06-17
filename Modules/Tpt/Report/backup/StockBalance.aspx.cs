using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Aspose.Cells;
using System.Drawing;

public partial class Modules_Tpt_Report_StockBalance : System.Web.UI.Page
{
    public DataTable DataTab = null;
    public DataTable GetData()
    {
        string dateTo = "";
        if (txt_Date.Value != null)
        {
            dateTo = txt_Date.Date.ToString("yyyy-MM-dd");
        }
        DataTab = C2.JobHouse.getStockBalance_New(txt_JobNo.Text, this.txt_CustId.Text, dateTo, txt_HblNo.Text, txt_LotNo.Text, this.txt_SKULine_Product.Text, this.cmb_WareHouse.Text, txt_Location.Text, SafeValue.SafeString(cbb_CargoType.Value),"", txt_InventoryId.Text, txt_Marking.Text);
        return DataTab;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Date.Date = DateTime.Today.AddDays(1);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        GetData();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null,null);
        SaveExcel();
    }
    public decimal BalanceQty(object lineId)
    {
        string sql = string.Format(@"select (tab_in.BalQty-isnull(tab_out.BalQty,0)) as  BalQty  from job_house mast
inner join (select sum(QtyOrig) as BalQty,LineId from job_house where CargoType='IN' and CargoStatus='C'  group by LineId) as tab_in on tab_in.LineId=mast.Id
left join (select sum(QtyOrig) as BalQty,LineId from job_house where CargoType='OUT' and CargoStatus='C'  group by LineId) as tab_out on tab_out.LineId=mast.Id
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceSkuQty(object lineId)
    {
        string sql = string.Format(@"select (tab_in.BalQty-isnull(tab_out.BalQty,0)) as  BalQty  from job_house mast
inner join (select sum(PackQty) as BalQty,LineId from job_house where CargoType='IN' and CargoStatus='C'  group by LineId) as tab_in on tab_in.LineId=mast.Id
left join (select sum(PackQty) as BalQty,LineId from job_house where CargoType='OUT' and CargoStatus='C'  group by LineId) as tab_out on tab_out.LineId=mast.Id
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceWeight(object lineId)
    {
        string sql = string.Format(@"select (tab_in.BalQty-isnull(tab_out.BalQty,0)) as  BalQty  from job_house mast
inner join (select sum(WeightOrig) as BalQty,LineId from job_house where CargoType='IN' and CargoStatus='C'  group by LineId) as tab_in on tab_in.LineId=mast.Id
left join (select sum(WeightOrig) as BalQty,LineId from job_house where CargoType='OUT' and CargoStatus='C'  group by LineId) as tab_out on tab_out.LineId=mast.Id
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceVolume(object lineId)
    {
        string sql = string.Format(@"select (tab_in.BalQty-isnull(tab_out.BalQty,0)) as  BalQty  from job_house mast
inner join (select sum(VolumeOrig) as BalQty,LineId from job_house where CargoType='IN' and CargoStatus='C'  group by LineId) as tab_in on tab_in.LineId=mast.Id
left join (select sum(VolumeOrig) as BalQty,LineId from job_house where CargoType='OUT' and CargoStatus='C'  group by LineId) as tab_out on tab_out.LineId=mast.Id
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public void SaveExcel()
    {
        DateTime now = DateTime.Today;

        string pathTemp = HttpContext.Current.Server.MapPath("~/files/templete/StockBalance.xls").ToLower();
        string dateTo = "";
        if (txt_Date.Value != null)
        {
            dateTo = txt_Date.Date.ToString("yyyy-MM-dd");
        }
        string file = string.Format(@"StockBalance for {0:dd} {0:MMMM} {0:yyyy}.xls", now).ToLower();
        string path = MapPath("~/files/excel/" );
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string pathUrl = string.Format("~/files/excel/{0}", file);
        string pathOpen = HttpContext.Current.Server.MapPath(pathUrl).ToLower();
        using (FileStream input = File.OpenRead(pathTemp), output = File.OpenWrite(pathOpen))
        {
            int read = -1;
            byte[] buffer = new byte[4096];
            while (read != 0)
            {
                read = input.Read(buffer, 0, buffer.Length);
                output.Write(buffer, 0, read);
            }
        }

        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\bin\License.lic"));
        Workbook workbook = new Workbook();
        workbook.Open(pathOpen);

        Worksheet sheet0 = workbook.Worksheets[0];
        Cells c1 = sheet0.Cells;

        int r = 1;

        c1[r, 2].PutValue(txt_JobNo.Text);
        c1[r, 4].PutValue(txt_LotNo.Text);
        c1[r, 7].PutValue(txt_HblNo.Text);
        c1[r, 9].PutValue(txt_SKULine_Product.Text);
        c1[r, 11].PutValue(txt_Date.Date.ToString("yyyy-MM-dd"));
        r = 3;
        c1[r, 2].PutValue(SafeValue.SafeString(cmb_WareHouse.Value));
        c1[r, 4].PutValue(SafeValue.SafeString(txt_Location.Text));
        c1[r, 7].PutValue(txt_CustName.Text);

        r = 8;

        //set style
        Aspose.Cells.Style style0 = workbook.Styles[workbook.Styles.Add()];
        //style0.ForegroundColor = System.Drawing.ColorTranslator.FromHtml("#B09067");
        //style0.ForegroundColor = Color.FromName("#B09067");
        style0.ForegroundColor = Color.Yellow;
        style0.Pattern = BackgroundType.Solid;
        //c1[t, 2].SetStyle(style0);

        Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()];
        style1.Font.IsBold = false;
        style1.Font.Size = 12;
        style1.HorizontalAlignment = TextAlignmentType.Center;
        style1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;


        //Select Data
        int cnt = 0;
        #region Data

        DataTable dt = GetData();
        int Colnum = dt.Columns.Count;//Cell Colnum number
        int Rownum = dt.Rows.Count;//Cell Row number      
        DataRow dr = null;
        DateTime today = DateTime.Now;

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            c1[r, 0].PutValue(R.Text(i + 1));
            c1[r, 1].PutValue(R.Text(dt.Rows[i]["PartyName"]));
            c1[r, 2].PutValue(R.Text(dt.Rows[i]["JobNo"]));
            c1[r, 3].PutValue(R.Text(dt.Rows[i]["WareHouseCode"]));
            c1[r, 4].PutValue(R.Text(dt.Rows[i]["WhsType"]));
            c1[r, 5].PutValue(R.Text(dt.Rows[i]["PermitNo"]));
            c1[r, 6].PutValue(R.Text(dt.Rows[i]["Location"]));
            c1[r, 7].PutValue(R.Text(dt.Rows[i]["BookingNo"]));
            c1[r, 8].PutValue(R.Text(dt.Rows[i]["HblNo"]));
            c1[r, 9].PutValue(R.Text(dt.Rows[i]["ContNo"]));
            c1[r, 10].PutValue(R.Text(dt.Rows[i]["OpsType"]));
            c1[r, 11].PutValue(R.Text(SafeValue.SafeDateStr(dt.Rows[i]["StockDate"])));
            c1[r, 12].PutValue(R.Text(dt.Rows[i]["SkuCode"]));
            //Stock
            //c1[r, 10].PutValue(R.Text(dt.Rows[i]["QtyOrig"]));
            //c1[r, 11].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
            //c1[r, 12].PutValue(R.Text(dt.Rows[i]["WeightOrig"]));
            //c1[r, 13].PutValue(R.Text(dt.Rows[i]["VolumeOrig"]));
            //c1[r, 14].PutValue(R.Text(dt.Rows[i]["PackQty"]));
            //c1[r, 15].PutValue(R.Text(dt.Rows[i]["PackUom"]));
            //Balance
            c1[r, 13].PutValue(R.Text(dt.Rows[i]["BalQty"]));
            c1[r, 14].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
            c1[r, 15].PutValue(R.Text(BalanceWeight(dt.Rows[i]["LineId"])));
            c1[r, 16].PutValue(R.Text(BalanceVolume(dt.Rows[i]["LineId"])));
            c1[r, 17].PutValue(R.Text(dt.Rows[i]["SkuQty"]));
            c1[r, 18].PutValue(R.Text(dt.Rows[i]["PackUom"]));
 
            c1[r, 19].PutValue(R.Text(dt.Rows[i]["Marking1"]));
            c1[r, 20].PutValue(R.Text(dt.Rows[i]["Marking2"]));
            c1[r, 21].PutValue(R.Text(dt.Rows[i]["Remark1"]));

            dr = dt.Rows[i];

            for (int j = 0; j <= 18; j++)
            {
                c1[r, j].SetStyle(style1);
            }
            cnt = i + 1;
            r++;
        }


        #endregion

        workbook.Save(pathOpen, FileFormatType.Excel2003);

        MemoryStream ms = new MemoryStream();
        workbook.Save(ms, FileFormatType.Excel2003);

        byte[] bt = ms.GetBuffer();

        try
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Length", bt.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
            Response.BinaryWrite(bt);
        }
        catch (Exception exc)
        {
            throw new Exception(exc.Message + "/" + exc.StackTrace);
        }
        ms.Dispose();

    }
}