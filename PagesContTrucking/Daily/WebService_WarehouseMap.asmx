<%@ WebService Language="C#" Class="WebService_WarehouseMap" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Cells;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService_WarehouseMap : System.Web.Services.WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [WebMethod]
    public void refresh()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string warehouse = SafeValue.SafeString(info["warehouse"]);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@warehouse", warehouse, SqlDbType.NVarChar, 100));

        string sql = string.Format(@"select * from ref_location where WarehouseCode=@warehouse");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        Common.WriteJson(true, "{\"list\":" + Common.DataTableToJson(dt) + "}");
    }
}