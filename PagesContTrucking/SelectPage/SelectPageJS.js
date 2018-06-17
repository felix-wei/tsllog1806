var temp1 = null;
var temp2 = null;
var temp3 = null;

function CTM_PutValue2(value1, value2) {
    if (temp1 != null) {
        temp1.SetText(value1);
    }
    if (temp2 != null) {
        temp2.SetText(value2);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function CTM_PutValue3(value1, value2, value3) {
    if (temp1 != null) {
        temp1.SetText(value1);
    }
    if (temp2 != null) {
        temp2.SetText(value2);
    }
    if (temp3 != null) {
        temp3.SetText(value3);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}



function PopupCTM_Driver(txtCode, txtName,txtTowHead) {
    temp1 = txtCode;
    temp2 = txtName;
    temp3 = txtTowHead;
    popubCtr.SetContentUrl('../SelectPage/DriverList.aspx');
    popubCtr.SetHeaderText('Driver');
    popubCtr.Show();
}
