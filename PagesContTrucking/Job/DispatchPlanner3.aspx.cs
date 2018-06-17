using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_DispatchPlanner3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            date_searchDate.Date = DateTime.Now;
            ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>refresh_Data();</script>");
        }
    }
    protected void btn_Refresh_Click(object sender, EventArgs e)
    {
        if (date_searchDate.Date == null || date_searchDate.Date < new DateTime(1900, 1, 1))
        {
            return;
        }
        ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>refresh_Data();</script>");
    }
}