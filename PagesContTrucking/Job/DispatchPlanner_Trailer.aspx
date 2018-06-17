<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner_Trailer.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner_Trailer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

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
		function OnCallback(a)
		{
			alert(a);
		}
		function DoBorrow()
		{
			grid0.GetValuesOnCustomCallback('B',OnCallback);

		}
		function DoReturn()
		{
			grid0.GetValuesOnCustomCallback('R',OnCallback);
		}
		function showBorrow()
		{
			document.getElementById('div_trailer_borrow').style.display = '';
		}
		function hideBorrow()
		{
			document.getElementById('div_trailer_borrow').style.display = 'none';
		}
		function showReturn(tr, fr, bd, rd)
		{
			txt_TrailerNo2.SetText(tr);
			txt_BorrowFrom2.SetText(fr);
			txt_BorrowDate2.SetText(bd);
			txt_ReturnBy2.SetText(rd);
			
			document.getElementById('div_trailer_return').style.display = '';
		}
		function hideReturn()
		{
			document.getElementById('div_trailer_return').style.display = 'none';
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

            <table>
                <tr>
                    <td style="width: 100px">Dispatch Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_searchDate" runat="server" EditFormatString="dd/MM/yyyy" Width="100"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Refresh" ClientInstanceName="btn_Refresh" runat="server" Text="Refresh" OnClick="btn_Refresh_Click"></dxe:ASPxButton>
                        <%--<input type="button" onclick="refreshGrid()" value="Refresh1" />--%>
                    </td>
					<td>
					&nbsp;&nbsp;<a href="javascript:showBorrow();">Borrow Trailer</a>
					</td>
					<td>
                    &nbsp;&nbsp;<a href='DispatchPlanner_TrailerBorrow.aspx'>Borrow History</a>
					
					</td>
					<td>
                    &nbsp;&nbsp;<a href='DispatchPlanner_TrailerSetup.aspx'>Manage Trailer</a>
					
					</td>
                </tr>
            </table>
        </div>
		<hr>
		<div id="div_trailer_borrow" style="display:none">

            <table border=0 cellspacing=2 style="border:solid 3px #808080">
                <tr>
                    <td style="width: 80px">Trailer No<br>
                        <dxe:ASPxTextBox ID="txt_TrailerNo" ClientInstanceName="txt_TrailerNo" runat="server" width="80"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 80px">Size<br>
                    <dxe:ASPxComboBox runat="server" ID="cmb_TrailerSize" ClientInstanceName="cmb_TrailerSize"
                        Width="80">
                        <Items>
                            <dxe:ListEditItem Value="20FT" Text="20FT" />
                            <dxe:ListEditItem Value="40FT" Text="40FT" />
                            <dxe:ListEditItem Value="40FT TRI" Text="40FT TRI" />
                        </Items>
                    </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 100px">Borrow From<br>
                        <dxe:ASPxTextBox ID="txt_BorrowFrom" ClientInstanceName="txt_BorrowFrom" runat="server" width="100"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 90px">Borrow Date<br>
                        <dxe:ASPxDateEdit ID="date_BorrowDate" Width=90 runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 90px">Return By<br>
                        <dxe:ASPxDateEdit ID="date_ReturnBy" Width=90 runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 120px">Remarks<br>
                        <dxe:ASPxTextBox ID="txt_BorrowRemark" ClientInstanceName="txt_BorrowRemark" runat="server" width="120"></dxe:ASPxTextBox>
                    </td>
					
                    <td>
					
                               <dxe:ASPxButton ID="btn_Borrow" runat="server" Width="80" AutoPostBack="false" UseSubmitBehavior="false"   Text="Borrow">
                                    <ClientSideEvents Click="function(s,e) {
										DoBorrow();
                                                 }" />
                                </dxe:ASPxButton>
 					
						</dxe:ASPxButton> 
                    </td>
					<td>
					&nbsp;&nbsp;<a href="javascript:hideBorrow();">Cancel</a>
					</td>
					
                </tr>
            </table>
			<hr>

		</div>
		<div id="div_trailer_return" style="display:none">

            <table border=1 cellspacing=2 style="border:solid 3px #808080">
                <tr>
                    <td style="width: 100px">
						Trailer No<br>
                        <dxe:ASPxTextBox Readonly="true" ID="txt_TrailerNo2" ClientInstanceName="txt_TrailerNo2" runat="server" width="80"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">Borrow From<br>
					    <dxe:ASPxTextBox Readonly="true" ID="txt_BorrowFrom2" ClientInstanceName="txt_BorrowFrom2" runat="server" width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 90px">Borrow Date<br>
                        <dxe:ASPxDateEdit Readonly="true" Width=90 ID="date_BorrowDate2" ClientInstanceName="txt_BorrowDate2" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 90px">Return By<br>
                        <dxe:ASPxDateEdit Readonly="true" Width=90 ID="date_ReturnBy2" ClientInstanceName="txt_ReturnBy2" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 90px">Return Date<br>
                        <dxe:ASPxDateEdit ID="date_ReturnDate" Width=90 ClientInstanceName="txt_ReturnDate2" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 120px">Remarks<br>
                        <dxe:ASPxTextBox ID="txt_ReturnRemark" ClientInstanceName="txt_ReturnRemark" runat="server" width="120"></dxe:ASPxTextBox>
                    </td>
					
                    <td><br>
                               <dxe:ASPxButton ID="btn_Return" runat="server" Width="80" AutoPostBack="false" UseSubmitBehavior="false"  Text="Return">
                                    <ClientSideEvents Click="function(s,e) {
										DoReturn();
                                                 }" />
                                </dxe:ASPxButton>
                    </td>
					<td>
					&nbsp;&nbsp;<a href="javascript:hideReturn();">Cancel</a>
					</td>
					
                </tr>
            </table>
			<hr>

		</div>
        <div>

            <table style="width: 100%">
                <tr>
                    <th>Trailer Status</th>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="grid_Trailer" runat="server" ClientInstanceName="grid0" AutoGenerateColumns="False" KeyFieldName="Id"
                            Width="100%" OnHtmlDataCellPrepared="grid_driver_HtmlDataCellPrepared"
							OnCustomDataCallback="grid0_CustomDataCallback"
							>
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings ShowFilterRow="true" />
                            <Columns>
                                <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Width="90">
                                    <DataItemTemplate>
									<span style='font-size:12px;padding:2px;<%# Eval("Note2","{0}") == "Y" ? "" : "background:orange;" %>'><b><%# Eval("ChessisCode")%></b></span>
                                    </DataItemTemplate>
								
								</dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Size" Caption="Size" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="CurContStatus" Caption="Status" Width="70"></dxwgv:GridViewDataColumn>
                              <%--  <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From"></dxwgv:GridViewDataColumn>--%>
                                <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To" Width="150"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="ToParkingLot" Caption="Parking" Width="150"></dxwgv:GridViewDataColumn>
                              <%--  <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time" Width="50"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn FieldName="Statuscode" Caption="Status" Width="35"></dxwgv:GridViewDataColumn> --%>
                                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="105">

                                    <DataItemTemplate>
										<%# Eval("ContainerNo") %><br>
                                        <a href='javascript: TrailerHistory("<%# Eval("JobNo") %>")'><%# Eval("JobNo") %></a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>

                                <dxwgv:GridViewDataTextColumn FieldName="" Caption="History" Width="105">

                                    <DataItemTemplate>
                                        <a href='DispatchPlanner_TrailerHistory.aspx?no=<%# Eval("ChessisCode") %>'>History</a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="" Caption="" Width="105">

                                    <DataItemTemplate>
										&nbsp;&nbsp;<a style='display:<%# Eval("Note2","{0}")!="N" ? "none" : ""%>' href='javascript:showReturn("<%# Eval("ChessisCode")%>","<%# Eval("Note4")%>","<%# Eval("Date6","{0:dd/MM/yyyy}")%>","<%# Eval("Date7","{0:dd/MM/yyyy}")%>");'>Return</a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
				 

								</Columns>
                            <Settings ShowFooter="true" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="400" ShowFilterRow="true" /> 
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
