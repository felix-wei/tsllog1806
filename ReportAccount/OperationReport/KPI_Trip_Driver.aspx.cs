using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportAccount_OperationReport_KPI_Trip_Driver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["driver"] != null)
            {
                string driver = Request.Params["driver"];
                string datefrom = Request.Params["from"];
                string dateto = Request.Params["to"];
                search_from.Text = datefrom;
                search_to.Text = dateto;
                search_driver.Text = driver;
                btn_search_Click(null, null);
            }
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
//        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.Statuscode,
//FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det1.SealNo,det1.TTTime,
//(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(16,2)) from job_cost where TripNo=det2.Id and LineType='DP') as Incentive,
//(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(16,2)) from job_cost where TripNo=det2.Id and LineType='CL') as Claim 
//From CTM_JobDet2 as det2
//left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
//where det2.Statuscode='C' and datediff(day,ToDate,@DateFrom)<=0 and datediff(day,ToDate,@DateTo)>=0 and DriverCode=@DriverCode
//order by ToDate");
        string sql = string.Format(@"select det2.Id,det2.JobNo,det2.ContainerNo,det2.Statuscode,
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det1.SealNo,det1.TTTime,
(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(16,2)) from job_cost as c where TripNo=det2.Id and LineType='DP' and c.DriverCode=det2.DriverCode) as Incentive,
(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(16,2)) from job_cost as c where TripNo=det2.Id and LineType='CL') as Claim 
From CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where det2.Statuscode='C' and datediff(day,FromDate,@DateFrom)<=0 and datediff(day,FromDate,@DateTo)>=0 and DriverCode=@DriverCode
union all 
select det2.Id,det2.JobNo,det2.ContainerNo,det2.Statuscode,
FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,TripCode as JobType,det1.SealNo,det1.TTTime,
(select cast(isnull(sum(isnull(Qty*Price,0)),0) as decimal(16,2)) from job_cost as c where TripNo=det2.Id and LineType='DP' and c.DriverCode=det2.DriverCode2) as Incentive,
0 as Claim 
From CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where det2.Statuscode='C' and datediff(day,FromDate,@DateFrom)<=0 and datediff(day,FromDate,@DateTo)>=0 and DriverCode2=@DriverCode
order by FromDate");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", search_driver.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", search_from.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", search_to.Text, SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        rp.DataSource = dt;
        rp.DataBind();
    }

    private int Trips = 0;
    private decimal Incentive = 0;
    private decimal Claim = 0;

    protected void rp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            Incentive += SafeValue.SafeDecimal(dr["Incentive"]);
            Claim += SafeValue.SafeDecimal(dr["Claim"]);
        }
        else
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Label lb_datefrom = (Label)e.Item.FindControl("lb_datefrom");
                Label lb_dateto = (Label)e.Item.FindControl("lb_dateto");
                Label lb_driver = (Label)e.Item.FindControl("lb_driver");
                lb_datefrom.Text = exchangeDateFormat(search_from.Text);
                //SafeValue.SafeDate(search_from.Text, new DateTime(1990, 1, 1)).ToString("dd/MM/yyyy");
                lb_dateto.Text = exchangeDateFormat(search_to.Text);
                //SafeValue.SafeDate(search_to.Text, new DateTime(1990, 1, 1)).ToString("dd/MM/yyyy");
                lb_driver.Text = search_driver.Text;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lb_Incentive = (Label)e.Item.FindControl("lb_Incentive");
                Label lb_Claim = (Label)e.Item.FindControl("lb_Claim");

                lb_Incentive.Text = Incentive.ToString();
                lb_Claim.Text = Claim.ToString();
            }
        }
    }
    private string exchangeDateFormat(string dd)
    {
        string res = "";
        if (dd.Length == 8)
        {
            res = dd.Substring(6, 2) + "/" + dd.Substring(4, 2) + "/" + dd.Substring(0, 4);
        }
        return res;
    }
}