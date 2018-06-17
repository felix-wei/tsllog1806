<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner_TrailerHistory.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner_TrailerHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function OpenJob(jobno) {
            parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });

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

            grid_Trailer.Refresh();
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
        <div>

            <table style="width: 100%">
                <tr>
                    <td style="width: 120px">Trailer No</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_TrailerNo" runat="server" ></dxe:ASPxButtonEdit>
                    </td>
                    <td style="width: 120px">History Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_From" runat="server" Width="100" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_To" runat="server" Width="100" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Refresh" ClientInstanceName="btn_Refresh" runat="server" Text="Retrieve" OnClick="btn_Refresh_Click"></dxe:ASPxButton>
                       
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Back" ClientInstanceName="btn_Back" runat="server" Text="Back To Trailers" OnClick="btn_Back_Click"></dxe:ASPxButton>
                       
                    </td>
                </tr>
            </table>
        </div>
		<hr>
        <div>

            <table style="width: 100%">
                <tr>
                    <th>Trailer History</th>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_Trailer" runat="server" ClientInstanceName="grid_Trailer" AutoGenerateColumns="False" KeyFieldName="Id"
                            Width="100%"  >
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings ShowFilterRow="true" />
                            <Columns>
                                <dxwgv:GridViewDataColumn FieldName="TripCode" Caption="TripType"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="150"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToParkingLot" Caption="Parking" Width="150"></dxwgv:GridViewDataColumn>
                               <dxwgv:GridViewDataColumn FieldName="FromDate" Caption="Date" Width="50"></dxwgv:GridViewDataColumn>
                               <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="35"></dxwgv:GridViewDataColumn> 
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">

                                    <DataItemTemplate>
										<%# Eval("ContainerNo") %><br>
                                        <a href='javascript: TrailerHistory("<%# Eval("JobNo") %>")'><%# Eval("JobNo") %></a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>

                                

								</Columns>
                            <%--<Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="200" ShowFilterRow="true" />--%>
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ChessisCode" ShowInColumn="ChessisCode"
                                    SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
