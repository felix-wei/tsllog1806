using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Aspose.Cells;
using System.Drawing;

public partial class Modules_Tpt_Report_StockMove : System.Web.UI.Page
{
    public DataTable DataTab = null;
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddMonths(-6);
            this.txt_end.Date = DateTime.Today.AddDays(1);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
        }
        GetData(this.txt_CustId.Text, dateFrom,dateTo,txt_HblNo.Text,txt_LotNo.Text,txt_SKULine_Product.Text,cmb_WareHouse.Text,txt_Location.Text,txt_ContNo.Text);
    }
    public DataTable GetData(string client, string date1, string date2, string hblNo, string lotNo, string sku, string warehouse, string location, string contNo)
    {
        DataTable tab = C2.JobHouse.getStockMove(client, date1,date2, hblNo,lotNo,sku,warehouse,location,contNo);
        return tab;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null,null);
        SaveExcel();
    }
    public void SaveExcel()
    {
        DateTime now = DateTime.Today;

        string pathTemp = HttpContext.Current.Server.MapPath("~/files/templete/StockMove.xls").ToLower();

        string file = string.Format(@"StockMove for {0:dd} {0:MMMM} {0:yyyy}.xls", now).ToLower();
        string path = MapPath("~/files/excel/");
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

        c1[r, 2].PutValue(txt_LotNo.Text);
        c1[r, 5].PutValue(txt_HblNo.Text);
        c1[r, 8].PutValue(txt_ContNo.Text);
        c1[r, 11].PutValue(txt_SKULine_Product.Text);
        r = 3;
        c1[r, 2].PutValue(SafeValue.SafeString(cmb_WareHouse.Value));
        c1[r, 5].PutValue(txt_Location.Text);
        c1[r, 8].PutValue(txt_CustName.Text);
        r = 5;
        c1[r, 2].PutValue(txt_from.Date.ToString("yyyy-MM-dd"));
        c1[r, 5].PutValue(txt_end.Date.ToString("yyyy-MM-dd"));

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
        style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
        style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

        Aspose.Cells.Style style2 = workbook.Styles[workbook.Styles.Add()];
        style2.Font.IsBold = false;
        style2.Font.Size = 12;
        style2.HorizontalAlignment = TextAlignmentType.Center;
        style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
        style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
        style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

        //Select Data
        int cnt = 0;
        r = 10;
        #region Data
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
        }

        DataTable dt = GetData(this.txt_CustId.Text, dateFrom, dateTo, txt_HblNo.Text, txt_LotNo.Text, txt_SKULine_Product.Text, cmb_WareHouse.Text, txt_Location.Text, txt_ContNo.Text);
        int Colnum = dt.Columns.Count;//Cell Colnum number
        int Rownum = dt.Rows.Count;//Cell Row number      
        DataRow dr = null;
        DateTime today = DateTime.Now;
        int n = 0;
        decimal handQty = 0;
        decimal skuQty = 0;
        string lastRefNo = "";
        string lastContNo="";
        string lastBookingNo = "";
        string lastSkuCode = "";
        string lastHblNo = "";

        decimal inQty = 0;
        decimal outQty = 0;
        decimal skuIn=0;
        decimal skuOut=0;

        decimal inWeight = 0;
        decimal outWeight = 0;
        decimal inVolume = 0;
        decimal outVolume = 0;

        decimal handWeight = 0;
        decimal handVolume = 0;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int no = SafeValue.SafeInt(dt.Rows[i]["No"], 0);
            string type = SafeValue.SafeString(dt.Rows[i]["CargoType"]);
            string bookingNo = SafeValue.SafeString(dt.Rows[i]["BookingNo"]);
            
            string refNo = SafeValue.SafeString(dt.Rows[i]["RefNo"]);
            string hblNo = SafeValue.SafeString(dt.Rows[i]["HblNo"]);
            string skuCode = SafeValue.SafeString(dt.Rows[i]["SkuCode"]); 
            if (type == "IN")
            {
                inQty = SafeValue.SafeDecimal(dt.Rows[i]["QtyOrig"]);
                skuIn = SafeValue.SafeDecimal(dt.Rows[i]["PackQty"]);
                inWeight = SafeValue.SafeDecimal(dt.Rows[i]["WeightOrig"]);
                inVolume = SafeValue.SafeDecimal(dt.Rows[i]["VolumeOrig"]);
            }
            else {
                outQty = SafeValue.SafeDecimal(dt.Rows[i]["QtyOrig"]);
                skuOut = SafeValue.SafeDecimal(dt.Rows[i]["PackQty"]);
                outWeight = SafeValue.SafeDecimal(dt.Rows[i]["WeightOrig"]);
                outVolume = SafeValue.SafeDecimal(dt.Rows[i]["VolumeOrig"]);
            }
            string contNo = SafeValue.SafeString(dt.Rows[i]["ContNo"]);

            if (refNo == lastRefNo && bookingNo == lastBookingNo && skuCode == lastSkuCode && hblNo == lastHblNo)
            {
                if (n == 0)
                {
                    handQty = inQty - outQty;
                    skuQty = skuIn - skuOut;
                    handWeight = inWeight - outWeight;
                    handVolume = inVolume - outVolume;
                }
                else
                {
                    handQty = handQty - outQty;
                    skuQty = skuQty - skuOut;
                    handWeight = handWeight - outWeight;
                    handVolume = handVolume - outVolume;
                }   
                c1[r, 0].PutValue(R.Text(i+1));
                c1[r, 1].PutValue(R.Text(dt.Rows[i]["JobNo"]));
                c1[r, 2].PutValue(R.Text(Helper.Safe.SafeDateStr(dt.Rows[i]["JobDate"])));
                c1[r, 3].PutValue(R.Text(dt.Rows[i]["ClientId"]));
                c1[r, 4].PutValue(R.Text(dt.Rows[i]["CargoType"]));
                c1[r, 5].PutValue(R.Text(dt.Rows[i]["WareHouseCode"]));
                c1[r, 6].PutValue("");
                c1[r, 7].PutValue("");
                c1[r, 8].PutValue(R.Text(dt.Rows[i]["Location"]));
                c1[r, 9].PutValue(R.Text(dt.Rows[i]["BookingNo"]));
                c1[r, 10].PutValue(R.Text(dt.Rows[i]["HblNo"]));
                c1[r, 11].PutValue(R.Text(dt.Rows[i]["ContNo"]));
                c1[r, 12].PutValue(R.Text(dt.Rows[i]["CargoType"]));
                c1[r, 13].PutValue(R.Text(dt.Rows[i]["SkuCode"]));

                c1[r, 14].PutValue(R.Text(""));
                c1[r, 15].PutValue(R.Text(""));
                c1[r, 16].PutValue(R.Text(""));
                c1[r, 17].PutValue(R.Text(""));
                c1[r, 18].PutValue(R.Text(""));
                c1[r, 19].PutValue(R.Text(""));

                c1[r, 20].PutValue(R.Text(outQty));
                c1[r, 21].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
                c1[r, 22].PutValue(R.Text(outWeight));
                c1[r, 23].PutValue(R.Text(outVolume));
                c1[r, 24].PutValue(R.Text(skuOut));
                c1[r, 25].PutValue(R.Text(dt.Rows[i]["PackUom"]));

                c1[r, 26].PutValue(R.Text(handQty));
                c1[r, 27].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
                c1[r, 28].PutValue(R.Text(handWeight));
                c1[r, 29].PutValue(R.Text(handVolume));
                c1[r, 30].PutValue(R.Text(skuQty));
                c1[r, 31].PutValue(R.Text(dt.Rows[i]["PackUom"]));

                c1[r, 32].PutValue(R.Text(dt.Rows[i]["Marking1"]));
                c1[r, 33].PutValue(R.Text(dt.Rows[i]["Marking2"]));
				c1[r, 34].PutValue(R.Text(dt.Rows[i]["Remark1"]));
                n++;
                for (int j = 0; j <= 32; j++)
                {
                    c1[r, j].SetStyle(style2);

                }
            }
            else
            {
                n = 0;
                handQty = inQty;
                skuQty = skuIn ;
                handWeight = inWeight;
                handVolume = inVolume;
                lastRefNo = SafeValue.SafeString(dt.Rows[i]["RefNo"]);
                c1[r, 0].PutValue(R.Text(i+1));
                c1[r, 1].PutValue(R.Text(dt.Rows[i]["JobNo"]));
                c1[r, 2].PutValue(R.Text(Helper.Safe.SafeDateStr(dt.Rows[i]["JobDate"])));
                c1[r, 3].PutValue(R.Text(dt.Rows[i]["ClientId"]));
                c1[r, 4].PutValue(R.Text(dt.Rows[i]["CargoType"]));
                c1[r, 5].PutValue(R.Text(dt.Rows[i]["WareHouseCode"]));
                c1[r, 6].PutValue(R.Text(dt.Rows[i]["WhsType"]));
                c1[r, 7].PutValue(R.Text(dt.Rows[i]["PermitNo"]));
                c1[r, 8].PutValue(R.Text(dt.Rows[i]["Location"]));
                c1[r, 9].PutValue(R.Text(dt.Rows[i]["BookingNo"]));
                c1[r, 10].PutValue(R.Text(dt.Rows[i]["HblNo"]));
                c1[r, 11].PutValue(R.Text(dt.Rows[i]["ContNo"]));
                c1[r, 12].PutValue(R.Text(dt.Rows[i]["CargoType"]));
                c1[r, 13].PutValue(R.Text(dt.Rows[i]["SkuCode"]));

                c1[r, 14].PutValue(R.Text(inQty));
                c1[r, 15].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
                c1[r, 16].PutValue(R.Text(inWeight));
                c1[r, 17].PutValue(R.Text(inVolume));
                c1[r, 18].PutValue(R.Text(skuIn));
                c1[r, 19].PutValue(R.Text(dt.Rows[i]["PackUom"]));

                c1[r, 20].PutValue(R.Text(""));
                c1[r, 21].PutValue(R.Text(""));
                c1[r, 22].PutValue(R.Text(""));
                c1[r, 23].PutValue(R.Text(""));
                c1[r, 24].PutValue(R.Text(""));
                c1[r, 25].PutValue(R.Text(""));

                c1[r, 26].PutValue(R.Text(handQty));
                c1[r, 27].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
                c1[r, 28].PutValue(R.Text(handWeight));
                c1[r, 29].PutValue(R.Text(handVolume));
                c1[r, 30].PutValue(R.Text(skuQty));
                c1[r, 31].PutValue(R.Text(dt.Rows[i]["PackUom"]));

                c1[r, 32].PutValue(R.Text(dt.Rows[i]["Marking1"]));
                c1[r, 33].PutValue(R.Text(dt.Rows[i]["Marking2"]));
                c1[r, 34].PutValue(R.Text(dt.Rows[i]["Remark1"]));
                for (int j = 0; j <= 32; j++)
                {
                    c1[r, j].SetStyle(style1);

                }
            }
			lastContNo = contNo;
            lastBookingNo = bookingNo;
			lastHblNo=hblNo;
		    lastSkuCode=skuCode;
            dr = dt.Rows[i];


            cnt = i + 1;
            c1.SetRowHeight(r, 20);
            r++;
        }

        Aspose.Cells.Style style3 = workbook.Styles[workbook.Styles.Add()];
        style2.Font.IsBold = false;
        style2.Font.Size = 12;
        style2.HorizontalAlignment = TextAlignmentType.Center;
        style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
        style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        for (int j = 0; j <= 32; j++)
        {
            c1[r, j].SetStyle(style1);

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