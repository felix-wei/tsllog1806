using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Report_MoveRpt : System.Web.UI.Page
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
        string inOutInd = SafeValue.SafeString(Request.QueryString["typ"], "OUT");
        string sql = string.Format(@"select * from (select det.ProductCode as Sku,max(sku.Description) as Des,max(sku.HsCode) as HsCode,sku.ProductClass,sum(det.Qty1) as Qty,
max(sku.att1) as Packing,sum(det.QtyPackWhole) as pkg,sum(det.QtyWholeLoose) as unit,max(det.Att1) as Att1,
max(det.Att2) as Att2,max(det.Att3) Att3,max(det.Att4) Att4,max(det.Att5) Att5,max(det.Att6) Att6,
sum(det.qty1*(case when sku.QtyPackingWhole<0 then 1 else sku.QtyPackingWhole end )* 
(case when sku.qtyWholeLoose=0 then 1 else sku.qtyWholeLoose end)+det.qty2*
(case when sku.qtyWholeLoose=0 then 1 else sku.qtyWholeLoose end)+det.Qty3) as TotQty from wh_doDet det
inner join wh_do mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType
left join ref_product sku on det.ProductCode=sku.code
where mast.DoType='{2}' and mast.DoDate between '{0}' and '{1}'
group by det.ProductCode,sku.ProductClass) as aa ", this.txt_from.Date.ToString("yyyy-MM-dd"), this.txt_end.Date.ToString("yyyy-MM-dd"), inOutInd);
        
            string sortBy = SafeValue.SafeString(Request.QueryString["by"], "0");
            if (sortBy == "1")
                sql += " order by TotQty";
            else
                sql += " order by TotQty Desc";
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("MovingReport");
    }
}