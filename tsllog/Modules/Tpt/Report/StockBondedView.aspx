<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockBondedView.aspx.cs" Inherits="Modules_Tpt_Report_StockBondedView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function PrintReport(url) {
            window.frames['viewFrame'].location = url + '&' + Date();
        }
    </script>
</head>
<frameset runat="server" id="rptFrameset" cols="250,*" border="0" framespacing="2" bordercolor="#9B98B6">
	<frame name="schFrame" runat="server" id="schFrame" src="StockBonded.aspx" />
	<frame name="viewFrame" src="/Modules/Tpt/Report/ReportViewer.pdf" />
</frameset>
</html>

