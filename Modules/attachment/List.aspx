<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Modules_attachment_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
    <title>Attachment List</title>
    <link href="/PagesContTrucking/script/Style_JobList.css" rel="stylesheet" />
    <link href="/PagesContTrucking/script/f_dev.css" rel="stylesheet" />
    <script src="/PagesContTrucking/script/jquery.js"></script>
    <script src="/PagesContTrucking/script/moment-with-locales.js"></script>
    <script src="/PagesContTrucking/script/angular.min.js"></script>
    <script src="/PagesContTrucking/script/angular-moment-picker.min.js"></script>
    <link href="/PagesContTrucking/script/angular-moment-picker.min.css" rel="stylesheet">
    <script src="/PagesContTrucking/script/anglar_common_app.js"></script>


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
                curView: {},
                masterData: {},
            };
            var vm = {
                init: function () {
                    SV_Common.setWebService('WebService_Attachment_List.asmx');
                    data.search = {
                        //From: new Date(moment().add(-102, 'days')),
                        From: new Date(), //moment().subtract(0,'days').calendar(),
                        To: new Date(), //moment().subtract(0,'days').calendar(),
                        no: '',
                        category: 'ALL',
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
                            data.list = res.context.list;
                            var image_StartWith = "/Photos/";

                            angular.forEach(data.list, function (row, index) {
                                row.FileIcon = 'file_icon/empty.png';
                                if (row.FilePath && row.FilePath.length > 0) {
                                    if (row.FilePath.trim().toLowerCase().indexOf('http://') == 0) {

                                    } else {
                                        row.FilePath = image_StartWith + row.FilePath;
                                    }
                                    var temp = vm.getFileType(row.FilePath);
                                    if (temp == 'image') {
                                        row.FileIcon = row.FilePath;
                                    } else {
                                        row.FileIcon = 'file_icon/' + temp + '.png';
                                    }
                                }
                            });
                        }
                        if (cb) {
                            cb(res);
                        }
                    });
                },
                getFileType: function (filename) {
                    var temp_file = filename.toString().toLowerCase();
                    //var temp_ar = temp_file.split('.');
                    var temp_index = temp_file.lastIndexOf('.');
                    var re = "file"
                    var img_file_end = "|psd|jpg|gif|bmp|jpeg|png|";
                    var excel_file_end = "|xls|xlsx|xlsm|xltm|xlsb|xlam|";
                    var word_file_end = "|doc|docx|";
                    var txt_file_end = "|txt|";
                    var pdf_file_end = "|pdf|";
                    if (temp_index >= 0) {
                        var file_end = temp_file.substring(temp_index + 1);
                        //console.log(temp_file, file_end);
                        //re = file_end;
                        if (img_file_end.indexOf('|' + file_end + '|') > -1) {
                            re = "image";
                        }
                        if (excel_file_end.indexOf('|' + file_end + '|') > -1) {
                            re = "excel";
                        }
                        if (word_file_end.indexOf('|' + file_end + '|') > -1) {
                            re = "word";
                        }
                        if (txt_file_end.indexOf('|' + file_end + '|') > -1) {
                            re = "text";
                        }
                        if (pdf_file_end.indexOf('|' + file_end + '|') > -1) {
                            re = "pdf";
                        }
                    }
                    return re;
                },
                getView: function (par, cb) {

                    data.curView = angular.copy(par);

                    if (cb) {
                        cb({ status: '1', });
                    }
                },
                save: function (par, cb) {

                    var func = "/saveTrip";
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
            };
            vm.init();
            return vm;
        })
            .controller('Ctr_DailyTrips', function ($scope, SV_DailyTrips, $timeout, SV_Popup) {

                $scope.vm = SV_DailyTrips.GetData();

                $scope.action = {
                    init: function () {
                        //var type = "IMP";
                        //var ss = window.location.search;
                        //if (ss.indexOf("?") != -1) {
                        //    ss = ss.substring(1);
                        //    var ar = ss.split("&");
                        //    for (var i = 0; i < ar.length; i++) {
                        //        var ar1 = ar[i].split("=");
                        //        if (ar1[0] == "type") {
                        //            type = ar1[1];
                        //        }
                        //    }
                        //}
                        //$scope.vm.search.type = type;
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
                        $scope.searchMore.viewDetail(row);
                    },
                    changedRow: function (row) {
                        row.changed = true;
                    },
                    openTabJob: function (row) {
                        console.log(row);
                        parent.navTab.openTab(row.JobNo, "/PagesContTrucking/Job/JobEdit.aspx?jobNo=" + row.JobNo, { title: row.JobNo, fresh: false, external: true });
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
                    addAttachment:function(res){
                        $scope.pageUpload.show();
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


                //========================== upload page
                $scope.pageUpload = {
                    data: {
                        list: [],
                        no: '',
                        title: '',
                        selectCallback: null,
                    },
                    show: function () {
                        $scope.pageUpload.popup.show($scope.pageUpload);
                    },
                    hide: function () {
                        $scope.pageUpload.data = {};

                        $scope.pageUpload.popup.hide($scope.pageUpload);
                    },
                    select: function (row) {
                        if ($scope.pageUpload.selectCallback && typeof ($scope.pageUpload.selectCallback) == 'function') {
                            //$scope.action.selectClient_callback(row);
                            $scope.pageUpload.selectCallback(row);
                        }
                        $scope.pageUpload.hide();
                    }
                }
                SV_Popup.SetPopup($scope.pageUpload, 'centerLarger');


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
                        //if ($scope.vm.curView.mast.changed) {
                        //    $scope.action.notice('Leave without save', '', 'warn', {
                        //        type: 'confirm',
                        //        callback: function (res) {
                        //            if (res) {
                        //                $scope.vm.curView = {};
                        //                $scope.partDetail.data.css = 'hide_content_detail';
                        //                $scope.$apply();
                        //            }
                        //        },
                        //        buttonText: 'Continue',
                        //        buttonStyle: 'warn'
                        //    });
                        //} else {
                        //    $scope.vm.curView = {};
                        //    $scope.partDetail.data.css = 'hide_content_detail';
                        //}
                        $scope.vm.curView = {};
                        $scope.partDetail.data.css = 'hide_content_detail';
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
        .file_icon {
            width: 160px;
            height: 160px;
        }

            .file_icon img {
                width: 160px;
                height: 160px;
            }

                .file_icon img:hover {
                    cursor: pointer;
                }
    </style>
</head>
<body ng-controller="Ctr_DailyTrips">
    <form id="form1" name="f_search" ng-submit="action.refresh();">
        <table class="bx_table_grid bx_table_grid_border0">
            <tr>
                <td>Date From</td>
                <td>
                    <input type="date" ng-model="vm.search.From" /></td>
                <td>To</td>
                <td>
                    <input type="date" ng-model="vm.search.To" /></td>
                <td>Category</td>
                <td>
                    <select ng-model="vm.search.category" class="single_select">
                        <option value="ALL">ALL</option>
                        <option value="Image">Image</option>
                        <option value="Signature">Signature</option>
                        <option value="Excel">Excel</option>
                        <option value="PDF">PDF</option>
                    </select>
                </td>
                <td>JobNo/ContNo</td>
                <td>
                    <input type="text" ng-model="vm.search.no" class="single_text" />
                </td>
                <td>
                    <input type="submit" class="button single_button" value="Retrieve" />
                </td>
                <td>
                    <input type="button" class="button" value="Add Attachment" ng-click="action.addAttachment();" />
                </td>
            </tr>
        </table>
    </form>


    <div class="joblist_content joblist_content_top35 has_footer {{partDetail.data.css}}">
        <div class="joblist_content_list">
            <table class="bx_table_grid bx_table_100pc">
                <tr>
                    <%--<th>#</th>--%>
                    <th>#</th>
                    <th>File Name</th>
                    <th>Note</th>
                    <th>JobNo</th>
                    <th>ContNo</th>
                    <th>CreateBy</th>
                    <th>CreateDate</th>
                </tr>
                <tr ng-repeat="row in vm.list">
                    <%--<td>
                        <button class="button">View</button>
                    </td--%>
                    <td class="file_icon">
                        <a href="{{row.FilePath}}" target="_blank">
                            <img ng-src="{{row.FileIcon}}" />
                        </a>
                    </td>
                    <%--<td class="file_icon">
                        <img ng-src="{{row.FileIcon}}" ng-click="action.viewRow(row);" />
                    </td>--%>
                    <td>{{row.FileName}}</td>
                    <td>{{row.FileNote}}</td>
                    <td>{{row.RefNo}}</td>
                    <td>{{row.ContainerNo}}</td>
                    <td>{{row.CreateBy}}</td>
                    <td>{{row.CreateDateTime|date:'yyyy/MM/dd HH:mm'}}</td>
                </tr>
            </table>
        </div>
        <div class="footer">
            <bx-pager f-data="pager"></bx-pager>
        </div>
        <div class="joblist_content_detail">
            <div class="header" style="padding-left: 0px;">
                <div style="padding-left:6px;max-width: 240px; display: inline-block; overflow: hidden; white-space: nowrap;">{{vm.curView.FileName}}</div>
                <button class="header_button" ng-click="partDetail.hide();">X</button>
                <%--<button class="header_button" style="background-color: green; color: white" ng-click="floatView.save();">Save</button>--%>
            </div>
            <div class="body">
                <div class="tabs" ng-init="common_tabs_current_tab='tab0'">
                    <div class="tab">
                        <div ng-click="common_tabs_current_tab='tab0';" ng-class="{true:'tab_select'}[common_tabs_current_tab=='tab0']">Information</div>
                        <%--<div ng-click="common_tabs_current_tab='tab1';" ng-class="{true:'tab_select'}[common_tabs_current_tab=='tab1']">&nbsp;&nbsp;Fee&nbsp;&nbsp;</div>
                        <div ng-click="common_tabs_current_tab='tab10';" ng-class="{true:'tab_select'}[common_tabs_current_tab=='tab10']">More</div>--%>
                    </div>
                    <div class="tabs_content">
                        <!--tab0-->
                        <div ng-show="common_tabs_current_tab=='tab0'">
                            <table class="bx_table_grid bx_table_grid_border0 bx_table_100pc">
                                <tr class="tr_label">
                                    <td>File Name</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="text" ng-model="vm.curView.FileName" class="single_text_full" />
                                    </td>
                                </tr>
                                <tr class="tr_label">
                                    <td>File Note</td>
                                </tr>
                                <tr>
                                    <td>
                                        <textarea ng-model="vm.curView.FileNote" class="single_textarea_full"></textarea>
                                    </td>
                                </tr>
                                <tr class="tr_label">
                                    <td>JobNo</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="text" ng-model="vm.curView.RefNo" class="single_text_full" />
                                    </td>
                                </tr>
                                <tr class="tr_label">
                                    <td>ContainerNo</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="text" ng-model="vm.curView.ContainerNo" class="single_text_full" />
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                                <tr>
                                    <td>
                                        <button class="button single_text_full" ng-click="floatView.save();">Save</button></td>
                                </tr>
                                <tr>
                                    <td>
                                        <button class="button single_text_full" ng-click="floatView.save();">Delete</button></td>
                                </tr>
                            </table>
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

    <div class="float_window " ng-class="pageUpload.popup.cssList[pageUpload.popup.curCss]">
        <div class="content">
            <div class="header">
                Upload
                <button class="header_button" ng-click="pageUpload.hide();">X</button>
            </div>
            <div class="body">
                <iframe src="/modules/Upload/MultipleUpload1.aspx" style="width:98%;height:98%;"></iframe>
            </div>
        </div>
    </div>
</body>
</html>
