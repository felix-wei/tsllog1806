using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportWarehouse_Report_ExpiryStockReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-30);
            this.txt_To.Date = DateTime.Today;
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select distinct Convert(nvarchar(10),mast.ExpiredDate,103) as ExpiredDate,sku.Code as Sku,det.LotNo,
sku.Att1 as Packing,sku.Description,sku.ProductClass,sku.HsCode,tab_hand.Qty1-ISNULL(tab_out.Qty1,0) as PQty
,tab_hand.Qty2-ISNULL(tab_out.Qty2,0) as WQty,tab_hand.Qty3-ISNULL(tab_out.Qty3,0) as LQty,
sku.Att4,sku.Att5,sku.Att6,sku.Att7,sku.Att8,sku.Att9,sku.QtyPackingWhole as pkg,sku.QtyWholeLoose as unit,sku.QtyLooseBase as stk
from wh_doDet2 det
inner join Wh_DoDet mast on det.DoNo=mast.DoNo and det.DoType='IN'
inner join ref_product sku on det.Product=sku.code 
inner join (select product ,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product,LotNo) as tab_hand on  tab_hand.Product=det.Product and tab_hand.LotNo=det.LotNo
left join (select Product,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from  wh_dodet2 det inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Product,LotNo) as tab_out on tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo where mast.ExpiredDate between '{0}' and '{1}'", this.txt_from.Date,txt_To.Date);
        if (this.txt_SKULine_Product.Text.Length > 0)
            sql += string.Format(" and sku.Code='{0}'", this.txt_SKULine_Product.Text);
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("ExpiryReport");
    }
}