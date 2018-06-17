<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyTrips.aspx.cs" Inherits="PagesContTrucking_Daily_DailyTrips" %>

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
                    pageSize: '100',
                },
                client: {
                    title: 'Client',
                },
                curView: {},
                downloadOptions: [
                    { c: 'BookingDate', t: 'Schedule Date', s: true },
                    { c: 'JobNo', t: 'OrderNo', s: true },
                    { c: 'ManualDO', t: 'Do No', s: true },
                    { c: 'MasterJobNo', t: 'MasterJobNo', s: true },
                    { c: 'Escort_Remark', t: 'Escort', s: true },
                    { c: 'TripInstruction', t: 'Trip Instruction', s: true },
                    { c: 'BookingTime', t: 'Schedule Time', s: true },
                    { c: 'TowheadCode', t: 'Prime Mover', s: true },
                    { c: 'ChessisCode', t: 'Trailer No', s: true },
                    { c: 'Incentive', t: 'Incentive', s: true },
                    { c: 'TotalClaim', t: 'Total Claim', s: true },
                    { c: 'ClientId', t: 'Owner Code', s: true },
                    { c: 'SP_ContainerNo', t: 'ContainerNo/DC/ChessisType', s: true },
                    { c: 'SealNo', t: 'Seal No', s: true },
                    { c: 'FromCode', t: 'From Location ID', s: true },
                    { c: 'ToCode', t: 'To Location ID', s: true },
                    { c: 'ContainerType', t: 'Container Size', s: true },
                    { c: 'OperatorCode', t: 'Job OP', s: true },
                    { c: 'Vessel/Voyage', t: 'Vessel Name/Voyage No', s: true },
                    { c: 'EtaDate', t: 'Eta Date', s: true },
                    { c: 'EtaTime', t: 'Eta Time', s: true },
                    { c: 'ReturnLastDate', t: 'Return Last Date', s: true },
                    { c: 'PermitNo/CarrierBkgNo', t: 'Permit No./Booking Ref', s: true },
                    { c: 'OpsType', t: 'CargoOP', s: true },
                    { c: 'Stuff_Ind', t: 'Un/Stuffing', s: true },
                    { c: 'UnClosed', t: 'UnClosed', s: true },
                    { c: 'UnBilled', t: 'UnBilled', s: true },
                    { c: 'WarehouseRemark', t: 'WarehouseRemark', s: true },
                ],
                //============= in download_excel must mach following list
                tripIndex: [
                    //{ id: 0, c: '', n: 'Empty' },
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
                    SV_Common.setWebService('WebService_DailyTrip.asmx');
                    data.search = {
                        From: new Date(),
                        To: new Date(),
                        ContNo: '',
                        Client: '',
                        ClientName:'',
                    };
                    //data.search.From.setDate(data.search.From.getDate() - 10);
                    vm.refresh_client();
                },
                GetData: function () { return data; },
                refresh: function (par, cb) {
                    var pars = angular.copy(data.search);
                    pars.From = moment(pars.From).format('YYYYMMDD');
                    pars.To = moment(pars.To).add(1, 'days').format('YYYYMMDD');
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
                            var currentInd = 0;
                            //angular.forEach(temp, function (row) {
                            //    if (row.TripCodeInd != data.tripIndex[currentInd].id) {
                            //        currentInd = row.TripCodeInd;
                            //        data.list.push({ title: data.tripIndex[currentInd].n, isTitle: true });
                            //    }
                            //    data.list.push(row);
                            //});
                            angular.forEach(data.tripIndex, function (titleRow, index) {
                                if (index > 0) {
                                    data.list.push({ title: titleRow.n, isTitle: true });
                                    for (; currentInd < temp.length; currentInd++) {
                                        if (temp[currentInd].TripCodeInd != titleRow.id) {
                                            break;
                                        }
                                        //if (titleRow.c == 'LOC') {
                                        //    if (!data.list[data.list.length - 1].isTitle) {
                                        //        if (data.list[data.list.length - 1].TripJobType != temp[currentInd].TripJobType) {
                                        //            data.list.push({ title: '', isTitle: true });
                                        //        }
                                        //    }
                                        //}
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
                        console.log(res);
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                download_excel_option: function (par, cb) {
                    var pars = angular.copy(data.search);
                    pars.DateHeader = moment(pars.From).format("YYYY/MM/DD") + " - " + moment(pars.To).format('YYYY/MM/DD');
                    pars.From = moment(pars.From).format('YYYYMMDD');
                    pars.To = moment(pars.To).add(1, 'days').format('YYYYMMDD');
                    pars.options = data.downloadOptions;

                    var func = "/download_excel_option";
                    SV_Common.http(func, pars, function (res) {
                        console.log(res);
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                refresh_client: function (par, cb) {
                    var pars = {};
                    var func = "/MasterData_Client";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            data.client.list = res.context;
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
                                parent.notice('Refresh Successful');
                            } else {
                                parent.notice('Refresh False', '', 'error');
                            }
                        });
                    },
                    viewRow: function (row) {
                        $scope.floatView.show(row);
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
                    download: function () {
                        SV_DailyTrips.download_excel(null, $scope.action.download_callback);
                    },
                    download_callback: function (par) {
                        console.log(par);
                        if (par.status == "1") {
                            window.location.href = "../../" + par.context;
                        }
                    },
                    download_option: function () {
                        var cc = false;
                        for (var i = 0; i < $scope.vm.downloadOptions.length; i++) {
                            if ($scope.vm.downloadOptions[i].s) {
                                cc = true;
                                break;
                            }
                        }
                        if (cc) {
                            SV_DailyTrips.download_excel_option(null, $scope.action.download_callback);
                        } else {
                            alert('Request choose some column!');
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
                SV_Popup.SetPopup($scope.masterData, 'center');


                $scope.downloadExcel = {
                    data: {
                        list: [],
                        no: '',
                        title: 'Download Options',
                        selectCallback: null,
                    },
                    show: function (dd, cb) {
                        $scope.downloadExcel.popup.show($scope.downloadExcel);
                    },
                    hide: function () {
                        //$scope.downloadExcel.data = {};

                        $scope.downloadExcel.popup.hide($scope.downloadExcel);
                    },
                    select: function (row) {
                        if (row.s) {
                            row.s = false;
                        } else {
                            row.s = true;
                        }
                    },
                    selectAll: function () {
                        angular.forEach($scope.vm.downloadOptions, function (row) {
                            row.s = true;
                        });
                    },
                    unSelectAll: function () {
                        angular.forEach($scope.vm.downloadOptions, function (row) {
                            row.s = false;
                        });
                    }
                }
                SV_Popup.SetPopup($scope.downloadExcel, 'right');



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
</head>
<body ng-controller="Ctr_DailyTrips">
    <form name="f_search" ng-submit="action.refresh();">
        <div class="bx_table">
            <div class="body">
                <div class="item item_empty">
                    <div>Date</div>
                    <div>
                        <input type="date" ng-model="vm.search.From" class="single_date_140" />
                    </div>
                    <div>To</div>
                    <div>
                        <input type="date" ng-model="vm.search.To" class="single_date_140" />
                    </div>
                    <div>Client</div>
                    <div style="width:200px;">
                        <div class="single_buttonselect_full">
                            <div class="code0">
                                <input type="text" ng-model="vm.search.Client" class="single_text" />
                                <a class="bx_a_button" ng-click="action.selectClient();">&nbsp;.. </a>
                            </div>
                            <input type="text" ng-model="vm.search.ClientName" class="code1" readonly />
                        </div>
                    </div>
                    <div>Mast/Job/ContNo</div>
                    <div>
                        <input type="text" ng-model="vm.search.ContNo" placeholder="(skip other option)" class="single_text" />
                    </div>
                    <div>
                        <input type="submit" value="Retrieve" class="single_button_110 button" />
                    </div>
                    <%--<div>
                        <input type="button" ng-click="action.download();" value="Save To Excel" class="single_button_110 button" />
                    </div>--%>
                    <div>
                        <input type="button" ng-click="downloadExcel.show();" value="Save To Excel" class="single_button_110 button" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="joblist_content joblist_content_top35 has_footer">
        <div class="joblist_content_list">
            <div class="bx_table">
                <div class="header">
                    <div class="col_150">Schedule Date</div>
                    <div class="col_150">OrderNo</div>
                    <div class="col_150">Do No</div>
                    <div class="col_150">MasterJobNo</div>
                    <div class="col_150">Escort</div>
                    <div class="col_150">Trip Instruction</div>
                    <div>Schedule Time</div>
                    <div>Prime Mover</div>
                    <div>Trailer No</div>
                    <div>Incentive</div>
                    <div>Total Claim</div>
                    <div>Owner Code</div>
                    <div>ContainerNo/DC/ChessisType</div>
                    <div>Seal No</div>
                    <div class="col_150">From Location ID</div>
                    <div class="col_150">To Location ID</div>
                    <div>Container Size</div>
                    <div>Job OP</div>
                    <div class="col_150">Vessel Name/Voyage No</div>
                    <div>Eta Date</div>
                    <div>Eta Time</div>
                    <div>Return Last Date</div>
                    <div class="col_150">Permit No./Booking Ref</div>
                    <div>CargoOP</div>
                    <div>Un/Stuffing</div>
                    <div>UnClosed</div>
                    <div>UnBilled</div>
                    <div class="col_150">WarehouseRemark</div>
                </div>
                <div class="body">
                    <div class="item" ng-repeat="row in vm.list">
                        <div ng-if="row.isTitle" style="font-weight: 600; font-size: 12px; padding: 8px;">{{row.title}}</div>
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
                        <div ng-if="!row.isTitle" style="width: 120px;">{{row.BookingDate|date:'dd/MM/yyyy'}}</div>
                        <div ng-if="!row.isTitle"><a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a></div>
                        <div ng-if="!row.isTitle">{{row.ManualDO}}</div>
                        <div ng-if="!row.isTitle">{{row.MasterJobNo}}</div>
                        <div ng-if="!row.isTitle">{{row.Escort_Remark}}</div>
                        <div ng-if="!row.isTitle" style="font-weight:bold;color:red">{{row.TripInstruction}}</div>
                        <div ng-if="!row.isTitle">{{row.BookingTime}}</div>
                        <div ng-if="!row.isTitle">{{row.TowheadCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ChessisCode}}</div>
                        <div ng-if="!row.isTitle">{{row.Incentive|number:2}}</div>
                        <div ng-if="!row.isTitle">{{row.TotalClaim|number:2}}</div>
                        <div ng-if="!row.isTitle">{{row.ClientId}}</div>
                        <div ng-if="!row.isTitle">{{row.ContainerNo}}/{{row.DischargeCell}}/{{row.showCT==1?row.RequestVehicleType:row.RequestTrailerType}}</div>
                        <div ng-if="!row.isTitle">{{row.SealNo}}</div>
                        <div ng-if="!row.isTitle">{{row.FromCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ToCode}}</div>
                        <div ng-if="!row.isTitle">{{row.ContainerType}}</div>
                        <div ng-if="!row.isTitle">{{row.OperatorCode}}</div>
                        <div ng-if="!row.isTitle">{{row.Vessel}}/{{row.Voyage}}</div>
                        <div ng-if="!row.isTitle">{{row.EtaDate}}</div>
                        <div ng-if="!row.isTitle">{{row.EtaTime}}</div>
                        <div ng-if="!row.isTitle">{{row.ReturnLastDate|date:'dd/MM/yyyy'}}</div>
                        <div ng-if="!row.isTitle">{{row.PermitNo}}/{{row.CarrierBkgNo}}</div>
                        <div ng-if="!row.isTitle">{{row.OpsType}}</div>
                        <div ng-if="!row.isTitle">{{row.Stuff_Ind}}</div>
                        <div ng-if="!row.isTitle">{{row.UnClosed}}</div>
                        <div ng-if="!row.isTitle">{{row.UnBilled}}</div>
                        <div ng-if="!row.isTitle">{{row.WarehouseRemark}}</div>
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
                        <div class="col_50">Code (Top 100)</div>
                        <div>Name</div>
                    </div>
                    <div class="body">
                        <div class="item" ng-repeat="row in masterData.data.list|filter:masterData.data.no|limitTo:100" ng-click="masterData.select(row);">
                            <div>&nbsp;{{row.c}}</div>
                            <div>{{row.n}}</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="float_window " ng-class="downloadExcel.popup.cssList[downloadExcel.popup.curCss]">
        <div class="content">
            <div class="header">
                {{downloadExcel.data.title}}
                <button class="header_button" ng-click="downloadExcel.hide();">X</button>
            </div>
            <div class="body">
                <div class="bx_table">
                    <div class="body">
                        <div class="item item_empty">
                            <div>
                                <input type="button" ng-click="downloadExcel.selectAll();" value="Select All" class="single_button_110 button" />
                            </div>
                            <div>
                                <input type="button" ng-click="downloadExcel.unSelectAll();" value="UnSelect All" class="single_button_110 button" />
                            </div>
                            <div>
                                <input type="button" ng-click="action.download_option();" value="Save To Excel" class="single_button_110 button" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bx_table bx_table_100pc">
                    <div class="header">
                        <div class="col_50">#</div>
                        <div>Option</div>
                    </div>
                    <div class="body">
                        <div class="item" ng-repeat="row in vm.downloadOptions" ng-click="downloadExcel.select(row);">
                            <div>
                                <input type="checkbox" ng-model="row.s" ng-click="downloadExcel.select(row);" />
                            </div>
                            <div>&nbsp;{{row.t}}</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
