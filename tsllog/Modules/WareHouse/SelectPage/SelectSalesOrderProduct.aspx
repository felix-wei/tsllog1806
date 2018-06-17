<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectSalesOrderProduct.aspx.cs" Inherits="WareHouse_SelectPage_SelectSalesOrderProduct" %>

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
                alert("Success! DO OUT No is " + v);
                grid_SKULine.Refresh();
                parent.AfterPopubMultiInv(v);
            }
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_SKULine.Refresh();
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
            <wilson:DataSource ID="dsIssueDet" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhTransDet"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
                KeyMember="Id" FilterExpression="Type='Attribute'" />
    <div>

               <table>
                <tr>
                    <td>
                        SoNo
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lbl_SoNo" Width="100" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Create Invoice" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid_SKULine.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_AddSKULine" Width="150" runat="server" Text="Add Product" AutoPostBack="false" UseSubmitBehavior="false"
                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&(SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'>
                            <ClientSideEvents Click="function(s,e) {
                                                       grid_SKULine.AddNewRow();
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        <dxwgv:ASPxGridView ID="grid_SKULine" ClientInstanceName="grid_SKULine" runat="server" OnRowInserting="grid_SKULine_RowInserting" OnBeforePerformDataSelect="grid_SKULine_BeforePerformDataSelect"
            OnRowDeleting="grid_SKULine_RowDeleting" OnRowUpdating="grid_SKULine_RowUpdating"  OnCustomDataCallback="grid_SKULine_CustomDataCallback"
            KeyFieldName="Id" Width="1750" AutoGenerateColumns="False" Styles-Cell-Paddings-Padding="1" Styles-EditForm-Paddings-Padding="0"
            DataSourceID="dsIssueDet" OnInit="grid_SKULine_Init" OnInitNewRow="grid_SKULine_InitNewRow" OnRowInserted="grid_SKULine_RowInserted" OnRowUpdated="grid_SKULine_RowUpdated">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="Inline" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <SettingsBehavior  ConfirmDelete="true"/>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="80">
                    <EditButton Text="Edit" Visible="true"></EditButton>
                    <DeleteButton Text="Delete" Visible="true"></DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn
                    Caption="LotNo" FieldName="LotNo" VisibleIndex="1" Width="80">
                    <EditItemTemplate>
                        <dxe:ASPxTextBox ID="txt_LotNo" runat="server" ClientInstanceName="txt_LotNo" Text='<%# Bind("LotNo") %>' Width="80px">
                        </dxe:ASPxTextBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Product Code" FieldName="ProductCode" VisibleIndex="2" Width="80">
                    <EditItemTemplate>
                        <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product" runat="server" Value='<%# Bind("ProductCode") %>' Width="80">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupProduct_so(txt_SKULine_Product,txt_Des,txt_Uom1,null,null,null,null,null,null,cb_Att1,cb_Att2,null,null,null,null,null,null,null,null);
                                                                           }" />
                        </dxe:ASPxButtonEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="3" Width="110px">
                    <EditItemTemplate>
                        <dxe:ASPxTextBox ID="txt_Des" runat="server" ClientInstanceName="txt_Des" Text='<%# Bind("Des1") %>' Width="120px">
                        </dxe:ASPxTextBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="LocationCode" VisibleIndex="2" Width="40">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="cmb_Location" ClientInstanceName="cmb_Location" runat="server" DataSourceID="dsRefLocation" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Value='<%# Bind("LocationCode") %>' TextField="Code" ValueField="Code" Width="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="LocationCode" VisibleIndex="3" Width="80px" Visible="false">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server"
                            Width="80px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse" Value='<%# Bind("LocationCode") %>'
                            ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Uom1" VisibleIndex="8" Width="60">
                    <EditItemTemplate>
                        <dxe:ASPxButtonEdit runat="server" ID="txt_Uom1" Width="60" ClientInstanceName="txt_Uom1" Text='<%# Bind("Uom1") %>'>
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom1,2);
                                                                            }" />
                        </dxe:ASPxButtonEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" CellStyle-HorizontalAlign="Left" VisibleIndex="10" Width="100">
                    <EditItemTemplate>
                        <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Bind("Currency") %>' runat="server" Width="60px" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_ExRate);
                                                                    }" />
                        </dxe:ASPxButtonEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Exchange Rate" FieldName="ExRate" CellStyle-HorizontalAlign="Left" VisibleIndex="11" Width="50">
                    <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                    </PropertiesSpinEdit>
                    <EditItemTemplate>
                        <dxe:ASPxSpinEdit ID="spin_ExRate" runat="server" Width="60" ClientInstanceName="spin_ExRate" Value='<%# Bind("ExRate") %>' Increment="1" SpinButtons-ShowIncrementButtons="false" DecimalPlaces="6" DisplayFormatString="0.000000">
                        </dxe:ASPxSpinEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataSpinEditColumn>

                <dxwgv:GridViewDataComboBoxColumn Caption="Length" FieldName="Att1" VisibleIndex="5" Width="60">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="cb_Att1" ClientInstanceName="cb_Att1" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att1") %>' DropDownStyle="DropDown">
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Diameter" FieldName="Att2" VisibleIndex="4" Width="50">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="cb_Att2" ClientInstanceName="cb_Att2" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att2") %>' DropDownStyle="DropDown">
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataComboBoxColumn>

                <dxwgv:GridViewDataDateColumn Caption="ExpiryDate" FieldName="ExpiredDate" VisibleIndex="6" Width="80">
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="txt_ExpiredDate" runat="server" Width="100" EditFormat="Custom" Value='<%# Bind("ExpiredDate") %>' EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty1" VisibleIndex="7" Width="40">
                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer" Width="40" />
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" CellStyle-HorizontalAlign="Left" VisibleIndex="9" Width="50">
                    <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                    </PropertiesSpinEdit>
                    <EditItemTemplate>
                        <dxe:ASPxSpinEdit ID="spin_Price" runat="server" Width="60" ClientInstanceName="spin_Price" Value='<%# Bind("Price") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" DecimalPlaces="2" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn Caption="Amount" VisibleIndex="13" Width="10">
                    <DataItemTemplate>
                        <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Qty1"), 0) * SafeValue.SafeDecimal(Eval("Price"), 0), 2) %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Qty1"), 0) * SafeValue.SafeDecimal(Eval("Price"), 0), 2) %>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="GST Type" FieldName="GstType" VisibleIndex="14" Width="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Gst Amount" FieldName="GstAmt" VisibleIndex="15" Width="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Total" FieldName="DocAmt" VisibleIndex="15" Width="10">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFooter="true" />
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="ProductCode" SummaryType="Count" DisplayFormat="{0:0}" />
                <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0:0}" />
                <dxwgv:ASPxSummaryItem FieldName="DocAmt" SummaryType="Sum" DisplayFormat="{0:00.00}" />
            </TotalSummary>
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
