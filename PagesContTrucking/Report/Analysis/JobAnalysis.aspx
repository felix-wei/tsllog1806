<%@ Page Title="" Language="C#" MasterPageFile="~/Mis/Drilldown.master" %>

<script runat="server">

    void Page_Init(object o, EventArgs e)
    {
        mis_drilldown_master master = Page.Master as mis_drilldown_master;
        master.report_name = "Trip Analysis";
        master.drilldown_list = new string[] {  };
        master.pivot_row = new string[] { "JobNo" };
        master.pivot_col = new string[] { "MONTH" };
        master.pivot_data = new string[] { "Qty" };
        master.pivot_filter = new string[] {"Type","Client", "Eta", "Etd", "Pol", "Pod", "YEAR",  "Wt:0.000", "M3:0.000","KPI","TripCode" };
        master.query_sql = @"select job.JobType as Type,job.JobNo as JobNo,party.Name as Client,job.Pol as Pol,job.Pod as Pod,CONVERT(nvarchar(10),job.EtaDate,111) as Eta,CONVERT(nvarchar(10),job.EtdDate,111) as Etd,det1.Weight as Wt,det1.Volume as M3,det1.QTY as Qty,
CONVERT(nvarchar(4),job.EtaDate,111) AS YEAR, CONVERT(nvarchar(7),job.EtaDate,111) AS MONTH,det2.DriverCode as KPI,det2.TripCode as TripCode
from CTM_Job as job
left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo
left outer join CTM_JobDet2 as det2 on job.jobNo=det2.JobNo
left outer join XXParty as party on job.ClientId=party.PartyId
where job.StatusCode<>'CNL' and (CONVERT(DATETIME, job.EtaDate, 111) BETWEEN '{0}' AND '{1}')";
        master.filename_template = "JobAnalysis_{0:yyyy}_{1:MM}_{2:dd}.xls";
   }

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

