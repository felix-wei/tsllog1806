<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SkuInventory.aspx.cs" Inherits="WareHouse_SelectPage_SkuInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
         <script type="text/javascript">
             function $(s) {
                 return document.getElementById(s) ? document.getElementById(s) : s;
             }
             function keydown(e) {
                 if (e.keyCode == 27) { parent.ClosePopupCtr(); }
             }
             document.onkeydown = keydown;
    </script>

    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
     <form id="form1" runat="server">
    <div>
       <table style="display:none">
                <tr>
                     <td>
                          <dxe:ASPxButton ID="btnSelect" runat="server" Text="Invert Select" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                    </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
        <table>
            <tr>
                <td><dxe:ASPxLabel runat="server" ID="lbl_Name" Text="SKU:"></dxe:ASPxLabel></td>
                <td><dxe:ASPxLabel runat="server" ID="lbl_Code" Text=""></dxe:ASPxLabel>/<dxe:ASPxLabel runat="server" ID="lbl_ProductName" Text=""></dxe:ASPxLabel></td>
            </tr>
        </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" AutoGenerateColumns="False" Width="100%">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Product Code" FieldName="ProductCode" Width="120" VisibleIndex="1"  Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="2" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Warehouse" FieldName="WareHouseId" VisibleIndex="2" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="2" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PO No" FieldName="PoNo" VisibleIndex="2" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="GRN No" FieldName="DoNo" VisibleIndex="2" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="GRN Date" FieldName="DoDate" VisibleIndex="2" Width="50">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2" Width="300"  Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="On Hand" FieldName="HandQty" VisibleIndex="2" Width="40">
<%--                        <DataItemTemplate>
                            <%# SafeValue.SafeInt(Eval("InQty"),0)-SafeValue.SafeInt(Eval("OutQty"),0) %>
                        </DataItemTemplate>--%>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="IncomingQty" FieldName="IncomingQty" VisibleIndex="2" Width="40" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PO Qty" FieldName="PoQty" VisibleIndex="2" Width="40" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SO Qty" FieldName="SoQty" VisibleIndex="2" Width="40" Visible="false">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="Blocked" FieldName="WipQtyIn" VisibleIndex="2" Width="50" Visible="false">
                    </dxwgv:GridViewDataTextColumn>                   
                    <dxwgv:GridViewDataTextColumn Caption="Available"  VisibleIndex="4" Width="50" Visible="false" >
                        <DataItemTemplate>
                            <%# SafeValue.SafeInt(Eval("HandQty"),0)+SafeValue.SafeInt(Eval("WipQtyIn"),0)-SafeValue.SafeInt(Eval("WipQty"),0) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="4" Width="40" Visible="false">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="5" Width="40" Visible="false">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="ML" FieldName="Att1" ReadOnly="true" VisibleIndex="6" Width="60" Visible="false">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="ACL%" FieldName="Att2" ReadOnly="true" VisibleIndex="6" Width="60" Visible="false">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="CO" FieldName="Att3" ReadOnly="true" VisibleIndex="6" Width="60" Visible="false">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="NRF/REF" FieldName="Att4" ReadOnly="true" VisibleIndex="6" Width="60" Visible="false">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="GBX" FieldName="Att5" ReadOnly="true" VisibleIndex="6" Width="60" Visible="false">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="DECODED" FieldName="Att6" ReadOnly="true" VisibleIndex="6" Width="60" Visible="false">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PKG UOM" FieldName="Uom2" VisibleIndex="10" Width="50" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" VisibleIndex="11" Width="50" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
