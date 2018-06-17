<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelperAuthority.aspx.cs" Inherits="PagesMaster_Control_HelperAuthority" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsRole" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Role" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsAuthority" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HelperAuthority" KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table>
                <tr>
                    <td>Role:</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_role" runat="server" AutoPostBack="false" DataSourceID="dsRole" IncrementalFilteringMode="StartsWith" TextField="Code" ValueField="Code" Width="120">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){gv.AddNewRow();}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <dxwgv:ASPxGridView ID="gv" runat="server" ClientInstanceName="gv" AutoGenerateColumns="false" DataSourceID="dsAuthority" KeyFieldName="Id" OnInit="gv_Init" OnBeforePerformDataSelect="gv_BeforePerformDataSelect" OnInitNewRow="gv_InitNewRow" OnRowDeleting="gv_RowDeleting" OnRowInserting="gv_RowInserting" OnRowUpdating="gv_RowUpdating">
            <SettingsEditing Mode="Inline" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Columns>
                <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                    <DataItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="gv_btn_eidt" runat="server" AutoPostBack="false" Text="Eidt" Width="40" ClientSideEvents-Click='<%# "function(s) { gv.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="gv_btn_del" runat="server" AutoPostBack="false" Text="Delete" Width="40" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){gv.DeleteRow("+Container.VisibleIndex+") }}"  %>'></dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <table>
                            <tr>

                                <td>
                                    <dxe:ASPxButton ID="gv_btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                        ClientSideEvents-Click='<%# "function(s) { gv.UpdateEdit() }"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="gv_btn_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                        ClientSideEvents-Click='<%# "function(s) { gv.CancelEdit() }"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Frame" FieldName="Frame" Width="150">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Value="/Warehouse/Job/SoList" Text="Sale Order List" />
                            <dxe:ListEditItem Value="/Warehouse/Job/SoEdit" Text="Sale Order Edit" />
                            <dxe:ListEditItem Value="/PagesContTrucking/Job/JobList" Text="Transport Haulier List" />
                            <dxe:ListEditItem Value="/PagesContTrucking/Job/JobEdit" Text="Transport Haulier Edit" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="Control" FieldName="Control" Width="150"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Control Type" FieldName="ControlType" Width="150">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Value="ASPxButton" Text="Button" />
                            <dxe:ListEditItem Value="TabPage" Text="TabPage" />
                            <dxe:ListEditItem Value="Column" Text="GridView Column" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="Status" Width="100">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Text="" Value="" />
                            <dxe:ListEditItem Text="Draft" Value="Draft" />
                            <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                            <dxe:ListEditItem Text="Delivered" Value="Delivered" />
                            <dxe:ListEditItem Text="Closed" Value="Closed" />
                            <dxe:ListEditItem Text="Canceled" Value="Canceled" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Role" FieldName="Role" Width="100" PropertiesComboBox-DataSourceID="dsRole">
                    <PropertiesComboBox TextField="Code" ValueField="Code"></PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Style" FieldName="IsHid" Width="100">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Value="0" Text="Enable" />
                            <dxe:ListEditItem Value="1" Text="Hidden" />
                            <dxe:ListEditItem Value="2" Text="Unable" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" Width="200"></dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
