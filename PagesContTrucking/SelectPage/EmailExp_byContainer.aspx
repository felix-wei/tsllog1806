<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailExp_byContainer.aspx.cs" Inherits="PagesContTrucking_SelectPage_EmailExp_byContainer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="display:none">
                <dxe:ASPxTextBox ID="txt_ContainerNo" runat="server" ></dxe:ASPxTextBox>
                <dxe:ASPxTextBox ID="txt_JobNo" runat="server" ></dxe:ASPxTextBox>
            </div>
            <table>
                <tr>
                    <td></td>
                    <td style="text-align:right">
                        <dxe:ASPxButton ID="btn_Send" runat="server" Text="Send Email" OnClick="btn_Send_Click" ></dxe:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td>Name:</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_name" runat="server" Width="450" ></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Email Address:</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_address" runat="server" Width="450"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Title:</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_title" runat="server" Width="450"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Context:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_context" runat="server" Rows="6" Width="450" ></dxe:ASPxMemo>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
