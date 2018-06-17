
function PopupUploadPhoto() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('../Upload.aspx?Type=AE&Sn=' + txt_RefN.GetText());
    popubCtr.Show();
}
function AfterUploadPhoto() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}

function PopupAccountCustomer(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Account/Customer');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Adr.aspx?partyType=C');
    popubCtr.Show();
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
////////////////for dropdown list
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
//bookmark
function OnbookCallback(v) {
    alert(v);
}

// detail       
function Calc_det() {
    spin_totAmt_det.SetNumber(spin_amt1_det.GetNumber() + spin_amt2_det.GetNumber() + spin_amt3_det.GetNumber() + spin_amt4_det.GetNumber() + spin_amt5_det.GetNumber());
}


//shutout
function ChgVes1(schId, schNo) {
    if (isShutout) {// shut out
        detailGrid.GetValuesOnCustomCallback(schId + ':' + schNo + ':' + txtBkgLineN.GetText(), OnCallback);
    } else {// change vessel
        detailGrid.GetValuesOnCustomCallback(schId + ':' + schNo + ';' + txtBkgLineN.GetText(), OnCallback);
    }
    popubCtr.Hide();
    isShutout = null;
    popubCtr.SetContentUrl('about:blank');
}
var isShutout = false;
function Shutout(port) {
    isShutout = true;
    popubCtr.SetContentUrl("ExportRefSelect.aspx?PortId=" + port);
    popubCtr.SetHeaderText("Shedule");
    popubCtr.Show();
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
//close job
function OnCloseCallBack(v) {
    if (v == "Success") {
        alert("Action Success!");
        grid_ref.Refresh();
    }
    else if (v == "Billing")
        alert("Have Billing, Can't void!");
    else if (v == "Fail")
        alert("Action Fail,please try again!");
}

function ShowHouse(jobNo) {
    window.location = "Air_ExportEdit.aspx?masterNo=" + txt_RefN.GetText() + "&no=" + jobNo;
}