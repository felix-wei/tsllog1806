<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateDeliveryOrder.aspx.cs" Inherits="PagesContTrucking_SelectPage_CreateDeliveryOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <style type="text/css">
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
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
            KeyMember="Id" FilterExpression="CodeType='2'" />
        <div>
            <table>
                <tr>
                    <td>Marking
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Marking" runat="server" Width="100px"></dxe:ASPxTextBox>
                    </td>
                    <td>JobNo
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_JobNo" runat="server" Width="100px"></dxe:ASPxTextBox>
                    </td>
                    <td>Cont No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Width="100px"></dxe:ASPxTextBox>
                    </td>
                    <td>Hbl No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HblNo" runat="server" Width="100px"></dxe:ASPxTextBox>
                    </td>

                    <td>
                        <dxe:ASPxButton ID="btn_Retrieve" runat="server" Text="Retrieve" Width="80" AutoPostBack="False" OnClick="btn_Retrieve_Click"
                            UseSubmitBehavior="False">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Width="80" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                    </td>
                    <td>

                    <dxe:ASPxButton ID="ASPxButton10" Width="60" runat="server" Text="OK"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid_wh.GetValuesOnCustomCallback('OK',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_wh" ClientInstanceName="grid_wh" runat="server"
                KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" OnCustomDataCallback="grid_wh_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
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
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" VisibleIndex="0" Width="150" Visible="false">
                        <DataItemTemplate>
                            <dxe:ASPxLabel ID="lbl_JobNo" runat="server" Text='<%# Eval("JobNo") %>' Width="80"></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No/BL/Container" VisibleIndex="0" Width="180" SortIndex="1" SortOrder="Descending">
                        <DataItemTemplate>
                            <table style="width: 180px;">
                                <tr>
                                    <td width="120px">Lot No</td>
                                    
                                    <td><%# Eval("BookingNo") %></td>
                                </tr>
                                <tr>
                                    <td>BL/Bkg No</td>
                                    
                                    <td><%# Eval("HblNo") %></td>
                                </tr>
                                <tr>
                                    <td>Cont No</td>
                                    
                                    <td><%# Eval("ContNo") %></td>
                                </tr>
                                <tr>
                                    <td>Type</td>
                                    
                                    <td><%# Eval("OpsType") %></td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Booking Info" VisibleIndex="2" Width="150">
                        <DataItemTemplate>
                            <table style="width: 120px">
                                <tr>
                                    <td>Qty</td>
                                    
                                    <td><%# Eval("Qty") %></td>
                                </tr>

                                <tr>
                                    <td>Weight</td>
                                    
                                    <td><%# Eval("Weight") %></td>
                                </tr>
                                 <tr>
                                    <td>Volume</td>
                                    
                                    <td><%# Eval("Volume") %></td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Actual Info" VisibleIndex="2" Width="100">
                        <DataItemTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>Qty</td>
                                    <td><%# Eval("QtyOrig") %></td>
                                    <td><%# Eval("PackTypeOrig") %></td>
                                    <td>SKU Code</td>
                                    <td colspan="2"><%# Eval("SkuCode") %></td>
                                </tr>
                                <tr>
                                    <td>Weight</td>
                                    <td colspan="2"><%# Eval("WeightOrig") %></td>
                                    <td>Qty</td>
                                    <td><%# Eval("PackQty") %></td>
                                    <td><%# Eval("PackUom") %></td>
                                </tr>
                                <tr>
                                    <td>Volume</td>
                                    <td colspan="2"><%# Eval("VolumeOrig") %></td>
                                    <td>Location</td>
                                    <td><%# Eval("Location") %></td>
                                </tr>
                                <tr>
                                    <td>Remark</td>
                                    <td colspan="4">
                                        <%# Eval("Desc1") %>
                                    </td>
                                </tr>
                                <tr style="display:none">
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
                    <dxwgv:GridViewDataTextColumn Caption="Balance Info"  FieldName="Qty" VisibleIndex="2" Width="160">
                        <DataItemTemplate>
                            <table style="width: 160px">
                                <tr>
                                    <td>Qty</td>

                                    <td>
                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                            ID="spin_Qty" Height="21px" Value='<%# BalanceQty(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("JobNo"),Eval("Location"))  %>' DecimalPlaces="3" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    </tr>
                                <tr>
                                    <td width="160px">SKU Qty</td>
                                    <td>
                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                            ID="spin_WholeQty" Height="21px" Value='<%# BalanceSkuQty(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("JobNo"),Eval("Location")) %>' DecimalPlaces="3" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Weight</td>

                                    <td>
                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                            ID="spin_Weight" Height="21px" Value='<%# BalanceWeight(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("JobNo"),Eval("Location"))  %>' DecimalPlaces="3" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                     </tr>
                                <tr>
                                    <td>Volume</td>

                                    <td>
                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                            ID="spin_Volume" Height="21px" Value='<%# BalanceVolume(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("JobNo"),Eval("Location"))  %>' DecimalPlaces="3" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td colspan="4">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>L</td>
                                                <td>
                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                        ID="spin_LengthPack" Height="21px" Value='<%# Eval("LengthPack")%>' DecimalPlaces="3" Increment="0">
                                                        <SpinButtons ShowIncrementButtons="false" />
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td>×</td>
                                                <td>W</td>
                                                <td>
                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                        ID="spin_WidthPack" Height="21px" Value='<%# Eval("WidthPack")%>' DecimalPlaces="3" Increment="0">
                                                        <SpinButtons ShowIncrementButtons="false" />
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td>×</td>
                                                <td>H</td>
                                                <td>
                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                        ID="spin_HeightPack" Height="21px" Value='<%# Eval("HeightPack")%>' DecimalPlaces="3" Increment="0">
                                                        <SpinButtons ShowIncrementButtons="false" />
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Marking1" VisibleIndex="2" Width="180" Visible="true">
                        <DataItemTemplate>
                            <dxe:ASPxLabel ID="memo_SkuCode" ClientInstanceName="memo_SkuCode" Text='<%# Bind("Marking1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Marking2" VisibleIndex="2" Width="180" Visible="true">
                        <DataItemTemplate>
                            <dxe:ASPxLabel ID="memo_SkuCode" ClientInstanceName="memo_SkuCode" Text='<%# Bind("Marking2") %>' Rows="6" runat="server" Width="180"></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                        <DataItemTemplate>
                            <dxe:ASPxLabel ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Bind("Remark1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>Landing</td>
                                    
                                    <td>
                                        <%# Eval("LandStatus") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>DG Cargo</td>
                                    
                                    <td>
                                        <%# Eval("DgClass") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Damage</td>
                                    
                                    <td>
                                        <%# Eval("DamagedStatus") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>DMG Remark</td>
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
                            </div>
                            <div class='<%# FilePath(SafeValue.SafeInt(Eval("Id"),0))!=""?"show":"hide" %>' style="min-width: 70px;">
                                <a href='<%# "/Photos/"+FilePath(SafeValue.SafeInt(Eval("Id"),0)) %>' target="_blank">View</a>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
