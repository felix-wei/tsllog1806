
//close job
function onDnCallback(v) {
    if (v == "Success") {
        grid_Dn.Refresh();
    }
}
function ShowHouse(refNo,jobNo) {
    window.location = "ImportEdit.aspx?masterNo=" + refNo + "&no=" + jobNo;
}
function PopupChgTemplate() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('/SelectPage/ChargeTemplate.aspx?refType=SI&Sn=' + txtRefNo.GetText() + '&jobNo=' + txtHouseNo.GetText());
    popubCtr.Show();
}

function OnChargeCallBack(v) {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
    grid.Refresh();
    grid = null;
}
function PopupUploadPhoto() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('../Upload.aspx?Type=I&Sn=' + txtRefNo.GetText() + '&jobNo=' + txtHouseNo.GetText());
    popubCtr.Show();
}
function AfterUploadPhoto() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
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
    else if (v == "NoMatch")
        alert("EST Amount is difference with Actaul amount,please check again");
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
    txt_contType.SetText(GridView.cpContType[rowIndex]);
}

// cust rate
function OnCallback(v) {
    Grid_Invoice_House.Refresh();
}
function PutAmt() {

    var qty = parseFloat(txt_Costing_Qty.GetText());
    var price = parseFloat(txt_Costing_Amt.GetText());
    var exRate = parseFloat(txt_Costing_Rate.GetText());
    var amt = parseFloat(qty * price * exRate).toFixed(2);
    txt_Costing_Cost.SetNumber(amt);
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
    window.location = 'ImportEdit.aspx?no=' + txtHouseNo.GetText();
}
function PopubBkg() {
    var expNo = txtBkgNo.GetText();
    window.open("../Export/ExportEdit.aspx?no=" + expNo);
}
//cancel booking
function CancelBkg() {
    detailGrid.GetValuesOnCustomCallback('CancelBkg', OnBookingCallback);
}

function AddImportCertificate(gridId) {
    grid = gridId;
    popubCtr1.SetHeaderText('Certificate');
    popubCtr1.SetContentUrl('/PagesFreight/Certificate/ImportCertificate.aspx?no=0&JobType=SI&RefN=' + txtRefNo.GetText() + '&JobN=' + txtHouseNo.GetText());
    popubCtr1.Show();
}
function ShowImportCertificate(gridId, invN) {
    grid = gridId;
    popubCtr1.SetHeaderText('Certificate');
    popubCtr1.SetContentUrl('/PagesFreight/Certificate/ImportCertificate.aspx?no=' + invN);
    popubCtr1.Show();
}
