<%@ WebHandler Language="C#" Class="Email" %>

using System;
using System.IO;
using System.Web;
using C2;
using Wilson.ORMapper;

public class Email : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";
        string err = "";
        string job = context.Request["job"] ?? "";
        string pic = context.Request["pic"] ?? "";
        try
        {
            string path1 = "";
            string newpath1 = "";
            string prefix = "~/Photos/";
            string zipName = string.Format("{0}-{1:yyyyMMddHH}.zip", job, DateTime.Now);
            if (File.Exists(prefix + zipName))
                File.Delete(prefix + zipName);
            string logo = System.Configuration.ConfigurationManager.AppSettings["CompanyMarkWater"];
            string companyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            string[] picsa = pic.Split(new char[] { ',' });
            string filePath = HttpContext.Current.Server.MapPath(prefix + "\\");
            string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
            string newSavePath=HttpContext.Current.Server.MapPath(prefix + "\\"+job+"\\"+"Watermark"+"\\").ToString();
            if (!Directory.Exists(newSavePath))
                Directory.CreateDirectory(newSavePath);
            for (int i = 1; i < picsa.Length; i++)
            {
                int id=SafeValue.SafeInt(picsa[i],0);
                if (id > 0)
                {
                    OPathQuery query = new OPathQuery(typeof(C2.CtmAttachment), "Id=" + picsa[i], "Id");
                    ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
                    C2.CtmAttachment foto = objSet[0] as C2.CtmAttachment;

                    OPathQuery query2 = new OPathQuery(typeof(C2.CtmJob), "JobNo='" + foto.RefNo + "'", "Id");
                    ObjectSet objSet2 = C2.Manager.ORManager.GetObjectSet(query2);
                    C2.CtmJob order = objSet2[0] as C2.CtmJob;
                    string fileName = foto.FileName;
                    pic = foto.FilePath.Trim();
                    //pic = SafeValue.SafeString(Psakd.CfsManager.ORManager.ExecuteScalar("select path from [xwjobphoto] where oid=" + picsa[i]), "");
                    string path="";

                    if (pic.Contains(fileStart))
                    {
                        path = prefix + pic.Replace(fileStart, "");
                        path1 = filePath + pic.Replace(fileStart, "");
                    }
                    else
                    {
                        path = prefix + pic;
                        path1 = filePath + pic;
                    }
                    if (System.IO.File.Exists(path1))
                    {
                        string fa = pic.ToLower().Replace(".jpg", "a.jpg");
                        string newFilePath = newSavePath + fileName;
                        string refNo = foto.RefNo;
                        string vessel = string.Format("{0}/{1}", order.Vessel, order.Voyage);
                        string cargoref = string.Format("{0}", foto.TripIndex);
                        string label = " ";
                        if (cargoref == "/")
                            cargoref = "";
                        C2.Email.AddWarkWater(path1, logo, newFilePath, fileName, companyName, "Image");
                        newpath1 += newFilePath + ",";
                    }
                }
            }

            C2.Zip.ZipFilesString(newpath1, filePath + zipName);

            err = "/photos/" + zipName;
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