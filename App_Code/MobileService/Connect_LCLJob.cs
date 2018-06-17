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
/// Connect_LCLJob 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Connect_LCLJob : System.Web.Services.WebService
{

    public Connect_LCLJob()
    {

        //如果使用设计的组件，请取消注释以下行 
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
        if (job["role"].ToString().ToLower().Equals("driver"))
        {
            sql_where = " and (det2.DriverCode=@DriverCode or det2.DriverCode2=@DriverCode or det2.DriverCode11=@DriverCode or det2.DriverCode12=@DriverCode)";
        }

        string sql = string.Format(@"select det2.Id,det2.JobNo,DriverCode,DriverCode2,TowheadCode,ChessisCode,det2.Statuscode,det2.Remark, 
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det2.DoubleMounting,
case det2.Statuscode when 'S' then 1 when 'C' then 3 when 'X' then 4 when 'A' then 5 else 2 end as OrderIndex,
job.IsTrucking,job.IsWarehouse,job.IsLocal,job.IsAdhoc,job.Vessel,job.Voyage,det2.ServiceType
From CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where (DATEDIFF(day,FromDate,@date)=0 ) and det2.JobType in ('WGR','WDO','TPT') {0}
order by OrderIndex,TripIndex1,ToDate", sql_where);
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
    public void LCL_JobView_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.DriverCode,det2.TowheadCode,det2.ContainerNo as ChessisCode,job.Vessel,job.Voyage,
job.CarrierBkgNo AS ClientRefNo,job.CarrierId,p.Name as CarrierName,det2.Remark,det2.Remark1,det2.Statuscode,
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,det2.Carpark,det2.ToParkingLot,det2.ParkingZone,det2.TripCode,det2.DoubleMounting,det2.PodType,
det2.Qty,det2.PackageType,det2.Weight,det2.Volume,job.JobType,det2.CargoVerify,det2.DeliveryResult,det2.ManualDo,
det2.DriverCode2,det2.DriverCode11,det2.DriverCode12,
job.EtaDate,job.EtaTime,job.EtdDate,job.EtdTime,det2.ServiceType
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
left outer join XXParty as p on job.ClientId=p.PartyId
where det2.Id={0}", job["No"]);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string mast = "{}";
        string attachment = "[]";
        string incentive = "[]";
        string claims = "[]";
        string cargoList = "[]";
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

            sql = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where TripId={0} and FileType='Image'", job["No"]);
            dt = ConnectSql_mb.GetDataTable(sql);
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
            //================== cargo list
            sql = string.Format(@"select det.Id as Oid,det.HblNo as HblN,det.HblNo as DnNo,det.JobType,det.Commodity,det.LandStatus,det.BookingNo
,det.PackTypeOrig,det.WeightOrig,det.VolumeOrig,det.QtyOrig,det.BookingNo,det.Marking2 as Desc1,det.CargoStatus,
det.Qty,det.UomCode,det.Weight as BkgWeight,det.Volume as BkgVolume,det.BkgSkuCode,det.BkgSkuQty,det.BkgSkuUnit,
det.SkuCode,det.PackQty,det.PackUom,det.BookingItem,det.ActualItem,
(select top 1 PermitNo from ref_permit where HblNo=det.HblNo and JobNo=det.JobNo) as PermitNo
from job_house as det 
where det.TripId=@TripNo 
order by det.HblNo");
            dt = ConnectSql_mb.GetDataTable(sql, list);
            cargoList = Common.DataTableToJson(dt);
        }

        string context = string.Format(@"{0}mast:{2},attachment:{3},incentive:{4},claims:{5},cargoList:{6}{1}", "{", "}", mast, attachment, incentive, claims, cargoList);


        bool status = true;
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void LCL_ChangeStatus(string info)
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
            status = "0";
            DateTime dtime = SafeValue_mb.DateTime_ClearTime(DateTime.Now);
            string time = DateTime.Now.ToString("HH:mm");
            int det2Id = SafeValue.SafeInt(job["Id"], 0);
            string user = SafeValue.SafeString(job["user"]);

            C2.CtmJobDet2Biz det2Bz = new C2.CtmJobDet2Biz(det2Id);
            C2.CtmJobDet2 det2 = det2Bz.getData();
            if (det2 != null)
            {
                det2.UpdateUser = user;
                det2.UpdateTime = DateTime.Now;
                det2.Statuscode = Statuscode;
                if (Statuscode.Equals("S"))
                {
                    det2.FromDate = dtime;
                    det2.FromTime = time;
                }
                if (Statuscode.Equals("C"))
                {
                    det2.ToDate = dtime;
                    det2.ToTime = time;
                }
                C2.BizResult result = det2Bz.update(user);
                if (result.status)
                {
                    status = "1";
                    //===========log
                    C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                    lg.Platform_isMobile();
                    lg.Controller = SafeValue.SafeString(job["user"]);
                    lg.Lat = SafeValue.SafeString(job["Lat"]);
                    lg.Lng = SafeValue.SafeString(job["Lng"]);
                    //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"], 0));
                    //lg.Remark = "Trip change status:" + Statuscode;
                    lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.Trip, 4, ":" + status);
                    lg.log();
                }
            }
            

            //if (Statuscode.Equals("S"))
            //{
            //    sql1 = ",FromDate=@FromDate,FromTime=@FromTime";
            //}
            //if (Statuscode.Equals("C"))
            //{
            //    sql1 = ",ToDate=@ToDate,ToTime=@ToTime";
            //}
            //sql = string.Format(@"update CTM_JobDet2 set Statuscode=@Statuscode,UpdateUser=@ByUser,UpdateTime=GetDate(){0} where Id=@Id", sql1);
            //list = new List<ConnectSql_mb.cmdParameters>();
            //cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@Statuscode", Statuscode, SqlDbType.NVarChar, 100);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@FromDate", dtime, SqlDbType.DateTime);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@FromTime", time, SqlDbType.NVarChar, 10);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@ToDate", dtime, SqlDbType.DateTime);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@ToTime", time, SqlDbType.NVarChar, 10);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@ByUser", ByUser, SqlDbType.NVarChar, 20);
            //list.Add(cpar);
            //if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
            //{
            //    status = "1";
            //    //EGL_JobTrip_AfterEndTrip(SafeValue.SafeString(job["Id"], "0"));
            //    C2.CtmJobDet2.tripStatusChanged(SafeValue.SafeInt(job["Id"].ToString(), 0));


            //    //===========log
            //    C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            //    lg.Platform_isMobile();
            //    lg.Controller = SafeValue.SafeString(job["user"]);
            //    lg.Lat = SafeValue.SafeString(job["Lat"]);
            //    lg.Lng = SafeValue.SafeString(job["Lng"]);
            //    lg.setActionLevel(SafeValue.SafeInt(job["Id"].ToString(), 0), CtmJobEventLogRemark.Level.Trip, 4, ":" + Statuscode);
            //    //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"].ToString(), 0));
            //    ////lg.Remark = "Trip change status:" + Statuscode;
            //    //lg.setRemark(CtmJobEventLogRemark.Level.Trip,4,":" + Statuscode);
            //    lg.log();
            //}
            //else
            //{
            //    status = "0";
            //}
        }
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void LCL_VerifyCargo(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string ByUser = job["ByUser"].ToString();
        int tripId = SafeValue.SafeInt(job["Id"], 0);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        //ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        if (status.Equals("1"))
        {
            status = "0";
            DateTime dtime = SafeValue_mb.DateTime_ClearTime(DateTime.Now);
            string time = DateTime.Now.ToString("HH:mm");
            C2.CtmJobDet2Biz det2Bz = new C2.CtmJobDet2Biz(tripId);
            C2.CtmJobDet2 det2 = det2Bz.getData();
            if (det2 != null)
            {
                det2.UpdateUser = ByUser;
                det2.UpdateTime = DateTime.Now;
                det2.CargoVerify = "Yes";
                C2.BizResult result = det2Bz.update(ByUser);
                if (result.status)
                {
                    status = "1";
                    //===========log
                    C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                    lg.Platform_isMobile();
                    lg.Controller = SafeValue.SafeString(job["user"]);
                    lg.Lat = SafeValue.SafeString(job["Lat"]);
                    lg.Lng = SafeValue.SafeString(job["Lng"]);
                    string sql = string.Format(@"select Id,BookingNo,QtyOrig,SkuCode from job_house where TripId=@Id");
                    list.Add(new ConnectSql_mb.cmdParameters("@Id", tripId, SqlDbType.Int));
                    DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                    lg.setActionLevel(SafeValue.SafeInt(job["Id"].ToString(), 0), CtmJobEventLogRemark.Level.Trip, 4, ":Verify Cargo Detail" + (dt.Rows.Count == 0 ? "[Empty]" : ""));
                    lg.log();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        lg.setActionLevel(SafeValue.SafeInt(dr["Id"], 0), CtmJobEventLogRemark.Level.Cargo, -1, "Driver Verify: " + dr["BookingNo"] + "/" + dr["QtyOrig"] + "/" + dr["SkuCode"]);
                        lg.log();
                    }
                }
            }

            //DateTime dtime = DateTime.Now;
            //string time = dtime.ToString("HH:mm");
            //string sql = string.Format(@"update CTM_JobDet2 set CargoVerify='Yes' where Id=@Id");
            //list = new List<ConnectSql_mb.cmdParameters>();
            //cpar = new ConnectSql_mb.cmdParameters("@Id", tripId, SqlDbType.Int);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@ByUser", ByUser, SqlDbType.NVarChar, 20);
            //list.Add(cpar);
            //if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
            //{
            //    status = "1";

            //    //===========log
            //    C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            //    lg.Platform_isMobile();
            //    lg.Controller = SafeValue.SafeString(job["user"]);
            //    lg.Lat = SafeValue.SafeString(job["Lat"]);
            //    lg.Lng = SafeValue.SafeString(job["Lng"]);
            //    sql = string.Format(@"select Id,BookingNo,QtyOrig,SkuCode from job_house where TripId=@Id");
            //    DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            //    lg.setActionLevel(SafeValue.SafeInt(job["Id"].ToString(), 0), CtmJobEventLogRemark.Level.Trip, 4, ":Verify Cargo Detail" + (dt.Rows.Count == 0 ? "[Empty]" : ""));
            //    lg.log();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow dr = dt.Rows[i];
            //        lg.setActionLevel(SafeValue.SafeInt(dr["Id"], 0), CtmJobEventLogRemark.Level.Cargo, -1, "Driver Verify: " + dr["BookingNo"] + "/" + dr["QtyOrig"] + "/" + dr["SkuCode"]);
            //        lg.log();
            //    }
            //}
            //else
            //{
            //    status = "0";
            //}
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

        string sql = string.Format(@"insert into CTM_Attachment (JobType,RefNo,ContainerNo,TripId,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values(@JobType,@RefNo,@ContainerNo,@TripId,@FileType,@FileName,@FilePath,@CreateBy,Getdate(),@FileNote)
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
            lg.setActionLevel(SafeValue.SafeInt(result.context, 0), CtmJobEventLogRemark.Level.Attachment, 1, ":" + job["FileType"] + "[" + job["FileName"] + "]");
            //lg.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(result.context, 0));
            ////lg.Remark = "Attachment upload file:" + job["FileName"];
            //lg.setRemark(CtmJobEventLogRemark.Level.Attachment, 1, " " + job["FileType"] + "[" + job["FileName"] + "]");
            lg.log();
        }

        Common.WriteJsonP(status, context);
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
        //lg.setRemark(CtmJobEventLogRemark.Level.Attachment, 2);
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
    public void LCL_JobEdit_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        
        string status = "0";
        string context = Common.StringToJson("");
        string ByUser = job["ByUser"].ToString();
        int det2Id = SafeValue.SafeInt(job["Id"], 0);
        string Remark1 = SafeValue.SafeString(job["Remark1"]);
        string TowheadCode = SafeValue.SafeString(job["TowheadCode"]);
        string ChessisCode = SafeValue.SafeString(job["ChessisCode"]);
        string ToCode = SafeValue.SafeString(job["ToCode"]);
        string ManualDo = SafeValue.SafeString(job["ManualDo"]);
        int Qty = SafeValue.SafeInt(job["Qty"], 0);
        string PackageType = SafeValue.SafeString(job["PackageType"]);
        decimal Weight = SafeValue.SafeDecimal(job["Weight"], 0);
        decimal Volume = SafeValue.SafeDecimal(job["Volume"], 0);

        C2.CtmJobDet2Biz det2Bz = new C2.CtmJobDet2Biz(det2Id);
        C2.CtmJobDet2 det2 = det2Bz.getData();
        if (det2 != null)
        {
            det2.TowheadCode = TowheadCode;
            det2.Remark1 = Remark1;
            det2.ToCode = ToCode;
            det2.UpdateUser = ByUser;
            det2.UpdateTime = DateTime.Now;
            det2.Qty = Qty;
            det2.PackageType = PackageType;
            det2.Weight = Weight;
            det2.Volume = Volume;
            det2.ContainerNo = ChessisCode;
            det2.ManualDo = ManualDo;
            
            C2.BizResult result = det2Bz.update(ByUser);
            if (result.status)
            {
                status = "1";

                Dictionary<string, decimal> d = new Dictionary<string, decimal>();
                d.Add("Trip", SafeValue.SafeDecimal(job["Trip"], 0));
                d.Add("OverTime", SafeValue.SafeDecimal(job["OverTime"], 0));
                d.Add("Standby", SafeValue.SafeDecimal(job["Standby"], 0));
                d.Add("PSA", SafeValue.SafeDecimal(job["PSA"], 0));
                C2.CtmJobDet2.Incentive_Save(SafeValue.SafeInt(job["Id"], 0), d);
                //d = new Dictionary<string, decimal>();
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
                //C2.CtmJobDet2.Claims_Save(SafeValue.SafeInt(job["Id"], 0), d);

                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"], 0));
                //lg.setRemark(CtmJobEventLogRemark.Level.Trip,3);
                lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.Trip, 3);
                lg.log();
            }
        }


//        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
//        ConnectSql_mb.cmdParameters cpar = null;
//        string sql = string.Format(@"update CTM_JobDet2 set TowheadCode=@TowheadCode,Remark1=@Remark1,ToCode=@ToCode,UpdateUser=@ByUser,UpdateTime=GetDate(),
//Qty=@Qty,PackageType=@PackageType,Weight=@Weight,Volume=@Volume,ContainerNo=@ChessisCode,ManualDo=@ManualDo 
//where Id=@Id");
//        #region params
//        cpar = new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@Remark1", job["Remark1"], SqlDbType.NVarChar, 300);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@TowheadCode", job["TowheadCode"], SqlDbType.NVarChar, 100);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ChessisCode", job["ChessisCode"], SqlDbType.NVarChar, 100);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ToCode", job["ToCode"], SqlDbType.NVarChar, 300);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ByUser", ByUser, SqlDbType.NVarChar, 20);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ManualDo", job["ManualDo"], SqlDbType.NVarChar, 100);
//        list.Add(cpar);

//        list.Add(new ConnectSql_mb.cmdParameters("@Qty", SafeValue.SafeInt(job["Qty"], 0), SqlDbType.Int));
//        list.Add(new ConnectSql_mb.cmdParameters("@PackageType", job["PackageType"], SqlDbType.NVarChar, 100));
//        list.Add(new ConnectSql_mb.cmdParameters("@Weight", SafeValue.SafeDecimal(job["Weight"], 0), SqlDbType.Decimal));
//        list.Add(new ConnectSql_mb.cmdParameters("@Volume", SafeValue.SafeDecimal(job["Volume"], 0), SqlDbType.Decimal));
//        #endregion
//        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);
        
        Common.WriteJsonP(status, context);

    }




    [WebMethod]
    public void LCL_List_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;

        string fromDate = job["from"].ToString();
        string toDate = job["to"].ToString();

        string sql = string.Format(@"select Id,JobNo,JobDate,JobType,ClientId,convert(nvarchar(10),JobDate,120) as showDate,
isnull((select top 1 det2.Id from ctm_jobdet2 as det2 where det2.JobNo=job.JobNo),0) as tripId 
from ctm_job as job
where job.JobType in ('WGR','WDO','TPT') and datediff(d,job.JobDate,@fromDate)<=0 and datediff(d,job.JobDate,@toDate)>=0");
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@fromDate", fromDate, SqlDbType.NVarChar, 8);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@toDate", toDate, SqlDbType.NVarChar, 8);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }


    [WebMethod]
    public void LCL_AddNew(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string JobType = job["JobType"].ToString();
        string ClientId = job["ClientId"].ToString();
        string PickupFrom = job["PickupFrom"].ToString();
        string DeliveryTo = job["DeliveryTo"].ToString();
        string Remark = job["Remark"].ToString();
        Decimal Qty = SafeValue.SafeDecimal(job["Qty"]);
        string PackageType = job["PackageType"].ToString();
        Decimal Weight = SafeValue.SafeDecimal(job["Weight"]);
        Decimal Volume = SafeValue.SafeDecimal(job["Volume"]);
        string user = job["user"].ToString();
        DateTime dtime = DateTime.Now;
        string JobNo = C2Setup.GetNextNo("", "CTM_Job_" + JobType, dtime);
        string wh = System.Configuration.ConfigurationManager.AppSettings["Warehosue"];
        string billingType = "None";
        if (JobType == "TPT")
        {
            billingType = "Job";
        }

        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,BillingType) 
