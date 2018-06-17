using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PSA_Import_PSA_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_DateFrom.Date = DateTime.Now;
            //btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        if (search_DateFrom.Date < new DateTime(1900, 1, 1))
        {
            search_DateFrom.Date = DateTime.Now;
        }

        string sql = string.Format(@"select [BILLING COMPANY] as Customer,[BILL NUMBER] as BillNo,[BILL ITEM NUMBER] as BillItem,
[ACCOUNT NUMBER] as AccountNo,[BILL DATE] as BillDate,[CONTAINER NUMBER] as ContainerNo,[AMOUNT] as Amount,[FULL VESSEL NAME] as Vessel 
from psa_bill p where [CONTAINER NUMBER] not in(select ContainerNo from CTM_JobDet1  det1 inner join CTM_Job job on det1.JobNo=job.JobNo where EtaDate < '{0}' )
and [BILL DATE] > '2018-03-31' and [TARIFF CODE] <> '7999' and [BILL DATE] < '{0}' and [AMOUNT]<>0", search_DateFrom.Date.ToString("yyyy-MM-dd"));
        //throw new Exception(sql);
		grid.DataSource = ConnectSql.GetTab(sql);
        grid.DataBind();
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("PSAContainerReport");
    }
}