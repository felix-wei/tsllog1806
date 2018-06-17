<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCargoForReturn.aspx.cs" Inherits="PagesContTrucking_SelectPage_SelectCargoForReturn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <style type="text/css">
        .show_cell {
          display:table-cell;
        }
        .show {
          display:block;
        }
        .hide {
           display:none
        }
    </style>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopub(); }
        }
        document.onkeydown = keydown;

        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }
        function OnCallback(v) {
            if (v == "Success") {
                alert("Action Success!");
                parent.AfterPopub();
            }
            else if (v != null && v.length > 0) {
                alert(v);
                grid.Refresh();
            }

        }
        function dimension_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Dimensionline_'+rowIndex, dimension_inline_callback);
            }, config.timeout);
           
        }
        function dimension_inline_callback(res){
            popubCtr.SetHeaderText('Dimension ');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/DimensionForCargo.aspx?id=' + res);
            popubCtr.Show();
        }
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsWh" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobHouse"
            KeyMember="Id" FilterExpression="1=0" />
            <table style="width: 100%">
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Width="100" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton10" Width="100" runat="server" Text="OK"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                    grid_wh.GetValuesOnCustomCallback('OK',OnCallback);              
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td width="90%"></td>
                </tr>
            </table>
            <div style="overflow-y: auto; width: 1000px">
                <dxwgv:ASPxGridView ID="grid_wh" ClientInstanceName="grid_wh" runat="server" 
                    OnCustomDataCallback="grid_wh_CustomDataCallback"
                    KeyFieldName="Id" Width="1600px" AutoGenerateColumns="False">
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior ConfirmDelete="true" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0"
                            Width="40">
                            <DataItemTemplate>
                                <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                </dxe:ASPxCheckBox>
                                <div style="display: none">
                                    <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                        Text='<%# Eval("Id") %>' Width="150">
                                    </dxe:ASPxTextBox>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="0" Width="180" SortIndex="1">
                            <DataItemTemplate>
                                <%# Eval("CargoStatus") %>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Lot No/BL/Container" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                            <DataItemTemplate>
                                <table style="width: 260px;">
                                    <tr>
                                        <td class="lbl">Lot No</td>

                                        <td><%# Eval("BookingNo") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">BL/Bkg No</td>

                                        <td><%# Eval("HblNo") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Cont No</td>

                                        <td><%# Eval("ContNo") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Trip No</td>

                                        <td><%# Eval("TripIndex") %></td>
                                    </tr>
                                    <%--                                                                    <tr>
                                                                        <td class="lbl">Permit No</td>

                                                                        <td><%# Eval("PermitNo") %></td>
                                                                    </tr>--%>
                                    <tr>
                                        <td class="lbl">Type</td>

                                        <td><%# Eval("OpsType") %></td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Marking1" VisibleIndex="2" Width="180" Visible="true">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Marking2" VisibleIndex="2" Width="180" Visible="true">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Booking Info" VisibleIndex="2" Width="260">
                            <DataItemTemplate>
                                <table style="width: 160px">
                                    <tr>
                                        <td class="lbl">Qty</td>

                                        <td class="ctl"><%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Qty")),1) %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Unit</td>

                                        <td class="ctl"><%# Eval("UomCode") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Weight</td>

                                        <td class="ctl"><%# Eval("Weight") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Volume</td>

                                        <td class="ctl"><%# Eval("Volume") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Item Code</td>

                                        <td class="ctl"><%# Eval("BookingItem") %></td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Booking SKU Info" VisibleIndex="2" Width="260">
                            <DataItemTemplate>
                                <table style="width: 160px">
                                    <tr>
                                        <td class="lbl">SKU Code</td>

                                        <td class="ctl"><%# Eval("BkgSKuCode") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Qty</td>

                                        <td class="ctl"><%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("BkgSkuQty")),1) %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Unit</td>

                                        <td class="ctl"><%# Eval("BkgSkuUnit") %></td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Actual Info" VisibleIndex="2" Width="260">
                            <DataItemTemplate>
                                <table style="width: 160px;">
                                    <tr>
                                        <td class="lbl">Qty</td>
                                        <td class="ctl"><%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("QtyOrig")),1) %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Unit</td>
                                        <td class="ctl"><%# Eval("PackTypeOrig") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Weight</td>
                                        <td class="ctl"><%# Eval("WeightOrig") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Volume</td>
                                        <td class="ctl"><%# Eval("VolumeOrig") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl1">Item Code</td>
                                        <td class="ctl"><%# Eval("ActualItem") %></td>

                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Actual SKU Info" VisibleIndex="2" Width="260">
                            <DataItemTemplate>
                                <table style="width: 160px">
                                    <tr>
                                        <td class="lbl">SKU Code</td>

                                        <td class="ctl"><%# Eval("SkuCode") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Qty</td>

                                        <td class="ctl"><%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("PackQty")),1) %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Unit</td>

                                        <td class="ctl"><%# Eval("PackUom") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Location</td>
                                        <td class="ctl"><%# Eval("Location") %></td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Dimension" VisibleIndex="2" Width="100px">
                            <DataItemTemplate>
                                <table style="width: 100px;">
                                    <tr>
                                        <td class="lbl">Length</td>
                                        <td class="ctl"><%# Eval("LengthPack") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Width</td>
                                        <td class="ctl"><%# Eval("WidthPack") %></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Height</td>
                                        <td class="ctl"><%# Eval("HeightPack") %></td>
                                    </tr>
                                    <tr>

                                        <td>
                                            <input type="button" class="button" value="Dimension" onclick="dimension_inline(<%# Container.VisibleIndex %>);" /></td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="BalQty" VisibleIndex="2" Width="160">
                            <DataItemTemplate>
                                <table style="width: 160px">
                                    <tr>
                                        <td>Qty</td>

                                        <td>
                                            <div style="display: none">
                                                <dxe:ASPxLabel ID="lbl_BalanceQty" runat="server" Text='<%# Eval("BalQty")  %>'></dxe:ASPxLabel>
                                            </div>
                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                                ID="spin_Qty" Height="21px" Value='<%# Eval("BalQty")   %>' DecimalPlaces="3" Increment="0">
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="160px">SKU Qty</td>
                                        <td>
                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                                ID="spin_WholeQty" Height="21px" Value='<%# BalanceSkuQty(Eval("LineId")) %>' DecimalPlaces="3" Increment="0">
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Weight</td>

                                        <td>
                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                                ID="spin_Weight" Height="21px" Value='<%# BalanceWeight(Eval("LineId"))  %>' DecimalPlaces="3" Increment="0">
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Volume</td>

                                        <td>
                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                                ID="spin_Volume" Height="21px" Value='<%# BalanceVolume(Eval("LineId"))  %>' DecimalPlaces="3" Increment="0">
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                            <EditItemTemplate></EditItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="In Date" FieldName="StockDate" VisibleIndex="2" Width="100px">
                            <DataItemTemplate>
                                <%# SafeValue.SafeDateStr(Eval("StockDate")) %>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="StorageType" Caption="Storage Type" Width="60" VisibleIndex="2">
                            <DataItemTemplate>
                                <%# Eval("StorageType") %>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <dxe:ASPxComboBox ID="cbb_StorageType" runat="server" Value='<%# Bind("StorageType") %>' Width="60">
                                    <Items>
                                        <dxe:ListEditItem Text="Daily" Value="Daily"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Weekly" Value="Weekly"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Monthly" Value="Monthly"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Yearly" Value="Yearly"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                    </Items>
                                </dxe:ASPxComboBox>
                            </EditItemTemplate>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Bill Date" FieldName="NextBillDate" VisibleIndex="2" Width="100px">
                            <DataItemTemplate>
                                <%# SafeValue.SafeDateStr(Eval("NextBillDate")) %>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="3" Width="200">
                            <DataItemTemplate>
                                <table style="width: 200px">
                                    <tr>
                                        <td class="lbl">Landing</td>

                                        <td>
                                            <%# Eval("LandStatus") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">DG Cargo</td>

                                        <td>
                                            <%# Eval("DgClass") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">Damage</td>
                                        <td>
                                            <%# Eval("DamagedStatus") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">DMG Remark</td>
                                        <td>
                                            <%# Eval("Remark2") %>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Attachment" VisibleIndex="6" Width="100">
                            <DataItemTemplate>
                                <div style="display: none">
                                    <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                    <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Text='<%# Eval("ContNo") %>'></dxe:ASPxTextBox>
                                </div>
                                <div class='<%# FilePath(SafeValue.SafeInt(Eval("Id"),0))!=""?"show":"hide" %>' style="min-width: 70px;">
                                    <a href='<%# FilePath(SafeValue.SafeInt(Eval("Id"),0)) %>' target="_blank">View</a>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFooter="True" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="Id" SummaryType="Count" DisplayFormat="{0}" />
                        <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
                 <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="800" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                    

      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
