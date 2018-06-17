using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;


public partial class Modules_Hr_Job_UnHrPayment : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today;
            this.txt_to.Date = DateTime.Today;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
            btn_search_Click(null, null);
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        string fromdate = "";
        string todate = "";
        string where = "";
        if (txt_from.Value != ""&&txt_to.Value!="")
        {
            fromdate = txt_from.Date.ToString("yyyy-MM-dd");
            todate = this.txt_to.Date.ToString("yyyy-MM-dd");
        }
        string acCode = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["HrCode"]);
        string sql = string.Format(@"select * from (
select TotalAmt,JobTime,ISNULL(PayAmt,0) as PayAmt,(TotalAmt-ISNULL(PayAmt,0)) as DiffAmt,AcCode,Amount1,Amount2,Amount3,Amount4,Amount5 from (
select isnull(sum(Amount7),0) as TotalAmt, CONVERT(varchar(100), JobTime, 23) as JobTime, isnull(sum(Amount1),0) as Amount1,
isnull(sum(Amount2),0) as Amount2,isnull(sum(Amount3),0) as Amount3,isnull(sum(Amount4),0) as Amount4,isnull(sum(Amount5),0) as Amount5 from JobCrews  where Status='Pay'  group by CONVERT(varchar(100), JobTime, 23)) as job
left join (select sum(LocAmt) as PayAmt,CONVERT(varchar(100), DocDate, 23) as JobDate,MAX(AcCode) as AcCode from XAApPaymentDet where AcCode='{2}' group by CONVERT(varchar(100), DocDate, 23)) 
as pay_det on pay_det.JobDate=job.JobTime) as tab where (CONVERT(varchar(100), PayTime, 23) between '{0}' and '{1}') ", fromdate, todate,acCode);
        sql = string.Format(@"select * from (
select TotalAmt,JobTime,ISNULL(PayAmt,0) as PayAmt,(TotalAmt-ISNULL(PayAmt,0)) as DiffAmt,AcCode,Amount1,Amount2,Amount3,Amount4,Amount5 from (
select isnull(sum(Amount7),0) as TotalAmt, CONVERT(varchar(100), JobTime, 23) as JobTime, isnull(sum(Amount1),0) as Amount1,
isnull(sum(Amount2),0) as Amount2,isnull(sum(Amount3),0) as Amount3,isnull(sum(Amount4),0) as Amount4,isnull(sum(Amount5),0) as Amount5 from JobCrews  where Status='Pay' and (CONVERT(varchar(100), PayDate, 23) between '{0}' and '{1}')  group by CONVERT(varchar(100), JobTime, 23)) as job
left join (select sum(LocAmt) as PayAmt,CONVERT(varchar(100), DocDate, 23) as JobDate,MAX(AcCode) as AcCode from XAApPaymentDet where AcCode='{2}' group by CONVERT(varchar(100), DocDate, 23)) 
as pay_det on pay_det.JobDate=job.JobTime) as tab    ", fromdate, todate,acCode);
//throw new Exception(sql);
        DataTable tab=ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();

    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("HrVoucher", true);
    }
}