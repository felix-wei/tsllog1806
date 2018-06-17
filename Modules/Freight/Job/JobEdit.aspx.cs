using Aspose.Cells;
using C2;
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

public partial class Modules_Freight_Job_JobEdit : System.Web.UI.Page
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
                int n=SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql),0);
                if (n > 0)
                {
                    Session["CTM_Job_" + txt_search_JobNo.Text] = " JobNo='" + txt_search_JobNo.Text + "'";
                    this.dsJob.FilterExpression = " JobNo='" + txt_search_JobNo.Text + "'";
                    if (this.grid_job.GetRow(0) != null)
                        this.grid_job.StartEdit(0);
                }
                else {
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
        e.NewValues["IsWarehouse"] = "No";
		e.NewValues["Pod"] = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        e.NewValues["WareHouseCode"] = System.Configuration.ConfigurationManager.AppSettings["Warehosue"];
	}
	protected void grid_job_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
	{
		string s = e.Parameters;
        ASPxPageControl pageControl = grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox orderNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
		if (s == "Void")
		{
			e.Result = job_void();
		}
		if (s == "Save")
		{
             Save();
		}
		if (s == "Close")
		{
			e.Result = job_close();
            
		}
        if (e.Parameters == "EmailToHaulier")
        {
            #region Send Email
            ASPxTextBox txt_Haulier = pageControl.FindControl("txt_Haulier") as ASPxTextBox;
            ASPxGridView grid = sender as ASPxGridView;
            string sql = string.Format(@"select * from CTM_Job where JobNo='{0}'", SafeValue.SafeString(orderNo.Text));
            DataTable dt = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                #region Action For Send

                bool action = false;
                DataTable tab_inv = ConnectSql.GetTab(sql);
                string path1 = string.Format("~/files/pdf/");
                string path2 = path1.Replace(' ', '_').Replace('\'', '_');
                string pathx = path2.Substring(1);
                string path3 = MapPath(path2);
                if (!Directory.Exists(path3))
                    Directory.CreateDirectory(path3);
                string p = string.Format(@"~\files\pdf\{0}_HaulierAdvice.pdf", orderNo.Text);

                string e_file = HttpContext.Current.Server.MapPath(p);

                MemoryStream ms = new MemoryStream();
                XtraReport rpt = new XtraReport();
                rpt.LoadLayout(Server.MapPath(@"~\ReportCfs\Report\HaulierTruckingAdvice_Imp.repx"));
                rpt.DataSource = DocPrint.PrintImpHaulier(orderNo.Text);

                System.IO.MemoryStream str = new MemoryStream();
                rpt.CreateDocument();
                rpt.ExportToPdf(e_file);

                string company = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                string address1 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"];
                string address2 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"];
                string address3 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
                sql = string.Format(@"select Email1,Email2,Name from xxparty where PartyId='{0}'", txt_Haulier.Text);
                DataTable tab = ConnectSql.GetTab(sql);
                string add = address1 + " " + address2 + " " + address3;
                string title = "Haulier Advice";
                if (tab.Rows.Count > 0)
                {
                    string email1 = SafeValue.SafeString(tab.Rows[0]["Email1"]);
                    string email2 = SafeValue.SafeString(tab.Rows[0]["Email2"]);
                    string name = SafeValue.SafeString(tab.Rows[0]["Name"]);
                    string mes = string.Format(@"<b>{0}</b><br><br>
{1}<br><br>
<b>Dear Customer, <br><br>Kindly review attached document for  HAULIER ADVICE.</b>
<br><br>
<b>This is a computer generated email, please DO NOT reply.
<br><br>
Pls email to : xglogistic@ugroup.com.sg for any questions.
</b><br><br>
***IMPORTANT NOTICE***<br><br>

1.     Pls wait for our delivery department to call you for delivery arrangement<br><br>

2.     Delivery arrangement will be made within 3 working days from the date of unstuffing of container.<br><br>
<br><br>", company, add);
                    if (email1.Length > 0)
                    {
                        Helper.Email.SendEmail(email1, "huang@cargoerp", "", title, mes, p);
                        //Helper.Email.SendEmail("huang@cargoerp.com", "99915945@qq.com,ymyg1985318@163.com", "", title, mes, p);
                        action = true;
                    }
                    if (email2.Length > 0)
                    {
                        if (action)
                            Helper.Email.SendEmail(email2, "", "", title, mes, p);
                        else
                            Helper.Email.SendEmail(email2, "huang@cargoerp", "", title, mes, p);
                    }
                    //"huang@cargoerp", ""
                    e.Result = "Success";
                }

                #endregion

            }
            #endregion
        }
        if (e.Parameters == "EmailToCarrier")
        {
            #region Send Email
            ASPxTextBox txt_Carrier = pageControl.FindControl("txt_Carrier") as ASPxTextBox;
            ASPxGridView grid = sender as ASPxGridView;
            string sql = string.Format(@"select * from CTM_Job where JobNo='{0}'", SafeValue.SafeString(orderNo.Text));
            DataTable dt = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                #region Action For Send

                bool action = false;
                DataTable tab_inv = ConnectSql.GetTab(sql);
                string path1 = string.Format("~/files/pdf/");
                string path2 = path1.Replace(' ', '_').Replace('\'', '_');
                string pathx = path2.Substring(1);
                string path3 = MapPath(path2);
                if (!Directory.Exists(path3))
                    Directory.CreateDirectory(path3);
                string p = string.Format(@"~\files\pdf\{0}_AuthLetter.pdf", orderNo.Text);

                string e_file = HttpContext.Current.Server.MapPath(p);

                MemoryStream ms = new MemoryStream();
                XtraReport rpt = new XtraReport();
                rpt.LoadLayout(Server.MapPath(@"~\ReportCfs\Report\AuthLetter.repx"));
                //rpt.DataSource = DocPrint.PrintAuthLetter(orderNo.Text);

                System.IO.MemoryStream str = new MemoryStream();
                rpt.CreateDocument();
                rpt.ExportToPdf(e_file);

                string company = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                string address1 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"];
                string address2 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"];
                string address3 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
                sql = string.Format(@"select Email1,Email2,Name from xxparty where PartyId='{0}'", txt_Carrier.Text);
                DataTable tab = ConnectSql.GetTab(sql);
                string add = address1 + " " + address2 + " " + address3;
                string title = "AUTHORISATION LETTER";
                if (tab.Rows.Count > 0)
                {
                    string email1 = SafeValue.SafeString(tab.Rows[0]["Email1"]);
                    string email2 = SafeValue.SafeString(tab.Rows[0]["Email2"]);
                    string name = SafeValue.SafeString(tab.Rows[0]["Name"]);
                    string mes = string.Format(@"<b>{0}</b><br><br>
{1}<br><br>
<b>Dear Customer, <br><br>Kindly review attached document for AUTHORISATION LETTER.</b>
<br><br>
<b>This is a computer generated email, please DO NOT reply.
<br><br>
Pls email to : xglogistic@ugroup.com.sg for any questions.
</b><br><br>
***IMPORTANT NOTICE***<br><br>

1.     Pls wait for our delivery department to call you for delivery arrangement<br><br>

2.     Delivery arrangement will be made within 3 working days from the date of unstuffing of container.<br><br>
<br><br>", company, add);
                    if (email1.Length > 0)
                    {
                        Helper.Email.SendEmail(email1, "huang@cargoerp", "", title, mes, p);
                        //Helper.Email.SendEmail("huang@cargoerp.com", "", "", title, mes, p);
                        action = true;
                    }
                    if (email2.Length > 0)
                    {
                        if (action)
                            Helper.Email.SendEmail(email2, "", "", title, mes, p);
                        else
                            Helper.Email.SendEmail(email2, "huang@cargoerp.com", "", title, mes, p);
                    }
                    //"huang@cargoerp", ""
                    e.Result = "Success";
                }

                #endregion

            }
            #endregion
        }
        else if (s== "SEND")
        {
            #region Send Email
            ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
            ASPxGridView grid = sender as ASPxGridView;
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
<b>Dear Customer, <br><br>Kindly review attached document for inovice.</b>
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

	}
    private string create_cargo()
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxLabel lbl_JobType = this.grid_job.FindEditFormTemplateControl("lbl_JobType") as ASPxLabel;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        string sql = string.Format(@"select ContainerNo,ContainerType from ctm_jobdet1 where JobNo='{0}'", txt_JobNo.Text);
        DataTable dt=ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row=dt.Rows[i];
            string contNo =SafeValue.SafeString(row["ContainerNo"]);
            C2.JobHouse house = new JobHouse();

            house.CargoType = "IN";
            house.CargoStatus = "Pending";
            house.JobNo = txt_JobNo.Text;
            house.ContNo = contNo;
            house.HblNo = "";
            house.BookingNo = txt_JobNo.Text + contNo;
            house.JobType = lbl_JobType.Text;
            house.QtyOrig = 1;
            house.PackQty = 0;
            house.Weight = 0;
            house.Volume = 0;
            house.WeightOrig = 0;
            house.VolumeOrig = 0;
            house.LandStatus = "Normal";
            house.DgClass = "Normal";
            house.DamagedStatus = "Normal";
            house.LengthPack = 0;
            house.WidthPack = 0;
            house.HeightPack = 0;
            house.RefNo = txt_JobNo.Text;
            house.Qty = 1;
            house.OpsType = "Delivery";
            house.Location = "HOLDING";
            house.SkuCode = "GENERAL";
            house.ClientId = btn_ClientId.Text;
            C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(house);
        }
        return "Action Success!";
    }
    private string copy_job() {
        string quoteNo = "";
        ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "QuoteNo='" + txt_QuoteNo.Text + "'");
        C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
        if(ctmJob!=null){
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

            SetQuotation(txt_QuoteNo.Text,quoteNo,ctmJob.ClientId,ctmJob.JobType);

        }
        return "Q_"+quoteNo;
    }
    private void SetQuotation(string no,string quoteNo, string clientId, string jobType)
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
		string user = HttpContext.Current.User.Identity.Name;
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
            log.ActionLevel_isJOB(SafeValue.SafeInt(Id.Text,0));

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
            int jobId = SafeValue.SafeInt(Id.Text,0);
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;
            elog.ActionLevel_isJOB(jobId);
            elog.setActionLevel(jobId, CtmJobEventLogRemark.Level.Job, 5);
            elog.log();
			return "";
		}

		return "error";
	}
    public string ValidateJob()
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        string ret = "";
        //Vessel/Voy/POL/POD/clientcode/clientref/clientpic/carrier/oceanbl
        ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
        ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
        ASPxTextBox pol = pageControl.FindControl("txt_Pol") as ASPxTextBox;
        ASPxTextBox custCode = pageControl.FindControl("txt_CustCode") as ASPxTextBox;
        ASPxTextBox carrier = pageControl.FindControl("txt_Carrier") as ASPxTextBox;
        ASPxTextBox oceanBl = pageControl.FindControl("txt_OceanBl") as ASPxTextBox;
        ASPxTextBox impRefNo = pageControl.FindControl("txt_ImpRefNo") as ASPxTextBox;
        ASPxTextBox byWho = pageControl.FindControl("txt_ByWho") as ASPxTextBox;

        if (ves.Text.Trim() == "")
            ret += "Please Fill in Vessel" + "\r\n";
        if (voy.Text.Trim() == "")
            ret += "Please Fill in Voyage" + "\r\n";
        if (pol.Text.Trim() == "")
            ret += "Please Fill in Pol" + "\r\n";
        int cnt = Helper.Safe.SafeInt(Helper.Sql.One("select count(*) from xxport where code='" + pol.Text.Trim() + "'"));
        if (cnt != 1)
            ret += "Please Fill in Valid Pol Code" + "\r\n";
        if (custCode.Text.Trim() == "")
            ret += "Please Fill in Customer" + "\r\n";
        if (impRefNo.Text.Trim() == "")
            ret += "Please Fill in Customer Ref" + "\r\n";
        if (byWho.Text.Trim() == "")
            ret += "Please Fill in Customer PIC" + "\r\n";
        if (carrier.Text.Trim() == "")
            ret += "Please Fill in Carrier" + "\r\n";
        if (oceanBl.Text.Trim() == "")
            ret += "Please Fill in Carrier Ref/Bl" + "\r\n";


        return ret;
    }	
    private void Save()
    {
        //
        string vali = ValidateJob();
        if (vali != "")
        {
            throw new Exception(vali);
        }

        try
        {
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox orderNo = pageControl.FindControl("txt_OrderNo") as ASPxTextBox;
            ASPxDateEdit eta = pageControl.FindControl("date_Eta") as ASPxDateEdit;






            string refN = orderNo.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + refN + "'");
            C2.CtmJob jo = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
            bool isNew = false;
            if (jo == null)
            {
                jo = new C2.CtmJob();
                isNew = true;
                refN = C2Setup.GetNextNo("I", "CI", eta.Date);
                jo.JobNo = refN;
                jo.JobType = "I";
                jo.CreateBy = HttpContext.Current.User.Identity.Name;
                jo.CreateDateTime = DateTime.Now;
                jo.JobStatus = "Pending";
            }

            ASPxComboBox orderType = pageControl.FindControl("cmb_OrderType") as ASPxComboBox;
            jo.OrderType = orderType.Text;
            ASPxTextBox termi = pageControl.FindControl("txt_Terminal") as ASPxTextBox;
            jo.Terminalcode = termi.Text;
            ASPxTextBox party = pageControl.FindControl("txt_Party") as ASPxTextBox;
            jo.ClientId = party.Text;

            ASPxComboBox fileClosed = pageControl.FindControl("cmb_FileClosed") as ASPxComboBox;
            jo.JobStatus = fileClosed.Text;

            
            //ASPxTextBox partyCode = pageControl.FindControl("txt_PartyCode") as ASPxTextBox;
            //jo.PartyCode = "";//partyCode.Text;
            ASPxDateEdit truckOn = pageControl.FindControl("date_TruckOn") as ASPxDateEdit;
            jo.CodDate = truckOn.Date;
            ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
            jo.Vessel = ves.Text;
            ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
            jo.Voyage = voy.Text;
            ASPxTextBox pol = pageControl.FindControl("txt_Pol") as ASPxTextBox;
            jo.Pol = pol.Text;
            jo.EtaDate = eta.Date;
            ASPxTextBox etaTime = pageControl.FindControl("txt_EtaTime") as ASPxTextBox;
            jo.EtaTime = etaTime.Text;
            
            ASPxMemo specInstruction = pageControl.FindControl("txt_SpecInstruction") as ASPxMemo;//special intruction
            jo.SpecialInstruction = specInstruction.Text;
            ASPxMemo collectTo = pageControl.FindControl("txt_CollectTo") as ASPxMemo;
            jo.YardRef = collectTo.Text;
            ASPxMemo condition = pageControl.FindControl("txt_Condition") as ASPxMemo;
            jo.AdditionalRemark = condition.Text;
            ASPxMemo rmk = pageControl.FindControl("txt_Rmk") as ASPxMemo;
            jo.TerminalRemark = rmk.Text;


            ASPxTextBox custCode = pageControl.FindControl("txt_CustCode") as ASPxTextBox;
            jo.PartyId = custCode.Text;
            ASPxTextBox agentId = pageControl.FindControl("txt_AgtCode") as ASPxTextBox;
            jo.AgentId = agentId.Text;
            ASPxDateEdit jobDate = pageControl.FindControl("date_JobDate") as ASPxDateEdit;
            jo.JobDate = jobDate.Date;
            ASPxTextBox issuedBy = pageControl.FindControl("txt_IssuedBy") as ASPxTextBox;
            jo.IssuedBy = issuedBy.Text;
            ASPxTextBox tallyDone = pageControl.FindControl("txt_TallyDone") as ASPxTextBox;
            jo.TallyDone = tallyDone.Text;
            ASPxTextBox entryBy = pageControl.FindControl("txt_EntryBy") as ASPxTextBox;
            jo.UpdateBy = entryBy.Text;
            ASPxTextBox carrier = pageControl.FindControl("txt_Carrier") as ASPxTextBox;
            jo.CarrierId = carrier.Text;
            ASPxTextBox oceanBl = pageControl.FindControl("txt_OceanBl") as ASPxTextBox;
            jo.CarrierBkgNo = oceanBl.Text;
            ASPxTextBox txt_BookingNo2 = pageControl.FindControl("txt_BookingNo2") as ASPxTextBox;
            jo.CarrierBlNo = txt_BookingNo2.Text;
            ASPxTextBox impRefNo = pageControl.FindControl("txt_ImpRefNo") as ASPxTextBox;
            jo.ClientRefNo = impRefNo.Text;
            ASPxTextBox byWho = pageControl.FindControl("txt_ByWho") as ASPxTextBox;
            jo.ClientContact = byWho.Text;
            ASPxTextBox temail = pageControl.FindControl("txt_Email") as ASPxTextBox;
            jo.EmailAddress = temail.Text;
            ASPxTextBox haulier = pageControl.FindControl("txt_Haulier") as ASPxTextBox;
            jo.HaulierId = haulier.Text;
            ASPxMemo truckRmk = pageControl.FindControl("txt_TruckingRmk") as ASPxMemo;
            jo.InternalRemark = truckRmk.Text;

            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(jo);
                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isWeb();
                elog.Controller = userId;

                elog.setActionLevel(jo.Id, CtmJobEventLogRemark.Level.Job, 1);
                elog.ActionLevel_isJOB(jo.Id);
                elog.log();

            }
            else
            {

                string old_state = Helper.Safe.SafeString(Helper.Sql.One("select StatusCode from ctm_job where JobNo='" + jo.JobNo + "'"));

                C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(jo);

                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isWeb();
                elog.Controller = userId;

                elog.setActionLevel(jo.Id, CtmJobEventLogRemark.Level.Job, 3);
                elog.ActionLevel_isJOB(jo.Id);
                elog.log();
                if (jo.StatusCode == "Confirmed" && jo.StatusCode != old_state)
                {
                    //SendTally(jo.No, jo.CustCode);
                }

            }


            if (isNew)
            {
                C2Setup.SetNextNo("I", "CI", jo.JobNo, eta.Date);
                Session["CfsImp"] = "JobNo='" + refN + "'";
                this.dsJob.FilterExpression = "JobNo='" + refN + "'";

                // orderNo.Text = refN;
                if (this.grid_job.GetRow(0) != null)
                    this.grid_job.StartEdit(0);
            }




        }
        catch (Exception ex)
        { throw new Exception(ex.Message + ex.StackTrace); }
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
        if (s == "Save")
            Save();
        if (s.Substring(0, 4) == "NOTI")
        {
            string job_no = s.Substring(4).Trim();

            SendTally(job_no);
            return;
        }

	}
    private string SendTally(string no)
    {
        string t = "nt";
        string n = "tsimp";
        string m = no;
        string h = "0";
        string s = "0";
        string a = "";
        string k1 = "";
        string k2 = "";
        string d1 = "";
        string d2 = "";
        string p = string.Format(@"~\doc\src\{0}\{1}_{2}_{3}_{4}_{5:yyyyMMdd_HHmmss}.pdf", t, n, t, m, h, DateTime.Now);
        string e_file = HttpContext.Current.Server.MapPath(p);

        DataTable dt = Helper.Sql.List("Select * from CTM_Job where JobNo='" + no + "'");
        if (dt.Rows.Count != 1)
            return "";
        DataRow dr = dt.Rows[0];
        string cust = Helper.Safe.SafeString(dr["CustCode"]);
        string status = Helper.Safe.SafeString(dr["FileClosed"]);
        if (status != "Confirmed")
            throw new Exception("Job Not Confirmed");




        try
        {

            XtraReport rpt = new XtraReport();
            rpt.LoadLayout(Server.MapPath(@"~\ReportCfs\Report\TallySheet_Imp_Pre3.repx"));
            //rpt.DataSource = DocPrint.DsImpTs_Pre(no, "CFSWHS");
            rpt.CreateDocument();
            rpt.ExportToPdf(e_file);


            string send_to = Helper.Safe.SafeString(dr["CollectTo2"]).Trim();
            if (send_to.Length < 10)
                return "";
            string send_cc = "";

            string _pod = Helper.Safe.SafeString(Helper.Sql.One(
                    string.Format("Select Name from xxPort where code='{0}'", dr["Pol"])
                ));
            if (_pod.Trim() == "")
                _pod = Helper.Safe.SafeString(dr["Pol"]);

            string subject = string.Format(@"WAREHOUSE Unstuffing Tallysheet : {1} V.{2} (ETA: {3:dd/MM/yyyy} POL: {0})",
            _pod, dr["VesselNo"], dr["VoyNo"],
                dr["Eta"], no
            );

            string content = string.Format(@"Dear Customer, <br><br>Kindly review attached document for import tallysheet notification.<br><br>
		<table border=2 width=400>
		<tr><td>WAREHOUSE Job No</td><td>{1}</td></tr>
		<tr><td>Your Ref No</td><td>{2}</td></tr>
		<tr><td>Vessel/Voy</td><td>{3} V.{4}</td></tr>
		<tr><td>Eta Singapore</td><td>{5:dd/MM/yyyy}</td></tr>
		<tr><td>Port of Loading</td><td>{6}</td></tr>
		</table>
		<br><br>Best Regards<br><br>  (CFS)<br>", "",
                dr["JobNo"],
                dr["ImpRefNo"],
                dr["VesselNo"], dr["VoyNo"],
                dr["Eta"],
                    _pod
            );
            // string content = "Dear Customer, \r\n\r\nKindly review attached document for import tallysheet notification.\r\nBest Regards\r\n\r\nWAREHOUSE";
            //send_to = "admin@cargo.ms";
            send_cc = "";
            //sendEmail();
            string err = Helper.Email.SendEmail(send_to,
                send_cc,
                "admin@cargo.ms",
                "Import Tallysheet Confirmation : " + no,
                 content,
                e_file);
        }
        catch (Exception ex)
        {
            //return "Error";
            throw new Exception(ex.Message + ex.StackTrace);
        }

        return "";
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
            ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
            ASPxButton btn_Confirm = this.grid_job.FindEditFormTemplateControl("btn_Confirm") as ASPxButton;
            ASPxButton btn_QuoteVoid = this.grid_job.FindEditFormTemplateControl("btn_QuoteVoid") as ASPxButton;
			//partyName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "PartyId" }));
            //txt_CarrierName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "CarrierId" }));
            //txt_ClientName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "ClientId" }));
            //txt_HaulierName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "HaulierId" }));

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
            string jobType = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "JobType" }), "IMP");
            string jobStatus = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "JobStatus" }), "IMP");
           
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
                    //cbb_Email1.Items.Insert(i, item);
                    //}
                }
            }
            #endregion
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
    private void job_cost() {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        string sql_rate = string.Format(@"select ChgCode,ChgCodeDes,Qty,Price,BillClass,BillScope,CurrencyId,ExRate from job_rate where ClientId='{0}' and JobNo='-1' and BillScope='JOB' and BillClass='TRUCKING'", btn_ClientId.Text);
        DataTable dt_rate = ConnectSql.GetTab(sql_rate);
        for (int i = 0; i < dt_rate.Rows.Count; i++)
        {
            #region Job
            string chgCode = SafeValue.SafeString(dt_rate.Rows[i]["ChgCode"]);
            string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeId='{0}'", chgCode);
            DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
            decimal gst = 0;
            string gstType = "";
            string chgTypeId = "";
            if (dt_chgCode.Rows.Count > 0)
            {
                gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
            }
            string chgCodeDes = SafeValue.SafeString(dt_rate.Rows[i]["ChgCodeDes"]);
            decimal price = SafeValue.SafeDecimal(dt_rate.Rows[i]["Price"]);
            decimal qty = SafeValue.SafeDecimal(dt_rate.Rows[i]["Qty"]);
            string scope = SafeValue.SafeString(dt_rate.Rows[i]["BillScope"]);
            string sql_cost = string.Format(@"select count(*) from job_cost where JobNo='{0}' and ChgCode='{1}'", txt_JobNo.Text, chgCode);
            int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_cost), 0);
            if (n == 0)
            {
                C2.Job_Cost cost = new C2.Job_Cost();
                cost.JobNo = txt_JobNo.Text;
                cost.ChgCode = chgCode;
                cost.ChgCodeDe = chgCodeDes;
                cost.ContNo = "";
                cost.ContType = "";
                cost.Price = price;
                cost.Qty = 1;
                cost.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                cost.ExRate = new decimal(1.0);
                cost.LineType = scope;
                decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
                cost.LocAmt = locAmt;
                C2.Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(cost);
            }
            #endregion
        }
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
		e.NewValues["UrgentInd"] = "No";
		e.NewValues["F5Ind"] = "No";
        e.NewValues["StatusCode"] = "SCHEDULED";
		e.NewValues["Permit"] = "";
		e.NewValues["YardAddress"] ="";
		e.NewValues["EmailInd"] = "No";
        e.NewValues["ContainerCategory"] = "Normal";
		
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
            cont.EmailInd = SafeValue.SafeString(cbb_EmailInd.Value,"N");

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

                Event_Log(cont.JobNo, "CONT",1,cont.Id,"");
            }
            else
            {
                C2.Manager.ORManager.StartTracking(cont, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(cont);
                Event_Log(cont.JobNo, "CONT",3, cont.Id,"");
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
            Trip_Update(sender, e);
        }
    }
    #endregion

    #region Trip
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
        this.dsTrip.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }

    protected void grid_Trip_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxMemo PickupFrom = pageControl.FindControl("txt_PickupFrom") as ASPxMemo;
        ASPxMemo DeliveryTo = pageControl.FindControl("txt_DeliveryTo") as ASPxMemo;
        string P_From = PickupFrom.Text;
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
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = @"select det1.Id,det1.ContainerNo,det1.ContainerType from CTM_JobDet1 as det1 left outer join CTM_Job as job on det1.JobNo=job.JobNo where job.Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxGridView grid_Trip = pageControl.FindControl("grid_Trip") as ASPxGridView;
        ASPxDropDownEdit dde_contNo = grid_Trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        ASPxGridView gvlist = dde_contNo.FindControl("gridPopCont") as ASPxGridView;
        gvlist.DataSource = C2.Manager.ORManager.GetDataSet(sql);
        gvlist.DataBind();
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
            Trip_Update(sender, e);
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

    private void Trip_Update(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
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

        ASPxDropDownEdit dde_Trip_ContNo = grd.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        ASPxTextBox dde_Trip_ContId = grd.FindEditFormTemplateControl("dde_Trip_ContId") as ASPxTextBox;
        //ASPxButtonEdit btn_CfsCode = grd.FindEditFormTemplateControl("btn_CfsCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_BayCode = grd.FindEditFormTemplateControl("cbb_Trip_BayCode") as ASPxComboBox;
        //ASPxComboBox cbb_Carpark = grd.FindEditFormTemplateControl("cbb_Carpark") as ASPxComboBox;
        ASPxComboBox cbb_Trip_TripCode = grd.FindEditFormTemplateControl("cbb_Trip_TripCode") as ASPxComboBox;

        ASPxButtonEdit btn_DriverCode = grd.FindEditFormTemplateControl("btn_DriverCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_TowheadCode = grd.FindEditFormTemplateControl("btn_TowheadCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_ChessisCode = grd.FindEditFormTemplateControl("btn_ChessisCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_SubletFlag = grd.FindEditFormTemplateControl("cbb_Trip_SubletFlag") as ASPxComboBox;
        //ASPxTextBox txt_SubletHauliername = grd.FindEditFormTemplateControl("txt_SubletHauliername") as ASPxTextBox;
        //ASPxComboBox cbb_StageCode = grd.FindEditFormTemplateControl("cbb_StageCode") as ASPxComboBox;
        //ASPxComboBox cbb_StageStatus = grd.FindEditFormTemplateControl("cbb_StageStatus") as ASPxComboBox;
        ASPxComboBox cbb_Trip_StatusCode = grd.FindEditFormTemplateControl("cbb_Trip_StatusCode") as ASPxComboBox;
        ASPxComboBox cmb_DoubleMounting = grd.FindEditFormTemplateControl("cmb_DoubleMounting") as ASPxComboBox;
        ASPxDateEdit txt_FromDate = grd.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_fromTime = grd.FindEditFormTemplateControl("txt_Trip_fromTime") as ASPxTextBox;
        ASPxDateEdit date_Trip_toDate = grd.FindEditFormTemplateControl("date_Trip_toDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_toTime = grd.FindEditFormTemplateControl("txt_Trip_toTime") as ASPxTextBox;
        ASPxMemo txt_Trip_Remark = grd.FindEditFormTemplateControl("txt_Trip_Remark") as ASPxMemo;
        ASPxMemo txt_Trip_FromCode = grd.FindEditFormTemplateControl("txt_Trip_FromCode") as ASPxMemo;
        ASPxMemo txt_Trip_ToCode = grd.FindEditFormTemplateControl("txt_Trip_ToCode") as ASPxMemo;
        //ASPxSpinEdit spin_Price = grd.FindEditFormTemplateControl("spin_Price") as ASPxSpinEdit;
        ASPxComboBox cbb_zone = grd.FindEditFormTemplateControl("cbb_zone") as ASPxComboBox;
        //ASPxSpinEdit spin_Incentive1 = grd.FindEditFormTemplateControl("spin_Incentive1") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Incentive2 = grd.FindEditFormTemplateControl("spin_Incentive2") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Incentive3 = grd.FindEditFormTemplateControl("spin_Incentive3") as ASPxSpinEdit;
        //ASPxComboBox cbb_Incentive4 = grd.FindEditFormTemplateControl("cbb_Incentive4") as ASPxComboBox;

        //ASPxComboBox cbb_Overtime = grd.FindEditFormTemplateControl("cbb_Overtime") as ASPxComboBox;
        ASPxMemo txt_driver_remark = grd.FindEditFormTemplateControl("txt_driver_remark") as ASPxMemo;
        //ASPxComboBox cbb_OverDistance = grd.FindEditFormTemplateControl("cbb_OverDistance") as ASPxComboBox;

        ASPxTextBox fromPackingLot = grd.FindEditFormTemplateControl("txt_FromPL") as ASPxTextBox;
        ASPxTextBox toPackingLot = grd.FindEditFormTemplateControl("txt_ToPL") as ASPxTextBox;
        //check_Trip_Status("0", trip.DriverCode,trip.Statuscode);

        //ASPxSpinEdit spin_Charge1 = grd.FindEditFormTemplateControl("spin_Charge1") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge2 = grd.FindEditFormTemplateControl("spin_Charge2") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge3 = grd.FindEditFormTemplateControl("spin_Charge3") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge4 = grd.FindEditFormTemplateControl("spin_Charge4") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge5 = grd.FindEditFormTemplateControl("spin_Charge5") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge6 = grd.FindEditFormTemplateControl("spin_Charge6") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge7 = grd.FindEditFormTemplateControl("spin_Charge7") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge8 = grd.FindEditFormTemplateControl("spin_Charge8") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge9 = grd.FindEditFormTemplateControl("spin_Charge9") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge10 = grd.FindEditFormTemplateControl("spin_Charge10") as ASPxSpinEdit;


        trip.ContainerNo = SafeValue.SafeString(dde_Trip_ContNo.Value);
        trip.Det1Id = SafeValue.SafeInt(dde_Trip_ContId.Text, 0);
        //trip.CfsCode = SafeValue.SafeString(btn_CfsCode.Value);
        //trip.BayCode = SafeValue.SafeString(cbb_Trip_BayCode.Value);

        trip.DriverCode = SafeValue.SafeString(btn_DriverCode.Value);
        trip.TowheadCode = SafeValue.SafeString(btn_TowheadCode.Value);
        trip.ChessisCode = SafeValue.SafeString(btn_ChessisCode.Value);
        //trip.SubletFlag = SafeValue.SafeString(cbb_Trip_SubletFlag.Value);
        //trip.SubletHauliername = SafeValue.SafeString(txt_SubletHauliername.Text);
        trip.TripCode = SafeValue.SafeString(cbb_Trip_TripCode.Value);
        trip.DoubleMounting = SafeValue.SafeString(cmb_DoubleMounting.Value,"No");
        //trip.StageCode = SafeValue.SafeString(cbb_StageCode.Value);
        //trip.Carpark = SafeValue.SafeString(cbb_Carpark.Value);
        //trip.StageStatus = SafeValue.SafeString(cbb_StageStatus.Value);
        trip.Statuscode = SafeValue.SafeString(cbb_Trip_StatusCode.Value);

        trip.FromDate = SafeValue.SafeDate(txt_FromDate.Date, new DateTime(1990, 1, 1));
        trip.FromTime = SafeValue.SafeString(txt_Trip_fromTime.Text);
        trip.ToDate = SafeValue.SafeDate(date_Trip_toDate.Date, new DateTime(1990, 1, 1));
        trip.ToTime = SafeValue.SafeString(txt_Trip_toTime.Text);
        trip.Remark = SafeValue.SafeString(txt_Trip_Remark.Text);
        trip.FromCode = SafeValue.SafeString(txt_Trip_FromCode.Text);
        trip.ToCode = SafeValue.SafeString(txt_Trip_ToCode.Text);
        //trip.Incentive1 = SafeValue.SafeDecimal(spin_Price.Text);
        trip.ParkingZone = SafeValue.SafeString(cbb_zone.Value);
        //trip.Incentive1 = SafeValue.SafeDecimal(spin_Incentive1.Text);
        //trip.Incentive2 = SafeValue.SafeDecimal(spin_Incentive2.Text);
        //trip.Incentive3 = SafeValue.SafeDecimal(spin_Incentive3.Text);
        //trip.Incentive4 = SafeValue.SafeDecimal(cbb_Incentive4.Value);

        //trip.Overtime = SafeValue.SafeSqlString(cbb_Overtime.Value);
        //trip.OverDistance = SafeValue.SafeSqlString(cbb_OverDistance.Value);
        trip.Remark1 = SafeValue.SafeString(txt_driver_remark.Text);

        trip.FromParkingLot = SafeValue.SafeString(fromPackingLot.Text);
        trip.ToParkingLot = SafeValue.SafeString(toPackingLot.Text);

        //trip.Charge1 = SafeValue.SafeDecimal(spin_Charge1.Text);
        //trip.Charge2 = SafeValue.SafeDecimal(spin_Charge2.Text);
        //trip.Charge3 = SafeValue.SafeDecimal(spin_Charge3.Text);
        //trip.Charge4 = SafeValue.SafeDecimal(spin_Charge4.Text);
        //trip.Charge5 = SafeValue.SafeDecimal(spin_Charge5.Text);
        //trip.Charge6 = SafeValue.SafeDecimal(spin_Charge6.Text);
        //trip.Charge7 = SafeValue.SafeDecimal(spin_Charge7.Text);
        //trip.Charge8 = SafeValue.SafeDecimal(spin_Charge8.Text);
        //trip.Charge9 = SafeValue.SafeDecimal(spin_Charge9.Text);
        //trip.Charge10 = SafeValue.SafeDecimal(spin_Charge10.Text);

        if (isNew)
        {
            trip.JobNo = jobNo.Text;
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(trip);
            Event_Log(trip.JobNo, "TRIP", 1, trip.Id,"");
        }
        else
        {
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(trip);
            Event_Log(trip.JobNo, "TRIP", 3, trip.Id,"");
            //EGL_JobTrip_AfterEndTrip("", dde_Trip_ContId.Text);

            C2.CtmJobDet2.tripStatusChanged(trip.Id);
            
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
            Event_Log(JobNo, "CONT", 4, contId,status);
        }
        if (isWarehouse == "No")
        {
            if (type == "IMP")
                status = "Customer-LD";
            if (type == "EXP")
                status = "Customer-MT";
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
            ConnectSql_mb.ExecuteNonQuery(sql);
            Event_Log(JobNo, "CONT", 4, contId,status);
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
        elog.Remark = CtmJobEventLogRemark.getDes("602") + e.Values["FileName"];
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
        e.NewValues["LineSource"] = "M";
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
        e.NewValues["ContType"] = SafeValue.SafeString(e.NewValues["ContType"]);
        e.NewValues["LineType"] = SafeValue.SafeString(e.NewValues["LineType"]);
        e.NewValues["DocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        e.NewValues["LocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["DocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["ExRate"], 0), 2);

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
        e.NewValues["SkuCode"] = "GENERAL";
        e.NewValues["Location"] = "HOLDING";
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


    #region Quotetion
    protected void Grid1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.JobRate));
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
        ASPxComboBox txt_ChgCode = grid.FindEditRowCellTemplateControl(null, "txt_ChgCode") as ASPxComboBox;


        e.NewValues["ChgCode"] = SafeValue.SafeString(txt_ChgCode.Value);
        string sql = string.Format(@"select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", SafeValue.SafeString(txt_ChgCode.Value));
        string chgcodeDes = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
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
        if (SafeValue.SafeString(e.NewValues["ChgCodeDe"]) == "")
        {
            e.NewValues["ChgCodeDe"] = chgcodeDes;
        }
        ASPxTextBox txt_QuoteNo = this.grid_job.FindEditFormTemplateControl("txt_QuoteNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        e.NewValues["JobNo"] = txt_QuoteNo.Text;
    }
    protected void Grid1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

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
        ASPxComboBox txt_ChgCode = grid.FindEditRowCellTemplateControl(null, "txt_ChgCode") as ASPxComboBox;
        string sql = string.Format(@"select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", SafeValue.SafeString(txt_ChgCode.Value));
        string chgcodeDes = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        e.NewValues["ChgCode"] = SafeValue.SafeString(txt_ChgCode.Value);
        e.NewValues["ChgCodeDe"] = SafeValue.SafeString(e.NewValues["ChgCodeDe"]);
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

    #endregion

    private void Event_Log(string jobNo, string level, int c,int id,string status)
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
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Quotation, c,status);
        }
        if (level == "CONT") {
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

    #region job detail
    private void cargo_save(ASPxGridView grid)
    {
        try
        {

            ASPxTextBox txt_cargo_id = grid.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
            string oId = txt_cargo_id.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + oId + "'");
            C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
            bool isNew = false;
            if (jo == null)
            {

                jo = new C2.JobHouse();
                isNew = true;
                jo.JobType = "I";
                jo.UserId = HttpContext.Current.User.Identity.Name;
                jo.EntryDate = DateTime.Now;


            }
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_OrderNo = pageControl.FindControl("txt_OrderNo") as ASPxTextBox;
            if (txt_OrderNo != null)
                jo.JobNo = txt_OrderNo.Text;
            ASPxTextBox txt_ExpBkgN = grid.FindEditFormTemplateControl("txt_ExpBkgN") as ASPxTextBox;
            if (txt_ExpBkgN != null)
                jo.BookingNo = txt_ExpBkgN.Text;
            ASPxTextBox txt_JobOrder = grid.FindEditFormTemplateControl("txt_JobOrder") as ASPxTextBox;
            ASPxTextBox txt_ExJobOrder = grid.FindEditFormTemplateControl("txt_ExJobOrder") as ASPxTextBox;
            if (txt_ExJobOrder != null)
                jo.HblNo = txt_ExJobOrder.Text;
            ASPxTextBox txt_ContNo = grid.FindEditFormTemplateControl("txt_ContNo") as ASPxTextBox;
            if (txt_ContNo != null)
                jo.ContNo = txt_ContNo.Text;


            ASPxTextBox txt_Carrier = grid.FindEditFormTemplateControl("txt_Carrier") as ASPxTextBox;
            if (txt_Carrier != null)
                jo.ShipperInfo = txt_Carrier.Text;
            ASPxDropDownEdit DropDownEdit_Party = grid.FindEditFormTemplateControl("DropDownEdit_Party") as ASPxDropDownEdit;
            ASPxTextBox txt_Party = grid.FindEditFormTemplateControl("txt_Party") as ASPxTextBox;
            if (DropDownEdit_Party != null)
            {
                jo.ConsigneeInfo = SafeValue.SafeString(DropDownEdit_Party.Text);
                //if (SafeValue.SafeString(DropDownEdit_Party.Text) == "")
                //{
                //    throw new Exception("请输入货主信息！");
                //}
            }
            if (txt_Party != null)
            {
                if (txt_Party.Text != "")
                {
                    VilaParty(jo.ConsigneeInfo, jo.ConsigneeRemark, jo.ConsigneeEmail, jo.Email1, jo.Email2, jo.Tel1, jo.Tel2, jo.Mobile1, jo.Mobile2, jo.Desc1);
                    jo.ConsigneeInfo = txt_Party.Text;
                }
                else
                {
                    throw new Exception("请输入货主信息！");
                }
            }


            ASPxComboBox cmb_Responsible = grid.FindEditFormTemplateControl("cmb_Responsible") as ASPxComboBox;
            if (cmb_Responsible != null)
            {
                jo.Responsible = SafeValue.SafeString(cmb_Responsible.Value);
                if (SafeValue.SafeString(cmb_Responsible.Value) == "")
                {
                    throw new Exception("请选择个人还是公司！");
                }
            }
            ASPxTextBox txt_ConsigneeRemark = grid.FindEditFormTemplateControl("txt_ConsigneeRemark") as ASPxTextBox;
            if (txt_ConsigneeRemark != null)
            {
                jo.ConsigneeRemark = txt_ConsigneeRemark.Text;
                if (SafeValue.SafeString(cmb_Responsible.Value) == "PERSON" && txt_ConsigneeRemark.Text == "")
                {
                    throw new Exception("请输入身份证号码/IC");
                }
            }
            ASPxTextBox txt_ConsigneeEmail = grid.FindEditFormTemplateControl("txt_ConsigneeEmail") as ASPxTextBox;
            if (txt_ConsigneeEmail != null)
                jo.ConsigneeEmail = txt_ConsigneeEmail.Text;
            ASPxTextBox txt_Email1 = grid.FindEditFormTemplateControl("txt_Email1") as ASPxTextBox;
            if (txt_Email1 != null)
                jo.Email1 = txt_Email1.Text;
            ASPxTextBox txt_Email2 = grid.FindEditFormTemplateControl("txt_Email2") as ASPxTextBox;
            if (txt_Email2 != null)
                jo.Email2 = txt_Email2.Text;
            ASPxTextBox txt_Tel1 = grid.FindEditFormTemplateControl("txt_Tel1") as ASPxTextBox;
            if (txt_Tel1 != null)
                jo.Tel1 = txt_Tel1.Text;
            ASPxTextBox txt_Tel2 = grid.FindEditFormTemplateControl("txt_Tel2") as ASPxTextBox;
            if (txt_Tel2 != null)
                jo.Tel2 = txt_Tel2.Text;
            ASPxTextBox txt_Mobile1 = grid.FindEditFormTemplateControl("txt_Mobile1") as ASPxTextBox;
            if (txt_Mobile1 != null)
                jo.Mobile1 = txt_Mobile1.Text;
            ASPxTextBox txt_Mobile2 = grid.FindEditFormTemplateControl("txt_Mobile2") as ASPxTextBox;
            if (txt_Mobile2 != null)
                jo.Mobile2 = txt_Mobile2.Text;
            ASPxMemo memo_Desc1 = grid.FindEditFormTemplateControl("memo_Desc1") as ASPxMemo;
            if (txt_ExpBkgN != null)
            {
                jo.Desc1 = memo_Desc1.Text;

            }
            if (txt_Tel1.Text.Trim() == "" && txt_Tel2.Text.Trim() == "" && txt_Mobile1.Text.Trim() == "" && txt_Mobile2.Text.Trim() == "")
            {
                throw new Exception("请输入一个联系方式！");
            }
            ASPxTextBox txt_ClientId = grid.FindEditFormTemplateControl("txt_ClientId") as ASPxTextBox;
            ASPxTextBox txt_ClientEmail = grid.FindEditFormTemplateControl("txt_ClientEmail") as ASPxTextBox;
            ASPxMemo memo_Remark1 = grid.FindEditFormTemplateControl("memo_Remark1") as ASPxMemo;
            ASPxCheckBox ckb_IsHold = grid.FindEditFormTemplateControl("ckb_IsHold") as ASPxCheckBox;
            #region
            if (ckb_IsHold != null)
            {
                if (ckb_IsHold.Checked)
                {
                    jo.IsHold = SafeValue.SafeBool(ckb_IsHold.Checked, false);
                    if (txt_ClientId != null)
                        jo.ClientId = jo.ConsigneeInfo;

                    if (txt_ClientEmail != null)
                        jo.ClientEmail = jo.Tel1 + " " + jo.Tel2 + " " + jo.Mobile1 + " " + jo.Mobile2;

                    if (memo_Remark1 != null)
                        jo.Remark1 = jo.Desc1;
                }
                else
                {
                    if (txt_ClientId != null)
                        jo.ClientId = txt_ClientId.Text;

                    if (txt_ClientEmail != null)
                        jo.ClientEmail = txt_ClientEmail.Text;

                    if (memo_Remark1 != null)
                        jo.Remark1 = memo_Remark1.Text;
                }
            }
            #endregion
            ASPxCheckBox ckb_Prepaid_Ind = grid.FindEditFormTemplateControl("ckb_Prepaid_Ind") as ASPxCheckBox;
            if (ckb_Prepaid_Ind != null)
            {
                if (ckb_Prepaid_Ind.Checked)
                {
                    jo.PrepaidInd = "YES";
                }
                else
                {
                    jo.PrepaidInd = "NO";
                }
            }
            ASPxSpinEdit spin_Collect_Amount1 = grid.FindEditFormTemplateControl("spin_Collect_Amount1") as ASPxSpinEdit;
            if (spin_Collect_Amount1 != null)
                jo.CollectAmount1 = SafeValue.SafeDecimal(spin_Collect_Amount1.Value);
            ASPxSpinEdit spin_Collect_Amount2 = grid.FindEditFormTemplateControl("spin_Collect_Amount2") as ASPxSpinEdit;
            if (spin_Collect_Amount2 != null)
                jo.CollectAmount2 = SafeValue.SafeDecimal(spin_Collect_Amount2.Value);
            ASPxComboBox cmb_Duty_Payment = grid.FindEditFormTemplateControl("cmb_Duty_Payment") as ASPxComboBox;
            if (cmb_Duty_Payment != null)
                jo.DutyPayment = SafeValue.SafeString(cmb_Duty_Payment.Value);
            ASPxComboBox cmb_Incoterm = grid.FindEditFormTemplateControl("cmb_Incoterm") as ASPxComboBox;
            if (cmb_Incoterm != null)
                jo.Incoterm = cmb_Incoterm.Text;
            ASPxMemo memo_Remark2 = grid.FindEditFormTemplateControl("memo_Remark2") as ASPxMemo;
            if (memo_Remark2 != null)
                jo.Remark2 = memo_Remark2.Text;
            ASPxSpinEdit spin_MiscFee = grid.FindEditFormTemplateControl("spin_MiscFee") as ASPxSpinEdit;
            if (spin_MiscFee != null)
                jo.Ocean_Freight = SafeValue.SafeDecimal(spin_MiscFee.Value);

            jo.UserId = HttpContext.Current.User.Identity.Name;


            ASPxSpinEdit spin_OtherFee1 = grid.FindEditFormTemplateControl("spin_OtherFee1") as ASPxSpinEdit;
            if (spin_OtherFee1 != null)
                jo.GstFee = SafeValue.SafeDecimal(spin_OtherFee1.Value);
            ASPxSpinEdit spin_OtherFee2 = grid.FindEditFormTemplateControl("spin_OtherFee2") as ASPxSpinEdit;
            if (spin_OtherFee2 != null)
                jo.PermitFee = SafeValue.SafeDecimal(spin_OtherFee2.Value);
            ASPxSpinEdit spin_OtherFee3 = grid.FindEditFormTemplateControl("spin_OtherFee3") as ASPxSpinEdit;
            if (spin_OtherFee3 != null)
                jo.HandlingFee = SafeValue.SafeDecimal(spin_OtherFee3.Value);
            ASPxSpinEdit spin_OtherFee4 = grid.FindEditFormTemplateControl("spin_OtherFee4") as ASPxSpinEdit;
            if (spin_OtherFee4 != null)
                jo.OtherFee = SafeValue.SafeDecimal(spin_OtherFee4.Value);
            ASPxComboBox cmb_IsBill = grid.FindEditFormTemplateControl("cmb_IsBill") as ASPxComboBox;
            if (cmb_IsBill != null)
            {
                if (cmb_IsBill.SelectedItem.Value != null)
                {
                    if (SafeValue.SafeString(cmb_IsBill.SelectedItem.Value) == "YES")
                    {
                        jo.IsBill = true;
                    }
                    else
                    {
                        jo.IsBill = false;
                    }
                }
                
            }
            ASPxComboBox cmb_CheckIn = grid.FindEditFormTemplateControl("cmb_CheckIn") as ASPxComboBox;
            if (cmb_CheckIn != null){
                if (SafeValue.SafeString(cmb_CheckIn.SelectedItem.Value) == "YES")
                {
                    jo.IsLong = true;
                }
                else
                {
                    jo.IsLong = false;
                }
            }
            jo.UserId = HttpContext.Current.User.Identity.Name;

            if (isNew)
            {
                jo.CargoStatus = "ORDER";
                C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(jo);

            }
            else
            {
                C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(jo);

            }
            UpdateParty(jo.ConsigneeInfo, jo.ConsigneeRemark, jo.ConsigneeEmail, jo.Email1, jo.Email2, jo.Tel1, jo.Tel2, jo.Mobile1, jo.Mobile2, jo.Desc1);
            this.dsWh.FilterExpression = "Id='" + jo.Id + "'";
            if (grid.GetRow(0) != null)
                grid.StartEdit(0);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void grd_Det_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "UpdateNew")
        {
            ASPxGridView grd = sender as ASPxGridView;
            if (!grd.IsEditing) return;
            //grd.UpdateEdit();
            //grd.AddNewRow();
            cargo_save(grd);
            grd.CancelEdit();
        }
        if (s == "UpdateClose")
        {
            ASPxGridView grd = sender as ASPxGridView;
            if (!grd.IsEditing) return;

            cargo_save(grd);
        }
        if (s == "UpdateNext")
        {
            ASPxGridView grd = sender as ASPxGridView;
            if (!grd.IsEditing) return;
            int cur = grd.EditingRowVisibleIndex;
            cargo_save(grd);
            //grd.UpdateEdit();
            if (cur < grd.VisibleRowCount - 1)
                grd.StartEdit(cur + 1);
        }

    }
    protected void grd_Det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobHouse));
        }
    }
    protected void grd_Det_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null && grd.GetMasterRowKeyValue() != null)
        {
            string oid = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            string orderNo = SafeValue.SafeString(Helper.Sql.One("select JobNo from CTM_Job where Id=" + oid), "");
            DataTable tab = ConnectSql.GetTab("select ContainerNo,SealNo from CTM_JobDet1 where JobNo='" + orderNo + "'");
            this.dsWh.FilterExpression = "JobNo='" + orderNo + "'";// 

        }
    }
    protected void grd_Det_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_OrderNo = pageControl.FindControl("txt_OrderNo") as ASPxTextBox;
        e.NewValues["LineN"] = 0;
        e.NewValues["JobNo"] = txt_OrderNo.Text;
        e.NewValues["ContNo"] = Helper.Safe.SafeString(Helper.Sql.One("select top 1 ContainerNo from CTM_JobDet1 where JobNo='" + txt_OrderNo.Text + "'"));
        e.NewValues["SealNo"] = " ";
        e.NewValues["BalanceM3"] = 0;
        e.NewValues["BalanceWt"] = 0;
        e.NewValues["BalanceQty"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["Pod"] = "SGSIN";
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["PackType"] = " ";
        e.NewValues["IsBill"] = "YES";
        e.NewValues["PermitBy"] = "UNPAID";
        e.NewValues["Responsible"] = "PERSON";
        e.NewValues["ClearRemark"] = " ";
        e.NewValues["ClearedOn"] = " ";
        e.NewValues["ClearedBy"] = " ";
        e.NewValues["ClearanceRmk"] = " ";
        e.NewValues["RecAmt"] = " ";
        e.NewValues["HblN"] = " ";
        e.NewValues["ExpBkgN"] = " ";
        e.NewValues["LandStatus"] = "Normal";
        e.NewValues["Commodity"] = "Normal";
        e.NewValues["CommodityRmk"] = " ";

        e.NewValues["Marking1"] = " ";
        e.NewValues["Marking2"] = " ";

        e.NewValues["Remark1"] = " ";
        e.NewValues["Remark2"] = " ";

        e.NewValues["Desc1"] = " ";
        e.NewValues["Desc2"] = " ";

        e.NewValues["Tally1"] = 0;
        e.NewValues["Tally2"] = 0;
        e.NewValues["Tally3"] = 0;
        e.NewValues["Tally4"] = 0;
        e.NewValues["Tally21"] = 0;
        e.NewValues["Tally22"] = 0;
        e.NewValues["Tally23"] = 0;
        e.NewValues["Tally24"] = 0;
        e.NewValues["Tally31"] = 0;
        e.NewValues["Tally32"] = 0;
        e.NewValues["Tally33"] = 0;
        e.NewValues["Tally34"] = 0;
        e.NewValues["Tally41"] = 0;
        e.NewValues["Tally42"] = 0;
        e.NewValues["Tally43"] = 0;
        e.NewValues["Tally44"] = 0;
        e.NewValues["Tally51"] = 0;
        e.NewValues["Tally52"] = 0;
        e.NewValues["Tally53"] = 0;
        e.NewValues["Tally54"] = 0;
        e.NewValues["Tally61"] = 0;
        e.NewValues["Tally62"] = 0;
        e.NewValues["Tally63"] = 0;
        e.NewValues["Tally64"] = 0;

        e.NewValues["IsOverLength"] = false;
        e.NewValues["IsHold"] = false;
        e.NewValues["IsBill"] = false;
        e.NewValues["IsNormal"] = true;
        e.NewValues["IsDg"] = false;
        e.NewValues["DgClass"] = " ";


        e.NewValues["QtyOrig"] = 0;
        e.NewValues["PackTypeOrig"] = " ";
        e.NewValues["WeightOrig"] = 0;
        e.NewValues["M3Orig"] = 0;
        e.NewValues["Remark3"] = " ";
        e.NewValues["JobNo"] = "0";
        e.NewValues["Type"] = "L";
        e.NewValues["Pod"] = "SGSIN";

        e.NewValues["WaiveAmt"] = 0;
        e.NewValues["WaiveRmk"] = " ";
        e.NewValues["ApprovedAmt"] = 0;
        e.NewValues["ApprovedRmk"] = " ";
        e.NewValues["UserId"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["EntryDate"] = DateTime.Now;


        e.NewValues["Package1"] = 1;
        e.NewValues["Package2"] = 1;
        e.NewValues["Package3"] = 1;
        e.NewValues["Package4"] = 1;
        e.NewValues["Package5"] = 1;
        e.NewValues["Package6"] = 1;
        e.NewValues["Package7"] = 1;
        e.NewValues["Package8"] = 1;
        e.NewValues["Package9"] = 1;
        e.NewValues["Package10"] = 1;

        e.NewValues["Length1"] = 0;
        e.NewValues["Length2"] = 0;
        e.NewValues["Length3"] = 0;
        e.NewValues["Length4"] = 0;
        e.NewValues["Length5"] = 0;
        e.NewValues["Length6"] = 0;
        e.NewValues["Length7"] = 0;
        e.NewValues["Length8"] = 0;
        e.NewValues["Length9"] = 0;
        e.NewValues["Length10"] = 0;

        e.NewValues["Width1"] = 0;
        e.NewValues["Width2"] = 0;
        e.NewValues["Width3"] = 0;
        e.NewValues["Width4"] = 0;
        e.NewValues["Width5"] = 0;
        e.NewValues["Width6"] = 0;
        e.NewValues["Width7"] = 0;
        e.NewValues["Width8"] = 0;
        e.NewValues["Width9"] = 0;
        e.NewValues["Width10"] = 0;

        e.NewValues["Height1"] = 0;
        e.NewValues["Height2"] = 0;
        e.NewValues["Height3"] = 0;
        e.NewValues["Height4"] = 0;
        e.NewValues["Height5"] = 0;
        e.NewValues["Height6"] = 0;
        e.NewValues["Height7"] = 0;
        e.NewValues["Height8"] = 0;
        e.NewValues["Height9"] = 0;
        e.NewValues["Height10"] = 0;

        e.NewValues["Volume1"] = 0;
        e.NewValues["Volume2"] = 0;
        e.NewValues["Volume3"] = 0;
        e.NewValues["Volume4"] = 0;
        e.NewValues["Volume5"] = 0;
        e.NewValues["Volume6"] = 0;
        e.NewValues["Volume7"] = 0;
        e.NewValues["Volume8"] = 0;
        e.NewValues["Volume9"] = 0;
        e.NewValues["Volume10"] = 0;
    } 
    private void cargo_update(ASPxGridView grid)
    {
        try
        {
            ASPxTextBox txt_cargo_id = grid.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
            string oId = txt_cargo_id.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + oId + "'");
            C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;

            ASPxSpinEdit spin_TotQty = grid.FindEditFormTemplateControl("spin_TotQty") as ASPxSpinEdit;
            jo.Qty = SafeValue.SafeInt(spin_TotQty.Value, 0);
            ASPxComboBox txt_PackType = grid.FindEditFormTemplateControl("txt_PackType") as ASPxComboBox;
            jo.PackTypeOrig = SafeValue.SafeString(txt_PackType.Text, "");
            ASPxSpinEdit spin_Weight = grid.FindEditFormTemplateControl("spin_Weight") as ASPxSpinEdit;
            jo.Weight = SafeValue.SafeDecimal(spin_Weight.Value);
            ASPxSpinEdit spin_M3 = grid.FindEditFormTemplateControl("spin_M3") as ASPxSpinEdit;
            jo.Volume = SafeValue.SafeDecimal(spin_M3.Value);
            ASPxComboBox cmb_Fee1CurrId = grid.FindEditFormTemplateControl("cmb_Fee1CurrId") as ASPxComboBox;
            jo.Currency = SafeValue.SafeString(cmb_Fee1CurrId.Value);
            C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(jo);

        }
        catch (Exception ex)
        { throw new Exception(ex.Message + ex.StackTrace); }
    }
    protected void grd_Det_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string oid = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        string orderNo = Helper.Sql.One("select JobNo from CTM_Job where Id='" + oid + "'").ToString();
        int index = SafeValue.SafeInt(Helper.Sql.One("select count(Id) from job_house where JobNo='" + orderNo + "'"), 1);
        if (index == 0)
            index = 1;
        else
            index++;
        e.NewValues["JobNo"] = orderNo;
        e.NewValues["JobType"] = "I";
        e.NewValues["LineN"] = index;
        e.NewValues["TotTally"] = SafeValue.SafeInt(e.NewValues["Tally1"], 0) + SafeValue.SafeInt(e.NewValues["Tally2"], 0) + SafeValue.SafeInt(e.NewValues["Tally3"], 0) + SafeValue.SafeInt(e.NewValues["Tally4"], 0) + SafeValue.SafeInt(e.NewValues["Tally21"], 0) + SafeValue.SafeInt(e.NewValues["Tally22"], 0) + SafeValue.SafeInt(e.NewValues["Tally23"], 0) + SafeValue.SafeInt(e.NewValues["Tally24"], 0) + SafeValue.SafeInt(e.NewValues["Tally31"], 0) + SafeValue.SafeInt(e.NewValues["Tally32"], 0) + SafeValue.SafeInt(e.NewValues["Tally33"], 0) + SafeValue.SafeInt(e.NewValues["Tally34"], 0) + SafeValue.SafeInt(e.NewValues["Tally41"], 0) + SafeValue.SafeInt(e.NewValues["Tally42"], 0) + SafeValue.SafeInt(e.NewValues["Tally43"], 0) + SafeValue.SafeInt(e.NewValues["Tally44"], 0) + SafeValue.SafeInt(e.NewValues["Tally51"], 0) + SafeValue.SafeInt(e.NewValues["Tally52"], 0) + SafeValue.SafeInt(e.NewValues["Tally53"], 0) + SafeValue.SafeInt(e.NewValues["Tally54"], 0) + SafeValue.SafeInt(e.NewValues["Tally61"], 0) + SafeValue.SafeInt(e.NewValues["Tally62"], 0) + SafeValue.SafeInt(e.NewValues["Tally63"], 0) + SafeValue.SafeInt(e.NewValues["Tally64"], 0);
        e.NewValues["UserId"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["EntryDate"] = DateTime.Now;

        //ASPxCheckBox isRate = (sender as ASPxGridView).FindEditFormTemplateControl("ck_Rate") as ASPxCheckBox;
        //if (isRate != null && isRate.Checked)
        //{
        //    e.NewValues["IsRate"] = true;
        //}
        //else
        //    e.NewValues["IsRate"] = false;

        if (SafeValue.SafeDecimal(e.NewValues["TotQty"], 0) == 0)
            e.NewValues["TotQty"] = e.NewValues["QtyOrig"];
        if (SafeValue.SafeDecimal(e.NewValues["Weight"], 0) == 0)
            e.NewValues["Weight"] = e.NewValues["WeightOrig"];
        if (SafeValue.SafeDecimal(e.NewValues["M3"], 0) == 0)
            e.NewValues["M3"] = e.NewValues["M3Orig"];


        string damage = string.Format("{0}", e.NewValues["Commodity"]);
        if (damage.ToLower() == "damage")
        {
            // C2.Email.RegisterEmail("Notify", "Pending", orderNo, "I", "kevin@sharpmanage.com", "", "", "Damaged Cargo Notification", "Please refer to Job No: I/" + orderNo, "");
        }

        e.NewValues["BalanceQty"] = SafeValue.SafeInt(e.NewValues["TotQty"], 0);
        e.NewValues["BalanceWt"] = e.NewValues["Weight"];
        e.NewValues["BalanceM3"] = e.NewValues["M3"];

        if (SafeValue.SafeString(e.NewValues["PackType"], "") == "")
            e.NewValues["PackType"] = e.NewValues["PackTypeOrig"];

        e.NewValues["ForkliftCharge"] = new decimal(-1);
        e.NewValues["ImportProcessingFee"] = new decimal(-1);
        e.NewValues["TracingFee"] = new decimal(-1);
        e.NewValues["WarehouseSurcharge"] = new decimal(-1);
        e.NewValues["AdminFee"] = new decimal(-1);

        for (int i = 0; i < 10; i++)
        {
            e.NewValues["OtherFee" + (i + 1).ToString()] = S.Decimal(e.NewValues["OtherFee" + (i + 1).ToString()]);
        }

        e.NewValues["ForkliftUnit"] = "";
        e.NewValues["ImportProcessingUnit"] = "";
        e.NewValues["TracingUnit"] = "";
        e.NewValues["WarehouseUnit"] = "";
        e.NewValues["AdminUnit"] = "";

        string[] txt = {"HblN","DnNo","ExBkgNo","CargoStatus","DgClass","LandStatus","Commodity","CommodityRmk","ClearedOn","ClearedBy","ClearanceRmk","ClearRemark","PackTypeOrig","PackType","Marking2","Remark1","Desc1",
            "ShipperInfo","ShipperRemark","ShipperEmail",
            "ConsigneeInfo","ConsigneeRemark","ConsigneeEmail",
            "NotifyInfo","NotifyRemark","NotifyEmail","TransportNeed","TransportCode","TransportRemark","TransportEmail"
        };
        for (int f = 0; f < txt.Length; f++)
        {
            e.NewValues[txt[f]] = Helper.Safe.SafeString(e.NewValues[txt[f]], "").ToUpper();
        }

        string _pod = S.Text(e.NewValues["Pod"]).Trim();
        if (_pod.Length < 3)
        {
            _pod = "SGSIN";
            e.NewValues["Pod"] = "SGSIN";
        }
        if (_pod == "SGSIN")
            e.NewValues["Type"] = "L";
        else
            e.NewValues["Type"] = "T";
    }
    protected void grd_Det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Det_BeforeGetCallbackResult(object sender, EventArgs e)
    {
        try
        {
            var g = sender as ASPxGridView;
            if (g.IsNewRowEditing)
            {
                string[] tb = { "txt_pod", "txt_Job_DoNo", "ASPxTextBox7", "txt_ExpBkgN", "ASPxTextBox1" };
                string[] sp = { "spin_Job_Qty", "spin_Job_Wt", "spin_Job_M3" };
                string[] mm = { "txt_Job_Mkg2", "txt_Job_Rmk1", "txt_Job_Desc1" };
                for (int i = 0; i < tb.Length; i++)
                {
                    var ctl = g.FindEditFormTemplateControl(tb[i]) as ASPxTextBox;
                    ctl.Text = "";
                }
                for (int i = 0; i < sp.Length; i++)
                {
                    var ctl = g.FindEditFormTemplateControl(sp[i]) as ASPxSpinEdit;
                    ctl.Value = 0;
                }
                for (int i = 0; i < mm.Length; i++)
                {
                    var ctl = g.FindEditFormTemplateControl(mm[i]) as ASPxMemo;
                    ctl.Text = "";
                }
            }
        }
        catch { }
    }
    protected void grd_Det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        //throw new Exception("Hi Insert Here");
        e.NewValues["TotTally"] = SafeValue.SafeInt(e.NewValues["Tally1"], 0) + SafeValue.SafeInt(e.NewValues["Tally2"], 0) + SafeValue.SafeInt(e.NewValues["Tally3"], 0) + SafeValue.SafeInt(e.NewValues["Tally4"], 0) + SafeValue.SafeInt(e.NewValues["Tally21"], 0) + SafeValue.SafeInt(e.NewValues["Tally22"], 0) + SafeValue.SafeInt(e.NewValues["Tally23"], 0) + SafeValue.SafeInt(e.NewValues["Tally24"], 0) + SafeValue.SafeInt(e.NewValues["Tally31"], 0) + SafeValue.SafeInt(e.NewValues["Tally32"], 0) + SafeValue.SafeInt(e.NewValues["Tally33"], 0) + SafeValue.SafeInt(e.NewValues["Tally34"], 0) + SafeValue.SafeInt(e.NewValues["Tally41"], 0) + SafeValue.SafeInt(e.NewValues["Tally42"], 0) + SafeValue.SafeInt(e.NewValues["Tally43"], 0) + SafeValue.SafeInt(e.NewValues["Tally44"], 0) + SafeValue.SafeInt(e.NewValues["Tally51"], 0) + SafeValue.SafeInt(e.NewValues["Tally52"], 0) + SafeValue.SafeInt(e.NewValues["Tally53"], 0) + SafeValue.SafeInt(e.NewValues["Tally54"], 0) + SafeValue.SafeInt(e.NewValues["Tally61"], 0) + SafeValue.SafeInt(e.NewValues["Tally62"], 0) + SafeValue.SafeInt(e.NewValues["Tally63"], 0) + SafeValue.SafeInt(e.NewValues["Tally64"], 0);
        e.NewValues["UserId"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["EntryDate"] = DateTime.Now;


        string[] txt = {"HblN","DnNo","ExBkgNo","CargoStatus","DgClass","LandStatus","Commodity","CommodityRmk","ClearedOn","ClearedBy","ClearanceRmk","ClearRemark","PackTypeOrig","PackType","Marking2","Remark1","Desc1",
                    "ShipperInfo","ShipperRemark","ShipperEmail",
            "ConsigneeInfo","ConsigneeRemark","ConsigneeEmail",
            "NotifyInfo","NotifyRemark","NotifyEmail","TransportNeed","TransportCode","TransportRemark","TransportEmail"
        };
        for (int f = 0; f < txt.Length; f++)
        {
            e.NewValues[txt[f]] = Helper.Safe.SafeString(e.NewValues[txt[f]], "").ToUpper();
        }
        string[] bol = { "IsHold", "IsBill", "IsDg", "IsNormal", "IsLong" };
        for (int f = 0; f < bol.Length; f++)
        {
            e.NewValues[bol[f]] = Helper.Safe.SafeBool(e.NewValues[bol[f]], false);
        }

        int oq = SafeValue.SafeInt(e.NewValues["QtyOrig"], 0);
        int tq = SafeValue.SafeInt(e.NewValues["TotQty"], 0);
        int bq = SafeValue.SafeInt(e.NewValues["BalanceQty"], 0);

        decimal ow = SafeValue.SafeDecimal(e.NewValues["WeightOrig"], 0);
        decimal tw = SafeValue.SafeDecimal(e.NewValues["Weight"], 0);
        decimal bw = SafeValue.SafeDecimal(e.NewValues["BalanceWt"], 0);

        decimal ov = SafeValue.SafeDecimal(e.NewValues["M3Orig"], 0);
        decimal tv = SafeValue.SafeDecimal(e.NewValues["M3"], 0);
        decimal bv = SafeValue.SafeDecimal(e.NewValues["BalanceM3"], 0);

        e.NewValues["QtyOrig"] = oq;
        e.NewValues["TotQty"] = tq;
        e.NewValues["WeightOrig"] = ow;
        e.NewValues["Weight"] = tw;
        e.NewValues["M3Orig"] = ov;
        e.NewValues["M3"] = tv;

        if (oq == bq && oq != tq)
            e.NewValues["BalanceQty"] = tq;
        if (ow == bw && ow != tw)
            e.NewValues["BalanceWt"] = tw;
        if (ov == bv && ov != tv)
            e.NewValues["BalanceM3"] = tv;
        string _clearrmk = Helper.Safe.SafeString(e.NewValues["ClearRemark"]).Trim();
        if (_clearrmk.Length < 4)
        {
            e.NewValues["BalanceQty"] = tq;
            e.NewValues["BalanceWt"] = tw;
            e.NewValues["BalanceM3"] = tv;
        }

        //e.NewValues["BalanceQty"] =SafeValue.SafeInt(e.NewValues["TotQty"],0);
        //e.NewValues["BalanceWt"] = e.NewValues["Weight"];
        //e.NewValues["BalanceM3"] = e.NewValues["M3"];

        for (int i = 0; i < 10; i++)
        {
            e.NewValues["OtherFee" + (i + 1).ToString()] = S.Decimal(e.NewValues["OtherFee" + (i + 1).ToString()]);
        }




        string oid = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        string orderNo = Helper.Sql.One("select joborderNo from CTM_Job where Id='" + oid + "'").ToString();
        string damage = string.Format("{0}", e.NewValues["Commodity"]);
        string damage0 = string.Format("{0}", e.OldValues["Commodity"]);
        if (damage0 != damage && damage.ToLower() == "damage")
        {
            //C2.Email.RegisterEmail("Notify", "Pending", orderNo, "I", "kevin@sharpmanage.com", "", "", "Damaged Cargo Notification", "Please refer to Job No: I/" + orderNo, "");
        }

        int fcnt = Helper.Safe.SafeInt(Helper.Sql.One("select count(*) from ctm_attachment where RefNo='" + orderNo + "'"));
        if (fcnt > 0)
        {
            // process photo no
            string _err = "";
            string _fn = e.NewValues["PackTypeOrig"].ToString().Trim();
            string _cn = e.NewValues["ContNo"].ToString().Trim();
            if (_fn.Length > 0)
            {
                string[] _fp = _fn.Split(new char[] { ',', ';', '.', '#', '/' });
                for (int i = 0; i < _fp.Length; i++)
                {
                    string _fno = _fp[i].Trim();
                    int _n = Helper.Safe.SafeInt(_fno);
                    if (_n == 0)
                        _err += string.Format("<{0}:Format>", _fno);
                    else
                    {
                        //int cnt = Helper.Safe.SafeInt(Helper.Sql.One("select count(*) from xwjobphoto where  orderno='"+orderNo+"' and name like '%"+_fno+".jpg' "));
                        int cnt = Helper.Safe.SafeInt(Helper.Sql.One("select count(*) from CTM_Attachment where ContainerNo='" + _cn + "' and  RefNo='" + orderNo + "' and FileName like '%" + _fno + ".jpg' "));
                        if (cnt == 0)
                        {
                            _err += string.Format("<{0}:Invalid>", _fno);
                        }
                        if (cnt > 1)
                        {
                            _err += string.Format("<{0}:Multiple>", _fno);
                        }
                    }
                }
                if (_err == "")
                {
                    for (int i = 0; i < _fp.Length; i++)
                    {
                        string _fno = _fp[i].Trim();
                        int _n = Helper.Safe.SafeInt(_fno);
                        Helper.Sql.Exec("Update CTM_Attachment set HblNo='" + Helper.Safe.SafeString(e.NewValues["HblN"]) + "' where RefNo='" + orderNo + "' and FileName like '%" + _fno + ".jpg' ");
                    }
                }
                else
                {
                    throw new Exception("Photo: " + _err);
                }
            }
        }

        string _pod = S.Text(e.NewValues["Pod"]).Trim();
        if (_pod.Length < 3)
        {
            _pod = "SGSIN";
            e.NewValues["Pod"] = "SGSIN";
        }
        if (_pod == "SGSIN")
            e.NewValues["Type"] = "L";
        else
            e.NewValues["Type"] = "T";
        //throw new Exception("Hi Insert End");

    }
    protected void grd_Det_CustomDataCallback(object sender,ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox orderNo = pageControl.FindControl("txt_OrderNo") as ASPxTextBox;
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar[0].Equals("Refresh"))
        {
            #region
            if (orderNo == null || orderNo.Text.Length < 2) return;
            DataTable tab = Helper.Sql.List(string.Format("select Id from xwjobdet where joborder='{0}' order by contno,dnno", orderNo.Text));
            string sql = "update xwjobdet set LineN='{0}' where Id='{1}'";
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                string oid = tab.Rows[i][0].ToString();
                string sql1 = string.Format(sql, i + 1, oid);
                C2.Manager.ORManager.ExecuteCommand(sql1);
            }
            #endregion
        }
        if (e.Parameters == "Save")
        {
            #region
            ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
            cargo_save(grid);
            grid.CancelEdit();
            #endregion
        }
        if (e.Parameters == "Update")
        {
            #region
            ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
            cargo_update(grid);
            grid.CancelEdit();
            #endregion
        }
        if (e.Parameters == "UpdateStatus")
        {
            #region
            ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
            updateStatus(grid);
            grid.CancelEdit();
            #endregion
        }
        if (ar[0].Equals("DeleteInline"))
        {
            #region
            ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
            int index = SafeValue.SafeInt(ar[1], 0);
            string Id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
            string sql = string.Format(@"update xwjobdet set JobNo='0',ContNo='' where Id='{0}'", Id);
            C2.Manager.ORManager.ExecuteCommand(sql);
            #endregion

        }
        if (ar.Length >= 2)
        {
            #region Send Email
            if (ar[0].Equals("SendEmailInline"))
            {
                ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
                int index = SafeValue.SafeInt(ar[1], 0);
                string Id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
                //Helper.Email.SendEmail();
                //C2.Email.SendEmail();
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + Id + "'");
                C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
                string email1 = SafeValue.SafeString(jo.Email1, "");
                string email2 = SafeValue.SafeString(jo.Email2, "");


                ProcessFile(Id, orderNo.Text, email1, email2);
                e.Result = "Success";
            }
            #endregion
        }
        if (ar.Length >= 2)
        {
            #region Update Line
            if (ar[0].Equals("UpdateInline"))
            {
                ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
                int index = SafeValue.SafeInt(ar[1], 0);
                string Id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
                bool action = Update_Inline(grid, Id, e, SafeValue.SafeInt(ar[1], -1));
                if (action)
                {
                    create_inv(grid, Id, e, SafeValue.SafeInt(ar[1], -1));
                    e.Result = "Success";
                }
                else
                    e.Result = "Error";
            }
            #endregion
        }
        if (ar.Length >= 2)
        {
            #region Upload PDF
            if (ar[0].Equals("UploadInline"))
            {
                ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
                int index = SafeValue.SafeInt(ar[1], 0);
                string Id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
                e.Result = Id;
            }
            #endregion
        }
        if (e.Parameters == "UpdateParty")
        {
            #region Update Party
            ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
            cargo_save(grid);
            #endregion
        }
        if (e.Parameters == "Vali")
        {
            #region Vali Party
            ASPxGridView grd = sender as ASPxGridView;
            ASPxComboBox cmb_Responsible = grd.FindEditFormTemplateControl("cmb_Responsible") as ASPxComboBox;
            ASPxTextBox txt_Party = grd.FindEditFormTemplateControl("txt_Party") as ASPxTextBox;
            ASPxTextBox txt_ConsigneeRemark = grd.FindEditFormTemplateControl("txt_ConsigneeRemark") as ASPxTextBox;
            ASPxTextBox txt_ConsigneeEmail = grd.FindEditFormTemplateControl("txt_ConsigneeEmail") as ASPxTextBox;
            if (SafeValue.SafeString(cmb_Responsible.Value) == "PERSON")
            {
                string sql = string.Format(@"select count(*) from xxparty where CrNo='{0}' and Name like '{1}%'", txt_ConsigneeRemark.Text, txt_Party.Text);
                int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (n > 0)
                {
                    e.Result = "OK";
                }
            }
            if (SafeValue.SafeString(cmb_Responsible.Value) == "COMPANY")
            {
                string sql = string.Format(@"select count(*) from xxparty where CrNo='{0}' and Name like '{1}%'", txt_ConsigneeEmail.Text, txt_Party.Text);
                int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (n > 0)
                {
                    e.Result = "OK";
                }
            }

            #endregion
        }
        if (e.Parameters == "ValiUen")
        {
            #region Vali UEN Party
            ASPxGridView grd = sender as ASPxGridView;
            ASPxComboBox cmb_Responsible = grd.FindEditFormTemplateControl("cmb_Responsible") as ASPxComboBox;
            ASPxTextBox txt_Party = grd.FindEditFormTemplateControl("txt_Party") as ASPxTextBox;
            ASPxTextBox txt_ConsigneeRemark = grd.FindEditFormTemplateControl("txt_ConsigneeRemark") as ASPxTextBox;
            ASPxTextBox txt_ConsigneeEmail = grd.FindEditFormTemplateControl("txt_ConsigneeEmail") as ASPxTextBox;
            string sql = string.Format(@"select count(*) from xxparty where CrNo='{0}' and Name like '{1}%'", txt_ConsigneeRemark.Text, txt_Party.Text);
            int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (n == 0)
            {
                e.Result = "OK";
            }
            #endregion
        }
        if (ar.Length >= 2)
        {
            #region  Create Invoice
            if (ar[0].Equals("CreateInvInline"))
            {
                ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
                int index = SafeValue.SafeInt(ar[1], 0);
                string Id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
                string name = SafeValue.SafeString(grid.GetRowValues(index, "ConsigneeInfo"));
                string partyTo = EzshipHelper.GetPartyId(name);
                string sql = string.Format(@"select count(*) from XAArInvoice where MastRefNo='{0}' and PartyTo='{1}'", orderNo.Text, partyTo);
                int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (n == 0)
                {
                    create_inv(grid, Id, e, SafeValue.SafeInt(ar[1], -1));
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Error";
                }

            }
            #endregion
        }

    }
    protected void ProcessFile(string Id, string orderNo, string email1, string email2)
    {
        DateTime now = DateTime.Now;
        string pathTemp = HttpContext.Current.Server.MapPath("~/excel/arrival_notice.xlsx").ToLower();

        #region
        string file = string.Format(@"arrival_notice_{1:yyyyMMdd}.xlsx", "", now.Date).ToLower();
        string path1 = string.Format("~/files/{0}/", Id);
        string path2 = path1.Replace(' ', '_').Replace('\'', '_');
        string pathx = path2.Substring(1);
        string path3 = MapPath(path2);
        if (!Directory.Exists(path3))
            Directory.CreateDirectory(path3);
        string pathUrl = string.Format("~/files/{0}/{1}", Id, file);
        string pathOpen = HttpContext.Current.Server.MapPath(pathUrl).ToLower();
        using (FileStream input = File.OpenRead(pathTemp), output = File.OpenWrite(pathOpen))
        {
            int read = -1;
            byte[] buffer = new byte[1024];
            while (read != 0)
            {
                read = input.Read(buffer, 0, buffer.Length);
                output.Write(buffer, 0, read);
            }
        }
        string sql = string.Format(@"select det.ConsigneeInfo,c.FtType,mast.Carrier,mast.Eta,mast.VesselNo,mast.VoyNo,mast.Pol,det.ClientId,det.Remark1,mast.BookingNo,mast.BookingNo2,det.Remark1,det.Remark2,det.Weight,det.M3,det.ExpBkgN,det.ContNo,det.SealNo,c.Remark,c.Ft20,c.Ft40,c.Ft45,mast.Carrier from CTM_Job mast inner join job_house det on det.JobNo=mast.JobNo inner join CTM_JobDet1 c on c.JobNo=mast.JobNo where det.Id={0}", Id);
        DataTable dt = ConnectSql.GetTab(sql);
        #endregion
        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\bin\License.lic"));
        Workbook workbook = new Workbook();
        workbook.Open(pathOpen);

        Worksheet sheet0 = workbook.Worksheets[0];
        Cells c1 = sheet0.Cells;
        if (dt.Rows.Count > 0)
        {
            #region
            int ft20 = SafeValue.SafeInt(dt.Rows[0]["Ft20"], 0);
            int ft40 = SafeValue.SafeInt(dt.Rows[0]["Ft40"], 0);
            int ft45 = SafeValue.SafeInt(dt.Rows[0]["Ft45"], 0);
            string ftType = SafeValue.SafeString(dt.Rows[0]["FtType"]);
            c1[10, 1].PutValue(now.ToString("dd-MMM-yy"));
            c1[12, 1].PutValue(dt.Rows[0]["ConsigneeInfo"]);
            string sql_contact = string.Format(@"select Contact1 from xxparty where Name='{0}'", dt.Rows[0]["ConsigneeInfo"].ToString());
            string contact = ConnectSql_mb.ExecuteScalar(sql_contact);
            c1[13, 1].PutValue(contact);
            string sql_carrier = string.Format(@"select Name from xxparty where PartyId='{0}'", dt.Rows[0]["Carrier"].ToString());
            string carrier = ConnectSql_mb.ExecuteScalar(sql_carrier);
            c1[17, 1].PutValue(carrier);
            c1[18, 1].PutValue(dt.Rows[0]["BookingNo"]);
            c1[19, 1].PutValue(dt.Rows[0]["ExpBkgN"]);
            c1[20, 1].PutValue(dt.Rows[0]["VesselNo"] + " / " + dt.Rows[0]["VoyNo"]);
            c1[21, 1].PutValue(SafeValue.SafeDate(dt.Rows[0]["Eta"], now.Date).ToString("dd.MM.yyyy"));
            string sql_pol = string.Format(@"select Name from XXPort where Code='{0}'", dt.Rows[0]["Pol"].ToString());
            string pol = ConnectSql_mb.ExecuteScalar(sql_pol);
            c1[22, 1].PutValue(pol);
            c1[24, 1].PutValue(dt.Rows[0]["ContNo"]);
            c1[24, 3].PutValue(dt.Rows[0]["SealNo"]);
            c1[25, 1].PutValue(dt.Rows[0]["Remark2"]);
            if (ft20 > 0)
            {
                c1[26, 1].PutValue(string.Format(@"PART OF THE {0} X 20{1} CONTAINER CONSIST OF 20 CARTONS", ft20, ftType));
            }
            if (ft40 > 0)
            {
                c1[26, 1].PutValue(string.Format(@"PART OF THE {0} X 40{1} CONTAINER CONSIST OF 20 CARTONS", ft40, ftType));
            }
            if (ft45 > 0)
            {
                c1[26, 1].PutValue(string.Format(@"PART OF THE {0} X 45{1} CONTAINER CONSIST OF 20 CARTONS", ft45, ftType));
            }
            sql = string.Format(@"select sum(Price2) as SumAmt from job_stock where JobDetId={0}", Id);
            decimal declared = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
            sql = string.Format(@"select MiscFee from job_house where Id={0}", Id);
            decimal miscFee = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));

            c1[27, 1].PutValue(declared + miscFee);
            c1[29, 1].PutValue(dt.Rows[0]["Weight"]);
            //c1[30, 1].PutValue(dt.Rows[0]["ExpBkgN"]);

            #endregion
        }
        workbook.Save(pathOpen, FileFormatType.Excel2003);


        Workbook wb = new Workbook();
        wb.Open(pathOpen);
        file = string.Format(@"arrival_notice_{1:yyyyMMdd}.pdf", "", now.Date).ToLower();
        path1 = string.Format("~/files/{0}/", Id);
        path2 = path1.Replace(' ', '_').Replace('\'', '_');
        pathx = path2.Substring(1);
        path3 = MapPath(path2);
        pathUrl = string.Format("~/files/{0}/{1}", Id, file);
        pathOpen = HttpContext.Current.Server.MapPath(pathUrl).ToLower();
        //Save the excel file to PDF format
        wb.Save(pathOpen, FileFormatType.Pdf);
        string company = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
        string address1 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"];
        string address2 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"];
        string address3 = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
        string add = address1 + " " + address2 + " " + address3;
        string mes = string.Format(@"<b>{0}</b><br><br>
{1}<br><br>
<b>Dear Customer, <br><br>Kindly review attached document for ARRIVAL NOTICE.</b>
<br><br>
<b>This is a computer generated email, please DO NOT reply.
<br><br>
Pls email to : xglogistic@ugroup.com.sg for any questions.
</b><br><br>
***IMPORTANT NOTICE***<br><br>

1.     Pls wait for our delivery department to call you for delivery arrangement<br><br>

2.     Delivery arrangement will be made within 3 working days from the date of unstuffing of container.<br><br>
<br><br>", company, add);
        bool action = false;
        if (email1.Length > 0)
        {
            Helper.Email.SendEmail(email1, "BRYAN.HU@ugroup.com.sg", "xglogistic@ugroup.com.sg", "ARRIVAL NOTICE/DOCS CONFIRMATION", mes, pathUrl);
            action = true;
        }
        if (email2.Length > 0)
        {
            if (action)
                Helper.Email.SendEmail(email2, "", "", "ARRIVAL NOTICE/DOCS CONFIRMATIO", mes, pathUrl);
            else
                Helper.Email.SendEmail(email2, "BRYAN.HU@ugroup.com.sg", "xglogistic@ugroup.com.sg", "ARRIVAL NOTICE/DOCS CONFIRMATIO", mes, pathUrl);
        }
        //"BRYAN.HU@ugroup.com.sg", "xglogistic@ugroup.com.sg"
    }
    private void updateStatus(ASPxGridView grid)
    {
        try
        {
            ASPxTextBox txt_cargo_id = grid.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
            string oId = txt_cargo_id.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + oId + "'");
            C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
            if (jo.CargoStatus == "USE")
            {
                jo.CargoStatus = "ORDER";
            }
            if (jo.CargoStatus == "ORDER")
            {
                jo.CargoStatus = "IN";
            }
            else if (jo.CargoStatus == "IN")
            {
                jo.CargoStatus = "PICKED";
            }
            else if (jo.CargoStatus == "PICKED")
            {
                jo.CargoStatus = "OUT";
            }
            C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(jo);
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }
    private bool Update_Inline(ASPxGridView grid, string Id, ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        bool action = false;
        if (rowIndex < 0)
        {
            return action;
        }

        #region
        ASPxCheckBox ckb_Prepaid_Ind = grid.FindRowCellTemplateControl(rowIndex, null, "ckb_Prepaid_Ind") as ASPxCheckBox;
        ASPxSpinEdit spin_OtherFee1 = grid.FindRowCellTemplateControl(rowIndex, null, "spin_OtherFee1") as ASPxSpinEdit;
        ASPxSpinEdit spin_CollectAmount1 = grid.FindRowCellTemplateControl(rowIndex, null, "spin_CollectAmount1") as ASPxSpinEdit;
        ASPxSpinEdit spin_CollectAmount2 = grid.FindRowCellTemplateControl(rowIndex, null, "spin_CollectAmount2") as ASPxSpinEdit;
        #endregion

        if (SafeValue.SafeDecimal(spin_OtherFee1.Value) > 0)
        {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            string prepaidInd = "NO";
            if (ckb_Prepaid_Ind.Checked)
            {
                prepaidInd = "YES";
            }
            string sql = string.Format(@"update job_house set OtherFee1=@OtherFee1,Collect_Amount1=@Amt1,Collect_Amount2=@Amt2,Prepaid_Ind=@PrepaidInd where Id=@Id");
            #region list
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@OtherFee1", SafeValue.SafeDecimal(spin_OtherFee1.Value), SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@Amt1", SafeValue.SafeDecimal(spin_CollectAmount1.Value), SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@Amt2", SafeValue.SafeDecimal(spin_CollectAmount2.Value), SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@PrepaidInd", prepaidInd, SqlDbType.NVarChar));

            list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.NVarChar));
            #endregion
            if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
            {
                action = true;
                //e.Result = "Save Success";
            }
        }
        return action;
    }
    private void create_inv(ASPxGridView grid, string Id, ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_OrderNo = pageControl.FindControl("txt_OrderNo") as ASPxTextBox;
        ASPxComboBox cmb_Fee1CurrId = grid.FindEditFormTemplateControl("cmb_Fee1CurrId") as ASPxComboBox;
        string client = "";
        string client1 = "";
        string client2 = "";
        string party = "";
        string prepaid = "";
        decimal amt1 = 0;
        decimal amt2 = 0;
        string dutyPayment = "";
        decimal fee1 = 0;
        decimal fee2 = 0;
        decimal miscFee = 0;
        decimal fee3 = 0;
        decimal fee4 = 0;
        string cntNo = "";
        bool isbill = false;
        string checkIn = "";
        decimal amt = 0;
        string currId = "";
        decimal exrate = 1;
        string markingNo = "";
        string sql = string.Format(@"select ExpBkgN,ConsigneeInfo,Prepaid_Ind,Collect_Amount1,Collect_Amount2,Duty_Payment,OtherFee1,IsBill,OtherFee2,MiscFee,OtherFee3,OtherFee4,ContNo,Fee1CurrId,CheckIn from job_house where Id={0}", Id);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            #region
            currId = SafeValue.SafeString(dt.Rows[0]["Fee1CurrId"]);
            if (currId != "SGD")
            {
                if (currId == "RMB")
                    currId = "CNY";
                string sql_rate = string.Format(@"select currencyExRate from XXCurrency where currencyId='{0}'", currId);
                exrate = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql_rate));
            }

            cntNo = SafeValue.SafeString(dt.Rows[0]["ContNo"]);
            prepaid = SafeValue.SafeString(dt.Rows[0]["Prepaid_Ind"]);
            dutyPayment = SafeValue.SafeString(dt.Rows[0]["Duty_Payment"]);
            isbill = SafeValue.SafeBool(dt.Rows[0]["IsBill"], true);
            party = SafeValue.SafeString(dt.Rows[0]["ConsigneeInfo"]);
            checkIn = SafeValue.SafeString(dt.Rows[0]["CheckIn"]);
            markingNo = SafeValue.SafeString(dt.Rows[0]["ExpBkgN"]);
            if (prepaid == "YES")
            {
                client = "EAST INTERNATIONAL";
            }
            if (dutyPayment.Equals("DUTY PAID"))
            {
                client1 = "EAST INTERNATIONAL";
            }
            if (checkIn == "PAID")
            {
                client2 = "EAST INTERNATIONAL";
            }

            amt1 = SafeValue.SafeDecimal(dt.Rows[0]["Collect_Amount1"]);
            amt2 = SafeValue.SafeDecimal(dt.Rows[0]["Collect_Amount2"]);
            if (amt1 > 0)
            {
                string sql_rate = string.Format(@"select currencyExRate from XXCurrency where currencyId='CNY'");
                decimal rate = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql_rate));
                amt = rate * amt1;
            }
            if (amt2 > 0)
            {
                amt = amt2;
            }
            fee1 = SafeValue.SafeDecimal(dt.Rows[0]["OtherFee1"]);
            fee2 = SafeValue.SafeDecimal(dt.Rows[0]["OtherFee2"]);
            fee3 = SafeValue.SafeDecimal(dt.Rows[0]["OtherFee3"]) * exrate;
            fee4 = SafeValue.SafeDecimal(dt.Rows[0]["OtherFee4"]) * exrate;
            miscFee = SafeValue.SafeDecimal(dt.Rows[0]["MiscFee"]) * exrate;
            #endregion
        }
        #region 代收/COLLECT ON BEHALF
        if (client == "EAST INTERNATIONAL")
        {
            decimal[] fee = { amt };
            string[] chgCode = { "OTHERS" };
            string[] chgDes = { "Payable" };
            insert_inv(txt_OrderNo.Text, client, fee, markingNo, chgCode, chgDes);
        }
        else
        {
            decimal[] fee = { amt };
            string[] chgCode = { "OTHERS" };
            string[] chgDes = { "Payable" };
            insert_inv(txt_OrderNo.Text, party, fee, markingNo, chgCode, chgDes);
        }
        #endregion
        #region GST/税
        if (client1 == "EAST INTERNATIONAL")
        {
            decimal[] fee = { fee1 };
            string[] chgCode = { "GST" };
            string[] chgDes = { "DUTY PAID" };
            if (fee1 > 0)
                insert_inv(txt_OrderNo.Text, client1, fee, markingNo, chgCode, chgDes);
        }
        else
        {
            decimal[] fee = { fee1 };
            string[] chgCode = { "GST" };
            string[] chgDes = { "DUTY UNPAID" };
            if (fee1 > 0)
                insert_inv(txt_OrderNo.Text, party, fee, markingNo, chgCode, chgDes);
        }
        #endregion
        #region 是否单独清关/Permit Fee
        if (client2 == "EAST INTERNATIONAL")
        {
            decimal[] fee = { fee2 };
            string[] chgCode = { "Permit Fee" };
            string[] chgDes = { "PAID" };
            if (fee2 > 0)
                insert_inv(txt_OrderNo.Text, client2, fee, markingNo, chgCode, chgDes);
        }
        else
        {
            decimal[] fee = { fee2 };
            string[] chgCode = { "Permit Fee" };
            string[] chgDes = { "UNPAID" };
            if (fee2 > 0)
            {
                insert_inv(txt_OrderNo.Text, party, fee, markingNo, chgCode, chgDes);
            }

        }
        #endregion
        #region 手续费/Handling Fee
        if (fee3 > 0)
        {
            decimal[] fee = { fee3 };
            string[] chgCode = { "Handling" };
            string[] chgDes = { "Handling Fee" };
            if (fee3 > 0)
                insert_inv(txt_OrderNo.Text, party, fee, markingNo, chgCode, chgDes);
        }
        #endregion
        #region 其他/Other Fee
        if (fee4 > 0)
        {
            decimal[] fee = { fee4 };
            string[] chgCode = { "OTHERS" };
            string[] chgDes = { "Other Fee" };
            if (fee4 > 0)
                insert_inv(txt_OrderNo.Text, party, fee, markingNo, chgCode, chgDes);
        }
        #endregion
        #region Group By CFS Invoice To DONGJI
        string sql_group = string.Format(@"select GroupId from xxparty where Name='{0}'", party);
        string groupId = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql_group));
        if (groupId == "CFS")
        {
            string sql_rate = string.Format(@"select currencyExRate from XXCurrency where currencyId='CNY'");
            decimal rate = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql_rate));
            amt = 3000;
            decimal[] fee = { amt };
            string[] chgCode = { "OTHERS " };
            string[] chgDes = { "OTHERS FEE FOR DONGJI" };
            insert_inv(txt_OrderNo.Text, "EAST INTERNATIONAL", fee, markingNo, chgCode, chgDes);
        }
        #endregion
        #region 海运费/OCEAN FREIGHT
        //if (miscFee>0){
        //    decimal[] fee = { miscFee };
        //    string[] chgCode = { "OCEAN FREIGHT"};
        //    string[] chgDes = { "OCEAN FREIGHT"};
        //    if(miscFee>0)
        //        insert_inv(txt_OrderNo.Text, party, fee, markingNo, chgCode, chgDes);
        //}
        #endregion
    }
    protected void insert_inv(string jobNo, string client, decimal[] fee, string markingNo, string[] chgCode, string[] chgDes)
    {
        string user = HttpContext.Current.User.Identity.Name;
        DateTime dtime = DateTime.Now;
        string invN = C2Setup.GetNextNo("", "AR-IV", dtime);

        string partyTo = EzshipHelper.GetPartyId(client);
        string sql_part1 = "";
        string sql = string.Format(@"select SequenceId from XAArInvoice where PartyTo='{0}' and MastRefNo='{1}'", partyTo, jobNo);
        string docId = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (docId.Length == 0)
        {
            #region Create Invoice
            sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo,JobRefNo)
values('IV',getdate(),'{4}','{0}',Year(getdate()),Month(getdate()),'CASH',getdate(),'',
'SGD','CI',1,'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}','{5}')
select @@IDENTITY", invN, user, "CS001", jobNo, partyTo, markingNo);
            docId = ConnectSql_mb.ExecuteScalar(sql);
            C2Setup.SetNextNo("", "AR-IV", invN, dtime);
            sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType)
