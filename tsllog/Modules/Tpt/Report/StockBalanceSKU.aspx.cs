using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_WareHouse_Report_StockBalanceSKU : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_Sch_Click(null,null);
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
        string sql = string.Format(@"select * from (select distinct job.ClientId,(select Name from XXParty where PartyId=job.ClientId) as PartyName,CargoStatus,CargoType,
ActualItem,p.Description,tab_in.WareHouseCode,isnull(tab_in.Remark1,'') as Remark1,isnull(tab_in.BalQty-tab_out.BalQty,0) BalQty
from job_house mast inner join ctm_job job on mast.JobNo=job.JobNo inner join ref_product p on mast.ActualItem=p.Code
inner join (select sum(case when CargoType='IN' and CargoStatus='C' then QtyOrig else 0 end) as BalQty,p.PartNo,WareHouseCode,max(Remark1) Remark1 from job_house h inner join ctm_job job on h.JobNo=job.JobNo left join ref_product p on h.ActualItem=p.Code group by p.PartNo,WareHouseCode) as tab_in on tab_in.PartNo=p.PartNo and tab_in.WareHouseCode=job.WareHouseCode
inner join (select sum(case when CargoType='OUT' and CargoStatus='C' then QtyOrig else 0 end) as BalQty,p.PartNo,WareHouseCode from job_house h inner join ctm_job job on h.JobNo=job.JobNo left join ref_product p on h.ActualItem=p.Code group by p.PartNo,WareHouseCode) as tab_out on tab_out.PartNo=p.PartNo and tab_out.WareHouseCode=job.WareHouseCode) as t 
 where CargoStatus='C' and CargoType='IN'");
        string where = "";
        string dateTo = "";
        if (this.txt_SKULine_Product.Text.Length > 0)
            where = GetWhere(where, string.Format(" ActualItem like '%{0}%'", this.txt_SKULine_Product.Text.Trim()));
        if (this.txt_CustId.Text.Length > 0)
        {
            where = GetWhere(where, string.Format(" ClientId='{0}'", this.txt_CustId.Text.Trim()));
        }
        if (this.cmb_WareHouse.Text.Length > 0)
        {
            where = GetWhere(where, string.Format(" WareHouseCode='{0}'", this.cmb_WareHouse.Text.Trim()));
        }
        if (where.Length > 0)
        {
            sql += " and" + where;
        }
        // sql += " group by det.ProductCode,sku.ProductClass,sku.HsCode,det.LotNo,mast.PartyName,det.qty3,det.DoType,det2.Packing order by det.ProductCode";
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("StockBalance_SKU");
    }
    private static string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}