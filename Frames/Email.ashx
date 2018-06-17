<%@ WebHandler Language="C#" Class="Email" %>

using System;
using System.Web;

public class Email : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string vars = "";
        vars += "to:" + context.Request["to"] ?? "[empty]";
        vars += "cc:" + context.Request["cc"] ?? "[empty]";
        vars += "bcc:" + context.Request["bcc"] ?? "[empty]";
        vars += "sub:" + context.Request["sub"] ?? "[empty]";
        vars += "msg:" + context.Request["msg"] ?? "[empty]";
        vars += "pic:" + context.Request["pic"] ?? "[empty]";
        vars += "job:" + context.Request["job"] ?? "[empty]";
        string err1 = "";
        string err2 = "";
        string pics = context.Request["pic"] ?? "";
        string[] picsa = pics.Split(new char[] { ',' });
        string files = "";
        string prefix = @"~/Photos/";
        string filePath = HttpContext.Current.Server.MapPath(prefix+"\\");
        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
        if (fileStart == null)
        {
            fileStart = "";
        }
        for (int i = 1; i < picsa.Length; i++)
        {
            string pic = "";
            string path = "";
            string path1 = "";
            pic = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select FilePath from [CTM_Attachment] where Id=" + picsa[i]), "");

            if (pic.Contains(fileStart))
            {
                path ="/Photos/"+ pic.Replace(fileStart, "");
                path1 = filePath + pic.Replace(fileStart, "");
            }
            else
            {
                path = "/Photos/" + pic;
                path1 = filePath + pic;
            }
            if (System.IO.File.Exists(path1))
            {
                files += (files == "" ? "" : ",") + path;
            }
            else
            {
                err1 += pic + " not found"; 
            }
        } 
        string _to = context.Request["to"] ?? "";
        string _cc = context.Request["cc"] ?? "";
        string _bcc = context.Request["bcc"] ?? "";
        string _sub = context.Request["sub"] ?? "";
        string _msg = context.Request["msg"] ?? "";
        string _job = context.Request["job"] ?? "";
        string err = Helper.Email.SendEmail(_to, _cc, _bcc, _sub + " [Job No: " + _job + "]", _msg, files);
        if (err == "")
            err2 = "Send Successfully";
        else
            err2 = "Error Sending." + err + err1;
        context.Response.Write(err2);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}