values (@JobNo,@JobDate,@JobDate,@JobDate,@JobDate,'USE',@user,getdate(),@user,getdate(),'0000','0000',@JobType,@ClientId,'',@PickupFrom,@DeliveryTo,@Remark,'','Booked',@JobNo,'Pending',getdate(),@WareHouseCode,@BillingType)");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 20));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", ClientId, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@PickupFrom", PickupFrom, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@DeliveryTo", DeliveryTo, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@JobDate", dtime, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@WareHouseCode", wh, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@BillingType", billingType, SqlDbType.NVarChar, 100));

        bool status = false;
        string context = Common.StringToJson("Save Error");
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        C2Setup.SetNextNo("", "CTM_Job_" + JobType, JobNo, dtime);
        if (res.status)
        {
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            lg.fixActionInfo_ByJobNo(JobNo);
            //lg.Remark = "Job Add New";
            lg.setRemark(CtmJobEventLogRemark.Level.Job, 1);
            lg.log();

            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot,Qty,PackageType,Weight,Volume) 
values (@JobNo,@ChessisCode,'','','',@FromCode,@FromDate,@FromTime,@ToCode,@FromDate,@FromTime,@Det1Id,'P',
'','N','','',@TripCode,'Normal','N',@FromParkingLot,@Qty,@PackageType,@Weight,@Volume)
select @@Identity");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", "", SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", "", SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@FromCode", PickupFrom, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@FromDate", dtime, SqlDbType.DateTime));
            list.Add(new ConnectSql_mb.cmdParameters("@FromTime", dtime.ToString("HH:mm"), SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@ToCode", DeliveryTo, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", 0, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@TripCode", "LOC", SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@FromParkingLot", "", SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@Qty", Qty, SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@PackageType", PackageType, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@Weight", Weight, SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@Volume", Volume, SqlDbType.Decimal));
            res = ConnectSql_mb.ExecuteScalar(sql, list);
            if (res.status)
            {
                status = true;

                lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                //lg.ActionLevel_isTRIP(SafeValue.SafeInt(res.context, 0));
                ////lg.Remark = "Trip Add New";
                //lg.setRemark(CtmJobEventLogRemark.Level.Trip, 1);
                lg.setActionLevel(SafeValue.SafeInt(res.context, 0), CtmJobEventLogRemark.Level.Trip, 1);
                lg.log();
            }
            else
            {
                context = Common.StringToJson("Save Trip Error");
            }
        }
        Common.WriteJsonP(status, context);
    }

    #region ePOD

    [WebMethod]
    public void LCL_JobView_GetPODData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.DriverCode,'' as ContainerNo,BillingRemark,DeliveryRemark,Satisfaction,
