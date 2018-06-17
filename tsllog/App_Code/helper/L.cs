using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public class L
{

    public static void Access(HttpRequest par)
    {
        string _ip = SafeValue.SafeString(par.ServerVariables.Get("REMOTE_ADDR"));
        string _method = SafeValue.SafeString(par.ServerVariables.Get("HTTP_METHOD"));
        string _url = SafeValue.SafeString(par.ServerVariables.Get("HTTP_URL"));
        string _agent = SafeValue.SafeString(par.ServerVariables.Get("HTTP_USER_AGENT"));
        string _auth = par.IsAuthenticated ? "Y" : "N";
        string _user = !par.IsAuthenticated ? "Guest" : HttpContext.Current.User.Identity.Name;

        if (_url.EndsWith(".aspx") || _url.EndsWith(".ashx") )
        {
            string sql = string.Format(@"insert into LogAccess(LogType,LogUser,LogTime,HttpAddress,HttpMethod,HttpUrl,HttpAgent,HttpAuth) 
		values('A','{5}',GetDate(),'{0}','{1}','{2}','{3}','{4}')", _ip, _method, _url, _agent, _auth, _user);
            D.Exec(sql);
        }
    }

    public static void Audit(string act,string ent, string key, string col, string va1, string va2 )
    {
        string _user = HttpContext.Current.User.Identity.Name;

        string sql = string.Format(@"insert into LogAudit(LogType,LogUser,LogTime,LogAction, LogEntity, LogKey, LogField,LogValue1,LogValue2) 
		values('A','{0}',GetDate(),'{1}','{2}','{3}','{4}','{5}','{6}')",_user,
	act,ent,key,col,va1,va2);
        D.Exec(sql);
    }
	
    public static void Error(string typ, string error, string trace)
    {
        //string _user = HttpContext.Current.User.Identity.Name;
        string _ip = SafeValue.SafeString(HttpContext.Current.Request.ServerVariables.Get("REMOTE_ADDR"));
        string _method = SafeValue.SafeString(HttpContext.Current.Request.ServerVariables.Get("HTTP_METHOD"));
        string _url = SafeValue.SafeString(HttpContext.Current.Request.ServerVariables.Get("HTTP_URL"));
        string _agent = SafeValue.SafeString(HttpContext.Current.Request.ServerVariables.Get("HTTP_USER_AGENT"));
        string _auth = HttpContext.Current.Request.IsAuthenticated ? "Y" : "N";
        string _user = !HttpContext.Current.Request.IsAuthenticated ? "Guest" : HttpContext.Current.User.Identity.Name;

		
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local"].ConnectionString);
        conn.Open();
        string sql = string.Format(@"insert into LogError(
		LogType,LogUser,LogTime,
		LogAction,LogModule,LogEntity,LogKey,
		ErrorInfo, ErrorTrace, 
		HttpAddress,HttpMethod, HttpUrl, HttpAgent,HttpAuth) 
		values('0','{1}',GetDate(),
		'','','','',
		@error, @trace,
		'{2}','{3}','{4}','{5}','{6}'
		)",
		typ, _user,	_ip, _method, _url, _agent, _auth);
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@error",error)); 
        cmd.Parameters.Add(new SqlParameter("@trace",trace)); 
        SafeValue.SafeSqlString(cmd.ExecuteNonQuery());
        conn.Close();
    }
	 
    public static bool CanView(string level){
        string _user = HttpContext.Current.User.Identity.Name;
	string acc = D.Text(string.Format("Select top 1 {0} from [User] Where Name='{1}'",level,_user));
	bool ret = false;
	if(acc != "None")
	    ret = true;
	return ret; 
    }  	

    public static bool CanEdit(string level){
        string _user = HttpContext.Current.User.Identity.Name;
	string acc = D.Text(string.Format("Select top 1 {0} from [User] Where Name='{1}'",level,_user));
	bool ret = false;
	if(acc == "Edit" || acc=="Admin")
	    ret = true;
	return ret; 
    }  	

    public static bool CanAdmin(string level){
        string _user = HttpContext.Current.User.Identity.Name;
	string acc = D.Text(string.Format("Select top 1 {0} from [User] Where Name='{1}'",level,_user));
	bool ret = false;
	if(acc == "Admin")
	    ret = true;
	return ret; 
    }  	

    public static bool CanBacklog(string level, DateTime dt){
        string _user = HttpContext.Current.User.Identity.Name;
	string acc = D.Text(string.Format("Select top 1 {0} from [User] Where Name='{1}'",level,_user));
	bool ret = false;
	DateTime _dt = new DateTime(dt.Year, dt.Month, dt.Day);
	if(acc == "Edit" && DateTime.Compare(_dt,DateTime.Today) == 0)
	    ret = true;
	if(acc == "Admin")
	    ret = true;
	//throw new Exception(ret.ToString());	
	return true;
	return ret; 
    }  	

}