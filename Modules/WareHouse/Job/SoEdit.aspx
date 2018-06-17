<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="SoEdit.aspx.cs" Inherits="WareHouse_Job_SoEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SO Edit</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript">
        var isUpload = false;
    </script>
    <script type="text/javascript">
        function PopupUploadPhoto() {
            popubCtr.SetHeaderText('Upload Attachment');
            popubCtr.SetContentUrl('../Upload.aspx?Type=SO&Sn=' + txt_DoNo.GetText());
            popubCtr.Show();
        }
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert("Please enter the Customer");
            }

            else if (v == "") {
                detailGrid.Refresh();
            }
            else if (v == "No Price") {
                alert("No Price,Can not Confirm");
            }
            else if (v != null) {
                window.location = 'SoEdit.aspx?no=' + v;
                //txt_SchRefNo.SetText(v);
                //txt_DoNo.SetText(v);
            }
        }
        function OnCloseCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                detailGrid.Refresh();
            }
            if (v == "Save") {
                detailGrid.Refresh();
            }
            else if (v == "Billing")
                alert("Have Billing, Can't void!");
            else if (v == "BalQty")
                alert("Not Receiving, Can't void!");
            else if (v == "Fail")
                alert("Action Fail,please try again!");
            else if (v == "NoMatch")
                alert("EST Amount is difference with Actaul amount,please check again");
            else if (v == "NotClose") {
                alert("Have Not Delivered, Can't close!");
            }
            else if (v != null && v != "Success") {
                window.location = 'SoEdit.aspx?no=' + v;
            }
        }
        function OnTransferCallBack(v) {
            if (v == "Fail")
                alert("Create DO out Fail");
            else if (v == "No Balance Qty or No Lot No!")
                alert(v);
            else if (v == "NO SKU Line")
                alert(v);
            else if (v == "No Customer")
                alert("Please enter the Customer");
            else if (v != null) {
                alert("Do Out No is " + v);
                detailGrid.Refresh();
				grid_DoIn.Refresh();
            }

            //else if (v = "Success") {
            //    alert("Create Do Out  Success!");
            //}
        }
        function MultipleAdd() {
            popubCtr.SetHeaderText('SKU List');
            popubCtr.SetContentUrl('../selectpage/SelectPoductFromStock.aspx?Type=SO&Sn=' + txt_DoNo.GetText() + '&WhId=' + txt_WareHouseId.GetText() + "&partyId=" + txt_ConsigneeCode.GetText());
            popubCtr.Show();
        }
        function MultiplePickProduct() {
            popubCtr.SetHeaderText('Multiple Product');
            popubCtr.SetContentUrl('../SelectPage/MultipleProduct.aspx?Type=SO&Sn=' + txt_DoNo.GetText()+'&WhId='+txt_WareHouseId.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_SKULine.Refresh();
            grid_PoRequest.Refresh();
        }
        function PrintDo(doNo, doType) {
            if (doType == "SO")
                window.open('/Modules/WareHouse/PrintView.aspx?document=wh_So&master=' + doNo + "&house=0");
            else if (doType == "Inv")
                window.open('/Modules/WareHouse/PrintView.aspx?document=wh_inv&master=' + doNo + "&house=0");
        }

        function AddInvoice1(gridId, JobType, refNo, jobNo,docType) {
            grid = gridId;
            popubCtr1.SetHeaderText('Invoice');
            popubCtr1.SetContentUrl('../Account/ArInvoice.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo + '&DocType=' + docType);
            popubCtr1.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.LogEvent"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssue" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhTrans"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssueDet" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhTransDet"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XAArInvoice"
                KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XAApPayable"
                KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssuePhoto" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhAttachment"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPartyGroup" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXPartyGroup"
                KeyMember="Code" />
            <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
                KeyMember="Id" FilterExpression="CodeType='1'" />
            <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
                KeyMember="Id" FilterExpression="CodeType='2'" />
            <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
                KeyMember="Id" FilterExpression="Type='Attribute'" />
            <wilson:DataSource ID="dsDoIn" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhDo" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPoRequest" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhPORequest" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsReceipt" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAArReceiptDet" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefWarehouse" KeyMember="Id" />
             <wilson:DataSource ID="dsWhPacking" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhPacking" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhCosting" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>SO No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SchRefNo" Width="150" ClientInstanceName="txt_SchRefNo" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='SoEdit.aspx?no='+txt_SchRefNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='SoEdit.aspx?no=0';
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='SoList.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton12" ClientInstanceName="btn_QuickOrder" runat="server" Width="120" Text="Quick Order" AutoPostBack="False"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e) {
                                window.location='EcOrderStaff.aspx';
                                          
                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>

            <dxwgv:ASPxGridView ID="grid_Issue" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="false" DataSourceID="dsIssue"
                OnBeforePerformDataSelect="grid_Issue_DataSelect" OnInitNewRow="grid_Issue_InitNewRow"
                OnInit="grid_Issue_Init" OnCustomDataCallback="grid_Issue_CustomDataCallback" OnCustomCallback="grid_Issue_CustomCallback"
                OnHtmlEditFormCreated="grid_Issue_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="text-align: left; padding: 2px 2px 2px 2px; width: 1050px">
                                <tr>
                                    <td width="100%">
                                        <table style="border: solid 1px black; padding: 2px 2px 2px 2px; width: 100%">
                                            <tr>
                                                <td>Print Document:&nbsp;&nbsp;
                                        <a href="#" onclick='PrintDo("<%# Eval("DoNo") %>","SO")'>SO</a>
                                                    &nbsp;&nbsp;
                                        <a href="#" onclick='PrintDo("<%# Eval("DoNo") %>","Inv")'>Proforma Invoice</a>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                            Text="Save" Enabled='<%# SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                                 }" />
                                        </dxe:ASPxButton>
                                    </td>
                                        <td>
                                        <dxe:ASPxButton ID="btn_CloseJob" ClientInstanceName="btn_CloseJob" runat="server" Width="80" Text="Close" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled"%>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                        detailGrid.GetValuesOnCustomCallback('Close',OnCloseCallBack);                 

                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                      <td>
                                        <dxe:ASPxButton ID="btn_ConfirmPI" Width="90" runat="server" Text="Confirm PI" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Delivered"  %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                               detailGrid.GetValuesOnCustomCallback('Confirm',OnTransferCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td >
                                        <dxe:ASPxButton ID="btn_CreatePO" Width="90" runat="server" Text="Create PO" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                               detailGrid.GetValuesOnCustomCallback('CreatePO',OnCloseCallBack);   
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                     <td style="display:none">
                                        <dxe:ASPxButton ID="btn_AddDoOut" ClientInstanceName="btn_AddDoOut" runat="server" Width="90" Text="Create DO" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                detailGrid.GetValuesOnCustomCallback('AddDoOut',OnTransferCallBack);                 
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_CreateBill" Width="90" runat="server" Text="Create Bill" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Delivered"  %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                AddInvoice1(Grid_Invoice, 'WH', txt_DoNo.GetText(), 'SO','IV');
                                                                
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                     <td style="display:none">
                                        <dxe:ASPxButton ID="btn_DropShipDo" Width="110" runat="server" Text="Drop Ship DO" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Delivered"  %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Void" ClientInstanceName="btn_Void" runat="server" Width="80" Text="Void" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Delivered" %>'>
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

                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                            <table>
                                <tr>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                </tr>
                                <tr>
                                    <td>No</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" BackColor="Control" ID="txt_DoNo" Text='<%# Eval("DoNo") %>' ClientInstanceName="txt_DoNo">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_IssueDate" Width="100%" runat="server" Value='<%# Eval("DoDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                <td>ExpectedDate
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_ExpectedDate" runat="server" Value='<%# Eval("ExpectedDate") %>' Width="100%" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                    <td>Status</td>
                                    <td>
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100%" ID="cmb_Status"
                                            runat="server" OnCustomJSProperties="cmb_Status_CustomJSProperties" Text='<%# Eval("DoStatus") %>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Draft" Value="Draft" />
                                                <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                                 <dxe:ListEditItem Text="Delivered" Value="Delivered" />
                                                <dxe:ListEditItem Text="Closed" Value="Closed" />
                                                <dxe:ListEditItem Text="Canceled" Value="Canceled" />

                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Customer</td>
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="0">
                                            <td width="94">
                                                <dxe:ASPxButtonEdit ID="txt_ConsigneeCode" ClientInstanceName="txt_ConsigneeCode" runat="server"
                                                    Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PartyId") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_ConsigneeCode,txt_ConsigneeName,null,null,txt_Country,txt_City,txt_PostalCode,memo_Address,'CV');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="270" ReadOnly="true" Text='<%# Eval("PartyName") %>' BackColor="Control" ID="txt_ConsigneeName" ClientInstanceName="txt_ConsigneeName">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </table>
                                    </td>
                                    <td>PIC
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Pic" runat="server" ClientInstanceName="txt_Pic" Text='<%# Eval("Pic") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2">Address</td>
                                    <td rowspan="2" colspan="3">
                                        <dxe:ASPxMemo ID="memo_Address" Rows="4" Width="100%" ClientInstanceName="memo_Address"
                                            runat="server" Text='<%# Eval("PartyAdd") %>'>
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Currency
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Eval("Currency") %>' runat="server" Width="100%" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_ExRate);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>ExRate
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_ExRate" ClientInstanceName="spin_ExRate" DisplayFormatString="0.000000"
                                            runat="server" Width="100%" Value='<%# Eval("ExRate")%>' DecimalPlaces="6" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>

                                </tr>
                                <tr>
                                    <td>Payment Term</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_PayTerm" runat="server" Width="100%" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("PayTerm")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="CASH" Value="CASH" />
                                                <dxe:ListEditItem Text="30DAYS" Value="30DAYS" />
                                                <dxe:ListEditItem Text="60DAYS" Value="60DAYS" />
                                                <dxe:ListEditItem Text="90DAYS" Value="90DAYS" />
                                                <dxe:ListEditItem Text="120DAYS" Value="120DAYS" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>INCO Term</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_IncoTerm" runat="server" Width="100%" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("IncoTerm")%>'>
                                            <Items>
                                                 <dxe:ListEditItem Text="CFR" Value="CFR" />
                                                <dxe:ListEditItem Text="CIF" Value="CIF" />
                                                <dxe:ListEditItem Text="CIP" Value="CIP" />
                                                <dxe:ListEditItem Text="CPT" Value="CPT" />
                                                <dxe:ListEditItem Text="DAF" Value="DAF" />
                                                <dxe:ListEditItem Text="DAP" Value="DAP" />
                                                <dxe:ListEditItem Text="DAT" Value="DAT" />
                                                <dxe:ListEditItem Text="DDP" Value="DDP" />
                                                <dxe:ListEditItem Text="DDU" Value="DDU" />
                                                <dxe:ListEditItem Text="DES" Value="DES" />
                                                <dxe:ListEditItem Text="DEQ" Value="DEQ" />
                                                <dxe:ListEditItem Text="EXW" Value="EXW" />
                                                <dxe:ListEditItem Text="FAS" Value="FAS" />
                                                <dxe:ListEditItem Text="FOB" Value="FOB" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="#" onclick="PopupPartyAdr(null,txt_CollectFrom);">Pick From</a>
                                    </td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_CollectFrom" Rows="5" Width="100%" ClientInstanceName="txt_CollectFrom"
                                            runat="server" Text='<%# Eval("CollectFrom") %>'>
                                        </dxe:ASPxMemo>
                                        <td><a href="#" onclick="PopupPartyAdr(null,txt_DeliveryTo);">Delivery To</a></td>
                                        <td colspan="3">
                                            <dxe:ASPxMemo ID="txt_DeliveryTo" Rows="5" Width="100%" ClientInstanceName="txt_DeliveryTo"
                                                runat="server" Text='<%# Eval("DeliveryTo") %>'>
                                            </dxe:ASPxMemo>
                                        </td>
                                </tr>
                               
                                <tr>
                                    <td rowspan="2">Remark</td>
                                    <td colspan="3" rowspan="2">
                                        <dxe:ASPxMemo runat="server" Width="100%" Rows="4" ID="txt_Remark" Text='<%# Eval("Remark") %>' ClientInstanceName="txt_Remark3">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>
                                     Supplier
                                    </td>
                                     <td colspan="3">
                                        <table cellpadding="0" cellspacing="0">
                                            <td width="94">
                                                <dxe:ASPxButtonEdit ID="txt_SupplierId" ClientInstanceName="txt_SupplierId" runat="server" Text='<%# Eval("SupplierId")%>' Width="100%" HorizontalAlign="Left" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                             PopupParty(txt_SupplierId,txt_SupplierName,null,null,null,null,null,null,'C');
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="270" ReadOnly="true" Text='<%# Eval("SupplierName") %>' BackColor="Control" ID="txt_SupplierName" ClientInstanceName="txt_SupplierName">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </table>
                                    </td>
                              
                                </tr>
                                <tr>
                                    <td>WareHouse</td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_WareHouseId" ClientInstanceName="txt_WareHouseId" runat="server" Text='<%# Eval("WareHouseId")%>' Width="100%" HorizontalAlign="Left" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupWh(txt_WareHouseId,null);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td> MasterDocNo</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_MasterDocNo" Text='<%# Eval("MasterDocNo") %>' ClientInstanceName="MasterDocNo">
                                        </dxe:ASPxTextBox>
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
                                                <td style="width: 80px;"></td>
                                                <td style="width: 160px; display: none;">Job Status
                                                                        <dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text='<%#Eval("StatusCode") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                        <hr>
                                    </td>
                                </tr>
                            </table>
                            <table style="display: none">
                                <tr>

                                    <td>Country</td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_Country" ClientInstanceName="txt_Country" runat="server"
                                            Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PartyCountry") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                      PopupCountry(null,txt_Country)
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>PostalCode</td>
                                    <td>

                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_PostalCode" Text='<%# Eval("PartyPostalcode") %>' ClientInstanceName="txt_PostalCode">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>City</td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_City" ClientInstanceName="txt_City" runat="server"
                                            Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PartyCity") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(null,txt_City)
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
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
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&(SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        MultiplePickProduct();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddSKULine" Width="150" runat="server" Text="Add SKU" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&(SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                       grid_SKULine.AddNewRow();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddSKULine1" Width="150" runat="server" Text="Pick from Stock" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&(SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                       MultipleAdd();
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
                                                        DataSourceID="dsIssueDet" OnInit="grid_SKULine_Init" OnInitNewRow="grid_SKULine_InitNewRow" OnRowInserted="grid_SKULine_RowInserted" OnRowUpdated="grid_SKULine_RowUpdated">
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
                                                                                <dxe:ASPxButton ID="btn_SKULine_del" runat="server" Enabled='<%# (SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'
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
                                                                                <dxe:ASPxButton ID="btn_SKULine_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed"%>'
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
                                                            <dxwgv:GridViewDataTextColumn
                                                                Caption="LotNo" FieldName="LotNo" VisibleIndex="1" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_LotNo" ClientInstanceName="txt_LotNo" runat="server" Value='<%# Bind("LotNo") %>' Width="80">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupSoLotNo(txt_LotNo,null,'DoNo');
                                                                           }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="ProductCode" VisibleIndex="2" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product" runat="server" Value='<%# Bind("ProductCode") %>' Width="60">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupProduct_so(txt_SKULine_Product,txt_Des,txt_Uom1,txt_Uom2,txt_Uom3,null,spin_QtyPack,spin_QtyWhole,spin_QtyBase,cb_Att1,cb_Att2,cb_Att3,cb_Att4,cb_Att5,cb_Att6,null,null,null,spin_Price);
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
                                                             <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="LocationCode" VisibleIndex="3" Width="80px">
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
                                                            <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Uom1" VisibleIndex="4" Width="60">
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
                                                           <%-- <dxwgv:GridViewDataSpinEditColumn Caption="Whole Qty" FieldName="Qty2" VisibleIndex="11" Width="60">
                                                                <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer" />
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Loose Qty" FieldName="Qty3" VisibleIndex="12" Width="60">
                                                                <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer" />
                                                            </dxwgv:GridViewDataSpinEditColumn>--%>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="5" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyPack" runat="server" Width="40" ClientInstanceName="spin_QtyPack" Value='<%# Bind("QtyPackWhole") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="6" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyWhole" runat="server" Width="40" ClientInstanceName="spin_QtyWhole" Value='<%# Bind("QtyWholeLoose") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="ML" FieldName="Att1" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att1" ClientInstanceName="cb_Att1" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att1") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="ACL%" FieldName="Att2" VisibleIndex="7" Width="50">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att2" ClientInstanceName="cb_Att2" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att2") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="CO" FieldName="Att3" VisibleIndex="7" Width="50">
                                                               <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att3" ClientInstanceName="cb_Att3" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att3") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="NRF/REF" FieldName="Att4" VisibleIndex="7" Width="50">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att4" ClientInstanceName="cb_Att4" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att4") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="GBX" FieldName="Att5" VisibleIndex="7" Width="50">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att5" ClientInstanceName="cb_Att5" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att5") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="DECODED" FieldName="Att6" VisibleIndex="7" Width="50">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att6" ClientInstanceName="cb_Att6" Width="50" DataSourceID="dsWhMastData"  TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att6") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataDateColumn Caption="ExpiryDate" FieldName="ExpiredDate" VisibleIndex="8" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxDateEdit ID="txt_ExpiredDate" runat="server" Width="100" EditFormat="Custom" Value='<%# Bind("ExpiredDate") %>' EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="STK" FieldName="QtyLooseBase"  VisibleIndex="8" Width="40">
                                                               <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("QtyLooseBase") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="PKGUOM" FieldName="Uom2" VisibleIndex="15" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom2" Width="50" ClientInstanceName="txt_Uom2" Text='<%# Bind("Uom2") %>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom2,2);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" VisibleIndex="17" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom3" Width="50" ClientInstanceName="txt_Uom3" Text='<%# Bind("Uom3") %>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom3,2);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
