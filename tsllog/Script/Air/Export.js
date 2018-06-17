
function PopupUploadPhoto() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('../Upload.aspx?Type=AE&Sn=' + txt_RefN.GetText());
    popubCtr.Show();
}
function AfterUploadPhoto() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
// change export ref
function PopupExpRef(txtId) {
    clientId = txtId;
    popubCtr.SetHeaderText('Import Ref List');
    popubCtr.SetContentUrl('ImportRefSelect.aspx');
    popubCtr.Show();
}
function PutRefNo(masterNo) {
    if (clientId != null) {
        clientId.SetText(masterNo);
        detailGrid.GetValuesOnCustomCallback('ChgRef', OnChgRefCallback);
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
        clientId = null;
    }
}
//cancel ref
function OnChgRefCallback(v) {
    if (v == "Success") {
        alert("Change Ref Success!");
        detailGrid.Refresh();
    }
    else if (v == "Fail")
        alert("Change Ref Fail,please try again!");

}
////////////////for dropdown list
function RowClickHandler(s, e) {
    SetLookupKeyValue(e.visibleIndex);
    DropDownEdit.HideDropDown();
}
function SetLookupKeyValue(rowIndex) {

    DropDownEdit.SetText(GridView.cpContN[rowIndex]);
    txt_sealN.SetText(GridView.cpSealN[rowIndex]);
}

// cust rate
function OnCallback(v) {
    Grid_Invoice_House.Refresh();
}

function PutCurrency(s, v) {
    if (clientId != null) {
        clientId.SetText(s);
        clientName.SetNumber(v);
        clientId = null;
        clientName = null;
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
        // PutAmt();
    }
}


// booking
function PopupTSList() {
    var tsInd = cmb_Hbl_Tranship.GetText();
    if (tsInd != "Y") {
        alert("This Job is not TS");
        return;
    }
    if (txtPod.GetText().length < 1) {
        alert("Please select port disc!");
        return;
    }
    popubCtr.SetHeaderText('Booking');
    popubCtr.SetContentUrl('ExportSchSelect.aspx?port=' + txtPod.GetText() + "&impNo=" + txtHouseNo.GetText());
    popubCtr.Show();
}
function PutBooking(schId) {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
    txtSchNo.SetText(schId);
    detailGrid.GetValuesOnCustomCallback('Booking', OnBookingCallback);
}
function OnBookingCallback(v) {
    alert(v);
    window.location = 'Air_ImportEdit.aspx?no=' + txtHouseNo.GetText();
}
function PopubBkg() {
    var expNo = txtBkgNo.GetText();
    window.open("../Export/ExportEdit.aspx?no=" + expNo);
}
//cancel booking
function CancelBkg() {
    detailGrid.GetValuesOnCustomCallback('CancelBkg', OnBookingCallback);
}
//close job
function OnCloseCallBack(v) {
    if (v == "Success") {
        alert("Action Success!");
        detailGrid.Refresh();
    }
    else if (v == "Billing")
        alert("Have Billing, Can't void!");
    else if (v == "Fail")
        alert("Action Fail,please try again!");
}

