<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="Air_ImportEdit.aspx.cs" Inherits="PagesAir_Import_Air_ImportEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Import_Doc.js"></script>
    <script type="text/javascript" src="/Script/Air/Import.js"></script>
     <script type="text/javascript" >
         var isUpload = false;
    </script>
    <title>Air Import Edit</title>

</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsJob" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.AirImport" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.AirAttachment" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.AirCosting" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXUom" KeyMember="Code" />
         <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXSalesman" KeyMember="Code" FilterExpression="" />
        <div>
            <table>
                <tr>
                    <td>Import No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='Air_ImportEdit.aspx?no='+txt_RefNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                                UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s,e) {
                                           window.location='Air_ImportRefList.aspx';
                        }" />
                            </dxe:ASPxButton>
                        </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Export" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" DataSourceID="dsJob"
                OnBeforePerformDataSelect="grid_Export_DataSelect" OnInitNewRow="grid_Export_InitNewRow"
                OnInit="grid_Export_Init" OnCustomCallback="grid_Export_CustomCallback" OnCustomDataCallback="grid_Export_CustomDataCallback"
                OnHtmlEditFormCreated="grid_Export_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 900px">
                                <tr>
                                    <td width="60%">
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                            Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>' UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e) {
                                            detailGrid.PerformCallback('Save');
                                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="View Master" AutoPostBack="false"
                                            UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e) {
                                           window.location='Air_ImportRefEdit.aspx?no='+txtRefNo.GetText();
                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_VoidHawb" Width="100" runat="server" Text="Void HAWB" AutoPostBack="false"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"%>'>
                                            <ClientSideEvents Click="function(s,e) {
                                         detailGrid.GetValuesOnCustomCallback('VoidHouse',OnCloseCallBack);
                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <table style="border: solid 1px black; padding: 2px 2px 2px 2px; width: 100%">
                                <tr>
                                    <td colspan="2" style="background-color: Gray; color: White;">
                                        <b>Print Documents</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href='/ReportFreightSea/printview.aspx?document=AIH_OrgHAWB&master=<%# Eval("RefNo")%>&house=<%# Eval("JobNo")%>'
                                            target="_blank">ORG HAWB</a>&nbsp;
                                    <a href='/ReportFreightSea/printview.aspx?document=AIH_DraftHAWB&master=<%# Eval("RefNo")%>&house=<%# Eval("JobNo")%>'
                                        target="_blank">Draft HAWB</a>&nbsp;
                                    <a href='/ReportFreightSea/printview.aspx?document=AIH_DO&master=<%# Eval("RefNo")%>&house=<%# Eval("JobNo")%>'
                                        target="_blank">DO</a>&nbsp;
                                    <a href='/ReportFreightSea/printview.aspx?document=AIH_DN&master=<%# Eval("RefNo")%>&house=<%# Eval("JobNo")%>' target="_blank">DN</a>&nbsp;&nbsp;
                                    <a href='/ReportFreightSea/printview.aspx?document=Air_PL&master=<%# Eval("RefNo")%>&house=<%# Eval("JobNo")%>'
                                        target="_blank">P&L</a></td>
                                    <td>&nbsp; 
                                    </td>
                                </tr>
                            </table>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txtHouseId" ClientInstanceName="txtHouseId" runat="server" ReadOnly="true"
                                    BackColor="Control" Text='<%# Eval("Id") %>' Width="100">
                                </dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txtRefNo" ClientInstanceName="txtRefNo" runat="server" ReadOnly="true"
                                    BackColor="Control" Text='<%# Eval("RefNo") %>' Width="130">
                               </dxe:ASPxTextBox>
                            </div>
                            <table style="padding: 2px 2px 2px 2px; width: 100%; background-color: white;">
                                <tr>
                                    <td style="text-align: left">Import No
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtHouseNo" ClientInstanceName="txtHouseNo" runat="server" ReadOnly="true"
                                            BackColor="Control" Text='<%# Eval("JobNo") %>' Width="150">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>HAWB No
                                    </td>
                                    <td>
                                      <dxe:ASPxTextBox ID="txtHouseBl" runat="server" Text='<%# Eval("HAWB") %>' Width="170">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>
                                        Do Ready
                                    </td>
                                    <td>
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txtDoReady" Width="55" runat="server"
                                            Text='<%# Eval("DoReadyInd") %>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                <dxe:ListEditItem Text="N" Value="N" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>TsInd</td>
                                    <td >
                                        <dxe:ASPxComboBox ID="txt_TsInd" runat="server" Width="55" Text='<%# Eval("TsInd")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                <dxe:ListEditItem Text="N" Value="N" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">Customer
                                    </td>
                                    <td colspan="3">
                                        <table width="100%" border="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButtonEdit ID="txtCustId" ClientInstanceName="txtCustId" runat="server"
                                                        Text='<%# Eval("CustomerId")%>' Width="100" HorizontalAlign="Left" AutoPostBack="False">
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCust(txtCustId,txtCust);
                                                                    }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtCust" ReadOnly="true" BackColor="Control" ClientInstanceName="txtCust"
                                                        runat="server" Width="350">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>TsBkgRef</td>
                                    <td colspan="3">
                                        <dxe:ASPxTextBox ID="txt_TsBkgRef"  Text='<%# Eval("TsBkgRef")%>'
                                            runat="server" Width="200">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>TsBkgUser</td>
                                    <td>
                                         <dxe:ASPxTextBox ID="txt_TsBkgUser"  Text='<%# Eval("TsBkgUser")%>'
                                            runat="server" Width="150">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>DeliveryDate</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_DeliveryDate" Width="170" runat="server" Value='<%# Eval("DeliveryDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>TsBkgTime</td>
                                    <td colspan="3">
                                        <dxe:ASPxDateEdit ID="date_TsBkgTime" Width="200" runat="server" Value='<%# Eval("TsBkgTime") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                                        </dxe:ASPxDateEdit>
                                    </td>

                                </tr>
                                <tr>
                                    <td>Weight</td>
                                    <td><dxe:ASPxSpinEdit ID="spin_Weight" runat="server" Text='<%# Eval("Weight")%>'
                                                        Width="150" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3">
                                                        <SpinButtons ShowIncrementButtons="false" />
                                                    </dxe:ASPxSpinEdit></td>
                                    <td>Volume</td>
                                    <td><dxe:ASPxSpinEdit ID="spin_Volume" runat="server" Text='<%# Eval("Volume")%>'
                                                        Width="170" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3">
                                                        <SpinButtons ShowIncrementButtons="false" />
                                                    </dxe:ASPxSpinEdit></td>
                                    <td colspan="4">
                                        <table>
                                            <tr>
                                                <td Width="60">Qty</td>
                                                <td>
                                                    <dxe:ASPxSpinEdit ID="spin_Qty" runat="server" Text='<%# Eval("Qty")%>'
                                                        Width="50" Increment="0">
                                                        <SpinButtons ShowIncrementButtons="false" />
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td>Pack Type</td>
                                                <td>
                                                    <dxe:ASPxButtonEdit ID="txt_PackageType" ClientInstanceName="txt_PackageType" runat="server"
                                                                            Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_PackageType,2);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Remark</td>
                                    <td colspan="7">
                                        <dxe:ASPxMemo ID="Memo_Remark" runat="server" Rows="3" Text='<%# Eval("Remark") %>' Width="790">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Marking</td>
                                    <td colspan="7">
                                        <dxe:ASPxMemo ID="Memo_Marking" runat="server" Rows="3" Text='<%# Eval("Marking") %>' Width="790">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Description</td>
                                    <td colspan="7">
                                         <dxe:ASPxMemo ID="Memo_Des" runat="server" Rows="3" Text='<%# Eval("Description") %>' Width="790">
                                                    </dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl_Hbl" Width="100%" Height="1020px" ActiveTabIndex="0">
                                <TabPages>
                                    <dxtc:TabPage Text="Air Import Info" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl1" runat="server">   
                                                <table>
                                            
                                                        <tr>
                                                            <td>
                                                                <table border="1" bordercolordark="white" bordercolorlight="#c19771" cellpadding="1" cellspacing="1" width="100%">
                                                                    <tr>
                                                                        <td width="50%">Shipper&#39;s Name and Address<br>
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td id="SHIPPER">
                                                                                        <dxe:ASPxTextBox ID="tbxShipperID" runat="server" Visible="False">
                                                                                        </dxe:ASPxTextBox>
                                                                                        <dxe:ASPxMemo ID="tbxShipperName" runat="server" ClientInstanceName="tbxShipperName" Rows="5" Text='<%# Eval("ShipperName") %>' TextMode="MultiLine" Width="350px">
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td valign="top"><a id="linkGetShipper" href="javascript:PopupCustAdr(null,tbxShipperName);" title="Retrieve Shipper List">
                                                                                        <img border="0" src="/images/ico_list.gif"></img></a> </td>
                                                                                </tr>
                                                                            </table>

                                                                        </td>
                                                                        <td valign="top">Air Waybill Issued By<br>
                                                                            <dxe:ASPxMemo ID="tbxIssuedBy" runat="server" ClientInstanceName="tbxIssuedBy" Rows="5" Text='<%# Eval("IssuedBy") %>' TextMode="MultiLine" Width="300px">
                                                                            </dxe:ASPxMemo>

                                                                    </tr>
                                                                    <tr>
                                                                        <td>Consignee&#39;s Name and Address<br>
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td id="CONSIGNEE">
                                                                                        <dxe:ASPxTextBox ID="tbxConsigneeID" runat="server" Visible="False">
                                                                                        </dxe:ASPxTextBox>
                                                                                        <dxe:ASPxMemo ID="tbxConsigneeName" runat="server" ClientInstanceName="tbxConsigneeName" Rows="5" Text='<%# Eval("ConsigneeName") %>' TextMode="MultiLine" Width="350px">
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td valign="top"><a id="linkGetConsignee" href="javascript:PopupAgentAdr(null,tbxConsigneeName);" title="Retrieve Consignee List">
                                                                                        <img border="0" src="/images/ico_list.gif"></img></a> </td>
                                                                                </tr>
                                                                            </table>

                                                                        </td>
                                                                        <td>&nbsp; </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">Issuing Carrier&#39;s Agent Name and City<br>
                                                                            <dxe:ASPxMemo ID="tbxCarrierAgent" runat="server" ClientInstanceName="tbxCarrierAgent" Rows="3" Text='<%# Eval("CarrierAgent") %>' TextMode="MultiLine" Width="350px">
                                                                            </dxe:ASPxMemo>

                                                                        </td>
                                                                        <td id="ACCOUNT" rowspan="3" valign="top">Accounting Information<br>
                                                                            <dxe:ASPxMemo ID="tbxAccountInfo" runat="server" ClientInstanceName="tbxAccountInfo" Rows="5" Text='<%# Eval("AccountInfo") %>' TextMode="MultiLine" Width="350px">
                                                                            </dxe:ASPxMemo>
                                                                            <a id="linkGetAccountInfo" href="javascript:PopupAgentAdr(null,tbxAccountInfo);" title="Retrieve Account info List">
                                                                            <img border="0" src="/images/ico_list.gif"></img></a> 

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>Agents IATA Code<br>
                                                                                        <dxe:ASPxTextBox ID="tbxAgentIATACode" runat="server" Text='<%# Eval("AgentIATACode") %>'>
                                                                                        </dxe:ASPxTextBox>

                                                                                    </td>
                                                                                    <td>Account No.<br>
                                                                                        <dxe:ASPxTextBox ID="tbxAgentAccountNo" runat="server" Text='<%# Eval("AgentAccountNo") %>'>
                                                                                        </dxe:ASPxTextBox>

                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Airport of Departure (Addr of First Carrier) and Requested Routing<br>
                                                                            <dxe:ASPxTextBox ID="tbxAirportDeparture" runat="server" ClientInstanceName="tbxAirportDeparture" Text='<%# Eval("AirportDeparture") %>' Width="350px">
                                                                            </dxe:ASPxTextBox>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>to<br>
                                                                                        <dxe:ASPxTextBox ID="tbxConnDest1" runat="server" Text='<%# Eval("ConnDest1") %>' Width="40px">
                                                                                        </dxe:ASPxTextBox>

                                                                                    </td>
                                                                                    <td>By First Carrier<br>
                                                                                        <dxe:ASPxTextBox ID="tbxConnCarrier1" runat="server" Text='<%# Eval("ConnCarrier1") %>' Width="130px">
                                                                                        </dxe:ASPxTextBox>

                                                                                    </td>
                                                                                    <td>to<br>
                                                                                        <dxe:ASPxTextBox ID="tbxConnDest2" runat="server" Text='<%# Eval("ConnDest2") %>' Width="50px">
                                                                                        </dxe:ASPxTextBox>

                                                                                    </td>
                                                                                    <td>by<br>
                                                                                        <dxe:ASPxTextBox ID="tbxConnCarrier2" runat="server" Text='<%# Eval("ConnCarrier2") %>' Width="65px">
                                                                                        </dxe:ASPxTextBox>

                                                                                    </td>
                                                                                    <td>to<br>
                                                                                        <dxe:ASPxTextBox ID="tbxConnDest3" runat="server" Text='<%# Eval("ConnDest3") %>' Width="50px">
                                                                                        </dxe:ASPxTextBox>
    
                                                                                    </td>
                                                                                    <td>by<br>
                                                                                        <dxe:ASPxTextBox ID="tbxConnCarrier3" runat="server" Text='<%# Eval("ConnCarrier3") %>' Width="65px">
                                                                                        </dxe:ASPxTextBox>
    
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td align="center" rowspan="2">Currency </td>
                                                                                    <td align="center" rowspan="2">Chgs Code </td>
                                                                                    <td align="center" colspan="2">WT VAL </td>
                                                                                    <td align="center" colspan="2">Other </td>
                                                                                    <td align="center" rowspan="2">Declared Value for Carriage </td>
                                                                                    <td align="center" rowspan="2">Declared Value for Customs </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>PPD </td>
                                                                                    <td>COLL </td>
                                                                                    <td>PPD </td>
                                                                                    <td>COLL </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxCurrency" runat="server" MaxLength="3" Text='<%# Eval("Currency") %>' Width="50px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxChgsCode" runat="server" Text='<%# Eval("ChgsCode") %>' Width="40px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxPPD1" runat="server" Text='<%# Eval("PPD1") %>' Width="30px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxCOLL1" runat="server" Text='<%# Eval("COLL1") %>' Width="40px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxPPD2" runat="server" Text='<%# Eval("PPD2") %>' Width="40px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxCOLL2" runat="server" Text='<%# Eval("COLL2") %>' Width="40px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxCarriageValue" runat="server" Text='<%# Eval("CarriageValue") %>' Width="80px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxCustomValue" runat="server" Text='<%# Eval("CustomValue") %>' Width="75px">
                                                                                        </dxe:ASPxTextBox>
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
                                                                                        <dxe:ASPxTextBox ID="tbxAirportDestination" runat="server" Text='<%# Eval("AirportDestination") %>' Width="200px">
                                                                                        </dxe:ASPxTextBox>

                                                                                    </td>
                                                                                    <td width="200">Requested Flight/Date<br>
                                                                                        <div style="float: left">
                                                                                            <dxe:ASPxTextBox ID="tbxRequestedFlight" runat="server" Text='<%# Eval("RequestedFlight") %>' Width="90px">
                                                                                            </dxe:ASPxTextBox>
                                                                                        </div>
                                                                                        <div style="float: left; margin-left: 0px;">
                                                                                            <dxe:ASPxTextBox ID="tbxRequestedDate" runat="server" Text='<%# Eval("RequestedDate") %>' Width="90px">
                                                                                            </dxe:ASPxTextBox>
                                                                                        </div>
                                                                                        <br>

                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>Amount of Insurance<br>
                                                                            <dxe:ASPxTextBox ID="tbxAmountInsurance" runat="server" Text='<%# Eval("AmountInsurance") %>'>
                                                                            </dxe:ASPxTextBox>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">Handling Information<br>
                                                                            <dxe:ASPxMemo ID="tbxHandlingInfo" runat="server" Rows="3" Text='<%# Eval("HandlingInfo") %>' TextMode="MultiLine" Width="500px">
                                                                            </dxe:ASPxMemo>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td align="center">No. of<br>Pieces<br>RCP 
                                                                                    </td>
                                                                                    <td align="center">Gross<br>Weight 
                                                                                        <br>
 
                                                                                    </td>
                                                                                    <td align="center">kg<br>lb
                                                                                    </td>
                                                                                    <td align="center">Rate Class </td>
                                                                                    <td align="center">Commodity<br>Item No. 
                                                                                        <br>

                                                                                    </td>
                                                                                    <td align="center">Chargeable<br>Weight 
                                                                                        <br>

                                                                                    </td>
                                                                                    <td align="center">Rate / Charge<br>

                                                                                    </td>
                                                                                    <td align="center">Total<br />
                                                                                        <asp:CheckBox ID="chkShowAmount" runat="server" AutoPostBack="True" Text="Show Amount" />
                                                                                    </td>
                                                                                    <td align="center">Nature and Quantity of Goods<br>(incl. Dimensions or Volume) 
                                                                                        <br>

                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxPiece" runat="server" Text='<%# Eval("Piece") %>' Width="35px">
                                                                                        </dxe:ASPxTextBox>
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
                                                                                        <dxe:ASPxTextBox ID="tbxRateClass" runat="server" Text='<%# Eval("RateClass") %>' Width="50px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxCommodityItemNo" runat="server" Text='<%# Eval("CommodityItemNo") %>' Width="65px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxSpinEdit isplayFormatString="0.0" DecimalPlaces="1" Increment="0" ID="tbxChargeableWeight" Width="65px" runat="server" Text='<%# Eval("ChargeableWeight") %>'>
                                                                                            <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxTextBox ID="tbxRateCharge" runat="server" Text='<%# Eval("RateCharge") %>' Width="65px">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.0" DecimalPlaces="1" Increment="0" ID="tbxTotal" Width="88px" runat="server" Text='<%# Eval("Total") %>'>
                                                                                            <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td align="center" rowspan="2">
                                                                                        <dxe:ASPxMemo ID="tbxGoodsNature" runat="server" Rows="10" Text='<%# Eval("GoodsNature") %>' TextMode="MultiLine" Width="350px">
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="8">
                                                                                        <dxe:ASPxMemo ID="tbxContentRemark" runat="server" Columns="65" Rows="8" Text='<%# Eval("ContentRemark") %>' TextMode="MultiLine">
                                                                                        </dxe:ASPxMemo>
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
                                                                                        <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td align="center"><b>Prepaid</b> </td>
                                                                                                <td align="center">Weight Charge </td>
                                                                                                <td align="center"><b>Collect</b> </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxWeightChargeP" runat="server" Text='<%# Eval("WeightChargeP") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxWeightChargeC" runat="server" Text='<%# Eval("WeightChargeC") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td align="center">Valuation Charge </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxValuationChargeP" runat="server" Text='<%# Eval("ValuationChargeP") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxValuationChargeC" runat="server" Text='<%# Eval("ValuationChargeC") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td align="center">Tax </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxTaxP" runat="server" Text='<%# Eval("TaxP") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxTaxC" runat="server" Text='<%# Eval("TaxC") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td align="center">Total Other Charges Due Agent </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxOtherAgentChargeP" runat="server" Text='<%# Eval("OtherAgentChargeP") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxOtherAgentChargeC" runat="server" Text='<%# Eval("OtherAgentChargeC") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td align="center">Total Other Charges Due Carrier </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxOtherCarrierChargeP" runat="server" Text='<%# Eval("OtherCarrierChargeP") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxOtherCarrierChargeC" runat="server" Text='<%# Eval("OtherCarrierChargeC") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td align="center" width="50%">Total Prepaid </td>
                                                                                                <td align="center">Total Collect </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxTotalPrepaid" runat="server" Text='<%# Eval("TotalPrepaid") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxTotalCollect" runat="server" Text='<%# Eval("TotalCollect") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td align="center" width="50%">Currency Conversion Rates </td>
                                                                                                <td align="center">cc Charges in Dest. Currency </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxCurrencyRate" runat="server" Text='<%# Eval("CurrencyRate") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="tbxChargeDestCurrency" runat="server" Text='<%# Eval("ChargeDestCurrency") %>'>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td>
                                                                            <table border="1" bordercolordark="white" bordercolorlight="#8686ff" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>Other Charges </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td width="100px">Item</td>
                                                                                                <td width="50px">Currency</td>
                                                                                                <td align="right" width="80px">Amount</td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge1" runat="server" Text='<%# Eval("OtherCharge1") %>' Width="100px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td width="80">
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge1Currency" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge1Currency") %>' Width="50px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit ID="tbxOtherCharge1Amount" runat="server" DecimalPlaces="2" DisplayFormatString="0.00" Increment="0" Text='<%# Eval("OtherCharge1Amount") %>' Width="50px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge2" runat="server" Text='<%# Eval("OtherCharge2") %>' Width="100px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td width="80">
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge2Currency" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge2Currency") %>' Width="50px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit ID="tbxOtherCharge2Amount" runat="server" DecimalPlaces="2" DisplayFormatString="0.00" Increment="0" Text='<%# Eval("OtherCharge2Amount") %>' Width="50px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge3" runat="server" Text='<%# Eval("OtherCharge3") %>' Width="100px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td width="80">
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge3Currency" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge3Currency") %>' Width="50px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit ID="tbxOtherCharge3Amount" runat="server" DecimalPlaces="2" DisplayFormatString="0.00" Increment="0" Text='<%# Eval("OtherCharge3Amount") %>' Width="50px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge4" runat="server" Text='<%# Eval("OtherCharge4") %>' Width="100px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td width="80">
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge4Currency" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge4Currency") %>' Width="50px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit ID="tbxOtherCharge4Amount" runat="server" DecimalPlaces="2" DisplayFormatString="0.00" Increment="0" Text='<%# Eval("OtherCharge4Amount") %>' Width="50px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge5" runat="server" Text='<%# Eval("OtherCharge5") %>' Width="100px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td width="80">
                                                                                                    <dxe:ASPxTextBox ID="tbxOtherCharge5Currency" runat="server" MaxLength="3" Text='<%# Eval("OtherCharge5Currency") %>' Width="50px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxSpinEdit ID="tbxOtherCharge5Amount" runat="server" DecimalPlaces="2" DisplayFormatString="0.00" Increment="0" Text='<%# Eval("OtherCharge5Amount") %>' Width="50px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Signature of Shipper or his Agent<br>
                                                                                        <dxe:ASPxTextBox ID="tbxSignatureShipper" runat="server" Text='<%# Eval("SignatureShipper") %>' Width="300px">
                                                                                        </dxe:ASPxTextBox>
                                                                                        <br>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td>Executed On (Date)<br>
                                                                                                    <br>
                                                                                                </td>
                                                                                                <td>at (Place)<br>
                                                                                                    <br>

                                                                                                </td>
                                                                                                <td>Signature of Issuing Carrier or its Agent<br>
                                                                                                    <br>
         
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="tbxExecuteDate" runat="server" Text='<%# Eval("ExecuteDate") %>' Width="120px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="tbxExecutePlace" runat="server" Text='<%# Eval("ExecutePlace") %>' Width="120px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="tbxSignatureIssuing" runat="server" Text='<%# Eval("SignatureIssuing") %>' Width="180px">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 80px;">CreateBy</td>
                                                                        <td style="width: 70px"><%# Eval("CreateBy") %></td>
                                                                        <td style="width: 100px;">CreateDateTime</td>
                                                                        <td style="width: 100px; text-align: center"><%# SafeValue.SafeDateStr( Eval("CreateDateTime")) %></td>
                                                                        <td style="width: 60px;">UpdateBy</td>
                                                                        <td style="width: 120px; text-align: center"><%# Eval("UpdateBy") %></td>
                                                                        <td style="width: 100px;">UpdateDateTime</td>
                                                                        <td style="text-align: center; width: 100px;"><%# SafeValue.SafeDateStr(Eval("UpdateDateTime")) %></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="TPT Info" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_tpt" runat="server">
                                                <table>
                                                    <tr style="border-bottom: solid 1px black">
                                                        <td>Permit
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxMemo runat="server" Rows="3" Width="440" ID="txt_Hbl_Permit" Text='<%# Eval("PermitRmk")%>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Transporter
                                                        </td>
                                                        <td colspan="4">
                                                            <dxe:ASPxButtonEdit ID="txt_Ref_H_Haulier" ClientInstanceName="txt_Ref_H_Haulier" AutoPostBack="False" runat="server" Text='<%# Eval("HaulierName") %>' Width="440">
                                                                <ClientSideEvents ButtonClick="function(s,e){
                                                                     PopupHaulier(txt_Ref_H_Haulier,txt_Ref_H_CrNo,null,txt_Ref_H_Attention);
                                                                    }" />
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>UEN No
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_CrNo" Width="170" ClientInstanceName="txt_Ref_H_CrNo"
                                                                runat="server" Text='<%# Eval("HaulierCrNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Attention
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_Attention" Width="170" ClientInstanceName="txt_Ref_H_Attention"
                                                                runat="server" Text='<%# Eval("HaulierAttention") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
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
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Driver License</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_DriverLicense" Width="170" ClientInstanceName="txt_DriverLicense"
                                                                runat="server" Text='<%# Eval("DriverLicense") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Collect Date</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_Ref_H_CltDate" Width="170" runat="server" Value='<%# Eval("HaulierCollectDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                                                                <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false">
                                                                    <TimeEditProperties EditFormatString="HH:mm">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                    </TimeEditProperties>
                                                                </TimeSectionProperties>
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>VehicleNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_VehicleNo" Width="170" ClientInstanceName="txt_VehicleNo"
                                                                runat="server" Text='<%# Eval("VehicleNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>VehicleType</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_VehicleType" Width="170" ClientInstanceName="txt_VehicleType"
                                                                runat="server" Text='<%# Eval("VehicleType") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Driver Remark</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="me_DriverRemark" Rows="4" Width="440" ClientInstanceName="me_DriverRemark"
                                                                runat="server" Text='<%# Eval("DriverRemark") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton11" runat="server" HorizontalAlign="Left" Width="95" Text="CollectFrom" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupPartyAdr(null,txt_Ref_H_CltFrm,'CV');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td colspan="3" rowspan="2">

                                                            <dxe:ASPxMemo ID="txt_Ref_H_CltFrm" Rows="4" Width="440" ClientInstanceName="txt_Ref_H_CltFrm"
                                                                runat="server" Text='<%# Eval("HaulierCollect") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton6" runat="server" HorizontalAlign="Left" Width="95" Text="TruckTo" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupPartyAdr(null,txt_Ref_H_TruckTo,'CV');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td colspan="3" rowspan="2">
                                                            <dxe:ASPxMemo ID="txt_Ref_H_TruckTo" Rows="4" Width="440" ClientInstanceName="txt_Ref_H_TruckTo"
                                                                runat="server" Text='<%# Eval("HaulierTruck") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Instruction
                                                        </td>
                                                        <td colspan="4">
                                                            <dxe:ASPxMemo Rows="3" ID="txt_Ref_H_Rmk1" Width="440" row runat="server" Text='<%# Eval("HaulierRemark") %>'>
                                                            </dxe:ASPxMemo>
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
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txtPODBy" ClientInstanceName="txtPODBy" Text='<%# Eval("PODBy") %>'
                                                                runat="server" Width="250" HorizontalAlign="Left" AutoPostBack="False">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                        PopupCust(null,txtPODBy);
                                                                                                            }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>PODTime</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_PodTime" Width="100" runat="server" Value='<%# Eval("PODTime") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo Rows="3" ID="me_Remark" Width="440" runat="server" Text='<%# Eval("PODRemark") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
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
                                    <dxtc:TabPage Text="Billing">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Inv" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice_Import, "AI", txtRefNo.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import, "AI", txtRefNo.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import, "AI", txtRefNo.GetText(), txtHouseNo.GetText());
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
                                                        <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import, "AI", txtRefNo.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import, "AI", txtRefNo.GetText(), txtHouseNo.GetText());
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
                                            <dxe:ASPxButton ID="ASPxButton9" Width="150" runat="server" Text="Add Internal Cost" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
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
                                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
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
                                    <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
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
                                                            <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'
                                                                UseSubmitBehavior="false">
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
                                                <Columns>
                                                   <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                 ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
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
                                                                        <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="800" Text='<%# Bind("FileNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                            
                                                                <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' >
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
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
