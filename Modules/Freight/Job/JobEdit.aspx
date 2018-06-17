<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="JobEdit.aspx.cs" Inherits="Modules_Freight_Job_JobEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/PagesContTrucking/script/StyleSheet.css" rel="stylesheet" />
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
    <script type="text/javascript" src="../script/order.js"></script>
    <style type="text/css">
        .show {
            display: block;
        }

        .hide {
            display: none;
        }

        .a_ltrip .P {
            background-color: orange;
        }

        .div_amt {
            width: 100%;
            text-align: center;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding-top: 2px;
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
        function GoJob_Page(v) {
            if (v == "Transfer") {
                popubCtrPic.SetHeaderText('Create Transfer Job ');
                popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/CreateJobOrder.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + btn_ClientId.GetText() + "&action=FRT");
                popubCtrPic.Show();
                //parent.navTab.openTab(v, "/Modules/Tpt/TptJobList.aspx?type=TR" , { title:v, fresh: false, external: true });
            } else if (v == "Delivery") {
                //parent.navTab.openTab(v, "/Modules/Tpt/TptJobList.aspx?type=DO" , { title:v, fresh: false, external: true });
                popubCtrPic.SetHeaderText('Create Delivery Order ');
                popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/CreateJobOrder.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + btn_ClientId.GetText() + "&action=WDO");
                popubCtrPic.Show();
            }
            else if (v == "ShowDO") {
                popubCtrPic.SetHeaderText('Show Delivery Order ');
                popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/ShowDeliveryOrder.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + btn_ClientId.GetText());
                popubCtrPic.Show();
            }
        }
        function onCallBack(v) {
            if (v == "PrintHaulier") {
                window.open('/PagesContTrucking/Report/RptPrintView.aspx?doc=haulier&no=' + txt_JobNo.GetText() + "&type=" + cbb_JobType.GetValue());
            }
            else if (v != null && v.length > 0) {
                alert(v);
                grid_job.Refresh();
                grid_Cost.Refresh();
            }
            else {
                grid_job.Refresh();
            }
        }
        function onNoCallBack(v) {
            var paras = new Array();
            paras = v.split('_');
            var par = paras[0];
            var jobno = paras[1];
            if (par == "J") {
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            }
            else if (par == "Q") {
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            }
            else if (par == "C") {
                window.location = 'JobEdit.aspx?no=' + jobno;
            }
            else {
                alert(v);
                grid_job.Refresh();
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
            popubCtrPic.SetContentUrl('../Upload.aspx?Type=CTM&Sn=0&jobNo=' + txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function PopupJobRate() {
            popubCtr.SetHeaderText('Job Rate ');
            popubCtr.SetContentUrl('../SelectPage/SelectJobRate.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + btn_ClientId.GetText());
            popubCtr.Show();
        }
        function PopupJobHouse() {
            popubCtrPic.SetHeaderText('Stock List');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/SelectStock.aspx?no=' + txt_JobNo.GetText() + "&client=" + btn_ClientId.GetText() + "&jobType=" + cbb_JobType.GetValue());
            popubCtrPic.Show();
        }
        function AfterPopub() {
            popubCtrPic.Hide();
            popubCtrPic.SetContentUrl('about:blank');
            grid_wh.Refresh();


            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_Cost.Refresh();
        }
        function AfterPopubRate() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid1.Refresh();
        }
        function ShowEmail() {
            var value = btn_SendEmail.GetText();
            if (value == "Email Quotation") {
                document.getElementById('email').style.display = 'block';
                btn_SendEmail.SetText('Close');
            }
            else {
                document.getElementById('email').style.display = 'none';
                btn_SendEmail.SetText('Email Quotation');
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
                var jobType = cbb_JobType.GetValue();
                if (jobType == "IMP") {
                    cbb_StatusCode.SetValue('Import');
                }
                if (jobType == "EXP") {
                    cbb_StatusCode1.SetValue('Collection');
                }
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
            //setTimeout(function () {
            //    gv_cont_trip.Refresh();
            //    //if (grid_Trip) {
            //    //    grid_Trip.CancelEdit();
            //    //}
            //}, 500);
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
        function dimension_inline(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Dimensionline_' + rowIndex, dimension_inline_callback);
            }, config.timeout);

        }
        function dimension_inline_callback(res) {
            popubCtrPic.SetHeaderText('Dimension ');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/DimensionForCargo.aspx?id=' + res);
            popubCtrPic.Show();
        }
        function AfterPopubDimension() {
            popubCtrPic.Hide();
            popubCtrPic.SetContentUrl('about:blank');
            grid_wh.Refresh();
        }
        function upload_inline(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Uploadline_' + rowIndex, upload_inline_callback);
            }, config.timeout);
        }
        function upload_inline_callback(res) {
            var ar = res.split('_');
            popubCtrPic.SetHeaderText('Upload Attachment');
            popubCtrPic.SetContentUrl('../Upload.aspx?Type=CTM&Sn=' + ar[0] + '&ContNo=' + ar[1] + '&jobNo=' + txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function PrintQuotation(invN, docType) {
            window.open('/ReportFreightSea/printview.aspx?document=Quotation&master=' + txt_QuoteNo.GetText() + '&docType=' + cmb_JobStatus.GetText());

        }
        function PopupMasterRate() {
            popubCtr.SetHeaderText('Bill Rate');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectRateForQuotation.aspx?quoteNo=' + txt_QuoteNo.GetText() + '&client=' + btn_ClientId.GetText());
            popubCtr.Show();
        }
        function PopupUom(codeId, typ) {
            clientId = codeId;
            popubCtr.SetHeaderText('UOM');
            popubCtr.SetContentUrl('/SelectPage/UomList.aspx?type=' + typ);
            popubCtr.Show();
        }
        function PopupRate() {
            popubCtr.SetHeaderText('Chg Code');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectChgCode.aspx?no=' + txt_QuoteNo.GetText() + '&client=' + btn_ClientId.GetText());
            popubCtr.Show();
        }
        function PopupCharges() {
            popubCtr.SetHeaderText('Chg Code');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectChargesForCost.aspx?no=' + txt_JobNo.GetText());
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
        <wilson:DataSource ID="dsJob" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJob" KeyMember="Id" />
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
        <wilson:DataSource ID="dsJobStock" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobStock"
            KeyMember="Id" FilterExpression="1=0"/>
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true' or IsAgent='true'" />
        <div>
            <table>
                <tr>
                    <td class="lbl">Job No</td>
                    <td class="ctl">
                        <dxe:ASPxTextBox ID="txt_search_JobNo" ClientInstanceName="txt_search_JobNo" runat="server" Width="100%"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Retrieve" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='JobEdit.aspx?no='+txt_search_JobNo.GetText();
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
                            <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" Width="120" ReadOnly="true" BackColor="Control"
                                runat="server" Text='<%# Eval("JobNo") %>'>
                            </dxe:ASPxTextBox>
                        </div>
                        <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="980px">
                            <TabPages>
                                <dxtc:TabPage Name="Import Order Information" Text="Job">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="display: none">
                                                <dxe:ASPxTextBox ID="txt_Sn" ClientInstanceName="txt_Sn" Width="170" ReadOnly="true"
                                                    runat="server" Text='<%# Eval("Id") %>'>
                                                </dxe:ASPxTextBox>
                                            </div>
                                            <table>
                                                <tr>
                                                    <td valign="top">
                                                        <table>
                                                            <tr>
                                                                <td class="lbl">Cfs Job No
                                                                <input type='hidden' id='email_job' style="width: 100%" value='<%# Eval("JobNo") %>' />
                                                                </td>
                                                                <td class="ctl">
                                                                    <table cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_OrderNo" ClientInstanceName="txt_OrderNo" Width="120" ReadOnly="true" BackColor="Control"
                                                                                    runat="server" Text='<%# Eval("JobNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="cmb_OrderType" Width="80" ClientInstanceName="cmb_OrderType" runat="server" Value='<%# Bind("OrderType") %>' BackColor="#BBFFFD">
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="AIR" Value="AIR" />
                                                                                        <dxe:ListEditItem Text="CONSOL" Value="CONSOL" />
                                                                                        <dxe:ListEditItem Text="FCL" Value="FCL" />
                                                                                        <dxe:ListEditItem Text="LCL" Value="LCL" />
                                                                                        <dxe:ListEditItem Text="RAILING" Value="RAILING" />
                                                                                        <dxe:ListEditItem Text="TSLOAD" Value="TSLOAD" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="lbl">Billing
                                                                </td>
                                                                <td class="ctl">
                                                                    <table cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="cmb_FileClosed" Width="70" runat="server" Value='<%# Bind("JobStatus") %>'>
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                                                        <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                                                                        <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-left: 8px;">
                                                                                <dxe:ASPxDateEdit ID="date_TruckOn" runat="server" Width="90" Value='<%# Bind("CodDate") %>'
                                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Vessel
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_Ves" Width="170" BackColor="#BBFFFD" runat="server" Text='<%# Bind("Vessel") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="lbl">Voyage
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_Voy" Width="170" BackColor="#BBFFFD" runat="server" Text='<%# Bind("Voyage") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">POL/AGENT
                                                                </td>
                                                                <td class="ctl">
                                                                    <table cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Pol" BackColor="#BBFFFD" ClientInstanceName="txt_Pol" Width="70" runat="server"
                                                                                    Text='<%# Bind("Pol") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-left: 4px;">
                                                                                <dxe:ASPxButton ID="ASPxButton19" Width="25" runat="server" Text=".." AutoPostBack="False">
                                                                                    <ClientSideEvents Click="function(s, e) {
                                                                PopupPort(txt_Pol,null);
                                                                    }" />
                                                                                </dxe:ASPxButton>
                                                                            </td>


                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_AgtCode" ClientInstanceName="txt_AgtCode" Width="70"
                                                                                    runat="server" Text='<%# Bind("AgentId") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-left: 4px;">
                                                                                <dxe:ASPxButton ID="ASPxButton20" Width="25" runat="server" Text=".." AutoPostBack="False">
                                                                                    <ClientSideEvents Click="function(s, e) {
                                                                PopupAgent(txt_AgtCode,null);
                                                                    }" />
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="lbl">Eta
                                                                </td>
                                                                <td class="ctl">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxDateEdit ID="date_Eta" BackColor="#BBFFFD" runat="server" Width="100" Value='<%# Bind("EtaDate") %>'
                                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_EtaTime" Width="60" runat="server" Text='<%# Bind("EtaTime") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">ContNo
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_ContNo" ReadOnly="true" Width="170" BackColor="Control"
                                                                        runat="server" Text='<%# Eval("ContNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="lbl">SealNo
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_SealNo" ReadOnly="true" Width="170" BackColor="Control"
                                                                        runat="server" Text='<%# Eval("SealNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Cont20
                                                                </td>
                                                                <td class="ctl">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit ID="spin_20" Width="60" ReadOnly="true" runat="server" BackColor="Control"
                                                                                    Value='<%# Eval("Ft20") %>'>
                                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td>Cont40
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit ID="spin_40" Width="60" ReadOnly="true" runat="server" BackColor="Control"
                                                                                    Value='<%# Eval("Ft40") %>'>
                                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="lbl">Cont45
                                                                </td>
                                                                <td class="ctl">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxSpinEdit ID="spin_45" Width="50" ReadOnly="true" runat="server" BackColor="Control"
                                                                                    Value='<%# Eval("Ft45") %>'>
                                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </td>
                                                                            <td class="lbl">Cont Type
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_FtType" runat="server" Width="50" Text='<%# Bind("FtType") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Special Instruction
                                                                </td>
                                                                <td colspan="3" class="ctl">
                                                                    <dxe:ASPxMemo ID="txt_SpecInstruction" Width="100%" Rows="2" runat="server" Text='<%# Bind("SpecialInstruction") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">
                                                                    <a href='javascript:PopupCustAdr(null,txt_CollectTo)'>Return Yard Info</a>
                                                                </td>
                                                                <td colspan="3" class="ctl">
                                                                    <dxe:ASPxMemo ID="txt_CollectTo" ClientInstanceName="txt_CollectTo" Width="100%" Rows="2" runat="server" Text='<%# Bind("YardRef") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>

                                                                <td class="lbl">O/C
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_Party" Width="80"
                                                                        runat="server" Text='<%# Eval("ClientId") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="lbl">HBL
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_BookingNo2" Width="100%" runat="server" Text='<%# Bind("CarrierBlNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td class="lbl">Terminal
                                                                </td>
                                                                <td class="ctl">

                                                                    <dxe:ASPxTextBox ID="txt_Terminal" Width="120" Rows="3" runat="server" Text='<%# Bind("Terminalcode") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="lbl">Permit No
                                                                </td>
                                                                <td class="ctl">

                                                                    <dxe:ASPxTextBox ID="txt_Permit" Width="100%" Rows="3" runat="server" Text='<%# Bind("PermitNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Portnet Ref
                                                                </td>
                                                                <td class="ctl">

                                                                    <dxe:ASPxTextBox ID="txt_Portnet" Width="120" Rows="3" runat="server" Text='<%# Bind("PortnetRef") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="lbl">Expiry Date
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxDateEdit ID="date_Yard" runat="server" Width="100%" Value='<%# Bind("PermitExpiry") %>'
                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="{0:dd/MM/yyyy}">
                                                                    </dxe:ASPxDateEdit>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Condition
                                                                </td>
                                                                <td colspan="3" class="ctl">
                                                                    <dxe:ASPxMemo ID="txt_Condition" Width="100%" Rows="3" runat="server" Text='<%# Bind("AdditionalRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Remark
                                                                </td>
                                                                <td colspan="3" class="ctl">
                                                                    <dxe:ASPxMemo ID="txt_Rmk" Width="100%" Rows="2" runat="server" Text='<%# Bind("TerminalRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Trucking Remarks
                                                                </td>
                                                                <td colspan="3" class="ctl">
                                                                    <dxe:ASPxMemo ID="txt_TruckingRmk" Width="100%" Rows="2" runat="server" Text='<%# Bind("InternalRemark") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">

                                                        <table>

                                                            <tr>
                                                                <td class="lbl">Client Code
                                                                </td>
                                                                <td class="ctl">
                                                                    <table cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_CustCode" ClientInstanceName="txt_CustCode" ReadOnly="false"
                                                                                    Width="120" BackColor="#BBFFFD" runat="server" Text='<%# Bind("PartyId") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-left: 4px;">
                                                                                <dxe:ASPxButton ID="btn_Cust" Width="40" runat="server" Text="Pick" AutoPostBack="False">
                                                                                    <ClientSideEvents Click="function(s, e) {
                                                                PopupCust(txt_CustCode,null,txt_ByWho,txt_Email);
                                                                    }" />
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Client Ref
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_ImpRefNo" BackColor="#BBFFFD" Width="170" runat="server" Text='<%# Bind("ClientRefNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Client PIC
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_ByWho" ClientInstanceName="txt_ByWho" BackColor="#BBFFFD" Width="170" runat="server" Text='<%# Bind("ClientContact") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Notify Email
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_Email" ClientInstanceName="txt_Email" BackColor="#BBFFFD" Width="170" runat="server" Text='<%# Bind("EmailAddress") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Carrier
                                                                </td>
                                                                <td class="ctl">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Carrier" ClientInstanceName="txt_Carrier" Width="120" BackColor="#BBFFFD" runat="server" Text='<%# Bind("CarrierId") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-left: 4px;">
                                                                                <dxe:ASPxButton ID="ASPxButton21" Width="40" runat="server" Text="Pick" AutoPostBack="False">
                                                                                    <ClientSideEvents Click="function(s, e) {
                                                                PopupCarrier(txt_Carrier,null);
                                                                    }" />
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Ocean BL
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_OceanBl" Width="170" BackColor="#BBFFFD" runat="server" Text='<%# Bind("CarrierBkgNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Haulier
                                                                </td>
                                                                <td class="ctl">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Haulier" Width="120" ClientInstanceName="txt_Haulier" runat="server" BackColor="#BBFFFD"
                                                                                    Text='<%# Bind("HaulierId") %>' ReadOnly="true">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_Haulier" runat="server" Width="40" UseSubmitBehavior="false"
                                                                                    AutoPostBack="false" Text="Pick">
                                                                                    <ClientSideEvents Click="function(s,e){
                                                        PopupHaulier(txt_Haulier,null,null);
                                                        }" />
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Create Date
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxDateEdit Width="170" ReadOnly="true" BackColor="Control" ID="date_JobDate"
                                                                        runat="server" Value='<%# Bind("JobDate") %>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                        DisplayFormatString="dd/MM/yyyy">
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td class="lbl">Tally Done
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_TallyDone" Width="170" runat="server" Text='<%# Bind("TallyDone") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Issued By
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_IssuedBy" Width="170" runat="server" Text='<%# Bind("IssuedBy") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl">Last Updated
                                                                </td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_EntryBy" ReadOnly="true" Width="170"
                                                                        runat="server" Text='<%# Bind("UpdateBy") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <table>
                                                <tr>
                                                    <td width="66%">
                                                        <table style="border: solid 1px black; padding: 6px;" cellpadding="6">
                                                            <tr>
                                                                <td>
                                                                    <a href='/Modules/Freight/Report/printview.aspx?doc=4&no=<%# Eval("JobNo")%>' target="_blank">Tallysheet Intended</a>
                                                                </td>
                                                                <td>
                                                                    <a href='/Modules/Freight/Report/printview.aspx?doc=4c&no=<%# Eval("JobNo")%>' target="_blank">Tallysheet Confirmed</a>
                                                                </td>

                                                                <td>
                                                                    <a href='/Modules/Freight/Report/printview.aspx?doc=2&no=<%# Eval("JobNo")%>' target="_blank">Haulier Advice</a>
                                                                </td>
                                                                <td>
                                                                    <a href='/Modules/Freight/Report/printview.aspx?doc=40&no=<%# Eval("JobNo")%>' target="_blank">Auth Letter</a>
                                                                </td>

                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton ID="ASPxButton22" Width="80" runat="server" Text="Email To Haulier" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                                            if(confirm('Confirm Send Email?')){
												        	 grid_job.GetValuesOnCustomCallback('EmailToHaulier',OnSendEmailCallBack);
                                                            }
                                                        }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButton ID="ASPxButton23" Width="80" runat="server" Text="Email To Carrier" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                                            if(confirm('Confirm Send Email?')){
												        	 grid_job.GetValuesOnCustomCallback('EmailToCarrier',OnSendEmailCallBack);
                                                            }
                                                        }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButton ID="btn_Save1" Width="100" runat="server" Text="Save" Enabled='<%# SafeValue.SafeSqlString(Eval("StatusCode"))=="USE" %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                                            grid_job.PerformCallback('Save')
                                                        }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButton ID="ASPxButton4xy" Width="180" Visible="true" runat="server" Text="Send Confirmed Tallysheet" AutoPostBack="false"
                                                                        UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
		 if(confirm('Ready to send confirmed tallysheet to customer ?')) { grid_job.PerformCallback('NOTI' + txt_OrderNo.GetText()); }
                                                        }" />
                                                                    </dxe:ASPxButton>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Container Information" Name="Container">
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
                                                    </tr>
                                                </table>
                                                <div style="overflow-y: auto; width: 1200px">
                                                    <dxwgv:ASPxGridView ID="grid_Cont" ClientInstanceName="grid_Cont" runat="server" KeyFieldName="Id" DataSourceID="dsCont" Width="800px" AutoGenerateColumns="False" OnInit="grid_Cont_Init" OnBeforePerformDataSelect="grid_Cont_BeforePerformDataSelect" OnRowDeleting="grid_Cont_RowDeleting" OnInitNewRow="grid_Cont_InitNewRow" OnRowInserting="grid_Cont_RowInserting" OnRowUpdating="grid_Cont_RowUpdating" OnRowDeleted="grid_Cont_RowDeleted" OnRowUpdated="grid_Cont_RowUpdated" OnRowInserted="grid_Cont_RowInserted" OnCustomDataCallback="grid_Cont_CustomDataCallback">
                                                        <SettingsPager PageSize="100" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsBehavior ConfirmDelete="true" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" Width="150" VisibleIndex="0">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    Enabled='<%# SafeValue.SafeString(Eval("canChange"))!="none" %>' ClientSideEvents-Click='<%# "function(s) { grd_Cont.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("canChange"))!="none" %>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Cont.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Cont.UpdateEdit()() }"  %>'>
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
                                                            <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="Cont No" VisibleIndex="1" Width="160">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Text='<%# Bind("ContainerNo") %>' Width="120"></dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataColumn FieldName="SealNo" Caption="Seal No" VisibleIndex="1" Width="160">
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn FieldName="ContainerType" Caption="Type" Width="60" VisibleIndex="6">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="60" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContainerType") %>'></dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn FieldName="F5Ind" Caption="Customs Supervision" Width="90" VisibleIndex="6">
                                                                <PropertiesComboBox>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="No" Value="No" />
                                                                        <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    </Items>
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataDateColumn FieldName="RequestDate" Caption="Customs Date" VisibleIndex="6"
                                                                Width="100">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" />
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Customs Remark" VisibleIndex="6" Width="80">
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataColumn FieldName="Weight" Caption="Cargo Wt" VisibleIndex="6" Width="90">
                                                                <DataItemTemplate>
                                                                    <%# S.Decimal(D.One("select Sum(Weight) from job_house where ContNo='"+Eval("ContainerNo","{0}")+"' and JobNo='"+Eval("JobNo","{0}")+"'")) %>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%# S.Decimal(D.One("select Sum(Weight) from job_house where ContNo='"+Eval("ContainerNo","{0}")+"' and JobNo='"+Eval("JobNo","{0}")+"'")) %>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataColumn FieldName="Volume" Caption="Cargo M3" VisibleIndex="6" Width="90">
                                                                <DataItemTemplate>
                                                                    <%# S.Decimal(D.One("select Sum(Volume) from job_house where ContNo='"+Eval("ContainerNo","{0}")+"' and JobNo='"+Eval("JobNo","{0}")+"'")) %>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%# S.Decimal(D.One("select Sum(Volume) from job_house where ContNo='"+Eval("ContainerNo","{0}")+"' and JobNo='"+Eval("JobNo","{0}")+"'")) %>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn FieldName="StatusCode" Caption="Status" VisibleIndex="6"
                                                                Width="180">
                                                                <PropertiesComboBox>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="SCHEDULED" Value="SCHEDULED" />
                                                                        <dxe:ListEditItem Text="UNSTUFFING" Value="UNSTUFFING" />
                                                                        <dxe:ListEditItem Text="COMPLETED" Value="COMPLETED" />
                                                                    </Items>
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn FieldName="CfsStatus" Caption="Cancel" Width="60" VisibleIndex="6">
                                                                <PropertiesComboBox>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="YES" Value="YES" />
                                                                        <dxe:ListEditItem Text="NO" Value="NO" />
                                                                    </Items>
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataColumn FieldName="Permit" Caption="Permit No" VisibleIndex="7" Width="80">
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataDateColumn FieldName="ScheduleStartDate" Caption="Truck Date" VisibleIndex="7"
                                                                Width="100">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" />
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataColumn FieldName="ScheduleStartTime" Caption="Time" VisibleIndex="7" Width="60">
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataColumn FieldName="PackageType" Caption="GEN/DG" VisibleIndex="7" Width="60">
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataDateColumn FieldName="CdtDate" Caption="Unstuff Date" VisibleIndex="7"
                                                                Width="100">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" />
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn FieldName="CdtTime" Width="70" Caption="Time"
                                                                VisibleIndex="8">
                                                                <PropertiesComboBox>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text=" " Value=" " />
                                                                        <dxe:ListEditItem Text="08:30" Value="08:30" />
                                                                        <dxe:ListEditItem Text="08:45" Value="08:45" />
                                                                        <dxe:ListEditItem Text="09:00" Value="09:00" />
                                                                        <dxe:ListEditItem Text="09:15" Value="09:15" />
                                                                        <dxe:ListEditItem Text="09:30" Value="09:30" />
                                                                        <dxe:ListEditItem Text="09:45" Value="09:45" />
                                                                        <dxe:ListEditItem Text="10:00" Value="10:00" />
                                                                        <dxe:ListEditItem Text="10:15" Value="10:15" />
                                                                        <dxe:ListEditItem Text="10:30" Value="10:30" />
                                                                        <dxe:ListEditItem Text="10:45" Value="10:45" />
                                                                        <dxe:ListEditItem Text="11:00" Value="11:00" />
                                                                        <dxe:ListEditItem Text="11:15" Value="11:15" />
                                                                        <dxe:ListEditItem Text="11:30" Value="11:30" />
                                                                        <dxe:ListEditItem Text="11:45" Value="11:45" />
                                                                        <dxe:ListEditItem Text="12:00" Value="12:00" />
                                                                        <dxe:ListEditItem Text="12:15" Value="12:15" />
                                                                        <dxe:ListEditItem Text="12:30" Value="12:30" />
                                                                        <dxe:ListEditItem Text="12:45" Value="12:45" />
                                                                        <dxe:ListEditItem Text="13:00" Value="13:00" />
                                                                        <dxe:ListEditItem Text="13:15" Value="13:15" />
                                                                        <dxe:ListEditItem Text="13:30" Value="13:30" />
                                                                        <dxe:ListEditItem Text="13:45" Value="13:45" />
                                                                        <dxe:ListEditItem Text="14:00" Value="14:00" />
                                                                        <dxe:ListEditItem Text="14:15" Value="14:15" />
                                                                        <dxe:ListEditItem Text="14:30" Value="14:30" />
                                                                        <dxe:ListEditItem Text="14:45" Value="14:45" />
                                                                        <dxe:ListEditItem Text="15:00" Value="15:00" />
                                                                        <dxe:ListEditItem Text="15:15" Value="15:15" />
                                                                        <dxe:ListEditItem Text="15:30" Value="15:30" />
                                                                        <dxe:ListEditItem Text="15:45" Value="15:45" />
                                                                        <dxe:ListEditItem Text="16:00" Value="16:00" />
                                                                        <dxe:ListEditItem Text="16:15" Value="16:15" />
                                                                        <dxe:ListEditItem Text="16:30" Value="16:30" />
                                                                        <dxe:ListEditItem Text="16:45" Value="16:45" />
                                                                        <dxe:ListEditItem Text="17:00" Value="17:00" />
                                                                        <dxe:ListEditItem Text="17:15" Value="17:15" />
                                                                        <dxe:ListEditItem Text="17:30" Value="17:30" />
                                                                        <dxe:ListEditItem Text="17:45" Value="17:45" />
                                                                        <dxe:ListEditItem Text="18:00" Value="18:00" />
                                                                        <dxe:ListEditItem Text="18:15" Value="18:15" />
                                                                        <dxe:ListEditItem Text="18:30" Value="18:30" />
                                                                        <dxe:ListEditItem Text="18:45" Value="18:45" />
                                                                        <dxe:ListEditItem Text="19:00" Value="19:00" />
                                                                        <dxe:ListEditItem Text="19:15" Value="19:15" />
                                                                        <dxe:ListEditItem Text="19:30" Value="19:30" />
                                                                        <dxe:ListEditItem Text="19:45" Value="19:45" />
                                                                        <dxe:ListEditItem Text="20:00" Value="20:00" />
                                                                        <dxe:ListEditItem Text="20:15" Value="20:15" />
                                                                        <dxe:ListEditItem Text="20:30" Value="20:30" />
                                                                        <dxe:ListEditItem Text="20:45" Value="20:45" />
                                                                        <dxe:ListEditItem Text="21:00" Value="21:00" />
                                                                        <dxe:ListEditItem Text="21:15" Value="21:15" />
                                                                        <dxe:ListEditItem Text="21:30" Value="21:30" />
                                                                        <dxe:ListEditItem Text="21:45" Value="21:45" />
                                                                        <dxe:ListEditItem Text="22:00" Value="22:00" />
                                                                        <dxe:ListEditItem Text="22:15" Value="22:15" />
                                                                        <dxe:ListEditItem Text="22:30" Value="22:30" />
                                                                        <dxe:ListEditItem Text="22:45" Value="22:45" />
                                                                        <dxe:ListEditItem Text="23:00" Value="23:00" />
                                                                    </Items>
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataDateColumn FieldName="CompletionDate" Caption="Return Date" VisibleIndex="8"
                                                                Width="100">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" />
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn FieldName="CompletionTime" Caption="Time" VisibleIndex="8"
                                                                Width="100">
                                                                <PropertiesComboBox>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="" Value="" />
                                                                        <dxe:ListEditItem Text="08:30" Value="08:30" />
                                                                        <dxe:ListEditItem Text="08:45" Value="08:45" />
                                                                        <dxe:ListEditItem Text="09:00" Value="09:00" />
                                                                        <dxe:ListEditItem Text="09:15" Value="09:15" />
                                                                        <dxe:ListEditItem Text="09:30" Value="09:30" />
                                                                        <dxe:ListEditItem Text="09:45" Value="09:45" />
                                                                        <dxe:ListEditItem Text="10:00" Value="10:00" />
                                                                        <dxe:ListEditItem Text="10:15" Value="10:15" />
                                                                        <dxe:ListEditItem Text="10:30" Value="10:30" />
                                                                        <dxe:ListEditItem Text="10:45" Value="10:45" />
                                                                        <dxe:ListEditItem Text="11:00" Value="11:00" />
                                                                        <dxe:ListEditItem Text="11:15" Value="11:15" />
                                                                        <dxe:ListEditItem Text="11:30" Value="11:30" />
                                                                        <dxe:ListEditItem Text="11:45" Value="11:45" />
                                                                        <dxe:ListEditItem Text="12:00" Value="12:00" />
                                                                        <dxe:ListEditItem Text="12:15" Value="12:15" />
                                                                        <dxe:ListEditItem Text="12:30" Value="12:30" />
                                                                        <dxe:ListEditItem Text="12:45" Value="12:45" />
                                                                        <dxe:ListEditItem Text="13:00" Value="13:00" />
                                                                        <dxe:ListEditItem Text="13:15" Value="13:15" />
                                                                        <dxe:ListEditItem Text="13:30" Value="13:30" />
                                                                        <dxe:ListEditItem Text="13:45" Value="13:45" />
                                                                        <dxe:ListEditItem Text="14:00" Value="14:00" />
                                                                        <dxe:ListEditItem Text="14:15" Value="14:15" />
                                                                        <dxe:ListEditItem Text="14:30" Value="14:30" />
                                                                        <dxe:ListEditItem Text="14:45" Value="14:45" />
                                                                        <dxe:ListEditItem Text="15:00" Value="15:00" />
                                                                        <dxe:ListEditItem Text="15:15" Value="15:15" />
                                                                        <dxe:ListEditItem Text="15:30" Value="15:30" />
                                                                        <dxe:ListEditItem Text="15:45" Value="15:45" />
                                                                        <dxe:ListEditItem Text="16:00" Value="16:00" />
                                                                        <dxe:ListEditItem Text="16:15" Value="16:15" />
                                                                        <dxe:ListEditItem Text="16:30" Value="16:30" />
                                                                        <dxe:ListEditItem Text="16:45" Value="16:45" />
                                                                        <dxe:ListEditItem Text="17:00" Value="17:00" />
                                                                        <dxe:ListEditItem Text="17:15" Value="17:15" />
                                                                        <dxe:ListEditItem Text="17:30" Value="17:30" />
                                                                        <dxe:ListEditItem Text="17:45" Value="17:45" />
                                                                        <dxe:ListEditItem Text="18:00" Value="18:00" />
                                                                        <dxe:ListEditItem Text="18:15" Value="18:15" />
                                                                        <dxe:ListEditItem Text="18:30" Value="18:30" />
                                                                        <dxe:ListEditItem Text="18:45" Value="18:45" />
                                                                        <dxe:ListEditItem Text="19:00" Value="19:00" />
                                                                        <dxe:ListEditItem Text="19:15" Value="19:15" />
                                                                        <dxe:ListEditItem Text="19:30" Value="19:30" />
                                                                        <dxe:ListEditItem Text="19:45" Value="19:45" />
                                                                        <dxe:ListEditItem Text="20:00" Value="20:00" />
                                                                        <dxe:ListEditItem Text="20:15" Value="20:15" />
                                                                        <dxe:ListEditItem Text="20:30" Value="20:30" />
                                                                        <dxe:ListEditItem Text="20:45" Value="20:45" />
                                                                        <dxe:ListEditItem Text="21:00" Value="21:00" />
                                                                        <dxe:ListEditItem Text="21:15" Value="21:15" />
                                                                        <dxe:ListEditItem Text="21:30" Value="21:30" />
                                                                        <dxe:ListEditItem Text="21:45" Value="21:45" />
                                                                        <dxe:ListEditItem Text="22:00" Value="22:00" />
                                                                        <dxe:ListEditItem Text="22:15" Value="22:15" />
                                                                        <dxe:ListEditItem Text="22:30" Value="22:30" />
                                                                        <dxe:ListEditItem Text="22:45" Value="22:45" />
                                                                        <dxe:ListEditItem Text="23:00" Value="23:00" />
                                                                    </Items>
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataColumn FieldName="Remark1" VisibleIndex="6" Width="100">
                                                            </dxwgv:GridViewDataColumn>
                                                        </Columns>

                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Cargo Information" Visible="true" Name="Warehouse">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_AddCargo" Width="100" runat="server" Text="Add Detail" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                    grd_Det.AddNewRow();
                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Refresh/刷新" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                    grd_Det.GetValuesOnCustomCallback('Refresh',OnCallBack);
                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton12" Width="100" runat="server" Text="Select Booking Order/选择订单" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                    MultipleAdd(txt_OrderNo.GetText());
                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <dxwgv:ASPxGridView ID="grd_Det" ClientInstanceName="grd_Det" runat="server" DataSourceID="dsWh"
                                                KeyFieldName="Id" Width="900px" OnBeforePerformDataSelect="grd_Det_BeforePerformDataSelect"
                                                OnBeforeGetCallbackResult="grd_Det_BeforeGetCallbackResult" OnHtmlEditFormCreated="grd_Det_HtmlEditFormCreated"
                                                OnInit="grd_Det_Init" OnInitNewRow="grd_Det_InitNewRow" OnRowInserting="grd_Det_RowInserting" OnCustomCallback="grd_Det_CustomCallback"
                                                OnRowUpdating="grd_Det_RowUpdating" OnRowDeleting="grd_Det_RowDeleting" OnCustomDataCallback="grd_Det_CustomDataCallback">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <SettingsEditing Mode="EditForm" />
                                                <Settings />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="110" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="ASPxButton17" runat="server" Text="Edit/编辑" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grd_Det.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="ASPxButton18" runat="server" Enabled='<%# SafeValue.SafeDecimal(Eval("OtherFee"),0)==0 %>'
                                                                            Text="Delete/删除" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){delete_inline("+Container.VisibleIndex+")} }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="ASPxButton19" runat="server" Text="Update/保存" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grd_Det.UpdateEdit()() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="ASPxButton20" runat="server" Text="Cancel/取消" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grd_Det.CancelEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="LineId" Caption=" " VisibleIndex="1" Width="20"
                                                        SortIndex="0" SortOrder="Ascending">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="BookingNo" Caption="Bkg RefNo/唛头号" VisibleIndex="1" Width="100">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ConsigneeInfo" Caption="CONSIGNEE/货主" VisibleIndex="1" Width="100">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="Email/电子邮箱" VisibleIndex="1" Width="30">
                                                        <DataItemTemplate>
                                                            <%# Eval("Email1") %>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="Contact Number/联系方式" VisibleIndex="1" Width="30">
                                                        <DataItemTemplate>
                                                            TEL:<%# Eval("Tel1") %>/<%# Eval("Tel2") %><br />
                                                            MOBILE:<%# Eval("Mobile1") %>/<%# Eval("Mobile2") %>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ClientId" Caption="Receiver Party/收货人" VisibleIndex="1" Width="100">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ClientEmail" Caption="Contact Number/联系方式" VisibleIndex="1" Width="30">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ContNo" Caption="Cont No/柜号" VisibleIndex="2" Width="100">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Qty" Caption="Qty/总箱数" VisibleIndex="5" Width="50">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Weight" Caption="Weight/重量" VisibleIndex="5"
                                                        Width="80">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Volume" Caption="CBM/M3/体积" VisibleIndex="5"
                                                        Width="80">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="5">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="ASPxButton17" runat="server" Text="Confirm Order" Width="80" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) {send_email("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="PrepaidInd" Caption="中国付/Freight prepaid" VisibleIndex="5"
                                                        Width="80">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxCheckBox ID="ckb_Prepaid_Ind" ClientInstanceName="ckb_Prepaid_Ind" runat="server" Checked='<%# SafeValue.SafeString(Eval("PrepaidInd"))=="YES"?true:false %>'>
                                                            </dxe:ASPxCheckBox>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewBandColumn Caption="代收/COLLECT ON BEHALF" VisibleIndex="5">
                                                        <Columns>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="RMB" FieldName="CollectAmount1" VisibleIndex="5">
                                                                <DataItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_CollectAmount1" ClientInstanceName="spin_CollectAmount1" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("CollectAmount1") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption=" SGD" FieldName="CollectAmount2" VisibleIndex="5">
                                                                <DataItemTemplate>
                                                                    <dxe:ASPxSpinEdit ID="spin_CollectAmount2" ClientInstanceName="spin_CollectAmount2" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("CollectAmount2") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                        </Columns>
                                                    </dxwgv:GridViewBandColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="报关税/Permit GST" FieldName="OtherFee" VisibleIndex="5">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxSpinEdit ID="spin_OtherFee1" ClientInstanceName="spin_OtherFee1" runat="server"
                                                                ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("OtherFee") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="110" VisibleIndex="5">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="ASPxButton17" runat="server" Enabled='<%# SafeValue.SafeDecimal(Eval("OtherFee"),0)==0 %>' Text="Save/保存" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { update_inline("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="PDF" Width="110" VisibleIndex="5">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <a href='<%# ShowFile(SafeValue.SafeString(Eval("Id"))) %>' target="_blank" style="display: <%# ShowFile(SafeValue.SafeString(Eval("Id"))).Length==0?"none":"block" %>">View</a>

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="110" VisibleIndex="5">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="ASPxButton17" runat="server" Text="Upload PDF" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { upload_inline("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="110" VisibleIndex="5">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>

                                                                        <dxe:ASPxButton Visible='<%# Eval("IsHadInvoice")=="YES"?false:true  %>' ID="btn_CreateInv" OnInit="btn_CreateInv_Init" runat="server" Text="Create Inv" Width="60" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { create_inv_inline("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                        <div style="display: none">
                                                                            <dxe:ASPxTextBox ID="txt_PartyTo" runat="server" Width="380px" Text='<%# Bind("ConsigneeInfo") %>'>
                                                                            </dxe:ASPxTextBox>

                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_cargo_id" ClientInstanceName="txt_cargo_id" Width="170" ReadOnly="true"
                                                                runat="server" Text='<%# Eval("Id") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </div>

                                                        <table style="width: 980px">
                                                            <tr>
                                                                <td width="90%"></td>
                                                                <td width="110">
                                                                    <dxe:ASPxButton ID="ASPxButton4" class="btn btn-primary" Width="100" runat="server" Text="Save&保存" Enabled='<%# SafeValue.SafeString(Eval("CargoStatus"))=="COMPLETED"?false:true %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false" ClientSideEvents-Click='<%# "function(s) { save_order() }"  %>'>
                                                                        <%--                                                                    <ClientSideEvents Click="function(s,e) {
                                                                    grd_Det.PerformCallback('UpdateClose');grd_Det.Refresh();
                                                                    }" />--%>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td width="110">
                                                                    <dxe:ASPxButton ID="ASPxButton5" Width="60" runat="server" Text="Close" AutoPostBack="false"
                                                                        UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                                                    grd_Det.CancelEdit();
                                                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="table table-bordered" style="width: 980px">
                                                            <tr>
                                                                <td colspan="8">
                                                                    <table>
                                                                        <tr>
                                                                            <td>唛头号：</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_ExpBkgN" Width="100%"
                                                                                    runat="server" Text='<%# Eval("BookingNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>单号：</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_ExJobOrder" Width="100%"
                                                                                    runat="server" Text='<%# Eval("DoNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="display: none">柜号：</td>
                                                                            <td style="display: none">
                                                                                <dxe:ASPxTextBox ID="txt_ContNo" Width="100%"
                                                                                    runat="server" Text='<%# Eval("ContNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>当前状态:</td>
                                                                            <td style="text-underline-position: below; display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">
                                                                                <dxe:ASPxLabel ID="lbl_Status" runat="server"></dxe:ASPxLabel>
                                                                                &nbsp;&nbsp;

                                                <div style="display: none">
                                                    <dxe:ASPxButton ID="btn_Status" ClientInstanceName="btn_Status" Width="100" runat="server" Text="下单">
                                                        <ClientSideEvents Click="function(s,e) {
                                                        if(confirm('确定对该订单做'+btn_Status.GetText()+'操作吗?')){
                                                                    grd_Det.GetValuesOnCustomCallback('UpdateStatus',OnCallBack);
                                                                    }}" />
                                                    </dxe:ASPxButton>
                                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="80" ID="cmb_CargoStatus"
                                                        Value='<%# Bind("CargoStatus")%>'>
                                                        <Items>
                                                            <dxe:ListEditItem Text="待确认" Value="USE" />
                                                            <dxe:ListEditItem Text="已下单" Value="ORDER" />
                                                            <dxe:ListEditItem Text="已入库" Value="IN" />
                                                            <dxe:ListEditItem Text="已排库" Value="PICKED" />
                                                            <dxe:ListEditItem Text="已出库" Value="OUT" />
                                                            <dxe:ListEditItem Text="已装船" Value="SHIPMENT" />
                                                            <dxe:ListEditItem Text="已出港" Value="DEPARTURE" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="180px"><font size="1" color="red">*</font>承运人/SHIPPER:</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxTextBox ID="txt_Carrier" runat="server" Width="100%" Text='<%# Bind("ShipperInfo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><font size="1" color="red">*</font>货主/CONSIGNEE:</td>
                                                                <td colspan="5">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div id="ddl_party">
                                                                                    <dxe:ASPxDropDownEdit ID="DropDownEdit_Party" runat="server" ClientInstanceName="DropDownEdit_Party"
                                                                                        EnableAnimation="False" Width="380px" AllowUserInput="False" Text='<%# Bind("ConsigneeInfo") %>'>
                                                                                        <DropDownWindowTemplate>
                                                                                            <dxwgv:ASPxGridView ID="gridParty" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridParty"
                                                                                                Width="100%" DataSourceID="dsCustomerMast" KeyFieldName="SequenceId" OnCustomJSProperties="gridParty_CustomJSProperties">
                                                                                                <Settings ShowFilterRow="true" />
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="PartyId" VisibleIndex="0" Width="50px">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="Name" VisibleIndex="0" Width="100%">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                </Columns>
                                                                                                <ClientSideEvents RowClick="RowClickHandlers" />
                                                                                            </dxwgv:ASPxGridView>
                                                                                        </DropDownWindowTemplate>
                                                                                    </dxe:ASPxDropDownEdit>
                                                                                </div>
                                                                                <div id="txt_party" style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Party" ClientInstanceName="txt_Party" runat="server" Width="380px" Text='<%# Bind("ConsigneeInfo") %>'>
                                                                                    </dxe:ASPxTextBox>

                                                                                </div>
                                                                                <div style="display: none">
                                                                                    <input type="text" id="partyId" value="<%# Eval("ConsigneeInfo") %>" />
                                                                                    <dxe:ASPxTextBox ID="txt_Party1" ClientInstanceName="txt_Party1" runat="server" Width="380px" Text='<%# Bind("ConsigneeInfo") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxCheckBox ID="ckb_Input" runat="server" Text="是否手动输入" ClientInstanceName="ckb_Input">
                                                                                    <ClientSideEvents CheckedChanged="function(s,e){
                                        CheckInput();
                                        }" />
                                                                                </dxe:ASPxCheckBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                                <td><font size="1" color="red">*</font>个人/公司</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ClientInstanceName="cmb_Responsible" ID="cmb_Responsible" Width="100%" runat="server" Value='<%# Bind("Responsible") %>' AutoPostBack="false">
                                                                        <ClientSideEvents TextChanged="function(s,e){
                                             CheckResponsible();
                                            }" />
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="个人" Value="PERSON" />
                                                                            <dxe:ListEditItem Text="公司" Value="COMPANY" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="person">
                                                                <td><font size="1" color="red">*</font>货主信息/CONSIGNEE UEN/IC</td>
                                                                <td width="40px">IC:</td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxTextBox ID="txt_ConsigneeRemark" ClientInstanceName="txt_ConsigneeRemark" runat="server" Width="100%" Text='<%# Bind("ConsigneeRemark") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                    <div style="display: none">
                                                                        <input type="text" id="ic" value="<%# Eval("ConsigneeRemark") %>" />
                                                                        <dxe:ASPxTextBox ID="txt_ConsigneeRemark1" ClientInstanceName="txt_ConsigneeRemark1" runat="server" Width="380px" Text='<%# Bind("ConsigneeRemark") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </div>
                                                                </td>
                                                                <td width="40px">UEN:</td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxTextBox ID="txt_ConsigneeEmail" ClientInstanceName="txt_ConsigneeEmail" runat="server" Width="100%" Text='<%# Bind("ConsigneeEmail") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxTextBox ID="txt_ConsigneeEmail1" ClientInstanceName="txt_ConsigneeEmail1" runat="server" Width="380px" Text='<%# Bind("ConsigneeEmail") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><font size="1" color="red">*</font>邮箱地址/EMAIL：</td>
                                                                <td>EMAIL 1:</td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxTextBox ID="txt_Email1" ClientInstanceName="txt_Email1" runat="server" Width="100%" Text='<%# Bind("Email1") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxTextBox ID="txt_Email1_1" ClientInstanceName="txt_Email1_1" runat="server" Width="380px" Text='<%# Bind("Email1") %>'>
                                                                        </dxe:ASPxTextBox>

                                                                    </div>
                                                                </td>
                                                                <td>EMAIL 2:</td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxTextBox ID="txt_Email2" ClientInstanceName="txt_Email2" runat="server" Width="100%" Text='<%# Bind("Email2") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxTextBox ID="txt_Email2_1" ClientInstanceName="txt_Email2_1" runat="server" Width="380px" Text='<%# Bind("Email2") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><font size="1" color="red">*</font>联系电话/CONTACT：<br />
                                                                </td>
                                                                <td colspan="7">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td width="100px">TEL 1/座机1:</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Tel1" ClientInstanceName="txt_Tel1" runat="server" Width="100%" Text='<%# Bind("Tel1") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Tel1_1" ClientInstanceName="txt_Tel1_1" runat="server" Width="380px" Text='<%# Bind("Tel1") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td width="100px">TEL 2/座机2:</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Tel2" ClientInstanceName="txt_Tel2" runat="server" Width="100%" Text='<%# Bind("Tel2") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Tel2_1" ClientInstanceName="txt_Tel2_1" runat="server" Width="380px" Text='<%# Bind("Tel2") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td width="110px">MOBILE 1/手机1：</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Mobile1" ClientInstanceName="txt_Mobile1" runat="server" Width="100%" Text='<%# Bind("Mobile1") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Mobile1_1" ClientInstanceName="txt_Mobile1_1" runat="server" Width="380px" Text='<%# Bind("Mobile1") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td width="110px">MOBILE 2/手机2:</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Mobile2" ClientInstanceName="txt_Mobile2" runat="server" Width="100%" Text='<%# Bind("Mobile2") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Mobile2_1" ClientInstanceName="txt_Mobile2_1" runat="server" Width="380px" Text='<%# Bind("Mobile2") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><font size="1" color="red">*</font>地址/ADDRESS：</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxMemo ID="memo_Desc1" runat="server" ClientInstanceName="memo_Desc1"
                                                                        Width="100%" Rows="2" Text='<%# Bind("Desc1") %>'>
                                                                    </dxe:ASPxMemo>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxMemo ID="memo_Desc1_1" ClientInstanceName="memo_Desc1_1" runat="server" Width="100%" Rows="2" Text='<%# Bind("Desc1") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">货主/CONSIGNEE 是 收货人/RECEIVER PARTY ?</td>
                                                                <td colspan="6">
                                                                    <dxe:ASPxCheckBox ID="ckb_IsHold" runat="server" ClientInstanceName="ckb_IsHold" Checked='<%# SafeValue.SafeBool(Eval("IsHold"),false) %>'>
                                                                        <ClientSideEvents CheckedChanged="function(s,e){
                                        CheckConsignee();
                                        }" />
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="receiver">
                                                                <td><font size="1" color="red">*</font>收货人/RECEIVER PARTY:</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxTextBox ID="txt_ClientId" ClientInstanceName="txt_ClientId" runat="server" Text='<%# Bind("ClientId") %>' Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="contact">
                                                                <td><font size="1" color="red">*</font>联系电话/CONTACT NUMBER</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxTextBox ID="txt_ClientEmail" runat="server" Text='<%# Bind("ClientEmail") %>' Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="address">
                                                                <td><font size="1" color="red">*</font>送货地址/DELIVERY ADDRESS：</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxMemo ID="memo_Remark1" runat="server"
                                                                        Width="100%" Rows="2" Text='<%# Bind("Remark1") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-row" %>">
                                                                <td>付款/Payable：</td>
                                                                <td width="150">中国付/Freight prepaid:</td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxCheckBox ID="ckb_Prepaid_Ind" ClientInstanceName="ckb_Prepaid_Ind" runat="server" Checked='<%# SafeValue.SafeString(Eval("PrepaidInd"))=="YES"?true:false %>'>
                                                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            CheckPrepaidInd();
                                            }" />
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                                <td width="200">代收/COLLECT ON BEHALF RMB：</td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_Collect_Amount1" ClientInstanceName="spin_Collect_Amount1" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="2" Width="100" Value='<%# Bind("CollectAmount1") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td width="200">代收/COLLECT ON BEHALF SGD：</td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_Collect_Amount2" ClientInstanceName="spin_Collect_Amount2" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="2" Width="100" Value='<%# Bind("CollectAmount2") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-row" %>">
                                                                <td>GST/税</td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxComboBox ID="cmb_Duty_Payment" ClientInstanceName="cmb_Duty_Payment" Width="100%" runat="server" Value='<%# Bind("DutyPayment") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="税中国付" Value="DUTY PAID" />
                                                                            <dxe:ListEditItem Text="税新加坡付" Value="DUTY UNPAID" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_OtherFee1" ClientInstanceName="spin_OtherFee1" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("GstFee") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>是否单独清关/Permit Fee:</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_IsBill" ValueField='<%# Bind("IsBill") %>' runat="server" Width="100">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="是" Value="YES" />
                                                                            <dxe:ListEditItem Text="否" Value="NO" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_CheckIn" ClientInstanceName="cmb_CheckIn" Width="100" runat="server" Value='<%# Bind("PermitBy") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="中国付" Value="PAID" Selected="true" />
                                                                            <dxe:ListEditItem Text="新加坡付" Value="UNPAID" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>

                                                                    <dxe:ASPxSpinEdit ID="spin_OtherFee2" ClientInstanceName="spin_OtherFee2" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("PermitFee") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>

                                                            </tr>
                                                            <tr style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-row" %>">
                                                                <td>货运条款/INCO-TERM:</td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxComboBox ID="cmb_Incoterm" ClientInstanceName="cmb_Incoterm" Width="100" runat="server" Value='<%# Bind("Incoterm") %>'>
                                                                        <ClientSideEvents TextChanged="function(s,e){
                                            CheckIncoTerm();
                                            }" />
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="EX-WORK" Value="EX-WORK " />
                                                                            <dxe:ListEditItem Text="FOB" Value="FOB" />
                                                                            <dxe:ListEditItem Text="CIF" Value="CIF" />
                                                                            <dxe:ListEditItem Text="CFR" Value="CFR" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td style="display: none">海运费/OCEAN FREIGHT：
                                                                </td>
                                                                <td style="display: none">
                                                                    <dxe:ASPxSpinEdit ID="spin_MiscFee" ClientInstanceName="spin_MiscFee" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" DecimalPlaces="2" Width="100" Value='<%# Bind("Ocean_Freight") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">手续费/Handling Fee</td>
                                                                <td style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">
                                                                    <dxe:ASPxSpinEdit ID="spin_OtherFee3" ClientInstanceName="spin_OtherFee3" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("HandlingFee") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">其他/Other Fee</td>
                                                                <td style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">
                                                                    <dxe:ASPxSpinEdit ID="spin_OtherFee4" ClientInstanceName="spin_OtherFee4" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("OtherFee") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>备注</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxMemo ID="memo_Remark2" ReadOnly="false" runat="server"
                                                                        Width="100%" Rows="2" Text='<%# Bind("Remark2") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton ID="ASPxButton3" Width="80" runat="server" Text="添加货物" Enabled='<%# SafeValue.SafeString(Eval("Id"),"")!="" %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                    grd_Stock.AddNewRow();
                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButton ID="ASPxButton7" Width="90" runat="server" Text="从Excel导入货物" Enabled='<%# SafeValue.SafeString(Eval("Id"),"")!="" %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                            isUpload=true;
                                    PopupExcel();
                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>总箱数
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_TotQty" ClientInstanceName="spin_TotQty" runat="server"
                                                                        ReadOnly="false" NumberType="Integer" Increment="2" Width="60" Value='<%# Bind("Qty") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="txt_PackType" ClientInstanceName="txt_PackType" Width="80" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("PackTypeOrig") %>' DropDownStyle="DropDown">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="CTN/箱" Value="CTN" />
                                                                            <dxe:ListEditItem Text="PKG/件" Value="PKG" />
                                                                            <dxe:ListEditItem Text="BAG/包" Value="BAG" />
                                                                            <dxe:ListEditItem Text="PAL/卡板" Value="PAL" />
                                                                            <dxe:ListEditItem Text="TON/吨" Value="TON" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>总量(KG)
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_Weight" ClientInstanceName="spin_Weight" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="2" Width="80" Value='<%# Bind("Weight") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>体积(CBM/M3)
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_M3" ClientInstanceName="spin_M3" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="2" Width="80" Value='<%# Bind("Volume") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>货币/Currency</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_Fee1CurrId" Width="60" runat="server" Value='<%# Bind("Currency") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="SGD" Value="SGD" Selected="true" />
                                                                            <dxe:ListEditItem Text="RMB" Value="RMB" />
                                                                            <dxe:ListEditItem Text="USD" Value="USD" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td width="110">
                                                                    <dxe:ASPxButton ID="ASPxButton1" Width="80" runat="server" Text="修改" Enabled='<%# SafeValue.SafeString(Eval("Id"),"")!="" %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                                                    grd_Det.GetValuesOnCustomCallback('Update',OnCallBack);
                                                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <%--  <td width="110">
                                                                <dxe:ASPxButton ID="ASPxButton6" Width="100" runat="server" Text="刷新" Enabled='<%# SafeValue.SafeString(Eval("Id"),"")!="" %>'
                                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s,e) {
                                                                    grd_Det.GetValuesOnCustomCallback('Update',OnCallBack);
                                                                    }" />
                                                                </dxe:ASPxButton>
                                                            </td>--%>
                                                            </tr>
                                                        </table>
                                                        <dxwgv:ASPxGridView ID="grd_Stock" ClientInstanceName="grd_Stock" runat="server" DataSourceID="dsJobStock"
                                                            KeyFieldName="Id" Width="980" OnBeforePerformDataSelect="grd_Stock_BeforePerformDataSelect" OnCustomDataCallback="grd_Stock_CustomDataCallback"
                                                            OnInit="grd_Stock_Init" OnInitNewRow="grd_Stock_InitNewRow" OnRowInserting="grd_Stock_RowInserting" OnRowUpdating="grd_Stock_RowUpdating"
                                                            OnRowDeleting="grd_Stock_RowDeleting">
                                                            <SettingsBehavior ConfirmDelete="True" />
                                                            <SettingsPager Mode="ShowAllRecords">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="Inline" />
                                                            <Settings />
                                                            <Columns>
                                                                <dxwgv:GridViewDataColumn Caption="#" Width="110" VisibleIndex="0">
                                                                    <DataItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton17" runat="server" Text="修改" Width="40" AutoPostBack="false"
                                                                                        ClientSideEvents-Click='<%# "function(s) { grd_Stock.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton18" runat="server"
                                                                                        Text="删除" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Stock.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </DataItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton19" runat="server" Text="保存" Width="40" AutoPostBack="false"
                                                                                        ClientSideEvents-Click='<%# "function(s) {update_stock_inline("+Container.VisibleIndex+") }"  %>'>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton20" runat="server" Text="取消" Width="40" AutoPostBack="false"
                                                                                        ClientSideEvents-Click='<%# "function(s) { grd_Stock.CancelEdit() }"  %>'>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EditItemTemplate>
                                                                </dxwgv:GridViewDataColumn>
                                                                <dxwgv:GridViewDataColumn FieldName="SortIndex" Caption="序号" VisibleIndex="1" Width="20"
                                                                    SortIndex="0" SortOrder="Ascending">
                                                                </dxwgv:GridViewDataColumn>
                                                                <dx:GridViewBandColumn Caption="" VisibleIndex="1">
                                                                    <Columns>
                                                                        <dxwgv:GridViewDataColumn FieldName="Marks1" Caption="COMMODITY" VisibleIndex="1" Width="100">
                                                                        </dxwgv:GridViewDataColumn>
                                                                        <dxwgv:GridViewDataColumn FieldName="Marks2" Caption="品 名" VisibleIndex="1" Width="100">
                                                                        </dxwgv:GridViewDataColumn>
                                                                    </Columns>
                                                                </dx:GridViewBandColumn>
                                                                <dxwgv:GridViewDataColumn FieldName="Uom1" Caption="单位" VisibleIndex="1" Width="100">
                                                                    <DataItemTemplate>
                                                                        <%# ShowUom(Eval("Uom1")) %>
                                                                    </DataItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <dxe:ASPxComboBox ID="txt_Uom1" ClientInstanceName="txt_Uom1" Width="100" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Uom1") %>' DropDownStyle="DropDown">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="CTN/箱" Value="CTN" />
                                                                                <dxe:ListEditItem Text="PKG/件" Value="PKG" />
                                                                                <dxe:ListEditItem Text="BAG/包" Value="BAG" />
                                                                                <dxe:ListEditItem Text="PAL/卡板" Value="PAL" />
                                                                                <dxe:ListEditItem Text="TON/吨" Value="TON" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </EditItemTemplate>
                                                                </dxwgv:GridViewDataColumn>
                                                                <dxwgv:GridViewDataSpinEditColumn FieldName="Qty2" Caption="数量(PCS)" VisibleIndex="1" Width="100">
                                                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer"></PropertiesSpinEdit>
                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                                <dxwgv:GridViewDataColumn FieldName="Uom2" Caption="货币" VisibleIndex="1" Width="30" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <dxe:ASPxComboBox ID="cmb_Incoterm" Width="100%" runat="server" Value='<%# Bind("Uom2") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="SGD" Value="SGD " />
                                                                                <dxe:ListEditItem Text="RMB" Value="RMB" />
                                                                                <dxe:ListEditItem Text="USD" Value="USD" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </EditItemTemplate>
                                                                </dxwgv:GridViewDataColumn>
                                                                <dxwgv:GridViewDataSpinEditColumn FieldName="Price1" Caption="单价" VisibleIndex="2" Width="100">
                                                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="3" NumberType="Float"></PropertiesSpinEdit>
                                                                </dxwgv:GridViewDataSpinEditColumn>

                                                                <dxwgv:GridViewDataSpinEditColumn FieldName="Price2" Caption="合计金额" VisibleIndex="5" Width="100">
                                                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="3" NumberType="Float"></PropertiesSpinEdit>
                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                            <Settings ShowFooter="True" />
                                                            <TotalSummary>
                                                                <dxwgv:ASPxSummaryItem FieldName="Price2" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                                            </TotalSummary>
                                                        </dxwgv:ASPxGridView>
                                                        <table style="width: 980px; background-color: #e1e0e0; display: none">
                                                            <tr style="text-align: right">
                                                                <td width="95%"></td>
                                                                <td>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxLabel ID="lbl_stockamt" ClientInstanceName="lbl_stockamt" runat="server" Text=""></dxe:ASPxLabel>
                                                                    </div>
                                                                    <dxe:ASPxLabel ID="lbl_total" ClientInstanceName="lbl_total" runat="server" Text=""></dxe:ASPxLabel>

                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </EditForm>
                                                </Templates>
                                                <Settings ShowFooter="false" />
                                                <TotalSummary>
                                                    <dxwgv:ASPxSummaryItem FieldName="Weight" ShowInGroupFooterColumn="Weight" SummaryType="Sum"
                                                        DisplayFormat="{0:0.000}" />
                                                    <dxwgv:ASPxSummaryItem FieldName="Volume" ShowInGroupFooterColumn="Volume" SummaryType="Sum"
                                                        DisplayFormat="{0:0.000}" />
                                                    <dxwgv:ASPxSummaryItem FieldName="Qty" ShowInGroupFooterColumn="Qty" SummaryType="Sum"
                                                        DisplayFormat="{0:0}" />
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
                                <dxtc:TabPage Text="Accounts Billing" Visible="true" Name="Costing">
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
                                                        <dxe:ASPxButton ID="ASPxButton11" Width="150" runat="server" Text="Add Charges" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                         PopupCharges();
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

                                            <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server" OnHtmlDataCellPrepared="grid_Cost_HtmlDataCellPrepared"
                                                DataSourceID="dsCosting" KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect"
                                                OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting"
                                                OnRowUpdating="grid_Cost_RowUpdating" OnRowInserted="grid_Cost_RowInserted" OnRowUpdated="grid_Cost_RowUpdated" OnHtmlEditFormCreated="grid_Cost_HtmlEditFormCreated" OnRowDeleting="grid_Cost_RowDeleting">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <SettingsEditing Mode="EditFormAndDisplayRow" />
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
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
                                                        <table width="100%">
                                                            <tr>
                                                                <td class="lbl">Charge Code
                                                                </td>
                                                                <td class="ctl">
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
                                                                <td class="ctl">
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
                                                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_ContNo" Text='<%# Bind("ContNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td class="lbl">Cont Type</td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContType") %>'></dxe:ASPxComboBox>

                                                                            </td>
                                                                            <td class="lbl">Line Type</td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ASPxComboBox1" ClientInstanceName="txt_ContType" runat="server" Width="60" ValueField="containerType" TextField="containerType" Value='<%# Bind("LineType") %>'>
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
                                                                    <table width="90%">
                                                                        <tr>
                                                                            <td class="lbl">Qty
                                                                            </td>
                                                                            <td class="lbl">UOC
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
                                                                                <dxe:ASPxTextBox Width="80px" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit"
                                                                                    runat="server" Text='<%# Bind("Unit") %>'>
                                                                                </dxe:ASPxTextBox>
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

                                <dxtc:TabPage Text="Event Log" Name="Activity" Visible="false">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxwgv:ASPxGridView ID="gv_activity" runat="server" ClientInstanceName="gv_activity" AutoGenerateColumns="false" DataSourceID="dsActivity" KeyFieldName="Id" OnBeforePerformDataSelect="gv_activity_BeforePerformDataSelect" Width="850">
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                <Columns>
                                                    <dxwgv:GridViewDataDateColumn FieldName="CreateDateTime" Caption="DateTime" PropertiesDateEdit-DisplayFormatString="yyyy/MM/dd hh:mm" SortOrder="Descending" Width="150"></dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Controller" Caption="User" Width="100"></dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Note"></dxwgv:GridViewDataColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>

                                <dxtc:TabPage Text="Control" Name="Close File" Visible="false">
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
                                                        <%--<dxe:ASPxButton ID="btn_JobVoid" ClientInstanceName="btn_JobVoid" runat="server" Text="View " AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>' Width="100">
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
            <dxpc:ASPxPopupControl ID="popubCtr2" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr2"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="450"
                Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
