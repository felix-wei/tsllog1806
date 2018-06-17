using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Model.CTM_JobEventLog ev = new Model.CTM_JobEventLog();
        ev.JobNo = "wwww";
        BLL.Class1 c = new BLL.Class1();
        c.show(ev);
    }
}