<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="PurchaseOrderEdit.aspx.cs" Inherits="WareHouse_PurchaseOrders_PurchaseOrderEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Purchase Order </title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript" >
          var isUpload = false;
          function ShowHouse(masterId) {
              window.location = "PurchaseOrderReceiptEdit.aspx?no=" + masterId;
          }
          function AddProduct() {
              popubCtr1.SetHeaderText('Product');
              //popubCtr.SetContentUrl('ArInvoiceList.aspx?id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
              popubCtr1.SetContentUrl('../SelectPage/AddProducts.aspx?party=' + txt_PartyId.GetText() + "&no=" + txt_PoNo.GetText());
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
              var packQty = parseFloat(spin_det_Qty1.GetText());
              var locaQty = parseFloat(spin_det_Qty2.GetText());
              var totalQty = FormatNumber(packQty * locaQty, 2);
              spin_det_Qty.SetNumber(totalQty);

              var exRate = parseFloat(spin_det_ExRate.GetText());
              var qty = parseFloat(spin_det_Qty.GetText());
              var price = parseFloat(spin_det_Price.GetText());
              var gst = parseFloat(spin_det_GstP.GetText());

              var amt = FormatNumber(qty * price, 2);
              var gstAmt = FormatNumber(amt * gst, 2);
              var docAmt = parseFloat(amt) + parseFloat(gstAmt);
              var locAmt = FormatNumber(docAmt * exRate, 2);
              spin_det_LocAmt.SetNumber(locAmt);
          }
    </script>
