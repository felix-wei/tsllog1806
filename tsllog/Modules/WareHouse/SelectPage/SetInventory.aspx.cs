using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
public partial class WareHouse_SelectPage_SetInventory : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            Bind();
            string code = SafeValue.SafeString(Request.QueryString["Loc"].ToString());
            lbl_Code.Text = code;
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
        string code = SafeValue.SafeString(Request.QueryString["Loc"].ToString());
        string sql = string.Format(@"select * from (select det.Product,det.LotNo,det.Des1,tab_in.DoNo,tab_in.DoDate,det.Att1,det.Att2,tab_hand.Qty1-isnull(tab_out.Qty1,0) as HandQty,det.ToLocId as Location,det.Qty1 from wh_TransferDet det
left join(select det.Id,det.Product,det.LotNo,det.Location,p.Description,p.Att1 as Packing,WarehouseId,mast.DoNo,mast.DoDate from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode!='CNL' 
left join ref_product p on p.code=det.Product where det.Dotype='IN' and len(det.doNo)>0 ) as tab_in on det.Product=tab_in.Product and det.LotNo=tab_in.LotNo
left join (select product ,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product,LotNo) as tab_hand on  tab_hand.Product=tab_in.Product and tab_hand.LotNo=tab_in.LotNo
left join (select Product,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3,MAX(Location) as Loction from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Product,LotNo) as tab_out on tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo
inner join (select top (1) mast.TransferNo from wh_Transfer mast inner join wh_TransferDet det on mast.TransferNo=det.TransferNo group by mast.TransferNo,TransferDate order by TransferDate desc) as tab_Trans on tab_Trans.TransferNo=det.TransferNo) as tab where Location='{0}'", code);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
}