<%--                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att7" FieldName="Att7" VisibleIndex="51" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att7" ClientInstanceName="cb_Att7" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att7") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att8" FieldName="Att8" VisibleIndex="51" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att8" ClientInstanceName="cb_Att8" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att8") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att9" FieldName="Att9" VisibleIndex="51" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att9" ClientInstanceName="cb_Att9" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att9") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att10" FieldName="Att10" VisibleIndex="51" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att10" ClientInstanceName="cb_Att10" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att10") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>--%>
                                                            <%--<dxwgv:GridViewDataTextColumn Caption="" FieldName="Id" VisibleIndex="200" Width="10">
                                                            </dxwgv:GridViewDataTextColumn>--%>

                                                             <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty1" VisibleIndex="18" Width="40">
                                                                <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer" Width="40" />
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" CellStyle-HorizontalAlign="Left" VisibleIndex="18" Width="50">
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_Price" runat="server" Width="60" ClientInstanceName="spin_Price" Value='<%# Bind("Price") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" DecimalPlaces="2" DisplayFormatString="0.00">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="DocAmt" VisibleIndex="18" Width="10">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="List Sale Price" ReadOnly="true"  CellStyle-HorizontalAlign="Left" VisibleIndex="18" Width="50">
                                                                <DataItemTemplate>
                                                                    <%# EzshipHelper.GetListSalePrice(EzshipHelper.GetPartyIdByDoNo(SafeValue.SafeString(Eval("DoNo"))),SafeValue.SafeString(Eval("ProductCode"))) %>
                                                                </DataItemTemplate>
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                              <dxwgv:GridViewDataSpinEditColumn Caption="Last Sale Price" ReadOnly="true"   CellStyle-HorizontalAlign="Left" VisibleIndex="18" Width="50">
                                                                  <DataItemTemplate>
                                                                      <%# EzshipHelper.GetLastSalePrice(EzshipHelper.GetPartyIdByDoNo(SafeValue.SafeString(Eval("DoNo"))),SafeValue.SafeString(Eval("ProductCode"))) %>
                                                                  </DataItemTemplate>
                                                                  <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                              <dxwgv:GridViewDataSpinEditColumn Caption="List Buy Price"   ReadOnly="true"  CellStyle-HorizontalAlign="Left" VisibleIndex="18" Width="50">
                                                                <DataItemTemplate>
                                                                      <%# EzshipHelper.GetListBuyPrice(EzshipHelper.GetPartyIdByDoNo(SafeValue.SafeString(Eval("DoNo"))),SafeValue.SafeString(Eval("ProductCode"))) %>
                                                                  </DataItemTemplate>
                                                                  <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                              </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Last Buy Price" ReadOnly="true"  CellStyle-HorizontalAlign="Left" VisibleIndex="18" Width="50">
                                                               <DataItemTemplate>
                                                                      <%# EzshipHelper.GetLastBuyPrice(EzshipHelper.GetPartyIdByDoNo(SafeValue.SafeString(Eval("DoNo"))),SafeValue.SafeString(Eval("ProductCode"))) %>
                                                                  </DataItemTemplate>
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>

                                                        </Columns>
                                                        <Settings ShowFooter="true" />
                                                        <TotalSummary>
                                                            <dxwgv:ASPxSummaryItem FieldName="ProductCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                                            <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0:0}" />
                                                            <dxwgv:ASPxSummaryItem FieldName="DocAmt" SummaryType="Sum" DisplayFormat="{0:00.00}" />
                                                        </TotalSummary>
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Po Request" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&(SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                       grid_PoRequest.AddNewRow();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div>
                                                    <%-- style="WIDTH: 900px; overflow-y: scroll;"--%>
                                                    <dxwgv:ASPxGridView ID="grid_PoRequest" ClientInstanceName="grid_PoRequest" runat="server" OnRowInserting="grid_PoRequest_RowInserting"
                                                        OnRowDeleting="grid_PoRequest_RowDeleting" OnRowUpdating="grid_PoRequest_RowUpdating" OnBeforePerformDataSelect="grid_PoRequest_BeforePerformDataSelect"
                                                        KeyFieldName="Id" Width="1000" AutoGenerateColumns="False" Styles-Cell-Paddings-Padding="2" DataSourceID="dsPoRequest" OnInit="grid_PoRequest_Init" OnInitNewRow="grid_PoRequest_InitNewRow" OnRowInserted="grid_PoRequest_RowInserted" OnRowUpdated="grid_PoRequest_RowUpdated">
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
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_PoRequest.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_del" runat="server" Enabled='<%# (SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_PoRequest.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed"%>'
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_PoRequest.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_PoRequest.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Sku" FieldName="Product" VisibleIndex="1" Width="60px">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product1" runat="server" Value='<%# Bind("Product") %>' Width="60">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupProduct(txt_SKULine_Product1,txt_Des11,txt_Uom11,txt_Uom21,txt_Uom31,null,spin_QtyPack1,spin_QtyWhole1,spin_QtyBase1,cb_Att1,cb_Att2,cb_Att3,cb_Att4,cb_Att5,cb_Att6,null,null,null,spin_Price1);
                                                                           }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="2" Width="100px">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_Des11" runat="server" Width="100" ClientInstanceName="txt_Des11" Text='<%# Bind("Des1") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                              <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="Location" VisibleIndex="3" Width="80px">
                                                                 <EditItemTemplate>
                                                                     <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server"
                                                                         Width="80px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse" Value='<%# Bind("Location") %>'
                                                                         ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                                                                         EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                                                                         <Columns>
                                                                             <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                                                                             <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                                                         </Columns>
                                                                     </dxe:ASPxComboBox>
                                                                 </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="QTY" FieldName="Qty1" VisibleIndex="3" Width="40">
                                                                <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                             <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" CellStyle-HorizontalAlign="Left" VisibleIndex="3" Width="50">
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_Price1" runat="server" Width="60" ClientInstanceName="spin_Price1" Value='<%# Bind("Price") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" DecimalPlaces="2" DisplayFormatString="0.00">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                             <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="4" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyPack" runat="server" Width="40" ClientInstanceName="spin_QtyPack1" Value='<%# Bind("QtyPackWhole") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="5" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyWhole" runat="server" Width="40" ClientInstanceName="spin_QtyWhole1" Value='<%# Bind("QtyWholeLoose") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                           <dxwgv:GridViewDataComboBoxColumn Caption="ML" FieldName="Att1" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att1" ClientInstanceName="cb_Att1" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att1") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="ACL%" FieldName="Att2" VisibleIndex="7" Width="50">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att2" ClientInstanceName="cb_Att2" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att2") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="CO" FieldName="Att3" VisibleIndex="7" Width="50">
                                                               <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att3" ClientInstanceName="cb_Att3" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att3") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="NRF/REF" FieldName="Att4" VisibleIndex="7" Width="50">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att4" ClientInstanceName="cb_Att4" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att4") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="GBX" FieldName="Att5" VisibleIndex="7" Width="50">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att5" ClientInstanceName="cb_Att5" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att5") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="DECODED" FieldName="Att6" VisibleIndex="7" Width="50">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att6" ClientInstanceName="cb_Att6" Width="50" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att6") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="STK" FieldName="QtyLooseBase"  VisibleIndex="8" Width="40">
                                                               <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase1" runat="server" Width="40" ClientInstanceName="spin_QtyBase1" Value='<%# Bind("QtyLooseBase") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <%--<dxwgv:GridViewDataSpinEditColumn Caption="Whole Qty" FieldName="Qty2" VisibleIndex="11" Width="60">
                                                                <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Loose Qty" FieldName="Qty3" VisibleIndex="12" Width="60">
                                                                <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>--%>

                                                            <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Uom1" VisibleIndex="13" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom1" Width="60" ClientInstanceName="txt_Uom11" Text='<%# Bind("Uom1") %>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom11,2);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>

                                                            <dxwgv:GridViewDataTextColumn Caption="PKG UOM" FieldName="Uom2" VisibleIndex="15" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom2" Width="60" ClientInstanceName="txt_Uom21" Text='<%# Bind("Uom2") %>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom21,2);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>

                                                            <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" VisibleIndex="17" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom3" Width="60" ClientInstanceName="txt_Uom31" Text='<%# Bind("Uom3") %>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom31,2);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                           <%-- <dxwgv:GridViewDataTextColumn Caption="B.UOM" FieldName="Uom4" VisibleIndex="19" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom4" Width="70" ClientInstanceName="txt_Uom41" Text='<%# Bind("Uom4") %>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom41,2);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>

                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att7" FieldName="Att7" VisibleIndex="51" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att7" ClientInstanceName="cb_Att7" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att7") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att8" FieldName="Att8" VisibleIndex="51" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att8" ClientInstanceName="cb_Att8" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att8") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att9" FieldName="Att9" VisibleIndex="51" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att9" ClientInstanceName="cb_Att9" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att9") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att10" FieldName="Att10" VisibleIndex="51" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cb_Att10" ClientInstanceName="cb_Att10" Width="60" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Att10") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>--%>                                                           
                                                             <dxwgv:GridViewDataTextColumn Caption="PO No" FieldName="PoNo" VisibleIndex="101" Width="120">
                                                                <DataItemTemplate>
                                                                     <a href='javascript: parent.navTab.openTab("<%# Eval("PoNo") %>","/Modules/WareHouse/Job/PoEdit.aspx?no=<%# Eval("PoNo") %>",{title:"<%# Eval("PoNo") %>", fresh:false, external:true});'><%# Eval("PoNo") %></a>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>

                                                        <Settings ShowFooter="true" />
                                                        <TotalSummary>
                                                            <dxwgv:ASPxSummaryItem FieldName="ProductCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                                            <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0:0}" />
                                                        </TotalSummary>
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Packing" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl5" runat="server">
                                               <dxe:ASPxButton ID="ASPxButton13" Width="150" runat="server" Text="Add Packing" AutoPostBack="false" UseSubmitBehavior="false"
                                                    Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                    <ClientSideEvents Click="function(s,e) {
                                                       Grid_Packing.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="Grid_Packing" ClientInstanceName="Grid_Packing" runat="server"
                                                    KeyFieldName="Id" DataSourceID="dsWhPacking" Width="100%" OnBeforePerformDataSelect="Grid_Packing_BeforePerformDataSelect"
                                                    OnRowUpdating="Grid_Packing_RowUpdating" OnRowInserting="Grid_Packing_RowInserting"
                                                    OnRowDeleting="Grid_Packing_RowDeleting" OnInitNewRow="Grid_Packing_InitNewRow"
                                                    OnInit="Grid_Packing_Init">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { Grid_Packing.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){Grid_Packing.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                       <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo" VisibleIndex="4">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="PACK TYPE" FieldName="PackageType" VisibleIndex="7">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="JobNo" VisibleIndex="7" Visible="false">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="MkgType" VisibleIndex="8" Visible="false">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_Mkg_HouseNo" runat="server" Text='<%# Bind("JobNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                            <dxe:ASPxTextBox ID="txt_Mkg_RefNo" runat="server" Text='<%# Bind("RefNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </div>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    Cont No
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_ContainerNo" runat="server" Text='<%# Bind("ContainerNo") %>' Width="120px">
                                                            </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    Seal No
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="120" ID="txt_sealN"
                                                                        Text='<%# Bind("SealNo") %>' ClientInstanceName="txt_sealN">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td width="90">Cont Type
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_contType" ClientInstanceName="txt_contType" Text='<%# Bind("ContainerType") %>' 
                                                                        runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_contType,1);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Weight
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                        runat="server" Width="120" ID="spin_wt" Value='<%# Bind("Weight")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>
                                                                    Volume
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                        runat="server" Width="120" ID="spin_m3" Value='<%# Bind("Volume")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>Qty
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120"
                                                                        ID="spin_pkg" Text='<%# Bind("Qty")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>Pack Type
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_pkgType" ClientInstanceName="txt_pkgType2" Text='<%# Bind("PackageType")%>'
                                                                        runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType2,2);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td width="45">
                                                                    Marking
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo runat="server" Rows="6" Width="298" ID="txt_mkg1" MaxLength="500" Text='<%# Bind("Marking")%>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                                <td>
                                                                    Description
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo runat="server" Rows="6" Width="304" ID="txt_des1" MaxLength="500" Text='<%# Bind("Description")%>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' >
                                                                    <ClientSideEvents Click="function(s,e){Grid_Packing.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                runat="server">
                                                            </dxwgv:ASPxGridViewTemplateReplacement>
                                                        </div>
                                                    </EditForm>
                                                </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Issue Info" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                                <div style="width: 900px">
                                                    <table border="0">
                                                        <tr>
                                                            <td>Vessel</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_Vessel" Text='<%# Eval("Vessel") %>' ClientInstanceName="txt_Vessel">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td width="80">Voyage</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_Voyage" Text='<%# Eval("Voyage") %>' ClientInstanceName="txt_Voyage">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>HBL/HAWB</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_HBL" Text='<%# Eval("Hbl") %>' ClientInstanceName="txt_HBL">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td>OBL/AWB</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_OBL" Text='<%# Eval("Obl") %>' ClientInstanceName="txt_OBL">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Etd</td>
                                                            <td>
                                                                <dxe:ASPxDateEdit ID="date_Etd" Width="150" runat="server" Value='<%# Eval("EtaDest") %>'
                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                            <td>DriveName</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_DriveName" Text='<%# Eval("Carrier") %>' ClientInstanceName="txt_DriveName">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td width="50">Vehno</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_Vehno" Text='<%# Eval("Vehicle") %>' ClientInstanceName="txt_Vehno">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>POL</td>
                                                            <td>
                                                                <dxe:ASPxButtonEdit ID="txt_POL" ClientInstanceName="txt_POL" runat="server" MaxLength="5"
                                                                    Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Pol") %>'>
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPort(txt_POL,null);
                                                                    }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </td>
                                                            <td>ETD POL</td>
                                                            <td>
                                                                <dxe:ASPxDateEdit ID="txt_EtdPol" Width="150" runat="server" Value='<%# Eval("Etd") %>'
                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>POD</td>
                                                            <td>
                                                                <dxe:ASPxButtonEdit ID="txt_POD" ClientInstanceName="txt_POD" runat="server" MaxLength="5"
                                                                    Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Pod") %>'>
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPort(txt_POD,null);
                                                                    }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </td>
                                                            <td>ETA POD</td>
                                                            <td>
                                                                <dxe:ASPxDateEdit ID="txt_EtaPod" Width="150" runat="server" Value='<%# Eval("Eta") %>'
                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Party" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl4" runat="server">
                                                <table style="width: 900px;">
                                                    <tr>
                                                        <td colspan="8" style="background-color: Gray; color: White;">
                                                            <b>Agent Info</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Code</td>
                                                        <td colspan="3">
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_AgentCode" ClientInstanceName="txt_AgentCode" runat="server"
                                                                            Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("AgentId") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_AgentCode,txt_AgentName,txt_AgentContact,txt_AgentTelexFax,txt_AgentCountry,txt_AgentCity,txt_AgentPostalCode,memo_AgentAddress,'A');
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="257" ID="txt_AgentName" Text='<%# Eval("AgentName") %>' ClientInstanceName="txt_AgentName">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>TelexFax</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_AgentTelexFax" Text='<%# Eval("AgentTel") %>' ClientInstanceName="txt_AgentTelexFax">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Contact</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_AgentContact" Text='<%# Eval("AgentContact") %>' ClientInstanceName="txt_AgentContact">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="3">Address</td>
                                                        <td colspan="3" rowspan="3">
                                                            <dxe:ASPxMemo ID="memo_AgentAddress" Rows="4" Width="351" ClientInstanceName="memo_AgentAddress"
                                                                runat="server" Text='<%# Eval("AgentAdd") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Postal Code</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_AgentPostalCode" Text='<%# Eval("AgentZip") %>' ClientInstanceName="txt_AgentPostalCode">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>City</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_AgentCity" ClientInstanceName="txt_AgentCity" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("AgentCity") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(null,txt_AgentCity)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Country</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_AgentCountry" ClientInstanceName="txt_AgentCountry" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("AgentCountry") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCountry(null,txt_AgentCountry)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8" style="background-color: Gray; color: White;">
                                                            <b>Notify Info</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Code</td>
                                                        <td colspan="3">
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_NotifyCode" ClientInstanceName="txt_NotifyCode" runat="server"
                                                                            Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("NotifyId") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_NotifyDECODE,txt_NotifyName,txt_NotifyContact,txt_NotifyTelexFax,txt_NotifyCountry,txt_NotifyCity,txt_NotifyPostalCode,memo_NotifyAddress,'A');
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="255" ID="txt_NotifyName" Text='<%# Eval("NotifyName") %>' ClientInstanceName="txt_NotifyName">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="auto-style1">Tel/Fax</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_NotifyTelexFax" runat="server" ClientInstanceName="txt_NotifyTelexFax" Text='<%# Eval("NotifyTel") %>' Width="150px">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Contact</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_NotifyContact" runat="server" ClientInstanceName="txt_NotifyContact" Text='<%# Eval("NotifyContact") %>' Width="150px">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="3">Address</td>
                                                        <td colspan="3" rowspan="3">
                                                            <dxe:ASPxMemo ID="memo_NotifyAddress" runat="server" ClientInstanceName="memo_NotifyAddress" Rows="4" Text='<%# Eval("NotifyAdd") %>' Width="350px">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td class="auto-style1">Postal Code</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_NotifyPostalCode" runat="server" ClientInstanceName="txt_NotifyPostalCode" Text='<%# Eval("NotifyZip") %>' Width="150px">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>City</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_NotifyCity" runat="server" ClientInstanceName="txt_NotifyCity" HorizontalAlign="Left" Text='<%# Eval("NotifyCity") %>' Width="150px">
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(null,txt_NotifyCity)
                                                                    }" />
                                                                <Buttons>
                                                                    <dxe:EditButton Text="..">
                                                                    </dxe:EditButton>
                                                                </Buttons>
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Country</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_NotifyCountry" runat="server" ClientInstanceName="txt_NotifyCountry" HorizontalAlign="Left" Text='<%# Eval("NotifyCountry") %>' Width="150px">
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCountry(null,txt_NotifyCountry)
                                                                    }" />
                                                                <Buttons>
                                                                    <dxe:EditButton Text="..">
                                                                    </dxe:EditButton>
                                                                </Buttons>
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Billing" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Bill" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton16" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled"  %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton7" Width="150" runat="server" Text="Auto SO Inv" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Invoice, "WH", txt_DoNo.GetText(), "SO","IV");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton9" Width="150" runat="server" Text="Auto DO Inv" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Invoice, "WH", txt_DoNo.GetText(), "DO","IV");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton11" Width="125" runat="server" Text="Auto CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Invoice, "WH", txt_DoNo.GetText(), "SO","CN");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Invoice" ClientInstanceName="Grid_Invoice"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsInvoice" Width="900px"
                                                    OnBeforePerformDataSelect="Grid_Invoice_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowInvoice(Grid_Invoice,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintWh_invoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1" Width="30">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="1" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="4" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="5" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="6" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled"  %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Payable" ClientInstanceName="Grid_Payable"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsVoucher" Width="100%"
                                                    OnBeforePerformDataSelect="Grid_Payable_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowPayable(Grid_Payable,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintPayable("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1" Width="30">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="1" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="4" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="5" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="6" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton14" Width="150" runat="server" Text="Add Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 &&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Cost.AddNewRow();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server"
                                                    DataSourceID="dsCosting" KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect"
                                                    OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting"
                                                    OnRowUpdating="grid_Cost_RowUpdating" OnRowInserted="grid_Cost_RowInserted" OnRowUpdated="grid_Cost_RowUpdated" OnHtmlEditFormCreated="grid_Cost_HtmlEditFormCreated" OnRowDeleting="grid_Cost_RowDeleting" OnCustomDataCallback="grid_Cost_CustomDataCallback">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="EditFormAndDisplayRow" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="70" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Cost.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Cost.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgCode" VisibleIndex="2">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgCodeDes" VisibleIndex="2">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="CostQty" VisibleIndex="3" Width="50">
                                                            <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="CostPrice" VisibleIndex="4" Width="50">
                                                            <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="GST" FieldName="CostGst" VisibleIndex="5" Width="50">
                                                            <PropertiesSpinEdit DisplayFormatString="{0:0.00}"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CostCurrency" VisibleIndex="5" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="ExRate" FieldName="CostExRate" VisibleIndex="5" Width="50">
                                                            <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000000}"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Amt" FieldName="CostLocAmt" VisibleIndex="5" Width="50">
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
                                                                        <dxe:ASPxButtonEdit ID="txt_CostChgCode" ClientInstanceName="txt_CostChgCode" Width="100" runat="server" Text='<%# Bind("ChgCode")%>' >
                                                                            <Buttons>
                                                                                <dxe:EditButton Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupChgCode(txt_CostChgCode,txt_CostDes);
                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>Description</td>
                                                                    <td colspan="2">
                                                                        <dxe:ASPxTextBox Width="200" ID="txt_CostDes" ClientInstanceName="txt_CostDes" runat="server" Text='<%# Bind("ChgCodeDes") %>'>
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
                                                                                <td>GST
                                                                                </td>
                                                                                <td>Amount
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostCostQty"
                                                                                        ID="spin_CostCostQty" Text='<%# Bind("CostQty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                   }" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostPrice"
                                                                                        runat="server" Width="100" ID="spin_CostCostPrice" Value='<%# Bind("CostPrice")%>' Increment="0" DecimalPlaces="2">
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                   }" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" MaxLength="3" runat="server" Width="80" Text='<%# Bind("CostCurrency") %>'>
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostCostCurrency,spin_CostCostExRate);
                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                                                        DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostExRate" ClientInstanceName="spin_CostCostExRate" Text='<%# Bind("CostExRate")%>' Increment="0">
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                   }" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                                                        DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostGst" ClientInstanceName="spin_CostCostGst" Text='<%# Bind("CostGst")%>' Increment="0">
                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                   }" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostAmt"
                                                                                        BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
                                                                                        Value='<%# Eval("CostLocAmt")%>'>
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Remark
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_Remark" runat="server" Width="100%" Text='<%# Bind("Remark") %>' Rows="2"></dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="8">
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' >
                                                                    <ClientSideEvents Click="function(s,e){grid_Cost.UpdateEdit();}" />
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
                                    <dxtc:TabPage Text="Payment" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl2" runat="server">
                                                 <dxwgv:ASPxGridView ID="grid_Payment" ClientInstanceName="grid_Payment" runat="server"
                                                    KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" DataSourceID="dsReceipt" OnBeforePerformDataSelect="grid_Payment_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                                                            Width="8%">
                                                             <DataItemTemplate>
                                                                <a href='javascript: parent.navTab.openTab("<%# Eval("DocNo") %>","/PagesAccount/EditPage/ArReceiptEdit.aspx?no=<%# Eval("DocNo") %>",{title:"<%# Eval("DocNo") %>", fresh:false, external:true});'><%# Eval("DocNo") %></a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="3"
                                                            Width="5%" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                                                            Width="8%">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="6"
                                                            Width="8%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="7"
                                                            Width="8%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="8"
                                                            Width="8%">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="9"
                                                            Width="8%">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PostInd" FieldName="ExportInd" VisibleIndex="90"
                                                            Width="8%" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>

                                                    </Columns>
                                                     <Settings ShowFooter="True" />
                                                     <TotalSummary>
                                                         <dxwgv:ASPxSummaryItem FieldName="DocNo" SummaryType="Count" DisplayFormat="{0}" />
                                                         <dxwgv:ASPxSummaryItem FieldName="DocAmt" SummaryType="Sum" DisplayFormat="{0}" />
                                                         <dxwgv:ASPxSummaryItem FieldName="LocAmt" SummaryType="Sum" DisplayFormat="{0:00.00}" />
                                                     </TotalSummary>
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
                                                            <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Upload Attachments" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled"  %>'>
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

                                                <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsIssuePhoto"
                                                    KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                    AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
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
                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
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
                                    <dxtc:TabPage Text="Log" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl6" runat="server">
                                                <dxwgv:ASPxGridView ID="grid_Log" ClientInstanceName="grid_Log" runat="server"
                                                    KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" DataSourceID="dsLog" OnBeforePerformDataSelect="grid_Log_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Action" VisibleIndex="2" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="User" FieldName="CreateBy" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Time" FieldName="CreateDateTime" VisibleIndex="4" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Delivery Order">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <dxwgv:ASPxGridView ID="grid_DoIn" ClientInstanceName="grid_DoIn"
                                                    runat="server" KeyFieldName="Id" DataSourceID="dsDoIn" Width="100%"
                                                    OnBeforePerformDataSelect="grid_DoIn_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Order No" FieldName="DoNo" VisibleIndex="1" Width="100">
                                                            <DataItemTemplate>
                                                                <a href='javascript: parent.navTab.openTab("<%# Eval("DoNo") %>","/Modules/WareHouse/Job/DoOutEdit.aspx?no=<%# Eval("DoNo") %>",{title:"<%# Eval("DoNo") %>", fresh:false, external:true});'><%# Eval("DoNo") %></a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Order Date" FieldName="DoDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Customer Ref" FieldName="CustomerReference" VisibleIndex="6" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RefDate" FieldName="CustomerDate" VisibleIndex="6" Width="80">
                                                            <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("CustomerDate")) %> </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DoStatus" VisibleIndex="6" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
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
                AllowResize="True" Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(isUpload)
	                    grd_Photo.Refresh();
                }" />
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(grid!=null)
	                    grid.Refresh();
	                    grid=null;
                        detailGrid.Refresh();
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
