<%@ WebService Language="C#" Class="WebService_UpdateContainer" %>

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
// [System.Web.Script.Services.ScriptService]
public class WebService_UpdateContainer : System.Web.Services.WebService
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
        string No = SafeValue.SafeString(info["ContNo"]);
        string JobStatus = SafeValue.SafeString(info["JobStatus"], "Operation");
        string Client = SafeValue.SafeString(info["Client"]);
        string Vessel = SafeValue.SafeString(info["Vessel"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));

        string newJob = "[]";
        string returnJob = "[]";
        string exportJob = "[]";
        string returnJobPending = "[]";
        string completed = "[]";
        string future = "[]";
        string subContract = "[]";

        string sql = "";
        //det1.WhsReadyInd,det1.WhsReadyLocation,det1.WhsReadyTime,det1.WhsReadyWeight,
        string sql_column = @"select det2.Id as tripId,det2.Det1Id as contId,job.Id as jobId,job.JobNo,
det2.FromDate,isnull(det2.FromTime,'00:00') as FromTime,
det2.TowheadCode,det2.ChessisCode,det2.DriverCode,det2.DriverCode2,det2.TripCode,det2.Statuscode,
job.ClientId,det1.ContainerNo,det1.SealNo,det2.FromCode,det2.ToCode,det1.ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,det1.Weight,job.Remark as JobRemark,det1.ContWeight,
job.CarrierId,job.PermitNo,det1.YardAddress,det1.EmailInd,det1.PortnetStatus,
job.WarehouseAddress as shipper,det1.Br as PickupRefNo,det1.TerminalLocation,convert(nvarchar,job.EtdDate,103) as EtdDate,job.EtdTime as EtdTime,
job.ClientContact as orderBy,det1.Remark as ContRemark,det1.Remark1 as ContRemark1,det1.Remark2 as ClientRemark,det1.ReleaseToHaulierRemark,job.PortnetRef,job.ReturnLastDate,
(select top 1 Tel from ref_contact as contact where contact.Name=job.ClientContact and contact.PartyId=job.partyId) as orderByTel,
det1.CfsStatus,det1.ScheduleStartDate,det1.ScheduleStartTime,det2.SubCon_Code,det2.BillingRemark,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as c1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as c3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as c5
from tb_Trips as ts
left outer join ctm_jobdet2 as det2 on det2.Id=ts.Id
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join ctm_job as job on det2.JobNo=job.JobNo";
        string sql_where = "";

        if (JobStatus.Equals("Operation"))
        {
            //================= new Job
            sql_where = " and datediff(d,det2.FromDate,@FromDate)>=0";
            if (No.Length > 0)
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
            }
            if (Client.Length > 0)
            {
                sql_where += " and job.ClientId=@Client";
            }
            if (Vessel.Length > 0)
            {
                sql_where += " and job.Vessel=@Vessel";
            }
            //            sql = string.Format(@"with tb_Trips as (
            //select det2.Id
            //from CTM_JobDet2 as det2
            //left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
            //left outer join ctm_job as job on det2.JobNo=job.JobNo
            //where job.StatusCode='USE' and det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='COL' or det2.TripCode='IMP') and isnull(det2.SubCon_Ind,'N')<>'Y' {0}
            //)
            //{1}
            //order by det2.TripCode,det1.ReleaseToHaulierRemark,convert(nvarchar(10),det2.FromDate,112),det2.FromTime", sql_where, sql_column);

            sql =string.Format(@"with tb_Trips as (
select Id from (
select det2.Id,det2.SubCon_Ind,ROW_NUMBER()over(partition by det2.Det1Id order by 
case TripCode
when 'COL' then 1
when 'IMP' then 1
when 'SLD' then 5
when 'SMT' then 5
else 10 end
) as rowId
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and det2.Statuscode<>'C' and det2.Statuscode<>'X' 
and (det2.TripCode='COL' or det2.TripCode='IMP' or (job.JobType='IMP' and det2.TripCode='SLD') or (job.JobType='EXP' and det2.TripCode='SMT')) {0}
)temp where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
)
{1}
order by det2.TripCode,convert(nvarchar(10),det2.FromDate,112),det2.FromTime
", sql_where, sql_column);
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            newJob = Common.DataTableToJson(dt);

            //================= future
            sql_where = " and datediff(d,det2.FromDate,@FromDate)<0";
            if (No.Length > 0)
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
            }
            if (Client.Length > 0)
            {
                sql_where += " and job.ClientId=@Client";
            }
            if (Vessel.Length > 0)
            {
                sql_where += " and job.Vessel=@Vessel";
            }
            sql = string.Format(@"with tb_Trips as (
            select det2.Id
            from CTM_JobDet2 as det2
            left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
            left outer join ctm_job as job on det2.JobNo=job.JobNo
            where job.StatusCode='USE' and det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='COL' or det2.TripCode='IMP') and isnull(det2.SubCon_Ind,'N')<>'Y' {0}
            )
            {1}
            order by det2.TripCode,convert(nvarchar(10),det2.FromDate,112),det2.FromTime", sql_where, sql_column);
            dt = ConnectSql_mb.GetDataTable(sql, list);
            future = Common.DataTableToJson(dt);

            //================= sub Contract
            sql_where = "";
            if (No.Length > 0)
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
            }
            if (Client.Length > 0)
            {
                sql_where += " and job.ClientId=@Client";
            }
            if (Vessel.Length > 0)
            {
                sql_where += " and job.Vessel=@Vessel";
            }
            sql = string.Format(@"with tb_Trips as (
select Id from(
select det2.Id,det2.SubCon_Ind,
ROW_NUMBER()over(partition by det2.Det1Id order by case det2.TripCode when 'COL' then 1 when 'EXP' then 3 when 'IMP' then 1 when 'RET' then 3 else 2 end ) as rowId
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and det2.Statuscode<>'C' and det2.Statuscode<>'X' {0}
) as temp
where rowId=1 and SubCon_Ind='Y'
)
{1}
order by convert(nvarchar(10),job.EtaDate,112),job.EtaTime", sql_where, sql_column);
            dt = ConnectSql_mb.GetDataTable(sql, list);
            subContract = Common.DataTableToJson(dt);

            //================= exportJob
            sql_where = "";
            if (No.Length > 0)
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
            }
            if (Client.Length > 0)
            {
                sql_where += " and job.ClientId=@Client";
            }
            if (Vessel.Length > 0)
            {
                sql_where += " and job.Vessel=@Vessel";
            }
            sql = string.Format(@"with tb_Trips as (
select Id from(
select det2.Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
case when det2.TripCode='SHF' then 1 
when det2.TripCode='SLD' then 4
when det2.TripCode='EXP' then 5
else 10 end) as rowId,det2.SubCon_Ind
from (
select det1.Id
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and (det2.Statuscode='C' or det2.Statuscode='X')  and det2.TripCode='COL' and det1.CfsStatus='Completed' {0}
) as temp
left outer join CTM_JobDet2 as det2 on det2.Det1Id=temp.Id
where det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='EXP' or det2.TripCode='SHF' or det2.TripCode='SLD')
) as temp1
where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
)
{1}
order by convert(nvarchar(10),job.EtaDate,112),job.EtaTime", sql_where, sql_column);
            dt = ConnectSql_mb.GetDataTable(sql, list);
            exportJob = Common.DataTableToJson(dt);



            //================= returnJob
            sql_where = "";
            if (No.Length > 0)
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
            }
            if (Client.Length > 0)
            {
                sql_where += " and job.ClientId=@Client";
            }
            if (Vessel.Length > 0)
            {
                sql_where += " and job.Vessel=@Vessel";
            }
            sql = string.Format(@"with tb_Trips as (
select Id from(
select det2.Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
case when det2.TripCode='SHF' then 1 
when det2.TripCode='SMT' then 4 
when det2.TripCode='RET' then 5
else 10 end) as rowId,det2.SubCon_Ind
from (
select det1.Id
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and det2.Statuscode='C' and det2.TripCode='IMP' and det1.CfsStatus='Completed' {0}
) as temp
left outer join CTM_JobDet2 as det2 on det2.Det1Id=temp.Id
where det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='RET' or det2.TripCode='SHF' or det2.TripCode='SMT')
) as temp1
where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
)
{1}
order by convert(nvarchar(10),job.ReturnLastDate,112)", sql_where, sql_column);
            dt = ConnectSql_mb.GetDataTable(sql, list);
            returnJob = Common.DataTableToJson(dt);


            //================= pending
            sql_where = "";
            if (No.Length > 0)
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
            }
            if (Client.Length > 0)
            {
                sql_where += " and job.ClientId=@Client";
            }
            if (Vessel.Length > 0)
            {
                sql_where += " and job.Vessel=@Vessel";
            }
            //            sql = string.Format(@"with tb_Trips as (
            //select Id from(
            //select det2.Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
            //case when det2.TripCode='SHF' then 1 
            //when job.JobType='IMP' and det2.TripCode='SLD' then 4 
            //when det2.TripCode='RET' then 6
            //when job.JobType='EXP' and det2.TripCode='SMT' then 4 
            //when det2.TripCode='EXP' then 6
            //else 10 end) as rowId,det2.SubCon_Ind
            //from (
            //select det1.Id
            //from CTM_JobDet2 as det2
            //left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
            //left outer join ctm_job as job on det2.JobNo=job.JobNo
            //where job.StatusCode='USE' and det2.Statuscode='C' and (det2.TripCode='IMP' or det2.TripCode='COL') and isnull(det1.CfsStatus,'')<>'Completed' {0}
            //) as temp
            //left outer join CTM_JobDet2 as det2 on det2.Det1Id=temp.Id
            //left outer join CTM_Job as job on det2.JobNo=job.JobNo
            //where det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='RET' or det2.TripCode='EXP' or det2.TripCode='SHF' or det2.TripCode='SMT' or det2.TripCode='SLD')
            //) as temp1
            //where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
            //)
            //{1}
            //order by job.JobType,case when job.JobType='IMP' then convert(nvarchar(10),job.ReturnLastDate,112) when job.JobType='EXP' then convert(nvarchar(10),job.EtaDate,112) else '17530101' end", sql_where, sql_column);


            sql = string.Format(@"with tb_Trips as (
select Id from(
select det2.Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
case when det2.TripCode='SHF' then 1 
when job.JobType='IMP' and det2.TripCode='SLD' then 4 
when det2.TripCode='RET' then 6
when job.JobType='EXP' and det2.TripCode='SMT' then 4 
when det2.TripCode='EXP' then 6
else 10 end) as rowId,det2.SubCon_Ind
from (
select Det1Id as Id from (
select det2.Id,det2.Statuscode,det2.Det1Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
case TripCode
when 'COL' then 1
when 'IMP' then 1
when 'SLD' then 0
when 'SMT' then 0
else 10 end
) as rowId
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and isnull(det1.CfsStatus,'')<>'Completed'
and (det2.TripCode='COL' or det2.TripCode='IMP' or (job.JobType='IMP' and det2.TripCode='SLD') or (job.JobType='EXP' and det2.TripCode='SMT')) --and det2.Statuscode<>'X' 
{0}
) as temp
where rowId=1 and (Statuscode='C' or Statuscode='X')) as temp
left outer join CTM_JobDet2 as det2 on det2.Det1Id=temp.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='RET' or det2.TripCode='EXP' or det2.TripCode='SHF' or det2.TripCode='SMT' or det2.TripCode='SLD')
) as temp1
where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
)
{1}
order by job.JobType,case when job.JobType='IMP' then convert(nvarchar(10),job.ReturnLastDate,112) when job.JobType='EXP' then convert(nvarchar(10),job.EtaDate,112) else '17530101' end", sql_where, sql_column);

            dt = ConnectSql_mb.GetDataTable(sql, list);
            returnJobPending = Common.DataTableToJson(dt);

        }
        else
        {
            //================= completed
            sql_where = "";
            if (No.Length > 0)
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
            }
            if (Client.Length > 0)
            {
                sql_where += " and job.ClientId=@Client";
            }
            if (Vessel.Length > 0)
            {
                sql_where += " and job.Vessel=@Vessel";
            }
            sql = string.Format(@"with tb_Trips as (
select det2.Id
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where det2.Statuscode='C' and (det2.TripCode='EXP' or det2.TripCode='RET') and det1.StatusCode='Completed' 
and isnull(job.StatusCode,'USE')='USE' and (isnull(job.JobStatus,'')<>'Billing' and isnull(job.JobStatus,'')<>'Completed') {0}
)
{1}
order by det2.TripCode,convert(nvarchar(10),det2.FromDate,112),det2.FromTime", sql_where, sql_column);
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            completed = Common.DataTableToJson(dt);
        }

        string context = string.Format("{0}\"newJob\":{2},\"returnJob\":{3},\"exportJob\":{4},\"pendingReturn\":{5},\"completed\":{6},\"future\":{7},\"subContract\":{8}{1}", "{", "}", newJob, returnJob, exportJob, returnJobPending, completed, future, subContract);
        Common.WriteJson(true, context);
    }

    [WebMethod]
    public void PlannerTKList_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        string From = info["From"].ToString();
        string No = SafeValue.SafeString(info["ContNo"]);
        string JobStatus = SafeValue.SafeString(info["JobStatus"], "Operation");
        string planDate = info["planDate"].ToString();
        //string Client = SafeValue.SafeString(info["Client"]);
        //string Vessel = SafeValue.SafeString(info["Vessel"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@planDate", planDate, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));
        //list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        //list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));

        string newJob = "[]";
        string returnJob = "[]";
        string exportJob = "[]";
        string returnJobPending = "[]";

        string sql = "";
        //det1.WhsReadyInd,det1.WhsReadyLocation,det1.WhsReadyTime,det1.WhsReadyWeight,
        string sql_column = @"select det2.Id as tripId,det2.Det1Id as contId,job.Id as jobId,job.JobNo,
