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
    public DataTable GetData(string jobNo, string client, string date1_from,string date1_to,string date2_from,string date2_to, string hblNo, string lotNo, string sku, string warehouse, string location
        , string mft_LotNo, string partNo, string onHold)
    {
        DataTable tab = C2.JobHouse.getStockBalance_New_G(jobNo,client, date1_from,date1_to, date2_from,date2_to, 
            hblNo,lotNo,sku,warehouse,location,mft_LotNo,partNo,onHold);
        return tab;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

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
        string dateFrom = "";
        string dateTo = "";
        string expiryFrom = "";
        string expiryTo = "";
        if (date_From_Date.Value != null && date_To_Date.Date != null)
        {
            dateFrom = date_From_Date.Date.ToString("yyyy-MM-dd");
            dateTo= date_To_Date.Date.ToString("yyyy-MM-dd");
        }
        if (date_From_ExpiryDate.Value != null && date_To_ExpiryDate.Date != null)
        {
            expiryFrom = date_From_ExpiryDate.Date.ToString("yyyy-MM-dd");
            expiryTo = date_To_ExpiryDate.Date.ToString("yyyy-MM-dd");
        }
        GetData(txt_JobNo.Text, this.txt_CustId.Text,dateFrom, dateTo,expiryFrom,expiryTo, txt_HblNo.Text, txt_LotNo.Text, this.txt_SKULine_Product.Text, this.cmb_WareHouse.Text, txt_Location.Text
            ,txt_Mft_LotNo.Text, btn_PartNo.Text,SafeValue.SafeString(cmb_OnHold.Value));
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
    public string trBackColor(object par)
    {
        decimal qty = SafeValue.SafeDecimal(par) ;
        string res = "";
        if (qty < 0)
        {
            res = "background-color:red;color:white;font-weight:bold";
        }
        else {
            res = "background-color:white;color:black";
        }
        return res;
    }
    public string show(object par)
    {
        string para = SafeValue.SafeString(par);
        string res = "";
        if(para=="FRT")
            res = "display:block";
        else
            res = "display:none";
        return res;
    }
    public string show1(object par)
    {
        string para = SafeValue.SafeString(par);
        string res = "";
        if (para != "FRT")
            res = "display:block";
        else
            res = "display:none";
        return res;
    }
    public void SaveExcel()
    {
        DateTime now = DateTime.Today;

        string pathTemp = HttpContext.Current.Server.MapPath("~/files/templete/StockBalance.xls").ToLower();
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
        string dateFrom = "";
        string dateTo = "";
        string expiryFrom = "";
        string expiryTo = "";
        if (date_From_Date.Value != null && date_To_Date.Date != null)
        {
            dateFrom = date_From_Date.Date.ToString("yyyy-MM-dd");
            dateTo = date_To_Date.Date.ToString("yyyy-MM-dd");
        }
        if (date_From_ExpiryDate.Value != null && date_To_ExpiryDate.Date != null)
        {
            expiryFrom = date_From_ExpiryDate.Date.ToString("yyyy-MM-dd");
            expiryTo = date_To_ExpiryDate.Date.ToString("yyyy-MM-dd");
        }
        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\bin\License.lic"));
        Workbook workbook = new Workbook();
        workbook.Open(pathOpen);

        Worksheet sheet0 = workbook.Worksheets[0];
        Cells c1 = sheet0.Cells;

        int r = 1;
        c1[r, 1].PutValue("Job No");
        c1[r, 2].PutValue(txt_JobNo.Text);
        c1[r, 3].PutValue("Lot No");
        c1[r, 4].PutValue(txt_LotNo.Text);
        c1[r, 3].PutValue("Hbl No");
        c1[r, 6].PutValue(txt_HblNo.Text);
        c1[r, 3].PutValue("SKU");
        c1[r, 8].PutValue(txt_SKULine_Product.Text);
        c1[r, 3].PutValue("Onhold");
        c1[r, 10].PutValue(cmb_OnHold.Value);
        r = 3;
        c1[r, 1].PutValue("WareHouse");
        c1[r, 2].PutValue(SafeValue.SafeString(cmb_WareHouse.Value));
        c1[r, 3].PutValue("Location");
        c1[r, 4].PutValue(SafeValue.SafeString(txt_Location.Text));
        c1[r, 5].PutValue("Customer");
        c1[r, 7].PutValue(txt_CustName.Text);
        c1[r, 8].PutValue("Part No");
        c1[r, 10].PutValue(btn_PartNo.Text);

        r = 5;
        c1[r, 1].PutValue("Mft LotNo");
        c1[r, 2].PutValue(SafeValue.SafeString(txt_Mft_LotNo.Value));
        c1[r, 3].PutValue("Mft LotDate");
        c1[r, 4].PutValue(SafeValue.SafeString(date_From_Date.Date.ToString("yyyy-MM-dd")));
        c1[r, 5].PutValue("To");
        c1[r, 6].PutValue(SafeValue.SafeString(date_To_Date.Date.ToString("yyyy-MM-dd")));
        c1[r, 5].PutValue("Mft Expiry Date");
        c1[r, 8].PutValue(SafeValue.SafeString(date_From_ExpiryDate.Date.ToString("yyyy-MM-dd")));
        c1[r, 5].PutValue("To");
        c1[r, 10].PutValue(SafeValue.SafeString(date_To_ExpiryDate.Date.ToString("yyyy-MM-dd")));

        r = 10;

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

        DataTable dt = GetData(txt_JobNo.Text, this.txt_CustId.Text, dateFrom, dateTo, expiryFrom, expiryTo, txt_HblNo.Text, txt_LotNo.Text, this.txt_SKULine_Product.Text, this.cmb_WareHouse.Text, txt_Location.Text
            , txt_Mft_LotNo.Text, btn_PartNo.Text, SafeValue.SafeString(cmb_OnHold.Value));
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
            c1[r, 11].PutValue(R.Text(SafeValue.SafeDateStr(dt.Rows[i]["JobDate"])));
            c1[r, 12].PutValue(R.Text(dt.Rows[i]["ActualItem"]));
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
            c1[r, 16].PutValue(R.Text(BalanceWeight(dt.Rows[i]["LineId"])));
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