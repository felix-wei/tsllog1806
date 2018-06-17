using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesFreight_Export_PrintObl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_printObl_Click(object sender, EventArgs e)
    {
        string refNo = this.txtRefNo.Text;
        string script = string.Format("<script>alert({0});</script>");
    }
}