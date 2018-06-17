var grid = null;

function AddInvoice(gridId, JobType, refNo, jobNo) {
    grid = gridId;
    popubCtr1.SetHeaderText('Invoice');
    popubCtr1.SetContentUrl('/OpsAccount/ArInvoiceEdit.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo);
    //popubCtr1.SetContentUrl('../Account/QuoteList.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo);
    popubCtr1.Show();
}
function AddInvoiceNew(gridId, JobType, refNo, jobNo) {
    grid = gridId;
    popubCtr1.SetHeaderText('Invoice');
    popubCtr1.SetContentUrl('/OpsAccount/QuoteList_new.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo);
    popubCtr1.Show();
}
function AddPayable(gridId, JobType, refNo, jobNo) {
    grid = gridId;
    popubCtr1.SetHeaderText('Payable');
    popubCtr1.SetContentUrl('/OpsAccount/ApPayableEdit.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo);
    popubCtr1.Show();
}

function AddVoucher(gridId, JobType, refNo, jobNo) {
    grid = gridId;
    popubCtr1.SetHeaderText('Voucher');
    popubCtr1.SetContentUrl('/OpsAccount/ApVoucherEdit.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo);
    popubCtr1.Show();
}
function AddCn(gridId, JobType, refNo, jobNo) {
    grid = gridId;
    popubCtr1.SetHeaderText('Credit Note');
    popubCtr1.SetContentUrl('/OpsAccount/ArCnEdit.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo);
    popubCtr1.Show();
}
function AddDn(gridId, JobType, refNo, jobNo) {
    grid = gridId;
    popubCtr1.SetHeaderText('Debit Note');
    popubCtr1.SetContentUrl('/OpsAccount/ArDnEdit.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo);
    popubCtr1.Show();
}


//bill
function ShowPayable(gridId, invN, docType) {
    grid = gridId;
    if (docType == "VO") {
        popubCtr1.SetHeaderText('Voucher');
        popubCtr1.SetContentUrl('/OpsAccount/ApVoucherEdit.aspx?no=' + invN);
    } else {
        popubCtr1.SetHeaderText('Payable');
        popubCtr1.SetContentUrl('/OpsAccount/ApPayableEdit.aspx?no=' + invN);
    }
    popubCtr1.Show();
}

function ShowInvoice(gridId, invN, docType) {
    grid = gridId;
    if (docType == "IV") {
        popubCtr1.SetHeaderText('Invoice');
        popubCtr1.SetContentUrl('/OpsAccount/ArInvoiceEdit.aspx?no=' + invN);
    } else if (docType == "CN") {
        popubCtr1.SetHeaderText('Credit Note');
        popubCtr1.SetContentUrl('/OpsAccount/ArCNEdit.aspx?no=' + invN);
    } else if (docType == "DN") {
        popubCtr1.SetHeaderText('Debit Note');
        popubCtr1.SetContentUrl('/OpsAccount/ArDnEdit.aspx?no=' + invN);
    }
    popubCtr1.Show();
}
