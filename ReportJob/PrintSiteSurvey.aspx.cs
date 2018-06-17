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

public partial class ReportJob_PrintSiteSurvey: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string refNo =SafeValue.SafeString(Request.QueryString["no"]);
            string type = SafeValue.SafeString(Request.QueryString["type"]);
            lbl_JobType.Text = type;
            string sql = string.Format(@"select * from JobInfo where JobNo='{0}'",refNo);
            DataTable tab = ConnectSql.GetTab(sql);
            this.grid.DataSource = tab;
            this.grid.DataBind();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }
    }
}