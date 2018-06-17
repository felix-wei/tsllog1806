﻿<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="SalesOrderEdit.aspx.cs" Inherits="WareHouse_SalesOrders_SalesOrderEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sales Orders</title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript" >
          var isUpload = false;
          function AddProduct() {
              popubCtr1.SetHeaderText('Product');
              //popubCtr.SetContentUrl('ArInvoiceList.aspx?id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
              popubCtr1.SetContentUrl('../SelectPage/AddProductFromReceipt.aspx?wh=' + txt_WarehouseId.GetText() + "&no=" + txt_SoNo.GetText());
              popubCtr1.Show();
          }
          function AfterPopubMultiInv() {
              popubCtr1.Hide();
              popubCtr1.SetContentUrl('about:blank');
              grid_det.Refresh();
          }
          function RowClickHandler(s, e) {
              SetLookupKeyValue(e.visibleIndex);
              DropDownEdit.HideDropDown();
          }
          function SetLookupKeyValue(rowIndex) {
              DropDownEdit.SetText(GridView.cpCode[rowIndex]);
              spin_det_GstP.SetText(GridView.cpGstValue[rowIndex]);
          }
          function PutAmt() {
              var amt = Calculate(spin_det_Qty.GetText(), spin_det_Price.GetText(), 1, 2);
              var gstAmt = Calculate(amt, spin_det_GstP.GetText(), spin_det_ExRate.GetText(), 2);
              var docAmt = Calculate(amt, spin_det_ExRate.GetText(), 1, 2);
              var locAmt = parseFloat(gstAmt) + parseFloat(docAmt);

              var packQty = parseFloat(spin_det_Qty1.GetText());
              var locaQty = parseFloat(spin_det_Qty2.GetText());
              var totalQty = FormatNumber(packQty * locaQty, 2);
              spin_det_Qty.SetNumber(totalQty);

              spin_det_GstAmt.SetNumber(gstAmt);
              spin_det_DocAmt.SetNumber(docAmt);
              spin_det_LocAmt.SetNumber(locAmt);
          }
    </script>
