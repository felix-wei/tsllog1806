<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTrips1.aspx.cs" Inherits="PagesContTrucking_Daily_UpdateTrips1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Update Trips</title>
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
                search: {},
                pager: {
                    totalItems: 0,
                    totalPages: 10,
                    curPage: 0,
                    pageSize: '10',
                },
                client: {
                    title: 'Client',
                },
                curView: {},
                //============= in download_excel must mach following list
                tripIndex: [
                    { id: 1, c: 'DL', n: 'Direct Loading' },
                    { id: 2, c: 'DD', n: 'Direct Delivery' },
                    { id: 3, c: 'LOC', n: 'Local Delivery' },
                    { id: 4, c: 'EXP', n: 'Export Container' },
                    { id: 5, c: 'IMP', n: 'Import Container' },
                    { id: 6, c: 'COL', n: 'Empty Collection' },
                    { id: 7, c: 'RET', n: 'Empty Return' },
                    { id: 8, c: 'SHF', n: 'Shifting' },
                    { id: 9, c: 'CRA', n: 'Crane Job' },
                    //{ id: 10, c: 'EP', n: 'Escort' },
                    { id: 11, c: '', n: 'Empty Trip Type' },
                    //{ id: 7, c: 'Completed', n: 'Completed' },
                ]
            };
            var vm = {
                init: function () {
                    SV_Common.setWebService('WebService_UpDateTrips.asmx');
                    data.search = {
                        From: new Date(),
                        To: new Date(),
                        ContNo: '',
                        status:'All',
                    };
                    //data.search.From.setDate(data.search.From.getDate() - 10);
                    //data.search.To.setDate(data.search.To.getDate() - 1);
                    //vm.refresh_client();
                },
                GetData: function () { return data; },
                refresh: function (par, cb) {



                    var pars = angular.copy(data.search);
                    pars.From = moment(data.search.From).format('YYYYMMDD');
                    pars.To = moment(data.search.To).add(1, 'days').format('YYYYMMDD');
                    //pars.To = moment(pars.To).add(1,'days').format('YYYYMMDD');
                    pars.curPage = data.pager.curPage;
                    pars.pageSize = data.pager.pageSize;
                    pars.locked = 'LOCKED';
                    var func = "/List_GetData_ByPage";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            data.pager.curPage = res.context.curPage;
                            data.pager.totalPages = res.context.totalPages;
                            data.pager.totalItems = res.context.totalItems;
                           
                            //console.log(res);
                            data.list = [];
                            var temp = res.context.list;
                            var currentInd =0;
                            angular.forEach(temp, function (row) {
                                //row.BookingDate1 = new Date(row.BookingDate);
                                //row.BookingTime1 = new Date(row.BookingDate + ' ' + row.BookingTime);
                                //row.FromDate = new Date(row.FromDate);
                                //row.ToDate = new Date(row.ToDate);
                            });
                            angular.forEach(data.tripIndex, function (titleRow, index) {
                                if (index > 0) {
                                    data.list.push({ title: titleRow.n, isTitle: true });
                                    for (; currentInd < temp.length; currentInd++) {
                                        if (temp[currentInd].TripCodeInd != titleRow.id) {
                                            break;
                                        }
                                        if (titleRow.c == 'LOC') {
                                            if (!data.list[data.list.length - 1].isTitle) {
                                                if (data.list[data.list.length - 1].TripJobType != temp[currentInd].TripJobType) {
                                                    data.list.push({ title: '', isTitle: true });
                                                }
                                            }
                                        }
                                        data.list.push(temp[currentInd]);
                                    }
                                } else {
                                    if (temp[0] && temp[0].TripCodeInd == titleRow.id) {
                                        data.list.push({ title: titleRow.n, isTitle: true });
                                        for (; currentInd < temp.length; currentInd++) {
                                            if (temp[currentInd].TripCodeInd != titleRow.id) {
                                                break;
                                            }
                                            data.list.push(temp[currentInd]);
                                        }
                                    }
                                }
                            });
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
                    changedRow:function(row){
                        row.changed=true;
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
                        parent.navTab.openTab(row.JobNo1, "/PagesContTrucking/Job/JobEdit.aspx?no=" + row.JobNo1, { title: row.JobNo1, fresh: false, external: true });
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
                        if ($scope.action.isLocked(row) == 'Y') {
                            parent.notice('Trip has be locked!', '', 'error');
                        } else {
                            if (row.FromDate) {
                                row.FromDate1 = moment(row.FromDate).format('YYYYMMDD');
                            }
                            if (row.ToDate) {
                                row.ToDate1 = moment(row.ToDate).format('YYYYMMDD');
                            }
                            if (row.BookingDate) {
                                row.BookingDate1 = moment(row.BookingDate).format('YYYYMMDD');
                            }
                            console.log('========', row);
                            SV_DailyTrips.save(row, $scope.action.save_callback);
                        }
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

                            if (parent.notice) {
                                parent.notice('Save Successful', '', 'ok');
                            } else {
                                console.log('Save Successful');
                            }
                        } else {
                            if (parent.notice) {
                                parent.notice('Save False', '', 'error');
                            } else {
                                console.log('Save False');
                            }
                        }
                    },
                    isLocked: function (row) {
                        if (row.TripStatus == "LOCKED" || row.TripStatus == "PAID") {
                            return 'Y';
                        }
                        else {
                            return 'N'
                        }
                    },
                    isPaid: function (row) {
                        if (row.TripStatus == "PAID") {
                            return 'Y';
                        }
                        else {
                            return 'N'
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
        .changed{
            background:#c6ffc6;
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
                        <input type="date" ng-model="vm.search.From" class="single_date_140" />
                    </div>
                    <div>To:</div>
                    <div>
                        <input type="date" ng-model="vm.search.To" class="single_date_140" />
                    </div>
                    <div>TK Status:</div>
                    <div>
                        <select class="single_select" ng-model="vm.search.status">
                            <option value="All">All</option>
                            <%--<option value="UnCompleted">UnCompleted</option>--%>
                            <option value="P">Pending</option>
                            <option value="C">Completed</option>
                        </select>
                    </div>
                    <div>Master/JobNo/ContNo</div>
                    <div>
                        <input type="text" ng-model="vm.search.ContNo" placeholder="(end with)" class="single_text" />
                    </div>
                    <div>
                        <input type="submit" value="Retrieve" class="single_button_110 button" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="joblist_content joblist_content_top35 has_footer">
        <div class="joblist_content_list">
            <div class="bx_table">
                <div class="header">
                    <div class="col_150">JobNo</div>
                    <div >#</div>
                    <div class="col_150">Trip From</div>
                    <div class="col_150">Trip To</div>
                    <div class="col_150">Trip Instruction</div>
                    <div class="col_150">Client</div>
                    <div class="col_150">Master Job No</div>
                    <div>ReturnLastDate</div>
                    <div >#</div>
                    <div>Do No</div>
                    <div>ContainerNo/ChessisType</div>
                    <div>Trip Status</div>
                    <div>ScheduleDate</div>
                    <div>ScheduleTime</div>
                    <div>FromDate</div>
                    <div>ToDate</div>
                    <div>Prime Mover(AT Driver)</div>
                    <div>Trailer</div>
                    <div>Trip($)</div>
                    <div>Overtime($)</div>
                    <div>Standby($)</div>
                    <div>PSA ALLOWANCE($)</div>
                    <div>DHC($)</div>
                    <div>WEIGHING($)</div>
                    <div>WASHING($)</div>
                    <div>REPAIR($)</div>
                    <div>DETENTION($)</div>
                    <div>DEMURRAGE($)</div>
                    <div>LIFT ON/OFF($)</div>
                    <div>C/SHIPMENT($)</div>
                    <div>EMF($)</div>
                    <div>ERP($)</div>
                    <div>ParkingFee($)</div>
                    <div>OTHER($)</div>
                    <div>LOCKED</div>
                    <div>PAID</div>
                </div>
                <div class="body">
                    <div class="item" ng-repeat="row in vm.list" ng-hide="row.isTitle&&(!vm.list[$index+1]||vm.list[$index+1].isTitle)">
                        <div ng-if="row.isTitle" style="font-weight: 600;font-size:12px;padding:8px;">{{row.title}}</div>
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
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]"><a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a></div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]" style="text-align:center">
                            <input type="button" class="button single_button_110" value="Save" ng-show="row.changed" ng-click="action.save(row);" />
                        </div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">
                            <%--{{row.PickupFrom}}--%>
                            <input type="text" ng-change="action.changedRow(row);" style="width:150px" ng-model="row.PickupFrom" />
                        </div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">
                            <%--{{row.DeliveryTo}}--%>
                            <input type="text" ng-change="action.changedRow(row);" style="width:150px" ng-model="row.DeliveryTo" />
                        </div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width:150px" ng-model="row.Remark" />
                        </div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.ClientName}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.MasterJobNo}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.ReturnLastDate|date:'yyyy/MM/dd'}}</div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]" style="text-align:center">
                            <input type="button" class="button single_button_110" value="Save" ng-show="row.changed" ng-click="action.save(row);" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width: 100px" ng-model="row.ManualDO" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width:150px" placeholder="(ContainerNo)" ng-show="row.showCT==0&&row.Det1Id>0" ng-model="row.ContainerNo" />
                            <input type="text" ng-change="action.changedRow(row);" style="width:150px" placeholder="(Crane Type)" ng-show="row.showCT==1" ng-model="row.RequestVehicleType" />
                            <input type="text" ng-change="action.changedRow(row);" style="width:150px" placeholder="(Chessis Type)" ng-show="row.showCT==2" ng-model="row.RequestTrailerType" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <select ng-change="action.changedRow(row);"  ng-model="row.Statuscode">
                                <option value="P">Pending</option>
                                <option value="C">Completed</option>
                                <option value="X">Cancel</option>
                            </select>
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.BookingDate" placeholder="yyyy/MM/dd"/>
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width:70px" ng-model="row.BookingTime" placeholder="HH:mm"/>
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.FromDate" placeholder="yyyy/MM/dd"/>
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.ToDate" placeholder="yyyy/MM/dd"/>
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.TowheadCode" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="text" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.ChessisCode" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.inc1" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.inc2" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.inc3" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.inc4" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c1" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c2" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c3" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c4" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c5" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c6" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c7" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c8" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-hide="row.TripCodeInd==3" ng-model="row.c9" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.c11" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.c12" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="number" ng-change="action.changedRow(row);" style="width:100px" ng-model="row.c10" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            {{action.isLocked(row)}}
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            {{action.isPaid(row)}}
                        </div>
                    </div>
                </div>
            <%--<div class="footer">
                    <bx-pager f-data="pager"></bx-pager>
                </div>--%>
        </div>
    </div>
    <div class="footer">
        <bx-pager f-data="pager"></bx-pager>
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
