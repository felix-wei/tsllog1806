using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_Controller 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect_Controller : System.Web.Services.WebService
{

    public Connect_Controller()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void Controller_Calendar_GetDataList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = "";

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string sql = string.Format(@"select det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,det1.YardAddress as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.SealNo,det1.ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,det1.UrgentInd,job.OperatorCode,job.CarrierBkgNo,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,job.Pod,job.Pod,
(select top 1 Code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,
isnull((select ',{0}{2}t{2}:{2}'+det2.TripCode+'{2},{2}s{2}:{2}'+det2.Statuscode+'{2}{1}' from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')),'') as trips,
datediff(hour,isnull((select top 1 convert(nvarchar(8),det2.FromDate,112)+' '+det2.FromTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and (det2.Statuscode='S' or det2.Statuscode='C') order by det2.FromDate,det2.FromTime),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)),
case when det1.StatusCode='Completed' then isnull((select top 1 convert(nvarchar(8),det2.ToDate,112)+' '+det2.ToTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and det2.Statuscode='C' order by det2.ToDate desc,det2.ToTime desc),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)) else getdate() end)  as hr
from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo
where datediff(d,@date,det1.ScheduleDate)=0
order by job.EtaDate,job.JobNo,det1.Id,det1.StatusCode desc, job.JobDate asc", "{", "}", "\"");
        list.Add(new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 10));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void ContainerTrips_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string sql = string.Format(@"select det1.Id,det1.JobNo,det1.ContainerNo,det1.SealNo,det1.StatusCode,
j.PickupFrom,j.DeliveryTo,j.YardRef,j.SpecialInstruction,j.Remark,j.PermitNo 
from CTM_JobDet1 as det1
left outer join CTM_Job as j on j.JobNo=det1.JobNo
where det1.Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);

        sql = string.Format(@"select Id,TripCode,StatusCode,ToCode,FromDate,FromTime,ToDate,ToTime,DriverCode,ChessisCode,ToParkingLot,Remark 
from CTM_JobDet2
where Det1Id=@Id");
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string trips = Common.DataTableToJson(dt);

        string context = string.Format(@"{0}mast:{2},trips:{3}{1}", "{", "}", mast, trips);

        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void ContainerTrips_AddNewTrip(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int contId = SafeValue.SafeInt(job["contId"], 0);
        string type = SafeValue.SafeString(job["type"], "SHF");
        string temp = Trip_AddNew(contId, type);
        if (temp.Equals(""))
        {

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCONT(contId);
            //lg.Remark = "Add Trip";
            lg.setActionLevel(contId, CtmJobEventLogRemark.Level.Trip, 1);
            lg.log();
        }
        Common.WriteJsonP(temp.Equals(""), Common.StringToJson(temp));
    }

    private string Trip_AddNew(int ContId, string newType)
    {
        string res = "";
        string sql = string.Format(@"select JobNo,ContainerNo from ctm_jobdet1 where Id=@contId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters(@"contId", ContId, SqlDbType.Int));
        DataTable dt_det1 = ConnectSql_mb.GetDataTable(sql, list);
        if (dt_det1.Rows.Count == 1)
        {
            string JobNo = dt_det1.Rows[0]["JobNo"].ToString();
            string ContainerNo = dt_det1.Rows[0]["ContainerNo"].ToString();



            //===========================

            sql = string.Format(@"select PickupFrom,DeliveryTo,YardRef,JobType from CTM_Job where JobNo='{0}'", SafeValue.SafeString(JobNo, ""));
            DataTable tab = ConnectSql.GetTab(sql);
            sql = string.Format(@"select top 1 * from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} order by Id desc", SafeValue.SafeString(JobNo, ""), ContId);
            DataTable dt = ConnectSql.GetTab(sql);
            string job_from = SafeValue.SafeString(tab.Rows[0]["PickupFrom"]);
            string job_to = SafeValue.SafeString(tab.Rows[0]["DeliveryTo"]);
            string job_Depot = SafeValue.SafeString(tab.Rows[0]["YardRef"]);
            string P_From = "";
            string P_From_Pl = "";
            string P_To = "";// DeliveryTo.Text;
            string trailer = "";
            string JobType = SafeValue.SafeString(tab.Rows[0]["JobType"]);
            string TripCode = "";
            DateTime FromDate = DateTime.Now;
            string FromTime = DateTime.Now.ToString("HH:mm");

            //string newType = SafeValue.SafeString(lb_newTrip_Type.Text, "SHF");
            TripCode = newType;
            switch (newType)
            {
                case "COL":
                    //add_newTrip_CheckMultiple(newType, JobNo, ContId);
                    if (!add_newTrip_CheckMultiple(newType, JobNo, ContId))
                    {
                        return "Exist trip:" + newType;
                    }
                    P_From = job_Depot;
                    P_To = job_from;
                    break;
                case "EXP":
                    //add_newTrip_CheckMultiple(newType, JobNo, ContId);
                    if (!add_newTrip_CheckMultiple(newType, JobNo, ContId))
                    {
                        return "Exist trip:" + newType;
                    }
                    P_From = job_from;
                    P_To = job_to;
                    break;
                case "IMP":
                    //add_newTrip_CheckMultiple(newType, JobNo, ContId);
                    if (!add_newTrip_CheckMultiple(newType, JobNo, ContId))
                    {
                        return "Exist trip:" + newType;
                    }
                    P_From = job_from;
                    P_To = job_to;
                    break;
                case "RET":
                    //add_newTrip_CheckMultiple(newType, JobNo, ContId);
                    if (!add_newTrip_CheckMultiple(newType, JobNo, ContId))
                    {
                        return "Exist trip:" + newType;
                    }
                    P_To = job_Depot;
                    break;
                case "SHF":
                    P_To = job_from;
                    break;
                case "LOC":
                    P_From = job_from;
                    P_To = job_to;
                    break;
            }
            if (dt.Rows.Count > 0)
            {
                P_From = dt.Rows[0]["ToCode"].ToString();
                P_From_Pl = dt.Rows[0]["ToParkingLot"].ToString();
                trailer = dt.Rows[0]["ChessisCode"].ToString();
            }

            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot) values (@JobNo,@ContainerNo,'','',@ChessisCode,@FromCode,@FromDate,@FromTime,@ToCode,@FromDate,@FromTime,@Det1Id,'P',
'','N','','',@TripCode,'Normal','N',@FromParkingLot)", JobNo, ContainerNo, trailer, P_From, FromDate, FromTime, P_To, ContId,
                                                TripCode, P_From_Pl);
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", trailer, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@FromCode", P_From, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@FromDate", FromDate, SqlDbType.DateTime));
            list.Add(new ConnectSql_mb.cmdParameters("@FromTime", FromTime, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ToCode", P_To, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", ContId, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@TripCode", TripCode, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@FromParkingLot", P_From_Pl, SqlDbType.NVarChar, 100));
            if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
            {
                sql = string.Format(@"update ctm_jobdet1 set StatusCode='{0}' where Id={1}", "InTransit", ContId);
                ConnectSql.ExecuteSql(sql);
            }
            else
            {
                res = "Save Error";
            }

            //sql = string.Format(@"select count(*) from ctm_jobdet2 where det1Id={0}", ContId);
            //int rowSum = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            //if (rowSum >= 1)
            //{
            //    sql = string.Format(@"update ctm_jobdet1 set StatusCode='{0}' where Id={1}", "InTransit", ContId);
            //    ConnectSql.ExecuteSql(sql);
            //}


        }
        else
        {
            res = "This Container inexistence";
        }
        return res;
    }
    private bool add_newTrip_CheckMultiple(string Type, string JobNo, int contId)
    {
        bool res = true;
        string sql = string.Format(@"select Id,TripCode from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} and TripCode='{2}' order by Id desc", JobNo, contId, Type);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            res = false;
        }
        return res;
    }

    [WebMethod]
    public void ContainerTrips_ChangeStatus(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = Common.StringToJson("");

        string sql = string.Format(@"update ctm_jobdet1 set StatusCode=@StatusCode where Id=@contId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@contId", SafeValue.SafeInt(job["contId"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", job["StatusCode"], SqlDbType.NVarChar, 100));
        bool status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        if (status)
        {
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCONT(SafeValue.SafeInt(job["contId"], 0));
            //lg.Remark = "Container change status:" + job["StatusCode"];
            lg.setActionLevel(SafeValue.SafeInt(job["contId"], 0), CtmJobEventLogRemark.Level.Container, 4, " :" + job["StatusCode"]);
            lg.log();
        }
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void ContainerTrips_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = Common.StringToJson("");

        string sql = string.Format(@"update ctm_jobdet1 set ContainerNo=@ContainerNo,SealNo=@SealNo where Id=@contId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@contId", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", job["ContainerNo"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@SealNo", job["SealNo"], SqlDbType.NVarChar, 100));
        ConnectSql_mb.ExecuteNonQuery(sql, list);

        sql = string.Format(@"update ctm_job set PickupFrom=@PickupFrom,DeliveryTo=@DeliveryTo,YardRef=@YardRef,
SpecialInstruction=@SpecialInstruction,Remark=@Remark,PermitNo=@PermitNo where JobNo=@JobNo");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", job["JobNo"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@PickupFrom", job["PickupFrom"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@DeliveryTo", job["DeliveryTo"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@YardRef", job["YardRef"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@SpecialInstruction", job["SpecialInstruction"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", job["Remark"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@PermitNo", job["PermitNo"], SqlDbType.NVarChar, 100));
        bool status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        if (status)
        {
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            lg.ActionLevel_isCONT(SafeValue.SafeInt(job["Id"], 0));
            lg.Remark = "Container Update";
            lg.log();
        }
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void ContainerTrip_Detail_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));

        string sql = string.Format(@"select Id,TripCode,StatusCode,ToCode,FromDate,FromTime,ToDate,ToTime,DriverCode,ChessisCode,ToParkingLot,Remark 
from CTM_JobDet2
where Id=@Id");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataRowToJson(dt);

        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void ContainerTrip_Detail_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", job["DriverCode"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", job["ChessisCode"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToParkingLot", job["ToParkingLot"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", job["Remark"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@ToCode", job["ToCode"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", job["FromDate_e"], SqlDbType.NVarChar, 10));

        string sql = string.Format(@"update CTM_JobDet2 set DriverCode=@DriverCode,ChessisCode=@ChessisCode,ToParkingLot=@ToParkingLot,Remark=@Remark,ToCode=@ToCode,FromDate=@FromDate  
where Id=@Id");
        bool status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        if (status)
        {
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"], 0));
            //lg.Remark = "Trip Update";
            lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0),CtmJobEventLogRemark.Level.Trip,3);
            lg.log();
        }
        Common.WriteJsonP(status, Common.StringToJson(""));
    }

    [WebMethod]
    public void ContainerTrip_Detail_Delete(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int status = 0;

        string deleteType = job["deleteType"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));

        string sql = string.Format(@"select StatusCode from CTM_JobDet2 where Id=@Id");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["StatusCode"].ToString() == "S")
            {
                status = 2;
                if (deleteType == "S")
                {
                    status = 0;
                }
            }
            if (status <= 0)
            {
                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"], 0));
                //lg.Remark = "Trip Delete";
                lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.Trip, 2);
                lg.log();
                sql = string.Format(@"delete from CTM_JobDet2 where Id=@Id");
                ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);
                if (result.status)
                {
                    status = 1;
                }
            }
        }


        Common.WriteJsonP(status, Common.StringToJson(job["Id"].ToString()));
    }



    [WebMethod]
    public void Cotroller_Driver_status_trips(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string sql = string.Format(@"select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,'' as TowheadCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode ,
det1.SealNo,det2.JobNo,det2.Det1Id,1 as IndexType,det2.Id as tripId 
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.JobNo=det1.JobNo and det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,@date,det2.FromDate)=0 and job.StatusCode<>'CNL' and det2.DriverCode<>'' 
union all
select log.Id,det2.ContainerNo,det2.ContainerType,det2.ChessisCode,log.Driver as DriverCode,det2.FromCode,det2.TowheadCode,det2.ToCode,det2.TripCode,FromTime,ToTime,Statuscode,
'' as SealNo,'' as JobNo,0 as Det1Id,0 as IndexType,det2.Id as tripId 
from CTM_DriverLog as log
left outer join (
select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.TowheadCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode 
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.id=det2.det1Id
 where det2.Id in(
select MAX(Id) as Id from(
select det2.Id,det2.DriverCode
from (
	select det2.DriverCode,MAX(FromTime) as MaxTime
	from CTM_JobDet2 as det2 
	left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
	where datediff(d,@date,det2.FromDate )=0 and isnull(det2.DriverCode,'')<>''
	group by det2.DriverCode) as det2MaxTime
left outer join CTM_JobDet2 as det2  on det2.DriverCode=det2MaxTime.DriverCode and det2.FromTime=det2MaxTime.MaxTime 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,@date,det2.FromDate )=0 and job.StatusCode<>'CNL'
) as temp group by DriverCode)
) as det2 on log.Driver=det2.DriverCode
where DATEDIFF(d,@date,log.Date)=0
order by DriverCode,IndexType,FromTime");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 100));

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        Common.WriteJsonP(true, Common.DataTableToJson(dt));

    }


    [WebMethod]
    public void Controller_Calendar_ContainerStatus_GetDataList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = "";

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string sql = string.Format(@"select det1.Id,det1.JobNo,det1.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode,job.ClientId,job.WarehouseAddress 
from CTM_JobDet1 as det1
left outer join(select * From CTM_JobDet2 where Id in(
select max(det2.Id) From (
select det2.Det1Id,MAX(FromTime) as MaxTime
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where datediff(d,@date,det2.FromDate )=0
group by det2.Det1Id) as MaxTime
left outer join CTM_JobDet2 as det2 on det2.Det1Id=MaxTime.Det1Id and det2.FromTime=MaxTime.MaxTime
group by det2.Det1Id
)) as det2 on det1.Id=det2.Det1Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,@date,det2.FromDate )=0 and job.StatusCode<>'CNL'
order by det1.ContainerNo");
        list.Add(new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 10));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }


    [WebMethod]
    public void Controller_Calendar_TrailerStatus_GetDataList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = "";

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string sql = string.Format(@"select Mast.Code as ChessisCode,Mast.Remark as Size,temp.Id,temp.ContainerNo,temp.ContainerType,temp.DriverCode,temp.FromCode,temp.ToCode,temp.TripCode,temp.FromTime,temp.ToTime,temp.Statuscode 
from CTM_MastData as Mast
left outer join (
select det2.Id,det2.ContainerNo,det1.ContainerType,det2.ChessisCode,det2.DriverCode,det2.FromCode,det2.ToCode,det2.TripCode,det2.FromTime,det2.ToTime,det2.Statuscode  
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where det2.Id in(
select MAX(Id) from(
select det2.Id,det2MaxTime.ChessisCode
from (
select det2.ChessisCode,MAX(FromTime) as MaxTime
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where datediff(d,@date,det2.FromDate )=0 and ISNULL(det2.ChessisCode,'')<>''
group by det2.ChessisCode
) as det2MaxTime 
left outer join CTM_JobDet2 as det2 on det2.ChessisCode=det2MaxTime.ChessisCode and det2.FromTime=det2MaxTime.MaxTime
left outer join CTM_JobDet1 as det1 on det2.det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where DATEDIFF(d,@date,det2.FromDate )=0 and job.StatusCode<>'CNL'
) as temp group by ChessisCode)
) as temp on temp.ChessisCode=Mast.Code 
where Mast.Type='chessis'
order by Id desc,ChessisCode");
        list.Add(new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 10));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }


}
