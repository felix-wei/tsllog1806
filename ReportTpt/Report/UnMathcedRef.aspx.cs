using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;

public partial class ReportTpt_Report_UnMathcedRef : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            this.txt_from.Date = DateTime.Today.AddDays(-15);
            this.txt_end.Date = DateTime.Today.AddDays(8);
        }

    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        string sql = @"select * from Tpt_job";
        string where = "";
        string agtId = this.btn_AgtId.Text.Trim();
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
        }
        if (agtId.Length > 0)
            where = GetWhere(where, " CustRef='" + agtId + "'");
        if (dateFrom.Length > 0 && dateTo.Length > 0)
            where = GetWhere(where, string.Format(" Eta >= '{0}' and Eta <= '{1}'", dateFrom, dateTo));
        if (where.Length > 0)
        {
            sql += " where " + where;
        
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.grid_Import.DataSource = tab;
        this.grid_Import.DataBind();
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
        btnRetrieve_Click(null, null);
        this.gridExport.WriteXlsToResponse("TransportList", true);
    }
}