convert(nvarchar(10),FromDate,120) as FromDate,FromTime,convert(nvarchar(10),ToDate,120) as ToDate,ToTime,det2.PodType,isnull(det2.OtHour,0) OtHour,
isnull(epodTrip,0) as epodTrip,epodSignedBy,det2.DeliveryResult,det2.epodHardCopy 
from CTM_JobDet2 as det2
where det2.Id=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", job["No"], SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = "{}";
        string attachment = "[]";
        string cargoList = "[]";
        if (dt.Rows.Count > 0)
        {
            mast = Common.DataRowToJson(dt);

            //========== attachment
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

            //============== cargo List
            sql = string.Format(@"select Id,TripIndex,BookingNo,Marking2 as Des,QtyOrig,UomCode,ActualItem,PackQty,PackUom,SkuCode
from job_house
where TripId=@tripId ");
            dt = ConnectSql_mb.GetDataTable(sql, list);
            cargoList = Common.DataTableToJson(dt);
        }

        string context = string.Format(@"{0}mast:{2},attachment:{3},cargoList:{4}{1}", "{", "}", mast, attachment, cargoList);


        bool status = true;
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void LCL_JobView_PODSave(string info)
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
        string epodSignedBy = SafeValue.SafeString(job["epodSignedBy"]);
        string DeliveryResult = SafeValue.SafeString(job["DeliveryResult"]);
        string epodHardCopy = SafeValue.SafeString(job["epodHardCopy"]);
        string context = Common.StringToJson("");

        string sql = string.Format(@"update CTM_JobDet2 set BillingRemark=@BillingRemark,DeliveryRemark=@DeliveryRemark,Satisfaction=@Satisfaction,PodType='C',
FromDate=@FromDate,FromTime=@FromTime,ToDate=@ToDate,ToTime=@ToTime,OtHour=@OtHour,epodTrip=@epodTrip,epodSignedBy=@epodSignedBy,DeliveryResult=@DeliveryResult,epodHardCopy=@epodHardCopy  
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
            ////lg.Remark = "Submit e-POD";
            //lg.setRemark(CtmJobEventLogRemark.Level.Trip, 5);
            lg.setActionLevel(tripId, CtmJobEventLogRemark.Level.Trip, 5);
            lg.log();

            string email = "";
            sql = string.Format(@"select job.JobNo,job.EmailAddress 
from ctm_jobdet2 as det2
left outer join ctm_job as job on job.JobNo=det2.JobNo
where det2.Id=@tripId");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                email = dt.Rows[0]["EmailAddress"].ToString();
            }

            if (!ePOD_SendEmail(tripId))
            {
                context = Common.StringToJson("Submit successful, but send false");
                lg.setActionLevel(tripId, CtmJobEventLogRemark.Level.Trip, 6, "[" + email + "] false");
                lg.log();
            }
            else
            {
                lg.setActionLevel(tripId, CtmJobEventLogRemark.Level.Trip, 6, "[" + email + "] successful");
                lg.log();
            }
        }
        Common.WriteJsonP(result.status, context);
    }
    [WebMethod]
    public void ePOD_reSend(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Platform_isMobile();
        lg.Controller = SafeValue.SafeString(job["user"]);
        lg.Lat = SafeValue.SafeString(job["Lat"]);
        lg.Lng = SafeValue.SafeString(job["Lng"]);
        int tripId = SafeValue.SafeInt(job["Id"], 0);
        string email = "";
        string context = Common.StringToJson("");


        string sql = string.Format(@"select job.JobNo,job.EmailAddress 
from ctm_jobdet2 as det2
left outer join ctm_job as job on job.JobNo=det2.JobNo
where det2.Id=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count > 0)
        {
            email = dt.Rows[0]["EmailAddress"].ToString();
        }

        if (!ePOD_SendEmail(tripId))
        {
            lg.setActionLevel(tripId, CtmJobEventLogRemark.Level.Trip, 6, "[" + email + "] false");
            context = Common.StringToJson("Send false");
        }
        else
        {
            lg.setActionLevel(tripId, CtmJobEventLogRemark.Level.Trip, 6, "[" + email + "] successful");
        }
        lg.log();
        Common.WriteJsonP(true, context);
    }



    private bool ePOD_SendEmail(int tripId)
    {
        bool res = false;
        string sql = string.Format(@"select job.JobNo,job.EmailAddress,det2.epodHardCopy 
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
                    string p = string.Format(@"~\files\pdf\DeliveryOrder_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
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
                        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DO.repx"));
                    }
                    rpt.DataSource = DocPrint.PrintDeliveryOrder(JobNo, "");

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
                    string signature_time = "";
                    string signature_time1 = "";
                    string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote,CreateDateTime From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@RefNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@sType", "Consignee", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
                    dt = ConnectSql_mb.GetDataTable(sql_signature, list);
                    if (dt.Rows.Count > 0)
                    {
                        Signature_Consignee = dt.Rows[0]["FilePath"].ToString().Replace(@"/500","");
                        signature_time = dt.Rows[0]["CreateDateTime"].ToString();
                    }
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@RefNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@sType", "Driver", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
                    dt = ConnectSql_mb.GetDataTable(sql_signature, list);
                    if (dt.Rows.Count > 0)
                    {
                        Signature_Driver = dt.Rows[0]["FilePath"].ToString();
                        signature_time1 = dt.Rows[0]["CreateDateTime"].ToString();
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
                    DevExpress.XtraReports.UI.XRLabel time = rpt.Report.FindControl("lbl_time", true) as DevExpress.XtraReports.UI.XRLabel;
                    if (time != null)
                    {
                        time.Text = signature_time;
                    }
                    signature = rpt.Report.FindControl("signature1", true) as DevExpress.XtraReports.UI.XRPictureBox;
                    if (signature != null)
                    {
                        signature.ImageUrl = Signature_Driver;
                    }
                    DevExpress.XtraReports.UI.XRLabel time1 = rpt.Report.FindControl("lbl_time1", true) as DevExpress.XtraReports.UI.XRLabel;
                    if (time1 != null)
                    {
                        time1.Text = signature_time1;
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

    #endregion

    #region WGR/WDO

    [WebMethod]
    public void WGRWDO_Calendar_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string sql_where = "";
        //if (job["role"].ToString().ToLower().Equals("driver"))
        //{
        //    sql_where = " and det2.DriverCode=@DriverCode";
        //}

        //        string sql = string.Format(@"select det1.Id,det1.JobNo,det1.ScheduleStartDate,det1.ScheduleStartTime,det1.CompletionDate,det1.CompletionTime,job.PickupFrom,job.DeliveryTo,job.ClientId,job.ClientContact,det1.StatusCode,det1.CfsStatus,det1.ContainerNo,
        //case det1.CfsStatus when 'Started' then 1 when 'Scheduled' then 2 when 'Completed' then 3 else 0 end as OrderIndex,
        //job.IsTrucking,job.IsWarehouse,job.IsLocal,job.IsAdhoc 
        //From CTM_JobDet1 as det1
        //left outer join CTM_Job as job on det1.JobNo=job.JobNo
        //where job.JobType=@JobType and det1.CfsStatus in ('Scheduled','Started','Completed') and datediff(d,det1.ScheduleStartDate,@date)=0
        //order by OrderIndex,det1.ScheduleStartDate", sql_where);
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.WarehouseStartDate as ScheduleStartDate,'' as ScheduleStartTime,det2.WarehouseEndDate as CompletionDate,
'' as CompletionTime,job.PickupFrom,job.DeliveryTo,det2.RequestTrailerType as RequestChassisType,
job.ClientId,job.ClientContact,det2.StatusCode,det2.WarehouseStatus as CfsStatus,det2.ContainerNo,
case det2.WarehouseStatus when 'Started' then 1 when 'Scheduled' then 2 when 'Completed' then 3 else 0 end as OrderIndex,
job.IsTrucking,job.IsWarehouse,job.IsLocal,job.IsAdhoc,det2.Self_Ind,det2.TripIndex,det2.WarehouseRemark,det2.ChessisCode  
From CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.JobType=@JobType and det2.WarehouseStatus in ('Scheduled','Started','Completed') and datediff(d,det2.WarehouseScheduleDate,@date)=0
order by OrderIndex,det2.WarehouseStartDate");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", job["JobType"], SqlDbType.NVarChar, 10));
        cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void WGRWDO_View_GetDetail(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;

        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.WarehouseStartDate as ScheduleStartDate,'' as ScheduleStartTime,det2.WarehouseEndDate as CompletionDate,'' as CompletionTime,
job.PickupFrom,job.DeliveryTo,det2.TowheadCode,
job.ClientId,job.ClientContact,det2.StatusCode,det2.WarehouseStatus as CfsStatus,job.JobType,det2.ContainerNo,det2.ChessisCode,
det2.RequestTrailerType as ContainerType,(case when det2.TripCode='WGR' then det2.WarehouseRemark when det2.TripCode='WDO' then det2.WarehouseRemark else det2.Remark end) as Remark,det2.JobType as TripJobType,det2.Self_Ind,det2.DriverCode
From CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id=@Id");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);

        sql = string.Format(@"select att.Id as Oid,att.FileType as FileType,att.FileName as FileName,att.FilePath as FilePath,att.FileNote as FileNote,1 as ShowCust  
from CTM_Attachment as att 
where TripId=@Id");
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

        string context = string.Format(@"{0}mast:{2},attachment:{3}{1}", "{", "}", mast, attachment);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void WGRWDO_View_ChangeStatus(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        string sql0 = "";
        string CfsStatus = job["status"].ToString();

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["No"], SqlDbType.Int);
        list.Add(cpar);
        string sql_1 = string.Format(@"select Self_Ind from CTM_JobDet2 where Id=@Id");
        DataTable dt = ConnectSql_mb.GetDataTable(sql_1, list);
        if (dt.Rows.Count > 0)
        {
            string selfInd = SafeValue.SafeString(dt.Rows[0]["Self_Ind"]);

            if (CfsStatus == "Started")
            {
                //sql0 = ",ScheduleStartDate=getdate(),ScheduleStartTime=convert(nvarchar(5),getdate(),114)";
                sql0 = ",WarehouseStartDate=getdate()";
                if (selfInd == "Yes")
                {
                    sql0 += ",FromDate=getdate(),FromTime=convert(nvarchar(5),getdate(),114),Statuscode='S'";
                }
            }
            if (CfsStatus == "Completed")
            {
                //sql0 = ",CompletionDate=getdate(),CompletionTime=convert(nvarchar(5),getdate(),114)";
                sql0 = ",WarehouseEndDate=getdate()";
                if (selfInd == "Yes")
                {
                    sql0 += ",ToDate=getdate(),ToTime=convert(nvarchar(5),getdate(),114),Statuscode='C'";
                }
            }
            //        string sql = string.Format(@"update CTM_JobDet1 set CfsStatus=@ContStatus{0} 
            //where Id=@Id", sql0);
            string sql = string.Format(@"update CTM_JobDet2 set WarehouseStatus=@ContStatus{0} 
where Id=@Id", sql0);

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
                lg.setActionLevel(SafeValue.SafeInt(job["No"], 0), CtmJobEventLogRemark.Level.Trip, 4, ":" + CfsStatus);
                lg.log();
            }
        }
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void WDO_Create_GetCargoList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string client = SafeValue.SafeString(job["ClientId"]);
        string jobNo = SafeValue.SafeString(job["JobNo"]);


        DataTable dt = C2.JobHouse.getStockBalance(jobNo, client, "", "", "", "", "", "","","","");
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }
    [WebMethod]
    public void WDO_Create_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        JArray jar = (JArray)JsonConvert.DeserializeObject(job["list"].ToString());
        string user = SafeValue.SafeString(job["user"]);
        string self = SafeValue.SafeString(job["self"], "Yes");
        string Lat = SafeValue.SafeString(job["Lat"]);
        string Lng = SafeValue.SafeString(job["Lng"]);

        //List<Dictionary<string, object>> trips = new List<Dictionary<string, object>>();
        Dictionary<string, int> trips = new Dictionary<string, int>();

        for (int i = 0; i < jar.Count; i++)
        {
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isMobile();
            elog.Controller = user;
            elog.Lat = Lat;
            elog.Lng = Lng;

            C2.CtmJobDet2 trip = null;
            string jobNo = SafeValue.SafeString(jar[i]["JobNo"]);
            int LineId = SafeValue.SafeInt(jar[i]["LineId"], 0);
            decimal valueQty = SafeValue.SafeDecimal(jar[i]["valueQty"]);
            decimal valueSkuQty = SafeValue.SafeDecimal(jar[i]["valueSkuQty"]);
            if (!trips.ContainsKey(jobNo))
            {
                #region ==== add trip
                trip = new C2.CtmJobDet2();
                trip.JobType = "WDO";
                trip.TripCode = "WDO";
                trip.Self_Ind = self;
                trip.WarehouseStatus = "Completed";
                trip.Statuscode = (self == "Yes" ? "C" : "P");
                trip.BookingDate = DateTime.Now;
                trip.FromDate = DateTime.Now;
                trip.ToDate = DateTime.Now;
                trip.FromTime = DateTime.Now.ToString("HH:mm");
                trip.ToTime = DateTime.Now.ToString("HH:mm");
                trip.WarehouseScheduleDate = DateTime.Now;
                trip.WarehouseStartDate = DateTime.Now;
                trip.WarehouseEndDate = DateTime.Now;
                trip.FromCode = "ELL2P";
                trip.ToCode = (self == "Yes" ? "Self collection" : "");
                trip.SubletFlag = "N";
                trip.BayCode = "B1";
                trip.StageCode = "Pending";
                trip.Overtime = "Normal";
                trip.OverDistance = "Y";
                trip.DriverCode = user;
                trip.JobNo = jobNo;

                //string sql = string.Format(@"select max(TripIndex) from CTM_JobDet2 where JobType=@JobType and JobNo=@JobNo");
                //List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                //list.Add(new ConnectSql_mb.cmdParameters("@JobNo", trip.JobNo, SqlDbType.NVarChar, 100));
                //list.Add(new ConnectSql_mb.cmdParameters("@JobType", trip.JobType, SqlDbType.NVarChar, 100));
                //string maxIdex = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql, list).context, "//00");
                //int n = SafeValue.SafeInt(maxIdex.Substring(maxIdex.LastIndexOf("/") + 1), 0) + 1;
                //string str = (100 + n).ToString().Substring(1);
                //trip.TripIndex = trip.JobNo + "/" + trip.JobType + "/" + str;

                trip.TripIndex = C2.CtmJobDet2.getTripIndex(trip.JobNo, trip.JobType);

                trip.CreateUser = HttpContext.Current.User.Identity.Name;
                trip.CreateTime = DateTime.Now;
                trip.UpdateUser = HttpContext.Current.User.Identity.Name;
                trip.UpdateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(trip);

                elog.setActionLevel(trip.Id, CtmJobEventLogRemark.Level.Trip, 1, ":self DO trip,WHS status:Completed" + (self == "Yes" ? ",TP status:Completed" : ""));
                elog.log();
                trips.Add(jobNo, trip.Id);
                #endregion
            }
            else
            {
                //trips[jobNo] = (trips[jobNo] + 1);

                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + trips[jobNo] + "'");
                trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
            }

            if (trip != null)
            {
                #region add DO cargo
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + LineId + "");
                C2.JobHouse house = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;

                house.CargoStatus = "C";
                house.CargoType = "OUT";
                house.JobNo = trip.JobNo;
                house.ContNo = "";
                house.JobType = trip.JobType;
                house.QtyOrig = valueQty;
                house.PackQty = valueSkuQty;
                house.Weight = 0;
                house.Volume = 0;
                house.WeightOrig = 0;
                house.VolumeOrig = 0;
                house.LandStatus = "Normal";
                house.DgClass = "Normal";
                house.DamagedStatus = "Normal";
                house.RefNo = trip.JobNo;
                //house.ClientId = ;
                house.LineId = LineId;
                house.ContId = 0; //SafeValue.SafeInt(cbb_ContNo.Value, 0);
                house.ContNo = "";// SafeValue.SafeString(cbb_ContNo.Text);
                house.TripIndex = trip.TripIndex;// SafeValue.SafeString(cbb_TripNo.Text);
                house.TripId = trip.Id;// SafeValue.SafeInt(cbb_TripNo.Value, 0);

                house.Qty = valueQty;
                house.OpsType = "Delivery";
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
    public void TSL_CientAlert_GetData_FromMB(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int contId = SafeValue.SafeInt(job["contId"], 0);
        string user = SafeValue.SafeString(job["user"]);
        string role = SafeValue.SafeString(job["role"]).ToLower();

        bool status = false;
        if (role.Equals("admin"))
        {
            status = true;
        }else
        {
            if (role.Equals("driver"))
            {
                string sql = string.Format(@"select Id
from CTM_JobDet2 
where Det1Id=@ContId and driverCode=@Driver");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@ContId", contId, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@Driver", user, SqlDbType.NVarChar, 100));
                DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                if (dt.Rows.Count > 0)
                {
                    status = true;
                }
            }
        }

        Common.WriteJsonP(status, Common.StringToJson(""));
    }

}
