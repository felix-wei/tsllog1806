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

public partial class WareHouse_Job_EcOrderList : System.Web.UI.Page
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
        string user = HttpContext.Current.User.Identity.Name;
        string sql = string.Format("select CustId from  [dbo].[User] where Name='{0}'", user);
        string partyId = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        string where = "";
        sql = "select * from Wh_Trans ";
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (txt_PoNo.Text.Trim() != "")
            where = GetWhere(where, "DoNo='" + txt_PoNo.Text.Trim() + "'");
        else if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " DoDate >= '" + dateFrom + "' and DoDate < '" + dateTo + "'");
        }

        if (where.Length > 0)
        {
            sql += string.Format(@" where " + where + " and PartyId='{0}' and DoType='SO' order by DoNo", partyId);
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