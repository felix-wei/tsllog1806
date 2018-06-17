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

public partial class Modules_Tpt_Report_TransferReport : System.Web.UI.Page
{
    public DataTable DataTab = null;
    #region
    public string sql = string.Format(@"select * from(select distinct ROW_NUMBER()over (order by mast.BookingNo,SkuCode,job.JobDate) as No,job.JobNo,job.JobDate,mast.CargoType,
(select top 1 WareHouseCode from CTM_Job j where mast.JobNo=j.JobNo) as WareHouseCode,job.ClientId,mast.TransferNo,
job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.QtyOrig,
mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,mast.RefNo,
isnull(PackQty,0) as SkuQty,PackUom from job_house mast inner join ctm_job job on mast.JobNo=job.JobNo and job.JobType='TR'
)as tab");
    #endregion
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
        string where = "";
        if (txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
        }
        if (txt_LotNo.Text.Length > 0)
        {
            where = GetWhere(where, string.Format("BookingNo='{0}'", this.txt_LotNo.Text));
        }
        if (txt_HblNo.Text.Length > 0)
            where = GetWhere(where, string.Format("HblNo='{0}'", this.txt_HblNo.Text));
        if (txt_ContNo.Text.Length > 0)
            where = GetWhere(where, string.Format("ContNo='{0}'", this.txt_ContNo.Text));
        if (this.txt_SKULine_Product.Text.Length > 0)
            where = GetWhere(where, string.Format("SkuCode='{0}'", this.txt_SKULine_Product.Text));
        if (this.txt_CustId.Text.Length > 0)
        {
            where = GetWhere(where, string.Format(" ClientId='{0}'", this.txt_CustId.Text));
        }
        if (this.cmb_WareHouse.Text.Length > 0)
        {
            where = GetWhere(where, string.Format(" WareHouseCode ='{0}'", this.cmb_WareHouse.Text));
        }
        if (this.txt_Location.Text.Length > 0)
        {
            where = GetWhere(where, string.Format(" Location ='{0}'", this.txt_Location.Text));
        }
        if (dateTo.Length > 0)
        {
            where = GetWhere(where, string.Format("  (JobDate between '{0}' and '{1}')", dateFrom, dateTo));
        }
        if (where.Length > 0)
        {
            sql += " where " + where;
        }
        // sql += " group by det.ProductCode,sku.ProductClass,sku.HsCode,det.LotNo,mast.PartyName,det.qty3,det.DoType,det2.Packing order by det.ProductCode";
        GetData(sql);
    }
    public DataTable GetData(string sql)
    {
        DataTable tab = ConnectSql.GetTab(sql);
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
        btn_Sch_Click(null, null);
        SaveExcel();
    }
    public void SaveExcel()
    {
        DateTime now = DateTime.Today;

        string pathTemp = HttpContext.Current.Server.MapPath("~/files/templete/StockTransfer.xls").ToLower();

        string file = string.Format(@"StockTransfer for {0:dd} {0:MMMM} {0:yyyy}.xls", now).ToLower();
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
        c1[r, 4].PutValue(txt_HblNo.Text);
        c1[r, 7].PutValue(txt_ContNo.Text);
        c1[r, 10].PutValue(txt_SKULine_Product.Text);
        r = 3;
        c1[r, 2].PutValue(SafeValue.SafeString(cmb_WareHouse.Value));
        c1[r, 4].PutValue(txt_Location.Text);
        c1[r, 7].PutValue(txt_CustName.Text);
        r = 5;
        c1[r, 2].PutValue(txt_from.Date.ToString("yyyy-MM-dd"));
        c1[r, 4].PutValue(txt_end.Date.ToString("yyyy-MM-dd"));

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
        r = 10;
        #region Data

        DataTable dt = GetData(sql);
        int Colnum = dt.Columns.Count;//Cell Colnum number
        int Rownum = dt.Rows.Count;//Cell Row number      
        DataRow dr = null;
        DateTime today = DateTime.Now;
        int n = 0;
        decimal handQty = 0;
        decimal skuQty = 0;
        string jobNo = "";
        string lastContNo = "";
        string lastBookingNo = "";
        string lastSkuCode = "";
        string lastHblNo = "";

        decimal inQty = 0;
        decimal outQty = 0;
        decimal skuIn = 0;
        decimal skuOut = 0;

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
            string transferNo = SafeValue.SafeString(dt.Rows[i]["TransferNo"]);
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
            else
            {
                outQty = SafeValue.SafeDecimal(dt.Rows[i]["QtyOrig"]);
                skuOut = SafeValue.SafeDecimal(dt.Rows[i]["PackQty"]);
                outWeight = SafeValue.SafeDecimal(dt.Rows[i]["WeightOrig"]);
                outVolume = SafeValue.SafeDecimal(dt.Rows[i]["VolumeOrig"]);
            }
            string contNo = SafeValue.SafeString(dt.Rows[i]["ContNo"]);

            if (refNo == jobNo && bookingNo == lastBookingNo && skuCode == lastSkuCode && hblNo == lastHblNo)
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
                c1[r, 0].PutValue(R.Text(i + 1));
                c1[r, 1].PutValue(R.Text(dt.Rows[i]["JobNo"]));
                c1[r, 2].PutValue(R.Text(Helper.Safe.SafeDateStr(dt.Rows[i]["JobDate"])));
                c1[r, 3].PutValue(R.Text(dt.Rows[i]["TransferNo"]));
                c1[r, 4].PutValue(R.Text(dt.Rows[i]["RefNo"]));
                c1[r, 5].PutValue(R.Text(dt.Rows[i]["ClientId"]));
                c1[r, 6].PutValue(R.Text(dt.Rows[i]["WareHouseCode"]));
                c1[r, 7].PutValue(R.Text(dt.Rows[i]["Location"]));
                c1[r, 8].PutValue(R.Text(dt.Rows[i]["BookingNo"]));
                c1[r, 9].PutValue(R.Text(dt.Rows[i]["HblNo"]));
                c1[r, 10].PutValue(R.Text(dt.Rows[i]["ContNo"]));
                c1[r, 11].PutValue(R.Text(dt.Rows[i]["OpsType"]));
                c1[r, 12].PutValue(R.Text(dt.Rows[i]["SkuCode"]));

                c1[r, 13].PutValue(R.Text(dt.Rows[i]["QtyOrig"]));
                c1[r, 14].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
                c1[r, 15].PutValue(R.Text(dt.Rows[i]["WeightOrig"]));
                c1[r, 16].PutValue(R.Text(dt.Rows[i]["VolumeOrig"]));
                c1[r, 17].PutValue(R.Text(dt.Rows[i]["PackQty"]));
                c1[r, 18].PutValue(R.Text(dt.Rows[i]["PackUom"]));
                c1[r, 19].PutValue(R.Text(handQty));
                c1[r, 20].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
                c1[r, 21].PutValue(R.Text(handWeight));
                c1[r, 22].PutValue(R.Text(handVolume));
                c1[r, 23].PutValue(R.Text(skuQty));
                c1[r, 24].PutValue(R.Text(dt.Rows[i]["PackUom"]));
                c1[r, 25].PutValue(R.Text(dt.Rows[i]["Marking1"]));
                c1[r, 26].PutValue(R.Text(dt.Rows[i]["Marking2"]));
                c1[r, 27].PutValue(R.Text(dt.Rows[i]["Remark1"]));
                n++;
            }
            else
            {
                n = 0;
                jobNo = SafeValue.SafeString(dt.Rows[i]["JobNo"]);
                c1[r, 0].PutValue(R.Text(i + 1));
                c1[r, 1].PutValue(R.Text(dt.Rows[i]["JobNo"]));
                c1[r, 2].PutValue(R.Text(Helper.Safe.SafeDateStr(dt.Rows[i]["JobDate"])));
                c1[r, 3].PutValue(R.Text(dt.Rows[i]["TransferNo"]));
                c1[r, 4].PutValue(R.Text(dt.Rows[i]["RefNo"]));
                c1[r, 5].PutValue(R.Text(dt.Rows[i]["ClientId"]));
                c1[r, 6].PutValue(R.Text(dt.Rows[i]["WareHouseCode"]));
                c1[r, 7].PutValue(R.Text(dt.Rows[i]["Location"]));
                c1[r, 8].PutValue(R.Text(dt.Rows[i]["BookingNo"]));
                c1[r, 9].PutValue(R.Text(dt.Rows[i]["HblNo"]));
                c1[r, 10].PutValue(R.Text(dt.Rows[i]["ContNo"]));
                c1[r, 11].PutValue(R.Text(dt.Rows[i]["OpsType"]));
                c1[r, 12].PutValue(R.Text(dt.Rows[i]["SkuCode"]));

                c1[r, 13].PutValue(R.Text(dt.Rows[i]["QtyOrig"]));
                c1[r, 14].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
                c1[r, 15].PutValue(R.Text(dt.Rows[i]["WeightOrig"]));
                c1[r, 16].PutValue(R.Text(dt.Rows[i]["VolumeOrig"]));
                c1[r, 17].PutValue(R.Text(dt.Rows[i]["PackQty"]));
                c1[r, 18].PutValue(R.Text(dt.Rows[i]["PackUom"]));

                c1[r, 19].PutValue(R.Text(inQty));
                c1[r, 20].PutValue(R.Text(dt.Rows[i]["PackTypeOrig"]));
                c1[r, 21].PutValue(R.Text(dt.Rows[i]["WeightOrig"]));
                c1[r, 22].PutValue(R.Text(dt.Rows[i]["VolumeOrig"]));
                c1[r, 23].PutValue(R.Text(skuIn));
                c1[r, 24].PutValue(R.Text(dt.Rows[i]["PackUom"]));

                c1[r, 25].PutValue(R.Text(dt.Rows[i]["Marking1"]));
                c1[r, 26].PutValue(R.Text(dt.Rows[i]["Marking2"]));
                c1[r, 27].PutValue(R.Text(dt.Rows[i]["Remark1"]));
            }
            lastContNo = contNo;
            lastBookingNo = bookingNo;
            lastHblNo = hblNo;
            lastSkuCode = skuCode;
            dr = dt.Rows[i];

            for (int j = 0; j <= 25; j++)
            {
                c1[r, j].SetStyle(style1);

            }
            cnt = i + 1;
            c1.SetRowHeight(r, 20);
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