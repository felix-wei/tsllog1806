<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobSchedule.aspx.cs" Inherits="WareHouse_Job_JobSchedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
            <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowHouse(masterId) {
            window.location = "JobScheduleEdit.aspx?no=" + masterId;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Date From">
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
                    <td><dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Status">
                        </dxe:ASPxLabel></td>
                    <td> <dxe:ASPxComboBox ID="cmb_Type" runat="server" Width="100px" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
                            <Items>
                                <dxe:ListEditItem Text="PENDING" Value="PENDING" />
                                <dxe:ListEditItem Text="WORKING" Value="WORKING" />
                                <dxe:ListEditItem Text="COMPLETE" Value="COMPLETE" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                      <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td style="display:none">
                        <dxe:ASPxButton ID="btn_Add" Width="100px" runat="server" Text="Add"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                    ShowHouse('0');	
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="20">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("JobNo") %>');">Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Schedule Date" FieldName="JobDate" VisibleIndex="2" Width="50">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Client Name" FieldName="CustomerName" VisibleIndex="4" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Origin" FieldName="OriginPort" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Destination" FieldName="DestinationPort" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Volume/Loads" FieldName="Volumne" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="Contact" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Materials" FieldName="WareHouseId" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Trucks/Mode" FieldName="Mode" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Supervisor/Crews" FieldName="Value6" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Start Date/Time" FieldName="PackDate" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="End Date/Time" FieldName="MoveDate" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="job Status" FieldName="JobStatus" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ops Note" FieldName="Notes" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ops Leave Note" FieldName="WareHouseId" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Whs Location" FieldName="WareHouseId" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Whs Note" FieldName="WareHouseId" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>

                </Columns>
                <Settings ShowFooter="True" />

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
