<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockCountEdit.aspx.cs" Inherits="Modules_WareHouse_Job_StockCountEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
         font-family:Arial;
         font-size:12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                <table>
            <tr>
                <td>
                    Date
                </td>
                    <td style="padding-left:40px;">
                        <dxe:ASPxDateEdit ID="txt_end" ClientInstanceName="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Add" Width="120px" runat="server" Text="Search"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                        		window.location = 'StockCountEdit.aspx?d=' + txt_end.GetText();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
            </tr>
        </table>
        <div>
            <wilson:DataSource ID="dsStockCount" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.StockCount" KeyMember="Id" />
            <wilson:DataSource ID="dsStockCountDet" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.StockCountDet" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
                KeyMember="Id" FilterExpression="CodeType='2'" />
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsStockCount" OnRowUpdating="grid_RowUpdating"
                KeyFieldName="Id" AutoGenerateColumns="False" Width="100%" OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting"
                OnInit="grid_Init" OnRowDeleting="grid_RowDeleting" Styles-Cell-HorizontalAlign="left">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing Mode="EditForm" />
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="false" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="StockNo" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Party" FieldName="PartyName" VisibleIndex="1">
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Date" ReadOnly="true" FieldName="StockDate" VisibleIndex="1">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WareHouseId" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Create" FieldName="CreatedBy" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <table style="text-align: left; padding: 2px 2px 2px 2px; width: 900px">
                            <tr>
                                <td width="70%"></td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                           grid.UpdateEdit();
                                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_DoNo" runat="server" Text='<%# Bind("StockNo") %>'></dxe:ASPxTextBox>
                                </td>
                                <td>Date</td>
                                <td>
                                   <dxe:ASPxDateEdit ID="txt_Date" runat="server" Value='<%# Bind("StockDate") %>' Width="100%" EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                                    </dxe:ASPxDateEdit>
                                </td>
                                 <td>Warehouse</td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_CustReference" Text='<%# Bind("WareHouseId") %>' ClientInstanceName="txt_CustReference">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Customer</td>
                                <td colspan="3">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" runat="server" Text='<%# Bind("PartyId")%>' Width="80" HorizontalAlign="Left" AutoPostBack="False">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupParty(txt_PartyId,txt_PartyName,null,null,txt_PartyCountry,txt_PartyCity,txt_PostalCode,txt_PartyAdd,'C');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="310px" BackColor="Control" ID="txt_PartyName"
                                                    ReadOnly="true" ClientInstanceName="txt_PartyName" Text='<%# Bind("PartyName")%>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                          
                            <tr>
                                <td rowspan="3">Address</td>
                                <td colspan="3" rowspan="3">
                                    <dxe:ASPxMemo ID="txt_PartyAdd" ClientInstanceName="txt_PartyAdd" Rows="6" runat="server" Width="100%" Text='<%# Bind("PartyAdd") %>'></dxe:ASPxMemo>
                                </td>
                                 <td rowspan="3">Remark</td>
                                <td rowspan="3">
                                    <dxe:ASPxMemo ID="txt_Remark" runat="server" Rows="6"  Value='<%# Bind("Remark") %>' Width="100%">
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            
                        </table>
                        <table>
                            <tr>
                                <td colspan="6">
                                    <hr>
                                    <table>
                                        <tr>
                                            <td style="width: 80px;">Creation</td>
                                            <td style="width: 200px"><%# Eval("CreatedBy")%> @ <%# SafeValue.SafeDateTimeStr( Eval("CreateDateTime"))%></td>
                                            <td style="width: 100px;">Last Updated</td>
                                            <td style="width: 200px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateTimeStr(Eval("UpdateDateTime"))%></td>
                                        </tr>
                                    </table>
                                    <hr>
                                </td>
                            </tr>
                        </table>
                        <div style="padding: 2px 2px 2px 2px">
                            <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="1000px" Height="500px">
                                <TabPages>
                                    <dxtc:TabPage Text="Line">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_Add" runat="server" Text="Add Line" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.AddNewRow();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="WIDTH: 1000px; overflow-y: scroll;">
                                                    <dxwgv:ASPxGridView ID="grid_det" ClientInstanceName="grid_det"
                                                        runat="server" DataSourceID="dsStockCountDet" KeyFieldName="Id" 
                                                        OnBeforePerformDataSelect="grid_det_BeforePerformDataSelect" OnRowUpdating="grid_det_RowUpdating" Styles-Cell-Paddings-Padding="2" Styles-EditForm-Paddings-Padding="2"
                                                        OnRowInserting="grid_det_RowInserting"  OnInitNewRow="grid_det_InitNewRow" OnInit="grid_det_Init" OnRowDeleting="grid_det_RowDeleting"
                                                        Width="1500px" AutoGenerateColumns="False">
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPager Mode="ShowAllRecords" />
                                                        <SettingsBehavior ConfirmDelete="true" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataTextColumn Caption="Id" ReadOnly="true" Visible="false" FieldName="Id" VisibleIndex="9" Width="50">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_DoDet_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_det.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_DoDet_del" runat="server" 
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_det.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_DoDet_edit" runat="server" 
                                                                                    Text="Update" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { grid_det.UpdateEdit(); }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_DoDet_cancle" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_det.CancelEdit(); }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="1" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_SKULine_LotNo" ClientInstanceName="txt_SKULine_LotNo" runat="server" Value='<%# Bind("LotNo") %>' Width="80">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupDoInLotNo(txt_SKULine_LotNo,null);
                                                                           }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="Product" VisibleIndex="2" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product" runat="server" Value='<%# Bind("Product") %>' Width="60">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupProduct(txt_SKULine_Product,txt_Des,txt_Uom1,txt_Uom2,txt_Uom3,null,spin_QtyPack,null,spin_QtyBase,cb_Att1,cb_Att2,cb_Att3,cb_Att4,cb_Att5,cb_Att6,null,null,null,null);
                                                                           }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxTextBox ID="txt_DoInId" runat="server" ClientInstanceName="txt_DoInId" Width="150" Text='<%# Bind("RefNo") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </div>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="3" Width="100">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_Des" runat="server" ClientInstanceName="txt_Des" Width="100" Text='<%# Bind("Description") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="New Qty" FieldName="NewQty" CellStyle-HorizontalAlign="Left" VisibleIndex="3" Width="60">
                                                                <PropertiesSpinEdit NumberType="Integer" Increment="3" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty1" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Float" Increment="3" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Bad Qty" FieldName="Qty2" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Float" Width="40" Increment="3" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Pallet Qty" FieldName="Qty3" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Integer" Width="40" Increment="0" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Gross Weight" FieldName="GrossWeight" VisibleIndex="8" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("GrossWeight") %>' Increment="3" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Nett Weight" FieldName="NettWeight" VisibleIndex="8" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("NettWeight") %>' Increment="3" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" CellStyle-HorizontalAlign="Left" VisibleIndex="4" Width="50">
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="4" Width="120">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="text_Remark" ClientInstanceName="cb_Remark" Width="120" runat="server" Text='<%# Bind("Remark") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="4" Width="120">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="text_PalletNo" ClientInstanceName="cb_Remark" Width="120" runat="server" Text='<%# Bind("PalletNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="UNIT" FieldName="Uom" VisibleIndex="4" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="txt_Uom1" ClientInstanceName="txt_Uom1" Width="60" DataSourceID="dsPackageType" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Uom") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Grade" FieldName="Att1" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att1" ClientInstanceName="cb_Att1" Width="60" runat="server" Text='<%# Bind("Att1") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Color" FieldName="Att2" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att2" ClientInstanceName="cb_Att2" Width="60" runat="server" Text='<%# Bind("Att2") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Size" FieldName="Att3" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att3" ClientInstanceName="cb_Att3" Width="60" runat="server" Text='<%# Bind("Att3") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="Att4" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att4" ClientInstanceName="cb_Att4" Width="60" runat="server" Text='<%# Bind("Att4") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Clot1" FieldName="Att5" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att5" ClientInstanceName="cb_Att5" Width="60" runat="server" Text='<%# Bind("Att5") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="ATT" FieldName="Att6" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att6" ClientInstanceName="cb_Att6" Width="60" runat="server" Text='<%# Bind("Att6") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataDateColumn Caption="ExpiryDate" FieldName="ExpiryDate" VisibleIndex="7" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxDateEdit ID="txt_ExpiredDate" runat="server" Value='<%# Bind("ExpiryDate") %>' Width="100" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Packing" FieldName="Packing" VisibleIndex="8" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_Packing" runat="server" Width="120" ClientInstanceName="txt_Packing" Value='<%# Bind("Packing") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                           
                                                            <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="11" Width="50">
                                                                <DataItemTemplate>
                                                                     <%# Eval("Location") %>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                </TabPages>
                            </dxtc:ASPxPageControl>
                        </div>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
