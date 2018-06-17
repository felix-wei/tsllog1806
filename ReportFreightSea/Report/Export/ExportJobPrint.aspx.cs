using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;


public partial class ReportFreightSea_Report_Export_ExportJobPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmb_JobType.Focus();
            this.cmb_RefType.Text = "Export";
            this.date_From.Date = DateTime.Now.AddDays(-30);
            this.date_End.Date = DateTime.Now;
        }
    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        string refType = "";
        if (this.cmb_RefType.Value != null)
            refType = this.cmb_RefType.Value.ToString();
        string jobType = this.cmb_JobType.Text.ToString();
        DateTime fromTime = this.date_From.Date;
        DateTime toTime = this.date_End.Date;
        string sql = string.Format(@"select * from (select job.JobNo,mast.JobType,job.CustomerId,mast.Vessel,mast.Voyage,mast.Volume,mast.Eta,mast.Etd,mast.Pod,mast.Weight,isnull(tabCnt.Cnt,0) as IvCnt from SeaExport job inner join SeaExportRef mast on job.RefNo=mast.RefNo left join (select JobRefNo,COUNT(sequenceId) as Cnt from XAArInvoiceDet where XAArInvoiceDet.MastType='SE' group by JobRefNo) tabCnt on tabCnt.jobrefno=job.jobno where mast.RefType like '{2}%' and mast.StatusCode<>'CNL') as Tab  where Tab.IvCnt=0 and Etd>='{0}' and Etd<'{1}'", fromTime.ToString("yyyy-MM-dd"), toTime.AddDays(1).ToString("yyyy-MM-dd"), refType);
        if (jobType != "ALL")
            sql = string.Format(@"select * from (select job.JobNo,mast.JobType,job.CustomerId,mast.Vessel,mast.Voyage,mast.Volume,mast.Eta,mast.Etd,mast.Pod,mast.Weight,isnull(tabCnt.Cnt,0) as IvCnt from SeaExport job inner join SeaExportRef mast on job.RefNo=mast.RefNo left join (select JobRefNo,COUNT(sequenceId) as Cnt from XAArInvoiceDet where XAArInvoiceDet.MastType='SE' group by JobRefNo) tabCnt on tabCnt.JobRefNo=job.JobNo where (mast.RefType like '{2}%') and mast.JobType='{3}' and mast.StatusCode<>'CNL') as Tab  where Tab.IvCnt=0 and Etd>='{0}' and Etd<'{1}'", fromTime.ToString("yyyy-MM-dd"), toTime.AddDays(1).ToString("yyyy-MM-dd"), refType, jobType);
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