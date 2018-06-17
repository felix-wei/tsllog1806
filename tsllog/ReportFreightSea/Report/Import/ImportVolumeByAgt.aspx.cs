using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using Wilson.ORMapper;
using System.Data.SqlClient;

public partial class ReportFreightSea_ImportVolumeByAgt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = new DateTime(2011, 1, 1);
            this.date_End.Date = DateTime.Today;
            this.cmb_PartyTo.Value = "NA";
        }
    }
    protected void btn_Post_click(object sender, EventArgs e)
    {
               
    }

}
