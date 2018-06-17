<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryMessage.aspx.cs" Inherits="PagesContTrucking_GPSMonitor_HistoryMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmDriverMessage" KeyMember="Id" FilterExpression="1=0" />
            <table >
                <tr>
                    <td>Send Date From:</td>
                    <td><dxe:ASPxDateEdit ID="search_DateFrom" runat="server" EditFormatString="dd/MM/yyyy" Width="120"></dxe:ASPxDateEdit></td>
                    <td>To:</td>
                    <td><dxe:ASPxDateEdit ID="search_DateTo" runat="server" EditFormatString="dd/MM/yyyy" Width="120"></dxe:ASPxDateEdit></td>
                    <td><dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton></td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="false" DataSourceID="dsTransport" KeyFieldName="Id" Width="650">
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="SendTo" Caption="SendTo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="SendDate" Caption="Send Date" PropertiesDateEdit-EditFormatString="dd/MM/yyyy" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="Content" Caption="Content" MinWidth="200"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="StatusCode" Caption="Status"></dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
