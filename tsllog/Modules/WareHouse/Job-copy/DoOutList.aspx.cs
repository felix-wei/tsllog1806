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
public partial class WareHouse_Job_DoOutList : System.Web.UI.Page
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
        string sql = "select *,dbo.fun_GetPartyName(PartyId) as PartyName from Wh_DO d";
        string dateFrom = "";
        string dateTo = "";
        string dateRef = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (date_RefDate.Value != null)
        {
            dateRef = date_RefDate.Date.ToString("yyyy-MM-dd");
        }
        if (txt_PoNo.Text.Trim() != "")
            where = GetWhere(where, "d.DoNo='" + txt_PoNo.Text.Trim() + "'");
        else if (txt_PermitNo.Text.Trim() != "")
        {
            where = GetWhere(where, "PermitNo='" + txt_PermitNo.Text.Trim() + "'");
        }
        else if (txt_LotNo.Text.Trim() != "")
        {
            where = GetWhere(where, "det.LotNo='" + txt_LotNo.Text.Trim() + "'");
        }
        else if (txt_SKUCode.Text.Trim() != "")
        {
            where = GetWhere(where, "det.ProductCode='" + txt_SKUCode.Text.Trim() + "'");
        }
        else if (this.txt_CustId.Text.Length > 0)
        {
            where = GetWhere(where, "PartyId='" + this.txt_CustId.Text.Trim() + "'");
        }
        else if (this.txt_CustomerRef.Text.Length > 0)
        {
            where = GetWhere(where, "CustomerReference='" + this.txt_CustomerRef.Text.Trim() + "'");
        }
        else if (dateRef.Length > 0)
        {
            where = GetWhere(where, "CustomerDate='" + dateRef + "'");
        }
        else if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " DoDate >= '" + dateFrom + "' and DoDate < '" + dateTo + "'");
        }

        if (where.Length > 0)
        {
            sql += " where " + where + " and d.DoType='OUT' order by d.DoNo";
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