<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="SelectParty.aspx.cs" Inherits="WareHouse_SelectPage_SelectParty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
      <div>
     <table>
            <tr>
                <td>
                  <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Name" />
                </td>
                <td>
                    <dxe:ASPxTextBox ID ="txt_Name" Width="200" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Search" 
                        onclick="btn_Sch_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                     <dxe:ASPxButton ID="btn_Save" Width="140" runat="server" Text="New Customer" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid.AddNewRow();
                                                 }" />
                                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
          <wilson:DataSource ID="dsXXParty" runat="server" ObjectSpace="C2.Manager.ORManager"
              TypeName="C2.XXParty" KeyMember="SequenceId"  FilterExpression="1=0"/>
          <wilson:DataSource ID="dsSubCompany" runat="server" ObjectSpace="C2.Manager.ORManager"
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
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" Width="100%" DataSourceID="dsXXParty" 
            OnCustomCallback="ASPxGridView1_CustomCallback" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
             KeyFieldName="PartyId"  OnInit="ASPxGridView1_Init" OnRowInserting="ASPxGridView1_RowInserting" 
            OnInitNewRow="ASPxGridView1_InitNewRow"
             AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing  Mode="EditForm"/>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutParty("<%# Eval("PartyId") %>","<%# Eval("Name") %>","<%# Eval("Contact1") %>","<%# Eval("Tel1") %>","<%# Eval("Fax1") %>","<%# Eval("Email1") %>","<%# Eval("ZipCode") %>","<%# Eval("Address") %>","<%# Eval("CountryId") %>","<%# Eval("City") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Id" FieldName="PartyId" VisibleIndex="1" Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
       </Columns>
            <Templates>
                <EditForm>
                    <div style="display: none">
                        <dxe:ASPxTextBox ID="txt_SequenceId" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                            BackColor="Control" Text='<%# Eval("SequenceId") %>' Width="170">
                        </dxe:ASPxTextBox>
                    </div>
                    <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                            <tr>
                                <td width="90%"></td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                        Enabled='<%# (SafeValue.SafeString(Eval("Status"),"USE")!="InActive")%>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid.PerformCallback('Save');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                 <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="GoTo List" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid.PerformCallback('Cancle');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
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
                            <tr style="display: none">
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
                            <tr style="display:none">
                                <td>Parent Company</td>
                                <td colspan="3">
                                    <%--<table cellpadding="0" cellspacing="0" width="100%">
                                        <td width="94">
                                            <dxe:ASPxButtonEdit ID="txt_ParentId" ClientInstanceName="txt_ParentId" runat="server"
                                                Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ParentId") %>'>
                                                <Buttons>
                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_ParentId,txt_ParentName,null,null,null,null,null,null,null,null,'C');
                                                                    }" />
                                            </dxe:ASPxButtonEdit>
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="true" Text='<%# Eval("ParentName") %>' BackColor="Control" ID="txt_ParentName" ClientInstanceName="txt_ParentName">
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </table>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <dxe:ASPxCheckBox ID="Customer" runat="server" Value='<%# Eval("IsCustomer") %>' Text="Customer">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td>
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
                                <td>Address： </td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txtAddress" runat="server" Rows="3" Value='<%# Eval("Address") %>' Width="100%">
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Telephone： </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtTel1" runat="server" Value='<%# Eval("Tel1") %>' Width="100%">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Fax： </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtFax1" runat="server" Value='<%# Eval("Fax1") %>' Width="100%">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtTel2" runat="server" Value='<%# Eval("Tel2") %>' Width="100%">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td></td>
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
                                <td>Email： </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtEmail1" runat="server" Value='<%# Eval("Email1") %>' Width="100%">
                                    </dxe:ASPxTextBox>

                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtContact2" runat="server" Value='<%# Eval("Contact2") %>' Width="100%">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td></td>
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
                                <td>Warning Amt</td>
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
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>            
    </div>
    </form>
</body>
</html>
