<%@ WebService Language="C#" Class="WebService_DailyTrip" %>

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
public class WebService_DailyTrip : System.Web.Services.WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void MasterData_Client()
    {
        string sql = string.Format(@"select PartyId as c,Name as n from XXParty where IsCustomer=1 order by PartyId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJson(true, Common.DataTableToJson(dt));
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
        string To = SafeValue.SafeString(info["To"]);
        string ContNo=SafeValue.SafeString(info["ContNo"]);
        string Client = SafeValue.SafeString(info["Client"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@BookingDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ToDate", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%"+ContNo+"%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", Client, SqlDbType.NVarChar, 100));

        string sql = "";
        string sql_where = "job.JobStatus<>'Voided' and isnull(det2.Self_Ind,'')<>'Yes'";
        if (ContNo.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.MasterJobNo like @No or det1.ContainerNo like @No)";
        }else
        {
            sql_where += " and det2.BookingDate>=@BookingDate and det2.BookingDate<@ToDate";
            if (Client.Length > 0)
            {
                sql_where += " and job.clientId=@ClientId";
            }
        }
        //        sql = string.Format(@"select count(*) from CTM_JobDet2 as det2 
        //left outer join ctm_job as job on det2.JobNo=job.JobNo
        //where datediff(d,det2.BookingDate,@BookingDate)=0 and job.JobStatus<>'Voided' and isnull(det2.Self_Ind,'')<>'Yes'");
        sql = string.Format(@"select count(*) from CTM_JobDet2 as det2 
left outer join ctm_job as job on det2.JobNo=job.JobNo
where {0}",sql_where);

        int totalItems = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        int totalPages = pageSize > 0 ? (totalItems / pageSize + (totalItems % pageSize > 0 ? 1 : 0)) : 1;
        curPage = curPage > totalPages ? totalPages : curPage;
        list.Add(new ConnectSql_mb.cmdParameters("@currenPage", pageSize * (curPage - 1), SqlDbType.Int));


        sql = string.Format(@"select ROW_NUMBER()over(order by TripCodeInd,JobNo,BookingTime,TripIndex) as rowId,*,
(Charge1+Charge2+Charge3+Charge4+Charge5+Charge6+Charge7+Charge8+Charge9+Charge10) as TotalClaim 
from (
select (case when det2.direct_inf='Direct Loading' and (det2.TripCode='SHF' or det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO') then 1 when det2.direct_inf='Direct Delivery' and (det2.TripCode='SHF' or det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO') then 2 else 
(case det2.TripCode when 'TPT' then 3 when 'WGR' then 3 when 'WDO' then 3 when 'LOC' then 3 
when 'EXP' then 4 when 'IMP' then 5 when 'COL' then 6 when 'RET' then 7 when 'SHF' then 8 when 'CRA' then 9 else 11 end) 
end ) as TripCodeInd,
(case det2.TripCode when 'TPT' then 2 when 'WGR' then 2 when 'WDO' then 2 when 'LOC' then 2 when 'CRA' then 1 else 0 end) as showCT,
det2.Id,det2.TripCode,det2.JobNo as JobNo,det2.JobNo as JobNo1,det2.TripIndex,det2.RequestVehicleType,det2.RequestTrailerType,det2.BookingTime,det2.ManualDO,det1.DischargeCell,
det2.FromTime,isnull(det2.TowheadCode,'')+' / '+isnull(det2.SubCon_Code,'') as TowheadCode,det2.ChessisCode,
isnull((select leg from (select t.Id,ROW_NUMBER()over(order by t.BookingDate) as leg from ctm_jobdet2 as t where t.Det1Id=det2.Det1Id and det2.Det1Id>0) as t where t.Id=det2.Id),0) as leg,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,job.ClientId,det1.ContainerNo,det1.SealNo,
det2.FromCode,det2.ToCode,case when det1.Id>0 then isnull(det1.ContainerType,'')+'/'+cast(isnull(det1.Weight,0) as nvarchar) else det2.RequestVehicleType end as ContainerType,
(case when det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO' or det2.TripCode='LOC' then '' else convert(nvarchar,job.EtaDate,103) end) as EtaDate,
(case when det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO' or det2.TripCode='LOC' then '' else job.EtaTime end) as EtaTime,
job.OperatorCode,job.Vessel,job.Voyage,job.CarrierBkgNo,job.MasterJobNo,(case when det2.ReturnLastDate is null or YEAR(det2.ReturnLastDate)<=2000 then '' else convert(nvarchar,det2.ReturnLastDate,103) end) as ReturnLastDate,
case when det2.Det1Id>0 then (select isnull(PermitNo,'')+',' from (
select distinct pm.PermitNo from job_house as h
left outer join ref_permit as pm on h.HblNo=pm.HblNo and pm.JobNo=det2.JobNo
where pm.PermitNo<>'' and h.ContId=det2.Det1Id
) as temp for xml path('')) else (select isnull(PermitNo,'')+',' from (
select distinct pm.PermitNo from job_house as h
left outer join ref_permit as pm on h.HblNo=pm.HblNo and pm.JobNo=det2.JobNo
where pm.PermitNo<>'' and h.TripId=det2.Id
) as temp for xml path(''))end as PermitNo,
det1.ContainerCategory,det2.Escort_Ind,job.JobType,det2.JobType as TripJobType,det2.Escort_Remark,
isnull(det1.Stuff_Ind,'No') as Stuff_Ind,job.SpecialInstruction,det2.Remark as TripInstruction,(select top 1 OpsType from job_house where TripId=det2.Id and OpsType<>'') as OpsType
,case when det2.TripCode='TPT' then det2.WarehouseRemark else '' end as WarehouseRemark,
(case when job.StatusCode='USE' then 'X' else '' end) as UnClosed,
case when (select count(*) as Cnt from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where mast.MastRefNo=job.JobNo)=0 then 'X' else '' end as UnBilled,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as Charge1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as Charge2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as Charge3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as Charge4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as Charge5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as Charge6,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as Charge7,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as Charge8,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as Charge9,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as Charge10,
det2.BookingDate
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
    public void download_excel()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string From = info["From"].ToString();

        string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).Parent.FullName;
        string file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "format.xlsx");
        string fileName = DateTime.Now.ToString("yyMMdd_HHmmss");
        string to_file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "Trips_" + fileName + ".xlsx");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@BookingDate", From, SqlDbType.NVarChar, 8));
        //        string sql = string.Format(@"select ROW_NUMBER()over(order by TripCodeInd,TripJobType,leg) as rowId,* 
        //from (
        //select (case when det2.direct_inf='Direct Loading' and det2.TripCode='SHF' then 1 when det2.direct_inf='Direct Delivery' and det2.TripCode='SHF' then 2 else 
        //(case det2.TripCode when 'TPT' then 3 when 'WGR' then 3 when 'WDO' then 3 when 'LOC' then 3 
        //when 'EXP' then 4 when 'IMP' then 5 when 'COL' then 6 when 'RET' then 7 when 'SHF' then 8 when 'CRA' then 9 else 11 end) 
        //end ) as TripCodeInd,
        //det2.Id,det2.TripCode,det2.JobNo as JobNo,det2.JobNo as JobNo1,
        //det2.FromTime,isnull(det2.TowheadCode,'')+' / '+isnull(det2.SubCon_Code,'') as TowheadCode,det2.ChessisCode,
        //isnull((select leg from (select t.Id,ROW_NUMBER()over(order by t.BookingDate) as leg from ctm_jobdet2 as t where t.Det1Id=det2.Det1Id and det2.Det1Id>0) as t where t.Id=det2.Id),0) as leg,
        //(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,job.ClientId,det1.ContainerNo,det1.SealNo,
        //det2.FromCode,det2.ToCode,case when det1.Id>0 then isnull(det1.ContainerType,'')+'/'+cast(isnull(det1.Weight,0) as nvarchar) else det2.RequestVehicleType end as ContainerType,
        //job.OperatorCode,job.Vessel,job.Voyage,job.EtaDate,job.EtaTime,job.PermitNo,job.CarrierBkgNo,
        //det1.ContainerCategory,det2.Escort_Ind,job.JobType,det2.JobType as TripJobType,det2.Escort_Remark,
        //isnull(det1.Stuff_Ind,'No') as Stuff_Ind,job.SpecialInstruction,(select top 1 OpsType from job_house where TripId=det2.Id and OpsType<>'') as OpsType 
        //from CTM_JobDet2 as det2
        //left outer join CTM_JobDet1 as det1 on det1.Id=det2.det1Id
        //left outer join CTM_Job as job on det2.JobNo=job.JobNo
        //where datediff(d,det2.BookingDate,@BookingDate)=0 and job.JobStatus<>'Voided' and isnull(det2.Self_Ind,'')<>'Yes'
        //) as temp");
        string sql = string.Format(@"select ROW_NUMBER()over(order by TripCodeInd,JobNo,BookingTime,TripIndex) as rowId,*,
(Charge1+Charge2+Charge3+Charge4+Charge5+Charge6+Charge7+Charge8+Charge9+Charge10) as TotalClaim 
from (
select (case when det2.direct_inf='Direct Loading' and (det2.TripCode='SHF' or det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO') then 1 when det2.direct_inf='Direct Delivery' and (det2.TripCode='SHF' or det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO') then 2 else 
(case det2.TripCode when 'TPT' then 3 when 'WGR' then 3 when 'WDO' then 3 when 'LOC' then 3 
when 'EXP' then 4 when 'IMP' then 5 when 'COL' then 6 when 'RET' then 7 when 'SHF' then 8 when 'CRA' then 9 else 11 end) 
end ) as TripCodeInd,
(case det2.TripCode when 'TPT' then 2 when 'WGR' then 2 when 'WDO' then 2 when 'LOC' then 2 when 'CRA' then 1 else 0 end) as showCT,
det2.Id,det2.TripCode,det2.JobNo as JobNo,det2.JobNo as JobNo1,det2.TripIndex,det2.RequestVehicleType,det2.RequestTrailerType,det2.BookingTime,det2.ManualDO,det1.DischargeCell,
det2.FromTime,isnull(det2.TowheadCode,'')+' / '+isnull(det2.SubCon_Code,'') as TowheadCode,det2.ChessisCode,
isnull((select leg from (select t.Id,ROW_NUMBER()over(order by t.BookingDate) as leg from ctm_jobdet2 as t where t.Det1Id=det2.Det1Id and det2.Det1Id>0) as t where t.Id=det2.Id),0) as leg,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,job.ClientId,det1.ContainerNo,det1.SealNo,
det2.FromCode,det2.ToCode,case when det1.Id>0 then isnull(det1.ContainerType,'')+'/'+cast(isnull(det1.Weight,0) as nvarchar) else det2.RequestVehicleType end as ContainerType,
(case when det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO' or det2.TripCode='LOC' then '' else convert(nvarchar,job.EtaDate,103) end) as EtaDate,
(case when det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO' or det2.TripCode='LOC' then '' else job.EtaTime end) as EtaTime,
job.OperatorCode,job.Vessel,job.Voyage,job.CarrierBkgNo,job.MasterJobNo,(case when det2.ReturnLastDate is null or YEAR(det2.ReturnLastDate)<=2000 then '' else convert(nvarchar,det2.ReturnLastDate,103) end) as ReturnLastDate,
case when det2.Det1Id>0 then (select isnull(PermitNo,'')+',' from (
select distinct pm.PermitNo from job_house as h
left outer join ref_permit as pm on h.HblNo=pm.HblNo and pm.JobNo=det2.JobNo
where pm.PermitNo<>'' and h.ContId=det2.Det1Id
) as temp for xml path('')) else (select isnull(PermitNo,'')+',' from (
select distinct pm.PermitNo from job_house as h
left outer join ref_permit as pm on h.HblNo=pm.HblNo and pm.JobNo=det2.JobNo
where pm.PermitNo<>'' and h.TripId=det2.Id
) as temp for xml path(''))end as PermitNo,
det1.ContainerCategory,det2.Escort_Ind,job.JobType,det2.JobType as TripJobType,det2.Escort_Remark,
isnull(det1.Stuff_Ind,'No') as Stuff_Ind,job.SpecialInstruction,det2.Remark as TripInstruction,(select top 1 OpsType from job_house where TripId=det2.Id and OpsType<>'') as OpsType
,case when det2.TripCode='TPT' then det2.WarehouseRemark else '' end as WarehouseRemark,
(case when job.StatusCode='USE' then 'X' else '' end) as UnClosed,
case when (select count(*) as Cnt from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where mast.MastRefNo=job.JobNo)=0 then 'X' else '' end as UnBilled,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as Charge1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as Charge2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as Charge3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as Charge4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as Charge5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as Charge6,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as Charge7,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as Charge8,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as Charge9,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as Charge10,
det2.BookingDate  
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.det1Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where datediff(d,det2.BookingDate,@BookingDate)=0 and job.JobStatus<>'Voided' and isnull(det2.Self_Ind,'')<>'Yes'
) as temp");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);

        List<string> tripCodeList = new List<string>();
        tripCodeList.Add("Empty");
        tripCodeList.Add("Direct Loading");
        tripCodeList.Add("Direct Delivery");
        tripCodeList.Add("Local Delivery");
        tripCodeList.Add("Export Container");
        tripCodeList.Add("Import Container");
        tripCodeList.Add("Empty Collection");
        tripCodeList.Add("Empty Return");
        tripCodeList.Add("Shifing");
        tripCodeList.Add("Crane Job");
        tripCodeList.Add("Escort - Preloading");


        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(HttpContext.Current.Server.MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        wb.Open(file);
        Worksheet ws = wb.Worksheets[0];
        ws.Cells[0, 1].PutValue(From);
        int baseRow = 2;
        int curTripCodeIndex = 0;
        int i = 0;
        for (; i < dt.Rows.Count;)
        {
            if (dt.Rows[i]["TripCodeInd"].ToString() != curTripCodeIndex.ToString())
            {
                ws.Cells[baseRow + i + 1, 0].PutValue(tripCodeList[++curTripCodeIndex]);
                baseRow = baseRow + 2;
                continue;
            }
            ws.Cells[baseRow + i, 0].PutValue(dt.Rows[i]["BookingDate"]);
            ws.Cells[baseRow + i, 1].PutValue(dt.Rows[i]["JobNo"]);
            ws.Cells[baseRow + i, 2].PutValue(dt.Rows[i]["ManualDO"]);
            ws.Cells[baseRow + i, 3].PutValue(dt.Rows[i]["MasterJobNo"]);
            ws.Cells[baseRow + i, 4].PutValue(dt.Rows[i]["Escort_Remark"]);
            ws.Cells[baseRow + i, 5].PutValue(dt.Rows[i]["BookingTime"]);
            ws.Cells[baseRow + i, 6].PutValue(dt.Rows[i]["TowheadCode"]);
            ws.Cells[baseRow + i, 7].PutValue(dt.Rows[i]["ChessisCode"]);
            ws.Cells[baseRow + i, 8].PutValue(dt.Rows[i]["Incentive"]);
            ws.Cells[baseRow + i, 9].PutValue(dt.Rows[i]["TotalClaim"]);
            ws.Cells[baseRow + i, 10].PutValue(dt.Rows[i]["ClientId"]);
            ws.Cells[baseRow + i, 11].PutValue(dt.Rows[i]["ContainerNo"] + "/" + dt.Rows[i]["DischargeCell"] + "/" + (dt.Rows[i]["showCT"].ToString() == "1" ? dt.Rows[i]["RequestVehicleType"] : dt.Rows[i]["RequestTrailerType"]));
            ws.Cells[baseRow + i, 12].PutValue(dt.Rows[i]["SealNo"]);
            ws.Cells[baseRow + i, 13].PutValue(dt.Rows[i]["FromCode"]);
            ws.Cells[baseRow + i, 14].PutValue(dt.Rows[i]["ToCode"]);
            ws.Cells[baseRow + i, 15].PutValue(dt.Rows[i]["ContainerType"]);
            ws.Cells[baseRow + i, 16].PutValue(dt.Rows[i]["OperatorCode"]);
            ws.Cells[baseRow + i, 17].PutValue(dt.Rows[i]["Vessel"] + "/" + dt.Rows[i]["Voyage"]);
            //ws.Cells[baseRow + i, 15].PutValue(SafeValue.SafeDate(dt.Rows[i]["EtaDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy"));
            ws.Cells[baseRow + i, 18].PutValue(dt.Rows[i]["EtaDate"]);
            ws.Cells[baseRow + i, 19].PutValue(dt.Rows[i]["EtaTime"]);
            ws.Cells[baseRow + i, 20].PutValue(dt.Rows[i]["ReturnLastDate"]);
            ws.Cells[baseRow + i, 21].PutValue(dt.Rows[i]["PermitNo"] + "/" + dt.Rows[i]["CarrierBkgNo"]);
            ws.Cells[baseRow + i, 22].PutValue(dt.Rows[i]["OpsType"]);
            ws.Cells[baseRow + i, 23].PutValue(dt.Rows[i]["Stuff_Ind"]);
            ws.Cells[baseRow + i, 24].PutValue(dt.Rows[i]["TripInstruction"]);
            ws.Cells[baseRow + i, 25].PutValue(dt.Rows[i]["WarehouseRemark"]);

            i++;
        }
        curTripCodeIndex++;
        for (; curTripCodeIndex < tripCodeList.Count; curTripCodeIndex++)
        {
            ws.Cells[baseRow + i + 1, 0].PutValue(tripCodeList[curTripCodeIndex]);
            baseRow = baseRow + 2;
        }
        wb.Save(to_file);

        string context = Common.StringToJson(Path.Combine("files", "Excel_DailyTrips", "Trips_" + fileName + ".xlsx"));
        Common.WriteJson(true, context);
    }

    class excelColumn
    {
        public string c { get; set; }
        public string t { get; set; }
    }
    [WebMethod]
    public void download_excel_option()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string From = info["From"].ToString();
        string To = SafeValue.SafeString(info["To"]);
        string DateHeader = SafeValue.SafeString(info["DateHeader"]);
        string ContNo=SafeValue.SafeString(info["ContNo"]);
        string Client = SafeValue.SafeString(info["Client"]);
        JArray ar = (JArray)JsonConvert.DeserializeObject(info["options"].ToString());

        List<excelColumn> list_excel = new List<excelColumn>();
        for (int c_i = 0; c_i < ar.Count; c_i++)
        {
            JObject row = (JObject)ar[c_i];
            bool s = SafeValue.SafeBool(row["s"], false);
            if (s)
            {
                excelColumn ec = new excelColumn();
                ec.c = SafeValue.SafeString(row["c"]);
                ec.t = SafeValue.SafeString(row["t"]);
                list_excel.Add(ec);
            }
        }

        string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).Parent.FullName;
        string file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "format2.xlsx");
        string fileName = DateTime.Now.ToString("yyMMdd_HHmmss");
        string to_file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "Trips_" + fileName + ".xlsx");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@BookingDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ToDate", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@No", "%"+ContNo+"%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", Client, SqlDbType.NVarChar, 100));


        string sql_where = "job.JobStatus<>'Voided' and isnull(det2.Self_Ind,'')<>'Yes'";
        if (ContNo.Length > 0)
        {
            sql_where += " and (job.JobNo like @No or job.MasterJobNo like @No or det1.ContainerNo like @No)";
        }else
        {
            sql_where += " and det2.BookingDate>=@BookingDate and det2.BookingDate<@ToDate";
            if (Client.Length > 0)
            {
                sql_where += " and job.clientId=@ClientId";
            }
        }
        string sql = string.Format(@"select ROW_NUMBER()over(order by TripCodeInd,JobNo,BookingTime,TripIndex) as rowId,*,
(Charge1+Charge2+Charge3+Charge4+Charge5+Charge6+Charge7+Charge8+Charge9+Charge10) as TotalClaim 
from (
select (case when det2.direct_inf='Direct Loading' and (det2.TripCode='SHF' or det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO') then 1 when det2.direct_inf='Direct Delivery' and (det2.TripCode='SHF' or det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO') then 2 else 
(case det2.TripCode when 'TPT' then 3 when 'WGR' then 3 when 'WDO' then 3 when 'LOC' then 3 
when 'EXP' then 4 when 'IMP' then 5 when 'COL' then 6 when 'RET' then 7 when 'SHF' then 8 when 'CRA' then 9 else 11 end) 
end ) as TripCodeInd,
(case det2.TripCode when 'TPT' then 2 when 'WGR' then 2 when 'WDO' then 2 when 'LOC' then 2 when 'CRA' then 1 else 0 end) as showCT,
det2.Id,det2.TripCode,det2.JobNo as JobNo,det2.JobNo as JobNo1,det2.TripIndex,det2.RequestVehicleType,det2.RequestTrailerType,det2.BookingTime,det2.ManualDO,det1.DischargeCell,
det2.FromTime,isnull(det2.TowheadCode,'')+' / '+isnull(det2.SubCon_Code,'') as TowheadCode,det2.ChessisCode,
isnull((select leg from (select t.Id,ROW_NUMBER()over(order by t.BookingDate) as leg from ctm_jobdet2 as t where t.Det1Id=det2.Det1Id and det2.Det1Id>0) as t where t.Id=det2.Id),0) as leg,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,job.ClientId,det1.ContainerNo,det1.SealNo,
det2.FromCode,det2.ToCode,case when det1.Id>0 then isnull(det1.ContainerType,'')+'/'+cast(isnull(det1.Weight,0) as nvarchar) else det2.RequestVehicleType end as ContainerType,
(case when det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO' or det2.TripCode='LOC' then '' else convert(nvarchar,job.EtaDate,103) end) as EtaDate,
(case when det2.TripCode='TPT' or det2.TripCode='WGR' or det2.TripCode='WDO' or det2.TripCode='LOC' then '' else job.EtaTime end) as EtaTime,
job.OperatorCode,job.Vessel,job.Voyage,job.CarrierBkgNo,job.MasterJobNo,(case when det2.ReturnLastDate is null or YEAR(det2.ReturnLastDate)<=2000 then '' else convert(nvarchar,det2.ReturnLastDate,103) end) as ReturnLastDate,
case when det2.Det1Id>0 then (select isnull(PermitNo,'')+',' from (
select distinct pm.PermitNo from job_house as h
left outer join ref_permit as pm on h.HblNo=pm.HblNo and pm.JobNo=det2.JobNo
where pm.PermitNo<>'' and h.ContId=det2.Det1Id
) as temp for xml path('')) else (select isnull(PermitNo,'')+',' from (
select distinct pm.PermitNo from job_house as h
left outer join ref_permit as pm on h.HblNo=pm.HblNo and pm.JobNo=det2.JobNo
where pm.PermitNo<>'' and h.TripId=det2.Id
) as temp for xml path(''))end as PermitNo,
det1.ContainerCategory,det2.Escort_Ind,job.JobType,det2.JobType as TripJobType,det2.Escort_Remark,
isnull(det1.Stuff_Ind,'No') as Stuff_Ind,job.SpecialInstruction,det2.Remark as TripInstruction,(select top 1 OpsType from job_house where TripId=det2.Id and OpsType<>'') as OpsType
,case when det2.TripCode='TPT' then det2.WarehouseRemark else '' end as WarehouseRemark,
(case when job.StatusCode='USE' then 'X' else '' end) as UnClosed,
case when (select count(*) as Cnt from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where mast.MastRefNo=job.JobNo)=0 then 'X' else '' end as UnBilled,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as Charge1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as Charge2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as Charge3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as Charge4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as Charge5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as Charge6,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as Charge7,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as Charge8,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as Charge9,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as Charge10,
convert(nvarchar,det2.BookingDate,103) as BookingDate
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.det1Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where {0}
) as temp",sql_where);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);

        List<string> tripCodeList = new List<string>();
        tripCodeList.Add("Empty");
        tripCodeList.Add("Direct Loading");
        tripCodeList.Add("Direct Delivery");
        tripCodeList.Add("Local Delivery");
        tripCodeList.Add("Export Container");
        tripCodeList.Add("Import Container");
        tripCodeList.Add("Empty Collection");
        tripCodeList.Add("Empty Return");
        tripCodeList.Add("Shifing");
        tripCodeList.Add("Crane Job");
        tripCodeList.Add("Escort - Preloading");


        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(HttpContext.Current.Server.MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        wb.Open(file);
        Worksheet ws = wb.Worksheets[0];
        ws.Cells[0, 1].PutValue(DateHeader);
        int baseRow = 2;
        int curTripCodeIndex = 0;
        int i = 0;

        //init excel header
        for (int col_i = 0; col_i < list_excel.Count; col_i++)
        {
            ws.Cells[baseRow - 1 + i, col_i].PutValue(list_excel[col_i].t);
        }
        //excel context
        for (; i < dt.Rows.Count;)
        {
            if (dt.Rows[i]["TripCodeInd"].ToString() != curTripCodeIndex.ToString())
            {
                ws.Cells[baseRow + i + 1, 0].PutValue(tripCodeList[++curTripCodeIndex]);
                baseRow = baseRow + 2;
                continue;
            }

            for (int col_i = 0; col_i < list_excel.Count; col_i++)
            {
                string val = download_excel_option_getColValue(list_excel[col_i], dt.Rows[i]);
                ws.Cells[baseRow + i, col_i].PutValue(val);
            }

            i++;
        }
        curTripCodeIndex++;
        for (; curTripCodeIndex < tripCodeList.Count; curTripCodeIndex++)
        {
            ws.Cells[baseRow + i + 1, 0].PutValue(tripCodeList[curTripCodeIndex]);
            baseRow = baseRow + 2;
        }
        wb.Save(to_file);

        string context = Common.StringToJson(Path.Combine("files", "Excel_DailyTrips", "Trips_" + fileName + ".xlsx"));
        Common.WriteJson(true, context);
    }


    private string download_excel_option_getColValue(excelColumn ec, DataRow dr)
    {
        string res = "";
        if (ec.c == "SP_ContainerNo")
        {
            res = dr["ContainerNo"] + "/" + dr["DischargeCell"];
            string temp = dr["showCT"].ToString();
            if (temp == "1")
            {
                res += "/" + dr["RequestVehicleType"];
            }
            else
            {
                res += "/" + dr["RequestTrailerType"];
            }
            return res;
        }
        string[] ar = ec.c.Split('/');
        for (int i = 0; i < ar.Length; i++)
        {
            res += dr[ar[i]].ToString() + "/";
        }
        if (res.EndsWith("/"))
        {
            res = res.Substring(0, res.Length - 1);
        }
        return res;
    }





    [WebMethod]
    public void List_GetData_ByPage_160529()
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
        //string To = info["To"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));

        string sql = "";
        sql = string.Format(@"select count(*) from CTM_JobDet2 as det2 where datediff(d,det2.FromDate,@FromDate)=0");
        int totalItems = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        int totalPages = pageSize > 0 ? (totalItems / pageSize + (totalItems % pageSize > 0 ? 1 : 0)) : 1;
        curPage = curPage > totalPages ? totalPages : curPage;
        list.Add(new ConnectSql_mb.cmdParameters("@currenPage", pageSize * (curPage - 1), SqlDbType.Int));


        sql = string.Format(@"select ROW_NUMBER()over(order by TripCodeInd) as rowId,* 
from (select *,(case NextTrip when 'COL' then 1 when 'RET' then 2 when 'IMP' then 3 when 'EXP' then 4 when 'SHF' then 5 when 'LOC' then 6 when 'Completed' then 7 else 0 end) as TripCodeInd from (
select *,
(case JobType 
when 'IMP' then (case when TKStatus='New' or isnull(TKStatus,'')='' then 'IMP' when TKStatus='Import' and TripCode='IMP' and  curTrip<>'C:IMP' then 'IMP' when TKStatus='WHS-MT' then 'RET' when TKStatus='Return' and curTrip<>'C:RET' then 'RET' when curTrip='C:RET' then 'Completed' else '' end) 
when 'EXP' then (case when TKStatus='New' or isnull(TKStatus,'')='' then 'COL' when TKStatus='Collection' and curTrip<>'C:COL'then 'COL' when TKStatus='WHS-LD' then 'EXP' when TKStatus='Export' and curTrip<>'C:EXP' then 'EXP' when curTrip='C:EXP' then 'Completed' else '' end) 
when 'WGR' then (case when TKStatus='New' or isnull(TKStatus,'')='' then 'LOC' when TKStatus='Start' and curTrip<>'C:LOC' then 'LOC' when TKStatus='Delivered' then 'LOC' when TKStatus='LCL-MT' then 'SHF' when TKStatus='Depart' then 'SHF' else '' end) 
when 'WDO' then (case when TKStatus='Start' or isnull(TKStatus,'')='' then 'SHF' when TKStatus='Arrival' and curTrip<>'C:LOC' then 'LOC' when TKStatus='LCL-LD' then 'LOC' when TKStatus='Depart' and curTrip<>'C:LOC' then 'LOC' else '' end) 
else ''  end) as NextTrip 
from (
select 
det2.Id as TripId,det2.TripCode,job.JobNo,det2.FromTime,det2.TowheadCode,det2.ChessisCode,det1.StatusCode as TKStatus,job.JobType,
(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(18,2)) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
job.ClientId,det1.ContainerNo,det1.SealNo,det2.Statuscode+':'+det2.TripCode as curTrip,
det2.FromCode,det2.ToCode,det1.ContainerType,job.OperatorCode,job.Vessel,job.Voyage,job.EtaDate,job.EtaTime,job.PermitNo,job.CarrierBkgNo,
(select count(*) from ctm_jobdet2 as inDet2 where det1.Id=inDet2.det1Id) as legs,
isnull((select leg from (select t.Id,ROW_NUMBER()over(order by t.FromDate) as leg from ctm_jobdet2 as t where t.Det1Id=det2.Det1Id) as t where t.Id=det2.Id),0) as legDesc
from CTM_JobDet1 as det1 
left outer join CTM_JobDet2 as det2 on det1.Id=det2.det1Id
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where datediff(d,det1.ScheduleDate,@FromDate)=0
) as temp 
where legs=legDesc
) temp
) as temp");
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
    public void download_excel_160529()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string From = info["From"].ToString();
        string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).Parent.FullName;
        string file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "format.xlsx");
        string fileName = DateTime.Now.ToString("yyMMdd_HHmmss");
        string to_file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "Trips_" + fileName + ".xlsx");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        string sql = string.Format(@"select ROW_NUMBER()over(order by TripCodeInd) as rowId,* 
from (select *,(case NextTrip when 'COL' then 1 when 'RET' then 2 when 'IMP' then 3 when 'EXP' then 4 when 'SHF' then 5 when 'LOC' then 6 when 'Completed' then 7 else 0 end) as TripCodeInd from (
select *,
(case JobType 
when 'IMP' then (case when TKStatus='New' or isnull(TKStatus,'')='' then 'IMP' when TKStatus='Import' and TripCode='IMP' and  curTrip<>'C:IMP' then 'IMP' when TKStatus='WHS-MT' then 'RET' when TKStatus='Return' and curTrip<>'C:RET' then 'RET' when curTrip='C:RET' then 'Completed' else '' end) 
when 'EXP' then (case when TKStatus='New' or isnull(TKStatus,'')='' then 'COL' when TKStatus='Collection' and curTrip<>'C:COL'then 'COL' when TKStatus='WHS-LD' then 'EXP' when TKStatus='Export' and curTrip<>'C:EXP' then 'EXP' when curTrip='C:EXP' then 'Completed' else '' end) 
when 'WGR' then (case when TKStatus='New' or isnull(TKStatus,'')='' then 'LOC' when TKStatus='Start' and curTrip<>'C:LOC' then 'LOC' when TKStatus='Delivered' then 'LOC' when TKStatus='LCL-MT' then 'SHF' when TKStatus='Depart' then 'SHF' else '' end) 
when 'WDO' then (case when TKStatus='Start' or isnull(TKStatus,'')='' then 'SHF' when TKStatus='Arrival' and curTrip<>'C:LOC' then 'LOC' when TKStatus='LCL-LD' then 'LOC' when TKStatus='Depart' and curTrip<>'C:LOC' then 'LOC' else '' end) 
else ''  end) as NextTrip 
from (
select 
det2.Id as TripId,det2.TripCode,job.JobNo,det2.FromTime,det2.TowheadCode,det2.ChessisCode,det1.StatusCode as TKStatus,job.JobType,
(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(18,2)) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
job.ClientId,det1.ContainerNo,det1.SealNo,det2.Statuscode+':'+det2.TripCode as curTrip,
det2.FromCode,det2.ToCode,det1.ContainerType,job.OperatorCode,job.Vessel,job.Voyage,job.EtaDate,job.EtaTime,job.PermitNo,job.CarrierBkgNo,
(select count(*) from ctm_jobdet2 as inDet2 where det1.Id=inDet2.det1Id) as legs,
isnull((select leg from (select t.Id,ROW_NUMBER()over(order by t.FromDate) as leg from ctm_jobdet2 as t where t.Det1Id=det2.Det1Id) as t where t.Id=det2.Id),0) as legDesc
from CTM_JobDet1 as det1 
left outer join CTM_JobDet2 as det2 on det1.Id=det2.det1Id
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where datediff(d,det1.ScheduleDate,@FromDate)=0
) as temp 
where legs=legDesc
) temp
) as temp");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);

        List<string> tripCodeList = new List<string>();
        tripCodeList.Add("Empty");
        tripCodeList.Add("Empty Collection");
        tripCodeList.Add("Empty Return");
        tripCodeList.Add("Import");
        tripCodeList.Add("Export");
        tripCodeList.Add("Shifing");
        tripCodeList.Add("Local");


        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(HttpContext.Current.Server.MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        wb.Open(file);
        Worksheet ws = wb.Worksheets[0];
        int baseRow = 2;
        int curTripCodeIndex = 0;
        int i = 0;
        for (; i < dt.Rows.Count;)
        {
            if (dt.Rows[i]["TripCodeInd"].ToString() != curTripCodeIndex.ToString())
            {
                ws.Cells[baseRow + i + 1, 0].PutValue(tripCodeList[++curTripCodeIndex]);
                baseRow = baseRow + 2;
                continue;
            }
            ws.Cells[baseRow + i, 0].PutValue(dt.Rows[i]["JobNo"]);
            ws.Cells[baseRow + i, 1].PutValue(dt.Rows[i]["leg"]);
            ws.Cells[baseRow + i, 2].PutValue(dt.Rows[i]["FromTime"]);
            ws.Cells[baseRow + i, 3].PutValue(dt.Rows[i]["TowheadCode"]);
            ws.Cells[baseRow + i, 4].PutValue(dt.Rows[i]["ChessisCode"]);
            ws.Cells[baseRow + i, 5].PutValue(dt.Rows[i]["Incentive"]);
            ws.Cells[baseRow + i, 6].PutValue(dt.Rows[i]["ClientId"]);
            ws.Cells[baseRow + i, 7].PutValue(dt.Rows[i]["ContainerNo"]);
            ws.Cells[baseRow + i, 8].PutValue(dt.Rows[i]["SealNo"]);
            ws.Cells[baseRow + i, 9].PutValue(dt.Rows[i]["FromCode"]);
            ws.Cells[baseRow + i, 10].PutValue(dt.Rows[i]["ToCode"]);
            ws.Cells[baseRow + i, 11].PutValue(dt.Rows[i]["ContainerType"]);
            ws.Cells[baseRow + i, 12].PutValue(dt.Rows[i]["OperatorCode"]);
            ws.Cells[baseRow + i, 13].PutValue(dt.Rows[i]["Vessel"] + "/" + dt.Rows[i]["Voyage"]);
            ws.Cells[baseRow + i, 14].PutValue(SafeValue.SafeDate(dt.Rows[i]["EtaDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy"));
            ws.Cells[baseRow + i, 15].PutValue(dt.Rows[i]["EtaTime"]);
            ws.Cells[baseRow + i, 16].PutValue(dt.Rows[i]["PermitNo"] + "/" + dt.Rows[i]["CarrierBkgNo"]);

            i++;
        }
        curTripCodeIndex++;
        for (; curTripCodeIndex < tripCodeList.Count; curTripCodeIndex++)
        {
            ws.Cells[baseRow + i + 1, 0].PutValue(tripCodeList[curTripCodeIndex]);
            baseRow = baseRow + 2;
        }
        wb.Save(to_file);

        string context = Common.StringToJson(Path.Combine("files", "Excel_DailyTrips", "Trips_" + fileName + ".xlsx"));
        Common.WriteJson(true, context);
    }

};