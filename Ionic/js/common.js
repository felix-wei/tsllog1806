//function GetRemoteUrl() {
//    //return 'http://moments.cargoerp.com';
//    return 'http://192.168.1.110:86';
//    //return 'http://127.0.0.1:86';
//}


//function GetWebServiceUrl_common() {
//    return GetRemoteUrl() + '/Connect.asmx';
//}

//function GetUploadUrl() {
//    return GetRemoteUrl();
//}
//function GetRemoteImageUrl(par) {
//    var temp = par;
//    if (par == undefined || par == null || par == '') {
//        //temp = 'Upload/zhaohui.png';
//        return 'img/logo.png';
//    }
//    return GetRemoteUrl() + '/' + temp;
//}



//  =============  json
function json2str(o) {
    var arr = [];
    var fmt = function (s) {
        if (typeof s == 'object' && s != null) return json2str(s);
        return /^(string|number)$/.test(typeof s) ? "'" + json2str_replace(s) + "'" : "'" + s + "'";
    }
    for (var i in o) arr.push("'" + i + "':" + fmt(o[i]));
    return '{' + arr.join(',') + '}';
}

function json2str_replace(s) {
    var r = s;
    r = r.toString().replace('&', '!AND!');
    r = r.toString().replace('?', '!WEN!');
    r = r.toString().replace('#', '!JING!');
    r = r.toString().replace('\'', '!DYIN!');
    r = r.toString().replace('-', '!HEN!');
    return r;
}




//============== date format
Date.prototype.DateFormat = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
Date.prototype.dateDiff = function (interval, objDate2) {
    var d = this, i = {}, t = d.getTime(), t2 = objDate2.getTime();
    i['y'] = objDate2.getFullYear() - d.getFullYear();
    i['q'] = i['y'] * 4 + Math.floor(objDate2.getMonth() / 4) - Math.floor(d.getMonth() / 4);
    i['m'] = i['y'] * 12 + objDate2.getMonth() - d.getMonth();
    i['ms'] = objDate2.getTime() - d.getTime();
    i['w'] = Math.floor((t2 + 345600000) / (604800000)) - Math.floor((t + 345600000) / (604800000));
    i['d'] = Math.floor(t2 / 86400000) - Math.floor(t / 86400000);
    i['h'] = Math.floor(t2 / 3600000) - Math.floor(t / 3600000);
    i['n'] = Math.floor(t2 / 60000) - Math.floor(t / 60000);
    i['s'] = Math.floor(t2 / 1000) - Math.floor(t / 1000);
    return i[interval];
}

Date.prototype.getMonth_e = function () {
    var d = this;
    var m = d.getMonth();
    var re = '';
    switch (m) {
        case 0: re = 'Jan'; break;
        case 1: re = 'Feb'; break;
        case 2: re = 'Mar'; break;
        case 3: re = 'Apr'; break;
        case 4: re = 'May'; break;
        case 5: re = 'Jun'; break;
        case 6: re = 'Jul'; break;
        case 7: re = 'Aug'; break;
        case 8: re = 'Sep'; break;
        case 9: re = 'Oct'; break;
        case 10: re = 'Nov'; break;
        case 11: re = 'Dec'; break;
    }
    return re;
}
Date.prototype.getDay_e = function () {
    var d = this;
    var m = d.getDay();
    var re = '';
    switch (m) {
        case 0: re = 'Sun'; break;
        case 1: re = 'Mon'; break;
        case 2: re = 'Tue'; break;
        case 3: re = 'Wed'; break;
        case 4: re = 'Thur'; break;
        case 5: re = 'Fri'; break;
        case 6: re = 'Sat'; break;
    }
    return re;
}


GetInitData_Login = function () {
    return {
        un: 'David Goh',
        pw: '123',
        company: 'citycontainer',
        //company: 'demo'
    }
}

GetInitData_Scheme = function () {
    //console.log('================= in common Get init data scheme')
    //============= light白，stable灰，positive蓝，calm浅蓝，balanced绿，energized黄，assertive红，royal紫，dark黑
    return 'positive';
}




f_GetFileType = function (filename) {
    var temp_file = filename.toString().toLowerCase();
    var temp_ar = temp_file.split('.');
    var re = "file"
    var img_file_end = "|psd|jpg|gif|bmp|jpeg|png|";
    var excel_file_end = "|xls|xlsx|xlsm|xltm|xlsb|xlam|"
    if (temp_ar.length > 1) {
        var file_end = temp_ar[1];
        re = file_end;
        if (img_file_end.indexOf('|' + file_end + '|') > -1) {
            re = "Image";
        }
        if (excel_file_end.indexOf('|' + file_end + '|') > -1) {
            re = "excel";
        }
    }
    return re;
}