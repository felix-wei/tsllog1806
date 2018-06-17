using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Report_IncomingStock : System.Web.UI.Page
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
        string sql = string.Format(@"select mast.DoDate,det.ProductCode as Sku,sku.ProductClass,sku.HsCode,det.LotNo,mast.PermitNo,mast.PartyName,det.Qty4 as Expected,det.Qty5 as Intransit,
det.QtyPackWhole as pkg,det.QtyWholeLoose as unit,det.Des1,
det.Att1,det.Att2,det.Att3,det.Att4,det.Att5,det.Att6,det.Packing
 from wh_doDet det
left outer join wh_do mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType
left join ref_product sku on det.ProductCode=sku.code where det.DoType='IN'
and mast.DoDate between '{0}' and '{1}' ", this.txt_from.Date.ToString("yyyy-MM-dd"), this.txt_end.Date.ToString("yyyy-MM-dd"));
        if (this.txt_SKULine_Product.Text.Length > 0)
            sql += string.Format(" and Det.ProductCode='{0}'", this.txt_SKULine_Product.Text.Replace("'",""));
        sql = "select * from (" + sql + " ) as tab where Intransit>0 order by Sku,DoDate ";
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("IncomingStock");
    }
}