<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RescheduleTrips.aspx.cs" Inherits="PagesContTrucking_Daily_RescheduleTrips" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
                    { id: 0, c: '', n: 'Empty' },
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
                    //{ id: 7, c: 'Completed', n: 'Completed' },
                ]
            };
            var vm = {
                init: function () {
                    SV_Common.setWebService('WebService_ScheduleTrip.asmx');
                    data.search = {
                        From: new Date(),
                        To: new Date(),
                        ContNo: '',
                    };
                    data.search.From.setDate(data.search.From.getDate() - 3);
                    data.search.To.setDate(data.search.To.getDate()-1);
                    //vm.refresh_client();
                },
                GetData: function () { return data; },
                refresh: function (par, cb) {



                    var pars = angular.copy(data.search);
                    pars.From = moment(pars.From).format('YYYYMMDD');
                    pars.To = moment(pars.To).format('YYYYMMDD');
                    pars.curPage = data.pager.curPage;
                    pars.pageSize = data.pager.pageSize;

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
                                //if (row.TripCodeInd != data.tripIndex[currentInd].id) {
                                //    currentInd = row.TripCodeInd;
                                //    data.list.push({ title: data.tripIndex[currentInd].n, isTitle: true });
                                //}
                                //data.list.push(row);
                                row.BookingDate1 = new Date(row.BookingDate);
                                row.BookingTime1 = new Date(row.BookingDate + ' ' + row.BookingTime);
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
                download_excel: function (par, cb) {
                    var pars = angular.copy(data.search);
                    pars.From = moment(pars.From).format('YYYYMMDD');
                    
                    var func = "/download_excel";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                reschedule: function (par, cb) {

                    var func = "/reschedule";
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
                                parent.notice('Refresh Successful');
                            } else {
                                parent.notice('Refresh False','','error');
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
                        if (row.JobType == 'IMP' || row.JobType == 'EXP') {
                            parent.navTab.openTab(row.JobNo1, "/PagesContTrucking/Job/JobEdit.aspx?no=" + row.JobNo1, { title: row.JobNo1, fresh: false, external: true });
                        }
                        if (row.JobType == 'WDO' || row.JobType == 'WGR' || row.JobType == 'TPT') {
                            parent.navTab.openTab(row.JobNo1, "/PagesContTrucking/Job/JobEdit.aspx?no=" + row.JobNo1, { title: row.JobNo1, fresh: false, external: true });
                        }
                        if (row.JobType == 'CRA') {
                            parent.navTab.openTab(row.JobNo1, "/PagesContTrucking/Job/JobEdit.aspx?no=" + row.JobNo1, { title: row.JobNo1, fresh: false, external: true });
                        }
                    },
                    schedule: function (row) {
						var temp={};
						temp.Id = row.Id;
						temp.ScheduleDate = moment(row.BookingDate1).format('YYYYMMDD');
						temp.ScheduleTime = moment(row.BookingTime1).format('HH:mm');
						SV_DailyTrips.reschedule(temp, $scope.action.schedule_callback);
                    },
                    schedule_callback: function (par) {
                        console.log(par);
                        if (par.status == "1") {
                            //window.location.href = "../../" + par.context;
                            $scope.action.refresh();
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
                    <div>Date From</div>
                    <div>
                        <input type="date" ng-model="vm.search.From" class="single_date_140" />
                    </div>
                    <div>To</div>
                    <div>
                        <input type="date" ng-model="vm.search.To" class="single_date_140" />
                    </div>
                    <div>Master/JobNo/ContNo</div>
                    <div>
                        <input type="text" ng-model="vm.search.ContNo" placeholder="(end with)"  class="single_text" />
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
                    <div class="col_120">#</div>
                    <div>ReScheduleDate</div>
                    <div>Time</div>
                    <div class="col_150">ReScheduleDate</div>
                    <div class="col_150">OrderNo</div>
                    <div>ContainerNo/ChessisType</div>
                    <div class="col_150">Escort</div>
                    <%--<div>Pickup Time</div>--%>
                    <div>Prime Mover</div>
                    <div>Trailer No</div>
                    <div>Incentive</div>
                    <div>Owner Code</div>
                    <div>Seal No</div>
                    <div class="col_150">From Location ID</div>
                    <div class="col_150">To Location ID</div>
                    <div>Container Size</div>
                    <div>OP</div>
                    <div class="col_150">Vessel Name/Voyage No</div>
                    <div>Eta Date</div>
                    <div>Eta Time</div>
                    <div class="col_150">Permit No./Booking Ref</div>
                </div>
                <div class="body">
                    <div class="item" ng-repeat="row in vm.list" ng-hide="row.isTitle&&(!vm.list[$index+1]||vm.list[$index+1].isTitle)">
                        <div ng-if="row.isTitle" style="font-weight: 600;font-size:12px;padding:8px;">{{row.title}}</div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <div ng-if="row.isTitle" style="border-left: 0px;"></div>
                        <%--<div ng-if="row.isTitle" style="border-left: 0px;"></div>--%>
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
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="button" class="button single_button_110" value="Reschedule" ng-show="row.changed" ng-click="action.schedule(row);" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="date" ng-model="row.BookingDate1" ng-change="action.changedRow(row);" />
                        </div>
                        <div ng-if="!row.isTitle" ng-class="{'true':'changed','false':''}[row.changed]">
                            <input type="time" ng-model="row.BookingTime1" ng-change="action.changedRow(row);" />
                        </div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.BookingDate}}&nbsp;{{row.BookingTime}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]"><a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a></div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.ContainerNo}}/{{row.RequestTrailerType}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.Escort_Remark}}</div>
                        <%--<div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.FromTime}}</div>--%>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.TowheadCode}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.ChessisCode}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.Incentive|number:2}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.ClientId}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.SealNo}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.FromCode}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.ToCode}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.ContainerType}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.OperatorCode}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.Vessel}}/{{row.Voyage}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.EtaDate|date:'dd/MM/yyyy'}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.EtaTime}}</div>
                        <div ng-if="!row.isTitle"  ng-class="{'true':'changed','false':''}[row.changed]">{{row.PermitNo}}/{{row.CarrierBkgNo}}</div>
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
