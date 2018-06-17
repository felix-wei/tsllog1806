<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTrips.aspx.cs" Inherits="PagesContTrucking_Daily_UpdateTrips" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Update Trips</title>
    <link href="../script/Style_JobList.css" rel="stylesheet" />
    <link href="../script/f_dev.css" rel="stylesheet" />
    <script src="../script/jquery.js"></script>
    <script src="../script/moment-with-locales.js"></script>



    <script src="../script/angular.min.js"></script>

    <script src="../script/angular-moment-picker.min.js"></script>
    <link href="../script/angular-moment-picker.min.css" rel="stylesheet">

    <script src="../script/anglar_common_app.js"></script>

    <script type="text/javascript">
        app.factory('SV_DailyTrips', function ($http, SV_Common) {
            var data = {
                list: [],
                search: {},
                pager: {
                    totalItems: 0,
                    totalPages: 10,
                    curPage: 0,
                    pageSize: '100',
                },
                curView: {},
                masterData: {},
            };
            var vm = {
                init: function () {
                    SV_Common.setWebService('WebService_UpDateTrips.asmx');
                    data.search = {
                        //From: new Date(moment().add(-102, 'days')),
                        From: new Date(), //moment().subtract(0,'days').calendar(),
                        To: new Date(), //moment().subtract(0,'days').calendar(),
                        ContNo: '',
                        type: 'ALL',
                        tripStatus: 'All',
                        allPending: 'YES',
                    };
                },
                GetData: function () { return data; },
                refresh: function (par, cb) {



                    var pars = angular.copy(data.search);
                    pars.From = moment(data.search.From).format('YYYYMMDD');
                    pars.To = moment(data.search.To).add(1, 'days').format('YYYYMMDD');
                    //pars.To = moment(pars.To).add(1,'days').format('YYYYMMDD');
                    pars.curPage = data.pager.curPage;
                    pars.pageSize = data.pager.pageSize;
                    console.log('=============== refresh:', pars);
                    var func = "/List_GetData_ByPage";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            data.pager.curPage = res.context.curPage;
                            data.pager.totalPages = res.context.totalPages;
                            data.pager.totalItems = res.context.totalItems;
                            console.log(res);
                            data.list = [];
                            var temp = res.context.list;
                            var currentInd = 0;
                            var allPending = false;
                            angular.forEach(temp, function (row) {
                                if (!allPending && row.rowId1 == 1) {
                                    allPending = true;
                                    data.list.push({ title: "All Pending", isTitle: true, style: 'color:red;', });
                                }
                                if (data.list.length == 0 || data.list[data.list.length - 1].ScheduleDate != row.ScheduleDate) {
                                    data.list.push({ title: row.ScheduleDate, ScheduleDate: row.ScheduleDate, isTitle: true, style: '', });
                                }
                                vm.refresh_dataRow_change(row);
                                data.list.push(row);
                            });

                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                refresh_dataRow_change:function(row){

                    var str_trips = row.trips;
                    row.trips = [];
                    if (str_trips && str_trips.length > 0) {
                        str_trips = str_trips.substring(0, str_trips.length - 1);
                        var ar_trips = str_trips.split(',');
                        angular.forEach(ar_trips, function (t) {
                            str_trips = t.split('|');
                            if (str_trips.length == 3) {
                                row.trips.push({ Id: str_trips[0], c: str_trips[1], t: str_trips[2] });
                            }
                        })
                    }
                    switch (row.Statuscode) {
                        case 'P':
                            row.Statuscode1 = "Pending";
                            break;
                        case 'S':
                            row.Statuscode1 = "Started";
                            break;
                        case 'C':
                            row.Statuscode1 = 'Completed';
                            break;
                        default:
                            row.Statuscode1 = row.Statuscode;
                    }
                },
                getView: function (par, cb) {

                    var pars = {};
                    pars.TripId = par.Id;

                    var func = "/View_GetData";
                    SV_Common.http(func, pars, function (res) {

                        if (res.status == '1') {
                            data.curView = res.context;
                            var row = data.curView.mast;
                            row.FromDate = new Date(row.FromDate);
                            row.ToDate = new Date(row.ToDate);

                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                save: function (par, cb) {

                    var func = "/saveTrip";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                saveView: function (par, cb) {
                    data.curView.mast.FromDate1 = moment(data.curView.mast.FromDate).format('YYYYMMDD');
                    data.curView.mast.ToDate1 = moment(data.curView.mast.ToDate).format('YYYYMMDD');

                    var func = "/saveTrip";
                    SV_Common.http(func, data.curView.mast, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                addNewTrip: function (par, cb) {
                    var func = "/AddTrip_ByCurrentyTripId";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                refreshPartList_byDet1Id: function (det1Id, cb) {
                    var Ids = "0";
                    var indexList = [];
                    angular.forEach(data.list, function (row,index) {
                        if (!row.isTitle && row.Det1Id && row.Det1Id > 0) {
                            if (row.Det1Id == det1Id) {
                                Ids += "," + row.Id;
                                indexList.push({ i: index });
                            }
                        }
                    });
                    if (indexList.length == 0) {
                        return;
                    }
                    var temp = {
                        Ids: Ids,
                    }
                    var func = "/RefreshList_ByTripId";
                    SV_Common.http(func, temp, function (res) {
                        if (res.status == '1') {
                            angular.forEach(res.context, function (row) {
                                vm.refresh_dataRow_change(row);
                                for (var i = 0; i < indexList.length; i++) {
                                    if (data.list[indexList[i].i].Id == row.Id) {
                                        data.list.splice(indexList[i].i, 1, row);
                                    }
                                }

                            });
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                readyForBilling: function (par, cb) {
                    var func = "/readyBilling";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },

                refresh_masterdata_driver: function (par, cb) {
                    if (!data.masterData.driver) {
                        data.masterData.driver = {
                            title: 'Driver (From driver on/off)',
                            list: [],
                        };
                    }
                    if (data.masterData.driver.list.length <= 0) {
                        var pars = {};
                        var func = "/MasterData_Driver";
                        SV_Common.http(func, pars, function (res) {
                            if (res.status == '1') {
                                console.log('====== get mastdata driver');
                                data.masterData.driver.list = res.context;
                            }
                            if (cb) {
                                cb(res);
                            }
                        });
                    }
                },
                refresh_masterdata_towhead: function (par, cb) {
                    if (!data.masterData.towhead) {
                        data.masterData.towhead = {
                            title: 'Prime Mover',
                            list: [],
                        };
                    }
                    if (data.masterData.towhead.list.length <= 0) {
                        var pars = {};
                        var func = "/MasterData_Towhead";
                        SV_Common.http(func, pars, function (res) {
                            if (res.status == '1') {
                                console.log('====== get mastdata towhead');
                                data.masterData.towhead.list = res.context;
                            }
                            if (cb) {
                                cb(res);
                            }
                        });
                    }
                },
                refresh_masterdata_trailer: function (par, cb) {
                    if (!data.masterData.trailer) {
                        data.masterData.trailer = {
                            title: 'Trailer',
                            list: [],
                        };
                    }
                    if (data.masterData.trailer.list.length <= 0) {
                        var pars = {};
                        var func = "/MasterData_Trailer";
                        SV_Common.http(func, pars, function (res) {
                            if (res.status == '1') {
                                console.log('====== get mastdata Trailer');
                                data.masterData.trailer.list = res.context;
                            }
                            if (cb) {
                                cb(res);
                            }
                        });
                    }
                },
                refresh_masterdata_parkingLot: function (par, cb) {
                    if (!data.masterData.parkingLot) {
                        data.masterData.parkingLot = {
                            title: 'ParkingLot',
                            list: [],
                        };
                    }
                    if (data.masterData.parkingLot.list.length <= 0) {
                        var pars = {};
                        var func = "/MasterData_ParkingLot";
                        SV_Common.http(func, pars, function (res) {
                            if (res.status == '1') {
                                console.log('====== get mastdata parkingLot');
                                data.masterData.parkingLot.list = res.context;
                            }
                            if (cb) {
                                cb(res);
                            }
                        });
                    }
                },
            };
            vm.init();
            return vm;
        })
            .controller('Ctr_DailyTrips', function ($scope, SV_DailyTrips, $timeout, SV_Popup) {

                $scope.vm = SV_DailyTrips.GetData();

                $scope.action = {
                    init: function () {
                        var type = "ALL";
                        var ss = window.location.search;
                        if (ss.indexOf("?") != -1) {
                            ss = ss.substring(1);
                            var ar = ss.split("&");
                            for (var i = 0; i < ar.length; i++) {
                                var ar1 = ar[i].split("=");
                                if (ar1[0] == "type") {
                                    type = ar1[1];
                                }
                            }
                        }
                        $scope.vm.search.type = type;
                        $scope.action.refresh();
                    },
                    refresh: function () {
                        SV_DailyTrips.refresh(null, function (res) {
                            //console.log('=============', $scope.vm);
                            if (res.status == '1') {
                                $scope.action.notice('Refresh Successful', '', '', null);

                            } else {
                                $scope.action.notice('Refresh False', '', 'error', null);

                            }
                        });
                    },
                    viewRow: function (row) {
                        if ($scope.vm.curView && $scope.vm.curView.mast && $scope.vm.curView.mast.changed) {
                            $scope.action.notice('Current detail without save', '', 'warn', {
                                type: 'confirm',
                                callback: function (res) {
                                    if (res) {
                                        $scope.searchMore.viewDetail(row);
                                        $scope.$apply();
                                    }
                                },
                                buttonText: 'Continue',
                                //buttonStyle: 'confirm_blue',
                                buttonStyle: 'warn',
                            });
                        } else {
                            $scope.searchMore.viewDetail(row);
                        }
                    },
                    changedRow: function (row) {
                        row.changed = true;
                    },
                    selectClient: function () {
                        $scope.masterData.show($scope.vm.client, $scope.action.selectClient_callback);
                    },
                    selectClient_callback: function (res) {
                        //console.log(res);
                        $scope.vm.search.Client = res.c;
                        $scope.vm.search.ClientName = res.n;
                    },
                    openTabJob: function (row) {
                        console.log(row);
                        parent.navTab.openTab(row.JobNo, "/PagesContTrucking/Job/JobEdit.aspx?no=" + row.JobNo, { title: row.JobNo, fresh: false, external: true });
                        //if (row.JobType == 'IMP' || row.JobType == 'EXP') {
                        //    parent.navTab.openTab(row.JobNo1, "/PagesContTrucking/Job/JobEdit.aspx?no=" + row.JobNo1, { title: row.JobNo1, fresh: false, external: true });
                        //}
                        //if (row.JobType == 'WDO' || row.JobType == 'WGR' || row.JobType == 'TPT') {
                        //    parent.navTab.openTab(row.JobNo1, "/PagesContTrucking/Job/JobEdit.aspx?no=" + row.JobNo1, { title: row.JobNo1, fresh: false, external: true });
                        //}
                        //if (row.JobType == 'CRA') {
                        //    parent.navTab.openTab(row.JobNo1, "/PagesContTrucking/Job/JobEdit.aspx?no=" + row.JobNo1, { title: row.JobNo1, fresh: false, external: true });
                        //}
                    },
                    save: function (row) {
                        if (row.FromDate) {
                            row.FromDate1 = moment(row.FromDate).format('YYYYMMDD');
                            row.ToDate1 = moment(row.ToDate).format('YYYYMMDD');
                        }
                        //if (row.ToDate) {
                        //    row.ToDate1 = moment(row.ToDate).format('YYYYMMDD');
                        //}
                        console.log('========', row);
                        SV_DailyTrips.save(row, $scope.action.save_callback);
                    },
                    save_callback: function (res) {
                        console.log(res);

                        if (res.status == '1') {
                            angular.forEach($scope.vm.list, function (row) {
                                //if(row.Det1Id)
                                if (res.context.tripId == row.Id) {
                                    row.changed = false;
                                } else {
                                    if (res.context.Det1Id == row.Det1Id) {
                                        row.ContainerNo = res.context.ContainerNo;
                                    }
                                }
                            });

                            $scope.action.notice('Save Successful', '', 'success', null);

                        } else {
                            $scope.action.notice('Save False', '', 'error', null);

                        }
                    },
                    notice: function (title, body, color, more) {
                        if (parent.notice) {
                            if (more) {
                                if (more.callback) {
                                    more.callback(confirm(title));
                                }
                            } else {
                                parent.notice(title, body, color, more);
                            }
                        } else {
                            if (more && more.type == 'confirm') {
                                if (more.callback) {
                                    more.callback(confirm(title));
                                }
                            }
                            console.log(title, body);
                        }
                    }
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
                        $scope.masterData.data.no = '';
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
                SV_Popup.SetPopup($scope.masterData, 'center');


                //======================== view detail right part
                $scope.floatView = {
                    data: {},
                    refresh_callback: function (res) {

                        if (res.status == '1') {
                            $scope.action.notice('Get Detail Data Successful', '', null, null);
                        } else {
                            $scope.action.notice('Get Detail Data False', '', 'error', null);

                        }
                    },
                    save: function () {
                        //$scope.action.notice('Confirm Save', 'SSSSSSSSSS', '', {
                        //    type: 'confirm',
                        //    callback: function (res) {
                        //        if (res) {
                        //            $scope.action.notice('Confirm Sss');
                        //        } else {

                        //            $scope.action.notice('Confirm ffff');
                        //        }
                        //    },
                        //    buttonText: 'Cccc',
                        //    buttonStyle: 'confirm'
                        //});
                        SV_DailyTrips.saveView(null, $scope.floatView.save_callback);
                    },
                    save_callback: function (res) {
                        if (res.status == '1') {
                            //$scope.vm.curView.mast.changed = false;
                            $scope.action.notice('Save Detail Successful', '', 'success', null);
                            SV_DailyTrips.refreshPartList_byDet1Id($scope.vm.curView.mast.Det1Id);

                            SV_DailyTrips.getView($scope.vm.curView.mast, null);
                        } else {
                            $scope.action.notice('Save Detail Error', res.context.Note, 'error', null);
                            //parent.notice("Save Error", null, 'error');
                        }
                    },
                    changeView: function () {
                        $scope.vm.curView.mast.changed = true;
                    },
                    selectDriver: function () {
                        SV_DailyTrips.refresh_masterdata_driver();
                        $scope.masterData.show($scope.vm.masterData.driver, $scope.floatView.selectDriver_callback);
                    },
                    selectDriver_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.DriverCode = res.c;
                        $scope.vm.curView.mast.TowheadCode = res.n;
                        $scope.floatView.changeView();
                    },
                    selectTowhead: function () {
                        SV_DailyTrips.refresh_masterdata_towhead();
                        $scope.masterData.show($scope.vm.masterData.towhead, $scope.floatView.selectTowhead_callback);
                    },
                    selectTowhead_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.TowheadCode = res.c;
                        $scope.floatView.changeView();
                    },
                    selectTrailer: function () {
                        SV_DailyTrips.refresh_masterdata_trailer();
                        $scope.masterData.show($scope.vm.masterData.trailer, $scope.floatView.selectTrailer_callback);
                    },
                    selectTrailer_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.ChessisCode = res.c;
                        $scope.floatView.changeView();
                    },
                    selectFromParkingLot: function () {
                        SV_DailyTrips.refresh_masterdata_parkingLot();
                        $scope.masterData.show($scope.vm.masterData.parkingLot, $scope.floatView.selectFromParkingLot_callback);
                    },
                    selectFromParkingLot_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.FromParkingLot = res.c;
                        $scope.vm.curView.mast.FromCode = res.n;
                        $scope.floatView.changeView();
                    },
                    selectToParkingLot: function () {
                        SV_DailyTrips.refresh_masterdata_parkingLot();
                        $scope.masterData.show($scope.vm.masterData.parkingLot, $scope.floatView.selectToParkingLot_callback);
                    },
                    selectToParkingLot_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.ToParkingLot = res.c;
                        $scope.vm.curView.mast.ToCode = res.n;
                        $scope.floatView.changeView();
                    },
                    addNewTrip: function (par) {

                        $scope.action.notice('Add New Trip: ' + par, '', 'success', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    var temp = {
                                        TripType: par,
                                        Id: $scope.vm.curView.mast.Id,
                                    };
                                    SV_DailyTrips.addNewTrip(temp, $scope.floatView.addNewTrip_callback);
                                    $scope.$apply();
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    addNewTrip_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Add trip successful', 'List refreshing', 'success');
                            SV_DailyTrips.refreshPartList_byDet1Id($scope.vm.curView.mast.Det1Id);
                        } else {
                            $scope.action.notice('Add trip false', res.context, 'warn');
                        }
                    },
                    readyForBilling: function () {

                        $scope.action.notice('Ready For Billing ?', '', 'success', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    var temp = {
                                        JobNo:$scope.vm.curView.mast.JobNo,
                                        Id: $scope.vm.curView.mast.Id,
                                    };
                                    SV_DailyTrips.readyForBilling(temp, $scope.floatView.readyForBilling_callback);
                                    $scope.$apply();
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    readyForBilling_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Ready for billing', 'List refreshing', 'success');
                            SV_DailyTrips.refreshPartList_byDet1Id($scope.vm.curView.mast.Det1Id);
                            SV_DailyTrips.getView($scope.vm.curView.mast, null);
                        } else {
                            $scope.action.notice('Save Error !', res.context, 'warn');
                        }
                    }
                };
                $scope.partDetail = {
                    data: {
                        css: 'hide_content_detail',
                    },
                    show: function (row) {
                        //$scope.floatView.data = row;
                        SV_DailyTrips.getView(row, $scope.floatView.refresh_callback);
                        $scope.partDetail.showPart();
                    },
                    showPart: function () {
                        switch ($scope.searchMore.data.defaultShowType) {
                            case 'rightSide':
                                $scope.partDetail.showRight();
                                break;
                            case 'bottomSide':
                                $scope.partDetail.showBottom();
                                break;
                            default:
                                $scope.partDetail.hideDirect();
                        }
                    },
                    showRight: function () {
                        $scope.partDetail.data.css = 'has_rightDetail320';
                    },
                    showBottom: function () {
                        $scope.partDetail.data.css = 'has_bottomDetail320';
                    },
                    hide: function () {
                        if ($scope.vm.curView.mast.changed) {
                            $scope.action.notice('Leave without save', '', 'warn', {
                                type: 'confirm',
                                callback: function (res) {
                                    if (res) {
                                        $scope.vm.curView = {};
                                        $scope.partDetail.data.css = 'hide_content_detail';
                                        $scope.$apply();
                                    }
                                },
                                buttonText: 'Continue',
                                buttonStyle: 'warn'
                            });
                        } else {
                            $scope.vm.curView = {};
                            $scope.partDetail.data.css = 'hide_content_detail';
                        }
                    },
                    hideDirect: function () {
                        $scope.partDetail.data.css = 'hide_content_detail';
                    }
                }


                //========================== search More
                $scope.searchMore = {
                    data: {
                        list: [],
                        no: '',
                        title: 'More Option',
                        selectCallback: null,
                        defaultShowType: 'rightSide',
                        defaultShowType_search: 'topSide',
                    },
                    viewDetail: function (row) {
                        var i = $scope.searchMore.data.defaultShowType.indexOf('float');
                        if (i >= 0) {
                            $scope.floatView.show(row);
                        } else {
                            $scope.partDetail.show(row);
                        }
                    }
                };

                $scope.action.init();

                $scope.pager = {
                    //===========data.totalItems: 0,
                    //===========data.totalPages: 10,
                    //===========data.curPage: 0,
                    //===========data.pageSize: '15',
                    data: SV_DailyTrips.GetData().pager,
                    refresh: function () {
                        $scope.action.refresh();
                        //console.log('============ refresh');
                    }
                }
            })
    </script>
    <style type="text/css">
        .changed {
            background: #c6ffc6;
        }
    </style>
</head>
<body ng-controller="Ctr_DailyTrips">
    <form name="f_search" ng-submit="action.refresh();">
        <div class="bx_table">
            <div class="body">
                <div class="item item_empty">
                    <div>Schedule Date:</div>
                    <div>
                        <input type="date" ng-model="vm.search.From" class="single_date" />
                    </div>
                    <div>To:</div>
                    <div>
                        <input type="date" ng-model="vm.search.To" class="single_date" />
                    </div>
                    <div>Trip&nbsp;Type:</div>
                    <div>
                        <select class="single_select" ng-model="vm.search.type">
                            <option value="ALL">ALL</option>
                            <option value="IMP">IMP</option>
                            <option value="RET">RET</option>
                            <option value="COL">COL</option>
                            <option value="EXP">EXP</option>
                            <option value="LOC">LOC</option>
                            <option value="SHF">SHF</option>
                        </select>
                    </div>
                    <div>Trip&nbsp;Status:</div>
                    <div>
                        <select class="single_select" ng-model="vm.search.tripStatus">
                            <option value="All">All</option>
                            <option value="NotCompleted">NotCompleted</option>
                            <option value="P">Pending</option>
                            <option value="C">Completed</option>
                        </select>
                    </div>
                    <div>
                        <input type="submit" value="Retrieve" class="single_button_110 button" />
                    </div>
                </div>
                <div class="item item_empty">

                    <div>ClientRef/JobNo/ContNo</div>
                    <div>
                        <input type="text" ng-model="vm.search.ContNo" placeholder="(end with)" class="single_text" />
                    </div>
                    <div>All&nbsp;Pending</div>
                    <div>
                        <select class="single_select" ng-model="vm.search.allPending">
                            <option value=""></option>
                            <option value="YES">YES (out of time range)</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="joblist_content joblist_content_top60 has_footer {{partDetail.data.css}}">
        <div class="joblist_content_list">
            <div class="bx_table">
                <div class="header">
                    <div class="col_150">OrderNo</div>
                    <div>$</div>
                    <div class="col_150">From Location ID</div>
                    <div class="col_150">To Location ID</div>
                    <div class="col_150">Job Remark</div>
                    <div>Trips</div>
                    <div>TripStatus</div>
                    <div>Driver</div>
                    <div>Prime Mover</div>
                    <div>Trailer No</div>
                    <div>Cont No</div>
                    <div>Seal No</div>
                    <div>Cont Size</div>
                    <div>Cont Weight</div>
                    <div>Cont Yard</div>
                    <div>Depot Last Date</div>
                    <div>Job OP</div>
                    <div>Carrier Ref No</div>
                    <div class="col_150">Vessel/Voyage</div>
                    <div>Eta Date</div>
                    <div>Eta Time</div>
                    <div>Incentive</div>
                    <div>Claim</div>
                    <div>Client</div>
                    <div class="col_150">Client Ref</div>
                </div>
                <div class="body">
                    <div class="item" ng-repeat="row in vm.list" style="{{row.Id&&vm.curView.mast&&vm.curView.mast.Id&&row.Id==vm.curView.mast.Id?'background-color:#f2f2f2': '' }}">
                        <div ng-if="row.isTitle" style="font-weight: 600; font-size: 12px; padding: 8px; {{row.style}}">{{row.title}}</div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="!row.isTitle">
                            <a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a>
                        </div>
                        <div ng-if="!row.isTitle">B</div>
                        <div ng-if="!row.isTitle">{{row.FromCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ToCode}}</div>
                        <div ng-if="!row.isTitle" style="font-weight: bold; color: red">{{row.JobRemark}}</div>
                        <div ng-if="!row.isTitle">
                            <div class="col_boxset">
                                <div class="col_box_small" ng-repeat="trip in row.trips" ng-class="{'P':'col_box_orange','S':'col_box_green','C':'col_box_blue','X':'col_box_gray'}[trip.t]"
                                    style="{{trip.Id==row.Id?'border:1px solid red': ''}}" ng-click="action.viewRow(trip);">
                                    {{trip.c}}
                                </div>
                            </div>
                        </div>
                        <div ng-if="!row.isTitle">{{row.Statuscode1}}</div>
                        <div ng-if="!row.isTitle">{{row.DriverCode}}</div>
                        <div ng-if="!row.isTitle">{{row.TowheadCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ChessisCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ContainerNo}}</div>
                        <div ng-if="!row.isTitle">{{row.SealNo}}</div>
                        <div ng-if="!row.isTitle">{{row.ContainerType}}</div>
                        <div ng-if="!row.isTitle">{{row.Weight}}</div>
                        <div ng-if="!row.isTitle">{{row.YardCode}}</div>
                        <div ng-if="!row.isTitle">{{row.DepotLastDate|date:'dd/MM/yyyy'}}</div>
                        <div ng-if="!row.isTitle">{{row.OperatorCode}}</div>
                        <div ng-if="!row.isTitle">{{row.CarrierBkgNo}}</div>
                        <div ng-if="!row.isTitle">{{row.Vessel}}/{{row.Voyage}}</div>
                        <div ng-if="!row.isTitle">{{row.EtaDate}}</div>
                        <div ng-if="!row.isTitle">{{row.EtaTime}}</div>
                        <div ng-if="!row.isTitle">{{row.Incentive|number:2}}</div>
                        <div ng-if="!row.isTitle">{{row.Claim|number:2}}</div>
                        <div ng-if="!row.isTitle">{{row.ClientId}}</div>
                        <div ng-if="!row.isTitle">{{row.MasterJobNo}}</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer">
            <bx-pager f-data="pager"></bx-pager>
        </div>

        <div class="joblist_content_detail">
            <div class="header" style="padding-left:0px;"><span style="color: white; padding: 6px; background-color: {{vm.curView.mast.Statuscode=='S'?'green':(vm.curView.mast.Statuscode=='C'?'blue':'orange')}}">{{vm.curView.mast.TripCode}}</span>
                <div style="max-width:150px;max-height:14px; display:inline-block;overflow:hidden;white-space:nowrap;">{{vm.curView.mast.JobNo}}</div>
                <button class="header_button" ng-click="partDetail.hide();">X</button>
                <button class="header_button" style="background-color: green; color: white" ng-click="floatView.save();">Save</button>
            </div>
            <div class="body" ng-include="'UpdateTrips_Detail.html'">
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
                            <div>Search (Top 50)</div>
                            <div>
                                <input type="text" ng-model="masterData.data.no" class="single_text" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bx_table bx_table_100pc">
                    <div class="header">
                        <div class="col_50">Code</div>
                        <div>#</div>
                    </div>
                    <div class="body">
                        <div class="item" ng-repeat="row in masterData.data.list|filter:masterData.data.no|limitTo:50" ng-click="masterData.select(row);">
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

