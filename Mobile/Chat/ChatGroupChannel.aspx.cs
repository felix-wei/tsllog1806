using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_Chat_ChatGroupChannel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["No"] != null && Request.QueryString["No"].ToString() != "")
            {
                string No = Request.QueryString["No"].ToString();
                string sql = string.Format(@"select * from Mobile_ChatGroup where Id={0}", No);
                DataTable dt = ConnectSql.GetTab(sql);
                if (dt.Rows.Count == 1)
                {
                    lb_chat.Text = dt.Rows[0]["group_name"].ToString();
                    lb_chatId.Text = No;
                }
                else
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }

                //=============Get login user Name
                //lb_user.Text = HttpContext.Current.User.Identity.Name;
                sql = string.Format(@"select name from [User] where name='{0}'", HttpContext.Current.User.Identity.Name);
                lb_user.Text = ConnectSql_mb.ExecuteScalar(sql);
            }
            else
            {
                //================跳到指定页面，（上一个页面UrlReferrer）
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}