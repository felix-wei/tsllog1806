<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultipleUpload.aspx.cs" Inherits="Modules_Upload_MultipleUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Style/language.css" rel="stylesheet" type="text/css" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/PagesContTrucking/script/StyleSheet.css" rel="stylesheet" />
    <link href="script/Style_JobList.css" rel="stylesheet" />
    <link href="script/f_dev.css" rel="stylesheet" />
    <link href="css/booking.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/jquery.fileupload.css" />
    <link rel="stylesheet" href="css/jquery.fileupload-ui.css" />
    <!-- CSS adjustments for browsers with JavaScript disabled -->
    <noscript><link rel="stylesheet" href="css/jquery.fileupload-noscript.css"/></noscript>
    <noscript><link rel="stylesheet" href="css/jquery.fileupload-ui-noscript.css"/></noscript>
    <script src="script/jquery.js"></script>
    <script src="script/moment.js"></script>

    <script src="script/angular/angular.min.js"></script>
    <script src="script/ng-file/ng-file-upload.js"></script>
    <script src="script/ng-file/ng-file-upload-shim.js"></script>
    <script src="script/ng-file/ng-file-upload-all.js"></script>
    <script src="script/anglar_common_app.js"></script>
    <script type="text/javascript">
        app.factory('SV_UploadAttach', function ($http, SV_Common) {
            var data = {
                job: {},
                cont: [],
                trip: [],
                attac: {},
                search: {}
            }
            var vm = {
                init: function () {
                    SV_Common.setWebService('Upload_Attach.asmx');
                    var JobNo = document.getElementById('JobNo');
                    console.log(JobNo.value);
                    if (JobNo.value != '')
                        vm.get_data(JobNo.value);

                },
                GetData: function () {
                    return data;
                },
                FormatDate: function (strTime) {
                    var date = new Date(strTime);
                    if (date.getFullYear() > '2000') {
                        var day = date.getDate();
                        var month = date.getMonth();
                        if (day < 10 || day < 10) {
                            day = '0' + day;
                        }
                        if (month < 10 || month < 10) {
                            month = '0' + (month + 1);
                        }
                        date = day + '/' + month + "/" + date.getFullYear();
                    }
                    else {
                        date = null;
                    }
                    return date;
                },
                FormatDate1: function (strTime) {
                    var date = new Date(strTime);
                    console.log(date);
                    if (date.getFullYear() < '2000') {
                        date = '';
                    }
                    return date;
                },
                get_data: function (par, cb) {
                    var pars = angular.copy(data.search);
                    pars.JobNo = par;
                    var func = "/Get_Data";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == true) {
                            var temp = res.context.job;
                            data.job.JobDate = vm.FormatDate1(temp.JobDate);
                            data.job = res.context.job;
                            data.cont = res.context.cont;
                            data.trip = res.context.trip;
                        }
                        if (cb) {
                            cb(res);
                        }
                    })
                },
                add_attac: function (par, cb) {
                    var pars = angular.copy(par);
                    var func = "/Add_Attach";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    })
                }
            }
            vm.init();
            return vm;

        })
        .controller('Ctrl_UploadAttach', function ($scope, SV_UploadAttach, $timeout, SV_Popup, Upload, $http) {
            $scope.log = '';
            $scope.vm = SV_UploadAttach.GetData();
            $scope.attac = {
                ContId:0,
                ContainerNo: '',
                TripIndex: '',
                TripId: 0,
                RefNo: '',
                FileName: '',
                FilePath: '',
                FileNote: '',
                FileDate: '',
                FileSize:'',
            }
            $scope.list = [];
            $scope.files = [];
            $scope.new_row = {};

            var JobNo = document.getElementById('JobNo');
            $scope.action = {
                data: ['cont', 'trip'],
                trip_select: function (row) {
                    $scope.action.is_show('trip');
                    $scope.new_row = row;
                    $scope.masterData.show_trip($scope.vm.trip, $scope.action.trip_select_callback);
                },
                trip_select_callback: function (par) {
                    $scope.new_row.TripIndex = par.TripIndex;
                    $scope.new_row.TripId = par.Id;
                },
                cont_select: function (row) {
                    $scope.action.is_show('cont');
                    $scope.new_row = row;
                    $scope.masterData.show_cont($scope.vm.cont, $scope.action.cont_select_callback);
                },
                cont_select_callback: function (par) {
                    $scope.new_row.ContainerNo = par.ContainerNo;
                    $scope.new_row.ContId = par.Id;
                },
                upload: function () {
                    var oList = $('#files');
                    var files = oFile.files;
                    if (files && files.length) {
                        var totalLength = files.length;
                        for (var i = 0; i < files.length; i++) {
                            $scope.list.push(files[i]);
                            var file = files[i];
                            if (!file.$error) {
                                Upload.upload({
                                    url: 'UploadFiles.ashx',
                                    fields: {
                                        'JobNo': JobNo.value,
                                    },
                                    file: file
                                }).progress(function (evt) {
                                    var progressPercentage = parseInt(100.0 *
                 evt.loaded / evt.total);
                                    $('#log_' + i).innerHTML = 'Upload Progress: ' + progressPercentage;
                                    $scope.log = 'Upload Progress: ' + progressPercentage +
                                        '% ';
                                    //+evt.config.file.name
                                }).success(function (data, status, headers, config) {
    
                                    $scope.attac.FileName = config.file.name;
                                    $scope.attac.FilePath = JobNo.value + '/' + $scope.attac.FileName;
                                    $scope.attac.FileType = 'Image';
                                    $scope.attac.CreateBy = '';
                                    $scope.attac.FileNote = '';
                                    $scope.attac.RefNo = JobNo.value;

                                    $scope.$apply();
                                    SV_UploadAttach.add_attac($scope.attac, null);
                                });
                            }
                            //$scope.log += $scope.log;
                        }
                        
                    }
                },
                is_show: function (type) {
                    for (var i = 0; i < $scope.action.data.length; i++) {
                        var obj = $scope.action.data[i];
                        var n = document.getElementById(obj);
                        if (type == obj) {
                            n.style.display = "block";
                        }
                        else {
                            n.style.display = "none";
                        }
                    }
                },
            }
            $scope.masterData = {
                data: {
                    trips: [],
                    conts: [],
                    action: '',
                    item: {
                        Code: '',
                        Descirption: ''
                    },
                    selectCallback: null,

                },
                show_trip: function (dd, cb) {
                    $scope.masterData.data.trips = dd;
                    $scope.masterData.data.title = 'Trips';
                    $scope.masterData.selectCallback = cb;
                    $scope.masterData.popup.show($scope.masterData);
                },
                show_cont: function (dd, cb) {
                    $scope.masterData.data.conts = dd;
                    $scope.masterData.data.title = 'Container';
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
                },
            }
            SV_Popup.SetPopup($scope.masterData, 'rightLarge');
            //$scope.action.get_booking(clientRefNo.value);files
        })
    </script>
