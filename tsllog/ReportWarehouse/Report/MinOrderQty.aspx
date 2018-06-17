<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MinOrderQty.aspx.cs" Inherits="ReportWarehouse_Report_MinOrderQty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
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
                    <td>Product
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

                     <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_CustId,null);
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        WareHouse Code
                    </td>
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

                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                     <td>
                          <dxe:ASPxButton ID="btnSelect" runat="server" Text="Invert Select" Width="110" AutoPostBack="False"
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
                    <dxe:ASPxButton ID="btn_CreatePo" Enabled="false" runat="server" Text="Create Po" Width="120" AutoPostBack="False" OnClick="btn_CreatePo_Click">
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
                          <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Code" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                           <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Width="10"  Text='<%# Eval("Code") %>'>
                            </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Code" VisibleIndex="1" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2" Width="150">
                            <DataItemTemplate>
                                 <dxe:ASPxLabel ID="lbl_Description" runat="server" Text='<%# Eval("Description") %>'></dxe:ASPxLabel>
                             </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                         <dxwgv:GridViewDataTextColumn Caption="HsCode" FieldName="HsCode" VisibleIndex="4" Width="80">
                             <DataItemTemplate>
                                 <dxe:ASPxLabel ID="lbl_HsCode" runat="server" Text='<%# Eval("HsCode") %>'></dxe:ASPxLabel>
                             </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Class" FieldName="ProductClass" VisibleIndex="5" Width="50">
                            <DataItemTemplate>
                                 <dxe:ASPxLabel ID="lbl_ProductClass" runat="server" Text='<%# Eval("ProductClass") %>'></dxe:ASPxLabel>
                             </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WareHouseId" VisibleIndex="5" Width="50">
                            <DataItemTemplate>
                                 <dxe:ASPxLabel ID="lbl_WareHouseId" runat="server" Text='<%# Eval("WareHouseId") %>'></dxe:ASPxLabel>
                             </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="PKG" FieldName="pkg" VisibleIndex="6" Width="50" Visible="false">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_PKG" runat="server" Text='<%# Eval("pkg") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="ML" FieldName="Att4" VisibleIndex="6" Width="50" >
                              <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Att4" runat="server" Text='<%# Eval("Att4") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ACL%" FieldName="Att5" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Att5" runat="server" Text='<%# Eval("Att5") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CO" FieldName="Att6" VisibleIndex="6" Width="50" >
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Att6" runat="server" Text='<%# Eval("Att6") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="NRF/REF" FieldName="Att7" VisibleIndex="6" Width="50" >
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Att7" runat="server" Text='<%# Eval("Att7") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="GBX" FieldName="Att8" VisibleIndex="6" Width="50" >
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Att8" runat="server" Text='<%# Eval("Att8") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="DECODED" FieldName="Att9" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Att9" runat="server" Text='<%# Eval("Att9") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Uom1" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Uom1" runat="server" Text='<%# Eval("Uom1") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="HandQty" FieldName="HandQty" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_HandQty" runat="server" Text='<%# Eval("HandQty") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ReorderQty" FieldName="MinOrderQty" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_MinOrderQty" runat="server" Text='<%# Eval("MinOrderQty") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6" Width="50">
                           <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty" runat="server" Width="50"  Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="6" Width="50">
                           <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Price" runat="server" Width="60" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFooter="true" />
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
