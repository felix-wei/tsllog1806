using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class WareHouse_Job_JobList : System.Web.UI.Page
{
	protected void Page_Init(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			this.txt_from.Date = DateTime.Today.AddDays(-30);
			this.txt_end.Date = DateTime.Today;
			EzshipHelper_Authority.Bind_Authority(this.Page);
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
			btn_search_Click(null, null);
	}

	protected void btn_search_Click(object sender, EventArgs e)
	{
		string type = "";
		if (Request.QueryString["type"] != null)
		{
			type = SafeValue.SafeString(Request.QueryString["type"]);
		}

		string where = "";
		string sql = string.Format(@"select * from JobInfo");
		string dateFrom = "";
		string dateTo = "";
		if (txt_from.Value != null && txt_end.Value != null)
		{
			dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
			dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
		}

		if (txt_PoNo.Text.Trim() != "")
			where = GetWhere(where, "JobNo='" + txt_PoNo.Text.Trim() + "'");
		else if (this.txt_CustId.Text.Length > 0)
		{
			where = GetWhere(where, "CusromerId='" + this.txt_CustId.Text.Trim() + "'");
		}

		if (dateFrom.Length > 0 && dateTo.Length > 0)
		{
			where = GetWhere(where, " JobDate >= '" + dateFrom + "' and JobDate < '" + dateTo + "'");
		}

		if(type!= "") {
			where = GetWhere(where, " JobStatus = '" + type + "'");
		}

		if (where.Length > 0)
		{
			sql += " where " + where + " order by JobNo";
		}

		DataTable tab = ConnectSql.GetTab(sql);
		this.grid.DataSource = tab;
		this.grid.DataBind();
	}

	private string GetWhere(string where, string s)
	{
		if (where.Length > 0)
			where += " and " + s;
		else
			where = s;
		return where;
	}

	protected string SaveNewJob()
	{
		try
		{
			ASPxDateEdit issueDate = ASPxPopupControl1.FindControl("date_IssueDate") as ASPxDateEdit;
			string issueN = "";
			JobInfo job = new JobInfo();
			issueN = C2Setup.GetNextNo("", "JobOrder", issueDate.Date);
			job.JobDate = issueDate.Date;

			job.JobStage = "Customer Inquir";
			ASPxComboBox cmb_JobType = ASPxPopupControl1.FindControl("cmb_JobType") as ASPxComboBox;
			job.JobType = SafeValue.SafeString(cmb_JobType.Value);
			//Main Info
            ASPxButtonEdit txt_CustomerId = ASPxPopupControl1.FindControl("txt_CustomerId") as ASPxButtonEdit;
			job.CustomerId = txt_CustomerId.Text;
			ASPxTextBox txt_CustomerName = ASPxPopupControl1.FindControl("txt_CustomerName") as ASPxTextBox;
			job.CustomerName = txt_CustomerName.Text;
			ASPxMemo memo_Address = ASPxPopupControl1.FindControl("memo_NewAddress") as ASPxMemo;
			job.CustomerAdd = memo_Address.Text;

			ASPxTextBox txt_PostalCode = ASPxPopupControl1.FindControl("txt_NewPostalCode") as ASPxTextBox;
			job.Postalcode = txt_PostalCode.Text;
			ASPxTextBox txt_Contact = ASPxPopupControl1.FindControl("txt_NewContact") as ASPxTextBox;
			job.Contact = txt_Contact.Text;
			ASPxTextBox txt_Tel = ASPxPopupControl1.FindControl("txt_NewTel") as ASPxTextBox;
			job.Tel = txt_Tel.Text;
			ASPxTextBox txt_Email = ASPxPopupControl1.FindControl("txt_NewEmail") as ASPxTextBox;
			job.Email = txt_Email.Text;
			ASPxTextBox txt_Fax = ASPxPopupControl1.FindControl("txt_NewFax") as ASPxTextBox;
			job.Fax = txt_Fax.Text;
			ASPxMemo remark = ASPxPopupControl1.FindControl("txt_NewRemark") as ASPxMemo;
			job.Remark = remark.Text;
			job.WorkStatus = "PENDING";

			string userId = HttpContext.Current.User.Identity.Name;

			job.CreateBy = userId;
			job.CreateDateTime = DateTime.Now;
			job.UpdateBy = userId;
			job.UpdateDateTime = DateTime.Now;
			job.JobNo = issueN;
			Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
			Manager.ORManager.PersistChanges(job);
			C2Setup.SetNextNo("", "JobOrder", issueN, issueDate.Date);

			return job.JobNo;
		}
		catch { }
		return "";
	}

	protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
	{
		string s = e.Parameters;
		if (s == "OK")
		{
			 Console.Write("JobNo:=========="+SaveNewJob());
            e.Result = SaveNewJob();
        }
    }
}