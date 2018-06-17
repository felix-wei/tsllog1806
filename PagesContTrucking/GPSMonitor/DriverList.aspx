<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverList.aspx.cs" Inherits="PagesContTrucking_GPSMonitor_DriverList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .time{
            margin-left:-2px;
        }
    </style>
    <script type="text/javascript">
        function reloadMap() {
            var temp = document.getElementById("ifr");
			var drv = search_driver.GetText();
			if(drv=="NOORDIN")
				drv = "MERVIN";
            //temp.src = "ShowMap.aspx?DriverCode=" + search_driver.GetText() + "&Date1=" + search_date.GetText() + "&Time1=" + search_Time1.GetText() + "&Date2=" + search_date2.GetText() + "&Time2=" + search_Time2.GetText();
            temp.src = "ShowMap.aspx?DriverCode=" + drv + "&Date1=" + search_date.GetText() + "&Time1=" + search_Time1.GetText() + "&Time2=" + search_Time2.GetText();
			alert(temp.src);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 300px; vertical-align: top">
                        <table>
                            <tr>
                                <td>Driver</td>
                                <td>
                                    <dxe:ASPxComboBox ID="search_driver" ClientInstanceName="search_driver" runat="server" Width="150" ValueField="Code" TextField="Code"></dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Date</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="search_date" ClientInstanceName="search_date" runat="server" Width="150" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>From</td>
                                <td>
                                    <dxe:ASPxTextBox ID="search_Time1" ClientInstanceName="search_Time1" runat="server" Width="60" CssClass="time">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <%--<tr>
                                <td></td>
                                <td>
                                    <dxe:ASPxDateEdit ID="search_date2" ClientInstanceName="search_date2" runat="server" Width="150" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>To</td>
                                <td>
                                    <dxe:ASPxTextBox ID="search_Time2" ClientInstanceName="search_Time2" runat="server" Width="60" CssClass="time" >
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Search" runat="server" Text="Retrieve" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){reloadMap();}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100%">
                        <iframe id="ifr" width="100%" height="600px"></iframe>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
