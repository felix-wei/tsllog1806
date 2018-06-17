
function PrintManifest(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_Manifest&master=' + refNo + '&house=' + jobNo);
}
function PrintHaulier(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_Haulier&master=' + refNo + '&house=' + jobNo);
}
function PrintDOPermit_Carrier(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_PermitCarrier&master=' + refNo + '&house=' + jobNo);
}
function PrintLetter_Carrier(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_AuthLetterCarrier&master=' + refNo + '&house=' + jobNo);
}
function PrintPermitList_Carrier(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_PermitListCarrier&master=' + refNo + '&house=' + jobNo);
}
function PrintBatch_DO(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_BatchDO&master=' + refNo + '&house=' + jobNo);
}
function PrintTransshipBkg(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_Transship_Bkg&master=' + refNo + '&house=' + jobNo);
}
function PrintDOPermit_Nvocc(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_PermitNvocc&master=' + refNo + '&house=' + jobNo);
}
function PrintLetter_Nvocc(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_AuthLetterNvocc&master=' + refNo + '&house=' + jobNo);
}
function PrintPermitList_Nvocc(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_PermitListNvocc&master=' + refNo + '&house=' + jobNo);
}
function PrintBatch_Invoice(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_BatchInvoice&master=' + refNo + '&house=' + jobNo);
}
function PrintCoverPage(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIM_CoverPage&master=' + refNo + '&house=' + jobNo);
}
function PrintDO(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_DO&master=' + refNo + '&house=' + jobNo);
}
function PrintPreAdvise(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_PreAdvise&master=' + refNo + '&house=' + jobNo);
}
function PrintDN(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_DN&master=' + refNo + '&house=' + jobNo + '&oid=0');
}
function PrintDN1(refNo, jobNo,dnId) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_DN&master=' + refNo + '&house=' + jobNo + '&oid=' + dnId);
}
function PrintArrival(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_ArrivalNotice&master=' + refNo + '&house=' + jobNo);
}
function PrintArrival_AG(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_ArrivalNoticeAg&master=' + refNo + '&house=' + jobNo);
}
function PrintPL(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SI_PL&master=' + refNo + '&house=' + jobNo);
}
function PrintCertificate(oid) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_Certificate&master=' + oid + '&house=0');
}
function PrintImport(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_JobOrder_IMF&master=' + refNo + '&house=' + jobNo);
}
function PrintJobOrder(refNo, jobNo) {
    window.open('/ReportFreightSea/printview.aspx?document=SIH_JobOrder&master=' + refNo + '&house=' + jobNo);
}