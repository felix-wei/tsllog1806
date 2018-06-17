<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="DoOutEdit.aspx.cs" Inherits="Pages_DoOutEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue</title>
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
        function OnCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                detailGrid.Refresh();
            }
           else if (v == "Had DoNo")
                alert("Had DoNo,please try again!");
            else if (v == "Fail")
                alert("Action Fail,please try again!");
            else if (v == "Permit")
                alert("Please enter Permit info !");
            else if (v == "LotNo")
                alert("Please enter LotNo !");
            else if (v == "No Balance")
                alert("No Balance,please try again!!");
            else if (v == "Ship") {
                alert("Action Success!");
                detailGrid.Refresh();
                grid_SKULine.Refresh();
            }
            else if (v == "Picked") {
                alert("Action Fail! please check Picked Qty");
            }
            else if(v=="Return")
            {
                alert("Action Success!");
                grid_PickDetail.Refresh();
                detailGrid.Refresh();
            }
            else if (v == "No Customer") {
                alert("Fail! No Customer");
            }
            else if (v != null) {
                alert("Action Success!");
                parent.navTab.openTab(v, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
                detailGrid.Refresh();
            }
        }
        function Refresh() {
            grid_PickDetail.Refresh();
            grid_SKULine.Refresh();
        }
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert("Please enter the Customer");
            }
            else if (v == "") {
                detailGrid.Refresh();
            }
            else if (v != null) {
                alert("Action Success!");
                parent.navTab.openTab(v, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + v + '&t=' + lbl_t.GetText(), { title: v, fresh: false, external: true });
                detailGrid.Refresh();
            }
        }
        function PopupContract(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetContentUrl('../SelectPage/SelectContract.aspx?party=' + this.txt_ConsigneeCode.GetText());
            popubCtr.SetHeaderText('Contract');
            popubCtr.Show();
        }
        function PopupUploadPhoto() {
            popubCtr.SetHeaderText('Upload Attachment');
            popubCtr.SetContentUrl('../Upload.aspx?Type=DO&Sn=' + txt_DoNo.GetText());
            popubCtr.Show();
        }
        function PrintDo(doNo, doType) {
            if (doType == "DoOut")
                window.open('/Modules/WareHouse/PrintView.aspx?document=wh_DoOut&master=' + doNo + "&house=0");
            else if (doType == "Picking")
                window.open('/Modules/WareHouse/PrintView.aspx?document=wh_Picking&master=' + doNo + "&house=0");
            else if (doType == "TallySheet")
                window.open('/Modules/WareHouse/PrintView.aspx?document=wh_TallySheet&master=' + doNo + "&house=0");
        }

        function PickStockProduct() {
            popubCtr.SetHeaderText('Pick Stock Product ');
            popubCtr.SetContentUrl('../SelectPage/PickStockProduct.aspx?type=OUT&Sn=' + txt_DoNo.GetText() + '&WhId=' + txt_WareHouseId.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_SKULine.Refresh();
            grid_PickDetail.Refresh();
        }
        function MultipleAdd() {
            popubCtr.SetHeaderText('SKU ');
            popubCtr.SetContentUrl('../SelectPage/SelectPoductFromStock.aspx?Type=OUT&Sn=' + txt_DoNo.GetText() + '&partyId=' + txt_ConsigneeCode.GetText() + '&WhId=' + txt_WareHouseId.GetText());
            popubCtr.Show();
        }
        function Multiple_AddPick() {
            popubCtr.SetHeaderText('Pick Detail ');
            popubCtr.SetContentUrl('../SelectPage/ProductList_do.aspx?partyId=' + txt_ConsigneeCode.GetText() + '&WhId=' + txt_WareHouseId.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultipleDo() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_PickDetail.Refresh();
        }
        function OnCostingCallBack(v) {
            if (v == "Success")
                grid_Cost.Refresh();
            else if (v != null)
                alert(v);
        }
        function GoList() {
            parent.navTab.openTab('Goods Receipt', "/Modules/WareHouse/Job/DoOutList.aspx?t=" + lbl_t.GetText(), { title: 'Goods Receipt', fresh: false, external: true });
        }
        function AddHouse(masterId) {
            parent.navTab.openTab('NEW OUT', "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + masterId + '&t=' + lbl_t.GetText(), { title: 'NEW OUT', fresh: false, external: true });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.LogEvent"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssue" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhDo"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssueDet" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhDoDet"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssueDet2" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhDoDet2"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssueDet3" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhDoDet3"
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
            <wilson:DataSource ID="dsPriority" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
                KeyMember="Id" FilterExpression="Type='Priority'" />
            <wilson:DataSource ID="dsOutStatus" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
                KeyMember="Id" FilterExpression="Type='OutStatus'" />
            <wilson:DataSource ID="dsPersonnel" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
                KeyMember="Id" FilterExpression="Type='Personnel'" />
            <wilson:DataSource ID="dsEquipmentNo" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
                KeyMember="Id" FilterExpression="Type='EquipmentNo'" />
            <wilson:DataSource ID="dsTptMode" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
                KeyMember="Id" FilterExpression="Type='TptMode'" />
            <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData" 
                KeyMember="Id"  FilterExpression="Type='Attribute'"/>
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhCosting" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsWhPacking" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhPacking" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>
                        <div style="display: none">
                            <dxe:ASPxLabel ID="lbl_t" ClientInstanceName="lbl_t" runat="server">
                            </dxe:ASPxLabel>
                        </div>
                        <dxe:ASPxLabel ID="lab_RefNo" runat="server" Text="No">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SchRefNo" Width="150" ClientInstanceName="txt_SchRefNo" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='DoOutEdit.aspx?no='+txt_SchRefNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           GoList();
                        }" />
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        AddHouse('0');
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
                            <table style="text-align: left; padding: 2px 2px 2px 2px; width: 900px">
                                <tr>
                                    <td width="85%">
                                        <table style="border: solid 1px black; padding: 2px 2px 2px 2px; width: 100%">
                                            <tr>
                                                <td>Print Document:&nbsp;&nbsp;
                                        <a href="#" onclick='PrintDo("<%# Eval("DoNo") %>","DoOut")'>DO</a>
                                                    &nbsp;&nbsp;
                                        <a href="#" onclick='PrintDo("<%# Eval("DoNo") %>","Picking")'> Pick Ticket</a>
                                                     &nbsp;&nbsp;
                                        <a href="#" onclick='PrintDo("<%# Eval("DoNo") %>","TallySheet")'> Tally Sheet</a>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="100" AutoPostBack="false"
                                            Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Save">
                                            <ClientSideEvents Click="function(s,e) {
                                                   detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                                 }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_CloseJob" Width="90" runat="server" Text="Close Job" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('CloseJob',OnCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Void" ClientInstanceName="btn_Void" runat="server" Width="100" Text="Void" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS"&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="RETURN" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_Void.GetText()+' Job?'))
                                                                    {
                                                                        detailGrid.GetValuesOnCustomCallback('Void',OnCallBack);                 
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
                                    <td style="width: 120px"></td>
                                    <td></td>
                                    <td style="width: 120px"></td>
                                    <td></td>
                                    <td style="width: 120px"></td>
                                    <td></td>
                                    <td style="width: 120px"></td>
                                </tr>
                                <tr>
                                    <td>No</td>
                                    <td>
                                         <dxe:ASPxButtonEdit ID="txt_DoNo" ClientInstanceName="txt_DoNo" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("DoNo")%>' Width="150" HorizontalAlign="Left" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                              
                                                                    }" />
                                    </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>Type</td>
                                    <td>
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="150" ID="cmb_Priority"
                                            runat="server" Text='<%# Eval("Priority") %>' DropDownStyle="DropDown">
                                            <Items>
                                                <dxe:ListEditItem Text="EXPORT" Value="EXPORT" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_IssueDate" Width="100%" runat="server" Value='<%# Eval("DoDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>Shippment Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_ExceptedDate" Width="100%" runat="server" Value='<%# Eval("ExpectedDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
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
                                                <dxe:ASPxTextBox runat="server" Width="260" ReadOnly="true" Text='<%# Eval("PartyName") %>' BackColor="Control" ID="txt_ConsigneeName" ClientInstanceName="txt_ConsigneeName">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </table>
                                    </td>
                                    <td>Customs Ref</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_CustReference" Text='<%# Eval("CustomerReference") %>' ClientInstanceName="txt_CustReference">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Ref Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="txt_CustDate" Width="100%" runat="server" Value='<%# Eval("CustomerDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2">Address</td>
                                    <td colspan="3" rowspan="2">
                                        <dxe:ASPxMemo ID="memo_Address" Rows="5" Width="100%" ClientInstanceName="memo_Address"
                                            runat="server" Text='<%# Eval("PartyAdd") %>'>
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>SO No</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_PONo" Text='<%# Eval("PONo") %>' ClientInstanceName="txt_PONo">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>SO Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PODate" Width="100%" runat="server" Value='<%# Eval("PoDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Permit No</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_PermitNo" Text='<%# Eval("PermitNo") %>' ClientInstanceName="txt_PermitNo">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Approval Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PermitApprovalDate" runat="server" Value='<%# Eval("PermitApprovalDate") %>' Width="100%" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td  rowspan="2">Remark</td>
                                    <td colspan="3" rowspan="2">
                                        <dxe:ASPxMemo runat="server" Width="100%" Rows="4" ID="txt_Remark1" Text='<%# Eval("Remark1") %>' ClientInstanceName="txt_Remark1">
                                        </dxe:ASPxMemo>
                                    </td>
                                <td rowspan="2">Other Permit</td>
                                <td rowspan="2">
                                   <dxe:ASPxMemo ID="txt_OtherPermit" Rows="4" runat="server" Width="100%" Text='<%# Eval("OtherPermit") %>'></dxe:ASPxMemo>                             

                                </td>
                                <td>Permit By</td>
                                <td>
                                    <dxe:ASPxComboBox ID="txt_PermitBy" Width="100%" runat="server" Value='<%# Eval("PermitBy") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                        <Items>
                                            <dxe:ListEditItem Text="SBL" Value="SBL" />
                                            <dxe:ListEditItem Text="Client" Value="Client" />
                                        </Items>
                                    </dxe:ASPxComboBox> 
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Permit Expiry Date
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PermitExpiryDate" runat="server" Value='<%# Eval("PermitExpiryDate") %>' Width="100%" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                     <td rowspan="2">
                                        <a href="#" onclick="PopupPartyAdr(null,memo_DeliveryTo);">Delivery To</a>
                                    </td>
                                    <td colspan="3" rowspan="2">
                                        <dxe:ASPxMemo ID="memo_DeliveryTo" Rows="4" Width="100%" ClientInstanceName="memo_DeliveryTo"
                                            runat="server" Text='<%# Eval("DeliveryTo") %>'>
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Mode</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_ModelType" Width="100%" runat="server" Value='<%# Eval("ModelType") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                        <Items>
                                            <dxe:ListEditItem Text="FCL" Value="LCL" />
                                            <dxe:ListEditItem Text="LCL" Value="FCL" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                    <td>TPT Mode</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cb_TptMode" Width="100%" runat="server" DataSourceID="dsTptMode" EnableIncrementalFiltering="true" TextField="Code" Value='<%# Eval("TptMode") %>' ValueField="Code" ValueType="System.String">
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Palletized</td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="ack_PalletizedInd" runat="server" Width="100%" Value='<%# Eval("PalletizedInd") %>'>
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td>Contractor</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_Contractor" ClientInstanceName="txt_Contractor" runat="server" Value='<%# Bind("Contractor") %>' Width="150">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupParty(null,txt_Contractor);
                                                                           }" />
                                    </dxe:ASPxButtonEdit>
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
                                <td>Status</td>
                                <td>
                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" DataSourceID="dsOutStatus" Width="100%" ID="cmb_Status"
                                        runat="server" Text='<%# Eval("DoStatus") %>' TextField="Code" ValueField="Code" DropDownStyle="DropDown">
                                    </dxe:ASPxComboBox>
                                </td>
                                 <td>Quotation</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="ASPxButtonEdit1" ClientInstanceName="txt_Quotation" runat="server" Value='<%# Bind("Quotation") %>' Width="160">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupContract(txt_Quotation,null);
                                                                           }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                  <td>
                                   Cont No
                               </td>
                                <td>
                                     <dxe:ASPxTextBox ID="txt_ContainerNo" Width="100%" runat="server" Text='<%# Eval("ContainerNo") %>'></dxe:ASPxTextBox>
                                </td>
                            </tr>
                             <tr>
                                  <td>Equipment No</td>
                                <td>
                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" DataSourceID="dsEquipmentNo" Width="160" ID="cmb_EquipmentNo"
                                        runat="server" TextField="Code" Text='<%# Eval("EquipNo") %>' ValueField="Code" DropDownStyle="DropDown">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Personnel</td>
                                <td>
                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" DataSourceID="dsPersonnel" Width="150" ID="cmb_Personnel"
                                        runat="server" TextField="Code" Text='<%# Eval("Personnel") %>' ValueField="Code" DropDownStyle="DropDown">
                                    </dxe:ASPxComboBox>
                                </td>
                                 <td>Operation Start
                                 </td>
                                 <td>
                                     <dxe:ASPxDateEdit ID="date_Operation" runat="server" Value='<%# Eval("OperationStart") %>' Width="100%" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                     </dxe:ASPxDateEdit>
                                 </td>
                                 <td>JobNo</td>
                                <td>
                                     <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?jobNo=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
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
                                                <td style="width: 80px;">Status</td>
                                                  <td style="width: 100px;">
                                                      <dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text='<%#Eval("StatusCode") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                        <hr>
                                    </td>
                                </tr>
                            </table>
                            <table style="display: none;">
                                <tr>
                                </tr>
                                <tr>
                                    <td>PostalCode</td>
                                    <td colspan="4">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="100">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_PostalCode" Text='<%# Eval("PartyPostalcode") %>' ClientInstanceName="txt_PostalCode">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>City</td>
                                                <td width="100">
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
                                                <td>Country</td>
                                                <td width="100">
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
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>CustomsSealNo</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_CustomsSealNo" Text='<%# Eval("CustomsSealNo") %>' ClientInstanceName="txt_CustomsSealNo">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ExternalReference</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_ExternalReference" Text='<%# Eval("ExternalReference") %>' ClientInstanceName="txt_ExternalReference">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>ExternalDate</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_ExternalDate" Width="100%" runat="server" Value='<%# Eval("ExternalDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Route</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Route" Text='<%# Eval("Route") %>' ClientInstanceName="txt_Route">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Group</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Group" Text='<%# Eval("GroupId") %>' ClientInstanceName="txt_Group">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PopulatetoReceipt</td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="ack_PopulatetoReceipt" runat="server">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td>ReceiptNo</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_ReceiptNo" Text='<%# Eval("ReceiptNo") %>' ClientInstanceName="txt_ReceiptNo">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>PopulatetoXDock</td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="ack_PopulatetoXDock" runat="server">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td>XDockReference</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_XDockReference" Text='<%# Eval("XDockReference") %>' ClientInstanceName="txt_XDockReference">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>TransporterName</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_TransName" Text='<%# Eval("TptName") %>' ClientInstanceName="txt_TransName">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Quotation</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Quotation" Text='<%# Eval("Quotation") %>' ClientInstanceName="txt_Quotation">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Remark2</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo runat="server" Width="100%" Rows="4" ID="txt_Remark2" Text='<%# Eval("Remark2") %>' ClientInstanceName="txt_Remark2">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Remark3</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo runat="server" Width="100%" Rows="4" ID="txt_Remark3" Text='<%# Eval("Remark3") %>' ClientInstanceName="txt_Remark3">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Remark4</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo runat="server" Width="100%" Rows="4" ID="txt_Remark4" Text='<%# Eval("Remark4") %>' ClientInstanceName="txt_Remark4">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl_Hbl" Width="1000px" Height="600px" ActiveTabIndex="0">
                                <TabPages>
                                    <dxtc:TabPage Text="SKU List" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_pod" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddSKULine" Width="150" runat="server" Text="Add SKU" AutoPostBack="false" UseSubmitBehavior="false"
                                                 Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                    <ClientSideEvents Click="function(s,e) {
                                                       grid_SKULine.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButton ID="btn_AddSKULine1" Width="150" runat="server" Text="Pick From Stock" AutoPostBack="false" UseSubmitBehavior="false"
                                                                    Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                                    <ClientSideEvents Click="function(s,e) {
                                                       MultipleAdd();
                                                        }" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                           <%-- <td>
                                                            <dxe:ASPxButton ID="ASPxButton7" Width="90" runat="server" Text="Pick All" AutoPostBack="False"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL"&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="RETURN"  %>'>
                                                                <ClientSideEvents Click="function(s, e) {
                                                                  PickStockProduct();
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>--%>
                                                            <td>
                                                            <dxe:ASPxButton ID="ASPxButton11" Width="90" runat="server" Text="Ship All" AutoPostBack="False"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL"&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="RETURN"  %>'>
                                                                <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('Ship',OnCallBack);
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="width: 1000px; overflow-y: scroll;"><%-- --%>
                                                    <dxwgv:ASPxGridView ID="grid_SKULine" ClientInstanceName="grid_SKULine" runat="server" OnRowInserting="grid_SKULine_RowInserting"
                                                        OnRowDeleting="grid_SKULine_RowDeleting" OnRowUpdating="grid_SKULine_RowUpdating" OnBeforePerformDataSelect="grid_SKULine_BeforePerformDataSelect"
                                                        KeyFieldName="Id" Width="1800px" AutoGenerateColumns="False" DataSourceID="dsIssueDet" OnInit="grid_SKULine_Init" OnInitNewRow="grid_SKULine_InitNewRow" OnRowInserted="grid_DoDet_RowInserted" OnRowUpdated="grid_DoDet_RowUpdated" OnRowDeleted="grid_DoDet_RowDeleted">
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" FieldName="DoInId" VisibleIndex="0" Width="40">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_SKULine.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                              <dxe:ASPxButton ID="btn_SKULine_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_SKULine.DeleteRow("+Container.VisibleIndex+");grid_PickDetail.Refresh(); }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_SKULine.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_SKULine_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_SKULine.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_DoInId" runat="server" ClientInstanceName="txt_DoInId" Width="150" Text='<%# Bind("DoInId") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="JobStatus" VisibleIndex="1" Width="80">
                                                                <PropertiesComboBox Width="80">
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                                        <dxe:ListEditItem Text="Picked" Value="Picked" />
                                                                        <dxe:ListEditItem Text="Shipped" Value="Shipped" />
                                                                        <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                                                                    </Items>
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="1" Width="60">
                                                             <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_Det1_LotNo" ClientInstanceName="txt_Det1_LotNo" runat="server" Text='<%# Bind("LotNo") %>' Width="60">
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="ProductCode" VisibleIndex="2" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_SKULine_Product" runat="server" Value='<%# Bind("ProductCode") %>' Width="80">
                                                                         <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupProductFromDoIn(txt_SKULine_Product,txt_Det1_LotNo,txt_Description,null,null,txt_Uom1,null,null,cb_Att1,cb_Att2,cb_Att3,cb_Att4,cb_Att5,cb_Att6,txt_WareHouseId.GetText(),txt_ConsigneeCode.GetText());
                                                                           }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="3" Width="100">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_Description" runat="server" ClientInstanceName="txt_Description" Text='<%# Bind("Des1") %>' ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                                <PropertiesTextEdit Width="100" />
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Expected" FieldName="Qty4" CellStyle-HorizontalAlign="Left" VisibleIndex="3" Width="60">
                                                                <PropertiesSpinEdit NumberType="Float" Increment="3" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="In Transit" FieldName="Qty5" CellStyle-HorizontalAlign="Left" VisibleIndex="3" Width="60">
                                                                <PropertiesSpinEdit NumberType="Float" Increment="3" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty1" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Float" Increment="3" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Bad Qty" FieldName="Qty2" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Float" Width="40" Increment="3" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Pallet Qty" FieldName="Qty3" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Integer" Width="40" Increment="0" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                             <dxwgv:GridViewDataSpinEditColumn Caption="Gross Weight" FieldName="GrossWeight" VisibleIndex="8" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("GrossWeight") %>' Increment="3" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                             <dxwgv:GridViewDataSpinEditColumn Caption="Nett Weight" FieldName="NettWeight" VisibleIndex="8" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("NettWeight") %>' Increment="3" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" CellStyle-HorizontalAlign="Left" VisibleIndex="4" Width="50">
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                             <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="4" Width="120">
                                                                  <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="text_Remark" ClientInstanceName="cb_Remark" Width="120"  runat="server" Text='<%# Bind("Remark") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContainerNo" VisibleIndex="4" Width="120">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="text_ContainerNo" ClientInstanceName="cb_Remark" Width="120"  runat="server" Text='<%# Bind("ContainerNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="4" Width="120">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="text_PalletNo" ClientInstanceName="cb_Remark" Width="120" runat="server" Text='<%# Bind("PalletNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="UNIT" FieldName="Uom1" VisibleIndex="4" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="txt_Uom1" ClientInstanceName="txt_Uom1" Width="60" DataSourceID="dsPackageType" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Uom1") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                             <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="4" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyPack" runat="server" Width="40" ClientInstanceName="spin_QtyPack" Value='<%# Bind("QtyPackWhole") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
