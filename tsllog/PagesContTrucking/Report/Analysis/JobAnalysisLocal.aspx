<%@ Page Title="" Language="C#" MasterPageFile="~/Mis/Drilldown.master" %>

<script runat="server">

    void Page_Init(object o, EventArgs e)
    {
        mis_drilldown_master master = Page.Master as mis_drilldown_master;
        master.report_name = "Trip Analysis Local";
        master.drilldown_list = new string[] {  };
        master.pivot_row = new string[] { "JobNo" };
        master.pivot_col = new string[] { "MONTH" };
        master.pivot_data = new string[] { "Qty" };
        master.pivot_filter = new string[] { "Type", "Customer", "Eta", "Etd", "Pol", "Pod", "YEAR", "Wt:0.000", "M3:0.000", "KPI", "TripCode" };
        master.query_sql = @"select job.JobType as Type,job.JobNo,party.Code as Customer,job.Pol,job.Pod,CONVERT(nvarchar(10),job.Eta,111) as Eta,CONVERT(nvarchar(10),job.Etd,111) as Etd,job.Wt,job.M3,job.Qty,
CONVERT(nvarchar(4),job.BkgDate,111) AS YEAR, CONVERT(nvarchar(7),job.BkgDate,111) AS MONTH,job.Driver as KPI,job.TripCode as TripCode
from TPT_Job as job
left outer join XXParty as party on job.Cust=party.PartyId
where job.StatusCode<>'CNL' and (CONVERT(DATETIME, job.BkgDate, 111) BETWEEN '{0}' AND '{1}')";
        master.filename_template = "JobAnalysis_Local_{0:yyyy}_{1:MM}_{2:dd}.xls";
   }

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

