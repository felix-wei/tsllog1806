<%@ WebService Language="C#" Class="WebService_UpdateTransport" %>


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
public class WebService_UpdateTransport : System.Web.Services.WebService
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

        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string JobNo = SafeValue.SafeString(info["JobNo"]);
        string JobType = SafeValue.SafeString(info["JobType"]);
        string DriverCode = SafeValue.SafeString(info["DriverCode"]);
        string Vessel = SafeValue.SafeString(info["Vessel"]);
        string Client = SafeValue.SafeString(info["Client"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", DriverCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));


        string sql = @"select job.Id,det2.Id as tripId,det2.JobNo,job.StatusCode as JobStatus,job.JobDate,job.ClientRefNo,det2.ClientRefNo as TripRefNo,det2.TripIndex,
job.PermitNo,job.Remark,job.SpecialInstruction,det2.ContainerNo,det2.RequestTrailerType as ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,
job.OperatorCode,job.CarrierBkgNo,job.IsTrucking,IsWarehouse,IsLocal,IsAdhoc,job.Escort_Ind,job.Escort_Remark,det2.RequestTrailerType,
job.Pol,job.Pod,job.Vessel,job.Voyage,det2.FromCode as PickupFrom,det2.ToCode as DeliveryTo,det2.FromDate,det2.FromTime,det2.FromDate as ScheduleDate,det2.StatusCode ,det2.WarehouseStatus,det2.DriverCode,det2.TowheadCode,det2.Direct_Inf,det2.SubCon_Ind,det2.SubCon_Code,
(select top 1 Name from XXParty where PartyId=job.ClientId) as client,
(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,det2.JobType,job.Contractor,job.ClientContact,det2.BookingDate,det2.BookingTime,det2.Self_Ind,det2.ChessisCode,
cast(det2.Id as nvarchar)+'|'+det2.TripCode+'|'+det2.Statuscode+',' as trips,
isnull( det2.Statuscode+':'+TripCode ,'') as str_trips,
det2.ServiceType,det2.ManualDo,det2.PermitNo as TripPermit,det2.Remark as TripDes,det2.Remark1 as DriverRemark,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as inc2
from CTM_JobDet2 as det2 
left outer join CTM_Job as job on job.JobNo=det2.JobNo and job.JobStatus<>'Voided' and job.StatusCode='USE'";
        string sql_where = " @DateFrom<=det2.FromDate and @DateTo>det2.FromDate";

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
        string tripList = Common.DataTableToJson(dt);


        string context = string.Format("{0}\"tripList\":{2}{1}", "{", "}", tripList);
        Common.WriteJson(true, context);
    }

    [WebMethod]
    public void View_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int TripId = SafeValue.SafeInt(info["TripId"], 0);
        string mast = "{}";

        string sql = string.Format(@"select det2.Id,det2.Det1Id,det2.ContainerNo,det2.DriverCode,det2.DriverCode2,det2.ChessisCode,det2.Statuscode,
job.JobStatus,job.StatusCode as JobStatusCode,job.EmailAddress,
det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.JobNo,det2.TripCode,
det2.FromCode,det2.ToCode,det2.Remark,det2.TowheadCode,det2.FromParkingLot,det2.ToParkingLot,det2.SubCon_Ind,det2.SubCon_Code,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Standby') as inc3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='PSA') as inc4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as c1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as c2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as c3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as c4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as c5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as c6,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as c7,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as c8,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as c9,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as c10
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

            C2.BizResult result = det2BZ.update(HttpContext.Current.User.Identity.Name);

            if (result.status)
            {
                decimal inc1 = SafeValue.SafeDecimal(info["inc1"]);
                decimal inc2 = SafeValue.SafeDecimal(info["inc2"]);
                decimal inc3 = SafeValue.SafeDecimal(info["inc3"]);
                decimal inc4 = SafeValue.SafeDecimal(info["inc4"]);
                decimal c1 = SafeValue.SafeDecimal(info["c1"]);
                decimal c2 = SafeValue.SafeDecimal(info["c2"]);
                decimal c3 = SafeValue.SafeDecimal(info["c3"]);
                decimal c4 = SafeValue.SafeDecimal(info["c4"]);
                decimal c5 = SafeValue.SafeDecimal(info["c5"]);
                decimal c6 = SafeValue.SafeDecimal(info["c6"]);
                decimal c7 = SafeValue.SafeDecimal(info["c7"]);
                decimal c8 = SafeValue.SafeDecimal(info["c8"]);
                decimal c9 = SafeValue.SafeDecimal(info["c9"]);
                decimal c10 = SafeValue.SafeDecimal(info["c10"]);
                Dictionary<string, decimal> d = new Dictionary<string, decimal>();
                d.Add("Trip", inc1);
                d.Add("OverTime", inc2);
                d.Add("Standby", inc3);
                d.Add("PSA", inc4);
                C2.CtmJobDet2.Incentive_Save(tripId, d);
                d = new Dictionary<string, decimal>();
                d.Add("DHC", c1);
                d.Add("WEIGHING", c2);
                d.Add("WASHING", c3);
                d.Add("REPAIR", c4);
                d.Add("DETENTION", c5);
                d.Add("DEMURRAGE", c6);
                d.Add("LIFT_ON_OFF", c7);
                d.Add("C_SHIPMENT", c8);
                d.Add("EMF", c9);
                d.Add("OTHER", c10);
                C2.CtmJobDet2.Claims_Save(tripId, d);

                status = true;
            }else
            {
                note = result.context;
            }

        }
        Common.WriteJson(status, "{\"tripId\":" + tripId + ",\"ContainerNo\":\"" + ContainerNo + "\",\"Note\":\"" + note + "\"}");


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

        Helper.Email.SendEmail(emailTo, emailCc, "", emailSubject, emailContent, "");
        Common.WriteJson(true, Common.StringToJson(""));
    }
}