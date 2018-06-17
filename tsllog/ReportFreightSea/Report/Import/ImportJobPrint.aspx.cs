using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;

public partial class ReportFreightSea_Report_Import_ImportJobPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmb_DocType.Focus();
            this.date_From.Date = DateTime.Now.AddDays(-30);
            this.date_End.Date = DateTime.Now;
        }        if (!IsPostBack)
        {
            this.cmb_DocType.Focus();
            this.date_From.Date = DateTime.Now.AddDays(-30);
            this.date_End.Date = DateTime.Now;
        }
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        string docType = this.cmb_DocType.Text.ToString();
        DateTime fromTime =this.date_From.Date;
        DateTime toTime = this.date_End.Date;
        string sql = string.Format(@"select * from (
select job.JobNo,mast.JobType,job.CustomerId,mast.Vessel,mast.Voyage,mast.Volume,mast.Eta,mast.Etd,mast.Pol,mast.Weight,isnull(tabCnt.Cnt,0) as IvCnt 
from SeaImport job 
inner join SeaImportRef mast on job.RefNo=mast.RefNo 
left join (select JobRefNo,COUNT(sequenceId) as Cnt from XAArInvoiceDet where XAArInvoiceDet.MastType='SI' group by JobRefNo) tabCnt on tabCnt.jobrefno=job.jobno
where mast.StatusCode<>'CNL' and Eta between '{0}' and '{1}'
) as Tab ", fromTime.ToString("yyyy-MM-dd"), toTime.AddDays(1).ToString("yyyy-MM-dd"));
          if (docType != "")
              sql = string.Format(@"select * from 
(
select job.JobNo,mast.JobType,job.CustomerId,mast.Vessel,mast.Voyage,mast.Volume,mast.Eta,mast.Etd,mast.Pol,mast.Weight,isnull(tabCnt.Cnt,0) as IvCnt 
from SeaImport job 
inner join SeaImportRef mast on job.RefNo=mast.RefNo 
left join (select JobRefNo,COUNT(sequenceId) as Cnt from XAArInvoiceDet where XAArInvoiceDet.MastType='SI' group by JobRefNo) tabCnt on tabCnt.JobRefNo=job.JobNo 
where mast.JobType='{2}' and mast.StatusCode<>'CNL'
) as Tab  where Tab.IvCnt=0 and Eta>='{0}' and Eta<'{1}'", fromTime.ToString("yyyy-MM-dd"), toTime.AddDays(1).ToString("yyyy-MM-dd"), docType);
          DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];              
         tab.Columns.Add("CustomerName", Type.GetType("System.String"));
          for (int i = 0; i < tab.Rows.Count; i++)
          {
              DataRow row = tab.Rows[i];
              string Customer = EzshipHelper.GetPartyName(SafeValue.SafeString(row["CustomerId"], ""));
              tab.Rows[i]["CustomerName"] = Customer;
          }
          this.grid_Import.DataSource = tab;
          this.grid_Import.DataBind();

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRetrieve_Click(null,null);
        this.gridExport.WriteXlsToResponse("ImportJobList", true);
    }
}