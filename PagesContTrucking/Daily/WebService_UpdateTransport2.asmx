<%@ WebService Language="C#" Class="WebService_UpdateTransport2" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Cells;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService_UpdateTransport2 : System.Web.Services.WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void List_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        DataTable dt = List_GetData_part1(info);
        string tripList = Common.DataTableToJson(dt);


        string context = string.Format("{0}\"tripList\":{2}{1}", "{", "}", tripList);
        Common.WriteJson(true, context);
    }

    [WebMethod]
    public void List_save2Excel()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        DataTable dt = List_GetData_part1(info);


        string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).Parent.FullName;
        string fileName = "" + DateTime.Now.Ticks;
        string to_file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "Tpt_" + fileName + ".csv");

        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(HttpContext.Current.Server.MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        //wb.Open(file);
        Worksheet ws = wb.Worksheets[0];
        int baseRow = 0;
        baseRow = 0;
        ws.Cells[baseRow++, 0].PutValue("Company:");
        ws.Cells[baseRow++, 0].PutValue("Address:");
        ws.Cells[baseRow++, 0].PutValue("TEL:");
        ws.Cells[baseRow++, 0].PutValue("FAX:");
        ws.Cells[baseRow++, 0].PutValue("ATTN:");
        baseRow = 0;
        ws.Cells[baseRow, 5].PutValue("Description");
        ws.Cells[baseRow++, 6].PutValue("Amount");
        ws.Cells[baseRow, 5].PutValue("Current Charges");
        ws.Cells[baseRow++, 6].PutValue(0);
        ws.Cells[baseRow, 5].PutValue("Total Amount Due");
        ws.Cells[baseRow++, 6].PutValue(0);
        ws.Cells[baseRow, 5].PutValue("Please Pay:");
        ws.Cells[baseRow++, 6].PutValue(0);
        baseRow = baseRow + 2;

        ws.Cells[baseRow, 0].PutValue("S/N");
        ws.Cells[baseRow, 1].PutValue("Date");
        ws.Cells[baseRow, 2].PutValue("From-To Date/Time");
        ws.Cells[baseRow, 3].PutValue("Con.No.");
        ws.Cells[baseRow, 4].PutValue("Description");
        ws.Cells[baseRow, 5].PutValue("Service");
        ws.Cells[baseRow, 6].PutValue("Amount");
        ws.Cells[baseRow, 7].PutValue("OT");
        ws.Cells[baseRow, 8].PutValue("Job Remark");
        ws.Cells[baseRow, 9].PutValue("From address");
        ws.Cells[baseRow, 10].PutValue("To address");
        baseRow++;
        int i = 0;
        for (; i < dt.Rows.Count;)
        {
            ws.Cells[baseRow + i, 0].PutValue(i + 1);
            ws.Cells[baseRow + i, 1].PutValue(SafeValue.SafeDate(dt.Rows[i]["ScheduleDate"], new DateTime(1753, 1, 1)).ToString("yyyy/MM/dd"));
            ws.Cells[baseRow + i, 2].PutValue(SafeValue.SafeDate(dt.Rows[i]["FromDate"], new DateTime(1753, 1, 1)).ToString("yyyy/MM/dd")+" "+SafeValue.SafeString(dt.Rows[i]["FromTime"])+ " - "+SafeValue.SafeDate(dt.Rows[i]["ToDate"], new DateTime(1753, 1, 1)).ToString("yyyy/MM/dd")+" "+SafeValue.SafeString(dt.Rows[i]["ToTime"]));
            ws.Cells[baseRow + i, 3].PutValue(dt.Rows[i]["ManualDo"]);
            ws.Cells[baseRow + i, 4].PutValue(dt.Rows[i]["BillingRemark"]);
            ws.Cells[baseRow + i, 5].PutValue(dt.Rows[i]["ServiceType"]);
            ws.Cells[baseRow + i, 6].PutValue(dt.Rows[i]["b_inc1"]);
            ws.Cells[baseRow + i, 7].PutValue(dt.Rows[i]["b_inc2"]);
            ws.Cells[baseRow + i, 8].PutValue(dt.Rows[i]["SpecialInstruction"]);
            ws.Cells[baseRow + i, 9].PutValue(dt.Rows[i]["PickupFrom"]);
            ws.Cells[baseRow + i, 10].PutValue(dt.Rows[i]["DeliveryTo"]);

            i++;
        }
        wb.Save(to_file);

        string context = Common.StringToJson(Path.Combine("files", "Excel_DailyTrips", "Tpt_" + fileName + ".csv"));
        Common.WriteJson(true, context);
    }


    public DataTable List_GetData_part1(JObject info)
    {
        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string JobNo = SafeValue.SafeString(info["JobNo"]);
        string JobType = SafeValue.SafeString(info["JobType"]);
        string DriverCode = SafeValue.SafeString(info["DriverCode"]);
        string Vessel = SafeValue.SafeString(info["Vessel"]);
        string Client = SafeValue.SafeString(info["Client"]);
        string JobStatus = SafeValue.SafeString(info["JobStatus"], "Operation");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", DriverCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));


        string sql = @"select job.Id,det2.Id as tripId,det2.JobNo,job.StatusCode as JobStatus,job.JobDate,job.ClientRefNo,det2.ClientRefNo as TripRefNo,det2.TripIndex,
job.PermitNo,job.Remark,job.SpecialInstruction,det2.ContainerNo,det2.RequestTrailerType as ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,job.EtdTime,
job.OperatorCode,job.CarrierBkgNo,job.IsTrucking,IsWarehouse,IsLocal,IsAdhoc,job.Escort_Ind,job.Escort_Remark,det2.RequestTrailerType,
job.Pol,job.Pod,job.Vessel,job.Voyage,det2.FromCode as PickupFrom,det2.ToCode as DeliveryTo,det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.FromDate as ScheduleDate,det2.StatusCode ,det2.WarehouseStatus,det2.DriverCode,det2.DriverCode2,det2.TowheadCode,det2.Direct_Inf,det2.SubCon_Ind,det2.SubCon_Code,
(select top 1 Name from XXParty where PartyId=job.ClientId) as client,
(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,det2.JobType,job.Contractor,job.ClientContact,det2.BookingDate,det2.BookingTime,det2.Self_Ind,det2.ChessisCode,
cast(det2.Id as nvarchar)+'|'+det2.TripCode+'|'+det2.Statuscode+',' as trips,
isnull( det2.Statuscode+':'+TripCode ,'') as str_trips,
det2.ServiceType,det2.ManualDo,det2.PermitNo as TripPermit,det2.Remark as TripDes,det2.Remark1 as DriverRemark,det2.BillingRemark,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustBill') as b_inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustOT') as b_inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustPermit') as b_inc3
from CTM_JobDet2 as det2 
left outer join CTM_Job as job on job.JobNo=det2.JobNo and job.JobStatus<>'Voided' and job.StatusCode='USE'";
        string sql_where = " @DateFrom<=det2.FromDate and @DateTo>det2.FromDate";

        if (JobStatus.Equals("Operation"))
        {
            sql_where += " and det2.StatusCode<>'C'";
        }
        else
        {
            sql_where += " and det2.StatusCode='C'";
        }
        if (JobNo.Length > 0)
        {
            //sql_where += " and (det2.JobNo like @JobNo or det2.ContainerNo like @JobNo or det2.PermitNo like @JobNo or det2.ManualDo like @JobNo or det2.PermitRemark like @JobNo or job.ClientRefNo like @JobNo or det2.ClientRefNo like @JobNo)";
            sql_where = " (det2.JobNo like @JobNo or det2.ContainerNo like @JobNo or det2.PermitNo like @JobNo or det2.ManualDo like @JobNo or det2.PermitRemark like @JobNo or job.ClientRefNo like @JobNo or det2.ClientRefNo like @JobNo)";
        }
        if (Client.Length > 0)
        {
            sql_where += " and job.ClientId=@Client";
        }
        if (Vessel.Length > 0)
        {
            sql_where += " and job.Vessel=@Vessel";
        }
        if (DriverCode.Length > 0)
        {
            sql_where += " and det2.DriverCode=@DriverCode";
        }
        if (JobType.Length > 0 && !JobType.Equals("ALL"))
        {
            sql_where += " and det2.JobType=@JobType";
        }
        else
        {
            sql_where += " and det2.JobType in ('TPT','WGR','WDO')";
        }
        sql = sql + " where " + sql_where + "  order by ScheduleDate asc, EtaDate,JobNo,Id,StatusCode desc";
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        return dt;
    }

    [WebMethod]
    public void View_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int TripId = SafeValue.SafeInt(info["TripId"], 0);
        string mast = "{}";

        string sql = string.Format(@"select det2.Id,det2.Det1Id,det2.ContainerNo,det2.DriverCode,det2.DriverCode2,det2.DriverCode11,det2.DriverCode12,
det2.ChessisCode,det2.Statuscode,
job.JobStatus,job.StatusCode as JobStatusCode,job.EmailAddress,det2.BillingRemark,
det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.JobNo,det2.TripCode,det2.Remark1,
det2.FromCode,det2.ToCode,det2.Remark,det2.TowheadCode,det2.FromParkingLot,det2.ToParkingLot,det2.SubCon_Ind,det2.SubCon_Code,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustBill') as inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustOT') as inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustPermit') as inc3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Trip' and (cc.DriverCode=det2.DriverCode or isnull(cc.DriverCode,'')='')) as d_inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime' and (cc.DriverCode=det2.DriverCode or isnull(cc.DriverCode,'')='')) as d_inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Standby' and (cc.DriverCode=det2.DriverCode or isnull(cc.DriverCode,'')='')) as d_inc3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Trip' and cc.DriverCode=det2.DriverCode2) as a_inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime' and cc.DriverCode=det2.DriverCode2) as a_inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Standby' and cc.DriverCode=det2.DriverCode2) as a_inc3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Trip' and cc.DriverCode=det2.DriverCode11) as d2_inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime' and cc.DriverCode=det2.DriverCode11) as d2_inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Standby' and cc.DriverCode=det2.DriverCode11) as d2_inc3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Trip' and cc.DriverCode=det2.DriverCode12) as a2_inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime' and cc.DriverCode=det2.DriverCode12) as a2_inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Standby' and cc.DriverCode=det2.DriverCode12) as a2_inc3
from CTM_JobDet2 as det2 
left outer join CTM_Job as job on det2.JobNo=job.JobNo 
where det2.Id=@TripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@TripId", TripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        mast = Common.DataRowToJson(dt, true);

        string context = string.Format("{0}\"mast\":{2}{1}", "{", "}", mast);
        Common.WriteJson(true, context);
        #region

        #endregion

    }


    [WebMethod]
    public void saveTrip()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int tripId = SafeValue.SafeInt(info["Id"], 0);
        string ContainerNo = SafeValue.SafeString(info["ContainerNo"]);
        string DriverCode = SafeValue.SafeString(info["DriverCode"]);
        string DriverCode2 = SafeValue.SafeString(info["DriverCode2"]);
        string DriverCode11 = SafeValue.SafeString(info["DriverCode11"]);
        string DriverCode12 = SafeValue.SafeString(info["DriverCode12"]);
        string ChessisCode = SafeValue.SafeString(info["ChessisCode"]);
        string Statuscode = SafeValue.SafeString(info["Statuscode"]);
        string FromDate = SafeValue.SafeString(info["FromDate1"]);
        string FromTime = SafeValue.SafeString(info["FromTime"]);
        string ToDate = SafeValue.SafeString(info["ToDate1"]);
        string ToTime = SafeValue.SafeString(info["ToTime"]);
        string FromCode = SafeValue.SafeString(info["FromCode"]);
        string ToCode = SafeValue.SafeString(info["ToCode"]);
        string Remark = SafeValue.SafeString(info["Remark"]);
        string BillLock = SafeValue.SafeString(info["BillLock"]);
        string TowheadCode = SafeValue.SafeString(info["TowheadCode"]);
        string FromParkingLot = SafeValue.SafeString(info["FromParkingLot"]);
        string ToParkingLot = SafeValue.SafeString(info["ToParkingLot"]);

        string SubCon_Ind = SafeValue.SafeString(info["SubCon_Ind"]);
        string SubCon_Code = SafeValue.SafeString(info["SubCon_Code"]);
        string BillingRemark = SafeValue.SafeString(info["BillingRemark"]);
        string Remark1 = SafeValue.SafeString(info["Remark1"]);


        FromTime = SafeValue_mb.convertTimeFormat(FromTime);
        ToTime = SafeValue_mb.convertTimeFormat(ToTime);

        DateTime dt_fromdate = DateTime.ParseExact(FromDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        DateTime dt_todate = DateTime.ParseExact(ToDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

        C2.CtmJobDet2Biz det2BZ = new C2.CtmJobDet2Biz(tripId);
        C2.CtmJobDet2 det2 = det2BZ.getData();
        //C2.CtmJobDet2 det2 = C2.Manager.ORManager.GetObject<C2.CtmJobDet2>(tripId);
        bool status = false;
        string note = "";
        if (det2 != null)
        {
            det2.ContainerNo = ContainerNo;
            det2.DriverCode = DriverCode;
            det2.DriverCode2 = DriverCode2;
            det2.DriverCode11 = DriverCode11;
            det2.DriverCode12 = DriverCode12;
            if (det2.DriverCode != null && det2.DriverCode.Length > 0)
            {
                det2.DriverCode3 = det2.DriverCode;
            }

            det2.TowheadCode = TowheadCode;
            det2.ChessisCode = ChessisCode;
            det2.Statuscode = Statuscode;
            det2.FromCode = FromCode;
            det2.ToCode = ToCode;
            det2.Remark = Remark;
            det2.FromDate = dt_fromdate;
            det2.FromTime = FromTime;
            det2.ToDate = dt_todate;
            det2.ToTime = ToTime;
            det2.BookingDate = dt_fromdate;
            det2.BookingTime = FromTime;
            //det2.BillLock = BillLock;
            det2.FromParkingLot = FromParkingLot;
            det2.ToParkingLot = ToParkingLot;
            det2.SubCon_Ind = SubCon_Ind;
            det2.SubCon_Code = SubCon_Code;
            det2.BillingRemark = BillingRemark;
            det2.Remark1 = Remark1;

            C2.BizResult result = det2BZ.update(HttpContext.Current.User.Identity.Name);

            if (result.status)
            {
                decimal inc1 = SafeValue.SafeDecimal(info["inc1"]);
                decimal inc2 = SafeValue.SafeDecimal(info["inc2"]);
                decimal inc3 = SafeValue.SafeDecimal(info["inc3"]);
                //decimal inc4 = SafeValue.SafeDecimal(info["inc4"]);
                Dictionary<string, decimal> d = new Dictionary<string, decimal>();
                d.Add("CustBill", inc1);
                d.Add("CustOT", inc2);
                d.Add("CustPermit", inc3);
                //d.Add("PSA", inc4);
                C2.CtmJobDet2.Billing_Save(tripId, d);

                inc1 = SafeValue.SafeDecimal(info["d_inc1"]);
                inc2 = SafeValue.SafeDecimal(info["d_inc2"]);
                inc3 = SafeValue.SafeDecimal(info["d_inc3"]);
                d = new Dictionary<string, decimal>();
                d.Add("Trip", inc1);
                d.Add("OverTime", inc2);
                d.Add("Standby", inc3);
                C2.CtmJobDet2.Incentive_Save(tripId, d);

                inc1 = SafeValue.SafeDecimal(info["a_inc1"]);
                inc2 = SafeValue.SafeDecimal(info["a_inc2"]);
                inc3 = SafeValue.SafeDecimal(info["a_inc3"]);
                d = new Dictionary<string, decimal>();
                d.Add("Trip", inc1);
                d.Add("OverTime", inc2);
                d.Add("Standby", inc3);
                C2.CtmJobDet2.Incentive_Save_ByDriver(tripId, d, det2.DriverCode2);


                inc1 = SafeValue.SafeDecimal(info["d2_inc1"]);
                inc2 = SafeValue.SafeDecimal(info["d2_inc2"]);
                inc3 = SafeValue.SafeDecimal(info["d2_inc3"]);
                d = new Dictionary<string, decimal>();
                d.Add("Trip", inc1);
                d.Add("OverTime", inc2);
                d.Add("Standby", inc3);
                C2.CtmJobDet2.Incentive_Save_ByDriver(tripId, d, det2.DriverCode11);


                inc1 = SafeValue.SafeDecimal(info["a2_inc1"]);
                inc2 = SafeValue.SafeDecimal(info["a2_inc2"]);
                inc3 = SafeValue.SafeDecimal(info["a2_inc3"]);
                d = new Dictionary<string, decimal>();
                d.Add("Trip", inc1);
                d.Add("OverTime", inc2);
                d.Add("Standby", inc3);
                C2.CtmJobDet2.Incentive_Save_ByDriver(tripId, d, det2.DriverCode12);


                status = true;
            }
            else
            {
                note = result.context;
            }

        }
        Common.WriteJson(status, "{\"tripId\":" + tripId + ",\"ContainerNo\":\"" + ContainerNo + "\",\"Note\":\"" + note + "\"}");


    }


    [WebMethod]
    public void readyBilling()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        string JobNo = SafeValue.SafeString(info["JobNo"]);
        int tripId = SafeValue.SafeInt(info["Id"], 0);

        string sql = "update CTM_Job set StatusCode=(case when StatusCode='CLS' then 'USE' else 'CLS' end),JobStatus=(case when JobStatus='Billing' then 'Confirmed' else 'Billing' end) where JobNo='" + JobNo + "'";
        bool status = false;
        if (ConnectSql.ExecuteSql(sql) > 0)
        {
            status = true;
            string userId = HttpContext.Current.User.Identity.Name;
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;
            elog.JobNo = JobNo;
            elog.ActionLevel = "JOB";
            elog.setActionLevel(0, CtmJobEventLogRemark.Level.Job, 7);
            elog.log();
        }
        Common.WriteJson(status, Common.StringToJson(""));
    }


    [WebMethod]
    public void PlannerTPTList_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        string From = info["From"].ToString();
        string No = SafeValue.SafeString(info["ContNo"]);
        string planDate = info["planDate"].ToString();
        //string Client = SafeValue.SafeString(info["Client"]);
        //string Vessel = SafeValue.SafeString(info["Vessel"]);

        string sql_where = "";
        if (No.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det2.ContainerNo like @No)";
        }
        string sql = string.Format(@"select 
det2.Id as tripId,det2.Det1Id as contId,job.Id as jobId,job.JobNo,
det2.FromDate,isnull(det2.FromTime,'00:00') as FromTime,det2.TripIndex1,
det2.TowheadCode,det2.ChessisCode,det2.DriverCode,det2.DriverCode2,isnull(det2.DriverCode3,'') as DriverCode3,det2.TripCode,det2.Statuscode,
job.ClientId,det2.ContainerNo,det2.FromCode,det2.ToCode,det2.RequestTrailerType as ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,job.Remark as JobRemark,
job.CarrierId,job.PermitNo,det2.ClientRefNo,
job.WarehouseAddress as shipper,convert(nvarchar,job.EtdDate,103) as EtdDate,job.EtdTime as EtdTime,job.ClientContact as orderBy,job.PortnetRef,job.ReturnLastDate,
(select top 1 Tel from ref_contact as contact where contact.Name=job.ClientContact and contact.PartyId=job.partyId) as orderByTel,det2.SubCon_Code
from ctm_jobdet2 as det2 
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,det2.FromDate,@FromDate)>=0 and 
det2.StatusCode<>'C' and job.IsLocal='Yes' and isnull(det2.DriverCode,'')='' and isnull(det2.DriverCode3,'')='' and (det2.TripCode in ('TPT','WGR','WDO')) and isnull(det2.SubCon_Ind,'N')<>'Y' and job.StatusCode <>'CNL' {0} 
order by TripCode,FromDate,TripIndex1", sql_where);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string dataList = Common.DataTableToJson(dt);

        //========== right part
        sql = string.Format(@"select 
det2.Id as tripId,det2.Det1Id as contId,job.Id as jobId,job.JobNo,
det2.FromDate,isnull(det2.FromTime,'00:00') as FromTime,det2.TripIndex1,
det2.TowheadCode,det2.ChessisCode,det2.DriverCode,det2.DriverCode2,isnull(det2.DriverCode3,'') as DriverCode3,det2.TripCode,det2.Statuscode,
job.ClientId,det2.ContainerNo,det2.FromCode,det2.ToCode,det2.RequestTrailerType as ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,job.Remark as JobRemark,
job.CarrierId,job.PermitNo,det2.ClientRefNo,
job.WarehouseAddress as shipper,convert(nvarchar,job.EtdDate,103) as EtdDate,job.EtdTime as EtdTime,job.ClientContact as orderBy,job.PortnetRef,job.ReturnLastDate,
(select top 1 Tel from ref_contact as contact where contact.Name=job.ClientContact and contact.PartyId=job.partyId) as orderByTel,det2.SubCon_Code
from ctm_jobdet2 as det2 
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,det2.FromDate,@FromDate)=0 and job.IsLocal='Yes' and (isnull(det2.DriverCode,'')<>'' or isnull(det2.DriverCode3,'')<>'') and (det2.TripCode in ('TPT','WGR','WDO')) and isnull(det2.SubCon_Ind,'N')<>'Y' and job.StatusCode <>'CNL' {0} 
order by TripCode,FromDate,TripIndex1", sql_where);
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", planDate, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string rightList = Common.DataTableToJson(dt);

        sql = string.Format(@"select Id,Code,TowheaderCode from ctm_driver where StatusCode='Active' order by Code");
        dt = ConnectSql_mb.GetDataTable(sql);
        string driverList = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"dataList\":{2},\"driverList\":{3},\"rightList\":{4}{1}", "{", "}", dataList, driverList,rightList);
        Common.WriteJson(true, context);
    }

    [WebMethod]
    public void PlanTrips_ByIndex()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Ids = SafeValue.SafeString(info["Ids"]);
        JArray Id_list = (JArray)JsonConvert.DeserializeObject(Ids);
        string driver = SafeValue.SafeString(info["driver"]);
        string planDate = SafeValue.SafeString(info["planDate"],DateTime.Now.ToString("yyyyMMdd"));

        string sql = string.Format(@"update ctm_jobdet2 set DriverCode3=@DriverCode,FromDate=@planDate,
TripIndex1=isnull((select max(oo.TripIndex1)
from ctm_jobdet2 as oo 
where datediff(d,oo.FromDate,(select top 1 FromDate from ctm_jobdet2 where Id=@tripId))=0 and DriverCode3=@DriverCode),0)+1 
where Id=@tripId");
        List<ConnectSql_mb.cmdParameters> list = null;
        for (int i = 0; i < Id_list.Count; i++)
        {
            int tripId = SafeValue.SafeInt(Id_list[i]["id"], 0);
            if (tripId > 0)
            {
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", driver, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@planDate", planDate, SqlDbType.NVarChar, 8));
                ConnectSql_mb.ExecuteNonQuery(sql, list);
                    
                    C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                    elog.Platform_isWeb();
                    elog.Controller = HttpContext.Current.User.Identity.Name;
                    elog.setActionLevel(tripId, CtmJobEventLogRemark.Level.Trip, -1,"Plan trip to "+driver+" ["+planDate+"]");
                    elog.log();
            }
        }

        Common.WriteJson(true, Common.StringToJson(""));
    }




    [WebMethod]
    public void PlanTrip_Exchange2Up()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int fromId = SafeValue.SafeInt(info["fromId"], 0);
        int toId = SafeValue.SafeInt(info["toId"], 0);
        int fromIndex = SafeValue.SafeInt(info["fromIndex"], 0);
        string sql = string.Format(@"update ctm_jobdet2 set TripIndex1=@TripIndex1 where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", fromId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@TripIndex1", fromIndex - 1, SqlDbType.Int));
        ConnectSql_mb.ExecuteNonQuery(sql, list);
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", toId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@TripIndex1", fromIndex, SqlDbType.Int));
        ConnectSql_mb.ExecuteNonQuery(sql, list);
        Common.WriteJson(true, Common.StringToJson(""));
    }

    [WebMethod]
    public void PlanTrip_Exchange2Down()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int fromId = SafeValue.SafeInt(info["fromId"], 0);
        int toId = SafeValue.SafeInt(info["toId"], 0);
        int fromIndex = SafeValue.SafeInt(info["fromIndex"], 0);
        string sql = string.Format(@"update ctm_jobdet2 set TripIndex1=@TripIndex1 where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", fromId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@TripIndex1", fromIndex + 1, SqlDbType.Int));
        ConnectSql_mb.ExecuteNonQuery(sql, list);
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", toId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@TripIndex1", fromIndex, SqlDbType.Int));
        ConnectSql_mb.ExecuteNonQuery(sql, list);
        Common.WriteJson(true, Common.StringToJson(""));
    }


    [WebMethod]
    public void Plan_AssignTrip()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int tripId = SafeValue.SafeInt(info["tripId"], 0);
        Common.WriteJson(Plan_AssignTrip(tripId), Common.StringToJson(""));
    }

    public bool Plan_AssignTrip(int tripId)
    {
        string sql = string.Format(@"update ctm_jobdet2 set DriverCode=DriverCode3, 
TowHeadCode=IsNull((select top 1 towheadercode from ctm_driver where code=DriverCode3),'')
where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", tripId, SqlDbType.Int));
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);
        return result.status; ;
    }

    [WebMethod]
    public void Plan_AssignALLTrip()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        JArray Ids = (JArray)JsonConvert.DeserializeObject(SafeValue.SafeString(info["Ids"]));
        //int tripId = SafeValue.SafeInt(info["tripId"], 0);
        for (int i = 0; i < Ids.Count; i++)
        {
            int tripId = SafeValue.SafeInt(Ids[i]["id"], 0);
            if (tripId > 0)
            {
                Plan_AssignTrip(tripId);
            }
        }
        Common.WriteJson(true, Common.StringToJson(""));
    }









    [WebMethod]
    public void MasterData_Client()
    {
        string sql = string.Format(@"select PartyId as c,Name as n from XXParty where IsCustomer=1 order by PartyId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }

    [WebMethod]
    public void MasterData_Vendor()
    {
        string sql = string.Format(@"select PartyId as c,Name as n from XXParty where IsVendor=1 order by PartyId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }

    [WebMethod]
    public void MasterData_Driver()
    {
        //        string sql = string.Format(@"select Driver as c,Towhead as n from CTM_DriverLog 
        //where IsActive='Y' and datediff(day,Date,getdate())=0");
        string sql = string.Format(@"select Code as c,TowheaderCode as n from ctm_driver where StatusCode='Active' order by c");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }

    [WebMethod]
    public void MasterData_Towhead()
    {
        string sql = string.Format(@"select Id,VehicleCode as c,VehicleType as n
from ref_Vehicle where VehicleStatus='Active' and VehicleType='Towhead'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }

    [WebMethod]
    public void MasterData_Trailer()
    {
        //string sql = string.Format(@"select Id,VehicleCode as c,VehicleType as n from ref_Vehicle where VehicleStatus='Active' and VehicleType='Trailer'");
        string sql = string.Format(@"SELECT Id,Code as c,Remark as n from CTM_MastData where Type='chessis' and isnull(Type1,'')<>'InActive'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }
    [WebMethod]
    public void MasterData_ParkingLot()
    {
        string sql = string.Format(@"select Id,Code as c,Address as n from PackingLot");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }



    [WebMethod]
    public void email_send()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        //int tripId = SafeValue.SafeInt(info["tripId"],0);
        string emailTo = SafeValue.SafeString(info["emailTo"]);
        string emailCc = SafeValue.SafeString(info["emailCc"]);
        string emailSubject = SafeValue.SafeString(info["emailSubject"]);
        string emailContent = SafeValue.SafeString(info["emailContent"]);
        string JobNo = SafeValue.SafeString(info["JobNo"]);
        emailContent = emailContent.Replace("\r\n", "<br/>");

        Helper.Email.SendEmail(emailTo, emailCc, "", emailSubject, emailContent, "");
        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Controller = HttpContext.Current.User.Identity.Name;
        lg.JobNo = JobNo;
        lg.Remark = "Sent e-mail:" + emailCc + ", Cc:" + emailCc;
        lg.log();
        Common.WriteJson(true, Common.StringToJson(""));
    }
}