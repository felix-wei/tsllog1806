using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

public partial class ReportFreightSea_Chart_SeaChartRPT_ByAgt : System.Web.UI.Page
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
            WebChartControl1.SeriesDataMember = "Agt";
            WebChartControl1.SeriesTemplate.ArgumentDataMember = "Date";
            WebChartControl1.SeriesTemplate.ToolTipPointPattern = @"Date:{HINT}<br/>Value:{V}";
        }
        else
        {
            WebChartControl1.SeriesDataMember = "Date";
            WebChartControl1.SeriesTemplate.ArgumentDataMember = "Agt";
            WebChartControl1.SeriesTemplate.ToolTipPointPattern = @"Date:{HINT}<br/>Agent:{A}<br/>Value:{V}";
        }
        int sumYear = search_DateTo.Date.Year - search_DateFrom.Date.Year;
        int sumMonth = sumYear * 12 + (search_DateTo.Date.Month - search_DateFrom.Date.Month + 1);
        string sql_where_Agt = search_AgtId.Text.Trim().Length > 0 ? " and AgentId='" + search_AgtId.Text.Trim() + "'" : "";
        string sql_select_Agt = search_XAxis.Text.Equals("Date") ? "''" : "AgentId";
        string sql_group_Agt = search_XAxis.Text.Equals("Date") ? "" : ",AgentId";
        string sql = "";
        string sql_Agt_Date = "";
        if (search_RefType.Text == "Import")
        {
            if (search_DateType.Text.Equals("Month"))
            {
                sql = string.Format(@"select cast(SUM((case when Weight/1000-Volume>0 then Weight/1000 else Volume end)) as numeric(21,3)) as value,convert(char(7),ref.Eta,111)+'/01' as DateDay,convert(char(7),ref.Eta,111) as Date,{3} as Agt
from SeaImportRef as ref 
where ref.Eta between '{0}' and '{1}' {2}
group by convert(char(7),ref.Eta,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_Agt, sql_select_Agt, sql_group_Agt);
            }
            else
            {
                sql = string.Format(@"select cast(SUM((case when Weight/1000-Volume>0 then Weight/1000 else Volume end)) as numeric(21,3)) as value,convert(char(4),ref.Eta,111)+'/01/01' as DateDay,convert(char(4),ref.Eta,111) as Date,{3} as Agt
from SeaImportRef as ref 
where ref.Eta between '{0}' and '{1}' {2}
group by convert(char(4),ref.Eta,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_Agt, sql_select_Agt, sql_group_Agt);
            }
            if (!search_XAxis.Text.Equals("Date"))
            {
                sql_Agt_Date = string.Format(@"select distinct AgentId from SeaImportRef where Eta between '{0}' and '{1}'{2} and ISNULL(AgentId,'')<>''",search_DateFrom.Date,search_DateTo.Date,sql_where_Agt);
            }
        }
        else
        {
            if (search_RefType.Text == "Export")
            {
                if (search_DateType.Text.Equals("Month"))
                {
                    sql = string.Format(@"select cast(SUM((case when Weight/1000-Volume>0 then Weight/1000 else Volume end)) as numeric(21,3)) as value,convert(char(7),ref.Etd,111)+'/01' as DateDay,convert(char(7),ref.Etd,111) as Date,{3} as Agt
from SeaExportRef as ref 
where ref.Etd between '{0}' and '{1}' {2} and ref.refType like 'SE%'
group by convert(char(7),ref.Etd,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_Agt, sql_select_Agt, sql_group_Agt);
                }
                else
                {
                    sql = string.Format(@"select cast(SUM((case when Weight/1000-Volume>0 then Weight/1000 else Volume end)) as numeric(21,3)) as value,convert(char(4),ref.Etd,111)+'/01/01' as DateDay,convert(char(4),ref.Etd,111) as Date,{3} as Agt
from SeaExportRef as ref 
where ref.Etd between '{0}' and '{1}' {2} and ref.refType like 'SE%'
group by convert(char(4),ref.Etd,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_Agt, sql_select_Agt, sql_group_Agt);
                }
                if (!search_XAxis.Text.Equals("Date"))
                {
                    sql_Agt_Date = string.Format(@"select distinct AgentId from SeaExportRef where Etd between '{0}' and '{1}'{2} and ISNULL(AgentId,'')<>'' and refType like 'SE%'", search_DateFrom.Date, search_DateTo.Date, sql_where_Agt);
                }
            }
            else
            {
                if (search_DateType.Text.Equals("Month"))
                {
                    sql = string.Format(@"select cast(SUM((case when Weight/1000-Volume>0 then Weight/1000 else Volume end)) as numeric(21,3)) as value,convert(char(7),ref.Etd,111)+'/01' as DateDay,convert(char(7),ref.Etd,111) as Date,{3} as Agt
from SeaExportRef as ref 
where ref.Etd between '{0}' and '{1}' {2} and ref.refType like 'SC%'
group by convert(char(7),ref.Etd,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_Agt, sql_select_Agt, sql_group_Agt);
                }
                else
                {
                    sql = string.Format(@"select cast(SUM((case when Weight/1000-Volume>0 then Weight/1000 else Volume end)) as numeric(21,3)) as value,convert(char(4),ref.Etd,111)+'/01/01' as DateDay,convert(char(4),ref.Etd,111) as Date,{3} as Agt
from SeaExportRef as ref 
where ref.Etd between '{0}' and '{1}' {2} and ref.refType like 'SC%'
group by convert(char(4),ref.Etd,111){4}", search_DateFrom.Date, search_DateTo.Date, sql_where_Agt, sql_select_Agt, sql_group_Agt);
                }
                if (!search_XAxis.Text.Equals("Date"))
                {
                    sql_Agt_Date = string.Format(@"select distinct AgentId from SeaExportRef where Etd between '{0}' and '{1}'{2} and ISNULL(AgentId,'')<>'' and refType like 'SC%'", search_DateFrom.Date, search_DateTo.Date, sql_where_Agt);
                }
            }
        }
        if (search_DateType.Text.Equals("Month"))
        {
            if (search_XAxis.Text.Equals("Date"))
            {
                sql = @"select ISNULL(ref.value,0) as value,ISNULL(ref.Date,convert(char(7),showDate.Date,111)) as Date,isnull(party.Name,'') as Agt from (
select DATEADD(month,Tops-1,'" + search_DateFrom.Date.Year + "-" + search_DateFrom.Date.Month + @"-1') as Date
from (select top " + sumMonth + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as ShowDate 
left outer join (" + sql + @") as ref on datediff(day,ref.DateDay,ShowDate.Date)=0
left outer join XXParty as party on party.PartyId=ref.Agt
order by ISNULL(ref.Date,convert(char(7),showDate.Date,111))";
            }
            else
            {
                sql = @"select isnull(party.Name,'')+'('+Agt.AgentId+')' as Agt,CONVERT(char(7),Agt.Date,111) as Date,ISNULL(ref.value,0) as value from(
select * from("+sql_Agt_Date+@") as Agt
left outer join (select DATEADD(month,Tops-1,'" + search_DateFrom.Date.Year + "-" + search_DateFrom.Date.Month + @"-1') as Date
from (select top " + sumMonth + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as Tops on 1=1) as Agt
left outer join (" + sql + @") as ref on ref.DateDay=agt.Date and Agt.AgentId=ref.Agt
left outer join XXParty as party on party.PartyId=Agt.AgentId";
            }
        }
        else
        {
            if (search_XAxis.Text.Equals("Date"))
            {
                sql = @"select ISNULL(ref.value,0) as value,ISNULL(ref.Date,convert(char(4),showDate.Date,111)) as Date,isnull(party.Name,'') as Agt from (
select DATEADD(year,Tops-1,'" + search_DateFrom.Date.Year +  @"-1-1') as Date
from (select top " + (sumYear+1) + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as ShowDate 
left outer join (" + sql + @") as ref on datediff(year,ref.DateDay,ShowDate.Date)=0
left outer join XXParty as party on party.PartyId=ref.Agt
order by ISNULL(ref.Date,convert(char(4),showDate.Date,111))";
            }
            else
            {
                sql=@"select isnull(party.Name,'')+'('+Agt.AgentId+')' as Agt,CONVERT(char(4),Agt.Date,111) as Date,ISNULL(ref.value,0) as value from(
select * from("+sql_Agt_Date+@") as Agt
left outer join (select DATEADD(year,Tops-1,'" + search_DateFrom.Date.Year +  @"-1-1') as Date
from (select top " + (sumYear+1) + @" ROW_NUMBER() over(order by id) as Tops from sysobjects) as Tops) as Tops on 1=1) as Agt
left outer join (" + sql + @") as ref on ref.DateDay=agt.Date and Agt.AgentId=ref.Agt
left outer join XXParty as party on party.PartyId=Agt.AgentId";
            }
        }
        DataTable dt = ConnectSql.GetTab(sql);
        WebChartControl1.DataSource = dt;
        WebChartControl1.DataBind();
        //WebChartControl1.SaveToFile(Server.MapPath("~//123.dat"));
    }

    protected void btn_search1_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        //((IChartContainer)this.WebChartControl1 as IChartContainer).Chart.ExportToPdf(Server.MapPath("~\\test123.pdf"));

        MemoryStream ms = new MemoryStream();
        ((IChartContainer)this.WebChartControl1 as IChartContainer).Chart.ExportToXls(ms);
        ExportXls1(ms,"123");
        // ((IChartContainer)this.WebChartControl1 as IChartContainer).Chart.ExportToImage("c:\\test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        //ExportToPdf();
        //ExportToXls();
    }
    private void ExportXls1(MemoryStream ms,string fileName)
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
    private void ExportToPdf()
    {
        PrintingSystem ps = new PrintingSystem();

        PrintableComponentLink link1 = new PrintableComponentLink();
        //link1.Component = ASPxPivotGridExporter1;
        link1.PrintingSystem = ps;

        PrintableComponentLink link2 = new PrintableComponentLink();
        WebChartControl1.DataBind();
        link2.Component = ((IChartContainer)WebChartControl1).Chart;
        link2.PrintingSystem = ps;

        CompositeLink compositeLink = new CompositeLink();
        compositeLink.Links.AddRange(new object[] { link1, link2 });
        compositeLink.PrintingSystem = ps;

        compositeLink.CreateDocument();
        compositeLink.PrintingSystem.ExportOptions.Pdf.DocumentOptions.Author = "Test";
        using (MemoryStream stream = new MemoryStream())
        {
            compositeLink.PrintingSystem.ExportToPdf(stream);
            Response.Clear();
            Response.Buffer = false;
            Response.AppendHeader("Content-Type", "application/pdf");
            Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Response.AppendHeader("Content-Disposition", "attachment; filename=test.pdf");
            Response.BinaryWrite(stream.GetBuffer());
            Response.End();
        }

        ps.Dispose();
    }
    private void ExportToXls()
    {
        btn_search_Click(null, null);
        PrintingSystem ps = new PrintingSystem();

        PrintableComponentLink link1 = new PrintableComponentLink();
        //link1.Component = ASPxPivotGridExporter1;
        link1.PrintingSystem = ps;

        PrintableComponentLink link2 = new PrintableComponentLink();
        WebChartControl1.DataBind();
        link2.Component = ((IChartContainer)WebChartControl1).Chart;
        link2.PrintingSystem = ps;

        CompositeLink compositeLink = new CompositeLink();
        compositeLink.Links.AddRange(new object[] { link1, link2 });
        compositeLink.PrintingSystem = ps;

        compositeLink.CreateDocument();
        compositeLink.PrintingSystem.ExportOptions.Pdf.DocumentOptions.Author = "Test";
        using (MemoryStream stream = new MemoryStream())
        {
            compositeLink.PrintingSystem.ExportToPdf(stream);


            byte[] bt = stream.GetBuffer();
            if (bt.Length > 0)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.Buffer = true;
                Response.ContentType = "application/xls";
                Response.AddHeader("Content-Length", bt.Length.ToString());
                Response.AddHeader("Content-Disposition", "inline; filename=123.xls");
                Response.BinaryWrite(bt);
            }
        }

        ps.Dispose();
    }
}