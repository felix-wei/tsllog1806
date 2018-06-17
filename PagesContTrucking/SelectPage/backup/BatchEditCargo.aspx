<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchEditCargo.aspx.cs" Inherits="PagesContTrucking_SelectPage_BatchEditCargo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="app">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/PagesContTrucking/script/bootstrap.css" rel="stylesheet" />
    <link href="/Style/language.css" rel="stylesheet" type="text/css" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/PagesContTrucking/script/StyleSheet.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
     <link href="../script/Style_JobList.css" rel="stylesheet" />
    <link href="../script/f_dev.css" rel="stylesheet" />
    <script src="../script/jquery.js"></script>
    <script src="../script/moment.js"></script>
    <script src="../script/angular/angular.min.js"></script>
    <script src="../script/anglar_common_app.js"></script>  
    <!-[if lt IE 9]><script src="http://css3-mediaqueries-js.googlecode.com/svn/trunk/css3-mediaqueries.js"></script><![endif]->
    <style type="text/css">
        html {
            width: 100%;
            padding: 0px;
            margin: 0px;
        }
        .form {
            width:100%;
           padding:0px;
           margin:0px;
        }
        .control {
            display: block;
            width: 100%;
            height: 30px;
            padding: 2px 4px;
            font: normal 100% Helvetica, Arial, sans-serif;
            line-height: 30px;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 1px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        }
        .textarea {
            display: block;
            width: 100%;
            height: 60px;
            padding: 0px;
            font-size: 12px;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 1px;
        }
        .width-40 {
            width: 40px;
        }
        .width-50 {
            width: 50px;
        }
        .width-60 {
            width: 60px;
        }
        .width-70 {
            width: 70px;
        }
        .width-80 {
            width: 80px;
        }
        .width-90 {
            width: 90px;
        }
        .width-100 {
            width: 100px;
        }
        .width-110 {
            width: 110px;
        }
        .width-120 {
            width: 120px;
        }
        .width-140 {
            width: 140px;
        }
        .width-160 {
            width: 160px;
        }
        .float_right {
            position: absolute; /*这里一定要设置*/
            z-index: 999999; /*这里是该元素与显示屏的距离，据说越大越好，但是我也没有看到效果，因为没有它也是可以的*/
            margin-top: 0px;
            right:10px;
            position:fixed; 
            -webkit-transition: .5s ease-in-out; /* css的transition允许css的属性值在一定的时间内从一个状态平滑的过渡到另一个状态 */
            -moz-transition: .5s ease-in-out; /*这里为了兼容其他浏览器*/
            -o-transition: .5s ease-in-out;
        }
    </style>
    <script type="text/javascript">
        app.factory('SV_CargoService', function ($http, SV_Common) {
            var data = {
                list: [],
                cont: [],
                trip:[],
                type: [],
                uom: [],
                sku: [],
                location:[],
                search: {},
            }
            var vm = {
                init: function () {
                    SV_Common.setWebService('TruckingService.asmx');
                    var par1 = document.getElementById('JobNo');
                    var par2 = document.getElementById('CargoType');
                    
                    console.log(par1.value);
                    console.log(par2.value);
                    vm.get_data(par1.value, par2.value);
                    //vm.get_type();
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
                            day = '0'+day;
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
                get_data: function (par1, par2, cb) {
                    var pars = angular.copy(data.search);
                    pars.JobNo = par1;
                    pars.CargoType = par2;
                    var func = "/Get_Data";
                    SV_Common.http(func, pars, function (res) {
                        if (res.status == true) {
                            var temp = res.context.list;
                            var currentInd = 0;
                            angular.forEach(temp, function (row) {
                                row.NextBillDate = vm.FormatDate(row.NextBillDate);
                                row.StockDate = vm.FormatDate1(row.StockDate);
                                row.BillingStartDate = vm.FormatDate1(row.BillingStartDate);
                            });
                            data.list = res.context.list;
                            data.cont = res.context.cont;
                            console.log(data.trip);
                            data.trip = res.context.trip;
                            data.type = res.context.type;
                            data.uom = res.context.uom;
                            data.sku = res.context.sku;
                            data.location = res.context.location;
                        }
                        if (cb) {
                            cb(res);
                        }
                    })
                },
                add: function (cb) {
                    var par1 = document.getElementById('JobNo');
                    var par2 = document.getElementById('CargoType');
                    var par3 = document.getElementById('Client');
                    var pars = angular.copy(data.search);
                    pars.JobNo = par1.value;
                    pars.CargoType = par2.value;
                    pars.Client = par3.value;
                    var func = "/Add_Cargo";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            vm.init();
                        }
                    })
                },
                copy: function (id,count, cb) {
                    var par1 = document.getElementById('JobNo');
                    var pars = angular.copy(data.search);
                    pars.JobNo = par1.value;
                    pars.Count = count;
                    pars.Id = id;
                    var func = "/Copy_Cargo";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            vm.init();
                        }
                    })
                },
                save: function (par, cb) {
                    var par3 = document.getElementById('Client');

                    var pars = angular.copy(data.search);
                    pars.list = angular.copy(par);
                    angular.forEach(pars.list, function (item) {
                        item.StockDate = moment(item.StockDate).format("YYYY-MM-DD");
                        item.BillingStartDate = moment(item.BillingStartDate).format("YYYY-MM-DD");
                    })
                    pars.Client = par3.value;
                    var func = "/Save_Data";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            vm.init();
                            cb(res);
                        }
                    })
                },
                del: function (par, cb) {
                    var pars = angular.copy(data.search);
                    pars.Id = par;
                    var func = "/Delete_Cargo";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            vm.init();
                        }
                    })
                },
                add_sku: function (par, cb) {
                    var par1 = document.getElementById('JobNo');
                    var pars = angular.copy(data.search);
                    pars.Code = par.Code;
                    pars.Name = par.Name;
                    pars.JobNo = par1.value;
                    var func = "/Add_SKU";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    })
                },
                save_sku: function (par, cb) {
                    var pars = angular.copy(data.search);
                    pars.Code = par.Code;
                    pars.Name = par.Name;
                    pars.Id = par.Id;
                    var func = "/Save_SKU";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            cb(res);
                        }
                    })
                },
                keyin_for_cont: function (cb) {
                    var par1 = document.getElementById('JobNo');
                    var pars = angular.copy(data.search);
                    pars.JobNo = par1.value;
                    var func = "/mutilpeUpdateContainer";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            vm.init();
                        }
                    })
                },
                copy_item:function(par,cb){
                    var pars = angular.copy(data.search);
                    pars.Id = par.Id;
                    var func = "/Copy_Item";
                    SV_Common.http(func, pars, function (res) {
                        if (cb) {
                            vm.init();
                            cb(res);
                            
                        }
                    })
                }
            }
            vm.init();
            return vm;
        })
        .controller('Ctrl_CargoController', function ($scope, SV_CargoService, $timeout, SV_Popup) {
            $scope.new_row = {};
            $scope.init = function () {
                $scope.vm = SV_CargoService.GetData();
                
                var par1 = document.getElementById('JobNo');
                var par2 = document.getElementById('CargoType');
                var in_th = document.getElementById('in_th');
                var out_th = document.getElementById('out_th');
                var storage_type_th = document.getElementById('storage_type_th');
                var billing_start_th = document.getElementById('billing_start_th');
                var bill_date_th = document.getElementById('bill_date_th');
                var add = document.getElementById('add');
                if (par2.value == "IN") {
                    in_th.style.display = 'block';
                    out_th.style.display = 'none';
                    add.style.display = 'block';
                }
                if (par2.value == "OUT") {
                    in_th.style.display = 'none';
                    out_th.style.display = 'block';
                    add.style.display = 'none';
                    billing_start_th.style.display = 'none';
                    storage_type_th.style.display = 'none';
                    bill_date_th.style.display = 'none';
                }
            },
            $scope.action = {
                data:['cont','trip','item','location'],
                add_data: function () {
                    SV_CargoService.add($scope.vm, $scope.action.add_data_callback);
                },
                add_data_callback: function (res) {
                    if (res.status == true) {
                        if (parent.parent.notice) {
                            parent.parent.notice('Add Successful！');
                        }
                    }
                },
                copy_data: function (id,count) {
                    SV_CargoService.copy(id, count, $scope.action.copy_data_callback);
                },
                copy_data_callback: function (res) {
                    if (res.status == true) {
                        if (parent.parent.notice) {
                            parent.parent.notice('Add Successful！');
                        }
                    }
                },
                save_data: function () {
                    SV_CargoService.save($scope.vm.list, $scope.action.save_data_callback);
                },
                save_data_callback: function (res) {
                    if (res.status == true) {
                        if (parent.parent.notice) {
                            parent.parent.notice('Save Successful！');
                        }
                    }
                },
                del_data: function (par) {
                    SV_CargoService.del(par, $scope.action.del_data_callback);
                },
                del_data_callback: function (res) {
                    if (res.status == true) {
                        if (parent.parent.notice) {
                            parent.parent.notice('Delete Successful！');
                        }
                    }
                },
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
                    $scope.new_row.ContNo = par.ContainerNo;
                    $scope.new_row.ContId = par.Id;
                },
                booking_item_select: function (row) {
                    $scope.action.is_show('item');
                    $scope.new_row = row;
                    $scope.masterData.show_item($scope.vm.sku, $scope.action.booking_item_select_callback);
                },
                booking_item_select_callback: function (par) {
                    $scope.new_row.BookingItem = par.Code;
                    $scope.new_row.Marking2 = par.Name;
                },
                booking_sku_select: function (row) {
                    $scope.action.is_show('item');
                    $scope.new_row = row;
                    $scope.masterData.show_item($scope.vm.sku, $scope.action.booking_sku_select_callback);
                },
                booking_sku_select_callback: function (par) {
                    $scope.new_row.BkgSKuCode = par.Code;
                },
                actual_item_select: function (row) {
                    $scope.action.is_show('item');
                    $scope.new_row = row;
                    $scope.masterData.show_item($scope.vm.sku, $scope.action.actual_item_select_callback);
                },
                actual_item_select_callback: function (par) {
                    $scope.new_row.ActualItem = par.Code;
                },
                sku_code_select: function (row) {
                    $scope.action.is_show('item');
                    $scope.new_row = row;
                    $scope.masterData.show_item($scope.vm.sku, $scope.action.sku_code_select_callback);
                },
                sku_code_select_callback: function (par) {
                    $scope.new_row.SkuCode = par.Code;
                },
                location_select: function (row) {
                    $scope.action.is_show('location');
                    $scope.new_row = row;
                    $scope.masterData.show_loc($scope.vm.location, $scope.action.location_select_callback);
                },
                location_select_callback: function (par) {
                    $scope.new_row.Location = par.Code;
                },
                is_show:function(type){
                    for (var i = 0; i < $scope.action.data.length;i++){
                        var obj = $scope.action.data[i];
                        var n = document.getElementById(obj);
                        if (type == obj) {
                            n.style.display = "block";
                        }
                        else {
                            n.style.display = "none";
                        }
                        if (type == "item") {
                            document.getElementById('sku_add').style.display = "block";
                        }
                        else {
                            document.getElementById('sku_add').style.display = "none";
                        }
                    }
                },
                keyin_for_cont: function () {
                    SV_CargoService.keyin_for_cont($scope.action.keyin_for_cont_callback);
                },
                keyin_for_cont_callback: function (res) {
                    if (res.status == true) {
                        if (parent.parent.notice) {
                            parent.parent.notice('Action Successful！');
                        }
                    }
                },
                copy_item:function(row){
                    SV_CargoService.copy_item(row,$scope.action.copy_item_callback);
                },
                copy_item_callback: function (res) {
                    if (res.context == "1") {
                        if (parent.parent.notice) {
                            parent.parent.notice('Action Successful！');
                        }
                    }
                },
                return_job: function () {
                    var par1 = document.getElementById('JobNo');
                    parent.navTab.openTab(par1.value, "/PagesContTrucking/Job/JobEdit.aspx?no=" + par1.value, { title: par1.value, fresh: false, external: true });
                    window.close();
                }
            }
            $scope.masterData = {
                data: {
                    trips: [],
                    conts: [],
                    items: [],
                    locations:[],
                    no: '',
                    title: '',
                    sku: {
                        Code: '',
                        Name:''
                        
                    },
                    action:'',
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
                show_item: function (dd, cb) {
                    $scope.masterData.data.items = dd;
                    $scope.masterData.data.title = 'Item List';
                    $scope.masterData.selectCallback = cb;
                    $scope.masterData.popup.show($scope.masterData);
                },
                show_loc: function (dd, cb) {
                    $scope.masterData.data.locations = dd;
                    $scope.masterData.data.title = 'Location';
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
                add_new: function () {
                    $scope.masterData.data.action = 'New';
                    $scope.masterData.data.sku.Code = '';
                    $scope.masterData.data.sku.Name = '';
                    $("#new_item").removeClass("hidden");
                },
                cancel_add: function () {

                    $("#new_item").addClass("hidden");
                },
                sku_save: function () {
                    SV_CargoService.add_sku($scope.masterData.data.sku, $scope.masterData.add_data_callback);
                },
                update_sku: function (row) {
                    SV_CargoService.save_sku(row, $scope.masterData.save_data_callback);
                },
                add_data_callback: function (res) {
                    if (res.status == true) {
                        $scope.masterData.data.items = res.context.list;
                        $("#new_item").addClass("hidden");
                        if (parent.parent.notice) {
                            parent.parent.notice('Add Successful！');
                        }
                    }
                },
                save_data_callback: function (res) {
                    if (res.status == true) {
                        if (parent.parent.notice) {
                            parent.parent.notice('Save Successful！');
                        }
                    }
                },
                edit_sku: function (id) {
                    console.log(id);
                    $("#lbl_code_" + id).addClass("hidden");
                    $("#lbl_name_" + id).addClass("hidden");
                    $("#edit_" + id).addClass("hidden");
                    $("#code_" + id).removeClass("hidden");
                    $("#name_" + id).removeClass("hidden");
                    $("#update_" + id).removeClass("hidden");
                    $("#cancel_" + id).removeClass("hidden");
                },
                row_cancel: function (id) {
                    console.log(id);
                    $("#lbl_code_" + id).removeClass("hidden");
                    $("#lbl_name_" + id).removeClass("hidden");
                    $("#edit_" + id).removeClass("hidden");
                    $("#code_" + id).addClass("hidden");
                    $("#name_" + id).addClass("hidden");
                    $("#update_" + id).addClass("hidden");
                    $("#cancel_" + id).addClass("hidden");
                }
            }
            SV_Popup.SetPopup($scope.masterData, 'rightLarge');
            $scope.init();
        })
    </script>
</head>
<body ng-controller="Ctrl_CargoController">
    <form id="form1" runat="server">
        <div class="form">
            <div class="body">
                <div style="display: none">
                    <input id="JobNo" runat="server" type="text" readonly="true" class="control onlyread" />
                    <input id="CargoType" runat="server" type="text" />
                    <input id="Client" runat="server" type="text" />
                </div>
                <table class="table table-hover table-responsive table-bordered">
                    <tr>
                        <td>
                            <div id="add" style="display: none">
                                <input type="button" class="single_button_110 button" value="Add New" ng-click="action.add_data();" />
                            </div>

                        </td>
                        <td>
                            <input type="button" class="single_button_110 button" value="Keyin for Container" ng-click="action.keyin_for_cont();" />
                        </td>
                        <td width="90%"></td>
                        <td>
                            <input type="button" class="single_button_110 button" value="Save" ng-click="action.save_data();" />
                        </td>
                         <td>
                            <input type="button" class="single_button_110 button" value="Return Job" ng-click="action.return_job();" />
                        </td>
                    </tr>
                </table>
                <table class="table table-hover table-responsive table-bordered">
                    <thead>
                        <tr style="background: #ccc">
                            <td rowspan="2">#</td>
                            <td rowspan="2">#</td>
                            <th width="100" rowspan="2">Status</th>
                            <th width="150" rowspan="2">Line Id</th>
                            <th width="180" colspan="8">Lot No/BL/Container</th>
                            <th width="180" rowspan="2">Marking</th>
                            <th width="100" rowspan="2">Inventory Id</th>
                            <th width="180" rowspan="2">Description</th>
                            <th width="260" colspan="5">Booking Info</th>
                            <th width="260" colspan="4">Booking SKU Info</th>
                            <th width="100" colspan="3">Booking Dimension</th>
                            <th width="260" colspan="5">Actual Info</th>
                            <th width="260" colspan="4">Actual SKU Info</th>
                            <th width="100" colspan="3">Actual Dimension</th>
                            <th width="260" colspan="5">Balance Info</th>
                            <th rowspan="2">
                                <div id="in_th">
                                    In Date
                                </div>
                                <div id="out_th">
                                    Out Date
                                </div>
                            </th>
                            <th rowspan="2" id="billing_start_th">Billing Start Date
                            </th>
                            <th rowspan="2" id="storage_type_th">StorageType</th>
                            <th rowspan="2" id="bill_date_th">Bill Date</th>
                            <th rowspan="2">Storage Rate</th>
                            <th rowspan="2">Remark</th>
                            <th colspan="4">Status</th>
                        </tr>
                        <tr style="background: #ccc">
                            <th>Lot No</th>
                            <th>NAV Dept</th>
                            <th>BL/Bkg No</th>
                            <th>Container</th>
                            <th>Trip</th>
                            <th>Permit</th>
                            <th>PoNo</th>
                            <th>Type</th>

                            <%--Booking Info--%>
                            <th>Qty</th>
                            <th>Unit</th>
                            <th>Weight</th>
                            <th>Volume</th>
                            <th>Item Code</th>

                            <%--Booking SKU Info--%>
                            <th>SKU Code</th>
                            <th>Qty</th>
                            <th>Unit</th>
                            <th>#</th>

                            <%--Booking Dimension--%>
                            <th>Length</th>
                            <th>Width</th>
                            <th>Height</th>

                            <%--Actual Info--%>
                            <th>Qty</th>
                            <th>Unit</th>
                            <th>Weight</th>
                            <th>Volume</th>
                            <th>Item Code</th>

                            <%--Actual SKU Info--%>
                            <th>SKU Code</th>
                            <th>Qty</th>
                            <th>Unit</th>
                            <th>Location</th>

                            <%--Actual Dimension--%>
                            <th>Length</th>
                            <th>Width</th>
                            <th>Height</th>

                            <%--Banlance--%>
                            <th>Qty</th>
                            <th>Unit</th>
                            <th>Weight</th>
                            <th>Volume</th>
                            <th>SKU Qty</th>

                            <th>Landing</th>
                            <th>DG Cargo</th>
                            <th>Damage</th>
                            <th>DMG Remark</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr ng-show="vm.list.length>0" ng-repeat="h in vm.list">
                            <td>
                                <input type="button" value="Delete" class="single_button_110 button" ng-click="action.del_data(h.Id);" />
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <select class="width-40 control" id="Count" ng-model="h.Count">
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                            </select></td>
                                        <td>
                                            <input type="button" value="Add Cargo" class="single_button_110 button" ng-click="action.copy_data(h.Id,h.Count);" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <select ng-model="h.CargoStatus" class="width-110 control">
                                    <option value="P">Pending</option>
                                    <option value="C">Completed</option>
                                </select>
                            </td>
                            <td>{{h.LineId}}
                            </td>
                            <td>
                                <input type="text" ng-model="h.BookingNo" class="width-140 control" /></td>
                            <td>
                                <input type="text" ng-model="h.Commodity" class="width-40 control" /></td>
                            <td>
                                <input type="text" ng-model="h.HblNo" class="width-100 control" /></td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <input type="text" width="120" ng-model="h.ContNo" class="width-120 control" /></td>
                                        <td>
                                            <a class="btn btn-default btn-sm" ng-click="action.cont_select(h);">&nbsp;...&nbsp;</a>
                                        </td>
                                    </tr>
                                </table>
                                <div style="display: none">
                                    <input type="text" id="txt_ContId" name="txt_ContId" class="width-60" ng-model="h.ContId" />
                                </div>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <input type="text" width="120" ng-model="h.TripIndex" class="width-160 control" /></td>
                                        <td>
                                            <a class="btn btn-default btn-sm" ng-click="action.trip_select(h);">&nbsp;...&nbsp;</a>
                                        </td>
                                    </tr>
                                </table>
                                <div style="display: none">
                                    <input type="text" id="txt_TripId" name="txt_TripId" width="60" ng-model="h.TripId" />
                                </div>
                            </td>
                            <td>{{h.PermitNo}}
                            </td>
                            <td>
                                <input type="text" class="width-140 control" ng-model="h.PoNo" /></td>
                            <td>
                                <select ng-model="h.OpsType" class="control width-100" ng-options="row.Code as row.Code for row in vm.type|orderBy:'Code'"></select>
                            </td>
                            <td>
                                <textarea rows="3" class="width-140 textarea" ng-model="h.Marking1"></textarea>
                            </td>
                            <td>
                                <input type="text" ng-model="h.InventoryId" class="width-100 control" />
                            </td>
                            <td>
                                <textarea rows="3" ng-model="h.Marking2" class="width-140 textarea"></textarea></td>
                            <%--Booking Info--%>
                            <td>
                                <input type="number" class="width-60 control" min="0" ng-model="h.Qty" /></td>
                            <td>
                                <select ng-model="h.UomCode" class="width-80 control" ng-options="row.Code as row.Code for row in vm.uom|orderBy:'Code'"></select>
                            </td>
                            <td>
                                <input type="number" min="0" class="width-80 control" ng-model="h.Weight" /></td>
                            <td>
                                <input type="number" min="0" class="width-80 control" ng-model="h.Volume" /></td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <input type="text" width="120" ng-model="h.BookingItem" class="width-140 control" /></td>
                                        <td>
                                            <a class="btn btn-default btn-sm" ng-click="action.booking_item_select(h);">&nbsp;...&nbsp;</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--Booking SKU Info--%>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <input type="text" width="120" ng-model="h.BkgSKuCode" class="width-140 control" /></td>
                                        <td>
                                            <a class="btn btn-default btn-sm" ng-click="action.booking_sku_select(h);">&nbsp;...&nbsp;</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <input type="number" min="0" ng-model="h.BkgSkuQty" class="width-60 control" /></td>
                            <td>
                                <select ng-model="h.BkgSkuUnit" data-placeholder="Select Uom" id="BkgSkuUnit"
                                    tabindex="-1" data-live-search="true"
                                    name="BkgSkuUnit" class="selectpicker show-tick control width-80" ng-options="row.Code as row.Code for row in vm.uom|orderBy:'Code'">
                                </select>
                            </td>
                            <td>
                                <a class="btn btn-primary btn-sm" ng-click="action.copy_item(h);">Copy</a>
                            </td>
                            <%--Booking Dimension--%>
                            <td>
                                <input type="number" min="0" class="width-60 control" ng-model="h.Length" /></td>
                            <td class="ctl">
                                <input type="number" min="0" class="width-80 control" ng-model="h.Width" />
                            </td>
                            <td class="ctl">
                                <input type="number" min="0" class="width-80 control" ng-model="h.Height" />
                            </td>
                            <%--Actual Info--%>
                            <td class="ctl">
                                <input type="number" min="0" class="width-60 control" ng-model="h.QtyOrig" />
                            </td>
                            <td class="ctl">
                                <select ng-model="h.PackTypeOrig" data-placeholder="Select Uom" id="PackTypeOrig"
                                    tabindex="-1" data-live-search="true"
                                    name="PackTypeOrig" class="selectpicker show-tick control width-80" ng-options="row.Code as row.Code for row in vm.uom|orderBy:'Code'">
                                </select>
                            </td>

                            <td class="ctl">
                                <input type="number" min="0" class="width-60 control" ng-model="h.WeightOrig" />
                            </td>
                            <td class="ctl">
                                <input type="number" min="0" class="width-60 control" ng-model="h.VolumeOrig" />
                            </td>
                            <td class="ctl">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <input type="text" width="120" ng-model="h.ActualItem" class="width-140 control" /></td>
                                        <td>
                                            <a class="btn btn-default btn-sm" ng-click="action.actual_item_select(h);">&nbsp;...&nbsp;</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--Actual SKU Info--%>
                            <td class="ctl">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <input type="text" width="120" ng-model="h.SkuCode" class="width-140 control" /></td>
                                        <td>
                                            <a class="btn btn-default btn-sm" ng-click="action.sku_code_select(h);">&nbsp;...&nbsp;</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <input type="number" min="0" ng-model="h.PackQty" class="width-60 control" />
                            </td>
                            <td>
                                <select ng-model="h.PackUom" data-placeholder="Select Uom" id="PackUom"
                                    tabindex="-1" data-live-search="true"
                                    name="PackUom" class="selectpicker show-tick control width-80" ng-options="row.Code as row.Code for row in vm.uom|orderBy:'Code'">
                                </select>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <input type="text" width="120" ng-model="h.Location" class="width-140 control" /></td>
                                        <td>
                                            <a class="btn btn-default btn-sm" ng-click="action.location_select(h);">&nbsp;...&nbsp;</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--Actual Dimension--%>
                            <td class="ctl">
                                <input type="number" min="0" ng-model="h.LengthPack" class="width-60 control" />
                            </td>
                            <td class="ctl">
                                <input type="number" min="0" ng-model="h.WidthPack" class="width-60 control" />
                            </td>
                            <td class="ctl">
                                <input type="number" min="0" ng-model="h.HeightPack" class="width-60 control" />
                            </td>
                            <td class="ctr" style="width: 100px">{{h.BalQty}}
                            </td>
                            <td>{{h.PackTypeOrig}}
                            </td>
                            <td>{{h.BalWeight}}
                            </td>
                            <td>{{h.BalVolume}}
                            </td>
                            <td>{{h.BalPack}}
                            </td>
                            <td>
                                <input type="date" ng-model="h.StockDate" class="width-140 control" /></td>
                            <td ng-show="h.CargoType=='IN'">
                                <input type="date" ng-model="h.BillingStartDate" class="width-140 control" /></td>
                            <td ng-show="h.CargoType=='IN'">
                                <select ng-model="h.StorageType" class="width-110 control">
                                    <option value="Daily">Daily</option>
                                    <option value="Weekly">Weekly</option>
                                    <option value="Monthly">Monthly</option>
                                    <option value="Yearly">Yearly</option>
                                    <option value=""></option>
                                </select></td>
                            <td ng-show="h.CargoType=='IN'">{{h.NextBillDate}}
                            </td>
                            <td class="ctl">
                                <input type="number" min="0" ng-model="h.Collect_Amount1" class="width-60 control" />
                            </td>
                            <td>
                                <textarea ng-model="h.Remark1" class="textarea width-140"></textarea></td>
                            <td>
                                <select ng-model="h.LandStatus" class="width-110 control">
                                    <option value="Normal">Normal</option>
                                    <option value="Shortland">Shortland</option>
                                    <option value="Overland">Overland</option>
                                    <option value=""></option>
                                </select>
                            </td>
                            <td>
                                <select ng-model="h.DgClass" class="width-110 control">
                                    <option value="Normal">Normal</option>
                                    <option value="Class 2">Class 2</option>
                                    <option value="Class 3">Class 3</option>
                                    <option value="Other Class">Other Class</option>
                                    <option value=""></option>
                                </select>
                            </td>
                            <td>
                                <select ng-model="h.DamagedStatus" class="width-110 control">
                                    <option value="Normal">Normal</option>
                                    <option value="Damaged">Damaged</option>
                                    <option value=""></option>
                                </select>
                            </td>
                            <td>
                                <textarea ng-model="h.Remark2" class="width-100 textarea"></textarea>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
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
                                    <div id="sku_add">
                                        <a class="btn btn-default btn-sm" ng-click="masterData.add_new();">&nbsp;Add New&nbsp;</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="bx_table bx_table_100pc" id="trip">
                            <div class="header">
                                <div class="col_100">Trip No</div>
                                <div>Trailer No</div>
                                <div>Destination</div>
                                <div>ParkingLot</div>
                                <div>Driver</div>
                                <div>Date</div>
                            </div>
                            <div class="body">
                                <div class="item" ng-repeat="row in masterData.data.trips|filter:masterData.data.no" ng-click="masterData.select(row);">
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
                        <div class="bx_table bx_table_100pc" id="item">
                            <div class="header">
                                <div class="col_50">#</div>
                                <div class="col_100">Code</div>
                                <div>Desctiption</div>
                                <div></div>
                                <div></div>
                            </div>
                            <div class="body">
                                <div class="item hidden" id="new_item">
                                    <div></div>
                                    <div>
                                        <textarea rows="3" id="Code" ng-model="masterData.data.sku.Code" class="single_text width-120 textarea"></textarea>
                                    </div>
                                    <div>
                                        <textarea rows="3" ng-model="masterData.data.sku.Name" class="single_text textarea"></textarea>
                                    </div>
                                    <div><a class="btn btn-primary btn-sm" ng-click="masterData.sku_save();">Save</a></div>
                                    <div><a class="btn btn-default btn-sm" ng-click="masterData.cancel_add();">Cancel</a></div>
                                </div>
                                <div class="item" id="item_list" ng-repeat="row in masterData.data.items|filter:masterData.data.no|orderBy:'Code'">
                                    <div>
                                        <a class="btn-link" ng-click="masterData.select(row);">select</a>
                                    </div>
                                    <div>
                                        &nbsp;
                                    <div id="lbl_code_{{row.Id}}">
                                        {{row.Code}}
                                    </div>
                                        <div class="hidden" id="code_{{row.Id}}">
                                            <textarea rows="3" ng-model="row.Code" class="single_text textarea"></textarea>
                                        </div>
                                    </div>
                                    <div>
                                        <div id="lbl_name_{{row.Id}}">
                                            {{row.Name}}
                                        </div>
                                        <div class="hidden" id="name_{{row.Id}}">
                                            <textarea rows="3" ng-model="row.Name" class="single_text textarea"></textarea>
                                        </div>
                                    </div>
                                    <div id="edit_{{row.Id}}">
                                        <a class="btn btn-default btn-sm" ng-click="masterData.edit_sku(row.Id);">Edit</a>
                                        <input type="hidden" ng-model="row.Id" />
                                    </div>
                                    <div id="update_{{row.Id}}" class="hidden">
                                        <a class="btn btn-primary btn-sm" ng-click="masterData.update_sku(row);">Update</a>
                                    </div>
                                    <div id="cancel_{{row.Id}}" class="hidden">
                                        <a class="btn btn-default btn-sm" ng-click="masterData.row_cancel(row.Id);">Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="bx_table bx_table_100pc" id="location">
                            <div class="header">
                                <div class="col_100">Code</div>
                                <div>Desctiption</div>
                            </div>
                            <div class="body">

                                <div class="item" ng-repeat="row in masterData.data.locations|filter:masterData.data.no" ng-click="masterData.select(row);">
                                    <div>&nbsp;{{row.Code}}</div>
                                    <div>{{row.Name}}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
