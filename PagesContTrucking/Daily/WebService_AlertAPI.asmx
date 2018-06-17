<%@ WebService Language="C#" Class="WebService_AlertAPI" %>

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
public class WebService_AlertAPI : System.Web.Services.WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    #region permit

    public string Permit_GetDataIDList()
    {
        string sql = string.Format(@"select det2.Id
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.TripCode='IMP' and datediff(d,getdate(),det2.FromDate)=1  and job.StatusCode<>'CNL' 
and len(isnull(job.PermitNo,''))<=10 and (det2.Statuscode<>'C' and det2.Statuscode<>'X')");
        return sql;
    }
    [WebMethod]
    public void Permit_GetList()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string sql = string.Format(@"select det2.Id as tripId,det1.Id as contId,job.Id as jobId,det2.JobNo,det1.ContainerNo,
det2.TripCode,det2.DriverCode,det2.TowheadCode,det2.FromDate,Det2.FromTime,Det2.FromCode,Det2.ToCode,job.PermitNo
from (
{0}
) as tb_id
left outer join CTM_JobDet2 as det2 on tb_id.Id=det2.Id
left outer join ctm_jobdet1 as det1 on  det2.det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo", Permit_GetDataIDList());

        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Permit_GetListSum()
    {
        string sql = string.Format(@"select count(*)
from (
{0}
) as tb_id", Permit_GetDataIDList());

        string context = ConnectSql_mb.ExecuteScalar(sql);
        Common.WriteJson(context);
    }
    #endregion



    public string MustReturn_GetDataIDList()
    {
        string sql = string.Format(@"select det2.Id
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.TripCode='RET' and job.StatusCode<>'CNL'  and datediff(d,getdate(),job.ReturnLastDate)<=2 and job.ReturnLastDate>'2017-01-01' and (det2.Statuscode<>'C' and det2.Statuscode<>'X')
");
        return sql;
    }
    [WebMethod]
    public void MustReturn_GetList()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string sql = string.Format(@"select det2.Id as tripId,det1.Id as contId,job.Id as jobId,det2.JobNo,det1.ContainerNo,
det2.TripCode,det2.DriverCode,det2.TowheadCode,det2.FromDate,Det2.FromTime,Det2.FromCode,Det2.ToCode,job.ReturnLastDate
from (
{0}
) as tb_id
left outer join CTM_JobDet2 as det2 on tb_id.Id=det2.Id
left outer join ctm_jobdet1 as det1 on  det2.det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo", MustReturn_GetDataIDList());

        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void MustReturn_GetListSum()
    {
        string sql = string.Format(@"select count(*)
from (
{0}
) as tb_id", MustReturn_GetDataIDList());

        string context = ConnectSql_mb.ExecuteScalar(sql);
        Common.WriteJson(context);
    }



    public string MustSend_GetDataIdList()
    {
        //        string sql = string.Format(@"select det2.Id
        //from CTM_JobDet2 as det2
        //left outer join CTM_Job as job on det2.JobNo=job.JobNo
        //where det2.TripCode='EXP' and det2.Statuscode<>'C' and 
        //(datediff(d,getdate(),job.EtaDate)<-1 or(datediff(hour,getdate(),convert(nvarchar,EtaDate,112)+' '+(case when len(EtaTime)=4 then substring(EtaTime,1,2)+':'+substring(EtaTime,3,2) else '00:00' end))<=12))

        //");
        string sql = string.Format(@"select det2.Id
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.TripCode='EXP' and (det2.Statuscode<>'C' and det2.Statuscode<>'X') and job.StatusCode<>'CNL'  and datediff(d,getdate(),job.EtaDate)<=1");
        return sql;
    }
    [WebMethod]
    public void MustSend_GetList()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string sql = string.Format(@"select det2.Id as tripId,det1.Id as contId,job.Id as jobId,det2.JobNo,det1.ContainerNo,
det2.TripCode,det2.DriverCode,det2.TowheadCode,det2.FromDate,Det2.FromTime,Det2.FromCode,Det2.ToCode,
convert(nvarchar,job.EtaDate,111)+' '+(case when len(job.EtaTime)=4 then substring(job.EtaTime,1,2)+':'+substring(job.EtaTime,3,2) else '00:00' end) as Eta
from (
{0}
) as tb_id
left outer join CTM_JobDet2 as det2 on tb_id.Id=det2.Id
left outer join ctm_jobdet1 as det1 on  det2.det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo", MustSend_GetDataIdList());

        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void MustSend_GetListSum()
    {
        string sql = string.Format(@"select count(*)
from (
{0}
) as tb_id", MustSend_GetDataIdList());

        string context = ConnectSql_mb.ExecuteScalar(sql);
        Common.WriteJson(context);
    }



    public string CDEM_GetDataIDList()
    {
        string sql = string.Format(@"select Id from 
(select det2.Id,
(select top 1 ToDate from ctm_jobdet2 as det2_imp where det2.det1Id=det2_imp.det1Id and det2_imp.Statuscode='C' and det2_imp.TripCode='IMP') as ToDate
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.TripCode='RET' and (det2.Statuscode<>'C' and det2.Statuscode<>'X') and job.StatusCode<>'CNL' 
) temp 
where datediff(day,temp.ToDate,getdate())>3
");
        return sql;
    }
    [WebMethod]
    public void CDEM_GetList()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string sql = string.Format(@"select det2.Id as tripId,det1.Id as contId,job.Id as jobId,det2.JobNo,det1.ContainerNo,
det2.TripCode,det2.DriverCode,det2.TowheadCode,det2.FromDate,Det2.FromTime,Det2.FromCode,Det2.ToCode,
(select top 1 ToDate from ctm_jobdet2 as det2_imp where det2.det1Id=det2_imp.det1Id and det2_imp.Statuscode='C' and det2_imp.TripCode='IMP') as ToDate
from (
{0}
) as tb_id
left outer join CTM_JobDet2 as det2 on tb_id.Id=det2.Id
left outer join ctm_jobdet1 as det1 on  det2.det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
order by ToDate", CDEM_GetDataIDList());

        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void CDEM_GetListSum()
    {
        string sql = string.Format(@"select count(*)
from (
{0}
) as tb_id", CDEM_GetDataIDList());

        string context = ConnectSql_mb.ExecuteScalar(sql);
        Common.WriteJson(context);
    }





    public string Charges_GetDataIDList()
    {
        //        string sql = string.Format(@"with tb_cont as (
        //select distinct
        //det1.Id,det1.JobNo,det1.ContainerNo,det1.ScheduleDate
        //from ctm_jobdet1 as det1 
        //left outer join job_cost as cc on det1.JobNo=cc.JobNo and det1.ContainerNo=cc.ContNo and cc.LineType='CL' and cc.Price>0
        //where cc.Id>0
        //)");
        string sql = string.Format(@"with tb_cont as (
select distinct
det1.Id,det1.JobNo,det1.ContainerNo,det1.ScheduleDate,job.JobType
from ctm_jobdet1 as det1 
left outer join ctm_job as job on det1.JobNo=job.JobNo
left outer join job_cost as cc on det1.JobNo=cc.JobNo and det1.ContainerNo=cc.ContNo and cc.LineType='CL' and cc.Price>0 and ChgCode<>'DHC'
left outer join CTM_JobEventLog as ll on det1.JobNo=ll.JobNo and det1.ContainerNo=ll.ContainerNo and ll.Note1Type='AlertCharges'
where det1.StatusCode='Completed' and cc.Id>0 and ll.Id is null
)");
        return sql;
    }
    [WebMethod]
    public void Charges_GetList()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string sql = string.Format(@"{0},
tb_cost as (
select cc.* from tb_cont as cont 
left outer join job_cost as cc on cc.JobNo=cont.JobNo and cc.ContNo=cont.ContainerNo and cc.LineType='CL' and cc.Price>0 and ChgCode<>'DHC'
)
select top 100 *,
(select isnull(ChgCode,'')+':'+cast(cast(isnull(cc.Qty,1)*isnull(cc.Price,0) as decimal(16,2)) as nvarchar)+'.' 
from tb_cost as cc where cont.JobNo=cc.JobNo and cont.ContainerNo=cc.ContNo for xml path('')) as text1,
cont.ScheduleDate as date1,
0 as tripId,Id as contId,0 as jobId,'-' as TripCode
From tb_cont as cont
order by cont.ScheduleDate desc", Charges_GetDataIDList());

        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Charges_GetListSum()
    {
        string sql = string.Format(@"{0}
select count(*) from tb_cont", Charges_GetDataIDList());

        string context = ConnectSql_mb.ExecuteScalar(sql);
        Common.WriteJson(context);
    }
    [WebMethod]
    public void Charges_alertIgnore()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int contId = SafeValue.SafeInt(info["contId"], 0);
        Charges_alertIgnore_part1(contId);
        Common.WriteJson(true, Common.StringToJson(""));
    }
    public void Charges_alertIgnore_part1(int contId)
    {
        string user = HttpContext.Current.User.Identity.Name;

        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = user;
        elog.Note1Type = "AlertCharges";
        elog.setActionLevel(contId, CtmJobEventLogRemark.Level.Container, -1);
        elog.Remark = "Charges alert ignore";
        elog.log();
    }




    public string Export_GetDataIDList()
    {
        //        string sql = string.Format(@"with tb_cont as (
        //select Id,JobNo,ContainerNo,SealNo,ScheduleDate 
        //from ctm_jobdet1 where StatusCode='Completed'
        //) ");
        string sql = string.Format(@"with tb_cont as (
select distinct 
det1.Id,det1.JobNo,det1.ContainerNo,det1.SealNo,det1.ScheduleDate,job.JobType
from ctm_jobdet1 as det1
left outer join ctm_job as job on det1.JobNo=job.JobNo
left outer join CTM_JobEventLog as ll on det1.JobNo=ll.JobNo and det1.ContainerNo=ll.ContainerNo and ll.Note1Type='AlertExport'
where (len(isnull(det1.ContainerNo,''))>10 or len(isnull(det1.sealno,''))>0) and job.JobType='EXP' and ll.Id is null
)");
        return sql;
    }
    [WebMethod]
    public void Export_GetList()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string sql = string.Format(@"{0}
select top 100 *,
SealNo as text1,cont.ScheduleDate as date1
from tb_cont as cont
order by cont.ScheduleDate desc", Export_GetDataIDList());

        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Export_GetListSum()
    {
        string sql = string.Format(@"{0}
select count(*) from tb_cont", Export_GetDataIDList());

        string context = ConnectSql_mb.ExecuteScalar(sql);
        Common.WriteJson(context);
    }
    [WebMethod]
    public void Export_alertIgnore()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int contId = SafeValue.SafeInt(info["contId"], 0);
        Export_alertIgnore_part1(contId);
        Common.WriteJson(true, Common.StringToJson(""));
    }
    public void Export_alertIgnore_part1(int contId)
    {
        string user = HttpContext.Current.User.Identity.Name;

        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = user;
        elog.Note1Type = "AlertExport";
        elog.setActionLevel(contId, CtmJobEventLogRemark.Level.Container, -1);
        elog.Remark= "Export alert ignore";
        elog.log();
    }



    [WebMethod]
    public void getEmailDetail_byContNo()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int contId = SafeValue.SafeInt(info["contId"], 0);

        string sql = string.Format(@"select job.EmailAddress,det1.ContainerNo,job.ClientId,
det1.JobNo,det1.ContainerNo as title,det1.Id as contId
from ctm_jobdet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where det1.Id=@contId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = string.Format("{0}\"mast\":{2}{1}", "{", "}", Common.DataRowToJson(dt));
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void email_byContNo()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int contId = SafeValue.SafeInt(info["contId"], 0);
        string type1 = SafeValue.SafeString(info["type"]);
        string emailTo = SafeValue.SafeString(info["emailTo"]);
        string emailCc = SafeValue.SafeString(info["emailCc"]);
        string emailSubject = SafeValue.SafeString(info["emailSubject"]);
        string emailContent = SafeValue.SafeString(info["emailContent"]);

        email_send(contId, emailTo, emailCc, emailSubject, emailContent);
        switch (type1)
        {
            case "Export":
                Export_alertIgnore_part1(contId);
                break;
            case "Charges":
                Charges_alertIgnore_part1(contId);
                break;
        }
        Common.WriteJson(true, Common.StringToJson(""));
    }

    public void email_send(int Det1Id,string emailTo,string emailCc,string emailSubject,string emailContent)
    {

        Helper.Email.SendEmail(emailTo, emailCc, "", emailSubject, emailContent, "");

        C2.CtmJobDet1Biz bz = new C2.CtmJobDet1Biz(Det1Id);
        C2.CtmJobDet1 det1 = bz.getData();
        if (det1 != null)
        {
            det1.EmailInd = "Y";
            bz.update(HttpContext.Current.User.Identity.Name);
        }
        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Platform_isWeb();
        lg.Controller = HttpContext.Current.User.Identity.Name;
        lg.setActionLevel(Det1Id, CtmJobEventLogRemark.Level.Container, -1);
        lg.Remark = "Sent e-mail:" + emailTo + ", Cc:" + emailCc;
        lg.log();
    }


    //======================== vehicle
    public string Vehcile_GetDataIDList()
    {
        string sql = string.Format(@"VehicleStatus='Active' and VehicleType='Towhead' and (datediff(d,@Date,LicenseExpiryDate)<30 or datediff(d,@Date,date2)<30)");
        return sql;
    }
    [WebMethod]
    public void Vehcile_GetList()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string date = SafeValue.SafeString(info["From"]);
        string sql = string.Format(@"select Id,VehicleCode,LicenseExpiryDate,Date2
from ref_Vehicle
where {0}", Vehcile_GetDataIDList());
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Date", date, SqlDbType.NVarChar, 10));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Vehcile_GetListSum()
    {
        string sql = string.Format(@"select count(*)
from ref_Vehicle
where {0}", Vehcile_GetDataIDList());

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Date", DateTime.Now.ToString("yyyyMMdd"), SqlDbType.NVarChar, 10));

        string context = ConnectSql_mb.ExecuteScalar(sql, list).context;
        Common.WriteJson(context);
    }




    //======================== pass
    public string Pass_GetDataIDList()
    {
        string sql = string.Format(@"datediff(d,@Date,Date1)<30 or datediff(d,@Date,Date2)<30");
        return sql;
    }
    [WebMethod]
    public void Pass_GetList()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string date = SafeValue.SafeString(info["From"]);
        string sql = string.Format(@"select e.Id,e.Name,  e.Telephone, p.ExpiryDate, p.Description, p.Remark 
from Hr_Person e, Pass_Certificate p
where p.Employee=e.Id and ExpiryDate>'2018-01-01' and {0}", @"datediff(d,@Date,p.ExpiryDate)<60 ");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Date", date, SqlDbType.NVarChar, 10));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataTableToJson(dt);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Pass_GetListSum()
    {
        string sql = string.Format(@"select Count(*)
from Hr_Person e, Pass_Certificate p
where p.Employee=e.Id and ExpiryDate>'2018-01-01' and {0}", @"datediff(d,@Date,p.ExpiryDate)<60 ");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Date", DateTime.Now.ToString("yyyyMMdd"), SqlDbType.NVarChar, 10));

        string context = ConnectSql_mb.ExecuteScalar(sql, list).context;
        Common.WriteJson(context);
    }


}