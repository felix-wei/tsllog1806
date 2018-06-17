angular.module('starter.filters', [])

.filter('FL_Port', function () {
    return function (list, search,top) {
        var top_in = (top ? top : 30);
        //var re = [];
        //console.log('===================', list, top_in);
        //for (var i = 0; i < list.length || re.length <= top_in; i++) {
        //    if (list[i].Code.toString().toLowerCase().indexof(search.toLowerCase()) >= 0) {
        //        re.push(list[i]);
        //    }
        //}
        //return re;
        if (list && list.length > 0) {
            var re = [];
            for (var i = 0; i < list.length || re.length <= top_in; i++) {
                if (list[i].Code) {
                    var t1 = list[i].Code.toString().toLowerCase();
                    var t2 = search.toLowerCase();
                    if (t1.indexOf(t2) >= 0) {
                        re.push(list[i]);
                    }
                } else {
                    re = list;
                    break;
                }
            }
            return re;
        }
        return list;
    }
})