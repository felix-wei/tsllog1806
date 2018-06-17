<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartyInfo.aspx.cs" Inherits="SelectPage_PartyInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsXXParty" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsCountry" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCountry" KeyMember="Code" />
        <wilson:DataSource ID="dsCity" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCity" KeyMember="Code" />
        <wilson:DataSource ID="dsPort" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXPort" KeyMember="Code" FilterExpression="isnull(Code,'')<>''" />
        <wilson:DataSource ID="dsCurrency" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCurrency" KeyMember="CurrencyId" />
        <wilson:DataSource ID="dsPartyAcc" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXPartyAcc" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsTerm" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXTerm" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsPartyGroup" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXPartyGroup" KeyMember="Code" />
        <wilson:DataSource ID="dsPartySale" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXPartySale" KeyMember="Id" />
        <wilson:DataSource ID="dsSaleman" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXSalesman" KeyMember="code" />
        <wilson:DataSource ID="dsPartyLog" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefPartyLog" KeyMember="Id" />
        <wilson:DataSource ID="dsAttachment" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.WhAttachment" KeyMember="Id" FilterExpression="1=0" />
                    <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Id" FilterExpression="CodeType='3'" />
        <wilson:DataSource ID="dsAddress" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefAddress" KeyMember="Id" />
        <wilson:DataSource ID="dsContact" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefContact" KeyMember="Id" />
    <div>
        <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsXXParty"
            KeyFieldName="SequenceId" AutoGenerateColumns="False"
            Width="100%" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting" OnCustomDataCallback="grid_CustomDataCallback" OnCustomCallback="grid_CustomCallback" OnHtmlEditFormCreated="grid_HtmlEditFormCreated">
            <SettingsEditing Mode="EditForm" />
            <SettingsCustomizationWindow Enabled="True" />
           <Settings ShowColumnHeaders="false" />
            <SettingsBehavior ConfirmDelete="True" />
            <Templates>
                <EditForm>
                    <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                        <tr>
                            <td width="90%"></td>
                            <td>
                                <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                    Enabled='<%# (SafeValue.SafeString(Eval("Status"),"USE")=="USE")%>'>
                                    <ClientSideEvents Click="function(s,e) {
                                                    grid.PerformCallback('Save');
                                                 }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <dxtc:ASPxPageControl ID="pageControl" runat="server" Width="100%" Height="500px" EnableCallBacks="true" BackColor="#F0F0F0" ActiveTabIndex="0">
                        <TabPages>
                            <dxtc:TabPage Text="Info" Visible="true">
                                <ContentCollection>
                                    <dxw:ContentControl>
                                        <div style="display: none">
                                            <dxe:ASPxTextBox ID="txt_SequenceId" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                BackColor="Control" Text='<%# Eval("SequenceId") %>' Width="170">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <div style="padding: 4px 4px 3px 4px;">
                                            <table width="70%">
                                                <tr>
                                                    <td>Party Id：
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtPartyId" runat="server" Width="100%" Value='<%# Eval("PartyId") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>UEN No:</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtCrNo" runat="server" EnableIncrementalFiltering="True" Value='<%# Eval("CrNo") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Short Name：
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtCode" runat="server" Width="100%" Value='<%# Eval("Code") %>'>
                                                        </dxe:ASPxTextBox>
                                                        <dxe:ASPxLabel ID="lblMessage" runat="server" Text=""></dxe:ASPxLabel>
                                                    </td>
                                                    <td>Group:</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbGroup" runat="server" DataSourceID="dsPartyGroup" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Value='<%# Eval("GroupId") %>' TextField="Code" ValueField="Code" Width="100%">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                                <dxe:ListBoxColumn FieldName="Description" Width="100%" />
                                                            </Columns>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Full Name： </td>
                                                    <td colspan="3">
                                                        <dxe:ASPxTextBox ID="txtName" runat="server" Value='<%# Eval("Name") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td colspan="3">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxCheckBox ID="Customer" runat="server" Value='<%# Eval("IsCustomer") %>' Text="Customer">
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                                <td style="display: none">
                                                                    <dxe:ASPxCheckBox ID="Agent" runat="server" Value='<%# Eval("IsAgent") %>' Text="Agent">
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxCheckBox ID="Vendor" runat="server" Value='<%# Eval("IsVendor") %>' Text="Vendor">
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="background-color: Gray; color: White;">
                                                        <b>Main</b>
                                                    </td>
                                                    <td colspan="2" style="background-color: Gray; color: White;">
                                                        <b>Accounting</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Address： </td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txtAddress" runat="server" Rows="3" Value='<%# Eval("Address") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                    <td>Address：</td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txtAddress1" runat="server" Rows="3" Value='<%# Eval("Address1") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Telephone： </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtTel1" runat="server" Value='<%# Eval("Tel1") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Telephone：</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtTel2" runat="server" Value='<%# Eval("Tel2") %>' Width="100%">
                                                        </dxe:ASPxTextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Fax：</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtFax1" runat="server" Value='<%# Eval("Fax1") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Fax：</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtFax2" runat="server" Value='<%# Eval("Fax2") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Contact： </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtContact1" runat="server" Value='<%# Eval("Contact1") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Contact：</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtContact2" runat="server" Value='<%# Eval("Contact2") %>' Width="100%">
                                                        </dxe:ASPxTextBox>


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Email：</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtEmail1" runat="server" Value='<%# Eval("Email1") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Email：</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtEmail2" runat="server" Value='<%# Eval("Email2") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ZipCode： </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_ZipCode" runat="server" Value='<%# Eval("ZipCode") %>' Width="100%">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Country： </td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbCountry" Width="100%" runat="server" DataSourceID="dsCountry" EnableIncrementalFiltering="true" TextField="Name" Value='<%# Eval("CountryId") %>' ValueField="Code" ValueType="System.String">
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>City： </td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbCity" Width="100%" runat="server" DataSourceID="dsCity" EnableIncrementalFiltering="true" TextField="Name" Value='<%# Eval("City") %>' ValueField="Code" ValueType="System.String">
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>Port </td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbPort" Width="100%" TextFormatString="{0}" IncrementalFilteringMode="StartsWith"
                                                            CallbackPageSize="30" runat="server" DataSourceID="dsPort" EnableCallbackMode="True" EnableIncrementalFiltering="true" TextField="Name" Value='<%# Eval("PortID") %>' ValueField="Code" ValueType="System.String">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                                            </Columns>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Term:</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbTerm" runat="server" DataSourceID="dsTerm" TextFormatString="{0}" EnableIncrementalFiltering="True" Value='<%# Eval("TermId") %>' IncrementalFilteringMode="StartsWith" TextField="Code" ValueField="SequenceId" Width="100%">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="Code" Width="100%" />
                                                            </Columns>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>Sales Man</td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" ID="txt_DefaultSale" ReadOnly="true" Width="100%" BackColor="Control"></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Credit Limit</td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="spin_WarningAmt" ClientInstanceName="spin_WarningAmt" DisplayFormatString="0.00"
                                                            runat="server" Width="100%" Value='<%# Eval("WarningAmt")%>' DecimalPlaces="2" Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>Warning Qty</td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit runat="server" Width="100%"
                                                            ID="spin_WarningQty" Value='<%# Eval("WarningQty")%>' Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Block Amt</td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="spin_BlockAmt" ClientInstanceName="spin_BlockAmt" DisplayFormatString="0.00"
                                                            runat="server" Width="100%" Value='<%# Eval("BlockAmt")%>' DecimalPlaces="2" Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>Block Qty</td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit runat="server" Width="100%"
                                                            ID="spin_BlockQty" Value='<%# Eval("BlockQty")%>' Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ClientType</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbb_ClientType" runat="server" Value='<%# Eval("ClientType") %>' DropDownStyle="DropDown" Width="100%">
                                                            <Items>
                                                                <dxe:ListEditItem Value="Direct" Text="Direct" />
                                                                <dxe:ListEditItem Value="Indirect" Text="Indirect" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>ParentCode</td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="btn_ParentCode" ClientInstanceName="btn_ParentCode" runat="server" Text='<%# Eval("ParentCode") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ParentCode,null);
                                                                        }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Remarks： </td>
                                                    <td colspan="3">
                                                        <dxe:ASPxMemo ID="memoRemark" runat="server" Rows="3" Value='<%# Eval("Remark") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Status：</td>
                                                    <td>
                                                        <dxe:ASPxLabel ID="lblStatus" runat="server" BackColor="Control" ReadOnly="True" Value='<%# Eval("Status") %>'></dxe:ASPxLabel>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>CreateBy： </td>
                                                    <td><%# Eval("CreateBy") %></td>
                                                    <td>CreateDateTime </td>
                                                    <td><%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                </tr>
                                                <tr>
                                                    <td>UpdateBy： </td>
                                                    <td><%# Eval("UpdateBy") %></td>
                                                    <td>UpdateDateTime </td>
                                                    <td><%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                </tr>
                                            </table>
                                        </div>

                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>

                            <dxtc:TabPage Text="Account Info" ActiveTabStyle-Height="600" Visible="false">
                                <ContentCollection>
                                    <dxw:ContentControl Height="600">
                                        <table width="450">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&(SafeValue.SafeString(Eval("Status"),"USE")=="USE")%>'>
                                                        <ClientSideEvents Click="function(s,e){
                                grid_Account.AddNewRow();
                                }" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                        <div>
                                            <dxwgv:ASPxGridView ID="grid_Account" runat="server" ClientInstanceName="grid_Account" DataSourceID="dsPartyAcc"
                                                KeyFieldName="SequenceId" AutoGenerateColumns="False"
                                                Width="1000px" OnInit="grid_Account_Init" OnInitNewRow="grid_Account_InitNewRow" OnRowDeleting="grid_Account_RowDeleting" OnRowUpdating="grid_Account_RowUpdating" OnRowInserting="grid_Account_RowInserting" OnBeforePerformDataSelect="grid_Account_DataSelect">
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings VerticalScrollableHeight="600" />
                                                <SettingsEditing Mode="Inline" />
                                                <SettingsCustomizationWindow Enabled="True" />
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="70" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                            Enabled='<%# SafeValue.SafeString(Eval("Status"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { grid_Account.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Status"),"USE")=="USE" %>'
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Account.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                            Enabled='<%# SafeValue.SafeString(Eval("Status"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { grid_Account.UpdateEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Status"),"USE")=="USE" %>'
                                                                            Text="Cancle" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { grid_Account.CancelEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="SequenceId" VisibleIndex="1" Visible="false">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="2" Width="280">
                                                        <PropertiesComboBox ValueType="System.String" TextFormatString="{0}" DataSourceID="dsCurrency" TextField="CurrencyName" EnableIncrementalFiltering="true"
                                                            ValueField="CurrencyId">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="CurrencyId" Width="70px" />
                                                                <dxe:ListBoxColumn FieldName="CurrencyName" Width="100%" />
                                                            </Columns>
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Don't be null" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="PartyId" FieldName="PartyId" VisibleIndex="3" Visible="false">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="ArCode" FieldName="ArCode" VisibleIndex="4" />
                                                    <dxwgv:GridViewDataTextColumn Caption="ApCode" FieldName="ApCode" VisibleIndex="5" />
                                                </Columns>

                                                <SettingsPager Mode="ShowPager"></SettingsPager>
                                                <Styles Header-HorizontalAlign="Center">
                                                    <Header HorizontalAlign="Center"></Header>
                                                    <Cell HorizontalAlign="Center"></Cell>
                                                </Styles>
                                            </dxwgv:ASPxGridView>
                                        </div>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                             <dxtc:TabPage Text="Address">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="ASPxButton1" Width="160" runat="server" Text="Add New" AutoPostBack="false"  Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&(SafeValue.SafeString(Eval("Status"),"USE")=="USE")%>'>
                                                <ClientSideEvents Click="function(s,e){
                                grid_address.AddNewRow();
                                }" />
                                            </dxe:ASPxButton>
                                            <dxwgv:ASPxGridView ID="grid_address" ClientInstanceName="grid_address" runat="server" Width="100%" DataSourceID="dsAddress" OnBeforePerformDataSelect="grid_address_BeforePerformDataSelect"
                                                KeyFieldName="Id" OnInit="grid_address_Init" OnInitNewRow="grid_address_InitNewRow" OnRowInserting="grid_address_RowInserting" OnRowUpdating="grid_address_RowUpdating" OnRowDeleting="grid_address_RowDeleting"
                                                AutoGenerateColumns="False">
                                                <SettingsPager Mode="ShowPager" PageSize="20">
                                                </SettingsPager>
                                                <SettingsBehavior ConfirmDelete="true" />
                                                <SettingsEditing Mode="Inline"></SettingsEditing>
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                                                        <EditButton Visible="True" />
                                                        <DeleteButton Visible="True">
                                                        </DeleteButton>
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Address" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="350px">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataMemoColumn Caption="Address" PropertiesMemoEdit-Rows="3" FieldName="Address1" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="350px">
                                                    </dxwgv:GridViewDataMemoColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Postcode" FieldName="Postcode" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="TypeId" VisibleIndex="1" Width="80">
                                                        <EditItemTemplate>
                                                            <dxe:ASPxComboBox ID="cbb_TypeId" runat="server" Text='<%# Bind("TypeId") %>' Width="80">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Location" Value="Location" />
                                                                    <dxe:ListEditItem Text="Owner" Value="Owner" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="1" Width="80">
                                                        <EditItemTemplate>
                                                            <dxe:ASPxComboBox ID="cbb_Location" runat="server" Text='<%# Bind("Location") %>' Width="80" DataSourceID="dsUom">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="Code" Caption="Code" />
                                                                </Columns>
                                                            </dxe:ASPxComboBox>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>

                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Contact">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="ASPxButton5" Width="160" runat="server" Text="Add New" AutoPostBack="false"  Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&(SafeValue.SafeString(Eval("Status"),"USE")=="USE")%>'>
                                                <ClientSideEvents Click="function(s,e){
                                grid_contact.AddNewRow();
                                }" />
                                            </dxe:ASPxButton>
                                            <dxwgv:ASPxGridView ID="grid_contact" ClientInstanceName="grid_contact" runat="server" Width="100%" DataSourceID="dsContact" OnBeforePerformDataSelect="grid_contact_BeforePerformDataSelect" OnRowUpdated="grid_contact_RowUpdated" OnRowInserted="grid_contact_RowInserted"
                                                KeyFieldName="Id" OnInit="grid_contact_Init" OnInitNewRow="grid_contact_InitNewRow" OnRowInserting="grid_contact_RowInserting" OnRowUpdating="grid_contact_RowUpdating" OnRowDeleting="grid_contact_RowDeleting"
                                                AutoGenerateColumns="False">
                                                <SettingsPager Mode="ShowPager" PageSize="20">
                                                </SettingsPager>
                                                <SettingsBehavior ConfirmDelete="true" />
                                                <SettingsEditing Mode="Inline"></SettingsEditing>
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                                                        <EditButton Visible="True" />
                                                        <DeleteButton Visible="True">
                                                        </DeleteButton>
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="1" Width="150px">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Tel" FieldName="Tel" VisibleIndex="1" Width="150">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Fax" FieldName="Fax" VisibleIndex="1" Width="150">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="1" Width="150">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Mobile" FieldName="Mobile" VisibleIndex="1" Width="150">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataMemoColumn Caption="Address" PropertiesMemoEdit-Rows="3" FieldName="Address" VisibleIndex="1" Width="200px">
                                                    </dxwgv:GridViewDataMemoColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Department" FieldName="Department" VisibleIndex="1" Width="100px">
                                                        <PropertiesComboBox>
                                                            <Items>
                                                                <dxe:ListEditItem  Value="" Text=""/>
                                                                <dxe:ListEditItem  Value="Sales" Text="Sales"/>
                                                                <dxe:ListEditItem  Value="Operation" Text="Operation"/>
                                                                <dxe:ListEditItem  Value="Finance" Text="Finance"/>
                                                                <dxe:ListEditItem  Value="Admin" Text="Admin"/>
                                                                <dxe:ListEditItem  Value="Others" Text="Others"/>
                                                            </Items>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataMemoColumn Caption="Remark" FieldName="Remark" VisibleIndex="1" Width="200" EditFormSettings-RowSpan="3">
                                                    </dxwgv:GridViewDataMemoColumn >
                                                    <dxwgv:GridViewDataCheckColumn Caption="Default?" FieldName="IsDefault" VisibleIndex="1" SortIndex="0" SortOrder="Descending">
                                                        <DataItemTemplate>
                                                            <%# IsDefault(SafeValue.SafeBool(Eval("IsDefault"),true)) %>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataCheckColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            <dxtc:TabPage Text="Sales">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table width="450">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton2" Width="100" runat="server" Text="Add New" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&(SafeValue.SafeString(Eval("Status"),"USE")!="InActive")%>'>
                                                            <ClientSideEvents Click="function(s,e){
                                grid_Sale.AddNewRow();
                                }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Sale" runat="server" ClientInstanceName="grid_Sale" DataSourceID="dsPartySale"
                                                    KeyFieldName="Id" AutoGenerateColumns="False"
                                                    Width="1000px" OnInit="grid_Sale_Init" OnInitNewRow="grid_Sale_InitNewRow" OnRowDeleting="grid_Sale_RowDeleting" OnRowUpdating="grid_Sale_RowUpdating" OnRowInserting="grid_Sale_RowInserting" OnBeforePerformDataSelect="grid_Sale_DataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="70" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                Enabled='<%# SafeValue.SafeString(Eval("Status"),"USE")!="InActive" %>' ClientSideEvents-Click='<%# "function(s) { grid_Sale.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Status"),"USE")!="InActive" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Sale.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                Enabled='<%# SafeValue.SafeString(Eval("Status"),"USE")!="InActive" %>' ClientSideEvents-Click='<%# "function(s) { grid_Sale.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Status"),"USE")!="InActive" %>'
                                                                                Text="Cancle" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { grid_Sale.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>

                                                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="1" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PartyId" FieldName="PartyId" VisibleIndex="2" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Sales Man" FieldName="Salesman" VisibleIndex="2" Width="280">
                                                            <PropertiesComboBox ValueType="System.String" TextFormatString="{0}" DataSourceID="dsSaleman" TextField="Name" EnableIncrementalFiltering="true"
                                                                ValueField="Code">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="Code" Width="70px" />
                                                                    <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                                                </Columns>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="Don't be null" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="DefaultInd" FieldName="DefaultInd" VisibleIndex="4" Width="280">
                                                            <PropertiesComboBox ValueType="System.String" TextFormatString="{0}" EnableIncrementalFiltering="true">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Y" Value="Y" />
                                                                    <dxe:ListEditItem Text="N" Value="N" />
                                                                </Items>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="Don't be null" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                    <Templates>
                                                    </Templates>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            <dxtc:TabPage Text="Attachments" Visible="false">
                                <ContentCollection>
                                    <dxw:ContentControl>
                                        <table>
                                            <tr>
                                                <td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton13" Width="100%" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0 %>' runat="server" Text="Upload Attachments"
                                                            AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                                         isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                        grd_Photo.Refresh();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                            </tr>
                                        </table>
                                        <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsAttachment"
                                            KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                            AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                            <Settings />
                                            <Columns>
                                                <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                    <DataItemTemplate>
                                                        <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                            ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                        </dxe:ASPxButton>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                    <DataItemTemplate>
                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                            Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                        </dxe:ASPxButton>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                    <DataItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <a href='<%# Eval("Path")%>' target="_blank">
                                                                        <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                        </dxe:ASPxImage>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <Templates>
                                                <EditForm>
                                                    <div style="display: none">
                                                        <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                    </div>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>Remark
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Bind("FileNote") %>'>
                                                                </dxe:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                    <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                                        <ClientSideEvents Click="function(s,e){grd_Photo.UpdateEdit();}" />
                                                                    </dxe:ASPxHyperLink>
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                    </table>
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
    </form>
</body>
</html>
