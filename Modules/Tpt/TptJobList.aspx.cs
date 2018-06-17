using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Aspose.Cells;

public partial class PagesContTrucking_Job_TptJobList : System.Web.UI.Page
{
    DataTable dtTmp = C2.Manager.ORManager.GetDataSet(@"select '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' as trips,Det1Id
,job.ClientRefNo as ClientRefNo,det2.ContainerNo as ContainerNo
FROM CTM_JobDet2 as det2 inner join CTM_Job job on det2.JobNo=job.JobNo").Tables[0];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() != "")
            {
                search_JobType.Text = Request.QueryString["type"].ToString();
            }
            //else
            //{
            //    cbb_StatusCode.Text = "All";
            //}
            //cb_ContStatus0.Checked = false;
            lb_new_WareHouseId.Text = System.Configuration.ConfigurationManager.AppSettings["Warehouse"].Trim();

            //cb_cs_New.Checked = true;
            //cb_cs_Start.Checked = true;
            //cb_cs_Delivered.Checked = true;
            //cb_cs_Depart.Checked = true;
            //cb_cs_Arrival.Checked = true;
            //cb_cs_Returned.Checked = true;
            //cb_cs_WHSLD.Checked = true;
            //cb_cs_WHSMT.Checked = true;

            cb_cs_Pending.Checked = true;
            cb_cs_Started.Checked = true;
            cb_cs_Completed.Checked = true;
            cb_cs_Cancel.Checked = false;


            txt_search_dateFrom.Date = DateTime.Today;
            txt_search_dateTo.Date = DateTime.Today;
            btn_search_Click(null, null);


            EzshipHelper_Authority.Bind_Authority(this.Page);
        }
    }
    public string xmlChangeToHtml1(object contNo, object jobNo)
    {
        string res = "";
        //string sql = string.Format(@"select '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' as trips,Det1Id from CTM_JobDet2 as det2 inner join CTM_Job job on det2.JobNo=job.JobNo where det2.ContainerNo=@ContainerNo and det2.JobNo=job.JobNo and job.ClientRefNo=@JobNo ");
        //List<ConnectEdi.cmdParameters> list = new List<ConnectEdi.cmdParameters>();
        //list.Add(new ConnectEdi.cmdParameters("@ContainerNo", contNo, SqlDbType.NVarChar));
        //list.Add(new ConnectEdi.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
        //DataTable tab = ConnectEdi.GetDataTable(sql, list);

        dtTmp.DefaultView.RowFilter = string.Format("ClientRefNo='{0}' and ContainerNo='{1}'", contNo, jobNo);//dtTmp
        DataTable tab = dtTmp.DefaultView.ToTable();
        if (tab.Rows.Count > 0)
        {
            string par = tab.Rows[0]["trips"].ToString();
            string contId = tab.Rows[0]["Det1Id"].ToString();
            res = par.ToString();
            res = res.Replace("&lt;", "<");
            res = res.Replace("&gt;", ">");
            if (res.Length < 2 && SafeValue.SafeInt(contId, 0) > 0)
            {
                res = "<span class='X'>Trips</span>";
            }
        }
        else
        {
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
        //throw new Exception(sql.ToString());
        this.grid_Transport.DataSource = getData();
        this.grid_Transport.DataBind();


    }

    private DataTable getData()
    {

        string From = txt_search_dateFrom.Date.ToString("yyyyMMdd");
        string To = txt_search_dateTo.Date.AddDays(1).ToString("yyyyMMdd");


        bool cs_Pending = cb_cs_Pending.Checked;
        bool cs_Started = cb_cs_Started.Checked;
        bool cs_Completed = cb_cs_Completed.Checked;
        bool cs_Cancel = cb_cs_Cancel.Checked;

        string JobNo = txt_search_jobNo.Text;
        string JobType = search_JobType.Text;
        string keyword = txt_Keyword.Text;
        string Client = btn_ClientId.Text;
        //string NextTrip = SafeValue.SafeString(search_NextTrip.Value);
        //string ClientName = info["ClientName"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Search1", "%" + keyword, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Search2", "%" + keyword + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        //list.Add(new ConnectSql_mb.cmdParameters("@NextTrip", NextTrip, SqlDbType.NVarChar, 10));



        //        string sql = string.Format(@"select job.Id,det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,det1.YardAddress as Depot,job.ClientRefNo,
        //job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.SealNo,det1.ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,
        //det1.UrgentInd,job.OperatorCode,job.CarrierBkgNo,det1.oogInd,det1.CfsStatus,det1.DischargeCell,job.IsTrucking,IsWarehouse,IsLocal,IsAdhoc,job.Escort_Ind,job.Escort_Remark,
        //job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,
        //(select top 1 Name from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
        //job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,job.Contractor,job.ClientContact,det2.BookingDate,det2.BookingTime,
        //(select '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')) as trips,
        //datediff(hour,isnull((select top 1 convert(nvarchar(8),det2.FromDate,112)+' '+det2.FromTime
        //from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and (det2.Statuscode='S' or det2.Statuscode='C') order by det2.FromDate,det2.FromTime),
        //convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)),
        //case when det1.StatusCode='Completed' then isnull((select top 1 convert(nvarchar(8),det2.ToDate,112)+' '+det2.ToTime
        //from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and det2.Statuscode='C' order by det2.ToDate desc,det2.ToTime desc),
        //convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)) else getdate() end)  as hr,
        //isnull((select ','+det2.Statuscode+':'+TripCode from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')),'')+',' as str_trips,
        //isnull((select top 1 det2.Statuscode+':'+TripCode from CTM_JobDet2 as det2 where det2.Det1Id=det1.Id order by det2.Id desc),'') as curTrip
        //from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo left outer join CTM_JobDet2 as det2 on job.JobNo=det2.JobNo and job.JobStatus<>'Voided'");
        string sql = string.Format(@"select job.Id,det2.Id as TripId,det2.JobNo,job.StatusCode as JobStatus,job.JobDate,job.ClientRefNo,det2.TripIndex, det2.DoubleMounting, det2.DriverCode2, det2.PermitNo, det2.PermitRemark, det2.ManualDo,
job.Remark,job.SpecialInstruction,det2.ContainerNo,det2.RequestTrailerType as ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,
job.OperatorCode,job.CarrierBkgNo,job.IsTrucking,IsWarehouse,IsLocal,IsAdhoc,job.Escort_Ind,job.Escort_Remark,det2.RequestTrailerType,
job.Pol,job.Pod,job.Vessel,job.Voyage,det2.FromCode as PickupFrom,det2.ToCode as DeliveryTo,det2.BookingDate as ScheduleDate,det2.StatusCode ,det2.WarehouseStatus,det2.DriverCode,det2.TowheadCode,det2.Direct_Inf,det2.SubCon_Ind,
det2.ServiceType,det2.WarehouseRemark,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as inc1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as inc2,
(select top 1 Name from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,det2.JobType,job.Contractor,job.ClientContact,det2.BookingDate,det2.BookingTime,det2.Self_Ind,
 (case when len(DriverCode)>0 then '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' else '<span class=''X''>'+TripCode+'</span>' end) as trips,
isnull( det2.Statuscode+':'+TripCode ,'') as str_trips
from CTM_JobDet2 as det2 
left outer join CTM_Job as job on job.JobNo=det2.JobNo and job.JobStatus<>'Voided'");
        string sql_where = "";
        if (keyword.Length > 0)
        {
            sql_where = GetWhere(sql_where,
            @" (det2.ContainerNo like @Search1 
			or det2.PermitNo like @Search1 
			or det2.ManualDo like @Search1 
			or det2.PermitRemark like @Search1 
			or det2.DriverCode like @Search1 
			or job.Vessel like @Search2 
			or job.ClientRefNo like @Search1)
			"
            );
        }
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "det2.JobNo like @JobNo");
        }
        //        string sql_part1 = string.Format(@"select *,(case JobType when 'WGR' then (case when StatusCode='New' then 'LOC' when StatusCode='Start' and curTrip<>'C:LOC' then 'LOC' when StatusCode='Delivered' then 'LOC' when StatusCode='LCL-MT' then 'SHF' when StatusCode='Depart' then 'SHF' else '' end) 
        //when 'WDO' then (case when StatusCode='Start' then 'SHF' when StatusCode='Arrival' and curTrip<>'C:LOC' then 'LOC' when StatusCode='LCL-LD' then 'LOC' when StatusCode='Depart' and curTrip<>'C:LOC' then 'LOC' else '' end) 
        //else ''  end) as NextTrip
        //from(");
        string sql_part1 = string.Format(@"select *,'' as NextTrip from(");
        string sql_part2 = string.Format(@") as temp");
        if (sql_where.Equals(""))
        {
            sql_where = " @DateFrom<=det2.BookingDate and @DateTo>det2.BookingDate";

            #region checkbox s
            string sql_where_ContStatus = "";
            if (cs_Pending)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det2.StatusCode='P'";
            }
            if (cs_Started)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det2.StatusCode='S'";
            }
            if (cs_Completed)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det2.StatusCode='C'";
            }
            if (cs_Cancel)
            {
                sql_where_ContStatus += (sql_where_ContStatus.Equals("") ? "" : " or ") + "det2.StatusCode='X'";
            }
            if (!sql_where_ContStatus.Equals(""))
            {
                sql_where = GetWhere(sql_where, "(" + sql_where_ContStatus + ")");
            }
            #endregion
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }


            sql = sql_part1 + sql + " where " + sql_where + sql_part2;

            sql += " order by BookingDate desc,EtaDate,JobNo,Id,StatusCode desc";


        }
        else
        {
            if (JobType.Length > 0 && !JobType.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "det2.JobType=@JobType");
            }
            else
            {
                sql_where = GetWhere(sql_where, "det2.JobType in ('TPT','WGR','WDO')");
            }
            sql = sql_part1 + sql + " where " + sql_where + sql_part2;
            sql += " order by ScheduleDate asc, EtaDate,JobNo,Id,StatusCode desc";
        }
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        return dt;
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
        if (status == "Start")
        {
            color = "orange";
        }
        if (status == "Arrival" || status == "Depart"
          || status == "LCL-MT" || status == "Delivered"
          || status == "LCL-LD" || status == "Returned")
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

        //=========== warehouse status
        if(status== "Started")
        {
            color = "green";
        }
        //===============end

        if (status == "P")
        {
            color = "orange";
        }
        if (status == "S")
        {
            color = "green";
        }
        if (status == "C")
        {
            color = "blue";
        }
        if (status == "X")
        {
            color = "gray";
        }
        return color;
    }

    public string warehouseStatus_exchange(string par,string type)
    {
        string res = par;
        if (type == "WDO")
        {
            if (par == "Started")
            {
                res = "LCL-MT";
            }
            if (par == "Completed")
            {
                res = "LCL-LD";
            }
        }
        if (type == "WGR")
        {
            if (par == "Started")
            {
                res = "LCL-LD";
            }
            if (par == "Completed")
            {
                res = "LCL-MT";
            }
        }
        return res;
    }

    protected void btn_save2Excel_Click(object sender, EventArgs e)
    {

        string temp = invoice_download_excel(getData());
        Response.Write("<script>window.open('" + temp + "');</script>");
    }
    public string invoice_download_excel(DataTable dt)
    {

        string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).Parent.FullName;
        string fileName =""+DateTime.Now.Ticks;
        string to_file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "Tpt_" + fileName + ".csv");
        
        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(HttpContext.Current.Server.MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        //wb.Open(file);
        Worksheet ws = wb.Worksheets[0];
        int baseRow = 0;
        baseRow = 0;
        ws.Cells[baseRow++, 0].PutValue("Company:");
        ws.Cells[baseRow++, 0].PutValue("Address:");
        ws.Cells[baseRow++, 0].PutValue("TEL:");
        ws.Cells[baseRow++, 0].PutValue("FAX:");
        ws.Cells[baseRow++, 0].PutValue("ATTN:");
        baseRow = 0;
        ws.Cells[baseRow, 5].PutValue("Description");
        ws.Cells[baseRow++, 6].PutValue("Amount");
        ws.Cells[baseRow, 5].PutValue("Current Charges");
        ws.Cells[baseRow++, 6].PutValue(0);
        ws.Cells[baseRow, 5].PutValue("Total Amount Due");
        ws.Cells[baseRow++, 6].PutValue(0);
        ws.Cells[baseRow, 5].PutValue("Please Pay:");
        ws.Cells[baseRow++, 6].PutValue(0);
        baseRow = baseRow + 2;

        ws.Cells[baseRow, 0].PutValue("S/N");
        ws.Cells[baseRow, 1].PutValue("Date");
        ws.Cells[baseRow, 2].PutValue("Con.No.");
        ws.Cells[baseRow, 3].PutValue("Description");
        ws.Cells[baseRow, 4].PutValue("Service");
        ws.Cells[baseRow, 5].PutValue("Amount");
        ws.Cells[baseRow, 6].PutValue("OT");
        ws.Cells[baseRow, 7].PutValue("Remark");
        baseRow++;
        int i = 0;
        for (; i < dt.Rows.Count;)
        {
            ws.Cells[baseRow + i, 0].PutValue(i+1);
            ws.Cells[baseRow + i, 1].PutValue(SafeValue.SafeDate( dt.Rows[i]["BookingDate"],new DateTime(1753,1,1)).ToString("yyyy-MM-dd"));
            ws.Cells[baseRow + i, 2].PutValue(dt.Rows[i]["ContainerNo"]);
            ws.Cells[baseRow + i, 3].PutValue(dt.Rows[i]["WarehouseRemark"]);
            ws.Cells[baseRow + i, 4].PutValue(dt.Rows[i]["ServiceType"]);
            ws.Cells[baseRow + i, 5].PutValue(dt.Rows[i]["inc1"]);
            ws.Cells[baseRow + i, 6].PutValue(dt.Rows[i]["inc2"]);
            ws.Cells[baseRow + i, 7].PutValue(dt.Rows[i]["SpecialInstruction"]);

            i++;
        }
        wb.Save(to_file);

        //string context = Common.StringToJson(Path.Combine("files", "Excel_DailyTrips", "inv_"  + fileName + ".csv"));
        //Common.WriteJson(true, context);
        string context = "../../files/Excel_DailyTrips/Tpt_" + fileName + ".csv";// Path.Combine("../files", "Excel_DailyTrips", "inv_" + fileName + ".csv");
        return context;
    }

}