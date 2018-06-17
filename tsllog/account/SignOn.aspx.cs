using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frames_SignOn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Browser.Cookies)
            {
                if (Request.Cookies["C2_LOGIN_INFO"] != null)
                {
                    this.Login1.UserName = Request.Cookies["C2_LOGIN_INFO"]["LOGIN_ID"];
                    this.Login1.RememberMeSet = true;
                }
            }
        }
    }

    protected void Login1_Authenticate(object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
    {
        string clientIp = Request.ServerVariables.Get("Remote_Addr").ToString();
        if (true)//this.Login1.UserName=="ZHANGYINGCHUN"||(clientIp.Length > 7 && (clientIp.Substring(0, 7) == "203.169"||clientIp.Substring(0, 3) == "192"||clientIp.Substring(0,3) == "218"||clientIp.Substring(0,3) == "127"||clientIp.Substring(0,3) == "175")))
        {
            if (this.Login1.RememberMeSet)
            {
                if (Request.Browser.Cookies)
                {
                    if (Request.Cookies["C2_LOGIN_INFO"] == null)
                    {
                        Response.Cookies["C2_LOGIN_INFO"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["C2_LOGIN_INFO"]["LOGIN_ID"] = this.Login1.UserName;
                        Response.Write(string.Format("<!-- new login as : [{0}]-->", Response.Cookies["C2_LOGIN_INFO"]["LOGIN_ID"]));
                    }
                    else
                    {
                        Response.Cookies["C2_LOGIN_INFO"]["LOGIN_ID"] = this.Login1.UserName;
                        Response.Write(string.Format("<!-- exist login as : [{0}]-->", Response.Cookies["C2_LOGIN_INFO"]["LOGIN_ID"]));
                    }
                }
            }

            C2.Membership membership = new C2.Membership();
            if (membership.ValidateUser(this.Login1.UserName, this.Login1.Password))
            {
                 System.Web.Security.FormsAuthentication.RedirectFromLoginPage(this.Login1.UserName.ToUpper(), this.Login1.RememberMeSet);
            }
        }
        else
        {
            // will login database later
            System.IO.StreamWriter w = new System.IO.StreamWriter(Server.MapPath("~/App_data/Login.txt"), true);
            w.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "  IP:" + clientIp + "   UserName:" + this.Login1.UserName);
            w.Close();
        }
    }    

}