using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_Performance 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Connect_Performance : System.Web.Services.WebService
{

    public Connect_Performance()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    [WebMethod]
    public void Performance_GetDataList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "0";
        string context = "\"\"";

        string sql = string.Format(@"select SequenceId,Name,Role,sum(isnull(pts_value,0)) as value
from (
select SequenceId,Name,Role,rc.pts_value 
from [User] as u
left outer join [pts_record] as rc on u.Name=rc.pts_user 
) as temp
group by SequenceId,Name,Role
order by value desc,Name ");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        //list.Add(new ConnectSql_mb.cmdParameters("@CompanyREG", job["company"], SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        status = "1";
        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void Performance_GetCategoryByType(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        bool status = true;

        string sql = string.Format(@"select item.*,ctg.cate_code,ctg.cate_name,t.type_code,t.type_name,ctg.Id as cateId  
        from pts_type as t
        left outer join pts_category as ctg on t.Id=ctg.type_id
        left outer join pts_item as item on ctg.type_id=item.type_id and ctg.Id=item.cate_id
        where t.type_code=@Type 
        order by ctg.cate_code");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Type", job["type"], SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string category = Common.DataTableToJson(dt);

        sql = string.Format(@"select SequenceId,Name,Role from [User] where SequenceId=@SequenceId");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@SequenceId", job["Id"], SqlDbType.Int));
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);


        string context = string.Format(@"{0}mast:{2},category:{3}{1}", "{", "}", mast, category);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Performance_EditSave(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        JArray jar = (JArray)JsonConvert.DeserializeObject(job["selectedList"].ToString());

        string fromName = job["user"].ToString();
        string toName = job["Name"].ToString();

        string sql = "";
        List<ConnectSql_mb.cmdParameters> list = null;
        DataTable dt = null;
        for (int i = 0; i < jar.Count; i++)
        {
            int itemId = SafeValue.SafeInt(jar[i]["Id"], 0);
            int valueIndex = SafeValue.SafeInt(jar[i]["value"], 0);
            if (valueIndex > 0 && valueIndex <= 5)
            {
                sql = string.Format(@"select type_id,cate_id,Id as item_id,0 as case_id,pts_value1,pts_value2,pts_value3,pts_value4,pts_value5 from pts_item where Id=@Id");
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@Id", itemId, SqlDbType.Int));
                dt = ConnectSql_mb.GetDataTable(sql, list);
                if (dt.Rows.Count > 0)
                {
                    int type_id = SafeValue.SafeInt(dt.Rows[0]["type_id"], 0);
                    int cate_id = SafeValue.SafeInt(dt.Rows[0]["cate_id"], 0);
                    int item_id = itemId;
                    int case_id = SafeValue.SafeInt(dt.Rows[0]["case_id"], 0);
                    decimal pts_value = SafeValue.SafeDecimal(dt.Rows[0]["pts_value" + valueIndex]);

                    sql = string.Format(@"insert into pts_record (type_id,cate_id,item_id,case_id,pts_index,pts_value,review_time,review_user,pts_user)
values (@type_id,@cate_id,@item_id,@case_id,@pts_index,@pts_value,getdate(),@review_user,@pts_user)
select @@Identity");
                    list.Add(new ConnectSql_mb.cmdParameters("@type_id", type_id, SqlDbType.Int));
                    list.Add(new ConnectSql_mb.cmdParameters("@cate_id", cate_id, SqlDbType.Int));
                    list.Add(new ConnectSql_mb.cmdParameters("@item_id", item_id, SqlDbType.Int));
                    list.Add(new ConnectSql_mb.cmdParameters("@case_id", case_id, SqlDbType.Int));
                    list.Add(new ConnectSql_mb.cmdParameters("@pts_index", valueIndex, SqlDbType.Int));
                    list.Add(new ConnectSql_mb.cmdParameters("@pts_value", pts_value, SqlDbType.Decimal));
                    list.Add(new ConnectSql_mb.cmdParameters("@review_user", fromName, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@pts_user", toName, SqlDbType.NVarChar, 100));

                    ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);

                    if (res.status)
                    {
                        //===========log
                        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                        lg.Platform_isMobile();
                        lg.Controller = SafeValue.SafeString(job["user"]);
                        lg.Lat = SafeValue.SafeString(job["Lat"]);
                        lg.Lng = SafeValue.SafeString(job["Lng"]);
                        lg.setActionLevel(SafeValue.SafeInt(res.context,0),CtmJobEventLogRemark.Level.Performance,1);
                        lg.log();
                    }
                }
            }
        }
        Common.WriteJsonP(true, Common.StringToJson(""));
    }


    [WebMethod]
    public void Case_GetDataList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "0";
        string context = "\"\"";

        string sql = string.Format(@"select * from pts_case ");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        status = "1";
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Case_GetDataDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string sql = string.Format(@"select c.*,t.type_code,t.type_name
from pts_case as c
left outer join pts_type as t on c.pts_type_id=t.Id
where c.Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);

        sql = string.Format(@"select * 
from pts_case_user 
where case_id=@Id");
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string users = Common.DataTableToJson(dt);

        string context = string.Format(@"{0}mast:{2},users:{3}{1}", "{", "}", mast, users);
        Common.WriteJsonP(true, context);
    }



    [WebMethod]
    public void Performance_GetCategoryByTypeId(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        bool status = true;

        string sql = string.Format(@"select item.*,ctg.cate_code,ctg.cate_name,t.type_code,t.type_name,ctg.Id as cateId  
from pts_type as t
left outer join pts_category as ctg on t.Id=ctg.type_id
left outer join pts_item as item on ctg.type_id=item.type_id and ctg.Id=item.cate_id
where t.Id=@typeId
order by ctg.cate_code");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@typeId", job["typeId"], SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string category = Common.DataTableToJson(dt);

        sql = string.Format(@"select *,user_name as Name from pts_case_user where Id=@userId");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@userId", job["userId"], SqlDbType.Int));
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);


        string context = string.Format(@"{0}mast:{2},category:{3}{1}", "{", "}", mast, category);
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void Attendance_EditSave(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string user = job["user"].ToString();
        string action = job["action"].ToString();

        string sql = string.Format(@"insert into pts_attendance (createdate,record_type,record_user,record_note) 
values(getdate(),@record_type,@record_user,@record_note)
select @@Identity");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@record_type", action, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@record_user", user, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@record_note", "", SqlDbType.NVarChar, 300));
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteScalar(sql, list);
        if (result.status && action.ToLower().IndexOf("in") >= 0)
        {
            sql = string.Format(@"insert into pts_record (type_id,cate_id,item_id,case_id,pts_index,pts_value,review_time,review_user,pts_user)
values (@type_id,@cate_id,@item_id,@case_id,@pts_index,@pts_value,getdate(),@review_user,@pts_user)");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@type_id", 0, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@cate_id", 0, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@item_id", 0, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@case_id", 0, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@pts_index", 0, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@pts_value", 10, SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@review_user", user, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@pts_user", user, SqlDbType.NVarChar, 100));
            ConnectSql_mb.ExecuteNonQuery(sql, list);


            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            lg.setActionLevel(SafeValue.SafeInt(result.context, 0), CtmJobEventLogRemark.Level.Attendance, 1);
            lg.log();
        }

        Common.WriteJsonP(true, Common.StringToJson(""));
    }

    [WebMethod]
    public void Attendance_historyList_ByUser(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string user = job["user"].ToString();
        string dateFrom = job["from"].ToString();
        string dateTo = job["to"].ToString();

        string sql = string.Format(@"select * from pts_attendance 
where record_user=@record_user and datediff(d,createdate,@dateFrom)<=0 and datediff(d,createdate,@dateTo)>=0");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@record_user", user, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@dateFrom", dateFrom, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@dateTo", dateTo, SqlDbType.NVarChar, 8));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        Common.WriteJsonP(true, Common.DataTableToJson(dt));
    }
    [WebMethod]
    public void Attendance_historyList_ByCompany(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string user = job["user"].ToString();
        string dateFrom = job["from"].ToString();

        string sql = string.Format(@"select * from pts_attendance 
where datediff(d,createdate,@dateFrom)=0");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@dateFrom", dateFrom, SqlDbType.NVarChar, 8));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        Common.WriteJsonP(true, Common.DataTableToJson(dt));
    }


}
