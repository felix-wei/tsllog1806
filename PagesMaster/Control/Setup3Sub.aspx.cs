using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesMaster_Control_Setup3Sub : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string masterId = SafeValue.SafeString(Request.QueryString["id"], "");
            this.dsSetupSub.SelectParameters.Clear();
            this.dsSetupSub.SelectParameters.Add("MastId", TypeCode.String, masterId);
        }
    }

    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["MastId"] = SafeValue.SafeString(Request.QueryString["id"], "");
    }
}