det2.FromDate,isnull(det2.FromTime,'00:00') as FromTime,
det2.TowheadCode,det2.ChessisCode,det2.DriverCode,det2.DriverCode2,isnull(det2.DriverCode3,'') as DriverCode3,det2.TripCode,det2.Statuscode,
job.ClientId,det1.ContainerNo,det1.SealNo,det2.FromCode,det2.ToCode,det1.ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,det1.Weight,job.Remark as JobRemark,det1.ContWeight,
job.CarrierId,job.PermitNo,det1.YardAddress,det1.PortnetStatus,
job.WarehouseAddress as shipper,det1.Br as PickupRefNo,det1.TerminalLocation,convert(nvarchar,job.EtdDate,103) as EtdDate,job.EtdTime as EtdTime,
job.ClientContact as orderBy,det1.Remark as ContRemark,det1.Remark1 as ContRemark1,det1.Remark2 as ClientRemark,det1.ReleaseToHaulierRemark,job.PortnetRef,job.ReturnLastDate,
(select top 1 Tel from ref_contact as contact where contact.Name=job.ClientContact and contact.PartyId=job.partyId) as orderByTel,
det1.CfsStatus,det1.ScheduleStartDate,det1.ScheduleStartTime,det2.SubCon_Code,det2.TripIndex1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as c1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as c3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as c5
from tb_Trips as ts
left outer join ctm_jobdet2 as det2 on det2.Id=ts.Id
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join ctm_job as job on det2.JobNo=job.JobNo";
        string sql_where = "";

        //================= new Job
        sql_where = " and datediff(d,det2.FromDate,@FromDate)>=0";
        if (No.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
        }
        //if (Client.Length > 0)
        //{
        //    sql_where += " and job.ClientId=@Client";
        //}
        //if (Vessel.Length > 0)
        //{
        //    sql_where += " and job.Vessel=@Vessel";
        //}
        //        sql = string.Format(@"with tb_Trips as (
        //select det2.Id
        //from CTM_JobDet2 as det2
        //left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
        //left outer join ctm_job as job on det2.JobNo=job.JobNo
        //where job.StatusCode='USE' and det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='COL' or det2.TripCode='IMP') and isnull(det2.SubCon_Ind,'N')<>'Y' 
        //and isnull(det2.DriverCode,'')='' and isnull(det2.DriverCode3,'')='' {0}
        //)
        //{1}
        //order by det2.TripCode,det1.ReleaseToHaulierRemark,convert(nvarchar(10),det2.FromDate,112),det2.FromTime", sql_where, sql_column);

        sql=string.Format(@"with tb_Trips as (
select Id from (
select det2.Id,det2.SubCon_Ind,ROW_NUMBER()over(partition by det2.Det1Id order by 
case TripCode
when 'COL' then 1
when 'IMP' then 1
when 'SLD' then 5
when 'SMT' then 5
else 10 end
) as rowId
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and det2.Statuscode<>'C' and det2.Statuscode<>'X' 
and (det2.TripCode='COL' or det2.TripCode='IMP' or (job.JobType='IMP' and det2.TripCode='SLD') or (job.JobType='EXP' and det2.TripCode='SMT')) and isnull(det2.DriverCode,'')='' and isnull(det2.DriverCode3,'')='' {0}
)temp where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
)
{1}
order by det2.TripCode,convert(nvarchar(10),det2.FromDate,112),det2.FromTime
", sql_where, sql_column);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        newJob = Common.DataTableToJson(dt);


        //================= exportJob
        sql_where = "";
        if (No.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
        }
        //if (Client.Length > 0)
        //{
        //    sql_where += " and job.ClientId=@Client";
        //}
        //if (Vessel.Length > 0)
        //{
        //    sql_where += " and job.Vessel=@Vessel";
        //}
        sql = string.Format(@"with tb_Trips as (
select Id from(
select det2.Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
case when det2.TripCode='SHF' then 1 
when det2.TripCode='SLD' then 4
when det2.TripCode='EXP' then 5
else 10 end) as rowId,det2.SubCon_Ind
from (
select det1.Id
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and (det2.Statuscode='C' or det2.Statuscode='X') and det2.TripCode='COL' and det1.CfsStatus='Completed' {0}
) as temp
left outer join CTM_JobDet2 as det2 on det2.Det1Id=temp.Id
where det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='EXP' or det2.TripCode='SHF' or det2.TripCode='SLD')
and isnull(det2.DriverCode,'')='' and isnull(det2.DriverCode3,'')=''
) as temp1
where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
)
{1}
order by convert(nvarchar(10),job.EtaDate,112),job.EtaTime", sql_where, sql_column);
        dt = ConnectSql_mb.GetDataTable(sql, list);
        exportJob = Common.DataTableToJson(dt);



        //================= returnJob
        sql_where = "";
        if (No.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
        }
        //if (Client.Length > 0)
        //{
        //    sql_where += " and job.ClientId=@Client";
        //}
        //if (Vessel.Length > 0)
        //{
        //    sql_where += " and job.Vessel=@Vessel";
        //}
        sql = string.Format(@"with tb_Trips as (
select Id from(
select det2.Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
case when det2.TripCode='SHF' then 1 
when det2.TripCode='SMT' then 4 
when det2.TripCode='RET' then 5
else 10 end) as rowId,det2.SubCon_Ind
from (
select det1.Id
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and det2.Statuscode='C' and det2.TripCode='IMP' and det1.CfsStatus='Completed'  {0}
) as temp
left outer join CTM_JobDet2 as det2 on det2.Det1Id=temp.Id
where det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='RET' or det2.TripCode='SHF' or det2.TripCode='SMT')
and isnull(det2.DriverCode,'')='' and isnull(det2.DriverCode3,'')=''
) as temp1
where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
)
{1}
order by convert(nvarchar(10),job.ReturnLastDate,112)", sql_where, sql_column);
        dt = ConnectSql_mb.GetDataTable(sql, list);
        returnJob = Common.DataTableToJson(dt);


        //================= pending
        sql_where = "";
        if (No.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
        }
        //if (Client.Length > 0)
        //{
        //    sql_where += " and job.ClientId=@Client";
        //}
        //if (Vessel.Length > 0)
        //{
        //    sql_where += " and job.Vessel=@Vessel";
        //}
        //        sql = string.Format(@"with tb_Trips as (
        //select Id from(
        //select det2.Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
        //case when det2.TripCode='SHF' then 1 
        //when job.JobType='IMP' and det2.TripCode='SLD' then 4 
        //when det2.TripCode='RET' then 6
        //when job.JobType='EXP' and det2.TripCode='SMT' then 4 
        //when det2.TripCode='EXP' then 6
        //else 10 end) as rowId,det2.SubCon_Ind
        //from (
        //select det1.Id
        //from CTM_JobDet2 as det2
        //left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
        //left outer join ctm_job as job on det2.JobNo=job.JobNo
        //where job.StatusCode='USE' and det2.Statuscode='C' and (det2.TripCode='IMP' or det2.TripCode='COL') and isnull(det1.CfsStatus,'')<>'Completed'  {0}
        //) as temp
        //left outer join CTM_JobDet2 as det2 on det2.Det1Id=temp.Id
        //left outer join CTM_Job as job on det2.JobNo=job.JobNo
        //where det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='RET' or det2.TripCode='EXP' or det2.TripCode='SHF' or det2.TripCode='SMT' or det2.TripCode='SLD')
        //and isnull(det2.DriverCode,'')='' and isnull(det2.DriverCode3,'')=''
        //) as temp1
        //where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
        //)
        //{1}
        //order by job.JobType,case when job.JobType='IMP' then convert(nvarchar(10),job.ReturnLastDate,112) when job.JobType='EXP' then convert(nvarchar(10),job.EtaDate,112) else '17530101' end", sql_where, sql_column);

        sql = string.Format(@"with tb_Trips as (
select Id from(
select det2.Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
case when det2.TripCode='SHF' then 1 
when job.JobType='IMP' and det2.TripCode='SLD' then 4 
when det2.TripCode='RET' then 6
when job.JobType='EXP' and det2.TripCode='SMT' then 4 
when det2.TripCode='EXP' then 6
else 10 end) as rowId,det2.SubCon_Ind
from (
select Det1Id as Id from (
select det2.Id,det2.Statuscode,det2.Det1Id,ROW_NUMBER()over(partition by det2.Det1Id order by 
case TripCode
when 'COL' then 1
when 'IMP' then 1
when 'SLD' then 0
when 'SMT' then 0
else 10 end
) as rowId
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and isnull(det1.CfsStatus,'')<>'Completed'
and (det2.TripCode='COL' or det2.TripCode='IMP' or (job.JobType='IMP' and det2.TripCode='SLD') or (job.JobType='EXP' and det2.TripCode='SMT')) --and det2.Statuscode<>'X' 
{0}
) as temp
where rowId=1 and (Statuscode='C' or Statuscode='X')) as temp
left outer join CTM_JobDet2 as det2 on det2.Det1Id=temp.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Statuscode<>'C' and det2.Statuscode<>'X' and (det2.TripCode='RET' or det2.TripCode='EXP' or det2.TripCode='SHF' or det2.TripCode='SMT' or det2.TripCode='SLD') and isnull(det2.DriverCode,'')='' and isnull(det2.DriverCode3,'')=''
) as temp1
where rowId=1 and isnull(SubCon_Ind,'N')<>'Y'
)
{1}
order by job.JobType,case when job.JobType='IMP' then convert(nvarchar(10),job.ReturnLastDate,112) when job.JobType='EXP' then convert(nvarchar(10),job.EtaDate,112) else '17530101' end", sql_where, sql_column);
        dt = ConnectSql_mb.GetDataTable(sql, list);
        returnJobPending = Common.DataTableToJson(dt);


        //=============== right part
        sql_where = " and datediff(d,det2.FromDate,@planDate)=0";
        if (No.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
        }

        sql = string.Format(@"with tb_Trips as (
select det2.Id
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where (job.StatusCode='USE' or job.StatusCode='CLS') and isnull(det2.SubCon_Ind,'N')<>'Y' and det2.TripCode in ('IMP','RET','COL','EXP','SHF','SMT','SLD') 
and (isnull(det2.DriverCode,'')<>'' or isnull(det2.DriverCode3,'')<>'') {0}
)
{1}
order by det2.TripIndex1,convert(nvarchar(10),det2.FromDate,112),det2.FromTime", sql_where, sql_column);
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string rightPart = Common.DataTableToJson(dt);


        sql = string.Format(@"select Id,Code,TowheaderCode from ctm_driver where StatusCode='Active' order by Code");
        dt = ConnectSql_mb.GetDataTable(sql);
        string driverList = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"newJob\":{2},\"returnJob\":{3},\"exportJob\":{4},\"pendingReturn\":{5},\"rightPart\":{6},\"driverList\":{7}{1}", "{", "}", newJob, returnJob, exportJob, returnJobPending,rightPart,driverList);
        Common.WriteJson(true, context);

    }


    [WebMethod]
    public void PlannerList_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        string From = info["From"].ToString();
        string No = SafeValue.SafeString(info["ContNo"]);
        //string Client = SafeValue.SafeString(info["Client"]);
        //string Vessel = SafeValue.SafeString(info["Vessel"]);

        string sql_where = "";
        if (No.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or job.PortnetRef like @No or det1.ContainerNo like @No)";
        }
        string sql = string.Format(@"select * from (
select (select top 1 o_det2.Id 
from ctm_jobdet2 as o_det2 
where o_det2.Det1Id=det2.Det1Id and o_det2.Statuscode<>'C'
order by case 
when o_det2.TripCode='IMP' or o_det2.TripCode='COL' then 1
when o_det2.TripCode='RET' or o_det2.TripCode='EXP' then 9
when job.JobType='IMP' and (o_det2.TripCode='SHF' or o_det2.TripCode='SLD') then 2
when job.JobType='IMP' and (o_det2.TripCode='SHF' or o_det2.TripCode='SMT') then 3
when job.JobType='EXP' and (o_det2.TripCode='SHF' or o_det2.TripCode='SMT') then 2
when job.JobType='EXP' and (o_det2.TripCode='SHF' or o_det2.TripCode='SLD') then 3
else 10 end) as oid,
det2.Id as tripId,det2.Det1Id as contId,job.Id as jobId,job.JobNo,
det2.FromDate,isnull(det2.FromTime,'00:00') as FromTime,det2.TripIndex1,
det2.TowheadCode,det2.ChessisCode,det2.DriverCode,det2.DriverCode2,isnull(det2.DriverCode3,'') as DriverCode3,det2.TripCode,det2.Statuscode,
job.ClientId,det1.ContainerNo,det1.SealNo,det2.FromCode,det2.ToCode,det1.ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,det1.Weight,job.Remark as JobRemark,det1.ContWeight,
job.CarrierId,job.PermitNo,det1.YardAddress,
job.WarehouseAddress as shipper,det1.Br as PickupRefNo,det1.TerminalLocation,convert(nvarchar,job.EtdDate,103) as EtdDate,job.EtdTime as EtdTime,job.ClientContact as orderBy,det1.Remark as ContRemark,det1.ReleaseToHaulierRemark,job.PortnetRef,job.ReturnLastDate,
(select top 1 Tel from ref_contact as contact where contact.Name=job.ClientContact and contact.PartyId=job.partyId) as orderByTel,
det1.CfsStatus,det1.ScheduleStartDate,det1.ScheduleStartTime,det2.SubCon_Code
from ctm_jobdet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,det2.FromDate,@FromDate)=0 and 
det2.StatusCode<>'C' and det2.Det1Id>0 and isnull(det2.DriverCode,'')='' and isnull(det2.SubCon_Ind,'N')<>'Y' and job.StatusCode <>'CNL' {0}
) as temp 
where tripId=oid
order by TripCode,FromDate,TripIndex1", sql_where);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string dataList = Common.DataTableToJson(dt);

        sql = string.Format(@"select Id,Code,TowheaderCode from ctm_driver where StatusCode='Active' order by Code");
        dt = ConnectSql_mb.GetDataTable(sql);
        string driverList = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"dataList\":{2},\"driverList\":{3}{1}", "{", "}", dataList, driverList);
        Common.WriteJson(true, context);
    }



    [WebMethod]
    public void PlannerTPTList_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        string From = info["From"].ToString();
        string No = SafeValue.SafeString(info["ContNo"]);
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
where datediff(d,det2.FromDate,@FromDate)=0 and 
det2.StatusCode<>'C' and job.IsLocal='Yes' and isnull(det2.DriverCode,'')='' and isnull(det2.SubCon_Ind,'N')<>'Y' and job.StatusCode <>'CNL' {0} 
order by TripCode,FromDate,TripIndex1", sql_where);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string dataList = Common.DataTableToJson(dt);

        sql = string.Format(@"select Id,Code,TowheaderCode from ctm_driver where StatusCode='Active' order by Code");
        dt = ConnectSql_mb.GetDataTable(sql);
        string driverList = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"dataList\":{2},\"driverList\":{3}{1}", "{", "}", dataList, driverList);
        Common.WriteJson(true, context);
    }

    [WebMethod]
    public void PlanTrips()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Ids = SafeValue.SafeString(info["Ids"], "0");
        string driver = SafeValue.SafeString(info["driver"]);
        string sql = string.Format(@"update ctm_jobdet2 set DriverCode3='{0}' where Id in ({1}) ", driver, Ids);
        int r = ConnectSql_mb.ExecuteNonQuery(sql);
        bool status = false;
        if (r > 0)
        {
            status = true;
        }
        Common.WriteJson(status, Common.StringToJson(""));
    }

    [WebMethod]
    public void PlanTrips_ByIndex()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Ids = SafeValue.SafeString(info["Ids"]);
        JArray Id_list = (JArray)JsonConvert.DeserializeObject(Ids);
        string driver = SafeValue.SafeString(info["driver"]);

        string sql = string.Format(@"update ctm_jobdet2 set DriverCode3=@DriverCode,
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
                ConnectSql_mb.ExecuteNonQuery(sql, list);
            }
        }

        Common.WriteJson(true, Common.StringToJson(""));
    }


    [WebMethod]
    public void PlanTKTrips_ByIndex()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Ids = SafeValue.SafeString(info["Ids"]);
        JArray Id_list = (JArray)JsonConvert.DeserializeObject(Ids);
        string driver = SafeValue.SafeString(info["driver"]);
        string planDate = SafeValue.SafeString(info["planDate"]);

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
        //string sql = string.Format(@"update ctm_jobdet2 set DriverCode=DriverCode3 where Id=@Id");
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
    public void View_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int TripId = SafeValue.SafeInt(info["TripId"], 0);
        string mast = "{}";

        string sql = string.Format(@"select det2.Id,det2.Det1Id,det2.ContainerNo,det2.DriverCode,det2.DriverCode2,det2.ChessisCode,det2.Statuscode,
job.JobStatus,job.StatusCode as JobStatusCode,det1.CfsStatus,det1.Weight,det2.SubCon_Ind,det2.SubCon_Code,
det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.JobNo,det2.TripCode,det2.BillingRemark,
job.ReturnLastDate,job.Id as jobId,job.EmailAddress,det1.EmailInd,det2.Remark1,det1.PortnetStatus,job.JobType,
det2.FromCode,det2.ToCode,det2.Remark,det2.TowheadCode,det2.FromParkingLot,det2.ToParkingLot,det1.SealNo,det1.ContWeight,det1.YardAddress,
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
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as c10,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as b_c2,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as b_c3,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as b_c4,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as b_c5,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as b_c6,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as b_c7,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as b_c8,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as b_c9,
(select top 1 NotBuildCustomer from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as b_c10
from CTM_JobDet2 as det2 
left outer join ctm_jobdet1 as det1 on det2.Det1Id=det1.Id
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
        string SealNo = SafeValue.SafeString(info["SealNo"]);
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

        decimal ContainerWt = SafeValue.SafeDecimal(info["Weight"]);
        string SubCon_Ind = SafeValue.SafeString(info["SubCon_Ind"]);
        string SubCon_Code = SafeValue.SafeString(info["SubCon_Code"]);
        decimal ContWeight = SafeValue.SafeDecimal(info["ContWeight"]);
        string YardAddress = SafeValue.SafeString(info["YardAddress"]);
        string BillingRemark = SafeValue.SafeString(info["BillingRemark"]);
        string Remark1 = SafeValue.SafeString(info["Remark1"]);

        int jobId = SafeValue.SafeInt(info["jobId"],0);
        string ReturnLastDate = SafeValue.SafeString(info["ReturnLastDate"]);
        FromTime = SafeValue_mb.convertTimeFormat(FromTime);
        ToTime = SafeValue_mb.convertTimeFormat(ToTime);

        DateTime dt_fromdate = DateTime.ParseExact(FromDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        DateTime dt_todate = DateTime.ParseExact(ToDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

        DateTime dt_ReturnLastDate=new DateTime(1753,1,1);
        try
        {
            dt_ReturnLastDate=DateTime.ParseExact(ReturnLastDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        }
        catch { }

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
                C2.CtmJobDet1Biz det1Bz = new C2.CtmJobDet1Biz(det2.Det1Id);
                C2.CtmJobDet1 det1 = det1Bz.getData();
                if (det1 != null)
                {

                    det1.Weight = ContainerWt;
                    det1.SealNo = SealNo;
                    det1.ContWeight = ContWeight;
                    det1.YardAddress = YardAddress;
                    //det1.ScheduleDate = dt_fromdate;
                    //det1.ScheduleTime = FromTime;
                    det1Bz.update(HttpContext.Current.User.Identity.Name);
                }
                C2.CtmJob job=C2.Manager.ORManager.GetObject<C2.CtmJob>(jobId);
                if (job != null)
                {
                    job.ReturnLastDate = dt_ReturnLastDate;
                    C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(job);
                }

            }
            else
            {
                note = result.context;
            }

            //C2.Manager.ORManager.StartTracking(det2, Wilson.ORMapper.InitialState.Updated);
            //C2.Manager.ORManager.PersistChanges(det2);

            //C2.CtmJobDet1 det1 = C2.Manager.ORManager.GetObject<C2.CtmJobDet1>(det2.Det1Id);
            //det1.ContainerNo = ContainerNo;
            //C2.Manager.ORManager.StartTracking(det1, Wilson.ORMapper.InitialState.Updated);
            //C2.Manager.ORManager.PersistChanges(det1);

            //string sql = string.Format(@"update ctm_jobdet2 set ContainerNo=@ContainerNo where Det1Id=@Det1Id");
            //List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            //list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", det1.Id, SqlDbType.Int));
            //list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", det1.ContainerNo, SqlDbType.NVarChar, 100));
            //ConnectSql_mb.ExecuteNonQuery(sql, list);


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

            string b_c2 = (SafeValue.SafeString(info["b_c2"]) == "Y" ? "Y" : "N");
            string b_c3 = (SafeValue.SafeString(info["b_c3"]) == "Y" ? "Y" : "N");
            string b_c4 = (SafeValue.SafeString(info["b_c4"]) == "Y" ? "Y" : "N");
            string b_c5 = (SafeValue.SafeString(info["b_c5"]) == "Y" ? "Y" : "N");
            string b_c6 = (SafeValue.SafeString(info["b_c6"]) == "Y" ? "Y" : "N");
            string b_c7 = (SafeValue.SafeString(info["b_c7"]) == "Y" ? "Y" : "N");
            string b_c8 = (SafeValue.SafeString(info["b_c8"]) == "Y" ? "Y" : "N");
            string b_c9 = (SafeValue.SafeString(info["b_c9"]) == "Y" ? "Y" : "N");
            string b_c10 = (SafeValue.SafeString(info["b_c10"]) == "Y" ? "Y" : "N");
            Dictionary<string, string> b_d = new Dictionary<string, string>();
            b_d.Add("WEIGHING", b_c2);
            b_d.Add("WASHING", b_c3);
            b_d.Add("REPAIR", b_c4);
            b_d.Add("DETENTION", b_c5);
            b_d.Add("DEMURRAGE", b_c6);
            b_d.Add("LIFT_ON_OFF", b_c7);
            b_d.Add("C_SHIPMENT", b_c8);
            b_d.Add("EMF", b_c9);
            b_d.Add("OTHER", b_c10);
            C2.CtmJobDet2.cost_ClaimBuildToCustomer(tripId, b_d);

            status = true;
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
    public void AddTrip_ByCurrentyTripId()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        int curTripId = SafeValue.SafeInt(info["Id"], 0);
        string TripType = SafeValue.SafeString(info["TripType"]);

        string sql = string.Format(@"select Det1.Id,det1.ContainerNo,det1.JobNo,det2.ChessisCode
,(select top 1 tt.ToCode from CTM_JobDet2 as tt where det1.Id=tt.Det1Id 
order by (case when tt.TripCode='SHF' then 0 when tt.TripCode='IMP' or tt.TripCode='COL' then 1 else 2 end),FromDate desc) as lastAddress  
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where det2.Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", curTripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        bool status = false;
        string context = "";
        if (dt.Rows.Count == 1)
        {
            int det1Id = SafeValue.SafeInt(dt.Rows[0]["Id"], 0);
            string ContainerNo = SafeValue.SafeString(dt.Rows[0]["ContainerNo"]);
            string JobNo = SafeValue.SafeString(dt.Rows[0]["JobNo"]);
            string lastAddress = SafeValue.SafeString(dt.Rows[0]["lastAddress"]);
            string ChessisCode = SafeValue.SafeString(dt.Rows[0]["ChessisCode"]);
            string user = HttpContext.Current.User.Identity.Name;

            C2.CtmJobDet2 det2 = new C2.CtmJobDet2();
            det2.Det1Id = det1Id;
            det2.JobNo = JobNo;
            det2.ContainerNo = ContainerNo;
            det2.TripCode = TripType;
            det2.Statuscode = "P";
            det2.FromDate = DateTime.Now;
            det2.ToDate = DateTime.Now;
            det2.FromTime = "08:00";
            det2.ToTime = "00:00";
            det2.FromCode = lastAddress;
            det2.ChessisCode = ChessisCode;

            C2.Manager.ORManager.StartTracking(det2, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det2);
            status = true;

            //C2.CtmJobDet2Biz bz = new C2.CtmJobDet2Biz(0);
            //C2.BizResult result = bz.insert(user, det2);
            //if (result.status)
            //{
            //    status = true;
            //}
            //else
            //{
            //    context = result.context;
            //}
        }
        Common.WriteJson(status, Common.StringToJson(context));
    }



    [WebMethod]
    public void AddTrip_ByTripIdType()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        int curTripId = SafeValue.SafeInt(info["Id"], 0);
        string TripType = SafeValue.SafeString(info["TripType"]);
        string toAddress = SafeValue.SafeString(info["ToCode"]);
        string user = HttpContext.Current.User.Identity.Name;
        bool status = false;
        string context = "";
        C2.CtmJobDet2 det2 = null;


        if (TripType.Equals("SMT") || TripType.Equals("SLD") || TripType.Equals("SHF"))
        {
            string sql = string.Format(@"select det2.Id as tripId,det1.Id as contId,job.Id as jobId,
job.JobNo,job.JobType,det1.ContainerNo
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where det2.Id=@tripId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@tripId", curTripId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count == 1)
            {
                //string JobNo = SafeValue.SafeString(dt.Rows[0]["JobNo"]);
                string JobType = SafeValue.SafeString(dt.Rows[0]["JobType"]);
                //string ContainerNo = SafeValue.SafeString(dt.Rows[0]["ContainerNo"]);
                int contId = SafeValue.SafeInt(dt.Rows[0]["contId"], 0);
                int copyFromTripId = 0;


                sql = string.Format(@"select Id,FromCode,ToCode,ChessisCode from CTM_JobDet2 where det1Id=@det1Id and TripCode=@TripCode");
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@det1Id", contId, SqlDbType.Int));
                if (JobType.Equals("IMP"))
                {
                    list.Add(new ConnectSql_mb.cmdParameters("@TripCode", "RET", SqlDbType.NVarChar, 10));
                    dt = ConnectSql_mb.GetDataTable(sql, list);
                    if (dt.Rows.Count > 0)
                    {
                        copyFromTripId = SafeValue.SafeInt(dt.Rows[0]["Id"], 0);
                    }
                }
                if (JobType.Equals("EXP"))
                {
                    list.Add(new ConnectSql_mb.cmdParameters("@TripCode", "EXP", SqlDbType.NVarChar, 10));
                    dt = ConnectSql_mb.GetDataTable(sql, list);
                    if (dt.Rows.Count > 0)
                    {
                        copyFromTripId = SafeValue.SafeInt(dt.Rows[0]["Id"], 0);
                    }
                }

                if (copyFromTripId > 0)
                {
                    C2.CtmJobDet2 retTrip = C2.Manager.ORManager.GetObject<C2.CtmJobDet2>(copyFromTripId);
                    if (retTrip != null)
                    {
                        det2 = retTrip;
                        det2.TripCode = TripType;
                        det2.Statuscode = "P";
                        det2.FromDate = SafeValue_mb.DateTime_ClearTime(DateTime.Now);
                        det2.ToDate = SafeValue_mb.DateTime_ClearTime(DateTime.Now);
                        det2.FromTime = "08:00";
                        det2.ToTime = "00:00";
                        det2.ToCode = toAddress;
                    }
                }
            }
        }
        if (det2 != null)
        {
            C2.CtmJobDet2Biz bz = new C2.CtmJobDet2Biz(0);
            bz.insert(user, det2);
            status = true;
        }
        Common.WriteJson(status, Common.StringToJson(context));
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
    public void containerEmpty()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        string JobNo = SafeValue.SafeString(info["JobNo"]);
        int tripId = SafeValue.SafeInt(info["Id"], 0);
        int det1Id = SafeValue.SafeInt(info["det1Id"], 0);

        C2.CtmJobDet1Biz bz = new C2.CtmJobDet1Biz(det1Id);
        C2.CtmJobDet1 det1 = bz.getData();
        bool status = false;

        if (det1 != null)
        {
            det1.StatusCode = "Customer-MT";
            det1.CfsStatus = "Completed";
            det1.ScheduleStartDate = DateTime.Now;
            det1.ScheduleStartTime = DateTime.Now.ToString("HH:mm");
            det1.CompletionDate = det1.ScheduleStartDate;
            det1.CompletionTime = det1.ScheduleStartTime;
            string userId = HttpContext.Current.User.Identity.Name;
            C2.BizResult result = bz.update(userId);
            if (result.status)
            {
                status = true;
                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isWeb();
                elog.Controller = userId;
                elog.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, -1, "Container Empty");
                elog.log();
            }
        }
        Common.WriteJson(status, Common.StringToJson(""));
    }

    [WebMethod]
    public void containerReadyExport()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        string JobNo = SafeValue.SafeString(info["JobNo"]);
        int tripId = SafeValue.SafeInt(info["Id"], 0);
        int det1Id = SafeValue.SafeInt(info["det1Id"], 0);
        decimal Weight = SafeValue.SafeDecimal(info["Weight"]);

        C2.CtmJobDet1Biz bz = new C2.CtmJobDet1Biz(det1Id);
        C2.CtmJobDet1 det1 = bz.getData();
        bool status = false;

        if (det1 != null)
        {
            det1.Weight = Weight;
            det1.StatusCode = "Customer-LD";
            det1.CfsStatus = "Completed";
            det1.ScheduleStartDate = DateTime.Now;
            det1.ScheduleStartTime = DateTime.Now.ToString("HH:mm");
            det1.CompletionDate = det1.ScheduleStartDate;
            det1.CompletionTime = det1.ScheduleStartTime;
            string userId = HttpContext.Current.User.Identity.Name;
            C2.BizResult result = bz.update(userId);
            if (result.status)
            {
                status = true;
                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isWeb();
                elog.Controller = userId;
                elog.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, -1, "Container Ready Export");
                elog.log();
            }
        }
        Common.WriteJson(status, Common.StringToJson(""));
    }



    [WebMethod]
    public void container_PortnetStatusChange()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        //string JobNo = SafeValue.SafeString(info["JobNo"]);
        int det1Id = SafeValue.SafeInt(info["det1Id"], 0);
        string PortnetStatus = SafeValue.SafeString(info["status"]);

        C2.CtmJobDet1Biz bz = new C2.CtmJobDet1Biz(det1Id);
        C2.CtmJobDet1 det1 = bz.getData();
        bool status = false;

        if (det1 != null)
        {
            det1.PortnetStatus = PortnetStatus;
            string userId = HttpContext.Current.User.Identity.Name;
            C2.BizResult result = bz.update(userId);
            if (result.status)
            {
                status = true;
                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isWeb();
                elog.Controller = userId;
                elog.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, -1, "Container Portnet status:"+PortnetStatus);
                elog.log();
            }
        }
        Common.WriteJson(status, Common.StringToJson(""));
    }



    [WebMethod]
    public void email_send()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        int Det1Id = SafeValue.SafeInt(info["Det1Id"],0);
        string emailTo = SafeValue.SafeString(info["emailTo"]);
        string emailCc = SafeValue.SafeString(info["emailCc"]);
        string emailSubject = SafeValue.SafeString(info["emailSubject"]);
        string emailContent = SafeValue.SafeString(info["emailContent"]);
        emailContent = emailContent.Replace("\r\n", "<br/>");

        Helper.Email.SendEmail(emailTo, emailCc, "", emailSubject, emailContent, "");

        C2.CtmJobDet1Biz bz = new C2.CtmJobDet1Biz(Det1Id);
        C2.CtmJobDet1 det1 = bz.getData();
        if (det1 != null)
        {
            det1.EmailInd = "Y";
            bz.update(HttpContext.Current.User.Identity.Name);
        }
        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Controller = HttpContext.Current.User.Identity.Name;
        lg.setActionLevel(Det1Id, CtmJobEventLogRemark.Level.Container, -1);
        lg.Remark = "Sent e-mail:" + emailCc + ", Cc:" + emailCc;
		lg.ActionType = "EMAIL";
        lg.log();
        Common.WriteJson(true, Common.StringToJson(""));
    }
	[WebMethod]
    public void email_generateFormat()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        int tripId = SafeValue.SafeInt(info["tripId"], 0);
        Common.WriteJson(true, email_generateFormat_part1(tripId));
    }
	[WebMethod]
    public string email_generateFormat1(int tripId)
    {
        return email_generateFormat_part1(tripId);
    }
    private string email_generateFormat_part1(int tripId)
    {

        string emailSubject = "";
        string emailContent = "";

        string sql = string.Format(@"select det2.JobNo,det2.TripCode,job.ClientRefNo,det1.ContainerNo,job.Vessel,
job.Voyage,job.JobType,
det1.ContainerType,det1.Br,det1.SealNo,det1.ContWeight,
det2.ToCode,det2.ChessisCode
from ctm_jobdet2 as det2
left outer join ctm_jobdet1 as det1 on det2.Det1Id=det1.Id
left outer join ctm_job as job on job.JobNo=det2.JobNo
where det2.Id=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count > 0)
        {
            string JobNo = SafeValue.SafeString(dt.Rows[0]["JobNo"]);
            string ClientRefNo = SafeValue.SafeString(dt.Rows[0]["ClientRefNo"]);
            string ContainerNo = SafeValue.SafeString(dt.Rows[0]["ContainerNo"]);
            string Vessel = SafeValue.SafeString(dt.Rows[0]["Vessel"]);

            string Voyage = SafeValue.SafeString(dt.Rows[0]["Voyage"]);
            string JobType = SafeValue.SafeString(dt.Rows[0]["JobType"]);
            string TripCode = SafeValue.SafeString(dt.Rows[0]["TripCode"]);
            string ContainerType = SafeValue.SafeString(dt.Rows[0]["ContainerType"]);
            string Br = SafeValue.SafeString(dt.Rows[0]["Br"]);
            string SealNo = SafeValue.SafeString(dt.Rows[0]["SealNo"]);
            decimal ContWeight = SafeValue.SafeDecimal(dt.Rows[0]["ContWeight"]);
            string ToCode = SafeValue.SafeString(dt.Rows[0]["ToCode"]);
            string ChessisCode = SafeValue.SafeString(dt.Rows[0]["ChessisCode"]);

			ToCode = ToCode.Replace("\r\n","\\r\\n").Replace("\n","\\n").Replace("\r","\\r");
			
            switch (TripCode)
            {
                case "IMP":
                    emailSubject = JobNo + "/" + ClientRefNo + "/" + ContainerNo;
                    emailContent = string.Format(@"OUR REF: {0}\r\nCLIENT REF: {1}\r\nVESSEL/VOY: {2}/{3}\r\nCNTR NO/SIZE: {4}/{5}\r\nDELIVERY LOCATION: {6}\r\nTRAILER NO: {7}\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORMED THAT OUR DRIVER IS NOW ON HIS WAY TO PSA TO COLLECT THE ABOVE MENTIONED CONTAINER.\r\n\r\nKINDLY PREPARE THE ASSIGNED PARKING LOT TO ACCEPT THE CONTAINER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD", JobNo, ClientRefNo, Vessel, Voyage, ContainerNo, ContainerType, ToCode, ChessisCode);
                    break;
                case "RET":
                    emailSubject = JobNo + "/" + ClientRefNo + "/" + ContainerNo;
                    emailContent = string.Format(@"OUR REF: {0}\r\nCLIENT REF: {1}\r\nVESSEL/VOY: {2}/{3}\r\nCNTR NO/SIZE: {4}/{5}\r\nDELIVERY LOCATION: {6}\r\nTRAILER NO: {7}\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORMED THAT OUR DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION TO COLLECT THE EMPTY CONTAINER FOR RETURN. \r\n\r\nKINDLY ENSURE THAT CONTAINER IS NOW EMPTY & THERE WILL BE NO BLOCKAGE FOR OUR DRIVER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD", JobNo, ClientRefNo, Vessel, Voyage, ContainerNo, ContainerType, ToCode, ChessisCode);
                    break;
                case "EXP":
                    emailSubject = ClientRefNo + "/" + JobNo + "/" + Vessel;
                    emailContent = string.Format(@"OUR REF: {0}\r\nCLIENT REF: {1}\r\nVESSEL/VOY: {2}/{3}\r\nBKG REF: {4}\r\nCNTR NO/SEAL NO/TARE WEIGHT/SIZE: {5}/{6}/{7}/{8}\r\nLOCATION: {9}\r\nTRAILER NO: {10}\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORMED THAT DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION TO COLLECT THE LOADED CONTAINER FOR EXPORT. \r\n\r\nKINDLY ENSURE THAT THERE IS NO BLOCKAGE FOR OUR DRIVER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD", JobNo, ClientRefNo, Vessel, Voyage, Br, ContainerNo, SealNo, ContWeight, ContainerType, ToCode, ChessisCode);
                    break;
                case "COL":
                    emailSubject = ClientRefNo + "/" + JobNo + "/" + Vessel;
                    emailContent = string.Format(@"OUR REF: {0}\r\nCLIENT REF: {1}\r\nVESSEL/VOY: {2}/{3}\r\nBKG REF: {4}\r\nCNTR NO/SEAL NO/TARE WEIGHT/SIZE: {5}/{6}/{7}/{8}\r\nLOCATION: {9}\r\nTRAILER NO: {10}\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORMED THAT DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION. \r\n\r\nKINDLY ENSURE THAT THE PARKING LOT IS CLEARED FOR OUR DRIVER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD", JobNo, ClientRefNo, Vessel, Voyage, Br, ContainerNo, SealNo, ContWeight, ContainerType, ToCode, ChessisCode);
                    break;
                default:
                    break;
            }
        }
/*		
	COL
\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORM THAT DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION. KINDLY ENSURE THAT\r\n\r\nTHE PARKING LOT IS CLEARED FOR OUR DRIVER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT

EXP
DEAR CUSTOMER
PLEASE BE INFORM THAT DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION TO COLLECT THE LOADED CONTAINER FOR EXPORT. KINDLY ENSURE THAT THERE IS NO BLOCKAGE FOR OUR DRIVER.
THANK YOU FOR YOUR SUPPORT
IMP
\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORM THAT OUR DRIVER IS NOW ON HIS WAY TO PSA TO COLLECT THE ABOVE MENTIONED CONTAINER.\r\n\r\nKINDLY PREPARE THE ASSIGNED PARKING LOT TO ACCEPT THE CONTAINER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD
RET
\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORM THAT OUR DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION TO COLLECT THE EMPTY CONTAINER FOR RETURN. \r\n\r\nKINDLY ENSURE THAT CONTAINER IS NOW EMPTY & THERE WILL BE NO BLOCKAGE FOR OUR DRIVER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT
		
		
*/
        string context = string.Format("{0}\"emailSubject\":\"{2}\",\"emailContent\":\"{3}\"{1}", "{", "}", emailSubject, emailContent);
        return context;
    }


}