<%@ WebHandler Language="C#" Class="gps" %>

using System;
using System.Web;

public class gps : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string driver = SafeValue.SafeString(context.Request.QueryString["u"]);
        string lat = SafeValue.SafeString(context.Request.QueryString["lat"]);
        string lng = SafeValue.SafeString(context.Request.QueryString["lng"]);
        //insert to db
        if(driver.Length>0)
        InsertGps(driver, lat, lng);
    }
    private void InsertGps(string driver, string lat, string lng)
    {
        string sql = string.Format("Insert Into Ctm_Gps(Driver,Lat,Lng,CreateBy,CreateDateTime) Values('{0}','{1}','{2}','{3}',getdate())"
            ,driver,lat,lng,EzshipHelper.GetUserName());
        ConnectSql.ExecuteSql(sql);
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}