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

public partial class PagesContainer_Job_ReleaseOrderList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-15);
            this.txt_end.Date = DateTime.Today.AddDays(8);
            if (EzshipHelper.GetUserName().ToUpper() == "ADMIN")
            {
                btn_Port.Enabled = true;
                btn_Port.Text = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
            }
            else
            {
                btn_Port.Text = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
            }
            btn_search_Click(null, null); 
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "DocType = 'RO'";
        if (txt_DocNo.Text.Trim() != "")
            where += " and DocNo='" + txt_DocNo.Text.Trim() + "'";
        string from = "";
        string to = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
           from = txt_from.Date.ToString("yyyy-MM-dd");
           to = txt_end.Date.ToString("yyyy-MM-dd");
        }
        if (from.Length > 0 && to.Length > 0)
            where = GetWhere(where, string.Format(" ShipEta>= '{0}' and ShipEta <= '{1}'", from, to));
        if (btn_Port.Text.Trim().Length > 0)
        {
            where = GetWhere(where, " shippol='" + btn_Port.Text + "'");
        }
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

}