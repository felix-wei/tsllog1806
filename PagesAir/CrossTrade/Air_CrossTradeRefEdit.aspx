<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="Air_CrossTradeRefEdit.aspx.cs" Inherits="PagesAir_CrossTrade_Air_CrossTradeRefEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cross Trade</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Import_Doc.js"></script>
    <script type="text/javascript" src="/Script/Air/CrossTradeRef.js"></script>
    <script type="text/javascript" src="/Script/Edi.js"></script>
         <script type="text/javascript" >
             var isUpload = false;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsImportRef" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.AirImportRef" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsImport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.AirImport" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.AirCosting" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Code" />
            <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.AirAttachment" KeyMember="Id" FilterExpression="1=0" />
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
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='Air_CrossTradeRefEdit.aspx?no='+txt_SchRefNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton9" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){
                        window.location='Air_CrossTradeRefEdit.aspx?no=0';
                    }" />
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                                UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s,e) {
                                           window.location='Air_CrossTradeRefList.aspx';
                        }" />
                            </dxe:ASPxButton>
                        </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_ref" ClientInstanceName="grid_ref" runat="server" KeyFieldName="Id"
                Width="900px" AutoGenerateColumns="False" DataSourceID="dsImportRef" OnInitNewRow="grid_ref_InitNewRow"
                OnInit="grid_ref_Init" OnCustomCallback="grid_ref_CustomCallback" OnHtmlEditFormCreated="grid_ref_HtmlEditFormCreated" OnCustomDataCallback="grid_ref_CustomDataCallback">
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
                                        Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' UseSubmitBehavior="false"
                                        Text="EDI">
                                         <ClientSideEvents  Click="function(s,e){
                                             EdiAirRef(txt_RefN.GetText(),'ACT');                                                     
                                                        }"/>
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="100" AutoPostBack="false"
                                        Enabled='<%#SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' UseSubmitBehavior="false"
                                        Text="Save">
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid_ref.PerformCallback('Save');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_CloseJob" Width="90" runat="server" Text="Close Job" AutoPostBack="False"
                                        UseSubmitBehavior="false"  Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'> 
                                        <ClientSideEvents Click="function(s, e) {
                                                                grid_ref.GetValuesOnCustomCallback('CloseJob',OnCloseCallBack);
                                                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_VoidMaster" runat="server" Width="100"  Text=" Void"  AutoPostBack="False"
                                        UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                               grid_ref.GetValuesOnCustomCallback('VoidMaster',OnCloseCallBack);                 
                                                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <table style="border: solid 1px black; padding: 2px 2px 2px 2px; width: 100%">
                            <tr>
                                <td colspan="6" style="background-color: Gray; color: White;">
                                    <b>Print Documents</b>
                                </td>
                            </tr>

                            <tr>
                                <td width="80"> <a href='/ReportFreightSea/printview.aspx?document=ACM_Manifest&master=<%# Eval("RefNo")%>&house=0'
                                        target="_blank">Manifest</a>
                                </td >
                                <td width="80"><a href='/ReportFreightSea/printview.aspx?document=ACM_OrgMAWB&master=<%# Eval("RefNo")%>&house=0'
                                    target="_blank">ORG MAWB</a>
                                </td>
                                <td width="80"><a href='/ReportFreightSea/printview.aspx?document=ACM_DraftMAWB&master=<%# Eval("RefNo")%>&house=0'
                                    target="_blank">Draft MAWB</a></td>
                                <td width="80"><a href='/ReportFreightSea/printview.aspx?document=Air_PL&master=<%# Eval("RefNo")%>&house=0'
                                    target="_blank">Print P&L</a></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                        <div style="display: none">
                            <dxe:ASPxTextBox runat="server" Width="60" ID="txt_Id" ClientInstanceName="txt_Id"
                                Text='<%# Eval("Id") %>'>
                            </dxe:ASPxTextBox>
                        </div>
                        <table style="padding: 2px 2px 2px 2px; width: 100%; background-color:white;">
                            <tr>
                                <td width="100">Ref No
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_RefN" ClientInstanceName="txt_RefN"
                                                    BackColor="Control" ReadOnly="true" Text='<%# Eval("RefNo")%>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="100">Ref Date</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_RefDate" Width="165" runat="server" Value='<%# Eval("RefDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>MAWB No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100" ID="txt_MAWB" Text='<%# Eval("MAWB")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Agent
                                </td>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_AgentId" ClientInstanceName="txt_AgentId" runat="server" Width="100" Text='<%# Eval("AgentId")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupAgent(txt_AgentId,txt_AgentName);
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="400" BackColor="Control" ID="txt_AgentName"
                                                    ReadOnly="true" ClientInstanceName="txt_AgentName">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>Booking Ref
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100" ID="txt_CrBkgRefN" Text='<%# Eval("CarrierBkgNo")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td>Customer
                                </td>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>

                                                <dxe:ASPxButtonEdit ID="txt_LocalCust" ClientInstanceName="txt_LocalCust"
                                                    runat="server" Width="100"
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
                                                <dxe:ASPxTextBox runat="server" BackColor="Control" Width="440"
                                                    ID="txt_LocalCustName"
                                                    ClientInstanceName="txt_LocalCustName" ReadOnly="true" Text="">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>Airline
                                </td>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="txt_CrAgentId" ClientInstanceName="txt_CrAgentId" runat="server" Text='<%# Eval("CarrierAgentId") %>' Width="100px" HorizontalAlign="Left" AutoPostBack="False">
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                    PopupVendor(txt_CrAgentId,txt_CrAgentName);
                                                                        }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_CrAgentName" runat="server" BackColor="Control" ClientInstanceName="txt_CrAgentName" ReadOnly="True" Width="400">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr
                        
                        
                        </table>
                        <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%" Height="1020px"  ActiveTabIndex="0">
                            <TabPages>
                                <dxtc:TabPage Text="Air CrossTrade Master" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl1" runat="server">
                                            <table border="0" width="900">
                                                <tr>
                                                    <td colspan="6">
                                                        <hr />
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="120">Departure Airport
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="tbxAirportCode0" ClientInstanceName="tbxAirportCode0" runat="server" Width="110"
                                                                        Text='<%# Eval("AirportCode0")%>' HorizontalAlign="Left" AutoPostBack="False" MaxLength="3">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupAirPort(tbxAirportCode0,tbxAirportName0);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirportName0" ClientInstanceName="tbxAirportName0" Width="120" runat="server" Text='<%# Eval("AirportName0")%>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>Departure&nbsp;&nbsp; Date/Time 
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="date_FlightDate0" Width="100" runat="server" Value='<%# Eval("FlightDate0")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="tbxFlightTime0" Width="80" runat="server" Text='<%# Eval("FlightTime0")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                        <table width="100%">
                                                            <tr>
                                                                <td width="120">Arrive Airport
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_AirportCode1" ClientInstanceName="txt_AirportCode1" runat="server" Width="110"
                                                                        Text='<%# Eval("AirportCode1")%>' HorizontalAlign="Left" AutoPostBack="False" MaxLength="3">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupAirPort(txt_AirportCode1,txt_AirportName1);
                                                                            }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_AirportName1" ClientInstanceName="txt_AirportName1" Width="120" runat="server" Text='<%# Eval("AirportName1")%>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>Final Arrival Date/Time   
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="spin_FlightDate1" Width="100" runat="server" Value='<%# Eval("FlightDate1")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_FlightTime1" Width="80" runat="server" Text='<%# Eval("FlightTime1")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" border="1" cellspacing="0">
                                                            <tr>
                                                                <td>#</td>
                                                                <td colspan="2">Airline</td>
                                                                <td>Flight No</td>
                                                                <td>Date</td>
                                                                <td>Time</td>
                                                                <td colspan="2">Airport</td>
                                                            </tr>

                                                            <tr class="eee">
                                                                <td>1.</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirlineCode1" Width="60" runat="server" Text='<%# Eval("AirlineCode1") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirlineName1" Width="120" runat="server" Text='<%# Eval("AirlineName1") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxFlightNo1" Width="60" runat="server" Text='<%# Eval("FlightNo1") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="tbxAirFlightDate1" Width="100" runat="server" Value='<%# Eval("AirFlightDate1") %>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirFlightTime1" Width="50" runat="server" Text='<%# Eval("AirFlightTime1") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                     <dxe:ASPxButtonEdit ID="tbxAirLinePortCode1" ClientInstanceName="tbxAirLinePortCode1" runat="server" Width="110"
                                                                        Text='<%# Eval("AirLinePortCode1")%>' HorizontalAlign="Left" AutoPostBack="False" MaxLength="3">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupAirPort(tbxAirLinePortCode1,tbxAirLinePortName1);
                                                                            }" /></dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirLinePortName1" ClientInstanceName="tbxAirLinePortName1" Width="120" runat="server" Text='<%# Eval("AirLinePortName1") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="eef">
                                                                <td>2.</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirlineCode2" Width="60" runat="server" Text='<%# Eval("AirlineCode2") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirlineName2" Width="120" runat="server" Text='<%# Eval("AirlineName2") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxFlightNo2" Width="60" runat="server" Text='<%# Eval("FlightNo2") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="tbxFlightDate2" Width="100" runat="server" Value='<%# Eval("FlightDate2") %>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxFlightTime2" Width="50" runat="server" Text='<%# Eval("FlightTime2") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="tbxAirportCode2" ClientInstanceName="tbxAirportCode2" runat="server" Width="110"
                                                                        Text='<%# Eval("AirportCode2")%>' HorizontalAlign="Left" AutoPostBack="False" MaxLength="3">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupAirPort(tbxAirportCode2,tbxAirportName2);
                                                                            }" /></dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirportName2" ClientInstanceName="tbxAirportName2" Width="120" runat="server" Text='<%# Eval("AirportName2") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="eee">
                                                                <td>3.</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirlineCode3" Width="60" runat="server" Text='<%# Eval("AirlineCode3") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirlineName3" Width="120" runat="server" Text='<%# Eval("AirlineName3") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxFlightNo3" Width="60" runat="server" Text='<%# Eval("FlightNo3") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="tbxFlightDate3" Width="100" runat="server" Value='<%# Eval("FlightDate3") %>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxFlightTime3" Width="50" runat="server" Text='<%# Eval("FlightTime3") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="tbxAirportCode3" ClientInstanceName="tbxAirportCode3" runat="server" Width="110"
                                                                        Text='<%# Eval("AirportCode3")%>' HorizontalAlign="Left" AutoPostBack="False" MaxLength="3">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupAirPort(tbxAirportCode3,tbxAirportName3);
                                                                            }" /></dxe:ASPxButtonEdit>                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirportName3" ClientInstanceName="tbxAirportName3" Width="120" runat="server" Text='<%# Eval("AirportName3") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="eef">
                                                                <td>4.</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirlineCode4" Width="60" runat="server" Text='<%# Eval("AirlineCode4") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirlineName4" Width="120" runat="server" Text='<%# Eval("AirlineName4") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxFlightNo4" Width="60" runat="server" Text='<%# Eval("FlightNo4") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="tbxFlightDate4" Width="100" runat="server" Value='<%# Eval("FlightDate4") %>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxFlightTime4" Width="50" runat="server" Text='<%# Eval("FlightTime4") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="tbxAirportCode4" ClientInstanceName="tbxAirportCode4" runat="server" Width="110"
                                                                        Text='<%# Eval("AirportCode4")%>' HorizontalAlign="Left" AutoPostBack="False" MaxLength="3">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupAirPort(tbxAirportCode4,tbxAirportName4);
                                                                            }" /></dxe:ASPxButtonEdit>                                                                   </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxAirportName4" ClientInstanceName="tbxAirportName4" Width="120" runat="server" Text='<%# Eval("AirportName4") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Consolidator
                                                    </td>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_NvoccAgentId" ClientInstanceName="txt_NvoccAgentId" Height="16px" runat="server" Text='<%# Eval("NvoccAgentId") %>' Width="105px" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupVendor(txt_NvoccAgentId,txt_NvoccAgentName);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_NvoccAgentName" runat="server" BackColor="Control" ClientInstanceName="txt_NvoccAgentName" ReadOnly="True" Width="395px">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Consol Ref
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="108" ID="txt_NvoccBl" Text='<%# Eval("NvoccBlNO")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Warehouse
                                                    </td>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_WhId" ClientInstanceName="txt_WhId" runat="server" Text='<%# Eval("WareHouseId")%>' Width="105" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                            PopupVendor(txt_WhId,txt_WhName);
                                                                }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="395" BackColor="Control" ID="txt_WhName" ClientInstanceName="txt_WhName"
                                                                        ReadOnly="true" Text=''>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Weight</td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="spin_Weight" BackColor="Control" runat="server" Text='<%# Eval("Weight")%>'
                                                            Width="108" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td>Ref Currency
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="cbx_Currency" ClientInstanceName="cbx_Currency" runat="server" Width="108" MaxLength="3"
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
                                                            runat="server" Width="108" Value='<%# Eval("ExRate")%>' DecimalPlaces="6" Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td Width="100">Volume</td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="spin_Volume" BackColor="Control" runat="server" Text='<%# Eval("Volume")%>'
                                                            Width="108" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Remarks</td>
                                                    <td colspan="3">
                                                        <dxe:ASPxMemo ID="txt_Remarks" ClientInstanceName="txt_Remarks" TextMode="MultiLine" Width="515" Rows="3" runat="server" Text='<%# Eval("Remarks") %>'></dxe:ASPxMemo>
                                                    </td>
                                                    <td>Qty
                                                    </td>
                                                    <td colspan="2">
                                                        <table>
                                                            <td>
                                                                <dxe:ASPxSpinEdit runat="server" Width="40" BackColor="Control" ReadOnly="true"
                                                                    ID="spin_Pkgs" Value='<%# Eval("Qty")%>'>
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" BackColor="Control" ReadOnly="true" Width="60" ID="txt_PkgsType"
                                                                    Text='<%# Eval("PackageType")%>'>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                   <td colspan="6">
												   <hr>
                                                        <table >
                                                            <tr>
                                                                <td style="width: 80px;">Creation</td>
                                                                <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                <td style="width: 90px;">Last Updated</td>
                                                                <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                <td style="width: 80px;">Job Status</td>
                                                                <td style="width: 160px"><dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text="" /></td>
                                                            </tr>
                                                        </table>
												   <hr>
                                                    </td>
                                                </tr>

                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="HAWB">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="ASPxButton3" Width="150" runat="server" Text="Add HAWB"
                            Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                        ShowHouse('0');
                                                        }" />
                        </dxe:ASPxButton>
                        <dxwgv:ASPxGridView ID="grid_Export" ClientInstanceName="detailGrid" runat="server"
                            KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" DataSourceID="dsImport"
                            OnBeforePerformDataSelect="grid_Export_DataSelect">
                            <SettingsCustomizationWindow Enabled="True" />
                            <SettingsEditing Mode="EditForm" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                    <DataItemTemplate>
                                        <a onclick='ShowHouse("<%# Eval("JobNo") %>");'>Edit</a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" VisibleIndex="1"
                                    SortIndex="1" SortOrder="Ascending" Width="200">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" VisibleIndex="3" Width="200"
                                    SortIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="HAWB" FieldName="Hawb" VisibleIndex="3"
                                    SortIndex="1" Width="200">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6">
                                    <PropertiesTextEdit DisplayFormatString="f0">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="7">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="True" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" />
                                <dxwgv:ASPxSummaryItem FieldName="GrossWeight" SummaryType="Sum" />

                                <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Airway Bill">
                                    <ContentCollection>

                                        <dxw:ContentControl ID="ContentControl4" runat="server">


                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td>MAWB:
						<asp:Label ID="lblMAWB" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblHAWB" runat="server" Font-Bold="True" Text='<%# Eval("Mawb") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellspacing="1" cellpadding="1" width="100%" bordercolordark="white" bordercolorlight="#c19771"
                                                border="1">
                                                <tr>
                                                    <td width="50%">Shipper's Name and Address<br>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td id="SHIPPER">
                                                                    <dxe:ASPxTextBox runat="server" ID="tbxShipperID" Visible="false"></dxe:ASPxTextBox>
                                                                    <dxe:ASPxMemo ID="tbxShipperName" ClientInstanceName="tbxShipperName" TextMode="MultiLine" Width="350" Rows="5" runat="server" Text='<%# Eval("ShipperName") %>'></dxe:ASPxMemo>
                                                                </td>
                                                                <td valign="top">
                                                                    <a id="linkGetShipper" title="Retrieve Shipper List"
                                                                        href="#" onclick="javascript:PopupCustAdr(null,tbxShipperName);">
                                                                        <img src="/images/ico_list.gif" border="0"></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">Air Waybill Issued By<br>
                                                        <dxe:ASPxMemo ID="tbxIssuedBy" ClientInstanceName="tbxIssuedBy" TextMode="MultiLine" Width="300" Rows="5" runat="server" Text='<%# Eval("IssuedBy") %>'></dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Consignee's Name and Address<br>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td id="CONSIGNEE">
                                                                    <dxe:ASPxTextBox runat="server" ID="tbxConsigneeID" Visible="false"></dxe:ASPxTextBox>
                                                                    <dxe:ASPxMemo ID="tbxConsigneeName" ClientInstanceName="tbxConsigneeName" TextMode="MultiLine" Width="350" Rows="5" runat="server" Text='<%# Eval("ConsigneeName") %>'></dxe:ASPxMemo>
                                                                </td>
                                                                <td valign="top"><a id="linkGetConsignee" title="Retrieve Consignee List"
                                                                    href="#" onclick="javascript:PopupAgentAdr(null,tbxConsigneeName);">
                                                                    <img src="/images/ico_list.gif" border="0"></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">Issuing Carrier's Agent Name and City<br>
                                                        <dxe:ASPxMemo ID="tbxCarrierAgent" ClientInstanceName="tbxCarrierAgent" TextMode="MultiLine" Width="350px" Rows="3" runat="server" Text='<%# Eval("CarrierAgent") %>'></dxe:ASPxMemo>
                                                    </td>
                                                    <td rowspan="3" valign="top" id="ACCOUNT">Accounting Information<br>
                                                        <dxe:ASPxMemo ID="tbxAccountInfo" ClientInstanceName="tbxAccountInfo" TextMode="MultiLine" Width="350px" Rows="5" runat="server" Text='<%# Eval("AccountInfo") %>'></dxe:ASPxMemo>
                                                        <a id="linkGetAccountInfo" title="Retrieve Account info List"
                                                            href="#" onclick="javascript:PopupAccountCustomer(null,tbxAccountInfo);">
                                                            <img src="/images/ico_list.gif" border="0"></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>Agents IATA Code<br>
                                                                    <dxe:ASPxTextBox ID="tbxAgentIATACode" runat="server" Text='<%# Eval("AgentIATACode") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>Account No.<br>
                                                                    <dxe:ASPxTextBox ID="tbxAgentAccountNo" runat="server" Text='<%# Eval("AgentAccountNo") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Airport of Departure (Addr of First Carrier) and Requested Routing<br>
                                                        <dxe:ASPxTextBox ID="tbxAirportDeparture" Width="350px" runat="server" Text='<%# Eval("AirportDeparture") %>'></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>to<br>
                                                                    <dxe:ASPxTextBox ID="tbxConnDest1" Width="40px" runat="server" Text='<%# Eval("ConnDest1") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>By First Carrier<br>
                                                                    <dxe:ASPxTextBox ID="tbxConnCarrier1" Width="130" runat="server" Text='<%# Eval("ConnCarrier1") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>to<br>
                                                                    <dxe:ASPxTextBox ID="tbxConnDest2" Width="50px" runat="server" Text='<%# Eval("ConnDest2") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>by<br>
                                                                    <dxe:ASPxTextBox ID="tbxConnCarrier2" Width="65" runat="server" Text='<%# Eval("ConnCarrier2") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>to<br>
                                                                    <dxe:ASPxTextBox ID="tbxConnDest3" Width="50px" runat="server" Text='<%# Eval("ConnDest3") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>by<br>
                                                                    <dxe:ASPxTextBox ID="tbxConnCarrier3" Width="65" runat="server" Text='<%# Eval("ConnCarrier3") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                            width="100%">
                                                            <tr>
                                                                <td rowspan="2" align="center">Currency
                                                                </td>
                                                                <td rowspan="2" align="center">Chgs Code
                                                                </td>
                                                                <td colspan="2" align="center">WT VAL
                                                                </td>
                                                                <td colspan="2" align="center">Other
                                                                </td>
                                                                <td rowspan="2" align="center">Declared Value for Carriage
                                                                </td>
                                                                <td rowspan="2" align="center">Declared Value for Customs
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>PPD
                                                                </td>
                                                                <td>COLL
                                                                </td>
                                                                <td>PPD
                                                                </td>
                                                                <td>COLL
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxCurrency" Width="50px" runat="server" MaxLength="3" Text='<%# Eval("Currency") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxChgsCode" Width="40px" runat="server" Text='<%# Eval("ChgsCode") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxPPD1" Width="30px" runat="server" Text='<%# Eval("PPD1") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxCOLL1" Width="40px" runat="server" Text='<%# Eval("COLL1") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxPPD2" Width="40px" runat="server" Text='<%# Eval("PPD2") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxCOLL2" Width="40px" runat="server" Text='<%# Eval("COLL2") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxCarriageValue" Width="80px" runat="server" Text='<%# Eval("CarriageValue") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxCustomValue" Width="75px" runat="server" Text='<%# Eval("CustomValue") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="auto-style1">Airport of Destination<br>
                                                                    <dxe:ASPxTextBox ID="tbxAirportDestination" Width="200px" runat="server" Text='<%# Eval("AirportDestination") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td width="200">Requested Flight/Date<br>
                                                                    <div style="float: left">
                                                                        <dxe:ASPxTextBox ID="tbxRequestedFlight" Width="90px" runat="server" Text='<%# Eval("RequestedFlight") %>'></dxe:ASPxTextBox>
                                                                    </div>
                                                                    <div style="float: left; margin-left: 0px;">
                                                                        <dxe:ASPxTextBox ID="tbxRequestedDate" Width="90px" runat="server" Text='<%# Eval("RequestedDate") %>'></dxe:ASPxTextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Amount of Insurance<br>
                                                        <dxe:ASPxTextBox ID="tbxAmountInsurance" runat="server" Text='<%# Eval("AmountInsurance") %>'></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Handling Information<br>
                                                        <dxe:ASPxMemo ID="tbxHandlingInfo" TextMode="MultiLine" Width="500px" Rows="3" runat="server" Text='<%# Eval("HandlingInfo") %>'></dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                            width="100%">
                                                            <tr>
                                                                <td align="center">No. of<br>
                                                                    Pieces<br>
                                                                    RCP
                                                                </td>
                                                                <td align="center">Gross<br>
                                                                    Weight
                                                                </td>
                                                                <td align="center">kg<br>
                                                                    lb
                                                                </td>
                                                                <td align="center">Rate Class
                                                                </td>
                                                                <td align="center">Commodity<br>
                                                                    Item No.
                                                                </td>
                                                                <td align="center">Chargeable<br>
                                                                    Weight
                                                                </td>
                                                                <td align="center">Rate / Charge<br>
                                                                </td>
                                                                <td align="center">Total<br />
                                                                    <asp:CheckBox ID="chkShowAmount" Text="Show Amount" AutoPostBack="True" runat="server"></asp:CheckBox>
                                                                </td>
                                                                <td align="center">Nature and Quantity of Goods<br>
                                                                    (incl. Dimensions or Volume)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxPiece" Width="35px" runat="server" Text='<%# Eval("Piece") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" DecimalPlaces="3" Increment="0" ID="tbxGrossWeight" Width="65px" runat="server" Text='<%# Eval("GrossWeight") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.0" DecimalPlaces="1" Increment="0" ID="tbxUnit" Width="50px" runat="server" Text='<%# Eval("Unit") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxRateClass" Width="50px" runat="server" Text='<%# Eval("RateClass") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxCommodityItemNo" Width="65px" runat="server" Text='<%# Eval("CommodityItemNo") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxSpinEdit isplayFormatString="0.0" DecimalPlaces="1" Increment="0" ID="tbxChargeableWeight" Width="65px" runat="server" Text='<%# Eval("ChargeableWeight") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxTextBox ID="tbxRateCharge" Width="65px" runat="server" Text='<%# Eval("RateCharge") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.0" DecimalPlaces="1" Increment="0" ID="tbxTotal" Width="88px" runat="server" Text='<%# Eval("Total") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td rowspan="2" align="center">
                                                                    <dxe:ASPxMemo ID="tbxGoodsNature" TextMode="MultiLine" Width="350px" Rows="10" runat="server" Text='<%# Eval("GoodsNature") %>'></dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8">
                                                                    <dxe:ASPxMemo ID="tbxContentRemark" TextMode="MultiLine" Columns="65" Rows="8" runat="server" Text='<%# Eval("ContentRemark") %>'></dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <b>Prepaid</b>
                                                                            </td>
                                                                            <td align="center">Weight Charge
                                                                            </td>
                                                                            <td align="center">
                                                                                <b>Collect</b>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxWeightChargeP" runat="server" Text='<%# Eval("WeightChargeP") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxWeightChargeC" runat="server" Text='<%# Eval("WeightChargeC") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td align="center">Valuation Charge
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxValuationChargeP" runat="server" Text='<%# Eval("ValuationChargeP") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxValuationChargeC" runat="server" Text='<%# Eval("ValuationChargeC") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td align="center">Tax
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxTaxP" runat="server" Text='<%# Eval("TaxP") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxTaxC" runat="server" Text='<%# Eval("TaxC") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td align="center">Total Other Charges Due Agent
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxOtherAgentChargeP" runat="server" Text='<%# Eval("OtherAgentChargeP") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxOtherAgentChargeC" runat="server" Text='<%# Eval("OtherAgentChargeC") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td align="center">Total Other Charges Due Carrier
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxOtherCarrierChargeP" runat="server" Text='<%# Eval("OtherCarrierChargeP") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxOtherCarrierChargeC" runat="server" Text='<%# Eval("OtherCarrierChargeC") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td align="center" width="50%">Total Prepaid
                                                                            </td>
                                                                            <td align="center">Total Collect
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxTotalPrepaid" runat="server" Text='<%# Eval("TotalPrepaid") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxTotalCollect" runat="server" Text='<%# Eval("TotalCollect") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td align="center" width="50%">Currency Conversion Rates
                                                                            </td>
                                                                            <td align="center">cc Charges in Dest. Currency
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxCurrencyRate" runat="server" Text='<%# Eval("CurrencyRate") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="tbxChargeDestCurrency" runat="server" Text='<%# Eval("ChargeDestCurrency") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table border="1" cellpadding="0" cellspacing="0" bordercolordark="white" bordercolorlight="#8686ff"
                                                            width="100%">
                                                            <tr>
                                                                <td>Other Charges
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                        <tr>
                                                                            <td width="100px">Item</td>
                                                                            <td width="50px">Currency</td>
                                                                            <td width="80px" align="right">Amount</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge1" Width="100px" runat="server" Text='<%# Eval("OtherCharge1") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td width="80">
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge1Currency" Width="50px" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge1Currency") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit ID="tbxOtherCharge1Amount" DisplayFormatString="0.00" Increment="0" DecimalPlaces="3" Width="60px" runat="server" Text='<%# Eval("OtherCharge1Amount") %>'>
                                                                                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge2" Width="100px" runat="server" Text='<%# Eval("OtherCharge2") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td width="80">
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge2Currency" Width="50px" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge2Currency") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" DecimalPlaces="3" Increment="0" ID="tbxOtherCharge2Amount" Width="60px" runat="server" Text='<%# Eval("OtherCharge2Amount") %>'>
                                                                                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge3" Width="100px" runat="server" Text='<%# Eval("OtherCharge3") %>'></dxe:ASPxTextBox>
                                                                            </td>

                                                                            <td width="80">
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge3Currency" Width="50px" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge3Currency") %>'></dxe:ASPxTextBox>
                                                                            </td>

                                                                            <td>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" DecimalPlaces="3" Increment="0" runat="server" ID="tbxOtherCharge3Amount" Text='<%# Eval("OtherCharge3Amount") %>' Width="60px">
                                                                                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>

                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge4" Width="100px" runat="server" Text='<%# Eval("OtherCharge4") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td width="80">
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge4Currency" Width="50px" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge4Currency") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" DecimalPlaces="3" ID="tbxOtherCharge4Amount" Increment="0" Width="60px" runat="server" Text='<%# Eval("OtherCharge4Amount") %>'>
                                                                                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge5" Width="100px" runat="server" Text='<%# Eval("OtherCharge5") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td width="80">
                                                                                <dxe:ASPxTextBox ID="tbxOtherCharge5Currency" Width="50px" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge5Currency") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" DecimalPlaces="3" Increment="0" ID="tbxOtherCharge5Amount" Width="60px" runat="server" Text='<%# Eval("OtherCharge5Amount") %>'>
                                                                                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Signature of Shipper or his Agent<br>
                                                                    <dxe:ASPxTextBox ID="tbxSignatureShipper" Width="300px" runat="server" Text='<%# Eval("SignatureShipper") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td>Executed On (Date)<br>
                                                                            </td>
                                                                            <td>at (Place)<br>
                                                                            </td>
                                                                            <td>Signature of Issuing Carrier or its Agent<br>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="tbxExecuteDate" Width="120" runat="server" Text='<%# Eval("ExecuteDate") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="tbxExecutePlace" Width="120" runat="server" Text='<%# Eval("ExecutePlace") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="tbxSignatureIssuing" Width="180" runat="server" Text='<%# Eval("SignatureIssuing") %>'></dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
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
                                                       AddInvoice(Grid_Invoice_Import, "ACT", txt_RefN.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton7" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import, "ACT", txt_RefN.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import, "ACT", txt_RefN.GetText(), "0");
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
                                                            <a onclick='ShowInvoice(Grid_Invoice_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintInvoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
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
                                                        <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import, "ACT", txt_RefN.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import, "ACT", txt_RefN.GetText(), "0");
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
                                            <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add Internal Cost" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Cost.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>
                                            <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server"
                                                DataSourceID="dsCosting" KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect" OnRowDeleted="grid_Cost_RowDeleted"
                                                OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting" OnRowInserted="grid_Cost_RowInserted" OnRowDeleting="grid_Cost_RowDeleting"
                                                OnRowUpdating="grid_Cost_RowUpdating" OnRowUpdated="grid_Cost_RowUpdated" OnHtmlEditFormCreated="grid_Cost_HtmlEditFormCreated">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <SettingsEditing Mode="EditFormAndDisplayRow" />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
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
                                                        <PropertiesSpinEdit DisplayFormatString="{0:0.000}"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Cost Price" FieldName="CostPrice" VisibleIndex="4" Width="50">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:0.00}"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Cost" FieldName="CostLocAmt" VisibleIndex="5" Width="50">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:0.00}"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Sale Qty" FieldName="SaleQty" VisibleIndex="13" Width="50">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:0.000}"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Sale Price" FieldName="SalePrice" VisibleIndex="14" Width="50">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:0.00}"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Sale" FieldName="SaleLocAmt" VisibleIndex="15" Width="50">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:0.00}"></PropertiesSpinEdit>
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
                                                                    <dxe:ASPxTextBox Width="270" ID="txt_CostDes" ClientInstanceName="txt_CostDes" BackColor="Control"
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
                                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txt_PayInd" Width="52" runat="server"
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
                                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txt_VerifryInd" Width="52" runat="server"
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
                                                            
                                                                <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>' >
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
                                <dxtc:TabPage Text="Attachments" Name="Attachments">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl8" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton12" Width="150" runat="server" Text="Upload Attachments"
                                                            Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                                 isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'>
                                                            <ClientSideEvents Click="function(s,e) {
                                                        grd_Photo.Refresh();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto"
                                                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                 AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                <Settings  />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'
                                                                 ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'
                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <div style="height: 100px;">
                                                                            <a href='<%# Eval("Path")%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </div>
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
                                                            </table>
                                                       <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                            
                                                                <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>' >
                                                                    <ClientSideEvents Click="function(s,e){grd_Photo.UpdateEdit();}" />
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
                            </TabPages>
                        </dxtc:ASPxPageControl>
                        

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
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
