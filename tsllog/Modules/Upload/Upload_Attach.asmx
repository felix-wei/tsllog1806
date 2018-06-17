<%@ WebService Language="C#" Class="Upload_Attach" %>

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Upload_Attach  : System.Web.Services.WebService {

    [WebMethod]
    public void Get_Data()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string jobNo = info["JobNo"].ToString();

        #region Job
        string sql = string.Format(@"select Id,JobNo,JobDate,JobType from ctm_job where JobNo=@JobNo");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataJob = Common.DataRowToJson(dt);
        #endregion

        #region Container
        sql = string.Format(@"select Id,ContainerNo from ctm_jobdet1 where JobNo=@JobNo");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataCont = Common.DataTableToJson(dt);
        #endregion

        #region Trip
        sql = string.Format(@"select Id,TripIndex,TripCode,ChessisCode,ToCode,ToParkingLot,DriverCode,FromDate from ctm_jobdet2 where JobNo=@JobNo");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
        dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataTrip = Common.DataTableToJson(dt);
        #endregion

        string context = string.Format("{0}\"job\":{2},\"cont\":{3},\"trip\":{4}{1}", "{", "}", DataJob,DataCont,DataTrip);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    ///添加文件
    public void Add_Attach()
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        string sql = string.Format(@"insert into CTM_Attachment (JobType,RefNo,ContainerNo,TripId,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values(@JobType,@RefNo,@ContainerNo,@TripId,@FileType,@FileName,@FilePath,@CreateBy,Getdate(),@FileNote)
select @@Identity");

        string fileEnd = job["FilePath"].ToString();
        //fileEnd = fileEnd.Substring(0, fileEnd.LastIndexOf('/')) + "/500/" + fileEnd.Substring(fileEnd.LastIndexOf('/') + 1);
        //fileEnd = fileStart + fileEnd;

        cpar = new ConnectSql_mb.cmdParameters("@JobType", "CTM", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@RefNo", job["RefNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", job["ContainerNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@TripId", job["TripId"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
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

        ConnectSql_mb.sqlResult result = ConnectSql_mb.ExecuteScalar(sql, list);
        if (result.status)
        {
            status = "1";

            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isWeb();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            lg.setActionLevel(SafeValue.SafeInt(result.context, 0), CtmJobEventLogRemark.Level.Attachment, 1, ":" + job["FileType"] + "[" + job["FileName"] + "]");
            //lg.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(result.context, 0));
            ////lg.Remark = "Attachment upload file:" + job["FileName"];
            //lg.setRemark(CtmJobEventLogRemark.Level.Attachment, 1, " " + job["FileType"] + "[" + job["FileName"] + "]");
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }
}