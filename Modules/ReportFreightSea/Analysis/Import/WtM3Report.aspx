<%@ Page Title="" Language="C#" MasterPageFile="~/Mis/Drilldown.master" %>

<script runat="server">

    void Page_Init(object o, EventArgs e)
    {
        mis_drilldown_master master = Page.Master as mis_drilldown_master;
        
     
      master.report_name = "Import Shipment Performance CBM Analysis";
      master.drilldown_list = new string[] { "AGENT","CUST","JOBTYPE", "ETA", "PORT", "STAFF", "TEU" };
    master.pivot_row = new string[] { "AGENT" };
    master.pivot_col = new string[] { "MONTH" };
    master.pivot_data = new string[] { "WEIGHT:0.000","VOLUME:0.000" };
    master.pivot_filter = new string[] { "JOBTYPE", "CUST", "PORT", "YEAR", "ETA", "STAFF" };
    master.query_sql = @"SELECT     m.Pol AS PORT, p.Code AS AGENT, m.jobtype AS JOBTYPE, CONVERT(nvarchar(4), 
                      m.Eta, 111) AS YEAR, CONVERT(nvarchar(7), m.Eta, 111) AS MONTH, CONVERT(nvarchar(10), m.Eta, 111) AS ETA, cust.Code AS CUST, c.Weight AS WEIGHT, 
                      c.Volume AS VOLUME
FROM SeaImport AS c INNER JOIN
                      SeaImportRef AS m ON c.RefNo = m.RefNo  LEFT OUTER JOIN
                      XXParty AS p ON m.AgentId = p.PartyId LEFT OUTER JOIN
                      XXParty AS cust ON c.CustomerId = cust.PartyId
WHERE     (CONVERT(DATETIME, m.Eta, 111) BETWEEN '{0}' AND '{1}')";
    master.filename_template = "Import_CBM_{0:yyyy}_{1:MM}_{2:dd}.xls";

    
    }

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

