<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner_TrailerBorrow.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner_TrailerBorrow" %>

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
                    <td style="width: 120px">Borrow Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_From" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_To" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
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
                    <th>Trailer Borrow History</th>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_Trailer" runat="server" ClientInstanceName="grid_Trailer" AutoGenerateColumns="False" KeyFieldName="Id"
                            Width="100%"  >
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings ShowFilterRow="true" />
                            <Columns>
                                <dxwgv:GridViewDataColumn FieldName="Code" Caption="Trailer" Width=120></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Size" Width="80"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Note4" Caption="Borrow From" Width="150"></dxwgv:GridViewDataColumn>
                               <dxwgv:GridViewDataColumn FieldName="Date6" Caption="Borrow Date" Width="100">
                                    <DataItemTemplate>
										<%# Eval("Date6","{0:dd/MM/yyyy}") == "01/01/1753" ? "" : Eval("Date6","{0:dd/MM/yyyy}") %>
                                    </DataItemTemplate>
							   </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Note5" Caption="Borrow Remark" Width="150"></dxwgv:GridViewDataColumn>
                               <dxwgv:GridViewDataColumn FieldName="Date7" Caption="Return By" Width="100">
                                    <DataItemTemplate>
										<%# Eval("Date7","{0:dd/MM/yyyy}") == "01/01/1753" ? "" : Eval("Date7","{0:dd/MM/yyyy}") %>
                                    </DataItemTemplate>
							   </dxwgv:GridViewDataColumn>
                               <dxwgv:GridViewDataColumn FieldName="Date8" Caption="Return Date" Width="100">
                                    <DataItemTemplate>
										<%# Eval("Date8","{0:dd/MM/yyyy}") == "01/01/1753" ? "" : Eval("Date8","{0:dd/MM/yyyy}") %>
                                    </DataItemTemplate>
							   </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Note6" Caption="Return Remark" Width="150"></dxwgv:GridViewDataColumn>
								</Columns>
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
