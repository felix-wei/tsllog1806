<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertAPI.aspx.cs" Inherits="PagesContTrucking_Daily_AlertAPI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Alert</title>
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
                    SV_Common.setWebService('WebService_AlertAPI.asmx');
                    data.search = {
                        //From: new Date(),
                        //To: new Date(), 
                        ContNo: '',
                        type: 'Permit',
                    };
                },
                GetData: function () { return data; },
                refresh: function (par, cb) {

                    //var pars = angular.copy(data.search);
                    //pars.From = moment(data.search.From).format('YYYYMMDD');
                    //pars.To = moment(data.search.To).add(1, 'days').format('YYYYMMDD');
                    ////pars.To = moment(pars.To).add(1,'days').format('YYYYMMDD');
                    //pars.curPage = data.pager.curPage;
                    //pars.pageSize = data.pager.pageSize;
                    //console.log('=============== refresh:', pars);



                    var func = "/Permit_GetList";
                    switch (data.search.type) {
                        case 'Permit':
                            func = "/Permit_GetList";
                            break;
                        case 'MustReturn':
                            func = "/MustReturn_GetList";
                            break;
                        case 'MustSend':
                            func = "/MustSend_GetList";
                            break;
                        case 'C-DEM':
                            func = "/CDEM_GetList";
                            break;
                        case 'Charges':
                            func = "/Charges_GetList";
                            break;
                        case 'Export':
                            func = "/Export_GetList";
                            break;
                    }
                    var pars = {};
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            data.list = res.context;
                            angular.forEach(data.list, function (row) {
                                if (row.TripCode == 'RET' && data.search.type == 'MustReturn') {
                                    row.ReturnLastDate = moment(row.ReturnLastDate).format('YYYY/MM/DD');
                                }
                            })
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                getView: function (par, cb) {

                    var func = "/getEmailDetail_byContNo";
                    var pars = {
                        contId: par.Id,
                    };
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {

                            data.curView = res.context;
                            data.curView.emailTab = {};
                            data.curView.emailTab.contId = data.curView.mast.contId;
                            data.curView.emailTab.emailTo = data.curView.mast.EmailAddress;
                            data.curView.emailTab.emailCc = '';
                            data.curView.emailTab.emailSubject = 'From TSL ' + data.search.type + ' (' + data.curView.mast.ContainerNo + ')';
                            data.curView.emailTab.emailContent = '';
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                ignore_save: function (par, cb) {
                    var func = "";

                    switch (data.search.type) {
                        case 'Charges':
                            func = "/Charges_alertIgnore";
                            break;
                        case 'Export':
                            func = "/Export_alertIgnore";
                            break;
                    }
                    if (func.length > 0) {
                        SV_Common.http(func, par, function (res) {
                            if (cb) {
                                cb(res);
                            }
                        });
                    } else {
                        cb({ status: 0, context: 'Save Error' });
                    }
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

                emailSend: function (par, cb) {
                    var temp = angular.copy(par);
                    temp.type = data.search.type;
                    //console.log(temp);
                    var func = "/email_byContNo";
                    par.sending = true;
                    SV_Common.http(func, temp, function (res) {
                        //console.log(temp,res);
                        par.sending = false;
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
                    init: function () {
                        var type = "Permit";
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
                    ignore_save: function (row) {
                        var temp = {};
                        temp.contId = row.Id;
                        SV_DailyTrips.ignore_save(temp, $scope.action.ignore_save_callback);
                    },
                    ignore_save_callback:function(res){
                        if (res.status == '1') {
                            $scope.action.notice('Save Successful', '', 'success', null);
                            $scope.action.refresh();

                        } else {
                            $scope.action.notice('Save fail', res.context, 'error', null);
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
                                        JobNo: $scope.vm.curView.mast.JobNo,
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
                    },
                    sendEmail: function () {
                        var temp = $scope.vm.curView.emailTab;
                        SV_DailyTrips.emailSend(temp, $scope.floatView.emailSend_callback);
                    },
                    emailSend_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Email Successful', '', 'success', null);
                            $scope.partDetail.hide();
                            $scope.action.refresh();

                        } else {
                            $scope.action.notice('Email fail', res.context, 'error', null);
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
                        if ($scope.vm.curView.mast&&$scope.vm.curView.mast.changed) {
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

        .bx_table .col_boxset_p {
            width: 120px;
        }

        .bx_table .col_box_small_p {
            width: 120px;
            height: 40px;
        }
    </style>
</head>
<body ng-controller="Ctr_DailyTrips">
    <form name="f_search" ng-submit="action.refresh();">
        <div class="bx_table">
            <div class="body">
                <div class="item item_empty">
                    <div>Type:</div>
                    <div>
                        <select class="single_select" ng-model="vm.search.type">
                            <option value="C-DEM">C-DEM</option>
                            <option value="Permit">Permit</option>
                            <option value="MustReturn">MustReturn</option>
                            <option value="MustSend">MustSend</option>
                            <option value="Charges">Charges</option>
                            <option value="Export">Export</option>
                        </select>
                    </div>
                    <div>
                        <input type="submit" value="Retrieve" class="single_button_110 button" />
                    </div>
                    <div>(Top 100 rows)</div>
                </div>
            </div>
        </div>
    </form>

    <div class="joblist_content joblist_content_top35 {{partDetail.data.css}}">
        <div class="joblist_content_list">
            <div class="bx_table">
                <div class="header">
                    <div class="col_150">JobNo</div>
                    <div class="col_150">ContainerNo</div>
                    <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'">TripType</div>
                    <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'">Driver</div>
                    <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'">Vehicle</div>
                    <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'" class="col_150">From</div>
                    <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'" class="col_150">To</div>
                    <div ng-show="vm.search.type=='Charges'||vm.search.type=='Export'">JobType</div>
                    <div ng-show="vm.search.type=='Charges'||vm.search.type=='Export'">ScheduleDate</div>
                    <div>
                        <div ng-show="vm.search.type=='Permit'">PermitNo</div>
                        <div ng-show="vm.search.type=='C-DEM'">Last IMP CompleteDate</div>
                        <div ng-show="vm.search.type=='MustReturn'">ReturnLastDate</div>
                        <div ng-show="vm.search.type=='MustSend'">ETA</div>
                        <div ng-show="vm.search.type=='Charges'">Detail</div>
                        <div ng-show="vm.search.type=='Export'">SealNo</div>
                    </div>
                    <div ng-show="vm.search.type=='Charges'||vm.search.type=='Export'">Action</div>
                </div>
                <div class="body">
                    <%--<div class="item" ng-repeat="row in vm.list" style="{{row.id&&vm.curview.mast&&vm.curview.mast.id&&row.id==vm.curview.mast.id?'background-color:#f2f2f2': '' }}">
                        <div ng-if="row.isTitle" style="font-weight: 600; font-size: 12px; padding: 8px; {{row.style}}">{{row.title}}</div>
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
                        <div ng-if="!row.isTitle">{{row.ContainerNo}}</div>
                        <div ng-if="!row.isTitle">{{row.TripCode}}</div>
                        <div ng-if="!row.isTitle">{{row.DriverCode}}</div>
                        <div ng-if="!row.isTitle">{{row.TowheadCode}}</div>
                        <div ng-if="!row.isTitle">{{row.FromCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ToCode}}</div>
                        <div ng-if="!row.isTitle">
                            <div ng-show="vm.search.type=='Permit'">{{row.PermitNo}}</div>
                            <div ng-show="vm.search.type=='C-DEM'">{{row.ToDate|date:'yyyy/MM/dd'}}</div>
                            <div ng-show="vm.search.type=='MustReturn'">{{row.ReturnLastDate}}</div>
                            <div ng-show="vm.search.type=='MustSend'">{{row.Eta}}</div>
                            <div ng-show="vm.search.type=='Charges'">{{row.tx}}</div>
                        </div>
                    </div>--%>
                    <div class="item" ng-repeat="row in vm.list">

                        <div>
                            <a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a>
                        </div>
                        <div>{{row.ContainerNo}}</div>
                        <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'">{{row.TripCode}}</div>
                        <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'">{{row.DriverCode}}</div>
                        <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'">{{row.TowheadCode}}</div>
                        <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'">{{row.FromCode}}</div>
                        <div ng-show="vm.search.type=='Permit'||vm.search.type=='C-DEM'||vm.search.type=='MustReturn'||vm.search.type=='MustSend'">{{row.ToCode}}</div>
                        <div ng-show="vm.search.type=='Charges'||vm.search.type=='Export'">{{row.JobType}}</div>
                        <div ng-show="vm.search.type=='Charges'||vm.search.type=='Export'">{{row.date1|date:'yyyy/MM/dd'}}</div>
                        <div>
                            <div ng-show="vm.search.type=='Permit'">{{row.PermitNo}}</div>
                            <div ng-show="vm.search.type=='C-DEM'">{{row.ToDate|date:'yyyy/MM/dd'}}</div>
                            <div ng-show="vm.search.type=='MustReturn'">{{row.ReturnLastDate}}</div>
                            <div ng-show="vm.search.type=='MustSend'">{{row.Eta}}</div>
                            <div ng-show="vm.search.type=='Charges'||vm.search.type=='Export'">{{row.text1}}</div>
                        </div>
                        <div ng-show="vm.search.type=='Charges'||vm.search.type=='Export'">
                            <table>
                                <tr>
                                    <td>
                                        <input type="button" value="Ignore" class="single_button_110 button" ng-click="action.ignore_save(row);" /></td>
                                    <td>
                                        <input type="button" value="Email" class="single_button_110 button" ng-click="action.viewRow(row);" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="joblist_content_detail">
            <div class="header" style="padding-left: 0px;">
                <div style="max-width: 150px; max-height: 14px; display: inline-block; overflow: hidden; white-space: nowrap;">{{vm.curView.mast.title}}</div>
                <button class="header_button" ng-click="partDetail.hide();">X</button>
                <%--<button class="header_button" style="background-color: green; color: white" ng-click="floatView.save();">Save</button>--%>
            </div>
            <div class="body">

                
                <form name="fEmail" ng-submit="floatView.sendEmail();" >
                    <table class="bx_table_100pc bx_table_grid bx_table_grid_border0">
                        <tr class="tr_label">
                            <td>Email To <b style="color: red;">(*)</b> :</td>
                        </tr>
                        <tr>
                            <td>
                                <input type="email" ng-model="vm.curView.emailTab.emailTo" class="single_text_full" placeholder="To@email.address" required /></td>
                        </tr>
                        <tr class="tr_label">
                            <td>Cc:</td>
                        </tr>
                        <tr>
                            <td>
                                <input type="email" ng-model="vm.curView.emailTab.emailCc" class="single_text_full" placeholder="Cc@email.address" /></td>
                        </tr>
                        <tr class="tr_label">
                            <td>Subject <b style="color: red;">(*)</b> :</td>
                        </tr>
                        <tr>
                            <td>
                                <input type="text" ng-model="vm.curView.emailTab.emailSubject" class="single_text_full" required /></td>
                        </tr>
                        <tr>
                            <td>
                                <textarea ng-model="vm.curView.emailTab.emailContent" class="single_textarea_full" required style="height: 200px;"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div ng-hide="vm.curView.emailTab.sending">
                                    <input type="submit" class="button" value="Send" ng-disabled="fEmail.$invalid" />
                                </div>
                                <div ng-show="vm.curView.emailTab.sending">
                                    <input type="button" class="button" value="Sending..." ng-disabled="true" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </form>


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

