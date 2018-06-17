using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Connect_EGL_DriverLog
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Connect_EGL_DriverLog : System.Web.Services.WebService
{

    public Connect_EGL_DriverLog()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }




    [WebMethod]
    public void UserLogin_DriverTowhead(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject jo = (JObject)JsonConvert.DeserializeObject(info_);
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format("select TowheaderCode as Towhead from CTM_Driver where Code=@Driver");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Driver", jo["user"], SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_DriverCash_GetList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");

        string role = job["role"].ToString();
        string user = job["user"].ToString();
        string no = job["no"].ToString();
        string from = job["from"].ToString();
        string to = job["to"].ToString();

        string sql = string.Format(@"select Id,EventDate,EventType,DriverCode,TotalAmount from Ref_DriverCash where (EventType='Allowance' or EventType='Deduction') and datediff(d,EventDate,@from)<=0 and datediff(d,EventDate,@to)>=0");
        if (role.Equals("Driver"))
        {
            sql += " and DriverCode=@user";
        }
        else
        {
            sql += " and DriverCode like @DriverCode";
        }
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", "%" + no + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@from", from, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@to", to, SqlDbType.NVarChar, 10));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_DriverCash_GetDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select dc.*,isnull(d.Id,0) as driverId from Ref_DriverCash as dc
left outer join ctm_driver as d on dc.DriverCode=d.Code
where dc.Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void EGL_DriverCash_SaveDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "0";
        string context = Common.StringToJson("");

        int Id = SafeValue.SafeInt(job["Id"], 0);
        string DriverCode = job["DriverCode"].ToString();
        string EventDate = job["EventDate"].ToString();
        string EventType = job["EventType"].ToString();
        decimal TotalAmount = SafeValue.SafeDecimal(job["TotalAmount"].ToString(), 0);
        string Description = job["Description"].ToString();

        string sql = "";
        sql = string.Format(@"update Ref_DriverCash set DriverCode=@DriverCode,EventDate=@EventDate,EventType=@EventType,TotalAmount=@TotalAmount,Description=@Description where Id=@Id");
        if (Id == 0)
        {
            sql = string.Format(@"insert into Ref_DriverCash (DriverCode,EventDate,EventType,TotalAmount,Description) values(@DriverCode,@EventDate,@EventType,@TotalAmount,@Description)");
        }
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", DriverCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@EventDate", EventDate, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@EventType", EventType, SqlDbType.NVarChar, 20));
        list.Add(new ConnectSql_mb.cmdParameters("@TotalAmount", TotalAmount, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Description", Description, SqlDbType.NVarChar, 300));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            status = "1";

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            lg.ActionLevel = "DriverCash";
            lg.ActionId = Id;
            lg.Remark = "Driver Cash Save";
            lg.log();

        }
        Common.WriteJsonP(status, context);
    }



    [WebMethod]
    public void EGL_Driver_GetList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");

        string role = job["role"].ToString();
        string user = job["user"].ToString();
        string no = job["no"].ToString();

        string sql = string.Format(@"select Id,Code,Name,Tel,ICNo,Remark from ctm_driver where 1=1");
        if (no != null && no.Length > 0)
        {
            sql += " and Code like @no";
        }
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@no", "%" + no + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_Driver_GetDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        int Id = SafeValue.SafeInt(job["Id"], 0);
        string sql = string.Format(@"select Id,Code,Name,Tel,ICNo,Remark,Isstaff,TowheaderCode,ServiceLevel,StatusCode,TeamNo,LicenseExpiry 
from ctm_driver where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt, true);
        string cashList = "[]";
        if (dt.Rows.Count > 0)
        {
            string Driver = dt.Rows[0]["Code"].ToString();

            sql = string.Format(@"select top 10 Id,EventDate,EventType from Ref_DriverCash where (EventType='Allowance' or EventType='Deduction') and DriverCode=@DriverCode
order by EventDate desc");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", Driver, SqlDbType.NVarChar, 100));
            dt = ConnectSql_mb.GetDataTable(sql, list);
            cashList = Common.DataTableToJson(dt);
        }
        string context = string.Format(@"{0}mast:{2},cashList:{3}{1}", "{", "}", mast, cashList);
        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void EGL_Driver_SaveDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "0";
        string context = Common.StringToJson("");

        int Id = SafeValue.SafeInt(job["Id"], 0);
        string Code = job["Code"].ToString();
        string Name = job["Name"].ToString();
        string Tel = job["Tel"].ToString();
        string ICNo = job["ICNo"].ToString();
        string Remark = job["Remark"].ToString();
        string Isstaff = job["Isstaff"].ToString();
        string TowheaderCode = job["TowheaderCode"].ToString();
        string ServiceLevel = job["ServiceLevel"].ToString();
        string StatusCode = job["StatusCode"].ToString();
        string TeamNo = job["TeamNo"].ToString();
        string LicenseExpiry = job["LicenseExpiry"].ToString();

        string sql = "";
        sql = string.Format(@"update ctm_driver set 
Code=@Code,Name=@Name,Tel=@Tel,ICNo=@ICNo,Remark=@Remark,Isstaff=@Isstaff,TowheaderCode=@TowheaderCode,ServiceLevel=@ServiceLevel,StatusCode=@StatusCode,TeamNo=@TeamNo,LicenseExpiry=@LicenseExpiry 
where Id=@Id");
        if (Id == 0)
        {
            sql = string.Format(@"insert into ctm_driver (Code,Name,Tel,ICNo,Remark,Isstaff,TowheaderCode,ServiceLevel,StatusCode,TeamNo,LicenseExpiry) 
values (@Code,@Name,@Tel,@ICNo,@Remark,@Isstaff,@TowheaderCode,@ServiceLevel,@StatusCode,@TeamNo,@LicenseExpiry)");
        }
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@Code", Code, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Name", Name, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Tel", Tel, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@ICNo", ICNo, SqlDbType.NVarChar, 50));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Isstaff", Isstaff, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@TowheaderCode", TowheaderCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ServiceLevel", ServiceLevel, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", StatusCode, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@TeamNo", TeamNo, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@LicenseExpiry", LicenseExpiry, SqlDbType.NVarChar, 8));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            status = "1";

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            lg.ActionLevel = "Driver";
            lg.ActionId = Id;
            lg.Remark = "Driver Save";
            lg.log();

        }
        Common.WriteJsonP(status, context);
    }


}
