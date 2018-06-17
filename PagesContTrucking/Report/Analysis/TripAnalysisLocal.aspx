<%@ Page Title="" Language="C#" MasterPageFile="~/Mis/Drilldown.master" %>

<script runat="server">

    void Page_Init(object o, EventArgs e)
    {
        mis_drilldown_master master = Page.Master as mis_drilldown_master;
        master.report_name = "Trip Analysis Local";
        master.drilldown_list = new string[] { };
        master.pivot_row = new string[] { "Driver" };
        master.pivot_col = new string[] { "MONTH" };
        master.pivot_data = new string[] { "Qty" };
        master.pivot_filter = new string[] { "Type", "SchDate", "TripCode", "YEAR", "Wt:0.000", "M3:0.000" };
        master.query_sql = @"select JobType as Type,CONVERT(nvarchar(10),job.BkgDate,111) as SchDate,job.Wt,job.M3,job.Qty,job.Driver,job.TripCode,job.VehicleNo,job.Pol,job.Pod,CONVERT(nvarchar(4),job.BkgDate,111) AS YEAR, CONVERT(nvarchar(7),job.BkgDate,111) AS MONTH
from TPT_Job as job
where job.StatusCode<>'CNL' and (CONVERT(DATETIME, job.BkgDate, 111) BETWEEN '{0}' AND '{1}')";
        master.filename_template = "TripAnalysis_Local_{0:yyyy}_{1:MM}_{2:dd}.xls";
   }

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

