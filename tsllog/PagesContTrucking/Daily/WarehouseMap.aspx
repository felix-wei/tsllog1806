<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseMap.aspx.cs" Inherits="PagesContTrucking_Daily_WarehouseMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Warehouse Map</title>
    <link href="../script/Style_JobList.css" rel="stylesheet" />
    <link href="../script/f_dev.css" rel="stylesheet" />
    <script src="../script/jquery.js"></script>
    <script src="../script/moment.js"></script>
    <script src="../script/angular/angular.min.js"></script>
    <script src="../script/anglar_common_app.js"></script>
    <script type="text/javascript">
        app.factory('SV_DailyTrips', function ($http, SV_Common) {
            var data = {
                list: [],
                search: {
                    warehouse: 'ELL44',
                },
                curView: {},
            };
            var vm = {
                init: function () {
                    SV_Common.setWebService('WebService_WarehouseMap.asmx');
                },
                GetData: function () { return data; },
                refresh: function (par, cb) {
                    var pars = angular.copy(data.search);
                    var func = "/refresh";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            var temp=res.context.list;
                            data.list = temp;
                            angular.forEach(temp, function (row) {
                                //row.BookingDate1 = new Date(row.BookingDate);
                                //row.BookingTime1 = new Date(row.BookingDate + ' ' + row.BookingTime);
                                //row.FromDate = new Date(row.FromDate);
                                //row.ToDate = new Date(row.ToDate);
                            });
                            console.log('=============', data);
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
            };
            vm.init();
            return vm;
        })
            .controller('Ctr_DailyTrips', function ($scope, SV_DailyTrips, $timeout, SV_Popup) {

                $scope.vm = SV_DailyTrips.GetData();

                $scope.action = {
                    refresh: function () {
                        SV_DailyTrips.refresh(null, function (res) {
                            //console.log('=============', $scope.vm);
                            if (res.status == '1') {
                                if (parent.notice) {
                                    parent.notice('Refresh Successful');
                                } else {
                                    console.log('Refresh Successful');
                                }
                            } else {
                                if (parent.notice) {
                                    parent.notice('Refresh False', '', 'error');
                                } else {
                                    console.log('Refresh False');
                                }
                            }
                        });
                    },
                    viewRow: function (row) {
                        $scope.floatView.show(row);
                    },

                }

                $scope.map = {
                    getFrameStyle: function () {
                        var res = "width:" + $scope.map.getFrameWidth() + "px;height:" + $scope.map.getFrameHeight() + "px;";
                        //var res = "width:0px;height:0px;";
                        return res;
                    },
                    getFrameWidth: function () {
                        return 368*2;
                    },
                    getFrameHeight: function () {
                        return 520*2;
                    },
                    getRoomStyle: function (row) {
                        var res = "width:" + $scope.map.getWidth(row.length) + "px;height:" + $scope.map.getHeigth(row.width) + "px;top:" + $scope.map.getTop(row.ToTop) + "px;left:" + $scope.map.getLeft(row.ToLeft) + "px;";
                        return res;
                    },
                    getWidth: function (par) {
                        return par * 2;
                    },
                    getLeft: function (par) {
                        return par *2;
                    },
                    getHeigth: function (par) {
                        return par * 2;
                    },
                    getTop: function (par) {
                        return par * 2;
                    },
                }

                //========================== master data
                $scope.masterData = {
                    data: {
                        list: [],
                        no: '',
                        title: '',
                        selectCallback: null,
                    },
                    show: function (dd, cb) {
                        $scope.masterData.data = dd;
                        $scope.masterData.selectCallback = cb;
                        $scope.masterData.popup.show($scope.masterData);
                    },
                    hide: function () {
                        $scope.masterData.data = {};

                        $scope.masterData.popup.hide($scope.masterData);
                    },
                    select: function (row) {
                        if ($scope.masterData.selectCallback && typeof ($scope.masterData.selectCallback) == 'function') {
                            //$scope.action.selectClient_callback(row);
                            $scope.masterData.selectCallback(row);
                        }
                        $scope.masterData.hide();
                    }
                }
                SV_Popup.SetPopup($scope.masterData, 'right');



                $scope.action.refresh();

            })
    </script>
    <style type="text/css">
        .changed{
            background:#c6ffc6;
        }
        .div_map_out{
            position:absolute;
            top:34px;
            right:0px;
            bottom:0px;
            left:0px;
            /*background:#ff6a00;*/
            overflow:auto
        }
        .div_map_inner{
            position:absolute;
            top:10px;
            left:10px;
            /*background:#c6ffc6;*/
            /*overflow:hidden;*/
        }
        .div_room{
            position:absolute;
            border:1px solid #000000;
        }
        .div_room:hover{
            cursor:pointer;
        }
    </style>
</head>
<body ng-controller="Ctr_DailyTrips">
    <form name="f_search" ng-submit="action.refresh();">
        <div class="bx_table">
            <div class="body">
                <div class="item item_empty">
                    <div>
                        <input type="submit" value="Refresh" class="single_button_110 button" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div class="div_map_out">
        <div class="div_map_inner" style="{{map.getFrameStyle();}}">
            <div class="div_room" ng-repeat="row in vm.list" style="{{map.getRoomStyle(row)}}">
                {{row.Code}}
            </div>
        </div>
    </div>


    <div class="float_window " ng-class="masterData.popup.cssList[masterData.popup.curCss]">
        <div class="content">
            <div class="header">
                {{masterData.data.title}}
                <button class="header_button" ng-click="masterData.hide();">X</button>
            </div>
            <div class="body">

                <div class="bx_table">
                    <div class="body">
                        <div class="item item_empty">
                            <div>Search</div>
                            <div>
                                <input type="text" ng-model="masterData.data.no" class="single_text" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bx_table bx_table_100pc">
                    <div class="header">
                        <div class="col_50">Code</div>
                        <div>Name</div>
                    </div>
                    <div class="body">
                        <div class="item" ng-repeat="row in masterData.data.list|filter:masterData.data.no" ng-click="masterData.select(row);">
                            <div>&nbsp;{{row.c}}</div>
                            <div>{{row.n}}</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
