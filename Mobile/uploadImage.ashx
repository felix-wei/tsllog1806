<%@ WebHandler Language="C#" Class="uploadHandler" %>

using System;
using System.Web;

public class uploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        context.Response.Charset = "utf-8";

        HttpPostedFile file = context.Request.Files["fileAddPic"];

        // fileAddPic为app端FileUploadOptions传入参数，此点非常重要

        string fileName = file.FileName;

        int i = fileName.LastIndexOf('/');
        //string folder = fileName.Substring(0, i);
        //fileName = fileName.Substring(i);
        
        //string uploadPath = "D:/website/cerp_collinsmovers/cerp_collinsmovers/Photos/" + folder + "/";
        //string uploadPath = "C:/cargoerp/cerp_collinsmovers/Photos/" + folder + "/";


        //string folder = "~/Mobile/Upload";
        string folder = "~/Photos";
        string uploadPath = HttpContext.Current.Server.MapPath(folder + "\\");
        if (i >= 0)
        {
            uploadPath += fileName.Substring(0, i) + "/";
            fileName = fileName.Substring(i);
        }

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadPath);
        if (!dir.Exists)
        {
            dir.Create();
        }

        if (file != null)
        {
            fileName = fileName.Replace(".3gpp", ".mp3");
            fileName = fileName.Replace(".amr", ".mp3");
            file.SaveAs(uploadPath + fileName);
            
            context.Response.Write("upload success");
            //======================重新存一张size：200的image
            RebuildImage.Rebuild(file.FileName, 500);
        }

        else
        {

            context.Response.Write("upload false");

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}