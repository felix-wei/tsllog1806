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
using DevExpress.Web.ASPxDataView;
public partial class PagesContainer_Job_StatusInquiryEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (txt_ContainerNo.Text.Trim() != "")
        {
            where = " and ContainerNo='" + txt_ContainerNo.Text.Trim() + "'";

        }
        if (where.Length == 0)
        {
            string status =this.cb_Status.Text.Trim();
            string from = "";
            string to="";
            if (this.date_EventFrom.Value != null&&this.date_EventTo.Value!=null)
            {
                from = this.date_EventFrom.Date.ToString("yyyy-MM-dd");
                to = this.date_EventTo.Date.ToString("yyyy-MM-dd");
            }

            if (from.Length > 0&&to.Length>0)
            {
                where = GetWhere(where, string.Format(" EventDateTime>='{0}' and EventDateTime<='{1}'",from,to));
            }
        }
        if (where.Length > 0)
        {
            where = GetWhere(where, " EventDateTime=(select MAX(EventDateTime) from Cont_AssetEvent)");
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