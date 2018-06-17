var clientId = null;
var clientName = null;
var add1 = null;
var add2 = null;
var add3 = null;
var add4 = null;
var add5 = null;
var add6 = null;
var add7 = null;
var add8 = null;
var add9 = null;
var add10 = null;
var add11 = null;
var add12 = null;
var add13 = null;
var add14 = null;
var add15 = null;
var add16 = null;
var add17 = null;
var add18 = null;
function PopupParty(id, name, contact, tel, country,city,zipCode, address, partyType) {
    clientId = id;
    clientName = name;
    add1 = contact;
    add2 = tel;
    add3 = country;
    add4 = city;
    add5 = zipCode;
    add6 = address;
    if (partyType == "A")
        popubCtr.SetHeaderText('Agent');
    else if (partyType == "C")
        popubCtr.SetHeaderText('Customer');
    else if (partyType == "V")
        popubCtr.SetHeaderText('Vendor');
    else
        popubCtr.SetHeaderText('Party');

    popubCtr.SetHeaderText('Party');
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/PartyList.aspx?partyType=' + partyType);
    popubCtr.Show();
}
function PopupParty(id, name, contact, tel, country, city, zipCode, address, partyType, group) {
    clientId = id;
    clientName = name;
    add1 = contact;
    add2 = tel;
    add3 = country;
    add4 = city;
    add5 = zipCode;
    add6 = address;
    add7 = group;
    if (partyType == "A")
        popubCtr.SetHeaderText('Agent');
    else if (partyType == "C")
        popubCtr.SetHeaderText('Customer');
    else if (partyType == "V")
        popubCtr.SetHeaderText('Vendor');
    else
        popubCtr.SetHeaderText('Party');

    popubCtr.SetHeaderText('Party');
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/PartyList.aspx?partyType=' + partyType +'&groupId='+group);
    popubCtr.Show();
}
function PopupBlanketOrder(id,name,type) {
    clientId = id;
    clientName = name;
    add3 = type;
    popubCtr.SetHeaderText('Blanket Summary');
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectBlanketOrder.aspx?type=' + type);
    popubCtr.Show();
}
function PopupParty(id, name, contact, tel, fax, email, zipCode, address, country, city, partyType) {
    clientId = id;
    clientName = name;
    add1 = contact;
    add2 = tel;
    add3 = fax;
    add4 = email;
    add5 = zipCode;
    add6 = address;
    add7 = country;
    add8 = city;
    if (partyType == "A")
        popubCtr.SetHeaderText('Agent');
    else if (partyType == "C")
        popubCtr.SetHeaderText('Customer');
    else if (partyType == "V")
        popubCtr.SetHeaderText('Supplier');
    else
        popubCtr.SetHeaderText('Party');

    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectParty.aspx?partyType=' + partyType);
    popubCtr.Show();
}
function PopupSalesOrderParty(id, name, contact, tel, country, city, zipCode, address, address1, salesman, partyType) {
    clientId = id;
    clientName = name;
    add1 = contact;
    add2 = tel;
    add3 = country;
    add4 = city;
    add5 = zipCode;
    add6 = address;
    add7 = address1;
    add8 = salesman;
    if (partyType == "A")
        popubCtr.SetHeaderText('Agent');
    else if (partyType == "C")
        popubCtr.SetHeaderText('Customer');
    else if (partyType == "V")
        popubCtr.SetHeaderText('Supplier');
    else
        popubCtr.SetHeaderText('Party');

    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectSalesOrderParty.aspx?partyType=' + partyType);
    popubCtr.Show();
}
function PutParty(id, name, contact, tel, country, city, zipCode, address) {
    if (clientId != null) {
        clientId.SetText(id);
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
        add3.SetText(country);
    }
    if (add4 != null) {
        add4.SetText(city);
    }
    if (add5 != null) {
        add5.SetText(zipCode);
    }
    if (add6 != null) {
        add6.SetText(address);
    }
    clientId = null;
    clientName = null;
    add1 = null;
    add2 = null;
    add3 = null;
    add4 = null;
    add5 = null;
    add6 = null;
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function PutParty(id, name, contact, tel, fax, email, zipCode, address,country, city) {
    if (clientId != null) {
        clientId.SetText(id);
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
    if (add5 != null) {
        add5.SetText(zipCode);
    }
    if (add6 != null) {
        add6.SetText(address);
    }
    if (add7 != null) {
        //add7.SetText(country);
    }
    if(add8!=null){
       // add8.SetText(city);
    }
    clientId = null;
    clientName = null;
    add1 = null;
    add2 = null;
    add3 = null;
    add4 = null;
    add5 = null;
    add6 = null;
    add7 = null;
    add8 = null;
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}

function PopupProduct(_sku, _des, _uom1, _uom2, _uom3, _uom4, _qtyWhole, _qtyLoose, _qtyBase, _att1, _att2, _att3, _att4, _att5, _att6, _att7, _att8, _att9, _att10, partyId,type) {
    clientId = _sku;
    clientName = _des;
    add1 = _uom1;
    add2 = _uom2;
    add3 = _uom3;
    add4 = _uom4;
    add5 = _qtyWhole;
    add6 = _qtyLoose;
    add7 = _qtyBase;
    add8 = _att1;
    add9 = _att2;
    add10 = _att3;
    add11 = _att4;
    add12 = _att5;
    add13 = _att6;
    add14 = _att7;
    add15 = _att8;
    add16 = _att9;
    add17 = _att10;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectProduct.aspx?PartyId=' + partyId+'&type='+type);
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}

function PopupProduct_Po(_sku, _des, _uom1, _uom2, _uom3, _uom4, _qtyWhole, _qtyLoose, _qtyBase, _att1, _att2, _att3, _att4, _att5, _att6, _att7, _att8, _att9, _att10) {
    clientId = _sku;
    clientName = _des;
    add1 = _uom1;
    add2 = _uom2;
    add3 = _uom3;
    add4 = _uom4;
    add5 = _qtyWhole;
    add6 = _qtyLoose;
    add7 = _qtyBase;
    add8 = _att1;
    add9 = _att2;
    add10 = _att3;
    add11 = _att4;
    add12 = _att5;
    add13 = _att6;
    add14 = _att7;
    add15 = _att8;
    add16 = _att9;
    add17 = _att10;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectProduct.aspx?type=PO');
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}

function PopupProduct_so(_sku, _des, _uom1, _uom2, _uom3, _uom4, _qtyWhole, _qtyLoose, _qtyBase, _att1, _att2, _att3, _att4, _att5, _att6, _att7, _att8, _att9, _att10) {
    clientId = _sku;
    clientName = _des;
    add1 = _uom1;
    add2 = _uom2;
    add3 = _uom3;
    add4 = _uom4;
    add5 = _qtyWhole;
    add6 = _qtyLoose;
    add7 = _qtyBase;
    add8 = _att1;
    add9 = _att2;
    add10 = _att3;
    add11 = _att4;
    add12 = _att5;
    add13 = _att6;
    add14 = _att7;
    add15 = _att8;
    add16 = _att9;
    add17 = _att10;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectProduct.aspx?type=SO');
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
function PutSku(v) {
    if (clientId != null) clientId.SetText(v[0]);
    if (clientName != null) clientName.SetText(v[1]);
    if (add1 != null) add1.SetText(v[2]);
    if (add2 != null) add2.SetText(v[3]);
    if (add3 != null) add3.SetText(v[4]);
    if (add4 != null) add4.SetText(v[5]);
    if (add5 != null) add5.SetText(v[6]);
    if (add6 != null) add6.SetText(v[7]);
    if (add7 != null) add7.SetText(v[8]);
    if (add8 != null) add8.SetText(v[9]);
    if (add9 != null) add9.SetText(v[10]);
    if (add10 != null) add10.SetText(v[11]);
    if (add11 != null) add11.SetText(v[12]);
    if (add12 != null) add12.SetText(v[13]);
    if (add13 != null) add13.SetText(v[14]);
    if (add14 != null) add14.SetText(v[15]);
    if (add15 != null) add15.SetText(v[16]);
    if (add16 != null) add16.SetText(v[17]);
    if (add17 != null) add17.SetText(v[18]);
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function PopupNewParty(txtId, txtName,type) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/PartyList.aspx');
    popubCtr.SetHeaderText(type);
    popubCtr.Show();
}
function PopupCarrier(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/SelectPage/PartyList.aspx');
    popubCtr.SetHeaderText('Carrier');
    popubCtr.Show();
}


function PopupWh(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/WareHouseList.aspx');
    popubCtr.SetHeaderText('WareHouse');
    popubCtr.Show();
}


function PopupLocation(txtId, txtName,type) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Unit&type='+type);
    popubCtr.SetHeaderText('Location');
    popubCtr.Show();
}
function PopupProductContract(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectProductToContract.aspx?partyId=' + txt_PartyId.GetText());
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
function PopupAllProduct(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectProduct.aspx?PartyId=' + txt_PartyId.GetText());
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
function PopupProductToWareHouse() {
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/TransferProduct.aspx?no=' + txt_DoNo.GetText());
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
function PopupProductPO(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/ProductList.aspx?type=PO');
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
function PopupProductSO(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectProdutFromReceipt.aspx?type=SO');
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
function PopupZoneCode(txtId, whCode) {
    clientId = txtId;
    popubCtr.SetHeaderText('Zone Code');
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Zone&wh=' + whCode);
    popubCtr.Show();
}
function PopupZoneCodeFromWh(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Zone Code');
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Zone&Wh=' + txt_WarehouseCode.GetText());
    popubCtr.Show();
}
function PopupStoreCodeFromWh(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetHeaderText('Zone Code');
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Store&Wh=' + txt_WarehouseCode.GetText() + '&Zone=' + btn_ZoneCode.GetText());
    popubCtr.Show();
}
function PopupStoreCode(txtId, whCode) {
    clientId = txtId;
    popubCtr.SetHeaderText('Store Code');
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Store&Wh=' + whCode);
    popubCtr.Show();
}
function PopupUploadPhoto() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('/warehouse/Upload.aspx?Type=PO&Sn=' + txt_RefNo.GetText());
    popubCtr.Show();
}
function PopupUploadPhotoPO() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('/warehouse/Upload.aspx?Type=PO&Sn=' + txt_RefNo.GetText());
    popubCtr.Show();
}
function PopupUploadPhotoPOR() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('/warehouse/Upload.aspx?Type=POR&Sn=' + txt_PoNo.GetText());
    popubCtr.Show();
}
function PopupUploadPhotoSO() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('/warehouse/Upload.aspx?Type=SO&Sn=' + txt_SoNo.GetText());
    popubCtr.Show();
}
function PopupUploadPhotoSOR() {
    popubCtr.SetHeaderText('Upload Attachment');
    popubCtr.SetContentUrl('/warehouse/Upload.aspx?Type=SOR&Sn=' + txt_RoNo.GetText());
    popubCtr.Show();
}

function AfterUploadPhoto() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}

function AddInvoice_SOR(gridId) {
    grid = gridId;
    popubCtr1.SetHeaderText('Invoice');
    //popubCtr1.SetContentUrl('../Account/ArInvoiceEdit.aspx?no=0&JobType=I&RefN=' + txtRefNo.GetText() + '&JobN=' + txtHouseNo.GetText());
    popubCtr1.SetContentUrl('/OpsAccount/QuoteList.aspx?no=0&JobType=SOR&RefN=' + txt_RoNo.GetText());
    popubCtr1.Show();
}
function AddCn_SOR(gridId) {
    grid = gridId;
    popubCtr1.SetHeaderText('Credit Note');
    popubCtr1.SetContentUrl('/OpsAccount/ArCnEdit.aspx?no=0&JobType=SOR&RefN=' + txt_RoNo.GetText());
    popubCtr1.Show();
}
function AddDn_SOR(gridId) {
    grid = gridId;
    popubCtr1.SetHeaderText('Debit Note');
    popubCtr1.SetContentUrl('/OpsAccount/ArDnEdit.aspx?no=0&JobType=SOR&RefN=' + txt_RoNo.GetText());
    popubCtr1.Show();
}
function AddPayable_S(gridId) {
    Add_Payable(gridId, 'SOR', txt_RoNo.GetText(), '0')
}

function AddVoucher_S(gridId) {
    Add_Voucher(gridId, 'SOR', txt_RoNo.GetText(), '0')
}

function EndCallBack() {
    if (detailGrid.cpSuccess != "") {
        alert(detailGrid.cpSuccess);
        detailGrid.Refresh();
    }
}
//close job
function OnCloseCallBack(v) {
    if (v == "Success") {
        alert("Action Success!");
        detailGrid.Refresh();
    }
    if (v == "Save") {
        detailGrid.Refresh();
    }
    else if (v == "Billing")
        alert("Have Billing, Can't void!");
    else if (v == "BalQty")
        alert("Not Receiving, Can't void!");
    else if (v == "Fail")
        alert("Action Fail,please try again!");
    else if (v == "NoMatch")
        alert("EST Amount is difference with Actaul amount,please check again");
    else if (v == "NotClose") {
        alert("Have Not Delivered, Can't close!");
    }
}
function PopupChgDes3(txtId) {
    clientId = txtId;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/ProductList.aspx');
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
function PopupProductFromDoIn(_sku, _lotNo, _des, _qty4, _qty5, _uom1, _uom2, _uom3, _att1, _att2, _att3, _att4, _att5, _att6,wh,partyId) {
    clientId = _sku;
    clientName = _lotNo;
    add1 = _des;
    add2 = _qty4;
    add3 = _qty5;
    add4 = _uom1;
    add5 = _uom2;
    add6 = _uom3;
    add7 = _att1;
    add8 = _att2;
    add9 = _att3;
    add10 = _att4;
    add11 = _att5;
    add12 = _att6;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectPoductFromDoIn.aspx?wh=' + wh + '&PartyId=' + partyId);
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
//below no use

function PopupProductFromReceipt(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('../SelectPage/SelectProdutFromReceipt.aspx');
    popubCtr.SetHeaderText('Products');
    popubCtr.Show();
}
function AddDoOut() {
    popubCtr1.SetHeaderText('List');
    popubCtr1.SetContentUrl('../SelectPage/AddDoOut.aspx?party=' + txt_PartyId.GetText() + "&no=" + txt_DoNo.GetText());
    popubCtr1.Show();
}
function PutTransfer(sku, loc, lotno, uom1, uom2, uom3, qtypack, qtywhole, qty, price) {
    if (clientId != null) {
        clientId.SetText(sku);
    }
    if (clientName != null) {
        clientName.SetText(loc);
    }
    if (add1 != null) {
        add1.SetText(lotno);
    }
    if (add2 != null) {
        add2.SetText(uom1);
    }
    if (add3 != null) {
        add3.SetText(uom2);
    }
    if (add4 != null) {
        add4.SetText(uom3);
    }
    if (add5 != null) {
        add5.SetText(qtypack);
    }
    if (add6 != null) {
        add6.SetText(qtywhole);
    }
    if (add7 != null) {
        add7.SetText(qty);
    }
    if (add8 != null) {
        add8.SetText(price);
    }
    clientId = null;
    clientName = null;
    add1 = null;
    add2 = null;
    add3 = null;
    add4 = null;
    add5 = null;
    add6 = null;
    add7 = null;
    add8 = null;
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function PopupPutAway(id) {
    popubCtr.SetContentUrl('../SelectPage/AddPutAway.aspx?id=' + id + "&doType=IN");
    popubCtr.SetHeaderText('PutAway');
    popubCtr.Show();
}
function PopupPoNo(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('../SelectPage/SelectAllPurchaseOrder.aspx?wh=' + txt_WarehouseId.GetText() + "&party=" + txt_PartyId.GetText());
    popubCtr.SetHeaderText('Purchase Order');
    popubCtr.Show();
}
function PopupSaleMan(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('../SelectPage/SalesManList.aspx');
    popubCtr.SetHeaderText('SalesMan');
    popubCtr.Show();
}
function PopupToWh(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/WareHouseList.aspx');
    popubCtr.SetHeaderText('WareHouse');
    popubCtr.Show();
}
function PopupWhLo(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/WareHouseList.aspx');
    popubCtr.SetHeaderText('WareHouse');
    popubCtr.Show();
}
function PopupLo(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Unit&wh=' + txt_WareHouseId.GetText());
    popubCtr.SetHeaderText('Location');
    popubCtr.Show();
}
function PopupFromLo(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/LocationList.aspx?loclevel=Unit');
    popubCtr.SetHeaderText('Location');
    popubCtr.Show();
}
function PopupDoInLotNo(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectLotNo.aspx');
    popubCtr.SetHeaderText('LotNo');
    popubCtr.Show();
}
function PopupSoLotNo(txtId, txtName,type) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectLotNo.aspx?no=' + type);
    popubCtr.SetHeaderText('LotNo');
    popubCtr.Show();
}
function PopupProductPrice(txtId, txtName) {
    clientId = txtId;
    clientName = txtName;
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectProductPrice.aspx?partyId=' + txt_PartyId.GetText() + "&sku=" + txt_SKULine_Product.GetText());
    popubCtr.SetHeaderText('LotNo');
    popubCtr.Show();
}




/* ------Receive / Payment*/

function ShowPayment(gridId, invN, docType) {
    grid = gridId;
    if (docType == "Receive") {
        popubCtr1.SetHeaderText('Receive');
        popubCtr1.SetContentUrl('/PagesAccount/EditPage/ArReceiptEdit.aspx?no=' + invN);
    } else if (docType == "Payment") {
        popubCtr1.SetHeaderText('Payment');
        popubCtr1.SetContentUrl('/PagesAccount/EditPage/ApPaymentEdit.aspx?no=' + invN);
    } 
    popubCtr1.Show();
}
function AddReceive(gridId, refNo, partyId) {
    grid = gridId;
    popubCtr1.SetHeaderText('Receive');
    popubCtr1.SetContentUrl('/PagesAccount/EditPage/ArReceiptEdit.aspx?no=0&RefN=' + refNo + '&PartyId=' + partyId);
    popubCtr1.Show();
}
function AddPayment(gridId, refNo, partyId) {
    grid = gridId;
    popubCtr1.SetHeaderText('Payment');
    popubCtr1.SetContentUrl('/PagesAccount/EditPage/ApPaymentEdit.aspx?no=0&RefN=' + refNo + '&PartyId=' + partyId);
    popubCtr1.Show();
}


function MultipleTemplete(refNo) {
    popubCtr2.SetHeaderText('Multiple Templete');
    popubCtr2.SetContentUrl('/selectpage/MultipleTemplete.aspx?no=' + refNo);
    popubCtr2.Show();
}
function PopupContract(memo_Content) {
    clientId = memo_Content;
    popubCtr2.SetHeaderText('Select Templete');
    popubCtr2.SetContentUrl('/selectpage/SelectTemplete.aspx?no=' + txt_DoNo.GetText());
    popubCtr2.Show();
}

function PutContract(content) {
    if (clientId != null) {
        clientId.SetText(content);
    }
    popubCtr2.Hide();
    popubCtr2.SetContentUrl('about:blank');
}
function PopupPersonInfo(id, icNo, name, contact, tel, country, city, zipCode, address, partyType) {
    clientId = id;
    clientName = icNo;
    add1 = name;
    add2 = contact;
    add3 = tel;
    add4 = country;
    add5 = city;
    add6 = zipCode;
    add7 = address;
    popubCtr.SetHeaderText(partyType);
    popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectPerson.aspx?partyType=' + partyType);
    popubCtr.Show();
}
function PutPersonInfo(id, icNo, name, contact, tel, country, city, zipCode, address) {
    if (clientId != null) {
        clientId.SetText(id);
    }
    if (clientName != null) {
        clientName.SetText(icNo);
    }
    if (add1 != null) {
        add1.SetText(name);
    }
    if (add2 != null) {
        add2.SetText(contact);
    }
    if (add3 != null) {
        add3.SetText(tel);
    }
    if (add4 != null) {
        add4.SetText(country);
    }
    if (add5 != null) {
        add5.SetText(city);
    }
    if (add6 != null) {
        add6.SetText(zipCode);
    }
    if (add7 != null) {
        add7.SetText(address);
    }
    clientId = null;
    clientName = null;
    add1 = null;
    add2 = null;
    add3 = null;
    add4 = null;
    add5 = null;
    add6 = null;
    add7 = null;
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}