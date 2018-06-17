﻿<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="SalesOrderReleaseList.aspx.cs" Inherits="WareHouse_SalesOrders_SalesOrderReleaseList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowHouse(masterId) {
            window.location = "SalesOrderReleaseEdit.aspx?no=" + masterId;
        }
    </script>
</head>
<body>
     <wilson:DataSource ID="dsWhSORelease" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.WhSORelease" KeyMember="Id" FilterExpression="1=0" />
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lab_PoNo" runat="server" Text="Release No">
                        </dxe:ASPxLabel>

                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_PoNo" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="ReleaseDate From">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td> <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Customer">
                        </dxe:ASPxLabel>
                    </td>
                    <td colspan="6">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="100px" HorizontalAlign="Left" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_CustId,txt_CustName);
                                }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="375px" runat="server" style="margin-bottom: 0px">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Customer RefNo">
                        </dxe:ASPxLabel>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_PartyRefNo" Width="170px" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td colspan="4">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Add" Width="100px" runat="server" Text="Add"
                                        AutoPostBack="False" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
                                    ShowHouse('0');	
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="900px" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("ReleaseNo") %>');">Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Id" FieldName="Id" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="30" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Release No" FieldName="ReleaseNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Release Date" FieldName="ReleaseDate" VisibleIndex="3" Width="100">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="PartyName" VisibleIndex="4" Width="100%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WarehouseId" VisibleIndex="4" Width="100%">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="ReleaseNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
