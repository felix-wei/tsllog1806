using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;
public partial class ReportAir_Report_UnbillingJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmb_RefType.Focus();
            this.date_From.Date = DateTime.Now.AddDays(-30);
            this.date_End.Date = DateTime.Now;
        }
    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        string docType = "";
        DateTime fromTime = this.date_From.Date;
        DateTime toTime = this.date_End.Date;
        string sql = string.Format(@"select * from (
select job.JobNo,job.CustomerId,mast.Volume,mast.FlightDate0,mast.FlightDate1,mast.AirportCode1,mast.Weight,isnull(tabCnt.Cnt,0) as IvCnt 
from air_job job inner join air_ref mast on job.RefNo=mast.RefNo 
left join (select JobRefNo,COUNT(sequenceId) as Cnt from XAArInvoiceDet where XAArInvoiceDet.MastType='AE' group by JobRefNo) tabCnt on tabCnt.jobrefno=job.jobno
where mast.StatusCode<>'CNL') as Tab  
where Tab.IvCnt=0 and FlightDate0>='{0}' and FlightDate0<'{1}' ", fromTime.ToString("yyyy-MM-dd"), toTime.AddDays(1).ToString("yyyy-MM-dd"));
        if (this.cmb_RefType.Text != "")
        {
            docType = this.cmb_RefType.Value.ToString();
            sql = string.Format(@"select * from (
select job.JobNo,job.CustomerId,mast.Volume,mast.FlightDate1,mast.FlightDate0,mast.AirportCode1,mast.Weight,isnull(tabCnt.Cnt,0) as IvCnt 
from air_job job inner join air_ref mast on job.RefNo=mast.RefNo 
left join (select JobRefNo,COUNT(sequenceId) as Cnt from XAArInvoiceDet where XAArInvoiceDet.MastType='AE' group by JobRefNo) tabCnt on tabCnt.JobRefNo=job.JobNo 
where mast.RefType='{2}' and mast.StatusCode<>'CNL') as Tab  
where Tab.IvCnt=0 and FlightDate0>='{0}' and FlightDate0<'{1}'", fromTime.ToString("yyyy-MM-dd"), toTime.AddDays(1).ToString("yyyy-MM-dd"), docType);
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        tab.Columns.Add("CustomerName", Type.GetType("System.String"));
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            DataRow row = tab.Rows[i];
            string Customer = EzshipHelper.GetPartyName(SafeValue.SafeString(row["CustomerId"], ""));
            tab.Rows[i]["CustomerName"] = Customer;
        }
        this.grid_Export.DataSource = tab;
        this.grid_Export.DataBind();

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRetrieve_Click(null, null);
        this.gridExport.WriteXlsToResponse("ExportJobList", true);
    }
}