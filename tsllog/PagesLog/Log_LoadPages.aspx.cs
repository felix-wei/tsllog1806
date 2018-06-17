using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesLog_Log_LoadPages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DateTime dt_to = DateTime.Now;
            DateTime dt_from = dt_to.AddMonths(-1);
            search_datefrom.Date = dt_from;
            search_dateto.Date = dt_to;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        DateTime minDate = new DateTime(1990, 1, 1);
        if (search_datefrom.Date <= minDate || search_dateto.Date <= minDate)
        {
            Response.Write("<script>alert('Please input From/To Date');</script>");
            return;
        }
        prepare_search();
        gv.DataBind();
    }

    private void prepare_search()
    {
        string sql_where = "";
        string user = search_user.Text.Trim();
        if (user.Length > 0)
        {
            sql_where = " and [User] like '" + user + "%'";
        }
        string url = search_url.Text.Trim();
        if (url.Length > 0)
        {
            sql_where += " and Url like '%" + url + "%'";
        }
        string sql = string.Format(@"select ROW_NUMBER() over(order by createdate desc) as Id, 
IP,
Method,
Url,
Agent,
Auth,
[User],
CONVERT(char(19),CreateDate,121) as Date
from HelperLog_Page
where createdate>='{0}' and createdate<='{1}' {2}", search_datefrom.Date.ToString("yyyyMMdd"), search_dateto.Date.AddDays(1).ToString("yyyyMMdd"), sql_where);
        DataTable dt = ConnectSql.GetTab(sql);
        gv.DataSource = dt;
    }

    protected void gv_BeforePerformDataSelect(object sender, EventArgs e)
    {

        DateTime minDate = new DateTime(1990, 1, 1);
        if (search_datefrom.Date <= minDate || search_dateto.Date <= minDate)
        {
            Response.Write("<script>alert('Please input From/To Date');</script>");
            return;
        }
        prepare_search();
    }
    protected void gv_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;
        if (e.VisibleIndex % 2 == 0)
        {
            e.Row.BackColor = System.Drawing.Color.Honeydew;
        } 
    }
}