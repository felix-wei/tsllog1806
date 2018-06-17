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

public partial class WareHouse_Report_StockMove : System.Web.UI.Page
{
    public DataTable DataTab = null;
    public string sql = string.Format(@"select * from (select tab_in.No,tab_in.DoNo,tab_in.Product,tab_in.LotNo,tab_in.Id,
tab_in.PartyId,tab_in.PartyName,tab_in.DoDate,tab_in.PoNo,tab_in.Remark,tab_in.GrossWeight,tab_in.NettWeight
,tab_hand.Qty1 as InQty,isnull(tab_out.OutDate,'') as OutDate,isnull(tab_out.Qty1,0) as OutQty,tab_in.PermitNo,tab_in.PartyInvNo,
isnull(tab_in.Qty2,0) as BadQty,isnull(tab_in.Qty3,0) as InPallet,isnull(tab_out.Qty3,0) as OutPallet,tab_hand.Qty1-isnull(tab_out.Qty1,0) as HandQty
,tab_in.DoDate as InDate,tab_hand.PalletNo
,tab_in.Des1,tab_in.Att3,tab_in.WareHouseId,tab_in.Location from 
(select distinct row_number() over (order by det.Id) as No, max(det.Id) as Id, max(mast.DoNo) as DoNo,max(det.Product) as Product,max(Qty2) as Qty2,max(Qty3) as Qty3,max(det.LotNo) as LotNo,max(p.HsCode) as HsCode,max(p.ProductClass) as ProductClass,max(mast.PartyId) as PartyId,max(mast.PartyName) as PartyName,max(mast.DoDate) as DoDate,max(det.QtyPackWhole) as Pkg,max(det.QtyWholeLoose) as Unit,max(mast.WareHouseId) as WareHouseId,max(mast.PoNo) as PoNo,max(det.Remark) as Remark,
max(det.GrossWeight) as GrossWeight,max(det.NettWeight) as NettWeight,max(det.PalletNo) as PalletNo,max(det.Location) as Location,max(mast.PermitNo) as PermitNo,max(mast.PartyInvNo) as PartyInvNo,
max(det.Des1) as Des1,max(det.att1) as Att1,max(det.att2) as Att2,max(det.att3) as Att3,max(det.att4) as Att4,max(det.att5) as Att5,max(det.att6) as Att6,max(det.packing) as Packing from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode='CLS' 
left join ref_product p on p.code=det.Product
where det.Dotype='IN' and len(det.doNo)>0 group by det.Id,Product,LotNo,Des1,PalletNo,det.Remark) as tab_in 
inner join (select product ,LotNo,max(Qty1) as Qty1,max(Qty2) as Qty2,max(Qty3) as Qty3,max(mast.DoDate) as InDate,det.PalletNo,det.Remark,det.Id from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  
where det.DoType='IN' and len(det.DoNo)>0  group by det.Id,Product,LotNo,Des1,PalletNo,det.Remark) as tab_hand on tab_hand.Id=tab_in.Id 
left join (select Product,LotNo,max(Qty1) as Qty1,max(Qty2) as Qty2,max(Qty3) as Qty3,max(mast.DoDate) as OutDate,det.PalletNo,det.Remark,Des1,max(det.RelaId) as RelaId
 from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN')
where det.DoType='OUT' and len(det.DoNo)>0 group by det.DoNo,Product,LotNo,Des1,PalletNo,det.Remark) as tab_out on isnull(tab_out.RelaId,'')=isnull(tab_in.Id,'') 
) as tab where 1=1");
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
        if (this.txt_SKULine_Product.Text.Length > 0)
            sql += string.Format("and Product='{0}'", this.txt_SKULine_Product.Text);
        if (this.txt_LotNo.Text.Length > 0)
        {
            string[] arr = this.txt_LotNo.Text.Trim().Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == 0)
                    sql += string.Format(" and (LotNo='{0}'", arr[i]);
                else
                    sql += string.Format(" or LotNo='{0}'", arr[i]);
                if (i == arr.Length - 1)
                    sql += ")";

            }
            //sql += string.Format("and LotNo='{0}'", this.txt_LotNo.Text);
        }
        if (this.txt_CustId.Text.Length > 0)
        {
            sql += string.Format("and PartyId='{0}'", this.txt_CustId.Text);
        }
        if (this.cmb_WareHouse.Text.Length > 0)
        {
            sql += string.Format("and WareHouseId ='{0}'", this.cmb_WareHouse.Text);
        }
        if (this.cmb_loc.Text.Length > 0)
        {
            sql += string.Format("and Location ='{0}'", this.cmb_loc.Text);
        }
        if (dateTo.Length > 0)
        {
            sql += string.Format(" and (DoDate between '{0}' and '{1}')", dateFrom, dateTo);
        }
        // sql += " group by det.ProductCode,sku.ProductClass,sku.HsCode,det.LotNo,mast.PartyName,det.qty3,det.DoType,det2.Packing order by det.ProductCode";
        GetData();
    }
    public DataTable GetData() {
        DataTable tab = ConnectSql.GetTab(sql);
        return tab;
    }
    protected void cmb_loc_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string s = cmb_WareHouse.SelectedItem.Value.ToString();

        string Sql = "SELECT Id,Code,Name from ref_location where Loclevel='Unit' and WarehouseCode='" + s + "'";

        DataSet ds = ConnectSql.GetDataSet(Sql);

        cmb_loc.DataSource = ds.Tables[0];

        cmb_loc.TextField = "Code";

        cmb_loc.ValueField = "Code";

        cmb_loc.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        SaveExcel();
    }
    public void SaveExcel()
    {
        DateTime now = DateTime.Today;

        string pathTemp = HttpContext.Current.Server.MapPath("~/files/StockMove.xls").ToLower();

        string file = string.Format(@"StockMove for {0:dd} {0:MMMM} {0:yyyy}.xls", now).ToLower();
        string pathUrl = string.Format("~/files/{0}", file);
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

        int r = 3;
        
        //set style
        Aspose.Cells.Style style0 = workbook.Styles[workbook.Styles.Add()];
        //style0.ForegroundColor = System.Drawing.ColorTranslator.FromHtml("#B09067");
        //style0.ForegroundColor = Color.FromName("#B09067");
        style0.ForegroundColor = Color.Yellow;
        style0.Pattern = BackgroundType.Solid;
        //c1[t, 2].SetStyle(style0);

        Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()];
        style1.Font.IsBold = true;
        style1.Font.Size = 12;
        style1.HorizontalAlignment = TextAlignmentType.Center;
        style1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;


        //Select Data
        int cnt = 0;
        decimal handQty = 0;
        #region Data

        DataTable dt = GetData();
        int Colnum = dt.Columns.Count;//Cell Colnum number
        int Rownum = dt.Rows.Count;//Cell Row number      
        DataRow dr = null;
        DateTime today = DateTime.Now;
        int n = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
             int no = SafeValue.SafeInt(dt.Rows[i]["No"],0);
             decimal inQty = SafeValue.SafeDecimal(dt.Rows[i]["InQty"]);
             decimal outQty = SafeValue.SafeDecimal(dt.Rows[i]["OutQty"]);
             if (n != no)
             {
                 handQty = inQty - outQty;
                 c1[r, 0].PutValue(R.Text(dt.Rows[i]["No"]));
                 c1[r, 1].PutValue(R.Text(dt.Rows[i]["Location"]));
                 c1[r, 2].PutValue(R.Text(dt.Rows[i]["PermitNo"]));
                 c1[r, 3].PutValue(R.Text(dt.Rows[i]["PartyInvNo"]));
                 c1[r, 4].PutValue(R.Text(dt.Rows[i]["LotNo"]));
                 c1[r, 5].PutValue(R.Text(dt.Rows[i]["Product"]));
                 c1[r, 6].PutValue(R.Text(dt.Rows[i]["Des1"]));
                 c1[r, 7].PutValue(R.Text(dt.Rows[i]["PalletNo"]));
                 c1[r, 8].PutValue(R.Text(dt.Rows[i]["Att3"]));
                 c1[r, 9].PutValue(R.Text(dt.Rows[i]["GrossWeight"]));
                 c1[r, 10].PutValue(R.Text(dt.Rows[i]["NettWeight"]));
                 c1[r, 11].PutValue(R.Text(Helper.Safe.SafeDateTimeStr(dt.Rows[i]["InDate"])));
                 c1[r, 12].PutValue(R.Text(dt.Rows[i]["InPallet"]));
                 c1[r, 13].PutValue(R.Text(dt.Rows[i]["InQty"]));
                 c1[r, 14].PutValue(R.Text(Helper.Safe.SafeDateTimeStr(dt.Rows[i]["OutDate"])));
                 c1[r, 15].PutValue(R.Text(dt.Rows[i]["OutPallet"]));
                 c1[r, 16].PutValue(R.Text(dt.Rows[i]["OutQty"]));
                 c1[r, 17].PutValue(R.Text(dt.Rows[i]["PoNo"]));
                 c1[r, 18].PutValue(R.Text(handQty));
                 c1[r, 19].PutValue(R.Text(dt.Rows[i]["Remark"]));
             }
             else
             {
                 handQty = handQty - outQty;
                 c1[r, 0].PutValue(R.Text(""));
                 c1[r, 1].PutValue(R.Text(""));
                 c1[r, 2].PutValue(R.Text(""));
                 c1[r, 3].PutValue(R.Text(""));
                 c1[r, 4].PutValue(R.Text(""));
                 c1[r, 5].PutValue(R.Text(""));
                 c1[r, 6].PutValue(R.Text(""));
                 c1[r, 7].PutValue(R.Text(""));
                 c1[r, 8].PutValue(R.Text(""));
                 c1[r, 9].PutValue(R.Text(""));
                 c1[r, 10].PutValue(R.Text(""));
                 c1[r, 11].PutValue(R.Text(""));
                 c1[r, 12].PutValue(R.Text(""));
                 c1[r, 13].PutValue(R.Text(""));
                 c1[r, 14].PutValue(R.Text(Helper.Safe.SafeDateTimeStr(dt.Rows[i]["OutDate"])));
                 c1[r, 15].PutValue(R.Text(dt.Rows[i]["OutPallet"]));
                 c1[r, 16].PutValue(R.Text(dt.Rows[i]["OutQty"]));
                 c1[r, 17].PutValue(R.Text(""));
                 c1[r, 18].PutValue(R.Text(handQty));
                 c1[r, 19].PutValue(R.Text(""));
             }
            dr = dt.Rows[i];

            for (int j = 0; j <= 19; j++)
            {
                c1[r, j].SetStyle(style1);
            }
            cnt = i + 1;
            r++;
            n = no;
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