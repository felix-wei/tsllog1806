function EdiAr(docNo) {
    window.open('/Edi/Edi_Ar.aspx?DocNo='+docNo);
}
function EdiSeaRef(refNo,refType) {
    window.open('/Edi/Edi_SeaRef.aspx?RefNo=' + refNo + '&RefType=' + refType);
}
function EdiAirRef(refNo, refType) {
    window.open('/Edi/Edi_AirRef.aspx?RefNo=' + refNo + '&RefType=' + refType);
}