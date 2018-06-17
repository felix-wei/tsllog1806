using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Connect_ClientApp
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Connect_ClientApp : System.Web.Services.WebService
{

    public Connect_ClientApp()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void UserLogin_Login(string info)
    {
        JObject jo = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        bool status = false;
        string context = Common.StringToJson("");
        string account = jo["account"].ToString();
        string pw = jo["password"].ToString();

        if (account == null || account.Length <= 0)
        {
            context = Common.StringToJson("Request Account");
        }
        else
        {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            //ConnectSql_mb.cmdParameters cpar = null;
            string sql = string.Format(@"select PartyId as Name,LoginCode as Pwd,'Client' as Role 
from xxparty 
where LoginInd='YES' and PartyId=@Tel");
            list.Add(new ConnectSql_mb.cmdParameters("@Tel", account, SqlDbType.NVarChar, 100));
            //list.Add(new ConnectSql_mb.cmdParameters("@PW", pw, SqlDbType.NVarChar, 100));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (pw.Equals(dt.Rows[i]["Pwd"].ToString()))
                    {
                        sql = string.Format(@"select top 1 PartyId as Name,LoginCode as Pwd,'Client' as Role 
from xxparty 
where LoginInd='YES' and PartyId=@Tel and LoginCode=@PW");
                        list = new List<ConnectSql_mb.cmdParameters>();
                        list.Add(new ConnectSql_mb.cmdParameters("@Tel", account, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@PW", pw, SqlDbType.NVarChar, 100));
                        DataTable dt1 = ConnectSql_mb.GetDataTable(sql, list);
                        status = true;
                        context = Common.DataRowToJson(dt1);

                        //===========log
                        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                        lg.Platform_isMobile();
                        lg.Controller = dt.Rows[i]["Name"].ToString();
                        lg.Remark = dt.Rows[i]["Name"] + " Login";
                        lg.ActionLevel = "Client";
                        lg.log();
                        break;
                    }
                }
                if (!status)
                {
                    context = Common.StringToJson("Password error");
                }
            }
            else
            {
                sql = string.Format(@"select PartyId as Name,LoginCode as Pwd,'Client' as Role 
from xxparty 
where PartyId=@Tel");
                dt = ConnectSql_mb.GetDataTable(sql, list);
                if (dt.Rows.Count > 0)
                {
                    context = Common.StringToJson("Expire Account");
                }
                else
                {
                    context = Common.StringToJson("Account is not Exist");
                }
            }
        }
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void HomeContList_Get(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        //bool status = true;
        //string context = Common.StringToJson("");

        string user = SafeValue.SafeString(job["user"]);
        //string No = SafeValue.SafeString(job["No"]);
        //string date = SafeValue.SafeString(job["date"]);
        if (user.Length > 0)
        {
            DateTime toDate = DateTime.Today.AddDays(1);
            DateTime fromDate = DateTime.Today.AddMonths(-3);
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@Client", user, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@FromDate", fromDate.ToString("yyyyMMdd"), SqlDbType.NVarChar, 8));
            list.Add(new ConnectSql_mb.cmdParameters("@ToDate", toDate.ToString("yyyyMMdd"), SqlDbType.NVarChar, 8));

            string newJob = "[]";
            string returnJob = "[]";
            string exportJob = "[]";
            string returnJobPending = "[]";
            string completed = "[]";
            string transport = "[]";

            string sql = "";
            string sql_column = @"select det2.Id as tripId,det2.Det1Id as contId,job.Id as jobId,job.JobNo,
det2.FromDate,isnull(det2.FromTime,'00:00') as FromTime,det2.ToDate,isnull(det2.ToTime,'00:00') as ToTime,
det2.TowheadCode,det2.ChessisCode,det2.DriverCode,det2.DriverCode2,det2.TripCode,det2.Statuscode,det1.ScheduleDate,
job.ClientId,det1.ContainerNo,det1.SealNo,det2.FromCode,det2.ToCode,det1.ContainerType,convert(nvarchar,job.EtaDate,103) as EtaDate,job.EtaTime as EtaTime,
isnull(job.OperatorCode,'') as OperatorCode,isnull(job.Vessel,'') as Vessel,isnull(job.Voyage,'') as Voyage,
isnull(job.ClientRefNo,'') as MasterJobNo,job.JobType,
isnull(job.CarrierBkgNo,'') as CarrierBkgNo,det1.Weight,convert(nvarchar,job.EtdDate,103) as EtdDate,job.EtdTime as EtdTime
from tb_Trips as ts
left outer join ctm_jobdet2 as det2 on det2.Id=ts.Id
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join ctm_job as job on det2.JobNo=job.JobNo";
            string sql_where = "";


            #region select lists

            //================= new Job
            sql_where = " and det2.FromDate<@ToDate and job.ClientId=@Client";

            sql = string.Format(@"with tb_Trips as (
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
)temp where rowId=1 
)
{1}
order by det2.TripCode,convert(nvarchar(10),det2.FromDate,112),det2.FromTime
", sql_where, sql_column);
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            newJob = Common.DataTableToJson(dt);


            //================= exportJob
            sql_where = " and job.ClientId=@Client";
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
where rowId=1 
)
{1}
order by convert(nvarchar(10),job.EtaDate,112),job.EtaTime", sql_where, sql_column);
            dt = ConnectSql_mb.GetDataTable(sql, list);
            exportJob = Common.DataTableToJson(dt);



            //================= returnJob
            sql_where = " and job.ClientId=@Client";
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
where rowId=1
)
{1}
order by convert(nvarchar(10),job.ReturnLastDate,112)", sql_where, sql_column);
            dt = ConnectSql_mb.GetDataTable(sql, list);
            returnJob = Common.DataTableToJson(dt);



            //================= pending
            sql_where = " and job.ClientId=@Client";

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
where rowId=1 
)
{1}
order by job.JobType,case when job.JobType='IMP' then convert(nvarchar(10),job.ReturnLastDate,112) when job.JobType='EXP' then convert(nvarchar(10),job.EtaDate,112) else '17530101' end", sql_where, sql_column);

            dt = ConnectSql_mb.GetDataTable(sql, list);
            returnJobPending = Common.DataTableToJson(dt);



            //================= completed
            sql_where = " and det2.FromDate>@FromDate and job.ClientId=@Client";
            sql = string.Format(@"with tb_Trips as (
select det2.Id
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as job on det2.JobNo=job.JobNo
where det2.Statuscode='C' and (((det2.TripCode='EXP' or det2.TripCode='RET') and det1.StatusCode='Completed' ) or det2.TripCode='TPT')
and isnull(job.StatusCode,'USE')='USE' and (isnull(job.JobStatus,'')<>'Billing' and isnull(job.JobStatus,'')<>'Completed') {0}
)
{1}
order by det2.TripCode,convert(nvarchar(10),det2.FromDate,112),det2.FromTime", sql_where, sql_column);
            dt = ConnectSql_mb.GetDataTable(sql, list);
            completed = Common.DataTableToJson(dt);


            //================ transport
            sql_where = " and det2.FromDate<@ToDate and job.ClientId=@Client";

            sql = string.Format(@"with tb_Trips as (
select Id from (
select det2.Id,det2.SubCon_Ind,ROW_NUMBER()over(order by det2.Id) as rowId
from CTM_JobDet2 as det2
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.StatusCode='USE' and det2.Statuscode<>'C' and det2.Statuscode<>'X' 
and det2.TripCode='TPT' {0}
)temp where rowId=1 
)
{1}
order by det2.TripCode,convert(nvarchar(10),det2.FromDate,112),det2.FromTime
", sql_where, sql_column);
            dt = ConnectSql_mb.GetDataTable(sql, list);
            transport = Common.DataTableToJson(dt);

            #endregion


            string context = string.Format("{0}\"newJob\":{2},\"returnJob\":{3},\"exportJob\":{4},\"pendingReturn\":{5},\"completed\":{6},\"transport\":{7}{1}", "{", "}", newJob, returnJob, exportJob, returnJobPending, completed, transport);
            Common.WriteJsonP(true, context);
        }
        else
        {
            Common.WriteJsonP(false, Common.StringToJson("no permission"));
        }
    }
    [WebMethod]
    public void ContainerList_Get(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        //bool status = true;
        //string context = Common.StringToJson("");

        string user = SafeValue.SafeString(job["user"]);
        string No = SafeValue.SafeString(job["No"]);
        string fromDate = SafeValue.SafeString(job["fromDate"]);
        string toDate = SafeValue.SafeString(job["toDate"]);


        if (user.Length > 0)
        {
            string sql = string.Format(@"select cont.Id as contId,cont.ContainerNo,cont.JobNo,job.JobDate
from CTM_JobDet1 as cont
left outer join CTM_Job as job on cont.JobNo = job.JobNo
where job.ClientId = @user and job.JobNo is not null and job.JobDate>=@fromDate and job.JobDate<@toDate");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@fromDate", fromDate, SqlDbType.NVarChar, 8));
            list.Add(new ConnectSql_mb.cmdParameters("@toDate", toDate, SqlDbType.NVarChar, 8));
            if (No.Length > 0)
            {
                list.Add(new ConnectSql_mb.cmdParameters("@ContNo", "%" + No + "%", SqlDbType.NVarChar, 100));
                sql += " and cont.ContainerNo like @ContNo";
            }
            sql += " order by job.JobDate desc";
            //throw new Exception(sql);
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            Common.WriteJsonP(true, Common.DataTableToJson(dt));
        }else
        {
            Common.WriteJsonP(false, "[]");
        }
    }

    [WebMethod]
    public void TransportDetail_Get(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        bool status = false;
        string context = "{}";
        string user = SafeValue.SafeString(job["user"]);
        int No = SafeValue.SafeInt(job["No"], 0);

        if (No > 0)
        {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@Id", No, SqlDbType.Int));
            status = true;
            string mast = "{}";
                string trips = "[]";
                string photo = "[]";
                string invoice = "[]";

                //string JobNo = SafeValue.SafeString(dt.Rows[0]["JobNo"]);
                //list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));

                string sql = string.Format(@"select Id,FromDate,ToDate,FromTime,ToTime,FromCode,ToCode,Statuscode,TripCode,
(Case statusCode when 'A' then 5 when 'S' then 6 when 'C' then 7 else 10 end) as Inx,convert(nvarchar(10),FromDate,120) as FromDate1 
from CTM_JobDet2 
where Id=@Id and Statuscode in ('C','S','A','P')
order by Inx,FromDate1 desc,FromTime desc");
                DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                trips = Common.DataTableToJson(dt);


                sql = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment 
where TripId=@Id and FileType='Image'");
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
                photo = Common.DataTableToJson(dt);

                sql = string.Format(@"select inv.DocNo,inv.DocDate,inv.DocType,det2.JobNo,det.ChgCode,det.ChgDes1,det.DocLineNo,det.SequenceId as detId,
det.GstType,det.Qty,det.Price,det.Unit,det.Currency,det.ExRate,det.Gst,isnull(det.GstAmt,0) as GstAmt,isnull(det.DocAmt,0) as DocAmt,det.LocAmt,det.JobRefNo
from XAArInvoice as inv
left outer join CTM_JobDet2 as det2 on det2.jobNo=inv.MastRefNo
left outer join XAArInvoiceDet as det on inv.SequenceId=det.DocId
where det2.Id=@Id and inv.DocType='IV'
order by inv.SequenceId,DocLineNo");
                dt = ConnectSql_mb.GetDataTable(sql, list);
                invoice = Common.DataTableToJson(dt);


                context = string.Format(@"{0}mast:{2},trips:{3},photo:{4},invoice:{5}{1}", "{", "}", mast, trips, photo, invoice);
            
        }
        else
        {
            context = Common.StringToJson("Requair No");
        }

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void ContainerDetail_Get(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        bool status = false;
        string context = "{}";
        string user = SafeValue.SafeString(job["user"]);
        int No = SafeValue.SafeInt(job["No"],0);

        if (No > 0)
        {
            string sql = string.Format(@"select cont.Id as contId,cont.ContainerNo,cont.JobNo,job.JobDate,cont.ContainerType,cont.SealNo,cont.Weight,job.Vessel,job.Voyage,
cont.CfsStatus,job.ReleaseToHaulierRemark,job.Remark as JobRemark,isnull(cont.Remark1,'') as ContRemark,
cont.ScheduleDate,isnull(cont.PortnetStatus,'') as PortnetStatus,
'Y' as ClientRemark_Ind,'Client Remark' as ClientRemark_lb,cont.Remark2 as ClientRemark,
'N' as v1_Ind,'N' as v2_Ind,
(select count(*) from ctm_jobdet2 as det2 where det1Id=cont.Id and det2.Statuscode<>'C' ) as unC_trips,
(select top 1 TripCode from ctm_jobdet2 as det2 where det1Id=cont.Id and det2.Statuscode<>'C' ) as unC_tripCode
from CTM_JobDet1 as cont
left outer join CTM_Job as job on cont.JobNo=job.JobNo
where cont.Id=@Id");
            //==job.ClientId=@user and 
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@user", user, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@Id", No, SqlDbType.Int));

            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

            if (dt.Rows.Count > 0)
            {
                status = true;
                string mast = Common.DataRowToJson(dt);
                string trips = "[]";
                string photo = "[]";
                string invoice = "[]";

                //string JobNo = SafeValue.SafeString(dt.Rows[0]["JobNo"]);
                //list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));

                sql = string.Format(@"select Id,FromDate,ToDate,FromTime,ToTime,FromCode,ToCode,Statuscode,TripCode,
(Case statusCode when 'A' then 5 when 'S' then 6 when 'C' then 7 else 10 end) as Inx,convert(nvarchar(10),FromDate,120) as FromDate1 
from CTM_JobDet2 
where det1Id=@Id and Statuscode in ('C','S','A')
order by Inx,FromDate1 desc,FromTime desc");
                dt = ConnectSql_mb.GetDataTable(sql, list);
                trips = Common.DataTableToJson(dt);


                sql = string.Format(@"select att.Id,att.FileType,att.FileName,att.FilePath,att.FileNote
From CTM_Attachment as att
left outer join CTM_JobDet1 as cont on att.ContainerNo=cont.ContainerNo and att.RefNo=cont.JobNo
where cont.Id=@Id and FileType='Image'");
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
                photo = Common.DataTableToJson(dt);

                sql = string.Format(@"select inv.DocNo,inv.DocDate,inv.DocType,det1.JobNo,det.ChgCode,det.ChgDes1,det.DocLineNo,det.SequenceId as detId,
det.GstType,det.Qty,det.Price,det.Unit,det.Currency,det.ExRate,det.Gst,isnull(det.GstAmt,0) as GstAmt,isnull(det.DocAmt,0) as DocAmt,det.LocAmt,det.JobRefNo
from XAArInvoice as inv
left outer join CTM_JobDet1 as det1 on det1.jobNo=inv.MastRefNo
left outer join XAArInvoiceDet as det on inv.SequenceId=det.DocId
where det1.Id=@Id and inv.DocType='IV'
order by inv.SequenceId,DocLineNo");
                dt = ConnectSql_mb.GetDataTable(sql, list);
                invoice = Common.DataTableToJson(dt);


                context = string.Format(@"{0}mast:{2},trips:{3},photo:{4},invoice:{5}{1}", "{", "}", mast, trips, photo, invoice);
            }
            else
            {
                context = Common.StringToJson("No permission");
            }
        }
        else
        {
            context = Common.StringToJson("Requair No");
        }

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void ContainerDetail_readyReturn(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        bool status = false;
        string context = "";
        string user = SafeValue.SafeString(job["user"]);
        int contId = SafeValue.SafeInt(job["contId"], 0);
        string remark = SafeValue.SafeString(job["remark"]);
        string mobileNo = SafeValue.SafeString(job["mobileNo"]);

        if (contId > 0)
        {

            C2.CtmJobDet1Biz bz = new C2.CtmJobDet1Biz(contId);
            C2.CtmJobDet1 det1 = bz.getData();

            if (det1 != null)
            {
                det1.StatusCode = "Customer-MT";
                det1.CfsStatus = "Completed";
                det1.ScheduleStartDate = DateTime.Now;
                det1.ScheduleStartTime = DateTime.Now.ToString("HH:mm");
                det1.CompletionDate = det1.ScheduleStartDate;
                det1.CompletionTime = det1.ScheduleStartTime;
                det1.Remark2 = remark;
                C2.BizResult result = bz.update("skip");
                if (result.status)
                {
                    status = true;
                    C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                    elog.Platform_isWeb();
                    elog.Controller = user;
                    elog.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, -1, "Container Empty ["+mobileNo+"]: "+(remark.Length>0?" :"+remark:""));
                    elog.log();
                }
            }
        }
        else
        {
            context = "Data Error";
        }
        Common.WriteJsonP(status, Common.StringToJson(context));
    }

    [WebMethod]
    public void ContainerDetail_readyExport(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        bool status = false;
        string context = "";
        string user = SafeValue.SafeString(job["user"]);
        int contId = SafeValue.SafeInt(job["contId"], 0);
        decimal wt = SafeValue.SafeDecimal(job["wt"], 0);
        string remark = SafeValue.SafeString(job["remark"]);
        string mobileNo = SafeValue.SafeString(job["mobileNo"]);

        if (contId > 0)
        {

            C2.CtmJobDet1Biz bz = new C2.CtmJobDet1Biz(contId);
            C2.CtmJobDet1 det1 = bz.getData();

            if (det1 != null)
            {
                det1.Weight = wt;
                det1.StatusCode = "Customer-LD";
                det1.CfsStatus = "Completed";
                det1.ScheduleStartDate = DateTime.Now;
                det1.ScheduleStartTime = DateTime.Now.ToString("HH:mm");
                det1.CompletionDate = det1.ScheduleStartDate;
                det1.CompletionTime = det1.ScheduleStartTime;
                det1.Remark2 = remark;
                C2.BizResult result = bz.update("skip");
                if (result.status)
                {
                    status = true;
                    C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                    elog.Platform_isWeb();
                    elog.Controller = user;
                    elog.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, -1, "Container Ready Export [" + mobileNo + "]: " + (remark.Length > 0 ? " :" + remark : ""));
                    elog.log();
                }
            }
        }
        else
        {
            context = "Data Error";
        }
        Common.WriteJsonP(status, Common.StringToJson(context));
    }



}
