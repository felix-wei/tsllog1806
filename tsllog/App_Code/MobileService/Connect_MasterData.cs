using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_MasterData 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect_MasterData : System.Web.Services.WebService
{

    public Connect_MasterData()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    #region Warehouse/location

    [WebMethod]
    public void EGL_MasterData_Warehouse_List(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Code as c,Name as d from ref_warehouse");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_MasterData_WarehouseLoation_List_ByCode(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        string status = "0";
        string context = Common.StringToJson("");

        string no = SafeValue.SafeString(job["no"]);
        //if (no.Length < 3)
        //{
        //    context = Common.StringToJson("The search code need more than 3 letter!");
        //}
        //else
        //{
        //    string sql = string.Format(@"select Code,WarehouseCode,ZoneCode from ref_location where Loclevel='Unit' and WarehouseCode=@WarehouseCode and Code like @Code");
        //    list = new List<ConnectSql_mb.cmdParameters>();
        //    list.Add(new ConnectSql_mb.cmdParameters("@Code", no + "%", SqlDbType.NVarChar, 100));
        //    list.Add(new ConnectSql_mb.cmdParameters("@WarehouseCode", job["wh"], SqlDbType.NVarChar, 100));
        //    DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        //    context = Common.DataTableToJson(dt);
        //    status = "1";
        //}
        string sql = string.Format(@"select Code,WarehouseCode,ZoneCode from ref_location where Loclevel='Unit' and WarehouseCode=@WarehouseCode and Code like @Code");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Code", no + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@WarehouseCode", job["wh"], SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        status = "1";
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_MasterData_Loation_AllList_ByCode(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        string status = "0";
        string context = Common.StringToJson("");

        string no = SafeValue.SafeString(job["no"]);
        string sql = string.Format(@"select Code as c,WarehouseCode as d,ZoneCode from ref_location where Loclevel='Unit' and Code like @Code order by WarehouseCode");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Code", no + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        status = "1";
        Common.WriteJsonP(status, context);
    }


    #endregion

    #region Customer
    [WebMethod]
    public void EGL_MasterData_Customer(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select PartyId as c,Name as d from XXParty where Status='USE' and IsCustomer=1");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void EGL_MasterData_Customer_ByCode(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        string sql = string.Format(@"select PartyId as c,Name as d,Address as addr from XXParty where Status='USE' and IsCustomer=1 and (PartyId like @Code or Name like @Code)");
        list.Add(new ConnectSql_mb.cmdParameters("@Code", SafeValue.SafeString(job["no"]) + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    #endregion

    #region Incentive
    [WebMethod]
    public void EGL_MasterData_Incentive(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Price1 as c,Type1 as d from CTM_MastData where [Type]='incentive' order by Price1");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void EGL_MasterData_Cost_DP(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string context = Common.StringToJson("");
        string sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='INCENTIVE'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        //DataTable dt = new DataTable();
        //dt.Columns.Add("c", typeof(string));
        //dt.Columns.Add("n", typeof(string));
        //DataRow dr = dt.NewRow();
        //dr["c"] = "Trip";
        //dr["n"] = "Trip";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "OverTime";
        //dr["n"] = "OverTime";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "Standby";
        //dr["n"] = "Standby";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "PSA";
        //dr["n"] = "PSA";
        //dt.Rows.Add(dr);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);

    }

    [WebMethod]
    public void EGL_MasterData_Cost_CL(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string context = Common.StringToJson("");
        string sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='CLAIMS'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);

        //DataTable dt = new DataTable();
        //dt.Columns.Add("c", typeof(string));
        //dt.Columns.Add("n", typeof(string));
        //DataRow dr = dt.NewRow();
        //dr["c"] = "DHC";
        //dr["n"] = "DHC";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "WEIGHING";
        //dr["n"] = "WEIGHING";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "WASHING";
        //dr["n"] = "WASHING";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "REPAIR";
        //dr["n"] = "REPAIR";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "DETENTION";
        //dr["n"] = "DETENTION";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "DEMURRAGE";
        //dr["n"] = "DEMURRAGE";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "LIFT ON/OFF";
        //dr["n"] = "LIFT ON/OFF";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "C/SHIPMENT";
        //dr["n"] = "C/SHIPMENT";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "EMF";
        //dr["n"] = "EMF";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["c"] = "OTHER";
        //dr["n"] = "OTHER";
        //dt.Rows.Add(dr);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);

    }


    #endregion

    #region Driver/Trailer/Towhead/ParkingLot

    [WebMethod]
    public void EGL_MasterData_Driver(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Id,Code as c,TowheaderCode as t from ctm_driver where StatusCode='Active'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    //[WebMethod]
    //public void EGL_MasterData_Towhead(string info)
    //{
    //    //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

    //    string status = "1";
    //    string context = Common.StringToJson("");

    //    string sql = string.Format(@"select VehicleCode as c from ref_Vehicle where VehicleStatus='Active'");
    //    DataTable dt = ConnectSql_mb.GetDataTable(sql);
    //    context = Common.DataTableToJson(dt);
    //    Common.WriteJsonP(status, context);
    //}

    [WebMethod]
    public void EGL_MasterData_Trailer(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Code as c from CTM_MastData where [Type]='chessis' and isnull(Type1,'')<>'InActive'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void EGL_MasterData_ParkingLot(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Code as c,Address as ad from PackingLot");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_MasterData_Towhead(string info)
    {
        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Id,VehicleCode as c,Date1 as dm from ref_Vehicle where VehicleStatus='Active'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_MasterData_Towhead_GetDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";

        //select Id,VehicleCode,VehicleType,VehicleStatus,ContractNo,ContractType from ref_Vehicle
        string sql = string.Format(@"select * from ref_Vehicle where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);
        string serviceLog = "[]";
        if (dt.Rows.Count > 0)
        {
            string VehicleCode = dt.Rows[0]["VehicleCode"].ToString();

            sql = string.Format(@"select top 10 Id,DriverCode,EventDate From ref_VehicleLog where VehicleCode=@VehicleCode order by EventDate desc");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@VehicleCode", VehicleCode, SqlDbType.NVarChar, 100));
            dt = ConnectSql_mb.GetDataTable(sql, list);
            serviceLog = Common.DataTableToJson(dt);
        }
        string context = string.Format(@"{0}mast:{2},serviceLog:{3}{1}", "{", "}", mast, serviceLog);
        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void EGL_MasterData_Towhead_SaveDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "0";
        string context = Common.StringToJson("");

        int Id = SafeValue.SafeInt(job["Id"], 0);

        string sql = string.Format(@"update ref_Vehicle set VehicleCode=@VehicleCode,VehicleType=@VehicleType,VehicleStatus=@VehicleStatus,ContractNo=@ContractNo,ContractType=@ContractType where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@VehicleCode", job["VehicleCode"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@VehicleType", job["VehicleType"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@VehicleStatus", job["VehicleStatus"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContractNo", job["ContractNo"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContractType", job["ContractType"], SqlDbType.NVarChar, 100));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            status = "1";
        }
        Common.WriteJsonP(status, context);
    }




    [WebMethod]
    public void EGL_MasterData_ServiceLog(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");
        string dateFrom = job["from"].ToString();
        string dateTo = job["to"].ToString();

        string sql = string.Format(@"select Id,DriverCode,VehicleCode,EventType,EventDate From ref_VehicleLog
where EventDate>=@dateFrom and EventDate<@dateTo");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@dateFrom", dateFrom, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@dateTo", dateTo, SqlDbType.NVarChar, 8));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_MasterData_ServiceLog_GetDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select l.*,isnull(v.Id,0) as vehicleId from ref_VehicleLog as l
left outer join ref_Vehicle as v on l.VehicleCode=v.VehicleCode
where l.Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void EGL_MasterData_ServiceLog_SaveDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "0";
        string context = Common.StringToJson("");

        int Id = SafeValue.SafeInt(job["Id"], 0);

        string sql = string.Format(@"update ref_VehicleLog set VehicleCode=@VehicleCode,DriverCode=@DriverCode,EventStatus=@EventStatus,EventDate=@EventDate,EventType=@EventType,TotalPrice=@TotalPrice,Description=@Description where Id=@Id");
        if (Id == 0)
        {
            sql = string.Format(@"insert into ref_VehicleLog (VehicleCode,DriverCode,EventStatus,EventDate,EventType,TotalPrice,Description) values(@VehicleCode,@DriverCode,@EventStatus,@EventDate,@EventType,@TotalPrice,@Description)");
        }
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@EventDate", job["EventDate"], SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@VehicleCode", job["VehicleCode"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", job["DriverCode"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@EventStatus", job["EventStatus"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@EventType", job["EventType"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@TotalPrice", SafeValue.SafeDecimal(job["TotalPrice"].ToString()), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Description", job["Description"], SqlDbType.NVarChar, 300));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            status = "1";
        }
        Common.WriteJsonP(status, context);
    }
    #endregion



    [WebMethod]
    public void EGL_MasterData_PackType_List(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Code as c,Description as n from XXUom where CodeType='2' order by Code");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }



    [WebMethod]
    public void EGL_MasterData_SkuCode(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"SELECT Id,Code as c,Name as d FROM ref_product where StatusCode='USE'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }


    [WebMethod]
    public void EGL_MasterData_JobNo(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string clientId = SafeValue.SafeString(job["clientId"]);

        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        string sql_where = "";
        if (clientId.Length > 0)
        {
            sql_where = "and ClientId=@ClientId";
        }

        string sql = string.Format(@"select JobNo as c,JobType+'/'+ClientId as d from Ctm_job where JobNo like @Code {0} order by JobNo desc",sql_where);
        list.Add(new ConnectSql_mb.cmdParameters("@Code", "%" + SafeValue.SafeString(job["no"]) + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", clientId, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void EGL_MasterData_Product(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        

        string sql = string.Format(@"select Code as c,Name as d  from ref_product where Code like @Code order by c");
        list.Add(new ConnectSql_mb.cmdParameters("@Code", "%" + SafeValue.SafeString(job["no"]) + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    #region quotation


    [WebMethod]
    public void EGL_MasterData_ChgCode_ByCode(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        string sql = string.Format(@"select ChgcodeId as c,ChgcodeDes as d,ArCode,ApCode from XXChgCode where ChgcodeId like @Code");
        list.Add(new ConnectSql_mb.cmdParameters("@Code", "%"+SafeValue.SafeString(job["no"]) + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    #endregion


    [WebMethod]
    public void EGL_MasterData_DeliveryResult(string info)
    {
        //JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Id,Remark as c from ctm_mastdata where type='doresult'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    #region HR


    [WebMethod]
    public void EGL_MasterData_HrPerson(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");
        string role = job["role"].ToString().Trim().ToLower();
        string user = job["user"].ToString();

        string sql_where = "";
        if (role != "admin" && role != "hr")
        {
            sql_where = " where Name=@Name";
        }

        string sql = string.Format(@"select Id,Name as c from Hr_Person {0} order by Name", sql_where);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Name", user, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    #endregion
}
