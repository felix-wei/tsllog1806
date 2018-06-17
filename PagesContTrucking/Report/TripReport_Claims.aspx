<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripReport_Claims.aspx.cs" Inherits="PagesContTrucking_Report_TripReport_Claims" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript">
        function print() {
            var driver = search_Driver.GetText();
            var d1 = search_DateFrom.GetText();
            var d2 = search_DateTo.GetText();
            var ar_d1 = d1.split('/');
            if (ar_d1.length == 3) {
                d1 = ar_d1[2] + ar_d1[1] + ar_d1[0];
            } else {
                alert('Date From is Error');
                return;
            }
            var ar_d2 = d2.split('/');
            if (ar_d2.length == 3) {
                d2 = ar_d2[2] + ar_d2[1] + ar_d2[0];
            } else {
                alert('Date To is Error');
                return;
            }
            console.log('==========print:', driver, d1, d2);
            if (driver && driver.length > 0) {
                window.open('RptPrintView.aspx?doc=2&p=' + driver + '&d1=' + d1 + '&d2=' + d2);
            } else {
                alert('Require Driver!');
            }
        }
        function OnCallBack(v) {
            if (v == "Success") {
                alert("Payment Voucher Created");
				btn_search.DoClick();
                //detailGrid.Refresh();
            }
            else if(v=="Driver"){
                alert("Please select driver");
            }
            else if (v == "Error") {
                alert("Please keyin select ");
            }
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }
        function open_page(no) {
            parent.navTab.openTab(no, "/PagesAccount/EditPage/ApPaymentEdit.aspx?no=" + no, { title: no, fresh: false, external: true });
        }
        function open_job(no) {
            parent.navTab.openTab(no, "/PagesContTrucking/Job/JobEdit.aspx?no=" + no, { title: no, fresh: false, external: true });
        }
    </script>
    
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsZone" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmParkingZone" KeyMember="id" FilterExpression="" />
        <div>
            <table>
                <tr>
                    <td>Job&nbsp;No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>Cont No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_contNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>Driver
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="search_Driver" ClientInstanceName="search_Driver" runat="server" AutoPostBack="False" Width="100">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(search_Driver,null,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td style="display:none;">PrimeMover</td>
                    <td style="display:none;">
                        <dxe:ASPxButtonEdit ID="search_TowheadCode" ClientInstanceName="search_TowheadCode" runat="server" AutoPostBack="False" Width="100">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(search_TowheadCode,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td style="display:none;">
                        Type
                    </td>
                    <td style="display:none;">
                        <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Width="100" DropDownStyle="DropDown">
                            <Items>
                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                <dxe:ListEditItem Text="COL" Value="COL" />
                                <dxe:ListEditItem Text="RET" Value="RET" />
                                <dxe:ListEditItem Text="LOC" Value="LOC" />
                                <dxe:ListEditItem Text="SHF" Value="SHF" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="display:none;">Zone</td>
                    <td style="display:none;">
                        <dxe:ASPxComboBox ID="cbb_zone" runat="server"  Width="100" DataSourceID="dsZone" TextField="Code" ValueField="Code">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Date From</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateFrom" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateTo" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
					<td>
                        <dxe:ASPxCheckBox ID="cb_Claimed" ClientInstanceName="cb_Claimed" runat="server" Text="Claimed">
                            <ClientSideEvents CheckedChanged="function(s,e){
                                            
                                            }" />
                        </dxe:ASPxCheckBox>
                    </td>
					<td>
                        <dxe:ASPxCheckBox ID="cb_UnClaimed" ClientInstanceName="cb_UnClaimed" runat="server" Text="Unclaimed">
                            <ClientSideEvents CheckedChanged="function(s,e){
                                            
                                            }" />
                        </dxe:ASPxCheckBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" ClientInstanceName="btn_search" OnClick="btn_search_Click" Width="80"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click" Width="80"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print" AutoPostBack="false" Width="80">
                            <ClientSideEvents Click="function(s,e){print();}" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnSelect" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
 		  
                   <td   style="padding:4px;"> <table style="border:solid 2px #336699;">
				   <tr>
				    <td>
                        <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Create Payment" Width="80" AutoPostBack="false">
                             <ClientSideEvents Click="function(s,e){
                                 detailGrid.GetValuesOnCustomCallback('Save',OnCallBack);
                                 }" />
                        </dxe:ASPxButton>
				   </td>
				   <td>
							 Date	
				   </td>
				   <td>
						 <dxe:ASPxDateEdit ID="date_voucher" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>

				   </td>
				   <td>
							 Mode
				   </td>
				   <td>
				            <dxe:ASPxComboBox ID="cmb_voucher_mode" runat="server" Width="100" DropDownStyle="DropDown">
                            <Items>
                                <dxe:ListEditItem Text="New" Value="New" />
                                <dxe:ListEditItem Text="Combine" Value="Combine" />
                            </Items>
                        </dxe:ASPxComboBox>

				   </td>
				  
				   </tr>
				   </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="950px" KeyFieldName="Id" AutoGenerateColumns="False"
               OnCustomDataCallback="grid_Transport_CustomDataCallback"  >
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10" Visible='<%# SafeValue.SafeInt(Eval("Cnt"),0)>0?false:true %>'>
                            </dxe:ASPxCheckBox>
                            
                            <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Id") %>' Width="50">
                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="ToDate" Caption="Date">
                        <DataItemTemplate>
                            <div style="white-space: nowrap;">
                                <%# SafeValue.SafeDate(Eval("ToDate"),new DateTime(1900,1,1)).ToString("dd-MM-yyyy") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time">
                        <DataItemTemplate>
                            <div style="white-space: nowrap;">
                                <%# Eval("FromTime") %>-<%# Eval("ToTime") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Id" Caption="Trip"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="ContainerNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Type"></dxwgv:GridViewDataColumn>
                     <dxwgv:GridViewDataColumn FieldName="RequestTrailerType" Caption="Chassis Type"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                    
                    <dxwgv:GridViewDataColumn FieldName="TowheadCode" Caption="Prime Mover"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>
                   <dxwgv:GridViewDataColumn FieldName="SubCon_Code" Caption="Contractor"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="JobNo">
                        <DataItemTemplate>
                            <div style="white-space: nowrap;">
                                <a onclick='open_job("<%# Eval("JobNo") %>");'> <%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="DocNo" Caption="Voucher No">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <a onclick='open_page("<%# Eval("DocNo") %>")'><%# Eval("DocNo") %></a>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>

                    <dxwgv:GridViewDataColumn FieldName="Client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TripCode" Caption="Trip Type"></dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SealNo" Caption="SealNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="ScheduleDate" Caption="ScheduleDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="FromTime"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ToTime" Caption="ToTime"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TripCodePrice" Visible="false" Caption="Trip Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="OverTimePrice" Visible="false" Caption="OT Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="QJPrice" Visible="false" Caption="OJ Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Total" Visible="false" Caption="Total Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TripCode" Visible="false" Caption="TripCode"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Overtime"  Caption="Overtime" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="OverDistance"  Caption="Outside Jurong" Visible="false"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 200px;">
                                <%# Eval("FromCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To">
                        <DataItemTemplate>
                            <div style="min-width: 200px;">
                                <%# Eval("ToCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge1" Caption="DHC">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge1")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge2" Caption="WEIGHING">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge2")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge3" Caption="WASHING">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge3")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge4" Caption="REPAIR">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge4")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge5" Caption="DETENTION">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge5")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge6" Caption="DEMURRAGE">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge6")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge7" Caption="LIFT ON/OFF">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge7")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge8" Caption="C/SHIPMENT">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge8")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge10" Caption="EMF">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge10")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ERP" Caption="ERP">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("ERP")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ParkingFee" Caption="ParkingFee">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("ParkingFee")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge9" Caption="OTHER">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Charge9")).ToString("n") %>
                        </DataItemTemplate></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TotalCharge" Caption="Total Voucher">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("TotalCharge")).ToString("n") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <Settings  ShowFooter="true"/>
                <TotalSummary>
                    <%--<dx:ASPxSummaryItem FieldName="Incentive1" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Incentive2" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Incentive3" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Incentive4" SummaryType="Sum" DisplayFormat="{0:0.00}"/>--%>
                    <dx:ASPxSummaryItem  FieldName="Id" SummaryType="Count" DisplayFormat="{0}"/>
                    <%--<dx:ASPxSummaryItem FieldName="Charge1" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge2" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge3" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge4" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge5" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge6" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge7" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge8" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge10" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge9" SummaryType="Sum" DisplayFormat="{0:0.00}"/>--%>
                    <dx:ASPxSummaryItem FieldName="TotalCharge" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="470"
                Width="800" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
