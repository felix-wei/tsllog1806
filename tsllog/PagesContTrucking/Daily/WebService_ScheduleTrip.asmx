<%@ WebService Language="C#" Class="WebService_ScheduleTrip" %>

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
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WebService_ScheduleTrip : System.Web.Services.WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void List_GetData_ByPage()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int curPage = SafeValue.SafeInt(info["curPage"], 0);
        string str_pageSize = info["pageSize"].ToString();
        curPage = curPage <= 0 ? 1 : curPage;
        int pageSize = 0;
        if (!str_pageSize.Equals("ALL"))
        {
            pageSize = SafeValue.SafeInt(str_pageSize, 0);
        }
        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string No=SafeValue.SafeString(info["ContNo"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@BookingDate", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%"+No, SqlDbType.NVarChar, 100));

        string sql = "";
        string sql_where="det2.statuscode<>'C' and job.JobStatus<>'Voided' and isnull(det2.Self_Ind,'')<>'Yes'";
        if(No.Length==0){
            sql_where+=" and datediff(d,det2.BookingDate,@BookingDate)>=0 and datediff(d,det2.BookingDate,@FromDate)<=0";
        }else{
            sql_where+=" and (job.JobNo like @No or job.MasterJobNo like @No or det1.ContainerNo like @No)";
        }
        sql = string.Format(@"select count(*) from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo 
where {0}",sql_where);
        int totalItems = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        int totalPages = pageSize > 0 ? (totalItems / pageSize + (totalItems % pageSize > 0 ? 1 : 0)) : 1;
        curPage = curPage > totalPages ? totalPages : curPage;
        list.Add(new ConnectSql_mb.cmdParameters("@currenPage", pageSize * (curPage - 1), SqlDbType.Int));


        sql = string.Format(@"select ROW_NUMBER()over(order by TripCodeInd,TripJobType,JobNo) as rowId,* 
from (
select (case when det2.direct_inf='Direct Loading' and (det2.TripCode='SHF' or det2.TripCode='TPT') then 1 when det2.direct_inf='Direct Delivery' and (det2.TripCode='SHF' or det2.TripCode='TPT') then 2 else 
(case det2.TripCode when 'TPT' then 3 when 'WGR' then 3 when 'WDO' then 3 when 'LOC' then 3 
when 'EXP' then 4 when 'IMP' then 5 when 'COL' then 6 when 'RET' then 7 when 'SHF' then 8 when 'CRA' then 9 else 11 end) 
end ) as TripCodeInd,
(case det2.TripCode when 'TPT' then 2 when 'WGR' then 2 when 'WDO' then 2 when 'LOC' then 2 when 'CRA' then 1 else 0 end) as showCT,
det2.Id,det2.TripCode,det2.JobNo as JobNo,det2.JobNo as JobNo1,
det2.FromTime,isnull(det2.TowheadCode,'')+' / '+isnull(det2.SubCon_Code,'') as TowheadCode,det2.ChessisCode,det2.RequestVehicleType,det2.RequestTrailerType,
isnull((select leg from (select t.Id,ROW_NUMBER()over(order by t.BookingDate) as leg from ctm_jobdet2 as t where t.Det1Id=det2.Det1Id and det2.Det1Id>0) as t where t.Id=det2.Id),0) as leg,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,job.ClientId,det2.ContainerNo,det1.SealNo,
det2.FromCode,det2.ToCode,case when det1.Id>0 then det1.ContainerType else det2.RequestVehicleType end as ContainerType,
job.OperatorCode,job.Vessel,job.Voyage,job.EtaDate,job.EtaTime,
case when det2.Det1Id>0 then (select isnull(PermitNo,'')+',' from (
select distinct pm.PermitNo from job_house as h
left outer join ref_permit as pm on h.HblNo=pm.HblNo and pm.JobNo=det2.JobNo
where pm.PermitNo<>'' and h.ContId=det2.Det1Id
) as temp for xml path('')) else (select isnull(PermitNo,'')+',' from (
select distinct pm.PermitNo from job_house as h
left outer join ref_permit as pm on h.HblNo=pm.HblNo and pm.JobNo=det2.JobNo
where pm.PermitNo<>'' and h.TripId=det2.Id
) as temp for xml path(''))end as PermitNo,job.CarrierBkgNo,job.SpecialInstruction,
det1.ContainerCategory,det2.Escort_Ind,job.JobType,det2.JobType as TripJobType,det2.Escort_Remark,convert(nvarchar(10),det2.BookingDate,120) as BookingDate,isnull(det2.BookingTime,'00:00') as BookingTime
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.det1Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where {0}
) as temp",sql_where);
        if (pageSize > 0)
        {
            sql = string.Format(@"select * from (
select top {0} * from (
select top {0} * from (", pageSize) + sql + string.Format(@"
) as t
where rowId>@currenPage
order by rowId
) as t
order by rowId desc
) as t
order by rowId");
        }
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"list\":{2},\"curPage\":{3},\"totalPages\":{4},\"totalItems\":{5}{1}", "{", "}", DataList, curPage, totalPages, totalItems);
        Common.WriteJson(true, context);
    }

    [WebMethod]
    public void reschedule()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int Id = SafeValue.SafeInt(info["Id"], 0);
        string rDate = SafeValue.SafeString(info["ScheduleDate"]);
        string rTime = SafeValue.SafeString(info["ScheduleTime"]);
        string sql = string.Format(@"update ctm_jobdet2 set BookingDate=@rDate,BookingTime=@rTime where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id",Id,SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@rDate",rDate,SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@rTime", rTime, SqlDbType.NVarChar, 10));
        Common.WriteJson(ConnectSql_mb.ExecuteNonQuery(sql,list).status, "{}");
    }


};