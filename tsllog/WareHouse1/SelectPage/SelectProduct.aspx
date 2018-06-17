<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectProduct.aspx.cs" Inherits="WareHouse_SelectPage_SelectProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Product</title>
            <script type="text/javascript">
                function $(s) {
                    return document.getElementById(s) ? document.getElementById(s) : s;
                }
                function keydown(e) {
                    if (e.keyCode == 27) { parent.ClosePopupCtr(); }
                }
                document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsProductClass" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefProductClass" KeyMember="Id" />
        <div>
            <table style="width:400px">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lbl_SKU" Width="50" runat="server" Text="SKU">
                        </dxe:ASPxLabel>
                        
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="170" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lbl_Type" Width="80" runat="server" Text="ProductClass">
                        </dxe:ASPxLabel>
                        
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbProductClass" Width="120px" TextFormatString="{0}" IncrementalFilteringMode="StartsWith"
                            CallbackPageSize="30" runat="server" DataSourceID="dsProductClass" EnableCallbackMode="True" EnableIncrementalFiltering="true" TextField="Code" ValueField="Code" ValueType="System.String">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                <dxe:ListBoxColumn FieldName="Description" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"
                KeyFieldName="Id"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutSku(new Array("<%# Eval("Code") %>","<%# Eval("Description") %>","<%# Eval("Uom1") %>","<%# Eval("Uom2") %>","<%# Eval("Uom3") %>","<%# Eval("Uom4") %>","<%# Eval("QtyPackingWhole") %>","<%# Eval("QtyWholeLoose") %>","<%# Eval("QtyLooseBase")%>","<%# Eval("Att4")%>","<%# Eval("Att5") %>","<%# Eval("Att6") %>","<%# Eval("Att7") %>","<%# Eval("Att8") %>","<%# Eval("Att9") %>","<%# Eval("Att10") %>","<%# Eval("Att11") %>","<%# Eval("Att12") %>","<%# Eval("Att13") %>"));'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name1" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description1" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                      <dxwgv:GridViewDataComboBoxColumn Caption="Att1" FieldName="Att4"  ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att2" FieldName="Att5" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att3" FieldName="Att6" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Att1" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Att13" VisibleIndex="10">
                    </dxwgv:GridViewDataTextColumn>

                </Columns>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridView ID="ASPxGridView2" runat="server" Width="100%"
                KeyFieldName="Id" Visible="false"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutSku(new Array("<%# Eval("Code") %>","<%# Eval("Description") %>","<%# Eval("Uom1") %>","<%# Eval("Uom2") %>","<%# Eval("Uom3") %>","<%# Eval("Uom4") %>","<%# Eval("QtyPackingWhole") %>","<%# Eval("QtyWholeLoose") %>","<%# Eval("QtyLooseBase")%>","<%# Eval("Att4")%>","<%# Eval("Att5") %>","<%# Eval("Att6") %>","<%# Eval("Att7") %>","<%# Eval("Att8") %>","<%# Eval("Att9") %>","<%# Eval("Att10") %>","<%# Eval("Att11") %>","<%# Eval("Att12") %>","<%# Eval("Att13") %>"));'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name1" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description1" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                      <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Att1" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PKG" FieldName="QtyPackingWhole" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PKG UOM" FieldName="Uom2" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UNIT" FieldName="QtyWholeLoose" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="STK" FieldName="QtyLooseBase" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="STK UOM" FieldName="Uom4" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Att13" VisibleIndex="10">
                    </dxwgv:GridViewDataTextColumn>

                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
