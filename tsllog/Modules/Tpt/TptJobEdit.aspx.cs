﻿using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wilson.ORMapper;

public partial class PagesContTrucking_Job_TptJobEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			//Session["CTM_Job"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                txt_search_JobNo.Text = Request.QueryString["no"].ToString();
                string sql = string.Format(@"select count(*) from ctm_job where JobNo='{0}'", Request.QueryString["no"]);
                int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
                if (n > 0)
                {
                    Session["CTM_Job_" + txt_search_JobNo.Text] = " JobNo='" + txt_search_JobNo.Text + "'";
                    this.dsJob.FilterExpression = " JobNo='" + txt_search_JobNo.Text + "'";
                    if (this.grid_job.GetRow(0) != null)
                        this.grid_job.StartEdit(0);
                }
                else
                {
                    Session["CTM_Job_" + txt_search_JobNo.Text] = " QuoteNo='" + txt_search_JobNo.Text + "'";
                    this.dsJob.FilterExpression = " QuoteNo='" + txt_search_JobNo.Text + "'";
                    if (this.grid_job.GetRow(0) != null)
                        this.grid_job.StartEdit(0);
                }
            }
			else
			{
				this.grid_job.AddNewRow();
			}
		}

		if (Session["CTM_Job_" + txt_search_JobNo.Text] != null)
		{
			this.dsJob.FilterExpression = Session["CTM_Job_" + txt_search_JobNo.Text].ToString();
			if (this.grid_job.GetRow(0) != null)
				this.grid_job.StartEdit(0);
		}
        OnLoad();
	}

	#region Job
    protected void grid_job_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.CtmJob));
		}
	}
	protected void grid_job_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		e.NewValues["JobDate"] = DateTime.Now;
		e.NewValues["EtaDate"] = DateTime.Now;
		e.NewValues["EtdDate"] = DateTime.Now;
		e.NewValues["CodDate"] = DateTime.Now;
		e.NewValues["JobType"] = "";
		e.NewValues["IsTrucking"] = "Yes";
		e.NewValues["Pod"] = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
	}
	protected void grid_job_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
	{
		string s = e.Parameters;
		if (s == "Void")
		{
			e.Result = job_void();
		}

		if (s == "Save")
		{
			e.Result = job_save();
		}

		if (s == "Close")
		{
			e.Result = job_close();
		}

		if (s == "AutoBilling")
		{
			job_auto_billing();
		}

		if (s == "AddGR")
		{
            ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            string doNo = add_wh("DOIN", "IMPORT", "IN", txt_JobNo.Text,"");
			container_add(doNo, "IN");
			e.Result = doNo;
		}

		if (s == "AddDO")
		{
            ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            string doNo = add_wh("DOOUT", "EXPORT", "OUT", txt_JobNo.Text, "");
			container_add(doNo, "OUT");
			e.Result = doNo;
		}

		if (s == "CreateInv")
		{
			ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            string sql = string.Format(@"select sum(isnull(fee1,0)+isnull(fee2,0)+isnull(fee3,0)+isnull(fee4,0)+isnull(fee5,0)+isnull(fee6,0)+isnull(fee11,0)+isnull(fee12,0)+isnull(fee13,0)+isnull(fee14,0)+isnull(fee15,0)+isnull(fee16,0)+isnull(fee17,0)+isnull(fee18,0)+isnull(fee19,0)+isnull(fee21,0)+isnull(fee22,0)+isnull(fee23,0)+isnull(fee24,0)+isnull(fee25,0)+isnull(fee26,0)+isnull(fee27,0)+isnull(fee28,0)+isnull(fee29,0)+isnull(fee30,0)+isnull(fee31,0)+isnull(fee32,0)+isnull(fee33,0)+isnull(fee34,0)+isnull(fee35,0)+isnull(fee36,0)+isnull(fee38,0)+isnull(fee39,0)) as Total  from CTM_JobDet1 where JobNo='{0}' group by  JobNo
", txt_JobNo.Text);
			decimal n = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
			if (n > 0)
			{
				job_create_inv();
			}
			else
			{
				e.Result = "No Price";
			}
		}
        if(s=="Costing"){
            ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
            #region Costing
            string sql = string.Format(@"select ContainerNo,ContainerType from CTM_JobDet1 where JobNo='{0}'", txt_JobNo.Text.Trim());
            DataTable dt_cnt = ConnectSql.GetTab(sql);
            for (int a = 0; a < dt_cnt.Rows.Count; a++)
            {
                string cntNo = dt_cnt.Rows[a]["ContainerNo"].ToString();
                string cntType = dt_cnt.Rows[a]["ContainerType"].ToString();
                string des=cntNo+" / "+cntType;
                sql = string.Format(@"select charge1,charge2,charge3,charge4,charge5,charge6,charge7,charge8,charge9,charge10 from CTM_JobDet2 where JobNo='{0}' and ContainerNo='{1}'", txt_JobNo.Text.Trim(), cntNo);
                DataTable tab = ConnectSql.GetTab(sql);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    decimal charge1 = SafeValue.SafeDecimal(tab.Rows[i]["charge1"]);
                    
                    decimal charge2 = SafeValue.SafeDecimal(tab.Rows[i]["charge2"]);
                    decimal charge3 = SafeValue.SafeDecimal(tab.Rows[i]["charge3"]);
                    decimal charge4 = SafeValue.SafeDecimal(tab.Rows[i]["charge4"]);
                    decimal charge5 = SafeValue.SafeDecimal(tab.Rows[i]["charge5"]);
                    decimal charge6 = SafeValue.SafeDecimal(tab.Rows[i]["charge6"]);
                    decimal charge7 = SafeValue.SafeDecimal(tab.Rows[i]["charge7"]);
                    decimal charge8 = SafeValue.SafeDecimal(tab.Rows[i]["charge8"]);
                    decimal charge9 = SafeValue.SafeDecimal(tab.Rows[i]["charge9"]);
                    decimal charge10 = SafeValue.SafeDecimal(tab.Rows[i]["charge10"]);

                    
                    string[] ChgCode_List = { "DHC","WEIGHING","WASHING","REPAIR","DETENTION","DEMURRAGE","LIFT ON/OFF","C/SHIPMENT","OTHER","EMF"};
                    if (charge1 > 0)
                    {
                        create_cost(ChgCode_List[0], 1, txt_JobNo.Text, des, charge1, "CTM");
                    }
                    if (charge2 > 0)
                    {
                        create_cost(ChgCode_List[1], 1, txt_JobNo.Text, des, charge2, "CTM");
                    }
                    if (charge3 > 0)
                    {
                        create_cost(ChgCode_List[2], 1, txt_JobNo.Text, des, charge3, "CTM");
                    }
                    if (charge4 > 0)
                    {
                        create_cost(ChgCode_List[3], 1, txt_JobNo.Text, des, charge4, "CTM");
                    }
                    if (charge5 > 0)
                    {
                        create_cost(ChgCode_List[4], 1, txt_JobNo.Text, des, charge5, "CTM");
                    }
                    if (charge6 > 0)
                    {
                        create_cost(ChgCode_List[5], 1, txt_JobNo.Text, des, charge6, "CTM");
                    }
                    if (charge7 > 0)
                    {
                        create_cost(ChgCode_List[6], 1, txt_JobNo.Text, des, charge7, "CTM");
                    }
                    if (charge8 > 0)
                    {
                        create_cost(ChgCode_List[7], 1, txt_JobNo.Text, des, charge8, "CTM");
                    }
                    if (charge9 > 0)
                    {
                        create_cost(ChgCode_List[8], 1, txt_JobNo.Text, des, charge9, "CTM");
                    }
                    if (charge10 > 0)
                    {
                        create_cost(ChgCode_List[9], 1, txt_JobNo.Text, des, charge10, "CTM");
                    }
                    e.Result = "Success";

                }
            }
            #endregion
        }
        if (s == "PrintHaulier")
        {
            e.Result = "PrintHaulier";
        }
        if (s == "GenerateQ")
        {
            e.Result = quotation_generate();
        }
        if (s == "ConfirmQ")
        {
            e.Result = quotation_confim();
        }
        if (s == "CompletedQ")
        {
            e.Result = quotation_completed();
        }
        if (s == "VoidQ")
        {
            e.Result = quotation_void();
        }
        if (s == "CopyQ")
        {
            e.Result = copy_job();
        }
		else if (s== "SEND")
        {
            #region Send Email
            ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
            ASPxGridView grid = sender as ASPxGridView;
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxComboBox cbb_Email1 = pageControl.FindControl("cbb_Email1") as ASPxComboBox;
            ASPxTextBox txt_Email2 = pageControl.FindControl("txt_Email2") as ASPxTextBox;
            ASPxTextBox txt_Email3 = pageControl.FindControl("txt_Email3") as ASPxTextBox;
            ASPxTextBox txt_Subject = pageControl.FindControl("txt_Subject") as ASPxTextBox;
            ASPxMemo memo_message = pageControl.FindControl("memo_message") as ASPxMemo;
            ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
            string path1 = string.Format("~/files/quotation/");
            string path2 = path1.Replace(' ', '_').Replace('\'', '_');
            string pathx = path2.Substring(1);
            string path3 = MapPath(path2);
            if (!Directory.Exists(path3))
                Directory.CreateDirectory(path3);
            string fileName = string.Format(@"~\files\quotation\{0}.pdf", txt_QuoteNo.Text);

            string e_file = HttpContext.Current.Server.MapPath(fileName);

            XtraReport rpt = new XtraReport();
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Quotation.repx"));
            DataSet set = InvoiceReport.DsQuotation(txt_QuoteNo.Text, "");

            rpt.DataSource = set;
            rpt.CreateDocument();

            rpt.ExportToPdf(e_file);


            string company = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            string address1 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"];
            string address2 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"];
            string address3 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
            string sql = string.Format(@"select Email1,Email2,Name from xxparty where PartyId='{0}'", btn_ClientId.Text);
            DataTable tab = ConnectSql.GetTab(sql);
            string add = address1 + " " + address2 + " " + address3;
            string title = "";
            if (txt_Subject.Text == "")
            {
                title = string.Format(@"" + txt_QuoteNo.Text + "//" + "QUOTATION FOR JOB");
            }
            else
            {
                title = SafeValue.SafeString(txt_Subject.Text);
            }
            if (tab.Rows.Count > 0)
            {
                string email1 = SafeValue.SafeString(cbb_Email1.Value);
                string email2 = SafeValue.SafeString(txt_Email2.Text);
                string email3 = SafeValue.SafeString(txt_Email3.Text);
                string name = SafeValue.SafeString(tab.Rows[0]["Name"]);
                string user = HttpContext.Current.User.Identity.Name;
                string mes =
   string.Format(@"<b>{0}</b><br><br>
{1}<br><br>
<b>Dear Customer, <br><br>Kindly review attached document for quotation.</b>
<br><br>
<b>This is a computer generated email, please DO NOT reply.
<br><br>
</b><br><br>
<b>{2}</b>
<br/>
                     ", company, add, user);

                string sql_email = string.Format(@"select Email from [dbo].[User] where Name='{0}'", user);

                string userEmail = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql_email));
                if (email1.Length > 0)
                {
                    try
                    {
                        Helper.Email.SendEmail(email1, email2 + "," + userEmail, email3, title, mes + memo_message.Text, fileName);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Error";
                }
            }


            #endregion
        }
        else if (s == "AutoLines")
        {
            e.Result = auto_add_lines();
        }
	}
    private string auto_add_lines()
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxComboBox cbb_Count = pageControl.FindControl("cbb_Count") as ASPxComboBox;
        ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        ASPxLabel lbl_JobType = this.grid_job.FindEditFormTemplateControl("lbl_JobType") as ASPxLabel;
        int count = SafeValue.SafeInt(cbb_Count.Value, 0);
        string userId = HttpContext.Current.User.Identity.Name;
        for (int i = 0; i < count; i++)
        {
            C2.JobRate rate = new JobRate();
            rate.JobNo = txt_QuoteNo.Text;
            rate.JobType = lbl_JobType.Text;
            rate.ChgCode = "";
            rate.ChgCodeDe = "";
            rate.ClientId = btn_ClientId.Text;
            rate.Qty = 1;
            rate.Price = 0;
            rate.Unit = "Unit";
            rate.ContSize = "";
            rate.ContType = "";
            rate.Remark = "";
            rate.BillType = "";
            rate.BillScope = "";
            rate.LineType = "QUOTED";
            rate.RowCreateUser = userId;
            rate.RowCreateTime = DateTime.Now;
            rate.RowUpdateTime = DateTime.Now;
            rate.RowUpdateUser = userId;

            C2.Manager.ORManager.StartTracking(rate, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(rate);
        }
        return "Action Success!";
    }
    private string copy_job()
    {
        string quoteNo = "";
        ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "QuoteNo='" + txt_QuoteNo.Text + "'");
        C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
        if (ctmJob != null)
        {
            quoteNo = C2Setup.GetNextNo("", "CTM_Job_Quoted", DateTime.Today);
            C2.CtmJob job = new CtmJob();
            job.QuoteNo = quoteNo;
            job.QuoteDate = DateTime.Today;
            job.QuoteStatus = "Pending";
            //Remark
            job.QuoteRemark = ctmJob.QuoteRemark;
            job.JobDes = ctmJob.JobDes;
            job.TerminalRemark = ctmJob.TerminalRemark;
            job.LumSumRemark = ctmJob.LumSumRemark;
            job.InternalRemark = ctmJob.InternalRemark;
            job.AdditionalRemark = ctmJob.AdditionalRemark;

            job.ClientId = ctmJob.ClientId;
            job.JobType = ctmJob.JobType;
            job.JobStatus = "Quoted";
            job.JobDate = DateTime.Today;
            job.SubClientId = ctmJob.SubClientId;
            job.EmailAddress = ctmJob.EmailAddress;
            job.ClientRefNo = ctmJob.ClientRefNo;
            job.ClientContact = ctmJob.ClientContact;


            C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(job);
            C2Setup.SetNextNo("", "CTM_Job_Quoted", quoteNo, DateTime.Today);

            SetQuotation(txt_QuoteNo.Text, quoteNo, ctmJob.ClientId, ctmJob.JobType);

        }
        return "Q_" + quoteNo;
    }
    private string quotation_void()
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string jobStatus = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select JobStatus from CTM_Job where Id={0}", Id.Text)));
        if (jobStatus == "Voided")
        {
            string sql = "update CTM_Job set JobStatus='Quoted' where Id=" + Id.Text;
            if (ConnectSql.ExecuteSql(sql) > 0)
            {
                Event_Log(txt_JobNo.Text, "QUOTATION", 6, SafeValue.SafeInt(Id.Text, 0), "");
                return "Action Success!";
            }
        }
        else
        {
            string sql = "update CTM_Job set JobStatus='Voided' where Id=" + Id.Text;
            if (ConnectSql.ExecuteSql(sql) > 0)
            {
                Event_Log(txt_JobNo.Text, "QUOTATION", 7, SafeValue.SafeInt(Id.Text, 0), "");
                return "Action Success!";
            }
        }

        return "error";
    }
    private string quotation_confim()
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
        ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
        string jobType = SafeValue.SafeString(cbb_JobType.Value);
        if (txt_JobNo.Text.Length < 4)
        {
            string jobno = C2Setup.GetNextNo("", "CTM_Job_" + jobType, DateTime.Today);
            string sql = "update CTM_Job set JobStatus='Confirmed',JobNo='" + jobno + "',JobDate=getdate(),QuoteStatus='Closed' where Id=" + Id.Text;
            if (ConnectSql.ExecuteSql(sql) > 0)
            {
                sql = string.Format(@"update CTM_JobEventLog set JobNo='{1}' where JobNo='{0}'", txt_QuoteNo.Text, jobno);
                ConnectSql.ExecuteSql(sql);
                C2Setup.SetNextNo("", "CTM_Job_" + jobType, jobno, DateTime.Today);
                Event_Log(jobno, "QUOTATION", 4, SafeValue.SafeInt(Id.Text, 0), "");
                return "C_" + jobno;
            }
        }
        else if (SafeValue.SafeString(cmb_JobStatus.Value) == "Quoted")
        {
            #region Confirmed
            if (txt_JobNo.Text == Id.Text)
            {
                string jobno = C2Setup.GetNextNo("", "CTM_Job_" + jobType, DateTime.Today);
                string sql = "update CTM_Job set JobStatus='Confirmed',JobNo='" + jobno + "',JobDate=getdate(),QuoteStatus='Closed' where Id=" + Id.Text;
                if (ConnectSql.ExecuteSql(sql) > 0)
                {
                    C2Setup.SetNextNo("", "CTM_Job_" + jobType, jobno, DateTime.Today);
                    Event_Log(txt_JobNo.Text, "QUOTATION", 4, SafeValue.SafeInt(Id.Text, 0), "");
                    return "C_" + jobno;
                }
            }
            else
            {
                string sql = "update CTM_Job set JobStatus='Confirmed',QuoteStatus='Closed'  where Id=" + Id.Text;
                if (ConnectSql.ExecuteSql(sql) > 0)
                {
                    Event_Log(txt_JobNo.Text, "QUOTATION", 4, SafeValue.SafeInt(Id.Text, 0), "");
                    return "C_" + txt_JobNo.Text;
                }
            }
            #endregion
        }
        else
        {
            string sql = "update CTM_Job set JobStatus='Quoted',QuoteStatus='Pending' where Id=" + Id.Text;
            if (ConnectSql.ExecuteSql(sql) > 0)
            {
                Event_Log(txt_JobNo.Text, "QUOTATION", 6, SafeValue.SafeInt(Id.Text, 0), "");
                return "C_" + txt_JobNo.Text;
            }
        }


        return "error";
    }
    private string quotation_completed()
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string sql = "update CTM_Job set JobStatus='Completed' where Id=" + Id.Text;
        if (ConnectSql.ExecuteSql(sql) > 0)
        {
            Event_Log(txt_JobNo.Text, "QUOTATION", 5, SafeValue.SafeInt(Id.Text, 0), "");
            return "Action Success!";
        }

        return "error";
    }
    private string quotation_generate()
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string sql = "update CTM_Job set QuoteStatus='Quoted' where Id=" + Id.Text;
        if (ConnectSql.ExecuteSql(sql) > 0)
        {
            Event_Log(txt_JobNo.Text, "QUOTATION", 5, SafeValue.SafeInt(Id.Text, 0), "");
            return "Action Success!";
        }

        return "error";


        return "error";
    }
    private void SetQuotation(string no, string quoteNo, string clientId, string jobType)
    {
        string sql = string.Format(@"select * from job_rate where JobNo='{0}'", no);
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
    private void create_cost(string code, decimal qty, string refNo, string des, decimal price, string type)
    {
        CtmCosting cost = new CtmCosting();
        cost.ChgCode = code;
        cost.RefNo = refNo;
        cost.JobType = type;
        cost.ChgCodeDes = des;
        cost.CostQty = qty;
        cost.CostPrice = price;
        cost.CostCurrency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        cost.CostExRate = 1;
        cost.CostGst = 0;
        decimal amt = SafeValue.ChinaRound(qty * price, 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * cost.CostGst), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * cost.CostExRate, 2);
        cost.CostDocAmt = docAmt;
        cost.CostLocAmt = locAmt;
        Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
        Manager.ORManager.PersistChanges(cost);
    }
	private void job_create_inv()
	{
		ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
		//ASPxTextBox txt_JobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
		ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
		ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
		string user = HttpContext.Current.User.Identity.Name;;
		string acCode = EzshipHelper.GetAccArCode("", "SGD");
        ASPxDateEdit jobEta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
        ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
        ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
        ASPxButtonEdit txt_Pol = pageControl.FindControl("txt_Pol") as ASPxButtonEdit;
        ASPxButtonEdit txt_Pod = pageControl.FindControl("txt_Pod") as ASPxButtonEdit;
		string[] ChgCode_List = { "TRUCKING", "FUEL", "DHC", "PORTENT", "CMS", "PSA LOLO", "", "", "", ""
                                    , "WEIGNING", "WASHING", "REPAIR"
                                    , "DETENTION", "DEMURRAGE", "C/S LOLO", "CNL/SHIPMENT"
                                    , "EMF", "OTHER", "", "PSA STORAGE", "EX ONE-WAY", "WRONG WEIGHT",
                                    "ELECTRICITY", "PERMIT", "EXCHANGE DO", "SEAL", "DOCUMENTATION",
                                    "ERP CHARGES", "HEAVYWEIGHT 23/24T", "PSA FLEXIBOOK", "PSA NO SHOW",
                                    "CHASSIS DEMURRAGE", "PARKING", "SHIFTING", "STAND-BY", "MISC 1", "MISC 2", "MISC 3" };

	    string sql = string.Format(@"select * from CTM_JobDet1 where JobNo='{0}'", txt_JobNo.Text);
		DataTable dt = ConnectSql.GetTab(sql);
		if (dt.Rows.Count > 0)
		{
			DateTime dtime = DateTime.Now;

			string invN = C2Setup.GetNextNo("", "AR-IV", dtime);
            string sql_cnt = string.Format(@"select count(*) from XAArInvoice where PartyTo='{0}' and MastRefNo='{1}'", btn_ClientId.Text,txt_JobNo.Text);
			int cnt = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql_cnt), 0);
            string des = "";
			if (cnt == 0)
			{
				#region Inv Mast
                sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo)
values('IV',getdate(),'{4}','{0}',Year(getdate()),Month(getdate()),'CASH',getdate(),'',
'SGD','CTM',1,'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}')
select @@IDENTITY", invN, user, acCode, txt_JobNo.Text, btn_ClientId.Text);
				string docId = ConnectSql_mb.ExecuteScalar(sql);
				C2Setup.SetNextNo("", "AR-IV", invN, dtime);
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string cntNo = SafeValue.SafeString(dt.Rows[i]["ContainerNo"]);
					string cntType = SafeValue.SafeString(dt.Rows[i]["ContainerType"]);
					//if()
						string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType)
