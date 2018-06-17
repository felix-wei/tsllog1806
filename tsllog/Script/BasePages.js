
function ClosePopupCtr() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function ClosePopupCtr1() {
    popubCtr1.Hide();
    popubCtr1.SetContentUrl('about:blank');
}

var clientId = null;
var clientName = null;
var contactId = null;
var emailId = null;
var telId = null;
var fax = null;
var mobile = null;
var crNoId = null;
var addressId = null;
var postCodeId = null;
function PutValue(s) {
    if (clientId != null) {
        clientId.SetText(s);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function PutValue(s, name) {
    if (clientId != null) {
        clientId.SetText(s);
    }
    if (clientName != null) {
        clientName.SetText(name);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function PutValue(s, name,contact,email) {
    if (clientId != null) {
        clientId.SetText(s);
    }
    if (clientName != null) {
        clientName.SetText(name);
    }
    if (contactId != null)
    {
        contactId.SetText(contact);
    }
    if (emailId != null) {
        emailId.SetText(email);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function PopupContact(txtId, txtName,partyId) {
    clientId = txtId;
    clientName = txtName;
    contactId = null;
    emailId = null;
    popubCtr.SetContentUrl('/SelectPage/SelectContact.aspx?partyId=' + partyId);
    popubCtr.SetHeaderText('Contact');
    popubCtr.Show();
}
//PARTY
function PopupParty(txtId, txtName, partyType) {
    clientId = txtId;
    clientName = txtName;

    popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=' + partyType);
    if (partyType == "A")
        popubCtr.SetHeaderText('Agent');
    else if (partyType == "C")
        popubCtr.SetHeaderText('Customer');
    else if (partyType == "V")
        popubCtr.SetHeaderText('Vendor');
    else
        popubCtr.SetHeaderText('Party');
    popubCtr.Show();
}
function PopupParty(txtId, txtName, partyType,txtContact,txtEmail) {
    clientId = txtId;
    clientName = txtName;
    contactId = txtContact;
    emailId = txtEmail;
    popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?partyType=' + partyType);
    if (partyType == "A")
        popubCtr.SetHeaderText('Agent');
    else if (partyType == "C")
        popubCtr.SetHeaderText('Customer');
    else if (partyType == "V")
        popubCtr.SetHeaderText('Vendor');
    else
        popubCtr.SetHeaderText('Party');
    popubCtr.Show();
}
function PopupParentParty(txtId, txtName, parentCode) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/PartyList.aspx?parentCode=' + parentCode);
    popubCtr.SetHeaderText('Party');
    popubCtr.Show();
}

function PopupPartyAdr(txtId, txtName, partyType) {
    clientId = txtId;
    clientName = txtName;
    if (partyType == "A")
        popubCtr.SetHeaderText('Agent');
    else if (partyType == "C")
        popubCtr.SetHeaderText('Customer');
    else if (partyType == "V")
        popubCtr.SetHeaderText('Vendor');
    else
        popubCtr.SetHeaderText('Party');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Adr.aspx?partyType=' + partyType);
    popubCtr.Show();
}

function PopupHaulier(txtName, crNo, fax, attention, partyType) {
    clientName = txtName;
    add1 = crNo;
    add2 = fax;
    add3 = attention;
    popubCtr.SetHeaderText('Haulier');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Haulier.aspx');
    popubCtr.Show();
}

function PopupHaulierAdr(txtName, partyType) {
    clientName = txtName;
    if (partyType == "A")
        popubCtr.SetHeaderText('Agent');
    else if (partyType == "C")
        popubCtr.SetHeaderText('Customer');
    else if (partyType == "V")
        popubCtr.SetHeaderText('Vendor');
    else
        popubCtr.SetHeaderText('Party');
    popubCtr.SetContentUrl('/SelectPage/PartyList_Haulier1.aspx?partyType=' + partyType);
    popubCtr.Show();
}


function PopupShipper(custId, name, a1, a2, a3, a4, nameAdd, partyType) {
    clientId = custId;
    clientName = name;
    add1 = a1;
    add2 = a2;
    add3 = a3;
    add4 = a4;
    clientAcCode = nameAdd;
    popubCtr.SetHeaderText('Party');
    popubCtr.SetContentUrl('/SelectPage/Party_Shipper.aspx?partyType=' + partyType);
    popubCtr.Show();
}
function PutShipper(shipperId, name, contact, tel, fax, email, nameAdd) {
    if (clientId != null) {
        clientId.SetText(shipperId);
    }
    if (clientName != null) {
        clientName.SetText(name);
    }
    if (add1 != null) {
        add1.SetText(contact);
    }
    if (add2 != null) {
        add2.SetText(tel);
    }
    if (add3 != null) {
        add3.SetText(fax);
    }
    if (add4 != null) {
        add4.SetText(email);
    }
    if (clientAcCode != null)
        clientAcCode.SetText(nameAdd);
    clientId = null;
    clientName = null;
    add1 = null;
    add2 = null;
    add3 = null;
    add4 = null;
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
// salesman
function PopupSalesman(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Salesman');
    popubCtr.SetContentUrl('/SelectPage/salesman.aspx?type=S');
    popubCtr.Show();
}
//Country
function PopupCountry(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Country');
    popubCtr.SetContentUrl('/SelectPage/CountryList.aspx');
    popubCtr.Show();
}
//City
function PopupCity(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('City');
    popubCtr.SetContentUrl('/SelectPage/CityList.aspx');
    popubCtr.Show();
}
// port
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
// charge code
function PopupChgCode(codeId, desId, mastType) {
    clientId = codeId;
    clientName = desId;
    popubCtr.SetHeaderText('Charge Code');
    popubCtr.SetContentUrl('/SelectPage/ChgCodeList.aspx?jobType=' + mastType);
    popubCtr.Show();
}

var unit = null;
var gstType = null;
var gstA = null;
var gstP = null;
var clientAcCode = null;
function PopupChgCode_Ar(codeId, desId, unitId, gstTypeId, gstPId, acCode, mastType) {
    clientId = codeId;
    clientName = desId;
    unit = unitId;
    gstType = gstTypeId;
    gstP = gstPId;
    clientAcCode = acCode;
    popubCtr.SetHeaderText('Charge Code');
    popubCtr.SetContentUrl('/SelectPage/ChgCodeList_Ar.aspx?jobType=' + mastType);
    popubCtr.Show();
}
function PopupChgCode_Ap(codeId, desId, unitId, gstTypeId, gstPId, acCode, mastType) {
    clientId = codeId;
    clientName = desId;
    unit = unitId;
    gstType = gstTypeId;
    gstP = gstPId;
    clientAcCode = acCode;
    popubCtr.SetHeaderText('Charge Code');
    popubCtr.SetContentUrl('/SelectPage/ChgCodeList_Ap.aspx?jobType=' + mastType);
    popubCtr.Show();
}

function PutChgCode(codeV, desV, unitV, gstTypeV, gstPV, acCode, acSource) {
    var len = arguments.length;
    if (2 == len) {
        if (clientId != null) {
            clientId.SetText(codeV);
        }
        if (clientName != null) {
            clientName.SetText(desV);
        }
        clientId = null;
        clientName = null;
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
    }
    else {
        if (clientId != null) {
            clientId.SetText(codeV);
        }
        if (clientName != null) {
            clientName.SetText(desV);
        }
        if (unit != null) {
            unit.SetText(unitV);
        }
        if (gstType != null) {
            gstType.SetText(gstTypeV);
        }
        if (gstP != null) {
            gstP.SetNumber(gstPV);
        }
        if (clientAcCode != null) {
            clientAcCode.SetText(acCode);
        }
        clientId = null;
        clientName = null;
        unit = null;
        gstType = null;
        gstA = null;
        gstP = null;

        clientAcCode = null;
        clientType = null;
        popubCtr.Hide();
        popubCtr.SetContentUrl('about:blank');
        PutBillingAmt();
    }
}

function PopupUom(codeId, typ) {
    clientId = codeId;
    popubCtr.SetHeaderText('UOM');
    popubCtr.SetContentUrl('/SelectPage/UomList.aspx?type=' + typ);
    popubCtr.Show();
}

//calculate
function Calculate(qtyV, priceV, exRateV, num) {
    var qty = parseFloat(qtyV);
    var price = parseFloat(priceV);
    var exRate = parseFloat(exRateV);
    if (exRate == 1)
        return FormatNumber(qty * price, num);
    else
        return FormatNumber(FormatNumber(qty * price, num) * exRate, num);
}
function Calc(qtyV, priceV, exRateV, num, totControl, otherAmtV) {
    var qty = parseFloat(qtyV);
    var price = parseFloat(priceV);
    var exRate = parseFloat(exRateV);
    var otherAmt = 0;
    if (otherAmtV != null) {
        otherAmt = parseFloat(otherAmtV);
    }
    if (exRate == 1)
        totControl.SetNumber(parseFloat(FormatNumber(qty * price, num)) + parseFloat(otherAmt));
    else
        totControl.SetNumber(parseFloat(FormatNumber(FormatNumber(qty * price, num) * exRate, num)) + parseFloat(otherAmt));
}
function Calc1(qtyV, priceV, exRateV, num, totControl, gstV) {
    var qty = parseFloat(qtyV);
    var price = parseFloat(priceV);
    var exRate = parseFloat(exRateV);
    var gst = parseFloat(gstV);
    var amt = FormatNumber(qty * price, num);
    var gstAmt = 0;
    if (gst > 0)
        gstAmt = FormatNumber(amt * gst, num)
    var docAmt = parseFloat(amt) + parseFloat(gstAmt);
    if (exRate == 1)
        totControl.SetNumber(docAmt);
    else
        totControl.SetNumber(FormatNumber(docAmt * exRate, num));
}
function PutBillingAmt() {
    Calc(spin_det_Qty.GetText(), spin_det_Price.GetText(), spin_det_GstP.GetText(), 2, spin_det_GstAmt, 0);
    Calc(spin_det_Qty.GetText(), spin_det_Price.GetText(), 1, 2, spin_det_DocAmt, spin_det_GstAmt.GetText());
    Calc(spin_det_DocAmt.GetText(), 1, spin_det_ExRate.GetText(), 2, spin_det_LocAmt, 0);
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
function PutQuote(quoteNo) {
    if (clientName != null) {
        clientName.SetText(quoteNo);
    }
    clientName = null;
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}






