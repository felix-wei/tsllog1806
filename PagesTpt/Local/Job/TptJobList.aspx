﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TptJobList.aspx.cs" Inherits="PagesTpt_Job_TptJobList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transport Job</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript">
        function ShowEdit(TptNo) {
            window.location = "TptJobEdit.aspx?no=" + TptNo + "&typ=" + txt_type.GetText();
        } </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.TptJob" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td style="display: none">
                        <dxe:ASPxTextBox ID="txt_type" ClientInstanceName="txt_type" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_TptNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_customerId" ClientInstanceName="txt_customerId" runat="server" Width="57" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupParty(txt_customerId,txt_customerName,'C');
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_customerName" ClientInstanceName="txt_customerName" ReadOnly="true" BackColor="Control" Width="200" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="75" runat="server" Text="Add" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                        ShowEdit('0');
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <hr />
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="1300" AutoGenerateColumns="False" DataSourceID="dsTransport" OnHtmlRowPrepared="grid_Transport_HtmlRowPrepared">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <a onclick='ShowEdit("<%# Eval("JobNo") %>");'>Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="JobNo" VisibleIndex="1" SortIndex="0" SortOrder="Descending" Width="70">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="JobType" VisibleIndex="3" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Cust" FieldName="CustName" UnboundType="String" VisibleIndex="3" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CustJobNo" FieldName="BkgRef" VisibleIndex="4" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="BKG Date" FieldName="BkgDate" VisibleIndex="5" Width="60">
                        <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("BkgDate")) %></DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="TPT Date" FieldName="TptDate" VisibleIndex="5" Width="60">
                        <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("TptDate")) %></DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Driver" FieldName="Driver" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="Vessel" Width="100" VisibleIndex="6">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Voyage" FieldName="Voyage" Width="60" VisibleIndex="7">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pol" FieldName="Pol" VisibleIndex="16" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pod" FieldName="Pod" VisibleIndex="17" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETA" FieldName="Eta" VisibleIndex="18" Width="60">
                        <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("Eta")) %></DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETD" FieldName="Etd" VisibleIndex="19" Width="60">
                        <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("Etd")) %></DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Wt" VisibleIndex="90" Width="55" PropertiesTextEdit-DisplayFormatString="0.000">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="M3" VisibleIndex="91" Width="55" PropertiesTextEdit-DisplayFormatString="0.000">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="92" Width="55" PropertiesTextEdit-DisplayFormatString="0.000">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="50" Width="55">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" DisplayFormat="{0}" />
                    <dxwgv:ASPxSummaryItem FieldName="Wt" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                    <dxwgv:ASPxSummaryItem FieldName="M3" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                    <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
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
