﻿<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="JobEdit.aspx.cs" Inherits="PagesContTrucking_Job_JobEdit" %>

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
    <script src="../script/jquery.js"></script>
    <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>
    <style type="text/css">
        .show {
            display: block;
        }

        .hide {
            display: none;
        }
    </style>
    <script type="text/javascript">
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }
        var config = {
            timeout: 0,
            gridview: 'grid_Transport',
        }

        $(function () {
            loading.hide();
        })
        function RowClickHandler(s, e) {
            dde_Trip_ContNo.SetText(gridPopCont.cpContN[e.visibleIndex]);
            dde_Trip_ContId.SetText(gridPopCont.cpContId[e.visibleIndex]);
            dde_Trip_ContNo.HideDropDown();
        }
        function onCallBack(v) {
            if (v == "PrintHaulier")
                window.open('/PagesContTrucking/Report/RptPrintView.aspx?doc=haulier&no=' + txt_JobNo.GetText() + "&type=" + cbb_JobType.GetValue());
            if (v != null && v.length > 0) {
                alert(v);
                grid_job.Refresh();
            }
            else {
                grid_job.Refresh();
            }
        }
        function onNoCallBack(v){
            var paras = new Array();
            paras=v.split('_');
            var par = paras[0];
            var jobno = paras[1];
            if(par=="J"){
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?jobNo=" + jobno, { title: jobno, fresh: false, external: true });
            }
            else if(par=="Q"){
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?quoteNo=" + jobno, { title: jobno, fresh: false, external: true });
            }
            if(par=="C"){
                window.location='JobEdit.aspx?quoteNo='+jobno;
            }
        }
        function onCostingCallBack(v) {
            if (v != null && v.length > 0) {
                grid_Cost.Refresh();
            }

        }
        function onGRCallBack(v) {
            if (v != null && v.length > 0) {
                alert("Action Success! No is " + v);
                parent.navTab.openTab(v, "/Modules/WareHouse/Job/DoInEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
            }
        }
        function onDOCallBack(v) {
            if (v != null && v.length > 0) {
                alert("Action Success! No is " + v);
                parent.navTab.openTab(v, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
            }
        }
        var isUpload = false;
        function PopupUploadPhoto() {
            popubCtrPic.SetHeaderText('Upload Attachment');
            popubCtrPic.SetContentUrl('../Upload.aspx?Type=CTM&Sn=' + txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function PopupJobRate() {
            popubCtr.SetHeaderText('Job Rate ');
            popubCtr.SetContentUrl('../SelectPage/SelectJobRate.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + btn_ClientId.GetText());
            popubCtr.Show();
        }
        function PopupJobHouse() {
            popubCtrPic.SetHeaderText('Stock List');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/SelectStock.aspx?no=' + txt_JobNo.GetText() + "&client=" + btn_ClientId.GetText()+"&jobType="+ cbb_JobType.GetValue());
            popubCtrPic.Show();
        }
        function AfterPopub(){
            popubCtrPic.Hide();
            popubCtrPic.SetContentUrl('about:blank');

        }
        function AfterPopubRate(){
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid1.Refresh();
        }
        function ShowEmail() {
            var value = btn_SendEmail.GetText();
            if (value == "Send Email") {
                document.getElementById('email').style.display = 'block';
                btn_SendEmail.SetText('Close');
            }
            else {
                document.getElementById('email').style.display = 'none';
                btn_SendEmail.SetText('Send Email');
            }
            
        }
        function OnSendEmailCallBack(v) {
            if (v == "Success") {
                alert("Send Success!");
            } else {
                alert("Pls keyin Email To");
            }
        }
        function charge_onCallBack(v) {
            if (v != null && v.length > 0) {
                if (v == 'refresh') {
                    gv_charge.Refresh();
                }
            }
        }
        function trip_callback(v) {
            //if (grid_Trip) {
            //    grid_Trip.CancelEdit();
            //}
            //alert(v);
            //console.log(v);
            var ar = v.split(',');
            var detail = {
                controller: ar[0],
                driver: ar[1],
                no: ar[2]
            }
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
        }
        function cont_trip_callback(v) {
            //trip_callback(v);

            setTimeout(function () {

                gv_cont_trip.Refresh();
                //if (ar[3] && ar[3] == "RefreshContainer") {
                //    grid_job.Refresh();
                //} else {
                //    gv_cont_trip.Refresh();
                //}
            }, 500);
            var ar = v.split(',');
            var detail = {

            }
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
            gv_cont_trip.CancelEdit();
        }
        function trip_delete_callback(v) {
            console.log(v);
        }
        //SV_Firebase.publish_system_msg_send('refresh', 'schedule', JSON.stringify({ controller: "BULL", driver: "BULL", no: "90" }));
        function container_batch_add() {
            var jobno = txt_JobNo.GetText();
            //console.log(jobno);
            Popup_ContainerBatchAdd(jobno, container_batch_add_cb);
        }
        function container_batch_add_cb(v) {
            console.log(v);
            if (v == "success") {
                //if (grid_Trip) {
                //    grid_Trip.CancelEdit();
                //}
                grid_Cont.CancelEdit();
                setTimeout(function () {
                    grid_Cont.Refresh();
                }, 500);
            }
        }
        function container_trip_add_cb(v) {
            if (v == "success") {
                //if (grid_Trip) {
                //    grid_Trip.CancelEdit();
                //}
                gv_cont_trip.Refresh();
                //grid_Cont.CancelEdit();
                //grid_Cont.Refresh();
                cbb_StatusCode.SetValue('InTransit');
                //console.log(gv_cont_trip);
                //setTimeout(function () {
                //    gv_cont_trip.StartEditRow(gv_cont_trip.pageRowCount - 1);
                //}, 800);

                var detail = {
                }
                console.log('=========', detail);
                SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
            }
        }
        function container_trip_update_cb(v) {
            gv_cont_trip.CancelEdit();
            setTimeout(function () {
                gv_cont_trip.Refresh();
                //if (grid_Trip) {
                //    grid_Trip.CancelEdit();
                //}
            }, 500);
            var ar = v.split(',');
            var driver = ",";
            for (var i = 2; i < ar.length; i++) {
                driver = driver + ar[i] + ',';
            }
            var detail = {
                controller: ar[0],
                no: ar[1],
                driver: driver,
            }
            console.log('=========', detail);
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));

        }
        function container_trip_delete_cb(v) {

            gv_cont_trip.CancelEdit();
            setTimeout(function () {
                //if (grid_Trip) {
                //    grid_Trip.CancelEdit();
                //}
                gv_cont_trip.Refresh();
            }, 500);
            var ar = v.split(',');
            var driver = ",";
            for (var i = 2; i < ar.length; i++) {
                driver = driver + ar[i] + ',';
            }
            var detail = {
                controller: ar[0],
                no: ar[1],
                driver: driver,
            }
            console.log('=========', detail);
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
        }
        function trip_update_cb(v) {
            //if (grid_Trip) {
            //    grid_Trip.CancelEdit();
            //}
            var ar = v.split(',');
            var driver = ",";
            for (var i = 2; i < ar.length; i++) {
                driver = driver + ar[i] + ',';
            }
            var detail = {
                controller: ar[0],
                no: ar[1],
                driver: driver,
            }
            console.log('=========', detail);
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));

        }
        function trip_delete_cb(v) {
            //if (grid_Trip) {
            //    grid_Trip.CancelEdit();
            //}
            var ar = v.split(',');
            var driver = ",";
            for (var i = 2; i < ar.length; i++) {
                driver = driver + ar[i] + ',';
            }
            var detail = {
                controller: ar[0],
                no: ar[1],
                driver: driver,
            }
            console.log('=========', detail);
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
        }

        var ContainerTripIndex = 0;

        function printJobSheet() {
            window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=4&no=" + txt_search_JobNo.GetText());
        }
        function printJobCost() {
            window.open("/PagesContTrucking/PrintJob.aspx?o=" + txt_search_JobNo.GetText());
        }
        function printTallySheetIndented() {
            window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=tallysheet_indented&no=" + txt_search_JobNo.GetText());
        }
        function printTallySheetConfirmed() {
            window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=tallysheet_confirmed&no=" + txt_search_JobNo.GetText());
        }
        function onGRCallBack(v) {
            if (v != null && v.length > 0) {
                grid_wh.Refresh();
                alert("Action Success! No is " + v);
                parent.navTab.openTab(v, "/Modules/WareHouse/Job/DoInEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
            }
        }
        function onDOCallBack(v) {
            if (v != null && v.length > 0) {
                grid_wh.Refresh();
                alert("Action Success! No is " + v);
                parent.navTab.openTab(v, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
            }
        }
        function ShowGR(masterId) {
            parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoInEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
            //window.location = "DoInEdit.aspx?no=" + masterId;
        }
        function ShowDO(masterId) {
            parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
            //window.location = "DoOutEdit.aspx?no=" + masterId;
        }
        function upload_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Uploadline_'+rowIndex, upload_inline_callback);
            }, config.timeout);
        }
        function upload_inline_callback(res){
            popubCtrPic.SetHeaderText('Upload Attachment');
            popubCtrPic.SetContentUrl('../Upload.aspx?Type=CTM&Sn=' +res);
            popubCtrPic.Show();
        }
        function PrintQuotation(invN, docType) {
            window.open('/ReportFreightSea/printview.aspx?document=Quotation&master=' + txt_QuoteNo.GetText() + '&docType=' + cmb_JobStatus.GetText());
            
        }
        function PopupMasterRate(){
            popubCtr1.SetHeaderText('Bill Rate');
            popubCtr1.SetContentUrl('/PagesContTrucking/SelectPage/SelectRateForQuotation.aspx?quoteNo='+ txt_QuoteNo.GetText()+'&client='+btn_ClientId.GetText());
            popubCtr1.Show();
        }
        function PopupUom(codeId, typ) {
            clientId = codeId;
            popubCtr.SetHeaderText('UOM');
            popubCtr.SetContentUrl('/SelectPage/UomList.aspx?type=' + typ);
            popubCtr.Show();
        }
        function PopupRate() {
            popubCtr.SetHeaderText('Chg Code');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectChgCode.aspx?no=' +txt_QuoteNo.GetText()+'&client='+btn_ClientId.GetText());
            popubCtr.Show();
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
        <wilson:DataSource ID="dsTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
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
        <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="" />
        <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RateType" KeyMember="Id" />
        <div>
            <table>
                <tr>
                    <td class=lbl>Job/Quote No</td>
                    <td class=ctl>
                        <dxe:ASPxTextBox ID="txt_search_JobNo" ClientInstanceName="txt_search_JobNo" runat="server" Width="100%"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <div id="job" runat="server">
                            <dxe:ASPxButton ID="btn_Retrieve" runat="server" Text="Retrieve" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){
                        window.location='JobEdit.aspx?jobNo='+txt_search_JobNo.GetText();
                    }" />
                            </dxe:ASPxButton>
                        </div>
                        <div>
                             <div id="quote" runat="server">
                            <dxe:ASPxButton ID="ASPxButton13" runat="server" Text="Retrieve" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){
                        window.location='JobEdit.aspx?quoteNo='+txt_search_JobNo.GetText();
                    }" />
                            </dxe:ASPxButton>
                        </div>
                        </div>
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
                                    <table style="padding:0px">
                                        <tr>
                                            <td style="font-weight:bold"><dxe:ASPxLabel ID="lbl_JobType0" runat="server" Font-Size="Large" Text="Type:"></dxe:ASPxLabel></td>
                                           <td  style="font-weight:bold">
                                               
                                               <dxe:ASPxLabel ID="lbl_JobType" runat="server" Font-Size="Large" Text='<%# Eval("JobType") %>'></dxe:ASPxLabel>
                                               <div style="display: none">
                                                   <dxe:ASPxComboBox ID="cbb_JobType" ClientInstanceName="cbb_JobType" runat="server" Value='<%# Eval("JobType") %>' Width="80">
                                                       <Items>
                                                           <dxe:ListEditItem Text="IMP" Value="IMP" />
                                                           <dxe:ListEditItem Text="EXP" Value="EXP" />
                                                           <dxe:ListEditItem Text="LOC" Value="LOC" />
                                                       </Items>
                                                   </dxe:ASPxComboBox>
                                               </div>
                                           </td>
                                            <td width="40%">

                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_Confirm" ClientInstanceName="btn_Confirm" runat="server" Text="Confirm Quotation" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm(btn_Confirm.GetText()+'?')) {
                                                        grid_job.GetValuesOnCustomCallback('ConfirmQ',onCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_Completed" ClientInstanceName="btn_Completed" runat="server" Text="Close Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"USE")!="Completed" %>' Width="100">
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
                                            <td>
                                                <dxe:ASPxButton ID="btn_AddRate" runat="server" Text="Print Quotation" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){
                                                      PrintQuotation();
                                                    }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td width="10%">

                                            </td>
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
                                                <dxe:ASPxButton ID="btn_PrintJobSheet" runat="server" Text="Print JobSheet" AutoPostBack="false" Width="100">
                                                    <ClientSideEvents Click="function(s,e){
                                                         grid_job.GetValuesOnCustomCallback('PrintHaulier',onCallBack);
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
                            <table >
                                <tr>
                                    <td class=lbl>Job No</td>
                                    <td class=ctl2>
                                        <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" runat="server" ReadOnly="true" BackColor="Control" Text='<%# Eval("JobNo") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>
                                    
                                    <td class=ctl2>
                                        <dxe:ASPxDateEdit ID="txt_JobDate" runat="server" Value='<%# Eval("JobDate") %>' Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>
                                    <td class=lbl2>Status
                                    </td>
                                    <td class=ctl2>
                                        <dxe:ASPxComboBox ID="cmb_JobStatus" ClientInstanceName="cmb_JobStatus" OnCustomJSProperties="cmb_JobStatus_CustomJSProperties" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("JobStatus") %>' Width="100%">
                                            <Items>
                                                <dxe:ListEditItem Text="Quoted" Value="Quoted" />
                                                <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                                <dxe:ListEditItem Text="Completed" Value="Completed" />
                                                <dxe:ListEditItem Text="Voided" Value="Voided" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                   
                                    <td class=lbl>Quotation No</td>
                                    <td class=ctl2>
                                        <dxe:ASPxTextBox ID="txt_QuoteNo" ClientInstanceName="txt_QuoteNo" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("QuoteNo") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>
                                
                                    <td class=ctl2>
                                        <dxe:ASPxDateEdit ID="date_QuoteDate" runat="server" Value='<%# Eval("QuoteDate") %>' Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>

                                    <td class=lbl2>Stage</td>
                                    <td class=ctl2>
                                        <dxe:ASPxComboBox ID="cbb_QuoteStatus" runat="server" Value='<%# Eval("QuoteStatus") %>' Width="100%">
                                            <Items>
                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                <dxe:ListEditItem Text="Closed" Value="Closed" />
                                                <dxe:ListEditItem Text="Failed" Value="Failed" />
                                                <dxe:ListEditItem Text="Voided" Value="Voided" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
							</table>
						</div>
                        <div style="margin-top:6px; padding: 10px; background-color: #FFFFFF; border: 1px solid #A8A8A8; white-space: nowrap">
                            <table >
                                <tr>
                                    <td class=lbl>Client</td>
                                    <td class=ctl>
                                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Text='<%# Eval("ClientId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td class=lbl>Sub Client
                                    </td>
                                    <td class=ctl>
                                        <dxe:ASPxButtonEdit ID="btn_SubClientId" ClientInstanceName="btn_SubClientId" runat="server" Text='<%# Eval("SubClientId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParentParty(btn_SubClientId,null,btn_ClientId.GetText());
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                     <td class=lbl>Contact</td>
                                    <td  class=ctl>
                                        <dxe:ASPxTextBox ID="txt_ClientContact" runat="server" Text='<%# Eval("ClientContact") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>
                                    <td class=lbl>Client Ref No
                                    </td>
                                    <td class=ctl>
                                        <dxe:ASPxTextBox ID="txt_ClientRefNo" runat="server" Text='<%# Eval("ClientRefNo") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3">
                                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                    </td>
                                     <td class=lbl>Email
                                    </td>
                                    <td colspan="3">
                                        <dxe:ASPxTextBox ID="txt_notifiEmail" runat="server" Width="100%" Value='<%# Eval("EmailAddress") %>'></dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <table>
                                    <tr style="display: none">
                                        <td colspan="10">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>Trucking</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cmb_IsTrucking" runat="server" Value='<%# Eval("IsTrucking") %>' Width="100">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td></td>
                                                    <td>Warehouse</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cmb_IsWarehouse" runat="server" Value='<%# Eval("IsWarehouse") %>' Width="100">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>Freight</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cmb_IsFreight" runat="server" Value='<%# Eval("IsFreight") %>' Width="100">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>Local</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cmb_IsLocal" runat="server" Value='<%# Eval("IsLocal") %>' Width="100">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>Adhoc</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cmb_IsAdhoc" runat="server" Value='<%# Eval("IsAdhoc") %>' Width="100">
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
                                                
                                                <tr>
                                                    <td class=lbl>Vessel</td>
                                                    <td class=ctl>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Ves" Text='<%# Eval("Vessel")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td class=lbl>Voyage
                                                    </td>
                                                    <td class=ctl>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Voy" Text='<%# Eval("Voyage")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td class=lbl>ETA
                                                    </td>
                                                    <td class=ctl>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="date_Eta" Width="100" runat="server" Value='<%# Eval("EtaDate")%>'
                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_EtaTime" runat="server" Text='<%# Eval("EtaTime") %>' Width="60">
                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td class=lbl>Terminal</td>
                                                    <td class=ctl>
                                                        <dxe:ASPxComboBox ID="cbb_Terminal" runat="server" Value='<%# Eval("Terminalcode") %>' Width="100%" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" DataSourceID="dsTerminal" ValueField="Code" TextField="Name"></dxe:ASPxComboBox>
                                                    </td>
                                                    <td class=lbl>Ref No</td>
                                                    <td class=ctl>
                                                        <dxe:ASPxTextBox ID="txt_CarrierBkgNo" runat="server" Text='<%# Eval("CarrierBkgNo") %>' Width="170"></dxe:ASPxTextBox>
                                                    </td>
                                                    <td class=lbl>Operator Code
                                                    </td>
                                                    <td class=ctl>
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
                                                    <td class=lbl>POL
                                                    </td>
                                                    <td class=ctl>
                                                        <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Width="100%" HorizontalAlign="Left" AutoPostBack="False"
                                                            MaxLength="5" Text='<%# Eval("Pol")%>'>
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pol);
                                                                    }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td class=lbl>POD
                                                    </td>
                                                    <td class=ctl>
                                                        <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Width="100%" HorizontalAlign="Left" AutoPostBack="False"
                                                            MaxLength="5" Text='<%# Eval("Pod")%>'>
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pod);
                                                                    }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td class=lbl>Shipper
                                                    </td>
                                                    <td class=ctl>
                                                        <dxe:ASPxTextBox ID="txt_WarehouseAddress" runat="server" Text='<%# Eval("WarehouseAddress") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
												
												   <tr>
                                                    <td class=lbl>Carrier
                                                    </td>
                                                    <td colspan="3" style="padding: 0px;">
                                                        <table>
                                                            <tr>
                                                                <td style="padding: 0px;">
                                                                    <dxe:ASPxButtonEdit ID="btn_CarrierId" ClientInstanceName="btn_CarrierId" runat="server" Text='<%# Eval("CarrierId") %>' Width="120" AutoPostBack="False" ReadOnly="true">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_CarrierId,txt_CarrierName);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td style="padding: 0px;">
                                                                    <dxe:ASPxTextBox ID="txt_CarrierName" ClientInstanceName="txt_CarrierName" runat="server" Width="220" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class=lbl>Contractor
                                                    </td>
                                                    <td colspan="3" style="padding: 0px;">
                                                        <table>
                                                            <tr>
                                                                <td style="padding: 0px;">
                                                                    <dxe:ASPxButtonEdit ID="btn_HaulierId" ClientInstanceName="btn_HaulierId" runat="server" Text='<%# Eval("HaulierId") %>' Width="120" AutoPostBack="False" ReadOnly="true">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_HaulierId,txt_HaulierName);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td style="padding: 0px;">
                                                                    <dxe:ASPxTextBox ID="txt_HaulierName" ClientInstanceName="txt_HaulierName" runat="server" Width="220" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class=lbl>Sub-Contract ?</td>
                                                    <td class=ctl>
                                                        <dxe:ASPxComboBox ID="cbb_Contractor" runat="server" Value='<%# Eval("Contractor") %>' Width="100%" DropDownStyle="DropDownList">
                                                            <Items>
                                                                <dxe:ListEditItem Value="YES" Text="YES" />
                                                                <dxe:ListEditItem Value="NO" Text="NO" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>

                                                </tr>
                                              
                                             
                                                <tr>
                                                    <td  class=lbl  >
                                                       
                                                        <a href="#" onclick="PopupCustAdr(null,txt_PickupFrom);">Pick From</a>
                                                    </td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_PickupFrom" Rows="3" ClientInstanceName="txt_PickupFrom" runat="server"
                                                            Width="100%" Text='<%# Eval("PickupFrom") %>'>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                    <td  class=lbl >
                                                        
                                                        <a href="#" onclick="PopupCustAdr(null,txt_DeliveryTo);">Delivery To</a>
                                                    </td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_DeliveryTo" Rows="3" ClientInstanceName="txt_DeliveryTo" runat="server"
                                                            Width="100%" Text='<%# Eval("DeliveryTo") %>'>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td  class=lbl  >
                                                        <a href="#" onclick="PopupCustAdr(null,txt_YardRef);">Depot Address</a>
                                                    </td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo runat="server" Rows="4" ID="txt_YardRef" ClientInstanceName="txt_YardRef" Value='<%# Eval("YardRef") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td  class=lbl >Special Instruction</td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_SpecialInstruction" Rows="3" runat="server" Text='<%# Eval("SpecialInstruction") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class=lbl  >Remark</td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_Remark" Rows="3" runat="server" Text='<%# Eval("Remark") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                    <td class=lbl  >Permit No</td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_PermitNo" Rows="3" runat="server" Text='<%# Eval("PermitNo") %>' Width="100%">
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
                                                            <dxe:ASPxButton ID="btn_ContAdd" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Add Container" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                grid_Cont.AddNewRow();
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_ContBatchAdd" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Batch Add" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){container_batch_add();}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <dxwgv:ASPxGridView ID="grid_Cont" ClientInstanceName="grid_Cont" runat="server" KeyFieldName="Id" DataSourceID="dsCont" Width="800px" AutoGenerateColumns="False" OnInit="grid_Cont_Init" OnBeforePerformDataSelect="grid_Cont_BeforePerformDataSelect" OnRowDeleting="grid_Cont_RowDeleting" OnInitNewRow="grid_Cont_InitNewRow" OnRowInserting="grid_Cont_RowInserting" OnRowUpdating="grid_Cont_RowUpdating" OnRowDeleted="grid_Cont_RowDeleted" OnRowUpdated="grid_Cont_RowUpdated" OnRowInserted="grid_Cont_RowInserted" OnCustomDataCallback="grid_Cont_CustomDataCallback">
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
                                                                    <td>ContainerNo</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ContainerNo") %>' AutoPostBack="False" Width="165">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo,txt_ContType);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>SealNo</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_SealNo" runat="server" Text='<%# Bind("SealNo") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>ContType</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="165" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContainerType") %>'></dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>OOG Ind
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_oogInd" runat="server" Text='<%# Bind("oogInd") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Discharge Cell
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_dischargeCell" ClientInstanceName="txt_dischargeCell" runat="server" Text='<%# Bind("DischargeCell") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>B/R</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_BR" ClientInstanceName="txt_BR" runat="server" Text='<%# Bind("Br") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Weight
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_Wt" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Volume
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_M3" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Qty</td>
                                                                    <td>
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
                                                                    <td>Trucking/Schedule Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_Schedule" runat="server" Width="165" Value='<%# Bind("ScheduleDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Dg Class</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="txt_DgClass" Text='<%# Bind("DgClass")%>'></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>TruckingStatus</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_StatusCode" ClientInstanceName="cbb_StatusCode" runat="server" Width="165" Value='<%# Bind("StatusCode") %>'>
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
                                                                    <%--                                                                    <td>DG / F5</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Width="165" Value='<%# Bind("F5Ind") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>--%>
                                                                    <td>Permit</td>
                                                                    <td>
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
                                                                    <td>Urgent Job</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_UrgentInd" runat="server" Width="165" Value='<%# Bind("UrgentInd") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>PortnetStatus</td>
                                                                    <td>
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

                                                                    <td>TerminalLocation</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_TerminalLocation" ClientInstanceName="txt_TerminalLocation" runat="server" Text='<%# Bind("TerminalLocation") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>MT/LDN</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_warehouse_status" runat="server" Text='<%# Bind("WarehouseStatus") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Empty" Text="Empty" />
                                                                                <dxe:ListEditItem Value="Laden" Text="Laden" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td>DG / J5</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Width="165" Value='<%# Bind("F5Ind") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="Y" />
                                                                                <dxe:ListEditItem Value="N" Text="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Email Sent</td>
                                                                    <td>
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

                                                                    <td>ScheduleStartDate
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_ScheduleStartDate" runat="server" Width="100%" Value='<%# Bind("ScheduleStartDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="date_ScheduleStartTime" runat="server" Text='<%# Bind("ScheduleStartTime") %>' Width="100%">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>WarehouseStatus</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_CfsStatus" runat="server" Text='<%# Bind("CfsStatus") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Pending" Text="Pending" />
                                                                                <dxe:ListEditItem Value="Started" Text="Started" />
                                                                                <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>


                                                                </tr>
                                                                <tr>
                                                                    <td>CompletionDate
                                                                    </td>
                                                                    <td>

                                                                        <dxe:ASPxDateEdit ID="date_CompletionDate" runat="server" Width="100%" Value='<%# Bind("CompletionDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
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
                                                                    <td style="vertical-align: central">
                                                                        <a href="#" onclick="PopupCustAdr(null,txt_YardAddress);">Depot Address</a>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <%--<dxe:ASPxTextBox ID="txt_YardAddress" runat="server" ClientInstanceName="txt_YardAddress" Text='<%# Bind("YardAddress") %>' Width="100%"></dxe:ASPxTextBox>--%>
                                                                        <dxe:ASPxMemo ID="txt_YardAddress" ClientInstanceName="txt_YardAddress" Rows="3" runat="server" Text='<%# Bind("YardAddress") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Remark</td>
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
                                                                                <td>Trip Type</td>
                                                                                <td>
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
                                                                                <td>PrimeMover</td>
                                                                                <td>
                                                                                    <dxe:ASPxButtonEdit ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheadCode") %>' AutoPostBack="False" Width="165">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(btn_TowheadCode,null);
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
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
                                                                                <td>Double Mounting</td>
                                                                                <td>
                                                                                    <dxe:ASPxComboBox ID="cmb_DoubleMounting" runat="server" Value='<%# Bind("DoubleMounting") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                            <dxe:ListEditItem Value="No" Text="No" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
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
                                                                            </tr>
                                                                            <tr>
                                                                                <td>From Date</td>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="txt_FromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>Time</td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="161">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>To</td>
                                                                                <td colspan="3">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td>
                                                                                    <%--<a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>--%>
                                                                                    <a href="#" onclick="PopupParkingLot(txt_ToPL,txt_Trip_ToCode);">To Parking Lot</a>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_ToPL" ClientInstanceName="txt_ToPL" runat="server" Text='<%# Bind("ToParkingLot") %>' Width="165">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>ToDate</td>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>Time</td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="161">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Instruction</td>
                                                                                <td colspan="3">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="414">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>


                                                                            <tr>
                                                                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Input</td>
                                                                            </tr>
                                                                            <tr>

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
                                                                            </tr>

                                                                            <tr>
                                                                                <td>DriverRemark</td>
                                                                                <td colspan="3">
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
                                <dxtc:TabPage Text="Trip" Visible="false">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <dxe:ASPxButton ID="btn_TripAdd" runat="server" Text="Add Trip" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){
                                grid_Trip.AddNewRow();
                                }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="grid_Trip" ClientInstanceName="grid_Trip" runat="server" Width="950px" DataSourceID="dsTrip" KeyFieldName="Id" OnInit="grid_Trip_Init" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_Trip_BeforePerformDataSelect" OnHtmlEditFormCreated="grid_Trip_HtmlEditFormCreated" OnRowInserting="grid_Trip_RowInserting" OnInitNewRow="grid_Trip_InitNewRow" OnRowDeleting="grid_Trip_RowDeleting" OnRowUpdating="grid_Trip_RowUpdating" OnRowDeleted="grid_Trip_RowDeleted" OnRowInserted="grid_Trip_RowInserted" OnRowUpdated="grid_Trip_RowUpdated" OnCustomDataCallback="grid_Trip_CustomDataCallback">
                                                    <SettingsPager PageSize="100" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='<%# "grid_Trip.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>

                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="12" Width="10%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Trip.GetValuesOnCustomCallback(\"Delete_"+Container.KeyValue+"\",trip_delete_cb);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="Statuscode" Caption="Satus"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToCode" Caption="Destination"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="TowheadCode" Caption="PrimeMover"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>

                                                        <dxwgv:GridViewDataDateColumn VisibleIndex="9" Width="100" FieldName="FromDate" Caption="Date">
                                                            <DataItemTemplate><%# SafeValue.SafeDate( Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM/yyyy")+"&nbsp;"+Eval("FromTime") %></DataItemTemplate>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="FromTime" Caption="Time">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="7" FieldName="ToTime" Caption="Time">
                                                        </dxwgv:GridViewDataColumn>--%>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="Incentive1" Caption="Total Incentive">
                                                            <DataItemTemplate><%# SafeValue.SafeDecimal(Eval("Incentive1"))+SafeValue.SafeDecimal(Eval("Incentive2"))+SafeValue.SafeDecimal(Eval("Incentive3"))+SafeValue.SafeDecimal(Eval("Incentive4")) %></DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="Incentive3" Caption="Standby">
                                                            <DataItemTemplate><%# SafeValue.SafeDecimal(Eval("Incentive3")) %></DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>--%>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="Charge1" Caption="Total Surcharge">
                                                            <DataItemTemplate><%# SafeValue.SafeDecimal(Eval("Charge1"))+SafeValue.SafeDecimal(Eval("Charge2"))+SafeValue.SafeDecimal(Eval("Charge3"))+SafeValue.SafeDecimal(Eval("Charge4"))+SafeValue.SafeDecimal(Eval("Charge5"))+SafeValue.SafeDecimal(Eval("Charge6"))+SafeValue.SafeDecimal(Eval("Charge7"))+SafeValue.SafeDecimal(Eval("Charge8"))+SafeValue.SafeDecimal(Eval("Charge9"))+SafeValue.SafeDecimal(Eval("Charge10")) %></DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
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
                                                                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                                                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                                                                <dxe:ListEditItem Text="COL" Value="COL" />
                                                                                <dxe:ListEditItem Text="RET" Value="RET" />
                                                                                <dxe:ListEditItem Text="LOC" Value="LOC" />
                                                                                <dxe:ListEditItem Text="SHF" Value="SHF" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
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
                                                                    <td>PrimeMover</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheadCode") %>' AutoPostBack="False" Width="165">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(btn_TowheadCode,null);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
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
                                                                </tr>
                                                                <tr>
                                                                    <td>From Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="txt_FromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="161">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>To</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="414">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>
                                                                        <%--<a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>--%>
                                                                        <a href="#" onclick="PopupParkingLot(txt_ToPL,txt_Trip_ToCode);">To Parking Lot</a>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_ToPL" ClientInstanceName="txt_ToPL" runat="server" Text='<%# Bind("ToParkingLot") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>ToDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Time</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="161">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Instruction</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="414">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>


                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Input</td>
                                                                </tr>
                                                                <tr>

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
                                                                </tr>



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
                                                                        <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="414">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>

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
                                                                    <td>Standby(hr)</td>
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
                                                                </tr>
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
                                            <table style="width:100%">
                                                <tr style="vertical-align:top;">
                                                    <td>
                                                        <a href="#" onclick="javasrcipt:PopupRemark(txt_JobDes,null)">Job Desrciption
                                                        </a>

                                                    </td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txt_JobDes" ClientInstanceName="txt_JobDes" Rows="3" runat="server" Text='<%# Eval("JobDes") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td><a href="#" onclick="javasrcipt:PopupRemark(txt_QuoteRemark,null)">Quote Remark
                                                    </a></td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txt_QuoteRemark" ClientInstanceName="txt_QuoteRemark" Rows="3" runat="server" Text='<%# Eval("QuoteRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
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
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_SendEmail" ClientInstanceName="btn_SendEmail" Width="120" runat="server" Text="Email Quotation"
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e){
                                            ShowEmail();
                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="8">
                                                        <div id="email" style="display: none">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>Email To</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_Email1" ClientInstanceName="cbb_Email1" runat="server" DropDownStyle="DropDown">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Email CC</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_Email2" runat="server"></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Email BCC</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_Email3" runat="server" Width="150"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Email Subject
                                                                    </td>
                                                                    <td colspan="5">
                                                                        <dxe:ASPxTextBox ID="txt_Subject" runat="server" Width="680"></dxe:ASPxTextBox>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Email Message
                                                                    </td>
                                                                    <td colspan="5">
                                                                        <dxe:ASPxMemo ID="memo_message" runat="server" Rows="3" Width="680"></dxe:ASPxMemo>
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
                                                    DataSourceID="ds1" KeyFieldName="Id" OnRowUpdating="Grid1_RowUpdating" OnRowDeleting="Grid1_RowDeleting"
                                                    OnRowInserting="Grid1_RowInserting" OnInitNewRow="Grid1_InitNewRow" OnBeforePerformDataSelect="grid1_BeforePerformDataSelect"
                                                    OnInit="Grid1_Init" Width="1300px" AutoGenerateColumns="False">
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsPager Mode="ShowPager" PageSize="10" />
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                                                            <EditButton Visible="True" />
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="999" Width="5%">
                                                            <DeleteButton Visible="true" />
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Update" FieldName="RowUpdateTime" VisibleIndex="998"
                                                            Width="140">
                                                            <DataItemTemplate>
                                                                <%# Eval("RowUpdateUser","{0}") %> <%# Eval("RowUpdateTime","{0:dd/MM#HH:mm}") %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="LineStatus" Caption="Optional" Width="50" VisibleIndex="98">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Y" Value="Y" />
                                                                    <dxe:ListEditItem Text="N" Value="N" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="ChgCode" Caption="ChargeCode" Width="150" VisibleIndex="1">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxComboBox ID="txt_ChgCode" ClientInstanceName="txt_ChgCode" Width="100" DataSourceID="dsRate" TextField="ChgCodeDe" runat="server" ValueField="ChgCodeId" ValueType="System.String" Value='<%# Bind("ChgCode") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                                                </dxe:ASPxComboBox>
                                                            </EditItemTemplate>
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", Eval("ChgCode")))) %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                                                            Width="150">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                                                            Width="150">
                                                        </dxwgv:GridViewDataTextColumn>
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
                                                         <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="5"
                                                            Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Rate" FieldName="Price" VisibleIndex="5"
                                                            Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFooter="true" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                                    </TotalSummary>

                                                </dxwgv:ASPxGridView>
                                            </div>
                                            <table style=" width: 1000px; vertical-align:top">
                                                <tr style="vertical-align:top;">
                                                    <td>
                                                        <a href="#" onclick="javasrcipt:PopupRemark(txt_TerminalRemark,null)">
                                                       Terms & Conditions
                                                        </a> 
                                                    </td>
                                                    <td>
                                                       <dxe:ASPxMemo ID="txt_TerminalRemark" ClientInstanceName="txt_TerminalRemark" Rows="3" runat="server" Text='<%# Eval("TerminalRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td>
                                                        <a href="#" onclick="javasrcipt:PopupRemark(memo_InternalRmark,null)">Internal Remark
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="memo_InternalRmark" ClientInstanceName="memo_InternalRmark" Rows="3" runat="server" Text='<%# Eval("InternalRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align:top;">
                                                    <td><a href="#" onclick="javasrcipt:PopupRemark(memo_LumSumRemark,null)">LumSum Remark
                                                    </a></td>
                                                    <td>
                                                       <dxe:ASPxMemo ID="memo_LumSumRemark" ClientInstanceName="memo_LumSumRemark" Rows="3" runat="server" Text='<%# Eval("LumSumRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td>
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
                                <dxtc:TabPage Text="Costing" Visible="true" Name="Costing">
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
                                            <div class='<%# SafeValue.SafeString(Eval("JobStatus"),"")=="Quoted"?"hide":"hide" %>'>
                                                <dxe:ASPxButton ID="ASPxButton9" Width="150" runat="server" Text="Add Rate" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        grid_Cost.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                            </div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton11" Width="150" runat="server" Text="Import Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                         grid_job.GetValuesOnCustomCallback('Costing',onCallBack);
                                                        }" />
                                                </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
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
                                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Cost.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Trip" FieldName="TripNo" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
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
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Amt" FieldName="LocAmt" VisibleIndex="5" Width="50">
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
                                                                <td>Remark
                                                                </td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="spin_CostRmk" Text='<%# Bind("Remark") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td colspan="4">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td>Cont No</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_ContNo" Text='<%# Bind("ContNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>Cont Type</td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContType") %>'></dxe:ASPxComboBox>

                                                                            </td>
                                                                        </tr>
                                                                    </table>
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
                                                                            <td></td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostCostQty"
                                                                                    ID="spin_CostCostQty" Text='<%# Bind("Qty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostPrice"
                                                                                    runat="server" Width="100" ID="spin_CostCostPrice" Value='<%# Bind("Price")%>' Increment="0" DecimalPlaces="2">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" runat="server" Width="80" Text='<%# Bind("CurrencyId") %>' MaxLength="3">
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
                                                                                    DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostExRate" ClientInstanceName="spin_CostCostExRate" Text='<%# Bind("ExRate")%>' Increment="0">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostAmt"
                                                                                    BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
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
                                <dxtc:TabPage Text="Warehouse" Visible="true" Name="Warehouse">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class='<%# SafeValue.SafeString(Eval("JobType"))=="IMP"?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                       grid_wh.AddNewRow();
                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>

                                                    </td>
                                                    <td>
                                                        <div class='<%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Add From Stock" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                       PopupJobHouse();
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
                                                        <dxe:ASPxButton ID="ASPxButton15" runat="server" Text="Print Tallysheet Confirmed" AutoPostBack="false" Width="100">
                                                            <ClientSideEvents Click="function(s,e){
                                                                        printTallySheetConfirmed();
                                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td width="90%"></td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>Warehouse</td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="txt_WareHouseId" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_WareHouseId" runat="server" Text='<%# Eval("WareHouseCode") %>' Width="170" AutoPostBack="False">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupWh(txt_WareHouseId,null);
                                                                        }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td>PermitNo</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_wh_PermitNo" Width="100%" runat="server" Text='<%# Eval("WhPermitNo") %>'></dxe:ASPxTextBox>
                                                    </td>
                                                    <td>IncoTerms</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbb_IncoTerms" Width="150" DataSourceID="dsIncoTerms" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Eval("IncoTerm") %>' DropDownStyle="DropDown">
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>PermitDate</td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="date_PermitDate" runat="server" Value='<%# Eval("PermitDate") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Permit By</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="txt_PermitBy" Width="170" runat="server" Value='<%# Eval("PermitBy") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Englee" Value="Englee" />
                                                                <dxe:ListEditItem Text="Client" Value="Client" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>Supp. InvNo  </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_PartyInvNo" Width="100%" runat="server" Text='<%# Eval("PartyInvNo") %>'></dxe:ASPxTextBox>
                                                    </td>
                                                    <td>GstAmt
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="150"
                                                            ID="spin_GstAmt" Height="21px" Value='<%# Bind("GstAmt")%>' DecimalPlaces="3" Increment="0">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>Payment Status</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbb_PaymentStatus" Width="120" runat="server" Value='<%# Eval("PaymentStatus") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Paid" Value="Paid" />
                                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                            <div style="overflow-y: auto; width: 1000px">
                                                <dxwgv:ASPxGridView ID="grid_wh" ClientInstanceName="grid_wh" runat="server" DataSourceID="dsWh" OnInit="grid_wh_Init" OnInitNewRow="grid_wh_InitNewRow"
                                                    OnRowInserting="grid_wh_RowInserting" OnRowUpdating="grid_wh_RowUpdating" OnRowDeleting="grid_wh_RowDeleting"
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
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="0" Width="180" SortIndex="1">
                                                            <DataItemTemplate>
                                                                <%# Eval("CargoStatus") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxComboBox ID="cbb_JobType" runat="server" Value='<%# Bind("CargoStatus") %>' Width="60">
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="P" Value="P" />
                                                                        <dxe:ListEditItem Text="C" Value="C" />
                                                                    </Items>
                                                                </dxe:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Lot No/BL/Container" VisibleIndex="0" Width="180" SortIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td width="120px">Lot No</td>

                                                                        <td><%# Eval("BookingNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>BL/Bkg No</td>

                                                                        <td><%# Eval("HblNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Cont No</td>

                                                                        <td><%# Eval("ContNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Type</td>

                                                                        <td><%# Eval("OpsType") %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 180px">
                                                                    <tr>
                                                                        <td width="150px">Lot No</td>

                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_BookingNo" Width="120" runat="server" Text='<%# Bind("BookingNo") %>'></dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="150px">BL/Bkg No</td>

                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_HblNo" runat="server" Width="120" Text='<%# Bind("HblNo") %>'></dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="150px">Cont No</td>

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
                                                                        <td>Type</td>

                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_JobType" runat="server" Value='<%# Bind("OpsType") %>' Width="120">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Delivery" Value="Delivery" />
                                                                                    <dxe:ListEditItem Text="Pickup" Value="Pickup" />
                                                                                    <dxe:ListEditItem Text="Receipt" Value="Receipt" />
                                                                                    <dxe:ListEditItem Text="Storage" Value="Storage" />
                                                                                    <dxe:ListEditItem Text="Tranship" Value="Tranship" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Booking Info" VisibleIndex="2" Width="160">
                                                            <DataItemTemplate>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td>Qty</td>

                                                                        <td><%# Eval("Qty") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Unit</td>

                                                                        <td><%# Eval("UomCode") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Weight</td>

                                                                        <td><%# Eval("Weight") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Volume</td>

                                                                        <td><%# Eval("Volume") %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 120px">
                                                                    <tr>
                                                                        <td>Qty</td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit runat="server" Width="100%"
                                                                                ID="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Unit</td>
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
                                                                        <td>Weight</td>

                                                                        <td>
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Weight" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Volume</td>

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
                                                        <dxwgv:GridViewDataTextColumn Caption="Actual Info" VisibleIndex="2" Width="450">
                                                            <DataItemTemplate>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>Qty</td>
                                                                        <td><%# Eval("QtyOrig") %></td>
                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                        <td>SKU Code</td>
                                                                        <td colspan="2"><%# Eval("SkuCode") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Weight</td>
                                                                        <td colspan="2"><%# Eval("WeightOrig") %></td>
                                                                        <td>Qty</td>
                                                                        <td><%# Eval("PackQty") %></td>
                                                                        <td><%# Eval("PackUom") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Volume</td>
                                                                        <td colspan="2"><%# Eval("VolumeOrig") %></td>
                                                                        <td>Location</td>
                                                                        <td><%# Eval("Location") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td >Remark</td>
                                                                        <td colspan="5">
                                                                            <%# Eval("Desc1") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display:none">
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
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td>Qty</td>
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
                                                                        <td>SKUCode</td>
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
                                                                        <td>Weight</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Weight" Height="21px" Value='<%# Bind("WeightOrig")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Qty</td>
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
                                                                        <td>Volume</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Volume" Height="21px" Value='<%# Bind("VolumeOrig")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Location</td>
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
                                                                        <td >Remark</td>
                                                                         <td colspan="5">
                                                                             <dxe:ASPxMemo ID="memo_Desc1" ClientInstanceName="memo_Desc1" Text='<%# Bind("Desc1") %>' Rows="2" runat="server" Width="100%"></dxe:ASPxMemo>
                                                                         </td>
                                                                    </tr>
                                                                    <tr style="display:none">
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
                                                                        <td>Qty</td>
                                                                        <td><%# BalanceQty(Eval("ClientId"),Eval("Marking1"),Eval("BookingNo")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Unit</td>

                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Weight</td>

                                                                        <td><%# BalanceWeight(Eval("ClientId"),Eval("Marking1"),Eval("BookingNo")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Volume</td>

                                                                        <td><%# BalanceVolume(Eval("ClientId"),Eval("Marking1"),Eval("BookingNo")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>SKU Qty</td>

                                                                        <td><%# BalanceSkuQty(Eval("ClientId"),Eval("Marking1"),Eval("BookingNo")) %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 160px">
                                                                    <tr>
                                                                        <td>Qty</td>
                                                                        <td><%# BalanceQty(Eval("ClientId"),Eval("Marking1"),Eval("BookingNo")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Unit</td>

                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Weight</td>

                                                                        <td><%# BalanceWeight(Eval("ClientId"),Eval("Marking1"),Eval("BookingNo")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Volume</td>

                                                                        <td><%# BalanceVolume(Eval("ClientId"),Eval("Marking1"),Eval("BookingNo")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>SKU Qty</td>

                                                                        <td><%# BalanceSkuQty(Eval("ClientId"),Eval("Marking1"),Eval("BookingNo")) %></td>
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
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Bind("Remark1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="3" Width="200">
                                                            <DataItemTemplate>
                                                                <table style="width: 200px">
                                                                    <tr>
                                                                        <td width="120px">Landing</td>

                                                                        <td>
                                                                            <%# Eval("LandStatus") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>DG Cargo</td>

                                                                        <td>
                                                                            <%# Eval("DgClass") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Damage</td>
                                                                        <td>
                                                                            <%# Eval("DamagedStatus") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>DMG Remark</td>
                                                                        <td>
                                                                            <%# Eval("Remark2") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 200px">
                                                                    <tr>
                                                                        <td>Landing</td>

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
                                                                        <td>DG Cargo</td>

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
                                                                        <td>Damage</td>
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
                                                                        <td width="150px">DMG Remark</td>
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
                                <dxtc:TabPage Text="Event Log" Name="Activity" Visible="false">
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
                                <dxtc:TabPage Text="Close File" Name="Close File" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl1" runat="server">

                                            <table>
                                                <tr valign="top">
                                                    <td>Remark</td>
                                                    <td>
                                                        <dxe:ASPxMemo ID="memo_CLSRMK" ClientInstanceName="memo_CLSRMK" Rows="3" runat="server" Width="600"></dxe:ASPxMemo>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_JobClose" ClientInstanceName="btn_JobClose" runat="server" Text="Controll Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>' Width="100">
                                                            <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_JobClose.GetText()+' it?')) {
                                                    grid_job.GetValuesOnCustomCallback('Close',onCallBack);
                                                    }
                                                 }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_JobVoid" ClientInstanceName="btn_JobVoid" runat="server" Text="Void Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>' Width="100">
                                                            <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_JobVoid.GetText()+' it?')) {
                                                    grid_job.GetValuesOnCustomCallback('Void',onCallBack);
                                                    }
                                                 }" />
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
