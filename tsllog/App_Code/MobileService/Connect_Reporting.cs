using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_Reporting 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect_Reporting : System.Web.Services.WebService
{

    public Connect_Reporting()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    #region FuelLog

    //======================== FuelLog
    [WebMethod]
    public void Vehcile_FuelLog_GetList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql_where = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where = "and CreateBy=@CreateBy";
        }
        string sql = string.Format(@"select * from Vehicle_FuelLog 
where datediff(day,ReportDate,@from)<=0 and datediff(day,ReportDate,@to)>=0 {0}
order by ReportDate", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.DateTime);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.DateTime);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Vehcile_FuelLog_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select * from Vehicle_FuelLog where Id=@Id");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["JobNo"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Vehcile_FuelLog_Save(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = "";
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["VehicleNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Type", job["Type"], SqlDbType.NVarChar, 10);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Volume", job["Volume"], SqlDbType.Decimal);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Amount", job["Amount"], SqlDbType.Decimal);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Note", job["Note"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ReportDate", job["ReportDate"], SqlDbType.DateTime);
        list.Add(cpar);
        ConnectSql_mb.sqlResult re = null;

        if (job["Id"].ToString().Equals("") || job["Id"].ToString().Equals("0"))
        {
            sql = string.Format(@"insert into Vehicle_FuelLog (VehicleNo,CreateDateTime,[Type],Volume,Amount,Note,CreateBy,ReportDate) values(@VehicleNo,getdate(),@Type,@Volume,@Amount,@Note,@CreateBy,@ReportDate)
select @@Identity");
            re = ConnectSql_mb.ExecuteScalar(sql, list);
            if (re.status)
            {
                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                lg.setActionLevel(SafeValue.SafeInt(re.context, 0), CtmJobEventLogRemark.Level.VehcileReport, 1);
                lg.log();
            }
        }
        else
        {
            sql = string.Format(@"update Vehicle_FuelLog set VehicleNo=@VehicleNo,[Type]=@Type,Volume=@Volume,Amount=@Amount,Note=@Note,ReportDate=@ReportDate where Id=@Id");
            re = ConnectSql_mb.ExecuteNonQuery(sql, list);
            if (re.status)
            {
                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.VehcileReport, 3);
                lg.log();
            }
        }
        if (!re.status || re.context.Equals("0"))
        {
            status = "0";
            context = Common.StringToJson("Save Error");
        }


        Common.WriteJsonP(status, context);
    }

    #endregion

    #region Mileage
    //======================== Mileage
    [WebMethod]
    public void Vehcile_Mileage_GetList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        //string sql = string.Format(@"select * from Vehicle_Mileage where VehicleNo like @VehicleNo order by Id desc");
        string sql_where = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where = "and CreateBy=@CreateBy";
        }
        string sql = string.Format(@"select * from Vehicle_Mileage 
where datediff(day,ReportDate,@from)<=0 and datediff(day,ReportDate,@to)>=0 {0}
order by ReportDate", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.DateTime);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.DateTime);
        list.Add(cpar);

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Vehcile_Mileage_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select * from Vehicle_Mileage where Id=@Id");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["JobNo"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);
    }

    [WebMethod]
    public void Vehcile_Mileage_Save(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");

        string sql_where = " and Id<@Id";

        if (job["Id"].ToString().Equals("") || job["Id"].ToString().Equals("0"))
        {
            sql_where = "";
        }
        string sql = string.Format(@"select isnull(max(value),0) from Vehicle_Mileage where createby=@CreateBy and VehicleNo=@VehicleNo {0}", sql_where);
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["VehicleNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);

        decimal maxValue = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
        if (maxValue > SafeValue.SafeDecimal(job["Value"]))
        {
            status = "0";
            context = Common.StringToJson("Mileage must more than " + maxValue);
        }
        else
        {
            list = new List<ConnectSql_mb.cmdParameters>();
            cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["VehicleNo"], SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Value", job["Value"], SqlDbType.Decimal);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note", job["Note"], SqlDbType.NVarChar, 300);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ReportDate", job["ReportDate"], SqlDbType.NVarChar, 30);
            list.Add(cpar);
            ConnectSql_mb.sqlResult re = null;
            if (job["Id"].ToString().Equals("") || job["Id"].ToString().Equals("0"))
            {
                sql = string.Format(@"insert into Vehicle_Mileage (VehicleNo,CreateDateTime,Value,Note,CreateBy,ReportDate) values(@VehicleNo,getdate(),@Value,@Note,@CreateBy,@ReportDate)
select @@Identity");
                re = ConnectSql_mb.ExecuteScalar(sql, list);
                if (re.status)
                {
                    //===========log
                    C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                    lg.Platform_isMobile();
                    lg.Controller = SafeValue.SafeString(job["user"]);
                    lg.Lat = SafeValue.SafeString(job["Lat"]);
                    lg.Lng = SafeValue.SafeString(job["Lng"]);
                    lg.setActionLevel(SafeValue.SafeInt(re.context, 0), CtmJobEventLogRemark.Level.VehcileReport, 5);
                    lg.log();
                }
            }
            else
            {
                sql = string.Format(@"update Vehicle_Mileage set VehicleNo=@VehicleNo,Value=@Value,Note=@Note,ReportDate=@ReportDate where Id=@Id");
                re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                if (re.status)
                {
                    //===========log
                    C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                    lg.Platform_isMobile();
                    lg.Controller = SafeValue.SafeString(job["user"]);
                    lg.Lat = SafeValue.SafeString(job["Lat"]);
                    lg.Lng = SafeValue.SafeString(job["Lng"]);
                    lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.VehcileReport, 7);
                    lg.log();
                }
            }

            if (!re.status || re.context.Equals("0"))
            {
                status = "0";
                context = Common.StringToJson("Save Error");
            }
        }
        Common.WriteJsonP(status, context);
    }
    #endregion


    #region IssueReport

    //==================== IssueReport

    [WebMethod]
    public void Vehcile_IssueReport_GetList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        //string sql = string.Format(@"select * from Vehicle_IssueReport where VehicleNo like @VehicleNo order by Id desc");
        string sql_where = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where = "and CreateBy=@CreateBy";
        }
        string sql = string.Format(@"select * from Vehicle_IssueReport 
where datediff(day,ReportDate,@from)<=0 and datediff(day,ReportDate,@to)>=0 {0}
order by ReportDate", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.DateTime);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.DateTime);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }


    [WebMethod]
    public void Vehcile_IssueReport_GetList_default(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        //string sql = string.Format(@"select * from Vehicle_IssueReport where VehicleNo like @VehicleNo order by Id desc");
        string sql_where = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where = "and CreateBy=@CreateBy";
        }
        string sql = string.Format(@"select * from Vehicle_IssueReport 
where datediff(day,ReportDate,@from)<=0 and datediff(day,ReportDate,@to)>=0 and (ActionType='' or ActionType='Prime Mover' or ActionType='Trailer' or ActionType='Accident' or ActionType='Others') {0}
order by ReportDate", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.DateTime);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.DateTime);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }
    [WebMethod]
    public void Vehcile_IssueReport_GetList_PrimMoverTyre(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        //string sql = string.Format(@"select * from Vehicle_IssueReport where VehicleNo like @VehicleNo order by Id desc");
        string sql_where = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where = "and CreateBy=@CreateBy";
        }
        string sql = string.Format(@"select * from Vehicle_IssueReport 
where datediff(day,ReportDate,@from)<=0 and datediff(day,ReportDate,@to)>=0 and ActionType='Prime Mover Tyre' {0}
order by ReportDate", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.DateTime);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.DateTime);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }
    [WebMethod]
    public void Vehcile_IssueReport_GetList_TrailerTyre(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        //string sql = string.Format(@"select * from Vehicle_IssueReport where VehicleNo like @VehicleNo order by Id desc");
        string sql_where = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where = "and CreateBy=@CreateBy";
        }
        string sql = string.Format(@"select * from Vehicle_IssueReport 
where datediff(day,ReportDate,@from)<=0 and datediff(day,ReportDate,@to)>=0 and ActionType='Trailer Tyre' {0}
order by ReportDate", sql_where);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.DateTime);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.DateTime);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }
    [WebMethod]
    public void Vehcile_IssueReport_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select * from Vehicle_IssueReport where Id=@Id");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["JobNo"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string detail = Common.DataRowToJson(dt, true);



        sql = string.Format(@"select Id,FileType,FileName,FilePath,FileNote,'' as FP500 From CTM_Attachment where JobType='IssueRp' and TripId={0}", job["JobNo"]);
        dt = ConnectSql_mb.GetDataTable(sql);
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
        context = string.Format(@"{0}detail:{2},attachment:{3}{1}", "{", "}", detail, attachment);
        Common.WriteJsonP(status, context);

    }

    [WebMethod]
    public void Vehcile_IssueReport_Save(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");
        string sql = "";
        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["VehicleNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Description", job["Description"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ActionTaken", job["ActionTaken"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Note", job["Note"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ActionType", job["ActionType"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CreateBy", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ReportDate", job["ReportDate"], SqlDbType.DateTime);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Mileage", SafeValue.SafeDecimal(job["Mileage"]), SqlDbType.Decimal);
        list.Add(cpar);
        if (job["Id"].ToString().Equals("") || job["Id"].ToString().Equals("0"))
        {
            sql = string.Format(@"insert into Vehicle_IssueReport (VehicleNo,CreateDateTime,Description,ActionTaken,Note,ActionType,CreateBy,ReportDate,Mileage) values(@VehicleNo,getdate(),@Description,@ActionTaken,@Note,@ActionType,@CreateBy,@ReportDate,@Mileage)
select @@IDENTITY");
        }
        else
        {
            sql = string.Format(@"update Vehicle_IssueReport set VehicleNo=@VehicleNo,Description=@Description,ActionTaken=@ActionTaken,Note=@Note,ActionType=@ActionType,ReportDate=@ReportDate,Mileage=@Mileage where Id=@Id");
        }

        ConnectSql_mb.sqlResult re = null;
        if (job["Id"].ToString().Equals("") || job["Id"].ToString().Equals("0"))
        {
            re = ConnectSql_mb.ExecuteScalar(sql, list);
            if (re.status)
            {
                context = Common.StringToJson(re.context);

                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                lg.setActionLevel(SafeValue.SafeInt(re.context, 0), CtmJobEventLogRemark.Level.VehcileReport, 9);
                lg.log();
            }
        }
        else
        {
            re = ConnectSql_mb.ExecuteNonQuery(sql, list);
            if (re.status)
            {

                //===========log
                C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                lg.Platform_isMobile();
                lg.Controller = SafeValue.SafeString(job["user"]);
                lg.Lat = SafeValue.SafeString(job["Lat"]);
                lg.Lng = SafeValue.SafeString(job["Lng"]);
                lg.setActionLevel(SafeValue.SafeInt(job["Id"], 0), CtmJobEventLogRemark.Level.VehcileReport, 11);
                lg.log();
            }
        }
        if (!re.status || re.context.Equals("0"))
        {
            status = "0";
            context = Common.StringToJson("Save Error");
        }
        else
        {
            status = "1";
        }
        Common.WriteJsonP(status, context);

    }

    [WebMethod]
    public void Vehcile_Attachment_Add(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        string sql = string.Format(@"insert into CTM_Attachment (JobType,RefNo,ContainerNo,TripId,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values(@JobType,@RefNo,@ContainerNo,@TripId,@FileType,@FileName,@FilePath,@CreateBy,Getdate(),@FileNote)
select @@Identity");
        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
        if (fileStart == null)
        {
            fileStart = "";
        }
        string fileEnd = job["FilePath"].ToString();
        fileEnd = fileEnd.Substring(0, fileEnd.LastIndexOf('/')) + "/500/" + fileEnd.Substring(fileEnd.LastIndexOf('/') + 1);
        fileEnd = fileStart + fileEnd;

        cpar = new ConnectSql_mb.cmdParameters("@JobType", job["JobType"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@RefNo", job["JobNo"], SqlDbType.NVarChar, 100);
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
            lg.setActionLevel(SafeValue.SafeInt(res.context, 0), CtmJobEventLogRemark.Level.Attachment, 1, ":" + job["FileType"] + "[" + job["FileName"] + "]");
            lg.log();
        }
        else
        {
            context = Common.StringToJson("Error:" + res.context);
        }

        Common.WriteJsonP(status, context);
    }
    #endregion


    #region incentive
    //====================== incentive

    [WebMethod]
    public void Reporting_Incentive_GetList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");

        string sql_where = "";
        string sql_where2 = "";
        if (job["role"].ToString().Equals("Driver"))
        {
            sql_where = " and DriverCode=@DriverCode";
            sql_where2 = " and DriverCode2=@DriverCode";
        }
        //        string sql = string.Format(@"with tb_trip as (
        //select *,convert(nvarchar(10),ToDate,120) as ReportDate From CTM_JobDet2 
        //where datediff(d,FromDate,@from)<=0 and datediff(d,FromDate,@to)>=0 and Statuscode='C'{0}
        //)
        //select ReportDate,DriverCode,sum(isnull(Incentive1,0)+isnull(Incentive2,0)+isnull(Incentive3,0)+isnull(Incentive4,0)+isnull(Incentive5,0)+isnull(Incentive6,0)) as Total,
        //sum(isnull(Charge1,0)+isnull(Charge2,0)+isnull(Charge3,0)+isnull(Charge4,0)+isnull(Charge5,0)+isnull(Charge6,0)+isnull(Charge7,0)+isnull(Charge8,0)+isnull(charge9,0)+isnull(charge10,0)) as Claims 
        //from tb_trip group by ReportDate,DriverCode
        //order by ReportDate,DriverCode", sql_where);

        //        string sql = string.Format(@"with tb_trip as (
        //select *,convert(nvarchar(10),ToDate,120) as ReportDate,
        //(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=t.Id and LineType='DP') as incentive,
        //(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=t.Id and LineType='CL') as claims 
        //From CTM_JobDet2 as t
        //where datediff(d,ToDate,@from)<=0 and datediff(d,ToDate,@to)>=0 and Statuscode='C'{0}
        //)
        //select ReportDate,DriverCode,sum(incentive) as Total,sum(claims) as Claims 
        //from tb_trip group by ReportDate,DriverCode
        //order by ReportDate,DriverCode", sql_where);

        string sql = string.Format(@"select ReportDate,DriverCode,sum(Total) as Total,sum(Claims) as Claims from (
select ReportDate,DriverCode,sum(incentive) as Total,sum(claims) as Claims 
from (select *,convert(nvarchar(10),FromDate,120) as ReportDate,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='DP' and c.DriverCode=t.DriverCode) as incentive,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=t.Id and LineType='CL') as claims 
From CTM_JobDet2 as t
where datediff(d,FromDate,@from)<=0 and datediff(d,FromDate,@to)>=0 and Statuscode='C'{0} ) as temp 
group by ReportDate,DriverCode
union all
select ReportDate,DriverCode2 as DriverCode,sum(incentive) as Total,sum(claims) as Claims 
from (select *,convert(nvarchar(10),FromDate,120) as ReportDate,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost as c where TripNo=t.Id and LineType='DP' and c.DriverCode=t.DriverCode2) as incentive,
0 as claims 
From CTM_JobDet2 as t
where datediff(d,FromDate,@from)<=0 and datediff(d,FromDate,@to)>=0 and Statuscode='C'{1} ) as temp 
group by ReportDate,DriverCode2
) as temp
where isnull(DriverCode,'')<>''
group by ReportDate,DriverCode
order by ReportDate,DriverCode", sql_where, sql_where2);

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["user"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@from", job["from"], SqlDbType.NVarChar, 10);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@to", job["to"], SqlDbType.NVarChar, 10);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }

    [WebMethod]
    public void Reporting_Incentive_GetDetail(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");

        string sql = string.Format(@"select Id,ContainerNo,ChessisCode,convert(nvarchar(10),FromDate,120) as ReportDate
From CTM_JobDet2 
where datediff(d,FromDate,@ReportDate)=0 and (DriverCode=@DriverCode or DriverCode2=@DriverCode) and Statuscode='C'");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@DriverCode", job["DriverCode"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ReportDate", job["ReportDate"], SqlDbType.DateTime);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string mast = Common.DataTableToJson(dt);

        ConnectSql_mb.cmdParameters LineType = new ConnectSql_mb.cmdParameters("@LineType", "DP", SqlDbType.NVarChar, 10);
        list.Add(LineType);
//        sql = string.Format(@"select isnull(Qty*Price,0) as val,Id,TripNo,ChgCode,ChgCodeDes from job_cost 
//where TripNo in (select Id from CTM_JobDet2 
//where datediff(d,ToDate,@ReportDate)=0 and (DriverCode=@DriverCode or DriverCode2=@DriverCode) and Statuscode='C') and LineType=@LineType");
        sql = string.Format(@"select isnull(c.Qty*c.Price,0) as val,c.Id,TripNo,ChgCode,ChgCodeDes 
from CTM_JobDet2 as det2 
left outer join job_cost as c on c.TripNo=det2.Id and c.DriverCode=det2.DriverCode
where datediff(d,det2.FromDate,@ReportDate)=0 and det2.DriverCode=@DriverCode 
and det2.Statuscode='C' and c.Id>0 and c.LineType=@LineType
union all
select isnull(c.Qty*c.Price,0) as val,c.Id,TripNo,ChgCode,ChgCodeDes 
from CTM_JobDet2 as det2 
left outer join job_cost as c on c.TripNo=det2.Id and c.DriverCode=det2.DriverCode2
where datediff(d,det2.FromDate,@ReportDate)=0 and det2.DriverCode2=@DriverCode 
and det2.Statuscode='C' and c.Id>0 and c.LineType=@LineType");
        string incentive = Common.DataTableToJson(ConnectSql_mb.GetDataTable(sql, list));

        LineType.value = "CL";
        string claims = Common.DataTableToJson(ConnectSql_mb.GetDataTable(sql, list));

        context = string.Format(@"{0}mast:{2},incentive:{3},claims:{4}{1}", "{", "}", mast, incentive, claims);

        Common.WriteJsonP(status, context);

    }

    [WebMethod]
    public void Cost_AddNew(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        bool status = false;
        string code = job["code"].ToString();
        string des = job["name"].ToString();
        decimal value = SafeValue.SafeDecimal(job["value"], 0);
        int TripNo = SafeValue.SafeInt(job["TripNo"], 0);
        string LineType = job["LineType"].ToString();

        string sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,det1.ContainerNo,det1.ContainerType
from ctm_jobdet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id=@TripNo");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@TripNo", TripNo, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

        sql = string.Format(@"insert into job_cost (LineId,LineType,JobNo,JobType,ContNo,ContType,TripNo,VendorId,ChgCode,ChgCodeDes,Remark,Qty,Price,CurrencyId,ExRate,DocAmt,LocAmt,CompanyId)
values((select count(*) from job_cost where JobNo=@JobNo),@LineType,@JobNo,@JobType,@ContNo,@ContType,@TripNo,'',@ChgCode,@ChgCodeDes,'',1,@Price,'SGD',1,0,0,0)
select @@Identity");
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@LineType", LineType, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", dt.Rows[0]["JobType"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", dt.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContType", dt.Rows[0]["ContainerType"], SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", code, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChgCodeDes", des, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Price", value, SqlDbType.Decimal));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
        if (res.status )
        {
            status = true;
            //===========log
            C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            lg.setActionLevel(SafeValue.SafeInt(res.context, 0), CtmJobEventLogRemark.Level.Invoice, 5);
            lg.log();
        }
        Common.WriteJsonP(status, Common.StringToJson(""));
    }

    [WebMethod]
    public void Cost_GetData_ById(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int No = SafeValue.SafeInt(job["No"], 0);

        string sql = string.Format(@"select * from job_cost where Id=@No");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@No", No, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        Common.WriteJsonP(true, Common.DataRowToJson(dt));
    }


    [WebMethod]
    public void Cost_Delete_ById(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));
        int No = SafeValue.SafeInt(job["No"], 0);
        bool status = false;
        //===========log
        C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
        lg.Platform_isMobile();
        lg.Controller = SafeValue.SafeString(job["user"]);
        lg.Lat = SafeValue.SafeString(job["Lat"]);
        lg.Lng = SafeValue.SafeString(job["Lng"]);
        lg.setActionLevel(No, CtmJobEventLogRemark.Level.Invoice, 6);

        string sql = string.Format(@"delete from job_cost where Id=@No");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@No", No, SqlDbType.Int));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (res.status && res.context == "1")
        {
            status = true;
            lg.log();
        }
        Common.WriteJsonP(status, Common.StringToJson(""));
    }

    #endregion



    [WebMethod]
    public void EGL_Reporting_Attachment_Add(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "0";
        string context = Common.StringToJson("");

        string sql = string.Format(@"insert into Vehicle_Reporting_Attachment (JobType,RefNo,ContainerNo,TripId,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values(@JobType,@RefNo,@ContainerNo,@TripId,@FileType,@FileName,@FilePath,@CreateBy,Getdate(),@FileNote)
select @@Identity");
        string fileStart = System.Configuration.ConfigurationManager.AppSettings["MobileServerUrl"].ToString();
        if (fileStart == null)
        {
            fileStart = "";
        }
        string fileEnd = job["FilePath"].ToString();
        fileEnd = fileEnd.Substring(0, fileEnd.LastIndexOf('/')) + "/500/" + fileEnd.Substring(fileEnd.LastIndexOf('/') + 1);
        fileEnd = fileStart + fileEnd;

        cpar = new ConnectSql_mb.cmdParameters("@JobType", job["ContainerNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@RefNo", job["JobNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        //cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", job["ContainerNo"], SqlDbType.NVarChar, 100);
        //list.Add(cpar);
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
            lg.Platform_isMobile();
            lg.Controller = SafeValue.SafeString(job["user"]);
            lg.Lat = SafeValue.SafeString(job["Lat"]);
            lg.Lng = SafeValue.SafeString(job["Lng"]);
            lg.setActionLevel(SafeValue.SafeInt(result.context, 0), CtmJobEventLogRemark.Level.Attachment, 1, ":" + job["FileType"] + "[" + job["FileName"] + "]");
            lg.log();
        }

        Common.WriteJsonP(status, context);
    }
}
