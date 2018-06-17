<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Log_LoadPages.aspx.cs" Inherits="PagesLog_Log_LoadPages" %>

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
            <table>
                <tr>
                    <td>User:</td>
                    <td>
                        <dxe:ASPxTextBox ID="search_user" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>URL:</td>
                    <td>
                        <dxe:ASPxTextBox ID="search_url" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>Date&nbsp;From:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_datefrom" runat="server" Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_dateto" runat="server" Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>&nbsp;&nbsp;
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <dxwgv:ASPxGridView ID="gv" ClientInstanceName="gv" runat="server" Styles-Cell-Wrap="Default" Width="1100px" AutoGenerateColumns="false" OnBeforePerformDataSelect="gv_BeforePerformDataSelect" OnHtmlRowPrepared="gv_HtmlRowPrepared" >
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="Id" FieldName="Id" CellStyle-HorizontalAlign="Center"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="IP" FieldName="IP"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Url" FieldName="Url" Width="200" >
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Method" FieldName="Method"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Agent" FieldName="Agent" Width="300"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Auth Location" FieldName="Auth"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="User" FieldName="User"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Date" FieldName="Date" Width="110"></dxwgv:GridViewDataColumn>
                </Columns>

            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
