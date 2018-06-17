<%@ Page Title="" Language="C#" MasterPageFile="~/Mis/Drilldown.master" %>

<script runat="server">

    void Page_Init(object o, EventArgs e)
    {
        mis_drilldown_master master = Page.Master as mis_drilldown_master;
        master.report_name = "Export Shipment Performance Teus Analysis";
        master.drilldown_list = new string[] { "REF", "CONT", "CONTTYPE", "JOBTYPE", "ETA", "PORT", "STAFF", "TEU" };
        master.pivot_row = new string[] { "AGENT" };
        master.pivot_col = new string[] { "MONTH" };
        master.pivot_data = new string[] { "TEU:0" };
        master.pivot_filter = new string[] { "JOBTYPE", "CARRIER", "PORT", "YEAR", "ETA", "STAFF" };
        master.query_sql = @"SELECT m.RefNo AS REF, UPPER(m.CreateBy) AS STAFF, v.Code AS CARRIER, m.Pod AS PORT, p.Code AS AGENT, 
                      m.jobtype AS JOBTYPE, c.ContainerNo AS CONT, c.ContainerType AS CONTTYPE, 
                      CASE substring(c.ContainerType, 1, 1) WHEN '2' THEN 1 WHEN '4' THEN 2 ELSE 0 END AS TEU, CONVERT(nvarchar(4), m.Eta, 111) AS YEAR, CONVERT(nvarchar(7), 
                      m.Eta, 111) AS MONTH, CONVERT(nvarchar(10), m.Eta, 111) AS ETA
FROM         SeaExportMkg AS c INNER JOIN
                      SeaExportRef AS m ON c.RefNo = m.RefNo  LEFT OUTER JOIN
                      XXParty AS p ON m.AgentId = p.PartyId LEFT OUTER JOIN
                      XXParty AS v ON m.CrAgentId = v.PartyId
WHERE m.statuscode<>'CNL' and c.MkgType='Cont' and (CONVERT(DATETIME, m.Eta, 111) BETWEEN '{0}' AND '{1}')";
        master.filename_template = "Export_Teus_{0:yyyy}_{1:MM}_{2:dd}.xls";
   }

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

