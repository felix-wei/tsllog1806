var temp1 = null;
var temp2 = null;
var temp3 = null;

function CTM_PutValue2(value1, value2) {
    if (temp1 != null) {
        temp1.SetText(value1);
    }
    if (temp2 != null) {
        temp2.SetText(value2);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function CTM_PutValue3(value1, value2, value3) {
    if (temp1 != null) {
        temp1.SetText(value1);
    }
    if (temp2 != null) {
        temp2.SetText(value2);
    }
    if (temp3 != null) {
        temp3.SetText(value3);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}

function PopupCTM_Driver(txtCode, txtName, txtTowHead) {
    temp1 = txtCode;
    temp2 = txtName;
    temp3 = txtTowHead;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/DriverList.aspx');
    popubCtr.SetHeaderText('Driver');
    //popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/DriverLogList.aspx?Date=' + Date);
    //popubCtr.SetHeaderText('DriverLog');
    popubCtr.Show();
}

function Popup_VehicleList(txtCode, txtType, searchType) {
    temp1 = txtCode;
    temp2 = txtType;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/VehicleList.aspx?type=' + searchType);
    popubCtr.SetHeaderText('Vehicle');
    popubCtr.Show();
}

function PopupCTM_DriverLog(txtCode, txtName, txtTowHead, Date) {
    temp1 = txtCode;
    temp2 = txtName;
    temp3 = txtTowHead;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/DriverList.aspx');
    popubCtr.SetHeaderText('Driver');
    //popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/DriverLogList.aspx?Date=' + Date);
    //popubCtr.SetHeaderText('DriverLog');
    popubCtr.Show();
}

function Popup_TerminalList(txtCode, txtType) {
    temp1 = txtCode;
    temp2 = txtType;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/TerminalList.aspx');
    popubCtr.SetHeaderText('Terminal');
    popubCtr.Show();
}

function Popup_TowheadList(txtCode, txtType) {
    temp1 = txtCode;
    temp2 = txtType;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/TowheadList.aspx');
    popubCtr.SetHeaderText('Towhead');
    popubCtr.Show();
}

function Popup_LocationList(txtCode, txtType, lcType) {
    temp1 = txtCode;
    temp2 = txtType;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/LocationList.aspx?type=' + lcType);
    popubCtr.SetHeaderText(lcType);
    popubCtr.Show();
}

var Popup_ContainerBatchAdd_cb = null;


function Popup_ContainerBatchEdit(jobno,jobtype, cb) {
    Popup_ContainerBatchAdd_cb = cb;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/Container_BatchEdit1.aspx?no=' + jobno + "&type=" + jobtype);
    popubCtr.SetHeaderText('Batch Edit');
    popubCtr.Show();
}

function Popup_ContainerBatchEdit_callback(v) {
    ClosePopupCtr();
    if (Popup_ContainerBatchAdd_cb) {
        Popup_ContainerBatchAdd_cb(v);
        Popup_ContainerBatchAdd_cb = null;
    }
}



function Popup_ContainerBatchAdd(jobno,jobtype, cb) {
    Popup_ContainerBatchAdd_cb = cb;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/Container_BatchAdd1.aspx?no=' + jobno + "&type=" + jobtype);
    popubCtr.SetHeaderText('Batch Add');
    popubCtr.Show();
}


function Popup_ContainerBatchAdd_callback(v) {
    ClosePopupCtr();
    if (Popup_ContainerBatchAdd_cb) {
        Popup_ContainerBatchAdd_cb(v);
        Popup_ContainerBatchAdd_cb = null;
    }
}

function PopupTrip(txtId,txtName, jobNo,type) {
    temp1 = txtId;
    temp2 = txtName;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectTrip.aspx?no=' + jobNo+'&tripType='+type);
    popubCtr.SetHeaderText('Trip List');
    popubCtr.Show();
}
function PopupParkingLot(txtId, txtName) {
    temp1= txtId;
    temp2 = txtName;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/ParkingLotList.aspx');
    popubCtr.SetHeaderText('Parking Lot');
    popubCtr.Show();
}
function PopupWh(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectWarehouse.aspx');
    popubCtr.SetHeaderText('WareHouse');
    popubCtr.Show();
}
function PopupSku(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Product List ');
    popubCtr.SetContentUrl('/SelectPage/SelectProduct.aspx');
    popubCtr.Show();
}
function PopupLoc(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Unit&wh=' + txt_WareHouseId.GetText());
    popubCtr.SetHeaderText('Location');
    popubCtr.Show();
}
function PopupRemark(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectRemarkTemplate.aspx');
    popubCtr.SetHeaderText('Remark Template');
    popubCtr.Show();
}
function PopupContNo(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectContainerByJob.aspx?no=' + txt_JobNo.GetText());
    popubCtr.SetHeaderText('Container List');
    popubCtr.Show();
}
function PopupEventLog(JobNo) {
    popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/EventLog.aspx?JobNo=' + JobNo);
    popubCtr.SetHeaderText('Event Log');
    popubCtr.Show();
}
function PopupJobReceipt(id) {
    popubCtrPic.SetHeaderText('Receipt List');
    popubCtrPic.SetContentUrl('/Modules/Tpt/SelectPage/JobReceiptList.aspx?no=' + id+ "&jobNo=" + txt_JobNo.GetText());
    popubCtrPic.Show();
}
function PopupTripCargo(id) {
    popubCtrPic.SetHeaderText('Cargo List');
    popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/ShowCargoForTrip.aspx?no=' + id);
    popubCtrPic.Show();
}
function PopupReturnCargo(type,jobNo) {
    popubCtrPic.SetHeaderText('Cargo List');
    popubCtrPic.SetContentUrl('/PagesContTrucking/SelectPage/SelectCargoForReturn.aspx?no=' + jobNo+'&type='+type);
    popubCtrPic.Show();
}

function printTptTrip(id,jobNo, jobType) {
    if (jobType == "WGR")
        window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=gr&no=" + jobNo + "&id=" + id);
    if (jobType == "WDO")
        window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=do&no=" + jobNo + "&id=" + id);
    if (jobType == "TPT")
        window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=tp&no=" + jobNo + "&id=" + id);
    if (jobType == "FRT")
        window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=tr&no=" + jobNo + "&id=" + id);
    if (jobType == "DN")
        window.open(" /PagesContTrucking/Report/RptPrintView.aspx?doc=DN&refType=dn&no=" + jobNo + "&id=" + id);
}

function PopupTripsList(jobno, tripId, tripIndex, canGO) {
    if (canGO != "GO") {
        return;
    }
    popubCtr1.SetHeaderText('Trip Edit');
    popubCtr1.SetContentUrl('/Modules/Tpt/SelectPage/TripEditForJobList.aspx?JobNo=' + jobno + "&tripId=" + tripId + "&tripIndex=" + tripIndex);
    popubCtr1.Show();
}
function AfterPopubTptTrip() {
    gv_tpt_trip.Refresh();
}
//Container/Trialer/Trip
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
        //setTimeout(function () {
        //    grid_Cont_Tpt.Refresh();
        //}, 500);
    }
}
function transport_trip_update_cb(v) {

    gv_tpt_trip.CancelEdit();
    //setTimeout(function () {
    //    gv_tpt_trip.Refresh();
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
    console.log('=========transport', detail);
    SV_Firebase.publish_system_msg_send('refreshList', 'ELL_LCLJob_Calendar', JSON.stringify(detail));

}
function transport_trip_delete_cb(v) {

    gv_tpt_trip.CancelEdit();
    //setTimeout(function () {
    //    //if (grid_Trip) {
    //    //    grid_Trip.CancelEdit();
    //    //}
    //    gv_tpt_trip.Refresh();
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
    SV_Firebase.publish_system_msg_send('refreshList', 'ELL_LCLJob_Calendar', JSON.stringify(detail));
}

function crane_trip_update_cb(v) {
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
    SV_Firebase.publish_system_msg_send('refreshList', 'SV_ELL_Crane_Schedule', JSON.stringify(detail));

}

function crane_trip_delete_cb(v) {
    //if (grid_Trip) {
    //    grid_Trip.CancelEdit();
    //}
    if (grid_Trip) {
        grid_Trip.Refresh();
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
    SV_Firebase.publish_system_msg_send('refreshList', 'SV_ELL_Crane_Schedule', JSON.stringify(detail));
}


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
    window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=gr&no=" + txt_JobNo.GetText());
}
function PrintDO() {
    window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=do&no=" + txt_JobNo.GetText());
}
function PrintTP() {
    window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=tp&no=" + txt_JobNo.GetText());
}
function PrintTR() {
    window.open("/PagesContTrucking/Report/RptPrintView.aspx?doc=TPT&refType=tr&no=" + txt_JobNo.GetText());
}
function PrintDN() {
    window.open(" /PagesContTrucking/Report/RptPrintView.aspx?doc=DN&refType=dn&no=" + txt_JobNo.GetText());
}
