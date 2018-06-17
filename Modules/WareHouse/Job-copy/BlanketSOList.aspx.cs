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

public partial class WareHouse_Job_BlanketSOList : System.Web.UI.Page
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
        string sql = string.Format(@"select DISTINCT d.DoNo,DoStatus,dbo.fun_GetPartyName(d.PartyId) as PartyName,
TotalQty,d.Currency,d.ExRate,d.PayTerm,d.IncoTerm,d.WareHouseId,d.DoDate,
(select SUM(DocAmt) from Wh_TransDet det where det.DoNo=d.DoNo GROUP BY DET.DoNo) as TotalAmt,
isnull((select sum(det1.Qty5) from Wh_DoDet det1 inner join Wh_DO do on do.PoNo=d.DoNo and do.DoNo=det1.DoNo where det1.DoType='OUT'),0) as PickQty,
isnull((select sum(det1.Qty1) from Wh_DoDet det1 inner join Wh_DO do on do.PoNo=d.DoNo and do.DoNo=det1.DoNo where det1.DoType='OUT'),0) as ShipQty,
isnull((select cast(sum(inv.Qty) as numeric(10,0)) from XAArInvoiceDet inv inner join Wh_TransDet det on REPLACE(inv.ChgCode,'(SKU)','')=det.ProductCode and det.DoType='BS' and inv.MastRefNo=det.DoNo where inv.DocType='IV' and inv.MastRefNo=d.DoNo),0) as BillQty
,TotalQty-ISNULL(SoQty,0) as OutstandingQty
from Wh_Trans d left join Wh_TransDet det on d.DoNo=det.DoNo
left join (select SUM(Qty1) as TotalQty,DoNo from Wh_TransDet where DoType='BS' group by DoNo) as tab_total on tab_total.DoNo=d.DoNo
left join (select sum(Qty1) as SoQty,mast.BlanketNo from wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and  mast.DoType='SO' and mast.DoStatus='Confirmed' group by mast.BlanketNo) as tab_blanket on tab_blanket.BlanketNo=d.DoNo");
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
        if (cmb_Type.Text.Trim() != "")
        {
            where = GetWhere(where, " DoStatus = '" + cmb_Type.Text + "'");
        }
        if (where.Length > 0)
        {
            sql += " where " + where + " and d.DoType='BS' order by d.DoNo";
        }
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