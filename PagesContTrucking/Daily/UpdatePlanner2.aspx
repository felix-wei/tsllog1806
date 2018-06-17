<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePlanner2.aspx.cs" Inherits="PagesContTrucking_Daily_UpdatePlanner2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
    <title>Update Job</title>
    <link href="../script/Style_JobList.css" rel="stylesheet" />
    <link href="../script/f_dev.css" rel="stylesheet" />
    <script src="../script/jquery.js"></script>
    <script src="../script/angular.min.js"></script>
    <script src="../script/anglar_common_app.js"></script>
    <script src="../script/moment.js"></script>
    <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>

    <script type="text/javascript">
        app.factory('SV_Job', function ($http, SV_Common) {
            var data = {
                leftList: [],
                rightList: [],
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
                    SV_Common.setWebService('WebService_UpDateContainer.asmx');
                    data.search = {
                        From: new Date(),
                        //To: new Date(moment().add(1, 'days')),
                        //From: new Date(), //moment().subtract(0,'days').calendar(),
                        //To: new Date(), //moment().subtract(0,'days').calendar(),
                        ContNo: '',
                        JobStatus: 'Operation',
                        viewMode: 'M1',
                        planDate: new Date(moment().add(0, 'days')),
                    };
                },
                GetData: function () { return data; },
                refresh: function (par, cb) {
                    vm.refreshLeft(par, cb);
                },
                refreshLeft: function (par, cb) {

                    var pars = angular.copy(data.search);
                    pars.From = moment(data.search.From).format('YYYYMMDD');
                    pars.planDate = moment(data.search.planDate).format('YYYYMMDD');
                    //pars.To = moment(data.search.To).add(1, 'days').format('YYYYMMDD');

                    console.log('=============== refresh:', pars);
                    var func = "/PlannerTKList_GetData";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == '1') {
                            //console.log(res);
                            data.list = res.context;

                            angular.forEach(data.list.newJob, function (row) {
                                if (row.Statuscode == 'P' && row.DriverCode && row.DriverCode.length > 0) {
                                    row.assigned = true;
                                    //row.Statuscode = 'A';
                                }
                            });
                            angular.forEach(data.list.returnJob, function (row) {
                                if (row.Statuscode == 'P' && row.DriverCode && row.DriverCode.length > 0) {
                                    row.assigned = true;
                                    //row.Statuscode = 'A';
                                }
                                row.ReturnLastDate = new Date(row.ReturnLastDate);
                                if (row.ReturnLastDate.getFullYear() >= 2017) {
                                    row.ReturnLastDate1 = 'RDate:' + moment(row.ReturnLastDate).format('DD/MM/YYYY');
                                }
                            })
                            angular.forEach(data.list.pendingReturn, function (row) {
                                if (row.Statuscode == 'P' && row.DriverCode && row.DriverCode.length > 0) {
                                    row.assigned = true;
                                    //row.Statuscode = 'A';
                                }
                                row.ReturnLastDate = new Date(row.ReturnLastDate);
                                if (row.ReturnLastDate.getFullYear() >= 2017) {
                                    row.ReturnLastDate1 = 'RDate:' + moment(row.ReturnLastDate).format('DD/MM/YYYY');
                                }
                            })

                            angular.forEach(data.list.exportJob, function (row) {
                                if (row.Statuscode == 'P' && row.DriverCode && row.DriverCode.length > 0) {
                                    row.assigned = true;
                                    //row.Statuscode = 'A';
                                }
                            });

                            var newList = [];
                            angular.forEach(data.list.driverList, function (row) {
                                var newD = { isTitle: true, text: row.Code, Code: row.Code, sum: 0 };
                                newList.push(newD);
                                angular.forEach(data.list.rightPart, function (rl_row) {
                                    if (rl_row.DriverCode==row.Code||rl_row.DriverCode3 == row.Code) {
                                        var temp_row = angular.copy(rl_row);
                                        newList.push(temp_row);
                                        newD.sum++;
                                    }
                                });
                            });
                            data.rightList = newList;
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                getView: function (par, cb) {
                    console.log('========== get View', par);
                    var pars = {};
                    pars.TripId = par.Id;

                    var func = "/View_GetData";
                    SV_Common.http(func, pars, function (res) {

                        if (res.status == '1') {
                            data.curView = res.context;
                            var row = data.curView.mast;
                            row.FromDate = new Date(row.FromDate);
                            row.ToDate = new Date(row.ToDate);
                            row.DriverCode_old = row.DriverCode;
                            row.DriverCode2_old = row.DriverCode2;
                            row.ReturnLastDate = new Date(row.ReturnLastDate);

                            row.EmailInd = (row.EmailInd == 'Y' ? 'Y' : 'N');
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
                            var detail = {
                                controller: '',
                                no: data.curView.mast.Id,
                                driver: ',' + data.curView.mast.DriverCode + ',' + data.curView.mast.DriverCode_old + ',' + data.curView.mast.DriverCode2 + ',' + data.curView.mast.DriverCode2_old + ',',
                            }
                            console.log('=========', detail);
                            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
                            cb(res);
                        }
                    });
                },
                addNewTrip: function (par, cb) {
                    //var func = "/AddTrip_ByCurrentyTripId";
                    var func = "/AddTrip_ByTripIdType";
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
                containerEmpty: function (par, cb) {
                    var func = "/containerEmpty";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                containerReadyExport: function (par, cb) {
                    var func = "/containerReadyExport";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                containerPortnetChangeStatus: function (par, cb) {
                    var func = "/container_PortnetStatusChange";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                plannerTrips: function (par, cb) {
                    par.planDate = moment(data.search.planDate).format('YYYYMMDD');
                    var func = "/PlanTKTrips_ByIndex";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                assignTrip: function (par, cb) {
                    var func = "/Plan_AssignTrip";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                assignALLTrip: function (par, cb) {
                    var func = "/Plan_AssignALLTrip";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                PlanTrip_Exchange2Up: function (par, cb) {
                    var func = "/PlanTrip_Exchange2Up";
                    SV_Common.http(func, par, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                PlanTrip_Exchange2Down: function (par, cb) {
                    var func = "/PlanTrip_Exchange2Down";
                    SV_Common.http(func, par, function (res) {
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
            .controller('Ctr_Job', function ($scope, SV_Job, $timeout, SV_Popup) {

                $scope.vm = SV_Job.GetData();

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
                        SV_Job.refresh(null, function (res) {
                            //console.log('=============', $scope.vm);
                            if (res.status == '1') {
                                $scope.action.notice('Refresh Successful', '', '', null);
                                $scope.planner.data.chooseLeftList = [];
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
                                        try {
                                            $scope.$apply();
                                        } catch (e) { }
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
                        SV_Job.refresh_masterdata_client();
                        $scope.masterData.show($scope.vm.masterData.client, $scope.action.selectClient_callback);
                    },
                    selectClient_callback: function (res) {
                        console.log(res);
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
                        SV_Job.save(row, $scope.action.save_callback);
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
                    },

                }

                $scope.lists = {
                    data: {
                        showInd: {
                            newJob: true,
                            returnJob: true,
                            exportJob: true,
                            pendingReturn: true,
                            completed: true,
                        }
                    },
                    toggleSH: function (par) {
                        //console.log(par);
                        $scope.lists.data.showInd[par] = !$scope.lists.data.showInd[par];
                    }
                }

                $scope.planner = {
                    data: {
                        chooseLeftList: [],
                    },
                    chooseList: function (row) {
                        var temp = $scope.planner.data.chooseLeftList;
                        var inside = false;
                        for (var i = 0; i < temp.length; i++) {
                            if (temp[i].tripId == row.tripId) {
                                temp.splice(i, 1);
                                inside = true;
                            }
                        }
                        if (!inside) {
                            temp.push(row);
                        }
                        console.log(temp);
                    },
                    plannToDriver: function (row) {
                        $scope.partDetail.hide();
                        if ($scope.planner.data.chooseLeftList.length > 0) {
                            //var Ids = "0";
                            var arr_id = [];
                            angular.forEach($scope.planner.data.chooseLeftList, function (dd) {
                                //Ids += "," + dd.tripId;
                                arr_id.push({ id: dd.tripId });
                            });
                            var par = { Ids: arr_id, driver: row.Code };
                            SV_Job.plannerTrips(par, $scope.planner.plannToDriver_callback);
                        }
                    },
                    plannToDriver_callback: function (res) {
                        if (res.status == '1') {
                            $scope.planner.data.chooseLeftList = [];
                            $scope.action.notice('Plan Successful', '', 'success', null);
                            $scope.action.refresh();
                        } else {
                            $scope.action.notice('Save plan Error', res.context.Note, 'error', null);
                        }
                    },
                    replan: function (row) {
                        $scope.partDetail.hide();
                        //var par = { Ids: row.tripId, driver: '' };
                        var par = { Ids: [{ id: row.tripId }], driver: '' };
                        SV_Job.plannerTrips(par, $scope.planner.plannToDriver_callback);
                    },
                    assign: function (row) {
                        $scope.action.notice('Confirm assign to driver [' + row.DriverCode3 + ']', '', 'warn', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    var par = { tripId: row.tripId };
                                    SV_Job.assignTrip(par, $scope.planner.assignToDriver_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            //buttonStyle: 'confirm_blue',
                            buttonStyle: 'warn',
                        });
                    },
                    assignToDriver_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Assign Successful', '', 'success', null);
                            $scope.action.refresh();
                        } else {
                            $scope.action.notice('Save assign Error', res.context.Note, 'error', null);
                        }
                    },
                    PlanTrip_Exchange2Up: function (fromRow, toRowId) {
                        if (toRowId >= 0) {
                            var toRow = $scope.vm.rightList[toRowId - 1];
                            if (toRow && !toRow.isTitle) {
                                var par = {
                                    fromId: fromRow.tripId,
                                    toId: toRow.tripId,
                                    fromIndex: fromRow.TripIndex1,
                                };
                                console.log("=== UP", par);
                                SV_Job.PlanTrip_Exchange2Up(par, $scope.planner.plannToDriver_callback);
                            }
                        }
                    },
                    PlanTrip_Exchange2Down: function (fromRow, toRowId) {
                        if (toRowId >= 0) {
                            var toRow = $scope.vm.rightList[toRowId + 1];
                            if (toRow && !toRow.isTitle) {
                                var par = {
                                    fromId: fromRow.tripId,
                                    toId: toRow.tripId,
                                    fromIndex: fromRow.TripIndex1,
                                };
                                console.log("=== DOWN", par);
                                SV_Job.PlanTrip_Exchange2Down(par, $scope.planner.plannToDriver_callback);
                            }
                        }
                    },
                    assginAllToDriver: function (row, rowId) {
                        $scope.action.notice('Confirm assign to driver [' + row.Code + ']', '', 'warn', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    var Ids = [];
                                    var driverCode = row.Code;
                                    for (var i = rowId + 1; i < $scope.vm.rightList.length; i++) {
                                        var temp_row = $scope.vm.rightList[i];
                                        if (temp_row && !temp_row.isTitle && temp_row.DriverCode3 == driverCode) {
                                            Ids.push({ id: temp_row.tripId });
                                        } else {
                                            break;
                                        }
                                    }
                                    var par = { Ids: Ids };
                                    console.log('Assign all to Driver ' + row.Code, par);
                                    SV_Job.assignALLTrip(par, $scope.planner.assignToDriver_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'warn',
                        });
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
                        SV_Job.saveView(null, $scope.floatView.save_callback);
                    },
                    save_callback: function (res) {
                        if (res.status == '1') {
                            $scope.vm.curView.mast.changed = false;
                            $scope.action.notice('Save Detail Successful', '', 'success', null);
                            $scope.action.refresh();
                            if ($scope.vm.curView.mast.Statuscode == 'C') {
                                //$scope.partDetail.hide();
                            } else {
                                SV_Job.getView($scope.vm.curView.mast, null);
                            }
                        } else {
                            $scope.action.notice('Save Detail Error', res.context.Note, 'error', null);
                            //parent.notice("Save Error", null, 'error');
                        }
                    },
                    changeView: function () {
                        $scope.vm.curView.mast.changed = true;
                    },
                    selectDriver: function () {
                        SV_Job.refresh_masterdata_driver();
                        $scope.masterData.show($scope.vm.masterData.driver, $scope.floatView.selectDriver_callback);
                    },
                    selectDriver_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.DriverCode = res.c;
                        $scope.vm.curView.mast.TowheadCode = res.n;
                        $scope.floatView.changeView();
                    },
                    selectDriver2: function () {
                        SV_Job.refresh_masterdata_driver();
                        $scope.masterData.show($scope.vm.masterData.driver, $scope.floatView.selectDriver2_callback);
                    },
                    selectDriver2_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.DriverCode2 = res.c;
                        $scope.floatView.changeView();
                    },
                    selectTowhead: function () {
                        SV_Job.refresh_masterdata_towhead();
                        $scope.masterData.show($scope.vm.masterData.towhead, $scope.floatView.selectTowhead_callback);
                    },
                    selectTowhead_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.TowheadCode = res.c;
                        $scope.floatView.changeView();
                    },
                    selectTrailer: function () {
                        SV_Job.refresh_masterdata_trailer();
                        $scope.masterData.show($scope.vm.masterData.trailer, $scope.floatView.selectTrailer_callback);
                    },
                    selectTrailer_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.ChessisCode = res.c;
                        $scope.floatView.changeView();
                    },
                    selectFromParkingLot: function () {
                        SV_Job.refresh_masterdata_parkingLot();
                        $scope.masterData.show($scope.vm.masterData.parkingLot, $scope.floatView.selectFromParkingLot_callback);
                    },
                    selectFromParkingLot_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.FromParkingLot = res.c;
                        //$scope.vm.curView.mast.FromCode = res.n;
                        $scope.floatView.changeView();
                    },
                    selectToParkingLot: function () {
                        SV_Job.refresh_masterdata_parkingLot();
                        $scope.masterData.show($scope.vm.masterData.parkingLot, $scope.floatView.selectToParkingLot_callback);
                    },
                    selectToParkingLot_callback: function (res) {
                        //console.log(res);
                        $scope.vm.curView.mast.ToParkingLot = res.c;
                        //$scope.vm.curView.mast.ToCode = res.n;
                        $scope.floatView.changeView();
                    },
                    selectSubContractCode: function () {
                        SV_Job.refresh_masterdata_vendor();
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
                                    SV_Job.addNewTrip(temp, $scope.floatView.addNewTrip_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    addNewTrip_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Add trip successful', 'List refreshing', 'success');
                            $scope.action.refresh();
                            $scope.partDetail.hide();
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
                                    SV_Job.readyForBilling(temp, $scope.floatView.readyForBilling_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    readyForBilling_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Ready for billing', 'List refreshing', 'success');
                            $scope.action.refresh();
                            SV_Job.getView($scope.vm.curView.mast, null);
                        } else {
                            $scope.action.notice('Save Error !', res.context, 'warn');
                        }
                    },
                    containerEmpty: function () {

                        $scope.action.notice('Container Empty ?', '', 'success', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    var temp = {
                                        JobNo: $scope.vm.curView.mast.JobNo,
                                        Id: $scope.vm.curView.mast.Id,
                                        det1Id: $scope.vm.curView.mast.Det1Id,
                                    };
                                    SV_Job.containerEmpty(temp, $scope.floatView.containerEmpty_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    containerReadyExport: function () {

                        $scope.action.notice('Container Ready Export ?', '', 'success', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    var temp = {
                                        JobNo: $scope.vm.curView.mast.JobNo,
                                        Id: $scope.vm.curView.mast.Id,
                                        det1Id: $scope.vm.curView.mast.Det1Id,
                                        Weight: $scope.vm.curView.mast.Weight,
                                    };
                                    SV_Job.containerReadyExport(temp, $scope.floatView.containerEmpty_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    containerEmpty_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Container Status changed', 'List refreshing', 'success');
                            $scope.action.refresh();
                            //SV_Job.getView($scope.vm.curView.mast, null);
                            $scope.partDetail.hide();
                        } else {
                            $scope.action.notice('Save Error !', res.context, 'warn');
                        }
                    },
                    containerPortnet_Created: function () {
                        $scope.action.notice('PortNet status [ Created ] ?', '', 'success', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    var temp = {
                                        Id: $scope.vm.curView.mast.Id,
                                        det1Id: $scope.vm.curView.mast.Det1Id,
                                        status: 'Created',
                                    };
                                    SV_Job.containerPortnetChangeStatus(temp, $scope.floatView.containerEmpty_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    containerPortnet_Released: function () {
                        $scope.action.notice('PortNet status [ Released ] ?', '', 'success', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    var temp = {
                                        Id: $scope.vm.curView.mast.Id,
                                        det1Id: $scope.vm.curView.mast.Det1Id,
                                        status: 'Released',
                                    };
                                    SV_Job.containerPortnetChangeStatus(temp, $scope.floatView.containerEmpty_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    sendEmail: function () {
                        SV_Job.emailSend(null, $scope.floatView.sendEmail_callback);
                    },
                    sendEmail_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Send Email successful', '', 'success');
                            //$scope.action.refresh();
                            SV_Job.getView($scope.vm.curView.mast, null);
                            //$scope.partDetail.hide();
                        } else {
                            $scope.action.notice('Send Error !', res.context, 'warn');
                        }
                    },
					generate_emailFormat: function () {
                        SV_Job.generateEmailFormat($scope.vm.curView.mast);
                    }
                };
                $scope.partDetail = {
                    data: {
                        css: 'hide_content_detail',
                    },
                    show: function (row) {
                        //$scope.floatView.data = row;
                        var temp = { Id: row.tripId };
                        SV_Job.getView(temp, $scope.floatView.refresh_callback);
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
                        if ($scope.vm.curView &&  $scope.vm.curView.mast && $scope.vm.curView.mast.changed) {
                            $scope.action.notice('Leave without save', '', 'warn', {
                                type: 'confirm',
                                callback: function (res) {
                                    if (res) {
                                        $scope.vm.curView = {};
                                        $scope.partDetail.data.css = 'hide_content_detail';
                                        try {
                                            $scope.$apply();
                                        } catch (e) { }
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

                $scope.floatView_addTrip = {
                    data: {
                        TripType: '',
                        Id: 0,
                        ToCode: '',
                    },
                    initNew: function (par) {
                        $scope.floatView_addTrip.data.TripType = par;
                        $scope.floatView_addTrip.data.Id = $scope.vm.curView.mast.Id;
                        $scope.floatView_addTrip.data.ToCode = '';
                    },
                    cancelNew: function () {
                        $scope.floatView_addTrip.data.TripType = '';
                        $scope.floatView_addTrip.data.Id = 0;
                    },
                    save: function () {

                        $scope.action.notice('Add New Trip: ' + $scope.floatView_addTrip.data.TripType, '', 'success', {
                            type: 'confirm',
                            callback: function (res) {
                                if (res) {
                                    SV_Job.addNewTrip($scope.floatView_addTrip.data, $scope.floatView_addTrip.save_callback);
                                    try {
                                        $scope.$apply();
                                    } catch (e) { }
                                }
                            },
                            buttonText: 'Confirm',
                            buttonStyle: 'confirm'
                        });
                    },
                    save_callback: function (res) {
                        if (res.status == '1') {
                            $scope.action.notice('Add trip successful', 'List refreshing', 'success');
                            $scope.action.refresh();
                            $scope.floatView_addTrip.cancelNew();
                            $scope.partDetail.hide();
                        } else {
                            $scope.action.notice('Add trip false', res.context, 'warn');
                        }
                    },
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
                    data: SV_Job.GetData().pager,
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

        .col_boxset_p {
            width: 50px;
        }

            .col_boxset_p .col_box_small_p {
                /*width: 130px;
                height: 40px;*/
                width: 44px;
                border-top: 0px;
                border-right: 0px;
                border-bottom: 0px;
                border-left: 9px solid #aaaaaa;
            }

            .col_boxset_p .col_box_small_p_blue {
                border-left: 9px solid blue;
            }

            .col_boxset_p .col_box_small_p_red {
                border-left: 9px solid red;
            }

            .col_boxset_p .col_box_small_p_grey {
                border-left: 9px solid #aaaaaa;
            }


        .joblist_content_list > .part1 {
            position: absolute;
            top: 0px;
            right: 100%;
            bottom: 0px;
            left: 0px;
            overflow: scroll;
        }

        .joblist_content_list > .part2 {
            position: absolute;
            top: 0px;
            right: 0px;
            bottom: 0px;
            left: 100%;
            overflow: scroll;
        }


        .joblist_content_list > .part1M1 {
            right: 40% !important;
        }

        .joblist_content_list > .part2M1 {
            left: 60% !important;
        }


        .joblist_content_list > .part1M2 {
            right: 70% !important;
        }

        .joblist_content_list > .part2M2 {
            left: 30% !important;
        }


        .joblist_content_list > .part1M3 {
            right: 100% !important;
        }

        .joblist_content_list > .part2M3 {
            left: 0% !important;
        }


        .joblist_content_list > .part1M4 {
            right: 0% !important;
        }

        .joblist_content_list > .part2M4 {
            left: 100% !important;
        }
    </style>
</head>
<body ng-controller="Ctr_Job">
    <form name="f_search" ng-submit="action.refresh();">
        <div class="bx_table">
            <div class="body">
                <div class="item item_empty">
                    <div>Schedule Date:</div>
                    <div>
                        <input type="date" ng-model="vm.search.From" class="single_date" />
                    </div>
                    <%--<div>To:</div>
                    <div>
                        <input type="date" ng-model="vm.search.To" class="single_date" />
                    </div>--%>
                    <div>ClientRef/JobNo/ContNo</div>
                    <div>
                        <input type="text" ng-model="vm.search.ContNo" placeholder="(end with)" class="single_text" />
                    </div>
                    <div>
                        View Mode
                    </div>
                    <div>
                        <select class="single_select" ng-model="vm.search.viewMode">
                            <option value="M1">Mode1 (6/4)</option>
                            <option value="M2">Mode2 (3/7)</option>
                            <option value="M3">Mode3 (0/10)</option>
                            <option value="M4">Mode4 (10/0)</option>
                        </select>
                    </div>
                    <div>Plan Date:</div>
                    <div>
                        <input type="date" ng-model="vm.search.planDate" class="single_date" />
                    </div>
                    <div>
                        <input type="submit" value="Retrieve" class="single_button_110 button" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="joblist_content joblist_content_top35 {{partDetail.data.css}}">
        <div class="joblist_content_list">
            <div class="part1" ng-class="{'M1':'part1M1','M2':'part1M2','M3':'part1M3','M4':'part1M4'}[vm.search.viewMode]">
                <table class="bx_table_grid">
                    <tr>
                        <th>Carrier</th>
                        <th>Customer</th>
                        <th>Vessel/Voyage</th>
                        <th>ETA/ETD</th>
                        <th>ContNo/PN/SealNo/Type/Wt</th>
                        <th>Internal Remark</th>
                        <th style="min-width: 100px;">From</th>
                        <th style="min-width: 100px;">To</th>
                        <th>Depot</th>
                        <th>Remark</th>
                        <th>Tralier</th>
                        <th>Trips</th>
                        <th>#</th>
                        <th>Permit</th>
                        <th style="min-width: 120px;">JobNo</th>
                    </tr>


                    <tr ng-show="vm.search.JobStatus=='Operation'">
                        <th colspan="22" style="padding: 6px;" ng-click="lists.toggleSH('newJob');">[ {{lists.data.showInd.newJob?'-':'+'}} ] New Job Current ({{vm.list.newJob.length}})</th>
                    </tr>
                    <tr ng-show="vm.search.JobStatus=='Operation'&&lists.data.showInd.newJob" ng-repeat="row in vm.list.newJob">
                    <td>
                        <div ng-show="row.JobType=='EXP'">Shipper:{{row.shipper}}<br />
                            Carrier:{{row.CarrierId}}<br />
                            PortNetRef:{{row.PortnetRef}}<br />
                            PickupRef:{{row.PickupRefNo}}</div>
                        <div ng-hide="row.JobType=='EXP'">
                            Carrier:{{row.CarrierId}}<br />
                            {{row.CarrierBkgNo}}</div>
                    </td>
                        <td>{{row.ClientId}}<br />
                            {{row.MasterJobNo}}</td>
                        <td>{{row.Vessel}}/{{row.Voyage}}</td>
                        <td>{{row.EtaDate}}&nbsp;{{row.EtaTime}}&nbsp;/{{row.TerminalLocation}}<br />
                            {{row.EtdDate}}&nbsp;{{row.EtdTime}}</td>
                        <td>{{row.ContainerNo}}/{{row.PortnetStatus}}<br />
                            /{{row.SealNo}}<br />
                            /{{row.ContainerType}}/{{row.Weight}}/{{row.ContWeight}}
                        </td>
                        <td>{{row.ReleaseToHaulierRemark}}</td>
                        <td>{{row.FromCode}}</td>
                        <td>{{row.ToCode}}</td>
                        <td>{{row.ReturnLastDate1}}<br />
                            {{row.YardAddress}}</td>
                        <td>Job:{{row.JobRemark}}<br />
                            ContIntr:{{row.ContRemark}}<br />
                            Cont:{{row.ContRemark1}}<br />
                            Client:<span style="color:red">{{row.ClientRemark}}</span></td>
                        <td>{{row.ChessisCode}}</td>
                        <td>
                            <div class="col_boxset col_boxset_p">
                                <div class="col_box_small col_box_small_p {{row.JobType=='IMP'?'col_box_small_p_grey':(row.JobType=='EXP'?'col_box_small_p_red':'')}}" ng-class="{'P':'col_box_orange','S':'col_box_green','C':'col_box_blue','X':'col_box_gray','A':'col_box_calm'}[row.Statuscode]" ng-click="action.viewRow(row);">
                                    &nbsp;{{row.TripCode}}
                                </div>
                            </div>
                        </td>
                        <td>
                            <input type="checkbox" ng-model="row.choose" ng-change="planner.chooseList(row);" />
                        </td>
                        <td>{{row.PermitNo}}</td>
                        <td>
                            <a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a>
                        </td>
                    </tr>


                    <tr ng-show="vm.search.JobStatus=='Operation'">
                        <th colspan="22" style="padding: 6px; background-color: #fe5454;" ng-click="lists.toggleSH('returnJob');">[ {{lists.data.showInd.returnJob?'-':'+'}} ] Empty Return ({{vm.list.returnJob.length}})</th>
                    </tr>
                    <tr ng-show="vm.search.JobStatus=='Operation'&&lists.data.showInd.returnJob" ng-repeat="row in vm.list.returnJob">
                        <td>
                        <div ng-show="row.JobType=='EXP'">Shipper:{{row.shipper}}<br />
                            Carrier:{{row.CarrierId}}<br />
                            PortNetRef:{{row.PortnetRef}}<br />
                            PickupRef:{{row.PickupRefNo}}</div>
                        <div ng-hide="row.JobType=='EXP'">
                            Carrier:{{row.CarrierId}}<br />
                            {{row.CarrierBkgNo}}</div>
                        </td>
                        <td>{{row.ClientId}}<br />
                            {{row.MasterJobNo}}</td>
                        <td>{{row.Vessel}}/{{row.Voyage}}</td>
                        <td>{{row.EtaDate}}&nbsp;{{row.EtaTime}}&nbsp;/{{row.TerminalLocation}}<br />
                            {{row.EtdDate}}&nbsp;{{row.EtdTime}}</td>
                        <td>{{row.ContainerNo}}/{{row.PortnetStatus}}<br />
                            /{{row.SealNo}}<br />
                            /{{row.ContainerType}}/{{row.Weight}}/{{row.ContWeight}}
                        </td>
                        <td>{{row.ReleaseToHaulierRemark}}</td>
                        <td>{{row.FromCode}}</td>
                        <td>{{row.ToCode}}</td>
                        <td>{{row.ReturnLastDate1}}<br />
                            {{row.YardAddress}}</td>
                        <td>Job:{{row.JobRemark}}<br />
                            ContIntr:{{row.ContRemark}}<br />
                            Cont:{{row.ContRemark1}}<br />
                            Client:<span style="color:red">{{row.ClientRemark}}</span></td>
                        <td>{{row.ChessisCode}}</td>
                        <td>
                            <div class="col_boxset col_boxset_p">
                                <div class="col_box_small col_box_small_p {{row.JobType=='IMP'?'col_box_small_p_grey':(row.JobType=='EXP'?'col_box_small_p_red':'')}}" ng-class="{'P':'col_box_orange','S':'col_box_green','C':'col_box_blue','X':'col_box_gray','A':'col_box_calm'}[row.Statuscode]" ng-click="action.viewRow(row);">
                                    &nbsp;{{row.TripCode}}
                                </div>
                            </div>
                        </td>
                        <td>
                            <input type="checkbox" ng-model="row.choose" ng-change="planner.chooseList(row);" />
                        </td>
                        <td>{{row.PermitNo}}</td>
                        <td>
                            <a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a>
                        </td>
                    </tr>

                    <tr ng-show="vm.search.JobStatus=='Operation'">
                        <th colspan="22" style="padding: 6px; background-color: #fe5454;" ng-click="lists.toggleSH('exportJob');">[ {{lists.data.showInd.exportJob?'-':'+'}} ] Export ({{vm.list.exportJob.length}})</th>
                    </tr>
                    <tr ng-show="vm.search.JobStatus=='Operation'&&lists.data.showInd.exportJob" ng-repeat="row in vm.list.exportJob">
                        <td>
                        <div ng-show="row.JobType=='EXP'">Shipper:{{row.shipper}}<br />
                            Carrier:{{row.CarrierId}}<br />
                            PortNetRef:{{row.PortnetRef}}<br />
                            PickupRef:{{row.PickupRefNo}}</div>
                        <div ng-hide="row.JobType=='EXP'">
                            Carrier:{{row.CarrierId}}<br />
                            {{row.CarrierBkgNo}}</div>
                        </td>
                        <td>{{row.ClientId}}<br />
                            {{row.MasterJobNo}}</td>
                        <td>{{row.Vessel}}/{{row.Voyage}}</td>
                        <td>{{row.EtaDate}}&nbsp;{{row.EtaTime}}&nbsp;/{{row.TerminalLocation}}<br />
                            {{row.EtdDate}}&nbsp;{{row.EtdTime}}</td>
                        <td>{{row.ContainerNo}}/{{row.PortnetStatus}}<br />
                            /{{row.SealNo}}<br />
                            /{{row.ContainerType}}/{{row.Weight}}/{{row.ContWeight}}
                        </td>
                        <td>{{row.ReleaseToHaulierRemark}}</td>
                        <td>{{row.FromCode}}</td>
                        <td>{{row.ToCode}}</td>
                        <td>{{row.ReturnLastDate1}}<br />
                            {{row.YardAddress}}</td>
                        <td>Job:{{row.JobRemark}}<br />
                            ContIntr:{{row.ContRemark}}<br />
                            Cont:{{row.ContRemark1}}<br />
                            Client:<span style="color:red">{{row.ClientRemark}}</span></td>
                        <td>{{row.ChessisCode}}</td>
                        <td>
                            <div class="col_boxset col_boxset_p">
                                <div class="col_box_small col_box_small_p {{row.JobType=='IMP'?'col_box_small_p_grey':(row.JobType=='EXP'?'col_box_small_p_red':'')}}" ng-class="{'P':'col_box_orange','S':'col_box_green','C':'col_box_blue','X':'col_box_gray','A':'col_box_calm'}[row.Statuscode]" ng-click="action.viewRow(row);">
                                    &nbsp;{{row.TripCode}}
                                </div>
                            </div>
                        </td>
                        <td>
                            <input type="checkbox" ng-model="row.choose" ng-change="planner.chooseList(row);" />
                        </td>
                        <td>{{row.PermitNo}}</td>
                        <td>
                            <a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a>
                        </td>
                    </tr>


                    <tr ng-show="vm.search.JobStatus=='Operation'">
                        <th colspan="22" style="padding: 6px; background-color: #aaaaaa;" ng-click="lists.toggleSH('pendingReturn');">[ {{lists.data.showInd.pendingReturn?'-':'+'}} ] Pending ({{vm.list.pendingReturn.length}})</th>
                    </tr>
                    <tr ng-show="vm.search.JobStatus=='Operation'&&lists.data.showInd.pendingReturn" ng-repeat="row in vm.list.pendingReturn">
                        <td>
                        <div ng-show="row.JobType=='EXP'">Shipper:{{row.shipper}}<br />
                            Carrier:{{row.CarrierId}}<br />
                            PortNetRef:{{row.PortnetRef}}<br />
                            PickupRef:{{row.PickupRefNo}}</div>
                        <div ng-hide="row.JobType=='EXP'">
                            Carrier:{{row.CarrierId}}<br />
                            {{row.CarrierBkgNo}}</div>
                        </td>
                        <td>{{row.ClientId}}<br />
                            {{row.MasterJobNo}}</td>
                        <td>{{row.Vessel}}/{{row.Voyage}}</td>
                        <td>{{row.EtaDate}}&nbsp;{{row.EtaTime}}&nbsp;/{{row.TerminalLocation}}<br />
                            {{row.EtdDate}}&nbsp;{{row.EtdTime}}</td>
                        <td>{{row.ContainerNo}}/{{row.PortnetStatus}}<br />
                            /{{row.SealNo}}<br />
                            /{{row.ContainerType}}/{{row.Weight}}/{{row.ContWeight}}
                        </td>
                        <td>{{row.ReleaseToHaulierRemark}}</td>
                        <td>{{row.FromCode}}</td>
                        <td>{{row.ToCode}}</td>
                        <td>{{row.ReturnLastDate1}}<br />
                            {{row.YardAddress}}</td>
                        <td>Job:{{row.JobRemark}}<br />
                            ContIntr:{{row.ContRemark}}<br />
                            Cont:{{row.ContRemark1}}<br />
                            Client:<span style="color:red">{{row.ClientRemark}}</span></td>
                        <td>{{row.ChessisCode}}</td>
                        <td>
                            <div class="col_boxset col_boxset_p">
                                <div class="col_box_small col_box_small_p {{row.JobType=='IMP'?'col_box_small_p_grey':(row.JobType=='EXP'?'col_box_small_p_red':'')}}" ng-class="{'P':'col_box_orange','S':'col_box_green','C':'col_box_blue','X':'col_box_gray','A':'col_box_calm'}[row.Statuscode]" ng-click="action.viewRow(row);">
                                    &nbsp;{{row.TripCode}}
                                </div>
                            </div>
                        </td>
                        <td>
                            <input type="checkbox" ng-model="row.choose" ng-change="planner.chooseList(row);" />
                        </td>
                        <td>{{row.PermitNo}}</td>
                        <td>
                            <a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a>
                        </td>
                    </tr>

                </table>
            </div>
            <div class="part2" ng-class="{'M1':'part2M1','M2':'part2M2','M3':'part2M3','M4':'part2M4'}[vm.search.viewMode]">
                <table class="bx_table_grid">
                    <tr>
                        <th>Up/Down/Replan/Assign</th>
                        <th>ContNo/PN/SealNo</th>
                        <th>ETA/ETD</th>
                        <th style="min-width: 100px;">From</th>
                        <th style="min-width: 100px;">To</th>
                        <th>Depot</th>
                        <th>Remark</th>
                        <th>Trips</th>
                        <th style="min-width: 120px;">JobNo</th>
                    </tr>
                    <tr ng-repeat="row in vm.rightList">
                        <td ng-show="row.isTitle" style="background-color: #aaaaaa; padding: 6px;" colspan="9">
                            <input type="button" value="Plan to:" ng-show="planner.data.chooseLeftList.length>0" ng-click="planner.plannToDriver(row);" />
                            {{row.text}} [{{row.sum}}]
                            <input type="button" value="Assign All" ng-hide="planner.data.chooseLeftList.length>0||row.sum==0" ng-click="planner.assginAllToDriver(row,$index);" />
                        </td>
                        <td ng-hide="row.isTitle">
                            <div style="min-width: 150px">
                                <input type="button" class="button" value="∧" ng-click="planner.PlanTrip_Exchange2Up(row,$index);" />
                                <input type="button" class="button" value="∨" ng-click="planner.PlanTrip_Exchange2Down(row,$index);" />
                                <input type="button" class="button" value="<" ng-click="planner.replan(row);" ng-hide="planner.data.chooseLeftList.length>0||(row.DriverCode&&row.DriverCode.length>0)" />
                                <input type="button" class="button" value=">" ng-click="planner.assign(row);" ng-hide="planner.data.chooseLeftList.length>0||(row.DriverCode&&row.DriverCode.length>0)" />
                            </div>
                        </td>
                        <td ng-hide="row.isTitle">{{row.ContainerNo}}/{{row.PortnetStatus}}<br />
                            /{{row.SealNo}}<br />
                            /{{row.ContainerType}}/{{row.Weight}}/{{row.ContWeight}}</td>
                        <td ng-hide="row.isTitle">{{row.EtaDate}}&nbsp;{{row.EtaTime}}&nbsp;/{{row.TerminalLocation}}<br />
                            {{row.EtdDate}}&nbsp;{{row.EtdTime}}</td>
                        <td ng-hide="row.isTitle">{{row.FromCode}}</td>
                        <td ng-hide="row.isTitle">{{row.ToCode}}</td>
                        <td ng-hide="row.isTitle">{{row.YardAddress}}</td>
                        <td ng-hide="row.isTitle">Job:{{row.JobRemark}}<br />
                            ContIntr:{{row.ContRemark}}<br />
                            Cont:{{row.ContRemark1}}<br />
                            Client:<span style="color:red">{{row.ClientRemark}}</span></td>
                        <td ng-hide="row.isTitle">
                            <div class="col_boxset col_boxset_p">
                                <div class="col_box_small col_box_small_p {{row.JobType=='IMP'?'col_box_small_p_grey':(row.JobType=='EXP'?'col_box_small_p_red':'')}}" ng-class="{'P':'col_box_orange','S':'col_box_green','C':'col_box_blue','X':'col_box_gray','A':'col_box_calm'}[row.Statuscode]" ng-click="action.viewRow(row);">
                                    &nbsp;{{row.TripCode}}
                                </div>
                            </div>
                        </td>
                        <td ng-hide="row.isTitle"><a href="#" ng-click="action.openTabJob(row);">{{row.JobNo}}</a></td>
                    </tr>
                </table>
            </div>
        </div>
        <%--<div class="footer">
            <bx-pager f-data="pager"></bx-pager>
        </div>--%>

        <div class="joblist_content_detail">
            <div class="header" style="padding-left: 0px;">
                <span style="color: white; padding: 6px; background-color: {{vm.curView.mast.Statuscode=='S'?'green':(vm.curView.mast.Statuscode=='C'?'blue':'orange')}}">{{vm.curView.mast.TripCode}}</span>
                <div style="max-width: 150px; max-height: 14px; display: inline-block; overflow: hidden; white-space: nowrap;">{{vm.curView.mast.JobNo}}</div>
                <button class="header_button" ng-click="partDetail.hide();">X</button>
                <button class="header_button" style="background-color: green; color: white" ng-click="floatView.save();">Save</button>
            </div>
            <div class="body" ng-include="'UpdateContainer_Detail.html'">
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
