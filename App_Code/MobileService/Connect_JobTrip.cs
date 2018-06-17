using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_JobTrip 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect_JobTrip : System.Web.Services.WebService
{

    public Connect_JobTrip()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void EGL_JobTrip_Calendar_GetDataList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where = " and (det2.DriverCode=@DriverCode or det2.DriverCode2=@DriverCode)";
        }
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,isnull(det2.DriverCode,'') as DriverCode,TowheadCode,ChessisCode,det2.Statuscode,det2.Remark, 
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det1.SealNo,det1.TTTime,det2.DoubleMounting,
case det2.Statuscode when 'S' then 1 when 'C' then 3 when 'X' then 4 when 'A' then 5 else 2 end as OrderIndex,
job.IsTrucking,job.IsWarehouse,job.IsLocal,job.IsAdhoc
From CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where (DATEDIFF(day,FromDate,@date)=0 ) and job.JobType in ('IMP','EXP','LOC') and isnull(det2.JobType,'') not in ('TPT','WGR','WDO') {0}
order by OrderIndex,TripIndex1,det2.FromDate,det2.FromTime,det2.BookingTime,det1.TTTime", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void EGL_JobTrip_GetDataList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where += " and (det2.DriverCode=@DriverCode or det2.DriverCode2=@DriverCode)";
        }
        if (job["no"].ToString().Trim().Length > 0)
        {
            sql_where += " and det2.ContainerNo like @ContainerNo";
        }
        string sql = string.Format(@"select det2.Id,det2.JobNo,ContainerNo,det2.DriverCode as DriverCode,TowheadCode,ChessisCode,det2.Statuscode,det2.Remark 
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Tripcode as JobType,
case det2.Statuscode when 'S' then 1 when 'C' then 3 when 'X' then 4 else 2 end as OrderIndex
From CTM_JobDet2 as det2
where DATEDIFF(day,BookingDate,@from)<=0 and DATEDIFF(day,BookingDate,@to)>=0 {0}
order by ContainerNo,OrderIndex", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", job["no"] + "%", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }



    [WebMethod]
    public void FCL_JobView_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        //        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.DriverCode,det2.TowheadCode,det2.ChessisCode,job.Vessel,job.Voyage,det1.BR as BR,
        //job.CarrierBkgNo AS ClientRefNo,det1.SealNo,det1.ContainerType,job.CarrierId,p.Name as CarrierName,det1.TTTime,det2.Remark,det2.Remark1,det2.Statuscode,
        //FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,det2.Carpark,det2.ToParkingLot,det2.ParkingZone,det2.TripCode,det2.DoubleMounting,
        //det2.Incentive1,det2.Incentive2,det2.Incentive3,det2.Incentive4,
        //det2.charge1,det2.charge2,det2.charge3,det2.charge4,det2.charge5,det2.charge6,det2.charge7,det2.charge8,det2.charge9,det2.charge10 
        //from CTM_JobDet2 as det2
        //left outer join CTM_Job as job on det2.JobNo=job.JobNo
        //left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
        //left outer join XXParty as p on job.ClientId=p.PartyId
        //where det2.Id={0}", job["No"]);
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.DriverCode,det2.TowheadCode,det2.ChessisCode,job.Vessel,job.Voyage,det1.BR as BR,det1.ContWeight,
job.CarrierBkgNo AS ClientRefNo,det1.SealNo,det1.ContainerType,job.CarrierId, p.Name as CarrierName,det1.TTTime,det2.Remark,det2.Remark1,det2.Statuscode,
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,det2.Carpark,det2.ToParkingLot,det2.ParkingZone,det2.TripCode,det2.DoubleMounting
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join XXParty as p on job.ClientId=p.PartyId
where det2.Id={0}", job["No"]);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string mast = "{}";
        string attachment = "[]";
        if (dt.Rows.Count > 0)
        {
            mast = Common.DataRowToJson(dt);
            sql = string.Format(@"select Id,FileType,FileName,FilePath,FileNote,'' as FP500 From CTM_Attachment where TripId={0}", job["No"]);
            dt = ConnectSql_mb.GetDataTable(sql);
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

        string context = string.Format(@"{0}job:{2},attachment:{3}{1}", "{", "}", mast, attachment);


        bool status = true;
        Common.WriteJsonP(status, context);
    }
    [WebMethod]
    public void FCL_JobMultiView_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string context = "";// string.Format(@"{0}{2},{3}{1}", "{", "}", mast, attachment);
        //        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.DriverCode,det2.TowheadCode,det2.ChessisCode,job.Vessel,job.Voyage,det1.BR as BR,
        //job.CarrierBkgNo AS ClientRefNo,det1.SealNo,det1.ContainerType,job.CarrierId,p.Name as CarrierName,det1.TTTime,det2.Remark,det2.Remark1,det2.Statuscode,
        //FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,det2.Carpark,det2.ToParkingLot,det2.ParkingZone,det2.TripCode,det2.DoubleMounting,
        //det2.Incentive1,det2.Incentive2,det2.Incentive3,det2.Incentive4,
        //det2.charge1,det2.charge2,det2.charge3,det2.charge4,det2.charge5,det2.charge6,det2.charge7,det2.charge8,det2.charge9,det2.charge10 
        //from CTM_JobDet2 as det2
        //left outer join CTM_Job as job on det2.JobNo=job.JobNo
        //left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
        //left outer join XXParty as p on job.ClientId=p.PartyId
        //where det2.Id={0}", job["No"]);
        string sql = string.Format(@"select top 1 det2.Id,det2.JobNo,det2.ContainerNo,det2.DriverCode,det2.DriverCode2,det2.TowheadCode,det2.ChessisCode,job.Vessel,job.Voyage,det1.BR as BR,det1.ContWeight,
job.PortnetRef AS ClientRefNo,det1.SealNo,det1.ContainerType,job.CarrierId, job.WarehouseAddress as CarrierName,det1.TTTime,det2.Remark,det2.Remark1,det2.Statuscode,
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,det2.Carpark,det2.ToParkingLot,det2.ParkingZone,det2.TripCode,det2.DoubleMounting
,job.EtaDate,job.EtaTime,job.EtdDate,job.EtdTime
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join XXParty as p on job.ClientId=p.PartyId
where det2.Id={0}", job["No"]);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string mast = "{}";
        string attachment = "[]";
        string incentive = "[]";
        string claims = "[]";
        if (dt.Rows.Count > 0)
        {
            string trip_status = dt.Rows[0]["Statuscode"].ToString();
            string trip_dbm = dt.Rows[0]["DoubleMounting"].ToString();
            string trip_driver = dt.Rows[0]["DriverCode"].ToString();
            mast = Common.DataRowToJson(dt);

            sql = string.Format(@"select Id,LineId,LineType,TripNo,ChgCode,ChgCodeDes,Qty,Price,VendorId from job_cost where TripNo=@TripNo and LineType='DP'");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@TripNo", SafeValue.SafeInt(job["No"], 0), SqlDbType.Int));
            dt = ConnectSql_mb.GetDataTable(sql, list);
            incentive = Common.DataTableToJson(dt);
            sql = string.Format(@"select Id,LineId,LineType,TripNo,ChgCode,ChgCodeDes,Qty,Price,VendorId from job_cost where TripNo=@TripNo and LineType='CL'");
            dt = ConnectSql_mb.GetDataTable(sql, list);
            claims = Common.DataTableToJson(dt);


            sql = string.Format(@"select Id,FileType,FileName,FilePath,FileNote,'' as FP500 From CTM_Attachment where TripId={0}", job["No"]);
            dt = ConnectSql_mb.GetDataTable(sql);
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
            context += string.Format(@"{0}job:{2},attachment:{3},incentive:{4},claims:{5}{1}", "{", "}", mast, attachment, incentive, claims);


            //============== double mounting trip
            if (trip_status.ToUpper().Equals("S") && trip_dbm.ToUpper().Equals("YES"))
            {
                list = new List<ConnectSql_mb.cmdParameters>();
                sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.DriverCode,det2.DriverCode2,det2.TowheadCode,det2.ChessisCode,job.Vessel,job.Voyage,det1.BR as BR,det1.ContWeight,
job.PortnetRef AS ClientRefNo,det1.SealNo,det1.ContainerType,job.CarrierId,p.Name as CarrierName,det1.TTTime,det2.Remark,det2.Remark1,det2.Statuscode,
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,det2.Carpark,det2.ToParkingLot,det2.ParkingZone,det2.TripCode,det2.DoubleMounting
,job.EtaDate,job.EtaTime,job.EtdDate,job.EtdTime
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join XXParty as p on job.ClientId=p.PartyId
where det2.Id<>@Id and det2.Statuscode=@Statuscode and det2.DoubleMounting=@DoubleMounting and det2.DriverCode=@DriverCode ");
                list.Add(new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(job["No"], 0), SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@Statuscode", "S", SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@DoubleMounting", "Yes", SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", trip_driver, SqlDbType.NVarChar, 100));
                dt = ConnectSql_mb.GetDataTable(sql, list);
                if (dt.Rows.Count > 0)
                {
                    mast = Common.DataRowToJson(dt);

                    sql = string.Format(@"select Id,FileType,FileName,FilePath,FileNote,'' as FP500 From CTM_Attachment where TripId={0}", dt.Rows[0]["Id"]);
                    dt = ConnectSql_mb.GetDataTable(sql);
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

                    sql = string.Format(@"select Id,LineId,LineType,TripNo,ChgCode,ChgCodeDes,Qty,Price,VendorId from job_cost where TripNo=@TripNo and LineType='DP'");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@TripNo", SafeValue.SafeInt(dt.Rows[0]["Id"], 0), SqlDbType.Int));
                    dt = ConnectSql_mb.GetDataTable(sql, list);
                    incentive = Common.DataTableToJson(dt);
                    sql = string.Format(@"select Id,LineId,LineType,TripNo,ChgCode,ChgCodeDes,Qty,Price,VendorId from job_cost where TripNo=@TripNo and LineType='CL'");
                    dt = ConnectSql_mb.GetDataTable(sql, list);
                    claims = Common.DataTableToJson(dt);

                    context += (context.Length > 0 ? "," : "") + string.Format(@"{0}job:{2},attachment:{3},incentive:{4},claims:{5}{1}", "{", "}", mast, attachment, incentive, claims);
                }
            }
        }

        //string context = string.Format(@"{0}{2},{3}{1}", "{", "}", mast, attachment);
        context = "[" + context + "]";
        bool status = true;
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void FCL_JobEdit_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string mast = "{}";
        string incentive = "[]";
        string claims = "[]";

        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.DriverCode,det2.DriverCode2,det2.TowheadCode,det2.ChessisCode,det2.Remark1,det1.ContWeight,
det1.SealNo,det2.Det1Id,det2.Carpark,det2.ToParkingLot,det2.ToCode,det2.ParkingZone,det2.TripCode,det2.Statuscode
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id 
where det2.Id={0}", job["No"]);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        mast = Common.DataRowToJson(dt);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        sql = string.Format(@"select Id,LineId,LineType,TripNo,ChgCode,ChgCodeDes,Qty,Price,VendorId from job_cost where TripNo=@TripNo and LineType='DP'");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@TripNo", SafeValue.SafeInt(dt.Rows[0]["Id"], 0), SqlDbType.Int));
        dt = ConnectSql_mb.GetDataTable(sql, list);
        incentive = Common.DataTableToJson(dt);
        sql = string.Format(@"select Id,LineId,LineType,TripNo,ChgCode,ChgCodeDes,Qty,Price,VendorId from job_cost where TripNo=@TripNo and LineType='CL'");
        dt = ConnectSql_mb.GetDataTable(sql, list);
        claims = Common.DataTableToJson(dt);

        string context = string.Format(@"{0}mast:{2},incentive:{3},claims:{4}{1}", "{", "}", mast, incentive, claims);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void FCL_JobEdit_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        //        string sql = string.Format(@"update CTM_JobDet2 set ContainerNo=@ContainerNo,ChessisCode=@ChessisCode,Remark1=@Remark1,Carpark=@Carpark,ToCode=@ToCode,ToParkingLot=@ToParkingLot,ParkingZone=@ParkingZone,
        //Incentive1=@Incentive1,Incentive2=@Incentive2,Incentive3=@Incentive3,Incentive4=@Incentive4,
        //charge1=@charge1,charge2=@charge2,charge3=@charge3,charge4=@charge4,charge5=@charge5,charge6=@charge6,charge7=@charge7,charge8=@charge8,charge9=@charge9,charge10=@charge10 
        //where Id=@Id");
        int det2Id = SafeValue.SafeInt(job["Id"], 0);
        string ContainerNo = SafeValue.SafeString(job["ContainerNo"]).ToUpper();
        string SealNo = SafeValue.SafeString(job["SealNo"]).ToUpper();
        string ChessisCode = SafeValue.SafeString(job["ChessisCode"]).ToUpper();
        string Remark1 = SafeValue.SafeString(job["Remark1"]);
        string Carpark = SafeValue.SafeString(job["Carpark"]);
        string ToCode = SafeValue.SafeString(job["ToCode"]);
        string ToParkingLot = SafeValue.SafeString(job["ToParkingLot"]);
        string ParkingZone = SafeValue.SafeString(job["ParkingZone"]);
        string user = SafeValue.SafeString(job["user"]);
        C2.CtmJobDet2Biz det2Bz = new C2.CtmJobDet2Biz(det2Id);
        C2.CtmJobDet2 det2 = det2Bz.getData();
        if (det2 != null)
        {
            det2.ContainerNo = ContainerNo;
            det2.ChessisCode = ChessisCode;
            det2.Remark1 = Remark1;
            det2.Carpark = Carpark;
            det2.ToCode = ToCode;
            det2.ToParkingLot = ToParkingLot;
            det2.ParkingZone = ParkingZone;
            C2.BizResult result = det2Bz.update(user);
            if (result.status)
            {

                status = "1";
                string sql1 = string.Format(@"update CTM_JobDet1 set SealNo=@SealNo,ContWeight=@ContWeight where Id=@Id");
                list = new List<ConnectSql_mb.cmdParameters>();
                cpar = new ConnectSql_mb.cmdParameters("@Id", job["Det1Id"], SqlDbType.Int);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@SealNo", SafeValue.SafeString(job["SealNo"]).ToUpper(), SqlDbType.NVarChar, 100);
                list.Add(cpar);
                list.Add(new ConnectSql_mb.cmdParameters("@ContWeight", SafeValue.SafeDecimal(job["ContWeight"]), SqlDbType.Decimal));
                ConnectSql_mb.ExecuteNonQuery(sql1, list);

                #region

                Dictionary<string, decimal> d = new Dictionary<string, decimal>();
                d.Add("Trip", SafeValue.SafeDecimal(job["Trip"], 0));
                d.Add("OverTime", SafeValue.SafeDecimal(job["OverTime"], 0));
                d.Add("Standby", SafeValue.SafeDecimal(job["Standby"], 0));
                d.Add("PSA", SafeValue.SafeDecimal(job["PSA"], 0));
                C2.CtmJobDet2.Incentive_Save(SafeValue.SafeInt(job["Id"], 0), d);
                d = new Dictionary<string, decimal>();
                d.Add("DHC", SafeValue.SafeDecimal(job["DHC"], 0));
                d.Add("WEIGHING", SafeValue.SafeDecimal(job["WEIGHING"], 0));
                d.Add("WASHING", SafeValue.SafeDecimal(job["WASHING"], 0));
                d.Add("REPAIR", SafeValue.SafeDecimal(job["REPAIR"], 0));
                d.Add("DETENTION", SafeValue.SafeDecimal(job["DETENTION"], 0));
                d.Add("DEMURRAGE", SafeValue.SafeDecimal(job["DEMURRAGE"], 0));
                d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(job["LIFT_ON_OFF"], 0));
                d.Add("C_SHIPMENT", SafeValue.SafeDecimal(job["C_SHIPMENT"], 0));
                d.Add("EMF", SafeValue.SafeDecimal(job["EMF"], 0));
                d.Add("OTHER", SafeValue.SafeDecimal(job["OTHER"], 0));
                d.Add("ERP", SafeValue.SafeDecimal(job["ERP"], 0));
                d.Add("ParkingFee", SafeValue.SafeDecimal(job["ParkingFee"], 0));
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
                #endregion
            }
        }
