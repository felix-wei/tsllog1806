<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportSalesProfitView.aspx.cs" Inherits="ReportFreigtSea_ExportSalesProfitView" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Profit/Loss </title>
    <script type="text/javascript">
        function PrintReport(url) {
           window.frames['viewFrame'].location=url+'&'+Date();
        }
    </script>
</head>
<frameset runat="server" id="rptFrameset" cols="250,*" border="0" framespacing="2" bordercolor="#9B98B6">
	<frame name="schFrame" runat="server" id="schFrame" src="ExportSalesProfit.aspx" />
	<frame name="viewFrame" src="/ReportFreightSea/ReportViewer.pdf" />
</frameset>
</html>
