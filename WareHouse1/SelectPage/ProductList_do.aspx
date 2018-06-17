<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductList_do.aspx.cs" Inherits="WareHouse_SelectPage_ProductList_do" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
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
                  parent.AfterPopubMultipleDo();
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
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="1000"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="SN" VisibleIndex="0">
                        <DataItemTemplate>
                            <%# Container.ItemIndex +1 %>
                        </DataItemTemplate>
                        <EditFormCaptionStyle Wrap="False"></EditFormCaptionStyle>
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10"  Checked="true">
                            </dxe:ASPxCheckBox>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Loc" Width="100" VisibleIndex="1">
                        <DataItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_Loc" ClientInstanceName="txt_Loc" Text='<%# Eval("Loc")%>'
                                runat="server" Width="100" HorizontalAlign="Left" AutoPostBack="False">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupLo(txt_Loc,null);
                                                                    }" />
                            </dxe:ASPxButtonEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Code" Width="60" VisibleIndex="1" SortIndex="0" SortOrder="Ascending">
                        <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_Product" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Code") %>' Width="60">
                                </dxe:ASPxTextBox>
                            <div style="display:none;">
                            <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Id") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="2" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="P.Qty" FieldName="Qty1" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty1" runat="server" Value='<%# Eval("Qty1") %>' Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="W.Qty" FieldName="Qty2" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty2" runat="server" Value='<%# Eval("Qty2") %>' Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="L.Qty" FieldName="Qty3" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty3" runat="server" Value='<%# Eval("Qty3") %>' Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att1" FieldName="Att1" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att2" FieldName="Att2" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att3" FieldName="Att3" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att4" FieldName="Att4" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att5" FieldName="Att5" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att6" FieldName="Att6" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="10" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="500" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>

