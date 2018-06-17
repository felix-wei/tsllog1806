using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_JobCrane 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Connect_JobCrane : System.Web.Services.WebService
{

    public Connect_JobCrane()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    [WebMethod]
    public void Crane_Calendar_GetDataList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        if (!job["role"].ToString().ToLower().Equals("admin"))
        {
            sql_where = " and det2.DriverCode=@DriverCode";
        }
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,DriverCode,TowheadCode,ChessisCode,det2.Statuscode,det2.Remark,det2.RequestVehicleType, 
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det2.DoubleMounting,det2.BookingDate,det2.BookingTime,det2.BookingTime2,v.ContractNo,det2.ByUser,
case det2.Statuscode when 'S' then 1 when 'C' then 3 when 'X' then 4 else 2 end as OrderIndex,
job.IsTrucking,job.IsWarehouse,job.IsLocal,job.IsAdhoc
From CTM_JobDet2 as det2
left outer join ref_Vehicle as v on det2.TowheadCode=v.VehicleCode
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where (DATEDIFF(day,BookingDate,@date)=0 ) and det2.TripCode='CRA' {0}
order by TowheadCode,convert(nvarchar(10), det2.BookingDate,120),det2.BookingTime desc", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        //context = Common.StringToJson(ConnectSql_mb.strcon);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Crane_AddNew(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string JobNo = job["JobNo"].ToString();
        string TowheadCode = job["TowheadCode"].ToString();
        string BookingDate = job["BookingDate"].ToString();
        string BookingTime = job["BookingTime"].ToString();
        string BookingTime2 = job["BookingTime2"].ToString();
        string DriverCode = job["DriverCode"].ToString();
        string BookingRemark = job["BookingRemark"].ToString();
        string ByUser = job["ByUser"].ToString();

        string ToCode = "";
        string Remark = "";
        string sql = string.Format(@"select DeliveryTo,SpecialInstruction from ctm_job where JobNo=@JobNo");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count == 1)
        {
            ToCode = dt.Rows[0]["DeliveryTo"].ToString();
            Remark = dt.Rows[0]["SpecialInstruction"].ToString();
        }


        sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot,BookingDate,BookingTime,BookingTime2,BookingRemark,