values");
					sql = "";
                    des += cntNo + " / " + cntType + "   ";
					for (int j = 0, j1 = 0; j < ChgCode_List.Length; j++)
					{
                        string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes from XXChgCode where ChgcodeDes like '{0}%'", ChgCode_List[j]);
                        if (ChgCode_List[j].Equals("TRUCKING"))
                        {
                            sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes from XXChgCode where ChgcodeDes like '{0}%'", ChgCode_List[j] + " " + cntType.Substring(0, 2));
                        }
                        DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                        string chgCodeId = "";
                        string note = "";
                        if (dt_chgCode.Rows.Count > 0)
                        {

                            chgCodeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeId"]);
                            note = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeDes"]);
                        }
                        else
                        {
                            chgCodeId = ChgCode_List[j];
                        }
						if (!ChgCode_List[j].Equals("") && !ChgCode_List[j].Equals("MISC") && !ChgCode_List[j].Equals("REMARK"))
						{
							j1++;

							decimal temp_fee = SafeValue.SafeDecimal(dt.Rows[i]["Fee" + (j + 1)]);
							if (temp_fee != 0)
							{
                                note += dt.Rows[i]["FeeNote" + (j + 1)].ToString();
								string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','{4}','Z',1,{5},'','SGD',1,0,0,{5},{5},{5},'{6}','{7}','{8}')", docId, invN, j1, ChgCode_List[j], note, temp_fee, txt_JobNo.Text, cntNo, "CTM");
								sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
							}
						}
					}

					if (sql.Length > 0)
					{
						sql = sql_part1 + sql;
						int re = ConnectSql.ExecuteSql(sql);
                        des = "Vessel/Voy:" + ves.Text + " / " + voy.Text + "\n" + "Pol/Pod:" +txt_Pol.Text + " / " + txt_Pod.Text + "\n" + "Eta:" +jobEta.Date.ToString("dd.MM.yy") + "\n" + "Container No: " + des;
						UpdateMaster(SafeValue.SafeInt(docId, 0),des);
					}
				}

				#endregion
			}
			else
			{
                string sql_id = string.Format(@"select SequenceId,DocNo from XAArInvoice where PartyTo='{0}' and MastRefNo='{1}'", btn_ClientId.Text, txt_JobNo.Text);
                DataTable dt_inv = ConnectSql.GetTab(sql_id);
                int sequenceId = 0;
                if (dt_inv.Rows.Count>0)
                {
                    sequenceId = SafeValue.SafeInt(dt_inv.Rows[0]["SequenceId"], 0);
                    invN = SafeValue.SafeString(dt_inv.Rows[0]["DocNo"]);
                }
				#region Inv Det
                for (int i = 0; i < dt.Rows.Count; i++)
				{
					string cntNo = SafeValue.SafeString(dt.Rows[i]["ContainerNo"]);
					string cntType = SafeValue.SafeString(dt.Rows[i]["ContainerType"]);

					//if()
						string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType)
values");
					sql = "";
                    des += cntNo + " / " + cntType + "   ";
					for (int j = 0, j1 = 0; j < ChgCode_List.Length; j++)
					{
                        string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes from XXChgCode where ChgcodeDes like '{0}%'", ChgCode_List[j]);
                        if (ChgCode_List[j].Equals("TRUCKING"))
                        {
                            sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes from XXChgCode where ChgcodeDes like '{0}%'", ChgCode_List[j] + " " + cntType.Substring(0, 2));
                        }
                        DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                        string chgCodeId = "";
                        string note = "";
                        if (dt_chgCode.Rows.Count > 0)
                        {

                            chgCodeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeId"]);
                            note = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeDes"]);
                        }
                        else
                        {
                            chgCodeId = ChgCode_List[j];
                        }
						sql_cnt = string.Format(@"select count(*) from XAArInvoiceDet where ChgCode='{0}' and JobRefNo='{1}'", ChgCode_List[j], cntNo);
						cnt = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql_cnt), 0);
						if (cnt == 0)
						{
							if (!ChgCode_List[j].Equals("") && !ChgCode_List[j].Equals("MISC") && !ChgCode_List[j].Equals("REMARK"))
							{
								j1++;

								decimal temp_fee = SafeValue.SafeDecimal(dt.Rows[i]["Fee" + (j + 1)]);
								if (temp_fee != 0)
								{
                                    note += dt.Rows[i]["FeeNote" + (j + 1)].ToString();
                                    string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','{4}','Z',1,{5},'','SGD',1,0,0,{5},{5},{5},'{6}','{7}','{8}')", sequenceId, invN, j1, chgCodeId, note, temp_fee, txt_JobNo.Text, cntNo, "CTM");
									sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
								}
							}
						}
					}

					if (sql.Length > 0)
					{
						sql = sql_part1 + sql;
						int re = ConnectSql.ExecuteSql(sql);
                        des = "Vessel/Voy:" + ves.Text + " / " + voy.Text + "\n" + "Pol/Pod:" + txt_Pol.Text + " / " + txt_Pod.Text + "\n" + "Eta:" + jobEta.Date.ToString("dd.MM.yy") + "\n" + "Container No: " + des;
						UpdateMaster(SafeValue.SafeInt(sequenceId, 0),des);
					}
				}

				#endregion
			}
		}
	}
	private void job_auto_billing()
	{
		ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
		//ASPxTextBox txt_JobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
		ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
		string user = HttpContext.Current.User.Identity.Name;;
		string acCode = EzshipHelper.GetAccArCode("", "SGD");
		string[] ChgCode_List = { "TRUCKING S", "WASHING Z", "CHASSIS DEMURRAGE S"
                                    , "W/HANDLING Z", "REPAIR Z", "PARKING S"
                                    , "FUEL S", "DETENTION Z", "SHIFTING S"
                                    , "DHC Z", "DEMURRAGE Z", "STAND-BY"
                                    , "PORTNET Z", "STORAGE Z", ""
                                    , "CMS S", "EXTRA ONE WAY S", ""
                                    , "OUTSIDE JURONG S", "ELECTRICITY Z", ""
                                    , "AIRPORT SURCHARGE S", "PERMIT S", "MISC"
                                    , "MCE", "EXCHANGE DO S", "REMARK" };
		string sql = string.Format(@"select * from CTM_JobDet1 where JobNo='{0}'", txt_JobNo.Text);
		DataTable dt = ConnectSql.GetTab(sql);
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			DateTime dtime = DateTime.Now;
			string invN = C2Setup.GetNextNo("", "AR-IV", dtime);
			sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo)
values('IV',getdate(),'','{0}',Year(getdate()),Month(getdate()),'CASH',getdate(),'',
'SGD','CTM',1,'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}')
select @@IDENTITY", invN, user, acCode, txt_JobNo.Text);
			string docId = ConnectSql_mb.ExecuteScalar(sql);
			C2Setup.SetNextNo("", "AR-IV", invN, dtime);
			string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo)
