using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportWarehouse_Report_SummaryReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = new DateTime(2013, 1, 1);
            this.date_End.Date = DateTime.Today;
            this.cmb_DocType.Text = "In";
        }
    }
}