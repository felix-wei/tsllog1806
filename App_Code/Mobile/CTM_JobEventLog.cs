using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// CTM_JobEventLog 的摘要说明
/// </summary>
public class CTM_JobEventLog
{
    private CTM_JobEventLog()
    {

    }
    #region columns
    public int Id { get; set; }
    DateTime CreateDateTime { get; set; }
    public string Controller { get; set; }
    public string JobNo { get; set; }
    public string JobType { get; set; }
    public string ParentJobNo { get; set; }
    public string ParentJobType { get; set; }
    public string ContainerNo { get; set; }
    public string Trip { get; set; }
    public string Driver { get; set; }
    public string Towhead { get; set; }
    public string Trail { get; set; }
    public string Remark { get; set; }
    public string Note1 { get; set; }
    public string Note1Type { get; set; }
    public string Note2 { get; set; }
    public string Note3 { get; set; }
    public string Note4 { get; set; }

    public string Lat { get; set; }
    public string Lng { get; set; }
    public string Platform { get; set; }
    #endregion
    private void log()
    {
        CTM_JobEventLog l = this;

        string sql = string.Format(@"insert into CTM_JobEventLog (CreateDateTime,Controller,JobNo,ContainerNo,Trip,Driver,Towhead,Trail,Remark,Note1,Note2,Note3,Note4,Lat,Lng,Platform,JobType,ParentJobNo,ParentJobType,Note1Type) values(getdate(),@Controller,@JobNo,@ContainerNo,@Trip,@Driver,@Towhead,@Trail,@Remark,@Note1,@Note2,@Note3,@Note4,@Lat,@Lng,@Platform,@JobType,@ParentJobNo,@ParentJobType,@Note1Type)");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@Controller", l.Controller, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@JobNo", l.JobNo, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", l.ContainerNo, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Trip", l.Trip, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Driver", l.Driver, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Towhead", l.Towhead, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Trail", l.Trail, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Remark", l.Remark, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Note1", l.Note1, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Note2", l.Note2, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Note3", l.Note3, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Note4", l.Note4, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Lat", l.Lat, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Lng", l.Lng, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Platform", l.Platform, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@JobType", l.JobType, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ParentJobNo", l.ParentJobNo, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ParentJobType", l.ParentJobType, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Note1Type", l.Note1Type, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        ConnectSql_mb.ExecuteNonQuery(sql, list);
    }
}