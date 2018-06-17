using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Freight_Job_JobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_new_WareHouseId.Text = System.Configuration.ConfigurationManager.AppSettings["Warehouse"].Trim();
           
            txt_from.Date = DateTime.Now.AddDays(-7);
            txt_end.Date = DateTime.Now;
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
        string dateFrom = txt_from.Date.ToString("yyyyMMdd");
        string dateTo = txt_end.Date.ToString("yyyyMMdd");
        string ContNo = txt_ContNo.Text;
        string clientRef = txt_RefNo.Text;
        string JobNo = txt_JobNo.Text;
        string hbl = txt_Hbl.Text;
        string obl = txt_Obl.Text;
        //string ClientName = info["ClientName"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", dateFrom, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", dateTo, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", ContNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientRefNo", clientRef, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@CarrierBlNo", hbl, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@CarrierBkgNo", obl, SqlDbType.NVarChar, 10));



        string sql = string.Format(@"select job.Id,det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,det1.YardAddress as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.SealNo,det1.ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,
det1.UrgentInd,job.OperatorCode,job.CarrierBlNo,job.CarrierBkgNo,det1.oogInd,det1.CfsStatus,det1.DischargeCell,IsTrucking,IsWarehouse,
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
        if (ContNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "det1.ContainerNo=@ContNo");
        }
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo=@JobNo");
        }
        if (sql_where.Equals(""))
        {
            sql_where = " datediff(d,@DateFrom,job.EtaDate)>=0 and datediff(d,@DateTo,job.EtaDate)<=0";

            if (hbl.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.CarrierBlNo=@CarrierBlNo");
            }
            if (obl.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.CarrierBkgNo=@CarrierBkgNo");
            }
            if (clientRef.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.ClientRefNo=@ClientRefNo");
            }
            sql += " where job.JobType in ('I')  and " + sql_where;
        }
        else
        {
            sql += " where " + sql_where;
            sql += " order by job.EtaDate,job.JobNo,det1.Id,det1.StatusCode desc, job.JobDate asc";
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
        string jobType = SafeValue.SafeString(cbb_new_jobtype.Value, "CONSOL");
        string jobStatus = SafeValue.SafeString(cbb_new_jobstatus.Text, "Quoted");
        //string jobType1 = "IMP";
        //if (jobType.IndexOf("EXP") > -1)
        //{
        //    jobType1 = "EXP";
        //}
        string jobType1 = jobType;

        string jobno = "";
        string user = HttpContext.Current.User.Identity.Name;
        string quoteNo = "";

        jobno = C2Setup.GetNextNo("I", "CI", date);
        
        string wh = txt_new_WareHouseId.Text;// System.Configuration.ConfigurationManager.AppSettings["Warehosue"];
        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,OrderType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,IsTrucking,IsWarehouse,JobType) values ('{0}','{4}',getdate(),getdate(),getdate(),'USE','{1}',getdate(),'{1}',getdate(),'{2}','{2}','{3}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','Pending',getdate(),'{13}','Yes','No','I') select @@identity", jobno, user, time4, cbb_new_jobtype.Value, txt_new_JobDate.Date, btn_new_ClientId.Text, txt_DepotAddress.Text, txt_FromAddress.Text, txt_ToAddress.Text, txt_new_remark.Text, txt_WarehouseAddress.Text, jobStatus, quoteNo, wh);
        int jobId = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
        if (jobId >0)
        {
            string userId = HttpContext.Current.User.Identity.Name;
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;

            elog.setActionLevel(jobId, CtmJobEventLogRemark.Level.Job, 1);
            elog.ActionLevel_isJOB(jobId);
            C2Setup.SetNextNo("I", "CI", jobno, date);
            e.Result = jobno;
            elog.log();
        }
    }
    private void GetJobRate(string quoteNo, string clientId, string jobType)
    {
        string sql = string.Format(@"select * from job_rate where ClientId='{0}'", clientId);
        DataTable dt = ConnectSql.GetTab(sql);
        string sql_part1 = string.Format(@"insert into job_rate(LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price) values");
        sql = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int id = SafeValue.SafeInt(dt.Rows[i]["Id"], 0);
            string LineType = SafeValue.SafeString(dt.Rows[i]["LineType"]);
            string LineStatus = SafeValue.SafeString(dt.Rows[i]["LineStatus"]);
            string ClientId = SafeValue.SafeString(dt.Rows[i]["ClientId"]);
            string BillScope = SafeValue.SafeString(dt.Rows[i]["BillScope"]);
            string BillType = SafeValue.SafeString(dt.Rows[i]["BillType"]);
            string BillClass = SafeValue.SafeString(dt.Rows[i]["BillClass"]);
            string ContSize = SafeValue.SafeString(dt.Rows[i]["ContSize"]);
            string ContType = SafeValue.SafeString(dt.Rows[i]["ContType"]);
            string ChgCode = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
            string ChgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
            string Remark = SafeValue.SafeString(dt.Rows[i]["Remark"]);
            decimal Price = SafeValue.SafeDecimal(dt.Rows[i]["Price"]);
            string sql_part2 = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13})", LineType, LineStatus, quoteNo, jobType, clientId, BillScope, BillType, BillClass, ContSize, ContType, ChgCode, ChgCodeDes, Remark, Price);
            sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
        }
        if (sql.Length > 0)
        {
            sql = sql_part1 + sql;
            int re = ConnectSql.ExecuteSql(sql);


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
        if (status == "Import" || status == "Collection"
          || status == "Customer-MT" || status == "Warehouse"
          || status == "Customer-LD"
          || status == "WHS-LD" || status == "Return"
          || status == "WHS-MT" || status == "Export")
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
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("CfsImport", true);
    }
}