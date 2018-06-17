
//house
function ShowHouse(refNo,jobNo) {
    window.location = "ExportEdit.aspx?masterNo=" + refNo + "&no=" + jobNo;
}

// change export ref
function PopupExpRef(txtId) {
    clientId = txtId;
    popubCtr.SetHeaderText('Export Ref List');
    popubCtr.SetContentUrl('ExportRefSelect.aspx');
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
//chg ref
function OnChgRefCallback(v) {
    if (v == "Success") {
        alert("Change Ref Success!");
        detailGrid.Refresh();
    }
    else if (v == "Fail")
        alert("Change Ref Fail,please try again!");

}
//cancel bkg
function OnCancelCallback(v) {
    if (v == "Success") {
        alert("Success!");
        detailGrid.Refresh();
    }
    else if (v == "Billing")
        alert("Have Billing, Can't void!");
    else if (v == "Fail")
        alert("Fail,please try again!");
}
//Transfer bkg
function OnTransferCallback(v) {
    if (v == "Success") {
        grid_Mkgs.Refresh();
    }
}
//request call back
function OnRequestCallback(v) {
    if (v == "Success") {
        grid_shipping.Refresh();
    }
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
function PutDetAmt() {
    var qty = parseFloat(spin_detQty.GetText());
    var price = parseFloat(spin_detPrice.GetText());
    var amt = qty * price;
    spin_detAmt.SetNumber(amt.toFixed(2));
}

function PopupUploadPhoto() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('../Upload.aspx?Type=E&Sn=' + txt_Hbl_RefN.GetText() + '&jobNo=' + txtHouseNo.GetText());
    popubCtr.Show();
}

function PopupQuote(txtId) {
    clientName = txtId;
    popubCtr.SetContentUrl('/SelectPage/QuoteList.aspx');
    popubCtr.SetHeaderText('Quote');
    popubCtr.Show();
}

function AddExportCertificate(gridId) {
    grid = gridId;
    popubCtr1.SetHeaderText('Certificate');
    popubCtr1.SetContentUrl('/PagesFreight/Certificate/ExportCertificate.aspx?no=0&JobType=SE&RefN=' + txtRefNo.GetText() + '&JobN=' + txtHouseNo.GetText());
    popubCtr1.Show();
}
function ShowExportCertificate(gridId, invN) {
    grid = gridId;
    popubCtr1.SetHeaderText('Certificate');
    popubCtr1.SetContentUrl('/PagesFreight/Certificate/ExportCertificate.aspx?no=' + invN);
    popubCtr1.Show();
}