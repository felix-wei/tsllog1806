<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipView_List.aspx.cs" Inherits="PagesContTrucking_DriverView_ShipView_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function goJob(jobno) {
            window.location = "ShipView_Edit.aspx?jobNo=" + jobno;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Job&nbsp;No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>Container&nbsp;No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>Client</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False" ReadOnly="true">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="Add&nbsp;New" AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                    goJob('0');	
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td>Job&nbsp;Type</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_JobType" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="KD-IMP" Value="KD-IMP" />
                                <dxe:ListEditItem Text="KD-EXP" Value="KD-EXP" />
                                <dxe:ListEditItem Text="FCL-IMP" Value="FCL-IMP" />
                                <dxe:ListEditItem Text="FCL-EXP" Value="FCL-EXP" />
                                <dxe:ListEditItem Text="LOCAL" Value="LOCAL" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>From</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>Container&nbsp;Status</td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_StatusCode" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Value="All" Text="All" />
                                <dxe:ListEditItem Value="New" Text="New" />
                                <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                <dxe:ListEditItem Value="InTransit" Text="InTransit" />
                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="1500" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" Width="100" SortOrder="Descending">
                        <DataItemTemplate>
                                <a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Date">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("JobDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="StatusCode" Caption="Cont Status"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ScheduleDate" Caption="Schedule Date" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Eta" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Etd" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtdDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Haulier"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Terminalcode" Caption="Terminal"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Pol" Caption="POL"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Pod" Caption="POD"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="Pickup From"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="Delivery To"></dxwgv:GridViewDataTextColumn>

                </Columns>
            </dxwgv:ASPxGridView>


            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="600" EnableViewState="False">
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
