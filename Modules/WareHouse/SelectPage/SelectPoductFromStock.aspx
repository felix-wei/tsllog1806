<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectPoductFromStock.aspx.cs" Inherits="WareHouse_SelectPage_SelectPoductFromStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
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
                    <td>
                        SKU
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        Lot No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                   
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Do Date">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                     <td>
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
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="100%"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="SN" VisibleIndex="0">
                        <DataItemTemplate>
                            <%# Container.ItemIndex +1%>
                        </DataItemTemplate>
                        <EditFormCaptionStyle Wrap="False"></EditFormCaptionStyle>
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" Border-BorderWidth="0" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Id") %>'>
                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                   <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Product" Width="150" VisibleIndex="2">
                        <DataItemTemplate>
                            <dxe:ASPxLabel ID="txt_Product"  runat="server"
                                    Text='<%# Eval("Product") %>' Width="60">
                                </dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="1">
                        <DataItemTemplate>
                            <%# Eval("LotNo") %>
                            <div style="display:none">
                            <dxe:ASPxTextBox ID="txt_LotNo" Border-BorderWidth="0" ReadOnly="true" runat="server"
                                    Text='<%# Eval("LotNo") %>' >
                                </dxe:ASPxTextBox>
                                </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="2" Width="120px">
                        <DataItemTemplate>
                            <dxe:ASPxLabel id="lbl_Description" ClientInstanceName="lbl_Description" runat="server" Width="40" Text='<%# Eval("Des1") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="2" Width="120px">
                        <DataItemTemplate>
                            <dxe:ASPxLabel id="lbl_PalletNo" ClientInstanceName="lbl_PalletNo" runat="server" Width="40" Text='<%# Eval("PalletNo") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="2" Width="120px">
                         <DataItemTemplate>
                            <dxe:ASPxLabel id="lbl_Remark" ClientInstanceName="lbl_Remark" runat="server" Width="40" Text='<%# Eval("Remark") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="In Date" FieldName="DoDate" Width="80" VisibleIndex="2">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxLabel id="lbl_Location" ClientInstanceName="lbl_Location" runat="server" Width="40" Text='<%# Eval("Location") %>'></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="On Hand" FieldName="HandQty" VisibleIndex="2" Width="50">
                         <DataItemTemplate>
                            <dxe:ASPxLabel ID="txt_HandQty"  runat="server"
                                    Text='<%# Eval("HandQty") %>' Width="60">
                                </dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Picked" FieldName="OutQty" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
<%--                    <dxwgv:GridViewDataTextColumn Caption="Reserved" FieldName="ReservedQty" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>--%>
<%--                    <dxwgv:GridViewDataTextColumn Caption="Avaible Qty" FieldName="AvaibleQty" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty1" runat="server" Value='<%# Eval("Qty") %>' Width="40" Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataTextColumn Caption="Bad Qty" FieldName="Qty2" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty2" runat="server" Value='<%# Eval("Qty2") %>' Width="40" Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pallet Qty" FieldName="Qty3" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty3" runat="server" Value='<%# Eval("Qty3") %>' Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Uom1" VisibleIndex="4" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="5" Width="40">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Grade" FieldName="Att1" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Color" FieldName="Att2" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Size" FieldName="Att3" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="Att4" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Clot1" FieldName="Att5" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att" FieldName="Att6" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PKG UOM" FieldName="Uom2" VisibleIndex="10" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" VisibleIndex="11" Width="50">
                    </dxwgv:GridViewDataTextColumn>
<%--                    <dxwgv:GridViewDataTextColumn Caption="Last SO Price" FieldName="Price" VisibleIndex="2" Width="50">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Price" runat="server" Value='<%# Eval("Price") %>' Width="80" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <%--<dxwgv:GridViewDataTextColumn Caption="2Wk Qty" FieldName="InQty_1" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="4Wk Qty" FieldName="InQty_2" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="OT Qty" FieldName="InQty_3" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <%--<dxwgv:GridViewDataTextColumn Caption="SO Confirmed Qty" FieldName="SoBalQty" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>--%>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