//        string sql = string.Format(@"update CTM_JobDet2 set ContainerNo=@ContainerNo,ChessisCode=@ChessisCode,Remark1=@Remark1,Carpark=@Carpark,ToCode=@ToCode,ToParkingLot=@ToParkingLot,ParkingZone=@ParkingZone
//where Id=@Id");
//        #region params
//        cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", SafeValue.SafeString(job["ContainerNo"]).ToUpper(), SqlDbType.NVarChar, 100);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ChessisCode", SafeValue.SafeString(job["ChessisCode"]).ToUpper(), SqlDbType.NVarChar, 100);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@Remark1", job["Remark1"], SqlDbType.NVarChar, 100);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@Carpark", job["Carpark"], SqlDbType.NVarChar, 100);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ToCode", job["ToCode"], SqlDbType.NVarChar, 300);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ToParkingLot", job["ToParkingLot"], SqlDbType.NVarChar, 100);
//        list.Add(cpar);
//        cpar = new ConnectSql_mb.cmdParameters("@ParkingZone", job["ParkingZone"], SqlDbType.NVarChar, 100);
//        list.Add(cpar);

//        //cpar = new ConnectSql_mb.cmdParameters("@Incentive1", SafeValue.SafeDecimal(job["Incentive1"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@Incentive2", SafeValue.SafeDecimal(job["Incentive2"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@Incentive3", SafeValue.SafeDecimal(job["Incentive3"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@Incentive4", SafeValue.SafeDecimal(job["Incentive4"]), SqlDbType.Decimal);
//        //list.Add(cpar);

