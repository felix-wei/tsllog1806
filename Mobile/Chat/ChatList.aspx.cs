using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_Chat_ChatList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "IsActive=1 and Name<>'" + HttpContext.Current.User.Identity.Name + "'";
        if (search_name.Text.Length > 0)
        {
            where += " and Name like '" + search_name.Text + "%'";

        }
        dsUser.FilterExpression = where;
    }
    protected void gv_BeforePerformDataSelect(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
}