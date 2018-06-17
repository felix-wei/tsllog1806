<%@ WebHandler Language="C#" Class="Signout" %>

using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;

public class Signout : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {

        if (HttpContext.Current.Request.Cookies["C2_LOGIN_INFO"] != null)
        {
            HttpContext.Current.Response.Cookies["C2_LOGIN_INFO"].Expires = DateTime.Now.AddDays(30);

            HttpContext.Current.Response.Write(string.Format("<!-- new login as : [{0}]-->", HttpContext.Current.Response.Cookies["C2_LOGIN_INFO"]["LOGIN_ID"]));
            //HttpContext.Current.Response.Write("<!-- Got Login Info"+HttpContext.Current.Response.Cookies["C2_LOGIN_INFO"]["LOGIN_ID"]+" -->");
        }
        else
        {
            HttpContext.Current.Response.Write("<!-- No Login Info -->");
        }
        
        FormsAuthentication.SignOut(); 
        context.Response.Redirect("/Default.aspx");
        
    }
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}