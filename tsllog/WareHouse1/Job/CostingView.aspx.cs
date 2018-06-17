using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using System.Web.Mail;
using System.IO;
using System.Net.Mail;
using DevExpress.XtraReports.UI;

public partial class CostingView : BasePage
{
    public static string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] != null)
        {
            id = Request.Params["id"];
        }
    }
}
