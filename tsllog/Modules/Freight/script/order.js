function OnCallBack(v) {
    if (v != null) {
        grd_Det.CancelEdit();
    }
}
function OnClickCallBack(v) {
    if (v != null) {
        grd_Stock.Refresh();
    }
}
function OnExcelCallBack() {

}
function PopupExcel() {
    popubCtr.SetHeaderText('Import Cargo and Select a Excel/从Excel批量导入货物');
    popubCtr.SetContentUrl('/Modules/Freight/SelectPages/UploadCargo.aspx?no=' + txt_cargo_id.GetText());
    popubCtr.Show();
}
function GoList() {
    parent.navTab.openTab('订单列表', "/PagesCfs/XW/Dongji/OrderList.aspx", { title: '订单列表', fresh: false, external: true });
}
function AddNew(masterId) {
    parent.navTab.openTab('新单', "/PagesCfs/XW/Dongji/OrderEdit.aspx?no=" + masterId, { title: '新单', fresh: false, external: true });
}
function MultipleAdd(jobNo) {
    popubCtr.SetHeaderText('订单列表/Packing List Sample');
    popubCtr.SetContentUrl('../SelectPages/BookingOrder.aspx?sn=' + jobNo);
    popubCtr.Show();
}
function AfterPopubMultiple() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
    grd_Det.Refresh();
}
function AfterPopubMultiple_1() {
    popubCtr2.Hide();
    popubCtr2.SetContentUrl('about:blank');
    Grid_Invoice_Import.Refresh();
}
function CheckConsignee() {
    console.log(ckb_IsHold.GetChecked());
    if (ckb_IsHold.GetChecked()) {
        document.getElementById("receiver").style.display = "none";
        document.getElementById("contact").style.display = "none";
        document.getElementById("address").style.display = "none";
    } else {
        document.getElementById("receiver").style.display = "table-row";
        document.getElementById("contact").style.display = "table-row";
        document.getElementById("address").style.display = "table-row";
    }
}


