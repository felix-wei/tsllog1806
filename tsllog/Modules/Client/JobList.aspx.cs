using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Client_JobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["type"] != null)
            {
                search_JobType.Value = SafeValue.SafeString(Request.QueryString["type"]);
                cbb_new_jobtype.Value = SafeValue.SafeString(Request.QueryString["type"]);
                lbl_type.Text = SafeValue.SafeString(Request.QueryString["type"]);
            }
            string userId = HttpContext.Current.User.Identity.Name;
            string sql_user = string.Format(@"select CustId from [dbo].[User] where Name='{0}'", userId);
            string custId = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql_user));
            btn_ClientId.Text = custId;
            btn_new_ClientId.Text = custId;
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
        if (res.Length < 2 && SafeValue.SafeInt(contId, 0) > 0)
        {
            res = "<span class='X'>Trips</span>";
        }
        return res;
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string From = txt_search_dateFrom.Date.ToString("yyyyMMdd");
        string To = txt_search_dateTo.Date.ToString("yyyyMMdd");
        //string ContNo = txt_search_ContNo.Text;
        string JobNo = txt_search_jobNo.Text;
        string JobType = search_JobType.Text;
        string Vessel = txt_Vessel.Text;

        string Client = btn_ClientId.Text;
        //string ClientName = info["ClientName"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));

        string sql = string.Format(@"select job.Id,det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,det1.YardAddress as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.SealNo,det1.ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,
det1.UrgentInd,job.OperatorCode,job.CarrierBkgNo,det1.oogInd,det1.CfsStatus,det1.DischargeCell,IsTrucking,IsWarehouse,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,
(select top 1 Code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,
(select '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')) as trips,
datediff(hour,isnull((select top 1 convert(nvarchar(8),det2.FromDate,112)+' '+det2.FromTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and (det2.Statuscode='S' or det2.Statuscode='C') order by det2.FromDate,det2.FromTime),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)),
case when det1.StatusCode='Completed' then isnull((select top 1 convert(nvarchar(8),det2.ToDate,112)+' '+det2.ToTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and det2.Statuscode='C' order by det2.ToDate desc,det2.ToTime desc),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)) else getdate() end)  as hr,
case job.JobType when 'IMP' then (case det1.StatusCode when 'New' then 'IMP' when 'InTransit' then 'RET' else '' end) 
when 'EXP' then (case det1.StatusCode when 'New' then 'COL' when 'InTransit' then 'EXP' else '' end) else '' end as NextTrip,
isnull((select ','+det2.Statuscode+':'+TripCode from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')),'')+',' as str_trips
from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo");
        string sql_where = "";
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
        }
        if (Client.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.clientId=@Client");
        }
        if (sql_where.Equals(""))
        {
            if (Vessel.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.Vessel=@Vessel");
            }
            if (JobType.Length > 0 && !JobType.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "job.JobType=@JobType");
            }


            if (sql_where.Length > 0)
            {
                sql_where = "and " + sql_where;
            }
            sql += " where job.JobType in ('IMP','EXP','LOC','WGR','WDO','TPT','FRT')  and JobStatus='Booked' " + sql_where;

        }
        else
        {
            sql += " where " + sql_where;
            sql += " order by job.EtaDate,job.JobNo desc, job.JobDate asc";
        }
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        //throw new Exception(sql.ToString());
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

        string jobType = SafeValue.SafeString(cbb_new_jobtype.Value, "IMP");
        string jobStatus = "Booked";

        string jobType1 = jobType;

        string jobno = "";
        string user = HttpContext.Current.User.Identity.Name;
        string quoteNo = "";
        string billingType = "None";
        if (jobType1 == "TPT")
        {
            billingType = "Job";
        }
        else
        {
            jobno = C2Setup.GetNextNo("", "CTM_Job_" + jobType1, date);
            quoteNo = jobno;
        }
        string wh = System.Configuration.ConfigurationManager.AppSettings["Warehosue"];
        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,BillingType,IsTrucking,IsWarehouse) values ('{0}','{4}',getdate(),getdate(),getdate(),'USE','{1}',getdate(),'{1}',getdate(),'{2}','{2}','{3}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','Pending',getdate(),'{13}','{14}','Yes','No') select @@identity", jobno, user, time4, cbb_new_jobtype.Value, txt_new_JobDate.Date, btn_ClientId.Text, "", txt_FromAddress.Text, txt_ToAddress.Text, txt_new_remark.Text, "", jobStatus, quoteNo, wh, billingType);
        int jobId = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
        string remark = "";
        if (jobId>0)
        {
            string userId = HttpContext.Current.User.Identity.Name;
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;
            remark = CtmJobEventLogRemark.getDes("106");
            elog.ActionLevel_isJOB(jobId);
            C2Setup.SetNextNo("", "CTM_Job_" + jobType1, jobno, date);        
            elog.Remark = remark;
            elog.log();

            e.Result = jobno;

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