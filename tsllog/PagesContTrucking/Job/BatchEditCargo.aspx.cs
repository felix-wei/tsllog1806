using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_BatchEditCargo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
            string cargoType = SafeValue.SafeString(Request.QueryString["type"]);
            JobNo.Value = jobNo;
            CargoType.Value = cargoType;
            //string id = job_new();
        }
    }
}