using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Report_TripReport_Month : System.Web.UI.Page
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
        if (search_DateFrom.Date < new DateTime(1900, 1, 1))
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
        }
        if (search_DateTo.Date < new DateTime(1900, 1, 1))
        {
            search_DateTo.Date = DateTime.Now.AddDays(8);
        }
        //        string sql = string.Format(@"select temp.*,driver.TowheaderCode as TowheadCode,Incentive1+Incentive2+Incentive3+Incentive4 as Total,'{0}'+' - '+'{1}' as Date 
        //from (
        //select DriverCode,sum(isnull(det2.Incentive1,0)) as Incentive1,sum(isnull(det2.Incentive2,0)) as Incentive2,
        //sum(isnull(det2.Incentive3,0)) as Incentive3,sum(isnull(det2.Incentive4,0)) as Incentive4
        //from ctm_jobdet2 as det2 
        //", search_DateFrom.Date.ToString("dd/MM/yyyy"), search_DateTo.Date.ToString("dd/MM/yyyy"));
        //        string sql_part1 = string.Format(@" group by DriverCode
        //) as temp
        //left outer join CTM_Driver as driver on temp.DriverCode=driver.Code ");

        //        string where = string.Format(@" where det2.Statuscode='C' and DATEDIFF(d,det2.ToDate,'{0}')<=0 and DATEDIFF(d,det2.ToDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date);
        //        if (search_Driver.Text.Trim().Length > 0)
        //        {
        //            where += string.Format(@" and det2.DriverCode='{0}'", search_Driver.Text);
        //        }
        string sql_where = "";
        if (search_Driver.Text.Trim().Length > 0)
        {
            sql_where += string.Format(@" and det2.DriverCode=@DriverCode");
        }
        string sql = string.Format(@"select *,SalaryAllowance+Deduction as Allowance,Incentive1+Incentive2+Incentive3+Incentive4+SalaryBasic+SalaryAllowance+Deduction+Addition as Total  from (
select temp.*,driver.TowheaderCode as TowheadCode,@Date1+' - '+@Date2 as Date,
isnull(SalaryBasic,0) as SalaryBasic,isnull(SalaryAllowance,0) as SalaryAllowance,
isnull((select sum(isnull(Qty*Price,0)) from job_cost 
where DATEDIFF(d,EventDate,@DateFrom)<=0 and DATEDIFF(d,EventDate,@DateTo)>=0 and (EventType='Allowance' or EventType='Deduction') and DriverCode=temp.DriverCode ),0) as Deduction,
isnull((select sum(isnull(Qty*Price,0)) from job_cost 
where DATEDIFF(d,EventDate,@DateFrom)<=0 and DATEDIFF(d,EventDate,@DateTo)>=0 and (EventType='Addition') and DriverCode=temp.DriverCode ),0) as Addition     
from (
select DriverCode,sum(isnull(Incentive1,0)) as Incentive1,sum(isnull(Incentive2,0)) as Incentive2,
sum(isnull(Incentive3,0)) as Incentive3,sum(isnull(Incentive4,0)) as Incentive4
from (
select DriverCode,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as Incentive1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as Incentive2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Standby') as Incentive3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='PSA') as Incentive4
from ctm_jobdet2 as det2 
where det2.Statuscode='C' and DATEDIFF(d,det2.ToDate,@DateFrom)<=0 and DATEDIFF(d,det2.ToDate,@DateTo)>=0 
) as temp
group by DriverCode
) as temp
left outer join CTM_Driver as driver on temp.DriverCode=driver.Code
) as temp", sql_where);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", search_DateFrom.Date.ToString("yyyy-MM-dd"), SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", search_DateTo.Date.ToString("yyyy-MM-dd"), SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@Date1", search_DateFrom.Date.ToString("dd/MM/yyyy"), SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Date2", search_DateTo.Date.ToString("dd/MM/yyyy"), SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", search_Driver.Text, SqlDbType.NVarChar, 100));
        grid_Transport.DataSource = ConnectSql_mb.GetDataTable(sql,list);
        grid_Transport.DataBind();
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("TripReport_Monthly" + (search_Driver.Text.Length > 0 ? "_" + search_Driver.Text : ""), true);
    }
}