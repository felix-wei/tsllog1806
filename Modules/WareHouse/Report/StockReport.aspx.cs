using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Report_Stock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddMonths(-6);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select det.Id,mast.DoNo,mast.DoType,Convert(nvarchar(10),mast.DoDate,103) as DoDate,det.Product as Sku,det.LotNo,
det.Qty1,det.Qty2,det.Qty3,det.Packing as Packing
,det.qty1*(case when sku.QtyPackingWhole=0 then 1 else sku.QtyPackingWhole end )* (case when sku.qtyWholeLoose=0 then 1 else sku.qtyWholeLoose end)+det.qty2* (case when sku.qtyWholeLoose=0 then 1 else sku.qtyWholeLoose end)+det.Qty3 as TotQty
 from wh_doDet2 det
inner join wh_do mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType
left join ref_product sku on det.Product=sku.code 
where mast.DoDate between '{0}' and '{1}'", this.txt_from.Date.ToString("yyyy-MM-dd"), this.txt_end.Date.ToString("yyyy-MM-dd"));
        if (this.txt_SKULine_Product.Text.Length > 0)
            sql += string.Format(" and Det.Product='{0}'", this.txt_SKULine_Product.Text);
        sql += " order by det.Product,mast.DoDate,mast.DoType,mast.DoNo";
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("StockReport");
    }
}