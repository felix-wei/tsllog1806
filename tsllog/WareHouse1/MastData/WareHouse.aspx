<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="WareHouse.aspx.cs" Inherits="WareHouse_MastData_WareHouse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>WareHouse</title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefWarehouse" KeyMember="Id" />

            <wilson:DataSource ID="dsRefZone" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefLocation" KeyMember="Id" />
            <wilson:DataSource ID="dsRefStore" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefLocation" KeyMember="Id" />
            <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefLocation" KeyMember="Id" />
            <table width="450">
                <tr>
                    <td>Name
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
                        window.location='WareHouse.aspx?name='+txt_Name.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsRefWarehouse"
                KeyFieldName="Id" AutoGenerateColumns="False"
                Width="100%" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting" OnCustomCallback="grid_CustomCallback" OnRowUpdating="grid_RowUpdating" OnRowInserting="grid_RowInserting" OnHtmlEditFormCreated="grid_HtmlEditFormCreated">
                <SettingsEditing Mode="EditForm" PopupEditFormWidth="750" NewItemRowPosition="Bottom" />
                <SettingsPager PageSize="10" Mode="ShowPager">
                </SettingsPager>
                <Settings ShowFilterRow="false" />
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" Visible="false" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" />
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" />
                    <dxwgv:GridViewDataTextColumn Caption="Address" FieldName="Address" VisibleIndex="4" />
                    <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="Contact" VisibleIndex="5" />
                    <dxwgv:GridViewDataTextColumn Caption="Tel" FieldName="Tel" VisibleIndex="6" />
                    <dxwgv:GridViewDataTextColumn Caption="Fax" FieldName="Fax" VisibleIndex="7" />
                    <dxwgv:GridViewDataTextColumn Caption="StockType" FieldName="StockType" VisibleIndex="7" />
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
                <Templates>
                    <EditForm>
                        <dxtc:ASPxPageControl ID="pageControl" runat="server" Width="100%" EnableCallBacks="true" BackColor="#F0F0F0">
                            <TabPages>
                                <dxtc:TabPage Text="Warehouse" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="display: none">
                                                <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                    BackColor="Control" Text='<%# Eval("Id") %>' Width="170">
                                                </dxe:ASPxTextBox>
                                            </div>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <table width="70%">
                                                    <tr>
                                                        <td>Code：
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Code" ClientInstanceName="txt_Code" runat="server" Width="100%" Value='<%# Bind("Code") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>StockType</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100px" ID="cmb_StockType"
                                                                runat="server" Text='<%# Bind("StockType") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Self" Value="Self" />
                                                                    <dxe:ListEditItem Text="EDI" Value="EDI" />
                                                                    <dxe:ListEditItem Text="Other" Value="Other" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Name： </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="txtName" runat="server" Value='<%# Bind("Name") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Address： </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="txtAddress" runat="server" Rows="3" Value='<%# Bind("Address") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Telephone： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtTel" runat="server" Value='<%# Bind("Tel") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Fax： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtFax" runat="server" Value='<%# Bind("Fax") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Contact： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtContact" runat="server" Value='<%# Bind("Contact") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                       
                                                    </tr>

                                                    <tr>
                                                        <td>Remarks： </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memoRemark" runat="server" Rows="3" Value='<%# Bind("Remark") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="4">
                                                            <hr>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 80px;">Creation</td>
                                                                    <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                    <td style="width: 100px;">Last Updated</td>
                                                                    <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                </tr>
                                                            </table>
                                                            <hr>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>

                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <table width="450">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton3" Width="160" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>' runat="server" Text="Add New Zone" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e){
                                grid_Zone.AddNewRow();
                                }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="grid_Zone" runat="server" ClientInstanceName="grid_Zone" DataSourceID="dsRefZone"
                                                KeyFieldName="Id" AutoGenerateColumns="False"   OnCellEditorInitialize="grid_Zone_CellEditorInitialize"
                                                Width="1000px" OnInit="grid_Zone_Init" OnInitNewRow="grid_Zone_InitNewRow" OnBeforePerformDataSelect="grid_Zone_BeforePerformDataSelect" OnRowDeleting="grid_Zone_RowDeleting" OnCustomCallback="grid_Zone_CustomCallback" OnRowUpdating="grid_Zone_RowUpdating" OnRowInserting="grid_Zone_RowInserting" OnHtmlEditFormCreated="grid_Zone_HtmlEditFormCreated">
                                                <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom" />
                                                <SettingsPager PageSize="10" Mode="ShowPager">
                                                </SettingsPager>
                                                <Settings ShowFilterRow="false" />
                                                <SettingsCustomizationWindow Enabled="True" />
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                                                        <EditButton Visible="true"></EditButton>
                                                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" Width="150">
                                                        <EditItemTemplate>
                                                            <dxe:ASPxTextBox ID="txt_ZoneCode" Width="100%" runat="server" Text='<%# Bind("Code")%>'></dxe:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" />
                                                </Columns>
                                                <Styles Header-HorizontalAlign="Center">
                                                    <Header HorizontalAlign="Center"></Header>
                                                    <Cell HorizontalAlign="Center"></Cell>
                                                </Styles>
                                            </dxwgv:ASPxGridView>
                                            <table width="450">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton2" Width="160" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>' Text="Add New Store" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e){
                                grid_Store.AddNewRow();
                                }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="grid_Store" runat="server" ClientInstanceName="grid_Store" DataSourceID="dsRefStore" OnRowInserting="grid_Store_RowInserting" OnRowUpdating="grid_Store_RowUpdating"
                                                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomCallback="grid_Store_CustomCallback" OnBeforePerformDataSelect="grid_Store_BeforePerformDataSelect"
                                                Width="1000px" OnInit="grid_Store_Init" OnInitNewRow="grid_Store_InitNewRow" OnRowDeleting="grid_Store_RowDeleting">
                                                <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom" />
                                                <SettingsPager PageSize="10" Mode="ShowPager">
                                                </SettingsPager>
                                                <Settings ShowFilterRow="false" />
                                                <SettingsCustomizationWindow Enabled="True" />
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                                                        <EditButton Visible="true"></EditButton>
                                                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" Width="150">
                                                        <EditItemTemplate>
                                                            <dxe:ASPxTextBox ID="txt_StoreCode" Width="100%" runat="server" Text='<%# Bind("Code")%>'></dxe:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" />
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Zone Code" FieldName="ZoneCode" VisibleIndex="5" Width="200">
                                                        <PropertiesComboBox DataSourceID="dsRefZone" ValueType="System.String" TextFormatString="{0}" TextField="Code" EnableIncrementalFiltering="true"
                                                            ValueField="Code">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="Code" Width="200" />
                                                            </Columns>
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Don't be null" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>

                                                </Columns>
                                                <Styles Header-HorizontalAlign="Center">
                                                    <Header HorizontalAlign="Center"></Header>
                                                    <Cell HorizontalAlign="Center"></Cell>
                                                </Styles>

                                            </dxwgv:ASPxGridView>
                                            <table width="450">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton4" Width="160" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'  Text="Add New Location" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e){
                                grid_Location.AddNewRow();
                                }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="grid_Location" runat="server" OnCustomCallback="grid_Location_CustomCallback" ClientInstanceName="grid_Location" DataSourceID="dsRefLocation"
                                                KeyFieldName="Id" AutoGenerateColumns="False" OnRowInserting="grid_Location_RowInserting" OnRowUpdating="grid_Location_RowUpdating"
                                                Width="1000px" OnInit="grid_Location_Init" OnInitNewRow="grid_Location_InitNewRow" OnBeforePerformDataSelect="grid_Location_BeforePerformDataSelect" OnRowDeleting="grid_Location_RowDeleting" OnHtmlEditFormCreated="grid_Location_HtmlEditFormCreated">
                                                <SettingsEditing Mode="EditForm" PopupEditFormWidth="750" NewItemRowPosition="Bottom" />
                                                <SettingsPager PageSize="10" Mode="ShowPager">
                                                </SettingsPager>
                                                <Settings ShowFilterRow="false" />
                                                <SettingsCustomizationWindow Enabled="True" />
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                                                        <EditButton Visible="true"></EditButton>
                                                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" />
                                                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" />
                                                    <dxwgv:GridViewDataTextColumn Caption="Zone Code" FieldName="ZoneCode" VisibleIndex="4" />
                                                    <dxwgv:GridViewDataTextColumn Caption="Store Code" FieldName="StoreCode" VisibleIndex="5" />
                                                    <dxwgv:GridViewDataTextColumn Caption="Length" FieldName="Length" VisibleIndex="6" />
                                                    <dxwgv:GridViewDataTextColumn Caption="Width" FieldName="Width" VisibleIndex="7" />
                                                    <dxwgv:GridViewDataTextColumn Caption="Height" FieldName="Height" VisibleIndex="8" />
                                                </Columns>
                                                <Styles Header-HorizontalAlign="Center">
                                                    <Header HorizontalAlign="Center"></Header>
                                                    <Cell HorizontalAlign="Center"></Cell>
                                                </Styles>
                                                <Templates>
                                                    <EditForm>
                                                        <dxtc:ASPxPageControl ID="pageControl_Location" runat="server" Width="100%" EnableCallBacks="true" BackColor="#F0F0F0">
                                                            <TabPages>
                                                                <dxtc:TabPage Text="Location" Visible="true">
                                                                    <ContentCollection>
                                                                        <dxw:ContentControl>
                                                                            <div style="display:none">
                                                                                <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                                                    BackColor="Control" Text='<%# Eval("Id") %>' Width="170">
                                                                                </dxe:ASPxTextBox>
                                                                            </div>
                                                                            <div style="padding: 4px 4px 3px 4px;">
                                                                                <table width="70%">
                                                                                    <tr>
                                                                                        <td>Code：
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxTextBox ID="txt_Location_Code" runat="server" Width="100%" Text='<%# Bind("Code") %>'>
                                                                                            </dxe:ASPxTextBox>
                                                                                        </td>
                                                                                        <td>Name:</td>
                                                                                        <td>
                                                                                            <dxe:ASPxTextBox ID="txt_location_Name" runat="server" EnableIncrementalFiltering="True" Text='<%# Bind("Name") %>' Width="100%">
                                                                                            </dxe:ASPxTextBox>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td>Party
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_l_PartyId" runat="server" Width="100%" Text='<%# Bind("PartyId") %>'
                                                                                                HorizontalAlign="Left" AutoPostBack="False">
                                                                                                <Buttons>
                                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                                </Buttons>
                                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupCust(txt_l_PartyId,txt_l_PartyName);
                                                                    }" />
                                                                                            </dxe:ASPxButtonEdit>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <dxe:ASPxTextBox ID="txt_PartyName" ClientInstanceName="txt_l_PartyName" runat="server" BackColor="Control" ReadOnly="True" Width="100%">
                                                                                            </dxe:ASPxTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>ZoneCode:</td>
                                                                                        <td>
                                                                                            <dxe:ASPxButtonEdit ID="btn_L_ZoneCode" ClientInstanceName="txt_L_ZoneCode" runat="server" Width="100%" Text='<%# Bind("ZoneCode") %>'>
                                                                                                <Buttons>
                                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                                </Buttons>
                                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                                                                                      PopupZoneCode(txt_L_ZoneCode,txt_WareHouseCode.GetText());
                                                                                                                                                                 }" />
                                                                                            </dxe:ASPxButtonEdit>
                                                                                        </td>
                                                                                        <td>StoreCode</td>
                                                                                        <td>
                                                                                            <dxe:ASPxButtonEdit ID="btn_L_StoreCode" ClientInstanceName="txt_L_StoreCode" Width="100%" runat="server" Text='<%# Bind("StoreCode") %>'>
                                                                                                <Buttons>
                                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                                </Buttons>
                                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                                         PopupStoreCode(txt_L_StoreCode,txt_WareHouseCode.GetText());
                                                                                                                }" />
                                                                                            </dxe:ASPxButtonEdit>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Length： </td>
                                                                                        <td>
                                                                                            <dxe:ASPxSpinEdit Increment="0" ID="txt_Length" runat="server" Text='<%# Bind("Length") %>' Width="100%" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                            </dxe:ASPxSpinEdit>
                                                                                        </td>
                                                                                        <td>Width： </td>
                                                                                        <td>
                                                                                            <dxe:ASPxSpinEdit ID="txt_Width" Increment="0" runat="server" Text='<%# Bind("Width") %>' Width="100%" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                            </dxe:ASPxSpinEdit>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Height： </td>
                                                                                        <td>
                                                                                            <dxe:ASPxSpinEdit ID="txt_Height" Increment="0" runat="server" Text='<%# Bind("Height") %>' Width="100%" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                            </dxe:ASPxSpinEdit>
                                                                                        </td>
                                                                                        <td>SpaceM3： </td>
                                                                                        <td>
                                                                                            <dxe:ASPxSpinEdit ID="txt_SpaceM3" Increment="0" runat="server" Text='<%# Bind("SpaceM3") %>' Width="100%" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                            </dxe:ASPxSpinEdit>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Remarks： </td>
                                                                                        <td colspan="3">
                                                                                            <dxe:ASPxMemo ID="memo_Remark" runat="server" Rows="3" Text='<%# Bind("Remark") %>' Width="100%">
                                                                                            </dxe:ASPxMemo>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                            <hr>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td style="width: 80px;">Creation</td>
                                                                                                    <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                                                    <td style="width: 100px;">Last Updated</td>
                                                                                                    <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <hr>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                                                <%--                                                                                                <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update">
                                                                                                    <ClientSideEvents Click="function(s,e) {
                                                                                                       grid_Location.PerformCallback('Save');
                                                                                                }" />
                                                                                                </dxe:ASPxHyperLink>--%>
                                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <div style="display: none">
                                                                                <dxe:ASPxTextBox ID="txt_WareHouseCode" ClientInstanceName="txt_WareHouseCode" runat="server" Width="100%" Value='<%# Bind("WarehouseCode") %>'></dxe:ASPxTextBox>
                                                                            </div>
                                                                        </dxw:ContentControl>
                                                                    </ContentCollection>
                                                                </dxtc:TabPage>
                                                            </TabPages>
                                                            <TabStyle BackColor="#F0F0F0">
                                                            </TabStyle>
                                                            <ContentStyle BackColor="#F0F0F0">
                                                            </ContentStyle>
                                                        </dxtc:ASPxPageControl>

                                                    </EditForm>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                            <TabStyle BackColor="#F0F0F0">
                            </TabStyle>
                            <ContentStyle BackColor="#F0F0F0">
                            </ContentStyle>
                        </dxtc:ASPxPageControl>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="600" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
