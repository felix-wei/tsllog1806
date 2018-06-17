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

public partial class Pages_ExportList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-7);
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
        if (txt_RefNo.Text.Trim() != "")
            where = "RefNo='" +txt_RefNo.Text.Trim() + "'";
        else if (this.txt_HouseNo.Text.Length > 0)
        {
            where = "JobNo='" +this.txt_HouseNo.Text.Trim() + "'";
        }
        else if (this.txt_HblN.Text.Length > 0)
        {
            where = "HblNo='" + this.txt_HblN.Text.Trim() + "'";
        }
        else if (this.txt_AgtId.Text.Length > 0)
        {
            where = "CustomerId='" + this.txt_AgtId.Text.Trim() + "'";

            if (dateFrom.Length > 0 && dateTo.Length > 0)
                where += string.Format(" and Etd >= '{0}' and Etd < '{1}'", dateFrom, dateTo);
            string sql = string.Format("SELECT job.JobNo FROM SeaExport AS job INNER JOIN SeaExportRef AS ref ON job.RefNo = ref.RefNo WHERE {0}", where);
            DataTable tab = Manager.ORManager.GetDataSet(sql).Tables[0];
            string where1 = "( 1=0";

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                where1 += string.Format(" or JobNo='{0}'", tab.Rows[i][0]);
            }
            where1 += ")";
            where = where1;
        }
        else if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where += string.Format(" Etd >= '{0}' and Etd < '{1}'", dateFrom, dateTo);
            string sql = string.Format("SELECT job.JobNo FROM SeaExport AS job INNER JOIN SeaExportRef AS ref ON job.RefNo = ref.RefNo WHERE {0}", where);
            DataTable tab = Manager.ORManager.GetDataSet(sql).Tables[0];
            string where1 = "( 1=0";

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                where1 += string.Format(" or JobNo='{0}'", tab.Rows[i][0]);
            }
            where1 += ")";
            where = where1;
        }

        if (where.Length > 0)
        {
            this.dsExport.FilterExpression = where;
        }
        else
        {
            this.dsExport.FilterExpression = "1=1";
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


}
