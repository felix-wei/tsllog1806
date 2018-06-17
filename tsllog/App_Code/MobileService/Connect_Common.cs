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
/// Connect_Common 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect_Common : System.Web.Services.WebService
{

    public Connect_Common()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    #region User Login

    [WebMethod]
    public void UserLogin_Login(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject jo = (JObject)JsonConvert.DeserializeObject(info_);
        bool status = false;
        string context = Common.StringToJson("");
        string account = jo["account"].ToString();
        string pw = jo["password"].ToString();

        if (account == null || account.Length <= 0)
        {
            context = Common.StringToJson("Request Account");
        }
        else
        {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = null;
            string sql = string.Format(@"select * from [user] where IsActive=1 and Tel=@Tel");
            cpar = new ConnectSql_mb.cmdParameters("@Tel", account, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                Encryption.EncryptClass encrypt = new Encryption.EncryptClass();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string PW_code = SafeValue.SafeString(encrypt.DESEnCode(dt.Rows[i]["Name"].ToString(), pw), "");
                    if (PW_code.Equals(dt.Rows[i]["Pwd"].ToString()))
                    {
                        sql = string.Format(@"select * from [user] where SequenceId=@SequenceId");
                        list = new List<ConnectSql_mb.cmdParameters>();
                        cpar = new ConnectSql_mb.cmdParameters("@SequenceId", dt.Rows[i]["SequenceId"], SqlDbType.Int);
                        list.Add(cpar);
                        DataTable dt1 = ConnectSql_mb.GetDataTable(sql, list);
                        status = true;
                        context = Common.DataRowToJson(dt1);

                        //===========log
                        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                        lg.Platform_isMobile();
                        lg.Controller = dt.Rows[i]["Name"].ToString();
                        lg.Remark = dt.Rows[i]["Name"] + " Login";
                        lg.ActionLevel = "USER";
                        lg.log();
                        break;
                    }
                }
                if (!status)
                {
                    context = Common.StringToJson("Password error");
                }
            }
            else
            {
                sql = string.Format(@"select * from [user] where IsActive=0 and Tel=@Tel");
                dt = ConnectSql_mb.GetDataTable(sql, list);
                if (dt.Rows.Count > 0)
                {
                    context = Common.StringToJson("Expire Account");
                }
                else
                {
                    context = Common.StringToJson("Account is not Exist");
                }
            }
        }
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void UserLogin_Login_auto(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject jo = (JObject)JsonConvert.DeserializeObject(info_);
        bool status = false;
        string context = Common.StringToJson("");
        string account = jo["account"].ToString();
        string pw = jo["password"].ToString();

        if (account == null || account.Length <= 0)
        {
            context = Common.StringToJson("Request Account");
        }
        else
        {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = null;
            string sql = string.Format(@"select top 1 * from [user] where IsActive=1 and Tel=@Tel and Pwd=@Pwd");
            cpar = new ConnectSql_mb.cmdParameters("@Tel", account, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Pwd", pw, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                status = true;
                context = Common.DataRowToJson(dt);
                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = dt.Rows[0]["Name"].ToString();
                lg.Remark = dt.Rows[0]["Name"] + " Login";
                lg.ActionLevel = "USER";
                lg.ActionType = "Login";
                lg.log();
            }
            else
            {
                context = Common.StringToJson("Password error");
            }
        }
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void UserLogin_CheckVersion(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject jo = (JObject)JsonConvert.DeserializeObject(info_);
        bool status = true;
        string context = Common.StringToJson("");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string sql = string.Format(@"with tb_user as(
select distinct Code from Modules_User where [User]=@user
),
tb_list_gl as(
select ROW_NUMBER()over(PARTITION by Code order by Id desc) rowId,* from Modules_List where [status]='gl'
),
tb_list as (
select l.* from tb_user as u 
left outer join tb_list_gl as l on u.Code=l.Code
where l.rowId=1 
),
tb_list_par as (
select l1.* from tb_list as l 
left outer join Modules_List_Par as p on l.Code=p.Code 
left outer join tb_list_gl as l1 on p.ParCode=l1.Code 
where p.ParCode is not null
),
tb_union as (
select * from tb_list
union
select * from tb_list_par
),
tb_mds as (
select ROW_NUMBER()over(partition by Code order by rowId) as rowId1, * From tb_union 
)
select Id,Code,Name,Display from tb_mds where rowId1=1");
        cpar = new ConnectSql_mb.cmdParameters("@user", jo["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void UserLogin_CheckUserIcon(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject jo = (JObject)JsonConvert.DeserializeObject(info_);
        string status = "0";
        string context = Common.StringToJson("");
        string account = SafeValue.SafeString(jo["User"]).Trim();
        if (account.Length > 0)
        {
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos", "UserIcon", account + ".jpg")))
            {
                status = "1";
            }
        }
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void UserLogin_DriverTowhead(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject jo = (JObject)JsonConvert.DeserializeObject(info_);
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format("select Towhead from CTM_DriverLog where datediff(d,date,getdate())=0 and IsActive='Y' and Driver=@Driver");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Driver", jo["user"], SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);
    }

    #endregion

    #region menu

    //=========== old function
    [WebMethod]
    public void Menu_GetData(string info)
    {

        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string user = SafeValue.SafeString(job["User"], "");
        string role = "";
        if (user.Equals("admin"))
        {
            role = "admin";
        }
        else
        {
            role = SafeValue.SafeString(job["Role"], "").ToLower();
        }

        string status = "1";
        string context = Common.StringToJson("");

        DataTable dt = new DataTable();
        dt.Columns.Add("link", typeof(string));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("icon", typeof(string));
        DataRow dr = null;
        string menu0 = "[]";
        string menu1 = "[]";


        //dr = dt.NewRow();
        //dr["link"] = "EGL_JobTrip_Schedule";
        //dr["name"] = "Job Schedule";
        //dr["icon"] = "ion-calendar";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["link"] = "EGL_JobTrip_List";
        //dr["name"] = "Job Search";
        //dr["icon"] = "ion-ios-paper-outline";
        //dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //dr["link"] = "EGL_JobTrip_TrailerPick";
        //dr["name"] = "Trailer Pick";
        //dr["icon"] = "ion-log-out";
        //dt.Rows.Add(dr);
        //dr = dt.NewRow();
        //dr["link"] = "EGL_JobTrip_TrailerPark";
        //dr["name"] = "Trailer Park";
        //dr["icon"] = "ion-log-in";
        //dt.Rows.Add(dr);

        //menu0 = Common.DataTableToJson(dt);

        //dt.Clear();
        //dr = dt.NewRow();
        //dr["link"] = "EGL_Incentive_List";
        //dr["name"] = "Incentive List";
        //dr["icon"] = "ion-cash";
        //dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //dr["link"] = "EGL_ParkingLot";
        //dr["name"] = "ParkingLot";
        //dr["icon"] = "ion-model-s";
        //dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //dr["link"] = "EGL_FuelLog_List";
        //dr["name"] = "Fuel Log";
        //dr["icon"] = "ion-soup-can-outline";
        //dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //dr["link"] = "EGL_Mileage_List";
        //dr["name"] = "Mileage";
        //dr["icon"] = "ion-compass";
        //dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //dr["link"] = "EGL_JobTrip_ContList";
        //dr["name"] = "Shifting";
        //dr["icon"] = "ion-arrow-swap";
        //dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //dr["link"] = "EGL_IssueReport_List";
        //dr["name"] = "Issue Report";
        //dr["icon"] = "ion-clipboard";
        //dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //dr["link"] = "EGL_IssueReport_List_PrimeMoverTyre";
        //dr["name"] = "PrimeMover Tyre";
        //dr["icon"] = "ion-help-buoy";
        //dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //dr["link"] = "EGL_IssueReport_List_TrailerTyre";
        //dr["name"] = "Trailer Tyre";
        //dr["icon"] = "ion-help-buoy";
        //dt.Rows.Add(dr);

        if (role.Equals("admin"))
        {
            #region admin menu
            //============================= menu0
            dt.Clear();
            dr = dt.NewRow();
            dr["link"] = "CMS_Controller_Schedule";
            dr["name"] = "Controller Schedule";
            dr["icon"] = "ion-calendar";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_JobTrip_Schedule";
            dr["name"] = "Driver Schedule";
            dr["icon"] = "ion-calendar";
            dt.Rows.Add(dr);

            menu0 = Common.DataTableToJson(dt);

            //============================= menu1
            dt.Clear();

            dr = dt.NewRow();
            dr["link"] = "CMS_Controller_DriverTrips";
            dr["name"] = "DS Driver Trips";
            dr["icon"] = "ion-ios-paper-outline";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "CMS_Controller_Schedule_ContainerStatus";
            dr["name"] = "DS Container Status";
            dr["icon"] = "ion-ios-paper-outline";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "CMS_Controller_Schedule_TrailerStatus";
            dr["name"] = "DS Trailer Status";
            dr["icon"] = "ion-ios-paper-outline";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_ParkingLot";
            dr["name"] = "DS ParkingLot";
            dr["icon"] = "ion-ios-paper-outline";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_JobTrip_List";
            dr["name"] = "Job Search";
            dr["icon"] = "ion-ios-paper";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["link"] = "EGL_JobTrip_TrailerPick";
            //dr["name"] = "Trailer Pick";
            //dr["icon"] = "ion-log-out";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["link"] = "EGL_JobTrip_TrailerPark";
            //dr["name"] = "Trailer Park";
            //dr["icon"] = "ion-log-in";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_Incentive_List";
            dr["name"] = "Incentive List";
            dr["icon"] = "ion-cash";
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["link"] = "EGL_FuelLog_List";
            dr["name"] = "Fuel Log";
            dr["icon"] = "ion-soup-can-outline";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_Mileage_List";
            dr["name"] = "Mileage";
            dr["icon"] = "ion-compass";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["link"] = "EGL_JobTrip_ContList";
            //dr["name"] = "Shifting";
            //dr["icon"] = "ion-arrow-swap";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_IssueReport_List";
            dr["name"] = "Issue Report";
            dr["icon"] = "ion-clipboard";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_IssueReport_List_PrimeMoverTyre";
            dr["name"] = "PrimeMover Tyre";
            dr["icon"] = "ion-help-buoy";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_IssueReport_List_TrailerTyre";
            dr["name"] = "Trailer Tyre";
            dr["icon"] = "ion-help-buoy";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "PENLOG_Map_View";
            dr["name"] = "Map";
            dr["icon"] = "ion-map";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_KPIReport_List_Trip";
            dr["name"] = "KPI-Trip";
            dr["icon"] = "ion-document-text";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_KPIReport_List_TEU";
            dr["name"] = "KPI-TEU";
            dr["icon"] = "ion-document-text";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_KPIReport_List_CUS";
            dr["name"] = "KPI-CUS";
            dr["icon"] = "ion-document-text";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_KPIReport_List_RateCT";
            dr["name"] = "Rate CT";
            dr["icon"] = "ion-document-text";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_DriverLog_DriverCash_List";
            dr["name"] = "Driver Cash";
            dr["icon"] = "ion-cash";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_MasterData_Towhead";
            dr["name"] = "Vehicle maintenance";
            dr["icon"] = "ion-android-bus";
            dt.Rows.Add(dr);

            //========= hr
            dr = dt.NewRow();
            dr["link"] = "EGL_HR_Leave_List";
            dr["name"] = "Leave List";
            dr["icon"] = "ion-person";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_HR_Leave_Calendar";
            dr["name"] = "Leave Calendar";
            dr["icon"] = "ion-calendar";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_HR_Leave_MyList";
            dr["name"] = "My Leave";
            dr["icon"] = "ion-person";
            dt.Rows.Add(dr);
            #endregion
        }
        else
        {
            #region driver menu
            //============================= menu0
            dt.Clear();
            dr = dt.NewRow();
            dr["link"] = "EGL_JobTrip_Schedule";
            dr["name"] = "Job Schedule";
            dr["icon"] = "ion-calendar";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_JobTrip_List";
            dr["name"] = "Job Search";
            dr["icon"] = "ion-ios-paper-outline";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_JobTrip_TrailerPick";
            dr["name"] = "Trailer Pick";
            dr["icon"] = "ion-log-out";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_JobTrip_TrailerPark";
            dr["name"] = "Trailer Park";
            dr["icon"] = "ion-log-in";
            dt.Rows.Add(dr);

            menu0 = Common.DataTableToJson(dt);

            //============================= menu1
            dt.Clear();
            dr = dt.NewRow();
            dr["link"] = "EGL_Incentive_List";
            dr["name"] = "Incentive List";
            dr["icon"] = "ion-cash";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_ParkingLot";
            dr["name"] = "ParkingLot";
            dr["icon"] = "ion-model-s";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_FuelLog_List";
            dr["name"] = "Fuel Log";
            dr["icon"] = "ion-soup-can-outline";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_Mileage_List";
            dr["name"] = "Mileage";
            dr["icon"] = "ion-compass";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_JobTrip_ContList";
            dr["name"] = "Shifting";
            dr["icon"] = "ion-arrow-swap";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_IssueReport_List";
            dr["name"] = "Issue Report";
            dr["icon"] = "ion-clipboard";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_IssueReport_List_PrimeMoverTyre";
            dr["name"] = "PrimeMover Tyre";
            dr["icon"] = "ion-help-buoy";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "EGL_IssueReport_List_TrailerTyre";
            dr["name"] = "Trailer Tyre";
            dr["icon"] = "ion-help-buoy";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["link"] = "CMS_Kit_Clock";
            dr["name"] = "Clock";
            dr["icon"] = "ion-clock";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_DriverLog_DriverCash_List";
            dr["name"] = "Driver Cash";
            dr["icon"] = "ion-cash";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["link"] = "EGL_HR_Leave_MyList";
            dr["name"] = "My Leave";
            dr["icon"] = "ion-person";
            dt.Rows.Add(dr);
            #endregion
        }


        menu1 = Common.DataTableToJson(dt);

        context = string.Format(@"[{0}name:'Job Managerment',list:{2},row:2{1},{0}name:'Reporting',list:{3},row:4{1}]", "{", "}", menu0, menu1);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Menu_GetData_ByType(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string user = job["User"].ToString();
        string role = job["Role"].ToString().ToLower();

        DataTable dt0 = new DataTable();
        //dt0.Columns.Add("link", typeof(string));
        dt0.Columns.Add("name", typeof(string));
        dt0.Columns.Add("icon", typeof(string));
        dt0.Columns.Add("t", typeof(int));
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("link", typeof(string));
        dt1.Columns.Add("name", typeof(string));
        dt1.Columns.Add("icon", typeof(string));
        dt1.Columns.Add("t", typeof(int));

		if(role == "hrmanager")
			role = "admin";
		
        DataRow dr = null;
        switch (role)
        {
            case "admin":
                #region admin
                //================= level0
                //dr = dt0.NewRow();
                //dr["name"] = "Job Management";
                //dr["t"] = 0;
                //dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "Trucking";
                dr["t"] = 1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "Warehouse";
                dr["t"] = 2;
                dt0.Rows.Add(dr);
                //dr = dt0.NewRow();
                //dr["name"] = "Search";
                //dr["t"] = 3;
                //dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "Management";
                dr["t"] = 4;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "Report";
                dr["t"] = 5;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "HR";
                dr["t"] = 6;
                dt0.Rows.Add(dr);

                //============================= level1
                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_Schedule";
                dr["name"] = "Dispatch";
                dr["icon"] = "ion-calendar";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_JobTrip_Schedule";
                dr["name"] = "Trucking";
                dr["icon"] = "ion-calendar";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "ELL_LCLJob_Calendar";
                dr["name"] = "Transport";
                dr["icon"] = "ion-calendar";
                dr["t"] = 1;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "ELL_Crane_Schedule";
                dr["name"] = "Crane";
                dr["icon"] = "ion-calendar";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "CMS_Controller_DriverTrips";
                //dr["name"] = "DS Driver Trips";
                //dr["icon"] = "ion-ios-paper-outline";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "CMS_Controller_Schedule_ContainerStatus";
                //dr["name"] = "DS Container Status";
                //dr["icon"] = "ion-ios-paper-outline";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "CMS_Controller_Schedule_TrailerStatus";
                //dr["name"] = "DS Trailer Status";
                //dr["icon"] = "ion-ios-paper-outline";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "EGL_ParkingLot";
                //dr["name"] = "DS ParkingLot";
                //dr["icon"] = "ion-ios-paper-outline";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_ImportCalendar";
                dr["name"] = "Unstuffing";
                dr["icon"] = "ion-calendar";
                dr["t"] = 2;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_ExportCalendar";
                dr["name"] = "Stuffing";
                dr["icon"] = "ion-calendar";
                dr["t"] = 2;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "XG_Review_Out";
                dr["name"] = "Release";
                dr["icon"] = "ion-log-out";
                dr["t"] = 2;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "XG_Review_In";
                dr["name"] = "Receipt";
                dr["icon"] = "ion-log-in";
                dr["t"] = 2;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_PendingList";
                dr["name"] = "Pending";
                dr["icon"] = "ion-calendar";
                dr["t"] = 2;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_ContainerSearch";
                dr["name"] = "Container";
                dr["icon"] = "ion-search";
                dr["t"] = 2;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "ELL_LCLJob_WGR_Calendar";
                dr["name"] = "WGR";
                dr["icon"] = "ion-calendar";
                dr["t"] = 2;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "ELL_LCLJob_WDO_Calendar";
                dr["name"] = "WDO";
                dr["icon"] = "ion-calendar";
                dr["t"] = 2;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_CargoSearch";
                dr["name"] = "Cargo";
                dr["icon"] = "ion-search";
                dr["t"] = 2;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_IssueReport_List";
                dr["name"] = "Issues";
                dr["icon"] = "ion-clipboard";
                dr["t"] = 2;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_StockBalance";
                dr["name"] = "Inventory";
                dr["icon"] = "ion-document-text";
                dr["t"] = 2;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_StockMovement";
                dr["name"] = "Movement";
                dr["icon"] = "ion-document-text";
                dr["t"] = 2;
                dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "ELL_JobProcess_Calendar";
                //dr["name"] = "Process";
                //dr["icon"] = "ion-document-text";
                //dr["t"] = 2;
                //dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "ELL_Quotation_List";
                //dr["name"] = "Quotation";
                //dr["icon"] = "ion-document-text";
                //dr["t"] = 2;
                //dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "EGL_JobTrip_List";
                //dr["name"] = "Job Search";
                //dr["icon"] = "ion-ios-paper";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_Incentive_List";
                dr["name"] = "Incentive List";
                dr["icon"] = "ion-cash";
                dr["t"] = 4;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "EGL_FuelLog_List";
                dr["name"] = "Fuel Log";
                dr["icon"] = "ion-soup-can-outline";
                dr["t"] = 4;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_Mileage_List";
                dr["name"] = "Mileage";
                dr["icon"] = "ion-compass";
                dr["t"] = 4;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "PENLOG_Map_View";
                dr["name"] = "Map";
                dr["icon"] = "ion-map";
                dr["t"] = 4;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_MasterData_Towhead";
                dr["name"] = "Vehicle";
                dr["icon"] = "ion-android-bus";
                dr["t"] = 4;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_MasterData_ServiceLog";
                dr["name"] = "Workshop";
                dr["icon"] = "ion-help-buoy";
                dr["t"] = 4;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_DriverLog_DriverList";
                dr["name"] = "Driver";
                dr["icon"] = "ion-person-stalker";
                dr["t"] = 4;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_DriverLog_DriverCash_List";
                dr["name"] = "Petty Cash";
                dr["icon"] = "ion-cash";
                dr["t"] = 4;
                dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_IssueReport_List_PrimeMoverTyre";
                //dr["name"] = "PrimeMover Tyre";
                //dr["icon"] = "ion-help-buoy";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_IssueReport_List_TrailerTyre";
                //dr["name"] = "Trailer Tyre";
                //dr["icon"] = "ion-help-buoy";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_List_Trip";
                dr["name"] = "KPI-Trip";
                dr["icon"] = "ion-document-text";
                dr["t"] = 5;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_List_TEU";
                dr["name"] = "KPI-TEU";
                dr["icon"] = "ion-document-text";
                dr["t"] = 5;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_List_CUS";
                dr["name"] = "KPI-CUS";
                dr["icon"] = "ion-document-text";
                dr["t"] = 5;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_List_RateCT";
                dr["name"] = "Rate CT";
                dr["icon"] = "ion-document-text";
                dr["t"] = 5;
                dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_Inventory_list";//EGL_Inventory
                //dr["name"] = "Inventory";
                //dr["icon"] = "ion-document-text";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);


                //========= hr
                dr = dt1.NewRow();
                dr["link"] = "EGL_HR_Leave_List";
                dr["name"] = "Leave List";
                dr["icon"] = "ion-person";
                dr["t"] = 6;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_HR_Leave_Calendar";
                dr["name"] = "Leave Calendar";
                dr["icon"] = "ion-calendar";
                dr["t"] = 6;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_HR_Leave_MyList";
                dr["name"] = "My Leave";
                dr["icon"] = "ion-person";
                dr["t"] = 6;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_HR_Attendance";
                dr["name"] = "Attendance";
                dr["icon"] = "ion-person";
                dr["t"] = 6;
                dt1.Rows.Add(dr);
                #endregion
                break;
            case "controller":
                #region controller
                //================= level0
                dr = dt0.NewRow();
                dr["name"] = "Job Management";
                dr["t"] = 0;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "Reporting";
                dr["t"] = 1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "HR";
                dr["t"] = 2;
                dt0.Rows.Add(dr);

                //============================= level1
                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_Schedule";
                dr["name"] = "Controller Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_JobTrip_Schedule";
                dr["name"] = "Driver Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_DriverTrips";
                dr["name"] = "DS Driver Trips";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_Schedule_ContainerStatus";
                dr["name"] = "DS Container Status";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_Schedule_TrailerStatus";
                dr["name"] = "DS Trailer Status";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_ParkingLot";
                dr["name"] = "DS ParkingLot";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "ELL_Crane_Schedule";
                dr["name"] = "Crane Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "EGL_JobTrip_List";
                //dr["name"] = "Job Search";
                //dr["icon"] = "ion-ios-paper";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_Incentive_List";
                dr["name"] = "Incentive List";
                dr["icon"] = "ion-cash";
                dr["t"] = 1;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "EGL_FuelLog_List";
                dr["name"] = "Fuel Log";
                dr["icon"] = "ion-soup-can-outline";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_Mileage_List";
                dr["name"] = "Mileage";
                dr["icon"] = "ion-compass";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_IssueReport_List";
                dr["name"] = "Issue Report";
                dr["icon"] = "ion-clipboard";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_IssueReport_List_PrimeMoverTyre";
                //dr["name"] = "PrimeMover Tyre";
                //dr["icon"] = "ion-help-buoy";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_IssueReport_List_TrailerTyre";
                //dr["name"] = "Trailer Tyre";
                //dr["icon"] = "ion-help-buoy";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_DriverLog_DriverCash_List";
                dr["name"] = "Driver Cash";
                dr["icon"] = "ion-cash";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_MasterData_Towhead";
                dr["name"] = "Vehicle maintenance";
                dr["icon"] = "ion-android-bus";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "PENLOG_Map_View";
                dr["name"] = "Map";
                dr["icon"] = "ion-map";
                dr["t"] = 1;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "EGL_HR_Leave_MyList";
                dr["name"] = "My Leave";
                dr["icon"] = "ion-person";
                dr["t"] = 2;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_HR_Attendance";
                dr["name"] = "Attendance";
                dr["icon"] = "ion-person";
                dr["t"] = 2;
                dt1.Rows.Add(dr);

                #endregion
                break;

            case "driver":
                #region driver

                //============================= menu0
                dr = dt0.NewRow();
                dr["name"] = "Job Management";
                dr["t"] = 0;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "Other Management";
                dr["t"] = 1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["name"] = "HR";
                dr["t"] = 2;
                dt0.Rows.Add(dr);


                //============================= menu1
                dr = dt1.NewRow();
                dr["link"] = "EGL_JobTrip_Schedule";
                dr["name"] = "Trucking";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "ELL_LCLJob_Calendar";
                dr["name"] = "Transport";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "EGL_JobTrip_List";
                //dr["name"] = "Job Search";
                //dr["icon"] = "ion-ios-paper-outline";
                //dr["t"] = 0;
                //dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_JobTrip_TrailerPick";
                //dr["name"] = "Trailer Pick";
                //dr["icon"] = "ion-log-out";
                //dr["t"] = 0;
                //dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "EGL_JobTrip_TrailerPark";
                //dr["name"] = "Trailer Park";
                //dr["icon"] = "ion-log-in";
                //dr["t"] = 0;
                //dt1.Rows.Add(dr);

                //====================== 4 column
                dr = dt1.NewRow();
                dr["link"] = "EGL_Incentive_List";
                dr["name"] = "Incentive List";
                dr["icon"] = "ion-cash";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_DriverLog_DriverCash_List";
                dr["name"] = "Driver Cash";
                dr["icon"] = "ion-cash";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_FuelLog_List";
                dr["name"] = "Fuel Log";
                dr["icon"] = "ion-soup-can-outline";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_Mileage_List";
                dr["name"] = "Mileage";
                dr["icon"] = "ion-compass";
                dr["t"] = 1;
                dt1.Rows.Add(dr);



                dr = dt1.NewRow();
                dr["link"] = "EGL_JobTrip_ContList";
                dr["name"] = "Shifting";
                dr["icon"] = "ion-arrow-swap";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_JobTrip_ContList_LOC";
                dr["name"] = "Local";
                dr["icon"] = "ion-arrow-swap";
                dr["t"] = 1;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "EGL_ParkingLot";
                dr["name"] = "ParkingLot";
                dr["icon"] = "ion-model-s";
                dr["t"] = 1;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_IssueReport_List";
                dr["name"] = "Issue Report";
                dr["icon"] = "ion-clipboard";
                dr["t"] = 1;
                dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_IssueReport_List_PrimeMoverTyre";
                //dr["name"] = "PrimeMover Tyre";
                //dr["icon"] = "ion-help-buoy";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_IssueReport_List_TrailerTyre";
                //dr["name"] = "Trailer Tyre";
                //dr["icon"] = "ion-help-buoy";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "CMS_Kit_Clock";
                //dr["name"] = "Clock";
                //dr["icon"] = "ion-clock";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_HR_Leave_MyList";
                dr["name"] = "My Leave";
                dr["icon"] = "ion-person";
                dr["t"] = 2;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_HR_Attendance";
                dr["name"] = "Attendance";
                dr["icon"] = "ion-person";
                dr["t"] = 2;
                dt1.Rows.Add(dr);
                #endregion
                break;
            case "crane":
                #region crane

                //============================= menu0
                dr = dt0.NewRow();
                dr["name"] = "Job Management";
                dr["t"] = 0;
                dt0.Rows.Add(dr);
                //dr = dt0.NewRow();
                //dr["name"] = "Other Management";
                //dr["t"] = 1;
                //dt0.Rows.Add(dr);


                //============================= menu1

                dr = dt1.NewRow();
                dr["link"] = "ELL_Crane_Schedule";
                dr["name"] = "Crane Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "EGL_Incentive_List";
                dr["name"] = "Incentive";
                dr["icon"] = "ion-cash";
                dr["t"] = 0;
                dt1.Rows.Add(dr);



                dr = dt1.NewRow();
                dr["link"] = "EGL_IssueReport_List";
                dr["name"] = "Issue Report";
                dr["icon"] = "ion-clipboard";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_DriverLog_DriverCash_List";
                dr["name"] = "Driver Cash";
                dr["icon"] = "ion-cash";
                dr["t"] = 0;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "EGL_FuelLog_List";
                dr["name"] = "Fuel Log";
                dr["icon"] = "ion-soup-can-outline";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_Mileage_List";
                dr["name"] = "Mileage";
                dr["icon"] = "ion-compass";
                dr["t"] = 0;
                dt1.Rows.Add(dr);


                #endregion
                break;

            //case "cfs_staff":
            case "cfs":
                #region cfs staff
                //============================= menu0
                dr = dt0.NewRow();
                dr["name"] = "Job Management";
                dr["t"] = 0;
                dt0.Rows.Add(dr);
                //dr = dt0.NewRow();
                //dr["name"] = "Other Management";
                //dr["t"] = 1;
                //dt0.Rows.Add(dr);

                //============================= menu1
                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_ImportCalendar";
                dr["name"] = "Unstuffing Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_ExportCalendar";
                dr["name"] = "Stuffing Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "XG_Review_Out";
                dr["name"] = "Cargo Release";
                dr["icon"] = "ion-log-out";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "XG_Review_In";
                dr["name"] = "Cargo Receipt";
                dr["icon"] = "ion-log-in";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_ContainerSearch";
                dr["name"] = "Container Search";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "XG_WH01_CargoSearch";
                dr["name"] = "Cargo Search";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "ELL_LCLJob_List";
                dr["name"] = "Booking";
                dr["icon"] = "ion-clipboard";
                dr["t"] = 0;
                dt1.Rows.Add(dr);


                //dr = dt1.NewRow();
                //dr["link"] = "";//EGL_KPIReport_List_Trip
                //dr["name"] = "KPI-Cash";
                //dr["icon"] = "ion-document-text";
                //dt1.Rows.Add(dr);
                //dr["t"] = 1;
                //dr = dt1.NewRow();
                //dr["link"] = "";//EGL_KPIReport_List_TEU
                //dr["name"] = "KPI-TEU";
                //dr["icon"] = "ion-document-text";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "";//EGL_KPIReport_List_CUS
                //dr["name"] = "KPI-CUS";
                //dr["icon"] = "ion-document-text";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);
                //dr = dt1.NewRow();
                //dr["link"] = "";//EGL_KPIReport_List_RateCT
                //dr["name"] = "Rate CT";
                //dr["icon"] = "ion-document-text";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);

                //dr = dt1.NewRow();
                //dr["link"] = "EGL_Inventory_list";//EGL_Inventory
                //dr["name"] = "Inventory";
                //dr["icon"] = "ion-document-text";
                //dr["t"] = 1;
                //dt1.Rows.Add(dr);
                #endregion
                break;

            case "test":

                #region test
                //============================= menu0
                dr = dt0.NewRow();
                dr["name"] = "Job Management";
                dr["t"] = 0;
                dt0.Rows.Add(dr);

                //============================= menu1
                dr = dt1.NewRow();
                dr["link"] = "ILOG_Performance_List";
                dr["name"] = "Performance";
                dr["icon"] = "ion-document-text";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "ILOG_Performance_caseList";
                dr["name"] = "Review";
                dr["icon"] = "ion-document-text";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "";
                dr["name"] = "KPI Setup";
                dr["icon"] = "ion-document-text";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "ILOG_Performance_attendanceEdit";
                dr["name"] = "Attendance";
                dr["icon"] = "ion-clock";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "ILOG_Performance_attendanceHistoryList";
                dr["name"] = "Attendance History";
                dr["icon"] = "ion-clock";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "ILOG_Performance_attendanceHistoryListTeam";
                dr["name"] = "Team History";
                dr["icon"] = "ion-clock";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                #endregion
                break;
        }

        string menu0 = Common.DataTableToJson(dt0);
        string menu1 = Common.DataTableToJson(dt1);
        string context = string.Format(@"{0}level0:{2},level1:{3}{1}", "{", "}", menu0, menu1);
        Common.WriteJsonP(true, context);
    }


    [WebMethod]
    public void Menu_GetTabsData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string user = job["User"].ToString();
        string role = job["Role"].ToString().ToLower();

        DataTable dt0 = new DataTable();
        dt0.Columns.Add("code", typeof(string));
        dt0.Columns.Add("name", typeof(string));
        dt0.Columns.Add("icon", typeof(string));
        dt0.Columns.Add("icon1", typeof(string));
        dt0.Columns.Add("hidden", typeof(bool));
        dt0.Columns.Add("module", typeof(string));
        dt0.Columns.Add("action", typeof(string));
        dt0.Columns.Add("t", typeof(int));
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("link", typeof(string));
        dt1.Columns.Add("name", typeof(string));
        dt1.Columns.Add("icon", typeof(string));
        dt1.Columns.Add("t", typeof(int));

        DataRow dr = null;
        switch (role)
        {
            case "admin":
                #region admin
                //================= level0
                dr = dt0.NewRow();
                dr["code"] = "tab0";
                dr["name"] = "Home";
                dr["icon"] = "ion-ios-home";
                dr["icon1"] = "ion-ios-home-outline";
                dr["module"] = "FirstScreen";
                dr["action"] = "tabDashboard";
                dr["t"] = 0;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["code"] = "tab1";
                dr["name"] = "Tab1";
                dr["icon"] = "ion-ios-home";
                dr["icon1"] = "ion-ios-home-outline";
                dr["hidden"] = true;
                dr["module"] = "";
                dr["action"] = "";
                dr["t"] = -1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["code"] = "tab2";
                dr["name"] = "Schedule";
                dr["icon"] = "ion-ios-calendar";
                dr["icon1"] = "ion-ios-calendar-outline";
                dr["module"] = "EGL_JobTrip";
                dr["action"] = "goTabSchedule";
                dr["t"] = -1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["code"] = "tab3";
                dr["name"] = "Issue";
                dr["icon"] = "ion-ios-paper";
                dr["icon1"] = "ion-ios-paper-outline";
                dr["module"] = "EGL_IssueReport";
                dr["action"] = "goTabIssueReport";
                dr["t"] = -1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["code"] = "tab4";
                dr["name"] = "Tab4";
                dr["icon"] = "ion-ios-home";
                dr["icon1"] = "ion-ios-home-outline";
                dr["hidden"] = true;
                dr["module"] = "";
                dr["action"] = "";
                dr["t"] = -1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["code"] = "tab5";
                dr["name"] = "Map";
                dr["icon"] = "ion-ios-location";
                dr["icon1"] = "ion-ios-location-outline";
                dr["module"] = "PENLOG_Map";
                dr["action"] = "goTabMap";
                dr["t"] = -1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["code"] = "tab6";
                dr["name"] = "Tab6";
                dr["icon"] = "ion-ios-home";
                dr["icon1"] = "ion-ios-home-outline";
                dr["hidden"] = true;
                dr["module"] = "";
                dr["action"] = "";
                dr["t"] = -1;
                dt0.Rows.Add(dr);
                dr = dt0.NewRow();
                dr["code"] = "tab7";
                dr["name"] = "Others";
                dr["icon"] = "ion-ios-box";
                dr["icon1"] = "ion-ios-box-outline";
                dr["module"] = "FirstScreen";
                dr["action"] = "tabDashboard";
                dr["t"] = 7;
                dt0.Rows.Add(dr);

                //============================= level1
                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_Schedule";
                dr["name"] = "Controller Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_JobTrip_Schedule";
                dr["name"] = "Driver Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 0;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_DriverTrips";
                dr["name"] = "DS Driver Trips";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 7;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_Schedule_ContainerStatus";
                dr["name"] = "DS Container Status";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 7;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "CMS_Controller_Schedule_TrailerStatus";
                dr["name"] = "DS Trailer Status";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 7;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_ParkingLot";
                dr["name"] = "DS ParkingLot";
                dr["icon"] = "ion-ios-paper-outline";
                dr["t"] = 7;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "ELL_Crane_Schedule";
                dr["name"] = "Crane Schedule";
                dr["icon"] = "ion-calendar";
                dr["t"] = 7;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_Incentive_List";
                dr["name"] = "Incentive List";
                dr["icon"] = "ion-cash";
                dr["t"] = 7;
                dt1.Rows.Add(dr);


                dr = dt1.NewRow();
                dr["link"] = "EGL_FuelLog_List";
                dr["name"] = "Fuel Log";
                dr["icon"] = "ion-soup-can-outline";
                dr["t"] = 7;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_Mileage_List";
                dr["name"] = "Mileage";
                dr["icon"] = "ion-compass";
                dr["t"] = 7;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_IssueReport_List";
                dr["name"] = "Issue Report";
                dr["icon"] = "ion-clipboard";
                dr["t"] = 7;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_DriverLog_DriverCash_List";
                dr["name"] = "Driver Cash";
                dr["icon"] = "ion-cash";
                dr["t"] = 7;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_MasterData_Towhead";
                dr["name"] = "Vehicle maintenance";
                dr["icon"] = "ion-android-bus";
                dr["t"] = 7;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "PENLOG_Map_View";
                dr["name"] = "Map";
                dr["icon"] = "ion-map";
                dr["t"] = 7;
                dt1.Rows.Add(dr);

                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_List_Trip";
                dr["name"] = "KPI-Trip";
                dr["icon"] = "ion-document-text";
                dr["t"] = 7;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_List_TEU";
                dr["name"] = "KPI-TEU";
                dr["icon"] = "ion-document-text";
                dr["t"] = 7;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_List_CUS";
                dr["name"] = "KPI-CUS";
                dr["icon"] = "ion-document-text";
                dr["t"] = 7;
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["link"] = "EGL_KPIReport_List_RateCT";
                dr["name"] = "Rate CT";
                dr["icon"] = "ion-document-text";
                dr["t"] = 7;
                dt1.Rows.Add(dr);


                #endregion
                break;
            case "controller":
                break;

            case "driver":
                break;
            case "crane":
                break;

            case "cfs":
                break;
        }

        string menu0 = Common.DataTableToJson(dt0);
        string menu1 = Common.DataTableToJson(dt1);
        string context = string.Format(@"{0}level0:{2},level1:{3}{1}", "{", "}", menu0, menu1);
        Common.WriteJsonP(true, context);
    }



    #endregion

    [WebMethod]
    public void Check_DriverMileage(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string user = job["User"].ToString();
        string role = job["Role"].ToString().ToLower();
        string status = "1";
        string context = Common.StringToJson("");
//        if (role == "driver")
//        {
//            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
//            string sql = string.Format(@"select count(*) from Vehicle_Mileage 
//where datediff(day,ReportDate,getdate())=0 and CreateBy=@CreateBy");
//            list.Add(new ConnectSql_mb.cmdParameters("@CreateBy", user, SqlDbType.NVarChar, 100));
//            if (ConnectSql_mb.ExecuteScalar(sql, list).context.Equals("0"))
//            {
//                status = "1";
//                context = Common.StringToJson("Require Mileage!");
//            }
//        }
        Common.WriteJsonP(status, context);
    }

    #region Position
    [WebMethod]
    public void Position_Upload(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string status = "0";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;

        if (job["user"] != null && job["user"].ToString().Trim().Length > 0)
        {
            string sql = string.Format(@"insert into CTM_Location([type],[user],geo_device,geo_latitude,geo_longitude,note1,create_date_time)
values('Location',@user,@geo_device,@geo_latitude,@geo_longitude,@note1,getdate())");
            list.Add(new ConnectSql_mb.cmdParameters("@user", job["user"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@geo_device", job["deviceId"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@geo_latitude", job["lat"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@geo_longitude", job["lng"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@note1", job["platform"], SqlDbType.NVarChar, 100));
            if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
            {
                status = "1";
                context = Common.StringToJson("");
            }
        }
        Common.WriteJsonP(status, context);
    }


    #endregion

    #region kit
    [WebMethod]
    public void Kit_ClockIn(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("Hello " + job["user"] + ", Clock in successful!");

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Kit_ClockOut(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "1";
        string context = Common.StringToJson("Hello " + job["user"] + ", Clock out successful!");

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Kit_EmailAttachment(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string addr = job["addr"].ToString();
        string subject = SafeValue.SafeString(job["subject"]);
        if (subject == "")
        {
            subject = "From " + System.Configuration.ConfigurationManager.AppSettings["CompanyName"].Trim();
        }
        string context = job["context"].ToString();
        string Ids = job["Ids"].ToString();
        string fromAttachmentTable = SafeValue.SafeString(job["table"]);
        fromAttachmentTable = (fromAttachmentTable == "" ? "CTM_Attachment" : fromAttachmentTable);
        string sql = string.Format(@"select FilePath from {1} where Id in ({0}) ", Ids, fromAttachmentTable);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);

        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
        if (fileStart == null)
        {
            fileStart = "";
        }
        string files = "";
        //string path0 = Server.MapPath("Photo").Replace("\\Mobile", "");
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    string temp = dt.Rows[i]["FilePath"].ToString();
        //    if (temp.ToLower().StartsWith("http:"))
        //    {

        //    }else
        //    {
        //        temp = Path.Combine(path0, temp);
        //    }
        //    files += (files.Length > 0 ? "\r\n" : "") + temp;
        //}
        //Helper.Email.SendEmail(addr, "", "", subject, context + ";" + files, "");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string temp = dt.Rows[i]["FilePath"].ToString();
            temp =@"~\Photos\"+ temp.Replace(fileStart, "");
            files += (files == "" ? "" : ",") + temp;
        }
        Helper.Email.SendEmail(addr, "", "", subject, context, files);

        Common.WriteJsonP(true, Common.StringToJson(""));
    }


    [WebMethod]
    public void Kit_Email(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string to = SafeValue.SafeString(job["to"]);
        string cc = SafeValue.SafeString(job["cc"]);
        string bcc = SafeValue.SafeString(job["bcc"]);
        string subject = SafeValue.SafeString(job["subject"]);
        string context = job["context"].ToString();
        Helper.Email.SendEmail(to, cc, bcc, subject, context, "");

        Common.WriteJsonP(true, Common.StringToJson(""));
    }
    [WebMethod]
    public void Kit_Timeline_GetList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = "[]";
        string actionLevel = SafeValue.SafeString(job["actionLevel"]);
        string actionNo = SafeValue.SafeString(job["actionId"]);
        int actionId = SafeValue.SafeInt(job["actionId"], 0);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@actionId", actionId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@actionNo", actionNo, SqlDbType.NVarChar, 100));

        string sql = "";
        switch (actionLevel)
        {
            case "ContId":
                sql = string.Format(@"select l.Id,l.CreateDateTime,Controller,l.JobNo,l.Remark,Lat,Lng,att.FileType,att.FilePath 
from CTM_JobDet1 as det1
left outer join CTM_JobEventLog as l on det1.ContainerNo=l.ContainerNo and det1.JobNo=l.JobNo
left outer join CTM_Attachment as att on l.ActionLevel='ATTACHMENT' and l.ActionId=att.Id
where det1.Id=@actionId and l.Id>0
order by CreateDateTime desc");
                break;
            case "JobNo":
                sql = string.Format(@"select l.Id,l.CreateDateTime,Controller,l.JobNo,Remark,Lat,Lng,att.FileType,att.FilePath 
from CTM_JobEventLog as l
left outer join CTM_Attachment as att on l.ActionLevel='ATTACHMENT' and l.ActionId=att.Id
where l.JobNo=@actionNo and l.Id>0
order by CreateDateTime desc");
                break;
            case "TripId":
                sql = string.Format(@"select l.Id,l.CreateDateTime,Controller,l.JobNo,l.Remark,Lat,Lng,'' as FileType,'' as FilePath 
from CTM_JobEventLog as l 
where l.ActionId=@actionId and l.ActionLevel='TRIP'
union all
select l.Id,l.CreateDateTime,Controller,l.JobNo,l.Remark,Lat,Lng,att.FileType,att.FilePath 
from ctm_attachment as att
left outer join CTM_JobEventLog as l on att.Id=l.ActionId and l.ActionLevel='ATTACHMENT'
where att.TripId=@actionId
order by CreateDateTime desc");
                break;
        }
        if (sql != "")
        {
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            context = Common.DataTableToJson(dt);
        }
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void Kit_Map_DriverJob(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = "[]";
        string driver = SafeValue.SafeString(job["driver"]);
        string date = SafeValue.SafeString(job["date"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@driver", driver, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@date", date, SqlDbType.NVarChar, 10));

        string sql = string.Format(@"select role from [user] where Name=@driver");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string role = "";
        if (dt.Rows.Count > 0)
        {
            role = dt.Rows[0][0].ToString().ToLower();
        }
        string sql_where = "";
        if (role == "driver")
        {
            sql_where = "and DriverCode=@driver";
        }
        sql = string.Format(@"select Id,JobNo as item_no, '' as postcode,
ToCode as address from (
select * from CTM_JobDet2 
where isnull(Statuscode,'')<>'C' and datediff(d,fromdate,@date)=0 {0}
) as temp", sql_where);
        dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }

    #endregion

    #region Driver cash

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

        string sql = string.Format(@"select * from Ref_DriverCash where Id=@Id");
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

    #endregion


}
