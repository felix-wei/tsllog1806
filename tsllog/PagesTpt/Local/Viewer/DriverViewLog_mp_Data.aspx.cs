using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesTpt_Local_Viewer_DriverViewLog_mp_Data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = SafeValue.SafeString(Request.Form["Type"]);
        if (type != null && type != "")
        {
            switch (type)
            {
                case "GetLog":
                    getLog();
                    break;
                case "UploadLog":
                    uploadLog();
                    break;
                default:
                    break;
            }
        }
    }

    public void uploadLog()
    {
        string jobNo = SafeValue.SafeString(Request.Form["JobNo"]);
        string driver = SafeValue.SafeString(Request.Form["Driver"]);
        string status = SafeValue.SafeString(Request.Form["Status"]);
        string logDate = SafeValue.SafeString(Request.Form["LogDate"]);
        string logTime = SafeValue.SafeString(Request.Form["LogTime"]);
        string remark = SafeValue.SafeString(Request.Form["Remark"]);
        int flag = InsertTripLog(jobNo, "Local", driver, status, logDate, logTime, remark);
        if (flag > 0)
        {
            UpdateTptStatus(jobNo, status, logTime, logDate);
            Response.Write("Success");
        }
        else
        {
            Response.Write("Error");
        }
    }
    private int InsertTripLog(string jobNo, string jobType, string driver, string status, string logDate, string logTime, string rmk)
    {
        string sql = string.Format(@"INSERT INTO [CTM_TripLog]([JobNo],[JobType],[Driver],[Status],[LogDate],[LogTime],[Remark],[CreateDateTime],[CreateBy],[UpdateDateTime],[UpdateBy])
     VALUES('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}'  ,'{8}' ,'{7}' ,'{8}' ,'{7}')", jobNo, jobType, driver, status, logDate, logTime, rmk, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        return ConnectSql.ExecuteSql(sql);
    }
    private void UpdateTptStatus(string jobNo, string jobStatus, string time, string date)
    {
        string sql = string.Format("Update tpt_job set JobProgress='{1}',tptTime='{2}',tptDate='{3}' where JobNo='{0}'", jobNo, jobStatus, time, date);
        ConnectSql.ExecuteSql(sql);
    }

    public void getLog()
    {
        string jobno = SafeValue.SafeString(Request.Form["JobNo"]);
        string driver = SafeValue.SafeString(Request.Form["Driver"]);
        string sql = "select JobNo,Driver,Status,convert(nvarchar,LogDate,103) as LogDate,LogTime,Remark from ctm_triplog";
        sql += string.Format(" Where JobNo='{0}' and Driver='{1}' and JobType='Local'", jobno, driver);
        sql += " order by LogDate,LogTime,UpdateDateTime";
        DataTable dt = ConnectSql.GetTab(sql);
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr=dt.Rows[i];
            if (i == 0)
            {
                result = dr["Status"].ToString();
            }
            else
            {
                result += "@;@" + dr["Status"].ToString();
            }
            result += "@,@" + dr["LogDate"] + " " + dr["LogTime"];
            result += "@,@" + dr["Remark"];
        }
        Response.Write(result);
    }
}