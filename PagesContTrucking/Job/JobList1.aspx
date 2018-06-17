<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobList1.aspx.cs" Inherits="PagesContTrucking_Job_JobList1" %>

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
        app.factory('SV_List', function ($http, SV_Common) {
            var data = {
                list: [],
                search: {},
                pager: {
                    totalItems: 0,
                    totalPages: 10,
                    curPage: 0,
                    pageSize: '20',
                },
                client: {
                    title: 'Client',
                },
                curView: {},
            };
            var vm = {
                init: function () {
                    SV_Common.setWebService('WebService_JobList.asmx');
                    data.search = {
                        From: new Date(),
                        To: new Date(),
                        ContNo: '',
                        ContStauts_0: true,
                        ContStauts_1: true,
                        ContStauts_2: false,
                        JobNo: '',
                        JobType: '',
                        Vessel: '',
                        Client: '',
                        ClientName: '',
                    };
                    data.search.From.setDate(data.search.From.getDate() - 10);
                    vm.refresh_client();
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
                            data.list = res.context.list;
                            data.pager.curPage = res.context.curPage;
                            data.pager.totalPages = res.context.totalPages;
                            data.pager.totalItems = res.context.totalItems;
                            angular.forEach(data.list, function (row) {
                                var str_trips = row.trips;
                                row.trips = [];
                                if (str_trips && str_trips.length > 0) {
                                    str_trips = str_trips.substring(0, str_trips.length - 1);
                                    var ar_trips = str_trips.split(',');
                                    angular.forEach(ar_trips, function (t) {
                                        str_trips = t.split('|');
                                        if (str_trips.length == 2) {
                                            row.trips.push({ c: str_trips[0], t: str_trips[1] });
                                        }
                                    })
                                }
                            });
                        }
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
                getView: function (par, cb) {
                    var pars = par;
                    var func = "/ContainerView_GetData";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            data.curView = res.context;
                            //console.log(data.curView);
                            angular.forEach(data.curView.trips, function (row) {
                                var fd = moment(row.FromDate);
                                row.FromDate1 = new Date(fd.year(), fd.month(), fd.date());
                            });
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                saveView: function (par, cb) {
                    var pars = data.curView.mast;
                    var func = "/ContainerView_Save";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            vm.refresh();
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                saveView_newTrip: function (par, cb) {
                    var pars = angular.copy(data.curView.mast);
                    pars.NewType = par;
                    var func = "/ContainerView_AddNewTrip";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            vm.refresh();
                            vm.getView(data.curView.mast);
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                saveTrip: function (par, cb) {
                    var pars = par;
                    par.FromDate = moment(par.FromDate1).format('YYYYMMDD');
                    var func = "/ContainerView_SaveTrip";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            vm.refresh();
                            vm.getView(data.curView.mast);
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                AddNewJob: function (par, cb) {
                    var pars = angular.copy(par);
                    pars.JobDate = moment(pars.JobDate).format('YYYYMMDD');
                    var func = "/AddNewJob";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            vm.refresh();
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
            .controller('Ctr_List', function ($scope, SV_List, $timeout, SV_Popup) {

                $scope.vm = SV_List.GetData();
                $scope.action = {
                    refresh: function () {
                        SV_List.refresh(null, function (res) {
                            //console.log('=============', $scope.vm);
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
                    click_contStatusLabel: function (par) {
                        switch (par) {
                            case 'New':
                                $scope.vm.search.ContStauts_0 = !$scope.vm.search.ContStauts_0;
                                break;
                            case 'InTransit':
                                $scope.vm.search.ContStauts_1 = !$scope.vm.search.ContStauts_1;
                                break;
                            case 'Completed':
                                $scope.vm.search.ContStauts_2 = !$scope.vm.search.ContStauts_2;
                                break;
                        }
                    },
                    addNewJob: function () {
                        $scope.newJob.show();
                    },
                    openTabJob: function (JobNo) {
                        parent.navTab.openTab(JobNo, "/PagesContTrucking/Job/JobEdit.aspx?jobNo=" + JobNo, { title: JobNo, fresh: false, external: true });
                    }
                }

                //======================== view detail
                $scope.floatView = {
                    data: {},
                    show: function (row) {
                        //console.log('===============', row);
                        $scope.floatView.data = row;
                        //var temp = {};
                        //temp.ContId = row.ContId;
                        //temp.JobNo = row.JobNo;
                        SV_List.getView(row, $scope.floatView.refresh_callback);
                        $scope.floatView.popup.show($scope.floatView);
                    },
                    hide: function () {
                        $scope.floatView.data = {};
                        $scope.floatView.popup.hide($scope.floatView);
                    },
                    refresh_callback: function (res) {

                    },
                    save: function () {
                        SV_List.saveView(null, $scope.floatView.save_callback);
                    },
                    save_callback: function (res) {
                        if (res.status == '1') {
                            parent.notice("Save Success", null, 'success');
                        } else {
                            parent.notice("Save Error", null, 'error');
                        }
                    },
                    addTrip: function (par) {
                        SV_List.saveView_newTrip(par, $scope.floatView.addTrip_callback);
                    },
                    addTrip_callback: function (res) {
                        if (res.status == '1') {
                            parent.notice("Add Trip Success", null, 'success');
                        } else {
                            parent.notice("Add Trip Error", null, 'error');
                        }
                    },
                    saveTrip: function (par) {
                        SV_List.saveTrip(par, $scope.floatView.saveTrip_callback);
                    },
                    saveTrip_callback: function (res) {
                        if (res.status == '1') {
                            parent.notice("Save Trip Success", null, 'success');
                        } else {
                            parent.notice("Save Trip Error", null, 'error');
                        }
                    }
                }
                SV_Popup.SetPopup($scope.floatView, 'rightLarge');

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

                //========================== new job
                $scope.newJob = {
                    data: {
                        title: 'New Job',
                        job: {},
                    },
                    show: function (dd, cb) {
                        $scope.newJob.initData();
                        $scope.newJob.popup.show($scope.newJob);
                    },
                    hide: function () {
                        $scope.action.refresh();
                        $scope.newJob.popup.hide($scope.newJob);
                    },
                    initData: function () {
                        var temp = {
                            JobDate: new Date(),
                            JobType: "",
                            Shipper: '',
                            Client: '',
                            ClientName: '',
                            FromAddress: '',
                            ToAddress: '',
                            DepotAddress: '',
                            Remark: '',
                        };
                        $scope.newJob.data.job = temp;
                    },
                    selectClient: function () {
                        $scope.masterData.show($scope.vm.client, $scope.newJob.selectClient_callback);
                    },
                    selectClient_callback: function (res) {
                        //console.log(res);
                        $scope.newJob.data.job.Client = res.c;
                        $scope.newJob.data.job.ClientName = res.n;
                    },
                    save: function () {
                        SV_List.AddNewJob($scope.newJob.data.job, $scope.newJob.save_callback)
                    },
                    save_callback: function (res) {
                        if (res.status == '1') {
                            parent.notice("Add New Job Success", null, 'success');
                            $scope.action.openTabJob(res.context);
                        } else {
                            parent.notice("Add New Job Error", res.context, 'error');
                        }
                    }
                }
                SV_Popup.SetPopup($scope.newJob, 'right');


                $scope.action.refresh();

                $scope.pager = {
                    //===========data.totalItems: 0,
                    //===========data.totalPages: 10,
                    //===========data.curPage: 0,
                    //===========data.pageSize: '15',
                    data: SV_List.GetData().pager,
                    refresh: function () {
                        $scope.action.refresh();
                        //console.log('============ refresh');
                    }
                }
            })
    </script>
</head>
<body ng-controller="Ctr_List">
    <form name="f_search" ng-submit="action.refresh();">
        <div class="bx_table">
            <div class="body">
                <div class="item item_empty">
                    <div>
                        ContNo
                    </div>
                    <div>
                        <input type="text" ng-model="vm.search.ContNo" class="single_text" />
                    </div>
                    <div>From</div>
                    <div>
                        <input type="date" ng-model="vm.search.From" class="single_date" />
                    </div>
                    <div>To</div>
                    <div>
                        <input type="date" ng-model="vm.search.To" class="single_date" />
                    </div>
                    <div>
                        ContStatus
                    </div>
                    <div>
                        <input type="checkbox" ng-model="vm.search.ContStauts_0" /><label ng-click="action.click_contStatusLabel('New');">&nbsp;New</label>
                        <input type="checkbox" ng-model="vm.search.ContStauts_1" /><label ng-click="action.click_contStatusLabel('InTransit');">&nbsp;InTransit</label>
                        <input type="checkbox" ng-model="vm.search.ContStauts_2" /><label ng-click="action.click_contStatusLabel('Completed');">&nbsp;Completed</label>
                    </div>
                </div>
                <div class="item item_empty">
                    <div>JobNo</div>
                    <div>
                        <input type="text" ng-model="vm.search.JobNo" class="single_text" />
                    </div>
                    <div>JobType</div>
                    <div>
                        <select ng-model="vm.search.JobType" class="single_select">
                            <option value="">ALL</option>
                            <option value="IMP">IMP</option>
                            <option value="EXP">EXP</option>
                            <option value="LOC">LOC</option>
                        </select>
                    </div>
                    <div>Vessel</div>
                    <div>
                        <input type="text" ng-model="vm.search.Vessel" class="single_text" />
                    </div>
                    <div>Client</div>
                    <div>
                        <%--<input type="text" ng-model="vm.search.Client" class="single_text_small" />
                        <a class="bx_a_button" ng-click="action.selectClient();">&nbsp;.. </a>
                        <input type="text" ng-model="vm.search.ClientName" class="single_text" readonly />--%>

                        <div class="single_buttonselect_full">
                            <div class="code0">
                                <input type="text" ng-model="vm.search.Client" class="single_text_small" />
                                <a class="bx_a_button" ng-click="action.selectClient();">&nbsp;.. </a>
                            </div>
                            <input type="text" ng-model="vm.search.ClientName" class="code1" readonly />
                        </div>
                    </div>
                    <div>
                        <input type="submit" value="Retrieve" class="single_button button" />
                    </div>
                    <div>
                        <input type="button" ng-click="action.addNewJob();" value="Add New" class="single_button button" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="joblist_content joblist_content_top60 has_footer">
        <div class="joblist_content_list">
            <div class="bx_table">
                <div class="header">
                    <div>JobNo</div>
                    <div>JobType</div>
                    <div class="col_50">Client</div>
                    <div class="col_60">JobStatus</div>
                    <div>ScheduleDate</div>
                    <div>ContainerNo</div>
                    <div class="col_50">OP</div>
                    <div class="col_50">Email</div>
                    <div class="col_50">Urgent</div>
                    <div>SealNo</div>
                    <div class="col_50">J5</div>
                    <div class="col_50">MT</div>
                    <div>Link Trips</div>
                    <div>ContType</div>
                    <div>ContStatus</div>
                    <div class="col_50">Hours</div>
                    <div class="col_60">Portnet</div>
                    <div class="col_60">Vessel</div>
                    <div class="col_60">Voyage</div>
                    <div>REFNO</div>
                    <div>ETA</div>
                    <div class="col_50">TT</div>
                    <div class="col_150">From</div>
                    <div class="col_150">To</div>
                    <div class="col_150">Deport</div>
                    <div>PermitNo</div>
                    <div>Terminal</div>
                    <div>ClientRefNo</div>
                    <div>JobDate</div>
                </div>
                <div class="body">
                    <div class="item" ng-class="{true:'item_selected',false:''}[row.Id==floatView.data.Id&&row.ContId==floatView.data.ContId]" ng-repeat="row in vm.list" ng-click="action.viewRow(row);">
                        <div>{{row.JobNo}}</div>
                        <div>{{row.JobType}}</div>
                        <div>{{row.client}}</div>
                        <div>{{row.JobStatus}}</div>
                        <div>{{row.ScheduleDate|date:'dd/MM/yyyy'}}</div>
                        <div>{{row.ContainerNo}}</div>
                        <div>{{row.OperatorCode}}</div>
                        <div>
                            <div class="col_box" ng-class="{'Y':'col_box_green'}[row.EmailInd]">{{row.EmailInd=='Y'?'@':' '}}</div>
                        </div>
                        <div>
                            <div class="col_box" ng-class="{'Y':'col_box_red'}[row.UrgentInd]">{{row.UrgentInd=='Y'?'Y':' '}}</div>
                        </div>
                        <div>{{row.SealNo}}</div>
                        <div>
                            <div class="col_box" ng-class="{'Y':'col_box_red'}[row.F5Ind]">{{row.F5Ind=='Y'?'Y':' '}}</div>
                        </div>
                        <div>{{row.WarehouseStatus}}</div>
                        <div>
                            <div class="col_boxset">
                                <div class="col_box_small" ng-repeat="trip in row.trips" ng-class="{'P':'col_box_orange','S':'col_box_green','C':'col_box_blue','X':'col_box_gray'}[trip.t]">{{trip.c}}</div>
                            </div>
                        </div>
                        <div>{{row.ContainerType}}</div>
                        <div>
                            <div class="col_box_large " ng-class="{'New':'col_box_orange','InTransit':'col_box_green','Completed':'col_box_blue'}[row.StatusCode]">{{row.StatusCode}}</div>
                        </div>
                        <div>
                            <div class="col_box" ng-class="{true:'col_box_red'}[row.hr>72]">{{row.hr}}</div>
                        </div>
                        <div>{{row.PortnetStatus}}</div>
                        <div>{{row.Vessel}}</div>
                        <div>{{row.Voyage}}</div>
                        <div>{{row.CarrierBkgNo}}</div>
                        <div>{{row.EtaDate|date:'dd/MM/yyyy'}}</div>
                        <div>{{row.EtaTime}}</div>
                        <div>{{row.PickupFrom}}</div>
                        <div>{{row.DeliveryTo}}</div>
                        <div>{{row.Depot}}</div>
                        <div>{{row.PermitNo}}</div>
                        <div>{{row.Terminalcode}}</div>
                        <div>{{row.ClientRefNo}}</div>
                        <div>{{row.JobDate|date:'dd/MM/yyyy'}}</div>
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

    <table>
        <tr>
            <td colspan></td>
        </tr>
    </table>

    <div class="float_window" ng-class="floatView.popup.cssList[floatView.popup.curCss]">
        <div class="content">
            <div class="header">
                {{floatView.data.JobNo}}
                <%--<button class="header_button_left" ng-click="floatView.toggle();">X</button>--%>
                <button class="header_button" ng-click="floatView.hide();">X</button>
                <%--<button class="header_button green" ng-click="floatView.hide();">Save</button>--%>
            </div>
            <div class="body">
                <%--<div ng-show="!floatView.data.ContId||floatView.data.ContId==0">
                    <input type="button" class="button" value="OpenJob" />
                </div>--%>
                <div>
                    <input type="button" class="button single_button" value="Save" ng-click="floatView.save();" />
                    <input type="button" class="button single_button" value="OpenJob" ng-click="action.openTabJob(vm.curView.mast.JobNo);" />
                    <input type="button" class="button single_button" value="Email" />
                    <div class="bx_table bx_table_100pc">
                        <div class="body">
                            <div class="item item_empty">
                                <div>
                                    ContainerNo:<br />
                                    <input type="text" ng-model="vm.curView.mast.ContainerNo" class="single_text_full" />
                                </div>
                                <div>
                                    SealNo:<br />
                                    <input type="text" ng-model="vm.curView.mast.SealNo" class="single_text_full" />
                                </div>
                            </div>
                            <div class="item item_empty">
                                <div>
                                    From:<br />
                                    <textarea ng-model="vm.curView.mast.PickupFrom" class="single_textarea_full"></textarea>
                                </div>
                                <div>
                                    To:<br />
                                    <textarea ng-model="vm.curView.mast.DeliveryTo" class="single_textarea_full"></textarea>
                                </div>
                            </div>
                            <div class="item item_empty">
                                <div>
                                    Depot:<br />
                                    <textarea ng-model="vm.curView.mast.YardRef" class="single_textarea_full"></textarea>
                                </div>
                                <div>
                                    Permit:<br />
                                    <textarea ng-model="vm.curView.mast.PermitNo" class="single_textarea_full"></textarea>
                                </div>
                            </div>
                            <div class="item item_empty">
                                <div>
                                    Special Instr:<br />
                                    <textarea ng-model="vm.curView.mast.SpecialInstruction" class="single_textarea_full"></textarea>
                                </div>
                                <div>
                                    Remark:<br />
                                    <textarea ng-model="vm.curView.mast.Remark" class="single_textarea_full"></textarea>
                                </div>
                            </div>
                            <div class="item item_empty">
                                <div>
                                    DG/J5:<br />
                                    <select ng-model="vm.curView.mast.F5Ind" class="single_select_full">
                                        <option value="Y">Y</option>
                                        <option value="N">N</option>
                                    </select>
                                </div>
                                <div>
                                    Portnet:<br />
                                    <select ng-model="vm.curView.mast.PortnetStatus" class="single_select_full">
                                        <option value=""></option>
                                        <option value="Created">Created</option>
                                        <option value="Released">Released</option>
                                    </select>
                                </div>
                            </div>
                            <div class="item item_empty">
                                <div>
                                    MT/LDN:<br />
                                    <select ng-model="vm.curView.mast.WarehouseStatus" class="single_select_full">
                                        <option value=""></option>
                                        <option value="Empty">Empty</option>
                                        <option value="Laden">Laden</option>
                                    </select>
                                </div>
                                <div>
                                    Email Sent:<br />
                                    <select ng-model="vm.curView.mast.EmailInd" class="single_select_full">
                                        <option value="Y">Y</option>
                                        <option value="N">N</option>
                                    </select>
                                </div>
                            </div>
                            <div class="item item_empty">
                                <div>
                                    Urgent Job:<br />
                                    <select ng-model="vm.curView.mast.UrgentInd" class="single_select_full">
                                        <option value="Y">Y</option>
                                        <option value="N">N</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <input type="button" class="button" value="COL" ng-click="floatView.addTrip('COL');" />
                    <input type="button" class="button" value="EXP" ng-click="floatView.addTrip('EXP');" />
                    <input type="button" class="button" value="IMP" ng-click="floatView.addTrip('IMP');" />
                    <input type="button" class="button" value="RET" ng-click="floatView.addTrip('RET');" />
                    <input type="button" class="button" value="SHF" ng-click="floatView.addTrip('SHF');" />
                    <input type="button" class="button" value="LOC" ng-click="floatView.addTrip('LOC');" />
                    <div class="bx_table bx_table_100pc bx_table_nofocus_row">
                        <div class="header">
                            <div>Information</div>
                            <div>#</div>
                        </div>
                        <div class="body">
                            <div class="item" ng-repeat="row in vm.curView.trips">
                                <div>
                                    <div class="bx_table bx_table_100pc">
                                        <div class="item item_empty">
                                            <div>Status:</div>
                                            <div class="col_200">{{row.Statuscode}}</div>
                                        </div>
                                        <div class="item item_empty">
                                            <div>Type:</div>
                                            <div class="col_200">{{row.TripCode}}</div>
                                        </div>
                                        <div class="item item_empty">
                                            <div>Destination:</div>
                                            <div class="col_200">{{row.ToCode}}</div>
                                        </div>
                                        <div class="item item_empty">
                                            <div>Date:</div>
                                            <div>{{row.FromDate|date:'dd/MM'}}&nbsp;{{row.FromTime}}-{{row.ToTime}}</div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div class="bx_table bx_table_100pc">
                                        <div class="item item_empty">
                                            <div>
                                                Driver:<br />
                                                <input type="text" ng-model="row.DriverCode" class="single_text_full" />
                                            </div>
                                            <div>
                                                Trailer:<br />
                                                <input type="text" ng-model="row.ChessisCode" class="single_text_full" />
                                            </div>
                                        </div>
                                        <div class="item item_empty">
                                            <div>
                                                ParkingLot:<br />
                                                <input type="text" ng-model="row.ToParkingLot" class="single_text_full" />
                                            </div>
                                            <div>
                                                FromDate:<br />
                                                <input type="date" ng-model="row.FromDate1" class="single_text_full" />
                                            </div>
                                        </div>
                                        <div class="item item_empty">
                                            <div>
                                                Instruction:<br />
                                                <textarea ng-model="row.Remark" class="single_textarea_full"></textarea>
                                            </div>
                                            <div>
                                                <input type="button" class="button single_button" value="Save" ng-click="floatView.saveTrip(row);" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="float_window " ng-class="newJob.popup.cssList[newJob.popup.curCss]">
        <div class="content">
            <div class="header">
                {{newJob.data.title}}
                <button class="header_button" ng-click="newJob.hide();">X</button>
            </div>
            <div class="body">
                <input type="button" value="Save" class="single_button button" ng-click="newJob.save();" />
                <div class="bx_table bx_table_100pc">
                    <div class="body">
                        <div class="item item_empty">
                            <div>Job Date</div>
                            <div>
                                <input type="date" ng-model="newJob.data.job.JobDate" class="single_text_full" />
                            </div>
                        </div>
                        <div class="item item_empty">
                            <div>Job Type</div>
                            <div>
                                <select ng-model="newJob.data.job.JobType" class="single_select_full">
                                    <option value="IMP">IMP</option>
                                    <option value="EXP">EXP</option>
                                    <option value="LOC">LOC</option>
                                    <option value="ADHOC">ADHOC</option>
                                    <option value="OTHER">OTHER</option>
                                </select>
                            </div>
                        </div>
                        <div class="item item_empty">
                            <div>Shipper</div>
                            <div>
                                <input type="text" ng-model="newJob.data.job.Shipper" class="single_text_full" />
                            </div>
                        </div>
                        <div class="item item_empty">
                            <div>Client</div>
                            <div>
                                <div class="single_buttonselect_full">
                                    <div class="code0">
                                        <input type="text" ng-model="newJob.data.job.Client" class="single_text_small" />
                                        <a class="bx_a_button" ng-click="newJob.selectClient();">&nbsp;..&nbsp;</a>
                                    </div>
                                    <input type="text" ng-model="newJob.data.job.ClientName" class="code1" readonly />
                                </div>
                            </div>
                        </div>
                        <div class="item item_empty">
                            <div>FromAddress</div>
                            <div>
                                <textarea class="single_textarea_full" ng-model="newJob.data.job.FromAddress"></textarea>
                            </div>
                        </div>
                        <div class="item item_empty">
                            <div>ToAddress</div>
                            <div>
                                <textarea class="single_textarea_full" ng-model="newJob.data.job.ToAddress"></textarea>
                            </div>
                        </div>
                        <div class="item item_empty">
                            <div>DepotAddress</div>
                            <div>
                                <textarea class="single_textarea_full" ng-model="newJob.data.job.DepotAddress"></textarea>
                            </div>
                        </div>
                        <div class="item item_empty">
                            <div>Remark</div>
                            <div>
                                <textarea class="single_textarea_full" ng-model="newJob.data.job.Remark"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
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
