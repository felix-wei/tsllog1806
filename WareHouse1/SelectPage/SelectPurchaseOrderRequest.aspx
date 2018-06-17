<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectPurchaseOrderRequest.aspx.cs" Inherits="WareHouse_SelectPage_SelectPurchaseOrderRequest" %>

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
               if (v != null && v.length > 0)
                   alert(v);
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
            <table>
                <tr>
                    <td>
                        SKU
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="display:none">
                        Lot No
                        <dxe:ASPxTextBox ID="txt_LotNo" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
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
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
         <div style="WIDTH: 950px; overflow-y: scroll;">
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="800"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server"
                                    Text='<%# Eval("Id") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Product" Width="100" VisibleIndex="1" SortIndex="0" SortOrder="Ascending">
                        <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_Product" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Product") %>' Width="100%">
                                </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>                   
                    <dxwgv:GridViewDataTextColumn Caption="Descrition" FieldName="Des1" Width="100" VisibleIndex="1" >
                    </dxwgv:GridViewDataTextColumn> 
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty1" VisibleIndex="2" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty1" runat="server" Value='<%# Eval("Qty1") %>' Width="60" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
<%--                    <dxwgv:GridViewDataTextColumn Caption="W.Qty" FieldName="Qty2" VisibleIndex="2" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty2" runat="server" Value='<%# Eval("Qty2") %>' Width="60" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="L.Qty" FieldName="Qty3" VisibleIndex="2" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty3" runat="server" Value='<%# Eval("Qty3") %>' Width="60" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="2" Width="80">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Price" runat="server" Value='<%# Eval("Price") %>' Width="80" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="ML" FieldName="Att1" VisibleIndex="51" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="ACL%" FieldName="Att2" VisibleIndex="52" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="CO" FieldName="Att3" VisibleIndex="53" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="NRF/REF" FieldName="Att4" VisibleIndex="54" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="GBX" FieldName="Att5" VisibleIndex="55" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="DECODE" FieldName="Att6" VisibleIndex="56" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
<%--                                                            <dxwgv:GridViewDataTextColumn Caption="Att7" FieldName="Att7" VisibleIndex="57" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Att8" FieldName="Att8" VisibleIndex="58" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Att9" FieldName="Att9" VisibleIndex="59" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Att10" FieldName="Att10" VisibleIndex="60" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>--%>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
