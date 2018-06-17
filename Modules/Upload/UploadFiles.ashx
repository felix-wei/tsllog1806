<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;
using System.IO;

public class UploadFile : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        HttpPostedFile fileAdd = context.Request.Files["file"];
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            var JobNo = context.Request.Form["JobNo"];
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string path = context.Server.MapPath("~/Photos/" +  JobNo.ToUpper());
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fname = context.Server.MapPath("~/Photos/" + JobNo.ToUpper() +"/" + file.FileName);
                file.SaveAs(fname);
                Console.Write(fname);

            }
        }

        //}
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}