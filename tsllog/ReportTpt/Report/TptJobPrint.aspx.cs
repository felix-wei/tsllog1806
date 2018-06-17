using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2;
using DevExpress.Printing;
using System.Data;

public partial class ReportTpt_Report_TptJobPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = DateTime.Now.AddDays(-30);
            this.date_End.Date = DateTime.Now;
        }
    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        DateTime fromTime = this.date_From.Date;
        DateTime toTime = this.date_End.Date;
        string sql = string.Format(@"select * from (select JobNo,[dbo].[fun_GetPartyName](Cust) as CustomerName,Vessel,Voyage,Eta,Etd,Pol,Wt,M3,Qty,isnull(tabCnt.Cnt,0) as IvCnt from TPT_Job job left join (select MastRefNo,COUNT(sequenceId) as Cnt from XAArInvoiceDet det where det.MastType='TPT'  group by MastRefNo) tabCnt on tabCnt.MastRefNo=job.jobno ) as tab where tab.IvCnt=0 and  Eta>='{0}' and Eta<'{1}'", fromTime.ToString("yyyy-MM-dd"), toTime.AddDays(1).ToString("yyyy-MM-dd"));
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.grid_Import.DataSource = tab;
        this.grid_Import.DataBind();

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRetrieve_Click(null, null);
        this.gridExport.WriteXlsToResponse("TransportJobList", true);
    }
}