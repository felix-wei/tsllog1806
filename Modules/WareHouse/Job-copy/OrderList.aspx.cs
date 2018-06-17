using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class WareHouse_Job_OrderList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-30);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            btn_search_Click(null, null);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string userId=HttpContext.Current.User.Identity.Name;
        string sql = string.Format(@"select *,dbo.fun_GetPartyName(d.PartyId) as PartyName,
(select SUM(Qty1) from Wh_TransDet det where det.DoNo=d.DoNo) as TotalQty, 
(select SUM(DocAmt) from Wh_TransDet det where det.DoNo=d.DoNo) as TotalAmt,
isnull((select sum(det1.Qty5) from Wh_DoDet det1 inner join Wh_DO do on do.PoNo=d.DoNo and do.DoNo=det1.DoNo where det1.DoType='OUT'),0) as PickQty,
isnull((select sum(det1.Qty1) from Wh_DoDet det1 inner join Wh_DO do on do.PoNo=d.DoNo and do.DoNo=det1.DoNo where det1.DoType='OUT'),0) as ShipQty,
isnull((select cast(sum(inv.Qty) as numeric(10,0)) from XAArInvoiceDet inv inner join Wh_TransDet det on REPLACE(inv.ChgCode,'(SKU)','')=det.ProductCode and det.DoType='SO' and inv.MastRefNo=det.DoNo where inv.DocType='IV' and inv.MastRefNo=d.DoNo),0) as BillQty
from Wh_Trans d");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (txt_PoNo.Text.Trim() != "")
            where = GetWhere(where, "d.DoNo='" + txt_PoNo.Text.Trim() + "'");
        else if (this.txt_CustId.Text.Length > 0)
        {
            where = GetWhere(where, "PartyId='" + this.txt_CustId.Text.Trim() + "'");
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " DoDate >= '" + dateFrom + "' and DoDate < '" + dateTo + "'");
        }
        //if (cmb_Type.Text.Trim() != "")
        //{
        //    where = GetWhere(where, " DoStatus = '" + cmb_Type.Text + "'");
        //}
        if (userId == "ADMIN")
        {
            where = GetWhere(where, " d.SalesId<>'" + userId + "'");
        }
        if(userId!="ADMIN")
        {
            where = GetWhere(where, " d.SalesId='" + userId + "'");
        }
        if (where.Length > 0)
        {
            sql += " where " + where + " and d.DoType='SO'  order by d.DoNo";
        }
        //throw new Exception(sql);
        DataTable tab = ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();
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