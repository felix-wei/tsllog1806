using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Report_PrintStockTallySheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            this.date_From.Date = DateTime.Today.AddDays(-1);
            this.date_End.Date = DateTime.Today;
        }
    }
}