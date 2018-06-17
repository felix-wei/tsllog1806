<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="SeaQuoteEdit.aspx.cs" Inherits="PagesFreight_Quote1_SeaQuoteEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quotation Edit</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/Pages.js"></script>
    <script type="text/javascript">
        function PrintSeaQuotation_Detail(refNo, jobNo) {
            window.open('/ReportFreightSea/printview.aspx?document=80&master=' + refNo + '&house=' + jobNo);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsSeaQuotation" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaQuotation" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsSeaQuotationDet1" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaQuotationDet1" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsSeaQuotationDet2" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaQuotationDet2" KeyMember="Id" FilterExpression="1=0" />

            <table>
                <tr>
                    <td>Quote No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txtSchNo" ClientInstanceName="txtSchNo" Width="110" runat="server"
                            Text="">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" Width="110" runat="server" Text="Retrieve" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) {
                     window.location='SeaQuoteEdit.aspx?no='+txtSchNo.GetText()
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Add New" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) {
                     window.location='SeaQuoteEdit.aspx?no=0'
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
                DataSourceID="dsSeaQuotation" Width="100%" KeyFieldName="Id" OnInit="ASPxGridView1_Init"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback"
                AutoGenerateColumns="False">
                <SettingsPager PageSize="50">
                </SettingsPager>
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <SettingsCustomizationWindow Enabled="True" />
                <Templates>
                    <EditForm>
                        <table border="0">
                            <tr>
                                <td colspan="6" width="730"> </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" runat="server" Text="Save" Width="100" AutoPostBack="false" UseSubmitBehavior="false" Paddings-PaddingRight="0">
                                        <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('');
                            }" />
                                    </dxe:ASPxButton>

                                </td>

                            </tr>
                        </table>
                        <table style="border: solid 1px black; padding: 2px 2px 2px 2px; width: 100%">
                            <tr>
                                <td colspan="7" style="background-color: Gray; color: White;">
                                    <b>Print Documents</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="#" onclick='PrintSeaQuotation_Detail("<%# Eval("QuoteNo")%>",0)'>SeaQuotation</a>&nbsp;
                                </td>
                            </tr>
                        </table>
                        <table border="0">
                            <tr>
                                <td colspan="6" style="display: none">
                                    <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                        Width="100" Text='<%# Eval("Id")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">Quote No
                                </td>
                                <td width="160">
                                    <dxe:ASPxTextBox runat="server" ID="txt_DocNo" ClientInstanceName="txt_DocNo" ReadOnly="true"
                                        BackColor="Control" Width="100%" Text='<%# Eval("QuoteNo")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Quote Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_DocDt" runat="server" Width="100%" Value='<%# Eval("QuoteDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td width="100">ExpireDate
                                </td>
                                <td width="160">
                                    <dxe:ASPxDateEdit ID="txt_ToDt" runat="server" Width="100%" Value='<%# Eval("ExpireDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">Quote Type
                                </td>
                                <td>
                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="100%" ID="cbx_QuoteType"
                                        Value='<%# Eval("QuoteType")%>' DropDownStyle="DropDown">
                                        <Items>
                                            <dxe:ListEditItem Text="FCL" Value="FCL" />
                                            <dxe:ListEditItem Text="LCL" Value="LCL" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Subject</td>
                                <td colspan="3">
                                    <dxe:ASPxTextBox runat="server" ID="txt_Subject" Width="100%" Text='<%# Eval("Subject") %>'></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Party To
                                </td>
                                <td colspan="6">
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="60" Text='<%# Bind("PartyTo") %>'
                                                    HorizontalAlign="Left" AutoPostBack="False">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                            PopupParty(txt_CustId,txt_CustName,'CA');
                                }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName"
                                                    BackColor="Control" Width="395" runat="server" Text='<%# Eval("PartyName") %>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>Tel
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Tel" ClientInstanceName="txt_Tel" Text='<%# Eval("Tel") %>'></dxe:ASPxTextBox>
                                </td>
                                <td>Fax
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Fax" ClientInstanceName="txt_Fax" Text='<%# Eval("Fax") %>'></dxe:ASPxTextBox>
                                </td>
                                <td>Contact
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Contact" ClientInstanceName="txt_Contact" Text='<%# Eval("Contact") %>'></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Pol
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Width="100%"
                                        HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Pol")%>' MaxLength="5">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pol);
                                                                    }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>Pod
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Width="100%"
                                        HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Pod")%>' MaxLength="5">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pod);
                                                                    }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>FinDest
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="txt_FinDest" ClientInstanceName="txt_FinDest" Width="100%" Text='<%# Eval("FinDest") %>'></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Vessel
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxTextBox runat="server" ID="txt_Vessel" ClientInstanceName="txt_Vessel" Width="100%" Text='<%# Eval("Vessel") %>'></dxe:ASPxTextBox>
                                </td>
                                <td>Voyage
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="txt_Voyage" ClientInstanceName="txt_Voyage" Width="100%" Text='<%# Eval("Voyage") %>'></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Eta</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_Eta" runat="server" Width="100%" Value='<%# Eval("Eta")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>Etd</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_Etd" runat="server" Width="100%" Value='<%# Eval("Etd")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>EtaDest</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_EtaDest" runat="server" Width="100%" Value='<%# Eval("EtaDest")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>Currency
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" runat="server" Width="100%"
                                        HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CurrencyId") %>' MaxLength="3">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_exRate);
                                                                    }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>ExRate
                                </td>
                                <td>
                                    <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_exRate" ClientInstanceName="spin_exRate"
                                        runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000000" DecimalPlaces="6">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>Salesman
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_SalesmanId" ClientInstanceName="txt_SalesmanId" runat="server" Text='<%# Eval("SalesmanId")%>'>
                                        <Buttons>
                                            <dxe:EditButton Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupSalesman(txt_SalesmanId,null);
                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>Create User/Date
                                </td>
                                <td>
                                    <%# Eval("CreateUser")%>-<%# Eval("CreateDate","{0:dd/MM/yyyy}")%>
                                </td>
                                <td>Update User/Date
                                </td>
                                <td>
                                    <%# Eval("UpdateUser")%>-<%# Eval("UpdateDate","{0:dd/MM/yyyy}")%>
                                </td>
                            </tr>
                            <tr>
                                <td>Remarks
                                </td>
                                <td colspan="5">
                                    <dxe:ASPxMemo runat="server" ID="txt_Remarks1" Rows="3" Width="660" Text='<%# Eval("Rmk")%>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Note1
                                </td>
                                <td colspan="5">
                                    <dxe:ASPxMemo runat="server" ID="txt_Note1" Rows="3" Width="660" Text='<%# Eval("Note1")%>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Note2
                                </td>
                                <td colspan="5">
                                    <dxe:ASPxMemo runat="server" ID="txt_Note2" Rows="3" Width="660" Text='<%# Eval("Note2")%>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Note3
                                </td>
                                <td colspan="5">
                                    <dxe:ASPxMemo runat="server" ID="txt_Note3" Rows="3" Width="660" Text='<%# Eval("Note3")%>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Note4
                                </td>
                                <td colspan="5">
                                    <dxe:ASPxMemo runat="server" ID="txt_Note4" Rows="3" Width="660" Text='<%# Eval("Note4")%>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Detail" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e){
                                grid_Det1.AddNewRow();
                            }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <%--<td colspan="2">
                                                <dxe:ASPxButton ID="ASPxButton5" runat="server" Width="100" Text="Standard Rate" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e){
                               ImportStdRate();
                            }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td colspan="2">
                                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Width="180" Text="Add Item From Quote" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e){
                               ImportDet();
                            }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="ASPxButton6" runat="server" Text="Print" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e){
                                PrintQuote(txt_DocNo.GetText());
                            }" />
                                                </dxe:ASPxButton>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="800">
                            <tr>
                                <td colspan="6">
                                    <dxwgv:ASPxGridView ID="grid_Det1" ClientInstanceName="grid_Det1" runat="server"
                                        DataSourceID="dsSeaQuotationDet1" KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect"
                                        OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting"
                                        OnRowUpdating="grid_Cost_RowUpdating" OnRowInserted="grid_Cost_RowInserted" OnRowUpdated="grid_Cost_RowUpdated" OnHtmlEditFormCreated="grid_Cost_HtmlEditFormCreated" OnRowDeleting="grid_Cost_RowDeleting">
                                        <SettingsBehavior ConfirmDelete="True" />
                                        <SettingsEditing Mode="EditFormAndDisplayRow" />
                                        <Columns>
                                            <dxwgv:GridViewDataColumn Caption="#" Width="70" VisibleIndex="0">
                                                <DataItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Det1.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Det1.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DataItemTemplate>
                                            </dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgCodeDes" VisibleIndex="2">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataSpinEditColumn Caption="Cost Qty" FieldName="CostQty" VisibleIndex="3" Width="50">
                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                                            </dxwgv:GridViewDataSpinEditColumn>
                                            <dxwgv:GridViewDataSpinEditColumn Caption="Cost Price" FieldName="CostPrice" VisibleIndex="4" Width="50">
                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                            </dxwgv:GridViewDataSpinEditColumn>
                                            <dxwgv:GridViewDataSpinEditColumn Caption="Cost" FieldName="CostLocAmt" VisibleIndex="5" Width="50">
                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                            </dxwgv:GridViewDataSpinEditColumn>
                                            <dxwgv:GridViewDataSpinEditColumn Caption="Sale Qty" FieldName="SaleQty" VisibleIndex="13" Width="50">
                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                                            </dxwgv:GridViewDataSpinEditColumn>
                                            <dxwgv:GridViewDataSpinEditColumn Caption="Sale Price" FieldName="SalePrice" VisibleIndex="14" Width="50">
                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                            </dxwgv:GridViewDataSpinEditColumn>
                                            <dxwgv:GridViewDataSpinEditColumn Caption="Sale" FieldName="SaleLocAmt" VisibleIndex="15" Width="50">
                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                            </dxwgv:GridViewDataSpinEditColumn>

                                        </Columns>
                                        <Templates>
                                            <EditForm>
                                                <div style="display: none">
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td>Charge Code
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_CostChgCode" ClientInstanceName="txt_CostChgCode" Width="100" runat="server" Text='<%# Bind("ChgCode")%>' ReadOnly='True'>
                                                                <Buttons>
                                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupChgCode(txt_CostChgCode,txt_CostDes);
                        }" />
                                                            </dxe:ASPxButtonEdit>

                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox Width="200" ID="txt_CostDes" ClientInstanceName="txt_CostDes" BackColor="Control"
                                                                ReadOnly="true" runat="server" Text='<%# Bind("ChgCodeDes") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Split Type
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cbo_SplitType" runat="server" Text='<%# Bind("SplitType")%>' Width="100">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Set" Value="Set" />
                                                                    <dxe:ListEditItem Text="WtM3" Value="WtM3" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="4">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="spin_CostRmk" Text='<%# Bind("Remark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td></td>
                                                                    <td>Qty
                                                                    </td>
                                                                    <td>Price
                                                                    </td>
                                                                    <td>Currency
                                                                    </td>
                                                                    <td>ExRate
                                                                    </td>
                                                                    <td>Amount
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Sale
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostSaleQty"
                                                                            ID="spin_CostSaleQty" Text='<%# Bind("SaleQty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostSalePrice"
                                                                            runat="server" Width="100" ID="spin_CostRevPrice" Value='<%# Bind("SalePrice")%>' Increment="0" DecimalPlaces="2">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="cmb_CostSaleCurrency" ClientInstanceName="cmb_CostSaleCurrency" runat="server" Width="80" Text='<%# Bind("SaleCurrency") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostSaleCurrency,null);
                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                                            DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostSaleExRate" ClientInstanceName="spin_CostSaleExRate" Text='<%# Bind("SaleExRate")%>' Increment="0">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostSaleAmt"
                                                                            BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostSaleAmt"
                                                                            Value='<%# Bind("SaleLocAmt")%>'>
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Cost
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostCostQty"
                                                                            ID="spin_CostCostQty" Text='<%# Bind("CostQty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostPrice"
                                                                            runat="server" Width="100" ID="spin_CostCostPrice" Value='<%# Bind("CostPrice")%>' Increment="0" DecimalPlaces="2">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" runat="server" Width="80" Text='<%# Bind("CostCurrency") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostCostCurrency,null);
                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                                            DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostExRate" ClientInstanceName="spin_CostCostExRate" Text='<%# Bind("CostExRate")%>' Increment="0">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostAmt"
                                                                            BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
                                                                            Value='<%# Bind("CostLocAmt")%>'>
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                </div>
                                            </EditForm>
                                        </Templates>
                                    </dxwgv:ASPxGridView>
                                </td>
                            </tr>

                        </table>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_Det2Add" runat="server" Text="Add Item" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                grid_Det2.AddNewRow();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxwgv:ASPxGridView ID="grid_Det2" ClientInstanceName="grid_Det2" runat="server" DataSourceID="dsSeaQuotationDet2" KeyFieldName="Id" Width="100%" EnableRowsCache="False" AutoGenerateColumns="false" OnInit="grid_Det2_Init" OnRowDeleted="grid_RowDeleted" OnRowInserting="grid_Det2_RowInserting" OnBeforePerformDataSelect="grid_Det2_BeforePerformDataSelect" OnRowDeleting="grid_Det2_RowDeleting" OnInitNewRow="grid_Det2_InitNewRow">
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <SettingsBehavior ConfirmDelete="true" />
                                        <SettingsEditing Mode="Inline" />
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn VisibleIndex="0">
                                                <EditButton Visible="true" />
                                                <DeleteButton Visible="true" />
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataColumn FieldName="Term" VisibleIndex="2" Width="100" />
                                            <dxwgv:GridViewDataColumn FieldName="Description" VisibleIndex="3" Width="300">
                                                <EditItemTemplate>
                                                    <%--<dxe:ASPxTextBox ID="txt_inner_term" runat="server" Text='<%# Bind("Term") %>'></dxe:ASPxTextBox>--%>
                                                    <dxe:ASPxMemo ID="txt_inner_des" runat="server" Rows="4" Width="300" Text='<%# Bind("Description") %>'></dxe:ASPxMemo>
                                                </EditItemTemplate>

                                            </dxwgv:GridViewDataColumn>
                                        </Columns>
                                    </dxwgv:ASPxGridView>


                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="800" EnableViewState="False">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
