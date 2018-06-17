function PopupPort(c1, c2) {
    par1 = c1;
    par2 = c2;
    popubCtr.SetHeaderText('Port');
    popubCtr.SetContentUrl('DispatchPlanner2_ChangeStage.aspx');
    popubCtr.Show();
}
function PopupChangeStage(ContNo,JobNo,Det2Id) {
    popubCtr.SetHeaderText('ChangeStage');
    popubCtr.SetContentUrl('DispatchPlanner2_ChangeStage.aspx?ContNo=' + ContNo + '&JobNo=' + JobNo + '&Det2Id=' + Det2Id);
    popubCtr.Show();
}
function PopupStage_Level1(ParId) {
    popubCtr.SetHeaderText('Stage');
    popubCtr.SetContentUrl('DispatchPlanner_Stage_level1.aspx?ParId='+ParId);
    popubCtr.Show();
}

function ClosePopupCtr() {
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}

var par1 = null;
var par2 = null;
var result_func;
function PutValue(c1, c2) {
    if (par1 != null) {
        par1.SetText(c1);
    }
    if (par2 != null) {
        par2.SetText(c2);
    }
    popubCtr.Hide();
    popubCtr.SetContentUrl('about:blank');
}
function EndFunction() {
    result_func();
}

function getElementPosition(element) {
    var result = new Object();
    result.x = 0;
    result.y = 0;
    result.width = 0;
    result.height = 0;
    if (element.offsetParent) {
        result.x = element.offsetLeft;
        result.y = element.offsetTop;
        var parent = element.offsetParent;
        while (parent) {
            result.x += parent.offsetLeft;
            result.y += parent.offsetTop;
            var parentTagName = parent.tagName.toLowerCase();
            if (parentTagName != "table " &&
            parentTagName != "body " &&
            parentTagName != "html " &&
            parentTagName != "div " &&
            parent.clientTop &&
            parent.clientLeft) {
                result.x += parent.clientLeft;
                result.y += parent.clientTop;
            }
            parent = parent.offsetParent;
        }
    }
    else if (element.left && element.top) {
        result.x = element.left;
        result.y = element.top;
    }
    else {
        if (element.x) {
            result.x = element.x;
        }
        if (element.y) {
            result.y = element.y;
        }
    }
    if (element.offsetWidth && element.offsetHeight) {
        result.width = element.offsetWidth;
        result.height = element.offsetHeight;
    }
    else if (element.style && element.style.pixelWidth && element.style.pixelHeight) {
        result.width = element.style.pixelWidth;
        result.height = element.style.pixelHeight;
    }
    return result;
}


function tc_layout_show() {
    $("#div_tc").css("display", "block");
}
function tc_layout_hid() {
    $("#div_tc").css("display", "none");
}

function check_is_date(date) {
    try{
        var temp=new Date(date);
    } catch (e) {
        return false;
    }
    if (date == null || date == "") {
        return false;
    }
    return true;
}