<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickStockProduct.aspx.cs" Inherits="WareHouse_SelectPage_PickStockProduct" %>

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
                 parent.AfterPopubMultiInv();
             }
             if (v == "Fail") {
                 alert("No Balance,please try again!!");
                 spin_AvaibleQty.focus();
                 //spin_AvaibleQty.

             }
             if (v != null && v.indexOf("Fail") > -1) {
                 if (confirm('Had Picked this SKU!Confirm RePick it?')) {
                     grid.GetValuesOnCustomCallback('OK', OnCallback);
                 }
             }
             
             else if (v != null && v.length > 0)
                 alert(v)
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
       <table>
                <tr>
                    <td style="display:none">
                        SKU
                    </td>
                    <td style="display:none">
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server" v>
                        </dxe:ASPxTextBox>
                    </td>
                     <td style="display:none">
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
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
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('Vali',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                           <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Width="10"  Text='<%# Eval("Id") %>'>
                            </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="ProductCode" Width="60" VisibleIndex="1" >
                         <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_Product" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("ProductCode") %>' Width="60">
                                </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="2">
                        <DataItemTemplate>
                            <%# Eval("LotNo") %>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_LotNo" Border-BorderWidth="0" ReadOnly="true" runat="server"
                                    Text='<%# Eval("LotNo") %>'>
                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Date Time" FieldName="DoDate" VisibleIndex="2" Width="50">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="2" Width="200" SortIndex="0" SortOrder="Ascending">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="LocationCode" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxLabel id="lbl_Location" ClientInstanceName="lbl_Location" runat="server" Width="40" Text='<%# Eval("LocationCode") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="OnHand" FieldName="AvaibleQty" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxLabel id="spin_AvaibleQty" ClientInstanceName="spin_AvaibleQty" runat="server" Width="40" Text='<%# Eval("AvaibleQty") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="To Pick" FieldName="Qty5" VisibleIndex="2" Width="40">
                          <DataItemTemplate>
                            <dxe:ASPxLabel id="spin_Picked" ClientInstanceName="spin_Picked" runat="server" Width="40" Text='<%# Eval("Qty5") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="PendingPickQty" FieldName="PendingPickQty" VisibleIndex="2" Width="40">
                          <DataItemTemplate>
                            <dxe:ASPxLabel id="spin_PengingPickQty" ClientInstanceName="spin_PengingPickQty" runat="server" Width="40" Text='<%# Eval("PendingPickQty") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Picking" FieldName="Qty" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty" runat="server" Width="40"  Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2" Width="100" >
                    </dxwgv:GridViewDataTextColumn>                   
                    <dxwgv:GridViewDataTextColumn Caption=" Price" FieldName="Price"  VisibleIndex="2" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxLabel ID="spin_Price" ClientInstanceName="spin_Price" runat="server" Width="60" Text='<%# Eval("Price") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Uom1" VisibleIndex="4" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="5" Width="40">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="ML" FieldName="Att1" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="ACL%" FieldName="Att2" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="CO" FieldName="Att3" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="NRF/REF" FieldName="Att4" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="GBX" FieldName="Att5" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="DECODED" FieldName="Att6" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PKG UOM" FieldName="Uom2" VisibleIndex="10" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" VisibleIndex="11" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
