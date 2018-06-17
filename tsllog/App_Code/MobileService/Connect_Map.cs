using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_Map 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect_Map : System.Web.Services.WebService
{

    public Connect_Map()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void Map_GetDataList()
    {

        string sql = string.Format(@"with tb1 as(
select * from (select ROW_NUMBER()over(PARTITION BY [user] ORDER BY create_date_time desc) as rowId,* from CTM_Location ) as temp where rowId=1
),
tb2 as(
select * from CTM_Driver where StatusCode='Active'
)
select [user] as u,geo_device as d,geo_latitude as lat,geo_longitude as lng,note1 as p,create_date_time as date 
from tb1 as t1 
left outer join tb2 as t2 on t1.[user]=t2.Code
where t2.Code<>''
order by u");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string json = Common.DataTableToJson(dt);
        Common.WriteJsonP("1", json);
    }

}
