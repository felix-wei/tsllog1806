<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mileage.aspx.cs" Inherits="PagesContTrucking_DriverReporting_Mileage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.VehicleMileage" KeyMember="Id" FilterExpression="1=0" />
        <table>
            <tr>
                <td>Driver</td>
                <td>
                    <dxe:ASPxTextBox ID="search_driver" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>Vehicle</td>
                <td>
                    <dxe:ASPxTextBox ID="search_vehicle" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>Date From:</td>
                <td>
                    <dxe:ASPxDateEdit ID="search_from" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" Width="120"></dxe:ASPxDateEdit>
                </td>
                <td>To:</td>
                <td>
                    <dxe:ASPxDateEdit ID="search_to" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" Width="120"></dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                        <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" DataSourceID="dsTransport" KeyFieldName="Id" OnRowInserting="grid_Transport_RowInserting" OnRowDeleting="grid_Transport_RowDeleting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init">
            <SettingsPager PageSize="100" />
            <SettingsEditing Mode="Inline" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="100">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CreateBy" Caption="Driver" Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="VehicleNo" Caption="VehicleNo" Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn FieldName="ReportDate" Caption="ReportDate" Width="100" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" PropertiesDateEdit-EditFormatString="dd/MM/yyyy" >
                </dxwgv:GridViewDataDateColumn>
                <dx:GridViewDataSpinEditColumn FieldName="Value" Caption="Mileage" Width="100" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" PropertiesSpinEdit-Increment="0" PropertiesSpinEdit-DecimalPlaces="2" PropertiesSpinEdit-DisplayFormatString="0.00"></dx:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Note" Caption="Note" Width="200"></dxwgv:GridViewDataTextColumn>
            </Columns>

        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
