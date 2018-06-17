<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowCargoForTrip.aspx.cs" Inherits="PagesContTrucking_SelectPage_ShowCargoForTrip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script  type="text/javascript">
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
        function dimension_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Dimensionline_'+rowIndex, dimension_inline_callback);
            }, config.timeout);
           
        }
        function dimension_inline_callback(res){
            popubCtrPic.SetHeaderText('Dimension ');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/DimensionForCargo.aspx?id=' + res);
            popubCtrPic.Show();
        }

    </script>
        <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <wilson:DataSource ID="dsWh" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobHouse"
            KeyMember="Id" FilterExpression="1=0" />
        <div>
            <dxwgv:ASPxGridView ID="grid_wh" ClientInstanceName="grid_wh" runat="server" DataSourceID="dsWh" OnInit="grid_wh_Init" OnInitNewRow="grid_wh_InitNewRow"
                OnRowInserting="grid_wh_RowInserting" OnRowUpdating="grid_wh_RowUpdating" OnRowDeleting="grid_wh_RowDeleting" OnRowInserted="grid_wh_RowInserted"
                OnBeforePerformDataSelect="grid_wh_BeforePerformDataSelect" OnCustomDataCallback="grid_wh_CustomDataCallback"
                KeyFieldName="Id" Width="1300px" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="true" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" Visible="false">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true"></DeleteButton>
                    </dx:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="#" Width="150" VisibleIndex="0" FieldName="Id" Visible="false">
                        <DataItemTemplate>
                            <table style="display: <%# SafeValue.SafeString(Eval("CargoType"))=="IN"?"block":"none"%>">
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_CargoCount" ClientInstanceName="cbb_CargoCount" runat="server" Width="50">
                                            <Items>
                                                <dxe:ListEditItem Text="1" Value="1" Selected="true" />
                                                <dxe:ListEditItem Text="2" Value="2" />
                                                <dxe:ListEditItem Text="3" Value="3" />
                                                <dxe:ListEditItem Text="4" Value="4" />
                                                <dxe:ListEditItem Text="5" Value="5" />
                                                <dxe:ListEditItem Text="6" Value="6" />
                                                <dxe:ListEditItem Text="7" Value="7" />
                                                <dxe:ListEditItem Text="8" Value="8" />
                                                <dxe:ListEditItem Text="9" Value="9" />
                                                <dxe:ListEditItem Text="10" Value="10" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>
                                        <input type="button" style="width: 80px" class="button" value="Add Cargo" onclick="copy_cargo_inline(<%# Container.VisibleIndex %>);" />
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="0" Width="180" SortIndex="1">
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
                                <tr>
                                    <td class="lbl">Type</td>

                                    <td><%# Eval("OpsType") %></td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Marking1" VisibleIndex="2" Width="180" Visible="true">
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Marking1" ClientInstanceName="memo_Marking1" Text='<%# Bind("Marking1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Marking2" VisibleIndex="2" Width="180" Visible="true">
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Marking2" ClientInstanceName="memo_Marking2" Text='<%# Bind("Marking2") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Booking Info" VisibleIndex="2" Width="260">
                        <DataItemTemplate>
                            <table style="width: 160px">
                                <tr>
                                    <td class="lbl">Qty</td>

                                    <td><%# Eval("Qty") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Unit</td>

                                    <td><%# Eval("UomCode") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Weight</td>

                                    <td><%# Eval("Weight") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Volume</td>

                                    <td><%# Eval("Volume") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Item Code</td>

                                    <td><%# Eval("BookingItem") %></td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Booking SKU Info" VisibleIndex="2" Width="160">
                        <DataItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td class="lbl">SKU Code</td>

                                    <td><%# Eval("BkgSKuCode") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Qty</td>

                                    <td><%# Eval("BkgSkuQty") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Unit</td>

                                    <td><%# Eval("BkgSkuUnit") %></td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Actual Info" VisibleIndex="2" Width="400">
                        <DataItemTemplate>
                            <table style="width: 280px;">
                                <tr>
                                    <td class="lbl">Qty</td>
                                    <td><%# Eval("QtyOrig") %></td>
                                    <td><%# Eval("PackTypeOrig") %></td>
                                    <td class="lbl1">SKU Code</td>
                                    <td colspan="2"><%# Eval("SkuCode") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl1">Item Code</td>
                                    <td colspan="2"><%# Eval("ActualItem") %></td>
                                    <td class="lbl">Qty</td>
                                    <td><%# Eval("PackQty") %></td>
                                    <td><%# Eval("PackUom") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Weight</td>
                                    <td colspan="2"><%# Eval("WeightOrig") %></td>
                                    <td class="lbl">Volume</td>
                                    <td colspan="2"><%# Eval("VolumeOrig") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Remark</td>
                                    <td colspan="2">
                                        <%# Eval("Desc1") %>
                                    </td>
                                    <td class="lbl">Location</td>
                                    <td><%# Eval("Location") %></td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <input type="button" class="button" value="Dimension" onclick="dimension_inline(<%# Container.VisibleIndex %>);" /></td>
                                                <td>
                                                    <input type="button" class="button" value="Process" onclick="process_inline(<%# Container.VisibleIndex %>);" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td colspan="4">
                                        <table>
                                            <tr>
                                                <td>L</td>
                                                <td><%# Eval("LengthPack") %></td>
                                                <td>×</td>
                                                <td>W</td>
                                                <td><%# Eval("WidthPack") %></td>
                                                <td>×</td>
                                                <td>H</td>
                                                <td><%# Eval("HeightPack") %></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Balance Info" VisibleIndex="2" Width="200">
                        <DataItemTemplate>
                            <table style="width: 160px">
                                <tr>
                                    <td class="lbl">Qty</td>
                                    <td><%# Eval("BalanceQty") %></td>

                                    <td><%# Eval("PackTypeOrig") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Weight</td>

                                    <td colspan="2"><%# Eval("BalanceWeight") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl">Volume</td>

                                    <td colspan="2"><%# Eval("BalanceVolume") %></td>
                                </tr>
                                <tr>
                                    <td class="lbl1">SKU Qty</td>

                                    <td colspan="2"><%# Eval("BalanceSkuQty") %></td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Bind("Remark1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                        </EditItemTemplate>
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
                            <input type="button" class="button" value="Upload" onclick="upload_inline(<%# Container.VisibleIndex %>);" />
                            <br />
                            <br />
                            <div class='<%# FilePath(SafeValue.SafeInt(Eval("Id"),0))!=""?"show":"hide" %>' style="min-width: 70px;">
                                <a href='<%# FilePath(SafeValue.SafeInt(Eval("Id"),0)) %>' target="_blank">View</a>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
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
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
                Width="1050" EnableViewState="False">
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
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                    if(grd_Photo!=null)
                        grd_Photo.Refresh();
                        grid_wh.Refresh();

      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr2" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr2"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="450"
                Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
