using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Report_StockBalance : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Date.Date = DateTime.Today.AddDays(1);
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
        string sql = string.Format(@"select * from (select tab_in.No,tab_in.DoNo,tab_in.Product,tab_in.LotNo,tab_in.Id,
tab_in.PartyId,tab_in.PartyName,tab_in.DoDate,tab_in.PoNo,tab_in.Remark,tab_in.GrossWeight,tab_in.NettWeight
,tab_in.Qty1 as InQty,tab_in.PermitNo,tab_in.PartyInvNo,tab_in.PalletNo,
isnull(tab_in.Qty2,0) as BadQty,isnull(tab_in.Qty3,0) as InPallet,tab_hand.Qty1 as HandQty
,tab_in.DoDate as InDate,tab_in.Des1,tab_in.Att3,tab_in.WareHouseId,tab_in.Location from 
(select distinct row_number() over (order by det.Id) as No, max(det.Id) as Id, max(mast.DoNo) as DoNo,max(det.Product) as Product,max(Qty1) as Qty1,max(Qty2) as Qty2,max(Qty3) as Qty3,max(det.LotNo) as LotNo,max(p.HsCode) as HsCode,
max(p.ProductClass) as ProductClass,max(mast.PartyId) as PartyId,max(mast.PartyName) as PartyName,max(mast.DoDate) as DoDate,max(det.QtyPackWhole) as Pkg,max(det.QtyWholeLoose) as Unit,max(mast.WareHouseId) as WareHouseId,max(mast.PoNo) as PoNo,max(det.Remark) as Remark,
max(det.GrossWeight) as GrossWeight,max(det.NettWeight) as NettWeight,max(det.PalletNo) as PalletNo,max(det.Location) as Location,max(mast.PermitNo) as PermitNo,max(mast.PartyInvNo) as PartyInvNo,
max(det.Des1) as Des1,max(det.att1) as Att1,max(det.att2) as Att2,max(det.att3) as Att3,max(det.att4) as Att4,max(det.att5) as Att5,max(det.att6) as Att6,max(det.packing) as Packing from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode='CLS' 
left join ref_product p on p.code=det.Product
where det.Dotype='IN' and len(det.doNo)>0 group by det.Id,Product,LotNo,Des1,PalletNo,det.Remark) as tab_in 
inner join (select product,LotNo, sum(isnull(Case when det.DoType='IN' then Qty1 else -Qty1 end,0)) as Qty1  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  
  group by Product,LotNo) as tab_hand on tab_hand.product=tab_in.Product and tab_hand.LotNo=tab_in.LotNo 
) as tab where 1=1");
        
        string dateTo = "";
        if (txt_Date.Value != null)
        {
            dateTo = txt_Date.Date.ToString("yyyy-MM-dd");
        }
        if (this.txt_SKULine_Product.Text.Length > 0)
            sql += string.Format("and Product='{0}'", this.txt_SKULine_Product.Text);
        if (this.txt_LotNo.Text.Length > 0)
        {
            string[] arr = this.txt_LotNo.Text.Trim().Split(',');
            for(int i=0;i<arr.Length;i++)
            {
                if(i==0)
                    sql += string.Format(" and (LotNo='{0}'", arr[i]);
                else
                    sql += string.Format(" or LotNo='{0}'", arr[i]);
                if (i == arr.Length - 1)
                    sql += ")";

            }
            //sql += string.Format("and LotNo='{0}'", this.txt_LotNo.Text);
        }

        if(this.txt_CustId.Text.Length>0){
            sql += string.Format("and PartyId='{0}'", this.txt_CustId.Text);
        }
        if (this.cmb_WareHouse.Text.Length > 0)
        {
            sql += string.Format("and WareHouseId like '%{0}%'", this.cmb_WareHouse.Text);
        }
        if (dateTo.Length > 0)
        {
            sql+=string.Format(" and DoDate < '{0}'",dateTo);
        }
       // sql += " group by det.ProductCode,sku.ProductClass,sku.HsCode,det.LotNo,mast.PartyName,det.qty3,det.DoType,det2.Packing order by det.ProductCode";
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("StockBalance");
    }


}