ByUser,CreateUser,UpdateUser,CreateTime,UpdateTime,Remark) values 
(@JobNo,'',@DriverCode,@TowheadCode,'',@FromCode,@FromDate,@FromTime,@ToCode,@FromDate,@FromTime,'P',
'','N','','',@TripCode,'Normal','N',@FromParkingLot,@BookingDate,@BookingTime,@BookingTime2,@BookingRemark,
@ByUser,@ByUser,@ByUser,getdate(),getdate(),@Remark)
select @@Identity");
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", DriverCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@TowheadCode", TowheadCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromCode", "", SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", "19000101", SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@FromTime", "00:00", SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@BookingDate", BookingDate, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@BookingTime", BookingTime, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@BookingTime2", BookingTime2, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@ToCode", ToCode, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@TripCode", "CRA", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromParkingLot", "", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@BookingRemark", BookingRemark, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ByUser", ByUser, SqlDbType.NVarChar, 20));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteScalar(sql, list);

        if (result.status)
        {
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isTRIP(SafeValue.SafeInt(result.context, 0));
            //lg.Remark = "Trip Add New";
            lg.setActionLevel(SafeValue.SafeInt(result.context, 0), CtmJobEventLogRemark.Level.Trip, 1);
            lg.log();
        }
        Common.WriteJsonP(result.status, Common.StringToJson(result.context));
    }


    [WebMethod]
    public void Crane_JobView_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = "";
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.DriverCode,det2.TowheadCode,det2.ChessisCode,job.Vessel,job.Voyage,
job.CarrierBkgNo AS ClientRefNo,job.CarrierId,p.Name as CarrierName,det2.Remark,det2.Remark1,det2.Statuscode,
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,det2.Carpark,det2.ToParkingLot,det2.ParkingZone,det2.TripCode,
det2.BookingDate,det2.BookingTime,det2.BookingTime2,det2.BookingRemark,det2.OtHour,det2.ByUser,det2.PodType,det2.OtHour,det2.RequestVehicleType  
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
left outer join XXParty as p on job.ClientId=p.PartyId
where det2.Id={0}", job["No"]);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string mast = "{}";
        string attachment = "[]";
        string incentive = "[]";
        string claims = "[]";
        if (dt.Rows.Count > 0)
        {
            mast = Common.DataRowToJson(dt);

            sql = string.Format(@"select Id,LineId,LineType,TripNo,ChgCode,ChgCodeDes,Qty,Price,VendorId from job_cost where TripNo=@TripNo and LineType='DP'");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@TripNo", SafeValue.SafeInt(job["No"], 0), SqlDbType.Int));
            dt = ConnectSql_mb.GetDataTable(sql, list);
            incentive = Common.DataTableToJson(dt);
            sql = string.Format(@"select Id,LineId,LineType,TripNo,ChgCode,ChgCodeDes,Qty,Price,VendorId from job_cost where TripNo=@TripNo and LineType='CL'");
            dt = ConnectSql_mb.GetDataTable(sql, list);
            claims = Common.DataTableToJson(dt);


            sql = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where TripId=@TripNo and FileType='Image'");
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
            attachment = Common.DataTableToJson(dt);

        }
        context = string.Format(@"{0}mast:{2},attachment:{3},incentive:{4},claims:{5}{1}", "{", "}", mast, attachment, incentive, claims);

        Common.WriteJsonP(true, context);
    }


    [WebMethod]
    public void Crane_ChangeStatus(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql1 = "";
        string sql = "";
        string Statuscode = job["Statuscode"].ToString();
        string ByUser = job["ByUser"].ToString();
        if (Statuscode.Equals("S"))
        {
            sql = string.Format(@"select DoubleMounting,DriverCode from CTM_JobDet2 where Id=@Id");
            list = new List<ConnectSql_mb.cmdParameters>();
            cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
            list.Add(cpar);
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            string dbMounting = "";
            string DriverCode = "";
            if (dt.Rows.Count > 0)
            {
                dbMounting = SafeValue.SafeString(dt.Rows[0]["DoubleMounting"], "");
                DriverCode = SafeValue.SafeString(dt.Rows[0]["DriverCode"], "");
            }
            sql = string.Format(@"select Id,DoubleMounting 
from ctm_jobdet2
where Statuscode='S' and DriverCode=@DriverCode");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", DriverCode, SqlDbType.NVarChar, 100));
            dt = ConnectSql_mb.GetDataTable(sql, list);

            if (dt.Rows.Count > 0)
            {
                status = "0";
                context = Common.StringToJson("Please end the other trip, then start this!");
            }
        }
        //if (Statuscode.Equals("C"))
        //{
        //    sql = string.Format(@"select isnull(sum(isnull(Qty,1)*isnull(Price,0)),0) as s from job_cost where TripNo=@Id and LineType='DP'");
        //    list = new List<ConnectSql_mb.cmdParameters>();
        //    cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
        //    list.Add(cpar);
        //    if (SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context, 0) == 0)
        //    {
        //        status = "0";
        //        context = Common.StringToJson("Require Trip Incentive!");
        //    }
        //}

        if (status.Equals("1"))
        {
            if (Statuscode.Equals("S"))
            {
                sql1 = ",FromDate=@FromDate,FromTime=@FromTime";
            }
            //if (Statuscode.Equals("C"))
            //{
            //    sql1 = ",ToDate=@ToDate,ToTime=@ToTime";
            //}
            if (Statuscode.Equals("C"))
            {
                sql1 = ",ToDate=@ToDate,ToTime=@ToTime,FromDate=BookingDate,FromTime=BookingTime";
            }

            DateTime dtime = DateTime.Now;
            string time = dtime.ToString("HH:mm");
            sql = string.Format(@"update CTM_JobDet2 set Statuscode=@Statuscode{0} where Id=@Id", sql1);
            list = new List<ConnectSql_mb.cmdParameters>();
            cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Statuscode", Statuscode, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@FromDate", dtime, SqlDbType.DateTime);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@FromTime", time, SqlDbType.NVarChar, 10);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ToDate", dtime, SqlDbType.DateTime);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ToTime", time, SqlDbType.NVarChar, 10);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ByUser", ByUser, SqlDbType.NVarChar, 20);
            list.Add(cpar);
            if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
            {
                status = "1";
                //EGL_JobTrip_AfterEndTrip(SafeValue.SafeString(job["Id"], "0"));


                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"], 0));
                //lg.Remark = "Trip change status:" + Statuscode;
                lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.Trip, 4, ":" + Statuscode);
                lg.log();
            }
            else
            {
                status = "0";
            }
        }
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void Attachment_Add(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        string sql = string.Format(@"insert into CTM_Attachment (JobType,RefNo,ContainerNo,TripId,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values 
(@JobType,@RefNo,@ContainerNo,@TripId,@FileType,@FileName,@FilePath,@CreateBy,Getdate(),@FileNote)
select @@Identity");

        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
        if (fileStart == null)
        {
            fileStart = "";
        }
        string fileEnd = job["FilePath"].ToString();
        fileEnd = fileEnd.Substring(0, fileEnd.LastIndexOf('/')) + "/500/" + fileEnd.Substring(fileEnd.LastIndexOf('/') + 1);
        fileEnd = fileStart + fileEnd;

        cpar = new ConnectSql_mb.cmdParameters("@JobType", "CTM", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@RefNo", job["JobNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", job["ContainerNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@TripId", job["TripId"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileType", job["FileType"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileName", job["FileName"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FilePath", fileEnd, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["CreateBy"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileNote", job["FileNote"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteScalar(sql, list);
        if (result.status)
        {
            status = "1";

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(result.context, 0));
            //lg.Remark = "Attachment upload file:" + job["FileName"];
            lg.setActionLevel(SafeValue.SafeInt(result.context, 0), CtmJobEventLogRemark.Level.Attachment, 1, ":" + job["FileType"] + "[" + job["FileName"] + "]");
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void Crane_JobEdit_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        string ByUser = job["ByUser"].ToString();
        decimal OtHourType = SafeValue.SafeDecimal(job["OverTime"]);
        string sql = string.Format(@"update CTM_JobDet2 set TowheadCode=@TowheadCode,Remark1=@Remark1,ToCode=@ToCode,DriverCode=@DriverCode,UpdateUser=@ByUser,UpdateTime=GetDate(),OtHour=@OtHour
where Id=@Id");

        #region params
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Remark1", job["Remark1"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@TowheadCode", job["TowheadCode"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["DriverCode"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ToCode", job["ToCode"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ByUser", ByUser, SqlDbType.NVarChar, 20);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@OtHour", SafeValue.SafeDecimal(job["OtHour"]), SqlDbType.Decimal);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@OtHourType", OtHourType, SqlDbType.Decimal);
        list.Add(cpar);
        #endregion

        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {
            status = "1";

            Dictionary<string, decimal> d = new Dictionary<string, decimal>();
            //d.Add("Trip", SafeValue.SafeDecimal(job["Trip"], 0));
            d.Add("OverTime", SafeValue.SafeDecimal(job["OverTime"], 0));
            //d.Add("Standby", SafeValue.SafeDecimal(job["Standby"], 0));
            //d.Add("PSA", SafeValue.SafeDecimal(job["PSA"], 0));

            //d.Add("OverTime", (OtHourType.Length > 0 ? 1 : 0));
            C2.CtmJobDet2.Incentive_Save(SafeValue.SafeInt(job["Id"], 0), d);
            d = new Dictionary<string, decimal>();
            d.Add("EXPENSE", SafeValue.SafeDecimal(job["EXPENSE"], 0));
            //d.Add("DHC", SafeValue.SafeDecimal(job["DHC"], 0));
            //d.Add("WEIGHING", SafeValue.SafeDecimal(job["WEIGHING"], 0));
            //d.Add("WASHING", SafeValue.SafeDecimal(job["WASHING"], 0));
            //d.Add("REPAIR", SafeValue.SafeDecimal(job["REPAIR"], 0));
            //d.Add("DETENTION", SafeValue.SafeDecimal(job["DETENTION"], 0));
            //d.Add("DEMURRAGE", SafeValue.SafeDecimal(job["DEMURRAGE"], 0));
            //d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(job["LIFT_ON_OFF"], 0));
            //d.Add("C_SHIPMENT", SafeValue.SafeDecimal(job["C_SHIPMENT"], 0));
            //d.Add("EMF", SafeValue.SafeDecimal(job["EMF"], 0));
            //d.Add("OTHER", SafeValue.SafeDecimal(job["OTHER"], 0));
            C2.CtmJobDet2.Claims_Save(SafeValue.SafeInt(job["Id"], 0), d);


            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"], 0));
            //lg.Remark = "Trip update";
            lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.Trip, 3);
            lg.log();

        }
        Common.WriteJsonP(status, context);
    }




    [WebMethod]
    public void Crane_JobView_GetPODData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.DriverCode,'' as ContainerNo,BillingRemark,DeliveryRemark,Satisfaction,
convert(nvarchar(10),FromDate,120) as FromDate,FromTime,convert(nvarchar(10),ToDate,120) as ToDate,ToTime,det2.PodType,isnull(det2.OtHour,0) OtHour,
isnull(TotalHour,0) as epodTrip,epodSignedBy,epodCB1,epodCB2,epodCB3,epodCB4,epodCB5,epodCB6,ManPowerNo,ExcludeLunch,det2.DeliveryResult,det2.epodHardCopy 
from CTM_JobDet2 as det2
where det2.Id=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", job["No"], SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = "{}";
        string attachment = "[]";
        if (dt.Rows.Count > 0)
        {
            mast = Common.DataRowToJson(dt);

            sql = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where TripId=@tripId and FileType='Signature'");
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
            attachment = Common.DataTableToJson(dt);

            //============== double mounting trip

        }

        string context = string.Format(@"{0}mast:{2},attachment:{3}{1}", "{", "}", mast, attachment);


        bool status = true;
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void Crane_JobView_PODSave(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int tripId = SafeValue.SafeInt(job["Id"], 0);
        string BillingRemark = job["BillingRemark"].ToString();
        string DeliveryRemark = job["DeliveryRemark"].ToString();
        string Satisfaction = job["Satisfaction"].ToString();
        string FromDate = job["FromDate"].ToString();
        string FromTime = job["FromTime"].ToString();
        string ToDate = job["ToDate"].ToString();
        string ToTime = job["ToTime"].ToString();
        decimal OtHour = SafeValue.SafeDecimal(job["OtHour"], 0);
        int epodTrip = SafeValue.SafeInt(job["epodTrip"], 0);
        string epodCB1 = SafeValue.SafeString(job["epodCB1"],"No");
        string epodCB2 = SafeValue.SafeString(job["epodCB2"],"No");
        string epodCB3 = SafeValue.SafeString(job["epodCB3"],"No");
        string epodCB4 = SafeValue.SafeString(job["epodCB4"],"No");
        string epodCB5 = SafeValue.SafeString(job["epodCB5"],"No");
        string epodCB6 = SafeValue.SafeString(job["epodCB6"],"No");
        int ManPowerNo = SafeValue.SafeInt(job["ManPowerNo"], 0);
        string ExcludeLunch = SafeValue.SafeString(job["ExcludeLunch"]);

        string epodSignedBy = SafeValue.SafeString(job["epodSignedBy"]);
        string DeliveryResult = SafeValue.SafeString(job["DeliveryResult"]);
        string epodHardCopy = SafeValue.SafeString(job["epodHardCopy"]);
        string context = Common.StringToJson("");

        string sql = string.Format(@"update CTM_JobDet2 set BillingRemark=@BillingRemark,DeliveryRemark=@DeliveryRemark,Satisfaction=@Satisfaction,PodType='C',
FromDate=@FromDate,FromTime=@FromTime,ToDate=@ToDate,ToTime=@ToTime,OtHour=@OtHour,TotalHour=@epodTrip,epodSignedBy=@epodSignedBy,
epodCB1=@epodCB1,epodCB2=@epodCB2,epodCB3=@epodCB3,epodCB4=@epodCB4,epodCB5=@epodCB5,epodCB6=@epodCB6,ManPowerNo=@ManPowerNo,ExcludeLunch=@ExcludeLunch,DeliveryResult=@DeliveryResult,epodHardCopy=@epodHardCopy 
where Id=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@BillingRemark", BillingRemark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@DeliveryRemark", DeliveryRemark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Satisfaction", Satisfaction, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", FromDate, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@FromTime", FromTime, SqlDbType.NVarChar, 5));
        list.Add(new ConnectSql_mb.cmdParameters("@ToDate", ToDate, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ToTime", ToTime, SqlDbType.NVarChar, 5));
        list.Add(new ConnectSql_mb.cmdParameters("@OtHour", OtHour, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@epodTrip", epodTrip, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@epodSignedBy", epodSignedBy, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@epodCB1", epodCB1, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@epodCB2", epodCB2, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@epodCB3", epodCB3, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@epodCB4", epodCB4, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@epodCB5", epodCB5, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@epodCB6", epodCB6, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@ManPowerNo", ManPowerNo, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@ExcludeLunch", ExcludeLunch, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@DeliveryResult", DeliveryResult, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@epodHardCopy", epodHardCopy, SqlDbType.NVarChar, 10));
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (result.status)
        {
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isTRIP(tripId);
            //lg.Remark = "Submit e-POD";
            lg.setActionLevel(tripId, CtmJobEventLogRemark.Level.Trip, 5);
            lg.log();

            if (!ePOD_SendEmail(tripId))
            {
                context = Common.StringToJson("Submit successful, but send false");
            }
        }
        Common.WriteJsonP(result.status, context);
    }


    [WebMethod]
    public void Attachment_Delete(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string status = "0";
        string context = Common.StringToJson("");
        int attachmentId = SafeValue.SafeInt(job["Id"], 0);
        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Platform_isMobile();
        lg.Controller = SafeValue.SafeString(job["user"]);
        lg.Lat = SafeValue.SafeString(job["Lat"]);
        lg.Lng = SafeValue.SafeString(job["Lng"]);
        //lg.fixActionInfo_ByAttachmentId(attachmentId);
        //lg.Remark = "Attachment delete file";
        lg.setActionLevel(attachmentId, CtmJobEventLogRemark.Level.Attachment, 2);

        string sql = string.Format(@"delete from CTM_Attachment where Id=@attachmentId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@attachmentId", attachmentId, SqlDbType.Int));

        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (result.status)
        {
            status = "1";

            //===========log
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void Attachment_AddMutiple(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        JArray jar = (JArray)JsonConvert.DeserializeObject(job["list"].ToString());

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");

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




    [WebMethod]
    public void ePOD_reSend(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int tripId = SafeValue.SafeInt(job["Id"], 0);

        string context = Common.StringToJson("");
        if (!ePOD_SendEmail(tripId))
        {
            context = Common.StringToJson("Send false");
        }
        Common.WriteJsonP(true, context);
    }
    private bool ePOD_SendEmail(int tripId)
    {
        bool res = false;
        string sql = string.Format(@"select job.JobNo,job.EmailAddress,job.JobType,det2.epodHardCopy  
from ctm_jobdet2 as det2
left outer join ctm_job as job on job.JobNo=det2.JobNo
where det2.Id=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count > 0)
        {
            string JobNo = dt.Rows[0]["JobNo"].ToString();
            string email = dt.Rows[0]["EmailAddress"].ToString();
            string JobType = dt.Rows[0]["JobType"].ToString();
            string hardCopy = dt.Rows[0]["epodHardCopy"].ToString();

            if (email != "")
            {
                try
                {
                    //bool action = false;
                    string path1 = string.Format("~/files/pdf/");
                    string path2 = path1.Replace(' ', '_').Replace('\'', '_');
                    string pathx = path2.Substring(1);
                    //string path3 = MapPath(path2);
                    //string path3 = Path.Combine(@"C:\boox\boox_ell", path2);
                    string path3 = HttpContext.Current.Server.MapPath(path2);
                    if (!Directory.Exists(path3))
                        Directory.CreateDirectory(path3);
                    string p = string.Format(@"~\files\pdf\DeliveryOrder_Crane_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                    string e_file = HttpContext.Current.Server.MapPath(p);
                    //if (!File.Exists(e_file))
                    //{
                    //    File.Create(e_file);
                    //}

                    MemoryStream ms = new MemoryStream();
                    XtraReport rpt = new XtraReport();
                    if (hardCopy == "HardCopy")
                    {
                        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DO_HardCopy.repx"));
                    }
                    else
                    {
                        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DeliveryOrder_CRA.repx"));
                    }
                    rpt.DataSource = DocPrint.PrintJobSheet(JobNo, JobType, tripId + "");


                    //============ bind signatrue
                    QR q = new QR();
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + JobNo + "'");
                    C2.CtmJob job = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
                    string text = string.Format(@"JobNo:" + JobNo + ",JobDate:" + job.JobDate.ToString("dd/MM/yyyy"));
                    Bitmap bt = q.Create_QR(text);
                    string path = HttpContext.Current.Server.MapPath("~/files/barcode/");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string fileName = JobNo + ".png";
                    string filePath = path + fileName;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    bt.Save(Server.MapPath("~/files/barcode/") + fileName);

                    string Signature_Consignee = "";
                    string Signature_Driver = "";
                    string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@RefNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@sType", "Consignee", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
                    dt = ConnectSql_mb.GetDataTable(sql_signature, list);
                    if (dt.Rows.Count > 0)
                    {
                        Signature_Consignee = dt.Rows[0]["FilePath"].ToString().Replace(@"/500", "");
                    }
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@RefNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@sType", "Driver", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
                    dt = ConnectSql_mb.GetDataTable(sql_signature, list);
                    if (dt.Rows.Count > 0)
                    {
                        Signature_Driver = dt.Rows[0]["FilePath"].ToString();
                    }
                    DevExpress.XtraReports.UI.XRPictureBox qr_code = rpt.Report.FindControl("barcode", true) as DevExpress.XtraReports.UI.XRPictureBox;
                    if (qr_code != null)
                    {
                        qr_code.ImageUrl = "/files/barcode/" + fileName;
                    }
                    DevExpress.XtraReports.UI.XRPictureBox signature = rpt.Report.FindControl("signature", true) as DevExpress.XtraReports.UI.XRPictureBox;
                    if (signature != null)
                    {
                        signature.ImageUrl = Signature_Consignee;
                    }
                    signature = rpt.Report.FindControl("signature1", true) as DevExpress.XtraReports.UI.XRPictureBox;
                    if (signature != null)
                    {
                        signature.ImageUrl = Signature_Driver;
                    }



                    System.IO.MemoryStream str = new MemoryStream();
                    rpt.CreateDocument();
                    rpt.ExportToPdf(e_file);


                    Helper.Email.SendEmail(email, "admin@cargoerp.com", "admin@cargoerp.com", "Delivery Order " + JobNo, "", p);

                    res = true;
                }
                catch { }

                //            string company = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                //            string address1 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"];
                //            string address2 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"];
                //            string address3 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
                //            sql = string.Format(@"select Email1,Email2,Name from xxparty where PartyId='{0}'",JobNo);
                //            DataTable tab = ConnectSql.GetTab(sql);
                //            string add = address1 + " " + address2 + " " + address3;
                //            string title = "Haulier Advice";
                //            if (tab.Rows.Count > 0)
                //            {
                //                string email1 = SafeValue.SafeString(tab.Rows[0]["Email1"]);
                //                string email2 = SafeValue.SafeString(tab.Rows[0]["Email2"]);
                //                string name = SafeValue.SafeString(tab.Rows[0]["Name"]);
                //                string mes = string.Format(@"<b>{0}</b><br><br>
                //{1}<br><br>
                //<b>Dear Customer, <br><br>Kindly review attached document for  HAULIER ADVICE.</b>
                //<br><br>
                //<b>This is a computer generated email, please DO NOT reply.
                //<br><br>
                //Pls email to : xglogistic@ugroup.com.sg for any questions.
                //</b><br><br>
                //***IMPORTANT NOTICE***<br><br>
                //
                //1.     Pls wait for our delivery department to call you for delivery arrangement<br><br>
                //
                //2.     Delivery arrangement will be made within 3 working days from the date of unstuffing of container.<br><br>
                //<br><br>", company, add);
                //                if (email1.Length > 0)
                //                {
                //                    Helper.Email.SendEmail(email1, "BRYAN.HU@ugroup.com.sg,xglogistic@ugroup.com.sg", "billing@ugroup.com.sg", title, mes, p);
                //                    //Helper.Email.SendEmail("huang@cargoerp.com", "99915945@qq.com,ymyg1985318@163.com", "", title, mes, p);
                //                    action = true;
                //                }
                //                if (email2.Length > 0)
                //                {
                //                    if (action)
                //                        Helper.Email.SendEmail(email2, "", "", title, mes, p);
                //                    else
                //                        Helper.Email.SendEmail(email2, "BRYAN.HU@ugroup.com.sg,xglogistic@ugroup.com.sg", "billing@ugroup.com.sg", title, mes, p);
                //                }
                //                //"BRYAN.HU@ugroup.com.sg", "billing@ugroup.com.sg"
                //                res = true;
                //            }


            }
        }
        return res;

    }
}
