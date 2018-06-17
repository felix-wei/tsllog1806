using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_SkuInventory : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            Bind();
            string code = SafeValue.SafeString(Request.QueryString["Sku"].ToString());
            string sql = string.Format(@"select Description from ref_product where Code='{0}'", code);
            string name = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
            lbl_Code.Text = code;
            lbl_ProductName.Text = name;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    protected void Bind()
    {
        string code = SafeValue.SafeString(Request.QueryString["Sku"].ToString());
        string sql = string.Format(@"select distinct d1.Product as ProductCode,ref.Description,ISNULL(tab_hand.Qty1-isnull(tab_out.Qty1,0),0) as HandQty,tab_Incoming.Qty as IncomingQty,d1.LotNo,d1.Location,
ISNULL((select sum(det.Qty1) from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where det.ProductCode=ref.Code and mast.DoType='SO' group by ProductCode),0) as WipQty, 
ISNULL((select sum(det.Qty1) from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where det.ProductCode=ref.Code and mast.DoType='PO' group by ProductCode),0) as WipQtyIn, 
(select sum(wd.Qty1) from Wh_TransDet wd, Wh_Trans wt where wt.DoType='PO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo and wd.ProductCode=d1.Product) as PoQty,
(select sum(wd.Qty1) from Wh_TransDet wd, Wh_Trans wt where wt.DoType='SO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo and wd.ProductCode=d1.Product) as SoQty,
mast.WareHouseId,mast.PoNo,mast.DoNo,mast.DoDate
from Wh_DoDet2 as d1 inner join Wh_DO mast on d1.DoNo=mast.DoNo and d1.DoType=mast.DoType left join ref_product ref on ref.Code=d1.Product
inner join (select product,LotNo,Location,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product,LotNo,Location) as tab_hand on  tab_hand.Product=d1.Product and tab_hand.LotNo=d1.LotNo and tab_hand.Location=d1.Location
left join (select Product,LotNo,Location,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Product,LotNo,Location) as tab_out on tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo and tab_out.Location=tab_hand.Location
inner join (select ProductCode,sum(Qty5) as Qty from Wh_DoDet where DoType='IN' group by ProductCode) as tab_Incoming on tab_Incoming.ProductCode=d1.Product 
where d1.Product='{0}' and d1.DoType='IN' and mast.StatusCode='CLS'", code);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
}