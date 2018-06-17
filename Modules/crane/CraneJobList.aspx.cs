using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Job_CraneJobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            txt_search_dateFrom.Date = DateTime.Now.AddDays(-7);
            txt_search_dateTo.Date = DateTime.Now;
            btn_search_Click(null, null);


            EzshipHelper_Authority.Bind_Authority(this.Page);
        }
    }

    public string xmlChangeToHtml(object par, object contId)
    {
        string res = par.ToString();
        res = res.Replace("&lt;", "<");
        res = res.Replace("&gt;", ">");
        //if (res.Length < 2 && SafeValue.SafeInt(contId, 0) > 0)
        //{
        //    res = "<span class='X'>Trips</span>";
        //}
        return res;
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string From = txt_search_dateFrom.Date.ToString("yyyyMMdd");
        string To = txt_search_dateTo.Date.ToString("yyyyMMdd");
        string JobNo = txt_search_jobNo.Text;
        string Client = btn_ClientId.Text;
        //string ClientName = info["ClientName"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        //list.Add(new ConnectSql_mb.cmdParameters("@ContNo", "%" + ContNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo + "%", SqlDbType.NVarChar, 100));
        //list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        //list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        //list.Add(new ConnectSql_mb.cmdParameters("@NextTrip", NextTrip, SqlDbType.NVarChar, 10));




        string sql = string.Format(@"select *,
(select '<span class='''+det2.Statuscode+'''>'+convert(nvarchar(2),det2.BookingDate,103)+substring(datename(m,det2.BookingDate),1,3)+'</span>' from CTM_JobDet2 as det2 where det2.JobNo=job.JobNo for xml path('')) as trips,
'CRA' as NextTrip,job.Escort_Ind,job.Escort_Remark,
isnull((select ','+det2.Statuscode+':'+TripCode from CTM_JobDet2 as det2 where det2.JobNo=job.JobNo for xml path('')),'')+',' as str_trips
from CTM_Job as job");
        string sql_where = "";
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
        }
        if (sql_where.Equals(""))
        {
            sql_where = " datediff(d,@DateFrom,job.JobDate)>=0 and datediff(d,@DateTo,job.JobDate)<=0";
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }
        }
        sql += " where job.JobType='CRA' and job.JobStatus<>'Voided' and " + sql_where;
        sql += " order by job.JobDate desc";

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
       // throw new Exception(sql.ToString());
        this.grid_Transport.DataSource = dt;
        this.grid_Transport.DataBind();


    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        e.Result = "Fail!";
        Job_New_Save(e);
    }


    private void Job_New_Save(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        DateTime date = DateTime.Now;
        string time4 = date.ToString("HHmm");
        string jobType = SafeValue.SafeString(cbb_new_jobtype.Value, "CRA");
        string jobType1 = jobType;
        string jobno = "";
        string jobStatus = SafeValue.SafeString(cbb_new_jobstatus.Text, "Quoted");
        string user = HttpContext.Current.User.Identity.Name;
        string quoteNo = "";
        if (jobStatus == "Quoted")
        {
            quoteNo = C2Setup.GetNextNo("", "CTM_Job_" + jobStatus, date);
        }
        else
        {
            jobno = C2Setup.GetNextNo("", "CTM_Job_" + jobType1, date);
            quoteNo = jobno;
        }
        string wh = System.Configuration.ConfigurationManager.AppSettings["Warehosue"];
        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,IsAdhoc) values ('{0}','{4}',getdate(),getdate(),getdate(),'USE','{1}',getdate(),'{1}',getdate(),'{2}','{2}','{3}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','Pending',getdate(),'{13}','Yes')", jobno, user, time4, cbb_new_jobtype.Value, txt_new_JobDate.Date, btn_new_ClientId.Text, "", "", txt_ToAddress.Text, txt_new_remark.Text, "", jobStatus, quoteNo, wh);
        int i = ConnectSql_mb.ExecuteNonQuery(sql);
        if (i == 1)
        {
            if (jobStatus == "Quoted")
            {
                C2Setup.SetNextNo("", "CTM_Job_" + jobStatus, quoteNo, date);
                //GetJobRate(quoteNo, btn_new_ClientId.Text, SafeValue.SafeString(cbb_new_jobtype.Value));
                e.Result = quoteNo;
            }
            else
            {
                C2Setup.SetNextNo("", "CTM_Job_" + jobType1, jobno, date);
                e.Result = jobno;
                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isWeb();
                elog.Controller = HttpContext.Current.User.Identity.Name;
                elog.fixActionInfo_ByJobNo(jobno);
                elog.Remark = "New Job";
                elog.log();
            }
        }
    }
    protected void grid_Transport_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        //if (e.DataColumn.Caption == "Job Status")
        //{
        //    string status = SafeValue.SafeString(this.grid_Transport.GetRowValues(e.VisibleIndex, "JobStatus"));
        //    if (status == "USE")
        //    {
        //        e.Cell.BackColor = System.Drawing.Color.LightGreen;
        //    }
        //    if (status == "CLS")
        //    {
        //        e.Cell.BackColor = System.Drawing.Color.Orange;
        //    }
        //    if (status == "CNL")
        //    {
        //        e.Cell.BackColor = System.Drawing.Color.Red;
        //    }
        //}
    }
    public string VilaStatus(string status)
    {
        string strStatus = "";
        if (status == "USE")
        {
            strStatus = "NEW";
        }
        if (status == "CLS")
        {
            strStatus = "COMPLATED";
        }
        if (status == "CNL")
        {
            strStatus = "CANCEL";
        }
        return strStatus;
    }
    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Scheduled")
        {
            color = "orange";
        }
        if (status == "InTransit")
        {
            color = "green";
        }
        if (status == "Completed")
        {
            color = "blue";
        }
        if (status == "Canceled")
        {
            color = "gray";
        }
        return color;
    }
}