function CheckResponsible() {
    var vm = cmb_Responsible.GetText();
    console.log(vm);
    if (vm == "公司") {
        cmb_Duty_Payment.SetText("税新加坡付");
        cmb_Duty_Payment.SetEnabled(false);
    }
    if (vm == "个人") {
        cmb_Duty_Payment.SetEnabled(true);
    }
}
function CheckIncoTerm() {
    var vm = cmb_Incoterm.GetText();
    console.log(vm);
    if (vm == "CIF" || vm == "CFR") {
        spin_MiscFee.SetText(0.00);
        spin_MiscFee.SetEnabled(false);
    }
    else {
        spin_MiscFee.SetEnabled(true);
    }
}
function CheckInput() {
    console.log(ckb_Input.GetChecked());
    if (ckb_Input.GetChecked()) {
        document.getElementById("txt_party").style.display = "block";
        document.getElementById("ddl_party").style.display = "none";
    } else {
        document.getElementById("ddl_party").style.display = "block";
        document.getElementById("txt_party").style.display = "none";
    }
}
function CheckPrepaidInd() {
    console.log(ckb_Prepaid_Ind.GetChecked());
    if (ckb_Prepaid_Ind.GetChecked()) {
        spin_Collect_Amount2.SetText(0.00);
        spin_Collect_Amount2.SetEnabled(false);
    } else {
        spin_Collect_Amount2.SetEnabled(true);
    }
}
window.onload = function () {
    CheckConsignee();
    CheckResponsible();
}
function RowClickHandlers(s, e) {
    DropDownEdit_Party.SetText(gridParty.cpName[e.visibleIndex]);
    txt_Party.SetText(gridParty.cpName[e.visibleIndex]);
    txt_Party1.SetText(gridParty.cpName[e.visibleIndex]);
    if (cmb_Responsible.GetText() == "个人") {
        txt_ConsigneeRemark.SetText(gridParty.cpCrNo[e.visibleIndex]);
        txt_ConsigneeRemark1.SetText(gridParty.cpCrNo[e.visibleIndex]);
    }
    if (cmb_Responsible.GetText() == "公司") {
        txt_ConsigneeEmail.SetText(gridParty.cpCrNo[e.visibleIndex]);
        txt_ConsigneeEmail1.SetText(gridParty.cpCrNo[e.visibleIndex]);
    }
    txt_Email1.SetText(gridParty.cpEmail1[e.visibleIndex]);
    txt_Email1_1.SetText(gridParty.cpEmail1[e.visibleIndex]);
    txt_Email2.SetText(gridParty.cpEmail2[e.visibleIndex]);
    txt_Email2_1.SetText(gridParty.cpEmail2[e.visibleIndex]);
    txt_Tel1.SetText(gridParty.cpTel1[e.visibleIndex]);
    txt_Tel1_1.SetText(gridParty.cpTel1[e.visibleIndex]);
    txt_Tel2.SetText(gridParty.cpTel2[e.visibleIndex]);
    txt_Tel2_1.SetText(gridParty.cpTel2[e.visibleIndex]);
    memo_Desc1.SetText(gridParty.cpAddress[e.visibleIndex]);
    memo_Desc1_1.SetText(gridParty.cpAddress[e.visibleIndex]);
    DropDownEdit_Party.HideDropDown();

}
function AfterUploadExcel() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
var loading = {
    show: function () {
        $("#div_tc").css("display", "block");
    },
    hide: function () {
        $("#div_tc").css("display", "none");
    }
}
$(function () {
    loading.hide();
})
var config = {
    timeout: 0,
    gridview: 'grd_Det',
}
function save_order() {
    //loading.show();
    var res = cmb_Responsible.GetText();
    var ic = txt_ConsigneeRemark.GetText();
    var ic1 = txt_ConsigneeRemark1.GetText();
    var uen = txt_ConsigneeEmail.GetText();
    var uen1 = txt_ConsigneeEmail1.GetText();
    var email1 = txt_Email1.GetText();
    var email1_1 = txt_Email1_1.GetText();
    var email2 = txt_Email2.GetText();
    var email2_1 = txt_Email2_1.GetText();
    var tel1 = txt_Tel1.GetText();
    var tel1_1 = txt_Tel1_1.GetText();
    var tel2 = txt_Tel2.GetText();
    var tel2_1 = txt_Tel2_1.GetText();
    var mobile1 = txt_Mobile1.GetText();
    var mobile1_1 = txt_Mobile1_1.GetText();
    var mobile2 = txt_Mobile2.GetText();
    var mobile2_1 = txt_Mobile2_1.GetText();
    var address = memo_Desc1.GetText();
    var address_1 = memo_Desc1_1.GetText();
    //setTimeout(function () {
    if (email1 != email1_1 || email2 != email2_1 || tel1 != tel1_1 || tel2 != tel2_1 ||
        mobile1 != mobile1_1 || mobile2 != mobile2_1 || address != address_1) {
        grd_Det.GetValuesOnCustomCallback('Vali', save_order_callBack);

    }
    else if (ic != ic1 || uen != uen1) {
        console.log(ic);
        console.log(ic1);
        console.log(uen);
        console.log(uen1);
        grd_Det.GetValuesOnCustomCallback('ValiUen', save_order_callBack);
    }
    else {
        console.log('Save==============');
        grd_Det.GetValuesOnCustomCallback('Save', OnCallBack);
    }
    //}, config.timeout);
}
function save_order_callBack(v) {
    if (v == "OK") {
        if (confirm("Confirm Update Consignee Information?")) {
            grd_Det.GetValuesOnCustomCallback('UpdateParty', OnCallBack);
        }
    }
    else {
        grd_Det.GetValuesOnCustomCallback('Save', OnCallBack);
    }
}
function update_inline(rowIndex) {
    console.log(rowIndex);
    loading.show();
    setTimeout(function () {
        grd_Stock.GetValuesOnCustomCallback('UpdateInline_' + rowIndex, update_inline_callBack);
    }, config.timeout);
}
function update_inline_callBack(v) {
    var miscFee = parseFloat(spin_MiscFee.GetText());
    var stockAmt = parseFloat(v);
    var total = FormatNumber(miscFee + stockAmt, 3);
    console.log(total);
    lbl_total.SetText(total);
    grd_Stock.CancelEdit();
}