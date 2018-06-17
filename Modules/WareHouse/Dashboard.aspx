<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Modules_WareHouse_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
        <script type="text/javascript">
            function ShowHouse(masterId, type) {
                if (type == "IN") {
                    parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoInEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
                }
                if (type == "OUT") {
                    parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
                }
            }
            var isRefresh = true;
            var timeTemp;

            window.onload = function () {
                timeTemp = setInterval(refreshGrid, 30000);
            }
            function refreshGrid() {
                grid.Refresh();
                grid1.Refresh();
                grid2.Refresh();

            }
    </script>
    <style type="text/css">
        a:hover {
            color: black;
        }

        .link a:link {
            color: red;
        }

        .link a:hover {
            color: red;
        }

        .none a:link {
        }


        .a_ltrip span {
            display: inline-block;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding: 2px;
            width: 33px;
            height: 21px;
            overflow: hidden;
            white-space: nowrap;
            text-align: center;
            margin: 2px;
            /*margin-top:2px;
            margin-left:2px;
            margin-bottom:2px;
            margin-right:4px;*/
        }

        .a_ltrip .S {
            background-color: green;
        }

        .a_ltrip .X {
            background-color: gray;
        }

        .a_ltrip .C {
            background-color: blue;
        }

        .a_ltrip .P {
            background-color: orange;
        }

        .a_ltrip .div_FixWith {
            min-width: 150px;
        }

        .div_contStatus {
            width: 80px;
            height: 21px;
            text-align: center;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding-top: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                    <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefLocation" KeyMember="Id" />
    <div>
        <table width="1000">
            <tr>
                <td>
                    <dxe:ASPxButton ID="btn_Refresh" runat="server" HorizontalAlign="Left" Width="100" Text="Refresh" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
                                                              refreshGrid();
                                                                    }" />
                                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <h2>Container Monitor </h2><br />
                    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("DoNo") %>','<%# Eval("DoType") %>');"><%# Eval("DoNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Cont No" FieldName="ContainerNo" VisibleIndex="3" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Client" FieldName="PartyName" VisibleIndex="3" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Status" FieldName="ContainerStatus" VisibleIndex="3" Width="120">
                         <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("ContainerStatus"))) %>" class="div_contStatus">
                                <%# Eval("ContainerStatus") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="SealNo" FieldName="SealNo" VisibleIndex="3" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn
                        Caption="Cont Type" FieldName="ContainerType" VisibleIndex="4" Width="120">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataSpinEditColumn
                        Caption="Weight" FieldName="Weight" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" PropertiesSpinEdit-DisplayFormatString="0.000" VisibleIndex="5" Width="100">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn
                        Caption="Volume" FieldName="M3" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" PropertiesSpinEdit-DisplayFormatString="0.000" VisibleIndex="5" Width="100">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn
                        Caption="Qty" FieldName="Qty" PropertiesSpinEdit-NumberType="Integer" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" VisibleIndex="5" Width="60">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="PackageType" FieldName="PkgType" VisibleIndex="9" Width="120">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Job Start" FieldName="JobStart" VisibleIndex="7" Width="80">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Job End" FieldName="JobEnd" VisibleIndex="7" Width="80">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="DoNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
                    <h2>Transport Monitor </h2><br />
                    <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("DoNo") %>','<%# Eval("DoType") %>');"><%# Eval("DoNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Client" FieldName="PartyName" VisibleIndex="3" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Transporter" FieldName="TransportName" VisibleIndex="3" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Status" FieldName="TransportStatus" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("TransportStatus"))) %>" class="div_contStatus">
                                <%# Eval("TransportStatus") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Driver Name" FieldName="DriverName" VisibleIndex="3" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn
                        Caption="Driver IC" FieldName="DriverIC" VisibleIndex="4" Width="120">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Driver Tel" FieldName="DriverTel" VisibleIndex="5" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="VehicleNo" FieldName="VehicleNo" VisibleIndex="5" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Job No" FieldName="TptJobNo" VisibleIndex="5" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Start" FieldName="TransportStart" VisibleIndex="7" Width="80">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="End" FieldName="TransportEnd" VisibleIndex="7" Width="80">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Remark" FieldName="Remarks" VisibleIndex="7" Width="80">
                    </dxwgv:GridViewDataDateColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="DoNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
                    <h2>Warehouse Monitor</h2><br />
                    <dxwgv:ASPxGridView ID="grid2" runat="server" ClientInstanceName="grid2"
                        KeyFieldName="Id" AutoGenerateColumns="False"
                        Width="100%">
                        <SettingsEditing Mode="EditForm" PopupEditFormWidth="750" NewItemRowPosition="Bottom" />
                        <SettingsPager PageSize="10" Mode="ShowPager">
                        </SettingsPager>
                        <Settings ShowFilterRow="false" />
                        <SettingsCustomizationWindow Enabled="True" />
                        <SettingsBehavior ConfirmDelete="True" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" Visible="false" VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" />
                            <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" />
                            <dxwgv:GridViewDataTextColumn Caption="Address" FieldName="Address" VisibleIndex="4" />
                            <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="Contact" VisibleIndex="5" />
                            <dxwgv:GridViewDataTextColumn Caption="Tel" FieldName="Tel" VisibleIndex="6" />
                            <dxwgv:GridViewDataTextColumn Caption="Fax" FieldName="Fax" VisibleIndex="7" />
                            <dxwgv:GridViewDataTextColumn Caption="StockType" FieldName="StockType" VisibleIndex="7" />
                        </Columns>
                        <Styles Header-HorizontalAlign="Center">
                            <Header HorizontalAlign="Center"></Header>
                            <Cell HorizontalAlign="Center"></Cell>
                        </Styles>
                        <SettingsDetail  ShowDetailButtons="true" ShowDetailRow="true"/>
                        <Templates>
                            <DetailRow>
                                <div style="display: none">
                                    <dxe:ASPxTextBox ID="txt_Code" ClientInstanceName="txt_Code" runat="server" Width="100%" Value='<%# Bind("Code") %>'>
                                    </dxe:ASPxTextBox>
                                </div>
                                <dxwgv:ASPxGridView ID="grid_Location" runat="server" DataSourceID="dsRefLocation" Width="100%" OnBeforePerformDataSelect="grid_Location_BeforePerformDataSelect">
                                    <SettingsPager PageSize="10" Mode="ShowPager">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="false" />
                                    <SettingsCustomizationWindow Enabled="True" />
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" Visible="false" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" />
                                        <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" />