<%--                                                            <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="5" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyWhole" runat="server" Width="40" ClientInstanceName="spin_QtyWhole" Value='<%# Bind("QtyWholeLoose") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>--%>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Grade" FieldName="Att1" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att1" ClientInstanceName="cb_Att1" Width="60" runat="server" Text='<%# Bind("Att1") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Color" FieldName="Att2" VisibleIndex="7" Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att2" ClientInstanceName="cb_Att2" Width="60" runat="server" Text='<%# Bind("Att2") %>' >
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Size" FieldName="Att3" VisibleIndex="7" Width="60">
                                                               <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att3" ClientInstanceName="cb_Att3" Width="60" runat="server" Text='<%# Bind("Att3") %>' >
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="Att4" VisibleIndex="7" Width="60" >
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att4" ClientInstanceName="cb_Att4" Width="60" runat="server" Text='<%# Bind("Att4") %>' >
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Clot1" FieldName="Att5" VisibleIndex="7"  Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att5" ClientInstanceName="cb_Att5" Width="60" runat="server" Text='<%# Bind("Att5") %>' >
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="ATT" FieldName="Att6"  VisibleIndex="7" Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att6" ClientInstanceName="cb_Att6" Width="60" runat="server" Text='<%# Bind("Att6") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataDateColumn Caption="ExpiryDate" FieldName="ExpiredDate" VisibleIndex="7" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxDateEdit ID="txt_ExpiredDate" runat="server" Value='<%# Bind("ExpiredDate") %>' Width="100" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataDateColumn>
                                                             <dxwgv:GridViewDataSpinEditColumn Caption="STK" FieldName="QtyLooseBase" VisibleIndex="8" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("QtyLooseBase") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="PKG UOM" FieldName="Uom2" VisibleIndex="10" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="txt_Uom2" ClientInstanceName="txt_Uom2" Width="60" DataSourceID="dsPackageType" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Uom2") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                   
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" VisibleIndex="11" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="txt_Uom3" ClientInstanceName="txt_Uom3" Width="60" DataSourceID="dsPackageType" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Uom3") %>' DropDownStyle="DropDown">
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                             <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="11" Width="50">
                                                                <DataItemTemplate>
                                                                     <%# Eval("Packing") %>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Pick Detail" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl2" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Pick Detail" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                       Multiple_AddPick();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                             <dxe:ASPxButton ID="ASPxButton12" Width="90" runat="server" Text="Return" AutoPostBack="False"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="CLS" %>'>
                                                                <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('Return',OnCallBack);
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <div style="WIDTH: 1000px; overflow-y: scroll;"><%-- --%>
                                                    <dxwgv:ASPxGridView ID="grid_PickDetail" ClientInstanceName="grid_PickDetail" runat="server" Styles-Cell-Paddings-Padding="2" Styles-EditForm-Paddings-Padding="2"
                                                        KeyFieldName="Id" Width="1800" AutoGenerateColumns="False" DataSourceID="dsIssueDet2" OnBeforePerformDataSelect="grid_PickDetail_BeforePerformDataSelect"
                                                        OnInit="grid_PickDetail_Init" OnRowDeleting="grid_PickDetail_RowDeleting"
                                                        OnInitNewRow="grid_PickDetail_InitNewRow" OnRowInserting="grid_PickDetail_RowInserting" OnRowUpdating="grid_PickDetail_RowUpdating" OnRowInserted="grid_PickDetail_RowInserted" OnRowUpdated="grid_PickDetail_RowUpdated">
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_PickDetail_edit" runat="server" Text="Edit" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_PickDetail.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_PickDetail_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    Text="Delete" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_PickDetail.DeleteRow("+Container.VisibleIndex+");grid_SKULine.Refresh(); }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_PickDetail_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_PickDetail.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_PickDetail_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_PickDetail.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DoType" UnboundType="String" VisibleIndex="0" Width="40">
                                                                </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" UnboundType="String" VisibleIndex="0" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="btn_Location" ClientInstanceName="btn_Location" Text='<%# Bind("Location")%>'
                                                                        runat="server" Width="80" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupLo(btn_Location,null);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn
                                                                Caption="Process" FieldName="ProcessStatus" VisibleIndex="0" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cmb_Status" ClientInstanceName="cmb_Status" runat="server" Value='<%# Bind("ProcessStatus") %>'
                                                                        Width="100" DropDownWidth="100" DropDownStyle="DropDownList"
                                                                        ValueType="System.String" EnableCallbackMode="true"
                                                                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="80">
                                                                        <Items>
                                                                            <dxe:ListEditItem Value="Draft" Text="Draft" Selected="true" />
                                                                            <dxe:ListEditItem Value="Waiting" Text="Waiting" />
                                                                            <dxe:ListEditItem Value="Delivered" Text="Delivered" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="1" Width="80">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_Det2_LotNo" Width="80" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Bind("LotNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="Product" VisibleIndex="2" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Bind("Product") %>' Width="60">
                                                                     </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="3" Width="100">
                                                                <PropertiesTextEdit Width="100" />
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty1" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Float" Increment="3" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Bad Qty" FieldName="Qty2" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Float" Width="40" Increment="3" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Pallet Qty" FieldName="Qty3" VisibleIndex="4" Width="40">
                                                                <PropertiesSpinEdit NumberType="Integer" Width="40" Increment="0" SpinButtons-ShowIncrementButtons="false">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Gross Weight" FieldName="GrossWeight" VisibleIndex="8" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("GrossWeight") %>' Increment="3" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Nett Weight" FieldName="NettWeight" VisibleIndex="8" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("NettWeight") %>' Increment="3" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                             <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" ReadOnly="true" CellStyle-BackColor="Control" CellStyle-HorizontalAlign="Left" VisibleIndex="4" Width="50">
                                                                <PropertiesSpinEdit NumberType="Float" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Width="50">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="4" Width="120">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="text_Remark" ClientInstanceName="cb_Remark" Width="120" runat="server" Text='<%# Bind("Remark") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContainerNo" VisibleIndex="4" Width="120">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="text_ContainerNo" ClientInstanceName="cb_Remark" Width="120" runat="server" Text='<%# Bind("ContainerNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="4" Width="120">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="text_PalletNo" ClientInstanceName="cb_Remark" Width="120" runat="server" Text='<%# Bind("PalletNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                             <dxwgv:GridViewDataTextColumn Caption="UNIT" FieldName="Uom1" VisibleIndex="4" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom1" ReadOnly="true" BackColor="Control" Width="50" ClientInstanceName="txt_Uom1" Text='<%# Bind("Uom1") %>'>
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                             <dxwgv:GridViewDataSpinEditColumn Caption="PKG" FieldName="QtyPackWhole" VisibleIndex="4" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyPack" runat="server" Width="40" ClientInstanceName="spin_QtyPack" Value='<%# Bind("QtyPackWhole") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
<%--                                                            <dxwgv:GridViewDataSpinEditColumn Caption="UNIT" FieldName="QtyWholeLoose" VisibleIndex="5" Width="40">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyWhole" runat="server" Width="40" ClientInstanceName="spin_QtyWhole" Value='<%# Bind("QtyWholeLoose") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>--%>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Grade" FieldName="Att1" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att1" ClientInstanceName="cb_Att1" Width="60" runat="server" Text='<%# Bind("Att1") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Color" FieldName="Att2" VisibleIndex="7" Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att2" ClientInstanceName="cb_Att2" Width="60" runat="server" Text='<%# Bind("Att2") %>' >
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Size" FieldName="Att3" VisibleIndex="7" Width="60">
                                                               <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att3" ClientInstanceName="cb_Att3" Width="60" runat="server" Text='<%# Bind("Att3") %>' >
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="Att4" VisibleIndex="7" Width="60" >
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att4" ClientInstanceName="cb_Att4" Width="60" runat="server" Text='<%# Bind("Att4") %>' >
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Clot1" FieldName="Att5" VisibleIndex="7"  Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att5" ClientInstanceName="cb_Att5" Width="60" runat="server" Text='<%# Bind("Att5") %>' >
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="ATT" FieldName="Att6"  VisibleIndex="7" Width="60">
                                                                 <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="cb_Att6" ClientInstanceName="cb_Att6" Width="60" runat="server" Text='<%# Bind("Att6") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="STK" FieldName="QtyLooseBase" VisibleIndex="7" Width="60">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_QtyBase" runat="server" Width="40" ClientInstanceName="spin_QtyBase" Value='<%# Bind("QtyLooseBase") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                    </dxe:ASPxSpinEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="PKG UOM" FieldName="Uom2" VisibleIndex="9" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom2" ReadOnly="true" BackColor="Control" Width="50" ClientInstanceName="txt_Uom2" Text='<%# Bind("Uom2") %>'>
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" VisibleIndex="10" Width="50">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit runat="server" ID="txt_Uom3" ReadOnly="true" BackColor="Control" Width="50" ClientInstanceName="txt_Uom3" Text='<%# Bind("Uom3") %>'>
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                          <%--  <dxwgv:GridViewDataComboBoxColumn Caption="Att7" FieldName="Att7" ReadOnly="true" VisibleIndex="51" Width="60">
                                                                <PropertiesComboBox DataSourceID="dsWhMastData" TextField="Code" ValueField="Code"  DropDownStyle="DropDown" />
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att8" FieldName="Att8" ReadOnly="true" VisibleIndex="51" Width="60">
                                                                <PropertiesComboBox DataSourceID="dsWhMastData" TextField="Code" ValueField="Code"  DropDownStyle="DropDown" />
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att9" FieldName="Att9" ReadOnly="true" VisibleIndex="51" Width="60">
                                                                <PropertiesComboBox DataSourceID="dsWhMastData" TextField="Code" ValueField="Code"  DropDownStyle="DropDown" />
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="Att10" FieldName="Att10" ReadOnly="true" VisibleIndex="51" Width="60">
                                                                <PropertiesComboBox DataSourceID="dsWhMastData" TextField="Code" ValueField="Code"  DropDownStyle="DropDown" />
                                                            </dxwgv:GridViewDataComboBoxColumn>--%>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="IsSch" FieldName="IsSch" Visible="false" VisibleIndex="51" Width="60">
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                        </Columns>
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Container" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl5" runat="server">
                                                <dxe:ASPxButton ID="ASPxButton13" Width="150" runat="server" Text="Add Container" AutoPostBack="false" UseSubmitBehavior="false"
                                                    Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                    <ClientSideEvents Click="function(s,e) {
                                                       grid_Cont.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <div style="WIDTH: 1000px; overflow-y: scroll;"><%-- --%>
                                                    <dxwgv:ASPxGridView ID="Grid_Cont" ClientInstanceName="grid_Cont" runat="server"
                                                        KeyFieldName="Id" DataSourceID="dsIssueDet3" Width="1200px" OnBeforePerformDataSelect="Grid_Cont_BeforePerformDataSelect"
                                                        OnRowUpdating="Grid_Cont_RowUpdating" OnRowInserting="Grid_Cont_RowInserting"
                                                        OnRowDeleting="Grid_Cont_RowDeleting" OnInitNewRow="Grid_Cont_InitNewRow"
                                                        OnInit="Grid_Cont_Init">
                                                        <SettingsBehavior ConfirmDelete="True" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Cont.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Cont.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Cont.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Cont.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContainerNo" Width="120" VisibleIndex="2" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="false">
                                                                <PropertiesTextEdit Width="120px"></PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn
                                                            Caption="Status" FieldName="ContainerStatus" VisibleIndex="3" Width="120">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxComboBox ID="cbb_StatusCode" ClientInstanceName="cbb_StatusCode" runat="server" Width="120" Value='<%# Bind("ContainerStatus") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="New" Text="New" />
                                                                                <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                                                <dxe:ListEditItem Value="Started" Text="Started" />
                                                                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                                <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Seal No" FieldName="SealNo" VisibleIndex="3" Width="120" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="false">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn
                                                                Caption="Cont Type" FieldName="ContainerType" VisibleIndex="4" Width="120">
                                                                <PropertiesComboBox
                                                                    ValueType="System.String" TextFormatString="{0}" DataSourceID="dsUom" DropDownWidth="200"
                                                                    TextField="Description" EnableIncrementalFiltering="true"
                                                                    ValueField="Code">
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn
                                                                            FieldName="Code" Width="70px" />
                                                                        <dxe:ListBoxColumn
                                                                            FieldName="Description" Width="100%" />
                                                                    </Columns>
                                                                    <Buttons>
                                                                        <dxe:EditButton Text="Clear"></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                if(e.buttonIndex == 0){
                                                                s.SetText('');
                                                                 }
                                                                }" />
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Weight" FieldName="Weight" VisibleIndex="5" Width="100">
                                                                <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000" Increment="0" NumberFormat="Custom">
                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                    </SpinButtons>
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Volume" FieldName="M3" VisibleIndex="6" Width="100">
                                                                <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000" Increment="0" NumberFormat="Custom">
                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                    </SpinButtons>
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="8" Width="60">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn Caption="PackageType" FieldName="PkgType" VisibleIndex="9" Width="120">
                                                                <PropertiesComboBox ValueType="System.String" DataSourceID="dsPackageType" TextField="Code" EnableIncrementalFiltering="true"
                                                                    ValueField="Code">
                                                                    <Buttons>
                                                                        <dxe:EditButton Text="Clear"></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                    if(e.buttonIndex == 0){
                                                                        s.SetText('');
                                                                 }
                                                                }" />
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataDateColumn Caption="Job Start" FieldName="JobStart" VisibleIndex="7" Width="150">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxDateEdit ID="txt_ExpiredDate" runat="server" Value='<%# Bind("JobStart") %>' Width="150" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm">
                                                                    </dxe:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataDateColumn>
                                                         <dxwgv:GridViewDataDateColumn Caption="Job End" FieldName="JobEnd" VisibleIndex="7" Width="150">
                                                             <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>   
                                                             <EditItemTemplate>
                                                                    <dxe:ASPxDateEdit ID="txt_ExpiredDate" runat="server" Value='<%# Bind("JobEnd") %>' Width="150" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm">
                                                                    </dxe:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataDateColumn>
                                                        </Columns>
                                                        <SettingsEditing Mode="InLine" />
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Transport" Visible="true">
                                        <ContentCollection>
                                           <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>Transporter
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxButtonEdit ID="txt_TransportName" ClientInstanceName="txt_TransportName" runat="server"
                                                                Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("TransportName") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupNewParty(null,txt_TransportName,'Transporter')
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Use Transport</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="txt_UseTransport" runat="server" Width="150px" DropDownStyle="DropDownList" Text='<%# Eval("UseTransport") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                    <dxe:ListEditItem Value="No" Text="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Transport Status</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="txt_TransportStatus" runat="server" Width="150px" DropDownStyle="DropDownList" Text='<%# Eval("TransportStatus") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Value="New" Text="New" />
                                                                    <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                                    <dxe:ListEditItem Value="InTransit" Text="InTransit" />
                                                                    <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                    <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Transport Start</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_TransportStart" runat="server" Value='<%# Eval("TransportStart") %>' Width="150px" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm">
                                                            </dxe:ASPxDateEdit>

                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td>Driver Name</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_DriveName" Text='<%# Eval("DriverName") %>' ClientInstanceName="txt_DriveName">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Driver IC</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_DriverIC" Text='<%# Eval("DriverIC") %>' ClientInstanceName="txt_DriverIC">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Transport End</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_TransportEnd" runat="server" Value='<%# Eval("TransportEnd") %>' Width="150px" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        </tr>
                                                    <tr>
                                                        
                                                        <td>Driver Tel</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_DriverTel" Text='<%# Eval("DriverTel") %>' ClientInstanceName="txt_DriveName">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Vehicle No</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_VechicleNo" Text='<%# Eval("VehicleNo") %>' ClientInstanceName="txt_DriveName">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                         <td>Job No</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_TptJobNo" Text='<%# Eval("TptJobNo") %>' ClientInstanceName="txt_DriveName">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td colspan="7">
                                                            <dxe:ASPxMemo ID="meno_Remark" Rows="3" Width="100%" runat="server" Text='<%# Eval("Remarks") %>'></dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Packing" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                               <dxe:ASPxButton ID="ASPxButton9" Width="150" runat="server" Text="Add Packing" AutoPostBack="false" UseSubmitBehavior="false"
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
                                    <dxtc:TabPage Text="Permit/Attachments" Name="Attachments" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl8" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Upload Attachments" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
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
                                                        <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContainerNo"></dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table width="95%">
                                                                <tr style="vertical-align:top">
                                                                    <td>Remark
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Bind("FileNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                     <td>Cont No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Width="170" Text='<%# Bind("ContainerNo") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
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
                                    <dxtc:TabPage Text="Freight">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>Use Freight</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="txt_UseFreight" runat="server" Width="180px" DropDownStyle="DropDownList" Text='<%# Eval("UseFreight") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                    <dxe:ListEditItem Value="No" Text="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Freight Status</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_FreightStatus" runat="server" Width="180px" DropDownStyle="DropDownList" Text='<%# Eval("FreightStatus") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Value="New" Text="New" />
                                                                    <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                                    <dxe:ListEditItem Value="InTransit" Text="InTransit" />
                                                                    <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                    <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
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
                                                        <td>OBL/AWB</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="180" ID="txt_OBL" Text='<%# Eval("Obl")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>HBL/HAWB</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_HBL" runat="server" Text='<%# Eval("Hbl") %>' Width="180">
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
                                                            <dxe:ASPxDateEdit ID="txt_EtaPod" Width="180" runat="server" ClientInstanceName="date_Eta"
                                                                Value='<%# Bind("Eta")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>Etd</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="txt_EtdPol" Width="180" runat="server" ClientInstanceName="date_Etd"
                                                                Value='<%# Bind("Etd")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>EtaDest</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_Etd" Width="140" runat="server" ClientInstanceName="date_EtaDest"
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
                                                                Width="655" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Carrier") %>'>
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
                                                            <dxe:ASPxTextBox ID="txt_Vehno" Width="180" ClientInstanceName="txt_Vehno"
                                                                runat="server" Text='<%# Eval("Vehicle") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>COO</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_COO" Width="180" ClientInstanceName="txt_COO"
                                                                runat="server" Text='<%# Eval("Coo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>ContainerYard</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_ContainerYard" Width="140" ClientInstanceName="txt_ContainerYard"
                                                                runat="server" Text='<%# Eval("ContainerYard") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
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
                                                                        <dxe:ASPxTextBox runat="server" Width="257"  ID="txt_AgentName" Text='<%# Eval("AgentName") %>' ClientInstanceName="txt_AgentName">
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
                                                                            PopupParty(txt_NotifyCode,txt_NotifyName,txt_NotifyContact,txt_NotifyTelexFax,txt_NotifyCountry,txt_NotifyCity,txt_NotifyPostalCode,memo_NotifyAddress,'A');
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
                                                            <dxe:ASPxButton ID="ASPxButton16" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Invoice" ClientInstanceName="Grid_Invoice"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsInvoice" Width="100%"
                                                    OnBeforePerformDataSelect="Grid_Invoice_BeforePerformDataSelect">
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
                                                            <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
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
                                                
                                                <table><tr><td>
                                                <dxe:ASPxButton ID="btn_Costing" Width="150" runat="server" Text="Import Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 &&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        detailGrid.GetValuesOnCustomCallback('Costing',OnCostingCallBack);
                                                        }" />
                                                </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                <dxe:ASPxButton ID="ASPxButton10" Width="150" runat="server" Text="Add Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 &&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        grid_Cost.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>

                                                    </td>
                                                       </tr></table>
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
                                    <dxtc:TabPage Text="Log" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl6" runat="server">
                                                <dxwgv:ASPxGridView ID="grid_Log" ClientInstanceName="grid_Log" runat="server"
                                                    KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" DataSourceID="dsLog" OnBeforePerformDataSelect="grid_Log_BeforePerformDataSelect">
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
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
