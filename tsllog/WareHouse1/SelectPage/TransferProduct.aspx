<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferProduct.aspx.cs" Inherits="WareHouse_SelectPage_TransferProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Product</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
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
                parent.ClosePopupCtr();
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
                    <td>SKU
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Lot No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
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
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared">
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
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Product" Width="60" VisibleIndex="1" SortIndex="0" SortOrder="Ascending">
                        <DataItemTemplate><%# Eval("Product") %>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_DoDetId" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Id") %>' Width="60">
                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="2" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Name" VisibleIndex="2" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="P.Qty" FieldName="Qty1" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="spin_Qty1" runat="server" Value='<%# Eval("Qty1") %>' Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="W.Qty" FieldName="Qty2" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="spin_Qty2" runat="server" Value='<%# Eval("Qty2") %>' Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="L.Qty" FieldName="Qty3" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="spin_Qty3" runat="server" Value='<%# Eval("Qty3") %>' Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="From" FieldName="FromLoc" VisibleIndex="2" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="To" FieldName="ToLoc" VisibleIndex="2" Width="120">
                        <DataItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_ToLoc" ClientInstanceName="txt_ToLoc" runat="server"
                                Text='<%# Eval("ToLoc") %>' Width="120">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupLocation(txt_ToLoc1,null);
                                                                   }" />
                            </dxe:ASPxButtonEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="600" EnableViewState="False">
                </dxpc:ASPxPopupControl>

        </div>
    </form>
</body>
</html>
