<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultipleProduct.aspx.cs" Inherits="WareHouse_SelectPage_MultipleProduct" %>

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
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" Width="50" VisibleIndex="1" >
                         <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_Product" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Code") %>' Width="50">
                                </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" Visible="false" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2" Width="200" SortIndex="0" SortOrder="Ascending">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Att1" VisibleIndex="2" Width="100" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="2" Width="80">
                        <DataItemTemplate>
                            <dxe:ASPxTextBox id="txt_LotNo" runat="server" Width="80"></dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty" runat="server" Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption=" Price" FieldName="Price"  VisibleIndex="2" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Price" runat="server" Width="60" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>

                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="On Hand" FieldName="HandQty" VisibleIndex="2" Width="100" >
                    </dxwgv:GridViewDataTextColumn>
<%--                    <dxwgv:GridViewDataComboBoxColumn Caption="Last Sell" FieldName="LastSell" ReadOnly="true" VisibleIndex="6" Width="50">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="List Sell" FieldName="Price1" ReadOnly="true" VisibleIndex="6" Width="50">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Last Buy" FieldName="LastBuy" ReadOnly="true" VisibleIndex="6" Width="50">

                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="List Buy" FieldName="Price2" ReadOnly="true" VisibleIndex="6" Width="50">

                    </dxwgv:GridViewDataComboBoxColumn>--%>
                      <dxwgv:GridViewDataComboBoxColumn Caption="Grade" FieldName="Att4" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Color" FieldName="Att5" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Size" FieldName="Att6" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="Att7" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="ATT" FieldName="Att8" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="ATT1" FieldName="Att9" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
