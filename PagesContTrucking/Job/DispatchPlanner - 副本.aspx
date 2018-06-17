<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
        var timeTemp;
        window.onload = function () {
            timeTemp=setInterval(refreshGrid, 30000);
        }
        function refreshGrid() {
            //window.location = "DispatchPlanner.aspx";
            //window.location.reload();
            //btn_Refresh.onClick();
            grid_driver1.Refresh();
            grid_driver2.Refresh();
            grid_driver3.Refresh();
            grid_driver4.Refresh();
            grid_driver5.Refresh();
            grid_driver6.Refresh();
            grid_Container.Refresh();
            grid_Driver.Refresh();
            grid_Trailer.Refresh();
        }

        function PopupSetSchDate() {
            clearTimeout(timeTemp);
            popubCtr.SetContentUrl('DispatchPlanner_SetSchDate.aspx');
            popubCtr.SetHeaderText('Set ScheduleDate');
            popubCtr.Show();
        }
        function AfterPopup() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            refreshGrid();
        }

        function PopupAddTrip(Id,jobNo, ContNo) {
            clearInterval(timeTemp);
            popubCtr.SetContentUrl('DispatchPlanner_AddTrip.aspx?det1Id='+Id+'&JobNo='+jobNo+'&ContNo='+ContNo);
            popubCtr.SetHeaderText('Add Trip');
            popubCtr.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <div>
            <table>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>Dispatch Date</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_searchDate" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Refresh" ClientInstanceName="btn_Refresh" runat="server" Text="Refresh" OnClick="btn_Refresh_Click"></dxe:ASPxButton>
                                    <%--<input type="button" onclick="refreshGrid()" value="Refresh1" />--%>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_SetSchDate" ClientInstanceName="btn_SetSchDate" runat="server" AutoPostBack="false" Text="Set Schedule Date">
                                        <ClientSideEvents Click="function(s,e){ PopupSetSchDate();}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th>Driver Trip : BULL</th>
                    <th>Driver Trip : SHANKAR</th>
                    <th>Container Status</th>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_driver1" runat="server" ClientInstanceName="grid_driver1" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_driver2" runat="server" ClientInstanceName="grid_driver2" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_Container" runat="server" ClientInstanceName="grid_Container" AutoGenerateColumns="False" KeyFieldName="Id" Width="100%" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105">
                                    <DataItemTemplate>
                                        <a href="#" onclick='PopupAddTrip("<%# Eval("Id") %>","<%# Eval("JobNo") %>","<%# Eval("ContainerNo") %>");'><%# Eval("ContainerNo") %></a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="Driver" Width="70"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="TripCode" Caption="Trip" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Start" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToTime" Caption="End" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="200" ShowFilterRow="true" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <th>Driver Trip : AH LEK</th>
                    <th>Driver Trip : AH TOH</th>
                    <th>Driver Status</th>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_driver3" runat="server" ClientInstanceName="grid_driver3" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_driver4" runat="server" ClientInstanceName="grid_driver4" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_Driver" runat="server" ClientInstanceName="grid_Driver" AutoGenerateColumns="False" KeyFieldName="Id" Width="100%" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="Driver" Width="70"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="TowheadCode" Caption="Towhead" Width="70"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="TripCode" Caption="Trip" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Start" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToTime" Caption="End" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="200" ShowFilterRow="true" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="DriverCode" ShowInColumn="DriverCode"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <th>Driver Trip : TIONG</th>
                    <th>Driver Trip : RAJU</th>
                    <th>Trailer Status</th>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_driver5" runat="server" ClientInstanceName="grid_driver5" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_driver6" runat="server" ClientInstanceName="grid_driver6" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_Trailer" runat="server" ClientInstanceName="grid_Trailer" AutoGenerateColumns="False" KeyFieldName="Id" Width="100%" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="70"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Cont" Width="105"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="75"></dxwgv:GridViewDataColumn>
                            </Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="200" ShowFilterRow="true" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ChessisCode" ShowInColumn="ChessisCode"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>

        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Assign Driver" AllowDragging="True" EnableAnimation="False" Height="550" 
            AllowResize="True" Width="900" EnableViewState="False">
            <ClientSideEvents CloseButtonClick="function(s,e){refreshGrid();}" />
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
