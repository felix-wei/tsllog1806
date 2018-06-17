using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Report_StockMove : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			this.txt_from.Date = DateTime.Today.AddMonths(-6);
			this.txt_end.Date = DateTime.Today.AddDays(1);
		}
	}

	protected void btn_Sch_Click(object sender, EventArgs e)
	{
		string sql = string.Format(@"select det.Product as Sku,sku.ProductClass,sku.HsCode,mast.PermitNo,det.LotNo,'' as PartyName,det.DoType,Convert(nvarchar(10),mast.DoDate,103) as DoDate,mast.WareHouseId
,det.Qty1,det.Qty2,det.Qty3,QtyPackWhole as pkg,det.QtyWholeLoose as unit
,det.qty1*(case when sku.QtyPackingWhole=0 then 1 else sku.QtyPackingWhole end )* (case when sku.qtyWholeLoose=0 then 1 else sku.qtyWholeLoose end)
+det.qty2* (case when sku.qtyWholeLoose=0 then 1 else sku.qtyWholeLoose end)
+det.qty3 as TotalQty,det.Att1,det.Att2,det.Att3,det.Att4,det.Att5,det.Att6
,det.Packing
 from wh_doDet2 det left outer join wh_do mast on mast.DoNO=det.DoNo and mast.DoType=det.DoType
left join ref_product sku on det.Product=sku.code
where mast.DoDate between '{0}' and '{1}'", this.txt_from.Date.ToString("yyyy-MM-dd"), this.txt_end.Date.ToString("yyyy-MM-dd"));
		if (this.txt_SKULine_Product.Text.Length > 0)
			sql += string.Format(" and det.Product='{0}'", this.txt_SKULine_Product.Text);
		if (this.txt_LotNo.Text.Length > 0)
		{
			string[] arr = this.txt_LotNo.Text.Trim().Split(',');
			for (int i = 0; i < arr.Length; i++)
			{
				if (i == 0)
					sql += string.Format(" and (Det.LotNo='{0}'", arr[i]);
				else
					sql += string.Format(" or  Det.LotNo='{0}'", arr[i]);
				if (i == arr.Length - 1)
					sql += ")";
			}

			//sql += string.Format("and LotNo='{0}'", this.txt_LotNo.Text);
		}

		if (txt_WareHouse.Text.Length > 0)
		{
			sql += string.Format("and mast.WareHouseId like '%{0}%'", this.txt_WareHouse.Text);
		}

		sql += " order by det.Product,DoDate,det.LotNo";
		//throw new Exception(sql);
        this.grid.DataSource = D.List(sql);
        this.grid.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("StockMove");
    }
}