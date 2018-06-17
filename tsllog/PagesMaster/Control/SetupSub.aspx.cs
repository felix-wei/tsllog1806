using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesMaster_Control_SetupSub : System.Web.UI.Page
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
        e.NewValues["Year"] = DateTime.Now.Year;
        e.NewValues["YearCurrentNo"] = 0;
        e.NewValues["Mth01CurrentNo"] = 0;
        e.NewValues["Mth02CurrentNo"] = 0;
        e.NewValues["Mth03CurrentNo"] = 0;
        e.NewValues["Mth04CurrentNo"] = 0;
        e.NewValues["Mth05CurrentNo"] = 0;
        e.NewValues["Mth06CurrentNo"] = 0;
        e.NewValues["Mth07CurrentNo"] = 0;
        e.NewValues["Mth08CurrentNo"] = 0;
        e.NewValues["Mth09CurrentNo"] = 0;
        e.NewValues["Mth10CurrentNo"] = 0;
        e.NewValues["Mth11CurrentNo"] = 0;
        e.NewValues["Mth12CurrentNo"] = 0;
    }
}