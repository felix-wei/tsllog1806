
function PopupUploadPhoto() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('../Upload.aspx?Type=E&Sn=' + txt_RefN.GetText());
    popubCtr.Show();
}
function AfterUploadPhoto() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
//costing calculate
function PutAmt() {

    var qty = parseFloat(spin_CostQty.GetText());
    var price = parseFloat(spin_CostPrice.GetText());
    var exRate = parseFloat(spin_CostExRate.GetText());
    var amt = parseFloat(qty * price * exRate).toFixed(2);
    spin_CostAmt.SetNumber(amt);
}
// change export ref
function PopupExpRef(txtId) {
    clientId = txtId;
    popubCtr.SetHeaderText('Export Ref List');
    popubCtr.SetContentUrl('ExportRefList.aspx');
    popubCtr.Show();
}
function PutRefNo(s) {
    if (clientId != null) {
        clientId.SetText(s);
        grid_Export.PerformCallback(s + ';' + txt_Hbl_ExportN.GetText());
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
        clientId = null;
    }
}
////////for dropdown list
function RowClickHandler(s, e) {
    SetLookupKeyValue(e.visibleIndex);
    DropDownEdit.HideDropDown();
}
function SetLookupKeyValue(rowIndex) {
    //HiddenField.Set("GridViewKeyValue", GridView.cpKeyValues[rowIndex]);
    DropDownEdit.SetText(GridView.cpKeyValues[rowIndex]);
    txt_AttCont.SetText(GridView.cpKeyValues[rowIndex]);
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
        PutAmt();
    }
}

// detail       
function Calc_det() {
    spin_totAmt_det.SetNumber(spin_amt1_det.GetNumber() + spin_amt2_det.GetNumber() + spin_amt3_det.GetNumber() + spin_amt4_det.GetNumber() + spin_amt5_det.GetNumber());
}
//close job
function OnCloseCallBack(v) {
    if (v == "Success") {
        alert("Success!");
        grid_ref.Refresh();
    }
    else if (v == "Billing")
        alert("Have Billing, Can't void!");
    else if (v == "Fail")
        alert("Fail,please try again!");
    else if (v == "NoMatch")
        alert("EST Amount is difference with Actaul amount,please check again");
}
//cancel OnReviewCallBack
function OnReviewCallBack(v) {
    if (v == "Success") {
        alert("Review Success!");
        grid_ref.Refresh();
    }
    else if (v == "Fail")
        alert("Review Fail,please try again!");
}

function AddTsBkg() {
    popubCtr1.SetHeaderText('T/S Booking');
    popubCtr1.SetContentUrl('ImportList.aspx?refNo=' + txt_RefN.GetText() + '&port=' + txt_Pod.GetText());
    popubCtr1.Show();
}
function AfterBooking() {
    popubCtr1.Hide();
    popubCtr1.SetContentUrl('about:blank');
    detailGrid.Refresh();
}
//house
function ShowHouse(jobNo) {
    window.location = "ExportEdit.aspx?masterNo=" + txt_RefN.GetText() + "&no=" + jobNo;
}