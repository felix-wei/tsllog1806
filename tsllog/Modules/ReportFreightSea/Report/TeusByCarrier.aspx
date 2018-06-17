<%@ Page Title="" Language="C#" MasterPageFile="~/Mis/Drilldown.master" %>

<script runat="server">

    void Page_Init(object o, EventArgs e)
    {
        mis_drilldown_master master = Page.Master as mis_drilldown_master;
        
     
      master.report_name = "Import Shipment Performance Analysis - By Carrier";
      master.drilldown_list = new string[] { "REF", "CONT", "CONTTYPE", "JOBTYPE", "ETA", "PORT", "STAFF", "TEU" };
    master.pivot_row = new string[] { "CARRIER" };
    master.pivot_col = new string[] { "MONTH" };
    master.pivot_data = new string[] { "TEU:0" };
    master.pivot_filter = new string[] { "JOBTYPE", "STAFF", "PORT", "YEAR", "ETA" };
    master.query_sql = @"SELECT     m.RefNo AS REF,UPPER(m.UserId) AS STAFF, v.Code AS CARRIER, m.Pol + '-' + p.Name AS PORT, 
                      CASE m.jobtype WHEN 'FCL' THEN 'FCL' WHEN 'LCL' THEN 'CONSOL' ELSE 'OTHERS' END AS JOBTYPE, c.ContainerNo AS CONT, c.ContainerType AS CONTTYPE, 
                      CASE substring(c.ContainerType, 1, 1) WHEN '2' THEN 1 WHEN '4' THEN 2 ELSE 0 END AS TEU, CONVERT(nvarchar(4), m.Eta, 111) AS YEAR, CONVERT(nvarchar(7), 
                      m.Eta, 111) AS MONTH, CONVERT(nvarchar(10), m.Eta, 111) AS ETA
FROM         SeaImportCont AS c INNER JOIN
                      SeaImportRef AS m ON c.RefNo = m.RefNo INNER JOIN
                      XXPort AS p ON m.Pol = p.Code INNER JOIN
                      XXParty AS v ON m.CrAgentId = v.PartyId
WHERE     (CONVERT(DATETIME, m.Eta, 111) BETWEEN '{0}' AND '{1}')";
    master.filename_template = "import_carrier_{0:yyyyMM}_{1:yyyyMM}_{2:yyyyMMddHHmmss}.xls";

    
    }

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