</head>
<body>
    <wilson:DataSource ID="dsWhPo" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhPo" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsPoDet" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhPODet" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsReceipt" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhPOReceiptDet" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
    <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
    <wilson:DataSource ID="dsAttachment" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhAttachment" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsGstType" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XXGstType" KeyMember="SequenceId"  />
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>PO No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_PoNo" Width="150" runat="server" ClientInstanceName="txt_RefPoNo">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='PurchaseOrderEdit.aspx?no='+txt_RefPoNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='PurchaseOrderList.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='PurchaseOrderEdit.aspx?no=0';
                                            }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_WhPo" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" OnInit="grid_WhPo_Init"  OnCustomDataCallback="grid_WhPo_CustomDataCallback" DataSourceID="dsWhPo" OnCustomCallback="grid_WhPo_CustomCallback" OnHtmlEditFormCreated="grid_WhPo_HtmlEditFormCreated" OnInitNewRow="grid_WhPo_InitNewRow">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 900px">
                                <tr>
                                    <td width="70%">
                                    </td>
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
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('CloseJob',OnCloseCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_AddInvoice" Width="140" runat="server" Text="Create Ap Invoice" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('Invoice',OnCloseCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Void" ClientInstanceName="btn_Void" runat="server" Width="100" Text="Void" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_Void.GetText()+' Job?'))
                                                                    {
                                                                        detailGrid.GetValuesOnCustomCallback('Void',OnCloseCallBack);                 
                                                                    }
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="980px" Height="300px">
                                <TabPages>
                                    <dxtc:TabPage Text="Purchase Order" Visible="true">
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
                                                        <td width="100">PO No
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="150" ID="txt_PoNo" ClientInstanceName="txt_PoNo" Text='<%# Eval("PoNo")%>' ReadOnly="True" BackColor="Control">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="100">Supplier RefNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_PartyRefNo" runat="server" ClientInstanceName="txt_PartyRefNo" Width="165px" Text='<%# Eval("PartyRefNo")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Po Date </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="txt_PoDate" runat="server" Value='<%# Eval("PoDate") %>' Width="120px" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Supplier
                                                        </td>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" runat="server" Text='<%# Eval("PartyId")%>' Width="150" HorizontalAlign="Left" AutoPostBack="False">
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
                                                        <td>Promise Date</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="txt_PromiseDate" runat="server" Value='<%# Eval("PromiseDate") %>' Width="120px" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
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
                                                                        <dxe:ASPxButtonEdit ID="txt_WarehouseId" ClientInstanceName="txt_WarehouseId" runat="server" Text='<%# Eval("WarehouseId") %>' Width="150px" HorizontalAlign="Left" AutoPostBack="False">
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
                                                         <td>Doc Amt</td>
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
                                                            <dxe:ASPxButtonEdit ID="txt_SalesmanId" ClientInstanceName="txt_SalesmanId" runat="server" Height="16px" Text='<%# Eval("SalesmanId") %>' Width="150px" HorizontalAlign="Left" AutoPostBack="False">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupVendor(null,txt_SalesmanId);
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
                                                                </tr>
                                                            </table>
                                                            <hr>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Receipt">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <dxwgv:ASPxGridView ID="Grid_Receipt" runat="server" OnBeforePerformDataSelect="Grid_Receipt_BeforePerformDataSelect"
                                                    OnInit="Grid_Receipt_Init" DataSourceID="dsReceipt" KeyFieldName="Id" Width="900px" AutoGenerateColumns="False">
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                                            <DataItemTemplate>
                                                                <a onclick="ShowHouse('<%# Eval("ReceiptNo") %>');">Edit</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Id" FieldName="Id" VisibleIndex="1"
                                                            SortIndex="1" SortOrder="Ascending" Width="30" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Receipt No" FieldName="ReceiptNo" VisibleIndex="1"
                                                            SortIndex="1" SortOrder="Ascending" Width="150">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DateTime" FieldName="CreateDateTime" VisibleIndex="1"
                                                            SortIndex="1">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Product" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="LocCode" VisibleIndex="4" Width="30">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Billing">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice_Import, "WH", txt_PoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import, "WH", txt_PoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import, "WH", txt_PoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Invoice_Import" ClientInstanceName="Grid_Invoice_Import"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsInvoice" Width="100%"
                                                    OnBeforePerformDataSelect="Grid_Invoice_Import_DataSelect">
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowInvoice(Grid_Invoice_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintInvoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="LocAmt" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddVoucher" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"  %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import, "WH", txt_PoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddPayable" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"  %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import, "WH", txt_PoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Payable_Import" ClientInstanceName="Grid_Payable_Import"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsVoucher" Width="100%"
                                                    OnBeforePerformDataSelect="Grid_Payable_Import_DataSelect">
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowPayable(Grid_Payable_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintPayable("<%# Eval("DocNo")%>","<%# Eval("DocType") %>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="LocAmt" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
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
                                                                <dxe:ASPxButton ID="ASPxButton13" Width="100%" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' runat="server" Text="Upload Attachments"
                                                                    AutoPostBack="false"
                                                                    UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s,e) {
                                                                         isUpload=true;
                                                        PopupUploadPhotoPO();
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
                                                                        <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' >
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
                               
                            </dxtc:ASPxPageControl>
                        </div>
                        <table>
                            <tr>
                                <td style="text-align: right; padding: 2px 2px 2px 2px"></td>
                                <td style="text-align: right; padding: 2px 2px 2px 2px">
                                    <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Det" AutoPostBack="false" UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
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
                                    <dxwgv:ASPxGridView ID="grid_PoDet" ClientInstanceName="grid_det"
                                        runat="server" DataSourceID="dsPoDet" KeyFieldName="Id"
                                        OnBeforePerformDataSelect="grid_PoDet_BeforePerformDataSelect" OnRowUpdating="grid_PoDet_RowUpdating"
                                        OnRowInserting="grid_PoDet_RowInserting" OnInitNewRow="grid_PoDet_InitNewRow" OnInit="grid_PoDet_Init" OnRowDeleting="grid_PoDet_RowDeleting"
                                        OnRowInserted="grid_PoDet_RowInserted" OnRowUpdated="grid_PoDet_RowUpdated" OnRowDeleted="grid_PoDet_RowDeleted" Width="100%"
                                        AutoGenerateColumns="False">
                                        <SettingsEditing Mode="EditForm" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <SettingsBehavior ConfirmDelete="true" />
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
                                                        Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_det.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                              <dxwgv:GridViewDataTextColumn
                                                Caption="Status" FieldName="StatusCode" VisibleIndex="7" Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFooter="true" />
                                        <TotalSummary>
                                            <dxwgv:ASPxSummaryItem
                                                FieldName="LocAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                        </TotalSummary>
                                        <Templates>
                                            <EditForm>
                                                <div style="display: none">
                                                    <dxe:ASPxTextBox ID="txt_det_Id" runat="server" ClientInstanceName="txt_det_GstType" Text='<%# Eval("Id") %>' Width="40"></dxe:ASPxTextBox>
                                                    <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_GstAmt"
                                                        ClientInstanceName="spin_det_GstAmt" runat="server" Value='<%# Eval("GstAmt") %>'>
                                                    </dxe:ASPxSpinEdit>
                                                    <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_DocAmt"
                                                        ClientInstanceName="spin_det_DocAmt" runat="server" Value='<%# Eval("DocAmt") %>'>
                                                    </dxe:ASPxSpinEdit>
                                                </div>
                                                <table style="border-bottom: solid 1px black; width: 100%">
                                                    <tr>
                                                        <td>Product</td>
                                                        <td width="60">
                                                            <dxe:ASPxButtonEdit ID="txt_det_Product" ClientInstanceName="txt_det_Product" runat="server" Value='<%# Bind("Product") %>' Width="120">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                      PopupProductPO(txt_det_Product,null);
                                                                   }" />
                                                            </dxe:ASPxButtonEdit>

                                                        </td>
                                                        <td colspan="6">
                                                            <table>
                                                                <tr>
                                                                    <td width="60">PackQty</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Qty1" NumberType="Integer"
                                                                            ClientInstanceName="spin_det_Qty1" runat="server" Value='<%# Bind("Qty1") %>'>
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td style="width:30px; text-align:center;">x</td>
                                                                    <td>UnitQty</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Qty2" NumberType="Integer"
                                                                            ClientInstanceName="spin_det_Qty2" runat="server" Value='<%# Bind("Qty2") %>'>
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td style="width:20px; text-align:center;">=</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Qty" NumberType="Integer" ReadOnly="true"
                                                                            ClientInstanceName="spin_det_Qty" runat="server" Value='<%# Bind("Qty") %>'>
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Qty</td>
                                                                </tr>
                                                            </table>
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

                                                    </tr>
                                                    <tr>
                                                        <td>BatchNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_BatchNo" Width="120" runat="server" Text='<%# Bind("BatchNo")%>'></dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="60">Price</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Price" ClientInstanceName="spin_det_Price"
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ReadOnly="false" runat="server" Text='<%# Bind("Price") %>'>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutAmt(); 
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td width="50">ExRate </td>
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
                                                                                                                <td>LocAmt</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="80" ID="spin_det_LocAmt"
                                                                ClientInstanceName="spin_det_LocAmt" DisplayFormatString="0.00" ReadOnly="false" BackColor="Control"
                                                                runat="server" Text='<%# Eval("LocAmt") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
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
                                                            <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'>
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
                AllowResize="True" Width="800" EnableViewState="False">
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
