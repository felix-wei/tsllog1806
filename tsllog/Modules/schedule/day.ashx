<%@ WebHandler Language="C#" Class="DayProcess" %>

using System;
using System.IO; 
using System.Web;
using System.Data;
using System.Drawing;
using Aspose.Cells;
using C2;
using Wilson.ORMapper;

public class DayProcess : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";
        string err = "";
		string del = context.Request.QueryString["type"] ?? "mail";
        string date_ = SafeValue.SafeString(context.Request.QueryString["date"], DateTime.Today.ToString("dd/MM/yyyy"));
        string[] s1 = date_.Split('/');
        DateTime d_ = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));

        try
        {
            C2.Daily d = new Daily();
            d.auto_insert_job_cost();
        }
        catch (Exception ex)
        {
            err = ex.Message + "<br>" + ex.StackTrace;
        }
        
        context.Response.Write(err);
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}