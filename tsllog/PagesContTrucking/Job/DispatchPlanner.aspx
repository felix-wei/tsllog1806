﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <title></title>
    <script type="text/javascript">
        var isRefresh = true;
        function PopupTripsList(jobno, contId) {
            isRefresh = false;
            popubCtr1.SetHeaderText('Trips List');
            popubCtr1.SetContentUrl('../SelectPage/TripListForJobList.aspx?JobNo=' + jobno + "&contId=" + contId);
            popubCtr1.Show();

        }
        function SetPar(a, b) {

        }
        function AfterAddTrip() {
            isRefresh = true;
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
        }
        var timeTemp;

        //window.onload = function () {
        //        timeTemp = setInterval(refreshGrid, 30000);
        //}

        function refreshGrid() {
            if (!isRefresh) {
                return;
            }
            //window.location = "DispatchPlanner.aspx";
            //window.location.reload();
            //btn_Refresh.onClick();
            grid_driver1.Refresh();
            grid_driver2.Refresh();
            grid_driver3.Refresh();
            grid_driver4.Refresh();
            grid_driver5.Refresh();
            grid_driver6.Refresh();
            grid_driver7.Refresh();
            grid_driver8.Refresh();
            grid_driver9.Refresh();
            grid_driver10.Refresh();
            grid_driver11.Refresh();
            grid_driver12.Refresh();
            grid_driver13.Refresh();
            grid_driver14.Refresh();
            grid_driver15.Refresh();
            grid_Container.Refresh();
            grid_Driver.Refresh();
            grid_Trailer.Refresh();
            grid_parkingLot.Refresh();
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

        function PopupAddTrip(Id, jobNo, ContNo) {
            clearInterval(timeTemp);
            popubCtr.SetContentUrl('DispatchPlanner_AddTrip.aspx?det1Id=' + Id + '&JobNo=' + jobNo + '&ContNo=' + ContNo);
            popubCtr.SetHeaderText('Add Trip');
            popubCtr.Show();
        }

        function container_end_cb(v) {

            //grid_Container.Refresh();
        }
    </script>
    <style type="text/css">
        .div {
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="outerdiv" style="text-align: center;">
            <div id="head" style="width: 750px;">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 120px">Dispatch Date</td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_searchDate" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                        </td>
                        <td>Team
                        </td>
                        <td>
                            <dxe:ASPxComboBox ID="cbb_TeamNo" runat="server" Width="120">
                                <Items>
                                    <dxe:ListEditItem Value="" Text="" />
                                    <dxe:ListEditItem Value="A" Text="A" />
                                    <dxe:ListEditItem Value="B" Text="B" />
                                    <dxe:ListEditItem Value="C" Text="C" />
                                    <dxe:ListEditItem Value="D" Text="D" />
                                    <dxe:ListEditItem Value="E" Text="E" />
                                    <dxe:ListEditItem Value="F" Text="F" />
                                </Items>
                            </dxe:ASPxComboBox>
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
                        <td>
                            <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="containerdiv" style="overflow: hidden; display: inline-block; width: 2580px">
                <div id="float1" style="float: left; width: 65%; overflow: hidden">
                    <div style="float: left; width: 1700px; overflow: scroll">
                        <div class="div">
                            <table style="width: 100%" id="tb1" runat="server">
                                <tr>
                                    <th>Driver Trip :
                                        <asp:Label ID="Label1" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver1" runat="server" ClientInstanceName="grid_driver1" Width="500px" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%; display: none" id="tb2" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label2" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver2" runat="server" Width="500px" ClientInstanceName="grid_driver2" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="div">
                            <table style="width: 100%" id="tb3" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label3" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <dxwgv:ASPxGridView ID="grid_driver3" runat="server" Width="500px" ClientInstanceName="grid_driver3" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                        <DataItemTemplate>
                                                            <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                                </Columns>
                                                <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                    VerticalScrollableHeight="226" ShowFilterRow="false" />
                                                <TotalSummary>
                                                    <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                        SummaryType="Count" DisplayFormat="{0}" />
                                                </TotalSummary>
                                            </dxwgv:ASPxGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%" id="tb4" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label4" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <dxwgv:ASPxGridView ID="grid_driver4" runat="server" Width="500px" ClientInstanceName="grid_driver4" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                        <DataItemTemplate>
                                                            <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                                </Columns>
                                                <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                    VerticalScrollableHeight="226" ShowFilterRow="false" />
                                                <TotalSummary>
                                                    <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                        SummaryType="Count" DisplayFormat="{0}" />
                                                </TotalSummary>
                                            </dxwgv:ASPxGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%" id="tb5" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label5" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver5" runat="server" Width="500px" ClientInstanceName="grid_driver5" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%" id="tb6" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label6" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver6" runat="server" Width="500px" ClientInstanceName="grid_driver6" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%" id="tb7" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label7" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver7" runat="server" ClientInstanceName="grid_driver7" Width="500px" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                   
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%" id="tb8" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label8" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver8" runat="server" Width="500px" ClientInstanceName="grid_driver8" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%; display: none" id="tb9" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label9" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver9" runat="server" Width="500px" ClientInstanceName="grid_driver9" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%; display: none" id="tb10" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label10" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver10" runat="server" Width="500px" ClientInstanceName="grid_driver10" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%; display: none" id="tb11" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label11" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver11" runat="server" Width="500px" ClientInstanceName="grid_driver11" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%; display: none" id="tb12" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label12" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver12" runat="server" Width="500px" ClientInstanceName="grid_driver12" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%; display: none" id="tb13" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label13" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver13" runat="server" Width="500px" ClientInstanceName="grid_driver13" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%; display: none" id="tb14" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label14" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver14" runat="server" Width="500px" ClientInstanceName="grid_driver14" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div">
                            <table style="width: 100%; display: none" id="tb15" runat="server">
                                <tr>
                                    <th>Driver Trip : 
                                        <asp:Label ID="Label15" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_driver15" runat="server" Width="500px" ClientInstanceName="grid_driver15" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                    <DataItemTemplate>
                                                        <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Det1Id") %>")'><%# Eval("ContainerNo") %></a>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="75"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                                VerticalScrollableHeight="226" ShowFilterRow="false" />
                                            <TotalSummary>
                                                <dxwgv:ASPxSummaryItem FieldName="ContainerNo" ShowInColumn="ContainerNo"
                                                    SummaryType="Count" DisplayFormat="{0}" />
                                            </TotalSummary>
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </div>
                <div id="float2" style="float: left; width: 35%; left: 5px; overflow: hidden">
                    <div class="div">
                        <table style="width: 100%">
                            <tr>
                                <th>Container Status</th>
                            </tr>
                            <tr>
                                <td>
                                    <dxwgv:ASPxGridView ID="grid_Container" runat="server" ClientInstanceName="grid_Container"
                                        AutoGenerateColumns="False" KeyFieldName="Id" Width="100%" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared" OnCustomDataCallback="grid_Container_CustomDataCallback">
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <Settings ShowFilterRow="true" />
                                        <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                <DataItemTemplate>
                                                    <%# Eval("ContainerNo") %>
                                                    <%--<a href="#" onclick='PopupAddTrip("<%# Eval("Id") %>","<%# Eval("JobNo") %>","<%# Eval("ContainerNo") %>");'><%# Eval("ContainerNo") %></a>--%>
                                                    <br />
                                                    <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("Id") %>")'>Link</a>
                                                    <%--<a href="#"  onclick='if(confirm("Confirm End"))  {<%# "grid_Container.GetValuesOnCustomCallback(\"End_"+Container.KeyValue+"\",container_end_cb);"  %> }'>End</a>--%>
                                                </DataItemTemplate>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="Driver" Width="70"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ClientId" Caption="Shipper" Width="70">
                                                <DataItemTemplate>
                                                    <%# Eval("ClientId")+" / "+Eval("WarehouseAddress") %>
                                                </DataItemTemplate>
                                            </dxwgv:GridViewDataColumn>
                                            <%--<dxwgv:GridViewDataColumn FieldName="ClientId" Caption="Clent" Width="45"></dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataColumn FieldName="WarehouseAddress" Caption="Shipper" Width="70"></dxwgv:GridViewDataColumn>--%>
                                            <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="TripCode" Caption="Trip" Width="50"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Start" Width="50"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ToTime" Caption="End" Width="50"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
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
                        </table>
                    </div>
                    <div class="div">
                        <table style="width: 100%">
                            <tr>
                                <th>Driver Status</th>
                            </tr>
                            <tr>
                                <td>
                                    <dxwgv:ASPxGridView ID="grid_Driver" runat="server" ClientInstanceName="grid_Driver" Width="100%" AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <Settings ShowFilterRow="true" />
                                        <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                        <Columns>
                                            <dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="Driver" Width="70"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="TowheadCode" Caption="PrimeMover" Width="70"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Size" Width="45"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="45"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From" Width="100"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="100"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="TripCode" Caption="Trip" Width="50"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Start" Width="50"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ToTime" Caption="End" Width="50"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="40"></dxwgv:GridViewDataColumn>
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
                        </table>
                    </div>
                    <div class="div">
                        <table style="width: 100%">
                            <tr>
                                <th>Trailer Status</th>
                            </tr>
                            <tr>
                                <td>
                                    <dxwgv:ASPxGridView ID="grid_Trailer" runat="server" ClientInstanceName="grid_Trailer" AutoGenerateColumns="False" KeyFieldName="Id"
                                        Width="100%" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared">
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <Settings ShowFilterRow="true" />
                                        <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                        <Columns>
                                            <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="70"></dxwgv:GridViewDataColumn>
                                            <%--<dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105"></dxwgv:GridViewDataTextColumn>--%>
                                            <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">
                                                
                                                <DataItemTemplate>
                                                    <a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>")'><%# Eval("ContainerNo") %></a>
                                                </DataItemTemplate>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataColumn FieldName="Size" Caption="Size" Width="50"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="35"></dxwgv:GridViewDataColumn>
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
                    <div class="div">
                        <table style="width: 100%">
                            <tr>
                                <th>Parking Lot</th>
                            </tr>
                            <tr>
                                <td>
                                    <dxwgv:ASPxGridView ID="grid_parkingLot" runat="server" ClientInstanceName="grid_parkingLot"
                                        AutoGenerateColumns="False" KeyFieldName="Id" OnHtmlDataCellPrepared="grid_packingLot_HtmlDataCellPrepared" Width="100%">
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <Settings ShowFilterRow="true" />
                                        <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn FieldName="Code" Caption="Code" Width="105"></dxwgv:GridViewDataTextColumn>
                                            <%--<dxwgv:GridViewDataTextColumn FieldName="DriverCode" Caption="Driver" Width="105"></dxwgv:GridViewDataTextColumn>--%>
                                            <dxwgv:GridViewDataTextColumn FieldName="Trailer" Caption="Trailer"></dxwgv:GridViewDataTextColumn>
                                            <%--<dxwgv:GridViewDataColumn FieldName="JobType" Caption="Status" Width="100"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="CreateDate" Caption="Time" Width="100"></dxwgv:GridViewDataColumn>--%>
                                            <dxwgv:GridViewDataColumn FieldName="balance" Caption="Balance" Width="70"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="Size40" Caption="Size" Width="70"></dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Address" Caption="Address" Width="105"></dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                            VerticalScrollableHeight="200" ShowFilterRow="true" />
                                        <TotalSummary>
                                            <dxwgv:ASPxSummaryItem FieldName="PLCode" ShowInColumn="PLCode"
                                                SummaryType="Count" DisplayFormat="{0}" />
                                        </TotalSummary>
                                    </dxwgv:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Assign Driver" AllowDragging="True" EnableAnimation="False" Height="550"
            AllowResize="True" Width="900" EnableViewState="False">
            <ClientSideEvents CloseButtonClick="function(s,e){refreshGrid();}" />
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="550px"
            Width="1000px" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
                   isRefresh = true;
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>