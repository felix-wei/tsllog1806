<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferHistory.aspx.cs" Inherits="WareHouse_Job_TransferHistory" %>

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
                    <td>Product:</td>
                    <td>
                        <dxe:ASPxTextBox ID="search_product" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>Lot&nbsp;No:</td>
                    <td>
                        <dxe:ASPxTextBox ID="search_lotno" runat="server" Width="120"></dxe:ASPxTextBox>
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
                    <td>&nbsp;&nbsp;
                        <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" OnClick="btn_search_Click">
                            <ClientSideEvents  Click="function(s,e){
                                gv.Refresh();  
                                }"/>
                        </dxe:ASPxButton>

                    </td>
                </tr>
            </table>
        </div>
        <div>
            <dxwgv:ASPxGridView ID="gv" ClientInstanceName="gv" runat="server" Width="100%" AutoGenerateColumns="false">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="Id" FieldName="Id" CellStyle-HorizontalAlign="Center"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="TransferNo" FieldName="TransferNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="LotNo" FieldName="LotNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Product" FieldName="Product"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Description" FieldName="Des"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="From Location" FieldName="Warehouse"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Qty" FieldName="Qty1"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="To Location" FieldName="Set"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="User" FieldName="User"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Date" FieldName="Date"></dxwgv:GridViewDataColumn>
                </Columns>

            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
