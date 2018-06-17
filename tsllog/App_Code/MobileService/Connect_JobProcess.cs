using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Connect_JobProcess
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Connect_JobProcess : System.Web.Services.WebService
{

    public Connect_JobProcess()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public void Calendar_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string sql_where = "";

        string sql = string.Format(@"select * from job_process where datediff(day,DatePlan,@date)=0", sql_where);
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void Calendar_GetAddNewLotItem(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string lotNo = SafeValue.SafeString(job["no"]);

        DataTable dt = C2.JobHouse.getStockBalance(lotNo, "", "", "", "", "", "", "","","","");
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }


    [WebMethod]
    public void JobProcess_GetView(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int Id = SafeValue.SafeInt(job["no"], 0);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.Int));

        string sql = string.Format(@"select p.*,h.BkgSKuCode,h.BkgSkuQty from job_process as p
left outer join job_house as h on p.jobno=h.jobno and p.houseId=h.Id 
where p.Id=@Id");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataRowToJson(dt);
        Common.WriteJsonP(true, context);
    }


    [WebMethod]
    public void JobProcess_changeStatus(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int Id = SafeValue.SafeInt(job["no"], 0);
        string ProcessStatus = SafeValue.SafeString(job["status"]);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@ProcessStatus", ProcessStatus, SqlDbType.NVarChar, 100));

        string sql = string.Format(@"update job_process set ProcessStatus=@ProcessStatus,DateProcess=getdate() where Id=@Id");

        bool status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        Common.WriteJsonP(status, Common.StringToJson(""));
    }
    [WebMethod]
    public void JobProcess_View_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int Id = SafeValue.SafeInt(job["no"], 0);
        string Remark2 = SafeValue.SafeString(job["Remark2"]);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark2", Remark2, SqlDbType.NVarChar, 100));

        string sql = string.Format(@"update job_process set Remark2=@Remark2 where Id=@Id");

        bool status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        Common.WriteJsonP(status, Common.StringToJson(""));
    }


    [WebMethod]
    public void JobProcess_GetAddNewData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int cargoId = SafeValue.SafeInt(job["no"], 0);
        string sql = string.Format(@"select * from job_house where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", cargoId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataRowToJson(dt);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void JobProcess_AddNew_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int HouseId = SafeValue.SafeInt(job["HouseId"], 0);
        string JobNo = SafeValue.SafeString(job["JobNo"]);
        string LotNo = SafeValue.SafeString(job["LotNo"]);
        string ProcessStatus = SafeValue.SafeString(job["ProcessStatus"]);

        int Qty = SafeValue.SafeInt(job["Qty"], 0);
        string DatePlan = SafeValue.SafeString(job["DatePlan"]);
        string ProcessType = SafeValue.SafeString(job["ProcessType"]);
        string LocationCode = SafeValue.SafeString(job["LocationCode"]);
        string Remark1 = SafeValue.SafeString(job["Remark1"]);
        string Remark2 = SafeValue.SafeString(job["Remark2"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@HouseId", HouseId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@LotNo", LotNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ProcessStatus", ProcessStatus, SqlDbType.NVarChar, 30));

        list.Add(new ConnectSql_mb.cmdParameters("@Qty", Qty, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@DatePlan", DatePlan, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@ProcessType", ProcessType, SqlDbType.NVarChar, 50));
        list.Add(new ConnectSql_mb.cmdParameters("@LocationCode", LocationCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark1", Remark1, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark2", Remark2, SqlDbType.NVarChar, 100));

        string sql = string.Format(@"insert into job_process (HouseId,JobNo,LotNo,ProcessStatus,Qty,DateEntry,DatePlan,DateProcess,DateInspect,ProcessType,LocationCode,Remark1,Remark2)
values(@HouseId,@JobNo,@LotNo,@ProcessStatus,@Qty,getdate(),@DatePlan,@DatePlan,@DatePlan,@ProcessType,@LocationCode,@Remark1,@Remark2)");

        bool status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        Common.WriteJsonP(status, Common.StringToJson(""));
    }

}
