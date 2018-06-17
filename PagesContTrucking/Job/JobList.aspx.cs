using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PagesContTrucking_Job_JobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.QueryString["ContStatus"] != null && Request.QueryString["ContStatus"].ToString() != "")
            //{
            //    cbb_StatusCode.Text = Request.QueryString["ContStatus"].ToString();
            //}
            //else
            //{
            //    cbb_StatusCode.Text = "All";
            //}
            //cb_ContStatus0.Checked = false;
            lb_new_WareHouseId.Text = System.Configuration.ConfigurationManager.AppSettings["Warehouse"].Trim();

            cb_cs_New.Checked = true;
            cb_cs_Import.Checked = true;
            cb_cs_Return.Checked = true;
            cb_cs_Collection.Checked = true;
            cb_cs_Export.Checked = true;
            cb_cs_WHSLD.Checked = true;
            cb_cs_WHSMT.Checked = true;
            cb_cs_CustomerLD.Checked = true;
            cb_cs_CustomerMT.Checked = true;


            txt_search_dateFrom.Date = DateTime.Now;
            txt_search_dateTo.Date = DateTime.Now;
            btn_search_Click(null, null);


            EzshipHelper_Authority.Bind_Authority(this.Page);
        }
    }

    public string xmlChangeToHtml1(object contNo, object jobNo)
    {
        string res = "";
        string sql = string.Format(@"select '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' as trips,Det1Id,DriverCode from CTM_JobDet2 as det2 inner join CTM_Job job on det2.JobNo=job.JobNo where det2.ContainerNo=@ContainerNo and det2.JobNo=job.JobNo and job.ClientRefNo=@JobNo ");
        List<ConnectEdi.cmdParameters> list = new List<ConnectEdi.cmdParameters>();
        list.Add(new ConnectEdi.cmdParameters("@ContainerNo", contNo, SqlDbType.NVarChar));
        list.Add(new ConnectEdi.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
        DataTable tab = ConnectEdi.GetDataTable(sql, list);
        if (tab.Rows.Count > 0)
        {
            string par = tab.Rows[0]["trips"].ToString();
            string contId = tab.Rows[0]["Det1Id"].ToString();
            string driverCode = tab.Rows[0]["DriverCode"].ToString();
            res = par.ToString();
            res = res.Replace("&lt;", "<");
            res = res.Replace("&gt;", ">");
            if (res.Length < 2 && SafeValue.SafeInt(contId, 0) > 0&&driverCode.Length==0)
            {
                res = "<span class='X'>Trips</span>";
            }
        }
        else {
            res = "<span class='X'>Trips</span>";
        }
        return res;
    }
    public string xmlChangeToHtml(object par, object contId)
    {

        string res = par.ToString();
        res = res.Replace("&lt;", "<");
        res = res.Replace("&gt;", ">");

        if ((res.Length < 2 && SafeValue.SafeInt(contId, 0) > 0))
        {
            res = "<span class='X'>Trips</span>";
        }
        return res;
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string From = txt_search_dateFrom.Date.ToString("yyyyMMdd");
        string To = txt_search_dateTo.Date.AddDays(1).ToString("yyyyMMdd");
        string ContNo = txt_search_ContNo.Text;
        //bool ContStatus_N = cb_ContStatus1.Checked;
        //bool ContStatus_I = cb_ContStatus2.Checked;
        //bool ContStatus_CL = cb_ContStatus3.Checked;
        //bool ContStatus_R = cb_ContStatus4.Checked;
        //bool ContStatus_E = cb_ContStatus5.Checked;
        //bool ContStatus_C = cb_ContStatus6.Checked;
        //bool ContStatus_UnC = cb_ContStatus7.Checked;

        bool cs_New = cb_cs_New.Checked;
        bool cs_Completed = cb_cs_Completed.Checked;
        bool cs_Uncomplete = false;// cb_cs_Uncomplete.Checked;
        bool cs_Import = cb_cs_Import.Checked;
        bool cs_Return = cb_cs_Return.Checked;
        bool cs_Collection = cb_cs_Collection.Checked;
        bool cs_Export = cb_cs_Export.Checked;
        bool cs_WHSMT = cb_cs_WHSMT.Checked;
        bool cs_WHSLD = cb_cs_WHSLD.Checked;
        bool cs_CustomerMT = cb_cs_CustomerMT.Checked;
        bool cs_CustomerLD = cb_cs_CustomerLD.Checked;

        string JobNo = txt_search_jobNo.Text;
        string JobType = search_JobType.Value.ToString();
        string Vessel = txt_Vessel.Text;
        string Client = btn_ClientId.Text;
        string NextTrip = SafeValue.SafeString(search_NextTrip.Value);
        string subContract = SafeValue.SafeString(cbb_subContract.Value, "ALL");
        //string ClientName = info["ClientName"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", "%" + ContNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@NextTrip", NextTrip, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@subContract", subContract, SqlDbType.NVarChar, 10));



        string sql = string.Format(@"select job.Id,det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,det1.YardAddress as Depot,job.ClientRefNo,
(select distinct isnull(pm.PermitNo,'')+',' from job_house as cg left outer join ref_permit as pm on cg.HblNo=pm.HblNo and cg.JobNo=pm.JobNo where cg.ContId=det1.Id for xml path('') ) as PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.SealNo,det1.ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,job.Escort_Ind,job.Escort_Remark,
det1.UrgentInd,job.OperatorCode,job.CarrierBkgNo,det1.oogInd,det1.CfsStatus,det1.DischargeCell,IsTrucking,IsWarehouse,IsLocal,IsAdhoc,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,
(select top 1 Name from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,job.Contractor,job.ClientContact,det1.ContainerCategory,
(select (case when len(DriverCode)>0 then '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' else '<span class=''X''>'+TripCode+'</span>' end) from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')) as trips,
datediff(hour,isnull((select top 1 convert(nvarchar(8),det2.FromDate,112)+' '+det2.FromTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and (det2.Statuscode='S' or det2.Statuscode='C') order by det2.FromDate,det2.FromTime),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)),
case when det1.StatusCode='Completed' then isnull((select top 1 convert(nvarchar(8),det2.ToDate,112)+' '+det2.ToTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and det2.Statuscode='C' order by det2.ToDate desc,det2.ToTime desc),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)) else getdate() end)  as hr,
isnull((select ','+det2.Statuscode+':'+TripCode from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')),'')+',' as str_trips,
isnull((select top 1 det2.Statuscode+':'+TripCode from CTM_JobDet2 as det2 where det2.Det1Id=det1.Id order by det2.Id desc),'') as curTrip,
isnull((select top 1 det2.ReturnLastDate from CTM_JobDet2 det2 where det2.JobNo=job.JobNo and det2.TripCode='RET'),'') as  ReturnLastDate
,det1.WhsReadyInd,det1.WhsReadyLocation,det1.WhsReadyTime,det1.WhsReadyWeight
from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo and job.JobStatus<>'Voided'");
        string sql_where = "";
        if (ContNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "det1.ContainerNo like @ContNo");
        }
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
        }
        string sql_part1 = string.Format(@"select *,(case JobType when 'IMP' then (case when StatusCode='New' then 'IMP' when StatusCode='Import' and curTrip<>'C:IMP' then 'IMP' when StatusCode='WHS-MT' then 'RET' when StatusCode='Return' and curTrip<>'C:RET' then 'RET' else '' end) 
when 'EXP' then (case when StatusCode='New' then 'COL' when StatusCode='Collection' and curTrip<>'C:COL'then 'COL' when StatusCode='WHS-LD' then 'EXP' when StatusCode='Export' and curTrip<>'C:EXP' then 'EXP' else '' end) 
else ''  end) as NextTrip
from(");
        string sql_part2 = string.Format(@") as temp");
        if (sql_where.Equals(""))
        {
            sql_where = " datediff(d,@DateFrom,job.EtaDate)>=0 and datediff(d,@DateTo,job.EtaDate)<=0";
            //sql_where = " @DateFrom<=job.ScheduleDate and @DateTo>job.ScheduleDate";
            if (!cs_Uncomplete)
            {
                string sql_where_ContStatus = "";
                if (cs_New)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='New'";
                }
                if (cs_Completed)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Completed'";
                }
                if (cs_Import)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Import'";
                }
                if (cs_Return)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Return'";
                }
                if (cs_Collection)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Collection'";
                }
                if (cs_Export)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Export'";
                }
                //================== warhouse
                if (cs_WHSMT)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='WHS-MT'";
                }
                if (cs_WHSLD)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='WHS-LD'";
                }
                if (cs_CustomerMT)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Customer-MT'";
                }
                if (cs_CustomerLD)
                {
                    sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det1.StatusCode='Customer-LD'";
                }
                //===================
                if (!sql_where_ContStatus.Equals(""))
                {
                    sql_where = GetWhere(sql_where, "(" + sql_where_ContStatus + ")");
                }
            }
            if (Vessel.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.Vessel=@Vessel");
            }
            if (JobType.Length > 0 && !JobType.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "job.JobType=@JobType");
            }
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }
            if(subContract.Length>0&& subContract != "ALL")
            {
                
                sql_where = GetWhere(sql_where, "job.Contractor=@subContract");
            }


            sql = sql_part1 + sql + " where job.JobType in ('IMP','EXP','LOC') and IsTrucking='Yes' and " + sql_where + sql_part2;
            if (NextTrip.Length > 0 && !NextTrip.Equals("ALL"))
            {
                sql = "select * from (" + sql + ") as temp where NextTrip=@NextTrip order by EtaDate,JobNo,Id,StatusCode desc, JobDate asc";
            }
            else
            {
                if (cs_Uncomplete)
                {
                    sql = "select * from (" + sql + ") as temp where StatusCode<>'Completed' and ((JobType='IMP' and CHARINDEX(',C:RET,',str_trips)>0) or (JobType='EXP' and CHARINDEX(',C:EXP,',str_trips)>0)) order by EtaDate,JobNo,Id,StatusCode desc, JobDate asc";
                }
                else
                {
                    sql += " order by EtaDate,JobNo,Id,StatusCode desc, JobDate asc";
                }
            }
        }
        else
        {
            sql = sql_part1 + sql + " where job.JobType in ('IMP','EXP','LOC') and IsTrucking='Yes' and " + sql_where+ sql_part2;
            sql += " order by EtaDate,JobNo,Id,StatusCode desc,ScheduleDate asc";
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
        if (jobStatus == "Quoted")
        {
            quoteNo = C2Setup.GetNextNo("", "CTM_Job_" + jobStatus, date);
        }
        else
        {
            jobno = C2Setup.GetNextNo("", "CTM_Job_" + jobType1, date);
            quoteNo = jobno;
        }
        string wh = txt_new_WareHouseId.Text;// System.Configuration.ConfigurationManager.AppSettings["Warehosue"];
        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,IsTrucking,IsWarehouse) values ('{0}','{4}',getdate(),getdate(),getdate(),'USE','{1}',getdate(),'{1}',getdate(),'{2}','{2}','{3}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','Pending',getdate(),'{13}','Yes','No') select @@identity", jobno, user, time4, cbb_new_jobtype.Value, txt_new_JobDate.Date, btn_new_ClientId.Text, txt_DepotAddress.Text, txt_FromAddress.Text, txt_ToAddress.Text, txt_new_remark.Text, txt_WarehouseAddress.Text, jobStatus, quoteNo, wh);
        int jobId = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
        if (jobId > 0)
        {
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
}