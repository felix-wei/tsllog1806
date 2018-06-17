<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fuel.aspx.cs" Inherits="PagesContTrucking_Vehicle_Fuel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefVehicleFuel" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>Date From</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_searchFrom" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_searchTo" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
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

            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="1300" DataSourceID="dsTransport" KeyFieldName="Id" OnInit="grid_Transport_Init" OnRowInserting="grid_Transport_RowInserting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" OnRowDeleting="grid_Transport_RowDeleting">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="100">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="True">
                        </DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="DriverCode" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="100%">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,null);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="VehicleCode" Caption="VehicleCode" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_VehicleCode" ClientInstanceName="btn_VehicleCode" runat="server" Text='<%# Bind("VehicleCode") %>' AutoPostBack="False" Width="100%">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_VehicleList(btn_VehicleCode,null);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="EventDate" Caption="Date" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="Description" Caption="Description" Width="150"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataSpinEditColumn FieldName="OpenLiter" Caption="OpenLiter" Width="70">
                        <PropertiesSpinEdit DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn FieldName="CloseLiter" Caption="CloseLiter" Width="70">
                        <PropertiesSpinEdit DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn FieldName="NewLiter" Caption="NewLiter" Width="70">
                        <PropertiesSpinEdit DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn FieldName="TotalPrice" Caption="TotalPrice" Width="70">
                        <PropertiesSpinEdit DisplayFormatString="0.00" Increment="0" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataColumn FieldName="DocNo" Caption="DocNo" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SupplierCode" Caption="SupplierCode" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_SupplierCode" ClientInstanceName="btn_SupplierCode" runat="server" Text='<%# Bind("SupplierCode") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_SupplierCode,null);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Width="150"></dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="true"  ShowFilterRow="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="DriverCode" ShowInColumn="DriverCode"
                        SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
