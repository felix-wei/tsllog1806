using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_SelectPoductFromDoIn : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            btn_Sch_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            string wh=SafeValue.SafeString(Request.QueryString["wh"]);
            cmb_WareHouse.Text = wh;
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string lotNo = this.txt_LotNo.Text.Trim().ToUpper();
        string wh = SafeValue.SafeString(Request.QueryString["wh"]);
        string reLotNo = SafeValue.SafeString(Request.QueryString["lotNo"]);
        string dateFrom = "";
        string dateTo = "";
        string where = "";
        string sql = @"select DISTINCT Code,ProductClass,LotNo,BrandName,Description,UomPacking,UomWhole,UomLoose,Att1 as Packing,Att4,Att5,Att6,Att7,Att8,Att9,tab_hand.HandQty,tab_hand.HandQty-isnull(tab_Reserved.ReservedQty,0) as AvaibleQty,tab_in.WareHouseId from ref_product as p
left join (select product,sum(isnull(Case when det.DoType='In' then Qty1 else -Qty1 end,0)) as HandQty from wh_dodet2 det inner join  wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode='CLS' group by product) as tab_hand on p.Code=tab_hand.Product
left join (select productCode as Product,sum(Qty5) as ReservedQty from wh_doDet det inner join  wh_do mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' group by productCode) as tab_Reserved on tab_Reserved.product=tab_hand.product
left join (select mast.WareHouseId,Product,LotNo from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode!='CNL' 
left join ref_product p on p.code=det.Product where det.Dotype='IN' and len(det.doNo)>0 ) as tab_in on tab_in.Product=p.Code
";
        if (name.Length > 0)
        {
            where =GetWhere(where,string.Format(" Code like '%{0}%'", name.Replace("'", "")));
        }
        if(cmb_WareHouse.Text.Trim()!=""){
            where =GetWhere(where, string.Format(" WareHouseId='{0}'", cmb_WareHouse.Text));
        }
        if (lotNo.Length > 0)
        {
            where =GetWhere(where, string.Format(" LotNo like '%{0}%'", lotNo));
        }
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where =GetWhere(where, string.Format(" DoDate >= '{0}' and DoDate < '{1}'", dateFrom, dateTo));
        }
        if (reLotNo.Length>0)
        {
            where = GetWhere(where, string.Format(" LotNo='{0}'", reLotNo));
        }
        if(where.Length>0){
            sql +=" where"+ where;
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}