//        //cpar = new ConnectSql_mb.cmdParameters("@charge1", SafeValue.SafeDecimal(job["charge1"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge2", SafeValue.SafeDecimal(job["charge2"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge3", SafeValue.SafeDecimal(job["charge3"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge4", SafeValue.SafeDecimal(job["charge4"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge5", SafeValue.SafeDecimal(job["charge5"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge6", SafeValue.SafeDecimal(job["charge6"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge7", SafeValue.SafeDecimal(job["charge7"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge8", SafeValue.SafeDecimal(job["charge8"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge9", SafeValue.SafeDecimal(job["charge9"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        //cpar = new ConnectSql_mb.cmdParameters("@charge10", SafeValue.SafeDecimal(job["charge10"]), SqlDbType.Decimal);
//        //list.Add(cpar);
//        #endregion

//        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
//        {
//            status = "1";
//            sql = string.Format(@"update CTM_JobDet1 set SealNo=@SealNo,ContainerNo=@ContainerNo where Id=@Id");
//            list = new List<ConnectSql_mb.cmdParameters>();
//            cpar = new ConnectSql_mb.cmdParameters("@Id", job["Det1Id"], SqlDbType.Int);
//            list.Add(cpar);
//            cpar = new ConnectSql_mb.cmdParameters("@SealNo", SafeValue.SafeString(job["SealNo"]).ToUpper(), SqlDbType.NVarChar, 100);
//            list.Add(cpar);
//            cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", SafeValue.SafeString(job["ContainerNo"]).ToUpper(), SqlDbType.NVarChar, 100);
//            list.Add(cpar);
//            ConnectSql_mb.ExecuteNonQuery(sql, list);

//            sql = string.Format(@"update job_house set ContNo=@ContainerNo where ContId=@Id");
//            ConnectSql_mb.ExecuteNonQuery(sql, list);

//            #region

//            Dictionary<string, decimal> d = new Dictionary<string, decimal>();
//            d.Add("Trip", SafeValue.SafeDecimal(job["Trip"], 0));
//            d.Add("OverTime", SafeValue.SafeDecimal(job["OverTime"], 0));
//            d.Add("Standby", SafeValue.SafeDecimal(job["Standby"], 0));
//            d.Add("PSA", SafeValue.SafeDecimal(job["PSA"], 0));
//            C2.CtmJobDet2.Incentive_Save(SafeValue.SafeInt(job["Id"], 0), d);
//            d = new Dictionary<string, decimal>();
//            d.Add("DHC", SafeValue.SafeDecimal(job["DHC"], 0));
//            d.Add("WEIGHING", SafeValue.SafeDecimal(job["WEIGHING"], 0));
//            d.Add("WASHING", SafeValue.SafeDecimal(job["WASHING"], 0));
//            d.Add("REPAIR", SafeValue.SafeDecimal(job["REPAIR"], 0));
//            d.Add("DETENTION", SafeValue.SafeDecimal(job["DETENTION"], 0));
//            d.Add("DEMURRAGE", SafeValue.SafeDecimal(job["DEMURRAGE"], 0));
//            d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(job["LIFT_ON_OFF"], 0));
//            d.Add("C_SHIPMENT", SafeValue.SafeDecimal(job["C_SHIPMENT"], 0));
//            d.Add("EMF", SafeValue.SafeDecimal(job["EMF"], 0));
//            d.Add("OTHER", SafeValue.SafeDecimal(job["OTHER"], 0));
//            d.Add("ERP", SafeValue.SafeDecimal(job["ERP"], 0));
//            d.Add("ParkingFee", SafeValue.SafeDecimal(job["ParkingFee"], 0));
//            C2.CtmJobDet2.Claims_Save(SafeValue.SafeInt(job["Id"], 0), d);


//            //===========log
//            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
//            lg.Platform_isMobile();
//            lg.Controller = SafeValue.SafeString(job["user"]);
//            lg.Lat = SafeValue.SafeString(job["Lat"]);
//            lg.Lng = SafeValue.SafeString(job["Lng"]);
//            //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"], 0));
//            //lg.Remark = "Trip update";
//            lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.Trip, 3);
//            lg.log();


