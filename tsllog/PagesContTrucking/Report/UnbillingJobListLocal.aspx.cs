using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Report_UnbillingJobListLocal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
            search_DateTo.Date = DateTime.Now.AddDays(8);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select job.JobNo,job.JobType,job.JobDate,job.Cust,par.Code as Customer,job.Vessel,job.Voyage,job.Pol,job.Pod,job.Eta as EtaDate,job.Etd as EtdDate
from tpt_job as job 
left outer join xxparty as par on job.Cust=par.PartyId");
        if (search_DateFrom.Date < new DateTime(1900, 1, 1))
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
        }
        if (search_DateTo.Date < new DateTime(1900, 1, 1))
        {
            search_DateTo.Date = DateTime.Now.AddDays(8);
        }
        string where = string.Format(@" where job.StatusCode<>'CNL' and DATEDIFF(d,job.JobDate,'{0}')<=0 and DATEDIFF(d,job.JobDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date);
        if (search_ClientId.Text.Trim().Length > 0)
        {
            where += string.Format(@" and job.Cust='{0}'", search_ClientId.Text);
        }
        grid_Transport.DataSource = ConnectSql.GetTab(sql + where);
        grid_Transport.DataBind();
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("UnbillingJobList_Local" + (search_ClientId.Text.Length > 0 ? "_" + search_ClientId.Text : ""), true);
    }
}