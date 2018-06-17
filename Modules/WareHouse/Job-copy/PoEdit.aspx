<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="PoEdit.aspx.cs" Inherits="WareHouse_Job_PoEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>PO Edit</title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Wh/Wh_Doc.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var isUpload = false;
        var clientId = null;
        var clientName = null;
        function ShowDoIn(masterId) {
            window.location = "DoInEdit.aspx?no=" + masterId;
        }
        function PopupContract(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetContentUrl('../SelectPage/SelectContract.aspx?party=' + this.txt_PartyId.GetText());
            popubCtr.SetHeaderText('Contract');
            popubCtr.Show();
        }
        function PopupUploadPhoto() {
            popubCtr.SetHeaderText('Upload Attachment');
            popubCtr.SetContentUrl('../Upload.aspx?Type=PO&Sn=' + txt_DoNo.GetText());
            popubCtr.Show();
        }

        function OnSaveCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                detailGrid.Refresh();
            }
            if (v != null && v.indexOf("Fail") > -1) {
                alert("Please enter the Supplier");
            }
            else if (v == "") {
                detailGrid.Refresh();
            }
            else if (v == "No Price") {
                alert("No Price,Can not Confirm");
            }
            else if (v != null && v != "Success") {
                window.location = 'PoEdit.aspx?no=' + v;
                //txt_SchRefNo.SetText(v);
                //txt_DoNo.SetText(v);
            }
        }

        function MultipleAdd() {
            popubCtr.SetHeaderText('PO Request List');
            popubCtr.SetContentUrl('../SelectPage/SelectPurchaseOrderRequest.aspx?Type=PO&Sn=' + txt_DoNo.GetText());
            popubCtr.Show();
        }
        function MultiplePickProduct() {
            popubCtr.SetHeaderText('Multiple Product');
            popubCtr.SetContentUrl('../SelectPage/MultipleProduct.aspx?Type=PO&Sn=' + txt_DoNo.GetText() + "&WhId=" + txt_WareHouseId.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_DoDet.Refresh();
        }

        function OnTransferCallBack(v) {
            if (v == "Fail")
                alert("Create DO in Fail");
            else if (v == "No Balance Qty!")
                alert(v);
            else if (v != null) {
                alert("Do in No is " + v);
                grid_DoIn.Refresh();
            }
            //else if (v = "Success") {
            //    alert("Create Do In  Success!");
           // }
        }
        function AddInvoice1(gridId, JobType, refNo, jobNo, docType) {
            grid = gridId;
            popubCtr1.SetHeaderText('Invoice');
            popubCtr1.SetContentUrl('../Account/ArInvoice.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo + '&DocType=' + docType);
            popubCtr1.Show();
        }
        function PrintDo(doNo, doType) {
            if (doType == "PO")
                //window.open('/ReportFreightSea/PrintView.aspx?document=wh_Po&master=' + doNo + "&house=0");
            window.open('/Modules/WareHouse/PrintView.aspx?document=wh_Po&master=' + doNo + "&house=0");
        }

    </script>
</head>
<body>
    <wilson:DataSource ID="dsLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.LogEvent"
        KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsDo" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhTrans" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsDoDet" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhTransDet" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsDoIn" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhDo" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
    <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
    <wilson:DataSource ID="dsAttachment" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhAttachment" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsProduct" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefProduct" KeyMember="Id" />
    <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XXUom" KeyMember="Id" FilterExpression="CodeType='2'" />
    <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhMastData" KeyMember="Id" FilterExpression="Type='Attribute'" />
    <wilson:DataSource ID="dsPayment" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XAApPayment" KeyMember="SequenceId" FilterExpression="1=0" />
     <wilson:DataSource ID="dsWhPacking" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhPacking" KeyMember="Id" FilterExpression="1=0" />
    <form id="form1" runat="server">

        <div>
            <table>
                <tr>
                    <td>No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_DoNo" Width="150" runat="server" ClientInstanceName="txt_RefDoNo">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='PoEdit.aspx?no='+txt_RefDoNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='PoList.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='PoEdit.aspx?no=0';
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
            <dxwgv:ASPxGridView ID="grd_Do" ClientInstanceName="detailGrid" runat="server" DataSourceID="dsDo" OnHtmlEditFormCreated="grd_Do_HtmlEditFormCreated"
                KeyFieldName="Id" Width="100%" OnCustomDataCallback="grd_Do_CustomDataCallback"
                AutoGenerateColumns="false" OnCustomCallback="grd_Do_CustomCallback" OnInit="grd_Do_Init" OnInitNewRow="grd_Do_InitNewRow">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <table style="text-align: left; padding: 2px 2px 2px 2px; width: 900px">
                            <tr>
                                <td width="70%">
                                    <table style="border: solid 1px black; padding: 2px 2px 2px 2px; width: 100%">
                                        <tr>
                                            <td>Print Document:&nbsp;&nbsp;
                                       <a href="#" onclick='PrintDo("<%# Eval("DoNo") %>","PO")'>PO</a>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                 <td>
                                        <dxe:ASPxButton ID="btn_ConfirmPI" Width="90" runat="server" Text="Confirm PO" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled"  %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                               detailGrid.GetValuesOnCustomCallback('Confirm',OnSaveCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                 <td>
                                    <dxe:ASPxButton ID="btn_AddDoIn" ClientInstanceName="btn_AddDoIn" runat="server" Width="100" Text="Create GRN" AutoPostBack="False"
                                        UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&(SafeValue.SafeString(Eval("DoStatus"),"USE")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"USE")=="Confirmed") %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                            detailGrid.GetValuesOnCustomCallback('AddDoIn',OnTransferCallBack);           
                                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_CloseJob" ClientInstanceName="btn_CloseJob" Width="90" runat="server" Text="Close Job" AutoPostBack="False"
                                        UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_CloseJob.GetText()+' Job?'))
                                                                    {
                                                                detailGrid.GetValuesOnCustomCallback('CloseJob',OnCloseCallBack);
                                                      } 
                                       }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                         <%--   <dxe:ASPxButton ID="btn_Void" ClientInstanceName="btn_Void" runat="server" Width="100" Text="Void" AutoPostBack="False"
                                        UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_Void.GetText()+' Job?'))
                                                                    {
                                                                        detailGrid.GetValuesOnCustomCallback('Void',OnCloseCallBack);                 
                                                                    }
                                        }" />
                                    </dxe:ASPxButton>--%>
                                </td>

                            </tr>
                        </table>
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
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
                                <td>No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_DoNo" runat="server" ClientInstanceName="txt_DoNo" ReadOnly="true" BackColor="Control" Width="100%" Text='<%# Eval("DoNo") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Date</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_Date" runat="server" Value='<%# Eval("DoDate") %>' Width="100%" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
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
                                            <dxe:ListEditItem Text="Received" Value="Received" />
                                            <dxe:ListEditItem Text="Closed" Value="Closed" />
                                            <dxe:ListEditItem Text="Canceled" Value="Canceled" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Supplier</td>
                                <td colspan="3">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" runat="server" Text='<%# Eval("PartyId")%>' Width="80" HorizontalAlign="Left" AutoPostBack="False">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupParty(txt_PartyId,txt_PartyName,null,null,txt_PartyCountry,txt_PartyCity,txt_PostalCode,txt_PartyAdd,'V');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="280px" BackColor="Control" ID="txt_PartyName"
                                                    ReadOnly="true" ClientInstanceName="txt_PartyName" Text='<%# Eval("PartyName")%>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                    <td>PIC
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Pic" runat="server" ClientInstanceName="txt_Pic" Text='<%# Eval("Pic") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                <td></td>
                                <td>
                                   
                                </td>
                            </tr>
                            <tr style="vertical-align:top;">
                                <td rowspan="2">Address</td>
                                <td rowspan="2" colspan="3" >
                                    <dxe:ASPxMemo ID="txt_PartyAdd" ClientInstanceName="txt_PartyAdd" Rows="3" runat="server" Width="100%" Text='<%# Eval("PartyAdd") %>'></dxe:ASPxMemo>
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
                                    <td>Pay Term</td>
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
                                    <dxe:ASPxMemo ID="txt_CollectFrom" Rows="4" Width="100%" ClientInstanceName="txt_CollectFrom"
                                        runat="server" Text='<%# Eval("CollectFrom") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                                <td><a href="#" onclick="PopupPartyAdr(null,txt_DeliveryTo);">Delivery To</a></td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_DeliveryTo" Rows="4" Width="100%" ClientInstanceName="txt_DeliveryTo"
                                        runat="server" Text='<%# Eval("DeliveryTo") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Instruction</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Remark1" Rows="4" Width="100%" ClientInstanceName="txt_Remark1"
                                        runat="server" Text='<%# Eval("Remark1") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                                <td>Internal Remark</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Remark2" Rows="4" Width="100%" ClientInstanceName="txt_Remark2"
                                        runat="server" Text='<%# Eval("Remark2") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td><dxe:ASPxLabel ID="lbl_WareHouse" runat="server" Text="WareHouse"></dxe:ASPxLabel></td>
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
                                            <%--<td style="width: 100px;">Status</td>
                                            <td><%# Eval("StatusCode")%> </td>--%>
                                        </tr>
                                    </table>
                                    <hr>
                                </td>
                            </tr>
                        </table>
                        <table style="display:none">
                            
                                <td>Country</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_PartyCountry" ClientInstanceName="txt_PartyCountry" runat="server"
                                        Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PartyCountry") %>'>
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCountry(null,txt_PartyCountry)
                                                                    }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>Postalcode</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_PostalCode" ClientInstanceName="txt_PostalCode" runat="server" Width="100%" Text='<%# Eval("PartyPostalcode") %>'></dxe:ASPxTextBox>
                                </td>
                                <td>City</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_PartyCity" ClientInstanceName="txt_PartyCity" runat="server"
                                        Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PartyCity") %>'>
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(null,txt_PartyCity)
                                                                    }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                        </table>
                        
                        <div style="padding: 2px 2px 2px 2px">
                            <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="1000px" Height="500px">
                                <TabPages>
                                    <dxtc:TabPage Text="SKU List" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl8" runat="server">

                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton6" runat="server" Text="Multiple Pick SKU" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&(SafeValue.SafeString(Eval("DoStatus"),"USE")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"USE")=="Confirmed")%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        MultiplePickProduct();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_Add" runat="server" Text="Add SKU" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&(SafeValue.SafeString(Eval("DoStatus"),"USE")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"USE")=="Confirmed")%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_DoDet.AddNewRow();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddSKULine1" Width="200" runat="server" Text="Pick from PO Request" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&(SafeValue.SafeString(Eval("DoStatus"),"USE")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"USE")=="Confirmed")%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                       MultipleAdd();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div><%-- style="WIDTH: 950px; overflow-y: scroll;"--%>
                                                    <dxwgv:ASPxGridView ID="grid_DoDet" ClientInstanceName="grid_DoDet"
                                                        runat="server" DataSourceID="dsDoDet" KeyFieldName="Id" OnRowUpdated="grid_DoDet_RowUpdated"
                                                        OnBeforePerformDataSelect="grid_DoDet_BeforePerformDataSelect" OnRowUpdating="grid_DoDet_RowUpdating"
                                                        OnRowInserting="grid_DoDet_RowInserting" OnRowInserted="grid_DoDet_RowInserted" OnInitNewRow="grid_DoDet_InitNewRow" OnInit="grid_DoDet_Init" OnRowDeleting="grid_DoDet_RowDeleting"
                                                        Width="1000px" AutoGenerateColumns="False" Styles-Cell-Paddings-Padding="2" Styles-EditForm-Paddings-Padding="2">
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPager Mode="ShowAllRecords" />
                                                        <SettingsBehavior ConfirmDelete="true" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="30px">
                                                                <DataItemTemplate>
                                                                    <table style="padding:0; width:30px;">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_DoDet_edit" runat="server" Text="Edit" Width="30" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_DoDet.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_DoDet_del" runat="server" Enabled='<%# (SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'
                                                                                    Text="Delete" Width="30" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_DoDet.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table style="padding:0; width:30px;">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_DoDet_edit" runat="server" Enabled='<%# (SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Draft"||SafeValue.SafeString(Eval("DoStatus"),"Draft")=="Confirmed")%>'
                                                                                    Text="Update" Width="30" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { grid_DoDet.UpdateEdit(); }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_DoDet_cancle" runat="server" Text="Cancel" Width="30" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_DoDet.CancelEdit(); }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn
                                                                Caption="LotNo" FieldName="LotNo" VisibleIndex="1" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_LotNo" runat="server" Width="80"  Text='<%# Bind("LotNo")%>' ClientInstanceName="txt_LotNo"></dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="ProductCode" VisibleIndex="2" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product" runat="server" Value='<%# Bind("ProductCode") %>' Width="60">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupProduct(txt_SKULine_Product,txt_Des,txt_Uom1,txt_Uom2,txt_Uom3,null,spin_QtyPack,spin_QtyWhole,spin_QtyBase,cb_Att1,cb_Att2,cb_Att3,cb_Att4,cb_Att5,cb_Att6,null,null,null,spin_Price);
                                                                           }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="3" Width="100">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_Des" runat="server" Width="100" ClientInstanceName="txt_Des" Text='<%# Bind("Des1") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Uom1" VisibleIndex="6" Width="60">
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
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="7" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyPack" runat="server" Width="40" ClientInstanceName="spin_QtyPack" Value='<%# Bind("QtyPackWhole") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="7" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyWhole" runat="server" Width="40" ClientInstanceName="spin_QtyWhole" Value='<%# Bind("QtyWholeLoose") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                           <%-- <dxwgv:GridViewDataSpinEditColumn Caption="Whole Qty" FieldName="Qty2" VisibleIndex="11" Width="60">
                                                                <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer" />
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Loose Qty" FieldName="Qty3" VisibleIndex="12" Width="60">
                                                                <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer" />
                                                            </dxwgv:GridViewDataSpinEditColumn>--%>
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
                                                            <dxwgv:GridViewDataDateColumn Caption="ExpiryDate" FieldName="ExpiredDate" VisibleIndex="8" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxDateEdit ID="txt_ExpiredDate" runat="server" Width="100" Value='<%# Bind("ExpiredDate") %>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="STK" FieldName="QtyLooseBase"  VisibleIndex="8" Width="40">
                                                               <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("QtyLooseBase") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>

                                                            <dxwgv:GridViewDataTextColumn Caption="PACK UOM" FieldName="Uom2" VisibleIndex="15" Width="40">
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
<%--                                                            <dxwgv:GridViewDataTextColumn Caption="B.UOM" FieldName="Uom4" VisibleIndex="19" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom4" Width="60" ClientInstanceName="txt_Uom4" Text='<%# Bind("Uom4") %>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_Uom4,2);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>--%>
                                                            <%--<dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" CellStyle-HorizontalAlign="Left" VisibleIndex="30" Width="100">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Bind("Currency") %>' runat="server" Width="60px" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_ExRate);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="ExRate" FieldName="ExRate" CellStyle-HorizontalAlign="Left" VisibleIndex="30" Width="100">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_ExRate" ClientInstanceName="spin_ExRate" DisplayFormatString="0.000000"
                                                                            runat="server" Width="70" Value='<%# Bind("ExRate")%>' DecimalPlaces="6" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.000000" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Gst" FieldName="Gst" CellStyle-HorizontalAlign="Left" VisibleIndex="30" Width="100">
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>--%>
                                                            
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
                                                            <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="DocAmt"  VisibleIndex="200" Width="10">
                                                            </dxwgv:GridViewDataTextColumn>
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
                                     <dxtc:TabPage Text="Packing" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl5" runat="server">
                                               <dxe:ASPxButton ID="ASPxButton14" Width="150" runat="server" Text="Add Packing" AutoPostBack="false" UseSubmitBehavior="false"
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
                                    <dxtc:TabPage Text="Shipment">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>Vessel</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="180" ID="txt_Vessel" ClientInstanceName="txt_Vessel"
                                                                Text='<%# Bind("Vessel")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Voyage</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="180" ID="txt_Voyage" ClientInstanceName="txt_Voyage"
                                                                Text='<%# Bind("Voyage")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Ocean BL</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="180" ID="txt_OceanBl" Text='<%# Eval("Obl")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Hbl No</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_HouseBl" runat="server" Text='<%# Eval("Hbl") %>' Width="180">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Pol</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol"
                                                                Text='<%# Eval("Pol")%>' runat="server" Width="180" HorizontalAlign="Left" AutoPostBack="False" MaxLength="5">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pol,null);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Pod</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod"
                                                                Text='<%# Eval("Pod")%>' runat="server" Width="180" HorizontalAlign="Left" AutoPostBack="False" MaxLength="5">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pod,null);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <tr>
                                                        </tr>
                                                        <td>Eta</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_Eta" Width="180" runat="server" ClientInstanceName="date_Eta"
                                                                Value='<%# Bind("Eta")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>Etd</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_Etd" Width="180" runat="server" ClientInstanceName="date_Etd"
                                                                Value='<%# Bind("Etd")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>EtaDest</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_EtaDest" Width="140" runat="server" ClientInstanceName="date_EtaDest"
                                                                Value='<%# Bind("EtaDest")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Carrier
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxButtonEdit ID="txt_Carrier" ClientInstanceName="txt_Carrier" runat="server"
                                                                Width="615" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Carrier") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCarrier(null,txt_Carrier)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Vehicle</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_VehicleNo" Width="180" ClientInstanceName="txt_VehicleNo"
                                                                runat="server" Text='<%# Eval("Vehicle") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>COO</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_COO" Width="180" ClientInstanceName="txt_COO"
                                                                runat="server" Text='<%# Eval("Coo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Party">
                                        <ContentCollection>
                                            <dxw:ContentControl>

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
                                                                        <dxe:ASPxButtonEdit ID="txt_AgentId" ClientInstanceName="txt_AgentId" runat="server" Text='<%# Eval("AgentId")%>' Width="80" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupParty(txt_AgentId,txt_AgentName,txt_AgentContact,txt_AgentTel,txt_AgentCountry,txt_AgentCity,txt_AgentZip,txt_AgentAdd,'A');
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="415px" ID="txt_AgentName"
                                                                            ClientInstanceName="txt_AgentName" Text='<%# Eval("AgentName")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>Contact</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_AgentContact"
                                                                ClientInstanceName="txt_AgentContact" Text='<%# Eval("AgentContact")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Tel</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_AgentTel"
                                                                ClientInstanceName="txt_AgentTel" Text='<%# Eval("AgentTel")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="3">Address</td>
                                                        <td colspan="3" rowspan="3">
                                                            <dxe:ASPxMemo ID="txt_AgentAdd" ClientInstanceName="txt_AgentAdd" Rows="6" runat="server" Text='<%# Eval("AgentAdd")%>' Width="495px"></dxe:ASPxMemo>
                                                        </td>
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
                                                        <td>Zip</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_AgentZip"
                                                                ClientInstanceName="txt_AgentZip" Text='<%# Eval("AgentZip")%>'>
                                                            </dxe:ASPxTextBox>
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
                                                                        <dxe:ASPxButtonEdit ID="txt_NotifyId" ClientInstanceName="txt_NotifyId" runat="server" Text='<%# Eval("NotifyId")%>' Width="80" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupParty(txt_NotifyId,txt_NotifyName,txt_NotifyContact,txt_NotifyTel,txt_NotifyCountry,txt_NotifyCity,txt_NotifyZip,txt_NotifyAdd,null);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="415px" ID="txt_NotifyName"
                                                                             ClientInstanceName="txt_NotifyName" Text='<%# Eval("NotifyName")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>Contact</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_NotifyContact"
                                                                ClientInstanceName="txt_NotifyContact" Text='<%# Eval("NotifyContact")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Tel</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_NotifyTel"
                                                                ClientInstanceName="txt_NotifyTel" Text='<%# Eval("NotifyTel")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="3">Address</td>
                                                        <td colspan="3" rowspan="3">
                                                            <dxe:ASPxMemo ID="txt_NotifyAdd" ClientInstanceName="txt_NotifyAdd" Rows="5" runat="server" Text='<%# Eval("NotifyAdd")%>' Width="495px"></dxe:ASPxMemo>
                                                        </td>
                                                        <td>City</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_NotifyCity" ClientInstanceName="txt_NotifyCity" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("NotifyCity") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(null,txt_NotifyCity)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Country</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_NotifyCountry" ClientInstanceName="txt_NotifyCountry" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("NotifyCountry") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCountry(null,txt_NotifyCountry)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Zip</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_NotifyZip"
                                                                ClientInstanceName="txt_NotifyZip" Text='<%# Eval("NotifyZip")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8" style="background-color: Gray; color: White;">
                                                            <b>Consignee Info</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Code</td>
                                                        <td colspan="3">
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_ConsigneeId" ClientInstanceName="txt_ConsigneeId" runat="server" Text='<%# Eval("ConsigneeId")%>' Width="80" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupParty(txt_ConsigneeId,txt_ConsigneeName,txt_ConsigneeContact,txt_ConsigneeTel,txt_ConsigneeCountry,txt_ConsigneeCity,txt_ConsigneeZip,txt_ConsigneeAdd,null);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="415px" ID="txt_ConsigneeName"
                                                                            ClientInstanceName="txt_ConsigneeName" Text='<%# Eval("ConsigneeName")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>Contact</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_ConsigneeContact"
                                                                ClientInstanceName="txt_ConsigneeContact" Text='<%# Eval("ConsigneeContact")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Tel</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_ConsigneeTel"
                                                                ClientInstanceName="txt_ConsigneeTel" Text='<%# Eval("ConsigneeTel")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="3">Address</td>
                                                        <td colspan="3" rowspan="3">
                                                            <dxe:ASPxMemo ID="txt_ConsigneeAdd" ClientInstanceName="txt_ConsigneeAdd" Rows="5" runat="server" Text='<%# Eval("ConsigneeAdd")%>' Width="495px"></dxe:ASPxMemo>
                                                        </td>
                                                        <td>City</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_ConsigneeCity" ClientInstanceName="txt_ConsigneeCity" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ConsigneeCity") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(null,txt_ConsigneeCity)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Zip</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150px" ID="txt_ConsigneeZip"
                                                                ClientInstanceName="txt_ConsigneeZip" Text='<%# Eval("ConsigneeZip")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Country</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_ConsigneeCountry" ClientInstanceName="txt_ConsigneeCountry" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ConsigneeCountry") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCountry(null,txt_ConsigneeCountry)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="GRN">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <dxwgv:ASPxGridView ID="grid_DoIn" ClientInstanceName="grid_DoIn"
                                                    runat="server" KeyFieldName="Id" DataSourceID="dsDoIn" Width="100%"
                                                    OnBeforePerformDataSelect="grid_DoIn_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="GRN No" FieldName="DoNo" VisibleIndex="1" Width="100">
                                                             <DataItemTemplate>
                                                                <a href='javascript: parent.navTab.openTab("<%# Eval("DoNo") %>","/Modules/WareHouse/Job/DoInEdit.aspx?no=<%# Eval("DoNo") %>",{title:"<%# Eval("DoNo") %>", fresh:false, external:true});'><%# Eval("DoNo") %></a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="GRN Date" FieldName="DoDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Customer Ref" FieldName="CustomerReference" VisibleIndex="6" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RefDate" FieldName="CustomerDate" VisibleIndex="6" Width="80">
                                                            <DataItemTemplate> <%# SafeValue.SafeDateStr(Eval("CustomerDate")) %> </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="6" Width="50">
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
                                                            <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice_Import, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                         <td>
                                                            <dxe:ASPxButton ID="ASPxButton10" Width="150" runat="server" Text="Auto Purchase Inv" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Invoice_Import, "WH", txt_DoNo.GetText(), "PO","PL");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton11" Width="150" runat="server" Text="Auto Supplier CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"Draft")!="Canceled" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Invoice_Import, "WH", txt_DoNo.GetText(), "PO","CN");
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
                                                                <a onclick='ShowInvoice(Grid_Invoice_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintWh_invoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>")'>Print</a>
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
                                                            <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                         <td>
                                                            <dxe:ASPxButton ID="ASPxButton7" Width="150" runat="server" Text="Auto PO Payable" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Payable_Import, "WH", txt_DoNo.GetText(), "PO","PL");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton9" Width="150" runat="server" Text="Auto DO Payable" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Payable_Import, "WH", txt_DoNo.GetText(), "DI","PL");
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
                                      <dxtc:TabPage Text="Payment" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl2" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton15" Width="150" runat="server" Text="Add Payment" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddPayment(grid_Payment, txt_DoNo.GetText(), txt_PartyName.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                 <dxwgv:ASPxGridView ID="grid_Payment" ClientInstanceName="grid_Payment" runat="server"
                                                    KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" DataSourceID="dsPayment" OnBeforePerformDataSelect="grid_Payment_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                                                            Width="8%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowPayment(grid_Payment,"<%# Eval("DocNo") %>","Payment");'><%# Eval("DocNo") %></a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Catetory" FieldName="DocType1" VisibleIndex="3"
                                                            Width="5%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                                                            Width="8%">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="6"
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
                                                            Width="8%">
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
                                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <td>
                                                                <dxe:ASPxButton ID="ASPxButton13" Width="100%" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Closed"&&SafeValue.SafeString(Eval("DoStatus"),"USE")!="Canceled"%>' runat="server" Text="Upload Attachments"
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
                                                                            <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'>
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
                                </TabPages>

                            </dxtc:ASPxPageControl>
                        </div>

                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="900" EnableViewState="False">
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
