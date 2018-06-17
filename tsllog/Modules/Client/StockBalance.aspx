<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockBalance.aspx.cs" Inherits="Modules_Client_StockBalance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <style type="text/css">
        body {
        font-family:Arial;
        font-size:12px;
        }
</style>
    <script type="text/javascript" src="/Script/pages.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <div>
            <table>
                <tr>
                    <td>SKU
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product" runat="server" Width="120">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                PopupProduct(txt_SKULine_Product,null);
                            }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                     <td>LotNo
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>WareHouse</td>
                    <td>
                         <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server"
                            Width="100px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse"
                            ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid"
                    KeyFieldName="Id" AutoGenerateColumns="False" Width="1400px">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="#" FieldName="No" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Location Code" FieldName="Location" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Permit No" FieldName="PermitNo" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Inv. No" FieldName="PartyInvNo" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="Product" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Case/Pallet" FieldName="PalletNo" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Size" FieldName="Att3" Width="80">
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewBandColumn Caption="Weight">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="Gross" FieldName="GrossWeight" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Nett" FieldName="NettWeight" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:GridViewBandColumn>
                        <dxwgv:GridViewBandColumn Caption="Incoming">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="InDate" Width="80">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Pallet" FieldName="InPallet" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Drum/Bag/Pc" FieldName="InQty" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                                 <dxwgv:GridViewDataTextColumn Caption="Pending" FieldName="InPending" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:GridViewBandColumn>
                        <dxwgv:GridViewBandColumn Caption="Outgoing">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="Date"  Width="80">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy "></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Pallet"  Width="80">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Drum/Bag/Pc" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                                 <dxwgv:GridViewDataTextColumn Caption="Pending" FieldName="OutPending" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:GridViewBandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Supplier DO" FieldName="PoNo" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Bal Qty" FieldName="HandQty" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            Stock Balance Report
                        </TitlePanel>
                    </Templates>
                    <Settings ShowFooter="true" ShowTitlePanel="true" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0}" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid" >
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
