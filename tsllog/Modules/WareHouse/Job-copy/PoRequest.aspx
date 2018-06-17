<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoRequest.aspx.cs" Inherits="WareHouse_Job_PoRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <title>PO Request</title>
</head>
<body>
    <wilson:DataSource ID="dsPoRequest" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhPORequest" KeyMember="Id" FilterExpression="isnull(PoNo, '')= ''" />
    <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhMastData" KeyMember="Id" FilterExpression="Type='Attribute'" />
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>SKU</td>
                    <td>
                        <dxe:ASPxTextBox runat="server" Width="120px" ID="txt_Product"
                            ClientInstanceName="txt_Product">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="Date_From" Width="140" runat="server" ClientInstanceName="FromDate"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                            DisplayFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="Date_To" Width="140" runat="server" ClientInstanceName="ToDate"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                            DisplayFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxCheckBox ID="ckb_ShowAll" runat="server" Text="Show All">
                        </dxe:ASPxCheckBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve" AutoPostBack="false" OnClick="btn_Sch_Click"
                            UseSubmitBehavior="false">
                        </dxe:ASPxButton>

                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                            grid.AddNewRow();
                         }" />
                        </dxe:ASPxButton>
                        <div style="display: none">
                            <dxe:ASPxTextBox runat="server" Width="100px" BackColor="Control" ID="txt_PartyId"
                                ClientInstanceName="txt_PartyId">
                            </dxe:ASPxTextBox>
                        </div>
                    </td>

                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid"
                runat="server" DataSourceID="dsPoRequest" KeyFieldName="Id"
                OnRowUpdating="grid_RowUpdating"
                OnRowInserting="grid_RowInserting" OnInitNewRow="grid_InitNewRow" OnInit="grid_Init" OnRowDeleting="grid_RowDeleting"
                Width="1900" AutoGenerateColumns="False">
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn Width="80">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="Product" VisibleIndex="2" Width="60">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product1" runat="server" Value='<%# Bind("Product") %>' Width="60">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupProduct(txt_SKULine_Product1,txt_Des1,txt_Uom11,txt_Uom21,txt_Uom31,txt_Uom41,spin_QtyPack1,spin_QtyWhole1,spin_QtyBase1,cb_Att1,cb_Att2,cb_Att3,cb_Att4,cb_Att5,cb_Att6,null,null,null,null);
                                                                           }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Date" FieldName="RequestDateTime" VisibleIndex="2" Width="80">
                        <PropertiesDateEdit Width="90" />
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="3" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txt_Des" Width="110" runat="server" ClientInstanceName="txt_Des1" Text='<%# Bind("Des1") %>'>
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty1" VisibleIndex="4" Width="40">
                        <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false" Width="40">
                        </PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                   <%-- <dxwgv:GridViewDataSpinEditColumn Caption="W.Qty" FieldName="Qty2" VisibleIndex="4" Width="60">
                        <PropertiesSpinEdit NumberType="Integer" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false">
                        </PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="L.Qty" FieldName="Qty3" VisibleIndex="4" Width="60">
                        <PropertiesSpinEdit NumberType="Integer" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false">
                        </PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>--%>
                    <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="11" Width="40">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit ID="spin_QtyPack" runat="server" Width="40" ClientInstanceName="spin_QtyPack1" Value='<%# Bind("QtyPackWhole") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="12" Width="40">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit ID="spin_QtyWhole" runat="server" Width="40" ClientInstanceName="spin_QtyWhole1" Value='<%# Bind("QtyWholeLoose") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" CellStyle-HorizontalAlign="Left" VisibleIndex="30" Width="60">
                        <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="60">
                        </PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att1" FieldName="Att1" VisibleIndex="51" Width="60">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cb_Att1" ClientInstanceName="cb_Att1" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att1") %>' DropDownStyle="DropDown">
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att2" FieldName="Att2" VisibleIndex="52" Width="60">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cb_Att2" ClientInstanceName="cb_Att2" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att2") %>' DropDownStyle="DropDown">
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att3" FieldName="Att3" VisibleIndex="53" Width="60">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cb_Att3" ClientInstanceName="cb_Att3" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att3") %>' DropDownStyle="DropDown">
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att4" FieldName="Att4" VisibleIndex="54" Width="60">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cb_Att4" ClientInstanceName="cb_Att4" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att4") %>' DropDownStyle="DropDown">
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att5" FieldName="Att5" VisibleIndex="55" Width="60">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cb_Att5" ClientInstanceName="cb_Att5" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att5") %>' DropDownStyle="DropDown">
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att6" FieldName="Att6" VisibleIndex="56" Width="60">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cb_Att6" ClientInstanceName="cb_Att6" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att6") %>' DropDownStyle="DropDown">
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="STK" FieldName="QtyLooseBase" VisibleIndex="57" Width="40">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase1" Value='<%# Bind("QtyLooseBase") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataSpinEditColumn>

                    <dxwgv:GridViewDataTextColumn Caption="P.UOM" FieldName="Uom1" VisibleIndex="61" Width="50">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit runat="server" ID="txt_Uom1" Width="50" ClientInstanceName="txt_Uom11" Text='<%# Bind("Uom1") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom11,2);
                                                                            }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="W.UOM" FieldName="Uom2" VisibleIndex="62" Width="50">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit runat="server" ID="txt_Uom2" Width="50" ClientInstanceName="txt_Uom21" Text='<%# Bind("Uom2") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom21,2);
                                                                            }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="L.UOM" FieldName="Uom3" VisibleIndex="63" Width="50">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit runat="server" ID="txt_Uom3" Width="50" ClientInstanceName="txt_Uom31" Text='<%# Bind("Uom3") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom31,2);
                                                                            }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="B.UOM" FieldName="Uom4" VisibleIndex="63" Width="50">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit runat="server" ID="txt_Uom4" Width="50" ClientInstanceName="txt_Uom41" Text='<%# Bind("Uom4") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom41,2);
                                                                            }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PO NO" ReadOnly="true" FieldName="PoNo" VisibleIndex="200" Width="70">
                        <DataItemTemplate><%# Eval("PoNo") %></DataItemTemplate>
                        <EditItemTemplate><%# Eval("PoNo") %></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="So No" ReadOnly="true" FieldName="SoNo" VisibleIndex="201" Width="71">
                        <DataItemTemplate><%# Eval("SoNo") %></DataItemTemplate>
                        <EditItemTemplate><%# Eval("SoNo") %></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CreateBy" ReadOnly="true" FieldName="CreateBy" VisibleIndex="202" Width="100">
                        <DataItemTemplate><%# Eval("CreateBy") %>&nbsp;<%# SafeValue.SafeDateStr(Eval("CreateDateTime")) %></DataItemTemplate>
                        <EditItemTemplate><%# Eval("CreateBy") %>&nbsp;<%# SafeValue.SafeDateStr(Eval("CreateDateTime")) %></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UpdateBy" ReadOnly="true" FieldName="UpdateBy" VisibleIndex="204" Width="101">
                        <DataItemTemplate><%# Eval("UpdateBy") %>&nbsp;<%# SafeValue.SafeDateStr(Eval("UpdateDateTime")) %></DataItemTemplate>
                        <EditItemTemplate><%# Eval("UpdateBy") %>&nbsp;<%# SafeValue.SafeDateStr(Eval("UpdateDateTime")) %></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="true" />
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="900" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
      if(isUpload)
	    grd_Photo.Refresh();
}" />
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
