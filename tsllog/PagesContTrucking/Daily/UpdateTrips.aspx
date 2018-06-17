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
                emailTab: {
                    sending: false,
                }
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
                        //allPending: 'YES',
                        allPending: '',
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
                    var func = "/List_GetData_ByPage_170313";
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
                                row.ReturnLastDate = new Date(row.ReturnLastDate);
                                if (row.ReturnLastDate.getFullYear() >= 2017) {
                                    row.ReturnLastDate1 = 'RDate:'+moment(row.ReturnLastDate).format('DD/MM/YYYY');
                                }
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
                            if (str_trips.length == 5) {
                                row.trips.push({ Id: str_trips[0], c: str_trips[1], t: str_trips[2],driver:str_trips[3],text:str_trips[4] });
                            }
                            if (str_trips.length == 7) {
                                row.trips.push({ Id: str_trips[0], c: str_trips[1], t: str_trips[2], driver: str_trips[3], text: str_trips[4], isSub: str_trips[5], subText: 'SUB: '+str_trips[6] });
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
                            row.ReturnLastDate = new Date(row.ReturnLastDate);


                            row.b_c2 = (row.b_c2 == 'Y');
                            row.b_c3 = (row.b_c3 == 'Y');
                            row.b_c4 = (row.b_c4 == 'Y');
                            row.b_c5 = (row.b_c5 == 'Y');
                            row.b_c6 = (row.b_c6 == 'Y');
                            row.b_c7 = (row.b_c7 == 'Y');
                            row.b_c8 = (row.b_c8 == 'Y');
                            row.b_c9 = (row.b_c9 == 'Y');
                            row.b_c10 = (row.b_c10 == 'Y');

                            row.EmailInd = (row.EmailInd == 'Y' ? 'Y' : 'N');
                            data.emailTab.emailTo = row.EmailAddress;
                            data.emailTab.emailCc = 'ops@tsllogistics.sg';
                            data.emailTab.emailSubject = '';
                            data.emailTab.emailContent = '';
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
                    var temp = angular.copy(data.curView.mast);
                    temp.FromDate1 = moment(temp.FromDate).format('YYYYMMDD');
                    temp.ToDate1 = moment(temp.ToDate).format('YYYYMMDD');
                    if (temp.ReturnLastDate) {
                        temp.ReturnLastDate = moment(temp.ReturnLastDate).format('YYYYMMDD');
                    } else {
                        temp.ReturnLastDate = "";
                    }
                    temp.b_c2 = (temp.b_c2 ? 'Y' : 'N');
                    temp.b_c3 = (temp.b_c3 ? 'Y' : 'N');
                    temp.b_c4 = (temp.b_c4 ? 'Y' : 'N');
                    temp.b_c5 = (temp.b_c5 ? 'Y' : 'N');
                    temp.b_c6 = (temp.b_c6 ? 'Y' : 'N');
                    temp.b_c7 = (temp.b_c7 ? 'Y' : 'N');
                    temp.b_c8 = (temp.b_c8 ? 'Y' : 'N');
                    temp.b_c9 = (temp.b_c9 ? 'Y' : 'N');
                    temp.b_c10 = (temp.b_c10 ? 'Y' : 'N');

                    var func = "/saveTrip";
                    SV_Common.http(func, temp, function (res) {
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
                    angular.forEach(data.list, function (row, index) {
                        row.ReturnLastDate = new Date(row.ReturnLastDate);
                        if (row.ReturnLastDate.getFullYear() >= 2017) {
                            row.ReturnLastDate1 = 'RDate:' + moment(row.ReturnLastDate).format('DD/MM/YYYY');
                        }
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
                        type: data.search.type,
                    }
                    var func = "/RefreshList_ByTripId_170313";
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
                save2Excel: function (par, cb) {

                    var pars = angular.copy(data.search);
                    pars.From = moment(data.search.From).format('YYYYMMDD');
                    pars.To = moment(data.search.To).add(1, 'days').format('YYYYMMDD');

                    var func = "/List_save2Excel";
                    SV_Common.http(func, pars, function (res) {

                        if (cb) {
                            cb(res);
                        }
                    });
                },

                refresh_masterdata_client: function (par, cb) {
                    if (!data.masterData.client) {
                        data.masterData.client = {
                            title: 'Client',
                            list: [],
                        };
                    }
                    if (data.masterData.client.list.length <= 0) {
                        var pars = {};
                        var func = "/MasterData_Client";
                        SV_Common.http(func, pars, function (res) {
                            if (res.status == '1') {
                                console.log('====== get mastdata client');
                                data.masterData.client.list = res.context;
                            }
                            if (cb) {
                                cb(res);
                            }
                        });
                    }
                },
				
				refresh_masterdata_vendor: function (par, cb) {
                    if (!data.masterData.vendor) {
                        data.masterData.vendor = {
                            title: 'Vendor',
                            list: [],
                        };
                    }
                    if (data.masterData.vendor.list.length <= 0) {
                        var pars = {};
                        var func = "/MasterData_Vendor";
                        SV_Common.http(func, pars, function (res) {
                            if (res.status == '1') {
                                console.log('====== get mastdata vendor');
                                data.masterData.vendor.list = res.context;
                            }
                            if (cb) {
                                cb(res);
                            }
                        });
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

                emailSend: function (par, cb) {
                    data.emailTab.sending = true;
                    data.emailTab.Det1Id = data.curView.mast.Det1Id;
                    var func = "/email_send";
                    SV_Common.http(func, data.emailTab, function (res) {
                        data.emailTab.sending = false;
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                generateEmailFormat: function (par, cb) {
                    var info = {};
                    info.tripId = par.Id;
                    var func = "/email_generateFormat";
                    SV_Common.http(func, info, function (res) {
                        if (res.status == '1') {
                            data.emailTab.emailSubject = res.context.emailSubject;
                            data.emailTab.emailContent = res.context.emailContent;
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                }
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
                        //$scope.masterData.show($scope.vm.client, $scope.action.selectClient_callback);
                        SV_DailyTrips.refresh_masterdata_client();
                        $scope.masterData.show($scope.vm.masterData.client, $scope.action.selectClient_callback);
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
                    save2excel: function () {
                        SV_DailyTrips.save2Excel(null, $scope.action.save2excel_callback);
                    },
                    save2excel_callback: function (res) {
                        if (res.status == '1') {
                            window.open('../../' + res.context);
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
                        //$scope.vm.curView.mast.FromCode = res.n;
                        $scope.floatView.changeView();
                    },
                    selectToParkingLot: function () {
                        SV_DailyTrips.refresh_masterdata_parkingLot();
                        $scope.masterData.show($scope.vm.masterData.parkingLot, $scope.floatView.selectToParkingLot_callback);
                    },
                    selectToParkingLot_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.ToParkingLot = res.c;
                        //$scope.vm.curView.mast.ToCode = res.n;
                        $scope.floatView.changeView();
                    },
					selectSubContractCode: function () {
                        SV_DailyTrips.refresh_masterdata_vendor();
                        $scope.masterData.show($scope.vm.masterData.vendor, $scope.floatView.selectSubContractCode_callback);
                    },
                    selectSubContractCode_callback: function (res) {
                        console.log(res);
                        $scope.vm.curView.mast.SubCon_Code = res.c;
                        //$scope.vm.curView.mast.ClientName = res.n;
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
                    },
                    sendEmail: function () {
                        SV_DailyTrips.emailSend(null, $scope.floatView.sendEmail_callback);
                    },
                    sendEmail_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Send Email successful', '', 'success');
                            SV_DailyTrips.getView($scope.vm.curView.mast, null);
                        } else {
                            $scope.action.notice('Send Error !', res.context, 'warn');
                        }
                    },
                    generate_emailFormat: function () {
                        SV_DailyTrips.generateEmailFormat($scope.vm.curView.mast);
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


                //======================= attachment part

                $scope.attachment = {
                    data: {
                        list: [],
                        no: '',
                        src: '',
                        title: 'Attachment List',
                        selectCallback: null,
                    },
                    show: function (dd, cb) {
                        //$scope.attachment.data = dd;
                        //$scope.attachment.data.no = '';
                        //$scope.attachment.selectCallback = cb;
                        $scope.attachment.popup.show($scope.attachment);
                    },
                    hide: function () {
                        //$scope.attachment.data = {};

                        $scope.attachment.popup.hide($scope.attachment);
                    },
                    showAttachments: function (jobno) {
                        console.log(jobno);
                        $scope.attachment.data.src = '/SelectPage/PhotoList.aspx?Type=CTM&Sn=0&jobNo=' + jobno;
                        $scope.attachment.show();
                        //=======/SelectPage/PhotoList.aspx?Type=CTM&Sn=0&jobNo=
                    },
                }
                SV_Popup.SetPopup($scope.attachment, 'centerLarger');




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
            width:120px;
        }
        .bx_table .col_box_small_p{
            width:120px;
            height:40px;
        }

.color_green{
    color:green;
}
.color_red{
    color:red;
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
                            <option value="ALL_CONT">Container</option>
                            <option value="ALL_TPT">Transport</option>
                            <%--<option value="IMP">IMP</option>
                            <option value="RET">RET</option>
                            <option value="COL">COL</option>
                            <option value="EXP">EXP</option>
                            <option value="LOC">LOC</option>
                            <option value="SHF">SHF</option>
                            <option value="TPT">TPT</option>--%>
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
                    <div>
                        <input type="button" value="Save to Excel" class="single_button_110 button" ng-click="action.save2excel();" />
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
                    <div>Client</div>
                    <div>
                        <div class="single_buttonselect_full">
                            <div class="code0 code0_full">
                                <input type="text" ng-model="vm.search.Client" />
                                <a class="bx_a_button" ng-click="action.selectClient();">&nbsp;.. </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="joblist_content joblist_content_top60 has_footer {{partDetail.data.css}}">
        <div class="joblist_content_list">
            <div class="bx_table">
                <div class="header">
                    <div class="col_150">JobNo</div>
                    <div class="col_150">Carrier</div>
                    <div>OP</div>
                    <div>Bill/OT/Permit($)</div>
                    <div class="col_150">Client</div>
                    <div>PIC</div>
                    <div class="col_150">Vessel/Voyage</div>
                    <div>ETA/ETD</div>
                    <div>ContainerNo</div>
                    <div>SealNo</div>
                    <div>Size/Weight</div>
                    <div>Permit</div>
                    <div class="col_150">Internal Remark</div>
                    <div class="col_150">From</div>
                    <div class="col_150">To</div>
                    <div>Depot</div>
                    <div class="col_150">Remark</div>
                    <div>Trips</div>
                    <div>Service</div>
                    <div style="min-width:0px;">R</div>
                    <div style="min-width:0px;">$</div>
                    <div>Charges(PSA/Wash/DT/DHC)</div>
                    <div>BillingRemark</div>
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
                        <div ng-if="!row.isTitle">
                            <a href="#" ng-click="action.openTabJob(row);" style="color:{{row.jobStatus=='Voided'?'red':'blue'}}">{{row.JobNo}}</a>
                        </div>
                        <div ng-if="!row.isTitle">
                            <div ng-hide="row.TripCode&&row.TripCode=='EXP'">Carrier:{{row.CarrierId}}<br />{{row.CarrierBkgNo}}</div>
                            <div ng-show="row.TripCode&&row.TripCode=='EXP'">Shipper:{{row.shipper}}<br />Carrier:{{row.CarrierId}}/{{row.CarrierBkgNo}}<br />PortNetRef:{{row.PortnetRef}}<br />PickupRef:{{row.PickupRefNo}}</div>
                        </div>
                        <div ng-if="!row.isTitle">{{row.OperatorCode}}</div>
                        <div ng-if="!row.isTitle">{{row.b_inc1|number:0}}/{{row.b_inc2|number:0}}/{{row.b_inc3|number:0}}</div>
                        <div ng-if="!row.isTitle">{{row.ClientId}}<br />{{row.MasterJobNo}}</div>
                        <div ng-if="!row.isTitle">{{row.orderBy}}<br />{{row.orderByTel}}</div>
                        <div ng-if="!row.isTitle">{{row.Vessel}}/{{row.Voyage}}</div>
                        <div ng-if="!row.isTitle">{{row.EtaDate}}&nbsp;{{row.EtaTime}}&nbsp;/{{row.TerminalLocation}}<br />{{row.EtdDate}}&nbsp;{{row.EtdTime}}</div>
                        <div ng-if="!row.isTitle">{{row.ContainerNo}}</div>
                        <div ng-if="!row.isTitle">{{row.SealNo}}</div>
                        <div ng-if="!row.isTitle">{{row.ContainerType}}/{{row.Weight}}</div>
                        <div ng-if="!row.isTitle">{{row.PermitNo}}</div>
                        <div ng-if="!row.isTitle">{{row.ReleaseToHaulierRemark}}</div>
                        <div ng-if="!row.isTitle">{{row.FromCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ToCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ReturnLastDate1}}<br />{{row.YardAddress}}</div>
                        <div ng-if="!row.isTitle">Job:{{row.JobRemark}}<br />ContIntr:{{row.ContRemark}}<br />Cont:{{row.ContRemark1}}<br />Client:<span style="color:red">{{row.ClientRemark}}</span></div>
                        <div ng-if="!row.isTitle">
                            <div class="col_boxset col_boxset_p">
                                <div class="col_box_small col_box_small_p" ng-repeat="trip in row.trips" ng-class="{'P':'col_box_orange','S':'col_box_green','C':'col_box_blue','X':'col_box_gray'}[trip.t]"
                                    style="{{trip.Id==row.Id?'border:1px solid red': ''}}" ng-click="action.viewRow(trip);">
                                    {{trip.text}}
                                    <p style="margin-top:0px;white-space:nowrap" ng-hide="trip.isSub&&trip.isSub=='Y'">{{trip.driver}}</p>
                                    <p style="margin-top:0px;white-space:nowrap" ng-show="trip.isSub&&trip.isSub=='Y'">{{trip.subText}}</p>
                                </div>
                            </div>
                        </div>
                        <div ng-if="!row.isTitle">{{row.ServiceType}}</div>
                        <div ng-if="!row.isTitle" style="background-color:{{row.jobStatus=='Completed'?'orange':'white'}};color:white;">R</div>
                        <div ng-if="!row.isTitle" style="background-color:{{row.InvAmt&&row.InvAmt>0?'green':'white'}};color:white;">B</div>
                        <div ng-if="!row.isTitle">{{row.psa|number:3}}&nbsp;/&nbsp;{{row.c3|number:3}}&nbsp;/&nbsp;{{row.c5|number:3}}&nbsp;/&nbsp;{{row.c1|number:3}}</div>
                        <div ng-if="!row.isTitle">{{row.BillingRemark}}</div>
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
            <div class="body" ng-include="'UpdateTrips_Detail1.html'">
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

    
    <div class="float_window " ng-class="attachment.popup.cssList[attachment.popup.curCss]">
        <div class="content">
            <div class="header">
                {{attachment.data.title}}
                <button class="header_button" ng-click="attachment.hide();">X</button>
            </div>
            <div class="body">
                <iframe style="position:absolute;top:0px;right:0px;bottom:0px;left:0px;width:99%;height:100%;border:0px;" src="{{attachment.data.src}}"></iframe>
            </div>
        </div>
    </div>

</body>
</html>

