<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="ImportEdit.aspx.cs" Inherits="Pages_ImportEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import </title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Import_Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/ImportEdit.js"></script>
    <script type="text/javascript">
        var isUpload = false;
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsRefCont" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaImportMkg" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsJob" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.SeaImport"
            KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsFullJob" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.SeaImport"
            KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaAttachment" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsMarking" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaImportMkg" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsDn" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaDn" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
         <wilson:DataSource ID="dsCertificate" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaCertificate" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lab_RefNo" runat="server" Text="Import No">
                        </dxe:ASPxLabel>
                    
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_RefNo" Width="150" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                        window.location='ImportEdit.aspx?no='+txt_RefNo.GetText();
                    }" />
                    </dxe:ASPxButton>
                </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='ImportRefList.aspx?refType='+lab_RefType.GetText();
                        }" />
                        </dxe:ASPxButton>
                    </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Export" ClientInstanceName="detailGrid" runat="server"
            KeyFieldName="SequenceId" Width="900px" AutoGenerateColumns="False" DataSourceID="dsJob"
            OnBeforePerformDataSelect="grid_Export_DataSelect" OnInitNewRow="grid_Export_InitNewRow"
            OnInit="grid_Export_Init"  OnCustomDataCallback="grid_Export_CustomDataCallback"  OnCustomCallback="grid_Export_CustomCallback"
            OnHtmlEditFormCreated="grid_Export_HtmlEditFormCreated"  >
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <Settings ShowColumnHeaders="false" />
            <Templates>
                <EditForm>
                    <div style="padding: 2px 2px 2px 2px">

                        <table style="text-align: right; padding: 2px 2px 2px 2px; width: 900px">
                            <tr>
                                <td width="60%"></td>
                                <td style="display: none">
                                    <dxe:ASPxTextBox ID="lab_RefType" ClientInstanceName="lab_RefType" runat="server" Text="">
                                    </dxe:ASPxTextBox>
                                </td>


                                <td>
                                    <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            detailGrid.PerformCallback('Save');
                                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_VoidHouse" Width="100" runat="server" ClientInstanceName="btn_VoidHouse" Text="Void" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                                    if(confirm('Confirm ' +btn_VoidHouse.GetText() +' this job?'))
                                                                    {
                                                                        detailGrid.GetValuesOnCustomCallback('VoidHouse',OnCloseCallBack);
                                                                    }
                                             }" />
                                    </dxe:ASPxButton>
                                    <div style="display: none">
                                        <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="txtHouseId" runat="server" ReadOnly="true"
                                            BackColor="Control" Text='<%# Eval("SequenceId") %>' Width="100">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Master" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                           window.location='ImportRefEdit.aspx?no='+txtRefNo.GetText()+'&refType='+lab_RefType.GetText();
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Add New" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                           window.location='ImportEdit.aspx?no=0&masterNo='+txtRefNo.GetText()+'&refType='+lab_RefType.GetText();
                        }" />
                                    </dxe:ASPxButton>
                                </td>

                            </tr>
                        </table>
                        <table style="border: solid 1px black; padding: 2px 2px 2px 2px;" width="100%">
                            <tr>
                                <td style="background-color: Gray; color: White;">
                                    <b>Print Documents:</b>
                                </td>
                                <td>
                                    <a href="#" onclick='PrintDO("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>D/O</a>
                                </td>
                                <td>
                                    <a href="#" onclick='PrintPreAdvise("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>PreAdvise</a>
                                </td>
                                <td>
                                    <a href="#" onclick='PrintDN("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>DN</a>
                                </td>
                                <td>
                                    <a href="#" onclick='PrintArrival("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Arrival</a>
                                </td>
                                <td>
                                    <a href="#" onclick='PrintArrival_AG("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Arrival(AG)</a>
                                </td>
                                <td>
                                    <a href="#" onclick='PrintPL("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>P & L</a>
                                </td>

                                <td>
                                    <a href="#" onclick='PrintJobOrder("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Job Order</a>
                                </td>
                                <td>
                                    <a href="#" onclick='PrintImport("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Job Order(IMF)</a>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>Import No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtHouseNo" ClientInstanceName="txtHouseNo" runat="server" ReadOnly="true"
                                        BackColor="Control" Text='<%# Eval("JobNo") %>' Width="150">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Hbl No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtHouseBl" runat="server" Text='<%# Eval("HblNo") %>' Width="150">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Department
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Dept" Width="160" runat="server" Text='<%# Eval("Dept")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                                    <td>
                                                        Ref No
                                                    </td>
                                                    <td>
                                                                    <dxe:ASPxButtonEdit ID="txtRefNo" ClientInstanceName="txtRefNo" runat="server" 
                                                                        Text='<%# Eval("RefNo")%>' Width="155" HorizontalAlign="Left" ReadOnly="true" AutoPostBack="False" BackColor="Control">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                 PopupExpRef(txtRefNo);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                    </td>
                            </tr>
                        </table>
                        <dxtc:ASPxPageControl runat="server" ID="pageControl_Hbl" Width="100%" Height="440px">
                            <TabPages>
                                <dxtc:TabPage Text="Import House" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl1" runat="server">
                                            <div style="display: none">
                                                <dxe:ASPxTextBox ID="txtHouseId" ClientInstanceName="txtHouseId" runat="server" ReadOnly="true"
                                                    BackColor="Control" Text='<%# Eval("SequenceId") %>' Width="100">
                                                </dxe:ASPxTextBox>
                                            </div>
                                            <table>
                                                <tr style="display:none">
                                                </tr>
                                                <tr>
                                                    <td>Customer
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="94">
                                                                    <dxe:ASPxButtonEdit ID="txtCustId" ClientInstanceName="txtCustId" Text='<%# Eval("CustomerId") %>'
                                                                        runat="server" Width="90" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                        PopupCust(txtCustId,txtCust);
                                                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtCust" ReadOnly="false" ClientInstanceName="txtCust" Text='<%# Eval("CustomerName") %>'
                                                                        runat="server" Width="306">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        Express Bl
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="150" ID="cmb_Hbl_ExpressBl"
                                                            runat="server" Text='<%# Eval("ExpressBl") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Forwarding
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="94">
                                                                    <dxe:ASPxButtonEdit ID="txtForwardingId" ClientInstanceName="txtForwardingId" Text='<%# Eval("ForwardingId") %>'
                                                                        runat="server" Width="90" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                        PopupAgent(txtForwardingId,txtForwardingName);
                                                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtForwadingName" ClientInstanceName="txtForwardingName" runat="server"
                                                                        ReadOnly="true" BackColor="Control" Width="306">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        Trucking
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="150" ID="cmb_Hbl_Trucking"
                                                            runat="server" Text='<%# Eval("TruckingInd") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td>Collect From
                                                </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="94">
                                                                    <dxe:ASPxButtonEdit ID="txtCltFrmId" ClientInstanceName="txtCltFrmId"
                                                                        runat="server" Text='<%# Eval("CltFrmId") %>' Width="90" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                        PopupVendor(txtCltFrmId,txtCltFrmName);
                                                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtCltFrmName" ClientInstanceName="txtCltFrmName" runat="server"
                                                                        ReadOnly="true" BackColor="Control" Width="306">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        Do Ready
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txtDoReady" Width="150" runat="server"
                                                            Text='<%# Eval("DoReadyInd") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td>Deliver To
                                                </td>
                                                    <td colspan="3">
                                                        <table cellspacing="0" border="0" cellpadding="0">
                                                            <tr>
                                                                <td width="94">
                                                                    <dxe:ASPxButtonEdit ID="txtDeliveryToId" ClientInstanceName="txtDeliveryToId"
                                                                        runat="server" Text='<%# Eval("DeliveryToId") %>' Width="90" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                        PopupCust(txtDeliveryToId,txtDeliveryToName);
                                                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtDeliveryToName" ClientInstanceName="txtDeliveryToName" runat="server"
                                                                        ReadOnly="true" BackColor="Control" Width="306">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Delivery Date"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="txtDeliveryDate" runat="server" Width="150" Value='<%# Eval("DeliveryDate")%>'
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Do Release To
                                                    </td>
                                                    <td colspan="3">
                                                        <dxe:ASPxTextBox ID="txtRelease" ClientInstanceName="txtRelease" Width="400" runat="server"
                                                            Text='<%# Eval("DoRealeaseTo")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label35" runat="server" Text="FR. Collect"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="cmb_FrCollect" Width="150"
                                                            runat="server" Text='<%# Bind("FrCollectInd") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Currency
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="txtCurrency" ClientInstanceName="txtCurrency" runat="server" MaxLength="3"
                                                            Text='<%# Bind("CollectCurrency")%>' Width="150" HorizontalAlign="Left" AutoPostBack="False">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                    PopupCurrency(txtCurrency,txtAmtExRate);
                                                                    }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td>Ex Rate
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="151" ID="txtAmtExRate"
                                                            ClientInstanceName="txtAmtExRate" runat="server" DisplayFormatString="0.000000" DecimalPlaces="6"
                                                            Value='<%# Bind("CollectExRate")%>' Increment="0">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>Collect Amt
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="150" ID="txtAmt"
                                                            runat="server" DisplayFormatString="0.00" Value='<%# Bind("CollectAmount")%>' Increment="0" DecimalPlaces="2">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Weight
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" ID="spin_Hbl_Wt" Width="150" runat="server"
                                                            ReadOnly="true" BackColor="Control" Value='<%# Eval("Weight") %>' Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        Volume
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" ID="spin_Hbl_M3" Width="151" runat="server"
                                                            ReadOnly="true" BackColor="Control" Value='<%# Eval("Volume") %>' Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>Qty
                                                    </td>
                                                    <td >
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                
                                                                <td>
                                                                    <dxe:ASPxSpinEdit Width="60" ID="spin_Hbl_Pkgs" runat="server" Value='<%# Eval("Qty") %>'
                                                                        ReadOnly="true" BackColor="Control" Increment="0">
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                            
                                                                <td style="padding-left:6px;">
                                                                    <dxe:ASPxTextBox ID="txt_Hbl_PkgType" runat="server" Width="84" Text='<%# Eval("PackageType") %>'
                                                                        ReadOnly="true" BackColor="Control">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Value Currency
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="btn_ValueCurrency" ClientInstanceName="btn_ValueCurrency" runat="server" Width="150" MaxLength="3" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ValueCurrency") %>'>
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupCurrency(btn_ValueCurrency,spin_ValueExRate);
                                                                                                }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td>Value ExRate
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="151" ID="spin_ValueExRate"
                                                            ClientInstanceName="spin_ValueExRate" runat="server" DisplayFormatString="0.000000" DecimalPlaces="6"
                                                            Value='<%# Eval("ValueExRate")%>' Increment="0">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>Value Amt
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="150" ID="spin_ValueAmt"
                                                            runat="server" DisplayFormatString="0.00" Value='<%# Eval("ValueAmt")%>' Increment="0" DecimalPlaces="2">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td rowspan="2">Remark</td>
                                                    <td colspan="3" rowspan="2">
                                                        <dxe:ASPxMemo ID="txt_Remark" runat="server" Width="400" Rows="3" Text='<%# Eval("Remark")%>'>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                    <td>Permit</td>
                                                    <td>
														<dxe:ASPxMemo runat="server" Rows="3" Width="150" ID="txt_Permit" Text='<%# Eval("PermitRmk")%>'>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                                 <tr>
                                                   <td colspan="6">
												   <hr>
                                                        <table >
                                                            <tr>
                                                                <td style="width: 80px;">Creation</td>
                                                                <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                <td style="width: 80px;">Last Updated</td>
                                                                <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                <td style="width: 80px;">Job Status</td>
                                                                <td style="width: 160px"><dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text="" /></td>
                                                            </tr>
                                                        </table>
												   <hr>
                                                    </td>
                                                </tr>

                                            </table>

                                            <table cellspacing=2 cellpadding=2 border=1>
                                            <tr>
                                            <td>
                                            CFS Forklift
					    </td>
                                            <td>
						<dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="60" ID="txtRateForklift"
                                                           ClientInstanceName="txtRateForklift" runat="server" DisplayFormatString="0.00"
                                                          Value='<%# Bind("RateForklift")%>' Increment="0">
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                            DO Processing 
					    </td>
                                            <td>
						<dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="50" ID="txtRateProcess"
                                                           ClientInstanceName="txtRateProcess" runat="server" DisplayFormatString="0.00"
                                                          Value='<%# Bind("RateProcess")%>' Increment="0">
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                            Tracing
					    </td>
                                            <td>
						<dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="50" ID="txtRateTracing"
                                                           ClientInstanceName="txtRateTracing" runat="server" DisplayFormatString="0.00"
                                                          Value='<%# Bind("RateTracing")%>' Increment="0">
                                                </dxe:ASPxSpinEdit>
                                            </td>
      
                                            <td>
                                           Surcharge
					    </td>
                                            <td>
						<dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="50" ID="txtRateWarehouse"
                                                           ClientInstanceName="txtRateWarehouse" runat="server" DisplayFormatString="0.00"
                                                          Value='<%# Bind("RateWarehouse")%>' Increment="0">
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                           Admin Fee
					    </td>
                                            <td>
						<dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="50" ID="txtRateAdmin"
                                                           ClientInstanceName="txtRateAdmin" runat="server" DisplayFormatString="0.00"
                                                          Value='<%# Bind("RateAdmin")%>' Increment="0">
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                            Nomination
					    </td>
                                            <td>

					<dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txtFlagNomination" Width="60" runat="server"
                                                            Text='<%# Eval("FlagNomination") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>

                                            </td>
                                            
                                            </tr>

                                            </table>

                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Tranship" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl3" runat="server">
                                            <table border="0">
                                                <tr>
                                                    <td>Tranship
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ClientInstanceName="cmb_Hbl_Tranship"
                                                            ID="cmb_Hbl_Tranship" Width="72" runat="server" Text='<%# Eval("TsInd") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>POD
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="txtPod" ClientInstanceName="txtPod"
                                                            Text='<%# Eval("TsPod")%>' runat="server" Width="188" HorizontalAlign="Left" AutoPostBack="False" MaxLength="5">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txtPod,null);
                                                                    }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td>Final Dest
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="txtFinalDest" ClientInstanceName="txtFinalDest" Text='<%# Eval("TsPortFinName") %>'
                                                            runat="server" Width="140" HorizontalAlign="Left" AutoPostBack="False">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(null,txtFinalDest);
                                                                                            }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Agent
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="76">
                                                                    <dxe:ASPxButtonEdit ID="txt_TAgentId" ClientInstanceName="txtTAgentId" Text='<%# Bind("TsAgentId")%>'
                                                                        runat="server" Width="72" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {PopupAgent(txtTAgentId,txtATgent);}" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="363" ReadOnly="true" BackColor="Control" ID="txt_TAgentName"
                                                                        ClientInstanceName="txtATgent" Text=''>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_BkgConnect" Text="Booking" Width="85" runat="server" AutoPostBack="false"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) {
                                                            if( parseInt(txtBkgId.GetText()) >0)
                                                            alert('Have booking');
                                                            else
                                                                PopupTSList();
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_BkgShow" Text="Cancel Booking" Width="140" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            UseSubmitBehavior="false" runat="server">
                                                            <ClientSideEvents Click="function(s,e){
                                                             if( parseInt(txtBkgId.GetText()) >0)
                                                                                    CancelBkg();
                                                                                    }
                                                                                    " />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Booking No
                                                    </td>
                                                    <td>
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txtSchNo" ReadOnly="true" ClientInstanceName="txtSchNo" BackColor="Control"
                                                                runat="server" Text='<%# Eval("TsSchNo") %>' Width="170">
                                                            </dxe:ASPxTextBox>
                                                        <dxe:ASPxTextBox ID="txtBkgId" ReadOnly="true" ClientInstanceName="txtBkgId" BackColor="Control"
                                                            runat="server" Text='<%# Eval("TsBkgId") %>' Width="170">
                                                        </dxe:ASPxTextBox>
                                                        </div>
                                                        <dxe:ASPxTextBox ID="txtBkgNo" ReadOnly="true" ClientInstanceName="txtBkgNo" BackColor="Control"
                                                            runat="server" Text='<%# Eval("TsBkgNo") %>' Width="188">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        Export No
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtExpRefNo" ReadOnly="true" ClientInstanceName="txtExpRefNo"
                                                            BackColor="Control" runat="server" Text='<%# Eval("TsJobNo") %>' Width="188">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        Export Ref No
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtExpNo" ReadOnly="true" ClientInstanceName="txtExpNo" BackColor="Control"
                                                            runat="server" Text='<%# Eval("TsRefNo") %>' Width="140">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Vesel
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="188" ID="txt_TVes" ClientInstanceName="txtTves"
                                                            Text='<%# Bind("TsVessel")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        Voyage
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="188" ID="txt_TVoy" ClientInstanceName="txtTvoy"
                                                            Text='<%# Bind("TsVoyage")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        Etd
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="date_EtdSin" Width="140" runat="server" ClientInstanceName="txtTetd"
                                                            Value='<%# Bind("TsEtd")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                            DisplayFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Coloader
                                                    </td>
                                                    <td colspan="3">
                                                        <dxe:ASPxTextBox runat="server" Width="440" ID="txt_Coloader" Text='<%# Eval("TsColoader")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        Eta Dest
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="date_EtaDest" Width="140" runat="server" ClientInstanceName="txtTeta"
                                                            Value='<%# Bind("TsEta")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                            DisplayFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Remark
                                                    </td>
                                                    <td colspan="3">
                                                        <dxe:ASPxTextBox runat="server" Width="440" ID="txt_TRemark" Text='<%# Bind("TsRemark")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Ag Rates
                                                    </td>
                                                    <td colspan="5">
                                                        <table cellspacing=0 >
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100"
                                                                        ID="spin_AgtRate" Text='<%# Bind("TsAgtRate")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>
                                                                    Tot Ag. Rate
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" ReadOnly="true" BackColor="Control" Width="100" ID="spin_TotAgtRate"
                                                                        Text='<%# Eval("TsTotAgtRate")%>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    Imp Rates
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="104"
                                                                        ID="spin_ImpRate" Text='<%# Bind("TsImpRate")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td Width="90">
                                                                    Tot. Im Rate
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" ReadOnly="true" BackColor="Control" Width="140" ID="spin_TotImpRate"
                                                                        Text='<%# Eval("TsTotImpRate")%>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="DG Cargo" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl4" runat="server">
                                            <table>
                                                <tr>
                                                    <td width="120">IMDG Class</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgImdgClass" runat="server" Width="160" Text='<%# Eval("DgImdgClass") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">UN Number</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgUnNo" runat="server" Text='<%# Eval("DgUnNo") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td rowspan="10" style="padding-left: 20px;" valign="top">
                                                        <a href="/__dg/dg_form.pdf" target="_blank">DG Cargo Form</a><br />
                                                        <a href="/__dg/dg_requirement.pdf" target="_blank">DG Cargo Requirement</a><br />
                                                        <br />
                                                        <a href="/__dg/dg_imdg_code.pdf" target="_blank">IMDG Code</a><br />
                                                        <a href="/__dg/un_packaging.pdf" target="_blank">UN Packaging Code</a><br />
                                                        <a href="/__dg/dg_class_coload.png" target="_blank">DG Cargo Coload Compliance</a><br />
                                                        <a href="/__dg/dg_chart.pdf" target="_blank">DG Chart Information</a><br />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Proper Shipping Name</td>
                                                    <td colspan="3">
                                                        <dxe:ASPxMemo ID="memo_DgShippingName" runat="server" Text='<%# Eval("DgShippingName") %>' Width="450" Rows="4">
                                                        </dxe:ASPxMemo>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td width="120">Technical Name</td>
                                                    <td colspan="3">
                                                        <dxe:ASPxMemo ID="memo_DgTecnicalName" runat="server" Text='<%# Eval("DgTecnicalName") %>' Width="450" Rows="4">
                                                        </dxe:ASPxMemo>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td width="120">MFAG Number 1</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgMfagNo1" runat="server" Text='<%# Eval("DgMfagNo1") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">MFAG Number 2</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgMfagNo2"  runat="server" Text='<%# Eval("DgMfagNo2") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">EMS (fire)</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgEmsFire" runat="server" Text='<%# Eval("DgEmsFire") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">EMS (spill)</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgEmsSpill" runat="server" Text='<%# Eval("DgEmsSpill") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120"></td>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="ack_DgLimitedQtyInd" runat="server" Text='Limited Quantity' Width="160">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td width="120"></td>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="ack_DgExemptedQtyInd" runat="server" Text='Exempted Quantity' Width="160">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Net Weight</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgNetWeight" runat="server" Text='<%# Eval("DgNetWeight") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">Flashpoint</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgFlashPoint" runat="server" Text='<%# Eval("DgFlashPoint") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Radioactivity</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgRadio" runat="server" Text='<%# Eval("DgRadio") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">Page Number</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgPageNo" runat="server" Text='<%# Eval("DgPageNo") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Packing Group</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgPackingGroup" runat="server" Text='<%# Eval("DgPackingGroup") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">Packing Type Code</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgPackingTypeCode" runat="server" Text='<%# Eval("DgPackingTypeCode") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Transport Code</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgTransportCode" runat="server" Text='<%# Eval("DgTransportCode") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">Category</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgCategory" runat="server" Text='<%# Eval("DgCategory") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="POD Info" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_pod" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>PODBy</td>
                                                        <td><dxe:ASPxButtonEdit ID="txtPODBy" ClientInstanceName="txtPODBy" Text='<%# Eval("PODBy") %>'
                                                                        runat="server" Width="250" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                        PopupCust(null,txtPODBy);
                                                                                                            }" />
                                                                    </dxe:ASPxButtonEdit></td>
                                                        <td>PODTime</td>
                                                        <td> <dxe:ASPxDateEdit ID="date_PodTime" Width="100" runat="server" Value='<%# Eval("PODTime") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo Rows="3" ID="me_Remark" Width="440" runat="server" Text='<%# Eval("PODRemark") %>'>
                                                            </dxe:ASPxMemo></td>
                                                    </tr>
                                                     <tr>
                                                        <td>Update User</td>
                                                        <td><%# Eval("PODUpdateUser") %></td>
                                                        <td>Update Time</td>
                                                        <td><%# SafeValue.SafeDateStr(Eval("PODUpdateTime")) %></td>
                                                    </tr>
                                                    
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Transport" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_tpt" runat="server">
                                                <table>
                                                    <tr>
                                                        <td colspan="6" style="background-color: Gray; color: White;">
                                                            <b>Transportatin Info</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_Ref_H_Haulier" runat="server" HorizontalAlign="Left" Width="100" Text="Haulier" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupHaulier(txt_Ref_H_Haulier,txt_Ref_H_CrNo,txt_Ref_H_Fax,txt_Ref_H_Attention);
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_Hauler" Width="475" ClientInstanceName="txt_Ref_H_Haulier"
                                                                runat="server" Text='<%# Eval("HaulierName") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>UEN No
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_CrNo" Width="170" ClientInstanceName="txt_Ref_H_CrNo"
                                                                runat="server" Text='<%# Eval("HaulierCrNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="110">Attention
                                                        </td>
                                                        <td width="190">
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_Attention" Width="170" ClientInstanceName="txt_Ref_H_Attention"
                                                                runat="server" Text='<%# Eval("HaulierAttention") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="110">Fax
                                                        </td>
                                                        <td width="190">
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_Fax" Width="170" ClientInstanceName="txt_Ref_H_Fax"
                                                                runat="server" Text='<%# Eval("HaulierFax") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="110">
                                                        </td>
                                                        <td width="190">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Driver Name</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_DriverName" Width="170" ClientInstanceName="txt_DriverName"
                                                                runat="server" Text='<%# Eval("DriverName") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Driver Mobile</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_DriverMobile" Width="170" ClientInstanceName="txt_DriverMobile"
                                                                runat="server" Text='<%# Eval("DriverMobile") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Driver License</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_DriverLicense" Width="170" ClientInstanceName="txt_DriverLicense"
                                                                runat="server" Text='<%# Eval("DriverLicense") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                         <td>VehicleNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_VehicleNo" Width="170" ClientInstanceName="txt_VehicleNo"
                                                                runat="server" Text='<%# Eval("VehicleNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Vehicle Type</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_VehicleType" Width="170" ClientInstanceName="txt_VehicleType"
                                                                runat="server" Text='<%# Eval("VehicleType") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Driver Remark</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="me_DriverRemark" Width="475" ClientInstanceName="me_DriverRemark"
                                                                runat="server" Text='<%# Eval("DriverRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="2">
                                                            <dxe:ASPxButton ID="ASPxButton11" runat="server" HorizontalAlign="Left" Width="100" Text="Truck To" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupPartyAdr(null,txt_Ref_H_CltFrm,'CV');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td rowspan="2" colspan="3">
                                                            <dxe:ASPxMemo ID="txt_Ref_H_CltFrm" Rows="4" Width="475" ClientInstanceName="txt_Ref_H_CltFrm"
                                                                runat="server" Text='<%# Eval("HaulierCollect") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Date</td>
                                                        <td>
                                                             <dxe:ASPxDateEdit ID="date_Ref_H_CltDate" Width="170" runat="server" Value='<%# Eval("HaulierCollectDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Time</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="date_Ref_H_CltTime" Width="170" runat="server" Text='<%# Eval("HaulierCollectTime") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="2">
                                                            <dxe:ASPxButton ID="ASPxButton12" runat="server" HorizontalAlign="Left" Width="100" Text="Return To" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupPartyAdr(null,txt_Ref_H_TruckTo,'CV');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td rowspan="2" colspan="3">
                                                            <dxe:ASPxMemo ID="txt_Ref_H_TruckTo" Rows="4" Width="475" ClientInstanceName="txt_Ref_H_TruckTo"
                                                                runat="server" Text='<%# Eval("HaulierTruck") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Date</td>
                                                        <td>
                                                             <dxe:ASPxDateEdit ID="date_Ref_H_DlvDate" Width="170" runat="server" Value='<%# Eval("HaulierDeliveryDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Time</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="date_Ref_H_DlvTime" Width="170" runat="server" Text='<%# Eval("HaulierDeliveryTime") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>SENT TO</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_SendTo" Width="170" ClientInstanceName="txt_DriverName"
                                                                runat="server" Text='<%# Eval("HaulierSendTo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Stuff/Unstuff By</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_StuffBy" Width="170" ClientInstanceName="txt_DriverMobile"
                                                                runat="server" Text='<%# Eval("HaulierStuffBy") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Shipping coload</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_Coload" Width="170" ClientInstanceName="txt_DriverLicense"
                                                                runat="server" Text='<%# Eval("HaulierCoload") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Person</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_Person" Width="170" ClientInstanceName="txt_DriverName"
                                                                runat="server" Text='<%# Eval("HaulierPerson") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Telephone</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_PersonTel" Width="170" ClientInstanceName="txt_DriverMobile"
                                                                runat="server" Text='<%# Eval("HaulierPersonTel") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remarks
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo Rows="3" ID="txt_Ref_H_Rmk1" Width="475" row="3" runat="server" Text='<%# Eval("HaulierRemark") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Shipper Info" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl15" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td width="100">
                                                                    <dxe:ASPxButton ID="btn_Shipper_Pick2" runat="server" Width="85" HorizontalAlign="Left" Text="Shipper" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                                PopupCustAdr(null,txt_Hbl_Shipper2);
                                                                                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Hbl_Shipper2" Rows="5" ClientInstanceName="txt_Hbl_Shipper2"
                                                                        runat="server" Width="300" Text='<%# Eval("SShipperRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td width="100">
                                                                    <dxe:ASPxButton ID="btn_Consignee_Pick2" runat="server" Width="85" HorizontalAlign="Left" Text="Consignee" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                                PopupAgentAdr(null,txt_Hbl_Consigee2);
                                                                                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Hbl_Consigee2" Rows="5" ClientInstanceName="txt_Hbl_Consigee2"
                                                                        runat="server" Width="300" Text='<%# Eval("SConsigneeRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="border-bottom: solid 1px black;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td width="100">
                                                                    <dxe:ASPxButton ID="btn_Party_Pick2" runat="server" Width="85" HorizontalAlign="Left" Text="Notify Party" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                                PopupAgentAdr(null,txt_Hbl_Party2);
                                                                                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Hbl_Party2" Rows="5" ClientInstanceName="txt_Hbl_Party2" runat="server"
                                                                        Width="300" Text='<%# Eval("SNotifyPartyRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td width="100">
                                                                    <dxe:ASPxButton ID="btn_Agent_Pick2" runat="server" Width="85" HorizontalAlign="Left" Text="Agent" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                                PopupAgentAdr(null,txt_Hbl_Agt2);
                                                                                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Hbl_Agt2" Rows="5" ClientInstanceName="txt_Hbl_Agt2" runat="server"
                                                                        Width="300" Text='<%# Eval("SAgentRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Marking" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl5" runat="server">
                                            <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Add Marking" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"  %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Mkgs.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>
                                            <dxwgv:ASPxGridView ID="grid_Mkgs" ClientInstanceName="grid_Mkgs" runat="server"
                                                DataSourceID="dsMarking" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_Mkgs_DataSelect"
                                                OnRowUpdating="grid_Mkgs_RowUpdating" OnInit="grid_Mkgs_Init" OnInitNewRow="grid_Mkgs_InitNewRow"
                                                OnRowInserting="grid_Mkgs_RowInserting" OnRowDeleting="grid_Mkgs_RowDeleting"
                                                OnRowInserted="grid_Mkgs_RowInserted" OnRowUpdated="grid_Mkgs_RowUpdated" OnRowDeleted="grid_Mkgs_RowDeleted"
                                                OnHtmlEditFormCreated="grid_Mkgs_HtmlEditFormCreated">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <SettingsEditing Mode="EditForm" />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" 
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_Mkgs.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%#SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Mkgs.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                                                    <dxe:ASPxDropDownEdit ID="DropDownEdit" runat="server" ClientInstanceName="DropDownEdit"
                                                                        Text='<%# Bind("ContainerNo") %>' Width="120px" AllowUserInput="True">
                                                                        <DropDownWindowTemplate>
                                                                            <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                                                                                Width="300px" DataSourceID="dsRefCont" KeyFieldName="SequenceId" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                                                <Columns>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="SequenceId" VisibleIndex="0" Visible="false">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="0">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="SealNo" VisibleIndex="1">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="1">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                </Columns>
                                                                                <ClientSideEvents RowClick="RowClickHandler" />
                                                                            </dxwgv:ASPxGridView>
                                                                        </DropDownWindowTemplate>
                                                                    </dxe:ASPxDropDownEdit>
                                                                </td>
                                                                <td>
                                                                    Seal No
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="120" ID="txt_sealN"
                                                                        Text='<%# Bind("SealNo") %>' ClientInstanceName="txt_sealN">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td width="60">Cont Type
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
                                                                    <dxe:ASPxMemo runat="server" Rows="6" Width="290" ID="txt_mkg1" MaxLength="500" Text='<%# Bind("Marking")%>'>
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
                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"))=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>' >
                                                                    <ClientSideEvents Click="function(s,e){grid_Mkgs.UpdateEdit();}" />
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
                                <dxtc:TabPage Text="Delivery Note" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl2" runat="server">
                                            <table><tr><td>
                                            <dxe:ASPxButton ID="btn_AddDn" Width="150" runat="server" Text="Add New" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"  %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Dn.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>
                                                </td>
                                                <td>
                                            <dxe:ASPxButton ID="ASPxButton13" Width="150" runat="server" Text="Auto DN" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"  %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Dn.GetValuesOnCustomCallback('AutoDn',onDnCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                                </td>
                                                   </tr></table>
                                            <dxwgv:ASPxGridView ID="grid_Dn" ClientInstanceName="grid_Dn" runat="server"
                                                DataSourceID="dsDn" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_Dn_DataSelect"
                                                OnRowUpdating="grid_Dn_RowUpdating" OnInit="grid_Dn_Init" OnInitNewRow="grid_Dn_InitNewRow" OnCustomDataCallback="grid_Dn_CustomDataCallback"
                                                OnRowInserting="grid_Dn_RowInserting" OnRowDeleting="grid_Dn_RowDeleting">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <SettingsEditing Mode="EditForm" />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" 
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_Dn.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%#SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Dn.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <button type="button" onclick='PrintDN1("<%#Eval("RefNo") %>","<%#Eval("JobNo") %>","<%#Eval("SequenceId") %>");'>Print</button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="PACK TYPE" FieldName="PackageType" VisibleIndex="7">
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
                                                                <td width="43">
                                                                    Weight
                                                                </td>
                                                                <td width="125">
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                        runat="server" Width="120" ID="spin_wt" Value='<%# Bind("Weight")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td width="43">
                                                                    Volume
                                                                </td>
                                                                <td width="145">
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                        runat="server" Width="120" ID="spin_m3" Value='<%# Bind("Volume")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td width="35">Qty
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
                                                                        runat="server" Width="115" HorizontalAlign="Left" AutoPostBack="False">
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
                                                                
                                                                <td>
                                                                 <a href="#" onclick="PopupHaulierAdr(txt_DnAdd,'')" >Address</a>                                           
                                                                </td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxMemo runat="server" Rows="6" Width="664" ID="txt_DnAdd" ClientInstanceName="txt_DnAdd" MaxLength="500" Text='<%# Bind("Address")%>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Marking
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo runat="server" Rows="6" Width="294" ID="txt_mkg1" MaxLength="500" Text='<%# Bind("Marking")%>'>
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
                                                                 <a href="#" onclick='PrintDN1("<%#Eval("RefNo") %>","<%#Eval("JobNo") %>","<%#Eval("SequenceId") %>");'>Print DN</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>' >
                                                                    <ClientSideEvents Click="function(s,e){grid_Dn.UpdateEdit();}" />
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
                                <dxtc:TabPage Text="Billing" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl_Inv" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice_Import, "SI", txtRefNo.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import, "SI", txtRefNo.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import, "SI", txtRefNo.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="Grid_Invoice_Import" ClientInstanceName="Grid_Invoice_Import"
                                                runat="server" KeyFieldName="InvoiceN" DataSourceID="dsInvoice" Width="100%"
                                                OnBeforePerformDataSelect="Grid_Invoice_Import_DataSelect">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="70">
                                                        <DataItemTemplate>
                                                            <a onclick='ShowInvoice(Grid_Invoice_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>
                                                                Edit</a>&nbsp; <a onclick='PrintInvoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>
                                                                    Print</a>
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
                                                        <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import, "SI", txtRefNo.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton9" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import, "SI", txtRefNo.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="Grid_Payable_Import" ClientInstanceName="Grid_Payable_Import"
                                                runat="server" KeyFieldName="SequenceId" DataSourceID="dsVoucher" Width="100%"
                                                OnBeforePerformDataSelect="Grid_Payable_Import_DataSelect">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="70">
                                                        <DataItemTemplate>
                                                            <a onclick='ShowPayable(Grid_Payable_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintPayable("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
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
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Certificate">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="btn_AddCertificate" Width="150" runat="server" Text="Add Certificate" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        AddImportCertificate(gird_Certificate)
                                                        }" />
                                            </dxe:ASPxButton>
                                            <dxwgv:ASPxGridView ID="gird_Certificate" runat="server" ClientInstanceName="gird_Certificate"
                                                OnInit="gird_Certificate_Init" OnInitNewRow="gird_Certificate_InitNewRow" OnRowInserting="gird_Certificate_RowInserting"
                                                OnRowUpdating="gird_Certificate_RowUpdating" Width="100%" OnRowDeleting="gird_Certificate_RowDeleting" KeyFieldName="Id" DataSourceID="dsCertificate" OnBeforePerformDataSelect="gird_Certificate_DataSelect">

                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                        <DataItemTemplate>
                                                            <a onclick='ShowImportCertificate(gird_Certificate,"<%# Eval("Id") %>");'>Edit</a>&nbsp; 
                                                            <a onclick='PrintCertificate("<%# Eval("Id") %>")'>Print</a>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="GstPermitNo" FieldName="GstPermitNo" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="GstPaidAmt" FieldName="GstPaidAmt" VisibleIndex="3">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="HandingAgent" FieldName="PartyName" VisibleIndex="4">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Certificate Date" FieldName="CerDate" VisibleIndex="5">
                                                         <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <table>
                                                            <tr>
                                                                <td>Description</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="memo_Description" runat="server" Text='<%# Bind("Description")%>' Width="300"></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>Qty</td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100"
                                                                        ID="spin_Qty" Text='<%# Bind("Qty")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>PackageType</td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_pkgType" ClientInstanceName="txt_pkgType" runat="server"
                                                                        Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType,2);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>Value</td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100"
                                                                        ID="spin_Amt" Text='<%# Bind("Amt")%>' Increment="0" DecimalPlaces="2">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="12">
                                                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                        <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                                                            <ClientSideEvents Click="function(s,e){gird_Certificate.UpdateEdit();}" />
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
                                <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl8" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Upload Attachments"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false"
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
                                            <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto"
                                                KeyFieldName="SequenceId" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                 AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                <Settings  />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'
                                                                ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'
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
                                                                <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("SequenceId") %>'></dxe:ASPxTextBox>
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
                            </TabPages>
                            <ClientSideEvents ActiveTabChanged="function(s, e) { 
                                var tit=s.GetActiveTab().GetText();
                                if(tit=='Attachments1')
                                    detailGrid.PerformCallback('Photo');
                                    }" />
                        </dxtc:ASPxPageControl>
                        <hr />
                        Full House Job
                        
                        <dxwgv:ASPxGridView ID="grid_Export1" runat="server"
                            KeyFieldName="SequenceId" Width="900px" AutoGenerateColumns="False" DataSourceID="dsFullJob"
                             OnBeforePerformDataSelect="grid_Export1_BeforePerformDataSelect">
                            <SettingsCustomizationWindow Enabled="True" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                    <DataItemTemplate>
                                        <a onclick="ShowHouse('<%# Eval("RefNo") %>','<%# Eval("JobNo") %>');">Edit</a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Import No" FieldName="JobNo" VisibleIndex="1" Width="150"
                                    SortIndex="1" SortOrder="Ascending">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Hbl No" FieldName="HblNo" VisibleIndex="3" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4" Width="50">
                                    <PropertiesTextEdit DisplayFormatString="#,##0.000">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5" UnboundType="Decimal" Width="50">
                                    <PropertiesTextEdit DisplayFormatString="#,##0.000">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6" Width="50">
                                    <PropertiesTextEdit DisplayFormatString="f0">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="T/S Ind" FieldName="TsInd" VisibleIndex="7" Width="50">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Exp BkgNo" FieldName="TsBkgNo" VisibleIndex="7" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Create By" FieldName="CreateBy" VisibleIndex="9" Width="50">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </div>
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
