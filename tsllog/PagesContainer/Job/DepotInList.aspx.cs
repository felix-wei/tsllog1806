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

public partial class PagesContainer_Job_DepotInList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (EzshipHelper.GetUserName().ToUpper() == "ADMIN")
        {
            btn_Port.Enabled = true;
            btn_Port.Text = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        }
        else
        {
            btn_Port.Text = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        //string Port = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        //string Port = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select Port from dbo.[User] where name='{0}'", Name)));
        string Port = btn_Port.Text.Trim();
        string docNo = txt_soNo.Text.Trim();
        where = string.Format("EventCode='depotin' and DocType='SO'");
        if (Port.Length > 0)
        {
            where += " and EventPort='" + Port + "'";
        }
        if (docNo.Length > 0)
        {
            where += " and docNo='" + docNo + "'";
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