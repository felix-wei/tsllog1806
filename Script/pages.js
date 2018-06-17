
function ClosePopupCtr() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function ClosePopupCtr1() {
    popubCtr1.Hide();
    popubCtr1.SetContentUrl('about:blank');
}

function PopupPort(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Port');
    popubCtr.SetContentUrl('/SelectPage/PortList.aspx?type=S');
    popubCtr.Show();
}
function PopupAirPort(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Port');
    popubCtr.SetContentUrl('/SelectPage/PortList.aspx?type=A');
    popubCtr.Show();
}
function PopupAgent(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=A');
    popubCtr.SetHeaderText('Agent');
    popubCtr.Show();
}
function PopupPartyInfo() {
    popubCtr1.SetHeaderText('Party Info');
    popubCtr1.SetContentUrl('/SelectPage/PartyInfo.aspx?partyId=' + btn_ClientId.GetText());
    popubCtr1.Show();
}
function PopupCust(txtId, txtName, txtType, txtAcCode) {
    var len = arguments.length;
    if (2 == len) {
        clientId = txtId;
        clientName = txtName;
        popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=C');
        popubCtr.SetHeaderText('Customer');
        popubCtr.Show();
    }
    else {
        clientId = txtId;
        clientName = txtName;
        clientType = txtType;
        clientAcCode = txtAcCode;
        popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=C');
        popubCtr.SetHeaderText('Party To');
        popubCtr.Show();
    }
}
function PopupIncoTerm(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('IncoTerm');
    popubCtr.SetContentUrl('/SelectPage/SelectIncoTerm.aspx');
    popubCtr.Show();
}
function PopupParty(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=C');
    popubCtr.SetHeaderText('Party');
    popubCtr.Show();
}
function PopupCTM_MasterData(txtCode, txtName,type) {
    clientId = txtCode;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/CTM_MasterDataList.aspx?type='+type);
    popubCtr.SetHeaderText(type);
    popubCtr.Show();
}
function PopupContainer(txtCode, txtType) {
    clientId = txtCode;
    clientName = txtType;
    popubCtr.SetContentUrl('/SelectPage/ContainerList.aspx');
    popubCtr.SetHeaderText('Container');
    popubCtr.Show();
}

function PopupVendor(txtId, txtName, txtType, txtAcCode) {
    var len = arguments.length;
    if (2 == len) {
        clientId = txtId;
        clientName = txtName;
        popubCtr.SetHeaderText('Vendor');
        popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=V');
        popubCtr.Show();
    }
    else {
        clientId = txtId;
        clientName = txtName;
        clientType = txtType;
        clientAcCode = txtAcCode;
        popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=V');
        popubCtr.SetHeaderText('Party To');
        popubCtr.Show();
    }
}

function PopupAgentAdr(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Agent');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Adr.aspx?partyType=A');
    popubCtr.Show();
}

function PopupCustAdr(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/PartyList_Adr.aspx?partyType=C');
    popubCtr.SetHeaderText('Customer');
    popubCtr.Show();
}

function PopupVendorAdr(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Vendor');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Adr.aspx?partyType=V');
    popubCtr.Show();
}
function PopupCurrency(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Currency');
    popubCtr.SetContentUrl('/SelectPage/CurrencyList.aspx');
    popubCtr.Show();
}

function PopupHaulier(txtName, crNo, attention) {
    clientName = txtName;
    add1 = crNo;
    add2 = attention;
    popubCtr.SetHeaderText('Vendor');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Haulier.aspx');
    popubCtr.Show();
}

function PopupHaulier1(txtName, crNo, attention) {
    clientName = txtName;
    add1 = crNo;
    add2 = attention;
    popubCtr.SetHeaderText('Haulier');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Haulier1.aspx?partyType=CV');
    popubCtr.Show();
}
function PopupHaulierVendor(txtName, crNo, attention) {
    clientName = txtName;
    add1 = crNo;
    add2 = attention;
    popubCtr.SetHeaderText('Vendor');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Haulier1.aspx?partyType=V');
    popubCtr.Show();
}

function PopupChart(txtId, txtName, txtSource) {
    clientId = txtId;
    clientName = txtName;
    clientType = txtSource;
    popubCtr.SetContentUrl('/SelectPage/ChartOfAccount.aspx');
    popubCtr.SetHeaderText('CharList');
    popubCtr.Show();
}
function PopupAddress(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/SelectAddress.aspx');
    popubCtr.SetHeaderText('Address List');
    popubCtr.Show();
}
function PopupAddress_byParty(txtId, txtName,party) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/SelectAddress.aspx?party='+party);
    popubCtr.SetHeaderText('Address List');
    popubCtr.Show();
}

function PopupConsignee(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Consignee');
    popubCtr.SetContentUrl('../SelectPage/AgentList.aspx');
    popubCtr.Show();
}


//function PopupChgCode_Ar(codeId, desId) {
//    clientId = codeId;
//    clientName = desId;
//    popubCtr.SetHeaderText('Charge Code');
//    popubCtr.SetContentUrl('../SelectPage/ChgCodeList_Ar.aspx?jobType=I');
//    popubCtr.Show();
//}

