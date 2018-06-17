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

public partial class Pages_Export_ExportRefSelect : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (txt_RefNo.Text.Trim() != "")
            where = "RefNo='" + txt_RefNo.Text.Trim() + "'";
        else
        {
            string obl = this.txt_Obl.Text.Trim();
            if (obl.Length > 0)
                where = GetWhere(where, "OblNo='" + obl + "'");

        }
        if (where.Length > 0)
        {
            Session["ExpSelWhere"] = where;
            this.dsExportRef.FilterExpression = where;
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
