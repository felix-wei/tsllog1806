<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectSetStock.aspx.cs" Inherits="WareHouse_SelectPage_SelectSetStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
          <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript">
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else if (v != null) {
                alert("Success! Sales Order No is " + v);
                grid.Refresh();
                parent.AfterPopubMultiInv(v);
            }
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid.Refresh();
        }
    </script>
        <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefLocation" KeyMember="Id" FilterExpression="Loclevel='Unit'" />
        <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXSalesman" KeyMember="Code" />
    <div>

               <table>
                <tr>
                    <td>
                        Product
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Salesmen</td>
                    <td>
                        <dxe:ASPxTextBox ID="lbl_Salesmen" runat="server" Width="120" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>Set</td>
                    <td>
                         <dxe:ASPxComboBox ID="cmb_Location" ClientInstanceName="cmb_Location" runat="server" DataSourceID="dsRefLocation" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"  TextField="Code" ValueField="Code" Width="80">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                     <td>
                          <dxe:ASPxButton ID="btnSelect" ClientInstanceName="btnSelect" runat="server" Text="Invert Select" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Create Sales Order" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"  Width="100%"
            KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback" 
              >
            <SettingsEditing Mode="Inline" />
            <SettingsPager PageSize="100" Mode="ShowPager">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                        </dxe:ASPxCheckBox>
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_Id" runat="server"  Width="10" Text='<%# Eval("Id") %>'>
                            </dxe:ASPxTextBox>
                        </div>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Product Code" FieldName="Product" Width="120" VisibleIndex="1">
                    <DataItemTemplate>
                        <dxe:ASPxLabel ID="lbl_ProductCode" runat="server" Text='<%# Eval("Product") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="2" Width="300">
                    <DataItemTemplate>
                        <dxe:ASPxLabel ID="lbl_Description" runat="server" Text='<%# Eval("Des1") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" Width="100" VisibleIndex="2">
                     <DataItemTemplate>
                        <dxe:ASPxLabel ID="lbl_LotNo" runat="server" Text='<%# Eval("LotNo") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
               <dxwgv:GridViewDataTextColumn Caption="Salesman" FieldName="Salesman" Width="60" VisibleIndex="2" Visible="false">
                   <DataItemTemplate>
                       <dxe:ASPxComboBox ID="cmb_SalesId" ClientInstanceName="cmb_SalesId" Text='<%# Eval("Salesman") %>' runat="server" DataSourceID="dsSalesman" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" TextField="Code" ValueField="Code" Width="70">
                           <Columns>
                               <dxe:ListBoxColumn FieldName="Code" Width="100%" />
                           </Columns>
                       </dxe:ASPxComboBox>
                   </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="GRN No" FieldName="DoNo" VisibleIndex="2" Width="120">
                    <DataItemTemplate>
                        <dxe:ASPxLabel ID="lbl_DoNo" runat="server" Text='<%# Eval("DoNo") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="GRN Date" FieldName="DoDate" VisibleIndex="2" Width="80">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                
                 <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="2" Width="40" CellStyle-HorizontalAlign="Left">
                    <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty" runat="server" Width="40"  Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="2" Width="40" CellStyle-HorizontalAlign="Left">
                    <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Price" runat="server" Width="40"  Increment="2" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>

                <dxwgv:GridViewDataTextColumn Caption="HandQty" FieldName="HandQty" VisibleIndex="2" Width="40" CellStyle-HorizontalAlign="Left">
                    <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_HandQty" ReadOnly="true" BackColor="Control" Value='<%# Eval("HandQty") %>' runat="server" Width="60"  Increment="2" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Diameter" FieldName="Att1" ReadOnly="true" VisibleIndex="6" Width="60">
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Length" FieldName="Att2" ReadOnly="true" VisibleIndex="6" Width="60">
 
                </dxwgv:GridViewDataComboBoxColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1100" EnableViewState="False">
            </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
