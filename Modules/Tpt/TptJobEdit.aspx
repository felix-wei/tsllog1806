﻿<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="TptJobEdit.aspx.cs" Inherits="PagesContTrucking_Job_TptJobEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../script/StyleSheet.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Edi.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="/Script/Tpt/page.js"></script>
    <script src="/PagesContTrucking/script/jquery.js"></script>
    <script src="/PagesContTrucking/script/firebase.js"></script>
    <script src="/PagesContTrucking/script/js_company.js"></script>
    <script src="/PagesContTrucking/script/js_firebase.js"></script>
    <style type="text/css">
        .show {
            display: block;
        }

        .hide {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function SelectAll() {
            jQuery("input[id*='ack_IsDel']").each(function () {
                this.click();
            });
        }
    </script>
    <style type="text/css">
        .fee_showhide {
            display: none;
        }

        .add_carpark {
            float: right;
            margin-right: 10px;
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsJob" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJob" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCont" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet1" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsContTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsTransportTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCraneTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <%--<wilson:DataSource ID="dsTripLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmTripLog" KeyMember="Id" FilterExpression="1=0" />--%>
        <wilson:DataSource ID="dsCharge" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobCharge" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsStock" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobStock" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsActivity" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobEventLog" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmAttachment" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Job_Cost" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXSalesman" KeyMember="Code" FilterExpression="" />
        <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="id" FilterExpression="" />
        <wilson:DataSource ID="dsTripCode" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='tripcode'" />
        <wilson:DataSource ID="dsTerminal" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='location' and type1='TERMINAL'" />
        <wilson:DataSource ID="dsCarpark" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='carpark'" />
        <wilson:DataSource ID="dsLogEvent" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmJobEventLog" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsZone" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmParkingZone" KeyMember="id" FilterExpression="" />
        <wilson:DataSource ID="dsWh" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobHouse"
            KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
            KeyMember="Id" FilterExpression="CodeType='2'" />
        <wilson:DataSource ID="dsIncoTerms" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.WhMastData" KeyMember="Id" FilterExpression="Type='IncoTerms'" />
        <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobRate" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="Quotation_Ind='Y'" />
        <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RateType" KeyMember="Id" />
        
        <div>
            <table>
                <tr>
                    <td>Job No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_JobNo" ClientInstanceName="txt_search_JobNo" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Retrieve" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='TptJobEdit.aspx?no='+txt_search_JobNo.GetText();
                    }" />
                        </dxe:ASPxButton>

                    </td>
                    <td style="display: none">
                        <dxe:ASPxButton ID="btn_GoSearch" runat="server" Text="Go Search" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.location='JobList.aspx';}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_job" runat="server" ClientInstanceName="grid_job" KeyFieldName="Id" DataSourceID="dsJob" Width="1000px" AutoGenerateColumns="False" OnInit="grid_job_Init" OnInitNewRow="grid_job_InitNewRow" OnCustomDataCallback="grid_job_CustomDataCallback" OnCustomCallback="grid_job_CustomCallback" OnHtmlEditFormCreated="grid_job_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <Templates>
                    <EditForm>
                        <div style="display: none">
                            <dxe:ASPxLabel ID="lb_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                        </div>
                        <%--<div style="float: right; padding-right: 50px">

                        </div>--%>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="padding: 0px">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Size="Large" Text="Type:"></dxe:ASPxLabel>
                                            </td>
                                            <td>

                                                <dxe:ASPxLabel ID="lbl_JobType" runat="server" Font-Size="Large" Text='<%# Eval("JobType") %>'></dxe:ASPxLabel>
                                                <div style="display: none">
                                                    <dxe:ASPxComboBox ID="cbb_JobType" ClientInstanceName="cbb_JobType" runat="server" Value='<%# Eval("JobType") %>' Width="80">
                                                        <Items>
                                                            <dxe:ListEditItem Text="WGR" Value="WGR" />
                                                            <dxe:ListEditItem Text="WDO" Value="WDO" />
                                                            <dxe:ListEditItem Text="TPT" Value="TPT" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </div>
                                            </td>
                                            <td width="40%"></td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_generate" ClientInstanceName="btn_generate" runat="server" Text="Generate Quotation" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("QuoteStatus"),"USE")=="Pending" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_generate.GetText()+'?')) {
                                                        grid_job.GetValuesOnCustomCallback('GenerateQ',onNoCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_Confirm" ClientInstanceName="btn_Confirm" runat="server" Text="Confirm Quotation" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm(btn_Confirm.GetText()+'?')) {
                                                        grid_job.GetValuesOnCustomCallback('ConfirmQ',onNoCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_Completed" ClientInstanceName="btn_Completed" runat="server" Text="Close Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&(SafeValue.SafeString(Eval("JobStatus"),"USE")!="Voided"&&SafeValue.SafeString(Eval("JobStatus"),"USE")=="Quoted")||(SafeValue.SafeString(Eval("JobStatus"),"USE")=="Confirmed")  %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Close Job ?')) {
                                                        grid_job.GetValuesOnCustomCallback('CompletedQ',onCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_QuoteVoid" ClientInstanceName="btn_QuoteVoid" runat="server" Text="Void Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Void Job?')) {
                                                        grid_job.GetValuesOnCustomCallback('VoidQ',onCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td width="10%"></td>
                                            <%--<td>
                                                <dxe:ASPxButton ID="btn_JobClose" ClientInstanceName="btn_JobClose" runat="server" Text="Close" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_JobClose.GetText()+' it?')) {
                                                    grid_job.GetValuesOnCustomCallback('Close',onCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>--%>
                                            <%--<td>
                                                <dxe:ASPxButton ID="btn_JobAutoBilling" ClientInstanceName="btn_JobAutoBilling" runat="server" Text="Auto Billing" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    grid_job.GetValuesOnCustomCallback('AutoBilling',onCallBack);
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>--%>
                                            <td>
                                                <div class='<%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="btn_PrintDeliveryOrder" runat="server" Text="Print Goods Receipt Note" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintGRN();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                                <div class='<%# SafeValue.SafeString(Eval("JobType"))=="WDO"?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="ASPxButton9" runat="server" Text="Print Delivery Order" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintDO();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                                <div class='<%# SafeValue.SafeString(Eval("JobType"))=="TPT"?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="ASPxButton11" runat="server" Text="Print Transport Instruction" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintTP();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                                <div class='<%# SafeValue.SafeString(Eval("JobType"))=="FRT"?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="ASPxButton16" runat="server" Text="Print Transfer Instruction" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintTR();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="ASPxButton13" runat="server" Text="Print DN" AutoPostBack="false" Width="100">
                                                    <ClientSideEvents Click="function(s,e){
                                                                        PrintDN();
                                                                        }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_PrintJobCost" runat="server" Text="Print Job Cost" AutoPostBack="false" Width="100">
                                                    <ClientSideEvents Click="function(s,e){printJobCost();}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td width="20%"></td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_JobSave" ClientInstanceName="btn_JobSave" runat="server" Text="Save" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    grid_job.PerformCallback('save');
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>

                                            <%--<td>
                                                <dxe:ASPxButton ID="ASPxButton9" ClientInstanceName="btn_JobAutoBilling" runat="server" Text="Create Inv" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    grid_job.GetValuesOnCustomCallback('CreateInv',onCallBack);
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div style="padding: 6px; background-color: #FFFFFF; border: 1px solid #A8A8A8; white-space: nowrap">
                            <table>
                                <tr>
                                    <td class="lbl">Job No</td>
                                    <td class="ctl3">
                                        <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" runat="server" ReadOnly="true" BackColor="Control" Text='<%# Eval("JobNo") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>

                                    <td class="ctl2">
                                        <dxe:ASPxDateEdit ID="txt_JobDate" runat="server" Value='<%# Eval("JobDate") %>' Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>
                                    <td class="lbl2">Status
                                    </td>
                                    <td class="ctl2">
                                        <dxe:ASPxComboBox ID="cmb_JobStatus" ClientInstanceName="cmb_JobStatus" OnCustomJSProperties="cmb_JobStatus_CustomJSProperties" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("JobStatus") %>' Width="100%">
                                            <Items>
                                                <dxe:ListEditItem Text="Booked" Value="Booked" />
                                                <dxe:ListEditItem Text="Quoted" Value="Quoted" />
                                                <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                                <dxe:ListEditItem Text="Completed" Value="Completed" />
                                                <dxe:ListEditItem Text="Voided" Value="Voided" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>

                                    <td class="lbl">Quotation No</td>
                                    <td class="ctl3">
                                        <dxe:ASPxTextBox ID="txt_QuoteNo" ClientInstanceName="txt_QuoteNo" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("QuoteNo") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>

                                    <td class="ctl2">
                                        <dxe:ASPxDateEdit ID="date_QuoteDate" runat="server" Value='<%# Eval("QuoteDate") %>' Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>

                                    <td class="lbl2">Stage</td>
                                    <td class="ctl2">
                                        <dxe:ASPxComboBox ID="cbb_QuoteStatus" runat="server" Value='<%# Eval("QuoteStatus") %>' Width="100%" OnCustomJSProperties="cbb_QuoteStatus_CustomJSProperties">
                                            <Items>
                                                <dxe:ListEditItem Text="None" Value="None" />
                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                <dxe:ListEditItem Text="Quoted" Value="Quoted" />
                                                <dxe:ListEditItem Text="Closed" Value="Closed" />
                                                <dxe:ListEditItem Text="Failed" Value="Failed" />
                                                <dxe:ListEditItem Text="Voided" Value="Voided" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-top: 6px; padding: 10px; background-color: #FFFFFF; border: 1px solid #A8A8A8; white-space: nowrap">
                            <table>
                                <tr>
                                    <td class="lbl">Client</td>
                                    <td class="ctl">
                                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Text='<%# Eval("ClientId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                     <td>
                                        <dxe:ASPxButton ID="ASPxButton23" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="View" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){
                                PopupPartyInfo();
                                }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td class=ctl></td>
                                    <td class="lbl" style="display:none">Sub Client
                                    </td>
                                    <td class="ctl" style="display:none">
                                        <dxe:ASPxButtonEdit ID="btn_SubClientId" ClientInstanceName="btn_SubClientId" runat="server" Text='<%# Eval("SubClientId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParentParty(btn_SubClientId,null,btn_ClientId.GetText());
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td class="lbl2">Master JobNo</td>
                                    <td class="ctl">
                                        <dxe:ASPxTextBox ID="txt_MasterJobNo" ClientInstanceName="txt_MasterJobNo" runat="server" Width="100%" Value='<%# Eval("MasterJobNo") %>'></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl" style="display: none"> <%--Ordered By--%></td>
                                    <td class="ctl" style="display: none">
                                            <dxe:ASPxButtonEdit ID="txt_ClientContact" ClientInstanceName="txt_ClientContact" runat="server" Text='<%# Eval("ClientContact") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                                <Buttons>
                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContact(txt_ClientContact,txt_notifiEmail,btn_ClientId);
                                                                        }" />
                                            </dxe:ASPxButtonEdit>
                                    </td>
                                    <td class="lbl">Client Ref No
                                    </td>
                                    <td class="ctl">
                                        <dxe:ASPxTextBox ID="txt_ClientRefNo" runat="server" Text='<%# Eval("ClientRefNo") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3">
                                        <dxe:ASPxTextBox ID="txt_ClientName"  ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl">Email
                                    </td>
                                    <td class="ctl">
                                        <dxe:ASPxTextBox ID="txt_notifiEmail" ClientInstanceName="txt_notifiEmail" runat="server" Width="100%" Value='<%# Eval("EmailAddress") %>'></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl2">Bill Type</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_BillingType" runat="server" Value='<%# Eval("BillingType") %>' Width="100%">
                                            <Items>
                                                <dxe:ListEditItem Text="None" Value="None" />
                                                <dxe:ListEditItem Text="Job" Value="Job" />
                                                <dxe:ListEditItem Text="Master" Value="Master" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3" >
                                       <dxe:ASPxLabel ForeColor="Red" runat="server" ID="lbl_BillingAlert"></dxe:ASPxLabel>
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td class="lbl2">Trucking</td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cmb_IsTrucking" runat="server" Value='<%# Eval("IsTrucking") %>' Width="60" OnCustomJSProperties="cmb_IsTrucking_CustomJSProperties">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td class="lbl2">Warehouse</td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cmb_IsWarehouse" runat="server" Value='<%# Eval("IsWarehouse") %>' Width="60" >
                                                        <Items>
                                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td class="lbl2">Transport</td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cmb_IsLocal" runat="server" Value='<%# Eval("IsLocal") %>' Width="100" OnCustomJSProperties="cmb_IsLocal_CustomJSProperties">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td class="lbl2">Crane</td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cmb_IsAdhoc" runat="server" Value='<%# Eval("IsAdhoc") %>' Width="100" OnCustomJSProperties="cmb_IsAdhoc_CustomJSProperties">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
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
                                                <td style="width: 100px;"></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                        <hr>
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <table>
                                    <tr style="display: none">
                                        <td colspan="10">
                                            <table style="width: 100%">
                                                <tr>
                                                    
                                                    <td>Freight</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cmb_IsFreight" runat="server" Value='<%# Eval("IsFreight") %>' Width="100">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>                                                   
                                                    <td>Others</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cmb_IsOthers" runat="server" Value='<%# Eval("IsOthers") %>' Width="100">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="980px">
                            <TabPages>
                                <dxtc:TabPage Name="Job" Text="Job">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table>
                                                <%--<tr>
                                                    <td></td>
                                                    <td width="170"></td>
                                                    <td></td>
                                                    <td width="170"></td>
                                                    <td></td>
                                                    <td width="170"></td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td>Job No</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" runat="server" ReadOnly="true" BackColor="Control" Text='<%# Eval("JobNo") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Job Date</td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="txt_JobDate" runat="server" Value='<%# Eval("JobDate") %>' Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                                    </td>
                                                    <td>Job Type</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbb_JobType" runat="server" Value='<%# Eval("JobType") %>' ReadOnly="true" Width="100%">
                                                            <Items>
                                                                <dxe:ListEditItem Text="KD-IMP" Value="KD-IMP" />
                                                                <dxe:ListEditItem Text="KD-EXP" Value="KD-EXP" />
                                                                <dxe:ListEditItem Text="FCL-IMP" Value="FCL-IMP" />
                                                                <dxe:ListEditItem Text="FCL-EXP" Value="FCL-EXP" />
                                                                <dxe:ListEditItem Text="LOCAL" Value="LOCAL" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="lbl">Vessel
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox runat="server" Width="170" ID="txt_Ves" Text='<%# Eval("Vessel")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td class="lbl">Voyage
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox runat="server" Width="170" ID="txt_Voy" Text='<%# Eval("Voyage")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td class="lbl">ETA
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td class="ctl">
                                                                    <dxe:ASPxDateEdit ID="date_Eta" Width="100" runat="server" Value='<%# Eval("EtaDate")%>'
                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_EtaTime" runat="server" Text='<%# Eval("EtaTime") %>' Width="100%">
                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">

                                                    <td>Terminal</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbb_Terminal" runat="server" Value='<%# Eval("Terminalcode") %>' Width="170" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" DataSourceID="dsTerminal" ValueField="Code" TextField="Name"></dxe:ASPxComboBox>
                                                    </td>
                                                    <td>REF No</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_CarrierBkgNo" runat="server" Text='<%# Eval("CarrierBkgNo") %>' Width="170"></dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Operator Code
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_OperatorCode" runat="server" Text='<%# Eval("OperatorCode") %>' Width="170"></dxe:ASPxTextBox>
                                                    </td>
                                                    <%--<td>Etd
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="date_Etd" Width="100" runat="server" Value='<%# Eval("EtdDate")%>'
                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_EtdTime" runat="server" Text='<%# Eval("EtdTime") %>' Width="60">
                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style='display: <%# Eval("IsImport") %>'>COD
                                                    </td>
                                                    <td style='display: <%# Eval("IsImport") %>'>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="date_Cod" Width="100" runat="server" Value='<%# Eval("CodDate")%>'
                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_CodTime" runat="server" Text='<%# Eval("CodTime") %>' Width="60">
                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td class="lbl">Pol
                                                    </td>
                                                    <td class="ctl">
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
                                                    <td class="lbl">Pod
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Width="170" HorizontalAlign="Left" AutoPostBack="False"
                                                            MaxLength="5" Text='<%# Eval("Pod")%>'>
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pod);
                                                                    }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td class="lbl">Shipper
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox ID="txt_WarehouseAddress" runat="server" Text='<%# Eval("WarehouseAddress") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lbl">Escort Ind</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxComboBox ID="cmb_Escort_Ind" runat="server" Width="165" Value='<%# Bind("Escort_Ind") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                <dxe:ListEditItem Value="" Text="" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td class="lbl">Escort Remark  </td>
                                                    <td colspan="3" class="ctl">
                                                        <dxe:ASPxMemo ID="txt_Escort_Remark" Rows="1" ClientInstanceName="txt_Escort_Remark" runat="server" Text='<%# Bind("Escort_Remark") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lbl">Contractor
                                                    </td>
                                                    <td colspan="3" style="padding: 0px;" class="ctl">
                                                        <table>
                                                            <tr>
                                                                <td style="padding: 0px;">
                                                                    <dxe:ASPxButtonEdit ID="btn_HaulierId" ClientInstanceName="btn_HaulierId" runat="server" Text='<%# Eval("HaulierId") %>' Width="168" AutoPostBack="False" ReadOnly="true">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_HaulierId,txt_HaulierName);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td style="padding: 0px;">
                                                                    <dxe:ASPxTextBox ID="txt_HaulierName" ClientInstanceName="txt_HaulierName" runat="server" Width="255" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="lbl">Sub Contractor</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxComboBox ID="cbb_Contractor" runat="server" Value='<%# Eval("Contractor") %>' Width="100%" DropDownStyle="DropDownList">
                                                            <Items>
                                                                <dxe:ListEditItem Value="YES" Text="YES" />
                                                                <dxe:ListEditItem Value="NO" Text="NO" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>

                                                </tr>
                                                <%--<tr>
                                                    <td>Client
                                                    </td>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Text='<%# Eval("ClientId") %>' Width="100" AutoPostBack="False" ReadOnly="true">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="300" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>Client Ref No
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_ClientRefNo" runat="server" Text='<%# Eval("ClientRefNo") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>--%>
                                                <tr style="display: none">
                                                    <td>Carrier
                                                    </td>
                                                    <td colspan="3" style="padding: 0px;">
                                                        <table>
                                                            <tr>
                                                                <td style="padding: 0px;">
                                                                    <dxe:ASPxButtonEdit ID="btn_CarrierId" ClientInstanceName="btn_CarrierId" runat="server" Text='<%# Eval("CarrierId") %>' Width="168" AutoPostBack="False" ReadOnly="true">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_CarrierId,txt_CarrierName);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td style="padding: 0px;">
                                                                    <dxe:ASPxTextBox ID="txt_CarrierName" ClientInstanceName="txt_CarrierName" runat="server" Width="255" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <%--<tr style="display: none">
                                                    <td>CarrierBlNo</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_CarrierBlNo" runat="server" Text='<%# Eval("CarrierBlNo") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td style="vertical-align: top" class="lbl">
                                                        <%--<dxe:ASPxButton ID="btn_PickupFrom" runat="server" Width="100" HorizontalAlign="Left" Text="Pickup From" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                        PopupCustAdr(null,txt_PickupFrom);
                                                                            }" />
                                                                    </dxe:ASPxButton>--%>
                                                        <a href="#" onclick="PopupAddress(null,txt_PickupFrom);">Pick From</a>
                                                    </td>
                                                    <td colspan="2" class="ctl">
                                                        <dxe:ASPxMemo ID="txt_PickupFrom" Rows="4" ClientInstanceName="txt_PickupFrom" runat="server"
                                                            Width="250" Text='<%# Eval("PickupFrom") %>'>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                    <td style="vertical-align: top" class="lbl">
                                                        <%--<dxe:ASPxButton ID="btn_DeliveryTo" runat="server" Width="85" HorizontalAlign="Left" Text="Delivery To" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                        PopupCustAdr(null,txt_DeliveryTo);
                                                                            }" />
                                                                    </dxe:ASPxButton>--%>
                                                        <a href="#" onclick="PopupAddress(null,txt_DeliveryTo);">Delivery To</a>
                                                    </td>
                                                    <td colspan="2" class="ctl">
                                                        <dxe:ASPxMemo ID="txt_DeliveryTo" Rows="4" ClientInstanceName="txt_DeliveryTo" runat="server"
                                                            Width="100%" Text='<%# Eval("DeliveryTo") %>'>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                                <td style="vertical-align: top">Port Address</td>
                                                                <td>
                                                                    <dxe:ASPxMemo runat="server" Rows="4" ID="txt_PortnetRef" Value='<%# Eval("PortnetRef") %>' Width="100%"></dxe:ASPxMemo>
                                                                </td>
                                                                <td style="vertical-align: top">
                                                                    <a href="#" onclick="PopupCustAdr(null,txt_WarehouseAddress);">Warehouse&nbsp;Address</a>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_WarehouseAddress" ClientInstanceName="txt_WarehouseAddress" Rows="4" runat="server" Text='<%# Eval("WarehouseAddress") %>' Width="250">
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>--%>
                                                <tr>
                                                    <td style="vertical-align: top" class="lbl">
                                                        <a href="#" onclick="PopupAddress(null,txt_YardRef);">Depot Address</a>
                                                    </td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo runat="server" Rows="4" ID="txt_YardRef" ClientInstanceName="txt_YardRef" Value='<%# Eval("YardRef") %>' Width="250"></dxe:ASPxMemo>
                                                    </td>
                                                    <td style="vertical-align: top" class="lbl">Special Instruction</td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_SpecialInstruction" Rows="4" runat="server" Text='<%# Eval("SpecialInstruction") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td style="vertical-align: top" class="lbl">Remark</td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_Remark" Rows="4" runat="server" Text='<%# Eval("Remark") %>' Width="250">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                    <td style="vertical-align: top" class="lbl">Permit No</td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_PermitNo" Rows="4" runat="server" Text='<%# Eval("PermitNo") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                            </table>


                                           
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                 <dxtc:TabPage Text="Container" Name="Container">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton24" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Add Container" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                grid_Cont.AddNewRow();
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton25" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Batch Add" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){container_batch_add();}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <dxwgv:ASPxGridView ID="grid_Cont" ClientInstanceName="grid_Cont" runat="server" KeyFieldName="Id" DataSourceID="dsCont" Width="1000px" AutoGenerateColumns="False" OnInit="grid_Cont_Init" OnBeforePerformDataSelect="grid_Cont_BeforePerformDataSelect" OnRowDeleting="grid_Cont_RowDeleting" OnInitNewRow="grid_Cont_InitNewRow" OnRowInserting="grid_Cont_RowInserting" OnRowUpdating="grid_Cont_RowUpdating" OnRowDeleted="grid_Cont_RowDeleted" OnRowUpdated="grid_Cont_RowUpdated" OnRowInserted="grid_Cont_RowInserted" OnCustomDataCallback="grid_Cont_CustomDataCallback">
                                                    <SettingsPager PageSize="100" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="12%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='<%# "grid_Cont.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                                <%--<a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Cont.DeleteRow("+Container.VisibleIndex+");"  %>}' style='display: <%# Eval("canChange")%>'>Delete</a>--%>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="11" Width="12%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Cont.DeleteRow("+Container.VisibleIndex+");"  %>}' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="1" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="SealNo" Caption="Seal No"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ContainerType" Caption="ContType"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="PortnetStatus" Caption="PortnetStatus"></dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="CfsInDate" Caption="Truck In Date"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="CfsOutDate" Caption="Truck Out Date"></dxwgv:GridViewDataColumn>--%>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="Permit" Caption="Permit"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="F5Ind" Caption="DG/J5"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="8" FieldName="Weight" Caption="Weight"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="8" FieldName="Weight" Caption="Over/Wt">
                                                            <DataItemTemplate>
                                                                <%# S.SafeDecimal(Eval("Weight"))>= S.SafeDecimal("21000") ? "Y" : ""%>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="9" FieldName="YardAddress" Caption="Depot Address"></dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="YardExpiryDate" Caption="Depot Expiry Date"></dxwgv:GridViewDataColumn>--%>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="PortnetStatus" Caption="Portnet Status" ></dxwgv:GridViewDataColumn>--%>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="StatusCode" Caption="Status"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="Weight" Caption="Weight"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="Volume" Caption="Volume"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="Qty" Caption="Qty"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="8" FieldName="PackageType" Caption="PackageType"></dxwgv:GridViewDataColumn>--%>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="lbl">ContainerNo</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ContainerNo") %>' AutoPostBack="False" Width="165">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo,txt_ContType);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td class="lbl">SealNo</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_SealNo" runat="server" Text='<%# Bind("SealNo") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">ContType</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContainerType") %>'></dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">OOG Ind
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_oogInd" runat="server" Text='<%# Bind("oogInd") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="lbl">Discharge Cell
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_dischargeCell" ClientInstanceName="txt_dischargeCell" runat="server" Text='<%# Bind("DischargeCell") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">B/R</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_BR" ClientInstanceName="txt_BR" runat="server" Text='<%# Bind("Br") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">Weight
                                                                    </td>
                                                                    <td  class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_Wt" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">Volume
                                                                    </td>
                                                                    <td  class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_M3" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">Qty</td>
                                                                    <td  class="ctl">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit runat="server" Width="70"
                                                                                        ID="spin_Pkgs" Height="21px" Value='<%# Bind("Qty")%>' NumberType="Integer" Increment="0" DisplayFormatString="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButtonEdit ID="txt_PkgsType" ClientInstanceName="txt_PkgsType" runat="server"
                                                                                        Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_PkgsType,2);
                                                                    }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <%--<td>Reqeust Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_Request" runat="server" Width="165" Value='<%# Bind("RequestDate")%>'
                                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>--%>
                                                                    <td class="lbl">Trucking/Schedule Date</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxDateEdit ID="date_Cont_Schedule" runat="server" Width="165" Value='<%# Bind("ScheduleDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">Dg Class</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="txt_DgClass" Text='<%# Bind("DgClass")%>'></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">TruckingStatus</td>
                                                                    <td class="ctl">
                                                                             <dxe:ASPxComboBox ID="cbb_StatusCode" ClientInstanceName="cbb_StatusCode" runat="server" Width="165" Value='<%# Bind("StatusCode") %>' >
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="New" Text="New" />
                                                                                <dxe:ListEditItem Value="Collection" Text="Collection" />
                                                                                <dxe:ListEditItem Value="Import" Text="Import" />
                                                                                <dxe:ListEditItem Value="WHS-MT" Text="WHS-MT" />
                                                                                <dxe:ListEditItem Value="WHS-LD" Text="WHS-LD" />
                                                                                <dxe:ListEditItem Value="Customer-MT" Text="Customer-MT" />
                                                                                <dxe:ListEditItem Value="Customer-LD" Text="Customer-LD" />
                                                                                <dxe:ListEditItem Value="Return" Text="Return" />
                                                                                <dxe:ListEditItem Value="Export" Text="Export" />
                                                                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                                <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>

                                                                       
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                    <td>Truck In</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_CfsIn" runat="server" Width="165" Value='<%# Bind("CfsInDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Truck Out</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_CfsOut" runat="server" Width="165" Value='<%# Bind("CfsOutDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                </tr>--%>
                                                                <%--<tr>
                                                                    <td>Yard Pickup</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_YardPickup" runat="server" Width="165" Value='<%# Bind("YardPickupDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Yard Return</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_YardReturn" runat="server" Width="165" Value='<%# Bind("YardReturnDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                </tr>--%>
                                                                <tr>
                                                                <%--<td>DG / F5</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Width="165" Value='<%# Bind("F5Ind") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>--%>
                                                                    <td class="lbl">Permit</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_Permit" runat="server" Width="165" Value='<%# Bind("Permit") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td style="display: none">TT Time</td>
                                                                    <td style="display: none">>
                                                                        <dxe:ASPxTextBox ID="txt_TTTime" runat="server" Text='<%# Bind("TTTime") %>' Width="165">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">Urgent Job</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_UrgentInd" runat="server" Width="165" Value='<%# Bind("UrgentInd") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="lbl">PortnetStatus</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="ASPxComboBox1" runat="server" Value='<%# Bind("PortnetStatus") %>' DropDownStyle="DropDown" Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="" Text="" />
                                                                                <dxe:ListEditItem Value="Created" Text="Created" />
                                                                                <dxe:ListEditItem Value="Released" Text="Released" />
                                                                                <%--<dxe:ListEditItem Value="NOT CREATED" Text="NOT CREATED" />--%>
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <%--<td>YardExpiry
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="date_YardExpiry" Width="100" runat="server" Value='<%# Bind("YardExpiryDate")%>'
                                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_YardExpiryTime" runat="server" Text='<%# Bind("YardExpiryTime") %>' Width="60">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style='display: <%# Eval("IsImport") %>'>D.T.
                                                                    </td>
                                                                    <td style='display: <%# Eval("IsImport") %>'>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="date_Cdt" Width="100" runat="server" Value='<%# Bind("CdtDate")%>'
                                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_CdtTime" runat="server" Text='<%# Bind("CdtTime") %>' Width="60">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>--%>
                                                                    <td class="lbl">TerminalLocation</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_TerminalLocation" ClientInstanceName="txt_TerminalLocation" runat="server" Text='<%# Bind("TerminalLocation") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">MT/LDN</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_warehouse_status" runat="server" Text='<%# Bind("WarehouseStatus") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Empty" Text="Empty" />
                                                                                <dxe:ListEditItem Value="Laden" Text="Laden" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="lbl">Direct ?</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_ContainerCategory" runat="server" Text='<%# Bind("ContainerCategory") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Normal" Text="Normal" />
                                                                                <dxe:ListEditItem Value="Direct Loading" Text="Direct Loading" />
                                                                                <dxe:ListEditItem Value="Direct Delivery" Text="Direct Delivery" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>                       
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">DG / J5</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Width="165" Value='<%# Bind("F5Ind") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="lbl">Email Sent</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_EmailInd" runat="server" Width="165" Value='<%# Bind("EmailInd") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">ScheduleStartDate
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxDateEdit ID="date_ScheduleStartDate" runat="server" Width="100%" Value='<%# Bind("ScheduleStartDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">Time</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="date_ScheduleStartTime" runat="server" Text='<%# Bind("ScheduleStartTime") %>' Width="100%">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">WarehouseStatus</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_CfsStatus" runat="server" Text='<%# Bind("CfsStatus") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Pending" Text="Pending" />
                                                                                <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                                                <dxe:ListEditItem Value="Started" Text="Started" />
                                                                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">CompletionDate
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxDateEdit ID="date_CompletionDate" runat="server" Width="100%" Value='<%# Bind("CompletionDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">Time</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_CompletionTime" runat="server" Text='<%# Bind("CompletionTime") %>' Width="100%">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                    <td>TerminalLocation</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox ID="txt_TerminalLocation" ClientInstanceName="txt_TerminalLocation" runat="server" Text='<%# Bind("TerminalLocation") %>' Width="100%"></dxe:ASPxTextBox>

                                                                        <dxe:ASPxMemo ID="txt_TerminalLocation" Rows="1" runat="server" Text='<%# Bind("TerminalLocation") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>--%>
                                                                <tr>
                                                                    <td style="vertical-align: central" class="lbl">
                                                                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress);">Depot Address</a>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <%--<dxe:ASPxTextBox ID="txt_YardAddress" runat="server" ClientInstanceName="txt_YardAddress" Text='<%# Bind("YardAddress") %>' Width="100%"></dxe:ASPxTextBox>--%>
                                                                        <dxe:ASPxMemo ID="txt_YardAddress" ClientInstanceName="txt_YardAddress" Rows="3" runat="server" Text='<%# Bind("YardAddress") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">Remark</td>
                                                                    <td colspan="3">
                                                                        <%--<dxe:ASPxTextBox ID="txt_ContRemark" runat="server" Text='<%# Bind("Remark") %>' Width="100%"></dxe:ASPxTextBox>--%>
                                                                        <dxe:ASPxMemo ID="txt_ContRemark" Rows="3" runat="server" Text='<%# Bind("Remark") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;"><b>TRIP:</b>
                                                                        <%--<dxe:ASPxButton ID="btn_cont_trailerCP" runat="server" Text="Trailer Carpark" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park3',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>
                                                                        <dxe:ASPxButton ID="btn_cont_afterWH" runat="server" Text="After WH" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park2',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>
                                                                        <dxe:ASPxButton ID="btn_cont_beforeWH" runat="server" Text="Before WH" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park1',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>--%>
                                                                        <dxe:ASPxButton ID="btn_cont_newTrip" runat="server" Text="New Trip" CssClass="add_carpark" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('AddNew',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <dxwgv:ASPxGridView ID="gv_cont_trip" ClientInstanceName="gv_cont_trip" runat="server" AutoGenerateColumns="false" Width="930" OnBeforePerformDataSelect="gv_cont_trip_BeforePerformDataSelect" DataSourceID="dsContTrip" KeyFieldName="Id" OnRowUpdating="gv_cont_trip_RowUpdating" OnRowUpdated="gv_cont_trip_RowUpdated" OnCustomDataCallback="gv_cont_trip_CustomDataCallback" OnHtmlEditFormCreated="gv_cont_trip_HtmlEditFormCreated">
                                                                <SettingsPager PageSize="100" />
                                                                <SettingsEditing Mode="EditForm" />
                                                                <SettingsBehavior ConfirmDelete="true" />
                                                                <Columns>
                                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0">
                                                                        <DataItemTemplate>
                                                                            <a href="#" onclick='<%# "gv_cont_trip.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>

                                                                        </DataItemTemplate>
                                                                    </dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="12">
                                                                        <DataItemTemplate>
                                                                            <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "gv_cont_trip.GetValuesOnCustomCallback(\"Delete_"+Container.KeyValue+"\",container_trip_delete_cb);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                                        </DataItemTemplate>
                                                                    </dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="Statuscode" Caption="Status">
                                                                        <DataItemTemplate>
                                                                            <%# change_StatusShortCode_ToCode(Eval("Statuscode")) %>
                                                                        </DataItemTemplate>
                                                                    </dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="TripCode" Caption="Trip Type"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToCode" Caption="Destination"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToParkingLot" Caption="ParkingLot"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataDateColumn VisibleIndex="9" Width="100" FieldName="FromDate" Caption="Date">
                                                                        <DataItemTemplate><%# SafeValue.SafeDate( Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("FromTime") %></DataItemTemplate>
                                                                    </dxwgv:GridViewDataDateColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="DoubleMounting" Caption="Double Mounting"></dxwgv:GridViewDataColumn>
                                                                </Columns>

                                                                <Templates>
                                                                    <EditForm>
                                                                        <div style="display: none">
                                                                            <dxe:ASPxLabel ID="lb_tripId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                            <dxe:ASPxTextBox ID="dde_Trip_ContId" ClientInstanceName="dde_Trip_ContId" runat="server" Text='<%# Bind("Det1Id") %>'></dxe:ASPxTextBox>
                                                                        </div>
                                                                        <table>
                                                                            <tr>
                                                                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Trip Detail</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Trip Type</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Width="165" DropDownStyle="DropDown">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Text="IMP" Value="IMP" />
                                                                                            <dxe:ListEditItem Text="EXP" Value="EXP" />
                                                                                            <dxe:ListEditItem Text="COL" Value="COL" />
                                                                                            <dxe:ListEditItem Text="RET" Value="RET" />
                                                                                            <dxe:ListEditItem Text="LOC" Value="LOC" />
                                                                                            <dxe:ListEditItem Text="SHF" Value="SHF" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td  class="lbl1">
                                                                                    Trailer Type 
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_TrailerType" ClientInstanceName="txt_TrailerType" runat="server" Text='<%# Bind("RequestTrailerType") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td class="lbl">Driver</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="165">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,btn_TowheadCode);
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">PrimeMover</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxButtonEdit ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheadCode") %>' AutoPostBack="False" Width="165">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(btn_TowheadCode,null);
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                                <td  class="lbl1">
                                                                                    Vehicle Type 
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_VehicleType" ClientInstanceName="txt_VehicleType" runat="server" Text='<%# Bind("RequestVehicleType") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>

                                                                                <td class="lbl">Status</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                                                                        <Items>
                                                                                            <%--<dxe:ListEditItem Value="U" Text="Use" />--%>
                                                                                            <dxe:ListEditItem Value="S" Text="Start" />
                                                                                            <%--<dxe:ListEditItem Value="D" Text="Doing" />
                                                                                <dxe:ListEditItem Value="W" Text="Waiting" />--%>
                                                                                            <dxe:ListEditItem Value="P" Text="Pending" />
                                                                                            <dxe:ListEditItem Value="C" Text="Completed" />
                                                                                            <dxe:ListEditItem Value="X" Text="Cancel" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Double Mounting</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cmb_DoubleMounting" runat="server" Value='<%# Bind("DoubleMounting") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                            <dxe:ListEditItem Value="No" Text="No" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>

                                                                                <td class="lbl">ScheduleDate</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxDateEdit ID="date_BookingDate" runat="server" Value='<%# Bind("BookingDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td class="lbl">Time</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_BookingTime" runat="server" Text='<%# Bind("BookingTime") %>' Width="162">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">From</td>
                                                                                <td colspan="3">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td  class="lbl1">
                                                                                    <a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_FromPL" ClientInstanceName="txt_FromPL" runat="server" Text='<%# Bind("FromParkingLot") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">From Date</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxDateEdit ID="txt_FromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td class="lbl">Time</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="161">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">To</td>
                                                                                <td colspan="3" class="ctl2">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td class="lbl">
                                                                                    <%--<a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>--%>
                                                                                    <a href="#" onclick="PopupParkingLot(txt_ToPL,txt_Trip_ToCode);">To Parking Lot</a>
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_ToPL" ClientInstanceName="txt_ToPL" runat="server" Text='<%# Bind("ToParkingLot") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">ToDate</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td  class="lbl">Time</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="161">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td class="lbl">Escort Ind</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cmb_Escort_Ind" runat="server" Width="165" Value='<%# Bind("Escort_Ind") %>'>
                                                                                        <Items>
																						    <dxe:ListEditItem Value="" Text="" />
                                                                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                            
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Instruction</td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td class="lbl">Escort Remark  </td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_Escort_Remark" ClientInstanceName="txt_Trip_Escort_Remark" runat="server" Text='<%# Bind("Escort_Remark") %>' Width="165">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Contractor
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxButtonEdit ID="btn_SubCon_Code" ClientInstanceName="btn_SubCon_Code" runat="server" Text='<%# Bind("SubCon_Code") %>' Width="165" AutoPostBack="False" ReadOnly="true">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_SubCon_Code,null);
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                                <td class="lbl">Sub-Contract</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_SubCon_Code" ClientInstanceName="cbb_SubCon_Code" runat="server" Value='<%# Bind("SubCon_Ind") %>' Width="100%" DropDownStyle="DropDownList">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="Y" Text="YES" />
                                                                                            <dxe:ListEditItem Value="N" Text="NO" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>

                                                                                </td>
                                                                                <td class="lbl">Self Ind</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_Self_Ind" runat="server" Value='<%# Bind("Self_Ind") %>' Width="100%" DropDownStyle="DropDownList">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="YES" Text="YES" />
                                                                                            <dxe:ListEditItem Value="NO" Text="NO" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Input</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Container
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxDropDownEdit ID="dde_Trip_ContNo" runat="server" ClientInstanceName="dde_Trip_ContNo"
                                                                                        Text='<%# Bind("ContainerNo") %>' Width="165" AllowUserInput="false">
                                                                                        <DropDownWindowTemplate>
                                                                                            <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridPopCont"
                                                                                                Width="300px" KeyFieldName="Id" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="0">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="1">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                </Columns>
                                                                                                <ClientSideEvents RowClick="RowClickHandler" />
                                                                                            </dxwgv:ASPxGridView>
                                                                                        </DropDownWindowTemplate>
                                                                                    </dxe:ASPxDropDownEdit>
                                                                                </td>
                                                                                <td class="lbl">Trailer</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxButtonEdit ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="165">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_MasterData(btn_ChessisCode,null,'Chessis');
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                                <td class="lbl">Zone</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_zone" runat="server" Value='<%# Bind("ParkingZone") %>' Width="165" DataSourceID="dsZone" TextField="Code" ValueField="Code">
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="lbl">DriverRemark</td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <%--<tr>

                                                                                <td>Incentive</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive1" Height="21px" Value='<%# Bind("Incentive1")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>Overtime</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive2" Height="21px" Value='<%# Bind("Incentive2")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>Standby</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive3" Height="21px" Value='<%# Bind("Incentive3")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>PSA ALLOWANCE</td>
                                                                                <td>
                                                                                    <dxe:ASPxComboBox ID="cbb_Incentive4" runat="server" Value='<%# Bind("Incentive4") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="0" Text="0" />
                                                                                            <dxe:ListEditItem Value="5" Text="5" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Surcharge</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>DHC</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge1" Height="21px" Value='<%# Bind("Charge1")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>WEIGHING</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge2" Height="21px" Value='<%# Bind("Charge2")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>WASHING</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge3" Height="21px" Value='<%# Bind("Charge3")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>REPAIR</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge4" Height="21px" Value='<%# Bind("Charge4")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>DETENTION</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge5" Height="21px" Value='<%# Bind("Charge5")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>DEMURRAGE</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge6" Height="21px" Value='<%# Bind("Charge6")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>LIFT ON/OFF</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge7" Height="21px" Value='<%# Bind("Charge7")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>C/SHIPMENT</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge8" Height="21px" Value='<%# Bind("Charge8")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>EMF</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge10" Height="21px" Value='<%# Bind("Charge10")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4"></td>
                                                                                <td>OTHER</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge9" Height="21px" Value='<%# Bind("Charge9")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>--%>

                                                                            <tr>
                                                                                <td colspan="6">
                                                                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                                        <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                                        </span>
                                                                                        <span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                            <%--<dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>--%>
                                                                                            <a onclick="gv_cont_trip.GetValuesOnCustomCallback('Update',container_trip_update_cb);"><u>Update</u></a>
                                                                                        </span>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EditForm>
                                                                </Templates>
                                                            </dxwgv:ASPxGridView>

                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="12">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>
                                                                            <%--<span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>--%>
                                                                            <span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                <a onclick="grid_Cont.GetValuesOnCustomCallback('save',container_batch_add_cb);" href="#">Update</a>
                                                                            </span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Transport" Name="Transport">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <table>
                                                    <%--style="display:<%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none" %>"--%>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_ContAdd" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Add Trailer" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                grid_Cont_Tpt.AddNewRow();
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td style="display:none">
                                                            <dxe:ASPxButton ID="btn_ContBatchAdd" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Batch Add" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){container_batch_add();}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <dxwgv:ASPxGridView ID="grid_Cont_Tpt" ClientInstanceName="grid_Cont_Tpt" runat="server" KeyFieldName="Id" DataSourceID="dsCont" Width="1000px" AutoGenerateColumns="False" OnInit="grid_Cont_Tpt_Init" OnBeforePerformDataSelect="grid_Cont_Tpt_BeforePerformDataSelect" OnRowDeleting="grid_Cont_Tpt_RowDeleting" OnInitNewRow="grid_Cont_Tpt_InitNewRow" OnRowInserting="grid_Cont_Tpt_RowInserting" OnRowUpdating="grid_Cont_Tpt_RowUpdating" OnRowDeleted="grid_Cont_Tpt_RowDeleted" OnRowUpdated="grid_Cont_Tpt_RowUpdated" OnRowInserted="grid_Cont_Tpt_RowInserted" OnCustomDataCallback="grid_Cont_Tpt_CustomDataCallback">
                                                    <SettingsPager PageSize="100" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="12%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='<%# "grid_Cont_Tpt.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                                <%--<a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Cont_Tpt.DeleteRow("+Container.VisibleIndex+");"  %>}' style='display: <%# Eval("canChange")%>'>Delete</a>--%>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="11" Width="12%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Cont_Tpt.DeleteRow("+Container.VisibleIndex+");"  %>}' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="1" FieldName="ContainerNo" Caption="Trailer No"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="SealNo" Caption="Seal No" Visible="false"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ContainerType" Caption="Type" Visible="false"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="StatusCode" Caption="Status"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="ScheduleDate" Caption="Schedule Date">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="ScheduleStartDate" Caption="Start Date">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeDateStr(Eval("ScheduleStartDate")) %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="CompletionDate" Caption="End Date">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeDateStr(Eval("CompletionDate")) %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="CfsInDate" Caption="Truck In Date"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="CfsOutDate" Caption="Truck Out Date"></dxwgv:GridViewDataColumn>--%>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="Remark" Caption="Remark"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="F5Ind" Caption="DG/J5" Visible="false"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="8" FieldName="Weight" Caption="Weight" Visible="false"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="8" FieldName="Weight" Caption="Over/Wt" Visible="false">
                                                            <DataItemTemplate>
                                                                <%# S.SafeDecimal(Eval("Weight"))>= S.SafeDecimal("21000") ? "Y" : ""%>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="9" FieldName="YardAddress" Caption="Depot Address" Visible="false"></dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="YardExpiryDate" Caption="Depot Expiry Date"></dxwgv:GridViewDataColumn>--%>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="PortnetStatus" Caption="Portnet Status" ></dxwgv:GridViewDataColumn>--%>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="StatusCode" Caption="Status"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="Weight" Caption="Weight"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="Volume" Caption="Volume"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="Qty" Caption="Qty"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="8" FieldName="PackageType" Caption="PackageType"></dxwgv:GridViewDataColumn>--%>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="lbl">Trailer No</td>
                                                                    <td class="ctl">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ContainerNo") %>' AutoPostBack="False" Width="135">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                PopupCTM_MasterData(btn_ContNo,null,'Chessis');
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                                <td class="lbl">Type</td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_VehicleType" ClientInstanceName="txt_VehicleType" runat="server" Width="80" Text='<%# Bind("VehicleType") %>'></dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </td>
                                                                    <td class="lbl">Trucking/Schedule Date</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxDateEdit ID="date_Cont_Schedule" runat="server" Width="165" Value='<%# Bind("ScheduleDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">TruckingStatus</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_StatusCode" ClientInstanceName="cbb_StatusCode" runat="server" Width="165" Value='<%# Bind("StatusCode") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="New" Text="New" />
                                                                                <dxe:ListEditItem Value="Start" Text="Start" />
                                                                                <dxe:ListEditItem Value="Arrival" Text="Arrival" />
                                                                                <dxe:ListEditItem Value="LCL-LD" Text="LCL-LD" />
                                                                                <dxe:ListEditItem Value="LCL-MT" Text="LCL-MT" />
                                                                                <dxe:ListEditItem Value="Depart" Text="Depart" />
                                                                                <dxe:ListEditItem Value="Delivered" Text="Delivered" />
                                                                                <dxe:ListEditItem Value="Return" Text="Returned" />
                                                                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                                <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">ScheduleStartDate
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxDateEdit ID="date_ScheduleStartDate" runat="server" Width="100%" Value='<%# Bind("ScheduleStartDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">Time</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="date_ScheduleStartTime" runat="server" Text='<%# Bind("ScheduleStartTime") %>' Width="100%">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">WarehouseStatus</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_CfsStatus" runat="server" Text='<%# Bind("CfsStatus") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Pending" Text="Pending" />
                                                                                <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                                                <dxe:ListEditItem Value="Started" Text="Started" />
                                                                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">CompletionDate
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxDateEdit ID="date_CompletionDate" runat="server" Width="100%" Value='<%# Bind("CompletionDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">Time</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_CompletionTime" runat="server" Text='<%# Bind("CompletionTime") %>' Width="100%">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">Direct ?</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_ContainerCategory" runat="server" Text='<%# Bind("ContainerCategory") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Normal" Text="Normal" />
                                                                                <dxe:ListEditItem Value="Local Direct" Text="Local Direct" />
               
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td> 
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">Remark</td>
                                                                    <td colspan="3">
                                                                        <%--<dxe:ASPxTextBox ID="txt_ContRemark" runat="server" Text='<%# Bind("Remark") %>' Width="100%"></dxe:ASPxTextBox>--%>
                                                                        <dxe:ASPxMemo ID="txt_ContRemark" Rows="3" runat="server" Text='<%# Bind("Remark") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                     <td class="lbl"></td>
                                                                    <td class="ctl">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;"><b>TRIP:</b>
                                                                        <%--<dxe:ASPxButton ID="btn_cont_trailerCP" runat="server" Text="Trailer Carpark" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park3',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>
                                                                        <dxe:ASPxButton ID="btn_cont_afterWH" runat="server" Text="After WH" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park2',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>
                                                                        <dxe:ASPxButton ID="btn_cont_beforeWH" runat="server" Text="Before WH" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park1',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>--%>
                                                                        <%-- <dxe:ASPxButton ID="btn_cont_newTrip" runat="server" Text="New Trip" CssClass="add_carpark" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('AddNew',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <dxwgv:ASPxGridView ID="gv_tpt_trip" ClientInstanceName="gv_tpt_trip" runat="server" AutoGenerateColumns="false" Width="930" OnBeforePerformDataSelect="gv_tpt_trip_BeforePerformDataSelect" DataSourceID="dsContTrip" KeyFieldName="Id" OnRowUpdating="gv_tpt_trip_RowUpdating" OnRowUpdated="gv_tpt_trip_RowUpdated" OnCustomDataCallback="gv_tpt_trip_CustomDataCallback" OnHtmlEditFormCreated="gv_tpt_trip_HtmlEditFormCreated">
                                                                <SettingsPager PageSize="100" />
                                                                <SettingsEditing Mode="EditForm" />
                                                                <SettingsBehavior ConfirmDelete="true" />
                                                                <Columns>
                                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0">
                                                                        <DataItemTemplate>
                                                                            <a href="#" onclick='<%# "gv_tpt_trip.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>

                                                                        </DataItemTemplate>
                                                                    </dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="12" Visible="false">
                                                                        <DataItemTemplate>
                                                                            <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "gv_tpt_trip.GetValuesOnCustomCallback(\"Delete_"+Container.KeyValue+"\",transport_trip_delete_cb);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                                        </DataItemTemplate>
                                                                    </dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Trailer No"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="Statuscode" Caption="Status">
                                                                        <DataItemTemplate>
                                                                            <%# change_StatusShortCode_ToCode(Eval("Statuscode")) %>
                                                                        </DataItemTemplate>
                                                                    </dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="TripCode" Caption="Trip Type"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToCode" Caption="Destination"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToParkingLot" Caption="ParkingLot"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="ChessisCode" Caption="Trailer" Visible="false"></dxwgv:GridViewDataColumn>
                                                                    <dxwgv:GridViewDataDateColumn VisibleIndex="9" Width="100" FieldName="FromDate" Caption="Date">
                                                                        <DataItemTemplate><%# SafeValue.SafeDate( Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("FromTime") %></DataItemTemplate>
                                                                    </dxwgv:GridViewDataDateColumn>
                                                                    <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="DoubleMounting" Caption="Double Mounting"></dxwgv:GridViewDataColumn>
                                                                </Columns>

                                                                <Templates>
                                                                    <EditForm>
                                                                        <div style="display: none">
                                                                            <dxe:ASPxLabel ID="lb_tripId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                            <dxe:ASPxTextBox ID="dde_Trip_ContId" ClientInstanceName="dde_Trip_ContId" runat="server" Text='<%# Bind("Det1Id") %>'></dxe:ASPxTextBox>
                                                                        </div>
                                                                        <table>
                                                                            <tr>
                                                                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Trip Detail</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Trip Type</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Width="165" DropDownStyle="DropDown">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Text="IMP" Value="IMP" />
                                                                                            <dxe:ListEditItem Text="EXP" Value="EXP" />
                                                                                            <dxe:ListEditItem Text="COL" Value="COL" />
                                                                                            <dxe:ListEditItem Text="RET" Value="RET" />
                                                                                            <dxe:ListEditItem Text="LOC" Value="LOC" />
                                                                                            <dxe:ListEditItem Text="SHF" Value="SHF" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                 <td  class="lbl1">
                                                                                    Trailer Type 
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_TrailerType" ClientInstanceName="txt_TrailerType" runat="server" Text='<%# Bind("RequestTrailerType") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                
                                                                                <td class="lbl">Driver</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="165">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,btn_TowheadCode);
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">PrimeMover</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxButtonEdit ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheadCode") %>' AutoPostBack="False" Width="165">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(btn_TowheadCode,null);
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                            <td  class="lbl1">
                                                                                    Vehicle Type 
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_VehicleType" ClientInstanceName="txt_VehicleType" runat="server" Text='<%# Bind("RequestVehicleType") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td class="lbl">Status</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="P" Text="Pending" />
                                                                                            <dxe:ListEditItem Value="C" Text="Completed" />
                                                                                            <dxe:ListEditItem Value="X" Text="Cancel" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Double Mounting</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cmb_DoubleMounting" runat="server" Value='<%# Bind("DoubleMounting") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                            <dxe:ListEditItem Value="No" Text="No" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td class="lbl">ScheduleDate</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxDateEdit ID="date_BookingDate" runat="server" Value='<%# Bind("BookingDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td class="lbl">Time</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_BookingTime" runat="server" Text='<%# Bind("BookingTime") %>' Width="162">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Agent</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxButtonEdit ID="btn_AgentId" ClientInstanceName="btn_AgentId" runat="server" Text='<%# Bind("AgentId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_AgentId,txt_AgentName,'A',null,null);
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                                <td colspan="4">
                                                                                     <dxe:ASPxTextBox ID="txt_AgentName" ClientInstanceName="txt_AgentName" runat="server" Text='<%# Bind("AgentName") %>' Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">From</td>
                                                                                <td colspan="3">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td class="lbl1">
                                                                                    <a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_FromPL" ClientInstanceName="txt_FromPL" runat="server" Text='<%# Bind("FromParkingLot") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">From Date</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxDateEdit ID="txt_FromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td class="lbl">Time</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="161">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">To</td>
                                                                                <td colspan="3" class="ctl2">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td class="lbl">
                                                                                    <%--<a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>--%>
                                                                                    <a href="#" onclick="PopupParkingLot(txt_ToPL,txt_Trip_ToCode);">To Parking Lot</a>
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_ToPL" ClientInstanceName="txt_ToPL" runat="server" Text='<%# Bind("ToParkingLot") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">ToDate</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td class="lbl">Time</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="161">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td class="lbl">Escort Ind</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cmb_Escort_Ind" runat="server" Width="165" Value='<%# Bind("Escort_Ind") %>'>
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                            <dxe:ListEditItem Value="Blank" Text="Blank" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Instruction</td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td class="lbl">Escort Remark  </td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_Escort_Remark" ClientInstanceName="txt_Trip_Escort_Remark" runat="server" Text='<%# Bind("Escort_Remark") %>' Width="165">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Input</td>
                                                                            </tr>
                                                                            <tr style="display: none">
                                                                                <td class="lbl">Container
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxDropDownEdit ID="dde_Trip_ContNo" runat="server" ClientInstanceName="dde_Trip_ContNo"
                                                                                        Text='<%# Bind("ContainerNo") %>' Width="165" AllowUserInput="false">
                                                                                        <DropDownWindowTemplate>
                                                                                            <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridPopCont"
                                                                                                Width="300px" KeyFieldName="Id" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="0">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="1">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                </Columns>
                                                                                                <ClientSideEvents RowClick="RowClickHandler" />
                                                                                            </dxwgv:ASPxGridView>
                                                                                        </DropDownWindowTemplate>
                                                                                    </dxe:ASPxDropDownEdit>
                                                                                </td>
                                                                                <td class="lbl">Trailer</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxButtonEdit ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="165">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_MasterData(btn_ChessisCode,null,'Chessis');
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                                <td class="lbl">Zone</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_zone" runat="server" Value='<%# Bind("ParkingZone") %>' Width="165" DataSourceID="dsZone" TextField="Code" ValueField="Code">
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td class="lbl">DriverRemark</td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">DeliveryRemark</td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_delivery_remark" ClientInstanceName="txt_delivery_remark" runat="server" Text='<%# Bind("DeliveryRemark") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">BillingRemark</td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_billing_remark" ClientInstanceName="txt_billing_remark" runat="server" Text='<%# Bind("BillingRemark") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">Satifaction Indator </td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_Satifaction_Indator" ClientInstanceName="txt_billing_remark" runat="server" Text='<%# Bind("Satisfaction") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Consignee Signature</td>
                                                                                <td colspan="2" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Signature</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="border-right: solid 1px lightgreen">
                                                                                    <img src='<%# show_consignee_signature(Eval("JobNo"),Eval("Id")) %>' style="width: 50%" />
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <img src='<%# show_driver_signature(Eval("JobNo"),Eval("Id")) %>' style="width: 50%" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6">
                                                                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                                        <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                                        </span>
                                                                                        <span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                            <%--<dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>--%>
                                                                                            <a onclick="gv_tpt_trip.GetValuesOnCustomCallback('Update',container_trip_update_cb);"><u>Update</u></a>
                                                                                        </span>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EditForm>
                                                                </Templates>
                                                            </dxwgv:ASPxGridView>
                                                            
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td width="10%">
                                                                        <dxe:ASPxButton ID="btn_cont_newTrip" runat="server" Text="Receipt List" CssClass="add_carpark" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                                                            <ClientSideEvents Click="function(s,e){PopupJobReceipt();}" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td width="80%"></td>
                                                                    <td width="10%>
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>
                                                                            <%--<span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>--%>
                                                                            <span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                <a onclick="grid_Cont_Tpt.GetValuesOnCustomCallback('save',transport_batch_add_cb);" href="#">Update</a>
                                                                            </span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                                
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                 <dxtc:TabPage Text="Crane" Name="Crane">
                                     <ContentCollection>
                                         <dxw:ContentControl>
                                             <div>
                                                <dxe:ASPxButton ID="ASPxButton26" runat="server" Text="Add Trip" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){
                                grid_Trip.AddNewRow();
                                }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="grid_Trip" ClientInstanceName="grid_Trip" runat="server" Width="1000px" DataSourceID="dsCraneTrip" KeyFieldName="Id" OnInit="grid_Trip_Init" AutoGenerateColumns="False"
                                                    OnBeforePerformDataSelect="grid_Trip_BeforePerformDataSelect" OnHtmlEditFormCreated="grid_Trip_HtmlEditFormCreated" OnRowInserting="grid_Trip_RowInserting" OnInitNewRow="grid_Trip_InitNewRow" OnRowDeleting="grid_Trip_RowDeleting" OnRowUpdating="grid_Trip_RowUpdating" OnRowDeleted="grid_Trip_RowDeleted" OnRowInserted="grid_Trip_RowInserted" OnRowUpdated="grid_Trip_RowUpdated" OnCustomDataCallback="grid_Trip_CustomDataCallback">
                                                    <SettingsPager PageSize="100" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="30">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='<%# "grid_Trip.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>

                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="12" Width="30">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Trip.GetValuesOnCustomCallback(\"Delete_"+Container.KeyValue+"\",trip_delete_cb);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>--%>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="Statuscode" Caption="Satus" Width="30"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToCode" Caption="Location"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="TowheadCode" Caption="Vehicle"></dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>--%>

                                                        <dxwgv:GridViewDataDateColumn VisibleIndex="9" Width="120" FieldName="BookingDate" Caption="BookingDate">
                                                            <DataItemTemplate><%# SafeValue.SafeDate( Eval("BookingDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("BookingTime")+"-"+Eval("BookingTime2") %></DataItemTemplate>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="FromDate" Caption="ActualDate" Width="160">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeDate(Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("FromTime")+"&nbsp;-&nbsp;"+ SafeValue.SafeDate(Eval("ToDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("ToTime")%>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="TotalHour" Caption="Hours"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="OtHour" Caption="OtHours"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="Remark" Caption="Remark"></dxwgv:GridViewDataColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxLabel ID="lb_tripId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                <dxe:ASPxTextBox ID="dde_Trip_ContId" ClientInstanceName="dde_Trip_ContId" runat="server" Text='<%# Bind("Det1Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table>
                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Trip Detail</td>
                                                                </tr>
                                                                                                                                <tr>
                                                                    <td>Trip Type</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Width="165" DropDownStyle="DropDown">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="CRA" Value="CRA" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Trailer Type 
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_TrailerType" ClientInstanceName="txt_TrailerType" runat="server" Text='<%# Bind("RequestTrailerType") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Driver</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="165">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,btn_TowheadCode);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Vehicle</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheadCode") %>' AutoPostBack="False" Width="165">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_VehicleList(btn_TowheadCode,null,'crane');
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                     <td>Vehicle Type 
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_VehicleType" ClientInstanceName="txt_VehicleType" runat="server" Text='<%# Bind("RequestVehicleType") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Status</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                                                            <Items>
                                                                                <%--<dxe:ListEditItem Value="U" Text="Use" />--%>
                                                                                <dxe:ListEditItem Value="S" Text="Start" />
                                                                                <%--<dxe:ListEditItem Value="D" Text="Doing" />
                                                                                <dxe:ListEditItem Value="W" Text="Waiting" />--%>
                                                                                <dxe:ListEditItem Value="P" Text="Pending" />
                                                                                <dxe:ListEditItem Value="C" Text="Completed" />
                                                                                <dxe:ListEditItem Value="X" Text="Cancel" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td>ByUser</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_ByUser" runat="server" Value='<%# Bind("ByUser") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Total Hour</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="txt_TotalHour" Height="21px" Value='<%# Bind("TotalHour")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>OT Hour</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="txt_OtHour" Height="21px" Value='<%# Bind("OtHour")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>ScheduleDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_BookingDate" runat="server" Value='<%# Bind("BookingDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_BookingTime" runat="server" Text='<%# Bind("BookingTime") %>' Width="162">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>To Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_BookingTime2" runat="server" Text='<%# Bind("BookingTime2") %>' Width="162">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>BookingRemark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_BookingRemark" ClientInstanceName="txt_BookingRemark" runat="server" Text='<%# Bind("BookingRemark") %>' Width="427">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                    <td>From</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="414">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>
                                                                        <a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_FromPL" ClientInstanceName="txt_FromPL" runat="server" Text='<%# Bind("FromParkingLot") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>--%>
                                                                <tr>
                                                                    <td>Start Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="txt_FromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="162">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>End Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="162">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Location</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="427">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <%--<td>
                                                                        <a href="#" onclick="PopupParkingLot(txt_ToPL,txt_Trip_ToCode);">To Parking Lot</a>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_ToPL" ClientInstanceName="txt_ToPL" runat="server" Text='<%# Bind("ToParkingLot") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td>Instruction</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="427">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Billing Remark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_BillingRemark" ClientInstanceName="txt_Trip_BillingRemark" runat="server" Text='<%# Bind("BillingRemark") %>' Width="427">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>


                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Input</td>
                                                                </tr>
                                                                <%--<tr>

                                                                    <td>Container
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDropDownEdit ID="dde_Trip_ContNo" runat="server" ClientInstanceName="dde_Trip_ContNo"
                                                                            Text='<%# Bind("ContainerNo") %>' Width="165" AllowUserInput="false">
                                                                            <DropDownWindowTemplate>
                                                                                <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridPopCont"
                                                                                    Width="300px" KeyFieldName="Id" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                                                    <Columns>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="0">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="1">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <ClientSideEvents RowClick="RowClickHandler" />
                                                                                </dxwgv:ASPxGridView>
                                                                            </DropDownWindowTemplate>
                                                                        </dxe:ASPxDropDownEdit>
                                                                    </td>
                                                                    <td>Trailer</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="165">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_MasterData(btn_ChessisCode,null,'Chessis');
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>Zone</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_zone" runat="server" Value='<%# Bind("ParkingZone") %>' Width="165" DataSourceID="dsZone" TextField="Code" ValueField="Code">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>--%>



                                                                <%--<tr>
                                <td>Stage</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_StageCode" runat="server" Value='<%# Bind("StageCode") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Pending" Text="Pending" />
                                            <dxe:ListEditItem Value="Port" Text="Port" />
                                            <dxe:ListEditItem Value="Yard" Text="Yard" />
                                            <dxe:ListEditItem Value="Park1" Text="Park1" />
                                            <dxe:ListEditItem Value="Warehouse" Text="Warehouse" />
                                            <dxe:ListEditItem Value="Park2" Text="Park2" />
                                            <dxe:ListEditItem Value="Park3" Text="Park3" />
                                            <dxe:ListEditItem Value="Completed" Text="Completed" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Stage Status</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_StageStatus" runat="server" Value='<%# Bind("StageStatus") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="" Text="" />
                                            <dxe:ListEditItem Value="DriveTo" Text="DriveTo" />
                                            <dxe:ListEditItem Value="Reach" Text="Reach" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>--%>
                                                                <tr>
                                                                    <td>DriverRemark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="427">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <%--<td>Incentive</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive1" Height="21px" Value='<%# Bind("Incentive1")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>--%>
                                                                    <td>Overtime</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive2" Height="21px" Value='<%# Bind("Incentive2")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <%--<td>Standby(hr)</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive3" Height="21px" Value='<%# Bind("Incentive3")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>--%>
                                                                </tr>

                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Surcharge</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>EXPENSE</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge1" Height="21px" Value='<%# Bind("Charge_EXPENSE")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <%--<td>SUBCONTRACT</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge2" Height="21px" Value='<%# Bind("Charge_SUBCONTRACT")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Billing</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Crane Charges</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge2" Height="21px" Value='<%# Bind("Billing_CraneCharges")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Crane Overtime</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge3" Height="21px" Value='<%# Bind("Billing_CraneOvertime")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Concrete Bucket</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge4" Height="21px" Value='<%# Bind("Billing_ConcreteBucket")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Sand Bucket</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge5" Height="21px" Value='<%# Bind("Billing_SandBucket")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Lifting Supervisor</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="ASPxSpinEdit2" Height="21px" Value='<%# Bind("Billing_LiftingSupervisor")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Rigger</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="ASPxSpinEdit3" Height="21px" Value='<%# Bind("Billing_Ringer")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Signal</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="ASPxSpinEdit4" Height="21px" Value='<%# Bind("Billing_Signal")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Lifting Equipment</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="ASPxSpinEdit5" Height="21px" Value='<%# Bind("Billing_LightEquipment")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Labour</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="ASPxSpinEdit6" Height="21px" Value='<%# Bind("Billing_Labour")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td>Lifting Team Overtime</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_LifingTeamOverTime" Height="21px" Value='<%# Bind("Charge_LiftingTeamOverTime")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Worker Overtime</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_WorkerOvertime" Height="21px" Value='<%# Bind("Charge_WorkerOvertime")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>ParkingFee</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ParkingFee" Height="21px" Value='<%# Bind("Charge_ParkingFee")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                    <td>DHC</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge1" Height="21px" Value='<%# Bind("Charge1")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>WEIGHING</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge2" Height="21px" Value='<%# Bind("Charge2")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>WASHING</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge3" Height="21px" Value='<%# Bind("Charge3")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>REPAIR</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge4" Height="21px" Value='<%# Bind("Charge4")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>DETENTION</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge5" Height="21px" Value='<%# Bind("Charge5")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>DEMURRAGE</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge6" Height="21px" Value='<%# Bind("Charge6")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>LIFT ON/OFF</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge7" Height="21px" Value='<%# Bind("Charge7")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>C/SHIPMENT</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge8" Height="21px" Value='<%# Bind("Charge8")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>EMF</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge10" Height="21px" Value='<%# Bind("Charge10")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td>OTHER</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge9" Height="21px" Value='<%# Bind("Charge9")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>--%>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>
                                                                            <span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                <%--<dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>--%>
                                                                                <a onclick="grid_Trip.GetValuesOnCustomCallback('Update',trip_update_cb);"><u>Update</u></a>
                                                                            </span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                         </dxw:ContentControl>
                                     </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Quotation" Name="Quotation">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table style="width: 100%">
                                                <tr style="vertical-align: top;">
                                                    <td class="lbl1">
                                                        <a href="#" onclick="javasrcipt:PopupRemark(txt_JobDes,null)">Job Description
                                                        </a>

                                                    </td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txt_JobDes" ClientInstanceName="txt_JobDes" Rows="3" runat="server" Text='<%# Eval("JobDes") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td class="lbl1"><a href="#" onclick="javasrcipt:PopupRemark(txt_QuoteRemark,null)">Quote Remark
                                                    </a></td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txt_QuoteRemark" ClientInstanceName="txt_QuoteRemark" Rows="3" runat="server" Text='<%# Eval("QuoteRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbb_Count" ClientInstanceName="cbb_Count" runat="server" Width="80">
                                                            <Items>
                                                                <dxe:ListEditItem Text="1" Value="1" />
                                                                <dxe:ListEditItem Text="2" Value="2" />
                                                                <dxe:ListEditItem Text="3" Value="3" />
                                                                <dxe:ListEditItem Text="4" Value="4" />
                                                                <dxe:ListEditItem Text="5" Value="5" />
                                                                <dxe:ListEditItem Text="6" Value="6" Selected="true" />
                                                                <dxe:ListEditItem Text="7" Value="7" />
                                                                <dxe:ListEditItem Text="8" Value="8" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton19" runat="server" Text="Auto Add Lines" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL"&&(SafeValue.SafeString(Eval("JobStatus"),"Quoted")=="Quoted"||SafeValue.SafeString(Eval("JobStatus"),"Booked")=="Booked") %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                                                 grid_job.GetValuesOnCustomCallback('AutoLines',onQuotedCallBack);
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton12" runat="server" Text="Add Charges" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL"&&SafeValue.SafeString(Eval("JobStatus"),"Quoted")=="Quoted" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                                                PopupRate();
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton14" runat="server" Text="Pick from Master Rate" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL"&&SafeValue.SafeString(Eval("JobStatus"),"Quoted")=="Quoted" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                                      PopupMasterRate();
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn" runat="server" Text="Copy Quotation" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                                     if(confirm('Copy to new quotation ?')){
                                                      grid_job.GetValuesOnCustomCallback('CopyQ',onNoCallBack);
                                                     }
                                                   }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td width="60%"></td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_AddRate" runat="server" Text="Print Quotation" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e){
                                                      PrintQuotation();
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_SendEmail" ClientInstanceName="btn_SendEmail" Width="120" runat="server" Text="Email Quotation"
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e){
                                            ShowEmail();
                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>

                                            </table>
                                            <table>
                                                <tr>
                                                    <td colspan="6">
                                                        <div id="email" style="display: none">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td class="lbl">Email To</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_Email1" ClientInstanceName="cbb_Email1" runat="server" DropDownStyle="DropDown">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="lbl">Email CC</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_Email2" runat="server"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">Email BCC</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_Email3" runat="server" Width="150"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">Email Subject
                                                                    </td>
                                                                    <td colspan="5">
                                                                        <dxe:ASPxTextBox ID="txt_Subject" runat="server" Width="100%"></dxe:ASPxTextBox>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">Email Message
                                                                    </td>
                                                                    <td colspan="5">
                                                                        <dxe:ASPxMemo ID="memo_message" runat="server" Rows="3" Width="100%"></dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td colspan="5" style="text-align: left">
                                                                        <dxe:ASPxButton ID="btn_ConfirmEmail" runat="server" Text="Confirm Email"
                                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                                            <ClientSideEvents Click="function(s,e){
                                            if(confirm('Confirm Send Email?')){
										        grid_job.GetValuesOnCustomCallback('SEND',OnSendEmailCallBack);
                                             }
                            }" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="overflow-y: scroll; width: 1000px">
                                                <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server"
                                                    DataSourceID="ds1" KeyFieldName="Id" OnRowUpdating="Grid1_RowUpdating" OnRowDeleting="Grid1_RowDeleting" OnCustomDataCallback="grid1_CustomDataCallback"
                                                    OnRowInserting="Grid1_RowInserting" OnInitNewRow="Grid1_InitNewRow" OnBeforePerformDataSelect="grid1_BeforePerformDataSelect"
                                                    OnInit="Grid1_Init" Width="1300px" AutoGenerateColumns="False"  Styles-CommandColumn-HorizontalAlign="Left"  Styles-CommandColumn-Spacing="10" >
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <SettingsEditing Mode="Batch" BatchEditSettings-StartEditAction="Click"/>
                                                    <Settings ShowTitlePanel="true"  />
                                                    <SettingsPager Mode="ShowPager" PageSize="10" />
                                                    <SettingsCommandButton UpdateButton-ButtonType="Button" UpdateButton-Text="Save" CancelButton-Text="Cancel"  CancelButton-ButtonType="Button" >
                                                    </SettingsCommandButton>
                                                    <Templates>
                                                        <TitlePanel>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                         <dxe:ASPxCheckBox ID="ack_DelAll" runat="server" Width="10" AutoPostBack="False"
                                                                            UseSubmitBehavior="False">
                                                                             <ClientSideEvents CheckedChanged="function(s, e) {
                                   SelectAll();
                                    }" />
                                                                </dxe:ASPxCheckBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btnDeleteAll" ClientInstanceName="btnDeleteAll" 
                                                                            Text="Delete All" runat="server" Cursor="pointer" AutoPostBack="false" UseSubmitBehavior="false">
                                                                            <ClientSideEvents Click="function(s, e) {
                                   if(confirm('Confirm Delete?')){
                                   grid1.GetValuesOnCustomCallback('OK',onCallBack); 
                                                                                  }    
                                    }" />
                                                                             </dxe:ASPxButton>
                                                                    </td>
                                                                    
                                                                </tr>
                                                            </table>
                                                        </TitlePanel>
                                                    </Templates>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" FieldName="Id" ReadOnly="true">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxCheckBox ID="ack_IsDel" runat="server" Width="10">
                                                                </dxe:ASPxCheckBox>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0" />
                                                        <dxwgv:GridViewCommandColumn Visible="false" VisibleIndex="0" Width="5%">
                                                            <EditButton Visible="True" />
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewCommandColumn Visible="false" VisibleIndex="0" Width="5%">
                                                            <DeleteButton Visible="true" />
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="Update" FieldName="RowUpdateTime" VisibleIndex="998"
                                                            Width="140">
                                                            <PropertiesDateEdit  EditFormat="DateTime"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="ChgCode" Caption="ChargeCode" Width="50" VisibleIndex="1">
                                                            <PropertiesComboBox DataSourceID="dsRate" ValueField="ChgCodeId" TextField="ChgCodeDe" ValueType="System.String" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataMemoColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                                                            Width="260" PropertiesMemoEdit-Rows="3">
                                                        </dxwgv:GridViewDataMemoColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="LumsumInd" Caption="Lumsum Ind" Width="50" VisibleIndex="1">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="" Value="" />
                                                                    <dxe:ListEditItem Text="1" Value="1" />
                                                                    <dxe:ListEditItem Text="2" Value="2" />
                                                                    <dxe:ListEditItem Text="3" Value="3" />
                                                                    <dxe:ListEditItem Text="4" Value="4" />
                                                                    <dxe:ListEditItem Text="5" Value="5" />
                                                                    <dxe:ListEditItem Text="6" Value="6" />
                                                                    <dxe:ListEditItem Text="7" Value="7" />
                                                                    <dxe:ListEditItem Text="8" Value="8" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty" VisibleIndex="1"
                                                            Width="50">
                                                            <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Rate" FieldName="Price" VisibleIndex="1"
                                                            Width="50">
                                                            <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="1"
                                                            Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataMemoColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                                                            Width="150" PropertiesMemoEdit-Rows="3">
                                                        </dxwgv:GridViewDataMemoColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="ContSize" Caption="Cont Size" Width="50" VisibleIndex="1">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="20" Value="20" />
                                                                    <dxe:ListEditItem Text="40" Value="40" />
                                                                    <dxe:ListEditItem Text="45" Value="45" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="ContType" Caption="Cont Type" Width="150" VisibleIndex="1">
                                                            <PropertiesComboBox DataSourceID="dsContType" ValueType="System.String" TextField="containerType" ValueField="containerType">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="BillType" Caption="Bill Type" Width="150" VisibleIndex="1">
                                                            <PropertiesComboBox DataSourceID="dsRateType" ValueType="System.String" TextField="Code" ValueField="Code">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="BillScope" Caption="Scope" Width="80" VisibleIndex="1">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Job" Value="JOB"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="Cont" Value="CONT"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="Adhoc" Value="ADHOC"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                         
                                                    </Columns>
                                                    <Settings ShowFooter="true" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                                    </TotalSummary>

                                                </dxwgv:ASPxGridView>
                                            </div>
                                            <table style="width: 1000px; vertical-align: top">
                                                <tr style="vertical-align: top;">
                                                    <td class="lbl1">
                                                        <a href="#" onclick="javasrcipt:PopupRemark(txt_TerminalRemark,null)">Terms & Conditions
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txt_TerminalRemark" ClientInstanceName="txt_TerminalRemark" Rows="3" runat="server" Text='<%# Eval("TerminalRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td class="lbl1">
                                                        <a href="#" onclick="javasrcipt:PopupRemark(memo_InternalRmark,null)">Internal Remark
                                                        </a>
                                                        </t>
                                                    <td>
                                                        <dxe:ASPxMemo ID="memo_InternalRmark" ClientInstanceName="memo_InternalRmark" Rows="3" runat="server" Text='<%# Eval("InternalRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: top;">
                                                    <td class="lbl1" style="display:none"><a href="#" onclick="javasrcipt:PopupRemark(memo_LumSumRemark,null)">LumSum Remark
                                                    </a></td>
                                                    <td style="display:none">
                                                        <dxe:ASPxMemo ID="memo_LumSumRemark" ClientInstanceName="memo_LumSumRemark" Rows="3" runat="server" Text='<%# Eval("LumSumRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td class="lbl1">
                                                        <a href="#" onclick="javasrcipt:PopupRemark(memo_AdditionalRemark,null)">Additional Remark
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="memo_AdditionalRemark" ClientInstanceName="memo_AdditionalRemark" Rows="3" runat="server" Text='<%# Eval("AdditionalRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Cargo Detail" Visible="true" Name="Cargo">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div class='<%# (SafeValue.SafeString(Eval("JobType"))!="WDO"&&SafeValue.SafeString(Eval("JobType"))!="FTR")?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                       grid_wh.AddNewRow();
                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>

                                                    </td>
                                                    <td>
                                                        <div class='<%# SafeValue.SafeString(Eval("JobType"))=="WDO"?"show":"hide"  %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Add From Stock" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                       PopupJobHouse();
                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                        <div class='<%# SafeValue.SafeString(Eval("JobType"))=="FTR"?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton15" runat="server" Text="Add From Stock" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                       PopupStockForTransfer();
                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_PrintTallySheet1" runat="server" Text="Print Tallysheet Indented" AutoPostBack="false" Width="100">
                                                            <ClientSideEvents Click="function(s,e){
                                                                        printTallySheetIndented();
                                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton17" runat="server" Text="Print Tallysheet Confirmed" AutoPostBack="false" Width="100">
                                                            <ClientSideEvents Click="function(s,e){
                                                                        printTallySheetConfirmed();
                                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td style="display:none">
                                                        <dxe:ASPxButton ID="ASPxButton22" runat="server" Text="Print Stock Tallysheet" AutoPostBack="false" Width="100">
                                                            <ClientSideEvents Click="function(s,e){
                                                                        printStockTallySheet();
                                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <div class='<%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton18" runat="server" Text="Create Transfer" AutoPostBack="false" Width="100">
                                                                <ClientSideEvents Click="function(s,e){
                                                                         GoJob_Page('Transfer');
                                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class='<%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton20" runat="server" Text="Create Delivery Order" AutoPostBack="false" Width="100">
                                                                <ClientSideEvents Click="function(s,e){
                                                                        GoJob_Page('Delivery');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class='<%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton21" runat="server" Text="Show Delivery Order" AutoPostBack="false" Width="100">
                                                                <ClientSideEvents Click="function(s,e){
                                                                        GoJob_Page('ShowDO');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                    </td>

                                                </tr>
                                            </table>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td class="lbl">Warehouse</t>
                                                    <td class="ctl">
                                                        <dxe:ASPxButtonEdit ID="txt_WareHouseId" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_WareHouseId" runat="server" Text='<%# Eval("WareHouseCode") %>' Width="170" AutoPostBack="False">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupWh(txt_WareHouseId,null);
                                                                        }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                        <td class="lbl">PermitNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_wh_PermitNo" Width="100%" runat="server" Text='<%# Eval("WhPermitNo") %>'></dxe:ASPxTextBox>
                                                        </td>
                                                        <td class="lbl">IncoTerms</td>
                                                        <td class="ctl">
                                                            <dxe:ASPxButtonEdit ID="txt_IncoTerm"  ClientInstanceName="txt_IncoTerm" runat="server" Text='<%# Eval("IncoTerm") %>' Width="150" AutoPostBack="False">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupIncoTerm(txt_IncoTerm,null);
                                                                        }" />
                                                        </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td class="lbl">PermitDate</td>
                                                        <td class="ctl">
                                                            <dxe:ASPxDateEdit ID="date_PermitDate" runat="server" Value='<%# Eval("PermitDate") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="lbl">Permit By</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxComboBox ID="txt_PermitBy" Width="170" runat="server" Value='<%# Eval("PermitBy") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Englee" Value="Englee" />
                                                                <dxe:ListEditItem Text="Client" Value="Client" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td class="lbl">Supp. InvNo  </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox ID="txt_PartyInvNo" Width="100%" runat="server" Text='<%# Eval("PartyInvNo") %>'></dxe:ASPxTextBox>
                                                    </td>
                                                    <td class="lbl">GstAmt
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="150"
                                                            ID="spin_GstAmt" Height="21px" Value='<%# Bind("GstAmt")%>' DecimalPlaces="3" Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td class="lbl1">Payment Status</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxComboBox ID="cbb_PaymentStatus" Width="120" runat="server" Value='<%# Eval("PaymentStatus") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Paid" Value="Paid" />
                                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="overflow-y: auto; width: 1000px">
                                                <dxwgv:ASPxGridView ID="grid_wh" ClientInstanceName="grid_wh" runat="server" DataSourceID="dsWh" OnInit="grid_wh_Init" OnInitNewRow="grid_wh_InitNewRow"
                                                    OnRowInserting="grid_wh_RowInserting" OnRowUpdating="grid_wh_RowUpdating" OnRowDeleting="grid_wh_RowDeleting" OnRowInserted="grid_wh_RowInserted"
                                                    OnBeforePerformDataSelect="grid_wh_BeforePerformDataSelect" OnCustomDataCallback="grid_wh_CustomDataCallback"
                                                    KeyFieldName="Id" Width="1300px" AutoGenerateColumns="False">
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px">
                                                            <EditButton Visible="true"></EditButton>
                                                            <DeleteButton Visible="true"></DeleteButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" Width="100%" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table style="display:<%# SafeValue.SafeString(Eval("CargoType"))=="IN"?"block":"none"%>">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_CargoCount" ClientInstanceName="cbb_CargoCount" runat="server" Width="50">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="1" Value="1" Selected="true" />
                                                                                    <dxe:ListEditItem Text="2" Value="2" />
                                                                                    <dxe:ListEditItem Text="3" Value="3" />
                                                                                    <dxe:ListEditItem Text="4" Value="4" />
                                                                                    <dxe:ListEditItem Text="5" Value="5" />
                                                                                    <dxe:ListEditItem Text="6" Value="6" />
                                                                                    <dxe:ListEditItem Text="7" Value="7" />
                                                                                    <dxe:ListEditItem Text="8" Value="8" />
                                                                                    <dxe:ListEditItem Text="9" Value="9" />
                                                                                    <dxe:ListEditItem Text="10" Value="10" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" style="width: 80px" class="button" value="Add Cargo" onclick="copy_cargo_inline(<%# Container.VisibleIndex %>);" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="0" Width="180" SortIndex="1">
                                                            <DataItemTemplate>
                                                                <%# Eval("CargoStatus") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxComboBox ID="cbb_JobType" runat="server" Value='<%# Bind("CargoStatus") %>' Width="90">
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Pending" Value="P" />
                                                                        <dxe:ListEditItem Text="Completed" Value="C" />
                                                                    </Items>
                                                                </dxe:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Type" FieldName="CargoType" ReadOnly="true" VisibleIndex="0">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Lot No/BL/Trailer" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <table style="width: 260px;">
                                                                    <tr>
                                                                        <td class="lbl">Lot No</td>

                                                                        <td><%# Eval("BookingNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">BL/Bkg No</td>

                                                                        <td><%# Eval("HblNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Trailer No</td>

                                                                        <td><%# Eval("ContNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Type</td>

                                                                        <td><%# Eval("OpsType") %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 260px">
                                                                    <tr>
                                                                        <td class="lbl">Lot No</td>

                                                                        <td class="ctl">
                                                                            <dxe:ASPxTextBox ID="txt_BookingNo" Width="120" runat="server" Text='<%# Bind("BookingNo") %>'></dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl1">BL/Bkg No</td>

                                                                        <td class="ctl">
                                                                            <dxe:ASPxTextBox ID="txt_HblNo" runat="server" Width="120" Text='<%# Bind("HblNo") %>'></dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Trailer No</td>

                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ContNo") %>' AutoPostBack="False" Width="120">
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo,null);
                                                                        }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Type</td>

                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_JobType" runat="server" Value='<%# Bind("OpsType") %>' Width="120">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Delivery" Value="Delivery" />
                                                                                    <dxe:ListEditItem Text="Export" Value="Export" />
                                                                                    <dxe:ListEditItem Text="Storage" Value="Storage" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Marking1" VisibleIndex="2" Width="180" Visible="true">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Marking1" ClientInstanceName="memo_Marking1" Text='<%# Bind("Marking1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Marking2" VisibleIndex="2" Width="180" Visible="true">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Marking2" ClientInstanceName="memo_Marking2" Text='<%# Bind("Marking2") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Booking Info" VisibleIndex="2" Width="160">
                                                            <DataItemTemplate>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>

                                                                        <td><%# Eval("Qty") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Unit</td>

                                                                        <td><%# Eval("UomCode") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>

                                                                        <td><%# Eval("Weight") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>

                                                                        <td><%# Eval("Volume") %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 120px">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td class="ctl">
                                                                            <dxe:ASPxSpinEdit runat="server" Width="100%"
                                                                                ID="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Unit</td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit runat="server" ID="txt_UomCode" Width="70" ClientInstanceName="txt_UomCode" Text='<%# Bind("UomCode") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_UomCode,2);
                                                                            }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>

                                                                        <td>
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Weight" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>

                                                                        <td>
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Volume" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Booking SKU Info" VisibleIndex="2" Width="160">
                                                            <DataItemTemplate>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td class="lbl">SKU Code</td>

                                                                        <td><%# Eval("BkgSKuCode") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>

                                                                        <td><%# Eval("BkgSkuQty") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Unit</td>

                                                                        <td><%# Eval("BkgSkuUnit") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" class="lbl">
                                                                            <input type="button" style="width: 100%" class="button" value="Copy" onclick="copy_inline(<%# Container.VisibleIndex %>);" />

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 120px">
                                                                    <tr>
                                                                        <td class="lbl">SKU Code</td>
                                                                        <td class="ctl">
                                                                            <dxe:ASPxButtonEdit ID="btn_BkgSKuCode" ClientInstanceName="btn_BkgSKuCode" runat="server" Text='<%# Bind("BkgSKuCode") %>' AutoPostBack="False" Width="100%">
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupSku(btn_BkgSKuCode);
                                                                        }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_BkgSkuQty" Height="21px" Value='<%# Bind("BkgSkuQty")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Unit</td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit runat="server" ID="txt_BkgSkuUnit" Width="70" ClientInstanceName="txt_BkgSkuUnit" Text='<%# Bind("BkgSkuUnit") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_BkgSkuUnit,2);
                                                                            }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Actual Info" VisibleIndex="2" Width="400">
                                                            <DataItemTemplate>
                                                                <table style="width: 280px;">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td><%# Eval("QtyOrig") %></td>
                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                        <td class="lbl1">SKU Code</td>
                                                                        <td colspan="2"><%# Eval("SkuCode") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>
                                                                        <td colspan="2"><%# Eval("WeightOrig") %></td>
                                                                        <td class="lbl">Qty</td>
                                                                        <td><%# Eval("PackQty") %></td>
                                                                        <td><%# Eval("PackUom") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>
                                                                        <td colspan="2"><%# Eval("VolumeOrig") %></td>
                                                                        <td class="lbl">Location</td>
                                                                        <td><%# Eval("Location") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Remark</td>
                                                                        <td colspan="4">
                                                                            <%# Eval("Desc1") %>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width:100%">
                                                                                <tr>
                                                                                    <td><input type="button" class="button" value="Dimension" onclick="dimension_inline(<%# Container.VisibleIndex %>);" /></td>
                                                                                    <td><input type="button" class="button" value="Process" onclick="process_inline(<%# Container.VisibleIndex %>);" /></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td colspan="4">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>L</td>
                                                                                    <td><%# Eval("LengthPack") %></td>
                                                                                    <td>×</td>
                                                                                    <td>W</td>
                                                                                    <td><%# Eval("WidthPack") %></td>
                                                                                    <td>×</td>
                                                                                    <td>H</td>
                                                                                    <td><%# Eval("HeightPack") %></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 420px">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit runat="server" Width="60"
                                                                                ID="spin_Qty" Height="21px" Value='<%# Bind("QtyOrig")%>' Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit runat="server" ID="txt_PackTypeOrig" Width="70" ClientInstanceName="txt_PackTypeOrig" Text='<%# Bind("PackTypeOrig") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_PackTypeOrig,2);
                                                                            }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                        <td class="lbl1">SKU Code</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxButtonEdit ID="btn_SkuCode" ClientInstanceName="btn_SkuCode" runat="server" Text='<%# Bind("SkuCode") %>' AutoPostBack="False" Width="100%">
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupSku(btn_SkuCode);
                                                                        }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Weight" Height="21px" Value='<%# Bind("WeightOrig")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td class="lbl">Qty</td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit runat="server" Width="60"
                                                                                ID="spin_PackQty" Height="100%" Value='<%# Bind("PackQty")%>' Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit runat="server" ID="txt_PackUom" Width="70" ClientInstanceName="txt_PackUom" Text='<%# Bind("PackUom") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_PackUom,2);
                                                                            }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Volume" Height="21px" Value='<%# Bind("VolumeOrig")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td class="lbl">Location</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxButtonEdit ID="txt_Location" ClientInstanceName="txt_Location" runat="server" Text='<%# Bind("Location") %>' AutoPostBack="False" Width="100%">
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupLoc(txt_Location);
                                                                        }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Remark</td>
                                                                        <td colspan="5">
                                                                            <dxe:ASPxMemo ID="memo_Desc1" ClientInstanceName="memo_Desc1" Text='<%# Bind("Desc1") %>' Rows="2" runat="server" Width="100%"></dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td colspan="6">
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td>L</td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                            ID="spin_LengthPack" Height="21px" Value='<%# Bind("LengthPack")%>' DecimalPlaces="3" Increment="0">
                                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>×</td>
                                                                                    <td>W</td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                            ID="spin_WidthPack" Height="21px" Value='<%# Bind("WidthPack")%>' DecimalPlaces="3" Increment="0">
                                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>×</td>
                                                                                    <td>H</td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                            ID="ASPxSpinEdit1" Height="21px" Value='<%# Bind("HeightPack")%>' DecimalPlaces="3" Increment="0">
                                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                         <dxwgv:GridViewDataTextColumn Caption="Balance Info" VisibleIndex="2" Width="200">
                                                            <DataItemTemplate>
                                                                <table style="width: 160px">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td><%# Eval("BalanceQty") %></td>

                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>

                                                                        <td colspan="2"><%# Eval("BalanceWeight") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>

                                                                        <td colspan="2"><%# Eval("BalanceVolume") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl1">SKU Qty</td>

                                                                        <td colspan="2"><%# Eval("BalanceSkuQty") %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 160px;line-height:20px;">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td><%# Eval("BalanceQty") %></td>

                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>

                                                                        <td colspan="2"><%# Eval("BalanceWeight") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>

                                                                        <td colspan="2"><%# Eval("BalanceVolume") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl1">SKU Qty</td>

                                                                        <td colspan="2"><%# Eval("BalanceSkuQty") %></td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Bind("Remark1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="3" Width="200">
                                                            <DataItemTemplate>
                                                                <table style="width: 200px">
                                                                    <tr>
                                                                        <td class="lbl">Landing</td>

                                                                        <td>
                                                                            <%# Eval("LandStatus") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">DG Cargo</td>

                                                                        <td>
                                                                            <%# Eval("DgClass") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Damage</td>
                                                                        <td>
                                                                            <%# Eval("DamagedStatus") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">DMG Remark</td>
                                                                        <td>
                                                                            <%# Eval("Remark2") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 200px">
                                                                    <tr>
                                                                        <td class="lbl">Landing</td>

                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_LandStatus" runat="server" Value='<%# Bind("LandStatus") %>' Width="100">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                                                                    <dxe:ListEditItem Text="Shortland" Value="Shortland" />
                                                                                    <dxe:ListEditItem Text="Overland" Value="Overland" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">DG Cargo</td>

                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_DgClass" runat="server" Value='<%# Bind("DgClass") %>' Width="100">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                                                                    <dxe:ListEditItem Text="Class 2" Value="Class 2" />
                                                                                    <dxe:ListEditItem Text="Class 3" Value="Class 3" />
                                                                                    <dxe:ListEditItem Text="Other Class" Value="Other Class" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Damage</td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_Damage" runat="server" Value='<%# Bind("DamagedStatus") %>' Width="100">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                                                                    <dxe:ListEditItem Text="Damaged" Value="Damaged" />

                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">DMG Remark</td>
                                                                        <td>
                                                                            <dxe:ASPxMemo ID="meno_Remark2" Rows="2" runat="server" Text='<%# Bind("Remark2") %>' Width="100%">
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Attachment" VisibleIndex="6" Width="100">
                                                            <DataItemTemplate>
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                                    <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Text='<%# Eval("ContNo") %>'></dxe:ASPxTextBox>
                                                                </div>
                                                                <input type="button" class="button" value="Upload" onclick="upload_inline(<%# Container.VisibleIndex %>);" />
                                                                <br />
                                                                <br />
                                                                <div class='<%# FilePath(SafeValue.SafeInt(Eval("Id"),0))!=""?"show":"hide" %>' style="min-width: 70px;">
                                                                    <a href='<%# "/Photos/"+FilePath(SafeValue.SafeInt(Eval("Id"),0)) %>' target="_blank">View</a>
                                                                </div>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFooter="True" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                                                    </TotalSummary>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Costing" Visible="true" Name="Billing">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl_Bill" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice_Import,"CTM", txt_JobNo.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton7" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import,"CTM", txt_JobNo.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import,"CTM", txt_JobNo.GetText(), "0");
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
                                                        <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import,"CTM", txt_JobNo.GetText(), "0");
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import,"CTM", txt_JobNo.GetText(), "0");
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
                                            <dxe:ASPxButton ID="ASPxButton10" Width="150" runat="server" Visible="false" Text="Import Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                       PopupJobRate();
                                                        }" />
                                            </dxe:ASPxButton>
                                            <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Cost.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>

                                            <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server" OnHtmlDataCellPrepared="grid_Cost_HtmlDataCellPrepared"
                                                DataSourceID="dsCosting" KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect"
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
                                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE"&& SafeValue.SafeString(Eval("LineSource"))!="S" %>'
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Cost.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Line Type" FieldName="LineType" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgCode" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgCodeDe" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContNo" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty" VisibleIndex="3" Width="50">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="Price" VisibleIndex="4" Width="50">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Gst Type" FieldName="GstType" VisibleIndex="5" Width="50">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Amt" FieldName="LocAmt" VisibleIndex="5" Width="50">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Invioice No" FieldName="InvoiceNo" VisibleIndex="5" Width="50">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Invoice Date" FieldName="InvoiceDate" VisibleIndex="5" Width="60">
                                                        <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("InvoiceDate")) %> </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Invoice Amt" FieldName="InvoiceAmt" VisibleIndex="5" Width="50">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Gst Type" FieldName="InvoiceGstType" VisibleIndex="5" Width="50">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <div style="display: none">
                                                        </div>
                                                        <table>
                                                            <tr>
                                                                <td class="lbl">Charge Code
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
                                                                        ReadOnly="true" runat="server" Text='<%# Bind("ChgCodeDe") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="lbl">Vendor
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
                                                                    <dxe:ASPxTextBox runat="server" BackColor="Control" Width="100%" ID="txt_CostVendorName"
                                                                        ClientInstanceName="txt_CostVendorName" ReadOnly="true" Text=''>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Remark
                                                                </td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="spin_CostRmk" Text='<%# Bind("Remark") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td colspan="5">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td class="lbl">Cont No</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_ContNo" Text='<%# Bind("ContNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td class="lbl">Cont Type</td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContType") %>'></dxe:ASPxComboBox>

                                                                            </td>
                                                                            <td class="lbl">Line Type</td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ASPxComboBox1" ClientInstanceName="txt_ContType" runat="server" Width="60" Value='<%# Bind("LineType") %>'>
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="Job" Value="JOB" />
                                                                                        <dxe:ListEditItem Text="Cont" Value="CONT" />
                                                                                        <dxe:ListEditItem Text="Claim" Value="CL" />
                                                                                        <dxe:ListEditItem Text="Transport" Value="TP" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="8">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td class="lbl">Qty
                                                                            </td>
                                                                            <td class="lbl">Price
                                                                            </td>
                                                                            <td class="lbl">Gst Type
                                                                            </td>
                                                                            <td class="lbl">Currency
                                                                            </td>
                                                                            <td class="lbl">ExRate
                                                                            </td>
                                                                            <td class="lbl">Amount
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostQty"
                                                                                    ID="spin_CostQty" Text='<%# Bind("Qty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostQty.GetText(),spin_CostPrice.GetText(),spin_CostExRate.GetText(),2,spin_CostLocAmt);
	                                                   }" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostPrice"
                                                                                    runat="server" Width="100" ID="spin_CostPrice" Value='<%# Bind("Price")%>' Increment="0" DecimalPlaces="2">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                          Calc(spin_CostQty.GetText(),spin_CostPrice.GetText(),spin_CostExRate.GetText(),2,spin_CostLocAmt);
	                                                   }" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox runat="server" ID="txt_Cost_GstType" ClientInstanceName="txt_Cost_GstType" Width="60" Text='<%# Bind("GstType")%>'>
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Value="S" Text="S" />
                                                                                        <dxe:ListEditItem Value="Z" Text="Z" />
                                                                                        <dxe:ListEditItem Value="E" Text="E" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>

                                                                            <td>
                                                                                <dxe:ASPxButtonEdit ID="cmb_CostCurrency" ClientInstanceName="cmb_CostCurrency" runat="server" Width="80" Text='<%# Bind("CurrencyId") %>' MaxLength="3">
                                                                                    <Buttons>
                                                                                        <dxe:EditButton Text=".."></dxe:EditButton>
                                                                                    </Buttons>
                                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostCurrency,spin_CostExRate);
                        }" />
                                                                                </dxe:ASPxButtonEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                                                    DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostExRate" ClientInstanceName="spin_CostExRate" Text='<%# Bind("ExRate")%>' Increment="0">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                            Calc(spin_CostQty.GetText(),spin_CostPrice.GetText(),spin_CostExRate.GetText(),2,spin_CostLocAmt);
	                                                   }" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostLocAmt"
                                                                                    BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostLocAmt"
                                                                                    Value='<%# Eval("LocAmt")%>'>
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                            <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
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
                                <dxtc:TabPage Text="Event Log" Name="Activity" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxwgv:ASPxGridView ID="gv_activity" runat="server" ClientInstanceName="gv_activity" AutoGenerateColumns="false" DataSourceID="dsActivity" KeyFieldName="Id" OnBeforePerformDataSelect="gv_activity_BeforePerformDataSelect" Width="850">
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                <Columns>
                                                    <dxwgv:GridViewDataDateColumn FieldName="CreateDateTime" Caption="DateTime" PropertiesDateEdit-DisplayFormatString="yyyy/MM/dd hh:mm" SortOrder="Descending" Width="150"></dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Controller" Caption="Controller" Width="100"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Note"></dxwgv:GridViewDataColumn>
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
                                                        <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Upload Attachments"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false"
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
                                                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                <Settings />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                Enabled='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <a href='<%# Eval("ImgPath")%>' target="_blank">
                                                                            <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                            </dxe:ASPxImage>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContainerNo" Width="200px"></dxwgv:GridViewDataTextColumn>
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
                                <dxtc:TabPage Text="Control" Name="Close File" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl1" runat="server">
                                            <table>
                                                <tr valign="top">
                                                    <td class="lbl">Remark</td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="memo_CLSRMK" ClientInstanceName="memo_CLSRMK" Rows="3" runat="server" Width="600"></dxe:ASPxMemo>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_JobClose" ClientInstanceName="btn_JobClose" runat="server" Text="Close Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>' Width="100">
                                                            <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_JobClose.GetText()+' it?')) {
                                                    grid_job.GetValuesOnCustomCallback('Close',onCallBack);
                                                    }
                                                 }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <%-- <dxe:ASPxButton ID="btn_JobVoid" ClientInstanceName="btn_JobVoid" runat="server" Text="Void" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>' Width="100">
                                                            <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_JobVoid.GetText()+' it?')) {
                                                    grid_job.GetValuesOnCustomCallback('Void',onCallBack);
                                                    }
                                                 }" />
                                                        </dxe:ASPxButton>--%>
                                                        <dxe:ASPxButton ID="btn_controlEventLog" runat="server" Text="Event Log" AutoPostBack="false" Width="100">
                                                            <ClientSideEvents Click="function(s,e){PopupEventLog(txt_JobNo.GetText())}" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" DataSourceID="dsLogEvent"
                                                            KeyFieldName="SequenceId" OnBeforePerformDataSelect="ASPxGridView1_BeforePerformDataSelect"
                                                            AutoGenerateColumns="False">
                                                            <SettingsPager Mode="ShowAllRecords">
                                                            </SettingsPager>
                                                            <Columns>
                                                                <dxwgv:GridViewDataTextColumn Caption="Action" FieldName="JobStatus" VisibleIndex="1" Width="30">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Inoice" FieldName="Value1" VisibleIndex="2">
                                                                    <PropertiesTextEdit DisplayFormatString="#,##0.00"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Ag.Rate" FieldName="Value2" VisibleIndex="2" Visible="false">
                                                                    <PropertiesTextEdit DisplayFormatString="#,##0.00"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="DN" FieldName="Value3" VisibleIndex="2" Visible="false">
                                                                    <PropertiesTextEdit DisplayFormatString="#,##0.00"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Fr.Collect" FieldName="Value4" VisibleIndex="2" Visible="false">
                                                                    <PropertiesTextEdit DisplayFormatString="#,##0.00"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Payment VO" FieldName="Value5" VisibleIndex="2" Visible="false">
                                                                    <PropertiesTextEdit DisplayFormatString="#,##0.00"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="CN" FieldName="Value6" VisibleIndex="2" Visible="false">
                                                                    <PropertiesTextEdit DisplayFormatString="#,##0.00"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Imp.Rate" FieldName="Value7" VisibleIndex="2" Visible="false">
                                                                    <PropertiesTextEdit DisplayFormatString="#,##0.00"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Other" FieldName="Value8" VisibleIndex="2" Visible="false">
                                                                    <PropertiesTextEdit DisplayFormatString="#,##0.00"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="P&L" FieldName="Value8" VisibleIndex="2" Width="100" Visible="false">
                                                                    <DataItemTemplate><%# SafeValue.SafeDecimal(Eval("Value1"),0)+SafeValue.SafeDecimal(Eval("Value2"),0)+SafeValue.SafeDecimal(Eval("Value3"),0)+SafeValue.SafeDecimal(Eval("Value4"),0)-SafeValue.SafeDecimal(Eval("Value5"),0)-SafeValue.SafeDecimal(Eval("Value6"),0)-SafeValue.SafeDecimal(Eval("Value7"),0)-SafeValue.SafeDecimal(Eval("Value8"),0) %></DataItemTemplate>
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="User" FieldName="Controller" VisibleIndex="99" Width="60">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Time" FieldName="CreateDateTime" Width="80" VisibleIndex="100" SortIndex="0" SortOrder="Descending">
                                                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesTextEdit>
                                                                </dxwgv:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dxwgv:ASPxGridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                        </dxtc:ASPxPageControl>
                    </EditForm>
                </Templates>

            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtrPic" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrPic"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                    if(grd_Photo!=null)
                        grd_Photo.Refresh();
                    grid_wh.Refresh();
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
