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
/// Summary description for Connect_Client
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Connect_Client : System.Web.Services.WebService
{

    public Connect_Client()
    {

        //Uncomment the following line if using designed components 
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
            string sql = string.Format(@"select * from [user] where IsActive=1 and Tel=@Tel and Role='Client'");
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
            string sql = string.Format(@"select top 1 * from [user] where IsActive=1 and Tel=@Tel and Pwd=@Pwd and Role='Client'");
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

    #region register

    [WebMethod]
    public void User_register_Submit(string info)
    {
        JObject jo = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string mobile = SafeValue.SafeString(jo["account"]);
        string pw = SafeValue.SafeString(jo["password"]);
        string username = SafeValue.SafeString(jo["userName"]);
        bool status = false;
        string context = "";

        Encryption.EncryptClass encrypt = new Encryption.EncryptClass();
        string pw_mm = SafeValue.SafeString(encrypt.DESEnCode(username, pw), "");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Name", username, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Pwd", pw_mm, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Tel", mobile, SqlDbType.NVarChar, 100));

        string sql = string.Format(@"select * from [User] where Name=@Name,Tel=@Tel");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count == 0)
        {
            sql = string.Format(@"insert into [User] (Name,Pwd,Email,Tel,Role,IsActive,CustId,Port) values(@Name,@Pwd,'',@Tel,'Client',1,'','')");
            ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);
            status = result.status;
            if (!status)
            {
                context = result.context;
            }
        }
        else
        {
            context = "This user name or mobile have be registered";
        }
        context = Common.StringToJson(context);
        Common.WriteJsonP(status, context);
    }

    #endregion

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

    #region menu

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




        menu1 = Common.DataTableToJson(dt);

        context = string.Format(@"[{0}name:'Job Managerment',list:{2},row:2{1},{0}name:'Reporting',list:{3},row:4{1}]", "{", "}", menu0, menu1);
        Common.WriteJsonP(status, context);
    }






    #endregion

    #region client
    [WebMethod]
    public void FSL_Container_Search_ByType(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string user = SafeValue.SafeString(job["user"]);
        string fromDate = SafeValue.SafeString(job["fromDate"]);
        string toDate = SafeValue.SafeString(job["toDate"]);
        string type = SafeValue.SafeString(job["type"]);

        string sql = string.Format(@"select job.Id as jobId,job.JobNo,job.EtaDate,
det1.Id as contId,det1.ContainerNo,det1.ContainerType,det1.ContainerCategory,det1.SealNo,det1.StatusCode,det1.WarehouseStatus,
det1.WhsReadyInd,det1.WhsReadyTime,det1.WhsReadyLocation,det1.WhsReadyWeight 
from CTM_JobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo 
left outer join [user] as u on u.CustId=job.ClientId
where job.EtaDate>=@fromDate and job.EtaDate<@toDate and u.Name=@client and job.JobType=@JobType");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@fromDate", fromDate, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@toDate", toDate, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", type, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@client", user, SqlDbType.NVarChar, 100));

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        Common.WriteJsonP(true, Common.DataTableToJson(dt));
    }

    [WebMethod]
    public void FSL_Container_ReadyReturn(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int contId = SafeValue.SafeInt(job["contId"], 0);
        string Time = SafeValue.SafeString(job["Time"]);
        string Location = SafeValue.SafeString(job["Location"]);
        string user = SafeValue.SafeString(job["user"]);

        C2.CtmJobDet1Biz det1Bz = new C2.CtmJobDet1Biz(contId);
        C2.CtmJobDet1 det1 = det1Bz.getData();
        bool status = false;
        string context = Common.StringToJson("");
        if (det1 != null)
        {
            try
            {
                det1.WhsReadyInd = "Y";
                det1.WhsReadyTime = Time;
                det1.WhsReadyLocation = Location;
                C2.Manager.ORManager.StartTracking(det1, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(det1);
                status = true;
                #region lot alert
                string sql = string.Format(@"select ClientId,EmailAddress 
from CTM_JobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where det1.Id=@contId");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));
                string client = "";
                string EmailAddress = "";
                DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                if (dt.Rows.Count > 0)
                {
                    client = SafeValue.SafeString(dt.Rows[0]["ClientId"]);
                    EmailAddress = SafeValue.SafeString(dt.Rows[0]["EmailAddress"]);
                }
                C2.CtmJobEventLog log = new C2.CtmJobEventLog();
                log.Platform_isMobile();
                log.Controller = user;
                log.ActionLevel_isCONT(contId);
                log.isAlert(client);
                log.setRemark(CtmJobEventLogRemark.Level.Container, -1, user + " set ready to return [" + Time + "]");
                log.log();
                #endregion

                C2.Email.SendEmail(EmailAddress, "admin@cargo.ms,alan@tsllogistics.sg", "", "TSL Customer Alert", "Dear Customer:Container:["+det1.ContainerNo+"] is ready to return.", "");


                context = "{\"Client\":\"" + client + "\",\"text\":\"" + det1.ContainerNo + " ready to return\"}";
            }
            catch { }
        }


        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void FSL_Container_ReadyExport(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int contId = SafeValue.SafeInt(job["contId"], 0);
        string Time = SafeValue.SafeString(job["Time"]);
        string Location = SafeValue.SafeString(job["Location"]);
        string user = SafeValue.SafeString(job["user"]);
        decimal Weight = SafeValue.SafeDecimal(job["Weight"]);

        C2.CtmJobDet1Biz det1Bz = new C2.CtmJobDet1Biz(contId);
        C2.CtmJobDet1 det1 = det1Bz.getData();
        bool status = false;
        string context = Common.StringToJson("");
        if (det1 != null)
        {
            try
            {
                det1.WhsReadyInd = "Y";
                det1.WhsReadyTime = Time;
                det1.WhsReadyLocation = Location;
                det1.WhsReadyWeight = Weight;
                det1.Weight = Weight;
                C2.Manager.ORManager.StartTracking(det1, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(det1);
                status = true;
                #region log alert
                string sql = string.Format(@"select ClientId,EmailAddress 
from CTM_JobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where det1.Id=@contId");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));
                string client = "";
                string EmailAddress = "";
                DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                if (dt.Rows.Count > 0)
                {
                    client = SafeValue.SafeString(dt.Rows[0]["ClientId"]);
                    EmailAddress = SafeValue.SafeString(dt.Rows[0]["EmailAddress"]);
                }
                C2.CtmJobEventLog log = new C2.CtmJobEventLog();
                log.Platform_isMobile();
                log.Controller = user;
                log.ActionLevel_isCONT(contId);
                log.isAlert(client);
                log.setRemark(CtmJobEventLogRemark.Level.Container, -1, user + " set ready export [" + Time + "]");
                log.log();
                #endregion
                C2.Email.SendEmail(EmailAddress, "admin@cargo.ms,alan@tsllogistics.sg", "", "TSL Customer Alert", "Dear Customer:Container:[" + det1.ContainerNo + "] is ready to export.", "");
                context = "{\"Client\":\"" + client + "\",\"text\":\"" + det1.ContainerNo + " ready to export\"}";
            }
            catch { }
        }
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void TSL_Container_Search(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string no = SafeValue.SafeString(job["no"]);

        string sql = string.Format(@"select Id,JobNo,ContainerNo,SealNo,ContainerType 
from CTM_JobDet1 where ContainerNo like @no");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@no", "%" + no + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        Common.WriteJsonP(true, Common.DataTableToJson(dt));
    }

    [WebMethod]
    public void TSL_ContainerDetail_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int refId = SafeValue.SafeInt(job["no"], 0);

        string sql = string.Format(@"select Id,JobNo,ContainerNo,SealNo,ContainerType 
from CTM_JobDet1 where Id=@refId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@refId", refId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        string mast = "{}";
        string trips = "[]";
        if (dt.Rows.Count > 0)
        {
            mast = Common.DataRowToJson(dt);

            sql = string.Format(@"select Id,TripCode,DriverCode,FromDate,FromTime,FromCode,ToCode,Statuscode 
from CTM_JobDet2
where Det1Id=@refId");
            dt = ConnectSql_mb.GetDataTable(sql, list);

            trips = Common.DataTableToJson(dt);
        }
        string context = string.Format(@"{0}mast:{2},trips:{3}{1}", "{", "}", mast, trips);
        Common.WriteJsonP(true, context);

    }


    [WebMethod]
    public void Container_Search(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string no = SafeValue.SafeString(job["no"]);

        string sql = string.Format(@"select Id,ContainerNo,ContainerType from Ref_Container where StatusCode='Use' and ContainerNo like @no");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@no", "%" + no + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        Common.WriteJsonP(true, Common.DataTableToJson(dt));
    }

    [WebMethod]
    public void ContainerDetail_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int refId = SafeValue.SafeInt(job["no"], 0);

        string sql = string.Format(@"select Id,ContainerNo,ContainerType,TankCat,Lessor,CommDate,OnHireDateTime,ManuDate 
from Ref_Container where Id=@refId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@refId", refId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        string mast = "{}";
        string events = "[]";
        if (dt.Rows.Count > 0)
        {
            mast = Common.DataRowToJson(dt);

            sql = string.Format(@"select ll.*,ship.Vessel,ship.Pol,ship.Pod 
From CTM_JobEventLog as ll 
left outer join Ref_Container as cont on ll.ContainerNo=cont.ContainerNo
left outer join SeaExportRef as ship on ll.JobNo=ship.RefNo
where cont.Id=@refId
order by CreateDateTime desc");
            dt = ConnectSql_mb.GetDataTable(sql, list);

            events = Common.DataTableToJson(dt);
        }
        string context = string.Format(@"{0}mast:{2},events:{3}{1}", "{", "}", mast, events);
        Common.WriteJsonP(true, context);

    }


    #region attachment

    [WebMethod]
    public void Attachment_AddMutiple(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        JArray jar = (JArray)JsonConvert.DeserializeObject(job["list"].ToString());

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"insert into CTM_Attachment(RefNo,ContainerNo,FileName,FilePath,FileType,CreateDateTime,JobNo,JobType,FileNote,TripId) 
values (@OrderNo,@ContNo,@FileName,@FilePath,@FileType,Getdate(),@CargoId,@OrderType,@FileNote,@TripId)
select @@Identity");
        string fileStart = null;
        try
        {
            fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
        }
        catch { }
        if (fileStart == null)
        {
            fileStart = "";
        }
        string CreateBy = job["user"].ToString();
        string JobNo = job["JobNo"].ToString();
        string ContainerNo = job["ContainerNo"].ToString();
        string TripId = job["TripId"].ToString();
        string CargoId = SafeValue.SafeString(job["CargoId"]);
        string JobType = "CTM";
        string FileNote = "";
        bool ShowCust = true;

        for (int i = 0; i < jar.Count; i++)
        {
            string fileEnd = jar[i]["FP"].ToString();
            fileEnd = fileEnd.Substring(0, fileEnd.LastIndexOf('/')) + "/" + fileEnd.Substring(fileEnd.LastIndexOf('/') + 1);
            fileEnd = fileStart + fileEnd;
            string FileType = jar[i]["FT"].ToString();
            string FileName = jar[i]["FN"].ToString();

            list = new List<ConnectSql_mb.cmdParameters>();
            cpar = new ConnectSql_mb.cmdParameters("@OrderNo", JobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ContNo", ContainerNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@CargoId", CargoId, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@TripId", TripId, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@FileType", FileType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@OrderType", JobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@FileName", FileName, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@FilePath", fileEnd, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@CreateBy", CreateBy, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@FileNote", FileNote, SqlDbType.NVarChar, 300);
            list.Add(cpar);
            list.Add(new ConnectSql_mb.cmdParameters("@ShowCust", ShowCust, SqlDbType.Bit));
            ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
            if (res.status)
            {
                //status = "1";
                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = CreateBy;
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                //lg.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(res.context, 0));
                //lg.Remark = "Attachment upload file:" + job["FileName"];
                lg.setActionLevel(SafeValue.SafeInt(res.context, 0), CtmJobEventLogRemark.Level.Attachment, 1, ":" + FileType + "[" + FileName + "]");
                lg.log();
            }
            else
            {
                status = "0";
                context = (context == "" ? "Save error:" : ",") + FileName;
            }
        }

        context = Common.StringToJson(context);
        Common.WriteJsonP(status, context);
    }

    #endregion

    #endregion

    #region client alert

    [WebMethod]
    public void TSL_CientAlert_GetList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string fromDate = SafeValue.SafeString(job["fromDate"]);
        string toDate = SafeValue.SafeString(job["toDate"]);
        string user = SafeValue.SafeString(job["user"]);

        string sql = string.Format(@"select l.* 
from CTM_JobEventLog as l
left outer join [user] as u on  l.Note1=u.CustId 
where u.Name=@user and l.Note1Type='AlertClient' and l.CreateDateTime>=@fromDate and l.CreateDateTime<@toDate
order by CreateDateTime desc");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@fromDate", fromDate, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@toDate", toDate, SqlDbType.NVarChar, 10));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        Common.WriteJsonP(true, Common.DataTableToJson(dt));
    }


    #endregion

}
