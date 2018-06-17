//Job List
function goJob(jobno) {
    parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
}
function PopupTripsList(jobno, contId, canGO) {
    if (canGO != "GO") {
        return;
    }
    popubCtr1.SetHeaderText('Trips List');
    popubCtr1.SetContentUrl('../SelectPage/TripListForJobList.aspx?JobNo=' + jobno + "&contId=" + contId);
    popubCtr1.Show();
}
function NewAdd_Visible(doShow, par) {
    if (doShow) {
        var t = new Date();
        var type = lbl_type.GetText();
        txt_new_JobDate.SetText(t);
        cbb_new_jobtype.SetValue(type);
        btn_new_ClientId.SetText('');
        txt_new_ClientName.SetText('');
        txt_FromAddress.SetText('');
        //txt_WarehouseAddress.SetText('');
        txt_ToAddress.SetText('');
        //txt_DepotAddress.SetText('');
        txt_new_remark.SetText('');
        if (par == 'Q') {
            lbl_Header.SetText('New Quotation');
            cbb_new_jobstatus.SetText("Quoted");
        }
        if (par == 'J') {
            lbl_Header.SetText('New Job');
            cbb_new_jobstatus.SetText("Confirmed");
        }
        ASPxPopupClientControl.Show();

    }
    else {
        ASPxPopupClientControl.Hide();
    }
}
function NewAdd_Save() {
    var jobType = cbb_new_jobtype.GetValue();
    //console.log(jobType);
    if (jobType && jobType.length > 0) {
        detailGrid.GetValuesOnCustomCallback('OK', OnSaveCallBack);
    } else {
        alert('Require JobType!');
    }
}
function OnSaveCallBack(v) {
    if (v != null && v.indexOf("Fail") > -1) {
        alert(v);
    }
    else {
        if (v != null) {

            goJob(v);

            ASPxPopupClientControl.Hide();
        }
    }
}
function onQuotedCallBack(v) {
    if (v != null && v.length > 0) {
        alert(v);
        grid1.Refresh();
    }
}
function AfterAddTrip() {
    popubCtr1.Hide();
    popubCtr1.SetContentUrl('about:blank');
}
function cbb_checkbox_Type(name) {
    //console.log('========= checkbox', name);
    if (name == 'ALL') {
        cb_ContStatus1.SetValue(cb_ContStatus0.GetValue());
        cb_ContStatus2.SetValue(cb_ContStatus0.GetValue());
        cb_ContStatus3.SetValue(cb_ContStatus0.GetValue());
    } else {
        if (cb_ContStatus1.GetValue() && cb_ContStatus2.GetValue() && cb_ContStatus3.GetValue()) {
            cb_ContStatus0.SetValue(true);
        } else {
            cb_ContStatus0.SetValue(false);
        }
    }
}
function cbb_checkbox_Type1(name) {
    //console.log('========= checkbox', name);
    if (name == 'Uncomplete') {
        if (cb_ContStatus4.GetValue()) {
            cb_ContStatus1.SetValue(false);
            cb_ContStatus2.SetValue(false);
            cb_ContStatus3.SetValue(false);
        }
    } else {
        if (cb_ContStatus1.GetValue() || cb_ContStatus2.GetValue() || cb_ContStatus3.GetValue()) {
            cb_ContStatus4.SetValue(false);
        }
    }
}


