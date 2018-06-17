<%@ WebHandler Language="C#" Class="api_firebase" %>

using System;
using System.Web;
using System.Net;
using System.Data;
using Newtonsoft.Json;

public class api_firebase : IHttpHandler
{

    public void ProcessRequest(HttpContext ctx)
    {

        string webAddr = "https://zhaohui.firebaseio.com/CargoerpMove/demo%26system.json";
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
        httpWebRequest.ContentType = "text/json";
        //====================PATCH:修改，POST ：push，PUT ：writing，DELETE ：删除
        httpWebRequest.Method = "PATCH";

        using (var streamWriter = new System.IO.StreamWriter(httpWebRequest.GetRequestStream()))
        {
            DateTime dtime = DateTime.Now;
            string date = dtime.ToString("yyyy-MM-dd HH:mm:ss");
            string json = "{\"company\":\"demo\",\"content\":{\"command\":\"\",\"date\":\"" + date + "\",\"detail\":\"David Goh Login\",\"target\":\"\"},\"type\":\"notice\"}";

            streamWriter.Write(json);
            streamWriter.Flush();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

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