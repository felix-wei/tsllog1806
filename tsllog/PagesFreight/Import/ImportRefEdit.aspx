<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ImportRefEdit.aspx.cs" Inherits="Pages_ImportRefEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import Shipment</title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Import_Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/ImportRefEdit.js"></script>
    <script type="text/javascript" src="/Script/Edi.js"></script>
    <script type="text/javascript">
        var isUpload = false;
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsImportRef" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaImportRef" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsImpRefCont" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaImportMkg" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaAttachment" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsImport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaImport" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaCosting" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Id" FilterExpression="CodeType='1'" />
            <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXSalesman" KeyMember="Code" FilterExpression="" />
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lab_RefNo" runat="server" Text="Ref No">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="150" ClientInstanceName="txt_SchRefNo" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="display: None">
                        <dxe:ASPxTextBox runat="server" Width="170" ID="txt_RefType" ClientInstanceName="txt_RefType">
                        </dxe:ASPxTextBox>
                        <dxe:ASPxTextBox runat="server" Width="170" ID="txt_MasterNo" ClientInstanceName="txt_MasterNo">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='ImportRefEdit.aspx?no='+txt_SchRefNo.GetText()+'&refType='+txt_RefType.GetText();;
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton9" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='ImportRefEdit.aspx?no=0&refType='+txt_RefType.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='ImportRefList.aspx?refType='+txt_RefType.GetText();
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_ref" ClientInstanceName="grid_ref" runat="server" KeyFieldName="SequenceId"
                Width="900px" AutoGenerateColumns="False" DataSourceID="dsImportRef" OnInitNewRow="grid_ref_InitNewRow"
                OnInit="grid_ref_Init" OnHtmlEditFormCreated="grid_ref_HtmlEditFormCreated" OnCustomCallback="grid_ref_CustomCallback"
                OnCustomDataCallback="grid_ref_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <Templates>
                    <EditForm>
                        <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                            <tr>
                                <td width="75%"></td>
                                <td>
                                    <dxe:ASPxButton ID="btn_EDI" runat="server" Width="100" AutoPostBack="false"
                                        Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' UseSubmitBehavior="false"
                                        Text="EDI">
                                        <ClientSideEvents Click="function(s,e){
                                             EdiSeaRef(txt_RefN.GetText(),'SI');                                                     
                                                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="100" AutoPostBack="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' UseSubmitBehavior="false"
                                        Text="Save">
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid_ref.PerformCallback('Save');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_CloseJob" Width="90" runat="server" Text="Close Job" AutoPostBack="False"
                                        UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                                                grid_ref.GetValuesOnCustomCallback('CloseJob',OnCloseCallBack);
                                                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_VoidMaster" ClientInstanceName="btn_VoidMaster" runat="server" Width="100" Text="Void" AutoPostBack="False"
                                        UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_VoidMaster.GetText()+' Master?'))
                                                                    {
                                                                        grid_ref.GetValuesOnCustomCallback('VoidMaster',OnCloseCallBack);                 
                                                                    }
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
                                    <a href="#" onclick='PrintManifest("<%# Eval("RefNo")%>",0)'>Manifest</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintHaulier("<%# Eval("RefNo")%>",0)'>Print Haulier</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintDOPermit_Carrier("<%# Eval("RefNo")%>",0)'>DO w/o Permit(Carrier)</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintLetter_Carrier("<%# Eval("RefNo")%>",0)'>Letter of Auth(Carrier)</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintPermitList_Carrier("<%# Eval("RefNo")%>",0)'>Permit List(Carrier)</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintBatch_DO("<%# Eval("RefNo")%>",0)'>Batch Print D/O</a>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="#" onclick='PrintTransshipBkg("<%# Eval("RefNo")%>",0)'>Transship Bkg</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintPL("<%# Eval("RefNo")%>",0)'>P & L</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintDOPermit_Nvocc("<%# Eval("RefNo")%>",0)'>DO w/o Permit(Nvocc)</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintLetter_Nvocc("<%# Eval("RefNo")%>",0)'>Letter of Auth(Nvocc)</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintPermitList_Nvocc("<%# Eval("RefNo")%>",0)'>Permit List(Nvocc)</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintBatch_Invoice("<%# Eval("RefNo")%>",0)'>Batch Print Invoice</a>&nbsp;
                                </td>
                                <td>
                                    <a href="#" onclick='PrintCoverPage("<%# Eval("RefNo")%>",0)'>Cover Page</a>&nbsp;
                                </td>
                            </tr>
                        </table>
                        <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%" Height="500px">
                            <TabPages>
                                <dxtc:TabPage Text="Import Ref" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl1" runat="server">
                                            <div style="display: none">
                                                <dxe:ASPxTextBox runat="server" Width="60" ID="txt_SequenceId" ClientInstanceName="txt_SequenceId"
                                                    Text='<%# Eval("SequenceId") %>'>
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
                                                    <td width="100">Ref No
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="170" ID="txt_RefN" ClientInstanceName="txt_RefN"
                                                            BackColor="Control" ReadOnly="true" Text='<%# Eval("RefNo")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">JobType
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="cbx_JobType"
                                                            BackColor="Control" ReadOnly="true" Text='<%# Eval("JobType")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Ref Date
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="date_RefDate" Width="165" runat="server" Value='<%# Eval("RefDate")%>'
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Agent
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="109">
                                                                    <dxe:ASPxButtonEdit ID="txt_AgentId" ClientInstanceName="txt_AgentId" runat="server" Width="105" HorizontalAlign="Left"
                                                                        Text='<%# Eval("AgentId")%>' AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupAgent(txt_AgentId,txt_AgentName);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="355" BackColor="Control" ID="txt_AgentName"
                                                                        ReadOnly="true" ClientInstanceName="txt_AgentName">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Eta
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="date_Eta" Width="165" runat="server" Value='<%# Eval("Eta")%>'
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Vessel
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="170" ID="txt_Ves" Text='<%# Eval("Vessel")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Voyage
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="txt_Voy" Text='<%# Eval("Voyage")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Etd
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="date_Etd" Width="165" runat="server" Value='<%# Eval("Etd")%>'
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Pol
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Width="170" HorizontalAlign="Left" AutoPostBack="False"
                                                            MaxLength="5" Text='<%# Eval("Pol")%>'>
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
                                                        <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Width="165" HorizontalAlign="Left" AutoPostBack="False"
                                                            MaxLength="5" Text='<%# Eval("Pod")%>'>
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pod);
                                                                    }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td>Ocean BL
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="txt_OceanBl" Text='<%# Eval("OblNo")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Carrier Agent
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="109">
                                                                    <dxe:ASPxButtonEdit ID="txt_CrAgentId" ClientInstanceName="txt_CrAgentId" runat="server" Width="105"
                                                                        Text='<%# Eval("CrAgentId")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupVendor(txt_CrAgentId,txt_CrAgentName);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" BackColor="Control" Width="355" ID="txt_CrAgentName"
                                                                        ClientInstanceName="txt_CrAgentName" ReadOnly="true" Text=''>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Carrier Bkg No
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="txt_CrBkgRefN" Text='<%# Eval("CrBkgNo")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>NVOCC Agent
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="109">
                                                                    <dxe:ASPxButtonEdit ID="txt_NvoccAgentId" ClientInstanceName="txt_NvoccAgentId" runat="server" Width="105"
                                                                        Text='<%# Eval("NvoccAgentId")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupVendor(txt_NvoccAgentId,txt_NvoccAgentName);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" BackColor="Control" Width="355" ID="txt_NvoccAgentName"
                                                                        ClientInstanceName="txt_NvoccAgentName" ReadOnly="true" Text="">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>NVOCC BL
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="txt_NvoccBl" Text='<%# Eval("NvoccBl")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Warehouse
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="109">
                                                                    <dxe:ASPxButtonEdit ID="txt_WhId" ClientInstanceName="txt_WhId" runat="server" Width="105"
                                                                        Text='<%# Eval("WarehouseId")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                         PopupVendor(txt_WhId,txt_WhName);
                                                                         }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="355" BackColor="Control" ID="txt_WhName" ClientInstanceName="txt_WhName"
                                                                        ReadOnly="true" Text=''>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Weight
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165" BackColor="Control"
                                                            ReadOnly="true" ID="spin_Wt" Height="21px" Value='<%# Eval("Weight")%>' DecimalPlaces="3">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Forward Agent
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="109">
                                                                    <dxe:ASPxButtonEdit ID="txt_ForwardAgentId" ClientInstanceName="txt_ForwardAgentId" runat="server" Width="105"
                                                                        Text='<%# Eval("ForwardAgentId")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                    PopupAgent(txt_ForwardAgentId,txt_ForwardAgentName);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" BackColor="Control" Width="355" ID="txt_ForwardAgentName"
                                                                        ClientInstanceName="txt_ForwardAgentName" ReadOnly="true" Text=''>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Volume
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165" BackColor="Control"
                                                            ReadOnly="true" ID="spin_M3" Height="21px" Value='<%# Eval("Volume")%>' DecimalPlaces="3">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Transport
                                                    </td>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="109">
                                                                    <dxe:ASPxButtonEdit ID="txt_TptId" ClientInstanceName="txt_TptId" runat="server" Width="105"
                                                                        Text='<%# Eval("TransportId")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupVendor(txt_TptId,txt_TptName);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" BackColor="Control" Width="355" ID="txt_TptName"
                                                                        ClientInstanceName="txt_TptName" ReadOnly="true" Text="">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Qty

                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit runat="server" Width="165" BackColor="Control" ReadOnly="true"
                                                            ID="spin_Pkgs" Height="21px" Value='<%# Eval("Qty")%>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr style="display: none;">
                                                    <td>LocalCust
                                                    </td>
                                                    <td colspan="5">
                                                        <table>
                                                            <tr>
                                                                <td>

                                                                    <dxe:ASPxButtonEdit ID="txt_LocalCust" ClientInstanceName="txt_LocalCust"
                                                                        runat="server" Width="105"
                                                                        Text='<%# Eval("LocalCust")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCust(txt_LocalCust,txt_LocalCustName);
                                                                    }" />

                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" BackColor="Control" Width="350"
                                                                        ID="txt_LocalCustName"
                                                                        ClientInstanceName="txt_LocalCustName" ReadOnly="true" Text="">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>Ref Currency
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="cbx_Currency" ClientInstanceName="cbx_Currency" runat="server" Width="165" MaxLength="3"
                                                            Text='<%# Eval("CurrencyId") %>' HorizontalAlign="Left" AutoPostBack="False">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(cbx_Currency,spin_CrExRate);
                                                                    }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td>Ex Rate
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="spin_CrExRate" ClientInstanceName="spin_CrExRate" DisplayFormatString="0.000000"
                                                            runat="server" Width="165" Value='<%# Eval("ExRate")%>' DecimalPlaces="6" Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>Pkgs Type
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" BackColor="Control" ReadOnly="true" Width="165" ID="txt_PkgsType"
                                                            Text='<%# Eval("PackageType")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>Est Rev Amount
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="spin_EstSaleAmt" DisplayFormatString="0.00" ReadOnly="True" BackColor="Control"
                                                            runat="server" Width="165" Value='<%# Eval("EstSaleAmt")%>' DecimalPlaces="2" Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>Est Exs Amount
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="spin_EstCostAmt" DisplayFormatString="0.00" ReadOnly="True" BackColor="Control"
                                                            runat="server" Width="165" Value='<%# Eval("EstCostAmt")%>' DecimalPlaces="2" Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td rowspan="3">Remark
                                                    </td>
                                                    <td colspan="3" rowspan="3">
                                                        <dxe:ASPxMemo ID="txt_Remark" Rows="3" runat="server" Width="465" Text='<%# Eval("Remark") %>'>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                    <td>Permit
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxMemo Rows="3" ID="txt_Ref_Permit" Width="165" row="3" runat="server" Text='<%# Bind("PermitRemark") %>'>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
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
                                                                 <td style="width: 80px;">Last Updated</td>
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
                                <dxtc:TabPage Text="Shipper" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl2" runat="server">
                                            <table>
                                                <tr>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td width="100">
                                                                    <dxe:ASPxButton ID="btn_Shipper_Pick1" runat="server" Width="85" HorizontalAlign="Left" Text="Shipper" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                        PopupCustAdr(null,txt_Shipper2);
                                                                            }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Shipper2" Rows="3" ClientInstanceName="txt_Shipper2" runat="server"
                                                                        Width="300" Text='<%# Eval("SShipperRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td width="100">
                                                                    <dxe:ASPxButton ID="btn_Consignee_Pick" runat="server" Width="85" Text="Consignee" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                        PopupAgentAdr(null,txt_Consigee2);
                                                                            }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Consigee2" Rows="3" ClientInstanceName="txt_Consigee2" runat="server"
                                                                        Width="300" Text='<%# Eval("SConsigneeRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="border-bottom: solid 1px black;"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td width="100">
                                                                    <dxe:ASPxButton ID="btn_Party_Pick" runat="server" Width="85" Text="Notify Party" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                        PopupAgentAdr(null,txt_Party2);
                                                                            }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Party2" Rows="3" ClientInstanceName="txt_Party2" runat="server"
                                                                        Width="300" Text='<%# Eval("SNotifyPartyRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td width="100">
                                                                    <dxe:ASPxButton ID="btn_Agent_Pick1" runat="server" Width="85" HorizontalAlign="Left" Text="Agent" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                        PopupAgentAdr(null,txt_Agt2);
                                                                            }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo Rows="3" ID="txt_Agt2" ClientInstanceName="txt_Agt2" runat="server"
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
                                <dxtc:TabPage Text="Haulier" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl2a" runat="server">
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
                                                    <td width="180">
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
                                                        <dxe:ASPxButton ID="ASPxButton15" runat="server" HorizontalAlign="Left" Width="100" Text="Truck To" AutoPostBack="False">
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
                                                        <dxe:ASPxButton ID="ASPxButton16" runat="server" HorizontalAlign="Left" Width="100" Text="Return To" AutoPostBack="False">
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
                                                <tr style="display: none">
                                                    <td>Weight
                                                    </td>
                                                    <td colspan="4">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                                                        ID="spin_HaulierWt" Height="21px" Value='<%# Eval("HaulierWt")%>' DecimalPlaces="3" Increment="0">
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>Volume
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="70"
                                                                        ID="spin_HaulierM3" Height="21px" Value='<%# Eval("HaulierM3")%>' DecimalPlaces="3" Increment="0">
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>Qty
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit runat="server" Width="65"
                                                                        ID="spin_HaulierPkgs" Height="21px" Value='<%# Eval("HaulierQty")%>' Increment="0">
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>Pkgs Type
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="80" ID="txt_HaulierPkgsType"
                                                                        Text='<%# Eval("HaulierPkgType")%>'>
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
                                <dxtc:TabPage Text="Container" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl5" runat="server">
                                            <dxe:ASPxButton ID="ASPxButton13" Width="150" runat="server" Text="Add Container" Visible='<%# SafeValue.SafeString(Eval("JobType"),"LCL")!="FCL" %>'
                                                Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                       grid_ref_Cont.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>
                                            <dxwgv:ASPxGridView ID="Grid_RefCont" ClientInstanceName="grid_ref_Cont" runat="server"
                                                KeyFieldName="SequenceId" DataSourceID="dsImpRefCont" Width="100%" OnBeforePerformDataSelect="Grid_RefCont_DataSelect"
                                                OnRowUpdating="Grid_RefCont_RowUpdating" OnRowInserting="Grid_RefCont_RowInserting"
                                                OnRowDeleting="Grid_RefCont_RowDeleting" OnInitNewRow="Grid_RefCont_InitNewRow"
                                                OnInit="Grid_RefCont_Init">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"N")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"N")=="USE" %>'
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"N")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"N")=="USE" %>'
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_ref_Cont.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.UpdateEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.CancelEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContainerNo" VisibleIndex="2" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="false">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Seal No" FieldName="SealNo" VisibleIndex="3" Width="200" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="false">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn
                                                        Caption="Cont Type" FieldName="ContainerType" VisibleIndex="4" Width="200">
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
                                                    <dxwgv:GridViewDataTextColumn Caption="MkgType" FieldName="MkgType" Visible="false" VisibleIndex="7" Width="100">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsEditing Mode="InLine" />
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>

                                <dxtc:TabPage Text="Billing" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl_Bill" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice_Import,"SI", txt_RefN.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton7" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import,"SI", txt_RefN.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import,"SI", txt_RefN.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>

                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="Grid_Invoice_Import" ClientInstanceName="Grid_Invoice_Import"
                                                runat="server" KeyFieldName="SequenceId" DataSourceID="dsInvoice" Width="100%"
                                                OnBeforePerformDataSelect="Grid_Invoice_Import_DataSelect">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="70">
                                                        <DataItemTemplate>
                                                            <a onclick='ShowInvoice(Grid_Invoice_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>","<%# Eval("MastType") %>");'>Edit</a>&nbsp; <a onclick='PrintInvoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
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
                                                        <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import,"SI", txt_RefN.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import,"SI", txt_RefN.GetText(), "0");
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
                                                            <a onclick='ShowPayable(Grid_Payable_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>","<%# Eval("MastType") %>");'>Edit</a>&nbsp; <a onclick='PrintPayable("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
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
                                            <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add Costing" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Cost.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>
                                            <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server"
                                                DataSourceID="dsCosting" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect"
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
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_Cost.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Cost.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                                    <dxwgv:GridViewDataTextColumn FieldName="JobNo" VisibleIndex="10" Visible="true" Width="120">
                                                    </dxwgv:GridViewDataTextColumn>
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
                                                                <td colspan="2">
                                                                    <dxe:ASPxTextBox Width="265" ID="txt_CostDes" ClientInstanceName="txt_CostDes" BackColor="Control"
                                                                        ReadOnly="true" runat="server" Text='<%# Bind("ChgCodeDes") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>Vendor
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_CostVendorId" ClientInstanceName="txt_CostVendorId" Width="80" runat="server" Text='<%# Bind("VendorId")%>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                    PopupVendor(txt_CostVendorId,txt_CostVendorName);
                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxTextBox runat="server" BackColor="Control" Width="200" ID="txt_CostVendorName"
                                                                        ClientInstanceName="txt_CostVendorName" ReadOnly="true" Text=''>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Job No
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox Width="100" ID="txt_CostJobNo" ClientInstanceName="txt_CostJobNo"
                                                                        runat="server" Text='<%# Bind("JobNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>Split Type
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cbo_SplitType" runat="server" Text='<%# Bind("SplitType")%>' Width="205">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="Set" Value="Set" />
                                                                            <dxe:ListEditItem Text="WtM3" Value="WtM3" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>Remark
                                                                </td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="spin_CostRmk" Text='<%# Bind("Remark") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Doc No
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="100" ID="txt_DocNo" ClientInstanceName="txt_DocNo" Text='<%# Bind("DocNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td colspan="2">
                                                                    <table>
                                                                        <td>Pay on Behalf
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txt_PayInd" Width="60" runat="server"
                                                                                Text='<%# Bind("PayInd") %>'>
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Y" Value="Y" />
                                                                                    <dxe:ListEditItem Text="N" Value="N" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td>Verifry Ind
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txt_VerifryInd" Width="60" runat="server"
                                                                                Text='<%# Bind("VerifryInd") %>'>
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Y" Value="Y" />
                                                                                    <dxe:ListEditItem Text="N" Value="N" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </table>
                                                                </td>
                                                                <td>Salesman
                                                                </td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Salesman"
                                                                        DataSourceID="dsSalesman" TextField="Code" ValueField="Code" Width="100%" Value='<%# Bind("Salesman")%>'>
                                                                    </dxe:ASPxComboBox>
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
                                                                                <dxe:ASPxButtonEdit ID="cmb_CostSaleCurrency" ClientInstanceName="cmb_CostSaleCurrency" runat="server" Width="80" Text='<%# Bind("SaleCurrency") %>' MaxLength="3">
                                                                                    <Buttons>
                                                                                        <dxe:EditButton Text=".."></dxe:EditButton>
                                                                                    </Buttons>
                                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostSaleCurrency,spin_CostSaleExRate);
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
                                                                                    Value='<%# Eval("SaleLocAmt")%>'>
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
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
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
                                                                                <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" runat="server" Width="80" Text='<%# Bind("CostCurrency") %>' MaxLength="3">
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
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
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
                                                        </table>
                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                            <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'>
                                                                <ClientSideEvents Click="function(s,e){grid_Cost.UpdateEdit();}" />
                                                            </dxe:ASPxHyperLink>
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                        </div>
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
                                                        <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Upload Attachments"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false"
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
                                                <Settings />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                    grid_ref.PerformCallback('Photo');
                                    }" />
                        </dxtc:ASPxPageControl>
                        <dxe:ASPxButton ID="ASPxButton3" Width="150" runat="server" Text="Add House"
                            Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                        ShowHouse('0');
                                                        }" />
                        </dxe:ASPxButton>
                        <dxwgv:ASPxGridView ID="grid_Export" ClientInstanceName="detailGrid" runat="server"
                            KeyFieldName="SequenceId" Width="900px" AutoGenerateColumns="False" DataSourceID="dsImport"
                            OnBeforePerformDataSelect="grid_Export_DataSelect">
                            <SettingsCustomizationWindow Enabled="True" />
                            <SettingsEditing Mode="EditForm" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                    <DataItemTemplate>
                                        <a onclick="ShowHouse('<%# Eval("JobNo") %>');">Edit</a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Import No" FieldName="JobNo" VisibleIndex="1" Width="120"
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
                                <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6" Width="30">
                                    <PropertiesTextEdit DisplayFormatString="f0">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="T/S Ind" FieldName="TsInd" VisibleIndex="7" Width="40">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Exp BkgNo" FieldName="TsBkgNo" VisibleIndex="7" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="8" Width="30">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Create By" FieldName="CreateBy" VisibleIndex="9" Width="50">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="True" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" DisplayFormat="{0:0}" />
                                <dxwgv:ASPxSummaryItem FieldName="Weight" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                <dxwgv:ASPxSummaryItem FieldName="Volume" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0:0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_ref">
            </dxwgv:ASPxGridViewExporter>
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
