<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Air_ExportRefList.aspx.cs" Inherits="PagesAir_Export_Air_ExportRefList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            window.location = "Air_ExportRefEdit.aspx?no=" + masterId;

        }
        function ShowHouse(masterNo, jobNo) {
            window.location = "Air_ExportEdit.aspx?masterNo=" + masterNo + "&no=" + jobNo;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Ref No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="130" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Agent
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_AgtId" ClientInstanceName="txt_AgtId" runat="server" Width="100" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupAgent(txt_AgtId,txt_AgtName);
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">

                        <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="txt_AgtName" ReadOnly="true" BackColor="Control" Width="370" runat="server">
                        </dxe:ASPxTextBox>

                    </td>
                </tr>
                <tr>
                    <td>MAWB</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_MAWB" Width="130px" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Customer</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="100" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_CustId,txt_CustName);
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">

                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="370" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="50">HAWB
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HAWB" Width="130px" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Flight Time" Width="70">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td width="30">
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td width="130">
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                    ShowJob('0');	
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_ref" ClientInstanceName="grid_ref" runat="server" KeyFieldName="Id"
                AutoGenerateColumns="False"  OnHtmlRowPrepared="grid_ref_HtmlRowPrepared">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager PageSize="100">
                </SettingsPager>
                <SettingsBehavior ConfirmDelete="True" />
                <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="900" PopupEditFormHorizontalAlign="WindowCenter"
                    PopupEditFormVerticalAlign="WindowCenter" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Ref No" VisibleIndex="0" Width="100"
                        SortIndex="1">
                        <DataItemTemplate>
                            <a onclick='ShowJob("<%# Eval("RefNo") %>");'><%# Eval("RefNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="MAWB" FieldName="MAWB" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Arrival Port" FieldName="AirportCode1" VisibleIndex="3" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Departure Date" FieldName="FlightDate0" VisibleIndex="4" Width="80" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Arrival Date" FieldName="FlightDate1" VisibleIndex="5" Width="80" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" VisibleIndex="20" Width="100"
                        SortIndex="1">
                        <DataItemTemplate>
                            <a onclick='ShowHouse("<%# Eval("RefNo") %>","<%# Eval("JobNo") %>");'><%# Eval("JobNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" Width="100" VisibleIndex="21">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="HAWB" FieldName="HAWB" VisibleIndex="22" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="24" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="25" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="26" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="JobStatusCode" VisibleIndex="100" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    </Columns>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_ref">
            </dxwgv:ASPxGridViewExporter>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="300"
                AllowResize="True" Width="400" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