//            //            sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='INCENTIVE'");
//            //            DataTable dt_incentive = ConnectSql_mb.GetDataTable(sql);
//            //            sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='CLAIMS'");
//            //            DataTable dt_claims = ConnectSql_mb.GetDataTable(sql);

//            //            sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,det1.ContainerNo,det1.ContainerType
//            //from ctm_jobdet2 as det2
//            //left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
//            //left outer join CTM_Job as job on det2.JobNo=job.JobNo
//            //where det2.Id=@TripId");
//            //            list = new List<ConnectSql_mb.cmdParameters>();
//            //            list.Add(new ConnectSql_mb.cmdParameters("@TripId", SafeValue.SafeInt(job["Id"], 0), SqlDbType.Int));
//            //            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
//            //            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
//            //            list.Add(new ConnectSql_mb.cmdParameters("@JobType", dt.Rows[0]["JobType"], SqlDbType.NVarChar, 100));
//            //            list.Add(new ConnectSql_mb.cmdParameters("@ContNo", dt.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
//            //            list.Add(new ConnectSql_mb.cmdParameters("@ContType", dt.Rows[0]["ContainerType"], SqlDbType.NVarChar, 100));


//            //            ConnectSql_mb.cmdParameters ChgCode = new ConnectSql_mb.cmdParameters("@ChgCode", "", SqlDbType.NVarChar, 100);
//            //            list.Add(ChgCode);
//            //            ConnectSql_mb.cmdParameters ChgCodeDes = new ConnectSql_mb.cmdParameters("@ChgCodeDes", "", SqlDbType.NVarChar, 100);
//            //            list.Add(ChgCodeDes);
//            //            ConnectSql_mb.cmdParameters Price = new ConnectSql_mb.cmdParameters("@Price", 0, SqlDbType.Decimal);
//            //            list.Add(Price);
//            //            ConnectSql_mb.cmdParameters LineType = new ConnectSql_mb.cmdParameters("@LineType", "", SqlDbType.NVarChar, 10);
//            //            list.Add(LineType);

//            //            LineType.value = "DP";
//            //            Dictionary<string, decimal> d = new Dictionary<string, decimal>();
//            //            d.Add("Trip", SafeValue.SafeDecimal(job["Trip"], 0));
//            //            d.Add("OverTime", SafeValue.SafeDecimal(job["OverTime"], 0));
//            //            d.Add("Standby", SafeValue.SafeDecimal(job["Standby"], 0));
//            //            d.Add("PSA", SafeValue.SafeDecimal(job["PSA"], 0));
//            //            cost_row2list_foreach(d, dt_incentive, list, ChgCode, ChgCodeDes, Price);

//            //            LineType.value = "CL";
//            //            d = new Dictionary<string, decimal>();
//            //            d.Add("DHC", SafeValue.SafeDecimal(job["DHC"], 0));
//            //            d.Add("WEIGHING", SafeValue.SafeDecimal(job["WEIGHING"], 0));
//            //            d.Add("WASHING", SafeValue.SafeDecimal(job["WASHING"], 0));
//            //            d.Add("REPAIR", SafeValue.SafeDecimal(job["REPAIR"], 0));
//            //            d.Add("DETENTION", SafeValue.SafeDecimal(job["DETENTION"], 0));
//            //            d.Add("DEMURRAGE", SafeValue.SafeDecimal(job["DEMURRAGE"], 0));
//            //            d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(job["LIFT_ON_OFF"], 0));
//            //            d.Add("C_SHIPMENT", SafeValue.SafeDecimal(job["C_SHIPMENT"], 0));
//            //            d.Add("EMF", SafeValue.SafeDecimal(job["EMF"], 0));
//            //            d.Add("OTHER", SafeValue.SafeDecimal(job["OTHER"], 0));
//            //            cost_row2list_foreach(d, dt_claims, list, ChgCode, ChgCodeDes, Price);