<%--                                        <dxwgv:GridViewDataTextColumn Caption="Zone Code" FieldName="ZoneCode" VisibleIndex="4" />
                                        <dxwgv:GridViewDataTextColumn Caption="Store Code" FieldName="StoreCode" VisibleIndex="5" />--%>
                                        <dxwgv:GridViewDataTextColumn Caption="Length" FieldName="Length" VisibleIndex="6" />
                                        <dxwgv:GridViewDataTextColumn Caption="Width" FieldName="Width" VisibleIndex="7" />
                                        <dxwgv:GridViewDataTextColumn Caption="Height" FieldName="Height" VisibleIndex="8" />
                                        <dxwgv:GridViewDataTextColumn Caption="SKU" VisibleIndex="8">
                                            <DataItemTemplate>
                                                <%# GetPacking(SafeValue.SafeString(Eval("Code"))) %>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="%" VisibleIndex="8">
                                            <DataItemTemplate>
                                                 <%# Percentage(SafeValue.SafeString(Eval("Code"))) %>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <Styles Header-HorizontalAlign="Center">
                                        <Header HorizontalAlign="Center"></Header>
                                        <Cell HorizontalAlign="Center"></Cell>
                                    </Styles>
                                </dxwgv:ASPxGridView>
                            </DetailRow>
                        </Templates>
                    </dxwgv:ASPxGridView>

                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