</head>
<body>
    <wilson:DataSource ID="dsWhSo" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhSo" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsSoDet" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhSoDet" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsAttachment" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhAttachment" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsGstType" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XXGstType" KeyMember="SequenceId"  />
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>SO No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SoNo" Width="150" runat="server" ClientInstanceName="txt_RefSoNo">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='SalesOrderEdit.aspx?no='+txt_RefSoNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='SalesOrderList.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='SalesOrderEdit.aspx?no=0';
                                            }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" OnInit="grid_Init" OnCustomDataCallback="grid_CustomDataCallback" DataSourceID="dsWhSo" OnCustomCallback="grid_CustomCallback" OnHtmlEditFormCreated="grid_HtmlEditFormCreated" OnInitNewRow="grid_InitNewRow">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 900px">
                                <tr>
                                    <td width="70%"></td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                <ClientSideEvents Click="function(s,e) {
                                            detailGrid.PerformCallback('Save');
                                            }" />
                                            </dxe:ASPxButton>
                                        </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_CloseJob" Width="90" runat="server" Text="Close Job" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL"%>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('CloseJob',OnCloseCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="display:none">
                                        <dxe:ASPxButton ID="btn_VoidMaster" ClientInstanceName="btn_VoidMaster" runat="server" Width="100" Text="Void" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_VoidMaster.GetText()+' Master?'))
                                                                    {
                                                                        detailGrid.GetValuesOnCustomCallback('VoidMaster',OnCloseCallBack);                 
                                                                    }
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="980px" Height="300px">
                                <TabPages>
                                    <dxtc:TabPage Text="Sales Order" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                                <div style="display: none">
                                                    <dxe:ASPxTextBox runat="server" Width="60" ID="txt_Id" ClientInstanceName="txt_Id"
                                                        Text='<%# Eval("Id") %>'>
                                                    </dxe:ASPxTextBox>
                                                </div>
                                                <table border="0" width="900">
                                                    <tr>
                                                        <td width="100"></td>
                                                        <td width="180"></td>
                                                        <td width="100"></td>
                                                        <td width="180"></td>
                                                        <td width="100"></td>
                                                        <td width="180"></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100">SO No
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="150" ID="txt_SoNo" ReadOnly="True" BackColor="Control" ClientInstanceName="txt_SoNo" Text='<%# Eval("SoNo")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="100">Customer RefNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_PartyRefNo" runat="server" ClientInstanceName="txt_PartyRefNo" Width="165px" Text='<%# Eval("PartyRefNo")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>So Date</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="txt_SoDate" runat="server" Value='<%# Eval("SoDate") %>' Width="120px" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                         <td>Customer
                                                        </td>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" Text='<%# Eval("PartyId")%>' runat="server" Width="150" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupCust(txt_PartyId,txt_PartyName);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="305px" BackColor="Control" ID="txt_PartyName"
                                                                            ReadOnly="true" ClientInstanceName="txt_PartyName">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>Request Date</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="txt_RequestDate" runat="server" Value='<%# Eval("RequestDate") %>' Width="120px" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                            
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>Warehouse
                                                        </td>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_WarehouseId" ClientInstanceName="txt_WarehouseId" Text='<%# Eval("WarehouseId") %>' runat="server" Width="150px" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                    PopupWh(txt_WarehouseId,txt_WhName);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_WhName" runat="server" BackColor="Control" ClientInstanceName="txt_WhName" ReadOnly="True" Width="305px">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                         <td>Doc Amt
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" IDecimalPlaces="2" Increment="0" ReadOnly="true" runat="server" BackColor="Control" Width="120px" ID="spin_DocAmt" Text='<%# Eval("DocAmt")%>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sales man
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_SalesmanId" ClientInstanceName="txt_SalesmanId" Text='<%# Eval("SalesmanId") %>' runat="server" Width="150px" HorizontalAlign="Left" AutoPostBack="False">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCust(null,txt_SalesmanId);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                       <td  colspan="2">
                                                            <table>
                                                                <tr>
                                                                    <td>Currency
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Eval("Currency") %>' runat="server" Width="60px" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_ExRate);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>Ex Rate
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit ID="spin_ExRate" ClientInstanceName="spin_ExRate" DisplayFormatString="0.000000"
                                                                            runat="server" Width="95" Value='<%# Eval("ExRate")%>' DecimalPlaces="6" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                         <td>Loc Amt</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" IDecimalPlaces="2" Increment="0" ReadOnly="true" runat="server" BackColor="Control" Width="120px" ID="spin_LocAmt"
                                                                Text='<%# Eval("LocAmt")%>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="txt_Remark" Rows="3" runat="server" Width="465" Text='<%# Eval("Remark") %>'></dxe:ASPxMemo>
                                                        </td>
                                                         <td></td>
                                                        <td></td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="6">
                                                            <hr>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 80px;">Creation</td>
                                                                    <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                    <td style="width: 100px;">Last Updated</td>
                                                                    <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                    <td style="width: 80px;">Job Status</td>
                                                                    <td style="width: 160px">
                                                                        <dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text="" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <hr>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl8" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <td>
                                                                <dxe:ASPxButton ID="ASPxButton13" Width="100%" runat="server" Text="Upload Attachments"
                                                                    AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' 
                                                                    UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s,e) {
                                                                         isUpload=true;
                                                        PopupUploadPhotoSO();
                                                        }" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td>
                                                        <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                                            UseSubmitBehavior="false" Enabled='<%#SafeValue.SafeString(Eval("Id"),"0")!="0"  %>'>
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
                                                                Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                                                        <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>' >
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
                                <ClientSideEvents ActiveTabChanged="function(s, e) { 
                                var tit=s.GetActiveTab().GetText();
                                if(tit=='Attachments1')
                                    detailGrid.PerformCallback('Photo');
                                    }" />
                            </dxtc:ASPxPageControl>
                        </div>
                        <table>
                            <tr>
                                <td style="text-align: right; padding: 2px 2px 2px 2px"></td>
                                <td style="text-align: right; padding: 2px 2px 2px 2px">
                                    <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Det" AutoPostBack="false" UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" &&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'> 
                                        <ClientSideEvents Click="function(s,e){
                                         grid_det.AddNewRow();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="text-align: right; padding: 2px 2px 2px 2px">
                                    <dxe:ASPxButton ID="btn_MulitplePick" runat="server" Text="Mulitple Pick" AutoPostBack="false" UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                        <ClientSideEvents Click="function(s,e){
                                         AddProduct();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <table width="900">
                            <tr>
                                <td>
                                    <dxwgv:ASPxGridView ID="grid_det" ClientInstanceName="grid_det"
                                        runat="server" DataSourceID="dsSoDet" KeyFieldName="Id"
                                        OnBeforePerformDataSelect="grid_det_BeforePerformDataSelect" OnRowUpdating="grid_det_RowUpdating"
                                        OnRowInserting="grid_det_RowInserting" OnInitNewRow="grid_det_InitNewRow" OnInit="grid_det_Init" OnRowDeleting="grid_det_RowDeleting"
                                        OnRowInserted="grid_det_RowInserted" OnRowUpdated="grid_det_RowUpdated" OnRowDeleted="grid_det_RowDeleted" Width="100%"
                                        AutoGenerateColumns="False">
                                        <SettingsEditing Mode="EditForm" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <SettingsCustomizationWindow Enabled="True" />
                                        <SettingsBehavior ConfirmDelete="True" />
                                        <Columns>
                                          <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                <DataItemTemplate>
                                                    <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                        ClientSideEvents-Click='<%# "function(s) { grid_det.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                    </dxe:ASPxButton>
                                                </DataItemTemplate>
                                            </dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                <DataItemTemplate>
                                                    <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                        Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_det.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                    </dxe:ASPxButton>
                                                </DataItemTemplate>
                                            </dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataTextColumn
                                                Caption="Product" FieldName="Product" VisibleIndex="3" Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn
                                                Caption="Price" FieldName="Price" VisibleIndex="3" Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn
                                                Caption="Qty" FieldName="Qty" VisibleIndex="5" Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                             <dxwgv:GridViewDataTextColumn
                                                Caption="Balance Qty" FieldName="BalQty" VisibleIndex="5" Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn
                                                Caption="Currency" FieldName="Currency" VisibleIndex="5" Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn
                                                Caption="Gst Type" FieldName="GstType" VisibleIndex="5" Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn
                                                Caption="Line Amt" FieldName="LocAmt" VisibleIndex="6" Width="80">
                                                <PropertiesTextEdit
                                                    DisplayFormatString="{0:#,##0.00}">
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFooter="true" />
                                        <TotalSummary>
                                            <dxwgv:ASPxSummaryItem
                                                FieldName="LocAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                        </TotalSummary>
                                        <Templates>
                                            <EditForm>
                                                 <div style="display:none">
                                                     <dxe:ASPxTextBox ID="txt_det_Id" runat="server" ClientInstanceName="txt_det_Id" Text='<%# Eval("Id") %>' Width="40"></dxe:ASPxTextBox>
                                                     <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_GstAmt"
                                                         ClientInstanceName="spin_det_GstAmt" runat="server" Value='<%# Eval("GstAmt") %>'>
                                                     </dxe:ASPxSpinEdit>
                                                     <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_DocAmt"
                                                         ClientInstanceName="spin_det_DocAmt" runat="server" Value='<%# Eval("DocAmt") %>'>
                                                     </dxe:ASPxSpinEdit>
                                                </div>
                                                <table style="border-bottom: solid 1px black; width:100%">
                                                    <tr>
                                                        <td>Product</td>
                                                        <td width="60">
                                                            <dxe:ASPxButtonEdit ID="txt_det_Product" ClientInstanceName="txt_det_Product" runat="server" Value='<%# Bind("Product") %>' Width="120">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                      PopupProductSO(txt_det_Product,txt_PORDetId);
                                                                   }" />
                                                            </dxe:ASPxButtonEdit>

                                                        </td>
                                                        <td>PackQty</td>
                                                        <td><dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Qty1"
                                                                ClientInstanceName="spin_det_Qty1" runat="server" Value='<%# Bind("Qty1") %>'>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit></td>
                                                        <td>Qty </td>
                                                        <td width="60">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Qty"
                                                                ClientInstanceName="spin_det_Qty" runat="server" Value='<%# Bind("Qty") %>'>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td width="60" style="display: none">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_BalQty"
                                                                ClientInstanceName="spin_det_BalQty" runat="server" Value='<%# Bind("BalQty") %>'>

                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>

                                                        <td>Currency</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_det_Currency" Width="60" ClientInstanceName="txt_det_Currency" MaxLength="3" runat="server" Value='<%# Bind("Currency") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                           PopupCurrency(txt_det_Currency,spin_det_ExRate);
                        }" />
                                                            </dxe:ASPxButtonEdit>

                                                        </td>
                                                        <td>Gst</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_GstP"
                                                                ClientInstanceName="spin_det_GstP" runat="server" Value='<%# Bind("Gst") %>'
                                                                DisplayFormatString="0.00">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                   PutAmt(); 
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>LocAmt</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="80" ID="spin_det_LocAmt"
                                                                ClientInstanceName="spin_det_LocAmt" DisplayFormatString="0.00" ReadOnly="false" BackColor="Control"
                                                                runat="server" Text='<%# Eval("LocAmt") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>BatchNo</td>
                                                        <td><dxe:ASPxTextBox ID="txt_BatchNo" Width="120" runat="server" Text='<%# Bind("BatchNo")%>'></dxe:ASPxTextBox> </td>
                                                         <td>LocaQty</td>
                                                        <td><dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Qty2"
                                                                ClientInstanceName="spin_det_Qty2" runat="server" Value='<%# Bind("Qty2") %>'>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit></td>
                                                        <td>Price</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Price" ClientInstanceName="spin_det_Price"
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ReadOnly="false" runat="server" Text='<%# Bind("Price") %>'>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>ExRate </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="70" ID="spin_det_ExRate"
                                                                ClientInstanceName="spin_det_ExRate"
                                                                runat="server" Value='<%# Bind("ExRate") %>' DisplayFormatString="0.000000" DecimalPlaces="6">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>GstType</td>
                                                        <td>
                                                            <dxe:ASPxDropDownEdit ID="DropDownEdit" runat="server" ClientInstanceName="DropDownEdit"
                                                                Text='<%# Bind("GstType") %>' Width="60px" AllowUserInput="True">
                                                                <DropDownWindowTemplate>
                                                                    <dxwgv:ASPxGridView ID="gridGstType" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                                                                        Width="100px" DataSourceID="dsGstType" KeyFieldName="SequenceId" OnCustomJSProperties="gridGstType_CustomJSProperties">
                                                                        <Columns>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="SequenceId" VisibleIndex="0" Visible="false">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="Code" VisibleIndex="0">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="GstValue" VisibleIndex="1">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <ClientSideEvents RowClick="RowClickHandler" />
                                                                    </dxwgv:ASPxGridView>
                                                                </DropDownWindowTemplate>
                                                            </dxe:ASPxDropDownEdit>
                                                        </td>
                                                        <td>Status</td>
                                                        <td style="width: 80px">
                                                            <dxe:ASPxComboBox ID="cmb_Status" ClientInstanceName="cmb_Status" runat="server" Value='<%# Eval("StatusCode") %>'
                                                                Width="80" DropDownWidth="100" DropDownStyle="DropDownList"
                                                                ValueType="System.String" EnableCallbackMode="true"
                                                                EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                                                                <Items>
                                                                    <dxe:ListEditItem Value="Draft" Text="Draft" Selected="true" />
                                                                    <dxe:ListEditItem Value="Waiting" Text="Waiting" />
                                                                    <dxe:ListEditItem Value="Delivered" Text="Delivered" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td colspan="11" style="width:100%">
                                                            <dxe:ASPxMemo ID="memo_Remrk" Width="99%" runat="server" Text='<%# Bind("Remark") %>'></dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="20"
                                                            style="text-align: right; padding: 2px 2px 2px 2px">
                                                           <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>' >
                                                                    <ClientSideEvents Click="function(s,e){grid_det.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton1" ReplacementType="EditFormCancelButton"
                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EditForm>
                                        </Templates>
                                    </dxwgv:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
                 <ClientSideEvents CloseUp="function(s, e) {
      if(isUpload)
	    grd_Photo.Refresh();
}" />
            </dxpc:ASPxPopupControl>
             <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="980" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    detailGrid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>

    </form>
</body>
</html>