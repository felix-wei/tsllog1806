using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportWarehouse_Report_StockValuation : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Date.Date = DateTime.Today;
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
        //        string sql = string.Format(@"select * from (select Product,LotNo,max(p.HsCode) HsCode,max(p.ProductClass) ProductClass,max(mast.PartyName) as PartyName,max(mast.PartyId) as PartyId
        //,sum(Case when det.DoType='In' then Qty1 else -Qty1 end) as Qty1,max(mast.DoDate) as DoDate
        //,sum(Case when det.DoType='In' then Qty2 else -Qty2 end) as Qty2,sum(case when det.DoType='In' then  det.QtyPackWhole else 0 end) as pkg
        //,sum(Case when det.DoType='In' then Qty3 else -Qty3 end) as Qty3,sum(case when det.DoType='In' then  det.QtyWholeLoose else 0 end) as unit
        //,max(p.Description) as des1,max(det.Att1) as Att1,max(det.att2) as Att2,max(det.att3) as Att3,max(det.att4) as Att4,max(det.att5) as Att5,max(det.att6) as Att6
        //,max(Packing) as Packing from wh_dodet2 det inner join wh_do mast on mast.DoNO=det.DoNo and mast.DoType=det.DoType
        //left join ref_product p on det.Product=p.Code 
        //group by Product,LotNo ) as tab where Qty1+Qty2+Qty3>0");
        string sql = string.Format(@"select * from (select tab_in.Product,tab_in.LotNo,tab_in.HsCode,tab_in.ProductClass,tab_in.PartyId,tab_in.PartyName,tab_in.DoDate
,tab_hand.Qty1-isnull(tab_out.Qty1,0) as Qty1
,tab_hand.Qty2-isnull(tab_out.Qty2,0) as Qty2
,tab_hand.Qty3-isnull(tab_out.Qty3,0) as Qty3
,tab_in.Pkg,tab_in.Unit
,tab_in.Des1,tab_in.Att1,tab_in.Att2,tab_in.Att3,tab_in.Att4,tab_in.Att5,tab_in.Att6,isnull(tab_in.Price,0) as Price,tab_in.WareHouseId,(tab_hand.Qty1-isnull(tab_out.Qty1,0))*(isnull(tab_in.Price,0)) as TotalPrice from 
(select det.Product,det.LotNo,p.HsCode,p.ProductClass,mast.PartyId,mast.PartyName,mast.DoDate,det.QtyPackWhole as Pkg,det.QtyWholeLoose as Unit,mast.WareHouseId,
det.Des1,det.att1,det.att2,det.att3,det.att4,det.att5,det.att6,det.Price from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode!='CNL' 
left join ref_product p on p.code=det.Product
where det.Dotype='IN' and len(det.doNo)>0 ) as tab_in 
left join (select product ,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product,LotNo) as tab_hand on  tab_hand.Product=tab_in.Product and tab_hand.LotNo=tab_in.LotNo
left join (select Product,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3
 from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN')
where det.DoType='Out' and len(det.DoNo)>0 group by Product,LotNo) as tab_out on tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo
) as tab where  Qty1+Qty2+Qty3>0 
");
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

        if (this.txt_CustId.Text.Length > 0)
        {
            sql += string.Format("and PartyId='{0}'", this.txt_CustId.Text);
        }
        if (this.cmb_WareHouse.Text.Length > 0)
        {
            sql += string.Format("and WareHouseId like '%{0}%'", this.cmb_WareHouse.Text);
        }
        if (dateTo.Length > 0)
        {
            sql += string.Format(" and DoDate < '{0}'", dateTo);
        }
        // sql += " group by det.ProductCode,sku.ProductClass,sku.HsCode,det.LotNo,mast.PartyName,det.qty3,det.DoType,det2.Packing order by det.ProductCode";
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("StockValuation");
    }
}