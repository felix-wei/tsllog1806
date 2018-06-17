using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_WH01 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Connect_WH01 : System.Web.Services.WebService
{
    public Connect_WH01()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent();
    }

    #region Import
    [WebMethod]
    public void XG_FCL_Import_Calendar_GetDataList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        //        string sql = string.Format(@"select c.ContNo,c.SealNo,c.ContStatus,c.ActualDate,c.row_id as Oid,c.JobNo as JobOrder,c.VesselNo,c.Pol,c.Pod 
        //from job_order as c
        //where datediff(day,ActualDate,@date)=0 and c.JobType='I'
        //order by case c.ContStatus when 'UNSTUFFING' THEN 1 WHEN 'STUFFING' THEN 1 WHEN 'SCHEDULED' THEN 2 WHEN 'COMPLETED' THEN 4 ELSE 3 END");
        string sql = string.Format(@"select det1.ContainerNo as ContNo,det1.SealNo,det1.CfsStatus as ContStatus,det1.ScheduleDate as ActualDate,det1.Id as Oid,det1.JobNo as JobOrder,
job.Vessel as VesselNo,job.Pol,job.Pod,det1.StatusCode as truckingStatus,job.WareHouseCode,(select top 1 Name from xxparty as p where p.partyId= job.ClientId) as ClientId
from CTM_JobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where job.JobType='IMP' and datediff(d,det1.ScheduleStartDate,@date)=0 and IsWarehouse='Yes' and det1.CfsStatus in ('Started','Scheduled','Completed') and job.WareHouseCode<>''");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_FCL_Import_Detail_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select det1.Id as Oid,det1.ContainerNo as ContNo,det1.SealNo,det1.CfsStatus as ContStatus,det1.ContainerType as ContType,det1.ScheduleDate as ActualDate,'' as ActualTime,
job.JobNo as JobOrderNo,job.JobType,job.Vessel as  VesselNo,job.Voyage as VoyNo,job.EtaDate as Eta,job.ClientId as CustCode,job.ClientRefNo as ImpRefNo,job.CreateBy as ByWho,
job.Pol,job.Pod,'' as OrderType,'' as Haulier,'' as UserId,'' as FileClosed,job.CarrierId as Carrier,job.CarrierBkgNo as BookingNo
from CTM_JobDet1 as det1
left outer join ctm_job as job on det1.JobNo=job.JobNo
where det1.Id=@Oid");
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);

        sql = string.Format(@"select att.Id as Oid,att.FileType as FileType,att.FileName as FileName,att.FilePath as FilePath,att.FileNote as FileNote,1 as ShowCust  
from CTM_Attachment as att 
left outer join CTM_JobDet1 as j on j.ContainerNo=att.ContainerNo and j.JobNo=att.refno
where j.Id=@Oid");
        dt = ConnectSql_mb.GetDataTable(sql, list);
        dt.Columns.Add("FP500", typeof(string));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string path = dt.Rows[i]["FilePath"].ToString();
            if (RebuildImage.Image_ExistOtherSize(path, dt.Rows[i]["FileType"].ToString(), 500))
            {
                path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
            }

            dt.Rows[i]["FP500"] = path;
        }

        string attachment = Common.DataTableToJson(dt);

        context = string.Format(@"{0}mast:{2},attachment:{3}{1}", "{", "}", mast, attachment);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_FCL_Import_Detail_ChangeStatus(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        string sql0 = "";
        string CfsStatus = job["status"].ToString();
        if (CfsStatus == "Started")
        {
            sql0 = ",ScheduleStartDate=getdate(),ScheduleStartTime=convert(nvarchar(5),getdate(),114)";
        }
        if (CfsStatus == "Completed")
        {
            sql0 = ",CompletionDate=getdate(),CompletionTime=convert(nvarchar(5),getdate(),114)";
        }
        string sql = string.Format(@"update CTM_JobDet1 set CfsStatus=@ContStatus{0} 
where Id=@Oid", sql0);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContStatus", CfsStatus, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {
            status = "1";
            C2.CtmJobDet1.contWarehouseStatusChanged(SafeValue.SafeInt(job["No"].ToString(), 0));


            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCONT(SafeValue.SafeInt(job["No"], 0));
            //lg.Remark = "Container change warehouse status:" + CfsStatus;
            lg.setActionLevel(SafeValue.SafeInt(job["No"], 0), CtmJobEventLogRemark.Level.Container, 5, ":" + CfsStatus);
            lg.log();
        }

        // **** additional action

        //L.Audit("EMAIL","TC", "", "", "","");	

        // change status alert email
        //try
        //{
        //    string _sql = string.Format(@"select ContNo,Ft20,Ft40,Ft45,FtType,Eta,VesselNo,VoyNo,Pol,Pod from job_order where row_id={0}", job["No"]);
        //    string _sub = "ABC";

        //    DataTable dcon = D.List(_sql);
        //    if (dcon.Rows.Count > 0)
        //    {
        //        _sub = string.Format("Import Container Status : {0}=>{6} ({1}{2}:{3}:{4}:{5}) ",
        //    dcon.Rows[0]["ContNo"],
        //    (dcon.Rows[0]["Ft20"].ToString() == "1" ? "20" : "") +
        //    (dcon.Rows[0]["Ft40"].ToString() == "1" ? "40" : "") +
        //    (dcon.Rows[0]["Ft45"].ToString() == "1" ? "45" : "")
        //    ,
        //    dcon.Rows[0]["FtType"],
        //    dcon.Rows[0]["VesselNo"],
        //    R.Date(dcon.Rows[0]["Eta"]),
        //    dcon.Rows[0]["Pol"],
        //    job["status"]);

        //        //L.Audit("EMAIL", "TC", "", "", "", _sub);

        //        string err = Helper.Email.SendEmail("demo@cargo.ms",
        //    "",
        //    "admin@cargo.ms", _sub,
        //     "Container Status Notification", "");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    //L.Audit("EMAIL", "TC", "", "", ex.Message, ex.StackTrace.Substring(0, 100));
        //    //status = "0";
        //    //context=ex.Message+ex.StackTrace;
        //}

        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void XG_FCL_Import_Detail_ChangeSavePending(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        string sql0 = "";
        string CfsStatus = job["status"].ToString();
        string date = job["date"].ToString();
        string time = job["time"].ToString();
        if (CfsStatus == "Started" || CfsStatus == "Scheduled")
        {
            sql0 = ",ScheduleStartDate=@ScheduleDate,ScheduleStartTime=@ScheduleTime";
        }
        if (CfsStatus == "Completed")
        {
            sql0 = ",CompletionDate=@ScheduleDate,CompletionTime=@ScheduleTime";
        }
        string sql = string.Format(@"update CTM_JobDet1 set CfsStatus=@ContStatus{0} 
where Id=@Oid", sql0);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContStatus", CfsStatus, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        list.Add(new ConnectSql_mb.cmdParameters("@ScheduleDate", date, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ScheduleTime", time, SqlDbType.NVarChar, 5));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {
            status = "1";
            C2.CtmJobDet1.contWarehouseStatusChanged(SafeValue.SafeInt(job["No"].ToString(), 0));


            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCONT(SafeValue.SafeInt(job["No"], 0));
            //lg.Remark = "Container change warehouse status:" + CfsStatus;
            lg.setActionLevel(SafeValue.SafeInt(job["No"], 0), CtmJobEventLogRemark.Level.Container, 5, ":" + CfsStatus);
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }
    #endregion

    #region Export
    [WebMethod]
    public void XG_FCL_Export_Calendar_GetDataList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        //string sql_where = "";
        //if (job["role"].ToString().Equals("Driver"))
        //{
        //    sql_where = " and det2.DriverCode=@DriverCode";
        //}
        //        string sql = string.Format(@"select c.ContNo,c.SealNo,c.ContStatus,c.ActualDate,c.row_id as Oid,c.JobNo as JobOrder,c.VesselNo,c.Pol,c.Pod 
        //from job_order as c
        //where datediff(day,ActualDate,@date)=0 and c.JobType='E'
        //order by case c.ContStatus when 'UNSTUFFING' THEN 1 WHEN 'STUFFING' THEN 1 WHEN 'SCHEDULED' THEN 2 WHEN 'COMPLETED' THEN 4 ELSE 3 END");
        string sql = string.Format(@"select det1.ContainerNo as ContNo,det1.SealNo,det1.CfsStatus as ContStatus,det1.ScheduleDate as ActualDate,det1.Id as Oid,det1.JobNo as JobOrder,
job.Vessel as VesselNo,job.Pol,job.Pod,det1.StatusCode as truckingStatus,job.WareHouseCode,(select top 1 Name from xxparty as p where p.partyId= job.ClientId) as ClientId
from CTM_JobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where job.JobType='EXP' and datediff(d,det1.ScheduleStartDate,@date)=0 and IsWarehouse='Yes' and det1.CfsStatus in ('Started','Scheduled','Completed') and job.WareHouseCode<>''");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        //cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        //list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_FCL_Export_Detail_ChangeStatus(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        //        string sql = string.Format(@"update job_order set ContStatus=@ContStatus 
        //where row_id=@Oid");

        string sql0 = "";
        string CfsStatus = job["status"].ToString();
        if (CfsStatus == "Started")
        {
            sql0 = ",ScheduleStartDate=getdate(),ScheduleStartTime=convert(nvarchar(5),getdate(),114)";
        }
        if (CfsStatus == "Completed")
        {
            sql0 = ",CompletionDate=getdate(),CompletionTime=convert(nvarchar(5),getdate(),114)";
        }
        string sql = string.Format(@"update CTM_JobDet1 set CfsStatus=@ContStatus{0} 
where Id=@Oid", sql0);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContStatus", CfsStatus, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {
            status = "1";
            C2.CtmJobDet1.contWarehouseStatusChanged(SafeValue.SafeInt(job["No"].ToString(), 0));

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCONT(SafeValue.SafeInt(job["No"], 0));
            //lg.Remark = "Container change warehouse status:" + CfsStatus;
            lg.setActionLevel(SafeValue.SafeInt(job["No"], 0), CtmJobEventLogRemark.Level.Container, 5, ":" + CfsStatus);
            lg.log();
        }

        //try
        //{
        //    string _sql = string.Format(@"select c.ContNo,c.Ft20,c.Ft40,c.Ft45,c.FtType,c.Eta,c.VesselNo,c.VoyNo,c.Pol,c.Pod from job_order c where row_id={0}", job["No"]);
        //    string _sub = "ABC";

        //    DataTable dcon = D.List(_sql);
        //    if (dcon.Rows.Count > 0)
        //    {
        //        _sub = string.Format("Export Container Status : {0}=>{6} ({1}{2}:{3}:{4}:{5}) ",
        //    dcon.Rows[0]["ContNo"],
        //    (dcon.Rows[0]["Ft20"].ToString() == "1" ? "20" : "") +
        //    (dcon.Rows[0]["Ft40"].ToString() == "1" ? "40" : "") +
        //    (dcon.Rows[0]["Ft45"].ToString() == "1" ? "45" : "")
        //    ,
        //    dcon.Rows[0]["FtType"],
        //    dcon.Rows[0]["VesselNo"],
        //    R.Date(dcon.Rows[0]["Eta"]),
        //    dcon.Rows[0]["Pol"],
        //    job["status"]);

        //        //L.Audit("EMAIL", "TC", "", "", "", _sub);

        //        string err = Helper.Email.SendEmail("demo@cargo.ms",
        //    "",
        //    "admin@cargo.ms", _sub,
        //     "Container Status Notification", "");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    //L.Audit("EMAIL", "TC", "", "", ex.Message, ex.StackTrace.Substring(0, 100));
        //    //status = "0";
        //    //context=ex.Message+ex.StackTrace;
        //}


        Common.WriteJsonP(status, context);
    }
    #endregion

    #region Pending

    [WebMethod]
    public void XG_FCL_Pending_Calendar_GetDataList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select det1.ContainerNo as ContNo,det1.SealNo,det1.CfsStatus as ContStatus,det1.ScheduleDate as ActualDate,det1.Id as Oid,det1.JobNo as JobOrder,
job.Vessel as VesselNo,job.Pol,job.Pod,det1.StatusCode as truckingStatus,job.WareHouseCode,job.ClientId
from CTM_JobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where job.JobType in ('IMP','EXP','WGR','WDO') and datediff(d,det1.ScheduleStartDate,@date)=0 and IsWarehouse='Yes' and det1.CfsStatus in ('','Pending') and job.WareHouseCode<>''");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_FCL_Pending_GetDataList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select det1.ContainerNo as ContNo,det1.SealNo,det1.CfsStatus as ContStatus,det1.ScheduleDate as ActualDate,det1.Id as Oid,det1.JobNo as JobOrder,
job.Vessel as VesselNo,job.Pol,job.Pod,det1.StatusCode as truckingStatus,job.WareHouseCode,job.ClientId
from CTM_JobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where job.JobType in ('IMP','EXP','TPT','WGR','WDO') and IsWarehouse='Yes' and det1.CfsStatus in ('','Pending') and job.WareHouseCode<>'' and det1.ContainerNo like @ContNo");

        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", "%" + job["No"] + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }



    #endregion

    #region attachment
    [WebMethod]
    public void XG_FCL_Camera_GetList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Id as Oid,ContainerNo as ContNo,JobNo as JobOrder 
from ctm_jobdet1
where Id=@Oid");
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);

        sql = string.Format(@"select det.HblNo as HblN,cast(det.Id as nvarchar) as Id
from job_house as det
left outer join CTM_JobDet1 as cont on det.JobNo=cont.JobNo
where cont.Id=@Oid 
union all
select '0','' 
order by Id");
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string cargo = Common.DataTableToJson(dt);

        context = string.Format(@"{0}mast:{2},cargo:{3}{1}", "{", "}", mast, cargo);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_FCL_Attachment_Add(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        //string sql = string.Format(@"insert into job_photo (JobNo,ContNo,Name,Path,UploadType,EntryDate,DoNo,OrderType,Remark,ShowCust) values (@OrderNo,@ContNo,@FileName,@FilePath,@FileType,Getdate(),@DoNo,@OrderType,@FileNote,@ShowCust)");
        string sql = string.Format(@"insert into CTM_Attachment(RefNo,ContainerNo,FileName,FilePath,FileType,CreateDateTime,JobNo,JobType,FileNote) 
values (@OrderNo,@ContNo,@FileName,@FilePath,@FileType,Getdate(),@DoNo,@OrderType,@FileNote)
select @@Identity");
        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
        if (fileStart == null)
        {
            fileStart = "";
        }
        string fileEnd = job["FilePath"].ToString();
        fileEnd = fileEnd.Substring(0, fileEnd.LastIndexOf('/')) + "/500/" + fileEnd.Substring(fileEnd.LastIndexOf('/') + 1);
        fileEnd = fileStart + fileEnd;

        cpar = new ConnectSql_mb.cmdParameters("@OrderNo", job["JobNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContNo", job["ContainerNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DoNo", job["TripId"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileType", "Image", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@OrderType", "CTM", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileName", job["FileName"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FilePath", fileEnd, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["CreateBy"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileNote", job["FileNote"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        list.Add(new ConnectSql_mb.cmdParameters("@ShowCust", job["ShowCust"].ToString().Equals("Y"), SqlDbType.Bit));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
        if (res.status)
        {
            status = "1";
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(res.context, 0));
            //lg.Remark = "Attachment upload file:" + job["FileName"];
            lg.setActionLevel(SafeValue.SafeInt(res.context, 0), CtmJobEventLogRemark.Level.Attachment, 1, ":" + job["FileType"] + "[" + job["FileName"] + "]");
            lg.log();
        }
        else
        {
            context = Common.StringToJson(res.context);
        }

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_FCL_Attachment_AddMutiple(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        JArray jar = (JArray)JsonConvert.DeserializeObject(job["list"].ToString());

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = "";

        //string sql = string.Format(@"insert into job_photo (JobNo,ContNo,Name,Path,UploadType,EntryDate,DoNo,OrderType,Remark,ShowCust) values (@OrderNo,@ContNo,@FileName,@FilePath,@FileType,Getdate(),@DoNo,@OrderType,@FileNote,@ShowCust)");
        string sql = string.Format(@"insert into CTM_Attachment(RefNo,ContainerNo,FileName,FilePath,FileType,CreateDateTime,JobNo,JobType,FileNote,TripId) 
values (@OrderNo,@ContNo,@FileName,@FilePath,@FileType,Getdate(),@CargoId,@OrderType,@FileNote,@TripId)
select @@Identity");
        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
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
            fileEnd = fileEnd.Substring(0, fileEnd.LastIndexOf('/')) + "/500/" + fileEnd.Substring(fileEnd.LastIndexOf('/') + 1);
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

    [WebMethod]
    public void XG_FCL_Attachment_GetDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select att.Id as Oid,att.FileType as FileType,att.FileName as FileName,att.FilePath as FilePath,att.FileNote as FileNote,1 as ShowCust,
isnull(att.JobNo,'') as DoNo,cargo.HblNo as DoNo1,cont.Id as contId 
from CTM_Attachment as att
left outer join CTM_JobDet1 as cont on cont.JobNo=att.RefNo and att.ContainerNo=cont.ContainerNo
left outer join job_house as cargo on cast(cargo.Id as nvarchar)=att.JobNo
where att.Id=@Oid");

        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["Oid"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Add("FP500", typeof(string));
            string path = dt.Rows[0]["FilePath"].ToString();
            if (RebuildImage.Image_ExistOtherSize(path, dt.Rows[0]["FileType"].ToString(), 500))
            {
                path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
            }

            dt.Rows[0]["FP500"] = path;
            status = "1";
        }
        context = Common.DataRowToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_FCL_Attachment_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        string sql = string.Format(@"update CTM_Attachment set JobNo=@DoNo,FileNote=@FileNote where Id=@Oid");

        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["Oid"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DoNo", job["DoNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileNote", job["FileNote"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        list.Add(new ConnectSql_mb.cmdParameters("@ShowCust", job["ShowCust"].ToString().Equals("Y"), SqlDbType.Bit));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (res.status)
        {
            status = "1";
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(job["Oid"], 0));
            //lg.Remark = "Attachment update";
            lg.setActionLevel(SafeValue.SafeInt(job["Oid"], 0), CtmJobEventLogRemark.Level.Attachment, 3);
            lg.log();
        }
        else
        {
            context = Common.StringToJson(res.context);
        }

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_FCL_Attachment_Delete(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        //===========log
        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Platform_isMobile();
        lg.Controller = SafeValue.SafeString(job["user"]);
        lg.Lat = SafeValue.SafeString(job["Lat"]);
        lg.Lng = SafeValue.SafeString(job["Lng"]);
        //lg.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(job["Oid"], 0));
        //lg.Remark = "Attachment delete";
        lg.setActionLevel(SafeValue.SafeInt(job["Oid"], 0), CtmJobEventLogRemark.Level.Attachment, 2);

        string sql = string.Format(@"delete CTM_Attachment where Id=@Oid");

        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["Oid"], SqlDbType.Int);
        list.Add(cpar);
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (res.status)
        {
            status = "1";
            lg.log();
        }
        else
        {
            context = Common.StringToJson(res.context);
        }

        Common.WriteJsonP(status, context);
    }

    #endregion

    #region search

    [WebMethod]
    public void XG_Container_Search_GetList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select top 30 c.Id as Oid,ContainerNo as ContNo,job.EtaDate as Eta,job.Vessel as VesselNo,job.Pol,job.Pod,job.JobType
from CTM_JobDet1 as c
left outer join CTM_Job as job on job.JobNo=c.JobNo
where c.ContainerNo like @ContNo
order by job.EtaDate desc");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@ContNo", "%" + job["no"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_Cargo_Search_GetList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select top 30 d.Id as Oid,det1.ContainerNo as ContNo,d.HblNo as HblN,job.EtaDate as Eta,job.Pol,job.Pod,job.JobType,d.QtyOrig as ActualQty
,isnull(d.OpsType,'L') as CargoStatus,d.BookingNo as ExpBkgN,d.WeightOrig as Weight,d.VolumeOrig as M3,job.Vessel as VesselNo,job.Voyage as VoyNo
from job_house as d
left outer join CTM_JobDet1 as det1 on det1.jobno=d.jobno and d.ContNo=det1.ContainerNo
left outer join CTM_Job as job on d.JobNo=job.JobNo 
where d.HblNo like @HblN
order by d.HblNo,(case job.JobType when 'IMP' then 1 when 'EXP' then 2 when 'WGR' then 3 when 'WDO' then 4 else 10 end),d.EntryDate desc");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@HblN", "%" + job["no"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_Cargo_Detail_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select det.Id as Oid,det1.Id as contId,det.HblNo as HblN,det.PackTypeOrig as PackType,det.WeightOrig Weight,det.VolumeOrig as M3,det.QtyOrig TotQty,det.OpsType as Type,
det.SkuCode,det.PackQty,det.PackUom, '' as pod,det.BkgSkuCode,det.BkgSkuQty,det.BkgSkuUnit,det.BookingItem,det.ActualItem,
det.Marking2,det.Marking1,det.Remark1,det.InventoryId,det.Location,
det.Qty,det.UomCode,det.Weight as BkgWeight,det.Volume as BkgVolume,
det1.ContainerNo as ContNo,det1.SealNo,det1.ContainerType as ContStatus,det1.ContainerType as ContType,det1.ScheduleDate as ActualDate,'' as ActualTime,
job.JobNo as JobOrderNo,job.JobType,job.Vessel as VesselNo,job.Voyage as VoyNo,job.EtaDate as Eta,job.ClientId as CustCode,job.ClientRefNo as ImpRefNo,job.CreateBy as ByWho,job.WareHouseCode,
job.Pol,job.Pod,'' as OrderType,job.CarrierId,det.BookingNo,det.CargoStatus as StatusCode,det.TripId,det.LengthPack,det.WidthPack,det.HeightPack
from job_house as det
left outer join CTM_JobDet1 as det1 on det.JobNo=det1.JobNo and det.ContNo=det1.ContainerNo
left outer join ctm_job as job on job.JobNo=det.JobNo
where det.Id=@Oid");
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);

        sql = string.Format(@"select att.Id as Oid,att.FileType as FileType,att.FileName as FileName,att.FilePath as FilePath,att.filenote as FileNote,1 as ShowCust 
from CTM_Attachment as att
left outer join job_house as det on det.JobNo=att.RefNo and cast(det.Id as nvarchar)=att.JobNo 
where det.Id=@Oid");

        dt = ConnectSql_mb.GetDataTable(sql, list);
        dt.Columns.Add("FP500", typeof(string));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string path = dt.Rows[i]["FilePath"].ToString();
            if (RebuildImage.Image_ExistOtherSize(path, dt.Rows[i]["FileType"].ToString(), 500))
            {
                path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
            }
            dt.Rows[i]["FP500"] = path;
        }
        string attachment = Common.DataTableToJson(dt);

        context = string.Format(@"{0}mast:{2},attachment:{3}{1}", "{", "}", mast, attachment);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_Cargo_Detail_Save(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        decimal LengthPack = SafeValue.SafeDecimal(job["LengthPack"]);
        decimal WidthPack = SafeValue.SafeDecimal(job["WidthPack"]);
        decimal HeightPack = SafeValue.SafeDecimal(job["HeightPack"]);

        List<ConnectSql_mb.cmdParameters> list = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"update job_house set 
QtyOrig=@TotQty,PackTypeOrig=@PackType,WeightOrig=@Weight,VolumeOrig=@M3,Remark1=@Remark1,Marking2=@Marking2,Marking1=@Marking1,
SkuCode=@SkuCode,PackQty=@PackQty,PackUom=@PackUom,ActualItem=@ActualItem,LengthPack=@LengthPack,WidthPack=@WidthPack,HeightPack=@HeightPack,
InventoryId=@InventoryId,Location=@Location 
where Id=@Oid");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Oid", job["Oid"], SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@TotQty", SafeValue.SafeInt(job["TotQty"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@PackType", job["PackType"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Weight", job["Weight"], SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@M3", job["M3"], SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark1", job["Remark1"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Marking2", job["Marking2"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Marking1", job["Marking1"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@PackQty", SafeValue.SafeInt(job["PackQty"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@PackUom", job["PackUom"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@SkuCode", job["SkuCode"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ActualItem", SafeValue.SafeString(job["ActualItem"]), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@LengthPack", LengthPack, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@WidthPack", WidthPack, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@HeightPack", HeightPack, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@InventoryId", job["InventoryId"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Location", job["Location"], SqlDbType.NVarChar, 100));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCARGO(SafeValue.SafeInt(job["Oid"], 0));
            //lg.Remark = "Cargo update";
            lg.setActionLevel(SafeValue.SafeInt(job["Oid"], 0), CtmJobEventLogRemark.Level.Cargo, 3);
            lg.log();
        }
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void XG_Cargo_Detail_Delete(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);
        int cargoId = SafeValue.SafeInt(job["Oid"], 0);

        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Platform_isMobile();
        lg.Controller = SafeValue.SafeString(job["user"]);
        lg.Lat = SafeValue.SafeString(job["Lat"]);
        lg.Lng = SafeValue.SafeString(job["Lng"]);
        lg.setActionLevel(cargoId, CtmJobEventLogRemark.Level.Cargo, 2);
        

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"delete from job_house where Id=@Oid");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Oid", cargoId, SqlDbType.Int));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {

            //===========log
            lg.log();
        }
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_Cargo_Detail_ChangeStatus(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string status = "0";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        //string sql = string.Format(@"update job_receipt set StatusCode=@StatusCode,ReceiveDate=getdate(),UserId=@UserId where row_id=@row_id");
        string sql = string.Format(@"update job_house set CargoStatus=@StatusCode,EntryDate=getdate(),StockDate=getdate() where Id=@row_id");
        list.Add(new ConnectSql_mb.cmdParameters("@row_id", SafeValue.SafeInt(job["Oid"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", job["StatusCode"], SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@UserId", job["UserId"], SqlDbType.NVarChar, 100));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            status = "1";
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCARGO(SafeValue.SafeInt(job["Oid"], 0));
            //lg.Remark = "Cargo status change:" + job["StatusCode"];
            lg.setActionLevel(SafeValue.SafeInt(job["Oid"], 0), CtmJobEventLogRemark.Level.Cargo, 4, ":" + job["StatusCode"]);
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void XG_CargoList_ByContainer_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select det.Id as Oid,det.HblNo as HblN,det.HblNo as DnNo,det.JobType,det.Commodity,det.LandStatus,det.BookingNo
,det.PackTypeOrig,det.WeightOrig,det.VolumeOrig,det.QtyOrig,det.BookingNo,det.Marking2 as Desc1,det.CargoStatus,
det.Qty,det.UomCode,det.Weight as BkgWeight,det.Volume as BkgVolume,det.BkgSkuCode,det.BkgSkuQty,det.BkgSkuUnit,
det.SkuCode,det.PackQty,det.PackUom
from job_house as det 
left outer join CTM_JobDet1 as cont on det.JobNo=cont.JobNo and det.ContNo=cont.ContainerNo
where cont.Id=@Oid 
order by det.HblNo");
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_CargoList_ByContainer_ChangStatus(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"update job_det set {0}='{1}' where row_id in ({2})", job["ColName"], job["Status"], job["No"]);
        list = new List<ConnectSql_mb.cmdParameters>();
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_CargoList_ByTrip_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select det.Id as Oid,det.HblNo as HblN,det.HblNo as DnNo,det.JobType,det.Commodity,det.LandStatus,det.BookingNo
,det.PackTypeOrig,det.WeightOrig,det.VolumeOrig,det.QtyOrig,det.BookingNo,det.Marking2 as Desc1,det.CargoStatus,
det.Qty,det.UomCode,det.Weight as BkgWeight,det.Volume as BkgVolume,det.BkgSkuCode,det.BkgSkuQty,det.BkgSkuUnit,
det.SkuCode,det.PackQty,det.PackUom,det.BookingItem,det.ActualItem,
(select top 1 PermitNo from ref_permit where HblNo=det.HblNo and JobNo=det.JobNo) as PermitNo
from job_house as det 
where det.TripId=@Oid 
order by det.HblNo");
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void XG_CargoList_ByTrip_GetCargoList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int tripId = SafeValue.SafeInt(job["no"], 0);
        string sql = string.Format(@"select JobNo,TripCode from ctm_jobdet2 where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", tripId, SqlDbType.Int));

        string context = "[]";
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count > 0)
        {
            string jobNo = SafeValue.SafeString(dt.Rows[0]["JobNo"]);
            string tripCode = SafeValue.SafeString(dt.Rows[0]["TripCode"]);
            string cargoType = "OUT";
            if (tripCode == "WGR")
            {
                cargoType = "IN";
            }
            sql = string.Format(@"with tb0 as (
select Id,Qty,QtyOrig,BookingNo,PackTypeOrig,ActualItem,SkuCode,PackQty,PackUom,Location,
Marking2,Remark1,Marking1,CargoType,LineId,JobNo,InventoryId
from job_house where JobNo=@JobNo and CargoType=@CargoType
)
select LineId,BookingNo,JobNo,sum(Qty) as bkQty,sum(QtyOrig) as atQty,sum(Qty)-sum(QtyOrig) as blQty,
(select top 1 PackTypeOrig from tb0 where LineId=tt.LineId) as PackTypeOrig,
(select top 1 ActualItem from tb0 where LineId=tt.LineId) as ActualItem,
(select top 1 SkuCode from tb0 where LineId=tt.LineId) as SkuCode,
(select top 1 PackQty from tb0 where LineId=tt.LineId) as PackQty,
(select top 1 PackUom from tb0 where LineId=tt.LineId) as PackUom,
(select top 1 Location from tb0 where LineId=tt.LineId) as Location,
(select top 1 Marking2 from tb0 where LineId=tt.LineId) as Marking2,
(select top 1 Remark1 from tb0 where LineId=tt.LineId) as Remark1,
(select top 1 Marking1 from tb0 where LineId=tt.LineId) as Marking1,
(select top 1 InventoryId from tb0 where LineId=tt.LineId) as InventoryId
from tb0 as tt group by LineId,BookingNo,JobNo");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@CargoType", cargoType, SqlDbType.NVarChar, 10));
            dt = ConnectSql_mb.GetDataTable(sql, list);
            context = Common.DataTableToJson(dt);
        }
        Common.WriteJsonP(true, context);
    }
    [WebMethod]
    public void XG_CargoList_ByTrip_AddCargo(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        JArray jar = (JArray)JsonConvert.DeserializeObject(job["list"].ToString());
        string user = SafeValue.SafeString(job["user"]);
        int tripId = SafeValue.SafeInt(job["tripId"], 0);
        string Lat = SafeValue.SafeString(job["Lat"]);
        string Lng = SafeValue.SafeString(job["Lng"]);

        Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query1) as C2.CtmJobDet2;

        if (trip != null)
        {
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isMobile();
            elog.Controller = user;
            elog.Lat = Lat;
            elog.Lng = Lng;
            for (int i = 0; i < jar.Count; i++)
            {

                string jobNo = trip.JobNo;
                int LineId = SafeValue.SafeInt(jar[i]["LineId"], 0);
                decimal valueQty = SafeValue.SafeDecimal(jar[i]["valueQty"]);
                decimal valueSkuQty = SafeValue.SafeDecimal(jar[i]["valueSkuQty"]);
                string Inventory = SafeValue.SafeString(jar[i]["Inventory"]);

                string cargoType = "OUT";
                if (trip.TripCode == "WGR")
                {
                    cargoType = "IN";
                }

                #region add DO cargo
                //Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + LineId + "");
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "CargoType='" + cargoType + "' and LineId=" + LineId);
                C2.JobHouse house = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;

                house.CargoStatus = "C";
                house.CargoType = cargoType;
                house.JobNo = trip.JobNo;
                house.ContNo = "";
                house.JobType = trip.JobType;
                house.QtyOrig = valueQty;
                house.PackQty = valueSkuQty;
                //house.Weight = 0;
                //house.Volume = 0;
                //house.WeightOrig = 0;
                //house.VolumeOrig = 0;
                //house.LandStatus = "Normal";
                //house.DgClass = "Normal";
                //house.DamagedStatus = "Normal";
                house.RefNo = trip.JobNo;
                //house.ClientId = ;
                house.LineId = LineId;
                house.ContId = 0; //SafeValue.SafeInt(cbb_ContNo.Value, 0);
                house.ContNo = "";// SafeValue.SafeString(cbb_ContNo.Text);
                house.TripIndex = trip.TripIndex;// SafeValue.SafeString(cbb_TripNo.Text);
                house.TripId = trip.Id;// SafeValue.SafeInt(cbb_TripNo.Value, 0);

                house.Qty = 0;
                house.OpsType = cargoType == "IN" ? "Storage" : "Delivery";
                house.InventoryId = Inventory;

                house.PackTypeOrig = house.UomCode;
                house.WeightOrig = house.Weight;
                house.VolumeOrig = house.Volume;
                house.SkuCode = house.BkgSKuCode;
                house.PackQty = house.BkgSkuQty;
                house.PackUom = house.BkgSkuUnit;
                house.ActualItem = house.BookingItem;
                house.StockDate = DateTime.Now;

                C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(house);

                elog.setActionLevel(house.Id, CtmJobEventLogRemark.Level.Cargo, 1);
                elog.log();
                #endregion


            }
        }

        string context = Common.StringToJson("");
        Common.WriteJsonP(true, context);
    }

    #endregion

    #region Driver schedule
    [WebMethod]
    public void XG_Driver_Calendar_GetDataList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        //if (job["role"].ToString().Equals("Driver"))
        //{
        //    sql_where = " and det2.DriverCode=@DriverCode";
        //}
        string sql = string.Format(@"select r.row_id as Oid,'' as Driver_Code,r.HblN,
j.ContNo,j.SealNo,j.ContStatus,j.ActualDate,j.JobNo as JobOrder,j.VesselNo,j.Pol,j.Pod 
from job_receipt as r 
left outer join job_order as j on r.JobNo=j.JobNo and r.jobType=j.JobType
where r.JobNo<>'' and r.ContNo<>'' and datediff(day,ActualDate,@date)=0 ");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        //cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        //list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }


    [WebMethod]
    public void XG_Driver_View_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select rec.row_id as Oid,det.HblN,det.PackType,det.Weight,det.M3,det.Type,'' as pod,det.Marking2,det.Desc1,det.Remark1,det.ClearedBy,det.ClearedOn,det.ClearRemarks,
det.ClearanceRmk,det.BalanceQty,det.BalanceWt,det.BalanceM3,
job.ContNo,job.SealNo,job.ContStatus,job.Ft20,job.Ft40,job.Ft45,job.FtType,job.ActualDate,job.ActualTime,
job.JobNo as JobOrderNo,job.JobType,job.VesselNo,job.VoyNo,job.Eta,job.CustCode,job.ImpRefNo,job.ByWho,job.Pol,job.Pod,job.OrderType,'' as Haulier,job.UserId,'' as FileClosed,
job.Carrier,job.BookingNo
from job_receipt as rec
left outer join job_det as det on rec.HblN=det.HblN and rec.JobNo=det.JobNo and rec.JobType=det.JobType
left outer join job_order as job on det.JobNo=job.ContNo and det.JobNo=job.JobNo and job.JobType=det.JobType
where rec.row_id=@Oid");
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Oid", job["No"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);

        sql = string.Format(@"select att.row_id as Oid,UploadType as FileType,Name as FileName,Path as FilePath,att.Remark as FileNote,att.ShowCust  
from job_photo as att
left outer join job_receipt as rec on rec.JobNo=att.JobNo and rec.ContNo=att.ContNo and rec.HblN=att.DoNo
where rec.row_id=@Oid");

        dt = ConnectSql_mb.GetDataTable(sql, list);
        dt.Columns.Add("FP500", typeof(string));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string path = dt.Rows[i]["FilePath"].ToString();
            if (RebuildImage.Image_ExistOtherSize(path, dt.Rows[i]["FileType"].ToString(), 500))
            {
                path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
            }
            dt.Rows[i]["FP500"] = path;
        }
        string attachment = Common.DataTableToJson(dt);

        context = string.Format(@"{0}mast:{2},attachment:{3}{1}", "{", "}", mast, attachment);
        Common.WriteJsonP(status, context);
    }

    #endregion

}
