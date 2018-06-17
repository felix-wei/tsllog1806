using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class PagesContTrucking_Job_QuotationList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_new_WareHouseId.Text = System.Configuration.ConfigurationManager.AppSettings["Warehouse"].Trim();
            txt_search_dateFrom.Date = DateTime.Now;//.AddDays(-15);
            txt_search_dateTo.Date = DateTime.Now.AddDays(8);
            //btn_search_Click(null, null);


            EzshipHelper_Authority.Bind_Authority(this.Page);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select job.JobNo,job.StatusCode,job.JobStatus,job.JobDate,job.YardRef as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,job.EtaDate,job.EtdDate,job.QuoteNo,job.QuoteStatus,job.QuoteDate,job.IsTrucking,IsWarehouse,IsLocal,IsAdhoc,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,
(select top 1 Name from XXParty where PartyId=job.ClientId) as client,(select top 1 Name from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,
(select count(*) from XAArInvoice where MastRefNo=job.JobNo) as billed
from CTM_Job as job ");//
        string where = "";
        if (txt_search_jobNo.Text != "")
        {
            where=GetWhere(where, " job.JobNo like '%" + txt_search_jobNo.Text + "%'");
        }
        else if (txt_search_QuoNo.Text != "") {
            where=GetWhere(where, " job.QuoteNo like '%" + txt_search_QuoNo.Text + "%'");
        }
        else
        {
            if (txt_search_dateFrom.Date > new DateTime(1900, 1, 1))
            {
                where=GetWhere(where, " datediff(d,'" + txt_search_dateFrom.Date + "',job.QuoteDate)>=0");
            }

            if (txt_search_dateTo.Date > new DateTime(1900, 1, 1))
            {
                where=GetWhere(where, " datediff(d,'" + txt_search_dateTo.Date + "',job.QuoteDate)<=0");
            }
            if (cbb_QuoteStatus.Text != "All")
            {
                where=GetWhere(where," job.QuoteStatus='" + cbb_QuoteStatus.Text + "'");
            }
            else{
               where = GetWhere(where, "job.QuoteStatus in ('Pending','Quoted','Closed','Failed','Voided')  ");
            }
            if (btn_ClientId.Text.Length > 0)
            {
                where=GetWhere(where," job.clientId='" + btn_ClientId.Text.Replace("'", "") + "'");
            }
        }
        if(where.Length>0)
            sql += " where " + where + " and job.QuoteStatus<>'None' and QuoteNo<>'' order by job.QuoteNo desc,job.QuoteDate asc";
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_Transport.DataSource = dt;
        if (sender != null)
        {
            this.grid_Transport.DataBind();
        }
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
        string jobStatus = SafeValue.SafeString(cbb_new_jobstatus.Text, "Quoted");
        //string jobType1 = "IMP";
        //if (jobType.IndexOf("EXP") > -1)
        //{
        //    jobType1 = "EXP";
        //}
        string jobType1 = jobType;
        string isTrucking = "No";
        if (cb_Trucking.Checked)
        {
            isTrucking = "Yes";
        }
        string isWarehouse = "No";
        if (cb_Warehouse.Checked)
        {
            isWarehouse = "Yes";
        }
        string isTransport = "No";
        if (cb_Transport.Checked)
        {
            isTransport = "Yes";
        }
        string isCrane = "No";
        if (cb_Crane.Checked)
        {
            isCrane = "Yes";
        }

        if (jobType == "IMP" || jobType == "EXP" || jobType == "LOC")
        {
            isTrucking = "Yes";
        }
        else if (jobType == "WGR" || jobType == "WDO" || jobType == "TPT")
        {
            isTransport = "Yes";
        }
        else if (jobType == "CRA")
        {
            isCrane = "Yes";
        }
        string warehouseCode = SafeValue.SafeString(txt_new_WareHouseId.Text);
        string jobno = "";
        string user = HttpContext.Current.User.Identity.Name;
        string quoteNo = "";
        string quoteStatus = "Pending";
        if (jobStatus == "Quoted")
        {
            quoteNo = C2Setup.GetNextNo("", "CTM_Job_" + jobStatus, txt_new_JobDate.Date);
        }
        else
        {
            quoteStatus = "None";
            jobno = C2Setup.GetNextNo("", "CTM_Job_" + jobType1, txt_new_JobDate.Date);
            quoteNo = jobno;
        }
        DateTime now = DateTime.Now;
        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,IsTrucking,IsWarehouse,IsLocal,IsAdhoc) 
values (@JobNo,@JobDate,@EtaDate,@CodDate,@StatusCode,@CreateBy,@CreateDatetime,@UpdateBy,@UpdateDatetime,@EtaTime,@EtdTime,@JobType,@JobStatus,@QuoteNo,@QuoteStatus,@QuoteDate,@WareHouseCode,@IsTrucking,@IsWarehouse,@IsLocal,@IsAdhoc) select @@identity");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobno, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@JobDate", txt_new_JobDate.Date, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@EtaDate", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@CodDate", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", "USE", SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@CreateBy", user, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@CreateDatetime", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@UpdateBy", user, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@UpdateDatetime", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@EtaTime", time4, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@EtdTime", time4, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", cbb_new_jobtype.Value, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@JobStatus", jobStatus, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteNo", quoteNo, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteStatus", quoteStatus, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteDate", now, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@WareHouseCode", warehouseCode, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@IsTrucking", isTrucking, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@IsWarehouse", isWarehouse, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@IsLocal", isTransport, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@IsAdhoc", isCrane, SqlDbType.NVarChar));

        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
        if (res.status)
        {
            int jobId = SafeValue.SafeInt(res.context, 0);
            string userId = HttpContext.Current.User.Identity.Name;
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;
            if (jobStatus == "Quoted")
            {

                elog.setActionLevel(jobId, CtmJobEventLogRemark.Level.Quotation, 1);
                elog.ActionLevel_isQuoted(jobId);
                C2Setup.SetNextNo("", "CTM_Job_" + jobStatus, quoteNo, date);
                //GetJobRate(quoteNo, btn_new_ClientId.Text, SafeValue.SafeString(cbb_new_jobtype.Value));
                e.Result = quoteNo;
            }
            else
            {
                elog.setActionLevel(jobId, CtmJobEventLogRemark.Level.Job, 1);
                elog.ActionLevel_isJOB(jobId);
                C2Setup.SetNextNo("", "CTM_Job_" + jobType1, jobno, date);
                e.Result = jobno;
            }
            elog.log();

            sql = string.Format(@"update CTM_Job set JobNo={0} where Id={0}",jobId);
            ConnectSql.ExecuteSql(sql);
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
    protected void grid_Transport_BeforePerformDataSelect(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("Quotation_" + DateTime.Now.ToString("yyyyMMdd_HHmmsss"), true);
    }
}