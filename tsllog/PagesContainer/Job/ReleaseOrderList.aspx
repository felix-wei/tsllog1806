<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReleaseOrderList.aspx.cs" Inherits="PagesContainer_Job_ReleaseOrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transport Job</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript">
        function ShowHouse(DocNo) {
            window.location = "ReleaseOrderEdit.aspx?no=" + DocNo;
        } </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.ContAsset" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>RO No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_DocNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Eta From">
                        </dxe:ASPxLabel>
                    </td>

                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>

                    <td>Port</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_Port" ClientInstanceName="btn_Port" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False" Enabled="false">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(btn_Port);
                                                                    }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="75" runat="server" Text="Add" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                        ShowHouse('0');
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" DataSourceID="dsTransport">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="30">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("DocNo")%>');">Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="RO No" FieldName="DocNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Descending" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DepotCode" FieldName="DepotCode" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="POL" FieldName="ShipPol" VisibleIndex="3" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="POD" FieldName="ShipPod" VisibleIndex="4" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="ShipVessel" VisibleIndex="5" Width="70">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Voyage" FieldName="ShipVoyage" VisibleIndex="6" Width="70">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Eta" FieldName="ShipEta" VisibleIndex="7" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ShipEta")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Etd" FieldName="ShipEtd" VisibleIndex="8" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ShipEtd")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ConsigneeName" FieldName="ConsigneeName" VisibleIndex="11"
                        Width="300">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ShipperName" FieldName="ShipperName" VisibleIndex="12"
                        Width="300">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DemurrageStartDate" FieldName="DemurrageStartDate" VisibleIndex="13" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("DemurrageStartDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="DocNo" SummaryType="Count" DisplayFormat="{0}" />
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
