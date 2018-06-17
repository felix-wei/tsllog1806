<%@ WebService Language="C#" Class="WebService_UpdateTrips" %>

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
public class WebService_UpdateTrips : System.Web.Services.WebService
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
        string locked = "";
        if (info["locked"] != null)
            locked = SafeValue.SafeString(info["locked"], "");
        curPage = curPage <= 0 ? 1 : curPage;
        int pageSize = 0;
        if (!str_pageSize.Equals("ALL"))
        {
            pageSize = SafeValue.SafeInt(str_pageSize, 0);
        }
        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string No = SafeValue.SafeString(info["ContNo"]);
        string type = SafeValue.SafeString(info["type"], "IMP");
        string tripStatus = SafeValue.SafeString(info["tripStatus"], "All");
        string allPending = SafeValue.SafeString(info["allPending"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ToDate", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@TripCode", type, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@tripStatus", tripStatus, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));

        string sql = "";
        string sql_inner = string.Format(@"select det2.Id,job.JobNo,convert(nvarchar(10),det2.FromDate,120) as ScheduleDate,TripCode,0 rowId1
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where ");
        string sql_inner_1 = "";
        string sql_innder_1_where = "";


        string sql_where = "1=1";
        if (type.Equals("ALL"))
        {
            sql_where = "det2.det1Id>0";
        }
        else
        {
            sql_where = "det2.TripCode=@TripCode";
        }
        if (No.Length == 0)
        {

            if (allPending == "YES")
            {
                sql_innder_1_where = sql_where;
                sql_innder_1_where += " and (det2.FromDate<@FromDate or det2.FromDate>=@ToDate) and det2.Statuscode<>'C' and det2.FromDate>'20000101' ";
                sql_inner_1 = string.Format(@"
union all
select det2.Id,job.JobNo,convert(nvarchar(10),det2.FromDate,120) as ScheduleDate,TripCode,1 rowId1
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where ");
            }

            //===== normal
            sql_where += " and det2.FromDate>=@FromDate and det2.FromDate<@ToDate";

            if (tripStatus != "All")
            {
                switch (tripStatus)
                {
                    case "NotCompleted":
                        sql_where += " and det2.Statuscode<>'C'";
                        break;
                    case "P":
                        sql_where += " and (det2.Statuscode='P' or det2.Statuscode='S')";
                        break;
                    default:
                        sql_where += " and det2.Statuscode=@tripStatus";
                        break;

                }
            }
        }
        else
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or det1.ContainerNo like @No)";
        }

        sql_inner = sql_inner + sql_where + sql_inner_1 + sql_innder_1_where;

        sql = string.Format(@"select count(*) from ({0}) as temp", sql_inner);
        int totalItems = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        int totalPages = pageSize > 0 ? (totalItems / pageSize + (totalItems % pageSize > 0 ? 1 : 0)) : 1;
        curPage = curPage > totalPages ? totalPages : curPage;
        list.Add(new ConnectSql_mb.cmdParameters("@currenPage", pageSize * (curPage - 1), SqlDbType.Int));



        sql = string.Format(@"select t.*,det2.Det1Id,
det2.FromTime,det2.TowheadCode,det2.ChessisCode,det2.TripCode,det2.DriverCode,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim,
job.ClientId,det1.ContainerNo,det1.SealNo,det2.FromCode,det2.ToCode,det1.ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
(case when job.JobStatus='Billing' and (select count(*) as Cnt from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where mast.MastRefNo=job.JobNo)=0 
then 'UnBill' else job.JobStatus end) as JobStatus,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,det1.Weight,det2.Statuscode,job.Remark as JobRemark,
(select cast(ts.Id as nvarchar)+'|'+ts.TripCode+'|'+ts.Statuscode+',' from CTM_JobDet2 as ts where ts.Det1Id=det2.Det1Id order by (case ts.TripCode when 'IMP' then 1 when 'COL' then 1 else 2 end),FromDate,FromTime for xml path('')) as trips
from (
select top {0} * from (
select top {0} * from (

select ROW_NUMBER()over(order by rowId1,ScheduleDate,TripCode,JobNo) as rowId,* 
from (
{1}
) as temp

) as t
where rowId>@currenPage
order by rowId
) as t
order by rowId desc
) as t
left outer join CTM_JobDet2 as det2 on t.Id=det2.Id
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
order by rowId", pageSize, sql_inner);
        //throw new Exception(sql);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"list\":{2},\"curPage\":{3},\"totalPages\":{4},\"totalItems\":{5}{1}", "{", "}", DataList, curPage, totalPages, totalItems);
        Common.WriteJson(true, context);
    }


    [WebMethod]
    public void RefreshList_ByTripId()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Ids = SafeValue.SafeString(info["Ids"], "0");
        string sql = string.Format(@"select det2.Id,job.JobNo,convert(nvarchar(10),det2.FromDate,120) as ScheduleDate,det2.Det1Id,
det2.FromTime,det2.TowheadCode,det2.ChessisCode,det2.TripCode,det2.DriverCode,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as Claim,
job.ClientId,det1.ContainerNo,det1.SealNo,det2.FromCode,det2.ToCode,det1.ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
(case when job.JobStatus='Billing' and (select count(*) as Cnt from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where mast.MastRefNo=job.JobNo)=0 
then 'UnBill' else job.JobStatus end) as JobStatus,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,det1.Weight,det2.Statuscode,job.Remark as JobRemark,
(select cast(ts.Id as nvarchar)+'|'+ts.TripCode+'|'+ts.Statuscode+',' from CTM_JobDet2 as ts where ts.Det1Id=det2.Det1Id order by (case ts.TripCode when 'IMP' then 1 when 'COL' then 1 else 2 end),FromDate,FromTime for xml path('')) as trips
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id in ({0})", Ids);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }






    [WebMethod]
    public void List_GetData_ByPage_170313()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int curPage = SafeValue.SafeInt(info["curPage"], 0);
        string str_pageSize = info["pageSize"].ToString();
        string locked = "";
        if (info["locked"] != null)
            locked = SafeValue.SafeString(info["locked"], "");
        curPage = curPage <= 0 ? 1 : curPage;
        int pageSize = 0;
        if (!str_pageSize.Equals("ALL"))
        {
            pageSize = SafeValue.SafeInt(str_pageSize, 0);
        }
        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string No = SafeValue.SafeString(info["ContNo"]);
        string type = SafeValue.SafeString(info["type"], "ALL");
        string tripStatus = SafeValue.SafeString(info["tripStatus"], "All");
        string allPending = SafeValue.SafeString(info["allPending"]);
        string Client = SafeValue.SafeString(info["Client"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ToDate", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@TripCode", type, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@tripStatus", tripStatus, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));

        string sql = "";
        string sql_inner = string.Format(@"select det2.Id,job.JobNo,convert(nvarchar(10),det2.FromDate,120) as ScheduleDate,TripCode,0 rowId1
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where ");
        string sql_inner_1 = "";
        string sql_innder_1_where = "";


        //string sql_where = "job.StatusCode='USE' and job.JobStatus<>'Voided'";
        string sql_where = "1=1";
        string sql_tripsWhere = "";
        if (type.IndexOf("ALL") >= 0)
        {
            sql_tripsWhere = " and ts.Id=det2.Id";
            if (type.Equals("ALL_TPT"))
            {
                sql_where += " and det2.TripCode in ('TPT','LOC')";
            }
            if (type.Equals("ALL_CONT"))
            {
                sql_where += " and det2.TripCode in ('IMP','RET','COL','EXP','SMT','SLD')";
            }
        }
        else
        {
            sql_where += " and det2.TripCode=@TripCode";
        }
        if (Client.Length > 0)
        {
            sql_where += " and job.ClientId=@Client";
        }
        if (No.Length == 0)
        {

            if (allPending == "YES")
            {
                sql_innder_1_where = sql_where;
                sql_innder_1_where += " and (det2.FromDate<@FromDate or det2.FromDate>=@ToDate) and det2.Statuscode<>'C' and det2.FromDate>'20000101' ";
                sql_inner_1 = string.Format(@"
union all
select det2.Id,job.JobNo,convert(nvarchar(10),det2.FromDate,120) as ScheduleDate,TripCode,1 rowId1
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where ");
            }

            //===== normal
            sql_where += " and det2.FromDate>=@FromDate and det2.FromDate<@ToDate";

            if (tripStatus != "All")
            {
                switch (tripStatus)
                {
                    case "NotCompleted":
                        sql_where += " and det2.Statuscode<>'C'";
                        break;
                    case "P":
                        sql_where += " and (det2.Statuscode='P' or det2.Statuscode='S')";
                        break;
                    default:
                        sql_where += " and det2.Statuscode=@tripStatus";
                        break;

                }
            }
        }
        else
        {
            sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or det1.ContainerNo like @No or det2.ContainerNo like @No)";
        }

        sql_inner = sql_inner + sql_where + sql_inner_1 + sql_innder_1_where;

        sql = string.Format(@"select count(*) from ({0}) as temp", sql_inner);
        int totalItems = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        int totalPages = pageSize > 0 ? (totalItems / pageSize + (totalItems % pageSize > 0 ? 1 : 0)) : 1;
        curPage = curPage > totalPages ? totalPages : curPage;
        list.Add(new ConnectSql_mb.cmdParameters("@currenPage", pageSize * (curPage - 1), SqlDbType.Int));



        sql = string.Format(@"select t.*,det2.Det1Id,
det2.FromTime,det2.TowheadCode,det2.ChessisCode,det2.DriverCode,job.ClientId,det1.SealNo,job.JobStatus as jobStatus,
(case when det2.TripCode='TPT' then det2.ContainerNo else det1.ContainerNo end) as ContainerNo,
(case when det2.TripCode='TPT' then det2.PermitNo else job.PermitNo end) as PermitNo,
(case when det2.TripCode='TPT' then det2.ClientRefNo else job.CarrierBkgNo end) as SEFNO,
det2.FromCode,det2.ToCode,det1.ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,det2.BillingRemark,det2.ServiceType,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,det1.Weight,det2.Statuscode,job.Remark as JobRemark,
job.CarrierId,det1.YardAddress,det1.WhsReadyInd,det1.WhsReadyLocation,det1.WhsReadyTime,det1.WhsReadyWeight,
job.WarehouseAddress as shipper,det1.Br as PickupRefNo,det1.TerminalLocation,convert(nvarchar,job.EtdDate,103) as EtdDate,job.EtdTime as EtdTime,
job.ClientContact as orderBy,det1.Remark as ContRemark,det1.Remark1 as ContRemark1,det1.Remark2 as ClientRemark,det1.ReleaseToHaulierRemark,job.PortnetRef,job.ReturnLastDate,
(select top 1 Tel from ref_contact as contact where contact.Name=job.ClientContact and contact.PartyId=job.partyId) as orderByTel,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as c1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as c3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as c5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustBill') as b_inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustOT') as b_inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustPermit') as b_inc3,
isnull((select sum(isnull(amount,0)) from psa_bill where JOB_NO=det2.JobNo),0) as psa,
(select sum(DocAmt) from XAArInvoice where MastRefNo=job.JobNo and DocType='IV') as InvAmt,
(select cast(ts.Id as nvarchar)+'|'+ts.TripCode+'|'+ts.Statuscode+'|'+isnull(ts.DriverCode,'')+'|'+ts.TripCode+' '+isnull(convert(nvarchar(5),FromDate,103),'')+' '+isnull(ts.FromTime,'00:00')+'|'+isnull(ts.SubCon_Ind,'N')+'|'+isnull(ts.SubCon_Code,'')+',' 
from CTM_JobDet2 as ts where ((ts.Det1Id=det2.Det1Id and det2.Det1Id>0) or ts.Id=det2.Id) {2} order by (case ts.TripCode when 'IMP' then 1 when 'COL' then 1 else 2 end),FromDate,FromTime for xml path('')) as trips
from (
select top {0} * from (
select top {0} * from (

select ROW_NUMBER()over(order by rowId1,ScheduleDate,TripCode,JobNo) as rowId,* 
from (
{1}
) as temp

) as t
where rowId>@currenPage
order by rowId
) as t
order by rowId desc
) as t
left outer join CTM_JobDet2 as det2 on t.Id=det2.Id
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
order by rowId", pageSize, sql_inner, sql_tripsWhere);
        //throw new Exception(sql);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"list\":{2},\"curPage\":{3},\"totalPages\":{4},\"totalItems\":{5}{1}", "{", "}", DataList, curPage, totalPages, totalItems);
        Common.WriteJson(true, context);
    }

    [WebMethod]
    public void RefreshList_ByTripId_170313()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Ids = SafeValue.SafeString(info["Ids"], "0");
        string type = SafeValue.SafeString(info["type"], "ALL");
        string sql_tripsWhere = "";
        if (type.Equals("ALL"))
        {
            sql_tripsWhere = " and ts.Id=det2.Id";
        }

        string sql = string.Format(@"select det2.Id,job.JobNo,convert(nvarchar(10),det2.FromDate,120) as ScheduleDate,det2.Det1Id,
det2.FromTime,det2.TowheadCode,det2.ChessisCode,det2.TripCode,det2.DriverCode,job.ClientId,det1.SealNo,job.JobStatus as jobStatus,
(case when det2.TripCode='TPT' then det2.ContainerNo else det1.ContainerNo end) as ContainerNo,
(case when det2.TripCode='TPT' then det2.PermitNo else job.PermitNo end) as PermitNo,
(case when det2.TripCode='TPT' then det2.ClientRefNo else job.CarrierBkgNo end) as SEFNO,
det2.FromCode,det2.ToCode,det1.ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,det2.BillingRemark,det2.ServiceType,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,job.SpecialInstruction,det2.Remark as TripInstruction,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,det1.Weight,det2.Statuscode,job.Remark as JobRemark,
job.CarrierId,det1.YardAddress,det1.WhsReadyInd,det1.WhsReadyLocation,det1.WhsReadyTime,det1.WhsReadyWeight,
job.WarehouseAddress as shipper,det1.Br as PickupRefNo,det1.TerminalLocation,convert(nvarchar,job.EtdDate,103) as EtdDate,job.EtdTime as EtdTime,job.ClientContact as orderBy,det1.Remark as ContRemark,det1.Remark1 as ContRemark1,det1.Remark2 as ClientRemark,det1.ReleaseToHaulierRemark,job.PortnetRef,job.ReturnLastDate,
(select top 1 Tel from ref_contact as contact where contact.Name=job.ClientContact) as orderByTel,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as c1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as c3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as c5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustBill') as b_inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustOT') as b_inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustPermit') as b_inc3,
isnull((select sum(isnull(amount,0)) from psa_bill where JOB_NO=det2.JobNo),0) as psa,
(select sum(DocAmt) from XAArInvoice where MastRefNo=job.JobNo and DocType='IV') as InvAmt,
(select cast(ts.Id as nvarchar)+'|'+ts.TripCode+'|'+ts.Statuscode+'|'+isnull(ts.DriverCode,'')+'|'+ts.TripCode+' '+isnull(convert(nvarchar(5),FromDate,103),'')+' '+isnull(ts.FromTime,'00:00')+'|'+isnull(ts.SubCon_Ind,'N')+'|'+isnull(ts.SubCon_Code,'')+',' 
from CTM_JobDet2 as ts where ts.Det1Id=det2.Det1Id {1} order by (case ts.TripCode when 'IMP' then 1 when 'COL' then 1 else 2 end),FromDate,FromTime for xml path('')) as trips
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id in ({0})", Ids, sql_tripsWhere);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }


    [WebMethod]
    public void List_save2Excel()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string No = SafeValue.SafeString(info["ContNo"]);
        string type = SafeValue.SafeString(info["type"], "ALL");
        string tripStatus = SafeValue.SafeString(info["tripStatus"], "All");
        string allPending = SafeValue.SafeString(info["allPending"]);
        string Client = SafeValue.SafeString(info["Client"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ToDate", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@TripCode", type, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@tripStatus", tripStatus, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%" + No + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));

        string sql_inner = "";
        string sql_where = "1=1";
        if (Client.Length > 0)
        {
            sql_where += " and job.ClientId=@Client";
        }

        if (type.Equals("ALL_TPT"))
        {
            sql_where += " and det2.TripCode in ('TPT','LOC')";
            sql_inner = string.Format(@"select ROW_NUMBER()over(order by ScheduleDate,TripCode,job.JobNo) as rowId,
det2.Id,job.JobNo,convert(nvarchar(10),job.JobDate,120) as JobDate,convert(nvarchar(10),det2.FromDate,120) as ScheduleDate,TripCode,
det2.FromDate as BookingDate,det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.ManualDo,det2.BillingRemark,det2.ServiceType,job.SpecialInstruction,det2.FromCode as PickupFrom,det2.ToCode as DeliveryTo,
job.EtaDate,job.EtaTime,job.EtdDate,job.EtdTime,det2.ContainerNo as ConTanNo1,det2.Remark1 as DriverRemark,det2.WarehouseRemark,
job.Vessel,job.Voyage,job.ReturnLastDate,
(select top 1 Name from xxparty where PartyId=job.CarrierId) as CarrierName,job.CarrierId,
(case when det2.TripCode='TPT' then det2.ClientRefNo else job.CarrierBkgNo end) as SEFNO,
(case when det2.TripCode='TPT' then det2.ContainerNo else det1.ContainerNo end) as ConTanNo,
(case when det2.TripCode='TPT' then '' else det1.SealNo end) as SealNo,
(case when det2.TripCode='TPT' then '' else det1.BR end) as pickupRefNo,
(case when det2.TripCode='TPT' then '' else isnull(det1.ContainerType,'')+'/'+ isnull(cast(det1.ContWeight as nvarchar),'') end) as ContWeight,
(case when det2.TripCode='TPT' then det2.PermitNo else job.PermitNo end) as PermitNo,
(case when det2.TripCode='TPT' then job.Remark else det1.Remark1 end) as ContRemark,
(case when det2.TripCode='TPT' then job.YardRef else det1.YardAddress end) as DepotAddress,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustBill') as b_inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustOT') as b_inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='TRIP' and ChgCode='CustPermit') as b_inc3
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where ");


            if (No.Length == 0)
            {

                //===== normal
                sql_where += " and det2.FromDate>=@FromDate and det2.FromDate<@ToDate";

                if (tripStatus != "All")
                {
                    switch (tripStatus)
                    {
                        case "NotCompleted":
                            sql_where += " and det2.Statuscode<>'C'";
                            break;
                        case "P":
                            sql_where += " and (det2.Statuscode='P' or det2.Statuscode='S')";
                            break;
                        default:
                            sql_where += " and det2.Statuscode=@tripStatus";
                            break;

                    }
                }
            }
            else
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or det1.ContainerNo like @No or det2.ContainerNo like @No)";
            }
        }
        if (type.Equals("ALL_CONT"))
        {
            //sql_where += " and job.JobType in ('IMP','EXP') and det1.ScheduleDate>=@FromDate and det1.ScheduleDate<@ToDate";
            sql_where += " and det1.Id in (select distinct det1Id from ctm_jobdet2 as det2 where det2.TripCode in ('IMP','RET','EXP','COL','SLD','SMT') and det2.FromDate>=@FromDate and det2.FromDate<@ToDate )";
            sql_inner = string.Format(@"select det1.Id as contId,job.JobNo,job.JobDate,
job.CarrierId,det1.BR as pickupRefNo,det1.ContainerNo as ConTanNo,det1.SealNo,
isnull(det1.ContainerType,'')+'/'+ isnull(cast(det1.Weight as nvarchar),'') as ContWeight,
isnull(job.PermitNo,'')+'/'+isnull((select PermitNo+',' from ref_permit where JobNo=job.JobNo for xml path('')),'') as PermitNo,
job.Vessel,job.Voyage,job.EtaDate,job.EtaTime,job.EtdDate,job.EtdTime,
job.PickupFrom as PickupFrom,job.DeliveryTo as DeliveryTo,det1.YardAddress as DepotAddress,job.ReturnLastDate,
det1.ScheduleDate,job.JobDate as BookingDate,det1.Remark1 as ContRemark,isnull(det1.PortnetStatus,'') as PortnetStatus
from ctm_jobdet1 as det1
left outer join ctm_job as job on det1.JobNo=job.JobNo
where ");


            if (No.Length == 0)
            {

                //===== normal
                sql_where += "";

                if (tripStatus != "All")
                {
                    switch (tripStatus)
                    {
                        case "NotCompleted":
                        case "P":
                            sql_where += " and det1.StatusCode<>'Completed'";
                            break;
                        case "C":
                            sql_where += " and det1.StatusCode='Completed'";
                            break;
                        default:
                            sql_where += " and det1.StatusCode=@tripStatus";
                            break;

                    }
                }
            }
            else
            {
                sql_where += " and (job.JobNo like @No or job.ClientRefNo like @No or det1.ContainerNo like @No)";
            }
        }

        string sql_orderBy = " order by BookingDate";

        DataTable dt = new DataTable();
        if (sql_inner.Length > 0)
        {
            string sql_temp = sql_inner + sql_where + sql_orderBy;
            //throw new Exception(sql_temp);
            dt = ConnectSql_mb.GetDataTable(sql_temp, list);
        }

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
        string sql_party = "select Name,Address from xxparty where PartyId=@Client";
        DataTable dt_party = ConnectSql_mb.GetDataTable(sql_party, list);
        if (dt_party.Rows.Count > 0)
        {
            ws.Cells[baseRow++, 1].PutValue(dt_party.Rows[0]["Name"]);
            ws.Cells[baseRow++, 1].PutValue(dt_party.Rows[0]["Address"]);
        }
        //baseRow = 0;
        //ws.Cells[baseRow, 5].PutValue("Description");
        //ws.Cells[baseRow++, 6].PutValue("Amount");
        //ws.Cells[baseRow, 5].PutValue("Current Charges");
        //ws.Cells[baseRow++, 6].PutValue(0);
        //ws.Cells[baseRow, 5].PutValue("Total Amount Due");
        //ws.Cells[baseRow++, 6].PutValue(0);
        //ws.Cells[baseRow, 5].PutValue("Please Pay:");
        //ws.Cells[baseRow++, 6].PutValue(0);
        //baseRow = baseRow + 2;
        baseRow = 6;


        if (type.Equals("ALL_TPT"))
        {
            ws.Cells[baseRow, 0].PutValue("S/N");
            ws.Cells[baseRow, 1].PutValue("Date");
            ws.Cells[baseRow, 2].PutValue("SEFNO");
            ws.Cells[baseRow, 3].PutValue("Con.No.");
            ws.Cells[baseRow, 4].PutValue("VESSEL");
            ws.Cells[baseRow, 5].PutValue("ETA/ETD");
            ws.Cells[baseRow, 6].PutValue("Start&End DateTime");
            ws.Cells[baseRow, 7].PutValue("From Address");
            ws.Cells[baseRow, 8].PutValue("To Address");
            ws.Cells[baseRow, 9].PutValue("Container/Tanker No");
            ws.Cells[baseRow, 10].PutValue("Service");
            ws.Cells[baseRow, 11].PutValue("Amount($)");
            ws.Cells[baseRow, 12].PutValue("OT($)");
            ws.Cells[baseRow, 13].PutValue("Permit($)");
            ws.Cells[baseRow, 14].PutValue("Driver Remark");
            ws.Cells[baseRow, 15].PutValue("Cargo Remark");

            baseRow++;
            int i = 0;
            for (; i < dt.Rows.Count;)
            {
                ws.Cells[baseRow + i, 0].PutValue(i + 1);
                ws.Cells[baseRow + i, 1].PutValue(SafeValue.SafeDate(dt.Rows[i]["BookingDate"], new DateTime(1753, 1, 1)).ToString("yyyy-MM-dd"));
                ws.Cells[baseRow + i, 2].PutValue(dt.Rows[i]["SEFNO"]);
                ws.Cells[baseRow + i, 3].PutValue(dt.Rows[i]["ManualDo"]);
                ws.Cells[baseRow + i, 4].PutValue(SafeValue.SafeString(dt.Rows[i]["Vessel"]));
                DateTime temp_date = SafeValue.SafeDate(dt.Rows[i]["EtaDate"], new DateTime(1753, 1, 1));
                string temp_string = "";
                //if (temp_date.Year > 2000)
                {
                    temp_string = temp_date.ToString("yyyy/MM/dd");
                    temp_string = temp_string + " " + SafeValue_mb.convertTimeFormat(dt.Rows[i]["EtaTime"].ToString());
                }
                temp_date = SafeValue.SafeDate(dt.Rows[i]["EtdDate"], new DateTime(1753, 1, 1));
                temp_string = temp_string + " - ";
                //if (temp_date.Year>2000)
                {
                    temp_string = temp_string + temp_date.ToString("yyyy/MM/dd");
                    temp_string = temp_string + " " + SafeValue_mb.convertTimeFormat(dt.Rows[i]["EtdTime"].ToString());
                }
                ws.Cells[baseRow + i, 5].PutValue(temp_string);
                ws.Cells[baseRow + i, 6].PutValue(SafeValue.SafeDate(dt.Rows[i]["FromDate"], new DateTime(1753, 1, 1)).ToString("yyyy/MM/dd") + " " + SafeValue.SafeString(dt.Rows[i]["FromTime"]) + " - " + SafeValue.SafeDate(dt.Rows[i]["ToDate"], new DateTime(1753, 1, 1)).ToString("yyyy/MM/dd") + " " + SafeValue.SafeString(dt.Rows[i]["ToTime"]));
                ws.Cells[baseRow + i, 7].PutValue(dt.Rows[i]["PickupFrom"]);
                ws.Cells[baseRow + i, 8].PutValue(dt.Rows[i]["DeliveryTo"]);
                ws.Cells[baseRow + i, 9].PutValue(dt.Rows[i]["ConTanNo"]);
                ws.Cells[baseRow + i, 10].PutValue(dt.Rows[i]["ServiceType"]);
                ws.Cells[baseRow + i, 11].PutValue(dt.Rows[i]["b_inc1"]);
                ws.Cells[baseRow + i, 12].PutValue(dt.Rows[i]["b_inc2"]);
                ws.Cells[baseRow + i, 13].PutValue(dt.Rows[i]["b_inc3"]);
                ws.Cells[baseRow + i, 14].PutValue(dt.Rows[i]["DriverRemark"]);
                ws.Cells[baseRow + i, 15].PutValue(dt.Rows[i]["WarehouseRemark"]);

                i++;
            }
        }

        if (type.Equals("ALL_CONT"))
        {
            ws.Cells[baseRow, 0].PutValue("S/N");
            ws.Cells[baseRow, 1].PutValue("JOB NUMBER");
            ws.Cells[baseRow, 2].PutValue("RECEIVED DATE");
            ws.Cells[baseRow, 3].PutValue("CARRIER");
            ws.Cells[baseRow, 4].PutValue("CONTAINER/TANKER NO");
            ws.Cells[baseRow, 5].PutValue("SEAL NO");
            ws.Cells[baseRow, 6].PutValue("SIZE/WEIGHT");
            ws.Cells[baseRow, 7].PutValue("PERMIT");
            ws.Cells[baseRow, 8].PutValue("VESSEL/VOYAGE");
            ws.Cells[baseRow, 9].PutValue("ETA/ETD");
            ws.Cells[baseRow, 10].PutValue("From Address");
            ws.Cells[baseRow, 11].PutValue("To Address");
            ws.Cells[baseRow, 12].PutValue("SCHEDULE DATE/TIME");
            ws.Cells[baseRow, 13].PutValue("DEPOT/LAST DATE");
            ws.Cells[baseRow, 14].PutValue("PortnetStatus");
            ws.Cells[baseRow, 15].PutValue("REMARK");
            ////ws.Cells[baseRow, 15].PutValue("Service");
            ////ws.Cells[baseRow, 16].PutValue("Amount($)");
            ////ws.Cells[baseRow, 17].PutValue("OT($)");
            ////ws.Cells[baseRow, 18].PutValue("Permit($)");
            ////ws.Cells[baseRow, 19].PutValue("Driver Remark");
            ////ws.Cells[baseRow, 20].PutValue("Cargo Remark");

            baseRow++;
            int i = 0;
            for (; i < dt.Rows.Count;)
            {
                ws.Cells[baseRow + i, 0].PutValue(i + 1);

                ws.Cells[baseRow + i, 1].PutValue(dt.Rows[i]["JobNo"]);
                ws.Cells[baseRow + i, 2].PutValue(SafeValue.SafeDate(dt.Rows[i]["JobDate"], new DateTime(1753, 1, 1)).ToString("yyyy/MM/dd"));
                ws.Cells[baseRow + i, 3].PutValue(dt.Rows[i]["CarrierId"] + "/" + dt.Rows[i]["pickupRefNo"]);
                ws.Cells[baseRow + i, 4].PutValue(dt.Rows[i]["ConTanNo"]);
                ws.Cells[baseRow + i, 5].PutValue(dt.Rows[i]["SealNo"]);
                ws.Cells[baseRow + i, 6].PutValue(dt.Rows[i]["ContWeight"]);
                ws.Cells[baseRow + i, 7].PutValue(dt.Rows[i]["PermitNo"]);
                ws.Cells[baseRow + i, 8].PutValue(dt.Rows[i]["Vessel"] + "/" + dt.Rows[i]["Voyage"]);
                DateTime temp_date = SafeValue.SafeDate(dt.Rows[i]["EtaDate"], new DateTime(1753, 1, 1));
                string temp_string = "";
                //if (temp_date.Year > 2000)
                {
                    temp_string = temp_date.ToString("yyyy/MM/dd");
                    temp_string = temp_string + " " + SafeValue_mb.convertTimeFormat(dt.Rows[i]["EtaTime"].ToString());
                }
                temp_date = SafeValue.SafeDate(dt.Rows[i]["EtdDate"], new DateTime(1753, 1, 1));
                temp_string = temp_string + " - ";
                //if (temp_date.Year>2000)
                {
                    temp_string = temp_string + temp_date.ToString("yyyy/MM/dd");
                    temp_string = temp_string + " " + SafeValue_mb.convertTimeFormat(dt.Rows[i]["EtdTime"].ToString());
                }
                ws.Cells[baseRow + i, 9].PutValue(temp_string);
                ws.Cells[baseRow + i, 10].PutValue(dt.Rows[i]["PickupFrom"]);
                ws.Cells[baseRow + i, 11].PutValue(dt.Rows[i]["DeliveryTo"]);
                ws.Cells[baseRow + i, 12].PutValue(SafeValue.SafeDate(dt.Rows[i]["ScheduleDate"], new DateTime(1753, 1, 1)).ToString("yyyy/MM/dd"));
                temp_date = SafeValue.SafeDate(dt.Rows[i]["ReturnLastDate"], new DateTime(1753, 1, 1));
                temp_string = dt.Rows[i]["DepotAddress"] + "/";
                if (temp_date.Year > 2000)
                {
                    temp_string = temp_string + temp_date.ToString("dd.MM.yyyy");
                }
                ws.Cells[baseRow + i, 13].PutValue(temp_string);
                ws.Cells[baseRow + i, 14].PutValue(dt.Rows[i]["PortnetStatus"]);
                ws.Cells[baseRow + i, 15].PutValue(dt.Rows[i]["ContRemark"]);
                //ws.Cells[baseRow + i, 15].PutValue(dt.Rows[i]["ServiceType"]);
                //ws.Cells[baseRow + i, 16].PutValue(dt.Rows[i]["b_inc1"]);
                //ws.Cells[baseRow + i, 17].PutValue(dt.Rows[i]["b_inc2"]);
                //ws.Cells[baseRow + i, 18].PutValue(dt.Rows[i]["b_inc3"]);
                //ws.Cells[baseRow + i, 19].PutValue(dt.Rows[i]["DriverRemark"]);
                //ws.Cells[baseRow + i, 20].PutValue(dt.Rows[i]["WarehouseRemark"]);

                i++;
            }
        }
        wb.Save(to_file);

        string context = Common.StringToJson(Path.Combine("files", "Excel_DailyTrips", "Tpt_" + fileName + ".csv"));
        Common.WriteJson(true, context);
    }


    [WebMethod]
    public void View_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int TripId = SafeValue.SafeInt(info["TripId"], 0);
        string mast = "{}";

        string sql = string.Format(@"select det2.Id,det2.Det1Id,det2.ContainerNo,det2.DriverCode,det2.ChessisCode,det2.Statuscode,
job.JobStatus,job.StatusCode as JobStatusCode,
det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.JobNo,det2.TripCode,
det2.FromCode,det2.ToCode,det2.Remark,det2.TowheadCode,det2.FromParkingLot,det2.ToParkingLot,
det2.BillingRemark,job.ReturnLastDate,job.Id as jobId,det1.YardAddress,job.EmailAddress,det1.EmailInd,det2.Remark1, det2.SubCon_Ind, det2.SubCon_Code,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Trip' and (cc.DriverCode=det2.DriverCode or isnull(cc.DriverCode,'')='')) as inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime' and (cc.DriverCode=det2.DriverCode or isnull(cc.DriverCode,'')='')) as inc2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='Standby' and (cc.DriverCode=det2.DriverCode or isnull(cc.DriverCode,'')='')) as inc3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as cc where TripNo=det2.Id and LineType='DP' and ChgCode='PSA' and (cc.DriverCode=det2.DriverCode or isnull(cc.DriverCode,'')='')) as inc4,
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
left outer join ctm_jobdet1 as det1 on det1.Id=det2.det1Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo 
where det2.Id=@TripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@TripId", TripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        mast = Common.DataRowToJson(dt);

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
        string ChessisCode = SafeValue.SafeString(info["ChessisCode"]);
        string Statuscode = SafeValue.SafeString(info["Statuscode"]);
        string FromDate = SafeValue.SafeString(info["FromDate1"]);
        string FromTime = SafeValue.SafeString(info["FromTime"]);
        string ToDate = SafeValue.SafeString(info["ToDate1"]);
        string ToTime = SafeValue.SafeString(info["ToTime"]);
        string FromCode = SafeValue.SafeString(info["FromCode"]);
        string ToCode = SafeValue.SafeString(info["ToCode"]);
        string Remark = SafeValue.SafeString(info["Remark"]);
        string SubConInd = SafeValue.SafeString(info["SubCon_Ind"]);
        string SubConCode = SafeValue.SafeString(info["SubCon_Code"]);
        string BillLock = SafeValue.SafeString(info["BillLock"]);
        string TowheadCode = SafeValue.SafeString(info["TowheadCode"]);
        string FromParkingLot = SafeValue.SafeString(info["FromParkingLot"]);
        string ToParkingLot = SafeValue.SafeString(info["ToParkingLot"]);

        string YardAddress = SafeValue.SafeString(info["YardAddress"]);
        string BillingRemark = SafeValue.SafeString(info["BillingRemark"]);
        int jobId = SafeValue.SafeInt(info["jobId"], 0);
        string ReturnLastDate = SafeValue.SafeString(info["ReturnLastDate"]);
        string Remark1 = SafeValue.SafeString(info["Remark1"]);

        FromTime = convertTimeFormat(FromTime);
        ToTime = convertTimeFormat(ToTime);

        DateTime dt_fromdate = DateTime.ParseExact(FromDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        DateTime dt_todate = DateTime.ParseExact(ToDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        DateTime dt_ReturnLastDate = new DateTime(1753, 1, 1);
        try
        {
            dt_ReturnLastDate = DateTime.ParseExact(ReturnLastDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        }
        catch { }


        C2.CtmJobDet2Biz det2BZ = new C2.CtmJobDet2Biz(tripId);
        C2.CtmJobDet2 det2 = det2BZ.getData();
        bool status = false;
        string note = "";
        if (det2 != null)
        {
            det2.ContainerNo = ContainerNo;
            det2.DriverCode = DriverCode;
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
            det2.BillingRemark = BillingRemark;
            det2.Remark1 = Remark1;

            det2.SubCon_Ind = SubConInd;
            det2.SubCon_Code = SubConCode;
			
            //C2.BizResult result = det2BZ.update(HttpContext.Current.User.Identity.Name);

            //if (result.status)
            //{
            //}
            //else
            //{
            //    note = result.context;
            //}

            C2.BizResult result = det2BZ.update(HttpContext.Current.User.Identity.Name);

            if (result.status)
            {
                C2.CtmJobDet1Biz det1Bz = new C2.CtmJobDet1Biz(det2.Det1Id);
                C2.CtmJobDet1 det1 = det1Bz.getData();
                if (det1 != null)
                {
                    //det1.YardAddress = YardAddress;
                    //det1.ScheduleDate = dt_fromdate;
                    //det1.ScheduleTime = FromTime;
                    //det1Bz.update(HttpContext.Current.User.Identity.Name);
                }
                C2.CtmJob job = C2.Manager.ORManager.GetObject<C2.CtmJob>(jobId);
                if (job != null)
                {
                    job.ReturnLastDate = dt_ReturnLastDate;
                    C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(job);
                }


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
            else
            {
                note = result.context;
            }
            //C2.Manager.ORManager.StartTracking(det2, Wilson.ORMapper.InitialState.Updated);
            //C2.Manager.ORManager.PersistChanges(det2);

            //C2.CtmJobDet1 det1 = C2.Manager.ORManager.GetObject<C2.CtmJobDet1>(det2.Det1Id);
            //if (det1 != null)
            //{
            //    det1.ContainerNo = ContainerNo;
            //    det1.YardAddress = YardAddress;
            //    det1.ScheduleDate = dt_fromdate;
            //    det1.ScheduleTime = FromTime;
            //    C2.Manager.ORManager.StartTracking(det1, Wilson.ORMapper.InitialState.Updated);
            //    C2.Manager.ORManager.PersistChanges(det1);
            //}

            //C2.CtmJob job=C2.Manager.ORManager.GetObject<C2.CtmJob>(jobId);
            //if (job != null)
            //{
            //    job.ReturnLastDate = dt_ReturnLastDate;
            //    C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
            //    C2.Manager.ORManager.PersistChanges(job);
            //}

            //string sql = string.Format(@"update ctm_jobdet2 set ContainerNo=@ContainerNo where Det1Id=@Det1Id");
            //List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            //list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", det1.Id, SqlDbType.Int));
            //list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", det1.ContainerNo, SqlDbType.NVarChar, 100));
            //ConnectSql_mb.ExecuteNonQuery(sql, list);

        }
        Common.WriteJson(status, "{\"tripId\":" + tripId + ",\"ContainerNo\":\"" + ContainerNo + "\",\"Note\":\"" + note + "\"}");


    }

    private string convertTimeFormat(string par)
    {
        string bt_h = "00";
        string bt_m = "00";
        if (par.Length == 4)
        {
            int int_h = SafeValue.SafeInt(par.Substring(0, 2), 0) + 100;
            int int_m = SafeValue.SafeInt(par.Substring(2, 2), 0) + 100;
            bt_h = int_h.ToString().Substring(1);
            bt_m = int_m.ToString().Substring(1);
        }
        if (par.Length == 5)
        {
            int int_h = SafeValue.SafeInt(par.Substring(0, 2), 0) + 100;
            int int_m = SafeValue.SafeInt(par.Substring(3, 2), 0) + 100;
            bt_h = int_h.ToString().Substring(1);
            bt_m = int_m.ToString().Substring(1);
        }
        string res = bt_h + ":" + bt_m;
        return res;
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

        string sql = string.Format(@"select Det1.Id,det1.ContainerNo,det1.JobNo 
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
    public void readyBilling()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        string JobNo = SafeValue.SafeString(info["JobNo"]);


        CtmJobBiz jobBz = new CtmJobBiz(JobNo);
        C2.BizResult res = jobBz.jobBilling(HttpContext.Current.User.Identity.Name);
        Common.WriteJson(res.status, Common.StringToJson(res.context));


        //int tripId = SafeValue.SafeInt(info["Id"], 0);

        //string sql = "update CTM_Job set StatusCode=(case when StatusCode='CLS' then 'USE' else 'CLS' end),JobStatus=(case when JobStatus='Billing' then 'Confirmed' else 'Billing' end) where JobNo='" + JobNo + "'";
        //bool status = false;
        //if (ConnectSql.ExecuteSql(sql) > 0)
        //{
        //    status = true;
        //    string userId = HttpContext.Current.User.Identity.Name;
        //    C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        //    elog.Platform_isWeb();
        //    elog.Controller = userId;
        //    elog.JobNo = JobNo;
        //    elog.ActionLevel = "JOB";
        //    elog.setActionLevel(0, CtmJobEventLogRemark.Level.Job, 7);
        //    elog.log();
        //}
        //Common.WriteJson(res, Common.StringToJson(""));
    }


    [WebMethod]
    public void email_send()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));

        int Det1Id = SafeValue.SafeInt(info["Det1Id"], 0);
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
                    emailContent = string.Format(@"OUR REF: {0}\r\nCLIENT REF: {1}\r\nVESSEL/VOY: {2}/{3}\r\nCNTR NO/SIZE: {4}/{5}\r\nDELIVERY LOCATION: {6}\r\nTRAILER NO: {7}\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORM THAT OUR DRIVER IS NOW ON HIS WAY TO PSA TO COLLECT THE ABOVE MENTIONED CONTAINER.\r\n\r\nKINDLY PREPARE THE ASSIGNED PARKING LOT TO ACCEPT THE CONTAINER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD", JobNo, ClientRefNo, Vessel, Voyage, ContainerNo, ContainerType, ToCode, ChessisCode);
                    break;
                case "RET":
                    emailSubject = JobNo + "/" + ClientRefNo + "/" + ContainerNo;
                    emailContent = string.Format(@"OUR REF: {0}\r\nCLIENT REF: {1}\r\nVESSEL/VOY: {2}/{3}\r\nCNTR NO/SIZE: {4}/{5}\r\nDELIVERY LOCATION: {6}\r\nTRAILER NO: {7}\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORM THAT OUR DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION TO COLLECT THE EMPTY CONTAINER FOR RETURN. \r\n\r\nKINDLY ENSURE THAT CONTAINER IS NOW EMPTY & THERE WILL BE NO BLOCKAGE FOR OUR DRIVER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD", JobNo, ClientRefNo, Vessel, Voyage, ContainerNo, ContainerType, ToCode, ChessisCode);
                    break;
                case "EXP":
                    emailSubject = ClientRefNo + "/" + JobNo + "/" + Vessel;
                    emailContent = string.Format(@"OUR REF: {0}\r\nCLIENT REF: {1}\r\nVESSEL/VOY: {2}/{3}\r\nBKG REF: {4}\r\nCNTR NO/SEAL NO/TARE WEIGHT/SIZE: {5}/{6}/{7}/{8}\r\nLOCATION: {9}\r\nTRAILER NO: {10}\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORM THAT DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION TO COLLECT THE LOADED CONTAINER FOR EXPORT. \r\n\r\nKINDLY ENSURE THAT THERE IS NO BLOCKAGE FOR OUR DRIVER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD", JobNo, ClientRefNo, Vessel, Voyage, Br, ContainerNo, SealNo, ContWeight, ContainerType, ToCode, ChessisCode);
                    break;
                case "COL":
                    emailSubject = ClientRefNo + "/" + JobNo + "/" + Vessel;
                    emailContent = string.Format(@"OUR REF: {0}\r\nCLIENT REF: {1}\r\nVESSEL/VOY: {2}/{3}\r\nBKG REF: {4}\r\nCNTR NO/SEAL NO/TARE WEIGHT/SIZE: {5}/{6}/{7}/{8}\r\nLOCATION: {9}\r\nTRAILER NO: {10}\r\n\r\nDEAR CUSTOMER\r\n\r\nPLEASE BE INFORM THAT DRIVER IS NOW ON HIS WAY TO ABOVE MENTIONED LOCATION. \r\n\r\nKINDLY ENSURE THAT THE PARKING LOT IS CLEARED FOR OUR DRIVER.\r\n\r\nTHANK YOU FOR YOUR SUPPORT\r\n\r\nTSL LOGISTICS PTE LTD", JobNo, ClientRefNo, Vessel, Voyage, Br, ContainerNo, SealNo, ContWeight, ContainerType, ToCode, ChessisCode);
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