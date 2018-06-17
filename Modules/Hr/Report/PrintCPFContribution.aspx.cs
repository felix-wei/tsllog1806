using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Hr_Report_PrintCPFContribution : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = DateTime.Today.AddMonths(-1).AddDays(-DateTime.Today.Day + 1);
            this.date_End.Date = DateTime.Today.AddDays(-DateTime.Today.Day);
        }
    }
}