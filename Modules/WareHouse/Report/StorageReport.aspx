<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageReport.aspx.cs" Inherits="ReportWarehouse_Report_StorageReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Unbilled Storage Report</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript">
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }
        function PutAmt() {
            var qty = parseFloat(lbl_Qty.GetText());
            var price = parseFloat(spin_Surcharge.GetText());
            var amt = FormatNumber(qty * price, 2);

            spin_SurchageAmt.SetNumber(amt);
        }
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <div>
            <table>
                <tr>
                     <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustName" ClientInstanceName="txt_CustName" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupParty(null,txt_CustName,null,null,null,null,null,null,'C');
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>Cut Off Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_CutOffDate" runat="server" Width="120px" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                     <td>
                          <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                    <dxe:ASPxButton ID="btn_CreateInv" Enabled="false" runat="server" Text="Create Inv" Width="120" AutoPostBack="False" OnClick="btn_CreateInv_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_CreateGstInv" Enabled="false" runat="server" Text="Create GST Inv" Width="120" AutoPostBack="False" OnClick="btn_CreateGstInv_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid"
                     AutoGenerateColumns="False" Width="100%">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>
                          <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Product" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                           <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Width="10"  Text='<%# Eval("Product") %>'>
                            </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="GRN No" FieldName="DoNo" VisibleIndex="1" Width="80">
                             <DataItemTemplate>
                                 <dxe:ASPxLabel ID="lbl_DoNo" runat="server" Text='<%# Eval("DoNo") %>'></dxe:ASPxLabel>
                             </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Do Date" FieldName="DoDate" VisibleIndex="1" Width="80">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="SKU No" FieldName="Product" VisibleIndex="1" Width="80">
                             <DataItemTemplate>
                                 <dxe:ASPxLabel ID="lbl_Product" runat="server" Text='<%# Eval("Product") %>'></dxe:ASPxLabel>
                             </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="2" Width="150">
                            <DataItemTemplate>
                                 <dxe:ASPxLabel ID="lbl_Description" runat="server" Text='<%# Eval("Des1") %>'></dxe:ASPxLabel>
                             </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Party" FieldName="PartyName" VisibleIndex="2" Width="150">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_PartyId" runat="server" Text='<%# Eval("PartyName") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                         <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="4" Width="80">
                              <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_LotNo" runat="server" Text='<%# Eval("LotNo") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Start Date" FieldName="StartDate" VisibleIndex="5" Width="80">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="5" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Qty" ClientInstanceName="lbl_Qty" runat="server" Text='<%# Eval("Qty") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChargeCode" VisibleIndex="5" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_ChargeCode" runat="server" Text='<%# Eval("ChargeCode") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxSpinEdit id="spin_Price" ClientInstanceName="spin_Price" Value='<%# Eval("Price") %>' runat="server" Width="60" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Days" FieldName="Days" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Days" runat="server" Text='<%# Eval("Days") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Total Amt" FieldName="TotalAmt" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Gst Amt" FieldName="GstAmt" VisibleIndex="6" Width="40">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Surcharge" FieldName="Surcharge" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxSpinEdit ID="spin_Surcharge" ClientInstanceName="spin_Surcharge" Value='<%# Eval("Surcharge") %>' runat="server" Width="60" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false">
                                    <ClientSideEvents NumberChanged="function(s, e) {
                                                                 PutAmt();               

                                        }" />
                                </dxe:ASPxSpinEdit>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>

                    </Columns>
                    <Settings ShowFooter="true" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="DoNo" SummaryType="Count" DisplayFormat="{0:#,##0}" />
                        <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
                        <dxwgv:ASPxSummaryItem FieldName="Price" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                        <dxwgv:ASPxSummaryItem FieldName="TotalAmt" SummaryType="Sum" DisplayFormat="{0}" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                </dxwgv:ASPxGridViewExporter>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="600" EnableViewState="False">
                </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
