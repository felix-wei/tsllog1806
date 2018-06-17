<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripEditForJobList.aspx.cs" Inherits="PagesContTrucking_SelectPage_TripEditForJobList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="/PagesContTrucking/script/firebase.js"></script>
    <script src="/PagesContTrucking/script/js_company.js"></script>
    <script src="/PagesContTrucking/script/js_firebase.js"></script>
    <script src="/PagesContTrucking/script/jquery.js"></script>
    <script type="text/javascript">
        function goJob() {
            //window.location = "JobEdit.aspx?jobNo=" + jobno;
            var jobno = txt_JobNo.GetText();
            parent.parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function OnCallback(v) {
            if (v == "Success" && v.indexOf('Success') >= 0) {
                alert('Action Success!');
                gv_tpt_trip.Refresh();
            }
        }
        function RowClickHandler(s, e) {
            dde_Trip_ContNo.SetText(gridPopCont.cpContN[e.visibleIndex]);
            dde_Trip_ContId.SetText(gridPopCont.cpContId[e.visibleIndex]);
            dde_Trip_ContNo.HideDropDown();
        }
        function save_job() {
            gv_tpt_trip.GetValuesOnCustomCallback('Save', OnCallback);
        }
        function complete_trip() {
            gv_tpt_trip.GetValuesOnCustomCallback('Complete', OnCallback);
        }
    </script>
    <style type="text/css">
        .fee_showhide {
            display: none;
        }

        .add_carpark {
            float: right;
            margin-right: 10px;
            width: 100px;
        }

        .gv_editform {
            width: 800px;
        }

        .gv {
            width: 950px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsZone" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmParkingZone" KeyMember="id" FilterExpression="" />
        <div>
            <div style="display: none">
                <dxe:ASPxLabel ID="lbl_TripId" runat="server" ClientInstanceName="lbl_TripId"></dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lb_ContId" runat="server" ClientInstanceName="lb_ContId"></dxe:ASPxLabel>
                <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" ReadOnly="true" BackColor="Control" runat="server" Width="200"></dxe:ASPxTextBox>
            </div>
            <table style="width: 100%">
                <tr>
                    <td>JobNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="lbl_JobNo" ClientInstanceName="lbl_JobNo" ReadOnly="true" BackColor="Control" runat="server" Width="200"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="2">
                        <table style="border-spacing: 0px;">
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_job_save" runat="server" Text="Save" Width="80" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            save_job();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Complete" Width="80" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            complete_trip();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px solid #808080" colspan="13"></td>
                </tr>
                <tr>
                    <td>ContNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Width="200"></dxe:ASPxTextBox>
                    </td>
                    <td>SealNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_sealno" runat="server" Width="200"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="1"></td>
                    
                    <td style="width:60px;"></td>
                </tr>
                <tr>
                    <td>From:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_from" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_from" runat="server"></asp:Label>--%>
                    </td>
                    <td>To:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_to" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_to" runat="server"></asp:Label>--%>
                    </td>
                    <td>Depot:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_depot" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_depot" runat="server"></asp:Label>--%>
                    </td>
                </tr>

                <tr>
                    <td>Job Instruction:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_instruction" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_depot" runat="server"></asp:Label>--%>
                    </td>
                    <td>Remark:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_Remark" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_depot" runat="server"></asp:Label>--%>
                    </td>
                    <td>Permit No:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_PermitNo" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_depot" runat="server"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <td>DG/J5:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Portnet:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_PortnetStatus" runat="server" DropDownStyle="DropDown" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="" Text="" />
                                            <dxe:ListEditItem Value="Created" Text="Created" />
                                            <dxe:ListEditItem Value="Released" Text="Released" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>MT/LDN:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_warehouse_status" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Empty" Text="Empty" />
                                            <dxe:ListEditItem Value="Laden" Text="Laden" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Email Sent:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_EmailInd" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Urgent&nbsp;Job:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_UrgentInd" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="gv_tpt_trip" ClientInstanceName="gv_tpt_trip" OnInit="gv_tpt_trip_Init" runat="server" AutoGenerateColumns="false" Width="950px" DataSourceID="dsTrip" KeyFieldName="Id" OnCustomDataCallback="gv_tpt_trip_CustomDataCallback">
                <SettingsPager PageSize="100" />
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <SettingsBehavior ConfirmDelete="true" />

                <Templates>
                    <EditForm>
                        <table>
                            <tr>
                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Trip Detail</td>
                            </tr>
                            <tr>
                                <td class="lbl">Trip Type</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Width="165" DropDownStyle="DropDown">
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
                                <td class="lbl1">Chassis Type 
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_TrailerType" ClientInstanceName="txt_TrailerType" runat="server" Text='<%# Bind("RequestTrailerType") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="lbl">Driver</td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,btn_TowheadCode);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">PrimeMover</td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheadCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(btn_TowheadCode,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td class="lbl1">Vehicle Type 
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_VehicleType" ClientInstanceName="cbb_VehicleType" runat="server" Value='<%# Bind("RequestVehicleType") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="25ton" Text="25ton" />
                                            <dxe:ListEditItem Value="50ton" Text="50ton" />
                                            <dxe:ListEditItem Value="70ton" Text="70ton" />
                                            <dxe:ListEditItem Value="100ton" Text="100ton" />
                                            <dxe:ListEditItem Value="110ton" Text="110ton" />
                                            <dxe:ListEditItem Value="160ton" Text="160ton" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>

                                <td class="lbl">Status</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                        <Items>
                                            <%--<dxe:ListEditItem Value="U" Text="Use" />--%>
                                            <dxe:ListEditItem Value="S" Text="Start" />
                                            <%--<dxe:ListEditItem Value="D" Text="Doing" />
                                                                                <dxe:ListEditItem Value="W" Text="Waiting" />--%>
                                            <dxe:ListEditItem Value="P" Text="Pending" />
                                            <dxe:ListEditItem Value="C" Text="Completed" />
                                            <dxe:ListEditItem Value="X" Text="Cancel" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>


                                <td class="lbl">T/P Schedule</td>
                                <td class="ctl">
                                    <dxe:ASPxDateEdit ID="date_BookingDate" runat="server" Value='<%# Bind("BookingDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                </td>
                                <td class="lbl">Time</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_BookingTime" runat="server" Text='<%# Bind("BookingTime") %>' Width="162">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="lbl">Double Mounting</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cmb_DoubleMounting" runat="server" Value='<%# Bind("DoubleMounting") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                            <dxe:ListEditItem Value="No" Text="No" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">From</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="414">
                                    </dxe:ASPxMemo>
                                </td>
                                <td class="lbl1">
                                    <a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_FromPL" ClientInstanceName="txt_FromPL" runat="server" Text='<%# Bind("FromParkingLot") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">From Date</td>
                                <td class="ctl">
                                    <dxe:ASPxDateEdit ID="txt_FromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                </td>
                                <td class="lbl">Time</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="161">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">To</td>
                                <td colspan="3" class="ctl2">
                                    <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="414">
                                    </dxe:ASPxMemo>
                                </td>
                                <td class="lbl">
                                    <%--<a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>--%>
                                    <a href="#" onclick="PopupParkingLot(txt_ToPL,txt_Trip_ToCode);">To Parking Lot</a>
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ToPL" ClientInstanceName="txt_ToPL" runat="server" Text='<%# Bind("ToParkingLot") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">ToDate</td>
                                <td class="ctl">
                                    <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                </td>
                                <td class="lbl">Time</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="161">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="lbl">Escort Ind</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cmb_Escort_Ind" runat="server" Width="165" Value='<%# Bind("Escort_Ind") %>'>
                                        <Items>
                                            <dxe:ListEditItem Value="" Text="" />
                                            <dxe:ListEditItem Value="Yes" Text="Yes" />

                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Trip Instruction</td>
                                <td colspan="3" class="ctl">
                                    <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="414">
                                    </dxe:ASPxMemo>
                                </td>
                                <td class="lbl">Escort Remark  </td>
                                <td colspan="3" class="ctl">
                                    <dxe:ASPxMemo ID="txt_Trip_Escort_Remark" ClientInstanceName="txt_Trip_Escort_Remark" runat="server" Text='<%# Bind("Escort_Remark") %>' Width="165">
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contractor
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_SubCon_Code" ClientInstanceName="btn_SubCon_Code" runat="server" Text='<%# Bind("SubCon_Code") %>' Width="165" AutoPostBack="False" ReadOnly="true">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_SubCon_Code,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td class="lbl">Sub-Contract</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_SubCon_Code" ClientInstanceName="cbb_SubCon_Code" runat="server" Value='<%# Bind("SubCon_Ind") %>' Width="100%" DropDownStyle="DropDownList">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="YES" />
                                            <dxe:ListEditItem Value="N" Text="NO" />
                                        </Items>
                                    </dxe:ASPxComboBox>

                                </td>
                                <td class="lbl">Self Ind</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_Self_Ind" runat="server" Value='<%# Bind("Self_Ind") %>' Width="100%" DropDownStyle="DropDownList">
                                        <Items>
                                            <dxe:ListEditItem Value="YES" Text="YES" />
                                            <dxe:ListEditItem Value="NO" Text="NO" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Input</td>
                            </tr>
                            <tr>
                                <td class="lbl">Container
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxDropDownEdit ID="dde_Trip_ContNo" runat="server" ClientInstanceName="dde_Trip_ContNo"
                                        Text='<%# Bind("ContainerNo") %>' Width="165" AllowUserInput="false">
                                        <DropDownWindowTemplate>
                                            <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridPopCont"
                                                Width="300px" KeyFieldName="Id" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents RowClick="RowClickHandler" />
                                            </dxwgv:ASPxGridView>
                                        </DropDownWindowTemplate>
                                    </dxe:ASPxDropDownEdit>
                                </td>
                                <td class="lbl">Trailer</td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_MasterData(btn_ChessisCode,null,'Chessis');
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td class="lbl">Zone</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_zone" runat="server" Value='<%# Bind("ParkingZone") %>' Width="165" DataSourceID="dsZone" TextField="Code" ValueField="Code">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="lbl">DriverRemark</td>
                                <td colspan="3" class="ctl">
                                    <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="414">
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>

                                <td class="lbl">Incentive</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive1" Height="21px" Value='<%# Bind("Incentive1")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td class="lbl">Overtime</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive2" Height="21px" Value='<%# Bind("Incentive2")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td class="lbl">Standby</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive3" Height="21px" Value='<%# Bind("Incentive3")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">PSA ALLOWANCE</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_Incentive4" runat="server" Value='<%# Bind("Incentive4") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="0" Text="0" />
                                            <dxe:ListEditItem Value="5" Text="5" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Surcharge</td>
                            </tr>
                            <tr>
                                <td class="lbl">DHC</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge1" Height="21px" Value='<%# Bind("Charge1")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td class="lbl">WEIGHING</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge2" Height="21px" Value='<%# Bind("Charge2")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td class="lbl">WASHING</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge3" Height="21px" Value='<%# Bind("Charge3")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="lbl">REPAIR</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge4" Height="21px" Value='<%# Bind("Charge4")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td class="lbl">DETENTION</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge5" Height="21px" Value='<%# Bind("Charge5")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td class="lbl">DEMURRAGE</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge6" Height="21px" Value='<%# Bind("Charge6")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                
                                <td class="lbl">LIFT ON/OFF</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge7" Height="21px" Value='<%# Bind("Charge7")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td class="lbl">C/SHIPMENT</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge8" Height="21px" Value='<%# Bind("Charge8")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                 <td class="lbl">EMF</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge10" Height="21px" Value='<%# Bind("Charge9")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                               
                                <td class="lbl">ERP</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ERP" Height="21px" Value='<%# Bind("Charge_ERP")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td class="lbl">ParkingFee</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ParkingFee" Height="21px" Value='<%# Bind("Charge_ParkingFee")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                
                                <td class="lbl">OTHER</td>
                                <td class="ctl">
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge9" Height="21px" Value='<%# Bind("Charge10")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="370"
            Width="600" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="popubCtrPic" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrPic"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
            Width="800" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
                    

      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
