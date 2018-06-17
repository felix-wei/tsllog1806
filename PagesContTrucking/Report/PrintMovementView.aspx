<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintMovementView.aspx.cs" Inherits="PagesContTrucking_Report_PrintMovementView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales Profit/Loss </title>
    <script type="text/javascript">
        function PrintReport(url) {
            window.frames['viewFrame'].location = url + '&' + Date();
        }
    </script>
</head>
<frameset runat="server" id="rptFrameset" cols="250,*" border="0" framespacing="2" bordercolor="#9B98B6">
	<frame name="schFrame" runat="server" id="schFrame" src="PrintMovement.aspx" />
	<frame name="viewFrame" src="/PagesContTrucking/Report/ReportViewer.pdf" />
</frameset>
</html>
