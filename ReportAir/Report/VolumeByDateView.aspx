<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VolumeByDateView.aspx.cs" Inherits="ReportAir_Report_VolumeByDateView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
            <script type="text/javascript">
                function PrintReport(url) {
                    window.frames['viewFrame'].location = url + '&' + Date();
                }
    </script>
</head>
<frameset runat="server" id="rptFrameset" cols="250,*" border="0" framespacing="2" bordercolor="#9B98B6">
	<frame name="schFrame" runat="server" id="schFrame" src="VolumeByDate.aspx" />
	<frame name="viewFrame" src="/ReportAir/ReportViewer.pdf" />
</frameset>
</html>
