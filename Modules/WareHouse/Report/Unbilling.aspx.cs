using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;

public partial class ReportWarehouse_Report_Unbilling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = DateTime.Now.AddDays(-30);
            this.date_End.Date = DateTime.Now;
        } if (!IsPostBack)
        {
            this.date_From.Date = DateTime.Now.AddDays(-30);
            this.date_End.Date = DateTime.Now;
        }
    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        DateTime fromTime = this.date_From.Date;
        DateTime toTime = this.date_End.Date;
        string sql = string.Format(@"select * from( 
select DoNo,DoType,DoDate,PartyName,CustomerReference,Case when CustomerDate<'2010-01-01' then '' else Convert(nvarchar(10),CustomerDate,10) end as CustomerDate,DoStatus,
isnull((select COUNT(SequenceId) as cnt from XAArInvoice where MastType='WH' and (MastRefNo=d.DoNo or JobRefNO=d.DoNo)),0) as IvCnt
 from Wh_DO d where d.DoDate between '{0}' and '{1}') as tab where IvCnt=0", fromTime.ToString("yyyy-MM-dd"), toTime.AddDays(1).ToString("yyyy-MM-dd"));      
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.grid.DataSource = tab;
        this.grid.DataBind();

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRetrieve_Click(null, null);
        this.gridExport.WriteXlsToResponse("UnBillingList", true);
    }
}