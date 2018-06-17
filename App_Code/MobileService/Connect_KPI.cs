using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_KPI 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect_KPI : System.Web.Services.WebService
{

    public Connect_KPI()
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
    public void CMS_KPI_GetTripReport(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = null;

        string Driver = SafeValue.SafeString(job["driver"]);
        string DateFrom = SafeValue.SafeString(job["from"]);
        string DateTo = SafeValue.SafeString(job["to"]);
        if (DateFrom.Equals("") || DateTo.Equals(""))
        {
            DateFrom = DateTime.Now.ToString("yyyyMMdd");
            DateTo = DateTime.Now.ToString("yyyyMMdd");
        }
        string sql_where = "";
        string sql_where2 = "";
        if (!Driver.Equals(""))
        {
            sql_where = "and DriverCode=@DriverCode";
            sql_where2 = "and DriverCode2=@DriverCode";
        }
        string sql = string.Format(@"select DriverCode,Count(Id) as Trips,sum(Incentive) as Incentive,sum(Claim) as Claim 
from (
select Id,DriverCode,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='DP' and c.DriverCode=t.DriverCode),0) as Incentive,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='CL'),0) as Claim 
from CTM_JobDet2 as t 
where Statuscode='C' and datediff(day,FromDate,@DateFrom)<=0 and datediff(day,FromDate,@DateTo)>=0 {0}
union all
select Id,DriverCode2 as DriverCode,
isnull((select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='DP' and c.DriverCode=t.DriverCode2),0) as Incentive,
0 as Claim 
from CTM_JobDet2 as t 
where Statuscode='C' and datediff(day,FromDate,@DateFrom)<=0 and datediff(day,FromDate,@DateTo)>=0 {1}
) as temp 
where DriverCode<>''
group by DriverCode
order by DriverCode	", sql_where, sql_where2);
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", Driver, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", DateFrom, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", DateTo, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }


    [WebMethod]
    public void CMS_KPI_GetTrips_ByDriver(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = null;

        string Driver = SafeValue.SafeString(job["driver"]);
        string DateFrom = SafeValue.SafeString(job["from"]);
        string DateTo = SafeValue.SafeString(job["to"]);
        if (DateFrom.Equals("") || DateTo.Equals(""))
        {
            DateFrom = DateTime.Now.ToString("yyyyMMdd");
            DateTo = DateTime.Now.ToString("yyyyMMdd");
        }

        //select det2.Id,det2.JobNo,det2.ContainerNo,DriverCode,TowheadCode,ChessisCode,det2.Statuscode,det2.Remark, 
        //FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det1.SealNo,det1.TTTime,det2.DoubleMounting,
//        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.Statuscode,
//FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det1.SealNo,det1.TTTime,
//(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
//(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim 
//From CTM_JobDet2 as det2
//left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
//where det2.Statuscode='C' and datediff(day,ToDate,@DateFrom)<=0 and datediff(day,ToDate,@DateTo)>=0 and DriverCode=@DriverCode
//order by ToDate");
        string sql=string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.Statuscode,
FromCode, FromDate, FromTime, ToCode, ToDate, ToTime, TripCode as JobType, det1.SealNo, det1.TTTime,
(select isnull(sum(isnull(Qty * Price, 0)), 0) from job_cost as c where TripNo = det2.Id and LineType = 'DP' and c.DriverCode = det2.DriverCode) as Incentive,
(select isnull(sum(isnull(Qty * Price, 0)), 0) from job_cost as c where TripNo = det2.Id and LineType = 'CL') as Claim
From CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id = det1.Id
where det2.Statuscode = 'C' and datediff(day, FromDate, @DateFrom)<= 0 and datediff(day, FromDate, @DateTo) >= 0 and DriverCode = @DriverCode
  union all
  select det2.Id, det2.JobNo, det2.ContainerNo, det2.Statuscode,
  FromCode, FromDate, FromTime, ToCode, ToDate, ToTime, TripCode as JobType, det1.SealNo, det1.TTTime,
  (select isnull(sum(isnull(Qty * Price, 0)), 0) from job_cost as c where TripNo = det2.Id and LineType = 'DP' and c.DriverCode = det2.DriverCode2) as Incentive,
0 as Claim
From CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id = det1.Id
where det2.Statuscode = 'C' and datediff(day, FromDate, @DateFrom)<= 0 and datediff(day, FromDate, @DateTo) >= 0 and DriverCode2 = @DriverCode
  order by FromDate");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", Driver, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", DateFrom, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", DateTo, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }



    [WebMethod]
    public void CMS_KPI_GetTEUReport(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = null;

        //string Driver = SafeValue.SafeString(job["driver"]);
        string DateFrom = SafeValue.SafeString(job["from"]);
        string DateTo = SafeValue.SafeString(job["to"]);
        if (DateFrom.Equals("") || DateTo.Equals(""))
        {
            DateFrom = DateTime.Now.ToString("yyyyMMdd");
            DateTo = DateTime.Now.ToString("yyyyMMdd");
        }
        string sql = string.Format(@"
with tb_today as(
select *,datediff(day,day0,day1)+1 as ds from (select @DateFrom as day0,@DateTo day1) as temp
),
tb_days as(
select top 360 ROW_NUMBER()over(order by Id) as Id from sysobjects
),
tb_days1 as (
select Id,DATEADD(day,Id-1,day0) as dd from tb_days
left outer join tb_today on 1=1
 where ds>=Id
),
tb_days2 as (
select Id,convert(varchar,dd,112) as dd from tb_days1
),
tb_trips as (
select det2.Id,convert(varchar,ToDate,112) as FromDate,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim
from CTM_JobDet2 as det2
left outer join tb_today on 1=1
where det2.Statuscode='C' and datediff(day,tb_today.day0,ToDate)>=0 and datediff(day,tb_today.day1,ToDate)<=0
),
tb1 as (
select FromDate,sum(Incentive) as Incentive,sum(Claim) as Claim,count(Id) as Trips
from tb_trips group by FromDate
),
tb_inv as (
select convert(varchar,docdate,112) as docdate,locamt from xaarinvoice
left outer join tb_today on 1=1 
where datediff(day,tb_today.day0,docdate)>=0 and datediff(day,tb_today.day1,docdate)<=0 and xaarinvoice.exportind='Y'
),
tb2 as(
select sum(locamt) as amt,docdate from tb_inv group by docdate
),
tb_cont as(
select convert(varchar,job.EtaDate,112) as eta,det1.ContainerType,isnull(substring(det1.ContainerType,1,1),'') as FCContType,(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'OTS' end) as JobType
from CTM_JobDet1 as det1
left outer join CTM_Job as job on job.jobno=det1.jobno
left outer join tb_today on 1=1 
where datediff(day,tb_today.day0,job.EtaDate)>=0 and datediff(day,tb_today.day1,job.EtaDate)<=0 and det1.StatusCode='Completed'
),
tb3 as (
select eta,
sum(case when JobType='IMP' and FCContType='2' then 1 else 0 end) as i20,
sum(case when JobType='IMP' and FCContType='4' then 1 else 0 end) as i40,
sum(case when JobType='EXP' and FCContType='2' then 1 else 0 end) as e20,
sum(case when JobType='EXP' and FCContType='4' then 1 else 0 end) as e40,
sum(case when JobType='OTS' and FCContType='2' then 1 else 0 end) as o20,
sum(case when JobType='OTS' and FCContType='4' then 1 else 0 end) as o40,
sum(case when FCContType='2' or FCContType='4' then 0 else 1 end) as oe
from tb_cont group by eta
),
tb_psaRB as (
select convert(varchar,[BILL DATE] ,112) as billdate,isnull(amount,0) as amount 
from psa_bill 
left outer join tb_today on 1=1 
where datediff(day,tb_today.day0,[BILL DATE] )>=0 and datediff(day,tb_today.day1,[BILL DATE] )<=0 and amount < 0
),
tb4 as (
select sum(amount) as amt,billdate from tb_psaRB group by billdate
)
select dd,isnull(tb2.amt,0) as inv,isnull(tb4.amt,0) as psaRB,
isnull(tb1.Incentive,0) as Incentive,isnull(tb1.Claim,0) as Claim, isnull(tb1.Trips,0) as Trips,
isnull(tb3.i20,0) as i20,isnull(tb3.i40,0) as i40,isnull(tb3.e20,0) as e20,isnull(tb3.e40,0) as e40,
isnull(tb3.o20,0) as o20,isnull(tb3.o40,0) as o40,isnull(tb3.oe,0) as oe
from tb_days2
left outer join tb1 on dd=tb1.FromDate
left outer join tb2 on dd=tb2.docdate
left outer join tb3 on dd=tb3.eta
left outer join tb4 on dd=tb4.billdate
order by dd");
        list = new List<ConnectSql_mb.cmdParameters>();
        //list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", Driver, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", DateFrom, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", DateTo, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }


    [WebMethod]
    public void CMS_KIP_RATECT_GetMasterData(string info)
    {
        string status = "1";
        string context = "";

        string sql = string.Format(@"select ChgcodeId as c,ChgcodeDes as d from xxchgcode
union all 
select '',''
order by ChgcodeId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string ChgCode = Common.DataTableToJson(dt);

        sql = string.Format(@"select PartyId as c from xxparty where IsCustomer='true' and PartyId<>''
union all 
select ''
order by PartyId");
        dt = ConnectSql_mb.GetDataTable(sql);
        string Client = Common.DataTableToJson(dt);

        context = string.Format(@"{0}ChgCode:{2},Client:{3}{1}", "{", "}", ChgCode, Client);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void CMS_KIP_RATECT_GetList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = null;

        string ChgCode = job["ChgCode"].ToString();
        string Client = job["Client"].ToString();
        string sql_where = "";
        if (ChgCode.Length > 0)
        {
            sql_where += " and chgcode=@ChgCode";
        }
        if (Client.Length > 0)
        {
            sql_where += " and status5=@Client";
        }
        string sql = string.Format(@"select status5, (select chgcodedes from xxchgcode where chgcodeid=det1.chgcode) as des, unit, price 
from seaquotedet1 as det1 
where quoteid='-1' and status1='BILL' {0}",sql_where);
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", ChgCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }



    [WebMethod]
    public void CMS_KPI_GetCUSReport(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        string status = "1";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = null;

        //string Driver = SafeValue.SafeString(job["driver"]);
        string DateFrom = SafeValue.SafeString(job["from"]);
        string DateTo = SafeValue.SafeString(job["to"]);
        if (DateFrom.Equals("") || DateTo.Equals(""))
        {
            DateFrom = DateTime.Now.ToString("yyyyMMdd");
            DateTo = DateTime.Now.ToString("yyyyMMdd");
        }
        string sql = string.Format(@"with tb_trips as (
select det2.Id,convert(varchar,ToDate,112) as FromDate,isnull(job.ClientId,'') as ClientId,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim
from CTM_JobDet2 as det2 
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where det2.Statuscode='C' and datediff(day,@DateFrom,ToDate)>=0 and datediff(day,@DateTo,ToDate)<=0 and job.ClientId<>''
),
tb1 as (
select ClientId,sum(Incentive) as Incentive,sum(Claim) as Claim,count(Id) as Trips
from tb_trips group by ClientId
),
tb_inv as (
select convert(varchar,docdate,112) as docdate,locamt,isnull(PartyTo,'') as PartyTo from xaarinvoice
where datediff(day,@DateFrom,docdate)>=0 and datediff(day,@DateTo,docdate)<=0 and ExportInd='Y'
),
tb2 as(
select sum(locamt) as amt,PartyTo from tb_inv group by PartyTo
),
tb_cont as(
select convert(varchar,job.EtaDate,112) as eta,det1.ContainerType,isnull(substring(det1.ContainerType,1,1),'') as FCContType,isnull(job.ClientId,'') as ClientId,
(case job.JobType when 'IMP' then 'IMP' when 'EXP' then 'EXP' else 'OTS' end) as JobType,
isnull((select sum(isnull(psa.amount,0)) from psa_bill as psa where psa.[CONTAINER NUMBER]=det1.ContainerNo),0) as amount 
from CTM_JobDet1 as det1
left outer join CTM_Job as job on job.jobno=det1.jobno
where datediff(day,@DateFrom,job.EtaDate)>=0 and datediff(day,@DateTo,job.EtaDate)<=0
),
tb3 as (
select ClientId,
sum(case when JobType='IMP' and FCContType='2' then 1 else 0 end) as i20,
sum(case when JobType='IMP' and FCContType='4' then 1 else 0 end) as i40,
sum(case when JobType='EXP' and FCContType='2' then 1 else 0 end) as e20,
sum(case when JobType='EXP' and FCContType='4' then 1 else 0 end) as e40,
sum(case when JobType='OTS' and FCContType='2' then 1 else 0 end) as o20,
sum(case when JobType='OTS' and FCContType='4' then 1 else 0 end) as o40,
sum(case when FCContType='2' or FCContType='4' then 0 else 1 end) as oe,
sum(amount) as psaRB
from tb_cont group by ClientId
),
tb_show as (
select xp.Name as ClientId,isnull(tb2.amt,0) as inv,isnull(tb3.psaRB,0) as psaRB,
isnull(tb1.Incentive,0) as Incentive,isnull(tb1.Claim,0) as Claim, isnull(tb1.Trips,0) as Trips,
isnull(tb3.i20,0) as i20,isnull(tb3.i40,0) as i40,isnull(tb3.e20,0) as e20,isnull(tb3.e40,0) as e40,
isnull(tb3.o20,0) as o20,isnull(tb3.o40,0) as o40,isnull(tb3.oe,0) as oe
from  tb1
left outer join tb2 on tb1.ClientId=tb2.PartyTo
left outer join tb3 on tb1.ClientId=tb3.ClientId
left outer join XXParty as xp on tb1.ClientId=xp.PartyId
)
select *,(i20+e20+o20)+(i40+e40+o40)*2 as teu from tb_show order by ClientId");
        list = new List<ConnectSql_mb.cmdParameters>();
        //list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", Driver, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", DateFrom, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", DateTo, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }

    [WebMethod]
    public void CMS_KPI_StoreBalance_GetList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        
        string DateFrom = SafeValue.SafeString(job["from"]);
        string JobNo = SafeValue.SafeString(job["JobNo"]);
        string Client = SafeValue.SafeString(job["Client"]);
        string HblNo = SafeValue.SafeString(job["HblNo"]);
        string LotNo = SafeValue.SafeString(job["LotNo"]);
        string SKU = SafeValue.SafeString(job["SKU"]);
        string Warehouse = SafeValue.SafeString(job["Warehouse"]);
        string Location = SafeValue.SafeString(job["Location"]);
        if (DateFrom.Equals("") )
        {
            DateFrom = DateTime.Now.ToString("yyyyMMdd");
        }
        DataTable dt=C2.JobHouse.getStockBalance(JobNo, Client, DateFrom, HblNo, LotNo, SKU, Warehouse, Location,"","","");
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);

    }
    [WebMethod]
    public void CMS_KPI_StoreMovement_GetList(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string DateFrom = SafeValue.SafeString(job["from"]);
        string DateTo = SafeValue.SafeString(job["to"]);
        string Client = SafeValue.SafeString(job["Client"]);
        string HblNo = SafeValue.SafeString(job["HblNo"]);
        string LotNo = SafeValue.SafeString(job["LotNo"]);
        string SKU = SafeValue.SafeString(job["SKU"]);
        string Warehouse = SafeValue.SafeString(job["Warehouse"]);
        string Location = SafeValue.SafeString(job["Location"]);
        string ContainerNo = SafeValue.SafeString(job["ContainerNo"]);
        DataTable dt = C2.JobHouse.getStockMove(Client, DateFrom, DateTo, HblNo, LotNo, SKU, Warehouse, Location, ContainerNo);
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);

    }

}
