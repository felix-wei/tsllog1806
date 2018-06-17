<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellingPriceEdit.aspx.cs" Inherits="WareHouse_MastData_SellingPriceEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
            <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
       <script type="text/javascript">
           function MultiplePickProduct() {
               popubCtr.SetHeaderText('Multiple Product');
               popubCtr.SetContentUrl('../SelectPage/SelectPurchasePrice.aspx?Type=SQ&Sn=' + txt_DoNo.GetText());
               popubCtr.Show();
           }
           function AfterPopubMultiInv() {
               popubCtr.Hide();
               popubCtr.SetContentUrl('about:blank');
               detailGrid.Refresh();
           }
           function OnSaveCallBack(v) {
               if (v != null && v.indexOf("Fail") > -1) {
                   alert(v);
               }
               else if (v == "") {
                   detailGrid.Refresh();
               }
               else if (v != null) {
                   window.location = 'SellingPrice.aspx?no=' + v;
                   //txt_SchRefNo.SetText(v);
                   //txt_DoNo.SetText(v);
               }
           }
           function PrintDo(doNo, doType) {
               if (doType == "SQ")
                   window.open('/ReportWarehouse/PrintView.aspx?document=wh_SQ&master=' + doNo + "&house=0");
           }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsPrice" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhTrans"
            KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsPriceDet" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhTransDet"
            KeyMember="Id" />
        <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
            KeyMember="Id" FilterExpression="Type='Attribute'" />
        <div>
            <table width="630">
                <tr>
                     <td width="120">Quotatin No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SQNo" ClientInstanceName="txt_SQNo" ReadOnly="true" BackColor="Control" Width="150" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Retrieve">
                            <ClientSideEvents Click="function(s,e){
                        window.location='SellingPriceEdit.aspx?no='+txt_SQNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton2" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='SellingPriceEdit.aspx?no=0';
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='SellingPrice.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div>
              <dxwgv:ASPxGridView ID="grid_Price" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="false" DataSourceID="dsPrice"
                 OnInitNewRow="grid_Price_InitNewRow"
                OnInit="grid_Price_Init" OnCustomDataCallback="grid_Price_CustomDataCallback" OnCustomCallback="grid_Price_CustomCallback"
                OnHtmlEditFormCreated="grid_Price_HtmlEditFormCreated" OnRowInserting="grid_Price_RowInserting" OnRowUpdating="grid_Price_RowUpdating" OnRowDeleting="grid_Price_RowDeleting">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="text-align: left; padding: 2px 2px 2px 2px; width: 1050px">
                                <tr>
                                   <td width="80%">
                                        <table style="border: solid 1px black; padding: 2px 2px 2px 2px; width: 80%">
                                            <tr>
                                                <td>Print Document:
                                        <a href="#" onclick='PrintDo("<%# Eval("DoNo") %>","SQ")'>Sales Quotation</a>
                                                    &nbsp;&nbsp;
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("DoStatus"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                             if(txt_PartyId.GetText()=='')
                                               {
                                                   alert('Company not be null !!!');                 
                                               }
                                               else
                                               {
                                                 detailGrid.UpdateEdit();
                                                }
                                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                        <td>
                                          <dxe:ASPxButton ID="btn_Cancle" ClientInstanceName="btn_Cancle" runat="server" Width="100" Text="Cancel" AutoPostBack="False"
                                            UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
                                                  detailGrid.CancelEdit();   
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>

                                                               
                                </tr>
                            </table>

                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" ClientInstanceName="txt_Id" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                            <table style="text-align: left; padding: 2px 2px 2px 2px;">
                                <tr>
                                    <td style="width: 60px;"></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                </tr>
                                <tr>
                                    <td>No
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_DoNo" runat="server" ClientInstanceName="txt_DoNo" ReadOnly="true" BackColor="Control" Width="100%" Text='<%# Eval("DoNo") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>FromDate</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_IssueDate" Width="100%" runat="server" Value='<%# Eval("DoDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>ToDate
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="txt_ExpectedDate" runat="server" Value='<%# Eval("ExpectedDate") %>' Width="100%" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Company</td>
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="0">
                                            <td width="94">
                                                <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" runat="server"
                                                    Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PartyId") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_PartyId,txt_PartyName,null,null,null,null,null,null,'V');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="290" ReadOnly="true" Text='<%# Eval("PartyName") %>' BackColor="Control" ID="txt_PartyName" ClientInstanceName="txt_PartyName">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </table>
                                    </td>
                                   <td>Status</td>
                                     <td>
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100%" ID="cmb_Status"
                                            runat="server" Text='<%# Eval("DoStatus") %>'>
                                            <Items>
                                               <dxe:ListEditItem Text="USE" Value="USE" />
                                              <dxe:ListEditItem Text="INACTIVE" Value="INACTIVE" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Remark</td>
                                    <td colspan="5">
                                        <dxe:ASPxMemo runat="server" Width="100%" Rows="4" ID="txt_Remark" Text='<%# Eval("Remark") %>' ClientInstanceName="txt_Remark3">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <hr>
                                        <table>
                                            <tr>
                                                 <td style="width: 80px;">Creation</td>
                                            <td style="width: 200px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateTimeStr( Eval("CreateDateTime"))%></td>
                                            <td style="width: 100px;">Last Updated</td>
                                            <td style="width: 200px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateTimeStr(Eval("UpdateDateTime"))%></td>
                                            <td style="width: 100px;">Status</td>
                                            <td><%# Eval("StatusCode")%> </td>
                                            </tr>
                                        </table>
                                        <hr>
                                    </td>
                                </tr>
                            </table>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl_Hbl" Width="900px" Height="440px" ActiveTabIndex="0">
                                <TabPages>
                                    <dxtc:TabPage Text="SKU List" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton10" runat="server" Text="Multiple Pick SKU" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("DoStatus"),"USE")=="USE"%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        MultiplePickProduct();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddSKULine" Width="150" runat="server" Text="Add SKU" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"USE")=="USE"%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                       grid_SKULine.AddNewRow();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>

                                                    </tr>
                                                </table>
                                                <div>
                                                    <%-- style="WIDTH: 900px; overflow-y: scroll;"--%>
                                                    <dxwgv:ASPxGridView ID="grid_SKULine" ClientInstanceName="grid_SKULine" runat="server" OnRowInserting="grid_SKULine_RowInserting"
                                                        OnRowDeleting="grid_SKULine_RowDeleting" OnRowUpdating="grid_SKULine_RowUpdating" OnBeforePerformDataSelect="grid_SKULine_BeforePerformDataSelect"
                                                        KeyFieldName="Id" Width="1000" AutoGenerateColumns="False" Styles-Cell-Paddings-Padding="1" Styles-EditForm-Paddings-Padding="0"  
                                                        DataSourceID="dsPriceDet" OnInit="grid_SKULine_Init" OnInitNewRow="grid_SKULine_InitNewRow" OnRowInserted="grid_SKULine_RowInserted" OnRowUpdated="grid_SKULine_RowUpdated">
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_SKULine.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_del" runat="server" 
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_SKULine.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_SKULine.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_SKULine.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                           <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="ProductCode" VisibleIndex="2" Width="100">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_Product" ClientInstanceName="txt_Product" runat="server" Value='<%# Bind("ProductCode") %>' Width="100">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupProduct(txt_Product,txt_Des,null,null,null,null,null,null,null,cb_Att1,cb_Att2,cb_Att3,cb_Att4,cb_Att5,cb_Att6,null,null,null,null);
                                                                           }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="3" Width="150px">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_Des" runat="server" ClientInstanceName="txt_Des" Text='<%# Bind("Des1") %>' Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty1" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer" Width="40" />
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" CellStyle-HorizontalAlign="Left" VisibleIndex="5" Width="50">
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_Price" runat="server" Width="60" ClientInstanceName="spin_Price" Value='<%# Bind("Price") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" DecimalPlaces="2" DisplayFormatString="0.00">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" CellStyle-HorizontalAlign="Left" VisibleIndex="5" Width="100">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Bind("Currency") %>' runat="server" Width="60px" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,null);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Total" FieldName="DocAmt" VisibleIndex="15" Width="10">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att1" FieldName="Att1" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att1" ClientInstanceName="cb_Att1" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att1") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att2" FieldName="Att2" VisibleIndex="7" Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att2" ClientInstanceName="cb_Att2" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att2") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att3" FieldName="Att3" VisibleIndex="7" Width="60">
                                                               <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att3" ClientInstanceName="cb_Att3" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att3") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att4" FieldName="Att4" VisibleIndex="7" Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att4" ClientInstanceName="cb_Att4" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att4") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att5" FieldName="Att5" VisibleIndex="7" Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att5" ClientInstanceName="cb_Att5" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att5") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att6" FieldName="Att6" VisibleIndex="7" Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att6" ClientInstanceName="cb_Att6" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att6") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                        </Columns>
                                                        <Settings ShowFooter="true" /> 
                                                         <TotalSummary>
                                                            <dxwgv:ASPxSummaryItem FieldName="ProductCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                                            <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0:0}" />
                                                            <dxwgv:ASPxSummaryItem FieldName="DocAmt" SummaryType="Sum" DisplayFormat="{0:00.00}" />
                                                        </TotalSummary>
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                </TabPages>
                            </dxtc:ASPxPageControl>
                            <hr />
                        </div>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="800" EnableViewState="False">
                </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
