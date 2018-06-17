function PrintLoadplan(schId) {
    window.open('/ReportFreightSea/PrintView.aspx?document=SEM_Loadplan&house=0&master=' + schId);
}

function PrintBkgConfirm(schId, bkgId) {
    window.open('/ReportFreightSea/PrintView.aspx?document=SEH_BkgConfirmation&master=' + schId + "&house=" + bkgId);
}
function PrintSo(schId, bkgId) {
    window.open('/ReportFreightSea/PrintView.aspx?document=SEH_ShippingOrder&master=' + schId + "&house=" + bkgId);
}
function PrintBL(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_BL&master=' + refNo + '&house=' + jobNo);
}
function PrintDraftBL(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_DraftBL&master=' + refNo + '&house=' + jobNo);
}
function PrintApprovedPermit(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_ApprovedPermit&master=' + refNo + '&house=' + jobNo);
}
function PrintHaulierSheet(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_Haulier&master=' + refNo + '&house=' + jobNo);
}
function PrintPermit_Carrier(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEM_PermitListCarrier&master=' + refNo + '&house=' + jobNo + '&oid=Carrier');
}
//function PrintPermit_Nvocc(refNo, jobNo) {
//    window.open('/ReportFreightSea/printview.aspx?document=SEM_PermitListNvocc&master=' + refNo + '&house=' + jobNo+'&oid=Nvocc');
//}
function PrintOceanBL_Carrier(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEM_OceanBLCarrier&master=' + refNo + '&house=' + jobNo);
}
function PrintOceanBL_Nvocc(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEM_OceanBLNvocc&master=' + refNo + '&house=' + jobNo);
}
function PrintHaulier(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEM_Haulier&master=' + refNo + '&house=' + jobNo);
}
function PrintPreAdvise(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEM_PreAdvise&master=' + refNo + '&house=' + jobNo);
}
function PrintPL(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SE_PL&master=' + refNo + '&house=' + jobNo);
}
function PrintBatchBL(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEM_BatchBL&master=' + refNo + '&house=' + jobNo);
}
function PrintBatchInvoice(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEM_BatchInvoice&master=' + refNo + '&house=' + jobNo);
}
function PrintCommercial(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_Commercial&master=' + refNo + '&house=' + jobNo);
}
function PrintPacking(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_Packing&master=' + refNo + '&house=' + jobNo);
}
function PrintShippingRequest(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_ShippingRequest&master=' + refNo + '&house=' + jobNo);
}
function PrintJobOrder(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_JobOrder&master=' + refNo + '&house=' + jobNo);
}
function PrintCertificate(oid) {//78
    window.open('/ReportFreightSea/printview.aspx?document=SEH_Certificate&master=' + oid + '&house=0');
}
function PrintJobOrder1(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEH_JobOrder_IMF&master=' + refNo + '&house=' + jobNo);
}
function PrintCargoManifest(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SEM_CargoManifest&master=' + refNo + '&house=' + jobNo);
}