<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ELLShiftingEdi.aspx.cs" Inherits="Modules_Tpt_ELLShiftingEdi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Edi.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../script/StyleSheet.css" rel="stylesheet" />
    <script src="/Script/Tpt/page.js"></script>
    <script src="/PagesContTrucking/script/jquery.js"></script>
    <script src="/PagesContTrucking/script/firebase.js"></script>
    <script src="/PagesContTrucking/script/js_company.js"></script>
    <script src="/PagesContTrucking/script/js_firebase.js"></script>
    <style type="text/css">
        .show {
            display: block;
        }

        .hide {
            display: none;
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
        var ContainerTripIndex = 0;
        function dimension_inline(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Dimensionline_' + rowIndex, dimension_inline_callback);
            }, config.timeout);

        }
        function dimension_inline_callback(res) {
            popubCtrPic.SetHeaderText('Dimension ');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/DimensionForCargo.aspx?id=' + res);
            popubCtrPic.Show();
        }
        function trip_delete_callback(v) {
            if (grid_Trip) {
                grid_Trip.CancelEdit();
                grid_Trip.Refresh();
            }
            var ar = v.split(',');
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTrip" runat="server" ObjectSpace="C2.ManagerEdi.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="lbl_Type" runat="server" Text="Type"></dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Width="165" DropDownStyle="DropDown">
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
                        <td>Driver
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" AutoPostBack="False" Width="165">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,null);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>

                        </td>
                        <td>
                            <dxe:ASPxButton ID="btn_TripAdd" runat="server" Text="Add Trip" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){
                                grid_Trip.AddNewRow();
                                }" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            <div style="margin-left: 2px; margin-top: 10px;">
                

                <dxwgv:ASPxGridView ID="grid_Trip" ClientInstanceName="grid_Trip" runat="server" DataSourceID="dsTrip" Width="950px" KeyFieldName="Id" OnInit="grid_Trip_Init" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_Trip_BeforePerformDataSelect" OnHtmlEditFormCreated="grid_Trip_HtmlEditFormCreated"  OnInitNewRow="grid_Trip_InitNewRow"  OnCustomDataCallback="grid_Trip_CustomDataCallback">
                    <SettingsPager PageSize="100" />
                    <SettingsEditing Mode="EditForm" />
                    <SettingsBehavior ConfirmDelete="true" />
                    <Columns>
                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="10%">
                            <DataItemTemplate>
                                <a href="#" onclick='<%# "grid_Trip.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="12" Width="10%">
                            <DataItemTemplate>
                                <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Trip.GetValuesOnCustomCallback(\"Delete_"+Container.KeyValue+"\",trip_delete_callback);"  %> }'>Delete</a>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataColumn>
                        <%--<dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>--%>
                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="Statuscode" Caption="Satus"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToCode" Caption="Destination"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="TowheadCode" Caption="PrimeMover"></dxwgv:GridViewDataColumn>
                        <%--<dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>--%>

                        <dxwgv:GridViewDataDateColumn VisibleIndex="9" Width="100" FieldName="FromDate" Caption="Date">
                            <DataItemTemplate><%# SafeValue.SafeDate( Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM/yyyy")+"&nbsp;"+Eval("FromTime") %></DataItemTemplate>
                        </dxwgv:GridViewDataDateColumn>
                        <%--<dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="FromTime" Caption="Time">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="ToTime" Caption="Time">
                                                        </dxwgv:GridViewDataColumn>--%>
                        <%--<dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="Incentive1" Caption="Total Incentive">
                                                            <DataItemTemplate><%# SafeValue.SafeDecimal(Eval("Incentive1"))+SafeValue.SafeDecimal(Eval("Incentive2"))+SafeValue.SafeDecimal(Eval("Incentive3"))+SafeValue.SafeDecimal(Eval("Incentive4")) %></DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="Charge1" Caption="Total Surcharge">
                                                            <DataItemTemplate><%# SafeValue.SafeDecimal(Eval("Charge1"))+SafeValue.SafeDecimal(Eval("Charge2"))+SafeValue.SafeDecimal(Eval("Charge3"))+SafeValue.SafeDecimal(Eval("Charge4"))+SafeValue.SafeDecimal(Eval("Charge5"))+SafeValue.SafeDecimal(Eval("Charge6"))+SafeValue.SafeDecimal(Eval("Charge7"))+SafeValue.SafeDecimal(Eval("Charge8"))+SafeValue.SafeDecimal(Eval("Charge9"))+SafeValue.SafeDecimal(Eval("Charge10")) %></DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>--%>
                    </Columns>
                    <Templates>
                        <EditForm>
                            <div style="display: none">
                                <dxe:ASPxLabel ID="lb_tripId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                <dxe:ASPxTextBox ID="dde_Trip_ContId" ClientInstanceName="dde_Trip_ContId" runat="server" Text='<%# Bind("Det1Id") %>'></dxe:ASPxTextBox>
                            </div>
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
                                </tr>
                                <tr>
                                    <td class="lbl">Qty</td>
                                    <td class="ctl">
                                        <dxe:ASPxSpinEdit runat="server" Width="165"
                                            ID="txt_trip_qty" Height="21px" Value='<%# Bind("Qty")%>' NumberType="Integer" Increment="0" DisplayFormatString="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="lbl">PackageType</td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_Trip_PkgsType" ClientInstanceName="txt_Trip_PkgsType" runat="server"
                                            Width="165" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_Trip_PkgsType,2);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
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
                                    <td class="lbl">Weight</td>
                                    <td class="ctl">
                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165"
                                            ID="txt_trip_Wt" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="2" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="lbl">Volume
                                    </td>
                                    <td class="ctl">
                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165"
                                            ID="txt_trip_M3" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="2" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl">From</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="450">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <%--<td class="lbl1">
                                                                        <a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_FromPL" ClientInstanceName="txt_FromPL" runat="server" Text='<%# Bind("FromParkingLot") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>--%>
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
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="450">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <%--<td class="lbl1">
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
                                </tr>
                                <tr>
                                    <td class="lbl">Instruction</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="450">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Input</td>
                                </tr>
                                <%--<tr>

                                                                    <td class="lbl">Container
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_ContNo" ClientInstanceName="txt_ContNo" runat="server" Text='<%# Bind("ContainerNo") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
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
                                                                </tr>--%>



                                <%--<tr>
                                <td>Stage</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_StageCode" runat="server" Value='<%# Bind("StageCode") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Pending" Text="Pending" />
                                            <dxe:ListEditItem Value="Port" Text="Port" />
                                            <dxe:ListEditItem Value="Yard" Text="Yard" />
                                            <dxe:ListEditItem Value="Park1" Text="Park1" />
                                            <dxe:ListEditItem Value="Warehouse" Text="Warehouse" />
                                            <dxe:ListEditItem Value="Park2" Text="Park2" />
                                            <dxe:ListEditItem Value="Park3" Text="Park3" />
                                            <dxe:ListEditItem Value="Completed" Text="Completed" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Stage Status</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_StageStatus" runat="server" Value='<%# Bind("StageStatus") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="" Text="" />
                                            <dxe:ListEditItem Value="DriveTo" Text="DriveTo" />
                                            <dxe:ListEditItem Value="Reach" Text="Reach" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>--%>
                                <tr>
                                    <td class="lbl">DriverRemark</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="450">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                            <span style="float: right">&nbsp
                                               <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                            </span>
                                            <span style='float: right;'>
                                                <%--<dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>--%>
                                                <a onclick="grid_Trip.GetValuesOnCustomCallback('Update',trip_update_cb);"><u>Update</u></a>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </EditForm>
                    </Templates>
                </dxwgv:ASPxGridView>
                 <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