</head>
<body ng-controller="Ctrl_UploadAttach">
    <form id="form1" runat="server">
        <div>
            <table class="table table-bordered table-responsive width-40-pen" >
                <tr>
                    <td>JobNo</td>
                    <td colspan="2"><input id="JobNo" runat="server" type="text" readonly="true" class="width-150 control onlyread" ng-model="vm.job.JobNo" /></td>
                </tr>
                <tr>
                    <td>Container</td>
                    <td>
                        <input type="text" ng-model="attac.ContainerNo" class="width-150 control" /></td>
                    <td>
                        <a class="btn btn-default btn-sm" ng-click="action.cont_select(attac);">&nbsp;...&nbsp;</a>
                    </td>
                </tr>
                <tr>
                    <td>Trip</td>
                    <td>
                        <table class="table">
                            <tr>
                                <td><input type="text" id="txt_TripId" class="width-60 control"  name="txt_TripId" width="60" ng-model="attac.TripId" /></td>
                                <td><input type="text" width="120" ng-model="attac.TripIndex" class="width-150 control" /></td>
                            </tr>
                        </table>
                      </td>
                    <td>
                        <a class="btn btn-default btn-sm" ng-click="action.trip_select(attac);">&nbsp;...&nbsp;</a>
                    </td>
                </tr>
            </table>

            <div style="display: none">
                
            </div>
            <table class="table table-responsive">
                <tr style="float: right">
                    <td>
                        <span class="btn btn-success fileinput-button" ng-class="{disabled: disabled}">
                            <i class="glyphicon glyphicon-plus"></i>
                            <span>Choose Files</span>
                            <input id="fileupload" type="file" ngf-select ng-model="files" multiple onchange="files_add()" />
                        </span>
                    </td>
                </tr>
            </table>
            <div id="files" class="files" runat="server"></div>
            <table class="table table-responsive hidden" id="btn_upload">
                <tr>
                    <td style="float: left">{{log}}
                    </td>
                    <td style="float: right"><a class="btn btn-info btn-sm width-80" ng-click='action.upload()'>Upload</a></td>
                </tr>
            </table>
            <div class="float_window float_right" ng-class="masterData.popup.cssList[masterData.popup.curCss]">
                <div class="content">
                    <div class="header">
                        {{masterData.data.title}}
                <a class="header_button" ng-click="masterData.hide();">&nbsp;X&nbsp;</a>
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
                        <div class="bx_table bx_table_100pc" id="trip">
                            <div class="header">
                                <div class="col_100"> Id</div>
                                <div class="col_100">Trip Type</div>
                                <div class="col_100">Trip No</div>
                                <div>Trailer No</div>
                                <div>Destination</div>
                                <div>ParkingLot</div>
                                <div>Driver</div>
                                <div>Date</div>
                            </div>
                            <div class="body">
                                <div class="item" ng-repeat="row in masterData.data.trips|filter:masterData.data.no" ng-click="masterData.select(row);">
                                     <div>&nbsp;{{row.Id}}</div>
                                    <div>&nbsp;{{row.TripCode}}</div>
                                    <div>&nbsp;{{row.TripIndex}}</div>
                                    <div>{{row.ChessisCode}}</div>
                                    <div>{{row.ToCode}}</div>
                                    <div>{{row.ToParkingLot}}</div>
                                    <div>{{row.DriverCode}}</div>
                                    <div>{{row.FromDate | date:'dd/MM/yyyy'}}</div>
                                </div>
                            </div>
                        </div>
                        <div class="bx_table bx_table_100pc" id="cont">
                            <div class="header">
                                <div class="col_100">Cont No</div>
                                <div>Cont Type</div>
                            </div>
                            <div class="body">
                                <div class="item" ng-repeat="row in masterData.data.conts|filter:masterData.data.no" ng-click="masterData.select(row);">
                                    <div>&nbsp;{{row.ContainerNo}}</div>
                                    <div>{{row.ContainerType}}</div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
        var oFile = document.getElementById("fileupload");
        var oList = document.getElementById('files');
        var fileList;
        function files_add() {
            oList.innerHTML = "";
            fileList = Array.prototype.slice.call(oFile.files);
            var node = document.createElement('table');
            node.setAttribute('id', 'imageList');
            node.className = "table table-responsive";
            var str = '';
            $.each(oFile.files, function (index, file) {
                str += '<tr id=\'d_'+index+'\'>';
                str += '<td width="70%"><img src=\'' + window.URL.createObjectURL(file) + '\' style=\'width:5%; float:left;padding:1px;margin:5px 0 0 4px; \'></td>';
                str += '<td width="10%" nowrap>' + file.name + '</td>';
                str += '<td width="10%" nowrap>' + formatFileSize(file.size) + '</td>';
                str += "<td width='10%'>'<a title='btn_" + index + "' class='btn btn-danger btn-sm width-80' onclick='del_img(" + index + ")'>Delete</a></td>";
                str += '<td><div id=\'log_' + index + '\'></div></td>';
                str += '</tr>';
            })
            node.innerHTML = str;
            oList.appendChild(node);
            $('#btn_upload').removeClass('hidden');
        }
        function formatFileSize (bytes) {
            if (typeof bytes !== 'number') {
                return '';
            }
            if (bytes >= 1000000000) {
                return (bytes / 1000000000).toFixed(2) + ' GB';
            }
            if (bytes >= 1000000) {
                return (bytes / 1000000).toFixed(2) + ' MB';
            }
            return (bytes / 1000).toFixed(2) + ' KB';
        }
        function del_img(index) {
            $('#imageList tr:eq(' + index + ')').remove();
            //var $li = document.getElementById('d_' + index);
            //oList.removeChild($li);
            fileList.splice(index, 1);
            if (fileList.length == 0) {
                $('#btn_upload').addClass('hidden');
            }
            
        };
        </script>
    </form>
</body>
</html>
