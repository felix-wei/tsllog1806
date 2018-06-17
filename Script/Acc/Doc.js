function PrintPayable(invN, docType, mastType) {
    if (docType == "VO") {
        window.open('/ReportFreightSea/printview.aspx?document=Voucher&master=' + invN);
    }
    return false;
}
function PrintWh_invoice(doNo, docType) {
    if (docType == "IV") {
        window.open('/Modules/WareHouse/PrintView.aspx?document=IV&master=' + doNo + "&house=0");
    } else if (docType == "CN") {
        window.open('/Modules/WareHouse/PrintView.aspx?document=CN&master=' + doNo + "&house=0");
    } else if (docType == "DN") {
        window.open('/Modules/WareHouse/PrintView.aspx?document=DN&master=' + doNo + "&house=0");
    }
}
function PrintInvoiceGoup(invN, docType) {
    if (docType == "IV") {
        window.open('/ReportFreightSea/printview.aspx?document=InvoiceGroup&master=' + invN + '&docType=' + docType);
    }
}
function PrintInvoice(invN, docType) {
    if (docType == "IV") {
        window.open('/ReportFreightSea/printview.aspx?document=Invoice&master=' + invN + '&docType=' + docType);
    }   
    else if (docType == "IV1") {
        window.open('/ReportFreightSea/printview.aspx?document=Invoice1&master=' + invN + '&docType=' + docType);
    }
    else if (docType == "PL") {
        window.open('/ReportFreightSea/printview.aspx?document=Payable&master=' + invN + '&docType=' + docType);
    }
    else if (docType == "PL1") {
        window.open('/ReportFreightSea/printview.aspx?document=Payable1&master=' + invN + '&docType=' + docType);
    }
    else if (docType == "CN") {
        window.open('/ReportFreightSea/printview.aspx?document=CreditNote&master=' + invN + '&docType=' + docType);
    } else if (docType == "DN") {
        window.open('/ReportFreightSea/printview.aspx?document=DebitNote&master=' + invN + '&docType=' + docType);
    }
}
function PrintInvoiceA4(invN, docType, mastType) {
    if (docType == "IV") {
        window.open('/ReportFreightSea/printview.aspx?document=SimpleInvoice&master=' + invN);
    } else if (docType == "CN") {
        window.open('/ReportFreightSea/printview.aspx?document=SimpleCreditNote&master=' + invN);
    } else if (docType == "DN") {
        window.open('/ReportFreightSea/printview.aspx?document=SimpleDebitNote&master=' + invN);
    }
}
var clientId = null;
var clientName = null;
function PopupRefNo(txtId, txtName) {

    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Ref No');
    popubCtr.SetContentUrl('/SelectPage/SelectAllRefNo.aspx');
    popubCtr.Show();
}
function PopupJobNo(mastNo, mastType, jobNo) {
    clientId = jobNo;
    popubCtr.SetHeaderText('Job No');
    popubCtr.SetContentUrl('/SelectPage/SelectJobNo.aspx?MastRefNo=' + mastNo + '&' + 'MastType=' + mastType);
    popubCtr.Show();
}
function PutGst(gstType, gst) {
    if (gstType.GetText() == 'S')
        gst.SetNumber(0.07);
    else
        gst.SetNumber(0.00);
}