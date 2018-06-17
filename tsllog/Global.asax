<%@ Application Language="C#" %>

<script runat="server">


    void Application_Start(object sender, EventArgs e)
    {
        // CFS.Thread.Instance().Start();  
        //if (null == HttpContext.Current.User || HttpContext.Current.User.Identity.Name.Length < 1)
        //    Response.Redirect(@"~\Frames\login.aspx");

        // register IoC
    }

    void Global_BeginRequest(object sender, EventArgs e)
    {

    }

    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        helper_log.log_page(base.Context.Request);
    }
   
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
       // CFS.Thread.Instance().Stop();

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    
</script>
