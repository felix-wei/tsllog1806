using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportFreightSea_Chart_SeaChartRPT_ByCustom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        dt_Year.Columns.Add("Year", typeof(Int32));
        int YearNow = DateTime.Now.Year;
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
        if (search_DateType.Text == "Year")
        {
            search_DateFrom_Month.Value = 1;
            search_DateTo_Month.Value = 12;
        }
        DateTime search_DateFrom = new DateTime(SafeValue.SafeInt(search_DateFrom_Year.Value, DateTime.Now.Year), SafeValue.SafeInt(search_DateFrom_Month.Value, DateTime.Now.Month), 1);
        DateTime search_DateTo = new DateTime(SafeValue.SafeInt(search_DateTo_Year.Value, DateTime.Now.Year), SafeValue.SafeInt(search_DateTo_Month.Value, DateTime.Now.Month), 1).AddMonths(1).AddDays(-1);

        if (search_XAxis.Text.Equals("Date"))
        {
            WebChartControl1.SeriesDataMember = "Custom";
            WebChartControl1.SeriesTemplate.ArgumentDataMember = "Date";
            WebChartControl1.SeriesTemplate.ToolTipPointPattern = @"Date:{HINT}<br/>Value:{V}";
        }
        else
        {
            WebChartControl1.SeriesDataMember = "Date";
            WebChartControl1.SeriesTemplate.ArgumentDataMember = "Custom";
            WebChartControl1.SeriesTemplate.ToolTipPointPattern = @"Date:{HINT}<br/>Custom:{A}<br/>Value:{V}";
        }
        int sumYear = search_DateTo.Date.Year - search_DateFrom.Date.Year;
        int sumMonth = sumYear * 12 + (search_DateTo.Date.Month - search_DateFrom.Date.Month + 1);
        string sql_where_cust = search_CustomerId.Text.Trim().Length > 0 ? " and CustomerId='" + search_CustomerId.Text + "'" : "";
        string sql_select_cust = search_XAxis.Text.Equals("Date") ? "''" : "CustomerId";
        string sql_group_cust = search_XAxis.Text.Equals("Date") ? "" : ",CustomerId";
        string sql = "";
        string sql_Cust_Date = "";
        if (search_RefType.Text == "Import")
        {
            if (search_DateType.Text.Equals("Month"))
            {
                sql = string.Format(@"select cast(SUM(case when job.Weight/1000-job.Volume>0 then job.Weight/1000 else job.Volume end) as numeric(21,3)) as value,CONVERT(char(7),ref.Eta,111)+'/01' as Date ,{3} as Custom
from SeaImport as job 
left outer join SeaImportRef as ref on job.RefNo=ref.RefNo
where ref.Eta between '{0}' and '{1}'{2}
group by CONVERT(char(7),ref.Eta,111){4}",search_DateFrom.Date,search_DateTo.Date,sql_where_cust,sql_select_cust,sql_group_cust);
            }
            else
            {
                sql = string.Format(@"select cast(SUM(case when job.Weight/1000-job.Volume>0 then job.Weight/1000 else job.Volume end) as numeric(21,3)) as value,CONVERT(char(4),ref.Eta,111)+'/01/01' as Date ,{3} as Custom
from SeaImport as job 
left outer join SeaImportRef as ref on job.RefNo=ref.RefNo
where ref.Eta between '{0}' and '{1}'{2}
group by CONVERT(char(4),ref.Eta,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_cust, sql_select_cust, sql_group_cust);
            }
            if (!search_XAxis.Text.Equals("Date"))
            {
                sql_Cust_Date = string.Format(@"select distinct job.CustomerId as Custom
from SeaImport as job 
left outer join SeaImportRef as ref on job.RefNo=ref.RefNo 
where Eta between '{0}' and '{1}'{2} and ISNULL(CustomerId,'')<>''", search_DateFrom.Date, search_DateTo.Date, sql_where_cust);
            }
        }
        else
        {
            if (search_RefType.Text == "Export")
            {
                if (search_DateType.Text.Equals("Month"))
                {
                    sql = string.Format(@"select cast(SUM(case when job.Weight/1000-job.Volume>0 then job.Weight/1000 else job.Volume end) as numeric(21,3)) as value,CONVERT(char(7),ref.Etd,111)+'/01' as Date ,{3} as Custom
from SeaExport as job 
left outer join SeaExportRef as ref on job.RefNo=ref.RefNo 
where ref.Etd between '{0}' and '{1}' {2} and ref.refType like 'SE%'
group by convert(char(7),ref.Etd,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_cust, sql_select_cust, sql_group_cust);
                }
                else
                {
                    sql = string.Format(@"select cast(SUM(case when job.Weight/1000-job.Volume>0 then job.Weight/1000 else job.Volume end) as numeric(21,3)) as value,CONVERT(char(4),ref.Etd,111)+'/01/01' as Date ,{3} as Custom
from SeaExport as job 
left outer join SeaExportRef as ref on job.RefNo=ref.RefNo 
where ref.Etd between '{0}' and '{1}' {2} and ref.refType like 'SE%'
group by convert(char(4),ref.Etd,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_cust, sql_select_cust, sql_group_cust);
                }
                if (!search_XAxis.Text.Equals("Date"))
                {
                    sql_Cust_Date = string.Format(@"select distinct job.CustomerId as Custom 
from SeaExport as job 
left outer join SeaExportRef as ref on job.RefNo=ref.RefNo 
where Etd between '{0}' and '{1}'{2} and ISNULL(job.CustomerId,'')<>'' and ref.refType like 'SE%'", search_DateFrom.Date, search_DateTo.Date, sql_where_cust);
                }
            }
            else
            {
                if (search_DateType.Text.Equals("Month"))
                {
                    sql = string.Format(@"select cast(SUM(case when job.Weight/1000-job.Volume>0 then job.Weight/1000 else job.Volume end) as numeric(21,3)) as value,CONVERT(char(7),ref.Etd,111)+'/01' as Date ,{3} as Custom
from SeaExport as job 
left outer join SeaExportRef as ref on job.RefNo=ref.RefNo 
where ref.Etd between '{0}' and '{1}' {2} and ref.refType like 'SC%'
group by convert(char(7),ref.Etd,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_cust, sql_select_cust, sql_group_cust);
                }
                else
                {
                    sql = string.Format(@"select cast(SUM(case when job.Weight/1000-job.Volume>0 then job.Weight/1000 else job.Volume end) as numeric(21,3)) as value,CONVERT(char(4),ref.Etd,111)+'/01/01' as Date ,{3} as Custom
from SeaExport as job 
left outer join SeaExportRef as ref on job.RefNo=ref.RefNo 
where ref.Etd between '{0}' and '{1}' {2} and ref.refType like 'SC%'
group by convert(char(4),ref.Etd,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_cust, sql_select_cust, sql_group_cust);
                }
                if (!search_XAxis.Text.Equals("Date"))
                {
                    sql_Cust_Date = string.Format(@"select distinct job.CustomerId as Custom 
from SeaExport as job 
left outer join SeaExportRef as ref on job.RefNo=ref.RefNo 
where Etd between '{0}' and '{1}'{2} and ISNULL(job.CustomerId,'')<>'' and ref.refType like 'SC%'", search_DateFrom.Date, search_DateTo.Date, sql_where_cust);
                }
            }
        }

        if (search_DateType.Text.Equals("Month"))
        {
            if (search_XAxis.Text.Equals("Date"))
            {
                sql = @"select ISNULL(job.value,0) as value,ISNULL(convert(char(7),job.Date,111),convert(char(7),showDate.Date,111)) as Date,isnull(party.Name,'') as Custom from (
select DATEADD(month,Tops-1,'" + search_DateFrom.Date.Year + "-" + search_DateFrom.Date.Month + @"-1') as Date
from (select top " + sumMonth + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as ShowDate 
left outer join (" + sql + @") as job on datediff(day,job.Date,ShowDate.Date)=0
left outer join XXParty as party on party.PartyId=job.Custom
order by ISNULL(convert(char(7),job.Date,111),convert(char(7),showDate.Date,111))";
            }
            else
            {
                sql = @"select isnull(party.Name,'')+'('+Cust.Custom+')' as Custom,CONVERT(char(7),Cust.Date,111) as Date,ISNULL(job.value,0) as value from(
select * from(" + sql_Cust_Date + @") as Cust
left outer join (select DATEADD(month,Tops-1,'" + search_DateFrom.Date.Year + "-" + search_DateFrom.Date.Month + @"-1') as Date
from (select top " + sumMonth + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as Tops on 1=1) as Cust
left outer join (" + sql + @") as job on job.Date=Cust.Date and job.Custom=Cust.Custom
left outer join XXParty as party on party.PartyId=Cust.Custom";
            }
        }
        else
        {
            if (search_XAxis.Text.Equals("Date"))
            {
                sql = @"select ISNULL(job.value,0) as value,ISNULL(convert(char(4),job.Date,111),convert(char(4),showDate.Date,111)) as Date,isnull(party.Name,'') as Custom from (
select DATEADD(year,Tops-1,'" + search_DateFrom.Date.Year +  @"-1-1') as Date
from (select top " + (sumYear+1) + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as ShowDate 
left outer join (" + sql + @") as job on datediff(day,job.Date,ShowDate.Date)=0
left outer join XXParty as party on party.PartyId=job.Custom
order by ISNULL(convert(char(4),job.Date,111),convert(char(4),showDate.Date,111))";
            }
            else
            {
                sql = @"select isnull(party.Name,'')+'('+Cust.Custom+')' as Custom,CONVERT(char(4),Cust.Date,111) as Date,ISNULL(job.value,0) as value from(
select * from(" + sql_Cust_Date + @") as Cust
left outer join (select DATEADD(year,Tops-1,'" + search_DateFrom.Date.Year + "-" + search_DateFrom.Date.Month + @"-1') as Date
from (select top " + (sumYear + 1) + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as Tops on 1=1) as Cust
left outer join (" + sql + @") as job on job.Date=Cust.Date and job.Custom=Cust.Custom
left outer join XXParty as party on party.PartyId=Cust.Custom";
            }
        }


        DataTable dt = ConnectSql.GetTab(sql);
        WebChartControl1.DataSource = dt;
        WebChartControl1.DataBind();
    }
}