//Save,Update,Create,Delete
function RowClickHandler(s, e) {
    dde_Trip_ContNo.SetText(gridPopCont.cpContN[e.visibleIndex]);
    dde_Trip_ContId.SetText(gridPopCont.cpContId[e.visibleIndex]);
    dde_Trip_ContNo.HideDropDown();
}
function onCallBack(v) {
    if (v == "PrintHaulier")
        window.open('/PagesContTrucking/Report/RptPrintView.aspx?doc=haulier&no=' + txt_JobNo.GetText() + "&type=" + cbb_JobType.GetValue());
    else if (v != null && v.length > 0) {
        alert(v);
        grid_job.Refresh();
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
        window.location = 'TptJobEdit.aspx?no=' + jobno;
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
function transport_batch_add() {
    var jobno = txt_JobNo.GetText();
    //console.log(jobno);
    Popup_ContainerBatchAdd(jobno, transport_batch_add_cb);
}
function transport_batch_add_cb(v) {
    console.log(v);
    if (v == "success") {
        //if (grid_Trip) {
        //    grid_Trip.CancelEdit();
        //}
        grid_Cont_Tpt.CancelEdit();
        setTimeout(function () {
            grid_Cont_Tpt.Refresh();
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
function transport_trip_update_cb(v) {

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
function transport_trip_delete_cb(v) {

    gv_tpt_trip.CancelEdit();
    setTimeout(function () {
        //if (grid_Trip) {
        //    grid_Trip.CancelEdit();
        //}
        gv_tpt_trip.Refresh();
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

//Popup
var isUpload = false;
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
function PopupUploadPhoto() {
    popubCtrPic.SetHeaderText('Upload Attachment');
    popubCtrPic.SetContentUrl('/PagesContTrucking/Upload.aspx?Type=CTM&Sn=0&jobNo=' + txt_JobNo.GetText());
    popubCtrPic.Show();
}
function PopupJobRate() {
    popubCtr.SetHeaderText('Job Rate ');
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectJobRate.aspx?no=' + txt_JobNo.GetText() + '&type=' + cbb_JobType.GetValue() + '&clientId=' + btn_ClientId.GetText());
    popubCtr.Show();
}
function PopupJobHouse() {
    popubCtrPic.SetHeaderText('Stock List');
    popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/SelectStock.aspx?no=' + txt_JobNo.GetText() + "&client=" + btn_ClientId.GetText() + '&jobType=' + cbb_JobType.GetValue());
    popubCtrPic.Show();
}
function PopupJobReceipt() {
    popubCtrPic.SetHeaderText('Receipt List');
    popubCtrPic.SetContentUrl('/Modules/Tpt/SelectPage/JobReceiptList.aspx?no=' + txt_Id.GetText() +"&jobNo="+ txt_JobNo.GetText());
    popubCtrPic.Show();
}
function AfterPopub() {
    popubCtrPic.Hide();
    popubCtrPic.SetContentUrl('about:blank');
}
function AfterPopubRate() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
    grid1.Refresh();
}
function PopupStockForTransfer() {
    popubCtrPic.SetHeaderText('Stock List');
    popubCtrPic.SetContentUrl('/Modules/Tpt/SelectPage/SelectStock.aspx?no=' + txt_JobNo.GetText() + "&client=" + btn_ClientId.GetText() + '&jobType=' + cbb_JobType.GetValue() + '&wh=' + txt_WareHouseId.GetText());
    popubCtrPic.Show();
}
function PopupLocation(txtId, txtName, type,wh) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Unit&type=' + type + '&wh='+wh);
    popubCtr.SetHeaderText('Location');
    popubCtr.Show();
}
//Print
function printJobSheet() {
    window.open(" /PagesContTrucking/Report/RptPrintView.aspx?doc=4&no=" + txt_search_JobNo.GetText());
}
function printJobCost() {
    window.open("/PagesContTrucking/PrintJob.aspx?o=" + txt_search_JobNo.GetText());
}
function printTallySheet() {
    window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=tallysheet&no=" + txt_search_JobNo.GetText());
}
function PrintQuotation(invN, docType) {
    window.open('/ReportFreightSea/printview.aspx?document=Quotation&master=' + txt_QuoteNo.GetText() + '&docType=' + cmb_JobStatus.GetText());

}
function PrintGRN() {
    window.open("http://ell.cargoerp.com/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=gr&no=" + txt_JobNo.GetText());
}
function PrintDO() {
    window.open("http://ell.cargoerp.com/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=do&no=" + txt_JobNo.GetText());
}
function PrintTP() {
    window.open("http://ell.cargoerp.com/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=tp&no=" + txt_JobNo.GetText());
}
function PrintTR() {
    window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=tr&no=" + txt_JobNo.GetText());
}
function PrintDN() {
    window.open(" /PagesContTrucking/Report/RptPrintView.aspx?doc=DN&refType=dn&no=" + txt_JobNo.GetText());
}

function ShowGR(masterId) {
    parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoInEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
    //window.location = "DoInEdit.aspx?no=" + masterId;
}
function ShowDO(masterId) {
    parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
    //window.location = "DoOutEdit.aspx?no=" + masterId;
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
    popubCtrPic.SetContentUrl('/PagesContTrucking/Upload.aspx?Type=CTM&Sn=' + ar[0] + '&ContNo=' + ar[1] + '&jobNo=' + txt_JobNo.GetText());
    popubCtrPic.Show();
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