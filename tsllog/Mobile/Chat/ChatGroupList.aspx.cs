using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_Chat_ChatGroupList : System.Web.UI.Page
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
        string where = "";
        if (search_name.Text.Length > 0)
        {
            where += " and ms.group_name like '" + search_name.Text + "%'";

        }
        string sql = string.Format(@"select ms.Id,ms.group_name from Mobile_ChatGroup_Det as det 
left outer join Mobile_ChatGroup as ms on det.group_name=cast(ms.Id as nvarchar)
where username='{0}'{1}", HttpContext.Current.User.Identity.Name,where);
        DataTable dt = ConnectSql.GetTab(sql);
        gv.DataSource = dt;
        gv.DataBind();
    }
    protected void gv_BeforePerformDataSelect(object sender, EventArgs e)
    {
        //btn_search_Click(null, null);
    }
}