using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PagesContTrucking_Job_NewJobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ContStatus"] != null && Request.QueryString["ContStatus"].ToString() != "")
            {
                cbb_StatusCode.Text = Request.QueryString["ContStatus"].ToString();
            }
            else
            {
                cbb_StatusCode.Text = "All";
            }
            txt_new_WareHouseId.Text = System.Configuration.ConfigurationManager.AppSettings["Warehouse"].Trim();
            txt_search_dateFrom.Date = DateTime.Now;//.AddDays(-15);
            txt_search_dateTo.Date = DateTime.Now;
            //btn_search_Click(null, null);


            EzshipHelper_Authority.Bind_Authority(this.Page);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,job.YardRef as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.ContainerType,job.EtaDate,job.EtdDate,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,
(select top 1 Code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,
(select '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')) as trips,
(select count(*) from XAArInvoice where MastRefNo=job.JobNo) as billed
from CTM_Job as job  
left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo 
where job.JobNo like '%{0}%' and isnull(det1.ContainerNo,'') like '%{1}%' ", txt_search_jobNo.Text, txt_search_ContNo.Text);//
        if (txt_search_dateFrom.Date > new DateTime(1900, 1, 1))
        {
            sql += " and datediff(d,'" + txt_search_dateFrom.Date + "',job.jobDate)>=0";
        }
        if (txt_Vessel.Text != "")
        {
            sql += " and job.Vessel='" + txt_Vessel.Text.Replace("'","") + "'";
        }
        if (txt_search_dateTo.Date > new DateTime(1900, 1, 1))
        {
            sql += " and datediff(d,'" + txt_search_dateTo.Date + "',job.jobDate)<=0";
        }
        if (cbb_StatusCode.Text != "All")
        {
            sql += " and det1.StatusCode='" + cbb_StatusCode.Text + "'";
        }
        if (btn_ClientId.Text.Length > 0)
        {
            sql += " and job.clientId='" + btn_ClientId.Text.Replace("'", "") + "'";
        }
        if (search_JobType.Text != "All")
        {
            sql += " and det1.StatusCode='" + search_JobType.Text.Replace("'", "") + "'";
        }
        else {
            sql += " and job.JobType in ('IMP','EXP','LOC','WGR','WDO','TPT','CRA','FRT')";
        }
        if (search_contNo.Text.Length > 0)
        {
            sql += " and det1.ContainerNo like '" + search_contNo.Text.Replace("'", "") + "%'";
        }
        sql += " order by job.EtaDate,job.JobNo,det1.Id,det1.StatusCode desc, job.JobDate asc";
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_Transport.DataSource = dt;
        if (sender != null)
        {
            this.grid_Transport.DataBind();
        }
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
        string isTrucking =SafeValue.SafeString(cmb_IsTrucking.Value);
        string isWarehouse = SafeValue.SafeString(cmb_IsWarehouse.Value);
        string subContract = SafeValue.SafeString(cbb_Contractor.Value);
        subContract = (subContract == "YES" ? "YES" : "NO");
        string warehouseCode = SafeValue.SafeString(txt_new_WareHouseId.Text);
        string jobno = "";
        string user = HttpContext.Current.User.Identity.Name;
        string quoteNo = "";
        string quoteStatus = "Pending";
        if (jobStatus == "Quoted")
        {
             
            quoteNo = C2Setup.GetNextNo("", "CTM_Job_"+jobStatus, date);
        }
        else
        {
            quoteStatus = "None";
            jobno = C2Setup.GetNextNo("", "CTM_Job_" + jobType1, date);
            quoteNo = jobno;
        }
        DateTime now = DateTime.Now;
        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,IsTrucking,IsWarehouse,Contractor,DepotCode) values (@JobNo,@JobDate,@EtaDate,@EtdDate,@CodDate,@StatusCode,@CreateBy,@CreateDatetime,@UpdateBy,@UpdateDatetime,@EtaTime,@EtdTime,@JobType,@ClientId,@YardRef,@PickupFrom,@DeliveryTo,@Remark,@WarehouseAddress,@JobStatus,@QuoteNo,@QuoteStatus,@QuoteDate,@WareHouseCode,@IsTrucking,@IsWarehouse,@Contractor,@DepotCode) select @@identity");
        if (jobType1 == "WGR" || jobType1 == "WDO" || jobType1 == "TPT")
        {
            sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,SpecialInstruction,WarehouseAddress,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,IsTrucking,IsWarehouse,Contractor,DepotCode) values (@JobNo,@JobDate,@EtaDate,@EtdDate,@CodDate,@StatusCode,@CreateBy,@CreateDatetime,@UpdateBy,@UpdateDatetime,@EtaTime,@EtdTime,@JobType,@ClientId,@YardRef,@PickupFrom,@DeliveryTo,@SpecialInstruction,@WarehouseAddress,@JobStatus,@QuoteNo,@QuoteStatus,@QuoteDate,@WareHouseCode,@IsTrucking,@IsWarehouse,@Contractor,@DepotCode) select @@identity");
        }
         List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobno, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@JobDate", txt_new_JobDate.Date, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@EtaDate", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@EtdDate", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@CodDate", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", "USE", SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@CreateBy", user, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@CreateDatetime", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@UpdateBy", user, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@UpdateDatetime", now, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@EtaTime", time4, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@EtdTime", time4, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", cbb_new_jobtype.Value, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", btn_new_ClientId.Text, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@YardRef", txt_DepotAddress.Text, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@PickupFrom", txt_FromAddress.Text, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@DeliveryTo", txt_ToAddress.Text, SqlDbType.NVarChar));
        if (jobType1 == "WGR" || jobType1 == "WDO" || jobType1 == "TPT")
        {
            list.Add(new ConnectSql_mb.cmdParameters("@SpecialInstruction", txt_new_remark.Text, SqlDbType.NVarChar));
        }
        else
        {
            list.Add(new ConnectSql_mb.cmdParameters("@Remark", txt_new_remark.Text, SqlDbType.NVarChar));
        }
        list.Add(new ConnectSql_mb.cmdParameters("@WarehouseAddress", txt_WarehouseAddress.Text, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@JobStatus", jobStatus, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteNo", quoteNo, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteStatus", quoteStatus, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteDate", now, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@WareHouseCode", warehouseCode, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@IsTrucking", isTrucking, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@IsWarehouse", isWarehouse, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Contractor", cbb_Contractor.Value, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@DepotCode", btn_DepotCode.Text, SqlDbType.NVarChar));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
        if (res.status)
        {
            int jobId = SafeValue.SafeInt(res.context,0);
            string userId = HttpContext.Current.User.Identity.Name;
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;
            if (jobStatus == "Quoted")
            {
                elog.setActionLevel(jobId, CtmJobEventLogRemark.Level.Quotation, 1);
                elog.ActionLevel_isQuoted(jobId);
                C2Setup.SetNextNo("", "CTM_Job_"+jobStatus, quoteNo, date);
                auto_add_trailer_trip(quoteNo, jobType1);
                //GetJobRate(quoteNo, btn_new_ClientId.Text, SafeValue.SafeString(cbb_new_jobtype.Value));
                e.Result = quoteNo;
            }
            else
            {
                elog.setActionLevel(jobId, CtmJobEventLogRemark.Level.Job, 1);
                elog.ActionLevel_isJOB(jobId);
                C2Setup.SetNextNo("", "CTM_Job_" + jobType1, jobno, date);
                auto_add_trailer_trip(jobno, jobType1);
                e.Result = jobno;
            }
            elog.log();
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
    private void auto_add_trailer_trip(string jobNo,string jobType) {
        if (jobType == "WGR" || jobType == "WDO" || jobType == "TPT")
        {

            C2.CtmJobDet1 det1 = new C2.CtmJobDet1();
            det1.JobNo = jobNo;
            det1.ScheduleDate = DateTime.Today;
            det1.StatusCode = "New";
            det1.CfsStatus = "Pending";
            det1.ScheduleStartDate = DateTime.Today;
            det1.ContainerNo = "";

            C2.Manager.ORManager.StartTracking(det1, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det1);

            string tripType = "";
            if (jobType == "WGR")
                tripType = "LOC";
            if (jobType == "WDO")
                tripType = "SHF";

            //if (jobType == "WGR" || jobType == "WDO" || jobType == "TPT")
            //{
                C2.CtmJobDet2 det2 = new C2.CtmJobDet2();
                det2.Det1Id = det1.Id;
                det2.TripCode = tripType;
                det2.ContainerNo = det1.ContainerNo;
                det2.JobNo = jobNo;
                C2.Manager.ORManager.StartTracking(det2, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(det2);
           // }
        }
    }
}