//            #endregion
//        }
        Common.WriteJsonP(status, context);
    }

    //    private void cost_row2list_foreach(Dictionary<string, decimal> d, DataTable dt_cost, List<ConnectSql_mb.cmdParameters> list, ConnectSql_mb.cmdParameters ChgCode, ConnectSql_mb.cmdParameters ChgCodeDes, ConnectSql_mb.cmdParameters Price)
    //    {
    //        string sql_select = string.Format(@"select Price from job_cost where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId");
    //        string sql_insert = string.Format(@"insert into job_cost (LineId,LineType,JobNo,JobType,ContNo,ContType,TripNo,VendorId,ChgCode,ChgCodeDes,Remark,Qty,Price,CurrencyId,ExRate,DocAmt,LocAmt,CompanyId)
    //values(0,@LineType,@JobNo,@JobType,@ContNo,@ContType,@TripId,'',@ChgCode,@ChgCodeDes,'',1,@Price,'SGD',1,0,0,0)");
    //        string sql_delete = string.Format(@"delete from job_cost where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId");
    //        string sql_update = string.Format(@"update job_cost set Qty=1,Price=@Price where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId");
    //        DataTable dt = null;

    //        foreach (string key in d.Keys)
    //        {
    //            decimal val = d[key];
    //            ChgCode.value = key;
    //            string key_des = key;
    //            for (int i = 0; i < dt_cost.Rows.Count; i++)
    //            {
    //                if (key == dt_cost.Rows[i]["c"].ToString())
    //                {
    //                    key_des = dt_cost.Rows[i]["n"].ToString();
    //                }
    //            }
    //            ChgCodeDes.value = key_des;
    //            Price.value = val;

    //            dt = ConnectSql_mb.GetDataTable(sql_select, list);
    //            if (val != 0)
    //            {
    //                if (dt.Rows.Count != 1)
    //                {
    //                    if (dt.Rows.Count > 1)
    //                    {
    //                        ConnectSql_mb.ExecuteNonQuery(sql_delete, list);
    //                    }
    //                    ConnectSql_mb.ExecuteNonQuery(sql_insert, list);
    //                }
    //                else
    //                {
    //                    if (val != SafeValue.SafeDecimal(dt.Rows[0]["Price"], 0))
    //                    {
    //                        ConnectSql_mb.ExecuteNonQuery(sql_update, list);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (dt.Rows.Count != 0)
    //                {
    //                    ConnectSql_mb.ExecuteNonQuery(sql_delete, list);
    //                }
    //            }
    //        }

    //    }

    [WebMethod]
    public void EGL_JobTrip_ChangeStatus(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = "";
//        string sql = string.Format(@"select count(*) from Vehicle_Mileage 
//where datediff(day,ReportDate,getdate())=0 and CreateBy=@CreateBy");
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        //if (ConnectSql_mb.ExecuteScalar(sql, list).context.Equals("0"))
        //{
        //    status = "0";
        //    context = Common.StringToJson("Require Mileage!");
        //}
        //else
        {
            string Statuscode = job["Statuscode"].ToString();
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

                if (dbMounting.ToUpper().Equals("YES"))
                {
                    if (dt.Rows.Count >= 2)
                    {
                        status = "0";
                        context = Common.StringToJson("Double mounting is full, please check!");
                    }
                    else
                    {
                        if (dt.Rows.Count == 1 && !SafeValue.SafeString(dt.Rows[0]["DoubleMounting"], "").ToUpper().Equals("YES"))
                        {
                            status = "0";
                            context = Common.StringToJson("The started trip is not double mounting, please check!");
                        }
                    }
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        status = "0";
                        context = Common.StringToJson("Please end the other trip, then start this!");
                    }
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
                DateTime dtime = SafeValue_mb.DateTime_ClearTime(DateTime.Now);
                string time = DateTime.Now.ToString("HH:mm");
                int det2Id = SafeValue.SafeInt(job["Id"], 0);
                string user = SafeValue.SafeString(job["user"]);

                C2.CtmJobDet2Biz det2Bz = new C2.CtmJobDet2Biz(det2Id);
                C2.CtmJobDet2 det2 = det2Bz.getData();
                status = "0";
                if (det2 != null)
                {
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
                        lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.Trip, 4, ":" + Statuscode);
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
                //sql = string.Format(@"update CTM_JobDet2 set Statuscode=@Statuscode {0} where Id=@Id", sql1);
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
                //    //lg.ActionLevel_isTRIP(SafeValue.SafeInt(job["Id"], 0));
                //    //lg.Remark = "Trip change status:" + Statuscode;
                //    lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.Trip, 4, ":" + status);
                //    lg.log();
                //}
                //else
                //{
                //    status = "0";
                //}
            }
        }
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_JobTrip_Attachment_Add(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        string sql = string.Format(@"insert into CTM_Attachment (JobType,RefNo,ContainerNo,TripId,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values(@JobType,@RefNo,@ContainerNo,@TripId,@FileType,@FileName,@FilePath,@CreateBy,Getdate(),@FileNote)
select @@Identity");

        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].Trim();
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
        string ContainerNo = SafeValue.SafeString(job["ContainerNo"]);
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
    public void EGL_ParkingZone_GetList()
    {
        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select * from CTM_ParkingZone");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }




    #region shifting


    [WebMethod]
    public void EGL_JobTrip_Cont_GetDataList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        //if (job["role"].ToString().Equals("Driver"))
        //{
        //    sql_where += " and det2.DriverCode=@DriverCode";
        //}
        if (job["no"].ToString().Trim().Length > 0)
        {
            sql_where += " and ContainerNo like @ContainerNo";
        }
        string sql = string.Format(@"select top 100 Id,JobNo,ContainerNo,ScheduleDate  
from ctm_jobdet1
where StatusCode<>'Completed' and len(ContainerNo)>4 {0}
order by ScheduleDate desc,ContainerNo,JobNo", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        //cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.NVarChar, 20);
        //list.Add(cpar);
        //cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        //list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", "%" + job["no"] + "%", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void EGL_JobTrip_Cont_GetDataView(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string sql = string.Format(@"select * from CTM_JobDet1 where id={0}", job["No"]);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        bool status = true;
        string mast = Common.DataRowToJson(dt);

        sql = string.Format(@"select Id,JobNo,ContainerNo,DriverCode,TripCode,Statuscode,ToCode,ToParkingLot from ctm_jobdet2 where Det1Id={0} order by Id", job["No"]);
        dt = ConnectSql_mb.GetDataTable(sql);

        string trips = Common.DataTableToJson(dt);

        string context = string.Format(@"{0}job:{2},trips:{3}{1}", "{", "}", mast, trips);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_JobTrip_Cont_AddNewInit(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string sql = string.Format(@"select top 1 ChessisCode,'' as ToParkingLot,'' as ToCode,'' as Remark1,0 as Incentive1,0 as Incentive2,0 as Incentive3,0 as charge2  
from ctm_jobdet2 where Det1Id={0} order by Id desc", job["No"]);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        bool status = true;

        string context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);

    }

    [WebMethod]
    public void EGL_JobTrip_Cont_AddNewSave(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select * from ctm_jobdet1 where Id=@Det1Id");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", job["Det1Id"], SqlDbType.Int));
        DataTable dt_det1 = ConnectSql_mb.GetDataTable(sql, list);
        if (dt_det1.Rows.Count == 1)
        {
            sql = string.Format(@"select PickupFrom,DeliveryTo,YardRef,JobType from CTM_Job where JobNo='{0}'", SafeValue.SafeString(dt_det1.Rows[0]["JobNo"], ""));
            DataTable dt_job = ConnectSql_mb.GetDataTable(sql);
            sql = string.Format(@"select top 1 * from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} order by Id desc", SafeValue.SafeString(dt_det1.Rows[0]["JobNo"], ""), SafeValue.SafeInt(job["Det1Id"], 0));
            DataTable dt_det2 = ConnectSql_mb.GetDataTable(sql);
            string job_from = SafeValue.SafeString(dt_job.Rows[0]["PickupFrom"]);
            string job_to = SafeValue.SafeString(dt_job.Rows[0]["DeliveryTo"]);
            string job_Depot = SafeValue.SafeString(dt_job.Rows[0]["YardRef"]);
            string P_From = "";
            string P_From_Pl = "";
            string P_To = "";// DeliveryTo.Text;
            string trailer = "";
            string JobType = SafeValue.SafeString(dt_job.Rows[0]["JobType"]);
            string TripCode = "SHF";
            DateTime FromDate = DateTime.Now;
            string FromTime = DateTime.Now.ToString("HH:mm");

            if (dt_det2.Rows.Count > 0)
            {
                P_From = dt_det2.Rows[0]["ToCode"].ToString();
                P_From_Pl = dt_det2.Rows[0]["ToParkingLot"].ToString();
                trailer = dt_det2.Rows[0]["ChessisCode"].ToString();
            }
            //            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
            //TripCode,FromParkingLot,ToParkingLot,Remark1,Incentive1,Incentive2,Incentive3,charge2) 
            //values (@JobNo,@ContainerNo,@DriverCode,@TowheadCode,@ChessisCode,@FromCode,@FromDate,@FromTime,@ToCode,@ToDate,@ToTime,@Det1Id,@Statuscode,
            //@TripCode,@FromParkingLot,@ToParkingLot,@Remark1,@Incentive1,@Incentive2,@Incentive3,@charge2)");
            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
TripCode,FromParkingLot,ToParkingLot,Remark1) 
values (@JobNo,@ContainerNo,@DriverCode,@TowheadCode,@ChessisCode,@FromCode,@FromDate,@FromTime,@ToCode,@ToDate,@ToTime,@Det1Id,@Statuscode,
@TripCode,@FromParkingLot,@ToParkingLot,@Remark1)
select @@IDENTITY");
            list = new List<ConnectSql_mb.cmdParameters>();
            #region params
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt_det1.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", dt_det1.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", job["DriverCode"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@TowheadCode", job["TowheadCode"], SqlDbType.NVarChar, 100));

            list.Add(new ConnectSql_mb.cmdParameters("@FromCode", P_From, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@FromDate", FromDate, SqlDbType.DateTime));
            list.Add(new ConnectSql_mb.cmdParameters("@FromTime", FromTime, SqlDbType.NVarChar, 40));
            list.Add(new ConnectSql_mb.cmdParameters("@ToDate", FromDate, SqlDbType.DateTime));
            list.Add(new ConnectSql_mb.cmdParameters("@ToTime", FromTime, SqlDbType.NVarChar, 40));
            list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", job["Det1Id"], SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@Statuscode", "C", SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@TripCode", TripCode, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@FromParkingLot", P_From_Pl, SqlDbType.NVarChar, 100));

            cpar = new ConnectSql_mb.cmdParameters("@ChessisCode", SafeValue.SafeString(job["ChessisCode"]).ToUpper(), SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Remark1", job["Remark1"], SqlDbType.NVarChar, 300);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ToCode", job["ToCode"], SqlDbType.NVarChar, 300);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ToParkingLot", job["ToParkingLot"], SqlDbType.NVarChar, 100);
            list.Add(cpar);

            //cpar = new ConnectSql_mb.cmdParameters("@Incentive1", SafeValue.SafeDecimal(job["Incentive1"]), SqlDbType.Decimal);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@Incentive2", SafeValue.SafeDecimal(job["Incentive2"]), SqlDbType.Decimal);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@Incentive3", SafeValue.SafeDecimal(job["Incentive3"]), SqlDbType.Decimal);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@charge2", SafeValue.SafeDecimal(job["charge2"]), SqlDbType.Decimal);
            //list.Add(cpar);

            #endregion
            ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteScalar(sql, list);
            if (result.status)
            {
                status = "1";
                //EGL_JobTrip_AfterEndTrip("", SafeValue.SafeString(job["Det1Id"], "0"));


                #region

                C2.CtmJobDet2.tripStatusChanged(SafeValue.SafeInt(result.context, 0));

                Dictionary<string, decimal> d = new Dictionary<string, decimal>();
                d.Add("Trip", SafeValue.SafeDecimal(job["Incentive1"], 0));
                d.Add("OverTime", SafeValue.SafeDecimal(job["Incentive2"], 0));
                d.Add("Standby", SafeValue.SafeDecimal(job["Incentive3"], 0));
                //d.Add("PSA", SafeValue.SafeDecimal(job["PSA"], 0));
                C2.CtmJobDet2.Incentive_Save(SafeValue.SafeInt(result.context, 0), d);
                d = new Dictionary<string, decimal>();
                //d.Add("DHC", SafeValue.SafeDecimal(job["DHC"], 0));
                d.Add("WEIGHING", SafeValue.SafeDecimal(job["charge2"], 0));
                //d.Add("WASHING", SafeValue.SafeDecimal(job["WASHING"], 0));
                //d.Add("REPAIR", SafeValue.SafeDecimal(job["REPAIR"], 0));
                //d.Add("DETENTION", SafeValue.SafeDecimal(job["DETENTION"], 0));
                //d.Add("DEMURRAGE", SafeValue.SafeDecimal(job["DEMURRAGE"], 0));
                //d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(job["LIFT_ON_OFF"], 0));
                //d.Add("C_SHIPMENT", SafeValue.SafeDecimal(job["C_SHIPMENT"], 0));
                //d.Add("EMF", SafeValue.SafeDecimal(job["EMF"], 0));
                //d.Add("OTHER", SafeValue.SafeDecimal(job["OTHER"], 0));
                C2.CtmJobDet2.Claims_Save(SafeValue.SafeInt(result.context, 0), d);


                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                //lg.ActionLevel_isTRIP(SafeValue.SafeInt(result.context, 0));
                //lg.Remark = "Trip add new";
                lg.setActionLevel(SafeValue.SafeInt(result.context, 0), CtmJobEventLogRemark.Level.Trip, 1);
                lg.log();

                //                sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='INCENTIVE'");
                //                DataTable dt_incentive = ConnectSql_mb.GetDataTable(sql);
                //                sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='CLAIMS'");
                //                DataTable dt_claims = ConnectSql_mb.GetDataTable(sql);

                //                sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,det1.ContainerNo,det1.ContainerType
                //from ctm_jobdet2 as det2
                //left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
                //left outer join CTM_Job as job on det2.JobNo=job.JobNo
                //where det2.Id=@TripId");
                //                list = new List<ConnectSql_mb.cmdParameters>();
                //                list.Add(new ConnectSql_mb.cmdParameters("@TripId", SafeValue.SafeInt(result.context, 0), SqlDbType.Int));
                //                DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                //                list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
                //                list.Add(new ConnectSql_mb.cmdParameters("@JobType", dt.Rows[0]["JobType"], SqlDbType.NVarChar, 100));
                //                list.Add(new ConnectSql_mb.cmdParameters("@ContNo", dt.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
                //                list.Add(new ConnectSql_mb.cmdParameters("@ContType", dt.Rows[0]["ContainerType"], SqlDbType.NVarChar, 100));


                //                ConnectSql_mb.cmdParameters ChgCode = new ConnectSql_mb.cmdParameters("@ChgCode", "", SqlDbType.NVarChar, 100);
                //                list.Add(ChgCode);
                //                ConnectSql_mb.cmdParameters ChgCodeDes = new ConnectSql_mb.cmdParameters("@ChgCodeDes", "", SqlDbType.NVarChar, 100);
                //                list.Add(ChgCodeDes);
                //                ConnectSql_mb.cmdParameters Price = new ConnectSql_mb.cmdParameters("@Price", 0, SqlDbType.Decimal);
                //                list.Add(Price);
                //                ConnectSql_mb.cmdParameters LineType = new ConnectSql_mb.cmdParameters("@LineType", "", SqlDbType.NVarChar, 10);
                //                list.Add(LineType);


                //                LineType.value = "DP";
                //                Dictionary<string, decimal> d = new Dictionary<string, decimal>();
                //                d.Add("Trip", SafeValue.SafeDecimal(job["Incentive1"], 0));
                //                d.Add("OverTime", SafeValue.SafeDecimal(job["Incentive2"], 0));
                //                d.Add("Standby", SafeValue.SafeDecimal(job["Incentive3"], 0));
                //                //d.Add("PSA", SafeValue.SafeDecimal(job["Incentive4"], 0));
                //                cost_row2list_foreach(d, dt_incentive, list, ChgCode, ChgCodeDes, Price);

                //                LineType.value = "CL";
                //                d = new Dictionary<string, decimal>();
                //                d.Add("WEIGHING", SafeValue.SafeDecimal(job["charge2"], 0));
                //                cost_row2list_foreach(d, dt_claims, list, ChgCode, ChgCodeDes, Price);

                #endregion
            }
        }
        Common.WriteJsonP(status, context);
    }

    //=====================================Local
    [WebMethod]
    public void EGL_JobTrip_Cont_GetDataList_LOC(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        //if (job["role"].ToString().Equals("Driver"))
        //{
        //    sql_where += " and det2.DriverCode=@DriverCode";
        //}
        if (job["no"].ToString().Trim().Length > 0)
        {
            sql_where += "and ContainerNo like @ContainerNo";
        }
        string sql = string.Format(@"select top 100 job.Id as jobId,det1.Id,det1.JobNo,ContainerNo,ScheduleDate  
from ctm_jobdet1 as det1
left outer join ctm_job as job on det1.jobno=job.JobNo
where det1.StatusCode<>'Completed' and len(det1.ContainerNo)>4 and job.JobType='LOC' {0} 
order by det1.ScheduleDate desc,det1.ContainerNo,det1.JobNo", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.NVarChar, 20);
        list.Add(cpar);
        //cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.NVarChar, 20);
        //list.Add(cpar);
        //cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        //list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", "%" + job["no"] + "%", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void EGL_JobTrip_Cont_AddNewSave_LOC(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select * from ctm_jobdet1 where Id=@Det1Id");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", job["Det1Id"], SqlDbType.Int));
        DataTable dt_det1 = ConnectSql_mb.GetDataTable(sql, list);
        if (dt_det1.Rows.Count == 1)
        {
            sql = string.Format(@"select PickupFrom,DeliveryTo,YardRef,JobType from CTM_Job where JobNo='{0}'", SafeValue.SafeString(dt_det1.Rows[0]["JobNo"], ""));
            DataTable dt_job = ConnectSql_mb.GetDataTable(sql);
            sql = string.Format(@"select top 1 * from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} order by Id desc", SafeValue.SafeString(dt_det1.Rows[0]["JobNo"], ""), SafeValue.SafeInt(job["Det1Id"], 0));
            DataTable dt_det2 = ConnectSql_mb.GetDataTable(sql);
            string job_from = SafeValue.SafeString(dt_job.Rows[0]["PickupFrom"]);
            string job_to = SafeValue.SafeString(dt_job.Rows[0]["DeliveryTo"]);
            string job_Depot = SafeValue.SafeString(dt_job.Rows[0]["YardRef"]);
            string P_From = "";
            string P_From_Pl = "";
            string P_To = "";// DeliveryTo.Text;
            string trailer = "";
            string JobType = SafeValue.SafeString(dt_job.Rows[0]["JobType"]);
            string TripCode = "LOC";
            DateTime FromDate = DateTime.Now;
            string FromTime = DateTime.Now.ToString("HH:mm");

            if (dt_det2.Rows.Count > 0)
            {
                P_From = dt_det2.Rows[0]["ToCode"].ToString();
                P_From_Pl = dt_det2.Rows[0]["ToParkingLot"].ToString();
                trailer = dt_det2.Rows[0]["ChessisCode"].ToString();
            }
            //            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
            //TripCode,FromParkingLot,ToParkingLot,Remark1,Incentive1,Incentive2,Incentive3,charge2) 
            //values (@JobNo,@ContainerNo,@DriverCode,@TowheadCode,@ChessisCode,@FromCode,@FromDate,@FromTime,@ToCode,@ToDate,@ToTime,@Det1Id,@Statuscode,
            //@TripCode,@FromParkingLot,@ToParkingLot,@Remark1,@Incentive1,@Incentive2,@Incentive3,@charge2)");
            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
TripCode,FromParkingLot,ToParkingLot,Remark1) 
values (@JobNo,@ContainerNo,@DriverCode,@TowheadCode,@ChessisCode,@FromCode,@FromDate,@FromTime,@ToCode,@ToDate,@ToTime,@Det1Id,@Statuscode,
@TripCode,@FromParkingLot,@ToParkingLot,@Remark1)
select @@IDENTITY");
            list = new List<ConnectSql_mb.cmdParameters>();
            #region params
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt_det1.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", dt_det1.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", job["DriverCode"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@TowheadCode", job["TowheadCode"], SqlDbType.NVarChar, 100));

            list.Add(new ConnectSql_mb.cmdParameters("@FromCode", P_From, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@FromDate", FromDate, SqlDbType.DateTime));
            list.Add(new ConnectSql_mb.cmdParameters("@FromTime", FromTime, SqlDbType.NVarChar, 40));
            list.Add(new ConnectSql_mb.cmdParameters("@ToDate", FromDate, SqlDbType.DateTime));
            list.Add(new ConnectSql_mb.cmdParameters("@ToTime", FromTime, SqlDbType.NVarChar, 40));
            list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", job["Det1Id"], SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@Statuscode", "C", SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@TripCode", TripCode, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@FromParkingLot", P_From_Pl, SqlDbType.NVarChar, 100));

            cpar = new ConnectSql_mb.cmdParameters("@ChessisCode", SafeValue.SafeString(job["ChessisCode"]).ToUpper(), SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Remark1", job["Remark1"], SqlDbType.NVarChar, 300);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ToCode", job["ToCode"], SqlDbType.NVarChar, 300);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ToParkingLot", job["ToParkingLot"], SqlDbType.NVarChar, 100);
            list.Add(cpar);

            //cpar = new ConnectSql_mb.cmdParameters("@Incentive1", SafeValue.SafeDecimal(job["Incentive1"]), SqlDbType.Decimal);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@Incentive2", SafeValue.SafeDecimal(job["Incentive2"]), SqlDbType.Decimal);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@Incentive3", SafeValue.SafeDecimal(job["Incentive3"]), SqlDbType.Decimal);
            //list.Add(cpar);
            //cpar = new ConnectSql_mb.cmdParameters("@charge2", SafeValue.SafeDecimal(job["charge2"]), SqlDbType.Decimal);
            //list.Add(cpar);
            #endregion

            ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteScalar(sql, list);
            if (result.status)
            {
                status = "1";
                //EGL_JobTrip_AfterEndTrip("", SafeValue.SafeString(job["Det1Id"], "0"));

                #region

                C2.CtmJobDet2.tripStatusChanged(SafeValue.SafeInt(result.context, 0));

                Dictionary<string, decimal> d = new Dictionary<string, decimal>();
                d.Add("Trip", SafeValue.SafeDecimal(job["Incentive1"], 0));
                d.Add("OverTime", SafeValue.SafeDecimal(job["Incentive2"], 0));
                d.Add("Standby", SafeValue.SafeDecimal(job["Incentive3"], 0));
                //d.Add("PSA", SafeValue.SafeDecimal(job["PSA"], 0));
                C2.CtmJobDet2.Incentive_Save(SafeValue.SafeInt(result.context, 0), d);
                d = new Dictionary<string, decimal>();
                //d.Add("DHC", SafeValue.SafeDecimal(job["DHC"], 0));
                d.Add("WEIGHING", SafeValue.SafeDecimal(job["charge2"], 0));
                //d.Add("WASHING", SafeValue.SafeDecimal(job["WASHING"], 0));
                //d.Add("REPAIR", SafeValue.SafeDecimal(job["REPAIR"], 0));
                //d.Add("DETENTION", SafeValue.SafeDecimal(job["DETENTION"], 0));
                //d.Add("DEMURRAGE", SafeValue.SafeDecimal(job["DEMURRAGE"], 0));
                //d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(job["LIFT_ON_OFF"], 0));
                //d.Add("C_SHIPMENT", SafeValue.SafeDecimal(job["C_SHIPMENT"], 0));
                //d.Add("EMF", SafeValue.SafeDecimal(job["EMF"], 0));
                //d.Add("OTHER", SafeValue.SafeDecimal(job["OTHER"], 0));
                C2.CtmJobDet2.Claims_Save(SafeValue.SafeInt(result.context, 0), d);


                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                //lg.ActionLevel_isTRIP(SafeValue.SafeInt(result.context, 0));
                //lg.Remark = "Trip add new";
                lg.setActionLevel(SafeValue.SafeInt(result.context, 0), CtmJobEventLogRemark.Level.Trip, 1);
                lg.log();

                //                sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='INCENTIVE'");
                //                DataTable dt_incentive = ConnectSql_mb.GetDataTable(sql);
                //                sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='CLAIMS'");
                //                DataTable dt_claims = ConnectSql_mb.GetDataTable(sql);

                //                sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,det1.ContainerNo,det1.ContainerType
                //from ctm_jobdet2 as det2
                //left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
                //left outer join CTM_Job as job on det2.JobNo=job.JobNo
                //where det2.Id=@TripId");
                //                list = new List<ConnectSql_mb.cmdParameters>();
                //                list.Add(new ConnectSql_mb.cmdParameters("@TripId", SafeValue.SafeInt(result.context, 0), SqlDbType.Int));
                //                DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                //                list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
                //                list.Add(new ConnectSql_mb.cmdParameters("@JobType", dt.Rows[0]["JobType"], SqlDbType.NVarChar, 100));
                //                list.Add(new ConnectSql_mb.cmdParameters("@ContNo", dt.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
                //                list.Add(new ConnectSql_mb.cmdParameters("@ContType", dt.Rows[0]["ContainerType"], SqlDbType.NVarChar, 100));


                //                ConnectSql_mb.cmdParameters ChgCode = new ConnectSql_mb.cmdParameters("@ChgCode", "", SqlDbType.NVarChar, 100);
                //                list.Add(ChgCode);
                //                ConnectSql_mb.cmdParameters ChgCodeDes = new ConnectSql_mb.cmdParameters("@ChgCodeDes", "", SqlDbType.NVarChar, 100);
                //                list.Add(ChgCodeDes);
                //                ConnectSql_mb.cmdParameters Price = new ConnectSql_mb.cmdParameters("@Price", 0, SqlDbType.Decimal);
                //                list.Add(Price);
                //                ConnectSql_mb.cmdParameters LineType = new ConnectSql_mb.cmdParameters("@LineType", "", SqlDbType.NVarChar, 10);
                //                list.Add(LineType);

                //                LineType.value = "DP";
                //                Dictionary<string, decimal> d = new Dictionary<string, decimal>();
                //                d.Add("Trip", SafeValue.SafeDecimal(job["Incentive1"], 0));
                //                d.Add("OverTime", SafeValue.SafeDecimal(job["Incentive2"], 0));
                //                d.Add("Standby", SafeValue.SafeDecimal(job["Incentive3"], 0));
                //                //d.Add("PSA", SafeValue.SafeDecimal(job["Incentive4"], 0));
                //                cost_row2list_foreach(d, dt_incentive, list, ChgCode, ChgCodeDes, Price);

                //                LineType.value = "CL";
                //                d = new Dictionary<string, decimal>();
                //                d.Add("WEIGHING", SafeValue.SafeDecimal(job["charge2"], 0));
                //                cost_row2list_foreach(d, dt_claims, list, ChgCode, ChgCodeDes, Price);

                #endregion
            }
        }
        Common.WriteJsonP(status, context);
    }


    #endregion


    //    public void EGL_JobTrip_AfterEndTrip(string TripId)
    //    {
    //        string sql = string.Format(@"select Det1Id from ctm_jobdet2 where Id={0}", TripId);
    //        DataTable dt = ConnectSql_mb.GetDataTable(sql);
    //        if (dt.Rows.Count > 0)
    //        {
    //            string ContId = dt.Rows[0][0].ToString();
    //            EGL_JobTrip_AfterEndTrip(TripId, ContId);
    //        }
    //    }

    //    public void EGL_JobTrip_AfterEndTrip(string TripId, string ContId)
    //    {
    //        string sql = string.Format(@"with tb1 as (
    //select sum(charge1) as charge1,sum(charge2) as charge2,sum(charge3) as charge3,sum(charge4) as charge4,sum(charge5) as charge5,
    //sum(charge6) as charge6,sum(charge7) as charge7,sum(charge8) as charge8,sum(charge9) as charge9,sum(charge10) as charge10 
    //from ctm_jobdet2 where Det1Id={0} and Statuscode='C'
    //)
    //update ctm_jobdet1 set 
    //fee3=(select charge1 from tb1),
    //fee11=(select charge2 from tb1),
    //fee12=(select charge3 from tb1),
    //fee13=(select charge4 from tb1),
    //fee14=(select charge5 from tb1),
    //fee15=(select charge6 from tb1),
    //fee16=(select charge7 from tb1),
    //fee17=(select charge8 from tb1),
    //fee18=(select charge9 from tb1),
    //fee19=(select charge10 from tb1)
    //where Id={0}", ContId);
    //        ConnectSql_mb.ExecuteNonQuery(sql);
    //    }

    [WebMethod]
    public void EGL_JobTrip_TripTrailer_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        string sql = "";
        string P_From = SafeValue.SafeString(job["FromCode"]);
        string P_From_Pl = SafeValue.SafeString(job["FromParkingLot"]);
        string P_To = SafeValue.SafeString(job["ToCode"]);
        string P_To_Pl = SafeValue.SafeString(job["ToParkingLot"]);
        string trailer = SafeValue.SafeString(job["Trailer"]);
        string TripCode = SafeValue.SafeString(job["TripCode"]);
        DateTime FromDate = DateTime.Now;
        string FromTime = DateTime.Now.ToString("HH:mm");

        string CheckType = SafeValue.SafeString(job["CheckType"]);
        string CheckRemark = SafeValue.SafeString(job["CheckRemark"]);

        sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
TripCode,FromParkingLot,ToParkingLot,Remark1,CheckType,CheckRemark) 
values (@JobNo,@ContainerNo,@DriverCode,@TowheadCode,@ChessisCode,@FromCode,@FromDate,@FromTime,@ToCode,@ToDate,@ToTime,@Det1Id,@Statuscode,
@TripCode,@FromParkingLot,@ToParkingLot,@Remark1,@CheckType,@CheckRemark)
select @@Identity");
        list = new List<ConnectSql_mb.cmdParameters>();
        #region params
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", "", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", job["Driver"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@TowheadCode", job["VehicleNo"], SqlDbType.NVarChar, 100));
        cpar = new ConnectSql_mb.cmdParameters("@ChessisCode", trailer.ToUpper(), SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Remark1", job["Remark1"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", 0, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@Statuscode", "C", SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@TripCode", TripCode, SqlDbType.NVarChar, 100));

        list.Add(new ConnectSql_mb.cmdParameters("@FromCode", P_From, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@FromParkingLot", P_From_Pl, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", FromDate, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@FromTime", FromTime, SqlDbType.NVarChar, 40));

        list.Add(new ConnectSql_mb.cmdParameters("@ToCode", P_To, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@ToParkingLot", P_To_Pl, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToDate", FromDate, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@ToTime", FromTime, SqlDbType.NVarChar, 40));

        list.Add(new ConnectSql_mb.cmdParameters("@CheckType", CheckType, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@CheckRemark", CheckRemark, SqlDbType.NVarChar, 300));

        #endregion

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
            //lg.ActionLevel_isTRIP(SafeValue.SafeInt(res.context, 0));
            //lg.Remark = "Trip add new";
            lg.setActionLevel(SafeValue.SafeInt(res.context, 0), CtmJobEventLogRemark.Level.Trip, 1);
            lg.log();
        }
        else
        {
            context = Common.StringToJson(res.context);
        }

        Common.WriteJsonP(status, context);
    }





    [WebMethod]
    public void EGL_JobTrip_Calendar_GetDataList_ByOption(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string date = job["date"].ToString();
        string role = job["role"].ToString();
        string user = job["user"].ToString();
        string groupBy = job["groupBy"].ToString();
        string filterJobType = job["filterJobType"].ToString();
        string filterTeam = job["filterTeam"].ToString();
        string sorting = job["sorting"].ToString();


        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        if (role.Equals("Driver"))
        {
            sql_where = " and det2.DriverCode=@DriverCode";
        }
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,DriverCode,TowheadCode,ChessisCode,det2.Statuscode,det2.Remark, 
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det1.SealNo,det1.TTTime,det2.DoubleMounting,
case det2.Statuscode when 'S' then 1 when 'C' then 3 when 'X' then 4 else 2 end as OrderIndex
From CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where (DATEDIFF(day,BookingDate,@date)=0 ) {0}
order by OrderIndex,det2.BookingTime,det1.TTTime", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@date", date, SqlDbType.NVarChar, 20);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", user, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        context = string.Format(@"{0}list:{2}{1}", "{", "}", context);
        Common.WriteJsonP(status, context);
    }
}
