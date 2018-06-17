<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverMessages.aspx.cs" Inherits="PagesContTrucking_GPSMonitor_DriverMessages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
        function ShowSend() {
            alert("Had sent to '" + txt_sendTo.GetText() + "':\n" + txt_Content.GetText());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Send To</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_sendTo" runat="server" ClientInstanceName="txt_sendTo">
                            <MaskSettings Mask="<0..9><0..9><0..9><0..9> <0..9><0..9><0..9><0..9>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Content</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_Content" runat="server" ClientInstanceName="txt_Content" Width="300"></dxe:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btn_Send" runat="server" Text="Send" AutoPostBack="false" Visible="false">
                            <ClientSideEvents Click="function(s,e){ShowSend();}" />
                        </dxe:ASPxButton>
                        <dxe:ASPxButton ID="btn_Send1" runat="server" Text="Send" OnClick="btn_Send1_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
