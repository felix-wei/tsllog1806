<%@ WebService Language="C#" Class="WebService_JobList" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Collections.Generic;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WebService_JobList : System.Web.Services.WebService
{
    [WebMethod]
    public void List_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string ContNo = info["ContNo"].ToString();
        bool ContStauts_0 = SafeValue.SafeBool(info["ContStauts_0"], false);
        bool ContStauts_1 = SafeValue.SafeBool(info["ContStauts_1"], false);
        bool ContStauts_2 = SafeValue.SafeBool(info["ContStauts_2"], false);
        string JobNo = info["JobNo"].ToString();
        string JobType = info["JobType"].ToString();
        string Vessel = info["Vessel"].ToString();
        string Client = info["Client"].ToString();
        string ClientName = info["ClientName"].ToString();
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", "%" + ContNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));


        string sql = string.Format(@"select job.Id,det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,det1.YardAddress as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.SealNo,det1.ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,det1.UrgentInd,job.OperatorCode,job.CarrierBkgNo,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,job.Pod,job.Pod,
(select top 1 Code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,
(select TripCode+'|'+det2.Statuscode+',' from CTM_JobDet2 as det2 where Det1Id=det1.Id order by FromDate,FromTime for xml path('')) as trips,
datediff(hour,isnull((select top 1 convert(nvarchar(8),det2.FromDate,112)+' '+det2.FromTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and (det2.Statuscode='S' or det2.Statuscode='C') order by det2.FromDate,det2.FromTime),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)),
case when det1.StatusCode='Completed' then isnull((select top 1 convert(nvarchar(8),det2.ToDate,112)+' '+det2.ToTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and det2.Statuscode='C' order by det2.ToDate desc,det2.ToTime desc),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)) else getdate() end)  as hr
from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo");
        string sql_where = "";
        if (ContNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "det1.ContainerNo like @ContNo");
        }
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
        }
        if (sql_where.Equals(""))
        {
            sql_where = " datediff(d,@DateFrom,det1.ScheduleDate)>=0 and datediff(d,@DateTo,det1.ScheduleDate)<=0";
            string sql_where_ContStatus = "";
            if (ContStauts_0)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='New'";
            }
            if (ContStauts_1)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='InTransit'";
            }
            if (ContStauts_2)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Completed'";
            }
            if (!sql_where_ContStatus.Equals(""))
            {
                sql_where = GetWhere(sql_where, "(" + sql_where_ContStatus + ")");
            }
            if (Vessel.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.Vessel=@Vessel");
            }
            if (JobType.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.JobType=@JobType");
            }
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }
        }
        sql += " where " + sql_where + " order by job.EtaDate,job.JobNo,det1.Id,det1.StatusCode desc, job.JobDate asc";
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(1, context);
    }

    [WebMethod]
    public void List_GetData_ByPage()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string ContNo = info["ContNo"].ToString();
        bool ContStauts_0 = SafeValue.SafeBool(info["ContStauts_0"], false);
        bool ContStauts_1 = SafeValue.SafeBool(info["ContStauts_1"], false);
        bool ContStauts_2 = SafeValue.SafeBool(info["ContStauts_2"], false);
        string JobNo = info["JobNo"].ToString();
        string JobType = info["JobType"].ToString();
        string Vessel = info["Vessel"].ToString();
        string Client = info["Client"].ToString();
        string ClientName = info["ClientName"].ToString();
        int curPage = SafeValue.SafeInt(info["curPage"], 0);
        string str_pageSize = info["pageSize"].ToString();
        curPage = curPage <= 0 ? 1 : curPage;
        int pageSize = 0;
        if (!str_pageSize.Equals("ALL"))
        {
            pageSize = SafeValue.SafeInt(str_pageSize, 0);
        }

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", "%" + ContNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));

        string sql_where = "";
        if (ContNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "det1.ContainerNo like @ContNo");
        }
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
        }
        if (sql_where.Equals(""))
        {
            sql_where = " datediff(d,@DateFrom,det1.ScheduleDate)>=0 and datediff(d,@DateTo,det1.ScheduleDate)<=0";
            string sql_where_ContStatus = "";
            if (ContStauts_0)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='New'";
            }
            if (ContStauts_1)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='InTransit'";
            }
            if (ContStauts_2)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Completed'";
            }
            if (!sql_where_ContStatus.Equals(""))
            {
                sql_where = GetWhere(sql_where, "(" + sql_where_ContStatus + ")");
            }
            if (Vessel.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.Vessel=@Vessel");
            }
            if (JobType.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.JobType=@JobType");
            }
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }
        }
        string sql = "";
        sql = "select count(*) from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo" + " where " + sql_where;
        int totalItems = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        int totalPages = pageSize > 0 ? (totalItems / pageSize + (totalItems % pageSize > 0 ? 1 : 0)) : 1;
        curPage = curPage > totalPages ? totalPages : curPage;

        sql = "";
        if (pageSize > 0)
        {
            sql += string.Format(@"select * from (
select top {0} * from (
select top {0} * from (", pageSize);
        }
        sql += string.Format(@"select ROW_NUMBER()over(order by job.EtaDate,job.JobNo,det1.Id,det1.StatusCode desc, job.JobDate asc) as rowId,
job.Id,det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,det1.YardAddress as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.SealNo,det1.ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,det1.UrgentInd,job.OperatorCode,job.CarrierBkgNo,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,
(select top 1 Code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,
(select TripCode+'|'+det2.Statuscode+',' from CTM_JobDet2 as det2 where Det1Id=det1.Id order by FromDate,FromTime for xml path('')) as trips,
datediff(hour,isnull((select top 1 convert(nvarchar(8),det2.FromDate,112)+' '+det2.FromTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and (det2.Statuscode='S' or det2.Statuscode='C') order by det2.FromDate,det2.FromTime),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)),
case when det1.StatusCode='Completed' then isnull((select top 1 convert(nvarchar(8),det2.ToDate,112)+' '+det2.ToTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and det2.Statuscode='C' order by det2.ToDate desc,det2.ToTime desc),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)) else getdate() end)  as hr
from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo");
        sql += " where " + sql_where;
        if (pageSize > 0)
        {
            sql += string.Format(@"
) as t
where rowId>={0}
order by rowId
) as t
order by rowId desc
) as t
order by rowId", pageSize * (curPage - 1));
        }
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);


        string context = string.Format("{0}\"list\":{2},\"curPage\":{3},\"totalPages\":{4},\"totalItems\":{5}{1}", "{", "}", DataList, curPage, totalPages, totalItems);
        Common.WriteJson(true, context);
    }


    private string GetWhere(string source, string par)
    {
        return source += (source.Equals("") ? " " : " and ") + par;
    }

    [WebMethod]
    public void MasterData_Client()
    {
        string sql = string.Format(@"select PartyId as c,Name as n from XXParty where IsCustomer=1 order by PartyId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
    }

    [WebMethod]
    public void ContainerView_GetData()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string ContId = info["ContId"].ToString();
        string JobNo = info["JobNo"].ToString();
        string mast = "{}";
        string trips = "[]";
        //        string sql = string.Format(@"select job.Id,PickupFrom,DeliveryTo,YardRef,job.SpecialInstruction,job.Remark,job.PermitNo,
        //det1.SealNo,det1.ContainerNo,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,det1.PortnetStatus,det1.UrgentInd,
        //det1.fee1,det1.fee2,det1.fee3,det1.fee4,det1.fee5,det1.fee6,det1.fee7,det1.fee8,det1.fee9,det1.fee10,
        //det1.fee11,det1.fee12,det1.fee13,det1.fee14,det1.fee15,det1.fee16,det1.fee17,det1.fee18,det1.fee19,det1.fee20,
        //det1.fee21,det1.fee22,det1.fee23,det1.fee24,det1.fee25,det1.fee26,det1.fee27,det1.fee28,det1.fee29,det1.fee30,
        //det1.fee31,det1.fee32,det1.fee33,det1.fee34,det1.fee35,det1.fee36,det1.fee37,det1.fee38,det1.fee39,det1.fee40,
        //det1.feeNote1,det1.feeNote2,det1.feeNote3,det1.feeNote4,det1.feeNote5,det1.feeNote6,det1.feeNote7,det1.feeNote8,det1.feeNote9,det1.feeNote10,
        //det1.feeNote11,det1.feeNote12,det1.feeNote13,det1.feeNote14,det1.feeNote15,det1.feeNote16,det1.feeNote17,det1.feeNote18,det1.feeNote19,det1.feeNote20,
        //det1.feeNote21,det1.feeNote22,det1.feeNote23,det1.feeNote24,det1.feeNote25,det1.feeNote26,det1.feeNote27,det1.feeNote28,det1.feeNote29,det1.feeNote30,
        //det1.feeNote31,det1.feeNote32,det1.feeNote33,det1.feeNote34,det1.feeNote35,det1.feeNote36,det1.feeNote37,det1.feeNote38,det1.feeNote39,det1.feeNote40 
        //from ctm_jobdet1 as det1
        //left outer join ctm_job as job on job.JobNo=det1.JobNo where det1.Id=@ContId");
        string sql = string.Format(@"select job.Id,PickupFrom,DeliveryTo,YardRef,job.SpecialInstruction,job.Remark,job.PermitNo,
det1.SealNo,det1.ContainerNo,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,det1.PortnetStatus,det1.UrgentInd,det1.Id as ContId,job.JobNo 
from ctm_jobdet1 as det1
left outer join ctm_job as job on job.JobNo=det1.JobNo where det1.Id=@ContId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@ContId", SafeValue.SafeInt(ContId, 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        if (dt.Rows.Count > 0)
        {
            mast = Common.DataRowToJson(dt);
            sql = string.Format(@"select Id,ContainerNo,Statuscode,TripCode,DoubleMounting,ToCode,FromDate,FromTime,ToDate,ToTime,
isnull(Incentive1,0)+isnull(Incentive2,0)+isnull(Incentive3,0)+isnull(Incentive4,0)+isnull(Incentive5,0)+isnull(Incentive6,0) as incentive,
isnull(Charge1,0)+isnull(Charge2,0)+isnull(Charge3,0)+isnull(Charge4,0)+isnull(Charge5,0)+isnull(Charge6,0)+isnull(Charge7,0)+isnull(Charge8,0)+isnull(Charge9,0)+isnull(Charge10,0) as claims,
DriverCode,TowheadCode,ChessisCode,ToParkingLot,Remark
from CTM_JobDet2
where JobNo=@JobNo and Det1Id=@ContId");
            dt = ConnectSql_mb.GetDataTable(sql, list);
            trips = Common.DataTableToJson(dt);
        }
        string context = string.Format("{0}\"mast\":{2},\"trips\":{3}{1}", "{", "}", mast, trips);
        Common.WriteJson(true, context);

    }

    [WebMethod]
    public void ContainerView_Save()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int jobId = SafeValue.SafeInt(info["Id"].ToString(), 0);
        string PickupFrom = info["PickupFrom"].ToString();
        string DeliveryTo = info["DeliveryTo"].ToString();
        string YardRef = info["YardRef"].ToString();
        string PermitNo = info["PermitNo"].ToString();
        string SpecialInstruction = info["SpecialInstruction"].ToString();
        string Remark = info["Remark"].ToString();

        int contId = SafeValue.SafeInt(info["ContId"].ToString(), 0);
        string ContainerNo = info["ContainerNo"].ToString();
        string SealNo = info["SealNo"].ToString();
        string F5Ind = info["F5Ind"].ToString();
        string PortnetStatus = info["PortnetStatus"].ToString();
        string WarehouseStatus = info["WarehouseStatus"].ToString();
        string EmailInd = info["EmailInd"].ToString();
        string UrgentInd = info["UrgentInd"].ToString();

        bool status = false;
        string context = Common.StringToJson("");
        string sql = "";
        List<ConnectSql_mb.cmdParameters> list = null;

        sql = string.Format(@"update CTM_Job set 
PickupFrom=@PickupFrom,DeliveryTo=@DeliveryTo,YardRef=@YardRef,SpecialInstruction=@SpecialInstruction,Remark=@Remark,PermitNo=@PermitNo 
where Id=@JobId");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobId", jobId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@PickupFrom", PickupFrom, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@DeliveryTo", DeliveryTo, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@YardRef", YardRef, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@SpecialInstruction", SpecialInstruction, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@PermitNo", PermitNo, SqlDbType.NVarChar, 100));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            sql = string.Format(@"update CTM_JobDet1 set 
SealNo=@SealNo,ContainerNo=@ContainerNo,F5Ind=@F5Ind,EmailInd=@EmailInd,WarehouseStatus=@WarehouseStatus,PortnetStatus=@PortnetStatus,UrgentInd=@UrgentInd
where Id=@ContId");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ContId", contId, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@SealNo", SealNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@F5Ind", F5Ind, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@EmailInd", EmailInd, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@WarehouseStatus", WarehouseStatus, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@PortnetStatus", PortnetStatus, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@UrgentInd", UrgentInd, SqlDbType.NVarChar, 10));
            if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
            {
                status = true;
            }
        }
        Common.WriteJson(status, context);
    }

    [WebMethod]
    public void ContainerView_AddNewTrip()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int contId = SafeValue.SafeInt(info["ContId"].ToString(), 0);
        string jobNo = info["JobNo"].ToString();
        string newType = info["NewType"].ToString();
        string ContainerNo = info["ContainerNo"].ToString();
        bool status = false;
        string context = Common.StringToJson("");

        string sql = string.Format(@"select PickupFrom,DeliveryTo,YardRef,JobType from CTM_Job where JobNo='{0}'", jobNo);
        DataTable tab = ConnectSql.GetTab(sql);
        sql = string.Format(@"select top 1 * from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} order by Id desc", jobNo, contId);
        DataTable dt = ConnectSql.GetTab(sql);
        string job_from = SafeValue.SafeString(tab.Rows[0]["PickupFrom"]);
        string job_to = SafeValue.SafeString(tab.Rows[0]["DeliveryTo"]);
        string job_Depot = SafeValue.SafeString(tab.Rows[0]["YardRef"]);
        string P_From = "";
        string P_From_Pl = "";
        string P_To = "";// DeliveryTo.Text;
        string trailer = "";
        string JobType = SafeValue.SafeString(tab.Rows[0]["JobType"]);
        string TripCode = "";
        DateTime FromDate = DateTime.Now;
        string FromTime = DateTime.Now.ToString("HH:mm");

        newType = (newType.Equals("") ? "SHF" : newType);
        TripCode = newType;
        switch (newType)
        {
            case "COL":
                add_newTrip_CheckMultiple(newType, jobNo, contId);
                P_From = job_Depot;
                P_To = job_from;
                break;
            case "EXP":
                add_newTrip_CheckMultiple(newType, jobNo, contId);
                P_From = job_from;
                P_To = job_to;
                break;
            case "IMP":
                add_newTrip_CheckMultiple(newType, jobNo, contId);
                P_From = job_from;
                P_To = job_to;
                break;
            case "RET":
                add_newTrip_CheckMultiple(newType, jobNo, contId);
                P_To = job_Depot;
                break;
            case "SHF":
                P_To = job_from;
                break;
            case "LOC":
                P_From = job_from;
                P_To = job_to;
                break;
        }
        if (dt.Rows.Count > 0)
        {
            P_From = dt.Rows[0]["ToCode"].ToString();
            P_From_Pl = dt.Rows[0]["ToParkingLot"].ToString();
            trailer = dt.Rows[0]["ChessisCode"].ToString();
        }

        sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot) values (@JobNo,@ContainerNo,'','',@ChessisCode,@FromCode,@FromDate,@FromTime,@ToCode,@FromDate,@FromTime,@Det1Id,'P',
'','N','','',@TripCode,'Normal','N',@FromParkingLot)");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", trailer, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromCode", P_From, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", FromDate, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@FromTime", FromTime, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToCode", P_To, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", contId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@TripCode", TripCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromParkingLot", P_From_Pl, SqlDbType.NVarChar, 100));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        
        sql = string.Format(@"select count(*) from ctm_jobdet2 where det1Id=@Det1Id");
        //int rowSum = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql,list).context, 0);
        if (ConnectSql_mb.ExecuteScalar(sql, list).context.Equals("1"))
        {
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='InTransit' where Id=@Det1Id");
            ConnectSql_mb.ExecuteNonQuery(sql, list);
        }
        Common.WriteJson(true, context);
    }

    private void add_newTrip_CheckMultiple(string Type, string JobNo, int ContId)
    {
        string sql = string.Format(@"select Id,TripCode from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} and TripCode='{2}' order by Id desc", JobNo, ContId, Type);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Exist trip:" + Type);
        }
    }
    
    [WebMethod]
    public void ContainerView_SaveTrip()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int tripId = SafeValue.SafeInt(info["Id"].ToString(), 0);
        string DriverCode = info["DriverCode"].ToString();
        string ChessisCode = info["ChessisCode"].ToString();
        string ToParkingLot = info["ToParkingLot"].ToString();
        string FromDate = info["FromDate"].ToString();
        string Remark = info["Remark"].ToString();

        string sql = "";
        sql = string.Format(@"update CTM_JobDet2 set DriverCode=@DriverCode,ChessisCode=@ChessisCode,ToParkingLot=@ToParkingLot,FromDate=@FromDate,Remark=@Remark
where Id=@TripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@TripId", tripId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", DriverCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", ChessisCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToParkingLot", ToParkingLot, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", FromDate, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
        ConnectSql_mb.ExecuteNonQuery(sql, list);

        Common.WriteJson(true, Common.StringToJson(""));
    }

    
    [WebMethod]
    public void AddNewJob()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string JobDate = info["JobDate"].ToString();
        string JobType = info["JobType"].ToString();
        string Shipper = info["Shipper"].ToString();
        string Client = info["Client"].ToString();
        string ClientName = info["ClientName"].ToString();
        string FromAddress = info["FromAddress"].ToString();
        string ToAddress = info["ToAddress"].ToString();
        string DepotAddress = info["DepotAddress"].ToString();
        string Remark = info["Remark"].ToString();

        bool status = false;
        string context = Common.StringToJson("");

        if (JobType == null || JobType.Equals(""))
        {
            Common.WriteJson(status, Common.StringToJson("Request JobType"));
            return;
        }
        DateTime date = DateTime.Now;
        string jobno = C2Setup.GetNextNo("", "CTM_Job_" + JobType, date);
        string user = "";
        string time4 = "0000";

        //string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress) values ('{0}','{4}',getdate(),getdate(),getdate(),'USE','{1}',getdate(),'{1}',getdate(),'{2}','{2}','{3}','{5}','{6}','{7}','{8}','{9}','{10}')", jobno, user, time4, cbb_new_jobtype.Value, txt_new_JobDate.Date, btn_new_ClientId.Text, txt_DepotAddress.Text, txt_FromAddress.Text, txt_ToAddress.Text, txt_new_remark.Text, txt_WarehouseAddress.Text); 
        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress) values 
(@JobNo,@JobDate,@JobDate,@JobDate,@JobDate,'USE',@User,getdate(),@User,getdate(),@Time4,@Time4,
@JobType,@ClientId,@DepotAddress,@FromAddress,@ToAddress,@Remark,@Shipper)");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobno, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobDate", JobDate, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@User", user, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Time4", time4, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 20));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", Client, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DepotAddress", DepotAddress, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@FromAddress", FromAddress, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@ToAddress", ToAddress, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Shipper", Shipper, SqlDbType.NVarChar, 100));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            C2Setup.SetNextNo("", "CTM_Job_" + JobType, jobno, date);
            context = Common.StringToJson(jobno);
            status = true;
        }
        Common.WriteJson(status, context);
    }
}