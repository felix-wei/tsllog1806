<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepotOutList.aspx.cs" Inherits="PagesContainer_Job_DepotOutList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Depot Out</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript">
        function ShowHouse(DocNo) {
            window.location = "ReleaseOrderEdit.aspx?no=" + DocNo;
        }
        function PopupDepotOut() {
            popubCtr.SetContentUrl('DeportOutEdit.aspx');
            popubCtr.Show();
        }
        function AfterPopubDeportIn() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            document.getElementById("btn_search").click();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.ContAssetEvent" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsUser" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.User" KeyMember="Id" FilterExpression="Port is not null" />
            <table>
                <tr>
                    <td>So No</td>
                    <td><dxe:ASPxTextBox ID="txt_soNo" runat="server"></dxe:ASPxTextBox></td>
                    <td>&nbsp;Port</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_Port" ClientInstanceName="btn_Port" runat="server" Width="150" HorizontalAlign="Left" AutoPostBack="False" Enabled="false">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(btn_Port);
                                                                    }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="75" runat="server" Text="Add" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                PopupDepotOut();
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
                    <dxwgv:GridViewDataTextColumn Caption="RO No" FieldName="DocNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Depot" FieldName="EventDepot" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Port" FieldName="EventPort" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                   <dxwgv:GridViewDataTextColumn Caption="ContainerNo" FieldName="ContainerNo" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContainerType" FieldName="ContainerType" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Event Time" FieldName="EventDateTime" VisibleIndex="6" Width="140">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Vehicle No" FieldName="VehicleNo" VisibleIndex="7">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Release Date" FieldName="ReleaseDate" VisibleIndex="8" Width="40">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="State" FieldName="State" VisibleIndex="9">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="DocNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Confirm RO" AllowDragging="True" EnableAnimation="False" Height="550"
                AllowResize="True" Width="700" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
