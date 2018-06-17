using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PagesMaster_LCList : System.Web.UI.Page
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
        string type =cmb_Status.Text.Trim();
        string where = "";
        string sql = string.Format(@"select * from Ref_LC");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (txt_LcNo.Text.Trim() != "")
            where = GetWhere(where, "LcNo='" + txt_LcNo.Text.Trim() + "'");
        if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " LcAppDate >= '" + dateFrom + "' and LcAppDate < '" + dateTo + "'");
        }
        if (type.Length > 0)
        {
            where = GetWhere(where, "StatusCode='"+type+"'");
        }
        if (where.Length > 0)
        {
            sql += " where " + where;
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