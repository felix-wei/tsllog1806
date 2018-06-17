using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Report_PurchaseReport : System.Web.UI.Page
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

        if (this.txt_CustId.Text.Length > 0)
        {
            where = "PartyId='" + this.txt_CustId.Text.Trim() + "'";

            if (dateFrom.Length > 0 && dateTo.Length > 0)
                where += string.Format(" and PoDate >= '{0}' and PoDate < '{1}'", dateFrom, dateTo);
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where += string.Format(" PoDate >= '{0}' and PoDate < '{1}'", dateFrom, dateTo);
        }

        if (where.Length > 0)
        {
            this.dsWhpo.FilterExpression = where;
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        this.gridExport.WriteXlsToResponse("PurchaseReport", true);
    }
}