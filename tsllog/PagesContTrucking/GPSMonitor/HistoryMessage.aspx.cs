using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_GPSMonitor_HistoryMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.search_DateFrom.Date = DateTime.Now.AddDays(-15);
            this.search_DateTo.Date = DateTime.Now;
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (this.search_DateFrom.Date < new DateTime(1900, 1, 1))
        {
            this.search_DateFrom.Date = DateTime.Now.AddDays(-15);
        }
        if (this.search_DateTo.Date < new DateTime(1900, 1, 1))
        {
            this.search_DateTo.Date = DateTime.Now;
        }
        string where = "SendDate between '" + this.search_DateFrom.Date + "' and '" + this.search_DateTo.Date + "'";
        this.dsTransport.FilterExpression = where;
    }
}