values");
			sql = "";
			for (int j = 0, j1 = 0; j < ChgCode_List.Length; j++)
			{
				if (!ChgCode_List[j].Equals("") && !ChgCode_List[j].Equals("MISC") && !ChgCode_List[j].Equals("REMARK"))
				{
					j1++;
					string temp_fee = dt.Rows[i]["Fee" + (j + 1)].ToString();
					string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','','Z',1,'{4}','','SGD',1,0,0,'{4}','{4}','{4}','{5}')", docId, txt_JobNo.Text, j1, ChgCode_List[j], temp_fee, invN);
					sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
				}
			}

			if (sql.Length > 0)
			{
				sql = sql_part1 + sql;
				int re = ConnectSql.ExecuteSql(sql);
			}
		}
	}
	private string job_close()
	{
		ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
		ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
		ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
		ASPxMemo memo_CLSRMK = pageControl.FindControl("memo_CLSRMK") as ASPxMemo;
		if (memo_CLSRMK.Text.Trim().Length == 0)
		{
			return "Request Remark!";
		}

		string sql = "update CTM_Job set StatusCode=case when StatusCode='CLS' then 'USE' else 'CLS' end where Id=" + Id.Text;
		if (ConnectSql.ExecuteSql(sql) > 0)
		{
			sql = string.Format(@"select StatusCode from CTM_Job where Id={0}", Id.Text);
			string status = ConnectSql.ExecuteScalar(sql).ToString();
			CtmJobEventLog log = new CtmJobEventLog();
			log.CreateDateTime = DateTime.Now;
			log.Controller = HttpContext.Current.User.Identity.Name;
            log.Platform_isWeb();
			log.JobNo = txt_JobNo.Text;
			log.JobStatus = status;
			log.Remark = memo_CLSRMK.Text;

			sql = string.Format(@"SELECT 
(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='IV') Rev1
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsAgtRate),2)),0) FROM SeaImport  WHERE (RefNo = mast.RefNo) AND (TsAgtRate > 0))*mast.ExRate as Rev2
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='DN') Rev3
,(SELECT isnull(sum(CollectAmount),0) FROM SeaImport  WHERE (RefNo = mast.RefNo))*mast.ExRate Rev4
,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = 'SI' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = 'SI' and DocType='SC')  as Cost1
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='CN') Cost2
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsImpRate),2)),0) FROM SeaImport WHERE (RefNo = mast.RefNo) AND (TsImpRate > 0))*mast.ExRate as Cost3
,( SELECT sum(SaleLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobType ='SI') Cost4
FROM SeaImportRef mast
where mast.RefNo='{0}'", txt_JobNo.Text);
			DataTable dt_PL = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
			if (dt_PL.Rows.Count == 1)
			{
				DataRow dr = dt_PL.Rows[0];
				decimal IV = SafeValue.SafeDecimal(dr["Rev1"], 0);
				decimal TsAgent = SafeValue.SafeDecimal(dr["Rev2"], 0);
				decimal DN = SafeValue.SafeDecimal(dr["Rev3"], 0);
				decimal FrCollect = SafeValue.SafeDecimal(dr["Rev4"], 0);
				decimal PLVO = SafeValue.SafeDecimal(dr["Cost1"], 0);
				decimal CN = SafeValue.SafeDecimal(dr["Cost2"], 0);
				decimal TsImp = SafeValue.SafeDecimal(dr["Cost3"], 0);
				decimal Other = SafeValue.SafeDecimal(dr["Cost4"], 0);

				log.Remark = memo_CLSRMK.Text;
				log.Value1 = IV;
				log.Value2 = TsAgent;
				log.Value3 = DN;
				log.Value4 = FrCollect;
				log.Value5 = PLVO;
				log.Value6 = CN;
				log.Value7 = TsImp;
				log.Value8 = Other;
			}

			C2.Manager.ORManager.StartTracking(log, Wilson.ORMapper.InitialState.Inserted);
			C2.Manager.ORManager.PersistChanges(log);

			return "";
		}

		return "error";
	}
	private string job_void()
	{
		ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
		string sql = "update CTM_Job set StatusCode=case when StatusCode='CNL' then 'USE' else 'CNL' end where Id=" + Id.Text;
		if (ConnectSql.ExecuteSql(sql) > 0)
		{
            string userId = HttpContext.Current.User.Identity.Name;
            int jobId = SafeValue.SafeInt(Id.Text, 0);
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;
            elog.ActionLevel_isJOB(jobId);
            elog.Remark = "Job Void";
            elog.log();
			return "";
		}

		return "error";
	}
	private string job_save()
	{
		string res = "";
		try
		{
			ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
			ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
			string jobNo = SafeValue.SafeString(txt_JobNo.Text, "");
			Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + jobNo + "'");
			C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;

			ASPxDateEdit jobDate = this.grid_job.FindEditFormTemplateControl("txt_JobDate") as ASPxDateEdit;
			bool isNew = false;
			if (ctmJob == null)
			{
				isNew = true;
				ctmJob = new C2.CtmJob();
				ctmJob.JobNo = C2Setup.GetNextNo("", "CTM_Job", jobDate.Date);
			}

			ASPxDateEdit jobEta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
			//ASPxDateEdit jobEtd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
			//ASPxDateEdit jobCod = pageControl.FindControl("date_Cod") as ASPxDateEdit;
			//ASPxButtonEdit partyId = pageControl.FindControl("btn_PartyId") as ASPxButtonEdit;
			ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
			ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
			ASPxButtonEdit carrier = pageControl.FindControl("btn_CarrierId") as ASPxButtonEdit;
			//ASPxTextBox CarrierBlNo = pageControl.FindControl("txt_CarrierBlNo") as ASPxTextBox;
			ASPxTextBox CarrierBkgNo = pageControl.FindControl("txt_CarrierBkgNo") as ASPxTextBox;
			ASPxComboBox Terminal = pageControl.FindControl("cbb_Terminal") as ASPxComboBox;
			ASPxMemo PickupFrom = pageControl.FindControl("txt_PickupFrom") as ASPxMemo;
			ASPxMemo DeliveryTo = pageControl.FindControl("txt_DeliveryTo") as ASPxMemo;
			ASPxTextBox txt_WarehouseAddress = pageControl.FindControl("txt_WarehouseAddress") as ASPxTextBox;
			//ASPxMemo txt_PortnetRef = pageControl.FindControl("txt_PortnetRef") as ASPxMemo;
			ASPxMemo txt_YardRef = pageControl.FindControl("txt_YardRef") as ASPxMemo;
			ASPxMemo Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
			ASPxMemo txt_PermitNo = pageControl.FindControl("txt_PermitNo") as ASPxMemo;
			ASPxMemo SpecialInstruction = pageControl.FindControl("txt_SpecialInstruction") as ASPxMemo;
			ASPxButtonEdit txt_Pol = pageControl.FindControl("txt_Pol") as ASPxButtonEdit;
			ASPxButtonEdit txt_Pod = pageControl.FindControl("txt_Pod") as ASPxButtonEdit;
			ASPxTextBox txt_EtaTime = pageControl.FindControl("txt_EtaTime") as ASPxTextBox;
			//ASPxTextBox txt_EtdTime = pageControl.FindControl("txt_EtdTime") as ASPxTextBox;
			//ASPxTextBox txt_CodTime = pageControl.FindControl("txt_CodTime") as ASPxTextBox;
			ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
			ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
			ASPxTextBox txt_ClientRefNo = this.grid_job.FindEditFormTemplateControl("txt_ClientRefNo") as ASPxTextBox;
            ASPxButtonEdit btn_HaulierId = pageControl.FindControl("btn_HaulierId") as ASPxButtonEdit;
			ASPxTextBox txt_OperatorCode = pageControl.FindControl("txt_OperatorCode") as ASPxTextBox;

			ASPxComboBox cmb_IsTrucking = this.grid_job.FindEditFormTemplateControl("cmb_IsTrucking") as ASPxComboBox;
			ASPxComboBox cmb_IsWarehouse = this.grid_job.FindEditFormTemplateControl("cmb_IsWarehouse") as ASPxComboBox;
			ASPxComboBox cmb_IsFreight = this.grid_job.FindEditFormTemplateControl("cmb_IsFreight") as ASPxComboBox;
			ASPxComboBox cmb_IsLocal = this.grid_job.FindEditFormTemplateControl("cmb_IsLocal") as ASPxComboBox;
			ASPxComboBox cmb_IsAdhoc = this.grid_job.FindEditFormTemplateControl("cmb_IsAdhoc") as ASPxComboBox;
			ASPxComboBox cmb_IsOthers = this.grid_job.FindEditFormTemplateControl("cmb_IsOthers") as ASPxComboBox;

            ASPxTextBox txt_notifiEmail = this.grid_job.FindEditFormTemplateControl("txt_notifiEmail") as ASPxTextBox;
            ASPxComboBox cbb_Contractor = pageControl.FindControl("cbb_Contractor") as ASPxComboBox;

            ASPxButtonEdit btn_SubClientId = this.grid_job.FindEditFormTemplateControl("btn_SubClientId") as ASPxButtonEdit;
            ASPxButtonEdit txt_WareHouseId = pageControl.FindControl("txt_WareHouseId") as ASPxButtonEdit;
            ASPxTextBox txt_wh_PermitNo = pageControl.FindControl("txt_wh_PermitNo") as ASPxTextBox;
            ASPxButtonEdit txt_IncoTerm = pageControl.FindControl("txt_IncoTerm") as ASPxButtonEdit;
            ASPxComboBox txt_PermitBy = pageControl.FindControl("txt_PermitBy") as ASPxComboBox;
            ASPxDateEdit date_PermitDate = pageControl.FindControl("date_PermitDate") as ASPxDateEdit;
            ASPxTextBox txt_PartyInvNo = pageControl.FindControl("txt_PartyInvNo") as ASPxTextBox;
            ASPxSpinEdit spin_GstAmt = pageControl.FindControl("spin_GstAmt") as ASPxSpinEdit;
            ASPxComboBox cbb_PaymentStatus = pageControl.FindControl("cbb_PaymentStatus") as ASPxComboBox;

            ASPxButtonEdit txt_ClientContact = this.grid_job.FindEditFormTemplateControl("txt_ClientContact") as ASPxButtonEdit;
            ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
            ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
            ASPxMemo txt_QuoteRemark = pageControl.FindControl("txt_QuoteRemark") as ASPxMemo;
            ASPxDateEdit date_QuoteDate = this.grid_job.FindEditFormTemplateControl("date_QuoteDate") as ASPxDateEdit;
            ASPxComboBox cbb_QuoteStatus = this.grid_job.FindEditFormTemplateControl("cbb_QuoteStatus") as ASPxComboBox;
            ASPxComboBox cbb_BillingType = this.grid_job.FindEditFormTemplateControl("cbb_BillingType") as ASPxComboBox;

            ASPxMemo txt_JobDes = pageControl.FindControl("txt_JobDes") as ASPxMemo;
            ASPxMemo txt_TerminalRemark = pageControl.FindControl("txt_TerminalRemark") as ASPxMemo;
            ASPxMemo memo_InternalRmark = pageControl.FindControl("memo_InternalRmark") as ASPxMemo;
            ASPxMemo memo_LumSumRemark = pageControl.FindControl("memo_LumSumRemark") as ASPxMemo;
            ASPxMemo memo_AdditionalRemark = pageControl.FindControl("memo_AdditionalRemark") as ASPxMemo;
            ASPxComboBox cmb_Escort_Ind = pageControl.FindControl("cmb_Escort_Ind") as ASPxComboBox;
            ASPxMemo txt_Escort_Remark = pageControl.FindControl("txt_Escort_Remark") as ASPxMemo;

            if (cmb_Escort_Ind != null)
                ctmJob.Escort_Ind = SafeValue.SafeString(cmb_Escort_Ind.Value);
            if (txt_Escort_Remark != null)
                ctmJob.Escort_Remark = SafeValue.SafeString(txt_Escort_Remark.Text);
            if (txt_ClientContact != null)
               ctmJob.ClientContact = SafeValue.SafeString(txt_ClientContact.Text);
            if (cmb_JobStatus != null)
              ctmJob.JobStatus = SafeValue.SafeString(cmb_JobStatus.Value);
            if (txt_QuoteNo != null)
              ctmJob.QuoteNo = SafeValue.SafeString(txt_QuoteNo.Text);
            if (txt_QuoteRemark != null)
              ctmJob.QuoteRemark = SafeValue.SafeString(txt_QuoteRemark.Text);
            if (date_QuoteDate != null)
               ctmJob.QuoteDate = SafeValue.SafeDate(date_QuoteDate.Date, new DateTime(1753, 1, 1));
            if (cbb_QuoteStatus != null)
               ctmJob.QuoteStatus = SafeValue.SafeString(cbb_QuoteStatus.Value);
            if (txt_JobDes != null)
                ctmJob.JobDes = SafeValue.SafeString(txt_JobDes.Text);
            if (txt_TerminalRemark != null)
                ctmJob.TerminalRemark = SafeValue.SafeString(txt_TerminalRemark.Text);
            if (memo_InternalRmark != null)
                ctmJob.InternalRemark = SafeValue.SafeString(memo_InternalRmark.Text);
            if (memo_LumSumRemark != null)
                ctmJob.LumSumRemark = SafeValue.SafeString(memo_LumSumRemark.Text);
            if (memo_AdditionalRemark != null)
                ctmJob.AdditionalRemark = SafeValue.SafeString(memo_AdditionalRemark.Text);
            if (jobDate != null)
			ctmJob.JobDate = SafeValue.SafeDate(jobDate.Date, new DateTime(1753, 1, 1));
            if (jobEta != null)
			ctmJob.EtaDate = SafeValue.SafeDate(jobEta.Date, new DateTime(1753, 1, 1));
			//ctmJob.EtdDate = SafeValue.SafeDate(jobEtd.Date, new DateTime(1753, 1, 1));
			//ctmJob.CodDate = SafeValue.SafeDate(jobCod.Date, new DateTime(1753, 1, 1));
			//ctmJob.PartyId = partyId.Text;
			ctmJob.Vessel = ves.Text;
			ctmJob.Voyage = voy.Text;
			ctmJob.CarrierId = carrier.Text;
			//ctmJob.CarrierBlNo = CarrierBlNo.Text;
			ctmJob.CarrierBkgNo = CarrierBkgNo.Text;
			ctmJob.Terminalcode = SafeValue.SafeString(Terminal.Value);
			ctmJob.PickupFrom = PickupFrom.Text;
			ctmJob.DeliveryTo = DeliveryTo.Text;
			ctmJob.WarehouseAddress = txt_WarehouseAddress.Text;
			//ctmJob.PortnetRef = txt_PortnetRef.Text;
			ctmJob.YardRef = txt_YardRef.Text;
			ctmJob.Remark = Remark.Text;
			ctmJob.PermitNo = txt_PermitNo.Text;
			ctmJob.SpecialInstruction = SpecialInstruction.Text;
			ctmJob.Pol = txt_Pol.Text;
			ctmJob.Pod = txt_Pod.Text;
			ctmJob.EtaTime = txt_EtaTime.Text;
			//ctmJob.EtdTime = txt_EtdTime.Text;
			//ctmJob.CodTime = txt_CodTime.Text;
			ctmJob.JobType = cbb_JobType.Text;
			ctmJob.ClientId = btn_ClientId.Text;
			ctmJob.ClientRefNo = txt_ClientRefNo.Text;
            ctmJob.HaulierId = btn_HaulierId.Text;
			ctmJob.OperatorCode = txt_OperatorCode.Text;

			ctmJob.IsTrucking = cmb_IsTrucking.Text;
			ctmJob.IsWarehouse = cmb_IsWarehouse.Text;
			ctmJob.IsFreight = cmb_IsFreight.Text;
			ctmJob.IsLocal = cmb_IsLocal.Text;
			ctmJob.IsAdhoc = cmb_IsAdhoc.Text;
			ctmJob.IsOthers = cmb_IsOthers.Text;
			ctmJob.EmailAddress = txt_notifiEmail.Text;
            ctmJob.Contractor = SafeValue.SafeString(cbb_Contractor.Value,"NO");
            ctmJob.SubClientId = btn_SubClientId.Text;
            if (txt_wh_PermitNo != null)
                ctmJob.WhPermitNo = SafeValue.SafeString(txt_wh_PermitNo.Text);
            if (txt_IncoTerm != null)
                ctmJob.IncoTerm = SafeValue.SafeString(txt_IncoTerm.Text);
            if (txt_PermitBy != null)
                ctmJob.PermitBy = SafeValue.SafeString(txt_PermitBy.Value);
            if (date_PermitDate != null)
                ctmJob.PermitDate = SafeValue.SafeDate(date_PermitDate.Date, new DateTime(1753, 1, 1));
            if (txt_PartyInvNo != null)
                ctmJob.PartyInvNo = SafeValue.SafeString(txt_PartyInvNo.Text);
            if (spin_GstAmt != null)
                ctmJob.GstAmt = SafeValue.SafeDecimal(spin_GstAmt.Value);
            if (cbb_PaymentStatus != null)
                ctmJob.PaymentStatus = SafeValue.SafeString(cbb_PaymentStatus.Value);
            if (txt_WareHouseId != null)
                ctmJob.WareHouseCode = txt_WareHouseId.Text;
            if (cbb_BillingType != null)
                ctmJob.BillingType = SafeValue.SafeString(cbb_BillingType.Value);

            ASPxTextBox txt_MasterJobNo = this.grid_job.FindEditFormTemplateControl("txt_MasterJobNo") as ASPxTextBox;
            ctmJob.MasterJobNo = txt_MasterJobNo.Text;
   

			string userId = HttpContext.Current.User.Identity.Name;
			if (isNew)
			{
				ctmJob.StatusCode = "USE";

				ctmJob.CreateBy = userId;
				ctmJob.CreateDateTime = DateTime.Now;
				ctmJob.UpdateBy = userId;
				ctmJob.UpdateDateTime = DateTime.Now;
				C2.Manager.ORManager.StartTracking(ctmJob, Wilson.ORMapper.InitialState.Inserted);
				C2.Manager.ORManager.PersistChanges(ctmJob);
			}
			else
			{
				ctmJob.UpdateBy = userId;
				ctmJob.UpdateDateTime = DateTime.Now;
                if (ctmJob.JobNo.Length == 0)
                    ctmJob.JobNo = ctmJob.Id.ToString();
				C2.Manager.ORManager.StartTracking(ctmJob, Wilson.ORMapper.InitialState.Updated);
				C2.Manager.ORManager.PersistChanges(ctmJob);

                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isWeb();
                elog.Controller = userId;
                elog.ActionLevel_isJOB(ctmJob.Id);
                elog.Remark = "Job Update";
                elog.log();
			}

			if (isNew)
			{
				txt_JobNo.Text = ctmJob.JobNo;
				//txt_search_JobNo.Text = txt_JobNo.Text;
				C2Setup.SetNextNo("", "CTM_Job", ctmJob.JobNo, jobDate.Date);
			}

			res = Job_Check_JobLevel(ctmJob.JobNo);

			Session["CTM_Job_" + txt_search_JobNo.Text] = "Id='" + ctmJob.Id + "'";
			this.dsJob.FilterExpression = Session["CTM_Job_" + txt_search_JobNo.Text].ToString();
			if (this.grid_job.GetRow(0) != null)
				this.grid_job.StartEdit(0);
		}
		catch { }

		return res;
	}
	public string Job_Check_JobLevel(string JobNo)
	{
		string res = "";
		string sql = string.Format(@"select JobNo,JobType,CarrierBkgNo,EtaDate from ctm_job where jobno='{0}'", JobNo);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		if (dt.Rows.Count > 0)
		{
			string JobType = dt.Rows[0]["JobType"].ToString();
			string refNo = dt.Rows[0]["CarrierBkgNo"].ToString();
			string Eta = SafeValue.SafeDate(dt.Rows[0]["EtaDate"], new DateTime(1900, 1, 1)).ToString("yyyyMMdd");
			if (JobType.Equals("EXP") && refNo.Length > 0)
			{
				sql = string.Format(@"select Id from ctm_job where CarrierBkgNo=@CarrierBkgNo");
				List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
				list.Add(new ConnectSql_mb.cmdParameters("@CarrierBkgNo", refNo, SqlDbType.NVarChar, 100));
				dt = ConnectSql_mb.GetDataTable(sql, list);
				if (dt.Rows.Count > 1)
				{
					res = "This Export Job RefNo exist " + dt.Rows.Count + " line";
				}
			}
		}

		return res;
	}
	public string Job_Check_ContLevel(string ContId)
	{
		string res = "";
		string sql = string.Format(@"select det1.ContainerNo,job.EtaDate,job.JobType from ctm_jobdet1 as det1
left outer join ctm_job as job on det1.JobNo=job.JobNo
where det1.Id={0}", ContId);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		if (dt.Rows.Count > 0)
		{
			string JobType = dt.Rows[0]["JobType"].ToString();
			string ContainerNo = dt.Rows[0]["ContainerNo"].ToString();
			string Eta = SafeValue.SafeDate(dt.Rows[0]["EtaDate"], new DateTime(1900, 1, 1)).ToString("yyyyMMdd");
			if (JobType.Equals("IMP"))
			{
				sql = string.Format(@"select * from CTM_JobDet1 as det1
left outer join ctm_job as job on job.jobno=det1.jobno
where det1.ContainerNo=@ContainerNo and datediff(day,job.EtaDate,@EtaDate)=0");
				List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
				list.Add(new ConnectSql_mb.cmdParameters("@EtaDate", Eta, SqlDbType.NVarChar, 100));
				list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
				dt = ConnectSql_mb.GetDataTable(sql, list);
				if (dt.Rows.Count > 1)
				{
					res = string.Format(@"[{0}] Container in {1} exist {2} line.", ContainerNo, SafeValue.SafeDate(dt.Rows[0]["EtaDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy"), dt.Rows.Count);
				}
			}
		}

		return res;
	}
    private string add_wh(string refType, string type, string doType, string jobNo, string whType)
	{
		string doNo = "";

		try
		{
			ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
			ASPxDateEdit jobDate = this.grid_job.FindEditFormTemplateControl("txt_JobDate") as ASPxDateEdit;
			WhDo whDo = new WhDo();
			doNo = C2Setup.GetNextNo("", refType, jobDate.Date);

			ASPxDateEdit jobEta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
			//ASPxDateEdit jobEtd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
			//ASPxDateEdit jobCod = pageControl.FindControl("date_Cod") as ASPxDateEdit;
			//ASPxButtonEdit partyId = pageControl.FindControl("btn_PartyId") as ASPxButtonEdit;
			ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
			ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
			ASPxButtonEdit carrier = pageControl.FindControl("btn_CarrierId") as ASPxButtonEdit;
			ASPxTextBox CarrierBkgNo = pageControl.FindControl("txt_CarrierBkgNo") as ASPxTextBox;
			ASPxComboBox Terminal = pageControl.FindControl("cbb_Terminal") as ASPxComboBox;
			ASPxMemo PickupFrom = pageControl.FindControl("txt_PickupFrom") as ASPxMemo;
			ASPxMemo DeliveryTo = pageControl.FindControl("txt_DeliveryTo") as ASPxMemo;
			ASPxTextBox txt_WarehouseAddress = pageControl.FindControl("txt_WarehouseAddress") as ASPxTextBox;
			ASPxMemo txt_YardRef = pageControl.FindControl("txt_YardRef") as ASPxMemo;
			ASPxMemo Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
			ASPxMemo txt_PermitNo = pageControl.FindControl("txt_PermitNo") as ASPxMemo;
			ASPxMemo SpecialInstruction = pageControl.FindControl("txt_SpecialInstruction") as ASPxMemo;

			ASPxTextBox txt_EtaTime = pageControl.FindControl("txt_EtaTime") as ASPxTextBox;
			ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
			ASPxTextBox txt_ClientName = this.grid_job.FindEditFormTemplateControl("txt_ClientName") as ASPxTextBox;
			ASPxTextBox txt_ClientRefNo = this.grid_job.FindEditFormTemplateControl("txt_ClientRefNo") as ASPxTextBox;
			ASPxTextBox txt_OperatorCode = pageControl.FindControl("txt_OperatorCode") as ASPxTextBox;

			whDo.DoDate = SafeValue.SafeDate(jobDate.Date, new DateTime(1753, 1, 1));
			whDo.Eta = SafeValue.SafeDate(jobEta.Date, new DateTime(1753, 1, 1));

			whDo.Vessel = ves.Text;
			whDo.Voyage = voy.Text;

			whDo.CollectFrom = PickupFrom.Text;
			whDo.DeliveryTo = DeliveryTo.Text;
			whDo.WareHouseId = txt_WarehouseAddress.Text;

			whDo.Remark = Remark.Text;
			whDo.PermitNo = txt_PermitNo.Text;

			whDo.Priority = type;
			whDo.PartyId = btn_ClientId.Text;
			whDo.PartyName = txt_ClientName.Text;
			whDo.CustomerReference = txt_ClientRefNo.Text;
			whDo.ContainerYard = txt_YardRef.Text;

			whDo.DoType = doType;
			whDo.StatusCode = "USE";
			whDo.CreateBy = EzshipHelper.GetUserName();
			whDo.CreateDateTime = DateTime.Now;
			whDo.DoNo = doNo;
            whDo.JobNo = jobNo;
			C2Setup.SetNextNo("", refType, doNo, jobDate.Date);
			Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
			Manager.ORManager.PersistChanges(whDo);
		}
		catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
		return doNo;
	}
	private void container_add(string doNo, string doType)
	{
		ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
		ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
		ASPxDateEdit jobDate = this.grid_job.FindEditFormTemplateControl("txt_JobDate") as ASPxDateEdit;
		string sql = string.Format(@"select count(*) from Ctm_JobDet1 where JobNo='{0}'", txt_JobNo.Text);
		int n1 = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
		sql = string.Format(@"select count(*) from Wh_DoDet3 where DoNo='{0}'", doNo);
		int n2 = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
		if (n1 > n2)
		{
			sql = string.Format(@"select * from Ctm_JobDet1 where JobNo='{0}'", txt_JobNo.Text);
			DataTable tab = ConnectSql.GetTab(sql);
			string userId = HttpContext.Current.User.Identity.Name;
			if (tab.Rows.Count > 0)
			{
				for (int i = 0; i < tab.Rows.Count; i++)
				{
					decimal weight = SafeValue.SafeDecimal(tab.Rows[i]["Weight"], 0);
					decimal m3 = SafeValue.SafeDecimal(tab.Rows[i]["Volume"], 0);;
					int qty = SafeValue.SafeInt(tab.Rows[i]["QTY"], 0);
					string containerNo = SafeValue.SafeString(tab.Rows[i]["ContainerNo"]);
					string sealNo = SafeValue.SafeString(tab.Rows[i]["SealNo"]);
					string pkgType = SafeValue.SafeString(tab.Rows[i]["PackageType"]);
					string containerType = SafeValue.SafeString(tab.Rows[i]["ContainerType"]);

					sql = string.Format(@"select count(*) from Wh_DoDet3 where DoNo='{0}' and ContainerNo='{1}'", doNo, containerNo);
					int result = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
					if (result == 0)
					{
						sql = string.Format(@"insert into Wh_DoDet3(DoNo,DoType,Weight,M3,Qty,ContainerNo,SealNo,PkgType,ContainerType,CreateBy,CreateDateTime,UpdateBy,UpdateDateTime,JobStart) 
Values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}',getdate(),'{9}',getdate(),'{10}')", doNo, doType, weight, m3, qty, containerNo, sealNo, pkgType, containerType, userId, jobDate.Date);
						ConnectSql.ExecuteSql(sql);
					}
				}
			}
		}
	}
	private void UpdateMaster(int docId,string des)
	{
		string sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId='{0}'", docId);
		C2.Manager.ORManager.ExecuteCommand(sql);
		decimal docAmt = 0;
		decimal locAmt = 0;
		sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAArInvoiceDet where DocId='{0}'", docId);
		DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
		for (int i = 0; i < tab.Rows.Count; i++)
		{
			if (tab.Rows[i]["AcSource"].ToString() == "CR")
			{
				docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
				locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
			}
			else
			{
				docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
				locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
			}
		}

		decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

		balAmt += SafeValue.SafeDecimal(Manager.ORManager.GetDataSet(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}',Description='{4}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId,des);
		C2.Manager.ORManager.ExecuteCommand(sql);
	}
	protected void grid_job_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
	{
		string s = e.Parameters;
		if (s == "save")
		{
			job_save();
		}
	}
	protected void grid_job_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
	{
		if (this.grid_job.EditingRowVisibleIndex > -1)
		{
			ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
			//ASPxTextBox partyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
			ASPxTextBox txt_CarrierName = pageControl.FindControl("txt_CarrierName") as ASPxTextBox;
			//ASPxTextBox txt_ClientName = pageControl.FindControl("txt_ClientName") as ASPxTextBox;
			ASPxTextBox txt_ClientName = this.grid_job.FindEditFormTemplateControl("txt_ClientName") as ASPxTextBox;
			ASPxTextBox txt_HaulierName = pageControl.FindControl("txt_HaulierName") as ASPxTextBox;

			//partyName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "PartyId" }));
			txt_CarrierName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "CarrierId" }));
			txt_ClientName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "ClientId" }));
			txt_HaulierName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "HaulierId" }));
            ASPxLabel lbl_BillingAlert = this.grid_job.FindEditFormTemplateControl("lbl_BillingAlert") as ASPxLabel;
            string clientId = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "ClientId" }));
            string sql_b = string.Format(@"select BillingAlert from XXParty where PartyId='{0}'", clientId);
            string billingAlert = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql_b));
            lbl_BillingAlert.Text = billingAlert;
            ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
            ASPxButton btn_Confirm = this.grid_job.FindEditFormTemplateControl("btn_Confirm") as ASPxButton;
            ASPxButton btn_QuoteVoid = this.grid_job.FindEditFormTemplateControl("btn_QuoteVoid") as ASPxButton;
			string StatusCode = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "StatusCode" }), "USE");
			switch (StatusCode)
			{
				case "CLS":
				//ASPxButton btnCLS = this.grid_job.FindEditFormTemplateControl("btn_JobClose") as ASPxButton;
				ASPxPageControl tabs = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
				ASPxButton btnCLS = tabs.FindControl("btn_JobClose") as ASPxButton;
				btnCLS.Text = "Open";
				break;
				case "CNL":
                ASPxButton btnCNL = pageControl.FindControl("btn_JobVoid") as ASPxButton;
				btnCNL.Text = "UnVoid";
				break;
				default:
                    break;
			}

			EzshipHelper_Authority.Bind_Authority(this.grid_job);
			EzshipHelper_Authority.Bind_Authority(pageControl);

            string jobType = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "JobType" }), "GR");
            string jobStatus = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "JobStatus" }));
            string quoteStatus = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "QuoteStatus" }), "None");

            ASPxPageControl tab = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            TabPage container = tab.TabPages.FindByName("Container") as TabPage;
            TabPage transport = tab.TabPages.FindByName("Transport") as TabPage;
            TabPage crane = tab.TabPages.FindByName("Crane") as TabPage;
            
            TabPage wh = tab.TabPages.FindByName("Cargo") as TabPage;
            TabPage activity = tab.TabPages.FindByName("Activity") as TabPage;
            TabPage attch = tab.TabPages.FindByName("Attachments") as TabPage;
            TabPage close = tab.TabPages.FindByName("Close File") as TabPage;
            TabPage billing = tab.TabPages.FindByName("Billing") as TabPage;
            TabPage quotation = tab.TabPages.FindByName("Quotation") as TabPage;
            if (jobType == "TP" && wh != null)
            {
                wh.Visible = false;
            }
            if (jobStatus == "Quoted")
            {
                container.Visible = false;
                transport.Visible = false;
                crane.Visible = false;
                wh.Visible = false;
                attch.Visible = false;
                close.Visible = false;
                billing.Visible = false;
                activity.Visible = false;
            }
            else
            {
                if (jobStatus == "Confirmed")
                {
                    cmb_JobStatus.Value = "Confirmed";
                    btn_Confirm.Text = "Re-Quote";
                }
                if (jobStatus == "Completed")
                {
                    cmb_JobStatus.Value = "Completed";
                    btn_Confirm.Text = "Re-Quote";
                }
                if (jobStatus == "Voided")
                {
                    cmb_JobStatus.Value = "Voided";
                    btn_QuoteVoid.Text = "UnVoid";
                    btn_Confirm.Text = "Re-Quote";
                }
                container.Visible = true;
                transport.Visible = true;
                crane.Visible = true;
                wh.Visible = true;
                attch.Visible = true;
                close.Visible = true;
                billing.Visible = true;
            }
            if (quoteStatus == "None")
            {
                quotation.Visible = false;
            }
            else
            {
                #region Email
                string partyTo = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "ClientId" }), "IMP");
                ASPxComboBox cbb_Email1 = pageControl.FindControl("cbb_Email1") as ASPxComboBox;
                string sql = string.Format(@"select Email1,Email2 from XXParty where PartyId='{0}'", partyTo);
                DataTable dt = ConnectSql.GetTab(sql);
                if (dt.Rows.Count > 0)
                {
                    var email1 = SafeValue.SafeString(dt.Rows[0]["Email1"]);
                    var email2 = SafeValue.SafeString(dt.Rows[0]["Email2"]);
                    string[] email_List = { email1, email2 };
                    for (int i = 0; i < email_List.Length; i++)
                    {
                        //if (email_List[i] != "")
                        //{
                        ListEditItem item = new ListEditItem();
                        item.Value = email_List[i];
                        item.Text = email_List[i];
                        cbb_Email1.Items.Insert(i, item);
                        //}
                    }
                }
                #endregion
            }
		}
	}
    protected void cbb_QuoteStatus_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cbb_QuoteStatus") as ASPxComboBox;
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        string sql = string.Format(@"select QuoteStatus from ctm_job where Id='{0}'", Id.Text);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        ASPxPageControl tab = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        TabPage quotation = tab.TabPages.FindByName("Quotation") as TabPage;
        if (status == "None")
        {
            cmb_JobStatus.Text = "None";
            if (quotation != null)
            {
                quotation.Visible = false;
            }
        }
        else
        {
            if (quotation != null)
            {
                quotation.Visible = true;
            }
        }
        if (status == "Pending")
        {
            cmb_JobStatus.Text = "Pending";
        }
        if (status == "Quoted")
        {
            cmb_JobStatus.Text = "Quoted";
        }
        if (status == "Closed")
        {
            cmb_JobStatus.Text = "Closed";
        }
        if (status == "Failed")
        {
            cmb_JobStatus.Text = "Failed";
        }
        if (status == "Voided")
        {
            cmb_JobStatus.Text = "Voided";
        }
    }
    protected void cmb_JobStatus_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        string sql = string.Format(@"select JobStatus from ctm_job where Id='{0}'", Id.Text);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        if (status == "Quoted")
        {
            cmb_JobStatus.Text = "Quoted";
        }
        if (status == "Confirmed")
        {
            cmb_JobStatus.Text = "Confirmed";
        }
        if (status == "Completed")
        {
            cmb_JobStatus.Text = "Completed";
        }
        if (status == "Voided")
        {
            cmb_JobStatus.Text = "Voided";
        }
    }
    protected void cmb_IsTrucking_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_IsTrucking = this.grid_job.FindEditFormTemplateControl("cmb_IsTrucking") as ASPxComboBox;
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
        string sql = string.Format(@"select IsTrucking from ctm_job where Id='{0}'", Id.Text);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "No");
        ASPxPageControl tab = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        TabPage tab_page = tab.TabPages.FindByName("Container") as TabPage;
        if (status == "No" && tab_page != null)
        {
            tab_page.Visible = false;

        }
        else
        {
            if (cmb_JobStatus.Text != "Quoted")
            {
                tab_page.Visible = true;
            }
        }
        cmb_IsTrucking.Text = status;
    }
    protected void cmb_IsWarehouse_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_IsWarehouse = this.grid_job.FindEditFormTemplateControl("cmb_IsWarehouse") as ASPxComboBox;
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        string sql = string.Format(@"select IsWarehouse from ctm_job where Id='{0}'", Id.Text);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "No");
        ASPxPageControl tab = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        TabPage tab_page = tab.TabPages.FindByName("Warehouse") as TabPage;

        cmb_IsWarehouse.Text = status;
    }
    protected void cmb_IsLocal_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_IsLocal = this.grid_job.FindEditFormTemplateControl("cmb_IsLocal") as ASPxComboBox;
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
        string sql = string.Format(@"select IsLocal from ctm_job where Id='{0}'", Id.Text);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "No");
        ASPxPageControl tab = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        TabPage tab_page = tab.TabPages.FindByName("Transport") as TabPage;
        if (status == "No" && tab_page != null)
        {
            tab_page.Visible = false;
        }
        else
        {
            if (cmb_JobStatus.Text != "Quoted")
            {
                tab_page.Visible = true;
            }
        }
        cmb_IsLocal.Text = status;
    }
    protected void cmb_IsAdhoc_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_IsAdhoc = this.grid_job.FindEditFormTemplateControl("cmb_IsAdhoc") as ASPxComboBox;
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
        string sql = string.Format(@"select IsAdhoc from ctm_job where Id='{0}'", Id.Text);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "No");
        ASPxPageControl tab = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        TabPage tab_page = tab.TabPages.FindByName("Crane") as TabPage;
        if (status == "No" && tab_page != null)
        {
            tab_page.Visible = false;
        }
        else
        {
            if (cmb_JobStatus.Text != "Quoted")
            {
                tab_page.Visible = true;
            }
        }
        cmb_IsAdhoc.Text = status;
    }
    #endregion

    #region Cont
    protected void grid_Cont_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobDet1));
        }
    }
    protected void grid_Cont_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsCont.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_Cont_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Cont_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxMemo txt_YardRef = pageControl.FindControl("txt_YardRef") as ASPxMemo;
        e.NewValues["RequestDate"] = DateTime.Now;
        e.NewValues["ScheduleDate"] = DateTime.Now;
        e.NewValues["ScheduleStartDate"] = DateTime.Now;
        e.NewValues["CfsInDate"] = DateTime.Now;
        e.NewValues["CfsOutDate"] = DateTime.Now;
        e.NewValues["YardPickupDate"] = DateTime.Now;
        e.NewValues["YardReturnDate"] = DateTime.Now;
        e.NewValues["CdtDate"] = DateTime.Now;
        e.NewValues["YardExpiryDate"] = DateTime.Now;
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["UrgentInd"] = "N";
        e.NewValues["F5Ind"] = "N";
        e.NewValues["StatusCode"] = "New";
        e.NewValues["Permit"] = "N";
        e.NewValues["YardAddress"] = txt_YardRef.Text;
        e.NewValues["EmailInd"] = "N";
        e.NewValues["ContainerCategory"] = "Normal";
        e.NewValues["CfsStatus"] = "Pending";
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        string partyId = SafeValue.SafeString(btn_ClientId.Text);
        string sql = string.Format(@"select * from MastertRate where CustomerId='{0}'", partyId);
        DataTable tab = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            string code = SafeValue.SafeString(tab.Rows[i]["Description"]);
            decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"]);
            if (code.Length > 0)
            {
                if (code.ToUpper().Equals("TRUCKING"))
                {
                    e.NewValues["Fee1"] = price;
                }
                if (code.ToUpper().Equals("FUEL"))
                {
                    e.NewValues["Fee2"] = price;
                }
                if (code.ToUpper().Equals("DHC"))
                {
                    e.NewValues["Fee3"] = price;
                }
                if (code.ToUpper().Equals("PORTNET"))
                {
                    e.NewValues["Fee4"] = price;
                }
                if (code.ToUpper() == "CMS")
                {
                    e.NewValues["Fee5"] = price;
                }
                if (code.ToUpper() == "PSA LOLO")
                {
                    e.NewValues["Fee6"] = price;
                }
            }
        }
    }
    protected void grid_Cont_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["F5Ind"] = SafeValue.SafeString(e.NewValues["F5Ind"], "");
        e.NewValues["UrgentInd"] = SafeValue.SafeString(e.NewValues["UrgentInd"], "");
        e.NewValues["PortnetStatus"] = SafeValue.SafeString(e.NewValues["PortnetStatus"], "");
        e.NewValues["ScheduleDate"] = SafeValue.SafeDate(e.NewValues["ScheduleDate"], new DateTime(1753, 1, 1));
        e.NewValues["RequestDate"] = SafeValue.SafeDate(e.NewValues["RequestDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsInDate"] = SafeValue.SafeDate(e.NewValues["CfsInDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsOutDate"] = SafeValue.SafeDate(e.NewValues["CfsOutDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardPickupDate"] = SafeValue.SafeDate(e.NewValues["YardPickupDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardReturnDate"] = SafeValue.SafeDate(e.NewValues["YardReturnDate"], new DateTime(1753, 1, 1));
        e.NewValues["CdtDate"] = SafeValue.SafeDate(e.NewValues["CdtDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardExpiryDate"] = SafeValue.SafeDate(e.NewValues["YardExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"], "");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "");
    }
    protected void grid_Cont_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["F5Ind"] = SafeValue.SafeString(e.NewValues["F5Ind"], "");
        e.NewValues["UrgentInd"] = SafeValue.SafeString(e.NewValues["UrgentInd"], "");
        e.NewValues["PortnetStatus"] = SafeValue.SafeString(e.NewValues["PortnetStatus"], "");
        e.NewValues["ScheduleDate"] = SafeValue.SafeDate(e.NewValues["ScheduleDate"], new DateTime(1753, 1, 1));
        e.NewValues["RequestDate"] = SafeValue.SafeDate(e.NewValues["RequestDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsInDate"] = SafeValue.SafeDate(e.NewValues["CfsInDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsOutDate"] = SafeValue.SafeDate(e.NewValues["CfsOutDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardPickupDate"] = SafeValue.SafeDate(e.NewValues["YardPickupDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardReturnDate"] = SafeValue.SafeDate(e.NewValues["YardReturnDate"], new DateTime(1753, 1, 1));
        e.NewValues["CdtDate"] = SafeValue.SafeDate(e.NewValues["CdtDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardExpiryDate"] = SafeValue.SafeDate(e.NewValues["YardExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"], "");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "");

    }
    protected void grid_Cont_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
        string sql = string.Format(@"delete from CTM_JobDet2 where Det1Id={0}", e.Values["Id"]);
        ConnectSql.ExecuteSql(sql);
    }
    protected void grid_Cont_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        //Trip_new_auto(jobNo.Text);
    }
    protected void grid_Cont_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
        if (SafeValue.SafeString(e.NewValues["ContainerNo"]) != SafeValue.SafeString(e.OldValues["ContainerNo"]))
        {
            string sql = string.Format("Update ctm_JobDet2 set ContainerNo='{1}' where Det1Id='{0}'", e.Keys["Id"], SafeValue.SafeString(e.NewValues["ContainerNo"]));
            ConnectSql.ExecuteSql(sql);
        }
        if (SafeValue.SafeString(e.NewValues["StatusCode"], "") == "Completed")
        {
            int contId = SafeValue.SafeInt(e.Keys["Id"], 0);
            C2.CtmJobDet1.contTruckingStatusChanged(SafeValue.SafeInt(contId, 0));
        }
    }
    private void Trip_new_auto(string JobNo)
    {
        string sql = string.Format(@"select * From ctm_job where JobNo='{0}'", JobNo);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        DataRow dr_job = dt.Rows[0];
        sql = string.Format(@"with tb1 as (
select * from ctm_jobdet1 where jobno='{0}'
),
tb2 as (
select * from ctm_jobdet2 where jobno='{0}'
)
select Id,ContainerNo from (
select *,(select count(*) from tb2 where tb1.Id=tb2.Det1Id) as tripCount from tb1 
) as tb where tripCount=0", JobNo);
        dt = ConnectSql_mb.GetDataTable(sql);
        sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance) values");
        string values = "";
        //string tripcode1 = "EMPTY", tripcode2 = "LADEN";
        //string stage_from1 = "Yard", stage_from2 = "Warehouse";
        //if (dr_job["JobType"].ToString().IndexOf("IMP") > -1)
        //{
        //    tripcode1 = "LADEN";
        //    tripcode2 = "EMPTY";
        //    stage_from1 = "Port";
        //}
        string JobType = "IMP";
        switch (dr_job["JobType"].ToString())
        {
            case "IMP":
                break;
            case "EXP":
                JobType = "EXP";
                break;
            case "LOC":
                JobType = "LOC";
                break;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //values += (values.Length > 0 ? "," : "") + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','B1','N','{7}','Pending','{5}','Normal','N'),('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','B1','N','{8}','Pending','{6}','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], dr_job["PickupFrom"], dr_job["DeliveryTo"], dt.Rows[i]["Id"], tripcode1, tripcode2, stage_from1, stage_from2);
            //values += "," + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','','N','','','','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"]);
            //values += "," + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','','N','','','','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"]);
            values += (values.Length > 0 ? "," : "") + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{6}','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"], "Port", JobType);
            values += "," + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{6}','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"], "Warehouse", JobType);
            values += "," + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{6}','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"], "Yard", JobType);
        }

        if (values.Length > 0)
        {
            sql = sql + values;
            try
            {
                int i = ConnectSql.ExecuteSql(sql);
            }
            catch { }
        }
    }
    protected void btn_cont_auto_invoice_Click(object sender, EventArgs e)
    {
    }
    protected void grid_Cont_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxLabel lbl_JobType = this.grid_job.FindEditFormTemplateControl("lbl_JobType") as ASPxLabel;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string s = e.Parameters;
        if (s == "save")
        {
            ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
            ASPxTextBox txt_Id = grd.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            ASPxButtonEdit btn_ContNo = grd.FindEditFormTemplateControl("btn_ContNo") as ASPxButtonEdit;
            ASPxTextBox txt_SealNo = grd.FindEditFormTemplateControl("txt_SealNo") as ASPxTextBox;
            ASPxComboBox cbbContType = grd.FindEditFormTemplateControl("cbbContType") as ASPxComboBox;

            ASPxSpinEdit spin_Wt = grd.FindEditFormTemplateControl("spin_Wt") as ASPxSpinEdit;
            ASPxSpinEdit spin_M3 = grd.FindEditFormTemplateControl("spin_M3") as ASPxSpinEdit;
            ASPxSpinEdit spin_Pkgs = grd.FindEditFormTemplateControl("spin_Pkgs") as ASPxSpinEdit;
            ASPxButtonEdit txt_PkgsType = grd.FindEditFormTemplateControl("txt_PkgsType") as ASPxButtonEdit;

            //ASPxDateEdit date_Cont_Request = grd.FindEditFormTemplateControl("date_Cont_Request") as ASPxDateEdit;
            ASPxDateEdit date_Cont_Schedule = grd.FindEditFormTemplateControl("date_Cont_Schedule") as ASPxDateEdit;
            ASPxTextBox txt_DgClass = grd.FindEditFormTemplateControl("txt_DgClass") as ASPxTextBox;

            //ASPxDateEdit date_Cont_CfsIn = grd.FindEditFormTemplateControl("date_Cont_CfsIn") as ASPxDateEdit;
            //ASPxDateEdit date_Cont_CfsOut = grd.FindEditFormTemplateControl("date_Cont_CfsOut") as ASPxDateEdit;
            ASPxComboBox ASPxComboBox1 = grd.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;

            //ASPxDateEdit date_Cont_YardPickup = grd.FindEditFormTemplateControl("date_Cont_YardPickup") as ASPxDateEdit;
            //ASPxDateEdit date_Cont_YardReturn = grd.FindEditFormTemplateControl("date_Cont_YardReturn") as ASPxDateEdit;
            ASPxComboBox cbb_StatusCode = grd.FindEditFormTemplateControl("cbb_StatusCode") as ASPxComboBox;
            ASPxComboBox cbb_F5Ind = grd.FindEditFormTemplateControl("cbb_F5Ind") as ASPxComboBox;
            ASPxComboBox cbb_UrgentInd = grd.FindEditFormTemplateControl("cbb_UrgentInd") as ASPxComboBox;
            ASPxComboBox cbb_EmailInd = grd.FindEditFormTemplateControl("cbb_EmailInd") as ASPxComboBox;

            //ASPxDateEdit date_Cdt = grd.FindEditFormTemplateControl("date_Cdt") as ASPxDateEdit;
            //ASPxTextBox txt_CdtTime = grd.FindEditFormTemplateControl("txt_CdtTime") as ASPxTextBox;
            //ASPxDateEdit date_YardExpiry = grd.FindEditFormTemplateControl("date_YardExpiry") as ASPxDateEdit;
            //ASPxTextBox txt_YardExpiryTime = grd.FindEditFormTemplateControl("txt_YardExpiryTime") as ASPxTextBox;

            ASPxTextBox txt_TerminalLocation = grd.FindEditFormTemplateControl("txt_TerminalLocation") as ASPxTextBox;
            ASPxMemo txt_YardAddress = grd.FindEditFormTemplateControl("txt_YardAddress") as ASPxMemo;
            ASPxMemo txt_ContRemark = grd.FindEditFormTemplateControl("txt_ContRemark") as ASPxMemo;
            //ASPxTextBox txt_Remark1 = grd.FindEditFormTemplateControl("txt_Remark1") as ASPxTextBox;
            ASPxComboBox cbb_Permit = grd.FindEditFormTemplateControl("cbb_Permit") as ASPxComboBox;

            ASPxComboBox cbb_warehouse_status = grd.FindEditFormTemplateControl("cbb_warehouse_status") as ASPxComboBox;
            ASPxTextBox txt_TTTime = grd.FindEditFormTemplateControl("txt_TTTime") as ASPxTextBox;
            ASPxTextBox txt_BR = grd.FindEditFormTemplateControl("txt_BR") as ASPxTextBox;
            ASPxComboBox cbb_CfsStatus = grd.FindEditFormTemplateControl("cbb_CfsStatus") as ASPxComboBox;
            ASPxDateEdit date_ScheduleStartDate = grd.FindEditFormTemplateControl("date_ScheduleStartDate") as ASPxDateEdit;
            ASPxTextBox date_ScheduleStartTime = grd.FindEditFormTemplateControl("date_ScheduleStartTime") as ASPxTextBox;
            ASPxComboBox cbb_oogInd = grd.FindEditFormTemplateControl("cbb_oogInd") as ASPxComboBox;
            ASPxTextBox txt_dischargeCell = grd.FindEditFormTemplateControl("txt_dischargeCell") as ASPxTextBox;
            ASPxDateEdit date_CompletionDate = grd.FindEditFormTemplateControl("date_CompletionDate") as ASPxDateEdit;
            ASPxTextBox txt_CompletionTime = grd.FindEditFormTemplateControl("txt_CompletionTime") as ASPxTextBox;
            ASPxComboBox cbb_ContainerCategory = grd.FindEditFormTemplateControl("cbb_ContainerCategory") as ASPxComboBox;
            int Id = SafeValue.SafeInt(txt_Id.Text, 0);
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet1), "Id='" + Id + "'");
            C2.CtmJobDet1 cont = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet1;
            bool isNew = false;
            if (cont == null)
            {
                isNew = true;
                cont = new C2.CtmJobDet1();
            }

            string old_containerno = cont.ContainerNo;
            cont.JobNo = jobNo.Text;
            cont.ContainerNo = SafeValue.SafeString(btn_ContNo.Text);
            cont.SealNo = SafeValue.SafeString(txt_SealNo.Text);
            cont.ContainerType = SafeValue.SafeString(cbbContType.Value);
            cont.Weight = SafeValue.SafeDecimal(spin_Wt.Text);
            cont.Volume = SafeValue.SafeDecimal(spin_M3.Text);
            cont.Qty = SafeValue.SafeInt(spin_Pkgs.Text, 0);
            cont.PackageType = SafeValue.SafeString(txt_PkgsType.Text);

            //cont.RequestDate = SafeValue.SafeDate(date_Cont_Request.Date, new DateTime(1990, 1, 1));
            cont.ScheduleDate = SafeValue.SafeDate(date_Cont_Schedule.Date, new DateTime(1990, 1, 1));
            cont.DgClass = SafeValue.SafeString(txt_DgClass.Text);
            //cont.CfsInDate = SafeValue.SafeDate(date_Cont_CfsIn.Date, new DateTime(1990, 1, 1));
            //cont.CfsOutDate = SafeValue.SafeDate(date_Cont_CfsOut.Date, new DateTime(1990, 1, 1));
            cont.PortnetStatus = SafeValue.SafeString(ASPxComboBox1.Value);

            //cont.YardPickupDate = SafeValue.SafeDate(date_Cont_YardPickup.Date, new DateTime(1990, 1, 1));
            //cont.YardReturnDate = SafeValue.SafeDate(date_Cont_YardReturn.Date, new DateTime(1990, 1, 1));

            cont.StatusCode = SafeValue.SafeString(cbb_StatusCode.Value);



            cont.F5Ind = SafeValue.SafeString(cbb_F5Ind.Value, "N");
            cont.UrgentInd = SafeValue.SafeString(cbb_UrgentInd.Value);
            cont.EmailInd = SafeValue.SafeString(cbb_EmailInd.Value, "N");

            //cont.CdtDate = SafeValue.SafeDate(date_Cdt.Date, new DateTime(1990, 1, 1));
            //cont.YardExpiryDate = SafeValue.SafeDate(date_YardExpiry.Date, new DateTime(1990, 1, 1));
            //cont.CdtTime = SafeValue.SafeString(txt_CdtTime.Text);
            //cont.YardExpiryTime = SafeValue.SafeString(txt_YardExpiryTime.Text);
            cont.TerminalLocation = SafeValue.SafeString(txt_TerminalLocation.Text);
            cont.YardAddress = SafeValue.SafeString(txt_YardAddress.Text);
            cont.Remark = SafeValue.SafeString(txt_ContRemark.Text);
            //cont.Remark1 = SafeValue.SafeString(txt_Remark1.Text);
            cont.Permit = SafeValue.SafeString(cbb_Permit.Value);

            cont.WarehouseStatus = SafeValue.SafeString(cbb_warehouse_status.Value);
            cont.TTTime = SafeValue.SafeString(txt_TTTime.Text);
            cont.Br = SafeValue.SafeString(txt_BR.Text);
            cont.CfsStatus = SafeValue.SafeString(cbb_CfsStatus.Value);
            cont.ScheduleStartDate = SafeValue.SafeDate(date_ScheduleStartDate.Date, new DateTime(1990, 1, 1));
            cont.ScheduleStartTime = SafeValue.SafeString(date_ScheduleStartTime);
            cont.OogInd = SafeValue.SafeString(cbb_oogInd.Value);
            cont.DischargeCell = SafeValue.SafeString(txt_dischargeCell.Text);
            cont.CompletionDate = SafeValue.SafeDate(date_CompletionDate.Date, new DateTime(1990, 1, 1));
            cont.ScheduleStartTime = SafeValue.SafeString(txt_CompletionTime.Text);
            cont.ContainerCategory = SafeValue.SafeString(cbb_ContainerCategory.Value);
            ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
            string partyId = SafeValue.SafeString(btn_ClientId.Text);
            string sql = string.Format(@"select * from MastertRate where CustomerId='{0}' and Description like '%{1}%'", partyId, SafeValue.SafeString(cbbContType.Value));
            DataTable tab = ConnectSql.GetTab(sql);
            //if (tab.Rows.Count > 0)
            //{
            //    cont.Fee1 = SafeValue.SafeDecimal(tab.Rows[0]["Price"]);
            //}
            //else
            //{
            //    cont.Fee1 = SafeValue.SafeDecimal(spin_fee1.Text, 0);
            //}
            //cont.Fee2 = SafeValue.SafeDecimal(spin_fee2.Text, 0);
            //cont.Fee3 = SafeValue.SafeDecimal(spin_fee3.Text, 0);
            //cont.Fee4 = SafeValue.SafeDecimal(spin_fee4.Text, 0);
            //cont.Fee5 = SafeValue.SafeDecimal(spin_fee5.Text, 0);
            //cont.Fee6 = SafeValue.SafeDecimal(spin_fee6.Text, 0);
            ////cont.Fee7 = SafeValue.SafeDecimal(spin_fee7.Text, 0);
            ////cont.Fee8 = SafeValue.SafeDecimal(spin_fee8.Text, 0);
            ////cont.Fee9 = SafeValue.SafeDecimal(spin_fee9.Text, 0);
            ////cont.Fee10 = SafeValue.SafeDecimal(spin_fee10.Text, 0);
            //cont.Fee11 = SafeValue.SafeDecimal(spin_fee11.Text, 0);
            //cont.Fee12 = SafeValue.SafeDecimal(spin_fee12.Text, 0);
            //cont.Fee13 = SafeValue.SafeDecimal(spin_fee13.Text, 0);
            //cont.Fee14 = SafeValue.SafeDecimal(spin_fee14.Text, 0);
            //cont.Fee15 = SafeValue.SafeDecimal(spin_fee15.Text, 0);
            //cont.Fee16 = SafeValue.SafeDecimal(spin_fee16.Text, 0);
            //cont.Fee17 = SafeValue.SafeDecimal(spin_fee17.Text, 0);
            //cont.Fee18 = SafeValue.SafeDecimal(spin_fee18.Text, 0);
            //cont.Fee19 = SafeValue.SafeDecimal(spin_fee19.Text, 0);
            ////cont.Fee20 = SafeValue.SafeDecimal(spin_fee20.Text, 0);
            //cont.Fee21 = SafeValue.SafeDecimal(spin_fee21.Text, 0);
            //cont.Fee22 = SafeValue.SafeDecimal(spin_fee22.Text, 0);
            //cont.Fee23 = SafeValue.SafeDecimal(spin_fee23.Text, 0);
            //cont.Fee24 = SafeValue.SafeDecimal(spin_fee24.Text, 0);
            //cont.Fee25 = SafeValue.SafeDecimal(spin_fee25.Text, 0);
            //cont.Fee26 = SafeValue.SafeDecimal(spin_fee26.Text, 0);
            //cont.Fee27 = SafeValue.SafeDecimal(spin_fee27.Text, 0);
            //cont.Fee28 = SafeValue.SafeDecimal(spin_fee28.Text, 0);
            //cont.Fee29 = SafeValue.SafeDecimal(spin_fee29.Text, 0);
            //cont.Fee30 = SafeValue.SafeDecimal(spin_fee30.Text, 0);
            //cont.Fee31 = SafeValue.SafeDecimal(spin_fee31.Text, 0);
            //cont.Fee32 = SafeValue.SafeDecimal(spin_fee32.Text, 0);
            //cont.Fee33 = SafeValue.SafeDecimal(spin_fee33.Text, 0);
            //cont.Fee34 = SafeValue.SafeDecimal(spin_fee34.Text, 0);
            //cont.Fee35 = SafeValue.SafeDecimal(spin_fee35.Text, 0);
            //cont.Fee36 = SafeValue.SafeDecimal(spin_fee36.Text, 0);
            //cont.Fee37 = SafeValue.SafeDecimal(spin_fee37.Text, 0);
            //cont.Fee38 = SafeValue.SafeDecimal(spin_fee38.Text, 0);
            //cont.Fee39 = SafeValue.SafeDecimal(spin_fee39.Text, 0);
            ////cont.Fee40 = SafeValue.SafeDecimal(spin_fee40.Text, 0);

            //cont.FeeNote1 = SafeValue.SafeString(txt_feeNote1.Text);
            //cont.FeeNote2 = SafeValue.SafeString(txt_feeNote2.Text);
            //cont.FeeNote3 = SafeValue.SafeString(txt_feeNote3.Text);
            //cont.FeeNote4 = SafeValue.SafeString(txt_feeNote4.Text);
            //cont.FeeNote5 = SafeValue.SafeString(txt_feeNote5.Text);
            //cont.FeeNote6 = SafeValue.SafeString(txt_feeNote6.Text);
            ////cont.FeeNote7 = SafeValue.SafeString(txt_feeNote7.Text);
            ////cont.FeeNote8 = SafeValue.SafeString(txt_feeNote8.Text);
            ////cont.FeeNote9 = SafeValue.SafeString(txt_feeNote9.Text);
            ////cont.FeeNote10 = SafeValue.SafeString(txt_feeNote10.Text);
            //cont.FeeNote11 = SafeValue.SafeString(txt_feeNote11.Text);
            //cont.FeeNote12 = SafeValue.SafeString(txt_feeNote12.Text);
            //cont.FeeNote13 = SafeValue.SafeString(txt_feeNote13.Text);
            //cont.FeeNote14 = SafeValue.SafeString(txt_feeNote14.Text);
            //cont.FeeNote15 = SafeValue.SafeString(txt_feeNote15.Text);
            //cont.FeeNote16 = SafeValue.SafeString(txt_feeNote16.Text);
            //cont.FeeNote17 = SafeValue.SafeString(txt_feeNote17.Text);
            //cont.FeeNote18 = SafeValue.SafeString(txt_feeNote18.Text);
            //cont.FeeNote19 = SafeValue.SafeString(txt_feeNote19.Text);
            ////cont.FeeNote20 = SafeValue.SafeString(txt_feeNote20.Text);
            //cont.FeeNote21 = SafeValue.SafeString(txt_feeNote21.Text);
            //cont.FeeNote22 = SafeValue.SafeString(txt_feeNote22.Text);
            //cont.FeeNote23 = SafeValue.SafeString(txt_feeNote23.Text);
            //cont.FeeNote24 = SafeValue.SafeString(txt_feeNote24.Text);
            //cont.FeeNote25 = SafeValue.SafeString(txt_feeNote25.Text);
            //cont.FeeNote26 = SafeValue.SafeString(txt_feeNote26.Text);
            //cont.FeeNote27 = SafeValue.SafeString(txt_feeNote27.Text);
            //cont.FeeNote28 = SafeValue.SafeString(txt_feeNote28.Text);
            //cont.FeeNote29 = SafeValue.SafeString(txt_feeNote29.Text);
            //cont.FeeNote30 = SafeValue.SafeString(txt_feeNote30.Text);
            //cont.FeeNote31 = SafeValue.SafeString(txt_feeNote31.Text);
            //cont.FeeNote32 = SafeValue.SafeString(txt_feeNote32.Text);
            //cont.FeeNote33 = SafeValue.SafeString(txt_feeNote33.Text);
            //cont.FeeNote34 = SafeValue.SafeString(txt_feeNote34.Text);
            //cont.FeeNote35 = SafeValue.SafeString(txt_feeNote35.Text);
            //cont.FeeNote36 = SafeValue.SafeString(txt_feeNote36.Text);
            //cont.FeeNote37 = SafeValue.SafeString(txt_feeNote37.Text);
            //cont.FeeNote38 = SafeValue.SafeString(txt_feeNote38.Text);
            //cont.FeeNote39 = SafeValue.SafeString(txt_feeNote39.Text);
            ////cont.FeeNote40 = SafeValue.SafeString(txt_feeNote40.Text);

            if (isNew)
            {
                C2.Manager.ORManager.StartTracking(cont, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(cont);
                //Trip_new_auto(jobNo.Text);
                e.Result = "success";

                Event_Log(cont.JobNo, "CONT", 1, cont.Id, "");
            }
            else
            {
                C2.Manager.ORManager.StartTracking(cont, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(cont);
                Event_Log(cont.JobNo, "CONT", 3, cont.Id, "");
                if (old_containerno != cont.ContainerNo)
                {
                    sql = string.Format("Update ctm_JobDet2 set ContainerNo='{1}' where Det1Id='{0}'", Id, cont.ContainerNo);
                    ConnectSql.ExecuteSql(sql);
                }

                e.Result = "success";
            }
            string res = Job_Check_ContLevel(cont.Id.ToString());
            e.Result = "success" + (res.Length > 0 ? ":" + res : "");
            //e.Result = btn_ContNo.Text;
        }
    }
    protected void gv_cont_trip_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        int contId = SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        string sql = "select JobNo,Id,ContainerNo from CTM_JobDet1 where Id='" + contId + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            string contNo = dr["ContainerNo"].ToString();
            string JobNo = dr["JobNo"].ToString();
            this.dsContTrip.FilterExpression = string.Format(@" JobNo='{0}' and Det1Id='{1}' and ContainerNo='{2}'", JobNo, contId, contNo);
        }
        else
        {
            this.dsContTrip.FilterExpression = "1=0";
        }
    }
    protected void gv_cont_trip_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ToCode"] = SafeValue.SafeString(e.NewValues["ToCode"]);
        e.NewValues["DriverCode"] = SafeValue.SafeString(e.NewValues["DriverCode"]);
        e.NewValues["TowheadCode"] = SafeValue.SafeString(e.NewValues["TowheadCode"]);
        e.NewValues["ChessisCode"] = SafeValue.SafeString(e.NewValues["ChessisCode"]);
        e.NewValues["StageStatus"] = SafeValue.SafeString(e.NewValues["StageStatus"]);
        e.NewValues["ChessisCode"] = SafeValue.SafeString(e.NewValues["ChessisCode"]);
        e.NewValues["FromDate"] = SafeValue.SafeDate(e.NewValues["FromDate"], new DateTime(1900, 1, 1));
    }
    protected void gv_cont_trip_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxLabel gv_cont_trip_Id = grd.FindEditRowCellTemplateControl(null, "gv_cont_trip_Id") as ASPxLabel;

        string sql = string.Format(@"select * from ctm_jobdet2 where id={0}", gv_cont_trip_Id.Text);
        DataTable dt1 = ConnectSql.GetTab(sql);
        DataRow dr = dt1.Rows[0];

        sql = string.Format(@"with tb1 as (
select det2.Id,
ROW_NUMBER()over(order by 
case when charindex('IMP',j.JobType)>0 then 
(case det2.StageCode when 'Port' then 1 when 'Park1' then 2 when 'Warehouse' then 3 when 'Park2' then 4 when 'Yard' then 5 when 'Park3' then 6 when 'Completed' then 7 else 0 end)
else 
(case det2.StageCode when 'Yard' then 1 when 'Park1' then 2 when 'Warehouse' then 3 when 'Park2' then 4 when 'Port' then 5 when 'Park3' then 6 when 'Completed' then 7 else 0 end)
end 
) as rowId
from ctm_jobdet2 as det2
left outer join ctm_jobdet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as j on j.jobno=det2.JobNo
where det1.Id='{1}'
)
select Id from tb1 where rowId>isnull((select rowId from tb1 where Id='{0}'),0)", dr["Id"], dr["Det1Id"]);
        DataTable dt = ConnectSql.GetTab(sql);
        string Ids = "0";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Ids += (Ids.Length > 0 ? "," : "") + dt.Rows[i][0];
        }

        sql = string.Format(@"update ctm_jobdet2 set ChessisCode='{1}' where Id in({0})", Ids, dr["ChessisCode"]);
        ConnectSql.ExecuteSql(sql);
    }
    protected void gv_cont_trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string action = e.Parameters;
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxGridView gv = pageControl.FindControl("grid_Cont") as ASPxGridView;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox txt_Id = gv.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxButtonEdit btn_ContNo = gv.FindEditFormTemplateControl("btn_ContNo") as ASPxButtonEdit;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;

        if (action.Equals("AddNew"))
        {
            string sql = string.Format(@"select top 1 * from CTM_JobDet2 where Det1Id={0} order by Id desc", txt_Id.Text);
            DataTable dt = ConnectSql.GetTab(sql);
            string FromCode = "";
            DateTime FromDate = DateTime.Now;
            string FromTime = DateTime.Now.ToString("HH:mm");
            string FromPL = "";
            string trailer = "";
            string JobType = SafeValue.SafeString(cbb_JobType.Value);
            string TripCode = "";
            if (dt.Rows.Count > 0)
            {
                FromCode = SafeValue.SafeString(dt.Rows[0]["ToCode"]);
                FromPL = SafeValue.SafeString(dt.Rows[0]["FromParkingLot"]);
                FromDate = SafeValue.SafeDate(dt.Rows[0]["ToDate"], DateTime.Now);
                FromTime = SafeValue.SafeString(dt.Rows[0]["ToTime"], "00:00");
                trailer = SafeValue.SafeString(dt.Rows[0]["ChessisCode"]);
            }
            else
            {
                switch (JobType)
                {
                    case "IMP":
                        TripCode = "IMP";
                        break;
                    case "EXP":
                        TripCode = "COL";
                        break;
                    case "LOC":
                        TripCode = "LOC";
                        break;
                }
            }

            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot,DoubleMounting) values ('{0}','{1}','','','{6}','{2}','{7}','{8}','{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{10}','Normal','N','{9}','No')", txt_JobNo.Text, btn_ContNo.Text, FromCode, "", txt_Id.Text, "", trailer, FromDate, FromTime, FromPL, TripCode);
            ConnectSql.ExecuteSql(sql);
            sql = string.Format(@"select count(*) from ctm_jobdet2 where det1Id={0}", txt_Id.Text);
            int rowSum = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (rowSum == 1)
            {
                if (JobType == "IMP")
                {
                    sql = string.Format(@"update ctm_jobdet1 set StatusCode='{0}' where Id={1}", "Import", txt_Id.Text);
                    ConnectSql.ExecuteSql(sql);
                }
                if (JobType == "EXP")
                {
                    sql = string.Format(@"update ctm_jobdet1 set StatusCode='{0}' where Id={1}", "Collection", txt_Id.Text);
                    ConnectSql.ExecuteSql(sql);
                }
            }
            e.Result = "success";
        }
        if (action.IndexOf("Delete_") >= 0)
        {
            Trip_Delete(sender, e, action.Replace("Delete_", ""));
        }
        if (action.Equals("Update"))
        {
            Trip_Update(sender, e, cbb_JobType.Text);
        }
    }
    protected void gv_cont_trip_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = @"select det1.Id,det1.ContainerNo,det1.ContainerType from CTM_JobDet1 as det1 where JobNo=(select JobNo from CTM_JobDet1 where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + ")";
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxGridView grid_Cont = pageControl.FindControl("grid_Cont") as ASPxGridView;
        ASPxGridView gv_cont_trip = grid_Cont.FindEditFormTemplateControl("gv_cont_trip") as ASPxGridView;
        ASPxDropDownEdit dde_contNo = gv_cont_trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        ASPxGridView gvlist = dde_contNo.FindControl("gridPopCont") as ASPxGridView;
        gvlist.DataSource = C2.Manager.ORManager.GetDataSet(sql);
        gvlist.DataBind();

    }
    #endregion

    #region Transoprt Trip
    protected void grid_Cont_Tpt_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
		{
			grd.ForceDataRowType(typeof(C2.CtmJobDet1));
		}
	}

	protected void grid_Cont_Tpt_BeforePerformDataSelect(object sender, EventArgs e)
	{
		ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
		string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
		this.dsCont.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
	}

	protected void grid_Cont_Tpt_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
	{
		e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
	}

	protected void grid_Cont_Tpt_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
		ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
		ASPxMemo txt_YardRef = pageControl.FindControl("txt_YardRef") as ASPxMemo;
		e.NewValues["RequestDate"] = DateTime.Now;
		e.NewValues["ScheduleDate"] = DateTime.Now;
		e.NewValues["CfsInDate"] = DateTime.Now;
		e.NewValues["CfsOutDate"] = DateTime.Now;
		e.NewValues["YardPickupDate"] = DateTime.Now;
		e.NewValues["YardReturnDate"] = DateTime.Now;
		e.NewValues["CdtDate"] = DateTime.Now;
		e.NewValues["YardExpiryDate"] = DateTime.Now;
		e.NewValues["Weight"] = 0;
		e.NewValues["Volume"] = 0;
		e.NewValues["Qty"] = 0;
		e.NewValues["UrgentInd"] = "N";
		e.NewValues["F5Ind"] = "N";
		e.NewValues["StatusCode"] = "New";
		e.NewValues["Permit"] = "N";
		e.NewValues["YardAddress"] = txt_YardRef.Text;
		e.NewValues["EmailInd"] = "N";
		ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        string partyId = SafeValue.SafeString(btn_ClientId.Text);
        string sql = string.Format(@"select * from MastertRate where CustomerId='{0}'", partyId);
        DataTable tab = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            string code = SafeValue.SafeString(tab.Rows[i]["Description"]);
            decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"]);
            if (code.ToUpper().Equals("TRUCKING"))
            {
                e.NewValues["Fee1"] = price;
            }
            if (code.ToUpper().Equals("FUEL"))
            {
                e.NewValues["Fee2"] = price;
            }
            if (code.ToUpper().Equals("DHC"))
            {
                e.NewValues["Fee3"] = price;
            }
            if (code.ToUpper().Equals("PORTNET"))
            {
                e.NewValues["Fee4"] = price;
            }
            if (code.ToUpper() == "CMS")
            {
                e.NewValues["Fee5"] = price;
            }
            if (code.ToUpper() == "PSA LOLO")
            {
                e.NewValues["Fee6"] = price;
            }
        }
    }

    protected void grid_Cont_Tpt_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["F5Ind"] = SafeValue.SafeString(e.NewValues["F5Ind"], "");
        e.NewValues["UrgentInd"] = SafeValue.SafeString(e.NewValues["UrgentInd"], "");
        e.NewValues["PortnetStatus"] = SafeValue.SafeString(e.NewValues["PortnetStatus"], "");
        e.NewValues["ScheduleDate"] = SafeValue.SafeDate(e.NewValues["ScheduleDate"], new DateTime(1753, 1, 1));
        e.NewValues["RequestDate"] = SafeValue.SafeDate(e.NewValues["RequestDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsInDate"] = SafeValue.SafeDate(e.NewValues["CfsInDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsOutDate"] = SafeValue.SafeDate(e.NewValues["CfsOutDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardPickupDate"] = SafeValue.SafeDate(e.NewValues["YardPickupDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardReturnDate"] = SafeValue.SafeDate(e.NewValues["YardReturnDate"], new DateTime(1753, 1, 1));
        e.NewValues["CdtDate"] = SafeValue.SafeDate(e.NewValues["CdtDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardExpiryDate"] = SafeValue.SafeDate(e.NewValues["YardExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"], "");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "");
       
    }

    protected void grid_Cont_Tpt_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["F5Ind"] = SafeValue.SafeString(e.NewValues["F5Ind"], "");
        e.NewValues["UrgentInd"] = SafeValue.SafeString(e.NewValues["UrgentInd"], "");
        e.NewValues["PortnetStatus"] = SafeValue.SafeString(e.NewValues["PortnetStatus"], "");
        e.NewValues["ScheduleDate"] = SafeValue.SafeDate(e.NewValues["ScheduleDate"], new DateTime(1753, 1, 1));
        e.NewValues["RequestDate"] = SafeValue.SafeDate(e.NewValues["RequestDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsInDate"] = SafeValue.SafeDate(e.NewValues["CfsInDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsOutDate"] = SafeValue.SafeDate(e.NewValues["CfsOutDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardPickupDate"] = SafeValue.SafeDate(e.NewValues["YardPickupDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardReturnDate"] = SafeValue.SafeDate(e.NewValues["YardReturnDate"], new DateTime(1753, 1, 1));
        e.NewValues["CdtDate"] = SafeValue.SafeDate(e.NewValues["CdtDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardExpiryDate"] = SafeValue.SafeDate(e.NewValues["YardExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"], "");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "");
    }

    protected void grid_Cont_Tpt_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
        string sql = string.Format(@"delete from CTM_JobDet2 where Det1Id={0}", e.Values["Id"]);
        ConnectSql.ExecuteSql(sql);
    }

    protected void grid_Cont_Tpt_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        //Trip_new_auto(jobNo.Text);
    }

    protected void grid_Cont_Tpt_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
        if (SafeValue.SafeString(e.NewValues["ContainerNo"]) != SafeValue.SafeString(e.OldValues["ContainerNo"]))
        {
            string sql = string.Format("Update ctm_JobDet2 set ContainerNo='{1}' where Det1Id='{0}'", e.Keys["Id"], SafeValue.SafeString(e.NewValues["ContainerNo"]));
            ConnectSql.ExecuteSql(sql);
        }
    }

    protected void grid_Cont_Tpt_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxLabel lbl_JobType = this.grid_job.FindEditFormTemplateControl("lbl_JobType") as ASPxLabel;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string s = e.Parameters;
        if (s == "save")
        {
            ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
            ASPxTextBox txt_Id = grd.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            ASPxButtonEdit btn_ContNo = grd.FindEditFormTemplateControl("btn_ContNo") as ASPxButtonEdit;
            ASPxTextBox txt_SealNo = grd.FindEditFormTemplateControl("txt_SealNo") as ASPxTextBox;
            ASPxComboBox cbbContType = grd.FindEditFormTemplateControl("cbbContType") as ASPxComboBox;

            ASPxSpinEdit spin_Wt = grd.FindEditFormTemplateControl("spin_Wt") as ASPxSpinEdit;
            ASPxSpinEdit spin_M3 = grd.FindEditFormTemplateControl("spin_M3") as ASPxSpinEdit;
            ASPxSpinEdit spin_Pkgs = grd.FindEditFormTemplateControl("spin_Pkgs") as ASPxSpinEdit;
            ASPxButtonEdit txt_PkgsType = grd.FindEditFormTemplateControl("txt_PkgsType") as ASPxButtonEdit;

            //ASPxDateEdit date_Cont_Request = grd.FindEditFormTemplateControl("date_Cont_Request") as ASPxDateEdit;
            ASPxDateEdit date_Cont_Schedule = grd.FindEditFormTemplateControl("date_Cont_Schedule") as ASPxDateEdit;
            ASPxTextBox txt_DgClass = grd.FindEditFormTemplateControl("txt_DgClass") as ASPxTextBox;

            //ASPxDateEdit date_Cont_CfsIn = grd.FindEditFormTemplateControl("date_Cont_CfsIn") as ASPxDateEdit;
            //ASPxDateEdit date_Cont_CfsOut = grd.FindEditFormTemplateControl("date_Cont_CfsOut") as ASPxDateEdit;
            ASPxComboBox ASPxComboBox1 = grd.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;

            //ASPxDateEdit date_Cont_YardPickup = grd.FindEditFormTemplateControl("date_Cont_YardPickup") as ASPxDateEdit;
            //ASPxDateEdit date_Cont_YardReturn = grd.FindEditFormTemplateControl("date_Cont_YardReturn") as ASPxDateEdit;
            ASPxComboBox cbb_StatusCode = grd.FindEditFormTemplateControl("cbb_StatusCode") as ASPxComboBox;
            ASPxComboBox cbb_F5Ind = grd.FindEditFormTemplateControl("cbb_F5Ind") as ASPxComboBox;
            ASPxComboBox cbb_UrgentInd = grd.FindEditFormTemplateControl("cbb_UrgentInd") as ASPxComboBox;
            ASPxComboBox cbb_EmailInd = grd.FindEditFormTemplateControl("cbb_EmailInd") as ASPxComboBox;

            //ASPxDateEdit date_Cdt = grd.FindEditFormTemplateControl("date_Cdt") as ASPxDateEdit;
            //ASPxTextBox txt_CdtTime = grd.FindEditFormTemplateControl("txt_CdtTime") as ASPxTextBox;
            //ASPxDateEdit date_YardExpiry = grd.FindEditFormTemplateControl("date_YardExpiry") as ASPxDateEdit;
            //ASPxTextBox txt_YardExpiryTime = grd.FindEditFormTemplateControl("txt_YardExpiryTime") as ASPxTextBox;

            ASPxTextBox txt_TerminalLocation = grd.FindEditFormTemplateControl("txt_TerminalLocation") as ASPxTextBox;
            ASPxMemo txt_YardAddress = grd.FindEditFormTemplateControl("txt_YardAddress") as ASPxMemo;
            ASPxMemo txt_ContRemark = grd.FindEditFormTemplateControl("txt_ContRemark") as ASPxMemo;
            //ASPxTextBox txt_Remark1 = grd.FindEditFormTemplateControl("txt_Remark1") as ASPxTextBox;
            ASPxTextBox cbb_VehicleType = grd.FindEditFormTemplateControl("txt_VehicleType") as ASPxTextBox;
            ASPxComboBox cbb_Permit = grd.FindEditFormTemplateControl("cbb_Permit") as ASPxComboBox;
            ASPxComboBox cbb_warehouse_status = grd.FindEditFormTemplateControl("cbb_warehouse_status") as ASPxComboBox;
            ASPxTextBox txt_TTTime = grd.FindEditFormTemplateControl("txt_TTTime") as ASPxTextBox;
            ASPxTextBox txt_BR = grd.FindEditFormTemplateControl("txt_BR") as ASPxTextBox;
            ASPxComboBox cbb_CfsStatus = grd.FindEditFormTemplateControl("cbb_CfsStatus") as ASPxComboBox;
            ASPxDateEdit date_ScheduleStartDate = grd.FindEditFormTemplateControl("date_ScheduleStartDate") as ASPxDateEdit;
            ASPxTextBox date_ScheduleStartTime = grd.FindEditFormTemplateControl("date_ScheduleStartTime") as ASPxTextBox;
            ASPxComboBox cbb_oogInd = grd.FindEditFormTemplateControl("cbb_oogInd") as ASPxComboBox;
            ASPxTextBox txt_dischargeCell = grd.FindEditFormTemplateControl("txt_dischargeCell") as ASPxTextBox;
            ASPxDateEdit date_CompletionDate = grd.FindEditFormTemplateControl("date_CompletionDate") as ASPxDateEdit;
            ASPxTextBox txt_CompletionTime = grd.FindEditFormTemplateControl("txt_CompletionTime") as ASPxTextBox;
            ASPxComboBox cbb_ContainerCategory = grd.FindEditFormTemplateControl("cbb_ContainerCategory") as ASPxComboBox;
            int Id = SafeValue.SafeInt(txt_Id.Text, 0);
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet1), "Id='" + Id + "'");
            C2.CtmJobDet1 cont = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet1;
            bool isNew = false;
            if (cont == null)
            {
                isNew = true;
                cont = new C2.CtmJobDet1();
            }

            string old_containerno = cont.ContainerNo;
            cont.JobNo = jobNo.Text;
            cont.ContainerNo = SafeValue.SafeString(btn_ContNo.Text);
            cont.ScheduleDate = SafeValue.SafeDate(date_Cont_Schedule.Date, new DateTime(1990, 1, 1));
            cont.StatusCode = SafeValue.SafeString(cbb_StatusCode.Value);
            cont.Remark = SafeValue.SafeString(txt_ContRemark.Text);
            cont.VehicleType = SafeValue.SafeString(cbb_VehicleType.Text);
            cont.CfsStatus = SafeValue.SafeString(cbb_CfsStatus.Value);
            cont.ScheduleStartDate = SafeValue.SafeDate(date_ScheduleStartDate.Date, new DateTime(1990, 1, 1));
            cont.ScheduleStartTime = SafeValue.SafeString(date_ScheduleStartTime);
            cont.CompletionDate = SafeValue.SafeDate(date_CompletionDate.Date, new DateTime(1990, 1, 1));
            cont.ScheduleStartTime = SafeValue.SafeString(txt_CompletionTime.Text);

            ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
            string partyId = SafeValue.SafeString(btn_ClientId.Text);


            if (isNew)
            {
                C2.Manager.ORManager.StartTracking(cont, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(cont);
                //Trip_new_auto(jobNo.Text);
                e.Result = "success";

                Event_Log(cont.JobNo, "CONT", 1, cont.Id, "");
            }
            else
            {
                C2.Manager.ORManager.StartTracking(cont, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(cont);
                Event_Log(cont.JobNo, "CONT", 3, cont.Id, "");
                if (old_containerno != cont.ContainerNo)
                {
                   string sql = string.Format("Update ctm_JobDet2 set ContainerNo='{1}' where Det1Id='{0}'", Id, cont.ContainerNo);
                    ConnectSql.ExecuteSql(sql);
                }

                e.Result = "success";
            }
            string res = Job_Check_ContLevel(cont.Id.ToString());
            e.Result = "success" + (res.Length > 0 ? ":" + res : "");
            //e.Result = btn_ContNo.Text;
        }
    }

    protected void gv_tpt_trip_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        int contId = SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        string sql = "select JobNo,Id,ContainerNo from CTM_JobDet1 where Id='" + contId + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            string contNo = dr["ContainerNo"].ToString();
            contNo = contNo.Replace("'", "''");
            string JobNo = dr["JobNo"].ToString();
            this.dsContTrip.FilterExpression = string.Format(@" JobNo='{0}' and Det1Id='{1}'", JobNo, contId);
        }
        else
        {
            this.dsContTrip.FilterExpression = "1=0";
        }
    }

    protected void gv_tpt_trip_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ToCode"] = SafeValue.SafeString(e.NewValues["ToCode"]);
        e.NewValues["DriverCode"] = SafeValue.SafeString(e.NewValues["DriverCode"]);
        e.NewValues["TowheadCode"] = SafeValue.SafeString(e.NewValues["TowheadCode"]);
        e.NewValues["ChessisCode"] = SafeValue.SafeString(e.NewValues["ChessisCode"]);
        e.NewValues["StageStatus"] = SafeValue.SafeString(e.NewValues["StageStatus"]);
        e.NewValues["ChessisCode"] = SafeValue.SafeString(e.NewValues["ChessisCode"]);
        e.NewValues["FromDate"] = SafeValue.SafeDate(e.NewValues["FromDate"], new DateTime(1900, 1, 1));
    }

    protected void gv_tpt_trip_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxLabel gv_tpt_trip_Id = grd.FindEditRowCellTemplateControl(null, "gv_tpt_trip_Id") as ASPxLabel;

        string sql = string.Format(@"select * from ctm_jobdet2 where id={0}", gv_tpt_trip_Id.Text);
        DataTable dt1 = ConnectSql.GetTab(sql);
        DataRow dr = dt1.Rows[0];

        sql = string.Format(@"with tb1 as (
select det2.Id,
ROW_NUMBER()over(order by 
case when charindex('IMP',j.JobType)>0 then 
(case det2.StageCode when 'Port' then 1 when 'Park1' then 2 when 'Warehouse' then 3 when 'Park2' then 4 when 'Yard' then 5 when 'Park3' then 6 when 'Completed' then 7 else 0 end)
else 
(case det2.StageCode when 'Yard' then 1 when 'Park1' then 2 when 'Warehouse' then 3 when 'Park2' then 4 when 'Port' then 5 when 'Park3' then 6 when 'Completed' then 7 else 0 end)
end 
) as rowId
from ctm_jobdet2 as det2
left outer join ctm_jobdet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as j on j.jobno=det2.JobNo
where det1.Id='{1}'
)
select Id from tb1 where rowId>isnull((select rowId from tb1 where Id='{0}'),0)", dr["Id"], dr["Det1Id"]);
        DataTable dt = ConnectSql.GetTab(sql);
        string Ids = "0";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Ids += (Ids.Length > 0 ? "," : "") + dt.Rows[i][0];
        }

        sql = string.Format(@"update ctm_jobdet2 set ChessisCode='{1}' where Id in({0})", Ids, dr["ChessisCode"]);
        ConnectSql.ExecuteSql(sql);
    }

    protected void gv_tpt_trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string action = e.Parameters;
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxGridView gv = pageControl.FindControl("grid_Cont_Tpt") as ASPxGridView;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox txt_Id = gv.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxButtonEdit btn_ContNo = gv.FindEditFormTemplateControl("btn_ContNo") as ASPxButtonEdit;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;

        if (action.Equals("AddNew"))
        {
            string sql = string.Format(@"select top 1 * from CTM_JobDet2 where Det1Id={0} order by Id desc", txt_Id.Text);
            DataTable dt = ConnectSql.GetTab(sql);
            string FromCode = "";
            DateTime FromDate = DateTime.Now;
            string FromTime = DateTime.Now.ToString("HH:mm");
            string FromPL = "";
            string trailer = "";
            string JobType = SafeValue.SafeString(cbb_JobType.Value);
            string TripCode = "";
            if (dt.Rows.Count > 0)
            {
                FromCode = SafeValue.SafeString(dt.Rows[0]["ToCode"]);
                FromPL = SafeValue.SafeString(dt.Rows[0]["FromParkingLot"]);
                FromDate = SafeValue.SafeDate(dt.Rows[0]["ToDate"], DateTime.Now);
                FromTime = SafeValue.SafeString(dt.Rows[0]["ToTime"], "00:00");
                trailer = SafeValue.SafeString(dt.Rows[0]["ChessisCode"]);
            }
            else
            {
                switch (JobType)
                {
                    case "IMP":
                        TripCode = "IMP";
                        break;
                    case "EXP":
                        TripCode = "COL";
                        break;
                    case "LOC":
                        TripCode = "LOC";
                        break;
                }
            }

            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot,DoubleMounting) values ('{0}','{1}','','','{6}','{2}','{7}','{8}','{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{10}','Normal','N','{9}','No')", txt_JobNo.Text, btn_ContNo.Text, FromCode, "", txt_Id.Text, "", btn_ContNo.Text, FromDate, FromTime, FromPL, TripCode, btn_ContNo.Text);
            ConnectSql.ExecuteSql(sql);
            sql = string.Format(@"select count(*) from ctm_jobdet2 where det1Id={0}", txt_Id.Text);
            int rowSum = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (rowSum == 1)
            {
                sql = string.Format(@"update ctm_jobdet1 set StatusCode='{0}' where Id={1}", "Srart", txt_Id.Text);
                ConnectSql.ExecuteSql(sql);
            }
            e.Result = "success";
        }
        if (action.IndexOf("Delete_") >= 0)
        {
            Trip_Delete(sender, e, action.Replace("Delete_", ""));
        }
        if (action.Equals("Update"))
        {
            Trip_Update(sender, e,"TPT");
        }
    }

    protected void gv_tpt_trip_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = @"select det1.Id,det1.ContainerNo,det1.ContainerType from CTM_JobDet1 as det1 where JobNo=(select JobNo from CTM_JobDet1 where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + ")";
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxGridView grid_Cont_Tpt = pageControl.FindControl("grid_Cont_Tpt") as ASPxGridView;
        ASPxGridView gv_tpt_trip = grid_Cont_Tpt.FindEditFormTemplateControl("gv_tpt_trip") as ASPxGridView;
        //ASPxDropDownEdit dde_contNo = gv_tpt_trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        //ASPxGridView gvlist = dde_contNo.FindControl("gridPopCont") as ASPxGridView;
        //gvlist.DataSource = C2.Manager.ORManager.GetDataSet(sql);
        //gvlist.DataBind();

    }

    public string show_driver_signature(object orderNo, object tripId)
    {
        string Signature_Driver = "";
        string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Driver", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            Signature_Driver = dt.Rows[0]["FilePath"].ToString();
            if (Signature_Driver.Length==0)
                Signature_Driver = "http://ell.cargoerp.com/images/logo.jpg";
        }
        
        return Signature_Driver;
    }
    public string show_consignee_signature(object orderNo, object tripId)
    {
        string Signature_Consignee = "http://ell.cargoerp.com/images/logo.jpg";
        string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Consignee", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            Signature_Consignee = dt.Rows[0]["FilePath"].ToString();
            if (Signature_Consignee.Length ==0)
                Signature_Consignee = "http://ell.cargoerp.com/images/logo.jpg";
        }
        return Signature_Consignee;
    }
    #endregion

    #region Crane Trip
    public string change_StatusShortCode_ToCode(object par)
    {
        string res = SafeValue.SafeString(par);
        switch (res)
        {
            case "P":
                res = "Pending";
                break;
            case "S":
                res = "Start";
                break;
            case "C":
                res = "Completed";
                break;
            case "X":
                res = "Cancel";
                break;
        }
        return res;
    }
    protected void grid_Trip_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobDet2));
        }
    }
    protected void grid_Trip_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsCraneTrip.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and JobType='CRA'";
    }
    protected void grid_Trip_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxMemo PickupFrom = pageControl.FindControl("txt_PickupFrom") as ASPxMemo;
        ASPxMemo DeliveryTo = pageControl.FindControl("txt_DeliveryTo") as ASPxMemo;
        string P_From = "";
        string P_From_Pl = "";
        string P_To = "";// DeliveryTo.Text;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string sql = string.Format(@"select top 1 * from ctm_jobdet2 where JobNo='{0}' order by Id desc", txt_JobNo.Text);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            P_From = dt.Rows[0]["ToCode"].ToString();
            P_From_Pl = dt.Rows[0]["ToParkingLot"].ToString();
        }
        e.NewValues["JobType"] = "CRA";
        e.NewValues["Statuscode"] = "P";
        e.NewValues["FromDate"] = DateTime.Now;
        e.NewValues["ToDate"] = DateTime.Now;
        e.NewValues["SubletFlag"] = "N";
        e.NewValues["BayCode"] = "B1";
        e.NewValues["StageCode"] = "Pending";
        e.NewValues["StageStatus"] = "";
        e.NewValues["FromParkingLot"] = P_From_Pl;
        e.NewValues["FromCode"] = P_From;
        e.NewValues["ToCode"] = P_To;
        e.NewValues["Overtime"] = "Normal";
        e.NewValues["OverDistance"] = "Y";
    }
    protected void grid_Trip_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        check_Trip_Status("0", e.NewValues["DriverCode"].ToString(), e.NewValues["Statuscode"].ToString());
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        e.NewValues["JobType"] = "CRA";
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["TripCode"] = SafeValue.SafeString(e.NewValues["TripCode"], "");
        e.NewValues["Det1Id"] = SafeValue.SafeInt(e.NewValues["Det1Id"], 0);
        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"], "");
        e.NewValues["BayCode"] = SafeValue.SafeString(e.NewValues["BayCode"], "");

        e.NewValues["FromDate"] = SafeValue.SafeDate(e.NewValues["FromDate"], new DateTime(1753, 1, 1));
        e.NewValues["ToDate"] = SafeValue.SafeDate(e.NewValues["ToDate"], new DateTime(1753, 1, 1));
        e.NewValues["StageCode"] = SafeValue.SafeString(e.NewValues["StageCode"], "Pending");
        e.NewValues["StageStatus"] = SafeValue.SafeString(e.NewValues["StageStatus"], "Pending");
        e.NewValues["Overtime"] = SafeValue.SafeString(e.NewValues["Overtime"], "");
        e.NewValues["OverDistance"] = SafeValue.SafeString(e.NewValues["OverDistance"], "");
    }
    protected void grid_Trip_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxLabel lb_tripId = grd.FindEditFormTemplateControl("lb_tripId") as ASPxLabel;
        check_Trip_Status(lb_tripId.Text.ToString(), e.NewValues["DriverCode"].ToString(), e.NewValues["Statuscode"].ToString());
        e.NewValues["TripCode"] = SafeValue.SafeString(e.NewValues["TripCode"], "");
        e.NewValues["Det1Id"] = SafeValue.SafeInt(e.NewValues["Det1Id"], 0);
        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"], "");
        e.NewValues["BayCode"] = SafeValue.SafeString(e.NewValues["BayCode"], "");

        e.NewValues["FromDate"] = SafeValue.SafeDate(e.NewValues["FromDate"], new DateTime(1753, 1, 1));
        e.NewValues["ToDate"] = SafeValue.SafeDate(e.NewValues["ToDate"], new DateTime(1753, 1, 1));
        e.NewValues["StageCode"] = SafeValue.SafeString(e.NewValues["StageCode"], "Pending");
        e.NewValues["StageStatus"] = SafeValue.SafeString(e.NewValues["StageStatus"], "Pending");
        e.NewValues["Overtime"] = SafeValue.SafeString(e.NewValues["Overtime"], "");
        e.NewValues["OverDistance"] = SafeValue.SafeString(e.NewValues["OverDistance"], "");
    }
    protected void grid_Trip_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Trip_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        //ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        //string sql = @"select det1.Id,det1.ContainerNo,det1.ContainerType from CTM_JobDet1 as det1 left outer join CTM_Job as job on det1.JobNo=job.JobNo where job.Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        //ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxGridView grid_Trip = pageControl.FindControl("grid_Trip") as ASPxGridView;
        //ASPxDropDownEdit dde_contNo = grid_Trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        //ASPxGridView gvlist = dde_contNo.FindControl("gridPopCont") as ASPxGridView;
        //gvlist.DataSource = C2.Manager.ORManager.GetDataSet(sql);
        //gvlist.DataBind();
    }
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] contIds = new object[grid.VisibleRowCount];
        object[] contNs = new object[grid.VisibleRowCount];
        object[] contTypes = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            contIds[i] = grid.GetRowValues(i, "Id");
            contNs[i] = grid.GetRowValues(i, "ContainerNo");
            contTypes[i] = grid.GetRowValues(i, "ContainerType");
        }

        e.Properties["cpContId"] = contIds;
        e.Properties["cpContN"] = contNs;
        e.Properties["cpContType"] = contTypes;
    }
    private void check_Trip_Status(string id, string driverCode, string status)
    {
        if (driverCode.Trim().Length == 0)
        {
            return;
        }

        if (status == "S" || status == "P")
        {
            string sql = string.Format(@"select COUNT(*) from CTM_JobDet2 where DriverCode='{0}' and Statuscode='{2}' and Id<>'{1}'", driverCode, id, status);
            int result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 0);
            if (result > 0)
            {
                throw new Exception("Status:'" + status + "' is existing for " + driverCode);
            }
        }
    }
    protected void grid_Trip_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    protected void grid_Trip_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    protected void grid_Trip_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    protected void grid_Trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "Update")
        {
            Trip_Update(sender, e, "CRA");
        }
        else
        {
            string temp = e.Parameters;
            string[] ar = temp.Split('_');
            if (ar.Length == 2)
            {
                if (ar[0] == "Delete")
                {
                    Trip_Delete(sender, e, ar[1]);
                }
            }
        }
    }

    private void Trip_Delete(object sender, ASPxGridViewCustomDataCallbackEventArgs e, string tripId)
    {
        Event_Log("", "TRIP", 2, SafeValue.SafeInt(tripId, 0), "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
        C2.Manager.ORManager.ExecuteDelete(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        //C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
        //C2.Manager.ORManager.PersistChanges(trip);

        //EGL_JobTrip_AfterEndTrip("", SafeValue.SafeString(trip.Det1Id, "0"));

        string re = HttpContext.Current.User.Identity.Name + "," + trip.Id + "," + trip.DriverCode;
        //delete Job_Cost for Trip
        string sql = string.Format(@"delete from job_cost where TripNo='{0}'", tripId);
        C2.Manager.ORManager.ExecuteScalar(sql);



        e.Result = re;
    }

    private void Trip_Update(object sender, ASPxGridViewCustomDataCallbackEventArgs e, string jobType)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxLabel lbl_JobType = this.grid_job.FindEditFormTemplateControl("lbl_JobType") as ASPxLabel;
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxLabel lb_tripId = grd.FindEditFormTemplateControl("lb_tripId") as ASPxLabel;
        string tripId = SafeValue.SafeString(lb_tripId.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
        string Driver_old = "";

        bool isNew = false;
        if (trip == null)
        {
            isNew = true;
            trip = new C2.CtmJobDet2();
        }
        else
        {
            Driver_old = trip.DriverCode;
        }

        //ASPxDropDownEdit dde_Trip_ContNo = grd.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        //ASPxButtonEdit btn_ChessisCode = grd.FindEditFormTemplateControl("btn_ChessisCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_zone = grd.FindEditFormTemplateControl("cbb_zone") as ASPxComboBox;
        ASPxTextBox dde_Trip_ContId = grd.FindEditFormTemplateControl("dde_Trip_ContId") as ASPxTextBox;
        //ASPxButtonEdit btn_CfsCode = grd.FindEditFormTemplateControl("btn_CfsCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_BayCode = grd.FindEditFormTemplateControl("cbb_Trip_BayCode") as ASPxComboBox;
        //ASPxComboBox cbb_Carpark = grd.FindEditFormTemplateControl("cbb_Carpark") as ASPxComboBox;
        ASPxComboBox cbb_Trip_TripCode = grd.FindEditFormTemplateControl("cbb_Trip_TripCode") as ASPxComboBox;

        ASPxButtonEdit btn_DriverCode = grd.FindEditFormTemplateControl("btn_DriverCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_TowheadCode = grd.FindEditFormTemplateControl("btn_TowheadCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_SubletFlag = grd.FindEditFormTemplateControl("cbb_Trip_SubletFlag") as ASPxComboBox;
        //ASPxTextBox txt_SubletHauliername = grd.FindEditFormTemplateControl("txt_SubletHauliername") as ASPxTextBox;
        //ASPxComboBox cbb_StageCode = grd.FindEditFormTemplateControl("cbb_StageCode") as ASPxComboBox;
        //ASPxComboBox cbb_StageStatus = grd.FindEditFormTemplateControl("cbb_StageStatus") as ASPxComboBox;
        ASPxComboBox cbb_Trip_StatusCode = grd.FindEditFormTemplateControl("cbb_Trip_StatusCode") as ASPxComboBox;
        //ASPxComboBox cmb_DoubleMounting = grd.FindEditFormTemplateControl("cmb_DoubleMounting") as ASPxComboBox;
        ASPxDateEdit txt_FromDate = grd.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_fromTime = grd.FindEditFormTemplateControl("txt_Trip_fromTime") as ASPxTextBox;
        ASPxDateEdit date_Trip_toDate = grd.FindEditFormTemplateControl("date_Trip_toDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_toTime = grd.FindEditFormTemplateControl("txt_Trip_toTime") as ASPxTextBox;
        ASPxMemo txt_Trip_Remark = grd.FindEditFormTemplateControl("txt_Trip_Remark") as ASPxMemo;
        //ASPxMemo txt_Trip_FromCode = grd.FindEditFormTemplateControl("txt_Trip_FromCode") as ASPxMemo;
        ASPxMemo txt_Trip_ToCode = grd.FindEditFormTemplateControl("txt_Trip_ToCode") as ASPxMemo;
        //ASPxSpinEdit spin_Price = grd.FindEditFormTemplateControl("spin_Price") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Incentive1 = grd.FindEditFormTemplateControl("spin_Incentive1") as ASPxSpinEdit;
        ASPxSpinEdit spin_Incentive2 = grd.FindEditFormTemplateControl("spin_Incentive2") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Incentive3 = grd.FindEditFormTemplateControl("spin_Incentive3") as ASPxSpinEdit;
        //ASPxComboBox cbb_Incentive4 = grd.FindEditFormTemplateControl("cbb_Incentive4") as ASPxComboBox;
        //ASPxSpinEdit spin_Incentive4 = grd.FindEditFormTemplateControl("spin_Incentive4") as ASPxSpinEdit;

        //ASPxComboBox cbb_Overtime = grd.FindEditFormTemplateControl("cbb_Overtime") as ASPxComboBox;
        ASPxMemo txt_driver_remark = grd.FindEditFormTemplateControl("txt_driver_remark") as ASPxMemo;
        //ASPxComboBox cbb_OverDistance = grd.FindEditFormTemplateControl("cbb_OverDistance") as ASPxComboBox;

        //ASPxTextBox fromPackingLot = grd.FindEditFormTemplateControl("txt_FromPL") as ASPxTextBox;
        //ASPxTextBox toPackingLot = grd.FindEditFormTemplateControl("txt_ToPL") as ASPxTextBox;
        //check_Trip_Status("0", trip.DriverCode,trip.Statuscode);

        ASPxSpinEdit spin_Charge1 = grd.FindEditFormTemplateControl("spin_Charge1") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge2 = grd.FindEditFormTemplateControl("spin_Charge2") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge3 = grd.FindEditFormTemplateControl("spin_Charge3") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge4 = grd.FindEditFormTemplateControl("spin_Charge4") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge5 = grd.FindEditFormTemplateControl("spin_Charge5") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge6 = grd.FindEditFormTemplateControl("spin_Charge6") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge7 = grd.FindEditFormTemplateControl("spin_Charge7") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge8 = grd.FindEditFormTemplateControl("spin_Charge8") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge9 = grd.FindEditFormTemplateControl("spin_Charge9") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge10 = grd.FindEditFormTemplateControl("spin_Charge10") as ASPxSpinEdit;

        ASPxSpinEdit spin_Charge_LifingTeamOverTime = grd.FindEditFormTemplateControl("spin_Charge_LifingTeamOverTime") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge_WorkerOvertime = grd.FindEditFormTemplateControl("spin_Charge_WorkerOvertime") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge_ParkingFee = grd.FindEditFormTemplateControl("spin_Charge_ParkingFee") as ASPxSpinEdit;


        ASPxSpinEdit ASPxSpinEdit2 = grd.FindEditFormTemplateControl("ASPxSpinEdit2") as ASPxSpinEdit;
        ASPxSpinEdit ASPxSpinEdit3 = grd.FindEditFormTemplateControl("ASPxSpinEdit3") as ASPxSpinEdit;
        ASPxSpinEdit ASPxSpinEdit4 = grd.FindEditFormTemplateControl("ASPxSpinEdit4") as ASPxSpinEdit;
        ASPxSpinEdit ASPxSpinEdit5 = grd.FindEditFormTemplateControl("ASPxSpinEdit5") as ASPxSpinEdit;
        ASPxSpinEdit ASPxSpinEdit6 = grd.FindEditFormTemplateControl("ASPxSpinEdit6") as ASPxSpinEdit;


        ASPxDateEdit date_BookingDate = grd.FindEditFormTemplateControl("date_BookingDate") as ASPxDateEdit;
        ASPxTextBox txt_BookingTime = grd.FindEditFormTemplateControl("txt_BookingTime") as ASPxTextBox;
        ASPxTextBox txt_BookingTime2 = grd.FindEditFormTemplateControl("txt_BookingTime2") as ASPxTextBox;
        ASPxMemo txt_BookingRemark = grd.FindEditFormTemplateControl("txt_BookingRemark") as ASPxMemo;
        ASPxMemo txt_Trip_BillingRemark = grd.FindEditFormTemplateControl("txt_Trip_BillingRemark") as ASPxMemo;

        ASPxSpinEdit txt_TotalHour = grd.FindEditFormTemplateControl("txt_TotalHour") as ASPxSpinEdit;
        ASPxSpinEdit txt_OtHour = grd.FindEditFormTemplateControl("txt_OtHour") as ASPxSpinEdit;
        ASPxTextBox txt_ByUser = grd.FindEditFormTemplateControl("txt_ByUser") as ASPxTextBox;
        ASPxTextBox txt_TrailerType = grd.FindEditFormTemplateControl("txt_TrailerType") as ASPxTextBox;
        ASPxTextBox txt_VehicleType = grd.FindEditFormTemplateControl("txt_VehicleType") as ASPxTextBox;
        if (txt_TrailerType != null)
            trip.RequestTrailerType = txt_TrailerType.Text;
        if (txt_VehicleType != null)
            trip.RequestVehicleType = txt_VehicleType.Text;
        //if(dde_Trip_ContNo!=null)
        //  trip.ContainerNo = SafeValue.SafeString(dde_Trip_ContNo.Value);
        //if (btn_ChessisCode != null)
        //   trip.ChessisCode = SafeValue.SafeString(btn_ChessisCode.Value);
        //if (cbb_zone != null)
        //   trip.ParkingZone = SafeValue.SafeString(cbb_zone.Value);
        if (dde_Trip_ContId != null)
            trip.Det1Id = SafeValue.SafeInt(dde_Trip_ContId.Text, 0);
        //trip.CfsCode = SafeValue.SafeString(btn_CfsCode.Value);
        //trip.BayCode = SafeValue.SafeString(cbb_Trip_BayCode.Value);
        if (btn_DriverCode != null)
            trip.DriverCode = SafeValue.SafeString(btn_DriverCode.Value);
        if (btn_TowheadCode != null)
            trip.TowheadCode = SafeValue.SafeString(btn_TowheadCode.Value);
        //trip.SubletFlag = SafeValue.SafeString(cbb_Trip_SubletFlag.Value);
        //trip.SubletHauliername = SafeValue.SafeString(txt_SubletHauliername.Text);
        if (cbb_Trip_TripCode != null)
            trip.TripCode = SafeValue.SafeString(cbb_Trip_TripCode.Value);
        //if (cmb_DoubleMounting != null)
        //   trip.DoubleMounting = SafeValue.SafeString(cmb_DoubleMounting.Value,"No");
        //trip.StageCode = SafeValue.SafeString(cbb_StageCode.Value);
        //trip.Carpark = SafeValue.SafeString(cbb_Carpark.Value);
        //trip.StageStatus = SafeValue.SafeString(cbb_StageStatus.Value);

        if (date_BookingDate != null)
            trip.BookingDate = SafeValue.SafeDate(date_BookingDate.Date, new DateTime(1900, 1, 1));
        if (txt_BookingTime != null)
            trip.BookingTime = SafeValue.SafeString(txt_BookingTime.Text);
        if (txt_BookingTime2 != null)
            trip.BookingTime2 = SafeValue.SafeString(txt_BookingTime2.Text);
        if (cbb_Trip_StatusCode != null)
            trip.Statuscode = SafeValue.SafeString(cbb_Trip_StatusCode.Value);
        if (txt_FromDate != null)
            trip.FromDate = SafeValue.SafeDate(txt_FromDate.Date, new DateTime(1990, 1, 1));
        if (txt_Trip_fromTime != null)
            trip.FromTime = SafeValue.SafeString(txt_Trip_fromTime.Text);
        if (date_Trip_toDate != null)
            trip.ToDate = SafeValue.SafeDate(date_Trip_toDate.Date, new DateTime(1990, 1, 1));
        if (txt_Trip_toTime != null)
            trip.ToTime = SafeValue.SafeString(txt_Trip_toTime.Text);
        if (txt_Trip_Remark != null)
            trip.Remark = SafeValue.SafeString(txt_Trip_Remark.Text);
        //if (txt_Trip_FromCode != null)
        //   trip.FromCode = SafeValue.SafeString(txt_Trip_FromCode.Text);
        if (txt_Trip_ToCode != null)
            trip.ToCode = SafeValue.SafeString(txt_Trip_ToCode.Text);

        //trip.Overtime = SafeValue.SafeSqlString(cbb_Overtime.Value);
        //trip.OverDistance = SafeValue.SafeSqlString(cbb_OverDistance.Value);
        if (txt_driver_remark != null)
            trip.Remark1 = SafeValue.SafeString(txt_driver_remark.Text);
        //if (fromPackingLot != null)
        //   trip.FromParkingLot = SafeValue.SafeString(fromPackingLot.Text);
        //if (toPackingLot != null)
        //   trip.ToParkingLot = SafeValue.SafeString(toPackingLot.Text);
        if (txt_BookingRemark != null)
            trip.BookingRemark = txt_BookingRemark.Text;
        if (txt_Trip_BillingRemark != null)
            trip.BillingRemark = txt_Trip_BillingRemark.Text;
        if (txt_OtHour != null)
            trip.OtHour = SafeValue.SafeDecimal(txt_OtHour.Text);
        if (txt_TotalHour != null)
            trip.TotalHour = SafeValue.SafeDecimal(txt_TotalHour.Text);
        if (txt_ByUser != null)
            trip.ByUser = SafeValue.SafeString(txt_ByUser.Text);

        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = HttpContext.Current.User.Identity.Name;
        if (isNew)
        {
            trip.JobNo = jobNo.Text;
            trip.CreateUser = HttpContext.Current.User.Identity.Name;
            trip.CreateTime = DateTime.Now;
            trip.UpdateUser = HttpContext.Current.User.Identity.Name;
            trip.UpdateTime = DateTime.Now;
            trip.JobType = jobType;
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(trip);



            elog.Remark = "New Trip";
        }
        else
        {
            trip.UpdateUser = HttpContext.Current.User.Identity.Name;
            trip.UpdateTime = DateTime.Now;
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(trip);
            //EGL_JobTrip_AfterEndTrip("", dde_Trip_ContId.Text);
            elog.Remark = "Update Trip";
        }
        elog.ActionLevel_isTRIP(trip.Id);
        elog.log();

        Dictionary<string, decimal> d = new Dictionary<string, decimal>();
        //d.Add("Trip", SafeValue.SafeDecimal(spin_Incentive1.Text));
        if (spin_Incentive2 != null)
            d.Add("OverTime", SafeValue.SafeDecimal(spin_Incentive2.Text));
        //d.Add("Standby", SafeValue.SafeDecimal(spin_Incentive3.Text));
        //d.Add("ALLOWANCE", SafeValue.SafeDecimal(spin_Incentive4.Text));
        C2.CtmJobDet2.Incentive_Save(trip.Id, d);
        d = new Dictionary<string, decimal>();
        if (spin_Charge1 != null)
            d.Add("EXPENSE", SafeValue.SafeDecimal(spin_Charge1.Text));
        //d.Add("DHC", SafeValue.SafeDecimal(spin_Charge1.Text));
        //d.Add("WEIGHING", SafeValue.SafeDecimal(spin_Charge2.Text));
        //d.Add("WASHING", SafeValue.SafeDecimal(spin_Charge3.Text));
        //d.Add("REPAIR", SafeValue.SafeDecimal(spin_Charge4.Text));
        //d.Add("DETENTION", SafeValue.SafeDecimal(spin_Charge5.Text));
        //d.Add("DEMURRAGE", SafeValue.SafeDecimal(spin_Charge6.Text));
        //d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(spin_Charge7.Text));
        //d.Add("C_SHIPMENT", SafeValue.SafeDecimal(spin_Charge8.Text));
        //d.Add("EMF", SafeValue.SafeDecimal(spin_Charge9.Text));
        //d.Add("OTHER", SafeValue.SafeDecimal(spin_Charge10.Text));

        C2.CtmJobDet2.Claims_Save(trip.Id, d);
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        decimal otHour = 0;
        if (txt_OtHour != null)
            otHour = SafeValue.SafeDecimal(txt_OtHour.Value, 0);
        string sql_rate = string.Format(@"select Price,ChgCode,VehicleType from job_rate where ClientId='{0}' and BillClass='CRANE' and JobType='{1}' and BillScope='TRIP' and LineStatus='N'", btn_ClientId.Text, lbl_JobType.Text);
        DataTable dt_rate = ConnectSql_mb.GetDataTable(sql_rate);
        decimal charge1 = 0;
        decimal charge2 = 0;
        decimal charge3 = 0;
        decimal charge4 = 0;
        decimal charge5 = 0;
        decimal charge6 = 0;
        decimal charge7 = 0;
        decimal charge8 = 0;
        decimal charge9 = 0;
        for (int i = 0; i < dt_rate.Rows.Count; i++)
        {
            decimal price = SafeValue.SafeDecimal(dt_rate.Rows[i]["Price"]);
            string chgCode = SafeValue.SafeString(dt_rate.Rows[i]["ChgCode"]);
            string vehicleType = SafeValue.SafeString(dt_rate.Rows[i]["VehicleType"]);

            string sql_vehicle = string.Format(@"select ContractNo from Ref_Vehicle where VehicleCode='{0}'", btn_TowheadCode.Text);
            string vehicle = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql_vehicle));

            d = new Dictionary<string, decimal>();
            if (chgCode != null && chgCode.Length > 0)
            {
                if (chgCode.ToUpper().Contains("TRIP"))
                {
                    #region TRIP
                    if (SafeValue.SafeDecimal(spin_Charge2.Text) == 0)
                    {
                        if (vehicleType.Length > 0 && vehicle.Length > 0)
                        {
                            if (vehicleType.Substring(0, 2) == vehicle.Substring(0, 2))
                            {
                                charge1 = price;
                            }
                        }
                    }
                    else
                    {
                        charge1 = SafeValue.SafeDecimal(spin_Charge2.Text);
                    }
                    #endregion
                }
                else if (chgCode.ToUpper().Contains("OVERTIME"))
                {
                    #region OVERTIME
                    if (SafeValue.SafeDecimal(spin_Charge3.Text) == 0)
                    {
                        if (vehicleType.Length > 0 && vehicle.Length > 0)
                        {
                            if (vehicleType.Substring(0, 2) == vehicle.Substring(0, 2))
                            {
                                charge2 = otHour * price;
                            }
                        }

                    }
                    else
                    {
                        charge2 = SafeValue.SafeDecimal(spin_Charge3.Text);
                    }
                    #endregion
                }
                else if (chgCode.ToUpper().Contains("CONCRETEBUCKET"))
                {
                    #region CONCRETE BUCKET
                    if (SafeValue.SafeDecimal(spin_Charge4.Text) == 0)
                    {
                        charge3 = price;
                    }
                    else
                    {
                        charge3 = SafeValue.SafeDecimal(spin_Charge4.Text);
                    }
                    #endregion
                }
                else if (chgCode.ToUpper().Contains("SENDBUCKET"))
                {
                    #region SEND BUCKET
                    if (SafeValue.SafeDecimal(spin_Charge5.Text) == 0)
                    {
                        charge4 = price;
                    }
                    else
                    {
                        charge4 = SafeValue.SafeDecimal(spin_Charge5.Text);
                    }
                    #endregion
                }
                else if (chgCode.ToUpper().Contains("LIFTINGSUPERVISOR"))
                {
                    #region LIFTING SUPERVISOR
                    if (SafeValue.SafeDecimal(ASPxSpinEdit2.Text) == 0)
                    {
                        charge5 = price;
                    }
                    else
                    {
                        charge5 = SafeValue.SafeDecimal(ASPxSpinEdit2.Text);
                    }
                    #endregion
                }
                else if (chgCode.ToUpper().Contains("RINGER"))
                {
                    #region Ringer
                    if (SafeValue.SafeDecimal(ASPxSpinEdit3.Text) == 0)
                    {
                        charge6 = price;
                    }
                    else
                    {
                        charge6 = SafeValue.SafeDecimal(ASPxSpinEdit3.Text);
                    }
                    #endregion
                }
                else if (chgCode.ToUpper().Contains("SIGNAL"))
                {
                    #region Signal
                    if (SafeValue.SafeDecimal(ASPxSpinEdit4.Text) == 0)
                    {
                        charge7 = price;
                    }
                    else
                    {
                        charge7 = SafeValue.SafeDecimal(ASPxSpinEdit4.Text);
                    }
                    #endregion
                }
                else if (chgCode.ToUpper().Contains("LIGNTEQUIPMENT"))
                {
                    #region LightEquipment
                    if (SafeValue.SafeDecimal(ASPxSpinEdit5.Text) == 0)
                    {
                        charge8 = price;
                    }
                    else
                    {
                        charge8 = SafeValue.SafeDecimal(ASPxSpinEdit5.Text);
                    }
                    #endregion
                }
                else if (chgCode.ToUpper().Contains("LABOUR"))
                {
                    #region Labour
                    if (SafeValue.SafeDecimal(ASPxSpinEdit5.Text) == 0)
                    {
                        charge9 = price;
                    }
                    else
                    {
                        charge9 = SafeValue.SafeDecimal(ASPxSpinEdit5.Text);
                    }
                    #endregion
                }
            }
        }
        d.Add("Trip", SafeValue.SafeDecimal(charge1));
        d.Add("OverTime", SafeValue.SafeDecimal(charge2));
        d.Add("ConcreteBucket", SafeValue.SafeDecimal(charge3));
        d.Add("SandBucket", SafeValue.SafeDecimal(charge4));
        d.Add("LiftingSupervisor", SafeValue.SafeDecimal(charge5));
        d.Add("Rigger", SafeValue.SafeDecimal(charge6));
        d.Add("Signal", SafeValue.SafeDecimal(charge7));
        d.Add("LiftingEquipment", SafeValue.SafeDecimal(charge8));
        d.Add("Labour", SafeValue.SafeDecimal(charge9));
        C2.CtmJobDet2.Billing_Save(trip.Id, d);
        d = new Dictionary<string, decimal>();
        if (spin_Charge_LifingTeamOverTime != null)
            d.Add("LiftingTeamOverTime", SafeValue.SafeDecimal(spin_Charge_LifingTeamOverTime.Text));
        if (spin_Charge_WorkerOvertime != null)
            d.Add("WorkerOvertime", SafeValue.SafeDecimal(spin_Charge_WorkerOvertime.Text));
        if (spin_Charge_ParkingFee != null)
            d.Add("ParkingFee", SafeValue.SafeDecimal(spin_Charge_ParkingFee.Text));
        C2.CtmJobDet2.Claims_Save(trip.Id, d);


        try
        {
            C2.CtmJobDet2.tripStatusChanged(trip.Id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ex.StackTrace);
        }

        string re = HttpContext.Current.User.Identity.Name + "," + trip.Id + "," + trip.DriverCode;

        if (!trip.DriverCode.Equals(Driver_old))
        {
            re += "," + Driver_old;
        }
        e.Result = re;

    }
    private void update_ContStatus_trip(int contId, string type)
    {
        string JobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string isWarehouse = SafeValue.SafeString(Request.QueryString["isWarehouse"]);
        string sql = "";
        string status = "";
        if (isWarehouse == "Yes")
        {
            if (type == "IMP")
                status = "WHS-LD";
            if (type == "EXP")
                status = "WHS-MT";
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
            ConnectSql_mb.ExecuteNonQuery(sql);
            Event_Log(JobNo, "CONT", 4, contId, status);
        }
        if (isWarehouse == "No")
        {
            if (type == "IMP")
                status = "Customer-LD";
            if (type == "EXP")
                status = "Customer-MT";
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
            ConnectSql_mb.ExecuteNonQuery(sql);
            Event_Log(JobNo, "CONT", 4, contId, status);
        }
    }

    //    public void EGL_JobTrip_AfterEndTrip(string TripId)
    //    {
    //        string sql = string.Format(@"select Det1Id from ctm_jobdet2 where Id={0}", TripId);
    //        DataTable dt = ConnectSql_mb.GetDataTable(sql);
    //        if (dt.Rows.Count > 0)
    //        {
    //            string ContId = dt.Rows[0][0].ToString();
    //            EGL_JobTrip_AfterEndTrip(TripId, ContId);
    //        }
    //    }

    //    public void EGL_JobTrip_AfterEndTrip(string TripId, string ContId)
    //    {
    //        string sql = string.Format(@"with tb1 as (
    //select sum(charge1) as charge1,sum(charge2) as charge2,sum(charge3) as charge3,sum(charge4) as charge4,sum(charge5) as charge5,
    //sum(charge6) as charge6,sum(charge7) as charge7,sum(charge8) as charge8,sum(charge9) as charge9,sum(charge10) as charge10 
    //from ctm_jobdet2 where Det1Id={0} and Statuscode='C'
    //)
    //update ctm_jobdet1 set 
    //fee3=(select charge1 from tb1),
    //fee11=(select charge2 from tb1),
    //fee12=(select charge3 from tb1),
    //fee13=(select charge4 from tb1),
    //fee14=(select charge5 from tb1),
    //fee15=(select charge6 from tb1),
    //fee16=(select charge7 from tb1),
    //fee17=(select charge8 from tb1),
    //fee18=(select charge9 from tb1),
    //fee19=(select charge10 from tb1)
    //where Id={0}", ContId);
    //        ConnectSql_mb.ExecuteNonQuery(sql);
    //    }

    #endregion

    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        this.dsJobPhoto.FilterExpression = "RefNo='" + SafeValue.SafeString(jobNo.Text, "") + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        C2.CtmJobEventLog elog = new CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = HttpContext.Current.User.Identity.Name;
        elog.fixActionInfo_ByAttachmentId(SafeValue.SafeInt(e.Values["Id"], 0));
        elog.Remark = "Delete File:" + e.Values["FileName"];
        elog.log();

        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["FileNote"] = " ";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }

    #endregion

    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsInvoice.FilterExpression = "MastType='CTM' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsVoucher.FilterExpression = "MastType='CTM' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    #endregion

    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Job_Cost));
        }
    }
    protected void grid_Cost_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["VendorId"] = " ";
        e.NewValues["ChgCode"] = " ";
        e.NewValues["ChgCodeDes"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["CurrencyId"] = "SGD";
        e.NewValues["ExRate"] = 1;
        e.NewValues["JobNo"] = "0";
        e.NewValues["Price"] = 0;
        e.NewValues["Qty"] = 0;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        e.NewValues["LineType"] = "CN";
        e.NewValues["GstType"] = "Z";
    }
    protected void grid_Cost_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        ASPxTextBox refNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = refNo.Text;
        e.NewValues["JobType"] = SafeValue.SafeString(cbb_JobType.Value);
        e.NewValues["DocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        e.NewValues["LocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["DocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["ExRate"], 0), 2);
    }
    protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox refNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;

    }
    protected void grid_Cost_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
    protected void grid_Cost_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Cost_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;

    }
    protected void grid_Cost_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox vendorName = grd.FindEditFormTemplateControl("txt_CostVendorName") as ASPxTextBox;
        vendorName.Text = EzshipHelper.GetPartyName(grd.GetRowValues(grd.EditingRowVisibleIndex, new string[] { "VendorId" }));
    }
    protected void grid_Cost_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "InvoiceAmt")
        {
            decimal locAmt = SafeValue.SafeDecimal(e.GetValue("LocAmt"));
            decimal invoiceAmt = SafeValue.SafeDecimal(e.GetValue("InvoiceAmt"));
            if (locAmt != invoiceAmt)
            {
                e.Cell.ForeColor = System.Drawing.Color.White;
                e.Cell.BackColor = System.Drawing.Color.Orange;
            }

        }
        if (e.DataColumn.FieldName == "InvoiceGstType")
        {
            string gstType = SafeValue.SafeString(e.GetValue("GstType"));
            string invoice_gstType = SafeValue.SafeString(e.GetValue("InvoiceGstType"));
            if (gstType != invoice_gstType)
            {
                e.Cell.ForeColor = System.Drawing.Color.White;
                e.Cell.BackColor = System.Drawing.Color.Orange;
            }
        }
    }
    #endregion

    private void updateJob_By_Date(string Id)
    {
        string sql = string.Format(@"update CTM_Job set UpdateBy='{0}',UpdateDateTime=getdate() where Id='{1}'", HttpContext.Current.User.Identity.Name, Id);
        ConnectSql.ExecuteSql(sql);
    }

    #region Trip Log
    protected void grid_TripLog_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmTripLog));
        }
    }
    protected void grid_TripLog_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        //this.dsTripLog.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and JobType='HS'";
    }
    protected void grid_TripLog_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["LogDate"] = DateTime.Now;
        e.NewValues["LogTime"] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        e.NewValues["Status"] = "U";
    }
    protected void grid_TripLog_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["Driver"] = SafeValue.SafeString(e.NewValues["Driver"], "");
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"], "U");
        e.NewValues["LogDate"] = SafeValue.SafeDate(e.NewValues["LogDate"], new DateTime(1753, 1, 1));
        e.NewValues["LogTime"] = SafeValue.SafeString(e.NewValues["LogTime"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["JobType"] = "HS";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_TripLog_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Driver"] = SafeValue.SafeString(e.NewValues["Driver"], "");
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"], "U");
        e.NewValues["LogDate"] = SafeValue.SafeDate(e.NewValues["LogDate"], new DateTime(1753, 1, 1));
        e.NewValues["LogTime"] = SafeValue.SafeString(e.NewValues["LogTime"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_TripLog_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }


    #endregion
    protected void btn_charge_add_Click(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox txt_jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string JobNo = txt_jobNo.Text;
        string sql = string.Format(@"select count(*) from CTM_JobCharge where JobNo='{0}'", JobNo);
        int cc = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
        if (cc == 0)
        {
            sql = string.Format(@"insert into CTM_JobCharge (JobNo,ItemName,ItemType,Cost,CreateDateTime) values('{0}','charge1','','0',getdate()),('{0}','charge2','','0',getdate()),('{0}','charge3','','0',getdate())", JobNo);
            int r = ConnectSql_mb.ExecuteNonQuery(sql);
        }
    }
    protected void gv_charge_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobCharge));
        }
    }
    protected void gv_charge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsCharge.FilterExpression = "JobNo='" + JobNo + "'";
    }
    protected void gv_charge_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Cost"] = SafeValue.SafeDecimal(e.NewValues["Cost"], 0);
        e.NewValues["ItemName"] = SafeValue.SafeString(e.NewValues["ItemName"]);
    }
    protected void gv_charge_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gv_charge_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Add")
        {
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
            ASPxTextBox txt_jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            string JobNo = txt_jobNo.Text;
            string sql = string.Format(@"select count(*) from CTM_JobCharge where JobNo='{0}'", JobNo);
            int cc = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
            if (cc == 0)
            {
                sql = string.Format(@"insert into CTM_JobCharge (JobNo,ItemName,ItemType,Cost,CreateDateTime) values('{0}','charge1','','0',getdate()),('{0}','charge2','','0',getdate()),('{0}','charge3','','0',getdate())", JobNo);
                int r = ConnectSql_mb.ExecuteNonQuery(sql);
                if (r > 0)
                {
                    e.Result = "refresh";
                }
            }
        }
    }

    #region  Stock

    protected void gv_stock_Init(object sender, EventArgs e)
    {

        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobStock));
        }
    }
    protected void gv_stock_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsStock.FilterExpression = "JobNo='" + JobNo + "'";

    }
    protected void gv_stock_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["StockStatus"] = "IN";
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["StockQty"] = 0;
        e.NewValues["PackingQty"] = 0;
    }
    protected void gv_stock_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;

    }
    protected void gv_stock_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gv_stock_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["StockDescription"] = SafeValue.SafeString(e.NewValues["StockDescription"]);
        e.NewValues["StockMarking"] = SafeValue.SafeString(e.NewValues["StockMarking"]);
        e.NewValues["StockUnit"] = SafeValue.SafeString(e.NewValues["StockUnit"]);
        e.NewValues["PackingUnit"] = SafeValue.SafeString(e.NewValues["PackingUnit"]);
        e.NewValues["PackingDimention"] = SafeValue.SafeString(e.NewValues["PackingDimention"]);
    }

    #endregion

    #region Activity
    protected void gv_activity_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsActivity.FilterExpression = "JobNo='" + JobNo + "'";
    }
    #endregion

    #region close tab
    protected void ASPxGridView1_BeforePerformDataSelect(object sender, EventArgs e)
    {

        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string job_no = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");

        this.dsLogEvent.FilterExpression = " JobNo='" + job_no + "' and (JobStatus='USE' or JobStatus='CLS')";
    }
    #endregion

    #region Cargo
    protected void grid_wh_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsWh.FilterExpression = "JobNo='" + JobNo + "'";
    }
    protected void grid_wh_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.JobHouse));
        }
    }
    protected void grid_wh_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        if (cbb_JobType.Text == "WGR")
        {
            e.NewValues["CargoType"] = "IN";
        }
        e.NewValues["OpsType"] = "Storage";
        e.NewValues["Qty"] = 1;
        e.NewValues["ContNo"] = "";
        e.NewValues["ClientId"] = btn_ClientId.Text;
        e.NewValues["JobNo"] = txt_JobNo.Text;
        e.NewValues["JobType"] = SafeValue.SafeString(cbb_JobType.Value);
        e.NewValues["UomCode"] = " ";
        e.NewValues["PackTypeOrig"] = " ";
        e.NewValues["LandStatus"] = " ";
        e.NewValues["DgClass"] = " ";
        e.NewValues["CargoStatus"] = "P";
        e.NewValues["PackUom"] = " ";
        e.NewValues["LandStatus"] = "Normal";
        e.NewValues["DgClass"] = "Normal";
        e.NewValues["CargoStatus"] = "Pending";
        e.NewValues["DamagedStatus"] = "Normal";
        e.NewValues["PackUom"] = " ";
        e.NewValues["SkuCode"] = "GENERAL";
        e.NewValues["Location"] = "HOLDING";
    }
    protected void grid_wh_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        if (cbb_JobType.Text == "WGR")
        {
            e.NewValues["CargoType"] = "IN";
            e.NewValues["RefNo"] = txt_JobNo.Text;
        }
        if (cbb_JobType.Text == "WDO")
        {
            e.NewValues["CargoType"] = "OUT";
        }
        if (SafeValue.SafeString(e.NewValues["BookingNo"]) == "")
        {
            throw new Exception("Pls enter the Lot No");
        }
       
        e.NewValues["JobType"] = SafeValue.SafeString(cbb_JobType.Value);
        
        e.NewValues["JobNo"] = txt_JobNo.Text;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        if (SafeValue.SafeDecimal(e.NewValues["LengthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["WidthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["HeightPack"]) == 0)
        {
            e.NewValues["VolumeOrig"] = SafeValue.SafeDecimal(e.NewValues["VolumeOrig"]);
        }
        else
        {
            e.NewValues["VolumeOrig"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["LengthPack"]) * SafeValue.SafeDecimal(e.NewValues["WidthPack"]) * SafeValue.SafeDecimal(e.NewValues["HeightPack"]), 3);
        } 
        e.NewValues["LandStatus"] = SafeValue.SafeString(e.NewValues["LandStatus"]);
        e.NewValues["DgClass"] = SafeValue.SafeString(e.NewValues["DgClass"]);
        e.NewValues["DamagedStatus"] = SafeValue.SafeString(e.NewValues["DamagedStatus"]);
        e.NewValues["ContNo"] = SafeValue.SafeString(e.NewValues["ContNo"]);
        e.NewValues["HblNo"] = SafeValue.SafeString(e.NewValues["HblNo"]);
        e.NewValues["BookingNo"] = SafeValue.SafeString(e.NewValues["BookingNo"]);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["OpsType"] = SafeValue.SafeString(e.NewValues["OpsType"]);
        e.NewValues["UomCode"] = SafeValue.SafeString(e.NewValues["UomCode"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
        e.NewValues["SkuCode"] = SafeValue.SafeString(e.NewValues["SkuCode"]);
        e.NewValues["QtyOrig"] = SafeValue.SafeDecimal(e.NewValues["QtyOrig"]);
        e.NewValues["PackTypeOrig"] = SafeValue.SafeString(e.NewValues["PackTypeOrig"]);
        e.NewValues["WeightOrig"] = SafeValue.SafeDecimal(e.NewValues["WeightOrig"]);
        e.NewValues["PackQty"] = SafeValue.SafeDecimal(e.NewValues["PackQty"]);
    }
    protected void grid_wh_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        if (cbb_JobType.Text == "GR")
        {
            e.NewValues["CargoType"] = "IN";
        }
        if (cbb_JobType.Text == "DO")
        {
            e.NewValues["CargoType"] = "OUT";
        }
        if (SafeValue.SafeString(e.NewValues["BookingNo"]) == "")
        {
            throw new Exception("Pls enter the Lot No");
        }
        e.NewValues["JobType"] = SafeValue.SafeString(cbb_JobType.Value);
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        if (SafeValue.SafeDecimal(e.NewValues["LengthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["WidthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["HeightPack"]) == 0)
        {
            e.NewValues["VolumeOrig"] = SafeValue.SafeDecimal(e.NewValues["VolumeOrig"]);
        }
        else
        {
            e.NewValues["VolumeOrig"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["LengthPack"]) * SafeValue.SafeDecimal(e.NewValues["WidthPack"]) * SafeValue.SafeDecimal(e.NewValues["HeightPack"]), 3);
        }
        e.NewValues["RefNo"] = SafeValue.SafeString(e.NewValues["RefNo"]);
        e.NewValues["LandStatus"] = SafeValue.SafeString(e.NewValues["LandStatus"]);
        e.NewValues["DgClass"] = SafeValue.SafeString(e.NewValues["DgClass"]);
        e.NewValues["DamagedStatus"] = SafeValue.SafeString(e.NewValues["DamagedStatus"]);
        e.NewValues["ContNo"] = SafeValue.SafeString(e.NewValues["ContNo"]);
        e.NewValues["HblNo"] = SafeValue.SafeString(e.NewValues["HblNo"]);
        e.NewValues["BookingNo"] = SafeValue.SafeString(e.NewValues["BookingNo"]);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["OpsType"] = SafeValue.SafeString(e.NewValues["OpsType"]);
        e.NewValues["UomCode"] = SafeValue.SafeString(e.NewValues["UomCode"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
        e.NewValues["SkuCode"] = SafeValue.SafeString(e.NewValues["SkuCode"]);
        e.NewValues["QtyOrig"] = SafeValue.SafeDecimal(e.NewValues["QtyOrig"]);
        e.NewValues["PackTypeOrig"] = SafeValue.SafeString(e.NewValues["PackTypeOrig"]);
        e.NewValues["WeightOrig"] = SafeValue.SafeDecimal(e.NewValues["WeightOrig"]);
        e.NewValues["PackQty"] = SafeValue.SafeDecimal(e.NewValues["PackQty"]);
    }
    protected void grid_wh_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    public string FilePath(int id)
    {
        string sql = string.Format("select top 1 FilePath from CTM_Attachment where JobNo='{0}'", id);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    protected void grid_wh_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("Uploadline"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                ASPxTextBox txt_ContNo = grid.FindRowCellTemplateControl(rowIndex, null, "txt_ContNo") as ASPxTextBox;
                e.Result = txt_Id.Text + "_" + txt_ContNo.Text;
            }
            if (ar[0].Equals("Dimensionline"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                e.Result = txt_Id.Text;
            }
            if (ar[0].Equals("Copyonline"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + txt_Id.Text + "'");
                C2.JobHouse job = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
                if (job != null)
                {
                    job.SkuCode = job.BkgSKuCode;
                    job.PackQty = job.BkgSkuQty;
                    job.PackUom = job.BkgSkuUnit;
                    job.WeightOrig = job.Weight;
                    job.VolumeOrig = job.Volume;
                    C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(job);

                    e.Result = "Success";
                }
            }
            if (ar[0].Equals("CopyonCargoline"))
            {

                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                ASPxComboBox cbb_CargoCount = grid.FindRowCellTemplateControl(rowIndex, null, "cbb_CargoCount") as ASPxComboBox;
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + txt_Id.Text + "'");
                C2.JobHouse job = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
                if (job != null)
                {

                    int count = SafeValue.SafeInt(cbb_CargoCount.Value, 0);
                    for (int i = 0; i < count; i++)
                    {
                        C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(job);

                        job.LineId = job.Id;
                        C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                        C2.Manager.ORManager.PersistChanges(job);

                        Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(C2.Dimension), "HouseId='" + txt_Id.Text + "'");
                        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query1);
                        for (int j = 0; j < objSet.Count; j++)
                        {
                            C2.Dimension d = objSet[i] as C2.Dimension;
                            d.HouseId = job.Id;

                            C2.Manager.ORManager.StartTracking(d, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(d);

                        }
                    }

                    e.Result = "Success";
                }
            }
        }
    }
    protected void grid_wh_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        string sql = string.Format(@"select top 1 Id from job_house order by Id desc");
        int id = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);

        sql = string.Format(@"update job_house set LineId={0} where Id={0}", id);
        ConnectSql.ExecuteSql(sql);
    }
    #endregion

    #region Quotetion
    protected void Grid1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobRate));
        }
    }
    protected void Grid1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Price"] = (decimal)0;
        e.NewValues["Unit"] = "";
        e.NewValues["Remark"] = " ";
        e.NewValues["MinAmt"] = (decimal)0;
        e.NewValues["Qty"] = (decimal)1;
        e.NewValues["CurrencyId"] = "SGD";
        e.NewValues["ExRate"] = 1;
        ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        e.NewValues["JobNo"] = txt_QuoteNo.Text;
    }
    protected void Grid1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;


        string chgCode = Helper.Safe.SafeString(e.NewValues["ChgCode"]).Trim();
        string sql = string.Format(@"select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", chgCode);
        string chgcodeDes = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        e.NewValues["ChgCode"] = chgCode;
        e.NewValues["ChgCodeDe"] = chgcodeDes;
        e.NewValues["LineStatus"] = Helper.Safe.SafeString(e.NewValues["LineStatus"]).Trim();
        e.NewValues["Unit"] = Helper.Safe.SafeString(e.NewValues["Unit"]).Trim();
        e.NewValues["ContType"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim();
        if (Helper.Safe.SafeString(e.NewValues["ContType"]).Trim() != "")
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim().Substring(0, 2);
        }
        else
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContSize"]).Trim();
        }
        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["BillScope"] = Helper.Safe.SafeString(e.NewValues["BillScope"]).Trim();
        e.NewValues["BillType"] = Helper.Safe.SafeString(e.NewValues["BillType"]).Trim();
        e.NewValues["BillClass"] = Helper.Safe.SafeString(e.NewValues["BillClass"]).Trim();
        e.NewValues["RowCreateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowCreateTime"] = DateTime.Now;
        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;
        e.NewValues["LumsumInd"] = Helper.Safe.SafeString(e.NewValues["LumsumInd"]).Trim();
        e.NewValues["ChgCodeDe"] = chgcodeDes;

        ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        e.NewValues["JobNo"] = txt_QuoteNo.Text;
    }
    protected void Grid1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["LumsumInd"] = Helper.Safe.SafeString(e.NewValues["LumsumInd"]).Trim();
        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;
        e.NewValues["LineStatus"] = Helper.Safe.SafeString(e.NewValues["LineStatus"]).Trim();
        e.NewValues["Unit"] = Helper.Safe.SafeString(e.NewValues["Unit"]).Trim();
        e.NewValues["ContType"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim();
        if (Helper.Safe.SafeString(e.NewValues["ContType"]).Trim() != "")
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim().Substring(0, 2);
        }
        else
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContSize"]).Trim();
        }
        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["BillScope"] = Helper.Safe.SafeString(e.NewValues["BillScope"]).Trim();
        e.NewValues["BillType"] = Helper.Safe.SafeString(e.NewValues["BillType"]).Trim();
        e.NewValues["BillClass"] = Helper.Safe.SafeString(e.NewValues["BillClass"]).Trim();
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        string chgCode = Helper.Safe.SafeString(e.NewValues["ChgCode"]).Trim();
        string sql = string.Format(@"select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", chgCode);
        string chgcodeDes = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        e.NewValues["ChgCode"] = chgCode;
        if (Helper.Safe.SafeString(e.NewValues["ChgCodeDe"]).Trim() == "")
            e.NewValues["ChgCodeDe"] = chgcodeDes;
        else
            e.NewValues["ChgCodeDe"] = Helper.Safe.SafeString(e.NewValues["ChgCodeDe"]);
        ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        e.NewValues["JobNo"] = txt_QuoteNo.Text;
    }
    protected void Grid1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid1_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select QuoteNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        ds1.FilterExpression = "JobNo='" + JobNo + "' and len(JobNo)>0";
    }
    protected void grid1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string para = e.Parameters;
        if (para == "OK")
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i].id;
                    string sql = string.Format(@"delete from job_rate where Id={0}", id);
                    ConnectSql_mb.ExecuteNonQuery(sql);
                    e.Result = "DeleteAll";
                }
            }
            else
            {
                e.Result = "Delete Fail";
            }
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public Record(int _id)
        {
            id = _id;
        }
    }
    private void OnLoad()
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        if (pageControl != null)
        {
            ASPxGridView grid = pageControl.FindControl("grid1") as ASPxGridView;
            if (grid != null)
            {
                int start = 0;
                int end = grid.VisibleRowCount;
                for (int i = start; i < end; i++)
                {

                    ASPxCheckBox isPay = grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)grid.Columns["Id"], "ack_IsDel") as ASPxCheckBox;
                    //ASPxTextBox id = grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)grid.Columns["Id"], "txt_Id") as ASPxTextBox;

                    if (isPay != null && isPay.Checked)
                    {
                        object id = grid.GetRowValues(i, "Id");
                        list.Add(new Record(SafeValue.SafeInt(id, 0)));
                    }
                    else if (isPay == null)
                        break;
                }
            }
        }
    }
    #endregion

    private void Event_Log(string jobNo, string level, int c, int id, string status)
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;

        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        if (level == "JOB")
        {
            elog.ActionLevel_isJOB(id);
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Job, c, status);
        }
        if (level == "QUOTATION")
        {
            elog.ActionLevel_isJOB(id);
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Quotation, c, status);
        }
        if (level == "CONT")
        {
            elog.ActionLevel_isCONT(id);
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Container, c, status);
        }
        if (level == "TRIP")
        {
            elog.ActionLevel_isTRIP(id);
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Trip, c, status);
        }
        elog.log();

    }

    
}