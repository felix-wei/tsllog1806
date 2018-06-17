using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using DevExpress.Web.ASPxDataView;

public partial class PagesTpt_Job_TptJobListByStatus : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
            string typ = SafeValue.SafeString(Request.QueryString["typ"], "");
            this.txt_type.Text = typ;
            btn_search_Click(null,null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (this.txt_customerId.Text.Length > 0)
            where = GetWhere(where, "Cust='" + this.txt_customerId.Text.Trim() + "'");
        if (dateFrom.Length > 0 && dateTo.Length > 0)
            where = GetWhere(where, string.Format(" JobDate >= '{0}' and JobDate < '{1}'", dateFrom, dateTo));

        where = GetWhere(where, " JobProgress='" + this.txt_type.Text + "'");
        where = GetWhere(where, " StatusCode<>'CNL'");
        if (where.Length > 0)
        {
            this.dsTransport.FilterExpression = where;
        }
        else
        {
            this.dsTransport.FilterExpression = "1=1";
        }
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }

    protected void grid_Transport_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            string closeInd = SafeValue.SafeString(this.grid_Transport.GetRowValues(e.VisibleIndex, "StatusCode"));
            if (closeInd == "CLS")
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            }
            else if (closeInd == "CNL")
            {
                e.Row.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                DateTime eta = SafeValue.SafeDate(this.grid_Transport.GetRowValues(0, "Eta"), new DateTime(1900, 1, 1));
                if ((DateTime.Today.Subtract(eta)).Days > 30)
                    e.Row.BackColor = System.Drawing.Color.LightPink;

            }
        }
    }
}