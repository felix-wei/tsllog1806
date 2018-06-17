using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_SelectBlanketOrder : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.txt_from.Date = DateTime.Today.AddDays(-30);
            //this.txt_end.Date = DateTime.Today;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        string sql = string.Format(@"select tab_mast.DoNo,tab_mast.DoDate,TotalQty,TotalQty-isnull(tab_a.Qty,0) as RemainingQty,PartyName from Wh_Trans tab_mast 
inner join (select SUM(Qty1) as TotalQty,DoNo from Wh_TransDet group by  DoNo) tab_det on tab_det.DoNo=tab_mast.DoNo
left join (select BlanketNo,SUM(Qty1) as Qty,MAX(mast.DoNo) as DoNo from Wh_Trans mast inner join Wh_TransDet det on mast.DoNo=det.DoNo group by BlanketNo) as tab_a on tab_a.BlanketNo=tab_mast.DoNo
where tab_mast.DoType='{0}'", type);
        DataTable tab = ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {


    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_Active_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = string.Format(@"select mast.DoNo,det.Qty1,det.Price,det.ProductCode,det.Des1,Uom1,LocationCode,LotNo,DocAmt from Wh_Trans mast inner join Wh_TransDet det on det.DoNo=mast.DoNo
where BlanketNo='" + SafeValue.SafeString(grd.GetMasterRowKeyValue()) + "'");
        DataTable tab = ConnectSql.GetTab(sql);
        grd.DataSource = tab;
    }
}