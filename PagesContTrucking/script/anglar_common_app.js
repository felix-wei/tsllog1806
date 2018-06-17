var app = angular.module('app', [])
    .factory('SV_Common', function ($http) {
        var data = {
            WebService: '',
        };
        var vm = {
            log: function (par, par1) {
                return;
                if (par1) {
                    console.log('********** common: ' + par + ' |', par1);
                } else {
                    console.log('********** common: ' + par);
                }
            },
            getData: function () {
                return data;
            },
            getWebService: function () {
                if (!data.WebService || data.WebService == '') {
                    throw { error: 'Request setWebService' };
                }
                return data.WebService;
            },
            setWebService: function (par) {
                data.WebService = par;
                vm.log('set websercie to [' + data.WebService + ']');
            },
            getRequest: function (func, pars) {
                return {
                    method: 'POST',
                    url: vm.getWebService() + func,
                    headers: {
                        //'Content-Type': 'json',
                        'Content-Type': 'application/x-www-form-urlencoded',
                    },
                    data: pars,
                    dataType: 'json',
                }
            },
            http: function (func, pars, cb) {
                var re = { status: "0", context: "Internet Error" };
                var req = vm.getRequest(func, pars);
                vm.log('http ' + req.url, req.data);
                $http(req).then(function (res) {
                    if (res.statusText == "OK") {
                        re = res.data;
                    }
                    if (cb) {
                        cb(re);
                    }
                }, function (e) {
                    console.log('================ error:', e);
                    if (cb) {
                        cb(re);
                    }
                });
            },
            http_post: function (func, pars, cb) {
                var re = { status: "0", context: "Internet Error" };
                var req = vm.getRequest(func, pars);
                vm.log('post ' + req.url, req.data);

                $http.post(req.url, req.data).then(function (res) {
                    if (res.statusText == "OK") {
                        re.status = "1";
                        re.context = res.data;
                    }
                    cb(re);
                }, function (e) {
                    console.log('================ error:', e);
                    if (cb) {
                        cb(re);
                    }
                });
            },
            ajax_post: function (func, pars, cb) {
                var re = { status: "0", context: "Internet Error" };
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: vm.getWebService() + func,
                    data: pars,
                    dataType: 'json',
                    success: function (result) {
                        console.log(result);
                    },
                    error: function (e) {
                        console.log(e);
                    }
                });
            }
        };
        return vm;
    })
    .factory('SV_Popup', function ($timeout) {
        var data = {};
        var vm = {
            getSide: function (side) {
                var res = 'r0';
                if (side) {
                    switch (side) {
                        case 'center':
                            res = 'c0';
                            break;
                        case 'centerLarger':
                            res = 'cl0';
                            break;
                        case 'right':
                            res = 'r0';
                            break;
                        case 'rightLarge':
                            res = 'rl0';
                            break;
                    }
                }
                return res;
            },
            SetPopup: function (source, side) {
                if (!source) {
                    console.log('============== request source');
                    return;
                }
                //console.log(source);
                source.popup = {
                    curCss: vm.getSide(side),
                    cssList: {
                        'c0': 'float_window_popup float_window_popup_init',
                        'c1': 'float_window_popup float_window_popup_show',
                        'c2': 'float_window_popup float_window_popup_hide',
                        'cl0': 'float_window_centerl float_window_centrel_init',
                        'cl1': 'float_window_centerl float_window_centrel_show',
                        'cl2': 'float_window_centerl float_window_centrel_hide',


                        'r0': 'float_window_right float_window_right_init',
                        'r1': 'float_window_right float_window_right_show',
                        'r2': 'float_window_right float_window_right_hide',
                        'rl0': 'float_window_right float_window_right_init',
                        'rl1': 'float_window_right float_window_rightl_show',
                        'rl2': 'float_window_right float_window_rightl_hide',
                    },
                    toggle: function (sc, par) {
                        if (sc && sc.popup) {
                            var t = '';
                            if (!par) {
                                switch (sc.popup.curCss) {
                                    case '':
                                    case 'c0':
                                        t = 'c1';
                                        break;
                                    case 'c1':
                                        t = 'c2';
                                        break;
                                    case 'c2':
                                        t = 'c0';
                                        break;
                                    case 'cl0':
                                        t = 'cl1';
                                        break;
                                    case 'cl1':
                                        t = 'cl2';
                                        break;
                                    case 'cl2':
                                        t = 'cl0';
                                        break;

                                    case 'r0':
                                        t = 'r1';
                                        break;
                                    case 'r1':
                                        t = 'r2';
                                        break;
                                    case 'r2':
                                        t = 'r0';
                                        break;
                                    case 'rl0':
                                        t = 'rl1';
                                        break;
                                    case 'rl1':
                                        t = 'rl2';
                                        break;
                                    case 'rl2':
                                        t = 'rl0';
                                        break;
                                }
                            } else {
                                t = par;
                            }
                            //console.log('============== ' + sc.popup.curCss + '->' + t);
                            if (t == 'c2' || t == 'cl2' || t == 'r2' || t == 'rl2') {
                                $timeout(function () {
                                    sc.popup.toggle(sc);
                                }, 200);
                            }
                            sc.popup.curCss = t;
                        } else {
                            console.log('============== request source');
                        }
                    },
                    show: function (sc) {
                        if (sc && sc.popup) {
                            var tt = sc.popup.curCss;
                            if (tt && tt.length > 0) {
                                tt = tt.substring(0, tt.length - 1) + '1';
                                sc.popup.toggle(sc, tt);
                            } else {
                                sc.popup.toggle(sc);
                            }
                        } else {
                            console.log('============== request source');
                        }
                    },
                    hide: function (sc) {
                        if (sc && sc.popup) {
                            var tt = sc.popup.curCss;
                            if (tt && tt.length > 0) {
                                tt = tt.substring(0, tt.length - 1) + '2';
                                sc.popup.toggle(sc, tt);
                            } else {
                                sc.popup.toggle(sc);
                            }
                        } else {
                            console.log('============== request source');
                        }
                    },

                }
            }
        }
        return vm;
    })
    .directive('bxPager', function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                fData: '=',
            },
            template: function (el, at) {
                var re = "";
                re += '<div class="pager">\
                        Total  <strong>{{fData.data.totalItems}}</strong>  items, \
                        <input type="text" class="pager_input" ng-model="fData.data.pageSize" ng-change="f_pager.pageSize_Change();" />\
                        Page Size.\
                        <button class="pager_button" ng-click="f_pager.curPage_AddValue(-fData.data.totalPages);" style="margin-left: 30px;">|<</button>\
                        <button class="pager_button" ng-click="f_pager.curPage_AddValue(-1);" ng-disabled="fData.data.curPage<=1"><</button>\
                        <input type="text" class="pager_input" ng-model="fData.data.curPage" ng-change="f_pager.curPage_Change();" />/{{fData.data.totalPages}} Pages&nbsp;\
                        <button class="pager_button" ng-click="f_pager.curPage_AddValue(1);" ng-disabled="fData.data.curPage>=fData.data.totalPages">></button>\
                        <button class="pager_button" ng-click="f_pager.curPage_AddValue(fData.data.totalPages);">>|</button>\
                    </div>';
                return re;
            },
            controller: function ($scope, $element, $attrs, $transclude) {

                $scope.f_pager = {
                    pageSize_Change: function () {
                        var i = parseInt($scope.fData.data.pageSize);
                        if (i && i >= 0) {
                            $scope.fData.data.pageSize = '' + i;
                        } else {
                            if ($scope.fData.data.pageSize == 'AL') {
                                $scope.fData.data.pageSize = '0';
                            } else {
                                $scope.fData.data.pageSize = 'ALL';
                            }
                        }
                        $scope.fData.data.curPage = 1;
                        if ($scope.fData.refresh && typeof ($scope.fData.refresh) == 'function') {
                            $scope.fData.refresh();
                        }
                    },
                    curPage_Change: function () {
                        var i = parseInt($scope.fData.data.curPage);
                        if (i && i > 0) {
                            if (i > $scope.fData.data.totalPages) {
                                $scope.fData.data.curPage = $scope.fData.data.totalPages;
                            } else {
                                $scope.fData.data.curPage = i;
                            }
                        } else {
                            $scope.fData.data.curPage = 0;
                        }
                        if ($scope.fData.refresh && typeof ($scope.fData.refresh) == 'function') {
                            $scope.fData.refresh();
                        }
                    },
                    curPage_AddValue: function (v) {
                        var i = parseInt(v);
                        //console.log($scope.fData.data.curPage,i);
                        if (i) {
                            var temp = $scope.fData.data.curPage + i;
                            if (temp < 1) {
                                temp = 1;
                            }
                            if (temp > $scope.fData.data.totalPages) {
                                temp = $scope.fData.data.totalPages;
                            }
                            if (temp != $scope.fData.data.curPage) {
                                $scope.fData.data.curPage = temp;
                                //SV_List.refresh(null, function (res) { });
                                //console.log($scope.fRefresh);
                                if ($scope.fData.refresh && typeof ($scope.fData.refresh) == 'function') {
                                    $scope.fData.refresh();
                                }
                            }
                        }
                    }
                }
            }
        }
    });