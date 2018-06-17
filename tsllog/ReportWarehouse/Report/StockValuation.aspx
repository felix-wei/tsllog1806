<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockValuation.aspx.cs" Inherits="ReportWarehouse_Report_StockValuation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
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
                     
                </tr>
                <tr>
                     <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_CustId,txt_CustName);
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="230" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                     <td>LotNo
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
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
                   
                </tr>
                <tr>
                    <td>Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_Date" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
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
                        <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="Product" VisibleIndex="1" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                         <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="2" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="HsCode" FieldName="HsCode" VisibleIndex="3" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Class" FieldName="ProductClass" VisibleIndex="4" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WareHouseId" VisibleIndex="4" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="DoDate" VisibleIndex="5" Width="80">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Party" FieldName="PartyName" VisibleIndex="6" Width="300">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="description" FieldName="Des1" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="PKG" FieldName="Pkg" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="UNIT" FieldName="Unit" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ML" FieldName="Att1" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ACL%" FieldName="Att2" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CO" FieldName="Att3" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="NRF/REF" FieldName="Att4" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="GBX" FieldName="Att5" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="DECODE" FieldName="Att6" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty1" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Total Amt" FieldName="TotalPrice"  VisibleIndex="7" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFooter="true" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0}" />
                         <dxwgv:ASPxSummaryItem  FieldName="TotalPrice" SummaryType="Sum" DisplayFormat="{0:00.00}" Tag="7" />
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
