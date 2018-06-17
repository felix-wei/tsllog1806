using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// helper_log 的摘要说明
/// </summary>
public partial class helper_log : System.Web.UI.Page
{
    public helper_log()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static void log_page(HttpRequest par)
    {
        string _ip = SafeValue.SafeString(par.ServerVariables.Get("REMOTE_ADDR"));
        string _method = SafeValue.SafeString(par.ServerVariables.Get("HTTP_METHOD"));
        string _url = SafeValue.SafeString(par.ServerVariables.Get("HTTP_URL"));
        string _agent = SafeValue.SafeString(par.ServerVariables.Get("HTTP_USER_AGENT"));
        string _auth = par.IsAuthenticated ? "Y" : "N";
        string _user = !par.IsAuthenticated ? "Guest" : HttpContext.Current.User.Identity.Name;

        if (_url.EndsWith(".aspx") ||_url.EndsWith(".ashx")   )
        {
            string sql = string.Format(@"insert into HelperLog_Page (IP,Method,Url,Agent,Auth,[User]) values('{0}','{1}','{2}','{3}','{4}','{5}')", _ip, _method, _url, _agent, _auth, _user);
            //throw new Exception(sql);
            ConnectSql.ExecuteSql(sql);
        }
    }
}