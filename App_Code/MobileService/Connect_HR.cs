using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Connect_HR
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Connect_HR : System.Web.Services.WebService
{

    public Connect_HR()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    public void HR_Leave_list_getdata(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);
        string role = job["role"].ToString().Trim().ToLower();
        string user = job["user"].ToString();
        string dateFrom = job["from"].ToString();
        string dateTo = job["to"].ToString();
        int personId = SafeValue.SafeInt(job["no"], 0);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        //if (role != "admin" && role != "hr")
        //{
        //    sql_where = " and l.Person<>(select top 1 Id from Hr_Person where name=@user)";
        //}
        sql_where = " and l.Person<>isnull((select top 1 Id from Hr_Person where name=@user),0)";
        if (personId > 0)
        {
            sql_where += " and l.Person=@personId";
        }
        string sql = string.Format(@"select l.*,p.Name as PersonName from Hr_Leave as l
left outer join Hr_Person as p on l.Person=p.Id 
where @dateFrom<=l.Date2 and l.Date1<@dateTo {0} order by l.Date1", sql_where);

        list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@personId", personId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@dateFrom", dateFrom, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@dateTo", dateTo, SqlDbType.NVarChar, 10));

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void HR_Leave_getDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int jobId = SafeValue.SafeInt(job["Id"], 0);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select *
,(select top 1 Name from Hr_Person where Id=Person) as PersonName 
,(select top 1 Name from Hr_Person where Id=ApproveBy) as ApproveName
from Hr_Leave
where Id=@Id");
        list.Add(new ConnectSql_mb.cmdParameters("@Id", jobId, SqlDbType.Int));

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Add(new DataColumn("balance", typeof(int)));
            string LeaveType = dt.Rows[0]["LeaveType"].ToString();
            int Person = SafeValue.SafeInt(dt.Rows[0]["Person"], 0);
            int blc = C2.HrLeaveTmp.getBalanceDays(Person, LeaveType, DateTime.Now.Year);
            dt.Rows[0]["balance"] = blc;
        }
        context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void HR_Leave_detail_save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int jobId = SafeValue.SafeInt(job["Id"], 0);
        int applyBy = SafeValue.SafeInt(job["Person"], 0);
        string LeaveType = SafeValue.SafeString(job["LeaveType"]);
        string Date1 = SafeValue.SafeString(job["Date1"]);
        string Time1 = SafeValue.SafeString(job["Time1"]);
        string Date2 = SafeValue.SafeString(job["Date2"]);
        string Time2 = SafeValue.SafeString(job["Time2"]);
        string Remark = SafeValue.SafeString(job["Remark"]);
        if (Time1 == "")
        {
            Time1 = "AM";
        }
        if (Time2 == "")
        {
            Time2 = "PM";
        }



        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string sql = string.Format(@"update Hr_Leave
set Person=@PersonBy,LeaveType=@LeaveType,Date1=@Date1,Time1=@Time1,Date2=@Date2,Time2=@Time2,Remark=@Remark,
Days=datediff(day,@Date1,@Date2)+1+(case @Time1 when 'AM' then 0 else -0.5 end)+(case @Time2 when 'PM' then 0 else -0.5 end)
where Id=@Id");
        list.Add(new ConnectSql_mb.cmdParameters("@Id", jobId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@PersonBy", applyBy, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@LeaveType", LeaveType, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Date1", Date1, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Time1", Time1, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Date2", Date2, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Time2", Time2, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);

        Common.WriteJsonP(result.status, Common.StringToJson(result.context));
    }

    [WebMethod]
    public void HR_Leave_detail_addNew(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int applyBy = SafeValue.SafeInt(job["Person"], 0);
        string LeaveType = SafeValue.SafeString(job["LeaveType"]);
        string Date1 = SafeValue.SafeString(job["Date1"]);
        string Time1 = SafeValue.SafeString(job["Time1"]);
        string Date2 = SafeValue.SafeString(job["Date2"]);
        string Time2 = SafeValue.SafeString(job["Time2"]);
        string Remark = SafeValue.SafeString(job["Remark"]);
        if (Time1 == "")
        {
            Time1 = "AM";
        }
        if (Time2 == "")
        {
            Time2 = "PM";
        }



        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string sql = string.Format(@"insert into Hr_Leave
(Person,LeaveType,Date1,Time1,Date2,Time2,Remark,Days,ApplyDateTime,ApproveStatus) values(
@PersonBy,@LeaveType,@Date1,@Time1,@Date2,@Time2,@Remark,datediff(day,@Date1,@Date2)+1+(case @Time1 when 'AM' then 0 else -0.5 end)+(case @Time2 when 'PM' then 0 else -0.5 end),getdate(),'Draft')");
        list.Add(new ConnectSql_mb.cmdParameters("@PersonBy", applyBy, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@LeaveType", LeaveType, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Date1", Date1, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Time1", Time1, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Date2", Date2, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Time2", Time2, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);

        Common.WriteJsonP(result.status, Common.StringToJson(result.context));
    }

    [WebMethod]
    public void HR_Leave_view_changeStatus(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int jobId = SafeValue.SafeInt(job["Id"], 0);
        string ApproveStatus = SafeValue.SafeString(job["status"]);
        string ApproveBy = SafeValue.SafeString(job["user"]);
        string ApproveRemark = SafeValue.SafeString(job["remark"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string sql = string.Format(@"update Hr_Leave
set ApproveStatus=@ApproveStatus,ApproveDate=getdate(),ApproveTime=replace(convert(nvarchar(5),getdate(),114),':',''),
ApproveBy=isnull((select top 1 Id from Hr_person where Name=@ApproveBy),0),ApproveRemark=@ApproveRemark 
where Id=@Id");
        list.Add(new ConnectSql_mb.cmdParameters("@Id", jobId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@ApproveStatus", ApproveStatus, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ApproveBy", ApproveBy, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ApproveRemark", ApproveRemark, SqlDbType.NVarChar, 300));
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);

        Common.WriteJsonP(result.status, Common.StringToJson(result.context));
    }



    [WebMethod]
    public void HR_Leave_calendar_getData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);
        string role = job["role"].ToString().Trim().ToLower();
        string user = job["user"].ToString();
        string dateFrom = job["from"].ToString();
        string dateTo = job["to"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        if (role != "admin" && role != "hr")
        {
            sql_where = " and l.Person<>isnull((select top 1 Id from Hr_Person where name=@user),0)";
        }
        string sql = string.Format(@"with daysOfMonth as(
select * from func_getCalendarByDay_w(@dateFrom,@dateTo)
),
tb0 as(
select l.*,p.Name as PersonName from Hr_Leave as l
left outer join Hr_Person as p on l.Person=p.Id 
where datediff(MONTH,@dateFrom,l.Date1)<=0 and datediff(MONTH,@dateFrom,l.Date2)>=0 
)
select *,(select count(*) from tb0 where datediff(day,tb_days.date,Date1)<=0 and datediff(day,tb_days.date,Date2)>=0) as cc
from daysOfMonth as tb_days", sql_where);

        list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@dateFrom", dateFrom, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@dateTo", dateTo, SqlDbType.NVarChar, 10));

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void HR_Leave_mylist_getdata(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);
        string user = job["user"].ToString();
        string dateFrom = job["from"].ToString();
        string dateTo = job["to"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select top 1 Id from Hr_Person where name=@user");
        list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
        int personId = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        string data_list = "[]";
        string data_al = "0";
        if (personId > 0)
        {
            sql = string.Format(@"select l.*,p.Name as PersonName from Hr_Leave as l
left outer join Hr_Person as p on l.Person=p.Id 
where @dateFrom<=l.Date2 and l.Date1<@dateTo and l.Person=@PersonId order by l.Date1");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@dateFrom", dateFrom, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@dateTo", dateTo, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@PersonId", personId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            data_list = Common.DataTableToJson(dt);
            data_al = SafeValue.SafeString(C2.HrLeaveTmp.getBalanceDays(personId, "AL", DateTime.Now.Year));
        }
        context = string.Format(@"{0}list:{2},AL:{3}{1}", "{", "}", data_list, data_al);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void HR_Leave_detail_myaddNew(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string user = SafeValue.SafeString(job["user"]);
        string LeaveType = SafeValue.SafeString(job["LeaveType"]);
        string Date1 = SafeValue.SafeString(job["Date1"]);
        string Time1 = SafeValue.SafeString(job["Time1"]);
        string Date2 = SafeValue.SafeString(job["Date2"]);
        string Time2 = SafeValue.SafeString(job["Time2"]);
        string Remark = SafeValue.SafeString(job["Remark"]);
        if (Time1 == "")
        {
            Time1 = "AM";
        }
        if (Time2 == "")
        {
            Time2 = "PM";
        }

        bool status = false;
        string context = "";

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
        string sql = string.Format(@"select top 1 Id from Hr_Person where name=@user");
        int applyBy = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        if (applyBy > 0)
        {
            sql = string.Format(@"insert into Hr_Leave
(Person,LeaveType,Date1,Time1,Date2,Time2,Remark,Days,ApplyDateTime,ApproveStatus) values(
@PersonBy,@LeaveType,@Date1,@Time1,@Date2,@Time2,@Remark,datediff(day,@Date1,@Date2)+1+(case @Time1 when 'AM' then 0 else -0.5 end)+(case @Time2 when 'PM' then 0 else -0.5 end),getdate(),'Draft')");
            list.Add(new ConnectSql_mb.cmdParameters("@PersonBy", applyBy, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@LeaveType", LeaveType, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@Date1", Date1, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@Time1", Time1, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@Date2", Date2, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@Time2", Time2, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
            ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);
            if (result.status)
            {
                status = true;
            }
        }
        else
        {
            context = "Requair add this user in HR Employee";
        }
        Common.WriteJsonP(status, Common.StringToJson(context));
    }




    [WebMethod]
    public void Attendance_addSave(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string user = SafeValue.SafeString(job["user"]);
        string role = SafeValue.SafeString(job["role"]);
        string type = SafeValue.SafeString(job["type"]);
        string lat = SafeValue.SafeString(job["lat"]);
        string lng = SafeValue.SafeString(job["lng"]);


        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Platform_isMobile();
        lg.Controller = user;
        lg.Lat = lat;
        lg.Lng = lng;
        lg.Note1Type = "Attendance";
        lg.Note1 = type;
        lg.log();

        Common.WriteJsonP(true, Common.StringToJson(""));
    }
}