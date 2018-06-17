using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Report_TripReportLocal : System.Web.UI.Page
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
        string sql = string.Format(@"select job.JobNo,job.JobType,job.JobDate,job.Cust,par.Code as Customer,job.BkgDate,job.BkgTime,job.TripCode,job.Driver,job.VehicleNo,job.PickFrm1,job.DeliveryTo1 
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
        string where = string.Format(@" where job.StatusCode<>'CNL' and DATEDIFF(d,job.BkgDate,'{0}')<=0 and DATEDIFF(d,job.BkgDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date);
        if (search_Driver.Text.Trim().Length > 0)
        {
            where += string.Format(@" and job.Driver='{0}'", search_Driver.Text);
        }
        grid_Transport.DataSource = ConnectSql.GetTab(sql + where);
        grid_Transport.DataBind();
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("TripReport_Local" + (search_Driver.Text.Length > 0 ? "_" + search_Driver.Text : ""), true);
    }
}