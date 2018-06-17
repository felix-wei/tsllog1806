<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripsUpdate.aspx.cs" Inherits="PagesContTrucking_Job_TripsUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/ConTrucking_planner.css" rel="stylesheet" />
    <link href="../script/f_dev.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>
    <script src="../script/jquery.js"></script>
    <style type="text/css">
        #grid_Transport_DXMainTable > tbody > tr:hover {
            background-color: #e9e9e9;
        }
    </style>
    <script type="text/javascript">
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }
        var config = {
            timeout: 0,
            gridview: 'grid_Transport',
        }

        $(function () {
            loading.hide();
        })

        //======================
        
        var var_par0 = null;
        var var_par1 = null;
        var var_par2 = null;
        function trip_update_poppup_exchange(s, func, par0, par1, par2) {
            //console.log(s.uniqueID, par0);
            var ar_id = s.uniqueID.split('$');
            var inputId0 = ar_id[0] + '_' + ar_id[1] + '_';
            var inputId2 = '_I';
            var_par0 = par0;
            var_par1 = par1;
            var_par2 = par2;
            if (par0 && par0.SetText) {
                var_par0 = {};
                var_par0.SetText = function (t) {
                    //console.log(t);
                    document.getElementById(inputId0 + par0.uniqueID.split('$')[2] + inputId2).value = t;
                }
            }
            if (par1 && par1.SetText) {
                var_par1 = {};
                var_par1.SetText = function (t) {
                    document.getElementById(inputId0 + par1.uniqueID.split('$')[2] + inputId2).value = t;
                }
            }
            if (par2 && par2.SetText) {
                var_par2 = {};
                var_par2.SetText = function (t) {
                    document.getElementById(inputId0 + par2.uniqueID.split('$')[2] + inputId2).value = t;
                }
            }
            func(var_par0, var_par1, var_par2);
        }


        //==================
        
        function trip_update_inline(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                detailGrid.GetValuesOnCustomCallback('UpdateInline_' + rowIndex, trip_update_inline_callback);
            }, config.timeout);
        }
        function trip_update_inline_callback(res){
            console.log(res);
            loading.hide();
            if (res.indexOf('Save Error') >= 0) {
                console.log('===========', res);
                alert('Save Error');
            } else {
                parent.notice('Save Successful', '', 'success');

                var ar = res.split(',');
                var driver = ",";
                for (var i = 2; i < ar.length; i++) {
                    driver = driver + ar[i] + ',';
                }
                var detail = {
                    controller: ar[0],
                    no: ar[1],
                    driver: driver,
                }
                console.log('=========', detail);
                SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div_tc" class="tc_layout">
        </div>
        <div>
            <table>
                <tr>
                    <td>JobNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="search_jobNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>ContainerNO:</td>
                    <td>
                        <dxe:ASPxTextBox ID="search_containerNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>Date&nbsp;From:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_dateFrom" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="120"></dxe:ASPxDateEdit>
                    </td>
                    <td>To:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_dateTo" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="120"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <asp:Button ID="btn_search" runat="server" CssClass="button" Text="Retrieve" OnClick="btn_search_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                    <td>Trip&nbsp;Status:</td>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxCheckBox ID="search_tripStatus1" runat="server" Text="Pending"></dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="search_tripStatus2" runat="server" Text="Start"></dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="search_tripStatus3" runat="server" Text="Completed"></dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="search_tripStatus4" runat="server" Text="Cancel"></dxe:ASPxCheckBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="100%"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Settings HorizontalScrollBarMode="Visible" />
                <Settings VerticalScrollableHeight="450" />
                <Settings VerticalScrollBarMode="Visible" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="Id" Caption="#" Settings-AllowSort="False" Width="70" FixedStyle="Left">
                        <DataItemTemplate>
                            <input type="button" class="button" value="Save" onclick="trip_update_inline(<%# Container.VisibleIndex %>);" />
                            <asp:TextBox ID="txt_tripId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:TextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="JobNo" Caption="JobNo" Settings-AllowSort="False" Width="116" FixedStyle="Left">
                        <DataItemTemplate>
                            <asp:TextBox ID="txt_JobNo" runat="server" Text='<%# Eval("JobNo") %>' ReadOnly="true" Width="100"></asp:TextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ContainerNo" Caption="ContainerNo" Settings-AllowSort="False" Width="116" FixedStyle="Left">
                        <DataItemTemplate>
                            <asp:TextBox ID="txt_ContainerNo" runat="server" Text='<%# Eval("ContainerNo") %>' ReadOnly="true" Width="100"></asp:TextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Tripcode" Caption="TripType" Settings-AllowSort="False" Width="66" FixedStyle="Left">
                        <DataItemTemplate>
                            <asp:TextBox ID="txt_TripType" runat="server" Text='<%# Eval("Tripcode") %>' ReadOnly="true" Width="50"></asp:TextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Width="6" Settings-AllowSort="False" FixedStyle="Left"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Statuscode" Caption="Status" Settings-AllowSort="False" Width="112">
                        <DataItemTemplate>
                            <dxe:ASPxComboBox ID="cbb_statuscode" runat="server" Value='<%# Eval("Statuscode") %>' Width="100">
                                <Items>
                                    <dxe:ListEditItem Value="P" Text="Pending" />
                                    <dxe:ListEditItem Value="S" Text="Starting" />
                                    <dxe:ListEditItem Value="C" Text="Complete" />
                                    <dxe:ListEditItem Value="X" Text="Cancel" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FromDate" Caption="FromDate" Settings-AllowSort="False" Width="112">
                        <DataItemTemplate>
                            <dxe:ASPxDateEdit ID="txt_FromDate" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Value='<%# Eval("FromDate") %>' CalendarProperties-ShowClearButton="false" Width="100"></dxe:ASPxDateEdit>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FromTime" Caption="FromTime" Settings-AllowSort="False" Width="70">
                        <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_FromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="50">
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DriverCode" Caption="Driver" Settings-AllowSort="False" Width="133">
                        <DataItemTemplate>
                            <asp:TextBox ID="txt_DriverCode_old" runat="server" Text='<%# Bind("DriverCode") %>' Visible="false"></asp:TextBox>
                            <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="120">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupCTM_Driver,btn_DriverCode,null,null);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer" Settings-AllowSort="False" Width="133">
                        <DataItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="120">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupCTM_MasterData,btn_ChessisCode,null,'Chessis');
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ToParkingLot" Caption="ToParkingLot/Address" Settings-AllowSort="False" Width="293">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_parkingLot" ClientInstanceName="txt_parkingLot" runat="server" Text='<%# Bind("ToParkingLot") %>' AutoPostBack="False" Width="120">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupParkingLot,txt_parkingLot,txt_toAddress,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxMemo ID="txt_toAddress" ClientInstanceName="txt_toAddress" runat="server" Width="150" Height="16" Text='<%# Bind("ToCode") %>'>
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Incentive1" Caption="Trip($)" Settings-AllowSort="False" Width="113">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100" ID="txt_Incentive1" Height="21px" Value='<%# Bind("Incentive1")%>' DecimalPlaces="2" Increment="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Incentive2" Caption="Overtime($)" Settings-AllowSort="False" Width="113">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100" ID="txt_Incentive2" Height="21px" Value='<%# Bind("Incentive2")%>' DecimalPlaces="2" Increment="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Incentive3" Caption="Standby($)" Settings-AllowSort="False" Width="113">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100" ID="txt_Incentive3" Height="21px" Value='<%# Bind("Incentive3")%>' DecimalPlaces="2" Increment="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Incentive4" Caption="PSA($)" Settings-AllowSort="False" Width="113">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100" ID="txt_Incentive4" Height="21px" Value='<%# Bind("Incentive4")%>' DecimalPlaces="2" Increment="0">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                </Columns>
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
    </form>
</body>
</html>
