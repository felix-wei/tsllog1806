using DevExpress.XtraCharts.Native;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportFreightSea_Chart_BillingChartRPT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //search_DateFrom.Date = DateTime.Now.AddYears(-1);
            //search_DateTo.Date = DateTime.Now;
            Date_DataBind();
        }
        if (search_DateType.Text.Equals("Month"))
        {
            search_DateFrom_Month.ClientVisible = true;
            search_DateTo_Month.ClientVisible = true;
        }
        else
        {
            search_DateFrom_Month.ClientVisible = false;
            search_DateTo_Month.ClientVisible = false;
        }
    }
    private void Date_DataBind()
    {
        //=========search date Year data bind
        DataTable dt_Year = new DataTable();
        dt_Year.Columns.Add("Year",typeof(Int32));
        int YearNow=DateTime.Now.Year;
        for (; YearNow >= 2010; YearNow--)
        {
            DataRow dr = dt_Year.NewRow();
            dr["Year"] = YearNow;
            dt_Year.Rows.Add(dr);
        }
        search_DateFrom_Year.DataSource = dt_Year;
        search_DateFrom_Year.DataBind();
        search_DateTo_Year.DataSource = dt_Year;
        search_DateTo_Year.DataBind();
        if (dt_Year.Rows.Count > 0)
        {
            search_DateFrom_Year.Value = DateTime.Now.Year - 1;
            search_DateTo_Year.Value = DateTime.Now.Year;
        }

        //===========search date Month data bind
        DataTable dt_Month = new DataTable();
        dt_Month.Columns.Add("Month", typeof(Int32));
        for (int i = 1; i <= 12; i++)
        {
            DataRow dr = dt_Month.NewRow();
            dr["Month"] = i;
            dt_Month.Rows.Add(dr);
        }
        search_DateFrom_Month.DataSource = dt_Month;
        search_DateFrom_Month.DataBind();
        search_DateTo_Month.DataSource = dt_Month;
        search_DateTo_Month.DataBind();
        if (dt_Month.Rows.Count > 0)
        {
            search_DateFrom_Month.Value = DateTime.Now.Month;
            search_DateTo_Month.Value = DateTime.Now.Month;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string PartyTo = SafeValue.SafeString(search_PartyTo.Value, "");
        if (PartyTo == "")
        {
            return;
        }
        //if (search_DateFrom.Date < new DateTime(1900, 1, 1))
        //{
        //    search_DateFrom.Date = DateTime.Now.AddYears(-1);
        //}
        //if (search_DateTo.Date < search_DateFrom.Date)
        //{
        //    search_DateTo.Date = search_DateFrom.Date.AddYears(1);
        //}
        if (search_DateType.Text == "Year")
        {
            search_DateFrom_Month.Value = 1;
            search_DateTo_Month.Value = 12;
        }
        DateTime DateFrom = new DateTime(SafeValue.SafeInt(search_DateFrom_Year.Value, DateTime.Now.Year), SafeValue.SafeInt(search_DateFrom_Month.Value, DateTime.Now.Month), 1);
        DateTime DateTo = new DateTime(SafeValue.SafeInt(search_DateTo_Year.Value, DateTime.Now.Year), SafeValue.SafeInt(search_DateTo_Month.Value, DateTime.Now.Month), 1).AddMonths(1).AddDays(-1);
        int sumYear = DateTo.Date.Year - DateFrom.Year;
        int sumMonth = sumYear * 12 + (DateTo.Month - DateFrom.Month + 1);
        if (sumMonth <= 0) return;
        string sql = "";
        if (search_DateType.Text.Equals("Month"))
        {
            if (search_Type.Text.Equals("Ar"))
            {
                sql = string.Format(@"select SUM(case doctype when 'IV' then LocAmt when 'DN' then LocAmt when 'CN' then -LocAmt else 0 end) as Amt, CONVERT(char(7),docdate,111)+'/01' as Date 
from XAArInvoice
where PartyTo='{0}' and DocDate between '{1}' and '{2}'
group by CONVERT(char(7),docdate,111)", PartyTo, DateFrom, DateTo);
            }
            else
            {
                sql = string.Format(@"select SUM(case doctype when 'PL' then LocAmt when 'SD' then LocAmt when 'VO' then LocAmt when 'SC' then -LocAmt else 0 end) as Amt, CONVERT(char(7),docdate,111)+'/01' as Date 
from XAApPayable
where PartyTo='{0}' and DocDate between '{1}' and '{2}'
group by CONVERT(char(7),docdate,111)", PartyTo, DateFrom, DateTo);
            }
            sql = @"select ISNULL(temp.Amt,0) as Amt,convert(char(7),showDate.Date,111) as Date 
from (select DATEADD(month,Tops-1,'" + DateFrom.Year + "-" + DateFrom.Date.Month + @"-1') as Date
from (select top " + sumMonth + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as ShowDate 
left outer join (" + sql + @") as temp on datediff(day,temp.Date,ShowDate.Date)=0
order by convert(char(7),showDate.Date,111)";
        }
        else
        {
            if (search_Type.Text.Equals("Ar"))
            {
                sql = string.Format(@"select SUM(case doctype when 'IV' then LocAmt when 'DN' then LocAmt when 'CN' then -LocAmt else 0 end) as Amt, CONVERT(char(4),docdate,111)+'/01/01' as Date 
from XAArInvoice
where PartyTo='{0}' and DocDate between '{1}' and '{2}'
group by CONVERT(char(4),docdate,111)", PartyTo, DateFrom, DateTo);
            }
            else
            {
                sql = string.Format(@"select SUM(case doctype when 'PL' then LocAmt when 'SD' then LocAmt when 'VO' then LocAmt when 'SC' then -LocAmt else 0 end) as Amt, CONVERT(char(4),docdate,111)+'/01/01' as Date 
from XAApPayable
where PartyTo='{0}' and DocDate between '{1}' and '{2}'
group by CONVERT(char(4),docdate,111)", PartyTo, DateFrom, DateTo);
            }
            sql = @"select ISNULL(temp.Amt,0) as Amt,convert(char(4),showDate.Date,111) as Date 
from (select DATEADD(year,Tops-1,'" + DateFrom.Year + @"-1-1') as Date
from (select top " + (sumYear+1) + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as ShowDate 
left outer join (" + sql + @") as temp on datediff(day,temp.Date,ShowDate.Date)=0
order by convert(char(4),showDate.Date,111)";
        }


        DataTable dt = ConnectSql.GetTab(sql);
        WebChartControl1.DataSource = dt;
        WebChartControl1.DataBind();
        WebChartControl1.Titles.RemoveAt(0);
        DevExpress.XtraCharts.ChartTitle title=new DevExpress.XtraCharts.ChartTitle();
        title.Text ="["+ search_PartyTo.Text + "]  Billing Chart (" + search_Type.Text + ")";
        WebChartControl1.Titles.Add(title );
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        MemoryStream ms = new MemoryStream();
        ((IChartContainer)this.WebChartControl1 as IChartContainer).Chart.ExportToXls(ms);
        ExportXls1(ms, "BillingChart");
    }
    private void ExportXls1(MemoryStream ms, string fileName)
    {
        byte[] bt = ms.GetBuffer();
        if (bt.Length > 0)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "application/xls";
            Response.AddHeader("Content-Length", bt.Length.ToString());
            Response.AddHeader("Content-Disposition", "inline; filename=" + fileName + ".xls");
            Response.BinaryWrite(bt);
        }
    }
}