//var unit = null;
//var gstType = null;
//var gstA = null;
//var gstP = null;
//function PopupChgCode(mastType, codeId, desId, unitId, gstTypeId, gstPId, acCode) {
//    clientId = codeId;
//    clientName = desId;
//    unit = unitId;
//    gstType = gstTypeId;
//    gstP = gstPId;
//    clientAcCode = acCode;
//    popubCtr.SetHeaderText('Charge Code');
//    popubCtr.SetContentUrl('../SelectPage/ChgCodeList_Ap.aspx?jobType=' + mastType);
//    popubCtr.Show();
//}

var unit = null;
var gstType = null;
var gstA = null;
var gstP = null;
var clientAcCode = null;
function PopupChgCode(codeId, desId, unitId, gstTypeId, gstPId, acCode, mastType) {
    var len = arguments.length;
    if (2 == len) {
        clientId = codeId;
        clientName = desId;
        popubCtr.SetHeaderText('Charge Code');
        popubCtr.SetContentUrl('/SelectPage/ChgCodeList.aspx?jobType=I');
        popubCtr.Show();
    }
    else {
        clientId = codeId;
        clientName = desId;
        unit = unitId;
        gstType = gstTypeId;
        gstP = gstPId;
        clientAcCode = acCode;
        popubCtr.SetHeaderText('Charge Code');
        popubCtr.SetContentUrl('/SelectPage/ChgCodeList.aspx?jobType=' + mastType);
        popubCtr.Show();
    }
}


var clientId = null;
var clientName = null;
function PutValue(s, name) {
    if (clientId != null) {
        clientId.SetText(s);
    }
    if (clientName != null) {
        if (name) {
            name = name.replace("@&dy&@", "'");
        }
        clientName.SetText(name);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}

var add1 = null;
var add2 = null;
var add3 = null;
function PutValue6(name, a1) {
    if (clientName != null) {
        clientName.SetText(name);
    }
    if (add1 != null) {
        add1.SetText(a1);
    }
    clientName = null;
    add1 = null;
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}

function PutHaulier(name, crNo, fax, attention) {
    if (clientName != null) {
        clientName.SetText(name);
    }
    if (add1 != null) {
        add1.SetText(crNo);
    }
    if (add2 != null) {
        add2.SetText(fax);
    }
    if (add3 != null) {
        add3.SetText(attention);
    }
    clientName = null;
    add1 = null;
    add2 = null;
    add3 = null;
    add4 = null;
    add5 = null;
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function PutHaulier1(name, a1, a2, tel) {
    var len = arguments.length;
    if (1 == len) {
        if (clientName != null) {
            clientName.SetText(name);
        }
        clientName = null;
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
    }
    else {
        if (clientName != null) {
            clientName.SetText(name);
        }
        if (add1 != null) {
            add1.SetText(a1);
        }
        if (add2 != null) {
            add2.SetText(a2 + " Tel" + tel);
        }
        clientName = null;
        add1 = null;
        add2 = null;
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
    }
}


function PutCurrency(s, v) {
    if (clientId != null) {
        clientId.SetText(s);
    }
    if (clientName != null) {
        clientName.SetNumber(v);
    }
        clientId = null;
        clientName = null;
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
}


function PutCurrency_Costing(s, v) {
    if (clientId != null) {
        clientId.SetText(s);
    }
    if (clientName != null) {
        clientName.SetNumber(v);
    }
        clientId = null;
        clientName = null;
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
        PutAmt();
}


function PopupGeneralCode(txtId) {
    clientId = txtId;
    clientName = null;
    popubCtr.SetHeaderText('General Code');
    popubCtr.SetContentUrl('/SelectPage/GenCodeList.aspx');
    popubCtr.Show();
}

function PopupBank(codeId, desId) {
    clientId = codeId;
    clientName = desId;
    popubCtr.SetHeaderText('Chart Of Account-Bank');
    popubCtr.SetContentUrl('/SelectPage/ChartOfAccount_bank.aspx');
    popubCtr.Show();
}
function FormatNumber(srcStr, nAfterDot) {
    var srcStr, nAfterDot;
    var resultStr, nTen;
    srcStr = "" + srcStr + "";
    strLen = srcStr.length;
    dotPos = srcStr.indexOf(".", 0);
    if (dotPos == -1) {
        resultStr = srcStr + ".";
        for (i = 0; i < nAfterDot; i++) {
            resultStr = resultStr + "0";
        }
    }
    else {
        if ((strLen - dotPos - 1) >= nAfterDot) {
            nAfter = dotPos + nAfterDot + 1;
            nTen = 1;
            for (j = 0; j < nAfterDot; j++) {
                nTen = nTen * 10;
            }
            resultStr = Math.round(parseFloat(srcStr) * nTen) / nTen;
        }
        else {
            resultStr = srcStr;
            for (i = 0; i < (nAfterDot - strLen + dotPos + 1) ; i++) {
                resultStr = resultStr + "0";
            }

        }
    }
    return resultStr;

}



function PutValue1(s, name,type) {
    if (clientId != null) {
        clientId.SetText(s);
    }
    if (clientName != null) {
        clientName.SetText(name);
    }
    if (clientType != null) {
        clientType.SetText(type);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}








