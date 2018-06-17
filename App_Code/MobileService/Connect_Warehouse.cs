using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;


/// <summary>
/// Connect_Warehouse 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Connect_Warehouse : System.Web.Services.WebService {

    public Connect_Warehouse () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void EGL_Warehouse_Inventory(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string code = SafeValue.SafeString(job["code"]);
        string party = SafeValue.SafeString(job["no"]);
        string loc = SafeValue.SafeString(job["loc"]);
        DataTable dt = null;
        string sql = string.Format(@"select *,tab_bal.BalQty,p.Name from job_house mast 
left join xxparty p on mast.ClientId=p.PartyId
left join (select sum(case when CargoType='IN' then Qty else -Qty end) as BalQty,ClientId from job_house group by ClientId) as tab_bal on tab_bal.ClientId=mast.ClientId 
where CargoType='IN' and tab_bal.BalQty>0");
        list = new List<ConnectSql_mb.cmdParameters>();
        if (party.Length > 0)
        {
            sql += " and p.Name like @Name";
            cpar = new ConnectSql_mb.cmdParameters("@Name", "%"+party + "%", SqlDbType.NVarChar, 200);
            list.Add(cpar);
        }
        if (loc.Length > 0)
        {
            sql += " and Remark2 like @Remark2";
            cpar = new ConnectSql_mb.cmdParameters("@Remark2", loc + "%", SqlDbType.NVarChar, 200);
            list.Add(cpar);
        }
        if (code.Length > 0)
        {
            sql += " and SkuCode like @Product";
            cpar = new ConnectSql_mb.cmdParameters("@Product", code + "%", SqlDbType.NVarChar, 200);
            list.Add(cpar);
        }
        dt = ConnectSql_mb.GetDataTable(sql, list);

        context = Common.DataTableToJson(dt);

        Common.WriteJsonP(status, context);
    }
}
