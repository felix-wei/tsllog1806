using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_Review 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Connect_Review : System.Web.Services.WebService
{
    public Connect_Review()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void Review_Job_Search(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        int jobId = SafeValue.SafeInt(job["No"].ToString(), 0);

        string sql = string.Format(@"select Id,JobNo,JobDate,ClientId,ClientRefNo
,(select top 1 DriverCode from ctm_jobdet2 where JobNo=job.JobNo order by Id desc) as DriverCode
,(select top 1 TowheadCode from ctm_jobdet2 where JobNo=job.JobNo order by Id desc) as TowheadCode 
from CTM_Job as job
where Id=@jobId");

        list.Add(new ConnectSql_mb.cmdParameters("@jobId", jobId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = "{}";
        string cargo = "[]";
        if (dt.Rows.Count == 1)
        {
            mast = Common.DataRowToJson(dt);
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"].ToString(), SqlDbType.NVarChar, 100));
            sql = string.Format(@"select Id,HblNo,CargoType,QtyOrig Qty,PackTypeOrig as PackType from job_house where JobNo=@JobNo");
            dt = ConnectSql_mb.GetDataTable(sql, list);
            cargo = Common.DataTableToJson(dt);
        }

        string context = string.Format(@"{0}mast:{2},cargo:{3}{1}", "{", "}", mast, cargo);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void Review_In_Search(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string status = "0";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        //        string sql = string.Format(@"select top 10 Id as Oid,HblNo as DnNo,(case when HblNo=@DnNo then 0 else 1 end) as ind  
        //from job_house where HblNo like @DnNo1 and CargoType='IN'
        //order by ind,DnNo");
        string sql = string.Format(@"select top 20 *,(case when DnNo=@DnNo then 0 else 1 end) as ind   from(
select Id as Oid,HblNo as DnNo,'HBLNO' as RowType
from job_house 
where HblNo like @DnNo1 and CargoType='IN' and JobType='WGR'
union all
select Id as Oid,JobNo as DnNo,'DONO' as RowType
from ctm_job
where JobNo like @DnNo1 and (JobType='WGR' )
) t
order by ind,Oid desc");

        list.Add(new ConnectSql_mb.cmdParameters("@DnNo", job["no"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DnNo1", job["no"] + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Review_In_Save(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string status = "0";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        //string sql = string.Format(@"update job_receipt set StatusCode=@StatusCode,ReceiveDate=getdate(),UserId=@UserId where row_id=@row_id");
        string sql = string.Format(@"update job_house set CargoStatus=@StatusCode,EntryDate=getdate() where Id=@row_id");
        list.Add(new ConnectSql_mb.cmdParameters("@row_id", SafeValue.SafeInt(job["Oid"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", job["StatusCode"], SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@UserId", job["UserId"], SqlDbType.NVarChar, 100));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            status = "1";

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCARGO(SafeValue.SafeInt(job["Oid"], 0));
            //lg.Remark = "Cargo status change:" + job["StatusCode"];
            lg.setActionLevel(SafeValue.SafeInt(job["Oid"], 0), CtmJobEventLogRemark.Level.Cargo, 4, ":" + job["StatusCode"]);
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Review_Out_Search(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string status = "0";
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        //        string sql = string.Format(@"select top 10 row_id as Oid,DnNo,(case when DnNo=@DnNo then 0 else 1 end) as ind  
        //from job_receipt where DnNo like @DnNo1 and JobType='E'
        //order by ind,DnNo");
        string sql = string.Format(@"select top 20 *,(case when DnNo=@DnNo then 0 else 1 end) as ind   from(
select Id as Oid,HblNo as DnNo,'HBLNO' as RowType
from job_house 
where HblNo like @DnNo1 and CargoType='OUT' and JobType='WDO'
union all
select Id as Oid,JobNo as DnNo,'DONO' as RowType
from ctm_job
where JobNo like @DnNo1 and (JobType='WDO')
) t
order by ind,Oid desc");
        list.Add(new ConnectSql_mb.cmdParameters("@DnNo", job["no"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DnNo1", job["no"] + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Review_Out_Save(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string status = "0";
        string context = Common.StringToJson("");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        //string sql = string.Format(@"update job_receipt set StatusCode=@StatusCode,ReceiveDate=getdate(),UserId=@UserId where row_id=@row_id");
        string sql = string.Format(@"update job_house set CargoStatus=@StatusCode,EntryDate=getdate() where Id=@row_id");
        list.Add(new ConnectSql_mb.cmdParameters("@row_id", SafeValue.SafeInt(job["Oid"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", job["StatusCode"], SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@UserId", job["UserId"], SqlDbType.NVarChar, 100));
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).context.Equals("1"))
        {
            status = "1";
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCARGO(SafeValue.SafeInt(job["Oid"], 0));
            //lg.Remark = "Cargo status change:" + job["StatusCode"];
            lg.setActionLevel(SafeValue.SafeInt(job["Oid"], 0), CtmJobEventLogRemark.Level.Cargo, 4, ":" + job["StatusCode"]);
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Review_Detail_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string status = "0";
        string context = Common.StringToJson("");
        int cargoId = SafeValue.SafeInt(job["Oid"].ToString(), 0);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DnNo", job["DnNo"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@cargoId", cargoId, SqlDbType.Int));

        //        string sql = string.Format(@"select row_id as Oid,RecNo,RecDate,DnNo,JobNo as JobOrder,JobType,ContNo,CustCode,RecFrom,HblN,VesselNo,PortLoad,Qty,PackType,Weight,M3,Eta,Markings,Contents,
        //'' as Driver_Code,StatusCode 
        //from job_receipt where DnNo=@DnNo");
        string sql = string.Format(@"select top 1 cargo.Id as Oid,RecDate,HblNo as DnNo,job.JobNo as JobOrder,cargo.JobType,ContNo,HblNo as HblN,
QtyOrig Qty,PackTypeOrig as PackType,WeightOrig as Weight,VolumeOrig as M3,Eta,Marking1 as Markings,Remark1 as Contents,
'' as Driver_Code,CargoStatus as StatusCode,Qty as BKQty,Weight as BKWeight,Volume as BKM3,job.ClientRefNo 
from job_house as cargo
left outer join ctm_job as job on cargo.JobNo=job.JobNo
where cargo.Id=@cargoId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataRowToJson(dt);


        if (dt.Rows.Count == 1)
        {
            string JobNo = dt.Rows[0]["JobOrder"].ToString();
            status = "1";
            //            sql = string.Format(@"select p.row_id as Oid,UploadType as FileType,Name as FileName,Path as FilePath,'' as FileNote
            //from job_photo as p
            //where DoNo=@DnNo and isnull(JobNo,'')=''");
            sql = string.Format(@"select p.Id as Oid,FileType,FileName,FilePath,FileNote
from CTM_Attachment as p
where JobNo=@cargoIdStr ");
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("cargoIdStr", cargoId, SqlDbType.NVarChar, 100));
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
            string attachment = Common.DataTableToJson(dt);

            //            sql = string.Format(@"select p.row_id as Oid,UploadType as FileType,Name as FileName,Path as FilePath,'' as FileNote,d.row_id 
            //from job_photo as p
            //left outer join job_det as d on d.HblN=p.DoNo collate Chinese_PRC_CI_AS and d.JobNo=p.JobNo
            //where DoNo=@DnNo and d.row_id>0");
            sql = string.Format(@"select p.Id as Oid,FileType,FileName,FilePath,FileNote
from CTM_Attachment as p
where RefNo=@JobNo and JobNo<>@cargoIdStr");
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
            string detAttachment = Common.DataTableToJson(dt);


            context = string.Format(@"{0}mast:{2},attachment:{3},detAttachment:{4}{1}", "{", "}", mast, attachment, detAttachment);
        }
        else
        {
            context = Common.StringToJson("This No inexistence");
        }
        Common.WriteJsonP(status, context);
    }


    [WebMethod]
    public void Review_Detail_Save(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        bool status = false;
        string context = Common.StringToJson("");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Oid", SafeValue.SafeInt(job["Oid"], 0), SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@Qty", SafeValue.SafeInt(job["Qty"], 0), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Weight", SafeValue.SafeDecimal(job["Weight"]), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@M3", SafeValue.SafeDecimal(job["M3"]), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@PackType", job["PackType"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Markings", job["Markings"], SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Contents", job["Contents"], SqlDbType.NVarChar, 300));

        //string sql = string.Format(@"update job_receipt set Qty=@Qty,Weight=@Weight,M3=@M3,PackType=@PackType,Markings=@Markings,Contents=@Contents where row_id=@Oid");
        string sql = string.Format(@"update job_house set QtyOrig=@Qty,WeightOrig=@Weight,VolumeOrig=@M3,PackTypeOrig=@PackType,Marking1=@Markings,Remark1=@Contents where Id=@Oid");
        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (result.status)
        {
            status = true;
            context = Common.StringToJson(result.context);


            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.ActionLevel_isCARGO(SafeValue.SafeInt(job["Oid"], 0));
            //lg.Remark = "Cargo update";
            lg.setActionLevel(SafeValue.SafeInt(job["Oid"], 0), CtmJobEventLogRemark.Level.Cargo, 3);
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Review_Save_Check_New(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        string status = "0";
        string context = Common.StringToJson("");
        string type = job["Type"].ToString();
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DnNo", job["JobNo"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Type", job["Type"], SqlDbType.NVarChar, 20));

        //        if (type.Equals("I"))
        //        {
        //            string sql = string.Format(@"select row_id as Oid,DnNo from job_receipt where DnNo=@DnNo");
        //            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        //            if (dt.Rows.Count == 0)
        //            {
        //                //sql = string.Format(@"insert into job_receipt (DnNo,JobType) values (@DnNo,@Type)");
        //                //ConnectSql_mb.ExecuteNonQuery(sql, list);

        //                sql = string.Format(@"select * from job_det where HblN=@DnNo");
        //                dt = ConnectSql_mb.GetDataTable(sql, list);

        //                string recNo = GetNo("CFS-REC");
        //                list.Add(new ConnectSql_mb.cmdParameters("@RecNo", recNo, SqlDbType.NVarChar, 100));
        //                if (dt.Rows.Count == 1)
        //                {
        //                    sql = string.Format(@"insert into job_receipt (DnNo,JobType,RecNo,RecDate,RptDate,JobNo,ContNo,VesselNo,Qty,PackType,Weight,M3,Eta)
        //(select HblN,d.JobType,@RecNo,getdate(),getdate(),d.JobNo,j.ContNo,j.VesselNo,d.TotQty,d.PackType,d.Weight,d.M3,j.Eta 
        //from job_det as d 
        //left outer join job_order as j on d.JobNo=j.JobNo
        //where HblN=@DnNo and d.JobType=@Type)");
        //                }
        //                else
        //                {
        //                    sql = string.Format(@"insert into job_receipt (DnNo,JobType,RecNo,RecDate) values (@DnNo,@Type,@RecNo,getdate())");
        //                }
        //                ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        //                if (res.context.Equals("1"))
        //                {
        //                    SetNo(recNo, "CFS-REC");
        //                    CTM_JobEventLog l = new CTM_JobEventLog();
        //                    l.JobNo = recNo;
        //                    l.JobType = "Receipt";
        //                    l.Remark = "New receipt: " + recNo;
        //                    l.log();
        //                }
        //            }
        //        }

        Common.WriteJsonP(status, context);
    }


    //private string GetNo(string noType)
    //{
    //    string sql = "select Counter from sys_counter where Category='" + noType + "'";
    //    int cnt = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0) + 1;

    //    return cnt.ToString();

    //}
    //private void SetNo(string no, string noType)
    //{
    //    string sql = string.Format("update sys_counter set counter='{0}' where category='{1}'", no, noType);
    //    int res = ConnectSql_mb.ExecuteNonQuery(sql);
    //}

    [WebMethod]
    public void Review_Attachment_Add(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        //string sql = string.Format(@"insert into job_photo(DoNo,Name,Path,UploadType,EntryDate,OrderType) values (@DoNo,@FileName,@FilePath,@FileType,Getdate(),@OrderType)");
        string sql = string.Format(@"insert into CTM_Attachment(RefNo,ContainerNo,JobNo,FileName,FilePath,FileType,CreateDateTime,JobType) values (@RefNo,@ContainerNo,@cargoId,@FileName,@FilePath,@FileType,Getdate(),@JobType)
select @@Identity");
        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
        if (fileStart == null)
        {
            fileStart = "";
        }
        string fileEnd = job["FilePath"].ToString();
        fileEnd = fileEnd.Substring(0, fileEnd.LastIndexOf('/')) + "/500/" + fileEnd.Substring(fileEnd.LastIndexOf('/') + 1);
        fileEnd = fileStart + fileEnd;


        cpar = new ConnectSql_mb.cmdParameters("@RefNo", job["JobNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@cargoId", job["TripId"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", job["ContainerNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@JobType", "CTM", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        //cpar = new ConnectSql_mb.cmdParameters("@FileType", "Image", SqlDbType.NVarChar, 100);
        //list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileType", job["FileType"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileName", job["FileName"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FilePath", fileEnd, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["CreateBy"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@FileNote", job["FileNote"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
        if (res.status)
        {
            status = "1";

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            //lg.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(res.context, 0));
            //lg.Remark = "Attachment upload file:" + job["FileName"];
            lg.setActionLevel(SafeValue.SafeInt(res.context, 0), CtmJobEventLogRemark.Level.Attachment, 1, ":" + job["FileType"] + "[" + job["FileName"] + "]");
            lg.log();
        }
        else
        {
            context = Common.StringToJson(res.context);
        }

        Common.WriteJsonP(status, context);
    }


}
