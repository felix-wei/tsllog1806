<%@ WebHandler Language="C#" Class="DriverDaily_Auto" %>

using System;
using System.Web;

public class DriverDaily_Auto : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");


        string sql = string.Format(@"insert into CTM_DriverLog (date,driver,towhead,IsActive) 
(select GETDATE(),Code,TowheaderCode,'Y' from CTM_Driver where Code not in(select Driver from CTM_DriverLog where DATEDIFF(d,GETDATE(),Date)=0) and StatusCode='Active')");
        int result = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
        string re = "Driver Daily False";
        if (result > 0)
        {
            re = result + " Driver Daily Success";
        }
        context.Response.Write(re);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}