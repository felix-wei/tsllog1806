<%@ page language="C#" autoeventwireup="true" validaterequest="false" enableviewstate="false" codefile="JobEdit.aspx.cs" inherits="PagesContTrucking_Job_JobEdit" %>

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
    <style type="text/css">
        .show {
            display: block;
        }

        .show-cell {
            display: table-cell;
        }

        .show-row {
            display: table-row;
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

        td .highlight {
            border-top: solid 2px #808080;
            border-bottom: solid 2px #808080;
            font-weight: bold;
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
        function GoJob_Page(v){
            if(v=="Transfer"){
                var client=encodeURIComponent(btn_ClientId.GetText());
                popubCtrPic.SetHeaderText('Create Transfer Job ');
                popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/CreateJobOrder.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + client+"&action=FRT");
                popubCtrPic.Show();
                //parent.navTab.openTab(v, "/Modules/Tpt/TptJobList.aspx?type=TR" , { title:v, fresh: false, external: true });
            }else if(v=="Delivery"){
                var client=encodeURIComponent(btn_ClientId.GetText());
                //parent.navTab.openTab(v, "/Modules/Tpt/TptJobList.aspx?type=DO" , { title:v, fresh: false, external: true });
                popubCtrPic.SetHeaderText('Create Delivery Order ');
                popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/CreateJobOrder.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + client+"&action=WDO");
                popubCtrPic.Show();
            }
            else if(v=="ShowDO"){
                var client=encodeURIComponent(btn_ClientId.GetText());
                popubCtrPic.SetHeaderText('Show Delivery Order ');
                popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/ShowDeliveryOrder.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + client);
                popubCtrPic.Show();
            }
        }
        function onCallBack(v) {
            if (v == "PrintHaulier"){
                window.open('/PagesContTrucking/Report/RptPrintView.aspx?doc=haulier&no=' + txt_JobNo.GetText() + "&type=" + cbb_JobType.GetValue());
            }
            else if(v=="DeleteAll"){
                grid1.Refresh();
            }
            else if(v=="Delete Fail"){
                alert('Pls Select at least one Quotation');
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
        function onAutoCallBack(v) {
            if(v=="Trip")
            {
                gv_tpt_trip.Refresh();
            }
            else if (v != null && v.indexOf('Action Success!') >= 0) {
                alert(v);
                grid_wh.Refresh();
            }
            
        }
        function onNoCallBack(v){
            var paras = new Array();
            paras=v.split('_');
            var par = paras[0];
            var jobno = paras[1];
            if(par=="J"){
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            }
            else if(par=="Q"){
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            }
            else if(par=="C"){
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
                grid_job.Refresh();
                // window.location='JobEdit.aspx?no='+jobno; 
            }
            else
            {
                alert(v);
                grid_job.Refresh();
            }
        }
        function onCostingCallBack(v) {
            if (v != null && v.length > 0) {
                grid_Cost.Refresh();
            }

        }
        function onQuotedCallBack(v){
            if (v != null && v.indexOf('Action Success!') >= 0) {
                alert(v);
                grid1.Refresh();
            }
        }
        function onPermitCallBack(v){
            if (v != null && v.indexOf('Action Success!') >= 0) {
                alert(v);
                grid_permit.Refresh();
            }
        }
        var isUpload = false;
        function PopupPhotoList() {
            popubCtrPic.SetHeaderText('Attachment List');
            popubCtrPic.SetContentUrl('/SelectPage/PhotoList.aspx?Type=CTM&Sn=0&jobNo=' + txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function PopupMultipleUploadPhoto() {
            popubCtrPic.SetHeaderText('Upload Attachment');
            popubCtrPic.SetContentUrl('/Modules/Upload/MultipleUpload.aspx?Type=CTM&Sn=0&jobNo=' + txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function PopupUploadPhoto() {
            popubCtrPic.SetHeaderText('Upload Attachment');
            popubCtrPic.SetContentUrl('../Upload.aspx?Type=CTM&Sn=0&jobNo=' + txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function PopupJobRate() {
            var client=encodeURIComponent(btn_ClientId.GetText());
            popubCtr.SetHeaderText('Job Rate ');
            popubCtr.SetContentUrl('../SelectPage/SelectJobRate.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' +client);
            popubCtr.Show();
        }
        function PopupJobHouse() {
		    var client=encodeURIComponent(btn_PartyId.GetText());
            popubCtrPic.SetHeaderText('Stock List');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/SelectStock.aspx?no=' + txt_JobNo.GetText() + "&client=" + client+"&jobType="+ cbb_JobType.GetValue());
            popubCtrPic.Show();
        }
        function AfterPopub(){
            popubCtrPic.Hide();
            popubCtrPic.SetContentUrl('about:blank');
            grid_wh.Refresh();
            grid_stockOut.Refresh();
            grid_OtherOut.Refresh();
            gv_tpt_trip.Refresh();

            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_Cost.Refresh();
        }
        function AfterPopubRate(){
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
                grid_job.Refresh();
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
        function parentNotic(title, body, color, more) {
            if (parent.notice) {
                if (more) {
                    if (more.callback) {
                        more.callback(confirm(title));
                    }
                } else {
                    parent.notice(title, body, color, more);
                }
            } else {
                if (more && more.type == 'confirm') {
                    if (more.callback) {
                        more.callback(confirm(title));
                    }
                }
                console.log(title, body);
            }
        }
        function job_callback(v) {
            if(v!=null&&v.length>0){
                parentNotic(v,'','error');
            }else{
                grid_job.Refresh();
                parentNotic('Job save successful!','','success');
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
        function container_batch_edit() {
            var jobno = txt_JobNo.GetText();
            var jobtype=lbl_JobType.GetText();
            //console.log(jobno);
            Popup_ContainerBatchEdit(jobno,jobtype, container_batch_edit_cb);
        }
        function container_batch_edit_cb(v) {
            console.log(v);
            if (v == "success") {
 
                grid_Cont.CancelEdit();
 
            }
        }


        function container_batch_add() {
            var jobno = txt_JobNo.GetText();
            var jobtype=lbl_JobType.GetText();
            //console.log(jobno);
            Popup_ContainerBatchAdd(jobno,jobtype, container_batch_add_cb);
        }
        function container_batch_add_cb(v) {
            console.log(v);
            if (v.toLowerCase() == "success") {
                parentNotic('Save successful!','','success');
                //if (grid_Trip) {
                //    grid_Trip.CancelEdit();
                //}
                grid_Cont.CancelEdit();
                //setTimeout(function () {
                //    grid_Cont.Refresh();
                //}, 500);
            }else{
                if(v.toLowerCase().indexOf('success')>=0){
                    parentNotic('Save successful!',v,'success');
                }else{
                    parentNotic('Save Error!',v,'error');
                }
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
                var jobType=cbb_JobType.GetValue();
                if(jobType=="IMP"){
                    cbb_StatusCode.SetValue('Import');
                }
                if(jobType=="EXP"){
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
        function transport_trip_add_cb(v) {
            if (v == "success") {
                //if (grid_Trip) {
                //    grid_Trip.CancelEdit();
                //}
                gv_tpt_trip.Refresh();
                //grid_Cont.CancelEdit();
                //grid_Cont.Refresh();
                var jobType=cbb_JobType.GetValue();
                if(jobType=="IMP"){
                    cbb_StatusCode.SetValue('Import');
                }
                if(jobType=="EXP"){
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
        function container_trip2_update_cb(v) {
            gv_cont2_trip.CancelEdit();
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
        function container_trip2_delete_cb(v) {

            gv_cont2_trip.CancelEdit();
            setTimeout(function () {
                //if (grid_Trip) {
                //    grid_Trip.CancelEdit();
                //}
                gv_cont2_trip.Refresh();
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
            if (grid_Trip) {
                grid_Trip.CancelEdit();
            }
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
        function transport_actions(actionCode,callback){
            console.log('==',actionCode);
            gv_tpt_trip.GetValuesOnCustomCallback(actionCode,callback);
        }
        var ContainerTripIndex = 0;
        function printJobSheet() {
            window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=4&no=" + txt_search_JobNo.GetText());
        }
        function printJobSheetForTrip(id,type) {
            window.open('/PagesContTrucking/Report/RptPrintView.aspx?doc=job_sheet&no=' + txt_JobNo.GetText() + "&refType=" + type+"&id="+id);
        }
        function printJobCost() {
            window.open("/PagesContTrucking/PrintJob.aspx?o=" + txt_search_JobNo.GetText());
        }
        function printTallySheetIndented() {
            window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=tallysheet_indented&no=" + txt_search_JobNo.GetText()+'&refType='+cbb_JobType.GetValue());
        }
        function printTallySheetConfirmed(type) {
            window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=tallysheet_confirmed&no=" + txt_search_JobNo.GetText()+'&refType='+cbb_JobType.GetValue()+'&type='+type);
        }
        function printStockTallySheet() {
            window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=stock_tallysheet&no=" + txt_search_JobNo.GetText()+'&refType='+cbb_JobType.GetValue());
        }
        function ShowGR(masterId) {
            parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoInEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
            //window.location = "DoInEdit.aspx?no=" + masterId;
        }
        function ShowDO(masterId) {
            parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
            //window.location = "DoOutEdit.aspx?no=" + masterId;
        }
        function dimension_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Dimensionline_'+rowIndex, dimension_inline_callback);
            }, config.timeout);
           
        }
        function dimension_inline1(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_stockOut.GetValuesOnCustomCallback('Dimensionline_'+rowIndex, dimension_inline_callback);
            }, config.timeout);
           
        }
        function dimension_inline2(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_OtherOut.GetValuesOnCustomCallback('Dimensionline_'+rowIndex, dimension_inline_callback);
            }, config.timeout);
           
        }
        function dimension_inline_callback(res){
            popubCtrPic.SetHeaderText('Dimension ');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/DimensionForCargo.aspx?id=' + res);
            popubCtrPic.Show();
        }
        function marubeni_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Marubeniline_'+rowIndex, marubeni_inline_callback);
            }, config.timeout);
           
        }
        function marubeni_inline1(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_stockOut.GetValuesOnCustomCallback('Marubeniline_'+rowIndex, marubeni_inline_callback);
            }, config.timeout);
           
        }
        function marubeni_inline_callback(res){
            console.log(res);
            popubCtrPic.SetHeaderText('Marubeni ');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/MarubeniForCargo.aspx?id=' + res);
            popubCtrPic.Show();
        }
        function process_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Dimensionline_'+rowIndex, process_inline_callback);
            }, config.timeout);
        }
        function process_inline1(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_stockOut.GetValuesOnCustomCallback('Dimensionline_'+rowIndex, process_inline_callback);
            }, config.timeout);
        }
        function process_inline2(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_OtherOut.GetValuesOnCustomCallback('Dimensionline_'+rowIndex, process_inline_callback);
            }, config.timeout);
        }
        function process_inline_callback(res){
            popubCtrPic.SetHeaderText('Process ');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/ProcessForCargo.aspx?id=' + res+'&no='+txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function copy_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Copyonline_'+rowIndex, copy_inline_callback);
            }, config.timeout);
        }
        function copy_inline1(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_stockOut.GetValuesOnCustomCallback('Copyonline_'+rowIndex, copy_inline1_callback);
            }, config.timeout);
        }
        function copy_cargo_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('CopyonCargoline_'+rowIndex, copy_inline_callback);
            }, config.timeout);
        }
        function copy_inline_callback(res){
            if(res=="Success")
                grid_wh.Refresh();
        }
        function copy_inline1_callback(res){
            if(res=="Success")
                grid_stockOut.Refresh();
        }
        function AfterPopubDimension(){
            popubCtrPic.Hide();
            popubCtrPic.SetContentUrl('about:blank');
            grid_wh.Refresh();
        }
        function cargo_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Cargoline_'+rowIndex, cargo_inline_callback);
            }, config.timeout);
        }
        function cargo_inline_callback(res){
            popubCtrPic.SetHeaderText('Cargo List');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/CargoList.aspx?id='+res);
            popubCtrPic.Show();
        }
        function cargo_inline1(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_stockOut.GetValuesOnCustomCallback('Cargoline_'+rowIndex, cargo_inline_callback);
            }, config.timeout);
        }
        function cargo_inline2(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_OtherOut.GetValuesOnCustomCallback('Cargoline_'+rowIndex, cargo_inline_callback);
            }, config.timeout);
        }
        function upload_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_wh.GetValuesOnCustomCallback('Uploadline_'+rowIndex, upload_inline_callback);
            }, config.timeout);
        }
        function upload_inline1(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_stockOut.GetValuesOnCustomCallback('Uploadline_'+rowIndex, upload_inline_callback);
            }, config.timeout);
        }
        function upload_inline_callback(res){
            var ar = res.split('_');
            popubCtrPic.SetHeaderText('Upload Attachment');
            popubCtrPic.SetContentUrl('../Upload.aspx?Type=CTM&Sn=' +ar[0]+'&ContNo='+ar[1]+'&jobNo='+txt_JobNo.GetText());
            popubCtrPic.Show();
        }
        function PrintQuotation(invN, docType) {
            window.open('/ReportFreightSea/printview.aspx?document=Quotation&master=' + txt_QuoteNo.GetText() + '&docType=' + cmb_JobStatus.GetText());
        }
        function PrintSingleQuotation(invN, docType) {
            window.open('/ReportFreightSea/printview.aspx?document=SingleQuotation&master=' + txt_QuoteNo.GetText() + '&docType=' + cmb_JobStatus.GetText());
        }
        function PopupMasterRate(){
            var client=encodeURIComponent(btn_ClientId.GetText());
            console.log(client);
            popubCtr.SetHeaderText('Bill Rate');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectRateForQuotation.aspx?quoteNo='+ txt_QuoteNo.GetText()+'&client='+client);
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
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectChgCode.aspx?no=' +txt_QuoteNo.GetText()+'&client='+client+"&type=Quoted");
            popubCtr.Show();
        }
        function PopupCharges() {
            popubCtr.SetHeaderText('Chg Code');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectChargesForCost.aspx?no=' + txt_JobNo.GetText());
            popubCtr.Show();
        }
        function TPT_self_showHide(s,e){
            var v=s.GetText();
            var ar_doc=document.getElementsByClassName('style_self_ind');

            if(v=='Yes'){
                for(var i=0;i<ar_doc.length;i++){
                    var d=ar_doc[i];
                    d.style.display='none';
                }
            }else{
                for(var i=0;i<ar_doc.length;i++){
                    var d=ar_doc[i];
                    d.style.display='';
                }
            }
        }
    </script>
    <script type="text/javascript">
        function SelectAll() {
            jQuery("input[id*='ack_IsDel']").each(function () {
                this.click();
            });
        }
        function SelectAll_Permit() {
            jQuery("input[id*='ack_IsDel_Permit']").each(function () {
                this.click();
            });
        }
        function SelectPhotoAll() {
            if (btn_SelectAll.GetText() == "Select All")
                btn_SelectAll.SetText("UnSelect All");
            else
                btn_SelectAll.SetText("Select All");
            jQuery("input[id*='ack_IsCheck']").each(function () {
                this.click();
            });
        }
        function onPermitCallback(v){
            if(v=="DeleteAll"){
                grid_permit.Refresh();
            }
            else
            {
                alert(v);
            }
        }
        function house_in_delete_cb(v){
            if(v!=null)
                grid_wh.Refresh();
        }
        function house_in_update_cb(v){
            if(v=="Success")
            {
                grid_wh.CancelEdit();
            }
        }
        function house_out_delete_cb(v){
            if(v!=null)
                grid_stockOut.Refresh();
        }
        function house_out_update_cb(v){
            if(v=="Success")
            {
                grid_stockOut.CancelEdit();
            }
        }
        function permit_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid_Cont.GetValuesOnCustomCallback('Permitline_'+rowIndex, permit_inline_callback);
            }, config.timeout);
        }
        function permit_inline_callback(res){
            popubCtrPic.SetHeaderText('Permit ');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/ShowContPermit.aspx?no=' + res);
            popubCtrPic.Show();
        }
        function trip_addnew(type) {
            //lb_newTrip_Type.SetText(type);
            //grid_Trip.AddNewRow();
            gv_cont_trip.GetValuesOnCustomCallback('AddNew_'+type, trip_addnew_callback);
        }
        function trip_addnew_callback(v) {
            if (v == "success") {
                gv_cont_trip.Refresh();
                //setTimeout(function () {
                //    grid_Trip.Refresh();
                //}, 300);
                //setTimeout(function () {
                //    grid_Trip.StartEditRow(grid_Trip.pageRowCount - 1);
                //}, 500);
                //cbb_StatusCode.SetValue('InTransit');
                //console.log(grid_Trip);

                var detail = {
                }
                console.log('=========', detail);
                SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
            }
        }
        function PopupSubJobNo() {
            var jobNo=txt_JobNo.GetText();
			var client=encodeURIComponent(btn_PartyId.GetText());
            popubCtrPic.SetHeaderText('Sub Job List');
            popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/SelectSubJob.aspx?no=' + jobNo+'&client='+client);
            popubCtrPic.Show();
        }
        function BatchCargoEdit(type){
            var jobNo=txt_JobNo.GetText();
            var client=encodeURIComponent(btn_PartyId.GetText());
            parent.navTab.openTab(jobNo+"|"+type, "/PagesContTrucking/SelectPage/BatchEditCargo.aspx?no=" + jobNo+'&type='+type+'&client='+client, { title: jobNo+"|"+type, fresh: false, external: true });
            //popubCtrCargo.SetHeaderText('Batch Edit');
            //popubCtrCargo.SetContentUrl('/PagesContTrucking/SelectPage/BatchEditCargo.aspx?no=' + jobNo+'&type='+type+'&client='+client);
            //popubCtrCargo.Show();
        }
        function onPhotoCallBack(v){
            if(v!=null&&v.indexOf('Success')>-1){
            }
            else
            {
                alert(v);
            }
        }
        function confirm_cargo_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            if(confirm('Confirm Action?')){
                setTimeout(function () {
                    grid_wh.GetValuesOnCustomCallback('ConfirmCargoline_'+rowIndex, confirm_inline_callback);
                }, config.timeout);
            }
        }
        function confirm_cargo_inline1(rowIndex){
            console.log(rowIndex);

            loading.show();
            if(confirm('Confirm Action?')){
                setTimeout(function () {
                    grid_stockOut.GetValuesOnCustomCallback('ConfirmCargoline_'+rowIndex, confirm_inline1_callback);
                }, config.timeout);
            }
        }
        function confirm_inline_callback(res){
            if(res=="Success")
                grid_wh.Refresh();
        }
        function confirm_inline1_callback(res){
            if(res=="Success")
                grid_stockOut.Refresh();
        }
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
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
        <wilson:datasource id="dsJob" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJob" keymember="Id" />
        <wilson:datasource id="dsCont" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobDet1" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsCont1" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobDet1" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsContTrip" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobDet2" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsTransportTrip" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobDet2" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsCraneTrip" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobDet2" keymember="Id" filterexpression="1=0" />
        <%--<wilson:DataSource ID="dsTripLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmTripLog" KeyMember="Id" FilterExpression="1=0" />--%>
        <wilson:datasource id="dsCharge" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobCharge" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsStock" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobStock" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsActivity" runat="server" objectspace="C2.Manager.ORManager" typename="C2.CtmJobEventLog" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsJobPhoto" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.CtmAttachment" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsInvoice" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.XAArInvoice" keymember="SequenceId" filterexpression="1=0" />
        <wilson:datasource id="dsVoucher" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.XAApPayable" keymember="SequenceId" filterexpression="1=0" />
        <wilson:datasource id="dsCosting" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.Job_Cost" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsSalesman" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.XXSalesman" keymember="Code" filterexpression="" />
        <wilson:datasource id="dsContType" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.Container_Type" keymember="id" filterexpression="" />
        <wilson:datasource id="dsTripCode" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.CtmMastData" keymember="Id" filterexpression="type='tripcode'" />
        <wilson:datasource id="dsTerminal" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.CtmMastData" keymember="Id" filterexpression="type='location' and type1='TERMINAL'" />
        <wilson:datasource id="dsCarpark" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.CtmMastData" keymember="Id" filterexpression="type='carpark'" />
        <wilson:datasource id="dsLogEvent" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.CtmJobEventLog" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsZone" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.CtmParkingZone" keymember="id" filterexpression="" />
        <wilson:datasource id="dsWh" runat="server" objectspace="C2.Manager.ORManager" typename="C2.JobHouse"
            keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsStockOut" runat="server" objectspace="C2.Manager.ORManager" typename="C2.JobHouse"
            keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsStockOtherOut" runat="server" objectspace="C2.Manager.ORManager" typename="C2.JobHouse"
            keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsPackageType" runat="server" objectspace="C2.Manager.ORManager" typename="C2.XXUom"
            keymember="Id" filterexpression="CodeType='2'" />
        <wilson:datasource id="dsIncoTerms" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.WhMastData" keymember="Id" filterexpression="Type='IncoTerms'" />
        <wilson:datasource id="ds1" runat="server" objectspace="C2.Manager.ORManager" typename="C2.JobRate" keymember="Id" filterexpression="1=0" />
        <wilson:datasource id="dsRate" runat="server" objectspace="C2.Manager.ORManager" typename="C2.XXChgCode" keymember="SequenceId" filterexpression="Quotation_Ind='Y'" />
        <wilson:datasource id="dsRateType" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.RateType" keymember="Id" />
        <wilson:datasource id="dsRefPermit" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.RefPermit" keymember="Id" filterexpression="1=1" />
        <wilson:datasource id="dsPermit" runat="server" objectspace="C2.Manager.ORManager"
            typename="C2.RefPermit" keymember="Id" filterexpression="1=1" />
        <wilson:datasource id="dsHouse" runat="server" objectspace="C2.Manager.ORManager" typename="C2.JobHouse"
            keymember="Id" filterexpression="1=1" />
        <wilson:datasource id="dsCargoStatus" runat="server" objectspace="C2.Manager.ORManager" typename="C2.XXUom"
            keymember="Id" filterexpression="CodeType='5'" />
        <div>
            <table>
                <tr>
                    <td class="lbl">Job/Quote No</td>
                    <td class="ctl">
                        <dxe:aspxtextbox id="txt_search_JobNo" clientinstancename="txt_search_JobNo" runat="server" width="100%"></dxe:aspxtextbox>
                    </td>
                    <td>
                        <dxe:aspxbutton id="btn_Retrieve" runat="server" text="Retrieve" autopostback="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='JobEdit.aspx?no='+txt_search_JobNo.GetText();
                    }" />
                        </dxe:aspxbutton>
                    </td>
                    <td style="display: none">
                        <dxe:aspxbutton id="btn_GoSearch" runat="server" text="Go Search" autopostback="false">
                            <ClientSideEvents Click="function(s,e){window.location='JobList.aspx';}" />
                        </dxe:aspxbutton>
                    </td>
                </tr>
            </table>
            <dxwgv:aspxgridview id="grid_job" runat="server" clientinstancename="grid_job" keyfieldname="Id" datasourceid="dsJob" width="1000px" autogeneratecolumns="False" oninit="grid_job_Init" oninitnewrow="grid_job_InitNewRow" oncustomdatacallback="grid_job_CustomDataCallback" oncustomcallback="grid_job_CustomCallback" onhtmleditformcreated="grid_job_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <Templates>
                    <EditForm>
                        <div style="display: none">
                            <dxe:ASPxLabel ID="lb_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                        </div>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="padding: 0px">
                                        <tr>
                                            <td style="font-weight: bold">
                                                <dxe:ASPxLabel ID="lbl_JobType0" runat="server" Font-Size="Large" Text="Type:"></dxe:ASPxLabel>
                                            </td>
                                            <td style="font-weight: bold">

                                                <dxe:ASPxLabel ID="lbl_JobType" ClientInstanceName="lbl_JobType" runat="server" Font-Size="Large" Text='<%# Eval("JobType") %>'></dxe:ASPxLabel>
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
                                                <dxe:ASPxButton ID="btn_Confirm" ClientInstanceName="btn_Confirm" runat="server" Text="Confirm Quote To Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("QuoteStatus"),"USE")!="None" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm(btn_Confirm.GetText()+'?')) {
                                                        grid_job.GetValuesOnCustomCallback('ConfirmQ',onNoCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_CompleteJob" ClientInstanceName="btn_CompleteJob" runat="server" Text="Complete Job / Billing Ready" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&(SafeValue.SafeString(Eval("JobStatus"),"USE")!="Voided"&&SafeValue.SafeString(Eval("JobStatus"),"USE")=="Quoted"||SafeValue.SafeString(Eval("JobStatus"),"Booked")=="Booked")||(SafeValue.SafeString(Eval("JobStatus"),"USE")=="Confirmed")  %>' Width="200">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_CompleteJob.GetText()+' ?')) {
                                                        grid_job.GetValuesOnCustomCallback('CompletedJob',onCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td style="display: none">
                                                <dxe:ASPxButton ID="btn_Completed" ClientInstanceName="btn_Completed" runat="server" Text="Close Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&(SafeValue.SafeString(Eval("JobStatus"),"USE")!="Voided"&&SafeValue.SafeString(Eval("JobStatus"),"USE")=="Quoted"||SafeValue.SafeString(Eval("JobStatus"),"Booked")=="Booked")||(SafeValue.SafeString(Eval("JobStatus"),"USE")=="Confirmed")  %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_JobClose.GetText()+' ?')) {
                                                        grid_job.GetValuesOnCustomCallback('CompletedQ',onCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td style="display: none">
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
                                                <div class='<%# (SafeValue.SafeString(Eval("JobType"))=="IMP"||SafeValue.SafeString(Eval("JobType"))=="EXP"||SafeValue.SafeString(Eval("JobType"))=="LOC")?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="btn_PrintJobSheet" runat="server" Text="Print JobSheet" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                         grid_job.GetValuesOnCustomCallback('PrintHaulier',onCallBack);
                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="btn_PrintDeliveryOrder" runat="server" Text="Print Goods Receipt Note" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintGRN();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                                <div class='<%# SafeValue.SafeString(Eval("JobType"))=="WDO"?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="ASPxButton15" runat="server" Text="Print Delivery Order" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintDO();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                                <div class='<%# SafeValue.SafeString(Eval("JobType"))=="TPT"?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="ASPxButton20" runat="server" Text="Print Transport Instruction" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintTP();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                                <div class='<%# SafeValue.SafeString(Eval("JobType"))=="FRT"?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="ASPxButton22" runat="server" Text="Print Transfer Instruction" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintTR();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# (SafeValue.SafeString(Eval("JobType"))=="WDO"||SafeValue.SafeString(Eval("JobType"))=="WGR"||SafeValue.SafeString(Eval("JobType"))=="TPT")?"show":"hide" %>' style="min-width: 70px;">
                                                    <dxe:ASPxButton ID="ASPxButton23" runat="server" Text="Print DN" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintDN();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_PrintJobCost" runat="server" Text="Print Job Cost" AutoPostBack="false" Width="100">
                                                    <ClientSideEvents Click="function(s,e){printJobCost();}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td width="10%"></td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_JobSave" ClientInstanceName="btn_JobSave" runat="server" Text="Save" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    
                                                         grid_job.GetValuesOnCustomCallback('save',job_callback);
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
                                                <%--<dxe:ListEditItem Text="Billing" Value="Billing" />--%>
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
                        <div style="margin-top: 6px; padding: 5px; background-color: #FFFFFF; border: 1px solid #A8A8A8; white-space: nowrap">
                            <table>
                                <tr>
                                    <td class="lbl">Client</td>
                                    <td class="ctl">
                                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Text='<%# Eval("ClientId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName,'C',txt_ClientContact,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton21" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="View" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){
                                PopupPartyInfo();
                                }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <%--<td class="ctl"></td>--%>
                                    <%--                                    <td class="lbl" style="display: none">Sub Client
                                    </td>
                                    <td class="ctl" style="display: none">
                                        <dxe:ASPxButtonEdit ID="btn_SubClientId" ClientInstanceName="btn_SubClientId" runat="server" Text='<%# Eval("SubClientId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParentParty(btn_SubClientId,null,btn_ClientId.GetText());
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>--%>
                                    <td class="lbl">Ordered By</td>
                                    <td class="ctl">
                                        <dxe:ASPxButtonEdit ID="txt_ClientContact" ClientInstanceName="txt_ClientContact" runat="server" Text='<%# Eval("ClientContact") %>' Width="150" AutoPostBack="False" ReadOnly="false">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContact(txt_ClientContact,txt_notifiEmail,btn_ClientId.GetText());
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>

                                    <td class="lbl">Client Ref No
                                    </td>
                                    <td class="ctl">
                                        <dxe:ASPxTextBox ID="txt_ClientRefNo" runat="server" Text='<%# Eval("ClientRefNo") %>' Width="150px"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl2" style="display:none">Bill Type</td>
                                    <td style="display:none">
                                        <dxe:ASPxComboBox ID="cbb_BillingType" runat="server" Value='<%# Eval("BillingType") %>' Width="150px">
                                            <Items>
                                                <dxe:ListEditItem Text="None" Value="None" />
                                                <dxe:ListEditItem Text="Job" Value="Job" />
                                                <dxe:ListEditItem Text="Master" Value="Master" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
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
                                                    <dxe:ASPxComboBox ID="cmb_IsWarehouse" runat="server" Value='<%# Eval("IsWarehouse") %>' Width="60">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="4" class="ctl">
                                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl">Email
                                    </td>
                                    <td class="ctl">
                                        <dxe:ASPxTextBox ID="txt_notifiEmail" ClientInstanceName="txt_notifiEmail" runat="server" Width="150" Value='<%# Eval("EmailAddress") %>'></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl2" style="display:none">Master JobNo</td>
                                    <td class="ctl" style="display:none">
                                        <dxe:ASPxTextBox ID="txt_MasterJobNo" ClientInstanceName="txt_MasterJobNo" runat="server" Width="150" Value='<%# Eval("MasterJobNo") %>'></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl2">Transport</td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cmb_IsLocal" runat="server" Value='<%# Eval("IsLocal") %>' Width="60" OnCustomJSProperties="cmb_IsLocal_CustomJSProperties">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td class="lbl2">Crane</td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cmb_IsAdhoc" runat="server" Value='<%# Eval("IsAdhoc") %>' Width="60" OnCustomJSProperties="cmb_IsAdhoc_CustomJSProperties">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="lbl">Owner Id</td>
                                    <td class="ctl">
                                        <dxe:ASPxButtonEdit ID="btn_PartyId" ClientInstanceName="btn_PartyId" runat="server" Text='<%# Eval("PartyId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_PartyId,txt_PartyName,'C',null,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td colspan="3" class="ctl">
                                        <dxe:ASPxTextBox ID="txt_PartyName" ClientInstanceName="txt_PartyName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl">Department
                                    </td>
                                    <td class="ctl">
                                                    <dxe:ASPxComboBox ID="cmb_department" runat="server" Value='<%# Eval("DepartmentId") %>' Width="150" >
                                                        <Items>
                                                            <dxe:ListEditItem Text="" Value="" />
                                                            <dxe:ListEditItem Text="PRIME MOVER" Value="PRIME MOVER" />
                                                            <dxe:ListEditItem Text="ESSILOR" Value="ESSILOR" />
                                                            <dxe:ListEditItem Text="UPS" Value="UPS" />
                                                            <dxe:ListEditItem Text="OFFICE" Value="OFFICE" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                    </td>
                                    
                                    
                                    <td colspan="5">
                                        <table width="100%">
                                            <tr>
                                                
                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="10">
                                        <dxe:ASPxLabel ForeColor="Red" runat="server" ID="lbl_BillingAlert"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="11">
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
                        <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="1000px">
                            <TabPages>
                                <dxtc:TabPage Name="Job" Text="Job">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table style="width: 1000px">
                                                <tr>
                                                    <td colspan="6" style="padding: 2px; background: #CCCCCC"><b>Vessel / Shipment</b></td>
                                                </tr>
                                                <tr style="display: <%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show-row":"none"%>">
                                                    <td class="lbl">Vessel</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Ves" Text='<%# Eval("Vessel")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td class="lbl">Voyage
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Voy" Text='<%# Eval("Voyage")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td class="lbl">ETA
                                                    </td>
                                                    <td class="ctl">
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
                                                <tr style="display:  show-row">

                                                    <td class="lbl">Terminal</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxComboBox ID="cbb_Terminal" runat="server" Value='<%# Eval("Terminalcode") %>' Width="100%" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" DataSourceID="dsTerminal" ValueField="Code" TextField="Name"></dxe:ASPxComboBox>
                                                    </td>
                                                    <td class="lbl">Pornet Ref No</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox ID="txt_PortnetRef" runat="server" Text='<%# Eval("PortnetRef") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                    <td class="lbl">ETD
                                                    </td>
                                                    <td class="ctl">
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
                                                    <%--                                                    <td style='display: <%# Eval("IsImport") %>'>COD
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
                                                <tr style="display: <%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show-row":"none"%>">
                                                    <td class="lbl">POL
                                                    </td>
                                                    <td class="ctl">
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
                                                    <td class="lbl">POD
                                                    </td>
                                                    <td class="ctl">
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
                                                    <td class="lbl">Shipper
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox ID="txt_WarehouseAddress" runat="server" Text='<%# Eval("WarehouseAddress") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>

                                                </tr>

                                                <tr style="display: <%#(SafeValue.SafeString(Eval("JobType"))=="IMP"||SafeValue.SafeString(Eval("JobType"))=="EXP"||SafeValue.SafeString(Eval("JobType"))=="LOC")?"show-cell":"none"%>">
                                                    <td class="lbl">Carrier
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
                                                    <td class="lbl">Operator Code
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox ID="txt_OperatorCode" runat="server" Text='<%# Eval("OperatorCode") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lbl">B/R</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxTextBox ID="txt_CarrierBkgNo" runat="server" Text='<%# Eval("CarrierBkgNo") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        <%--<dxe:ASPxButton ID="btn_AllCont" ClientInstanceName="btn_AllCont" runat="server" AutoPostBack="false" UseSubmitBehavior="false" Text="Keyin for Cont/Trips">
                                                            <ClientSideEvents Click="function(s,e){
                                                                 if(confirm('Confirm '+btn_AllCont.GetText()+'?')) {
                                                                   grid_job.GetValuesOnCustomCallback('UpdateBkgNo',onCallBack);
                                                                 }
                                                                }" />
                                                        </dxe:ASPxButton>--%>
                                                        <dxe:ASPxButton ID="ASPxButton33" ClientInstanceName="btn_AllCont" runat="server" AutoPostBack="false" UseSubmitBehavior="false" Text="Keyin for Cont">
                                                            <ClientSideEvents Click="function(s,e){
                                                                 if(confirm('Confirm '+btn_AllCont.GetText()+'?')) {
                                                                   grid_job.GetValuesOnCustomCallback('Update_KeyinCont',onCallBack);
                                                                 }
                                                                }" />
                                                        </dxe:ASPxButton>
                                                        <span>(B/R,HaulierRemark,DepotAddr)</span>
                                                    </td>


                                                </tr>
                                                <tr>
                                                    
                                                                <td class="lbl1">Internal Remark</td>
                                                                <td colspan="2" style="vertical-align: top" rowspan="2">
                                                                    <dxe:ASPxMemo ID="memo_releaseToHaulierRemark" Rows="3" runat="server" Text='<%# Eval("ReleaseToHaulierRemark") %>' Width="100%">
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                </tr>
                                                <tr><td></td></tr>
                                                <tr><td></td></tr>
                                                <tr>
                                                    
                                                    <td class="lbl" style="vertical-align: top" rowspan="2">
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            Job Instruction
                                                        </div>
                                                    </td>
                                                    <td colspan="2" style="vertical-align: top" rowspan="2">
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo ID="txt_SpecialInstruction" Rows="3" runat="server" Text='<%# Eval("SpecialInstruction") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr><td></td></tr>
                                                <tr><td></td></tr>
                                                <tr style="display: none;">
                                                    <td class="lbl">Contractor
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
                                                    <td class="lbl">Sub-Contract ?</td>
                                                    <td class="ctl">
                                                        <dxe:ASPxComboBox ID="cbb_Contractor" runat="server" Value='<%# Eval("Contractor") %>' Width="100%" DropDownStyle="DropDownList">
                                                            <Items>
                                                                <dxe:ListEditItem Value="YES" Text="YES" />
                                                                <dxe:ListEditItem Value="NO" Text="NO" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td colspan="6" style="padding: 2px; background: #CCCCCC"><b>Transportation Description</b></td>
                                                </tr>

                                                <tr>
                                                    <td class="lbl">
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))=="CRA"?"show":"hide"%>">
                                                            <a href="#" onclick="PopupAddress_byParty(null,txt_JobLocation,btn_ClientId.GetText());">Job Location</a>
                                                        </div>
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            <a href="#" onclick="PopupAddress_byParty(null,txt_PickupFrom,btn_ClientId.GetText());">Pick From</a>
                                                        </div>
                                                    </td>
                                                    <td colspan="2">
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))=="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo ID="txt_JobLocation" Rows="6" ClientInstanceName="txt_JobLocation" runat="server"
                                                                Width="100%" Text='<%# Eval("DeliveryTo") %>'>
                                                            </dxe:ASPxMemo>
                                                        </div>
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo ID="txt_PickupFrom" Rows="3" ClientInstanceName="txt_PickupFrom" runat="server"
                                                                Width="100%" Text='<%# Eval("PickupFrom") %>'>
                                                            </dxe:ASPxMemo>
                                                        </div>
                                                    </td>
                                                    <td class="lbl">
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            <a href="#" onclick="PopupAddress_byParty(null,txt_DeliveryTo,btn_ClientId.GetText());">Delivery To</a>
                                                        </div>
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))=="CRA"?"show":"hide"%>">
                                                            Job Instruction
                                                        </div>
                                                    </td>
                                                    <td colspan="2">
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo ID="txt_DeliveryTo" Rows="3" ClientInstanceName="txt_DeliveryTo" runat="server"
                                                                Width="100%" Text='<%# Eval("DeliveryTo") %>'>
                                                            </dxe:ASPxMemo>
                                                        </div>
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))=="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo ID="txt_JobInstruction" Rows="6" runat="server" Text='<%# Eval("SpecialInstruction") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lbl">

                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            <a href="#" onclick="PopupAddress(null,txt_YardRef);">Depot Address</a>
                                                        </div>
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))=="CRA"?"show":"hide"%>">
                                                            Remark
                                                        </div>
                                                    </td>
                                                    <td colspan="2">
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo runat="server" Rows="2" ID="txt_YardRef" ClientInstanceName="txt_YardRef" Value='<%# Eval("YardRef") %>' Width="100%"></dxe:ASPxMemo>
                                                        </div>
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))=="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo ID="txt_RemarkCRA" Rows="2" runat="server" Text='<%# Eval("Remark") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </div>
                                                    </td>
                                                    <td class="lbl" style="vertical-align: top" rowspan="2">
                                                        <%--<div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            Job Instruction
                                                        </div>--%>
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))=="CRA"?"show":"hide"%>">
                                                            <a href="#" onclick="PopupAddress(null,txt_FromAddress);">From Address</a>
                                                        </div>

                                                    </td>
                                                    <td colspan="2" style="vertical-align: top" rowspan="2">
                                                        <%--<div class="<%#SafeValue.SafeString(Eval("JobType"))!="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo ID="txt_SpecialInstruction" Rows="3" runat="server" Text='<%# Eval("SpecialInstruction") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </div>--%>
                                                        <div class="<%#SafeValue.SafeString(Eval("JobType"))=="CRA"?"show":"hide"%>">
                                                            <dxe:ASPxMemo ID="txt_FromAddress" Rows="2" ClientInstanceName="txt_FromAddress" runat="server"
                                                                Width="100%" Text='<%# Eval("PickupFrom") %>'>
                                                            </dxe:ASPxMemo>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr >
                                                    <td class="lbl">
                                                        <div class="<%#SafeValue.SafeString(Eval("IsTrucking"))=="Yes"?"show":"hide"%>">
                                                            Return Last Date
                                                        </div>
                                                    </td>
                                                    <td class="ctl">
                                                        <div class="<%#SafeValue.SafeString(Eval("IsTrucking"))=="Yes"?"show":"hide"%>">
                                                        <dxe:ASPxDateEdit ID="date_ReturnLastDate" Width="100%" runat="server" Value='<%# Eval("ReturnLastDate")%>'
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                        </div>
                                                    </td>
                                                    <td colspan="2">
                                                        <%--<dxe:ASPxButton ID="btn_AllTrips" ClientInstanceName="btn_AllTrips" runat="server" AutoPostBack="false" UseSubmitBehavior="false" Text="Keyin for RET Trips">
                                                            <ClientSideEvents Click="function(s,e){
                                                                   if(confirm('Confirm '+btn_AllTrips.GetText()+'?')) {
                                                                    grid_job.GetValuesOnCustomCallback('UpdateDeport',onCallBack);
                                                                   }
                                                                }" />
                                                        </dxe:ASPxButton>--%>
                                                        
                                                        <dxe:ASPxButton ID="ASPxButton34" ClientInstanceName="btn_AllTrips" runat="server" AutoPostBack="false" UseSubmitBehavior="false" Text="Keyin for Trip">
                                                            <ClientSideEvents Click="function(s,e){
                                                                 if(confirm('Confirm '+btn_AllTrips.GetText()+'?')) {
                                                                   grid_job.GetValuesOnCustomCallback('Update_KeyinTrip',onCallBack);
                                                                 }
                                                                }" />
                                                        </dxe:ASPxButton>
                                                        <span>(only pending trips)</span>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td colspan="6" style="padding: 2px; background: #CCCCCC"><b>Warehouse</b></td>
                                                </tr>--%>

                                                <tr>
                                                    <td colspan="6" style="padding: 2px; background: #CCCCCC"><b>Special Requirements</b></td>
                                                </tr>

                                                <tr style="display: none">
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
                                                <tr style="display: <%#(SafeValue.SafeString(Eval("JobType"))=="IMP"||SafeValue.SafeString(Eval("JobType"))=="EXP"||SafeValue.SafeString(Eval("JobType"))=="LOC")?"show-cell":"none"%>">
                                                    <td class="lbl">Remark</td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_Remark" Rows="3" runat="server" Text='<%# Eval("Remark") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                    <td class="lbl">Permit No</td>
                                                    <td colspan="2">
                                                        <dxe:ASPxMemo ID="txt_PermitNo" Rows="3" runat="server" Text='<%# Eval("PermitNo") %>' Width="100%">
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td class="lbl1">Fumigation Ind</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_Fumigation_Ind" runat="server" Width="100" Value='<%# Bind("Fumigation_Ind") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Value="" Text="" />
                                                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                            <dxe:ListEditItem Value="No" Text="No" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td class="lbl1">Fumigation Status</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_FumigationStatus" runat="server" Width="100" Value='<%# Bind("FumigationStatus") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Value="" Text="" />
                                                                            <dxe:ListEditItem Value="Pending" Text="Pending" />
                                                                            <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td class="lbl1">Stamping Ind</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_Stamping_Ind" runat="server" Width="100" Value='<%# Bind("Stamping_Ind") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Value="" Text="" />
                                                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                            <dxe:ListEditItem Value="No" Text="No" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td class="lbl1">Stamping Status</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_StampingStatus" runat="server" Width="100" Value='<%# Bind("StampingStatus") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Value="" Text="" />
                                                                            <dxe:ListEditItem Value="Pending" Text="Pending" />
                                                                            <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lbl1">Fumigation Remark</td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxMemo ID="memo_FumigationRemark" Rows="3" runat="server" Text='<%# Eval("FumigationRemark") %>' Width="100%">
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                                <td class="lbl1">Stamping Remark</td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxMemo ID="memo_StampingRemark" Rows="3" runat="server" Text='<%# Eval("StampingRemark") %>' Width="100%">
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
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_ContBatchEdit" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Batch Edit" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){container_batch_edit();}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Cont" ClientInstanceName="grid_Cont" runat="server" KeyFieldName="Id" DataSourceID="dsCont" Width="1000px" AutoGenerateColumns="False" OnInit="grid_Cont_Init" OnBeforePerformDataSelect="grid_Cont_BeforePerformDataSelect" OnRowDeleting="grid_Cont_RowDeleting" OnInitNewRow="grid_Cont_InitNewRow" OnRowInserting="grid_Cont_RowInserting" OnRowUpdating="grid_Cont_RowUpdating" OnRowDeleted="grid_Cont_RowDeleted" OnRowUpdated="grid_Cont_RowUpdated" OnRowInserted="grid_Cont_RowInserted" OnCustomDataCallback="grid_Cont_CustomDataCallback">
                                                    <SettingsPager PageSize="100" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="2%">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='<%# "grid_Cont.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                                <%--<a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Cont.DeleteRow("+Container.VisibleIndex+");"  %>}' style='display: <%# Eval("canChange")%>'>Delete</a>--%>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="11" Width="100">
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
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="11" Width="80">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='<%# "permit_inline("+Container.VisibleIndex+");"  %>'>Permit</a>
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                                </div>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="ScheduleDate" Caption="Schedule Dt">
                                                            <DataItemTemplate>
                                                                 <%# Eval("ScheduleDate","{0:dd/MMM/yy}") %>
                                                            </DataItemTemplate>
														</dxwgv:GridViewDataColumn>
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
                                                                    <td class="lbl">PickUp RefNo</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_BR" ClientInstanceName="txt_BR" runat="server" Text='<%# Bind("Br") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">Tare Weight
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_Wt" Height="21px" Value='<%# Bind("ContWeight")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">VGM Weight
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_Wt2" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">Cargo Weight
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165" ID="spin_cargoWt" Height="21px" Value='<%# Bind("CargoWeight")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    </tr>
                                                                <tr>
                                                                    <td class="lbl">Qty</td>
                                                                    <td class="ctl">
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
                                                                    <td class="lbl">Volume
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="165"
                                                                            ID="spin_M3" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl"  >Service</td>
                                                                    <td class="ctl"  >
                                                                        <dxe:ASPxComboBox ID="cmb_ServiceType" runat="server" Text='<%# Bind("ServiceType") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" />
                                                                                <dxe:ListEditItem Value="COL" Text="COL" />
                                                                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                                                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                                                                <dxe:ListEditItem Value="RET" Text="RET" />
                                                                                <%--<dxe:ListEditItem Value="LOC" Text="LOC" />
                                                                                <dxe:ListEditItem Value="SMT" Text="SMT" />
                                                                                <dxe:ListEditItem Value="SLD" Text="SLD" />
                                                                                <dxe:ListEditItem Value="SHF" Text="SHF" />--%>
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <%--<td>Reqeust Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Cont_Request" runat="server" Width="165" Value='<%# Bind("RequestDate")%>'
                                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>--%>

                                                                    <td class="lbl"  >Trucking/Schedule Date</td>
                                                                    <td class="ctl"  >
                                                                        <dxe:ASPxDateEdit ID="date_Cont_Schedule" runat="server" Width="165" Value='<%# Bind("ScheduleDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">TruckingStatus</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_StatusCode" ClientInstanceName="cbb_StatusCode" runat="server" Width="165" Value='<%# Bind("StatusCode") %>'>
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
                                                                    <td class="lbl">Dg Class</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox runat="server" Width="165" ID="txt_DgClass" Text='<%# Bind("DgClass")%>'></dxe:ASPxTextBox>
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
                                                                    <td class="lbl" style="display: none">Permit No</td>
                                                                    <td class="ctl" style="display: none">
                                                                        <dxe:ASPxComboBox ID="cbb_PermitNo" Width="165" runat="server" DataSourceID="dsPermit" EnableIncrementalFiltering="true" TextField="PermitNo" Value='<%# Eval("PermitNo") %>' ValueField="PermitNo" ValueType="System.String">
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
                                                                        <dxe:ASPxComboBox ID="cmb_PortnetStatus" runat="server" Value='<%# Bind("PortnetStatus") %>' DropDownStyle="DropDown" Width="165">
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
                                                                    <td class="lbl">
                                                                        <div class=" <%# SafeValue.SafeString(Eval("JobType"))=="IMP"?"show-cell":"hide"%>">Unstuffing</div>
                                                                        <div class="<%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"show-cell":"hide"%>">Stuffing</div>
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cmb_Stuff_Ind" runat="server" Width="165" Value='<%# Bind("Stuff_Ind") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="" Text="" />
                                                                                <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                <dxe:ListEditItem Value="No" Text="No" />
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
                                                                    <td class="lbl">WHS StartDate
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
                                                                    <td class="lbl">Job Instruction<br />(COL,IMP Instr)</td>
                                                                    <td colspan="3">
                                                                        <%--<dxe:ASPxTextBox ID="txt_ContRemark" runat="server" Text='<%# Bind("Remark") %>' Width="100%"></dxe:ASPxTextBox>--%>
                                                                        <dxe:ASPxMemo ID="txt_ContRemark" Rows="3" runat="server" Text='<%# Bind("Remark") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">Cont Remark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_ContRemark1" Rows="3" runat="server" Text='<%# Bind("Remark1") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">Return to Haulier Remark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_releaseToHaulierRemark" Rows="3" runat="server" Text='<%# Bind("ReleaseToHaulierRemark") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>--%>
                                                                <tr style="background-color: lightgreen; padding: 4px; padding-left: 10px;">

                                                                    <%--<dxe:ASPxButton ID="btn_cont_trailerCP" runat="server" Text="Trailer Carpark" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park3',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>
                                                                        <dxe:ASPxButton ID="btn_cont_afterWH" runat="server" Text="After WH" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park2',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>
                                                                        <dxe:ASPxButton ID="btn_cont_beforeWH" runat="server" Text="Before WH" CssClass="add_carpark">
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('Park1',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>--%>

                                                                    <%--<td colspan="6">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td><b>TRIP:</b></td>
                                                                                <td width="90%"></td>
                                                                                <td>Date</td>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="date_Schedule" runat="server" Width="100" Value='<%# Bind("ScheduleDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="btn_addTrip_COL" Width="50" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' runat="server" Text="COL" AutoPostBack="false">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('COL');
                                                 }" />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="btn_addTrip_EXP" Width="50" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' runat="server" Text="EXP" AutoPostBack="false">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('EXP');
                                                 }" />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton1" Width="50" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' runat="server" Text="IMP" AutoPostBack="false">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('IMP');
                                                 }" />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton2" Width="50" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' runat="server" Text="RET" AutoPostBack="false">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('RET');
                                                 }" />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton3" Width="50" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' runat="server" Text="SHF" AutoPostBack="false">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('SHF');
                                                 }" />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton4" Width="50" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' runat="server" Text="LOC" AutoPostBack="false">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('LOC');
                                                 }" />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <dxe:ASPxButton ID="btn_cont_newTrip" runat="server" Text="New Trip" CssClass="add_carpark" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                                                            <ClientSideEvents Click="function(s,e){gv_cont_trip.GetValuesOnCustomCallback('AddNew',container_trip_add_cb);}" />
                                                                        </dxe:ASPxButton>
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                            <%--<dxwgv:ASPxGridView ID="gv_cont_trip" ClientInstanceName="gv_cont_trip" runat="server" AutoGenerateColumns="false" Width="930" OnBeforePerformDataSelect="gv_cont_trip_BeforePerformDataSelect" DataSourceID="dsContTrip" KeyFieldName="Id" OnRowUpdating="gv_cont_trip_RowUpdating" OnRowUpdated="gv_cont_trip_RowUpdated" OnCustomDataCallback="gv_cont_trip_CustomDataCallback" OnHtmlEditFormCreated="gv_cont_trip_HtmlEditFormCreated">
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
                                                                                <td class="lbl1">Chassis Type 
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
                                                                        Popup_TowheadList(btn_TowheadCode.null);
                                                                        }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                                <td class="lbl1">Vehicle Type 
                                                                                </td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_VehicleType" ClientInstanceName="cbb_VehicleType" runat="server" Value='<%# Bind("RequestVehicleType") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="25ton" Text="25ton" />
                                                                                            <dxe:ListEditItem Value="50ton" Text="50ton" />
                                                                                            <dxe:ListEditItem Value="70ton" Text="70ton" />
                                                                                            <dxe:ListEditItem Value="80ton" Text="80ton" />
                                                                                            <dxe:ListEditItem Value="100ton" Text="100ton" />
                                                                                            <dxe:ListEditItem Value="110ton" Text="110ton" />
                                                                                            <dxe:ListEditItem Value="160ton" Text="160ton" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>

                                                                                <td class="lbl">Status</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="S" Text="Start" />
                                                                                            <dxe:ListEditItem Value="P" Text="Pending" />
                                                                                            <dxe:ListEditItem Value="C" Text="Completed" />
                                                                                            <dxe:ListEditItem Value="X" Text="Cancel" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>


                                                                                <td class="lbl">T/P Schedule</td>
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
                                                                                <td class="lbl">Double Mounting</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxComboBox ID="cmb_DoubleMounting" runat="server" Value='<%# Bind("DoubleMounting") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                            <dxe:ListEditItem Value="No" Text="No" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">From</td>
                                                                                <td colspan="3">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="100%">
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
                                                                                    <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="100%">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                                <td class="lbl">Return Last Date
                                                                                </td>
                                                                                <td class="ctl">

                                                                                    <dxe:ASPxDateEdit ID="date_ReturnLastDate" Width="100%" runat="server" Value='<%# Eval("ReturnLastDate")%>'
                                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">To</td>
                                                                                <td colspan="3" class="ctl2">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="100%">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td class="lbl">
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
                                                                                    <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="100%">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
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
                                                                                <td class="lbl">Escort/OOG Remark  </td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_Escort_Remark" ClientInstanceName="txt_Trip_Escort_Remark" runat="server" Text='<%# Bind("Escort_Remark") %>' Width="100%">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>

                                                                                <td class="lbl">Escort/OOG Ind</td>
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
                                                                                <td class="lbl">Trip Instruction</td>
                                                                                <td colspan="3" class="ctl">
                                                                                    <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="100%">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                                <td class="lbl" style="display: <%#(SafeValue.SafeString(Eval("TripCode"))=="SHF")?"show-cell":"none"%>">Direct ?</td>
                                                                                <td class="ctl" style="display: <%#(SafeValue.SafeString(Eval("TripCode"))=="SHF")?"show-cell":"none"%>">
                                                                                    <dxe:ASPxComboBox ID="cbb_DirectInf" runat="server" Text='<%# Bind("DirectInf") %>' Width="165">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="Normal" Text="Normal" />
                                                                                            <dxe:ListEditItem Value="Direct Loading" Text="Direct Loading" />
                                                                                            <dxe:ListEditItem Value="Direct Delivery" Text="Direct Delivery" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
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
                                                                                    <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="100%">
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>

                                                                                <td class="lbl">Incentive</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive1" Height="21px" Value='<%# Bind("Incentive1")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td class="lbl">Overtime</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive2" Height="21px" Value='<%# Bind("Incentive2")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td class="lbl">Standby</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive3" Height="21px" Value='<%# Bind("Incentive3")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">PSA ALLOWANCE</td>
                                                                                <td class="ctl">
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
                                                                                <td class="lbl">DHC</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge1" Height="21px" Value='<%# Bind("Charge1")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td class="lbl">WEIGHING</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge2" Height="21px" Value='<%# Bind("Charge2")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td class="lbl">WASHING</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge3" Height="21px" Value='<%# Bind("Charge3")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lbl">REPAIR</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge4" Height="21px" Value='<%# Bind("Charge4")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>

                                                                                <td class="lbl">DETENTION</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge5" Height="21px" Value='<%# Bind("Charge5")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td class="lbl">DEMURRAGE</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge6" Height="21px" Value='<%# Bind("Charge6")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                <td class="lbl">LIFT ON/OFF</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge7" Height="21px" Value='<%# Bind("Charge7")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td class="lbl">C/SHIPMENT</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge8" Height="21px" Value='<%# Bind("Charge8")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td class="lbl">EMF</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge10" Height="21px" Value='<%# Bind("Charge9")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                <td class="lbl">ERP</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ERP" Height="21px" Value='<%# Bind("Charge_ERP")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td class="lbl">ParkingFee</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ParkingFee" Height="21px" Value='<%# Bind("Charge_ParkingFee")%>' DecimalPlaces="2" Increment="0">
                                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                 <td class="lbl">OTHER</td>
                                                                                <td class="ctl">
                                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge9" Height="21px" Value='<%# Bind("Charge10")%>' DecimalPlaces="2" Increment="0">
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
                                                                                            <a onclick="gv_cont_trip.GetValuesOnCustomCallback('Update',container_trip_update_cb);"><u>Update</u></a>
                                                                                        </span>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EditForm>
                                                                </Templates>
                                                            </dxwgv:ASPxGridView>--%>
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
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_NewTPTTrip" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Add TPT" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                gv_tpt_trip.GetValuesOnCustomCallback('NewTPT',onAutoCallBack);
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_NewWGRTrip" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Add WGR" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                gv_tpt_trip.GetValuesOnCustomCallback('NewWGR',onAutoCallBack);
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_NewWDOTrip" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Add WDO" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                gv_tpt_trip.GetValuesOnCustomCallback('NewWDO',onAutoCallBack);
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_NewReturnWDO" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Return From Warehouse" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                PopupReturnCargo('OUT',txt_JobNo.GetText());
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_NewReturnWGR" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Text="Return to Warehouse" AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                PopupReturnCargo('IN',txt_JobNo.GetText());
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cbb_TripCount" ClientInstanceName="cbb_TripCount" runat="server" Width="50">
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
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="gv_tpt_trip" ClientInstanceName="gv_tpt_trip" OnInit="gv_tpt_trip_Init" OnInitNewRow="gv_tpt_trip_InitNewRow" runat="server" AutoGenerateColumns="false" Width="1000px" OnBeforePerformDataSelect="gv_tpt_trip_BeforePerformDataSelect" DataSourceID="dsTransportTrip" KeyFieldName="Id" OnRowUpdating="gv_tpt_trip_RowUpdating" OnRowUpdated="gv_tpt_trip_RowUpdated" OnCustomDataCallback="gv_tpt_trip_CustomDataCallback" OnHtmlEditFormCreated="gv_tpt_trip_HtmlEditFormCreated">
                                                    <SettingsPager PageSize="100" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <%--<dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='printTptTrip("<%# Eval("Id")%>","<%# Eval("JobNo")%>","<%# Eval("JobType")%>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>--%>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <%-- <a href="#" onclick='PopupTripsList("<%# Eval("JobNo") %>","<%#Eval("Id")%>","<%# Eval("TripIndex") %>","GO")'>Edit</a>--%>
                                                                <a href="#" onclick='<%# "gv_tpt_trip.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="12">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "gv_tpt_trip.GetValuesOnCustomCallback(\"Delete_"+Container.KeyValue+"\",transport_trip_delete_cb);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="TripIndex" Caption="Trip No" Width="180"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ManualDo" Caption="Do No" Width="100"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Cont No" Width="100"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ClientRefNo" Caption="SEF No" Width="100"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="PermitNo" Caption="Permit No" Width="180"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ChessisCode" Caption="Trailer No / Chassis Type" Visible="false">
                                                            <DataItemTemplate>
                                                                 <%# Eval("ChessisCode") %> / <%# Eval("RequestTrailerType") %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="Statuscode" Caption="Status">
                                                            <DataItemTemplate>
                                                                <%# change_StatusShortCode_ToCode(Eval("Statuscode")) %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="TripCode" Caption="Trip Type"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToCode" Caption="Destination"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataDateColumn VisibleIndex="9" Width="100" FieldName="FromDate" Caption="Date">
                                                            <DataItemTemplate><%# SafeValue.SafeDate( Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("FromTime") %></DataItemTemplate>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="9" FieldName="DriverCode" Caption="Bill/OT/Permit">
                                                            <DataItemTemplate><%# SafeValue.SafeDecimal(Eval("BillingTrip")).ToString("f0") %> / <%# SafeValue.SafeDecimal(Eval("BillingOT")).ToString("f0") %> / <%# SafeValue.SafeDecimal(Eval("BillingPermit")).ToString("f0") %></DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="DoubleMounting" Caption="Double Mounting" Width="50" HeaderStyle-Wrap="True"></dxwgv:GridViewDataColumn>--%>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="13" Width="50">
                                                            <DataItemTemplate>

                                                                <a href="#" onclick='<%# "PopupTripCargo(\""+Eval("Id")+"\");"  %> ' style='display: <%# Eval("canChange")%>'>Cargo List</a>
                                                                <br />
                                                                <br />
                                                                <a href="#" onclick='printTptTrip("<%# Eval("Id")%>","<%# Eval("JobNo")%>","<%# Eval("JobType")%>")'>Print</a>
                                                                <br />
                                                                <br />
                                                                <a href="#" onclick='<%# "gv_tpt_trip.GetValuesOnCustomCallback(\"CopyNew_"+Eval("Id")+"\",onAutoCallBack);"  %> ' style='display: <%# Eval("canChange")%>'>Copy&New</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxLabel ID="lb_tripId" ClientInstanceName="lb_tripId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                <dxe:ASPxTextBox ID="dde_Trip_ContId" ClientInstanceName="dde_Trip_ContId" runat="server" Text='<%# Bind("Det1Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table>
                                                                 
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Trip Detail</td>
                                                                </tr>
                                                                <tr>
                                                                   
                                                                    <td class="lbl">Trip Type</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)==0 %>' Width="100" DropDownStyle="DropDown">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="WGR" Value="WGR" />
                                                                                <dxe:ListEditItem Text="WDO" Value="WDO" />
                                                                                <dxe:ListEditItem Text="TPT" Value="TPT" />
                                                                              
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="lbl">Self Del/Col</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_Self_Ind" ClientInstanceName="cbb_Self_Ind" runat="server" Value='<%# Bind("Self_Ind") %>' Width="100" DropDownStyle="DropDown" AutoPostBack="false">
                                                                            <ClientSideEvents ValueChanged="function(s,e){TPT_self_showHide(s,e);}" />
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="lbl" >Direct ?</td>
                                                                    <td class="ctl" >
                                                                                    <dxe:ASPxComboBox ID="cbb_DirectInf" runat="server" Text='<%# Bind("DirectInf") %>' Width="100">
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Value="Normal" Text="Normal" />
                                                                                            <dxe:ListEditItem Value="Direct Loading" Text="Direct Loading" />
                                                                                            <dxe:ListEditItem Value="Direct Delivery" Text="Direct Delivery" />
                                                                                        </Items>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>

                                                                </tr>
																
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">

                                                                    <td class="lbl">T/P Schedule</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxDateEdit ID="date_BookingDate" runat="server" Value='<%# Bind("BookingDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">Time</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_BookingTime" runat="server" Text='<%# Bind("BookingTime") %>' Width="100">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>

                                                                    <td class="lbl">Trip Status</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="100">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="P" Text="Pending" />
                                                                                <dxe:ListEditItem Value="C" Text="Completed" />
                                                                                <dxe:ListEditItem Value="X" Text="Cancel" />
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
														 
																<td class="lbl">Service</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cmb_ServiceType" runat="server" Text='<%# Bind("ServiceType") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="" Text="" />
                                                                                <dxe:ListEditItem Value="One-Way" Text="One-Way" />
                                                                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" />
                                                                                <dxe:ListEditItem Value="Whole-Day" Text="Whole-Day" />
                                                                                <dxe:ListEditItem Value="Half-Way" Text="Half-Way" />
                                                                                <dxe:ListEditItem Value="Double-Mount" Text="Double-Mount" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
																	<%--<td class="lbl">Double Mounting</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cmb_DoubleMount" runat="server" Value='<%# Bind("DoubleMounting") %>' Width="100">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                <dxe:ListEditItem Value="No" Text="No" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>--%>
																	 <td class="lbl1">SEF No 
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_ClientRefNo" ClientInstanceName="txt_ClientRefNo" runat="server" Text='<%# Bind("ClientRefNo") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
																
																</tr>																
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">

                                                                    <td class="lbl">Trailer No</td>
                                                                    <td class="ctl">

                                                                        <dxe:ASPxButtonEdit ID="btn_Trailer" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="100%">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                PopupCTM_MasterData(btn_ContNo,null,'Chessis');
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td class="lbl1">Request Chassis Type 
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_TrailerType" ClientInstanceName="txt_TrailerType" runat="server" Text='<%# Bind("RequestTrailerType") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                   
                                                                    <td class="lbl1">Do No 
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_ManualDO" ClientInstanceName="txt_ManualDO" runat="server" Text='<%# Bind("ManualDo") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>

                                                                </tr>
																<tr>
																	<td colspan=2></td>
																    <td class="lbl1">Container/Tanker No 
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_ContainerNo2" ClientInstanceName="txt_ContainerNo" runat="server" Text='<%# Bind("ContainerNo") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
																    <td class="lbl1">Permit No 
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_PermitNo2" ClientInstanceName="txt_PermitNo" runat="server" Text='<%# Bind("PermitNo") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>

																</tr>
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">

                                                                    <td class="lbl">Vehicle</td>
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
                                                                    <td class="lbl1">Request Vehicle
                                                                    </td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_VehicleType" ClientInstanceName="txt_VehicleType" runat="server" Value='<%# Bind("RequestVehicleType") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">Driver/Attendant</td>
                                                                    <td class="ctl">
																		<table><tr><td>
                                                                        <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="80">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,btn_TowheadCode);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
																		</td>
																		<td>
                                                                        <dxe:ASPxButtonEdit ID="btn_DriverCode2" ClientInstanceName="btn_DriverCode2" runat="server" Text='<%# Bind("DriverCode2") %>' AutoPostBack="False" Width="80">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode2,null,null);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
																		</td>
																		</tr>
																		</table>
                                                                    </td>


                                                                </tr>

                                                                <tr style="display: none">
                                                                    <td class="lbl">Double Mounting</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cmb_DoubleMounting2" runat="server" Value='<%# Bind("DoubleMounting") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                <dxe:ListEditItem Value="No" Text="No" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
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
                                                                    <td colspan="2">
                                                                        <dxe:ASPxTextBox ID="txt_AgentName" ClientInstanceName="txt_AgentName" runat="server" Text='<%# Bind("AgentName") %>' Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">
                                                                    <td class="lbl">From</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="100%">
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
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">
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
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">
                                                                    <td class="lbl">To</td>
                                                                    <td colspan="3" class="ctl2">
                                                                        <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="100%">
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
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">
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
                                                                    <td class="lbl">Escort/OOG Ind</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cmb_Escort_Ind" runat="server" Width="165" Value='<%# Bind("Escort_Ind") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Yes" Text="Yes" />
                                                                                <dxe:ListEditItem Value="" Text="" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>

                                                                </tr>
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">
                                                                    <td class="lbl">Instruction</td>
                                                                    <td colspan="3" class="ctl">
                                                                        <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td class="lbl">Escort/OOG Remark  </td>
                                                                    <td colspan="3" class="ctl">
                                                                        <dxe:ASPxMemo ID="txt_Trip_Escort_Remark" ClientInstanceName="txt_Trip_Escort_Remark" runat="server" Text='<%# Bind("Escort_Remark") %>' Width="165">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">
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
                                                                    <td class="lbl">Return Type</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_ReturnType" ClientInstanceName="cbb_ReturnType" runat="server" Value='<%# Bind("ReturnType") %>' Width="100%" DropDownStyle="DropDownList">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="Y" Text="YES" />
                                                                                <dxe:ListEditItem Value="N" Text="NO" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style='display: <%# SafeValue.SafeString(Eval("Self_Ind"))=="Yes"?"none":"" %>' class="style_self_ind">
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: #cccccc; padding: 4px; padding-left: 10px;">
                                                                        <div style="display: <%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"block":"none"%>">Warehouse</div>
                                                                        <div style="display: <%# SafeValue.SafeString(Eval("TripCode"))=="TPT"?"block":"none"%>">Cargo</div>
                                                                    </td>
                                                                </tr>
                                                                <tr style="display: <%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"table-row":"none"%>">
                                                                    <td class="lbl">Schedule Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_WarehouseScheduleDate" runat="server" Value='<%# Bind("WarehouseScheduleDate") %>' Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">Start Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_WarehouseStartDate" runat="server" Value='<%# Bind("WarehouseStartDate") %>' Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="lbl">End Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_WarehouseEndDate" runat="server" Value='<%# Bind("WarehouseEndDate") %>' Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">
                                                                        <div style="display: <%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"block":"none"%>">Remark</div>
                                                                        <div style="display: <%# SafeValue.SafeString(Eval("TripCode"))=="TPT"?"block":"none"%>">Detail</div>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_WarehouseRemark" Text='<%# Bind("WarehouseRemark") %>' runat="server" Width="100%">
                                                                        </dxe:ASPxMemo>
                                                                    </td>

                                                                    <td class="lbl">
                                                                        <div style="display: <%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"block":"none"%>">Container Status</div>
                                                                    </td>
                                                                    <td>
                                                                        <div style="display: <%# SafeValue.SafeString(Eval("TripCode"))!="TPT"?"block":"none"%>">
                                                                            <dxe:ASPxComboBox ID="cbb_WarehouseStatus" Text='<%# Bind("WarehouseStatus") %>' runat="server" Width="165">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Value="Pending" Text="Pending" />
                                                                                    <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                                                                    <dxe:ListEditItem Value="Started" Text="Started" />
                                                                                    <dxe:ListEditItem Value="Completed" Text="Completed" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Permit</td>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td class="lbl">PermitNo</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxTextBox ID="txt_PermitNo" Width="100%" runat="server" Text='<%# Eval("PermitNo") %>'></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">IncoTerms</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxButtonEdit ID="txt_IncoTerm" ClientInstanceName="txt_IncoTerm" runat="server" Text='<%# Eval("IncoTerm") %>' Width="100%" AutoPostBack="False">
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
                                                                        <dxe:ASPxDateEdit ID="date_PermitDate" runat="server" Value='<%# Eval("PermitDate") %>' Width="100%" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td class="lbl">Permit By</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="txt_PermitBy" Width="100%" runat="server" Value='<%# Eval("PermitBy") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="TSL" Value="TSL" />
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
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                            ID="spin_GstAmt" Height="21px" Value='<%# Bind("GstAmt")%>' DecimalPlaces="3" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>

                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td class="lbl1">Payment Status</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_PaymentStatus" Width="100%" runat="server" Value='<%# Eval("PaymentStatus") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Paid" Value="Paid" />
                                                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="lbl">Permit Remark</td>
                                                                    <td class="ctl" colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_PermitRemark" runat="server" Text='<%# Eval("PermitRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                                    </td>
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
                                                                    <%--<td class="lbl">Trailer</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxButtonEdit ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="165">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_MasterData(btn_ChessisCode,null,'Chessis');
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>--%>
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

                                                                    <td class="lbl">Incentive</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive1" Height="21px" Value='<%# Bind("Incentive1")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">Overtime</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive2" Height="21px" Value='<%# Bind("Incentive2")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">PSA ALLOWANCE</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxComboBox ID="cbb_Incentive4" runat="server" Value='<%# Bind("Incentive4") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="0" Text="0" />
                                                                                <dxe:ListEditItem Value="5" Text="5" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lbl">ERP</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ERP" Height="21px" Value='<%# Bind("Charge_ERP")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">ParkingFee</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ParkingFee" Height="21px" Value='<%# Bind("Charge_ParkingFee")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">OTHER</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge9" Height="21px" Value='<%# Bind("Charge10")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
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
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Customer Billing</td>
                                                                </tr>
                                                                <tr>

                                                                    <td class="lbl">Billing</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_BillingTrip" Height="21px" Value='<%# Bind("BillingTrip")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">Overtime</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_BillingOT" Height="21px" Value='<%# Bind("BillingOT")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="lbl">PermitCharge</td>
                                                                    <td class="ctl">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_BillingPermit" Height="21px" Value='<%# Bind("BillingPermit")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="5%">
                                                                        <dxe:ASPxButton ID="btn_cont_newTrip" runat="server" Text="Receipt List" CssClass="add_carpark" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                                                            <ClientSideEvents Click="function(s,e){PopupJobReceipt(lb_tripId.GetText());}" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td colspan="5">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            </span>
                                                                            <span style='float: right; display: <%# Eval("canChange")%>'>
                                                                                <%--<dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>--%>
                                                                                <a onclick="gv_tpt_trip.GetValuesOnCustomCallback('Update',transport_trip_update_cb);"><u>Update</u></a>
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
                                                <dxe:ASPxButton ID="btn_TripAdd" runat="server" Text="Add Trip" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' AutoPostBack="false">
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
                                                                <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Trip.GetValuesOnCustomCallback(\"Delete_"+Container.KeyValue+"\",crane_trip_delete_cb);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                            <DataItemTemplate>
                                                                <a href="#" onclick='printJobSheetForTrip("<%# Eval("Id")%>","<%# Eval("TripCode")%>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="TripIndex" Caption="No" Width="30"></dxwgv:GridViewDataColumn>
                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>--%>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="Statuscode" Caption="Satus" Width="30"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToCode" Caption="Location"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="TowheadCode" Caption="Vehicle"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="RequestVehicleType" Caption="Ton"></dxwgv:GridViewDataColumn>

                                                        <%--<dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>--%>

                                                        <dxwgv:GridViewDataDateColumn VisibleIndex="9" Width="120" FieldName="BookingDate" Caption="Schedule">
                                                            <DataItemTemplate><%# SafeValue.SafeDate( Eval("BookingDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("BookingTime")+"-"+Eval("BookingTime2") %></DataItemTemplate>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="FromDate" Caption="Actual Time" Width="160">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeDate(Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("FromTime")+"&nbsp;-&nbsp;"+ SafeValue.SafeDate(Eval("ToDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("ToTime")%>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="TotalHour" Caption="Billable Hrs"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="OtHour" Caption="OT Hrs"></dxwgv:GridViewDataColumn>
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
                                                                    <%--<td>Trailer Type 
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_TrailerType" ClientInstanceName="txt_TrailerType" runat="server" Text='<%# Bind("RequestTrailerType") %>' Width="165">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>--%>
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
                                                                    <td>By User</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_ByUser" runat="server" Value='<%# Bind("ByUser") %>' Width="165"></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
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
                                                                        <%--<dxe:ASPxTextBox ID="txt_VehicleType" ClientInstanceName="txt_VehicleType" runat="server" Text='<%# Bind("RequestVehicleType") %>' Width="165">
                                                                        </dxe:ASPxTextBox>--%>

                                                                        <dxe:ASPxComboBox ID="cbb_VehicleType" ClientInstanceName="cbb_VehicleType" runat="server" Value='<%# Bind("RequestVehicleType") %>' Width="165">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="25ton" Text="25ton" />
                                                                                <dxe:ListEditItem Value="50ton" Text="50ton" />
                                                                                <dxe:ListEditItem Value="70ton" Text="70ton" />
                                                                                <dxe:ListEditItem Value="80ton" Text="80ton" />
                                                                                <dxe:ListEditItem Value="100ton" Text="100ton" />
                                                                                <dxe:ListEditItem Value="110ton" Text="110ton" />
                                                                                <dxe:ListEditItem Value="160ton" Text="160ton" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>

                                                                </tr>

                                                                <tr>
                                                                    <td class="highlight">ScheduleDate</td>
                                                                    <td class="highlight">
                                                                        <dxe:ASPxDateEdit ID="date_BookingDate" runat="server" Value='<%# Bind("BookingDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="highlight">Time</td>
                                                                    <td class="highlight">
                                                                        <dxe:ASPxTextBox ID="txt_BookingTime" runat="server" Text='<%# Bind("BookingTime") %>' Width="162">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="highlight">To Time</td>
                                                                    <td class="highlight">
                                                                        <dxe:ASPxTextBox ID="txt_BookingTime2" runat="server" Text='<%# Bind("BookingTime2") %>' Width="162">
                                                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Instruction/Remark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="427">
                                                                        </dxe:ASPxMemo>
                                                                        <div style="display: none">
                                                                            <dxe:ASPxMemo ID="txt_BookingRemark" ClientInstanceName="txt_BookingRemark" runat="server" Text='<%# Bind("BookingRemark") %>' Width="427">
                                                                            </dxe:ASPxMemo>
                                                                            <dxe:ASPxMemo ID="txt_Trip_BillingRemark" ClientInstanceName="txt_Trip_BillingRemark" runat="server" Text='<%# Bind("BillingRemark") %>' Width="427">
                                                                            </dxe:ASPxMemo>
                                                                            <dxe:ASPxMemo ID="txt_DeliveryRemark" ClientInstanceName="txt_DeliveryRemark" runat="server" Text='<%# Bind("DeliveryRemark") %>' Width="427">
                                                                            </dxe:ASPxMemo>
                                                                        </div>
                                                                    </td>
                                                                    <td>No of Manpower
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0" runat="server" Width="100%" NumberType="Integer"
                                                                            ID="spin_Manpower" Height="21px" Value='<%# Bind("ManPowerNo")%>' DecimalPlaces="0" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
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
                                                                    <td>Work Location</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="427">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Exclude Lunch</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbb_ExcludeLunch" ClientInstanceName="cbb_ExcludeLunch" runat="server" Width="100%" Value='<%# Bind("ExcludeLunch") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="" Value="" />
                                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
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
                                                                    <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Delivery / Driver Input</td>
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
                                                                    <td>Billable Hour</td>
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
                                                                    <td rowspan="4" colspan="2">
                                                                        <table cellspacing="0" cellpadding="0" padding="0" style="border: 1px solid #CCCCCC; line-height: 25px; padding-left: 5px; width: 100%">
                                                                            <tr>
                                                                                <td width="60">
                                                                                    <dxe:ASPxCheckBox ID="ckb_epodCB1" runat="server"></dxe:ASPxCheckBox>
                                                                                </td>
                                                                                <td>Lifting Gear</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxCheckBox ID="ckb_epodCB2" runat="server"></dxe:ASPxCheckBox>
                                                                                </td>
                                                                                <td>Lifting Team</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxCheckBox ID="ckb_epodCB3" runat="server"></dxe:ASPxCheckBox>
                                                                                </td>
                                                                                <td>Risk Assessment</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxCheckBox ID="ckb_epodCB4" runat="server"></dxe:ASPxCheckBox>
                                                                                </td>
                                                                                <td>Sand and Concrete Bucket</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxCheckBox ID="ckb_epodCB5" runat="server"></dxe:ASPxCheckBox>
                                                                                </td>
                                                                                <td>ERP</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxCheckBox ID="ckb_epodCB6" runat="server"></dxe:ASPxCheckBox>
                                                                                </td>
                                                                                <td>Others</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td>Driver Remark</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="427">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Job Start Date</td>
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
                                                                    <td>Job End Date</td>
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
                                                                    <td colspan="6">
                                                                        <hr>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <%--<td>Incentive</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive1" Height="21px" Value='<%# Bind("Incentive1")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>--%>

                                                                    <td>Overtime $</td>
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
                                                                                <a onclick="grid_Trip.GetValuesOnCustomCallback('Update',crane_trip_update_cb);"><u>Update</u></a>
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
                                <dxtc:TabPage Text="StockIn" Name="StockIn">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table>
                                                <tr>
                                                    <td>

                                                        <dxe:ASPxButton ID="ASPxButton30" runat="server" Text="Batch Edit" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                       BatchCargoEdit('IN');
                    }" />
                                                        </dxe:ASPxButton>

                                                    </td>
                                                    <td>

                                                        <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                       grid_wh.AddNewRow();
                    }" />
                                                        </dxe:ASPxButton>

                                                    </td>
                                                    <td>

                                                        <dxe:ASPxButton ID="ASPxButton24" runat="server" Text="Refresh" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                       grid_wh.Refresh();
                    }" />
                                                        </dxe:ASPxButton>

                                                    </td>
                                                    <td>
                                                        <div>
                                                            <dxe:ASPxButton ID="ASPxButton18" runat="server" Text="Add Container as Cargo" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                                <ClientSideEvents Click="function(s,e){
                                                                   grid_job.GetValuesOnCustomCallback('AutoCreateCargo',onAutoCallBack);
                                                                  }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                        <div class='<%# SafeValue.SafeString(Eval("JobType"))=="FTR"?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton26" runat="server" Text="Add From Stock" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                                <ClientSideEvents Click="function(s,e){
                       PopupStockForTransfer();
                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class='<%# (SafeValue.SafeString(Eval("JobType"))=="IMP"||SafeValue.SafeString(Eval("JobType"))=="WGR")?"show":"hide" %>' style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton13" runat="server" Text="Create Transfer" AutoPostBack="false" Width="100" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                                <ClientSideEvents Click="function(s,e){
                                                                         GoJob_Page('Transfer');
                                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <dxe:ASPxButton ID="ASPxButton16" runat="server" Text="Create Delivery Order" AutoPostBack="false" Width="100" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                                <ClientSideEvents Click="function(s,e){
                                                                        GoJob_Page('Delivery');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div style="min-width: 70px;">
                                                            <dxe:ASPxButton ID="ASPxButton17" runat="server" Text="Show Delivery Order" AutoPostBack="false" Width="100" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                                <ClientSideEvents Click="function(s,e){
                                                                        GoJob_Page('ShowDO');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </div>
                                                    </td>
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
                                                    <td width="90%"></td>
                                                </tr>
                                            </table>
                                            <table style="width: 65%; margin-bottom: 10px; border: solid 1px #A8A8A8; background: #e8e8e8">
                                                <tr>
                                                    <td style="width: 150px;display:none">
                                                        <a onclick="printTallySheetIndented();" style="text-decoration: underline; width: 150px">Internal Tallysheet</a>

                                                    </td>
                                                    <td width="20px"></td>
                                                    <td style="width: 150px">
                                                        <a onclick="printTallySheetConfirmed('IN');" style="text-decoration: underline; width: 150px">Completed Tallysheet</a>

                                                    </td>
                                                    <td width="20px"></td>
                                                    <td style="width: 150px">
                                                        <a onclick="printStockTallySheet();" style="text-decoration: underline; width: 150px">Stock Tallysheet</a>
                                                    </td>

                                                </tr>
                                            </table>
                                            <div style="overflow-y: auto; width: 1000px">
                                                <dxwgv:ASPxGridView ID="grid_wh" ClientInstanceName="grid_wh" runat="server" DataSourceID="dsWh" OnInit="grid_wh_Init" OnInitNewRow="grid_wh_InitNewRow"
                                                    OnRowInserting="grid_wh_RowInserting" OnRowUpdating="grid_wh_RowUpdating" OnRowDeleting="grid_wh_RowDeleting" OnRowInserted="grid_wh_RowInserted"
                                                    OnBeforePerformDataSelect="grid_wh_BeforePerformDataSelect" OnCustomDataCallback="grid_wh_CustomDataCallback" OnCustomCallback="grid_wh_CustomCallback"
                                                     OnHtmlRowPrepared="grid_wh_HtmlRowPrepared"
                                                    KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsPager Mode="ShowPager" PageSize="10"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="60px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td><a href="#" onclick='<%# "grid_wh.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a></td>
                                                                        <td><a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_wh.GetValuesOnCustomCallback(\"Delete_"+Container.VisibleIndex+"\",house_in_delete_cb);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a></td>
                                                                    </tr>
                                                                </table>

                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td><a href="#" onclick='<%# "grid_wh.GetValuesOnCustomCallback(\"Update_"+Container.VisibleIndex+"\",house_in_update_cb);" %>'>Update</a></td>
                                                                        <td>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>' Width="165">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" Visible="false">
                                                            <EditButton Visible="true"></EditButton>
                                                            <DeleteButton Visible="true"></DeleteButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" Width="150" VisibleIndex="0" FieldName="Id" Visible="false">
                                                            <DataItemTemplate>
                                                                <table style="display: <%# SafeValue.SafeString(Eval("CargoType"))=="IN"?"block":"none"%>">
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
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="60" SortIndex="1">
                                                            <DataItemTemplate>
                                                                <div style="display: <%# SafeValue.SafeString(Eval("CargoStatus"))=="C"?"block":"none"%>">
                                                                    <input type="button" style="width: 60px" class="button" value="Pending" onclick="confirm_cargo_inline(<%# Container.VisibleIndex %>);" />
                                                                    
                                                                </div>
                                                                <div style="display: <%# SafeValue.SafeString(Eval("CargoStatus"))=="P"?"block":"none"%>">
                                                                    <input type="button" style="width: 60px" class="button" value="Confirm" onclick="confirm_cargo_inline(<%# Container.VisibleIndex %>);" />
                                                                </div>
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>' Width="165">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <div style="display: <%# SafeValue.SafeString(Eval("CargoStatus"))=="C"?"block":"none"%>">
                                                                    <input type="button" style="width: 60px" class="button" value="Pending" onclick="confirm_cargo_inline(<%# Container.VisibleIndex %>);" />
                                                                </div>
                                                                <div style="display: <%# SafeValue.SafeString(Eval("CargoStatus"))=="P"?"block":"none"%>">
                                                                    <input type="button" style="width: 60px" class="button" value="Confirm" onclick="confirm_cargo_inline(<%# Container.VisibleIndex %>);" />
                                                                </div>
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>' Width="165">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="CargoStatus" VisibleIndex="1" Width="40">
                                                            <DataItemTemplate><%# Eval("CargoStatus") %></DataItemTemplate>
                                                            <EditItemTemplate><%# Eval("CargoStatus") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Item Code" VisibleIndex="1" Width="120">
                                                            <DataItemTemplate>
                                                                <%# Eval("ActualItem") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxButtonEdit ID="btn_ActualItem" ClientInstanceName="btn_ActualItem" runat="server" Text='<%# Eval("ActualItem") %>' AutoPostBack="False" Width="100%">
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupSku(btn_ActualItem);
                                                                        }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Booking Qty" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Qty")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit runat="server" Width="100"
                                                                    ID="spin_Qty" Height="21px" Value='<%# Eval("Qty")%>' DisplayFormatString="0.0" DecimalPlaces="1" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Actual Qty" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("QtyOrig")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit runat="server" Width="100" DisplayFormatString="0.0" DecimalPlaces="1"
                                                                    ID="spin_QtyOrig" Height="21px" Value='<%# Eval("QtyOrig")%>' Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Wt" FieldName="Weight" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate><%# Eval("Weight") %></DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                                    ID="spin_Weight" Height="21px" Value='<%# Eval("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="M3" FieldName="Volume" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate><%# Eval("Volume") %></DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                                    ID="spin_Volume" Height="21px" Value='<%# Eval("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="1" Width="100">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Eval("Remark1") %>' Rows="3" runat="server" Width="100"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Lot No" Width="100" SortIndex="1" VisibleIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("BookingNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_BookingNo" Width="120" runat="server" Text='<%# Eval("BookingNo") %>'></dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Pallet No" Width="100" SortIndex="1" VisibleIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("PalletNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_PalletNo" ClientInstanceName="txt_PalletNo" runat="server" Text='<%# Eval("PalletNo") %>' AutoPostBack="False" Width="100%">
                                                                </dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Carton No" VisibleIndex="1" Width="100" SortIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("CartonNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_CartonNo" ClientInstanceName="txt_CartonNo" runat="server" Text='<%# Eval("CartonNo") %>' AutoPostBack="False" Width="100%">
                                                                </dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="1" Width="40">
                                                            <DataItemTemplate><%# Eval("Location") %></DataItemTemplate>
                                                            <EditItemTemplate><%# Eval("Location") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Expiry Date" VisibleIndex="1" Width="100">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeDateStr(Eval("Mft_ExpiryDate")) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxDateEdit ID="date_Mft_ExpiryDate" runat="server" Value='<%# Bind("Mft_ExpiryDate") %>' Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="On Hold" VisibleIndex="1" Width="100">
                                                            <DataItemTemplate>
                                                                <%# Eval("OnHold") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxComboBox ID="cmb_OnHold" runat="server" Value='<%# Eval("OnHold") %>' Width="100%">
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Yes" Value="Y" />
                                                                        <dxe:ListEditItem Text="No" Value="N" />
                                                                    </Items>
                                                                </dxe:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Balance Qty" VisibleIndex="2" Width="60" Visible="false">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("BalanceQty")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("BalanceQty")),1) %>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFooter="True" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="Id" SummaryType="Count" DisplayFormat="{0}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                                                    </TotalSummary>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="StockOut" Name="StockOut">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table style="width: 100%; padding: 0px">
                                                <tr>
                                                    <td>

                                                        <dxe:ASPxButton ID="ASPxButton31" runat="server" Text="Batch Edit" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                       BatchCargoEdit('OUT');
                    }" />
                                                        </dxe:ASPxButton>

                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton29" runat="server" Text="Add From Stock" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                       PopupJobHouse();
                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>

                                                        <dxe:ASPxButton ID="ASPxButton25" runat="server" Text="Refresh" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                       grid_stockOut.Refresh();
                    }" />
                                                        </dxe:ASPxButton>

                                                    </td>
                                                    <td width="90%"></td>
                                                </tr>
                                            </table>
                                            <table style="width: 60%; border: solid 1px #A8A8A8; background: #e8e8e8" >
                                                <tr>
                                                    <td style="width: 200px;display:none">
                                                        <a onclick="printTallySheetIndented();" style="text-decoration: underline; width: 150px"> Tallysheet Indented</a>

                                                    </td>
                                                    <td width="20px"></td>
                                                    <td style="width: 200px">
                                                        <a onclick="printTallySheetConfirmed('OUT');" style="text-decoration: underline; width: 200px"> Tallysheet Confirmed</a>

                                                    </td>
                                                    <td width="20px"></td>
                                                    <td style="width: 200px">
                                                        <a onclick="printStockTallySheet();" style="text-decoration: underline; width: 150px"> Stock Tallysheet</a>

                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="overflow-y: auto; width: 1000px">
                                                <table style="width: 1000px;">
                                                    <tr>
                                                        <td colspan="6" style="padding: 2px; background: #CCCCCC"><b>CURRENT</b></td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_stockOut" ClientInstanceName="grid_stockOut" runat="server" DataSourceID="dsStockOut" OnInit="grid_stockOut_Init" OnInitNewRow="grid_stockOut_InitNewRow"
                                                    OnRowInserting="grid_stockOut_RowInserting" OnRowUpdating="grid_stockOut_RowUpdating" OnRowDeleting="grid_stockOut_RowDeleting" OnRowInserted="grid_stockOut_RowInserted"
                                                    OnBeforePerformDataSelect="grid_stockOut_BeforePerformDataSelect" OnCustomDataCallback="grid_stockOut_CustomDataCallback"
                                                    KeyFieldName="Id" Width="1300px" AutoGenerateColumns="False">
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsPager Mode="ShowPager" PageSize="10"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="60px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td><a href="#" onclick='<%# "grid_stockOut.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a></td>
                                                                        <td><a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_stockOut.GetValuesOnCustomCallback(\"Delete_"+Container.VisibleIndex+"\",house_out_delete_cb);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a></td>
                                                                    </tr>
                                                                </table>

                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td><a href="#" onclick='<%# "grid_stockOut.GetValuesOnCustomCallback(\"Update_"+Container.VisibleIndex+"\",house_out_update_cb);" %>'>Update</a></td>
                                                                        <td>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>' Width="165">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" Visible="false">
                                                            <EditButton Visible="true"></EditButton>
                                                            <DeleteButton Visible="true"></DeleteButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" Width="150" VisibleIndex="0" FieldName="Id" Visible="false">
                                                            <DataItemTemplate>
                                                                <table style="display: <%# SafeValue.SafeString(Eval("CargoType"))=="IN"?"block":"none"%>">
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
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="60" SortIndex="1">
                                                            <DataItemTemplate>
                                                                <div style="display: <%# SafeValue.SafeString(Eval("CargoStatus"))=="C"?"block":"none"%>">
                                                                    <input type="button" style="width: 60px" class="button" value="Pending" onclick="confirm_cargo_inline1(<%# Container.VisibleIndex %>);" />
                                                                    
                                                                </div>
                                                                <div style="display: <%# SafeValue.SafeString(Eval("CargoStatus"))=="P"?"block":"none"%>">
                                                                    <input type="button" style="width: 60px" class="button" value="Confirm" onclick="confirm_cargo_inline1(<%# Container.VisibleIndex %>);" />
                                                                </div>
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>' Width="165">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <div style="display: <%# SafeValue.SafeString(Eval("CargoStatus"))=="C"?"block":"none"%>">
                                                                    <input type="button" style="width: 60px" class="button" value="Pending" onclick="confirm_cargo_inline1(<%# Container.VisibleIndex %>);" />
                                                                </div>
                                                                <div style="display: <%# SafeValue.SafeString(Eval("CargoStatus"))=="P"?"block":"none"%>">
                                                                    <input type="button" style="width: 60px" class="button" value="Confirm" onclick="confirm_cargo_inline1(<%# Container.VisibleIndex %>);" />
                                                                </div>
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>' Width="165">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="CargoStatus" VisibleIndex="1" Width="40">
                                                            <DataItemTemplate><%# Eval("CargoStatus") %></DataItemTemplate>
                                                            <EditItemTemplate><%# Eval("CargoStatus") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Item Code" VisibleIndex="1" Width="120">
                                                            <DataItemTemplate>
                                                                <%# Eval("ActualItem") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxButtonEdit ID="btn_ActualItem" ClientInstanceName="btn_ActualItem" runat="server" Text='<%# Eval("ActualItem") %>' AutoPostBack="False" Width="100%">
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupSku(btn_ActualItem);
                                                                        }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Booking Qty" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Qty")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit runat="server" Width="100"
                                                                    ID="spin_Qty" Height="21px" Value='<%# Eval("Qty")%>' DisplayFormatString="0.0" DecimalPlaces="1" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Actual Qty" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("QtyOrig")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit runat="server" Width="100" DisplayFormatString="0.0" DecimalPlaces="1"
                                                                    ID="spin_QtyOrig" Height="21px" Value='<%# Eval("QtyOrig")%>' Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Wt" FieldName="Weight" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate><%# Eval("Weight") %></DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                                    ID="spin_Weight" Height="21px" Value='<%# Eval("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="M3" FieldName="Volume" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate><%# Eval("Volume") %></DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                                    ID="spin_Volume" Height="21px" Value='<%# Eval("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="1" Width="100">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Eval("Remark1") %>' Rows="3" runat="server" Width="100"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Lot No" Width="100" SortIndex="1" VisibleIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("BookingNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_BookingNo" Width="120" runat="server" Text='<%# Eval("BookingNo") %>'></dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Pallet No" Width="100" SortIndex="1" VisibleIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("PalletNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_PalletNo" ClientInstanceName="txt_PalletNo" runat="server" Text='<%# Eval("PalletNo") %>' AutoPostBack="False" Width="100%">
                                                                </dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Carton No" VisibleIndex="1" Width="100" SortIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("CartonNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_CartonNo" ClientInstanceName="txt_CartonNo" runat="server" Text='<%# Eval("CartonNo") %>' AutoPostBack="False" Width="100%">
                                                                </dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="1" Width="40">
                                                            <DataItemTemplate><%# Eval("Location") %></DataItemTemplate>
                                                            <EditItemTemplate><%# Eval("Location") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Expiry Date" VisibleIndex="1" Width="100" SortIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeDateStr(Eval("Mft_ExpiryDate")) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxDateEdit ID="date_Mft_ExpiryDate" runat="server" Value='<%# Bind("Mft_ExpiryDate") %>' Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Balance Qty" VisibleIndex="2" Width="60" Visible="false">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("BalanceQty")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("BalanceQty")),1) %>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFooter="True" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                                                    </TotalSummary>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                            <div style="overflow-y: auto; width: 1000px">
                                                <div style="display: <%# (SafeValue.SafeString(Eval("JobType"))=="WDO"||SafeValue.SafeString(Eval("JobType"))=="WDO")?"none":"block"%>">
                                                    <table style="width: 1000px;">
                                                        <tr>
                                                            <td colspan="6" style="padding: 2px; background: #CCCCCC"><b>OTHERS DELIVERY ORDER</b></td>
                                                        </tr>
                                                    </table>
                                                    <dxwgv:ASPxGridView ID="grid_OtherOut" ClientInstanceName="grid_OtherOut" runat="server" DataSourceID="dsStockOtherOut"
                                                        OnBeforePerformDataSelect="grid_OtherOut_BeforePerformDataSelect" OnCustomDataCallback="grid_OtherOut_CustomDataCallback"
                                                        KeyFieldName="Id" Width="1300px" AutoGenerateColumns="False">
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsBehavior ConfirmDelete="true" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPager Mode="ShowPager" PageSize="2"></SettingsPager>
                                                        <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="CargoStatus" VisibleIndex="1" Width="40">
                                                            <DataItemTemplate><%# Eval("CargoStatus") %></DataItemTemplate>
                                                            <EditItemTemplate><%# Eval("CargoStatus") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Item Code" VisibleIndex="1" Width="120">
                                                            <DataItemTemplate>
                                                                <%# Eval("ActualItem") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxButtonEdit ID="btn_ActualItem" ClientInstanceName="btn_ActualItem" runat="server" Text='<%# Eval("ActualItem") %>' AutoPostBack="False" Width="100%">
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupSku(btn_ActualItem);
                                                                        }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Booking Qty" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Qty")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit runat="server" Width="100"
                                                                    ID="spin_Qty" Height="21px" Value='<%# Eval("Qty")%>' DisplayFormatString="0.0" DecimalPlaces="1" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Actual Qty" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("QtyOrig")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit runat="server" Width="100" DisplayFormatString="0.0" DecimalPlaces="1"
                                                                    ID="spin_QtyOrig" Height="21px" Value='<%# Eval("QtyOrig")%>' Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Wt" FieldName="Weight" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate><%# Eval("Weight") %></DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                                    ID="spin_Weight" Height="21px" Value='<%# Eval("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="M3" FieldName="Volume" VisibleIndex="1" Width="60">
                                                            <DataItemTemplate><%# Eval("Volume") %></DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                                                                    ID="spin_Volume" Height="21px" Value='<%# Eval("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="1" Width="100">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Eval("Remark1") %>' Rows="3" runat="server" Width="100"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Lot No" Width="100" SortIndex="1" VisibleIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("BookingNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_BookingNo" Width="120" runat="server" Text='<%# Eval("BookingNo") %>'></dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Pallet No" Width="100" SortIndex="1" VisibleIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("PalletNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_PalletNo" ClientInstanceName="txt_PalletNo" runat="server" Text='<%# Eval("PalletNo") %>' AutoPostBack="False" Width="100%">
                                                                </dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Carton No" VisibleIndex="1" Width="100" SortIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# Eval("CartonNo") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_CartonNo" ClientInstanceName="txt_CartonNo" runat="server" Text='<%# Eval("CartonNo") %>' AutoPostBack="False" Width="100%">
                                                                </dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="1" Width="40">
                                                                <DataItemTemplate><%# Eval("Location") %></DataItemTemplate>
                                                                <EditItemTemplate><%# Eval("Location") %></EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Expiry Date" VisibleIndex="1" Width="100" SortIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.SafeDateStr(Eval("Mft_ExpiryDate")) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxDateEdit ID="date_Mft_ExpiryDate" runat="server" Value='<%# Bind("Mft_ExpiryDate") %>' Width="100%" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Balance Qty" VisibleIndex="2" Width="60" Visible="false">
                                                            <DataItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("BalanceQty")),1) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("BalanceQty")),1) %>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                        <Settings ShowFooter="True" />
                                                        <TotalSummary>
                                                            <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                                                        </TotalSummary>
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Permit" Name="Permit">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table style="width: 1000px; display: none">
                                                <tr>
                                                    <td colspan="6">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td class="lbl">Warehouse</td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxButtonEdit ID="txt_WareHouseId1" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_WareHouseId1" runat="server" Text='<%# Eval("WareHouseCode") %>' Width="170" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupWh(txt_WareHouseId1,null);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td class="lbl">PermitNo</td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxTextBox ID="txt_wh_PermitNo" Width="100%" runat="server" Text='<%# Eval("WhPermitNo") %>'></dxe:ASPxTextBox>
                                                                </td>
                                                                <td class="lbl">IncoTerms</td>
                                                                <td class="ctl">
                                                                    <dxe:ASPxButtonEdit ID="txt_IncoTerm" ClientInstanceName="txt_IncoTerm" runat="server" Text='<%# Eval("IncoTerm") %>' Width="150" AutoPostBack="False">
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
                                                                            <dxe:ListEditItem Text="TSL" Value="TSL" />
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

                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Add New" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                                                 grid_permit.GetValuesOnCustomCallback('AutoLines',onPermitCallBack);
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>

                                                        <dxe:ASPxButton ID="ASPxButton27" runat="server" Text="Refresh" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e){
                       grid_permit.Refresh();
                    }" />
                                                        </dxe:ASPxButton>

                                                    </td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView runat="server" ID="grid_permit" OnInit="grid_permit_Init" OnInitNewRow="grid_permit_InitNewRow" ClientInstanceName="grid_permit" OnRowInserting="grid_permit_RowInserting" OnRowUpdating="grid_permit_RowUpdating"
                                                Width="1000px" DataSourceID="dsRefPermit" KeyFieldName="Id" AutoGenerateColumns="false" OnRowDeleting="grid_permit_RowDeleting" OnBeforePerformDataSelect="grid_permit_BeforePerformDataSelect"
                                                OnCustomDataCallback="grid_permit_CustomDataCallback" Styles-CommandColumn-HorizontalAlign="Left" Styles-CommandColumn-Spacing="10">
                                                <SettingsBehavior ConfirmDelete="true" />
                                                <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="Click" />
                                                <Settings ShowTitlePanel="true" />
                                                <SettingsPager Mode="ShowPager" PageSize="10" />
                                                <SettingsCommandButton DeleteButton-ButtonType="Button" CancelButton-Text="Cancel" UpdateButton-ButtonType="Button" CancelButton-ButtonType="Button" UpdateButton-Text="Save Line"></SettingsCommandButton>
                                                <Templates>
                                                    <TitlePanel>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxCheckBox ID="ack_DelAll" runat="server" Width="10" AutoPostBack="False"
                                                                        UseSubmitBehavior="False">
                                                                        <ClientSideEvents CheckedChanged="function(s, e) {
                                   SelectAll_Permit();
                                    }" />
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButton ID="btnDeleteAll" ClientInstanceName="btnDeleteAll"
                                                                        Text="Delete All" runat="server" Cursor="pointer" AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                if(confirm('Confirm Delete?')){
                                   grid_permit.GetValuesOnCustomCallback('Delete',onPermitCallback); 
                                                                                  }  
                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </TitlePanel>
                                                </Templates>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="#" Width="50" VisibleIndex="0" FieldName="Id" ReadOnly="true">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxCheckBox ID="ack_IsDel_Permit" runat="server" Width="10">
                                                            </dxe:ASPxCheckBox>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate></EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Permit No" FieldName="PermitNo" VisibleIndex="1"
                                                        Width="150">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="ContId" Caption="Container" Width="120" VisibleIndex="1" Visible="false">
                                                        <PropertiesComboBox DataSourceID="dsCont1" ValueField="Id" TextField="ContainerNo" ValueType="System.Int32" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="HblNo" Caption="Hbl No" Width="120" VisibleIndex="1">
                                                        <PropertiesComboBox DataSourceID="dsHouse" ValueField="HblNo" TextField="HblNo" ValueType="System.String" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Permit By" FieldName="PermitBy" VisibleIndex="1"
                                                        Width="150">
                                                        <PropertiesComboBox>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Client" Value="Client" />
                                                                <dxe:ListEditItem Text="TSL" Value="TSL" />
                                                            </Items>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="Permit Date" FieldName="PermitDate" VisibleIndex="1" Width="120px">
                                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="IncoTerms" FieldName="IncoTerm" VisibleIndex="1"
                                                        Width="100" Visible="false">
                                                    </dxwgv:GridViewDataTextColumn>

                                                    <dxwgv:GridViewDataSpinEditColumn Caption="GstAmt" FieldName="GstAmt" Visible="false" VisibleIndex="1" Width="120px">
                                                        <PropertiesSpinEdit NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Supp. InvNo" Visible="false" FieldName="PartyInvNo" VisibleIndex="1"
                                                        Width="150">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Payment Status" FieldName="PaymentStatus" VisibleIndex="1"
                                                        Width="150">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Quotation" Name="Quotation">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table style="width: 100%">
                                                <tr style="vertical-align: top;">
                                                    <td class="lbl">
                                                        <a href="#" onclick="javasrcipt:PopupRemark(txt_JobDes,null)">Job Description
                                                        </a>

                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxMemo ID="txt_JobDes" ClientInstanceName="txt_JobDes" Rows="3" runat="server" Text='<%# Eval("JobDes") %>' Width="350"></dxe:ASPxMemo>
                                                    </td>
                                                    <td class="lbl" style="display: none"><a href="#" onclick="javasrcipt:PopupRemark(txt_QuoteRemark,null)">Quote Remark
                                                    </a></td>
                                                    <td class="ctl" style="display: none">
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
                                                        <dxe:ASPxButton ID="ASPxButton19" runat="server" Text="Auto Add Lines" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                                                 grid_job.GetValuesOnCustomCallback('AutoLines',onQuotedCallBack);
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton12" runat="server" Text="Add Charges" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                                                PopupRate();
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton14" runat="server" Text="Pick from Master Rate" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                                      PopupMasterRate();
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td style="display: <%# SafeValue.SafeString(Eval("JobNo"))==SafeValue.SafeString(Eval("QuoteNo"))?"none":"table-cell"%>">
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
                                                        <dxe:ASPxButton ID="ASPxButton6" runat="server" Text="Print Quotation: Single Unit" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e){
                                                      PrintSingleQuotation();
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_AddRate" runat="server" Text="Print Quotation: Multiple Unit" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e){
                                                      PrintQuotation();
                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td style="display: <%# SafeValue.SafeString(Eval("JobNo"))==SafeValue.SafeString(Eval("QuoteNo"))?"none":"table-cell"%>">
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
                                                                        <dxe:ASPxTextBox ID="txt_Email3" runat="server"></dxe:ASPxTextBox>
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
                                                <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server" OnRowUpdating="grid1_RowUpdating" OnRowInserting="grid1_RowInserting" OnInit="grid1_Init" OnInitNewRow="grid1_InitNewRow"
                                                    DataSourceID="ds1" KeyFieldName="Id" OnCustomDataCallback="grid1_CustomDataCallback" OnBeforePerformDataSelect="grid1_BeforePerformDataSelect"
                                                    Width="1300px" AutoGenerateColumns="False" Styles-CommandColumn-HorizontalAlign="Left" Styles-CommandColumn-Spacing="10">
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Row" BatchEditSettings-StartEditAction="Click" />
                                                    <Settings ShowTitlePanel="true" />
                                                    <SettingsPager Mode="ShowAllRecords" />
                                                    <SettingsCommandButton CancelButton-Text="Cancel" UpdateButton-ButtonType="Button" CancelButton-ButtonType="Button" UpdateButton-Text="Save Line"></SettingsCommandButton>
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
                                                        <dxwgv:GridViewDataTextColumn Caption="#" Width="50" VisibleIndex="0" FieldName="Id" ReadOnly="true">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxCheckBox ID="ack_IsDel" runat="server" Width="10">
                                                                </dxe:ASPxCheckBox>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewCommandColumn Visible="false" VisibleIndex="0" Width="5%">
                                                            <EditButton Visible="True" />
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewCommandColumn Visible="false" VisibleIndex="0" Width="5%">
                                                            <DeleteButton Visible="true" />
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Index" FieldName="LineIndex" VisibleIndex="1" SortOrder="Ascending" SortIndex="0"
                                                            Width="50">
                                                            <PropertiesSpinEdit Increment="0" DecimalPlaces="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="Update" FieldName="RowUpdateTime" VisibleIndex="998"
                                                            Width="140">
                                                            <PropertiesDateEdit EditFormat="DateTime"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="ChgCode" Caption="ChargeCode" Width="50" VisibleIndex="1">
                                                            <PropertiesComboBox DataSourceID="dsRate" ValueField="ChgCodeId" TextField="ChgCodeDe" ValueType="System.String" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataMemoColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                                                            Width="260" PropertiesMemoEdit-Rows="3" UnboundType="String">
                                                            <PropertiesMemoEdit ValidationSettings-RequiredField-IsRequired="false" NullText=" "></PropertiesMemoEdit>
                                                        </dxwgv:GridViewDataMemoColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="LumsumInd" Caption="Lumsum Ind" Width="50" VisibleIndex="1">
                                                            <PropertiesComboBox ValueType="System.String">
                                                                <Items>
                                                                    <dxe:ListEditItem Text=" " Value=" " />
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
                                                            <Settings AllowAutoFilterTextInputTimer="Default" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataMemoColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                                                            Width="150" PropertiesMemoEdit-Rows="3">
                                                        </dxwgv:GridViewDataMemoColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="ContSize" Caption="Cont Size" Width="50" VisibleIndex="1" Visible="false">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text=" " Value=" " />
                                                                    <dxe:ListEditItem Text="20" Value="20" />
                                                                    <dxe:ListEditItem Text="40" Value="40" />
                                                                    <dxe:ListEditItem Text="45" Value="45" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="ContType" Caption="Cont Type" Width="150" VisibleIndex="1" Visible="false">
                                                            <PropertiesComboBox DataSourceID="dsContType" ValueType="System.String" TextField="containerType" ValueField="containerType">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="BillType" Caption="Bill Type" Width="150" VisibleIndex="1" Visible="false">
                                                            <PropertiesComboBox DataSourceID="dsRateType" ValueType="System.String" TextField="Code" ValueField="Code">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="BillScope" Caption="Scope" Width="80" VisibleIndex="1" Visible="false">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Job" Value="JOB"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="Cont" Value="CONT"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="Adhoc" Value="ADHOC"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text=" " Value=" "></dxe:ListEditItem>
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>

                                                    </Columns>
                                                    <Settings ShowFooter="true" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="Price" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                                    </TotalSummary>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                            <table style="width: 1000px; vertical-align: top">
                                                <tr style="vertical-align: top;">
                                                    <td class="lbl">
                                                        <a href="#" onclick="javasrcipt:PopupRemark(txt_TerminalRemark,null)">Terms & Conditions
                                                        </a>
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxMemo ID="txt_TerminalRemark" ClientInstanceName="txt_TerminalRemark" Rows="3" runat="server" Text='<%# Eval("TerminalRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td class="lbl" style="display: none">
                                                        <a href="#" onclick="javasrcipt:PopupRemark(memo_AdditionalRemark,null)">Additional Remark
                                                        </a>
                                                    </td>
                                                    <td class="ctl" style="display: none">
                                                        <dxe:ASPxMemo ID="memo_AdditionalRemark" ClientInstanceName="memo_AdditionalRemark" Rows="3" runat="server" Text='<%# Eval("AdditionalRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: top;">
                                                    <td class="lbl" style="display: none"><a href="#" onclick="javasrcipt:PopupRemark(memo_LumSumRemark,null)">LumSum Remark
                                                    </a></td>
                                                    <td class="ctl" style="display: none">
                                                        <dxe:ASPxMemo ID="memo_LumSumRemark" ClientInstanceName="memo_LumSumRemark" Rows="3" runat="server" Text='<%# Eval("LumSumRemark") %>' Width="100%"></dxe:ASPxMemo>
                                                    </td>
                                                    <td class="lbl">
                                                        <a href="#" onclick="javasrcipt:PopupRemark(memo_InternalRmark,null)">Internal Remark
                                                        </a>
                                                    </td>
                                                    <td class="ctl">
                                                        <dxe:ASPxMemo ID="memo_InternalRmark" ClientInstanceName="memo_InternalRmark" Rows="3" runat="server" Text='<%# Eval("InternalRemark") %>' Width="350"></dxe:ASPxMemo>
                                                    </td>

                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Costing" Name="Costing">
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
                                                        <dxe:ASPxButton ID="ASPxButton7" Width="150" Visible="true" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
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
                                                runat="server" KeyFieldName="SequenceId" DataSourceID="dsInvoice" Width="1000px"
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
                                                        <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Visible="false" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
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
                                                runat="server" KeyFieldName="SequenceId" DataSourceID="dsVoucher" Width="1000px"
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
                                            <div style="overflow-y: auto; width: 1000px">
                                                <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server" OnHtmlDataCellPrepared="grid_Cost_HtmlDataCellPrepared"
                                                    DataSourceID="dsCosting" KeyFieldName="Id" Width="1000px" OnBeforePerformDataSelect="grid_Cost_DataSelect"
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
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE"&& SafeValue.SafeString(Eval("LineSource"))=="M" %>'
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
                                                        <dxwgv:GridViewDataTextColumn Caption="Rec No" FieldName="ReceiptNo" VisibleIndex="5" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Rec Remark" FieldName="ReceiptRemark" VisibleIndex="5" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
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
                                                                                    <dxe:ASPxTextBox runat="server" Width="95%" ID="txt_ContNo" Text='<%# Bind("ContNo") %>'>
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
                                                                    <td class="lbl">Rec No</td>
                                                                    <td colspan="2">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Receipt" Text='<%# Bind("ReceiptNo") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">Rec Remark</td>
                                                                    <td colspan="2">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Remark" Text='<%# Bind("ReceiptRemark") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td class="lbl">Pay Ind</td>
                                                                    <td>
                                                                        <dxe:ASPxCheckBox runat="server" Width="100%" ID="ckb_Pay_Ind"></dxe:ASPxCheckBox>
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
                                                    <dxwgv:GridViewDataColumn FieldName="Controller" Caption="User" Width="100"></dxwgv:GridViewDataColumn>
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
                                                        <dxe:ASPxButton ID="ASPxButton28" Width="150" runat="server" Text="Show Attachments"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                                PopupPhotoList();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton32" Width="150" runat="server" Text="Multiple Upload Attachments"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                                PopupMultipleUploadPhoto();
                                                        }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>

                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Control" Name="Close File" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl1" runat="server" Width="1000px">
                                            <table style="width: 1000px">
                                                <tr valign="top">
                                                    <td class="lbl" rowspan="2">Remark</td>
                                                    <td rowspan="2">
                                                        <dxe:ASPxMemo ID="memo_CLSRMK" ClientInstanceName="memo_CLSRMK" Rows="3" runat="server" Width="300"></dxe:ASPxMemo>
                                                    </td>
                                                    <td class="lbl1">Billing Master No
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_BillingRefNo" ClientInstanceName="txt_BillingRefNo" runat="server" Text='<%# Eval("BillingRefNo") %>' Width="100%"></dxe:ASPxTextBox>
                                                    </td>
                                                    <td class="lbl1">Show In Billing
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbb_ShowInvoice_Ind" runat="server" Value='<%# Eval("ShowInvoice_Ind") %>' Width="60">
                                                            <Items>
                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="display:none">
                                                                    <dxe:ASPxButton ID="btn_ChooseSubJob" runat="server" Text="Choose Sub Job" AutoPostBack="false" UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>' >
                                                                        <ClientSideEvents Click="function(s,e){
                                                                   PopupSubJobNo();
                                                                 }" />
                                                                    </dxe:ASPxButton>
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
                                                                    <dxe:ASPxButton ID="btn_JobVoid" ClientInstanceName="btn_JobVoid" runat="server" Text="Void Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>' Width="100">
                                                                        <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Confirm '+btn_JobVoid.GetText()+' it?')) {
                                                    grid_job.GetValuesOnCustomCallback('Void',onCallBack);
                                                    }
                                                 }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>

                                                                    <dxe:ASPxButton ID="btn_controlEventLog" runat="server" Text="Event Log" AutoPostBack="false" Width="100">
                                                                        <ClientSideEvents Click="function(s,e){PopupEventLog(txt_JobNo.GetText())}" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="4">
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
            </dxwgv:aspxgridview>
            <dxpc:aspxpopupcontrol id="popubCtr" runat="server" closeaction="CloseButton" modal="True"
                popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter" clientinstancename="popubCtr"
                headertext="Party" allowdragging="True" enableanimation="False" height="570"
                width="1050" enableviewstate="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:aspxpopupcontrol>
            <dxpc:aspxpopupcontrol id="popubCtrPic" runat="server" closeaction="CloseButton" modal="True"
                popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter" clientinstancename="popubCtrPic"
                headertext="Party" allowdragging="True" enableanimation="False" height="600"
                width="1050" AllowResize="true" enableviewstate="False">
                <ClientSideEvents CloseUp="function(s, e) {
                    

      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:aspxpopupcontrol>
            <dxpc:aspxpopupcontrol id="popubCtr1" runat="server" closeaction="CloseButton" modal="True"
                popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter" clientinstancename="popubCtr1"
                headertext="Customer" allowdragging="True" enableanimation="False" height="570"
                width="1050" enableviewstate="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:aspxpopupcontrol>
            <dxpc:aspxpopupcontrol id="popubCtr2" runat="server" closeaction="CloseButton" modal="True"
                popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter" clientinstancename="popubCtr2"
                headertext="Party" allowdragging="True" enableanimation="False" height="450"
                width="600" enableviewstate="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:aspxpopupcontrol>
            <dxpc:ASPxPopupControl ID="popubCtrCargo" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrCargo"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="100%"
                Width="100%" MaxWidth="2600px" MaxHeight="1100px" MinHeight="600px" MinWidth="1100px" AllowResize="true" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
