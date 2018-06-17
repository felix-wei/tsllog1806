<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectProductFromSales.aspx.cs" Inherits="WareHouse_SelectPage_SelectProductFromSales" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
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
              if (v != null && v.length > 0)
                  alert(v)
              else {
                  parent.AfterPopubMultiInv();
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
        <div>
            <table>
                <tr>
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
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Code" Width="100" VisibleIndex="1" SortIndex="0" SortOrder="Ascending">
                        <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_Product" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Code") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            <div style="display:none;">
                            <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Id") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty In Stock" FieldName="Qty" VisibleIndex="2" Width="80">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty" runat="server" Value='<%# Eval("Qty") %>' Width="80" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Last PO Price" FieldName="Price" VisibleIndex="2" Width="80">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Price" runat="server" Value='<%# Eval("Price") %>' Width="80" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>

