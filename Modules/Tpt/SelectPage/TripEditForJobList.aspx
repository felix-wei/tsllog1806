<%@ page language="C#" autoeventwireup="true" codefile="TripEditForJobList.aspx.cs" inherits="Modules_Tpt_SelectPage_TripEditForJobList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="/PagesContTrucking/script/firebase.js"></script>
    <script src="/PagesContTrucking/script/js_company.js"></script>
    <script src="/PagesContTrucking/script/js_firebase.js"></script>
    <script src="/PagesContTrucking/script/jquery.js"></script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopubTptTrip(); }
        }
        document.onkeydown = keydown;
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
        <wilson:datasource id="dsTrip" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobDet2" keymember="Id" filterexpression="1=0" />
        <div>
            <div style="display: none">
                <dxe:aspxlabel id="lbl_TripId" runat="server" clientinstancename="lbl_TripId"></dxe:aspxlabel>
                <dxe:aspxtextbox id="txt_JobNo" clientinstancename="txt_JobNo" readonly="true" backcolor="Control" runat="server" width="200"></dxe:aspxtextbox>
            </div>
            <dxwgv:aspxgridview id="gv_tpt_trip" clientinstancename="gv_tpt_trip" oninit="gv_tpt_trip_Init" runat="server" autogeneratecolumns="false" width="900px" datasourceid="dsTrip" keyfieldname="Id" oncustomdatacallback="gv_tpt_trip_CustomDataCallback">
                <SettingsPager PageSize="100" />
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <SettingsBehavior ConfirmDelete="true" />

                <Templates>
                    <EditForm>
                        <table style="width: 100%"><tr>
                                <td style="border-bottom: 1px solid #808080" colspan="13"></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>JobNo:</td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" Text='<%# Bind("JobNo") %>' ReadOnly="true" BackColor="Control" runat="server" Width="100%"></dxe:ASPxTextBox>
                                            </td>
                                            <td>Trip No:</td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_TripIndex" ClientInstanceName="txt_TripIndex" Text='<%# Bind("TripIndex") %>' ReadOnly="true" BackColor="Control" runat="server" Width="100%"></dxe:ASPxTextBox>
                                            </td>
                                            <td width="10%"></td>
                                        </tr>
                                    </table>
                                </td>

                                <td colspan="1"></td>
                                <td colspan="2">
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton ID="btn_job_save" runat="server" Text="Save" Width="50" Enabled='<%# SafeValue.SafeString(Eval("canChange"),"")=="none"?false:true %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e){
                                                        gv_tpt_trip.GetValuesOnCustomCallback('Save',OnCallback);
                                                        }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td style="padding-left: 3px;">
                                                <dxe:ASPxButton ID="ASPxButton6" runat="server" Text="Open Job" Width="50" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                            goJob();
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 60px;"></td>
                            </tr>
                            <tr>
                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Trip Detail</td>
                            </tr>
                            <tr>
                                <td class="lbl">Trip Type</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Width="165" DropDownStyle="DropDown" ReadOnly="true">
                                        <Items>
                                            <dxe:ListEditItem Text="WGR" Value="WGR" />
                                            <dxe:ListEditItem Text="WDO" Value="WDO" />
                                            <dxe:ListEditItem Text="TPT" Value="TPT" />
                                            <dxe:ListEditItem Text="LOC" Value="LOC" />
                                            <dxe:ListEditItem Text="SHF" Value="SHF" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td class="lbl1">Self Delivery/Collection</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_Self_Ind" runat="server" Value='<%# Bind("Self_Ind") %>' Width="165" DropDownStyle="DropDown">
                                        <Items>
                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                            <dxe:ListEditItem Text="No" Value="No" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td class="lbl" >Direct ?</td>
                                <td class="ctl" >
                                    <dxe:ASPxComboBox ID="cbb_DirectInf" runat="server" Text='<%# Bind("DirectInf") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Normal" Text="Normal" />
                                            <dxe:ListEditItem Value="Direct Loading" Text="Direct Loading" />
                                            <dxe:ListEditItem Value="Direct Delivery" Text="Direct Delivery" />
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

                                <td class="lbl">T/P Status</td>
                                <td class="ctl">
                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="P" Text="Pending" />
                                            <dxe:ListEditItem Value="C" Text="Completed" />
                                            <dxe:ListEditItem Value="X" Text="Cancel" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="6">
                                    <hr />
                                </td>
                            </tr>
                            <tr>

                                <td class="lbl">CONT/Trailer</td>
                                <td class="ctl">

                                    <dxe:ASPxButtonEdit ID="btn_Trailer" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                PopupCTM_MasterData(btn_ContNo,null,'Chessis');
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td class="lbl1">Request Chassis Type 
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_TrailerType" ClientInstanceName="txt_TrailerType" runat="server" Text='<%# Bind("RequestTrailerType") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="lbl1">Do No 
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ManualDO" ClientInstanceName="txt_ManualDO" runat="server" Text='<%# Bind("ManualDo") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>

                                <td class="lbl">Vehicle</td>
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
                                <td class="lbl1">Request Vehicle
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_VehicleType" ClientInstanceName="txt_VehicleType" runat="server" Value='<%# Bind("RequestVehicleType") %>' Width="165">
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
                                <td class="lbl">From</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="100%">
                                    </dxe:ASPxMemo>
                                </td>
                                <%--<td class="lbl1">
                                    <a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_FromPL" ClientInstanceName="txt_FromPL" runat="server" Text='<%# Bind("FromParkingLot") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>--%>
                                
                                <td class="lbl">Attendant</td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_Attended" ClientInstanceName="btn_Attended" runat="server" Text='<%# Bind("DriverCode2") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_Attended,null,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
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
                                    <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="100%">
                                    </dxe:ASPxMemo>
                                </td>
                                <%--<td class="lbl">
                                    <a href="#" onclick="PopupParkingLot(txt_ToPL,txt_Trip_ToCode);">To Parking Lot</a>
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ToPL" ClientInstanceName="txt_ToPL" runat="server" Text='<%# Bind("ToParkingLot") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>--%>
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
                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                            <dxe:ListEditItem Value="" Text="" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="lbl">Instruction</td>
                                <td colspan="3" class="ctl">
                                    <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="100%">
                                    </dxe:ASPxMemo>
                                </td>
                                <td class="lbl">Escort Remark  </td>
                                <td colspan="3" class="ctl">
                                    <dxe:ASPxMemo ID="txt_Trip_Escort_Remark" ClientInstanceName="txt_Trip_Escort_Remark" runat="server" Text='<%# Bind("Escort_Remark") %>' Width="165">
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr  class="style_self_ind">
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
                                    <dxe:ASPxComboBox ID="cbb_SubCon_Code" ClientInstanceName="cbb_SubCon_Code" runat="server" Value='<%# Bind("SubCon_Ind") %>' Width="165" DropDownStyle="DropDownList">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="YES" />
                                            <dxe:ListEditItem Value="N" Text="NO" />
                                        </Items>
                                    </dxe:ASPxComboBox>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="14">
                                    <table style="width:100%">
                                        <tr>
                                            <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="background-color: #cccccc; padding: 4px; padding-left: 10px;">
                                                <div style="display:<%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"block":"none"%>">Warehouse</div>
                                                <div style="display:<%# SafeValue.SafeString(Eval("TripCode"))=="TPT"?"block":"none"%>">Cargo</div>
                                            </td>
                                        </tr>
                                        <tr style="display:<%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"table-row":"none"%>">
                                            <td class="lbl">Schedule Date</td>
                                            <td>
                                                <dxe:ASPxDateEdit ID="date_WarehouseScheduleDate" Value='<%# Bind("WarehouseScheduleDate") %>' runat="server" Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                            </td>
                                            <td class="lbl">Start Date</td>
                                            <td>
                                                <dxe:ASPxDateEdit ID="date_WarehouseStartDate" Value='<%# Bind("WarehouseStartDate") %>' runat="server" Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                            </td>
                                            <td class="lbl">End Date</td>
                                            <td>
                                                <dxe:ASPxDateEdit ID="date_WarehouseEndDate" Value='<%# Bind("WarehouseEndDate") %>' runat="server" Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <div style="display:<%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"block":"none"%>">Remark</div>
                                                 <div style="display:<%# SafeValue.SafeString(Eval("TripCode"))=="TPT"?"block":"none"%>">Detail</div>
                                            </td>
                                            <td colspan="3">
                                                <dxe:ASPxMemo ID="memo_WarehouseRemark" Text='<%# Bind("WarehouseRemark") %>' runat="server" Width="100%">
                                                </dxe:ASPxMemo>
                                            </td>

                                            <td class="lbl"><div style="display:<%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"block":"none"%>">Warehouse Status</div></td>
                                            <td>
                                                <div style="display:<%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"block":"none"%>">
                                                <dxe:ASPxComboBox ID="cbb_WarehouseStatus" Text='<%# Bind("WarehouseStatus") %>' runat="server" Width="165">
                                                    <Items>
                                                        <dxe:ListEditItem Value="Pending" Text="Pending" />
                                                        <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                        <dxe:ListEditItem Value="Started" Text="Started" />
                                                        <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                                    </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="border-bottom: 1px solid #808080"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="background-color: #cccccc; padding: 4px; padding-left: 10px;">Driver Input</td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">DriverRemark</td>
                                            <td colspan="3" class="ctl">
                                                <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" Text='<%# Bind("Remark1") %>' runat="server" Width="100%">
                                                </dxe:ASPxMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">DeliveryRemark</td>
                                            <td colspan="3" class="ctl">
                                                <dxe:ASPxMemo ID="txt_delivery_remark" ClientInstanceName="txt_delivery_remark" Text='<%# Bind("DeliveryRemark") %>' runat="server" Width="100%">
                                                </dxe:ASPxMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">BillingRemark</td>
                                            <td colspan="3" class="ctl">
                                                <dxe:ASPxMemo ID="txt_billing_remark" ClientInstanceName="txt_billing_remark" Text='<%# Bind("BillingRemark") %>' runat="server" Width="100%">
                                                </dxe:ASPxMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">Satifaction Indator </td>
                                            <td colspan="3" class="ctl">
                                                <dxe:ASPxMemo ID="txt_Satifaction_Indator" ClientInstanceName="txt_billing_remark" Text='<%# Bind("Satisfaction") %>' runat="server" Width="100%">
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
                                            <td class="ctl">
                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive2" Height="21px" Value='<%# Bind("Incentive2")%>' DecimalPlaces="2" Increment="0">
                                                    <SpinButtons ShowIncrementButtons="false" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
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
                                        <tr>
                                            <td colspan="2" style="background-color: #cccccc; padding: 4px; padding-left: 10px;">Consignee Signature</td>
                                            <td colspan="2" style="background-color: #cccccc; padding: 4px; padding-left: 10px;">Driver Signature</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border-right: solid 1px lightgreen">
                                                <img src='<%# show_consignee_signature(Eval("JobNo"),Eval("Id")) %>' style="width: 50%" />
                                            </td>
                                            <td colspan="2">
                                                <img src='<%# show_driver_signature(Eval("JobNo"),Eval("Id")) %>' style="width: 50%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                <dxe:ASPxButton ID="btn_cont_newTrip" runat="server" Text="Receipt List" CssClass="add_carpark" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                                    <ClientSideEvents Click="function(s,e){PopupJobReceipt(lbl_TripId.GetText());}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td colspan="5"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-bottom: 1px solid #808080" colspan="13"></td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:aspxgridview>
        </div>
        <dxpc:aspxpopupcontrol id="popubCtr" runat="server" closeaction="CloseButton" modal="True"
            popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter" clientinstancename="popubCtr"
            headertext="Party" allowdragging="True" enableanimation="False" height="370"
            width="600" enableviewstate="False">
            <ClientSideEvents CloseUp="function(s, e) {
      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:aspxpopupcontrol>
        <dxpc:aspxpopupcontrol id="popubCtrPic" runat="server" closeaction="CloseButton" modal="True"
            popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter" clientinstancename="popubCtrPic"
            headertext="Party" allowdragging="True" enableanimation="False" height="570"
            width="800" enableviewstate="False">
            <ClientSideEvents CloseUp="function(s, e) {
                    

      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:aspxpopupcontrol>
    </form>
</body>
</html>
