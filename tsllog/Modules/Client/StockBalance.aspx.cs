using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Client_StockBalance : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddMonths(-1);
            this.txt_end.Date = DateTime.Today.AddDays(1);
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
        string name = HttpContext.Current.User.Identity.Name;
        string sql = string.Format(@"select * from (select tab_in.No,tab_in.DoNo,tab_in.Product,tab_in.LotNo,tab_in.Id,
tab_in.PartyId,tab_in.PartyName,tab_in.DoDate,tab_in.PoNo,tab_in.Remark,tab_in.GrossWeight,tab_in.NettWeight
,isnull(tab_on.Qty1,0) as InQty,tab_in.PermitNo,tab_in.PartyInvNo,tab_in.PalletNo,
isnull(tab_in.Qty2,0) as BadQty,isnull(tab_in.Qty3,0) as InPallet,isnull(tab_hand.Qty1,0) as HandQty
,tab_in.DoDate as InDate,tab_in.Des1,tab_in.Att3,tab_in.WareHouseId,tab_in.Location,isnull(In_Pending.Qty1,0) as InPending,isnull(Out_Pending.Qty1,0) as OutPending from 
(select distinct row_number() over (order by det.Id) as No, max(det.Id) as Id, max(mast.DoNo) as DoNo,max(det.Product) as Product,max(Qty1) as Qty1,max(Qty2) as Qty2,max(Qty3) as Qty3,max(det.LotNo) as LotNo,
max(p.ProductClass) as ProductClass,max(mast.PartyId) as PartyId,max(mast.PartyName) as PartyName,max(mast.DoDate) as DoDate,max(det.QtyPackWhole) as Pkg,max(det.QtyWholeLoose) as Unit,max(mast.WareHouseId) as WareHouseId,max(mast.PoNo) as PoNo,max(det.Remark) as Remark,
max(det.GrossWeight) as GrossWeight,max(det.NettWeight) as NettWeight,max(det.PalletNo) as PalletNo,max(det.Location) as Location,max(mast.PermitNo) as PermitNo,max(mast.PartyInvNo) as PartyInvNo,
max(det.Des1) as Des1,max(det.att3) as Att3 from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType 
left join ref_product p on p.code=det.Product
where det.Dotype='IN' group by det.Id,Product,LotNo) as tab_in 
left join (select product,LotNo, sum(isnull(Case when det.DoType='IN' then Qty1 else -Qty1 end,0)) as Qty1  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  
  group by Product,LotNo) as tab_hand on tab_hand.product=tab_in.Product and tab_hand.LotNo=tab_in.LotNo 
left join (select product,LotNo,sum(Qty1) as Qty1  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS' and det.DoType='IN'
 group by Product,LotNo) as tab_on on tab_on.product=tab_in.Product and tab_on.LotNo=tab_in.LotNo 
left join (select Product,LotNo,sum(Qty1) as Qty1,max(Qty2) as Qty2,max(Qty3) as Qty3,max(mast.DoDate) as OutDate,det.PalletNo,det.Remark,Des1,max(det.RelaId) as RelaId
 from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN')
where det.DoType='OUT' and len(det.DoNo)>0 group by det.DoNo,Product,LotNo,Des1,PalletNo,det.Remark) as tab_out on isnull(tab_out.RelaId,'')=isnull(tab_in.Id,'') 
left join (select max(det.Id) as Id,Product,LotNo,max(Qty1) as Qty1 from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='USE' or mast.StatusCode='RETURN')
where det.DoType='IN' and len(det.DoNo)>0 group by det.Id,Product,LotNo,Des1,PalletNo,det.Remark) as In_Pending on isnull(In_Pending.Id,'')=isnull(tab_in.Id,'') 
left join (select max(det.Id) as Id,Product,LotNo,max(Qty1) as Qty1,max(det.RelaId) as RelaId from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='USE' or mast.StatusCode='RETURN')
where det.DoType='OUT' and len(det.DoNo)>0 group by det.Id,Product,LotNo) as Out_Pending on isnull(Out_Pending.RelaId,'')=isnull(tab_in.Id,'') 
) as tab where 1=1");
        string sql_n = string.Format(@"select CustId from  [dbo].[User] where Name='{0}'", name);
        string partyId = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql_n));
        string dateFrom = "";
        string dateTo = "";
        if (partyId.Length > 0)
        {
            sql += string.Format("and PartyId='{0}'", partyId);
            if (txt_from.Value != null&&txt_end.Value!=null)
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
            if (this.cmb_WareHouse.Text.Length > 0)
            {
                sql += string.Format("and WareHouseId like '%{0}%'", this.cmb_WareHouse.Text);
            }
            if (dateTo.Length > 0)
            {
                sql += string.Format(" and (DoDate between '{0}' and '{1}')", dateFrom, dateTo);
            }
            // sql += " group by det.ProductCode,sku.ProductClass,sku.HsCode,det.LotNo,mast.PartyName,det.qty3,det.DoType,det2.Packing order by det.ProductCode";
            this.grid.DataSource = ConnectSql.GetTab(sql);
            this.grid.DataBind();
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("StockBalance");
    }
}