values ");
            sql = "";
            for (int i = 0; i < fee.Length; i++)
            {
                if (fee[i] > 0)
                {
                    string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','RV001','CR','{3}','{4}','Z',1,{5},'','SGD',1,0,0,{5},{5},{5},'{6}','{7}','{8}')", docId, invN, 1, chgCode[i].ToString(), chgDes[i].ToString(), SafeValue.SafeDecimal(fee[i]), jobNo, markingNo, "I");
                    sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
                }
            }
            #endregion
        }
        else
        {
            #region Create Invoice Det
            sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType)
values ");
            sql = "";
            for (int i = 0; i < fee.Length; i++)
            {
                if (fee[i] > 0)
                {
                    string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','RV001','CR','{3}','{4}','Z',1,{5},'','SGD',1,0,0,{5},{5},{5},'{6}','{7}','{8}')", docId, invN, 1, chgCode[i].ToString(), chgDes[i].ToString(), SafeValue.SafeDecimal(fee[i]), jobNo, markingNo, "I");
                    sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
                }
            }
            #endregion
        }
        if (sql.Length > 0)
        {
            sql = sql_part1 + sql;
            int re = ConnectSql.ExecuteSql(sql);
            UpdateMaster(SafeValue.SafeInt(docId, 0));
        }
    }
    private void UpdateMaster(int docId)
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

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    public string ShowUom(object s)
    {
        string uom = SafeValue.SafeString(s);
        string str = "";
        if (uom == "CTN")
        {
            str = "CTN/箱";
        }
        if (uom == "PKG")
        {
            str = "PKG/件";
        }
        if (uom == "BAG")
        {
            str = "BAG/包";
        }
        if (uom == "PAL")
        {
            str = "PAL/卡板";
        }
        if (uom == "TON")
        {
            str = "TON/吨";
        }
        return str;
    }
    public string ShowFile(string Id)
    {
        string str = "";
        string sql = string.Format(@"select top 1 FilePath from CTM_Attachment where RefNo='{0}'", Id);
        string path = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (path.Length > 0)
        {
            str = "/Photos/" + path.Replace("\\", "/");
        }
        return str;
    }
    protected void grd_Det_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        if (grid.EditingRowVisibleIndex > -1)
        {
            string oid = SafeValue.SafeString(grid.GetRowValues(grid.EditingRowVisibleIndex, new string[] { "Id" }));
            ASPxCheckBox ckb_Prepaid_Ind = grid.FindEditFormTemplateControl("ckb_Prepaid_Ind") as ASPxCheckBox;
            ASPxButton btn_Status = grid.FindEditFormTemplateControl("btn_Status") as ASPxButton;
            ASPxLabel lbl_Status = grid.FindEditFormTemplateControl("lbl_Status") as ASPxLabel;
            string sql = string.Format(@"select CargoStatus from job_house where Id={0}", oid);
            string status = SafeValue.SafeString(Helper.Sql.One(sql));
            #region Cargo Status
            if (status == "USE")
            {
                lbl_Status.Text = "待确认";
                btn_Status.Visible = false;

            }
            if (status == "DEPARTURE")
            {
                lbl_Status.Text = "已出港";
                btn_Status.Text = "抵港";
            }
            if (status == "ARRIVED")
            {
                lbl_Status.Text = "已抵港";
                btn_Status.Text = "入库";
            }
            else if (status == "GR")
            {
                lbl_Status.Text = "已入库";
                btn_Status.Text = "出库";
            }
            else if (status == "DO")
            {
                lbl_Status.Text = "已出库";
                btn_Status.Text = "派送中";
            }
            else if (status == "SEND")
            {
                lbl_Status.Text = "派送中";
                btn_Status.Text = "派送完成";
            }
            else if (status == "COMPLETED")
            {
                lbl_Status.Text = "派送完成";
                btn_Status.Visible = false;
            }
            #endregion
            #region IsBill
            ASPxComboBox cmb_IsBill = grid.FindEditFormTemplateControl("cmb_IsBill") as ASPxComboBox;
            sql = string.Format(@"select IsBill from job_house where Id={0}", oid);
            bool isBill = SafeValue.SafeBool(Helper.Sql.One(sql), true);
            if (isBill)
            {
                cmb_IsBill.SelectedIndex = 0;
            }
            else
            {
                cmb_IsBill.SelectedIndex = 1;
            }
            #endregion
            #region Total Amt
            ASPxLabel lbl_stockamt = grid.FindEditFormTemplateControl("lbl_stockamt") as ASPxLabel;
            ASPxLabel lbl_total = grid.FindEditFormTemplateControl("lbl_total") as ASPxLabel;

            sql = string.Format(@"select sum(Price2) from job_stock where OrderId={0}", oid);
            decimal totalAmt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
            lbl_stockamt.Text = SafeValue.SafeString(totalAmt);

            sql = string.Format(@"select Ocean_Freight from job_house where Id={0}", oid);
            decimal miscFee = SafeValue.SafeDecimal(Helper.Sql.One(sql));

            sql = string.Format(@"select sum(Price2) from job_stock where OrderId={0}", oid);
            totalAmt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
            lbl_total.Text = SafeValue.SafeString(totalAmt + miscFee);
            #endregion
        }
    }
    protected bool VilaParty(string code, string uen, string ic, string email1, string email2, string tel1, string tel2, string mobile1, string mobile2, string address)
    {
        bool action = false;
        string sql = string.Format(@"select count(*) from XXParty where Name like '%{0}%'", code);
        int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        if (n > 0)
        {
            action = true;
        }
        else
        {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = null;
            #region list add
            if (code.Length > 4)
            {
                cpar = new ConnectSql_mb.cmdParameters("@PartyId", code.Substring(0, 4), SqlDbType.NVarChar, 100);
                list.Add(cpar);
            }
            else
            {
                cpar = new ConnectSql_mb.cmdParameters("@PartyId", code, SqlDbType.NVarChar, 100);
                list.Add(cpar);
            }
            cpar = new ConnectSql_mb.cmdParameters("@Code", code, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Name", code, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Status", "USE", SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Address", address, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Tel1", tel1, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Tel2", tel2, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Fax1", mobile1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Fax2", mobile1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Email1", email1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Email2", email2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            sql = string.Format(@"insert into XXParty(PartyId,Code,Name,IsCustomer,Status,Address,Tel1,Tel2,Fax1,Fax2,Email1,Email2) values(@PartyId,@Code,@Name,1,@Status,@Address,@Tel1,@Tel2,@Fax1,@Fax2,@Email1,@Email2)");
            ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
            if (re.status)
            {
                action = true;
            }
            else
            {
            }
            #endregion
        }
        return action;
    }
    protected bool UpdateParty(string name, string ic, string uen, string email1, string email2, string tel1, string tel2, string mobile1, string mobile2, string address)
    {
        string sql = string.Format(@"select count(*) from XXParty where Name like '{0}%' and (CrNo='{1}' or CrNo='{2}')", name, uen, ic);
        int count = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        bool action = false;
        if (count > 0)
        {
            #region  Update Party
            string code = "";
            if (name.Length > 4)
            {
                code = name.Substring(0, 4);
            }
            else
            {
                code = name;
            }
            if (ic.Length > 0)
            {
                sql = string.Format(@"update XXParty set Code='{0}',Name='{1}',Address='{2}',Tel1='{3}',Tel2='{4}',Fax1='{5}',Fax2='{6}',Email1='{7}',Email2='{8}',CrNo='{9}' where Name like '{0}' or CrNo='{9}'", code, name, address, tel1, mobile1, "", "", email1, email2, ic);
                int n = ConnectSql_mb.ExecuteNonQuery(sql);
                if (n > 0)
                {
                    action = true;
                }
            }
            if (uen.Length > 0)
            {
                sql = string.Format(@"update XXParty set Code='{0}',Name='{1}',Address='{2}',Tel1='{3}',Tel2='{4}',Fax1='{5}',Fax2='{6}',Email1='{7}',Email2='{8}',CrNo='{9}' where Name like '{0}' or CrNo='{9}'", code, name, address, tel1, tel2, mobile1, mobile2, email1, email2, uen);
                int n = ConnectSql_mb.ExecuteNonQuery(sql);
                if (n > 0)
                {
                    action = true;
                }
            }
            #endregion
        }
        return action;
    }
    protected void gridParty_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] PartyId = new object[grid.VisibleRowCount];
        object[] Name = new object[grid.VisibleRowCount];
        object[] CrNo = new object[grid.VisibleRowCount];
        object[] Address = new object[grid.VisibleRowCount];
        object[] Tel1 = new object[grid.VisibleRowCount];
        object[] Tel2 = new object[grid.VisibleRowCount];
        object[] Fax1 = new object[grid.VisibleRowCount];
        object[] Fax2 = new object[grid.VisibleRowCount];
        object[] Email1 = new object[grid.VisibleRowCount];
        object[] Email2 = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            PartyId[i] = grid.GetRowValues(i, "PartyId");
            Name[i] = grid.GetRowValues(i, "Name");
            CrNo[i] = grid.GetRowValues(i, "CrNo");
            Address[i] = grid.GetRowValues(i, "Address");
            Tel1[i] = grid.GetRowValues(i, "Tel1");
            Tel2[i] = grid.GetRowValues(i, "Tel2");
            Fax1[i] = grid.GetRowValues(i, "Fax1");
            Fax2[i] = grid.GetRowValues(i, "Fax2");
            Email1[i] = grid.GetRowValues(i, "Email1");
            Email2[i] = grid.GetRowValues(i, "Email2");
        }
        e.Properties["cpPartyId"] = PartyId;
        e.Properties["cpName"] = Name;
        e.Properties["cpCrNo"] = CrNo;
        e.Properties["cpAddress"] = Address;
        e.Properties["cpTel1"] = Tel1;
        e.Properties["cpTel2"] = Tel2;
        e.Properties["cpFax1"] = Fax1;
        e.Properties["cpFax2"] = Fax2;
        e.Properties["cpEmail1"] = Email1;
        e.Properties["cpEmail2"] = Email2;
    }
    protected void btn_CreateInv_Init(object sender, EventArgs e)
    {
        ASPxButton btn = sender as ASPxButton;
        ASPxGridView grid = sender as ASPxGridView;
        GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;

        btn.Text = "Create Inv";
        btn.ClientInstanceName = String.Format("btn_CreateInv{0}", container.VisibleIndex);
        btn.ClientSideEvents.Click = String.Format("function (s, e) {{ create_inv_inline({0}); }}", container.VisibleIndex);

    }
    #endregion

    #region Stock
    protected void grd_Stock_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobStock));
        }
    }
    protected void grd_Stock_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        string Id = SafeValue.SafeString(grid.GetMasterRowKeyValue());
        e.NewValues["OrderId"] = Id;
        e.NewValues["JobNo"] = "0";
        string sql = string.Format(@"select count(*) from job_house where OrderId={0}", Id);
        int n = SafeValue.SafeInt(Helper.Sql.One(sql), 0);
        if (n == 0)
        {
            e.NewValues["SortIndex"] = 1;
        }
        else
        {
            n++;
            e.NewValues["SortIndex"] = n;
        }
    }
    protected void grd_Stock_BeforePerformDataSelect(object sender, EventArgs e)
    {
        //ASPxGridView grd = sender as ASPxGridView;
        //ASPxTextBox txt_cargo_id = grd.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
        //this.dsJobStock.FilterExpression = "OrderId=" + SafeValue.SafeInt(txt_cargo_id.Text, 0) + "";// 
        ASPxGridView grid = sender as ASPxGridView;
        string Id = SafeValue.SafeString(grid.GetMasterRowKeyValue());
        this.dsJobStock.FilterExpression = "OrderId=" + SafeValue.SafeInt(Id, 0) + "";// 

    }
    protected void grd_Stock_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        string Id = SafeValue.SafeString(grid.GetMasterRowKeyValue());
        e.NewValues["OrderId"] = SafeValue.SafeInt(Id, 0);
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"], 0);
        e.NewValues["Marking1"] = SafeValue.SafeString(e.NewValues["Marking1"]);
        e.NewValues["Marking2"] = SafeValue.SafeString(e.NewValues["Marking2"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]));
        string sql = string.Format(@"select M3 from job_house where Id={0}", Id);
        decimal m3 = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
        if (SafeValue.SafeString(e.NewValues["Marks2"]) == "海运费")
        {
            e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]) * m3);
        }
    }
    protected void grd_Stock_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        string Id = SafeValue.SafeString(grid.GetMasterRowKeyValue());
        e.NewValues["JobDetId"] = SafeValue.SafeInt(Id, 0);
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"], 0);
        e.NewValues["Marks1"] = SafeValue.SafeString(e.NewValues["Marks1"]);
        e.NewValues["Marks2"] = SafeValue.SafeString(e.NewValues["Marks2"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]));
        string sql = string.Format(@"select M3 from job_house where Id={0}", Id);
        decimal m3 = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
        if (SafeValue.SafeString(e.NewValues["Marks2"]) == "海运费")
        {
            e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]) * m3);
        }
    }
    protected void grd_Stock_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Stock_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("UpdateStockInline"))
            {
                ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
                int index = SafeValue.SafeInt(ar[1], 0);
                string Id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
                grid.UpdateEdit();


                string jobDetId = SafeValue.SafeString(grid.GetMasterRowKeyValue());
                string sql = string.Format(@"select sum(Price2) from job_stock where JobDetId={0}", jobDetId);
                decimal totalAmt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                e.Result = totalAmt;
            }
        }
    }
    #endregion
}