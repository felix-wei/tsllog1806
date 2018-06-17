<%@ Page Title="" Language="C#" MasterPageFile="~/Mis/Drilldown.master" %>

<script runat="server">

    void Page_Init(object o, EventArgs e)
    {
        mis_drilldown_master master = Page.Master as mis_drilldown_master;
        master.report_name = "Trip Analysis";
        master.drilldown_list = new string[] { };
        master.pivot_row = new string[] { "ContNo" };
        master.pivot_col = new string[] { "MONTH" };
        master.pivot_data = new string[] { "Qty" };
        master.pivot_filter = new string[] { "Type", "ContNo", "Size", "SchDate", "Driver", "TripCode", "PrimeMover", "YEAR", "Wt:0.000", "M3:0.000" };
        master.query_sql = @"select job.JobType as Type,det2.ContainerNo as ContNo,det1.ContainerType as Size,CONVERT(nvarchar(10),det1.ScheduleDate,111) as SchDate,det1.Weight as Wt,det1.Volume as M3,det1.QTY as Qty,det2.DriverCode as Driver,
det2.TripCode as TripCode,det2.TowheadCode as PrimeMover,job.Pol as Pol,job.Pod as Pod,CONVERT(nvarchar(4),det1.ScheduleDate,111) AS YEAR, CONVERT(nvarchar(7),det1.ScheduleDate,111) AS MONTH
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Statuscode<>'Cancel' and job.StatusCode<>'CNL' and (CONVERT(DATETIME, det1.ScheduleDate, 111) BETWEEN '{0}' AND '{1}')";
        master.filename_template = "TripAnalysis_{0:yyyy}_{1:MM}_{2:dd}.xls";
   }

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

