angular.module('starter.controllers', [])


.controller('Ctrl_Menu', function ($scope, $state, SV_AppStyle, $ionicSideMenuDelegate, $ionicModal, SV_User, SV_JobList, SV_Schedule, SV_Login, SV_DeviceData, SV_Hinter, $q) {
    SV_AppStyle.SetFix($scope);
    $scope.sideMenus = {
        initingDevice: null,
        initingDom: null,
        login: function () {
            SV_User.Logout();
            $state.go('menu.login');
        },
        openPage: function (uri) {
            $state.go('menu.' + uri);
        },
        openDashboard: function (par) {
            $state.go('menu.dashboard1', { No: par });
        },
        style: function () {
            $scope.style.openModal();
            $ionicSideMenuDelegate.toggleLeft();
        },
        user: function () {
            $state.go('menu.user');
        },
        exit: function () {
            //navigator.app.exitApp();
            ionic.Platform.exitApp();
        }
    }
    $scope.User = SV_User.GetData();
    $scope.action = {
        permis: function (code, type) {
            return SV_User.Permis(code, type);
        }
    }

    $scope.style = {
        modal: null,
        openModal: function () {
            $scope.style.modal.show();
        },
        closeModal: function () {
            $scope.style.modal.hide();
        }
    };
    $scope.sideMenus.initingDevice = $q.defer();
    $scope.sideMenus.initingDom = $q.defer();
    $q.all([$scope.sideMenus.initingDevice.promise, $scope.sideMenus.initingDom.promise]).then(function (d) {
        //console.log('=========== left menu readyed');
        try {
            SV_DeviceData.GetData();
            SV_Hinter.GetNotificationDefault();
            SV_Hinter.SetNotificationOnclick(function (id, state, json) {
                var noticeName = SV_Hinter.GetNotificationName_ById(id);
                if (noticeName == 'chat') {
                    $state.go('menu.message');
                }
                if (noticeName == 'group') {
                    $state.go('menu.messagegroup');
                }
                var ar_notice = noticeName.split('|');
                if (ar_notice.length == 2) {
                    if (ar_notice[0] == 'lcl') {
                        $state.go('menu.local_jobdetail', { No: ar_notice[1] });
                    }
                    if (ar_notice[0] == 'fcl') {
                        $state.go('menu.jobdetail', { No: ar_notice[1] });
                    }
                }
            })
        }
        catch (e) { }
    })
    ionic.Platform.ready(function () {
        $scope.sideMenus.initingDevice.resolve();
    });

    ionic.DomUtil.ready(function () {
        $scope.sideMenus.initingDom.resolve();
    });

    $ionicModal.fromTemplateUrl('templates/modal_style.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.style.modal = modal;
    });

})

.controller('Ctrl_Login', function ($scope, SV_Login, SV_Company, $state, $ionicPopup) {
    $scope.company = SV_Company.GetData();
    $scope.vm = SV_Login.GetData();
    //console.log($scope.company, $scope.vm);
    $scope.action = {
        login: function () {
            SV_Login.Login($scope.action.login_callback);
        },
        login_callback: function (par) {
            if (par.status == "1") {
                //SV_User.Login(par.context);
                $state.go('menu.dashboard');
            } else {
                var title = "";
                if (par.context == "inexistence") {
                    title = "The MobileNo or UserName is not Exist";
                }
                if (par.context == "passworderror") {
                    title = "Password Error";
                }
                $ionicPopup.alert({
                    title: title,
                });
            }
        }
    }
})

.controller('Ctrl_User', function ($scope, SV_User, SV_Company, $state, $ionicNavBarDelegate, SV_Login) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    $scope.User = SV_User.GetData();
    $scope.Company = SV_Company.GetData();

    $scope.action = {
        style: function () {
            $scope.style.openModal();
        },
        layout: function () {
            //SV_User.Logout();
            SV_Login.Logout();
            $state.go('menu.login');
        },
        exit: function () {
            //navigator.app.exitApp();

            ionic.Platform.exitApp();
        }
    }
})


.controller('Ctrl_JobList', function ($scope, SV_JobList, SV_JobAdd, $ionicModal, $state, $ionicPopup) {
    $scope.job = SV_JobList.GetData();
    $scope.action = {
        search: function () {
            if ($scope.job.search.length == 0) {
                $ionicPopup.alert({ title: 'Required Search' });
                return;
            }
            SV_JobList.RefreshData($scope.action.search_callback);
        },
        search_callback: function (par) {

        },
        scan: function () {
            cordova.plugins.barcodeScanner.scan(function (result) {
                if (!result.cancelled) {
                    $scope.job.search = result.text;
                    SV_JobList.RefreshData($scope.action.search_callback);
                }
            }, function (e) {
                $ionicPopup.alert({
                    title: 'Scanning Error',
                    //template: e
                });
            });
        },
        addNew: function () {
            $scope.new.openModal();
        },
        openDetail: function (No) {
            $state.go('menu.jobdetail', { No: No });
        }
    }

    $scope.new = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.new.modal.hide();
        },
        openModal: function () {
            $scope.new.modal.show();
        },
        save: function () {
            SV_JobAdd.Save($scope.new.save_callback);
        },
        save_callback: function (par) {
            $scope.new.closeModal();
            //console.log(par);
            $state.go('menu.jobdetail', { No: par.context });
        }
    }
    $scope.new.data = SV_JobAdd.GetData();


    //================== modal

    $scope.$on('$destroy', function () {
        $scope.new.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.new.modal = modal;
    });
})

.controller('Ctrl_JobDetail', function ($scope, $stateParams, $ionicNavBarDelegate, SV_JobDetail, SV_MasterData, $ionicPopup, $ionicModal, $ionicPopover, SV_Modal, SV_User, $state) {
    SV_MasterData.RefreshData();
    SV_MasterData.SetFix($scope);
    SV_Modal.Modal_Upload($scope);
    SV_Modal.Popup_Progress($scope, 'Upload');
    SV_Modal.Modal_FullScreen_Image($scope);
    SV_Modal.Modal_Signature($scope);
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }

    ionic.DomUtil.ready(function () {
        SV_JobDetail.RefreshData($scope.vm.JobNo);
        //console.log($scope.vm);
    });

    $scope.vm = {
        JobNo: $stateParams.No,
        job: SV_JobDetail.GetData().job,
    }
    //========================= info
    $scope.info = {
        pol_select: function () {
            $scope.Fix_MasterData.port.openModal($scope.info.pol_select_callback, $scope.vm.job.info.Pol);
        },
        pol_select_callback: function (par) {
            $scope.vm.job.info.Pol = par;
        },
        pod_select: function () {
            $scope.Fix_MasterData.port.openModal($scope.info.pod_select_callback, $scope.vm.job.info.Pod);
        },
        pod_select_callback: function (par) {
            $scope.vm.job.info.Pod = par;
        },
        party_select: function (par) {
            switch (par) {
                case 'ClientId':
                    $scope.Fix_MasterData.party.openModal($scope.info.client_select_callback, $scope.vm.job.info.ClientId);
                    break;
                case 'CarrierId':
                    $scope.Fix_MasterData.party.openModal($scope.info.carrier_selelct_callback, $scope.vm.job.info.CarrierId);
                    break;
                case 'HaulierId':
                    $scope.Fix_MasterData.party.openModal($scope.info.haulier_select_callback, $scope.vm.job.info.HaulierId);
                    break;
            }
        },
        client_select_callback: function (par) {
            $scope.vm.job.info.ClientId = par;
        },
        carrier_selelct_callback: function (par) {
            $scope.vm.job.info.CarrierId = par;
        },
        haulier_select_callback: function (par) {
            $scope.vm.job.info.HaulierId = par;
        },
        save: function () {
            SV_JobDetail.Info_Save($scope.info.save_callback)
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par == "1") {
                title = "Save Success";
            }
            $ionicPopup.alert({ title: title });
        },

    }

    //========================= container
    $scope.container = {
        modal: null,
        data: {},
        openModal: function () {
            $scope.container.modal.show();
        },
        closeModal: function () {
            $scope.container.modal.hide();
        },
        edit: function (row) {
            $scope.container.data = angular.copy(row);
            $scope.container.data.Weight = parseFloat($scope.container.data.Weight);
            $scope.container.data.Volume = parseFloat($scope.container.data.Volume);
            $scope.container.data.QTY = parseInt($scope.container.data.QTY);
            //console.log($scope.container.data);
            $scope.container.openModal();
        },
        add: function () {
            SV_JobDetail.Container_GetNew($scope.container.add_callback);
            $scope.container.openModal();
        },
        add_callback: function (par) {
            $scope.container.data = par;
        },
        container_select: function () {
            $scope.Fix_MasterData.container.openModal($scope.container.container_select_callback, $scope.container.data.ContainerNo);
        },
        container_select_callback: function (par) {
            var temp = angular.fromJson(par);
            $scope.container.data.ContainerNo = temp.ContainerNo;
            $scope.container.data.ContainerType = temp.ContainerType;
        },
        save: function () {
            var dd = $scope.container.data;
            if (!dd.ContainerNo || dd.ContainerNo.length == 0) {
                $ionicPopup.alert({ title: 'ContainerNo is required' });
                return;
            }
            SV_JobDetail.Container_Save(dd, $scope.container.save_callback);
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par.status == "1") {
                title = "Save Success";
                $scope.container.closeModal();
            }
            $ionicPopup.alert({ title: title });
        }
    }

    //========================  trip
    $scope.trip = {
        modal: null,
        data: {},
        openModal: function () {
            $scope.trip.modal.show();
        },
        closeModal: function () {
            $scope.trip.modal.hide();
        },
        add: function () {
            SV_JobDetail.Trip_GetNew($scope.trip.add_callback);
            $scope.trip.openModal();
        },
        add_callback: function (par) {
            $scope.trip.data = par;
        },
        edit: function (row) {
            $scope.trip.data = row;
            row.FromDate = new Date(row.FromDate).DateFormat('yyyy-MM-dd');
            //row.FromTime = date.DateFormat('hh:mm');
            row.ToDate = new Date(row.ToDate).DateFormat('yyyy-MM-dd');
            //row.ToTime = date.DateFormat('hh:mm');
            $scope.trip.openModal();
        },
        save: function () {
            var dd = $scope.trip.data;
            //if (!dd.ContainerNo || dd.ContainerNo.length == 0) {
            //    $ionicPopup.alert({ title: 'ContainerNo is required' });
            //    return;
            //}
            SV_JobDetail.Trip_Save(dd, $scope.trip.save_callback);
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par.status != "0") {
                title = "Save Success";
                $scope.trip.closeModal();
            }
            $ionicPopup.alert({ title: title });
        },
        driver_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.trip.driver_select_callback, $scope.trip.data.DriverCode, 'Driver');
        },
        driver_select_callback: function (par) {
            $scope.trip.data.DriverCode = par.Code;
        },
        towhead_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.trip.towhead_select_callback, $scope.trip.data.TowheadCode, 'Towhead');
        },
        towhead_select_callback: function (par) {
            $scope.trip.data.TowheadCode = par.Code;
        },
        trail_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.trip.trail_select_callback, $scope.trip.data.ChessisCode, 'Trail');
        },
        trail_select_callback: function (par) {
            $scope.trip.data.ChessisCode = par.Code;
        },
        container_changed: function () {
            $scope.trip.data.ContainerNo;
            for (var i = 0; i < $scope.vm.job.container.length; i++) {
                if ($scope.vm.job.container[i].ContainerNo == $scope.trip.data.ContainerNo) {
                    $scope.trip.data.Det1Id = $scope.vm.job.container[i].Id;
                    break;
                }
            }
            //console.log($scope.trip.data);
        }
    }

    //========================== attachment
    $scope.attachment = {
        popover: null,
        openPopover: function ($event) {
            $scope.attachment.popover.show($event);
        },
        closePopover: function () {
            $scope.attachment.popover.hide();
        },
        openCamera: function () {
            $scope.attachment.closePopover();

            $scope.Modal_Upload.then = $scope.attachment.getImage_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Upload.setSharpness(10);
            $scope.Modal_Upload.openCamera();
        },
        takePicture: function () {
            $scope.attachment.closePopover();
            $scope.Modal_Upload.then = $scope.attachment.getImage_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Upload.setSharpness(10);
            $scope.Modal_Upload.openAlbum();
        },
        drawing: function () {
            $scope.attachment.closePopover();
            $scope.Modal_Signature.then = $scope.attachment.getImage_callback;
            $scope.Modal_Signature.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Signature.openModal();
        },
        getImage_callback: function (par) {
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.JobNo + '/' + temp.FileName;
            temp.JobType = 'CTM';
            temp.RefNo = $scope.vm.JobNo;
            temp.FileType = 'Image';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_JobDetail.Attachment_Add(temp);
        },
        AddAttach: function () {
            $scope.attachment.closePopover();
            $scope.Modal_Upload.then = $scope.attachment.uploadAttach_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Upload.openAttach();
        },
        uploadAttach_callback: function (par) {
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            //temp.FilePath = 'Upload/' + $scope.vm.JobNo + '/' + temp.FileName;
            temp.FilePath = $scope.vm.JobNo + '/' + temp.FileName;
            temp.JobType = 'Tpt';
            temp.RefNo = $scope.vm.JobNo;
            temp.FileType = f_GetFileType(temp.FileName);
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_JobDetail.Attachment_Add(temp);
        },
        Message: {
            modal: null,
            data: '',
            openModal: function () {
                $scope.attachment.closePopover();
                $scope.attachment.Message.data = '';
                $scope.attachment.Message.modal.show();
            },
            closeModal: function () {
                $scope.attachment.Message.modal.hide();
            },
            upload: function () {
                if ($scope.attachment.Message.data.length == 0) {
                    $scope.attachment.Message.closeModal();
                    return;
                }
                var temp = {};
                //temp.FileName = par.substr(par.lastIndexOf('/') + 1);
                temp.FilePath = $scope.vm.JobNo + '/' + temp.FileName;
                temp.JobType = 'Tpt';
                temp.RefNo = $scope.vm.JobNo;
                temp.FileType = '';
                temp.CreateBy = SV_User.GetUserName();
                temp.FileNote = $scope.attachment.Message.data;
                SV_JobDetail.Activity_Add(temp, function (par) {
                    $scope.attachment.Message.closeModal();
                });
            }
        }
    }

    //========================== signature
    $scope.signature = {
        data: {},
        type: '',
        drawing: function (sign, type) {
            $scope.signature.data = sign;
            $scope.signature.type = type;
            $scope.Modal_Signature.then = $scope.signature.drawing_callback;
            $scope.Modal_Signature.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Signature.openModal();
        },
        drawing_callback: function (par) {
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.JobNo + '/' + temp.FileName;
            temp.JobType = 'CTM';
            temp.RefNo = $scope.vm.JobNo;
            temp.FileType = 'Signature';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = $scope.signature.type;
            SV_JobDetail.Signature_Add(temp);
        }
    }

    //========================== charge
    $scope.charge = {
        add: function () {
            SV_JobDetail.Charge_Add($scope.charge.add_callback);
        },
        add_callback: function (par) {
            var title = 'Save False';
            if (par.status == "1") {
                title = 'Save Success';
            }
            if (par.status == "2") {
                title = 'Exist Charge Items';
            }
            $ionicPopup.alert({ title: title });
        },
        modal: null,
        data: {},
        openModal: function () {
            $scope.charge.modal.show();
        },
        closeModal: function () {
            $scope.charge.modal.hide();
        },
        edit: function (row) {
            $scope.charge.data = row;
            row.Cost = parseFloat(row.Cost, 10);
            $scope.charge.openModal();
        },
        save: function () {
            SV_JobDetail.Charge_Save($scope.charge.data, $scope.charge.save_callback);
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par.status == "1") {
                title = "Save Success";
                $scope.charge.closeModal();
            }
            $ionicPopup.alert({ title: title });
        }
    }

    $scope.billing = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.billing.modal.hide();
        },
        openModal: function () {
            $scope.billing.modal.show();
        },
        addNew: function () {
            $scope.billing.data.MastRefNo = $scope.vm.JobNo;
            $scope.billing.data.MastType = 'CTM';
            $scope.billing.data.DocType = 'IV';
            $scope.billing.openModal();
        },
        save: function () {
            SV_JobDetail.Billing_Add($scope.billing.data, $scope.billing.save_callback);
        },
        save_callback: function (par) {
            if (par.status == '0') {
                $ionicPopup.alert({ title: 'Save False' });
                return;
            }
            $scope.billing.closeModal();
            //console.log(par);
            var url = 'menu.invoice_detail';
            if ($scope.billing.data.DocType == 'PVG') {
                url = 'menu.payment_detail';
            }
            $state.go(url, { No: par.context });
        },
        openDetail: function (row) {
            console.log(row);
            var url = 'menu.invoice_detail';
            if (row.DocType == 'PVG') {
                url = 'menu.payment_detail';
            }
            $state.go(url, { No: row.DocNo });
        }
    }

    $scope.historyLog = {
        user: SV_User.GetUserName(),
        isOwn: function (row) {
            return row.Controller == $scope.historyLog.user;
        }
    }


    $scope.stock = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.stock.modal.hide();
        },
        openModal: function () {
            $scope.stock.modal.show();
        },
        addNew: function () {
            $scope.stock.data.JobNo = $scope.vm.JobNo;
            $scope.stock.data.StockStatus = 'IN';
            $scope.stock.openModal();
        },
        save: function () {
            SV_JobDetail.Stock_Add($scope.stock.data, $scope.stock.save_callback);
        },
        save_callback: function (par) {
            if (par.status == '0') {
                $ionicPopup.alert({ title: 'Save False' });
                return;
            }
            $scope.stock.closeModal();
            //console.log(par);
            $state.go('menu.stock_detail', { No: par.context });
        },
        openDetail: function (row) {
            $state.go('menu.stock_detail', { No: row.Id });
        }
    }

    //================== modal,popover

    $scope.$on('$destroy', function () {
        SV_MasterData.OnDestory($scope);
        $scope.Modal_Signature.destory();
        $scope.container.modal.remove();
        $scope.trip.modal.remove();
        $scope.charge.modal.remove();
        $scope.billing.modal.remove();
        $scope.stock.modal.remove();

        $scope.attachment.popover.remove();
        $scope.attachment.Message.modal.remove();
    });

    $ionicModal.fromTemplateUrl('templates/modal_job_container.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.container.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_trip.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.trip.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_charge.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.charge.modal = modal;
    });
    $ionicPopover.fromTemplateUrl('popover_attachments.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.attachment.popover = popover;
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_billing_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.billing.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_stock_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.stock.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_upload_text.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.attachment.Message.modal = modal;
    });

})


.controller('Ctrl_Schedule', function ($scope, $state, SV_Schedule) {
    $scope.vm = SV_Schedule.GetData();
    $scope.calendar = {
        selectDay: $scope.vm.selectDay,
        setSelectDay: function (row) {
            //$scope.calendar.selectDay = row.day;
            $scope.vm.selectDay = row.date;
            $scope.calendar.selectDay = row.date;
            //console.log('============== set select day',$scope.calendar.selectDay);
            SV_Schedule.RefeshScheduleList($scope.calendar.selectDay);
        },
        isSelectDay: function (par) {
            if (par == '' || !$scope.calendar.selectDay) { return ''; }
            var re = par.dateDiff('d', $scope.calendar.selectDay);

            //console.log($scope.calendar.selectDay, par, re);
            var result = '';
            if (re == 0) {
                result = 'td_select';
            }
            return result;
        },
        getSelectDay_yM: function () {
            var result = '';
            if ($scope.calendar.selectDay) {
                result = $scope.calendar.selectDay.getDay_e() + ', ' + $scope.calendar.selectDay.getDate() + ' ' + $scope.calendar.selectDay.getMonth_e() + ' ' + $scope.calendar.selectDay.getFullYear();//DateFormat('yyyy-MM');
            }
            return result;
        },
        weekdays_left: function () {
            var date = angular.copy($scope.vm.days[0].date);//new Date($scope.vm.days[0].date);
            date.setDate(date.getDate() - 7);
            //console.log(date, new Date($scope.vm.days[0].date));
            SV_Schedule.GetWeekdays(date, null);
        },
        weekdays_right: function () {
            var date = angular.copy($scope.vm.days[0].date);//new Date($scope.vm.days[0].date);
            date.setDate(date.getDate() + 7);
            SV_Schedule.GetWeekdays(date, null);
        },
        weekdays_center: function () {
            var date = new Date();
            SV_Schedule.GetWeekdays(date, $scope.calendar.weekdays_changed_today);
        },
        weekdays_changed_today: function () {
            $scope.vm.selectDay = new Date();
            $scope.calendar.selectDay = new Date();
            SV_Schedule.RefeshScheduleList($scope.calendar.selectDay);
        },
        onDrag_horizontal: 0,
        onDragLeft: function () {
            $scope.calendar.onDrag_horizontal++;
            //console.log('left');
        },
        onDragRight: function () {
            $scope.calendar.onDrag_horizontal--;
            //console.log('right');
        },
        onEvent: function (par) {
            if (par == 'touch') {
                $scope.calendar.onDrag_horizontal = 0;
            }
            if (par == 'release') {
                var re = $scope.calendar.onDrag_horizontal;
                $scope.calendar.onDrag_horizontal = 0;
                //console.log(re);
                if (re * re > 16) {
                    if (re > 0) {
                        $scope.calendar.weekdays_right();
                    } else {
                        $scope.calendar.weekdays_left();
                    }
                }
            }
        },
        showAll_toggle: function () {
            $scope.vm.showAll = !$scope.vm.showAll;
            SV_Schedule.RefeshScheduleList($scope.calendar.selectDay);
        }
    };
    $scope.job = {
        openJob: function (row) {
            $state.go('menu.jobdetail', { No: row.JobNo });
        },
        openTrip: function (row) {
            //console.log(row);
            $state.go('menu.scheduletrip', { No: row.Id });
        }
    }
})

.controller('Ctrl_ScheduleTrip', function ($scope, $stateParams, $ionicNavBarDelegate, SV_ScheduleTrip, SV_User, $ionicPopup, $ionicPopover, SV_MyStatus) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }

    $scope.vm = SV_ScheduleTrip.GetData();
    $scope.mystatus = SV_MyStatus.GetData();
    $scope.user = SV_User.GetUserName();
    SV_ScheduleTrip.RefreshData($stateParams.No);

    $scope.action = {
        popover: null,
        actionList: [],
        openPopover: function ($event) {
            $scope.action.popover.show($event);
        },
        closePopover: function () {
            $scope.action.popover.hide();
        },
        popover_action: function (row) {
            $scope.action.closePopover();
            console.log(row);
            if (row.data.status == 'S') {
                if (row.data.code == 'keep') {
                    $scope.vm.trip.ChessisCode = $scope.mystatus.status.Trail;
                    $scope.action.Status_Change_part1(row.data.status);
                }
                if (row.data.code == 'trip') {
                    $scope.mystatus.status.Trail = $scope.vm.trip.ChessisCode;
                    SV_MyStatus.trail_save(function () { $scope.action.Status_Change_part1(row.data.status) });
                }
            }
            if (row.data.status == 'C') {
                if (row.data.code == 'keep') {
                    $scope.action.Status_Change_part1(row.data.status);
                }
                if (row.data.code == 'takeoff') {
                    $scope.mystatus.status.Trail = '';
                    SV_MyStatus.trail_save(function () { $scope.action.Status_Change_part1(row.data.status) });
                }
            }
        },
        Status_Change: function (par, $event) {

            $scope.action.Status_Change_part1(par);
            return;

            //if (par == 'S') {
            //    if ($scope.mystatus.status.Trail == $scope.vm.trip.ChessisCode) {
            //        if ($scope.mystatus.status.Trail.length == 0) {
            //            $ionicPopup.alert({ title: 'No trail' });
            //            return;
            //        }
            //        //else {
            //        //    $scope.action.Status_Change_part1(par);
            //        //}
            //    }
            //    $scope.action.actionList = [];
            //    if ($scope.mystatus.status.Trail && $scope.mystatus.status.Trail.length > 0) {
            //        $scope.action.actionList.push({ data: { code: 'keep', status: par }, text: $scope.mystatus.status.Trail + '(connected)' });
            //    }
            //    if ($scope.vm.trip.ChessisCode && $scope.vm.trip.ChessisCode.length > 0) {
            //        $scope.action.actionList.push({ data: { code: 'trip', status: par }, text: $scope.vm.trip.ChessisCode });
            //    }
            //    $scope.action.actionList.push({ data: { code: 'cancel', status: par }, text: 'Cancel' });
            //    $scope.action.openPopover($event);
            //}
            //if (par == 'C') {
            //    $scope.action.actionList = [];
            //    if ($scope.mystatus.status.Trail && $scope.mystatus.status.Trail.length > 0) {
            //        $scope.action.actionList.push({ data: { code: 'keep', status: par }, text: $scope.mystatus.status.Trail + '(keep connect)' });
            //    }
            //    if ($scope.vm.trip.ChessisCode && $scope.vm.trip.ChessisCode.length > 0) {
            //        $scope.action.actionList.push({ data: { code: 'takeoff', status: par }, text: 'Take off' });
            //    }
            //    $scope.action.actionList.push({ data: { code: 'cancel', status: par }, text: 'Cancel' });
            //    $scope.action.openPopover($event);
            //}
        },
        Status_Change_part1: function (par) {
            var trip = $scope.vm.trip;
            var temp = {
                Id: trip.Id,
                JobNo: trip.JobNo,
                ContainerNo: trip.ContainerNo,
                Driver: trip.DriverCode,
                Towhead: trip.TowheadCode,
                Trail: trip.ChessisCode,
                User: SV_User.GetUserName(),
                Status: par
            }
            SV_ScheduleTrip.Status_Save(temp, $scope.action.Status_Change_callback);
        },
        Status_Change_callback: function (re) {
            var title = 'Save False';
            if (re == "1") {
                title = 'Save Success';
            }
            $ionicPopup.alert({ title: title });
        }
    }

    $scope.$on('$destroy', function () {
        $scope.action.popover.remove();
    });
    $ionicPopover.fromTemplateUrl('popover_schedule_tirp_status_changed.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.action.popover = popover;
    });
})


.controller('Ctrl_Schedule1', function ($scope, $state, SV_Schedule1, $ionicPopup, $ionicTabsDelegate, $timeout, $ionicModal, SV_JobAdd) {
    $scope.vm = {};

    var tab = SV_Schedule1.GetFocus();
    var tab_index = 0;
    if (tab) {
        switch (tab.name) {
            case 'Today':
                tab_index = 0;
                break;
            case 'This week':
                tab_index = 1;
                break;
            case 'Later':
                tab_index = 2;
                break;
            case 'Past':
                tab_index = 3;
                break;
        }
    }
    ionic.DomUtil.ready(function () {
        $scope.vm = SV_Schedule1.GetData();
        $scope.search.data = $scope.vm.search;
        //console.log($scope.vm);
        $timeout(function () {
            $ionicTabsDelegate.$getByHandle('handle_local_schedule1').select(tab_index);
        }, 100);
    });

    $scope.action = {
        onTabSelected: function (name, index) {
            SV_Schedule1.SetFocus(name);
        },
        Refresh_DropDown: function (name) {
            SV_Schedule1.Refresh_ByName(name, $scope.action.Refresh_DropDown_callback);

        },
        Refresh_DropDown_callback: function (re) {
            $scope.$broadcast('scroll.refreshComplete');
        },
        openJob: function (row) {
            $state.go('menu.jobdetail', { No: row.JobNo });
        },
        openTrip: function (row) {
            //console.log(row);
            $state.go('menu.scheduletrip', { No: row.Id });
        },
        addNew: function () {
            $scope.new.openModal();
        },
    }

    $scope.search = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            SV_Schedule1.Search(function (re) {
                //console.log('========== in search re');
                $scope.search.data.list = re;
            });
        },
        search_changed: function () {
            if ($scope.search.data.search.length == 0) {
                $scope.search.data.list = [];
            }
        },
        openJob: function (row) {
            $state.go('menu.jobdetail', { No: row.JobNo });
        }
    }

    $scope.new = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.new.modal.hide();
        },
        openModal: function () {
            $scope.new.modal.show();
        },
        save: function () {
            SV_JobAdd.Save($scope.new.save_callback);
        },
        save_callback: function (par) {
            $scope.new.closeModal();
            //console.log(par);
            $state.go('menu.jobdetail', { No: par.context });
        }
    }
    $scope.new.data = SV_JobAdd.GetData();



    //================== modal

    $scope.$on('$destroy', function () {
        //SV_Schedule1.SetFocus('');
        $scope.search.modal.remove();
        $scope.new.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_haulier_schedule_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.new.modal = modal;
    });
})





.controller('Ctrl_ContactList', function ($scope, SV_ContactList, $state) {
    $scope.contact = SV_ContactList.GetData();
    $scope.action = {
        openDetail: function (row, index) {
            //console.log(row, index);
            $state.go('menu.contactdetail', { Index: index });
        }
    }
})

.controller('Ctrl_ContactDetail', function ($scope, $stateParams, $state, SV_ContactList, SV_User, $ionicNavBarDelegate) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    $scope.contactlist = SV_ContactList.GetData();
    $scope.contact = { UserName: SV_User.GetUserName() };
    $scope.contact.Index = parseInt($stateParams.Index, 10);
    $scope.contact.data = $scope.contactlist.list[$scope.contact.Index];
    console.log($scope.contact);
    $scope.action = {
        chat: function () {
            $state.go('menu.messagechat', { Id: $scope.contact.data.SequenceId });
        }
    }
})


.controller('Ctrl_ContactDetail1', function ($scope, $stateParams, $state, SV_ContactList, SV_User, $ionicNavBarDelegate) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    //$scope.contactlist = SV_ContactList.GetData_ByType($stateParams.Index, $stateParams.Index);
    $scope.contact = { UserName: SV_User.GetUserName() };
    //$scope.contact.Index = $stateParams.No;
    //$scope.contact.data = $scope.contactlist.list[$scope.contact.Index];
    //console.log($scope.contact);

    ionic.DomUtil.ready(function () {
        var temp_no = $stateParams.No;
        var temp_ar = temp_no.toString().split('&');
        $scope.contact.type = temp_ar[0];
        $scope.contact.no = temp_ar[1];

        SV_ContactList.GetData_ByType($scope.contact.type, $scope.contact.no, function (re) {
            $scope.contact.data = re;
        });
    });
    $scope.action = {
        chat: function () {
            $state.go('menu.messagechat', { Id: $scope.contact.data.SequenceId });
        }
    }
})



.controller('Ctrl_Message', function ($scope, SV_MessageChat, $state, SV_MessageGroup) {

    $scope.message = {};
    $scope.message.chat = SV_MessageChat.GetData();
    //$scope.message.group = SV_MessageGroup.GetData();
    //console.log($scope.message);

    $scope.action = {
        openChat: function (row) {
            $state.go('menu.messagechat', { Id: row.SequenceId });
        },
        //openGroup: function (row) {
        //    $state.go('menu.messagegroupchat', { Id: row.Id });
        //},
        //openGroupNew: function () {
        //    $state.go('menu.messagegroup_setting', { Id: '0' });
        //}
    }
})

.controller('Ctrl_MessageChat', function ($scope, $stateParams, SV_MessageChat, $ionicNavBarDelegate, SV_User, $ionicScrollDelegate, SV_Modal) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    SV_Modal.Modal_FullScreen_Image($scope);

    $scope.chat = SV_MessageChat.GetUser_ById($stateParams.Id);
    $scope.sv_mc = SV_MessageChat.GetData();

    ionic.DomUtil.ready(function () {
        //SV_MessageChat.addsubscribe_chat();
        $scope.Detail.msg.from = SV_User.GetUserName();
        if (!$scope.chat || !$scope.chat.Name || $scope.chat.Name == '') {
            $scope.GoBack();
            return;
        }
        $scope.Detail.msg.to = $scope.chat.Name;
        //console.log($scope.chat);
        SV_MessageChat.setCallback($scope.action.sendMsg_callback, $scope.action.arrivedMsg_callback, $scope.action.setNoReadCount);
        SV_MessageChat.openChat($scope.Detail.msg.from, $scope.Detail.msg.to, $scope.chat);
    });

    $scope.action = {
        arrivedMsg_callback: function (par) {
            //try {
            //$scope.Detail.newMsg = '';
            $ionicScrollDelegate.$getByHandle('handle_msg_chat_detail').resize();
            $ionicScrollDelegate.$getByHandle('handle_msg_chat_detail').scrollBottom();
            //$ionicBackdrop.retain();
            //$ionicBackdrop.release();
            //} catch (e) { }
            if (par) {
                SV_MessageChat.receiveMsg_toServer(par);
            }
        },
        sendMsg_callback: function () {
            $scope.Detail.msg.text = '';
        },
        sendMsg: function () {
            var msg = $scope.Detail.msg;
            //console.log(msg);
            if (msg.text.length == 0) {
                return;
            }
            SV_MessageChat.sendMsg(msg.from, msg.to, msg.text);
        },
        viewMore: function () {
            SV_MessageChat.toViewMore($scope.Detail.msg.from, $scope.Detail.msg.to, $scope.chat, $scope.action.viewMore_callback);
        },
        viewMore_callback: function () {
            $ionicScrollDelegate.$getByHandle('handle_msg_chat_detail').resize();
            $ionicScrollDelegate.$getByHandle('handle_msg_chat_detail').scrollTop();
        },
        setNoReadCount: function () {
            $scope.chat.noRead = 0;
        },
        isOwn: function (row) {
            return row.sender == $scope.Detail.msg.from;
        }
    }
    $scope.Detail = {
        msg: {
            text: '',
            from: '',
            to: '',
        }
    }

    $scope.$on('$destroy', function () {
        SV_MessageChat.setCallback(null, null, null);
        //console.log('============= in chat destory!');
    });
})


.controller('Ctrl_MessageGroup', function ($scope, $state, SV_MessageGroup) {

    $scope.message = {};
    //$scope.message.chat = SV_MessageChat.GetData();
    $scope.message.group = SV_MessageGroup.GetData();
    //console.log($scope.message);

    $scope.action = {
        //openChat: function (row) {
        //    $state.go('menu.messagechat', { Id: row.SequenceId });
        //},
        openGroup: function (row) {
            $state.go('menu.messagegroupchat', { Id: row.Id });
        },
        openGroupNew: function () {
            $state.go('menu.messagegroup_setting', { Id: '0' });
        },
        openGroupSearch: function () {
            $state.go('menu.messagegroup_search');
        }
    }
})

.controller('Ctrl_MessageGroupChat', function ($scope, $state, $stateParams, SV_MessageGroup, $ionicNavBarDelegate, SV_User, $ionicScrollDelegate, SV_Modal) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    SV_Modal.Modal_FullScreen_Image($scope);
    $scope.chat = SV_MessageGroup.GetGroup_ById($stateParams.Id);
    $scope.sv_mc = SV_MessageGroup.GetData();

    ionic.DomUtil.ready(function () {
        $scope.Detail.msg.from = SV_User.GetUserName();
        if (!$scope.chat) {
            $scope.GoBack();
            return;
        }
        $scope.Detail.msg.to = 'ChatGroup' + $scope.chat.Id;
        //console.log($scope.chat);
        SV_MessageGroup.setCallback($scope.action.sendMsg_callback, $scope.action.arrivedMsg_callback, $scope.action.setNoReadCount);
        SV_MessageGroup.openChat($scope.Detail.msg.from, $scope.Detail.msg.to, $scope.chat);
    });


    $scope.action = {
        arrivedMsg_callback: function (par) {
            //try {
            //$scope.Detail.newMsg = '';
            $ionicScrollDelegate.$getByHandle('handle_msg_group_detail').resize();
            $ionicScrollDelegate.$getByHandle('handle_msg_group_detail').scrollBottom();
            //$ionicBackdrop.retain();
            //$ionicBackdrop.release();
            //} catch (e) { }
            if (par) {
                SV_MessageGroup.receiveMsg_toServer(par);
            }
        },
        sendMsg_callback: function () {
            $scope.Detail.msg.text = '';
        },
        sendMsg: function () {
            var msg = $scope.Detail.msg;
            //console.log(msg);
            if (msg.text.length == 0) {
                return;
            }
            SV_MessageGroup.sendMsg(msg.from, msg.to, msg.text);
        },
        viewMore: function () {
            SV_MessageGroup.toViewMore($scope.Detail.msg.from, $scope.Detail.msg.to, $scope.chat, $scope.action.viewMore_callback);
        },
        viewMore_callback: function () {
            $ionicScrollDelegate.$getByHandle('handle_msg_group_detail').resize();
            $ionicScrollDelegate.$getByHandle('handle_msg_group_detail').scrollTop();
        },
        setNoReadCount: function () {
            $scope.chat.noRead = 0;
        },
        goSetting: function () {
            $state.go('menu.messagegroup_setting', { Id: $scope.chat.Id });
        },
        isOwn: function (row) {
            return row.sender == $scope.Detail.msg.from;
        }
    }

    $scope.Detail = {
        msg: {
            text: '',
            from: '',
            to: '',
        }
    }

    $scope.$on('$destroy', function () {
        SV_MessageGroup.setCallback(null, null, null);
    });
})

.controller('Ctrl_MessageGroupSetting', function ($scope, $stateParams, $state, SV_MessageGroupSetting, $ionicNavBarDelegate, $ionicPopup) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    $scope.vm = SV_MessageGroupSetting.GetData();
    if ($stateParams.Id == '0') {
        $scope.vm.group = {};
        $scope.vm.group.isNew = true;
    } else {
        SV_MessageGroupSetting.Refresh($stateParams.Id);
    }

    ionic.DomUtil.ready(function () {
        if (!$scope.vm.group) {
            $scope.GoBack();
            return;
        }
    });

    $scope.action = {
        AddGroup: function () {
            SV_MessageGroupSetting.AddGroup($scope.action.AddGroup_callback);
        },
        AddGroup_callback: function (re) {
            $scope.GoBack();
        },
        ExitGroup: function () {
            $ionicPopup.confirm({
                title: 'Exit Group',
                template: 'Are you sure want to exit "' + $scope.vm.group.group_name + '" group?'
            }).then(function (res) {
                if (res) {
                    SV_MessageGroupSetting.ExitGroup($scope.action.ExitGroup_callback);
                }
            });
        },
        ExitGroup_callback: function (re) {
            $scope.GoBack();
        },
        showMombers: function () {
            $state.go('menu.messagegroup_setting_show', { No: $scope.vm.group.Id });
        },
        addMombers: function () {
            $state.go('menu.messagegroup_setting_add');
        }
    }
})

.controller('Ctrl_MessageGroupSetting_show', function ($scope, $stateParams, SV_MessageGroupSetting, $ionicNavBarDelegate) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    $scope.vm = {};
    SV_MessageGroupSetting.GetMembers($stateParams.No, function (list) {
        $scope.vm.mombers = list;
    });

})

.controller('Ctrl_MessageGroupSetting_add', function ($scope, $stateParams, SV_MessageGroupSetting, $ionicNavBarDelegate, $ionicPopup) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    $scope.vm = {};
    SV_MessageGroupSetting.GetJoinUsers(function (list) {
        $scope.vm.list = list;
    });

    $scope.action = {
        join: function () {
            var list = [];
            angular.forEach($scope.vm.list, function (l) {
                if (l.selected) {
                    list.push({ name: l.Name });
                }
            })
            console.log(list);
            if (list.length > 0) {
                SV_MessageGroupSetting.JoinToMombers(list, $scope.action.join_callback);
            } else {
                $scope.GoBack();
            }
        },
        join_callback: function (re) {
            var temp = "Add False";
            if (re == "1") {
                temp = "Add Success";
            }
            $ionicPopup.alert({ title: temp });
            if (re == "1") {
                $scope.GoBack();
            }
        }
    }
})

.controller('Ctrl_MessageGroupSearch', function ($scope, $ionicNavBarDelegate, SV_MessageGroupSearch, $ionicPopup) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    $scope.vm = SV_MessageGroupSearch.GetData();
    //SV_MessageGroupSearch.Refresh();

    $scope.action = {
        search: function () {
            SV_MessageGroupSearch.Refresh();
        },
        join: function (row) {
            $ionicPopup.confirm({
                title: 'Join in',
                template: 'Are you sure want to join in "' + row.group_name + '" group?'
            }).then(function (res) {
                if (res) {
                    SV_MessageGroupSearch.Join(row, $scope.action.join_callback);
                }
            });
        },
        join_callback: function (re) {
            var temp = "Join in False";
            if (re == "1") {
                temp = "Join in Success";
            }
            $ionicPopup.alert({ title: temp });
            if (re == "1") {
                $scope.GoBack();
            }
        }
        //popover: null,
        //openPopover: function ($event) {
        //    $scope.action.popover.show($event);
        //},
        //closePopover: function () {
        //    $scope.action.popover.hide();
        //},
    }

    //================== modal,popover

    //$scope.$on('$destroy', function () {
    //    $scope.action.popover.remove();
    //});

    //$ionicPopover.fromTemplateUrl('popover_attachments.html', {
    //    scope: $scope,
    //}).then(function (popover) {
    //    $scope.action.popover = popover;
    //});
})


.controller('Ctrl_Map', function ($scope, SV_Map, SV_User) {
    $scope.vm = SV_Map.GetData();
    $scope.User = SV_User.GetData();

    ionic.DomUtil.ready(function () {
        $scope.map.refresh();
    });


    $scope.map = {
        driver: '',
        poly: null,
        map: null,
        location: {},
        markers: [],
        refresh: function () {
            if (!google) { return; }
            var mapOptions = {
                center: new google.maps.LatLng(1.35, 103.83),
                zoom: 11,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"),
                mapOptions);


            // 线条设置
            //var polyOptions = {
            //    strokeColor: '#000000',    // 颜色
            //    strokeOpacity: 1.0,    // 透明度
            //    strokeWeight: 2    // 宽度
            //}
            //var poly = new google.maps.Polyline(polyOptions);
            //poly.setMap(map);    // 装载


            //$scope.map.poly = poly;
            $scope.map.map = map;
            //SV_Map.RefreshMap($scope.map.driver, $scope.map.refresh_callback);

            SV_Map.RefreshData($scope.map.refresh_callback);
        },
        refresh_callback: function () {
            console.log('==========in refresh Map callback');
            if ($scope.map.markers) {
                $.each($scope.map.markers, function (index, d) {
                    //console.log(d);
                    d.setMap(null);
                })
            }
            $scope.map.markers.splice(0, $scope.map.markers.length);

            $scope.map.addMark();
        },
        addMark: function () {
            var loc = $scope.vm.list;
            var map = $scope.map.map;
            for (var i = 0; i < loc.length; i++) {
                //console.log(loc[i]);
                //marker = new google.maps.Marker({
                //    position: new google.maps.LatLng(loc[i].geo_latitude, loc[i].geo_longitude),
                //    map: map,
                //    title: "User:" + loc[i].user_login + "\nDate:" + loc[i].row_create_time
                //});

                var icon_ = "http://maps.gstatic.com/mapfiles/ridefinder-images/mm_20_orange.png";
                if (loc[i].IsOld == "0") {
                    //icon_ = "http://maps.gstatic.com/mapfiles/ridefinder-images/mm_20_green.png";
                    icon_ = "http://maps.gstatic.com/mapfiles/ridefinder-images/mm_20_blue.png";
                }
                var title_ = loc[i].u + " " + loc[i].date;
                var marker = new google.maps.Marker({ position: new google.maps.LatLng(loc[i].lat, loc[i].lng), map: map, icon: icon_, title: title_ });
                var content = "<div><table><tr><td><b>" + loc[i].u + "</b></td></tr><tr><td>" + loc[i].date + "</td></tr></table></div>";
                $scope.map.markers.push(marker);
                $scope.map.initInfoWindow(map, marker, content);
            }
        },
        initInfoWindow: function (map, marker, content) {
            var infowindow = new google.maps.InfoWindow({ content: content });
            infowindow.open(map, marker);
            google.maps.event.addListener(marker, 'click', function () { infowindow.open(map, marker); });
        }
    }
})

.controller('Ctrl_MyStatus', function ($scope, SV_User, SV_MyStatus, SV_MasterData, $ionicNavBarDelegate, $ionicPopover) {
    $scope.Fix_MasterData = {};

    SV_MasterData.RefreshData();
    SV_MasterData.SetFix_MasterData($scope);
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    $scope.vm = SV_MyStatus.GetData();
    $scope.action = {
        towhead_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.action.towhead_select_callback, $scope.vm.status.Towhead, 'Towhead');
        },
        towhead_select_callback: function (par) {
            $scope.vm.status.Towhead = par.Code;
            SV_MyStatus.towhead_save();
        },
        trail_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.action.trail_select_callback, $scope.vm.status.Trail, 'Trail');
        },
        trail_select_callback: function (par) {
            $scope.vm.status.Trail = par.Code;
            SV_MyStatus.trail_save();
        },
        popover: null,
        openPopover: function ($event) {
            $scope.action.popover.show($event);
        },
        closePopover: function () {
            $scope.action.popover.hide();
        },
        takeOff_All: function () {
            $scope.action.closePopover();
            $scope.vm.status.Towhead = '';
            SV_MyStatus.towhead_save();
            $scope.vm.status.Trail = '';
            SV_MyStatus.trail_save();
        },
        takeOff_Towhead: function () {
            $scope.action.closePopover();
            $scope.vm.status.Towhead = '';
            SV_MyStatus.towhead_save();
        },
        takeOff_Trail: function () {
            $scope.action.closePopover();
            $scope.vm.status.Trail = '';
            SV_MyStatus.trail_save();
        }
    }

    $scope.$on('$destroy', function () {
        SV_MasterData.OnDestory($scope);
        $scope.action.popover.remove();
    });

    $ionicPopover.fromTemplateUrl('popover_mystatus_takeoff.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.action.popover = popover;
    });
})




.controller('Ctrl_LocalJobList', function ($scope, SV_LocalJobList, SV_LocalJobAdd, $ionicModal, $state, $ionicPopup) {
    $scope.job = SV_LocalJobList.GetData();
    $scope.action = {
        search: function () {
            if ($scope.job.search.length == 0) {
                $ionicPopup.alert({ title: 'Required Search' });
                return;
            }
            SV_LocalJobList.RefreshData($scope.action.search_callback);
        },
        search_callback: function (par) {

        },
        scan: function () {
            cordova.plugins.barcodeScanner.scan(function (result) {
                if (!result.cancelled) {
                    $scope.job.search = result.text;
                    SV_LocalJobList.RefreshData($scope.action.search_callback);
                }
            }, function (e) {
                $ionicPopup.alert({
                    title: 'Scanning Error',
                    //template: e
                });
            });
        },
        addNew: function () {
            $scope.new.openModal();
        },
        openDetail: function (No) {
            $state.go('menu.local_jobdetail', { No: No });
        }
    }

    $scope.new = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.new.modal.hide();
        },
        openModal: function () {
            $scope.new.modal.show();
        },
        save: function () {
            SV_LocalJobAdd.Save($scope.new.save_callback);
        },
        save_callback: function (par) {
            $scope.new.closeModal();
            //console.log(par);
            $state.go('menu.local_jobdetail', { No: par.context });
        }
    }
    $scope.new.data = SV_LocalJobAdd.GetData();


    //================== modal

    $scope.$on('$destroy', function () {
        $scope.new.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_local_job_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.new.modal = modal;
    });
})

.controller('Ctrl_LocalJobDetail', function ($scope, $stateParams, $ionicNavBarDelegate, SV_LocalJobDetail, SV_MasterData, $ionicPopup, $ionicModal, $ionicPopover, SV_Modal, SV_User, $state) {
    SV_MasterData.RefreshData();
    SV_MasterData.SetFix($scope);
    SV_Modal.Modal_Upload($scope);
    SV_Modal.Popup_Progress($scope, 'Upload');
    SV_Modal.Modal_FullScreen_Image($scope);
    SV_Modal.Modal_Signature($scope);
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }

    ionic.DomUtil.ready(function () {
        SV_LocalJobDetail.RefreshData($scope.vm.JobNo);
        //console.log($scope.vm);
    });

    $scope.vm = {
        JobNo: $stateParams.No,
        job: SV_LocalJobDetail.GetData().job,
    }
    //========================= info
    $scope.info = {
        pol_select: function () {
            $scope.Fix_MasterData.port.openModal($scope.info.pol_select_callback, $scope.vm.job.info.Pol);
        },
        pol_select_callback: function (par) {
            $scope.vm.job.info.Pol = par;
        },
        pod_select: function () {
            $scope.Fix_MasterData.port.openModal($scope.info.pod_select_callback, $scope.vm.job.info.Pod);
        },
        pod_select_callback: function (par) {
            $scope.vm.job.info.Pod = par;
        },
        party_select: function (par) {
            switch (par) {
                case 'ClientId':
                    $scope.Fix_MasterData.party.openModal($scope.info.client_select_callback, $scope.vm.job.info.ClientId);
                    break;
                case 'CarrierId':
                    $scope.Fix_MasterData.party.openModal($scope.info.carrier_selelct_callback, $scope.vm.job.info.CarrierId);
                    break;
                case 'HaulierId':
                    $scope.Fix_MasterData.party.openModal($scope.info.haulier_select_callback, $scope.vm.job.info.HaulierId);
                    break;
                case 'Cust':
                    $scope.Fix_MasterData.party.openModal($scope.info.customer_select_callback, $scope.vm.job.info.Cust);
                    break;
            }
        },
        client_select_callback: function (par) {
            $scope.vm.job.info.ClientId = par;
        },
        carrier_selelct_callback: function (par) {
            $scope.vm.job.info.CarrierId = par;
        },
        haulier_select_callback: function (par) {
            $scope.vm.job.info.HaulierId = par;
        },
        customer_select_callback: function (par) {
            $scope.vm.job.info.Cust = par;
        },
        tripcode_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.info.tripcode_select_callback, $scope.vm.job.info.TripCode, 'Trip');
        },
        tripcode_select_callback: function (par) {
            $scope.vm.job.info.TripCode = par.Code;
        },
        driver_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.info.driver_select_callback, $scope.vm.job.info.Driver, 'Driver');
        },
        driver_select_callback: function (par) {
            //console.log(par);
            $scope.vm.job.info.Driver = par.Code;
            $scope.vm.job.info.VehicleNo = par.TowheaderCode;
        },
        towhead_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.info.towhead_select_callback, $scope.vm.job.info.VehicleNo, 'Towhead');
        },
        towhead_select_callback: function (par) {
            $scope.vm.job.info.VehicleNo = par.Code;
        },
        save: function () {
            SV_LocalJobDetail.Info_Save($scope.info.save_callback)
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par == "1") {
                title = "Save Success";
            }
            $ionicPopup.alert({ title: title });
        },
        trip_status_change: function () {
            $ionicPopup.confirm({
                title: 'Change Trip Status',
                template: 'Are you sure ' + SV_LocalJobDetail.GetNextProgress() + '?'
            }).then(function (res) {
                if (res) {
                    SV_LocalJobDetail.trip_status_save();
                }
            });
        }
    }


    //========================== attachment
    $scope.attachment = {
        popover: null,
        openPopover: function ($event) {
            $scope.attachment.popover.show($event);
        },
        closePopover: function () {
            $scope.attachment.popover.hide();
        },
        openCamera: function () {
            $scope.attachment.closePopover();

            $scope.Modal_Upload.then = $scope.attachment.getImage_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Upload.setSharpness(10);
            $scope.Modal_Upload.openCamera();
        },
        takePicture: function () {
            $scope.attachment.closePopover();
            $scope.Modal_Upload.then = $scope.attachment.getImage_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Upload.setSharpness(10);
            $scope.Modal_Upload.openAlbum();
        },
        drawing: function () {
            $scope.attachment.closePopover();
            $scope.Modal_Signature.then = $scope.attachment.getImage_callback;
            $scope.Modal_Signature.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Signature.openModal();
        },
        getImage_callback: function (par) {
            //console.log('================ in controller get image callback');
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            //temp.FilePath = 'Upload/' + $scope.vm.JobNo + '/' + temp.FileName;
            temp.FilePath = $scope.vm.JobNo + '/' + temp.FileName;
            temp.JobType = 'Tpt';
            temp.RefNo = $scope.vm.JobNo;
            temp.FileType = 'Image';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_LocalJobDetail.Attachment_Add(temp);
        },
        AddAttach: function () {
            $scope.attachment.closePopover();
            $scope.Modal_Upload.then = $scope.attachment.uploadAttach_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.JobNo + '/';
            $scope.Modal_Upload.openAttach();
        },
        uploadAttach_callback: function (par) {
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.JobNo + '/' + temp.FileName;
            temp.JobType = 'Tpt';
            temp.RefNo = $scope.vm.JobNo;
            temp.FileType = f_GetFileType(temp.FileName);
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_LocalJobDetail.Attachment_Add(temp);
        },
        Message: {
            modal: null,
            data: '',
            openModal: function () {
                $scope.attachment.closePopover();
                $scope.attachment.Message.data = '';
                $scope.attachment.Message.modal.show();
            },
            closeModal: function () {
                $scope.attachment.Message.modal.hide();
            },
            upload: function () {
                if ($scope.attachment.Message.data.length == 0) {
                    $scope.attachment.Message.closeModal();
                    return;
                }
                var temp = {};
                //temp.FileName = par.substr(par.lastIndexOf('/') + 1);
                temp.FilePath = $scope.vm.JobNo + '/' + temp.FileName;
                temp.JobType = 'Tpt';
                temp.RefNo = $scope.vm.JobNo;
                temp.FileType = '';
                temp.CreateBy = SV_User.GetUserName();
                temp.FileNote = $scope.attachment.Message.data;
                SV_LocalJobDetail.Activity_Add(temp, function (par) {
                    $scope.attachment.Message.closeModal();
                });
            }
        }
    }

    $scope.billing = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.billing.modal.hide();
        },
        openModal: function () {
            $scope.billing.modal.show();
        },
        addNew: function () {
            $scope.billing.data.MastRefNo = $scope.vm.JobNo;
            $scope.billing.data.MastType = 'TPT';
            $scope.billing.data.DocType = 'IV';
            $scope.billing.openModal();
        },
        save: function () {
            SV_LocalJobDetail.Billing_Add($scope.billing.data, $scope.billing.save_callback);
        },
        save_callback: function (par) {
            if (par.status == '0') {
                $ionicPopup.alert({ title: 'Save False' });
                return;
            }
            $scope.billing.closeModal();
            //console.log(par);
            var url = 'menu.invoice_detail';
            if ($scope.billing.data.DocType == 'PVG') {
                url = 'menu.payment_detail';
            }
            $state.go(url, { No: par.context });
        },
        openDetail: function (row) {
            //console.log(row);
            var url = 'menu.invoice_detail';
            if (row.DocType == 'PVG') {
                url = 'menu.payment_detail';
            }
            $state.go(url, { No: row.DocNo });
        }
    }

    $scope.historyLog = {
        user: SV_User.GetUserName(),
        isOwn: function (row) {
            return row.Controller == $scope.historyLog.user;
        }
    }

    $scope.stock = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.stock.modal.hide();
        },
        openModal: function () {
            $scope.stock.modal.show();
        },
        addNew: function () {
            $scope.stock.data.JobNo = $scope.vm.JobNo;
            $scope.stock.data.StockStatus = 'IN';
            $scope.stock.openModal();
        },
        save: function () {
            SV_LocalJobDetail.Stock_Add($scope.stock.data, $scope.stock.save_callback);
        },
        save_callback: function (par) {
            if (par.status == '0') {
                $ionicPopup.alert({ title: 'Save False' });
                return;
            }
            $scope.stock.closeModal();
            //console.log(par);
            $state.go('menu.stock_detail', { No: par.context });
        },
        openDetail: function (row) {
            $state.go('menu.stock_detail', { No: row.Id });
        }
    }

    //==================== watch
    $scope.watch = {
        openContact: function (row) {
            $state.go('menu.contactdetail1', { No: row.name + "&" + row.value });
        }
    }

    //=================== footer more

    $scope.footerMore = {
        popover: null,
        openPopover: function ($event) {
            $scope.footerMore.popover.show($event);
        },
        closePopover: function () {
            $scope.footerMore.popover.hide();
        },
        items: {
            modal: null,
            closeModal: function () {
                $scope.footerMore.items.modal.hide();
            },
            openModal: function () {
                $scope.footerMore.items.modal.show();
            },
            open: function () {
                $scope.footerMore.closePopover();
                $scope.footerMore.items.openModal();
            },
            addNew: function () {
                $scope.footerMore.items.closeModal();
                $scope.stock.addNew();
            },
            openDetail: function (row) {
                $scope.footerMore.items.closeModal();
                $scope.stock.openDetail(row);
            }
        },
        billing: {
            modal: null,
            closeModal: function () {
                $scope.footerMore.billing.modal.hide();
            },
            openModal: function () {
                $scope.footerMore.billing.modal.show();
            },
            open: function () {
                $scope.footerMore.closePopover();
                $scope.footerMore.billing.openModal();
            },
            addNew: function () {
                $scope.footerMore.billing.closeModal();
                $scope.billing.addNew();
            },
            openDetail: function (row) {
                $scope.footerMore.billing.closeModal();
                $scope.billing.openDetail(row);
            }
        },
        watch: {
            modal: null,
            closeModal: function () {
                $scope.footerMore.watch.modal.hide();
            },
            openModal: function () {
                $scope.footerMore.watch.modal.show();
            },
            open: function () {
                $scope.footerMore.closePopover();
                $scope.footerMore.watch.openModal();
            },
            openContact: function (row) {
                $scope.footerMore.watch.closeModal();
                $scope.watch.openContact(row);
            }
        },
        trip: {
            modal: null,
            closeModal: function () {
                $scope.footerMore.trip.modal.hide();
            },
            openModal: function () {
                $scope.footerMore.trip.modal.show();
            },
            open: function () {
                //$scope.footerMore.closePopover();
                $scope.footerMore.trip.openModal();
            },
        },
    }


    //================== modal,popover

    $scope.$on('$destroy', function () {
        SV_MasterData.OnDestory($scope);
        $scope.Modal_Signature.destory();

        $scope.attachment.popover.remove();
        $scope.footerMore.popover.remove();
        $scope.billing.modal.remove();
        $scope.stock.modal.remove();
        $scope.attachment.Message.modal.remove();
    });

    $ionicPopover.fromTemplateUrl('popover_attachments.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.attachment.popover = popover;
    });
    $ionicPopover.fromTemplateUrl('popover_local_job_more.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.footerMore.popover = popover;
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_billing_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.billing.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_stock_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.stock.modal = modal;
    });
    $ionicModal.fromTemplateUrl('modal_local_job_item_list', {
        scope: $scope
    }).then(function (modal) {
        $scope.footerMore.items.modal = modal;
    });
    $ionicModal.fromTemplateUrl('modal_local_job_billing_list', {
        scope: $scope
    }).then(function (modal) {
        $scope.footerMore.billing.modal = modal;
    });
    $ionicModal.fromTemplateUrl('modal_local_job_watch_list', {
        scope: $scope
    }).then(function (modal) {
        $scope.footerMore.watch.modal = modal;
    });
    $ionicModal.fromTemplateUrl('modal_local_job_trip_detail', {
        scope: $scope
    }).then(function (modal) {
        $scope.footerMore.trip.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_upload_text.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.attachment.Message.modal = modal;
    });
})

.controller('Ctrl_LocalSchedule', function ($scope, $state, SV_LocalSchedule) {
    $scope.vm = SV_LocalSchedule.GetData();
    $scope.calendar = {
        selectDay: $scope.vm.selectDay,
        setSelectDay: function (row) {
            //$scope.calendar.selectDay = row.day;
            $scope.vm.selectDay = row.date;
            $scope.calendar.selectDay = row.date;
            //console.log('============== set select day',$scope.calendar.selectDay);
            SV_LocalSchedule.RefeshScheduleList($scope.calendar.selectDay);
        },
        isSelectDay: function (par) {
            if (par == '' || !$scope.calendar.selectDay) { return ''; }
            var re = par.dateDiff('d', $scope.calendar.selectDay);

            //console.log($scope.calendar.selectDay, par, re);
            var result = '';
            if (re == 0) {
                result = 'td_select';
            }
            return result;
        },
        getSelectDay_yM: function () {
            var result = '';
            if ($scope.calendar.selectDay) {
                result = $scope.calendar.selectDay.getDay_e() + ', ' + $scope.calendar.selectDay.getDate() + ' ' + $scope.calendar.selectDay.getMonth_e() + ' ' + $scope.calendar.selectDay.getFullYear();//DateFormat('yyyy-MM');
            }
            return result;
        },
        weekdays_left: function () {
            var date = angular.copy($scope.vm.days[0].date);//new Date($scope.vm.days[0].date);
            date.setDate(date.getDate() - 7);
            //console.log(date, new Date($scope.vm.days[0].date));
            SV_LocalSchedule.GetWeekdays(date, null);
        },
        weekdays_right: function () {
            var date = angular.copy($scope.vm.days[0].date);//new Date($scope.vm.days[0].date);
            date.setDate(date.getDate() + 7);
            SV_LocalSchedule.GetWeekdays(date, null);
        },
        weekdays_center: function () {
            var date = new Date();
            SV_LocalSchedule.GetWeekdays(date, $scope.calendar.weekdays_changed_today);
        },
        weekdays_changed_today: function () {
            $scope.vm.selectDay = new Date();
            $scope.calendar.selectDay = new Date();
            SV_LocalSchedule.RefeshScheduleList($scope.calendar.selectDay);
        },
        onDrag_horizontal: 0,
        onDragLeft: function () {
            $scope.calendar.onDrag_horizontal++;
            //console.log('left');
        },
        onDragRight: function () {
            $scope.calendar.onDrag_horizontal--;
            //console.log('right');
        },
        onEvent: function (par) {
            if (par == 'touch') {
                $scope.calendar.onDrag_horizontal = 0;
            }
            if (par == 'release') {
                var re = $scope.calendar.onDrag_horizontal;
                $scope.calendar.onDrag_horizontal = 0;
                //console.log(re);
                if (re * re > 16) {
                    if (re > 0) {
                        $scope.calendar.weekdays_right();
                    } else {
                        $scope.calendar.weekdays_left();
                    }
                }
            }
        },
        showAll_toggle: function () {
            $scope.vm.showAll = !$scope.vm.showAll;
            SV_LocalSchedule.RefeshScheduleList($scope.calendar.selectDay);
        }
    };
    $scope.job = {
        openJob: function (row) {
            $state.go('menu.local_jobdetail', { No: row.JobNo });
        },
        openTrip: function (row) {
            //console.log(row);
            $state.go('menu.scheduletrip', { No: row.Id });
        }
    }
})

.controller('Ctrl_LocalSchedule1', function ($scope, $state, SV_LocalSchedule1, $ionicPopup, $ionicTabsDelegate, $timeout, $ionicModal, SV_LocalJobAdd) {
    $scope.vm = {};

    var tab = SV_LocalSchedule1.GetFocus();
    var tab_index = 0;
    if (tab) {
        switch (tab.name) {
            case 'Today':
                tab_index = 0;
                break;
            case 'This week':
                tab_index = 1;
                break;
            case 'Later':
                tab_index = 2;
                break;
            case 'Past':
                tab_index = 3;
                break;
        }
    }
    ionic.DomUtil.ready(function () {
        //$scope.vm.today = SV_LocalSchedule1.GetData_ByName('Today');
        //$scope.vm.thisWeek = SV_LocalSchedule1.GetData_ByName('This week');
        //$scope.vm.later = SV_LocalSchedule1.GetData_ByName('Later');
        //$scope.vm.past = SV_LocalSchedule1.GetData_ByName('Past');
        $scope.vm = SV_LocalSchedule1.GetData();
        $scope.search.data = $scope.vm.search;
        //console.log($scope.vm);
        $timeout(function () {
            $ionicTabsDelegate.$getByHandle('handle_local_schedule1').select(tab_index);
        }, 100);
    });

    $scope.action = {
        onTabSelected: function (name, index) {
            SV_LocalSchedule1.SetFocus(name);
        },
        Refresh_DropDown: function (name) {
            SV_LocalSchedule1.Refresh_ByName(name, $scope.action.Refresh_DropDown_callback);

        },
        Refresh_DropDown_callback: function (re) {
            $scope.$broadcast('scroll.refreshComplete');
        },
        openJob: function (row) {
            $state.go('menu.local_jobdetail', { No: row.JobNo });
        },
        addNew: function () {
            $scope.new.openModal();
        },
    }

    $scope.search = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            SV_LocalSchedule1.Search(function (re) {
                //console.log('========== in search re');
                $scope.search.data.list = re;
            });
        },
        search_changed: function () {
            if ($scope.search.data.search.length == 0) {
                $scope.search.data.list = [];
            }
        },
        openJob: function (row) {
            $state.go('menu.local_jobdetail', { No: row.JobNo });
        }
    }

    $scope.new = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.new.modal.hide();
        },
        openModal: function () {
            $scope.new.modal.show();
        },
        save: function () {
            SV_LocalJobAdd.Save($scope.new.save_callback);
        },
        save_callback: function (par) {
            $scope.new.closeModal();
            //console.log(par);
            $state.go('menu.local_jobdetail', { No: par.context });
        }
    }
    $scope.new.data = SV_LocalJobAdd.GetData();



    //================== modal

    $scope.$on('$destroy', function () {
        //SV_LocalSchedule1.SetFocus('');
        $scope.search.modal.remove();
        $scope.new.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_local_schedule_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_local_job_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.new.modal = modal;
    });
})




.controller('Ctrl_Dashboard', function ($scope, $state, SV_User, SV_Login, $ionicBackdrop, $timeout, SV_Firebase, SV_Hinter) {

    $scope.User = SV_User.GetData();
    $scope.action = {
        GoPage: function (par) {
            $state.go('menu.' + par);
        },
        permis: function (code, type) {
            return SV_User.Permis(code, type);
        },
        test: function () {
            //$scope.Modal_Upload.then = function () { };
            //$scope.Modal_Upload.upload.path = '/';
            //$scope.Modal_Upload.openChooser('pdf');
            console.log('==================in dashboard');
            //console.log('==================in dashboard', MR_func1());
        }
    }
    ionic.DomUtil.ready(function () {
    });
})

.controller('Ctrl_Dashboard1', function ($scope, $state, $stateParams, SV_User, SV_Login, $ionicBackdrop, $timeout, SV_Firebase, SV_Hinter) {

    $scope.User = SV_User.GetData();
    $scope.type = $stateParams.No;
    $scope.action = {
        GoPage: function (par) {
            $state.go('menu.' + par);
        },
        test: function () {
            //$scope.Modal_Upload.then = function () { };
            //$scope.Modal_Upload.upload.path = '/';
            //$scope.Modal_Upload.openChooser('pdf');
        }
    }
    ionic.DomUtil.ready(function () {

    });
})



.controller('Ctrl_InvoiceList', function ($scope, SV_InvoiceList, $state, $ionicPopup, $ionicModal) {
    $scope.vm = SV_InvoiceList.GetData();
    $scope.action = {
        search: function () {
            if ($scope.vm.search.length == 0) {
                $ionicPopup.alert({ title: 'Required Search' });
                return;
            }
            SV_InvoiceList.Refresh($scope.action.search_callback);
        },
        search_callback: function (par) {

        },
        scan: function () {
            cordova.plugins.barcodeScanner.scan(function (result) {
                if (!result.cancelled) {
                    $scope.vm.search = result.text;
                    SV_InvoiceList.Refresh($scope.action.search_callback);
                }
            }, function (e) {
                $ionicPopup.alert({
                    title: 'Scanning Error',
                    //template: e
                });
            });
        },
        openDetail: function (row) {
            $state.go('menu.invoice_detail', { No: row.DocNo });
        },
        addNew: function () {
            $scope.new.data.DocType = 'IV';
            $scope.new.openModal();
        },
    }

    $scope.new = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.new.modal.hide();
        },
        openModal: function () {
            $scope.new.modal.show();
        },
        save: function () {
            SV_InvoiceList.Save($scope.new.data, $scope.new.save_callback);
        },
        save_callback: function (par) {
            if (par.status == '0') {
                $ionicPopup.alert({ title: 'Save False' });
                return;
            }
            $scope.new.closeModal();
            //console.log(par);
            $state.go('menu.invoice_detail', { No: par.context });
        }
    }


    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            if ($scope.search.data.search.length > 0) {
                $scope.vm.search = $scope.search.data.search;
                $scope.action.search();
                $scope.search.closeModal();
            }
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.new.modal.remove();
        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_invoice_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.new.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });

})

.controller('Ctrl_InvoiceDetail', function ($scope, SV_InvoiceDetail, $stateParams, $ionicNavBarDelegate, $ionicModal, SV_MasterData, $ionicPopup, SV_Print, $ionicPopover, SV_Modal, SV_User) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    SV_MasterData.SetFix($scope);
    SV_MasterData.RefreshData_Invoice();
    $scope.vm = SV_InvoiceDetail.GetData();
    $scope.vm.No = $stateParams.No;
    SV_InvoiceDetail.Refresh($scope.vm.No);
    SV_Modal.Modal_Upload($scope);
    SV_Modal.Popup_Progress($scope, 'Upload');
    SV_Modal.Modal_FullScreen_Image($scope);

    $scope.MasterData = SV_MasterData.GetData();
    //console.log($scope.MasterData);
    $scope.mast = {
        customer_select: function () {
            $scope.Fix_MasterData.party.openModal($scope.mast.customer_select_callback, $scope.vm.job.mast.PartyTo);
        },
        customer_select_callback: function (par) {
            $scope.vm.job.mast.PartyTo = par;
        },
        save: function () {
            SV_InvoiceDetail.Mast_Save($scope.mast.save_callback);
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par == "1") {
                title = "Save Success";
            }
            $ionicPopup.alert({ title: title });
        },
        preview: function () {
            console.log('Mobile/PrintInvoice.aspx?no=' + $scope.vm.No);
            SV_Print.print('Mobile/PrintInvoice.aspx?no=' + $scope.vm.No);
        }
    }

    $scope.det = {
        modal: null,
        openModal: function () {
            $scope.det.modal.show();
        },
        closeModal: function () {
            $scope.det.modal.hide();
        },
        data: {},
        openDet: function (row) {
            $scope.det.data = angular.copy(row);;
            $scope.det.openModal();
        },
        add: function () {
            SV_InvoiceDetail.Item_GetNew(function (re) {
                $scope.det.data = re;
                //console.log($scope.det.data);
                $scope.det.openModal();
            });
        },
        Chg_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.det.Chg_select_callback, $scope.det.data.ChgCode, 'Chg');
        },
        Chg_select_callback: function (par) {
            //console.log(par);
            $scope.det.data.ChgCode = par.ChgcodeId;
            $scope.det.data.ChgDes1 = par.ChgcodeDes;
            $scope.det.data.Unit = par.ChgUnit;
            $scope.det.data.GstType = par.GstTypeId;
            $scope.det.data.Gst = par.GstP;
            $scope.det.data.AcCode = par.ArCode;
            $scope.det.Doc_changed();
        },
        Doc_changed: function () {
            var d = $scope.det.data;
            d.Gst = parseFloat(d.Gst, 10);
            d.GstAmt = (d.Price * d.Qty * d.Gst).toFixed(2);
            //d.DocAmt = d.Price * d.Qty + d.GstAmt;
            d.DocAmt = (d.Price * d.Qty + d.Price * d.Qty * d.Gst).toFixed(2);

            d.ExRate = parseFloat(d.ExRate, 10);
            //d.LocAmt = d.DocAmt * d.ExRate;
            d.LocAmt = ((d.Price * d.Qty + d.Price * d.Qty * d.Gst) * d.ExRate).toFixed(2);
            d.LineLocAmt = (parseFloat($scope.vm.job.mast.ExRate, 10) * ((d.Price * d.Qty + d.Price * d.Qty * d.Gst) * d.ExRate)).toFixed(2);
        },
        GstType_changed: function () {
            var t = null;
            for (var i = 0; i < $scope.MasterData.Invoice_GstType.list.length; i++) {
                if ($scope.MasterData.Invoice_GstType.list[i].Code == $scope.det.data.GstType) {
                    t = $scope.MasterData.Invoice_GstType.list[i];
                    break;
                }
            }
            if (t) {
                $scope.det.data.Gst = t.GstValue;
                $scope.det.Doc_changed();
            }
        },
        save: function () {
            SV_InvoiceDetail.Item_Save($scope.det.data, $scope.det.save_callback);
        },
        save_callback: function (re) {
            if (re.status == "1") {
                $scope.det.closeModal();
                return;;
            }
            $ionicPopup.alert({ title: 'Save Item Error' });
        }
    }

    $scope.attachment = {
        popover: null,
        openPopover: function ($event) {
            $scope.attachment.popover.show($event);
        },
        closePopover: function () {
            $scope.attachment.popover.hide();
        },
        GetPdf: function () {
            $scope.Modal_Upload.then = $scope.attachment.uploadPdf_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.No + '/';
            $scope.Modal_Upload.openChooser('pdf');
        },
        GetExcel: function () {
            $scope.Modal_Upload.then = $scope.attachment.uploadExcel_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.No + '/';
            $scope.Modal_Upload.openChooser('excel');
        },
        uploadPdf_callback: function (par) {
            $scope.attachment.closePopover();
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.No + '/' + temp.FileName;
            temp.JobType = $scope.vm.job.mast.DocType;
            temp.RefNo = $scope.vm.No;
            temp.FileType = 'pdf';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_InvoiceDetail.Attachment_Add(temp);
        },
        uploadExcel_callback: function (par) {
            $scope.attachment.closePopover();
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.No + '/' + temp.FileName;
            temp.JobType = $scope.vm.job.mast.DocType;
            temp.RefNo = $scope.vm.No;
            temp.FileType = 'excel';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_InvoiceDetail.Attachment_Add(temp);
        }
    }

    $scope.historyLog = {
        user: SV_User.GetUserName(),
        isOwn: function (row) {
            return row.Controller == $scope.historyLog.user;
        }
    }

    //================== modal,popover

    $scope.$on('$destroy', function () {
        SV_MasterData.OnDestory($scope);
        $scope.det.modal.remove();
        $scope.attachment.popover.remove();
    });

    $ionicModal.fromTemplateUrl('templates/modal_invoice_det.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.det.modal = modal;
    });
    $ionicPopover.fromTemplateUrl('popover_attachments.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.attachment.popover = popover;
    });
})


.controller('Ctrl_QuotationList', function ($scope, SV_QuotationList, $state, $ionicPopup, $ionicModal) {
    $scope.vm = SV_QuotationList.GetData();
    $scope.action = {
        search: function () {
            if ($scope.vm.search.length == 0) {
                $ionicPopup.alert({ title: 'Required Search' });
                return;
            }
            SV_QuotationList.Refresh($scope.action.search_callback);
        },
        search_callback: function (par) {

        },
        scan: function () {
            cordova.plugins.barcodeScanner.scan(function (result) {
                if (!result.cancelled) {
                    $scope.vm.search = result.text;
                    SV_QuotationList.Refresh($scope.action.search_callback);
                }
            }, function (e) {
                $ionicPopup.alert({
                    title: 'Scanning Error',
                    //template: e
                });
            });
        },
        openDetail: function (row) {
            $state.go('menu.quotation_detail', { No: row.DocNo });
        },
        addNew: function () {
            $scope.new.data.DocType = 'QT';
            $scope.new.openModal();
        },
    }

    $scope.new = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.new.modal.hide();
        },
        openModal: function () {
            $scope.new.modal.show();
        },
        save: function () {
            SV_QuotationList.Save($scope.new.data, $scope.new.save_callback);
        },
        save_callback: function (par) {
            if (par.status == '0') {
                $ionicPopup.alert({ title: 'Save False' });
                return;
            }
            $scope.new.closeModal();
            //console.log(par);
            $state.go('menu.quotation_detail', { No: par.context });
        }
    }

    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            if ($scope.search.data.search.length > 0) {
                $scope.vm.search = $scope.search.data.search;
                $scope.action.search();
                $scope.search.closeModal();
            }
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.new.modal.remove();
        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_quotation_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.new.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });

})

.controller('Ctrl_QuotationDetail', function ($scope, SV_QuotationDetail, $stateParams, $ionicNavBarDelegate, $ionicModal, SV_MasterData, $ionicPopup, SV_Print, $ionicPopover, SV_Modal, SV_User) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    SV_MasterData.SetFix($scope);
    SV_MasterData.RefreshData_Invoice();
    $scope.vm = SV_QuotationDetail.GetData();
    $scope.vm.No = $stateParams.No;
    SV_QuotationDetail.Refresh($scope.vm.No);
    SV_Modal.Modal_Upload($scope);
    SV_Modal.Popup_Progress($scope, 'Upload');
    SV_Modal.Modal_FullScreen_Image($scope);

    $scope.MasterData = SV_MasterData.GetData();
    //console.log($scope.MasterData);
    $scope.mast = {
        customer_select: function () {
            $scope.Fix_MasterData.party.openModal($scope.mast.customer_select_callback, $scope.vm.job.mast.PartyTo);
        },
        customer_select_callback: function (par) {
            $scope.vm.job.mast.PartyTo = par;
        },
        save: function () {
            SV_QuotationDetail.Mast_Save($scope.mast.save_callback);
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par == "1") {
                title = "Save Success";
            }
            $ionicPopup.alert({ title: title });
        },
        preview: function () {
            console.log('Mobile/PrintInvoice.aspx?no=' + $scope.vm.No);
            SV_Print.print('Mobile/PrintInvoice.aspx?no=' + $scope.vm.No);
        }
    }

    $scope.det = {
        modal: null,
        openModal: function () {
            $scope.det.modal.show();
        },
        closeModal: function () {
            $scope.det.modal.hide();
        },
        data: {},
        openDet: function (row) {
            $scope.det.data = angular.copy(row);;
            $scope.det.openModal();
        },
        add: function () {
            SV_QuotationDetail.Item_GetNew(function (re) {
                $scope.det.data = re;
                //console.log($scope.det.data);
                $scope.det.openModal();
            });
        },
        Chg_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.det.Chg_select_callback, $scope.det.data.ChgCode, 'Chg');
        },
        Chg_select_callback: function (par) {
            //console.log(par);
            $scope.det.data.ChgCode = par.ChgcodeId;
            $scope.det.data.ChgDes1 = par.ChgcodeDes;
            $scope.det.data.Unit = par.ChgUnit;
            $scope.det.data.GstType = par.GstTypeId;
            $scope.det.data.Gst = par.GstP;
            $scope.det.data.AcCode = par.ArCode;
            $scope.det.Doc_changed();
        },
        Doc_changed: function () {
            var d = $scope.det.data;
            d.Gst = parseFloat(d.Gst, 10);
            d.GstAmt = (d.Price * d.Qty * d.Gst).toFixed(2);
            //d.DocAmt = d.Price * d.Qty + d.GstAmt;
            d.DocAmt = (d.Price * d.Qty + d.Price * d.Qty * d.Gst).toFixed(2);

            d.ExRate = parseFloat(d.ExRate, 10);
            //d.LocAmt = d.DocAmt * d.ExRate;
            d.LocAmt = ((d.Price * d.Qty + d.Price * d.Qty * d.Gst) * d.ExRate).toFixed(2);
            d.LineLocAmt = (parseFloat($scope.vm.job.mast.ExRate, 10) * ((d.Price * d.Qty + d.Price * d.Qty * d.Gst) * d.ExRate)).toFixed(2);
        },
        GstType_changed: function () {
            var t = null;
            for (var i = 0; i < $scope.MasterData.Invoice_GstType.list.length; i++) {
                if ($scope.MasterData.Invoice_GstType.list[i].Code == $scope.det.data.GstType) {
                    t = $scope.MasterData.Invoice_GstType.list[i];
                    break;
                }
            }
            if (t) {
                $scope.det.data.Gst = t.GstValue;
                $scope.det.Doc_changed();
            }
        },
        save: function () {
            SV_QuotationDetail.Item_Save($scope.det.data, $scope.det.save_callback);
        },
        save_callback: function (re) {
            if (re.status == "1") {
                $scope.det.closeModal();
                return;;
            }
            $ionicPopup.alert({ title: 'Save Item Error' });
        }
    }

    $scope.attachment = {
        popover: null,
        openPopover: function ($event) {
            $scope.attachment.popover.show($event);
        },
        closePopover: function () {
            $scope.attachment.popover.hide();
        },
        GetPdf: function () {
            $scope.Modal_Upload.then = $scope.attachment.uploadPdf_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.No + '/';
            $scope.Modal_Upload.openChooser('pdf');
        },
        GetExcel: function () {
            $scope.Modal_Upload.then = $scope.attachment.uploadExcel_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.No + '/';
            $scope.Modal_Upload.openChooser('excel');
        },
        uploadPdf_callback: function (par) {
            $scope.attachment.closePopover();
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.No + '/' + temp.FileName;
            temp.JobType = $scope.vm.job.mast.DocType;
            temp.RefNo = $scope.vm.No;
            temp.FileType = 'pdf';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_QuotationDetail.Attachment_Add(temp);
        },
        uploadExcel_callback: function (par) {
            $scope.attachment.closePopover();
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.No + '/' + temp.FileName;
            temp.JobType = $scope.vm.job.mast.DocType;
            temp.RefNo = $scope.vm.No;
            temp.FileType = 'excel';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_QuotationDetail.Attachment_Add(temp);
        }
    }


    $scope.historyLog = {
        user: SV_User.GetUserName(),
        isOwn: function (row) {
            return row.Controller == $scope.historyLog.user;
        }
    }

    //================== modal,popover

    $scope.$on('$destroy', function () {
        SV_MasterData.OnDestory($scope);
        $scope.det.modal.remove();
        $scope.attachment.popover.remove();
    });

    $ionicModal.fromTemplateUrl('templates/modal_quotation_item.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.det.modal = modal;
    });
    $ionicPopover.fromTemplateUrl('popover_attachments.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.attachment.popover = popover;
    });
})


.controller('Ctrl_PaymentList', function ($scope, SV_PaymentList, $state, $ionicPopup, $ionicModal) {
    $scope.vm = SV_PaymentList.GetData();
    $scope.action = {
        search: function () {
            if ($scope.vm.search.length == 0) {
                $ionicPopup.alert({ title: 'Required Search' });
                return;
            }
            SV_PaymentList.Refresh($scope.action.search_callback);
        },
        search_callback: function (par) {

        },
        scan: function () {
            cordova.plugins.barcodeScanner.scan(function (result) {
                if (!result.cancelled) {
                    $scope.vm.search = result.text;
                    SV_PaymentList.Refresh($scope.action.search_callback);
                }
            }, function (e) {
                $ionicPopup.alert({
                    title: 'Scanning Error',
                    //template: e
                });
            });
        },
        openDetail: function (row) {
            $state.go('menu.payment_detail', { No: row.DocNo });
        },
        addNew: function () {
            $scope.new.data.DocType = 'PVG';
            $scope.new.openModal();
        },
    }

    $scope.new = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.new.modal.hide();
        },
        openModal: function () {
            $scope.new.modal.show();
        },
        save: function () {
            SV_PaymentList.Save($scope.new.data, $scope.new.save_callback);
        },
        save_callback: function (par) {
            if (par.status == '0') {
                $ionicPopup.alert({ title: 'Save False' });
                return;
            }
            $scope.new.closeModal();
            //console.log(par);
            $state.go('menu.payment_detail', { No: par.context });
        }
    }

    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            if ($scope.search.data.search.length > 0) {
                $scope.vm.search = $scope.search.data.search;
                $scope.action.search();
                $scope.search.closeModal();
            }
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.new.modal.remove();
        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_payment_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.new.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });

})

.controller('Ctrl_PaymentDetail', function ($scope, SV_PaymentDetail, $stateParams, $ionicNavBarDelegate, $ionicModal, SV_MasterData, $ionicPopup, SV_Print, $ionicPopover, SV_Modal, SV_User) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    SV_MasterData.SetFix($scope);
    SV_MasterData.RefreshData_Invoice();
    $scope.vm = SV_PaymentDetail.GetData();
    $scope.vm.No = $stateParams.No;
    SV_PaymentDetail.Refresh($scope.vm.No);
    SV_Modal.Modal_Upload($scope);
    SV_Modal.Popup_Progress($scope, 'Upload');
    SV_Modal.Modal_FullScreen_Image($scope);

    $scope.MasterData = SV_MasterData.GetData();
    //console.log($scope.MasterData);
    $scope.mast = {
        customer_select: function () {
            $scope.Fix_MasterData.party.openModal($scope.mast.customer_select_callback, $scope.vm.job.mast.PartyTo);
        },
        customer_select_callback: function (par) {
            $scope.vm.job.mast.PartyTo = par;
        },
        save: function () {
            SV_PaymentDetail.Mast_Save($scope.mast.save_callback);
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par == "1") {
                title = "Save Success";
            }
            $ionicPopup.alert({ title: title });
        },
        preview: function () {
            console.log('Mobile/PrintInvoice.aspx?no=' + $scope.vm.No);
            SV_Print.print('Mobile/PrintInvoice.aspx?no=' + $scope.vm.No);
        }
    }

    $scope.det = {
        modal: null,
        openModal: function () {
            $scope.det.modal.show();
        },
        closeModal: function () {
            $scope.det.modal.hide();
        },
        data: {},
        openDet: function (row) {
            $scope.det.data = angular.copy(row);;
            $scope.det.openModal();
        },
        add: function () {
            SV_PaymentDetail.Item_GetNew(function (re) {
                $scope.det.data = re;
                //console.log($scope.det.data);
                $scope.det.openModal();
            });
        },
        Chg_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.det.Chg_select_callback, $scope.det.data.ChgCode, 'Chg');
        },
        Chg_select_callback: function (par) {
            //console.log(par);
            $scope.det.data.ChgCode = par.ChgcodeId;
            $scope.det.data.ChgDes1 = par.ChgcodeDes;
            $scope.det.data.Unit = par.ChgUnit;
            $scope.det.data.GstType = par.GstTypeId;
            $scope.det.data.Gst = par.GstP;
            $scope.det.data.AcCode = par.ArCode;
            $scope.det.Doc_changed();
        },
        Doc_changed: function () {
            var d = $scope.det.data;
            d.Gst = parseFloat(d.Gst, 10);
            d.GstAmt = (d.Price * d.Qty * d.Gst).toFixed(2);
            //d.DocAmt = d.Price * d.Qty + d.GstAmt;
            d.DocAmt = (d.Price * d.Qty + d.Price * d.Qty * d.Gst).toFixed(2);

            d.ExRate = parseFloat(d.ExRate, 10);
            //d.LocAmt = d.DocAmt * d.ExRate;
            d.LocAmt = ((d.Price * d.Qty + d.Price * d.Qty * d.Gst) * d.ExRate).toFixed(2);
            d.LineLocAmt = (parseFloat($scope.vm.job.mast.ExRate, 10) * ((d.Price * d.Qty + d.Price * d.Qty * d.Gst) * d.ExRate)).toFixed(2);
        },
        GstType_changed: function () {
            var t = null;
            for (var i = 0; i < $scope.MasterData.Invoice_GstType.list.length; i++) {
                if ($scope.MasterData.Invoice_GstType.list[i].Code == $scope.det.data.GstType) {
                    t = $scope.MasterData.Invoice_GstType.list[i];
                    break;
                }
            }
            if (t) {
                $scope.det.data.Gst = t.GstValue;
                $scope.det.Doc_changed();
            }
        },
        save: function () {
            SV_PaymentDetail.Item_Save($scope.det.data, $scope.det.save_callback);
        },
        save_callback: function (re) {
            if (re.status == "1") {
                $scope.det.closeModal();
                return;;
            }
            $ionicPopup.alert({ title: 'Save Item Error' });
        }
    }

    $scope.attachment = {
        popover: null,
        openPopover: function ($event) {
            $scope.attachment.popover.show($event);
        },
        closePopover: function () {
            $scope.attachment.popover.hide();
        },
        GetPdf: function () {
            $scope.Modal_Upload.then = $scope.attachment.uploadPdf_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.No + '/';
            $scope.Modal_Upload.openChooser('pdf');
        },
        GetExcel: function () {
            $scope.Modal_Upload.then = $scope.attachment.uploadExcel_callback;
            $scope.Modal_Upload.upload.path = $scope.vm.No + '/';
            $scope.Modal_Upload.openChooser('excel');
        },
        uploadPdf_callback: function (par) {
            $scope.attachment.closePopover();
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.No + '/' + temp.FileName;
            temp.JobType = $scope.vm.job.mast.DocType;
            temp.RefNo = $scope.vm.No;
            temp.FileType = 'pdf';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_PaymentDetail.Attachment_Add(temp);
        },
        uploadExcel_callback: function (par) {
            $scope.attachment.closePopover();
            var temp = {};
            temp.FileName = par.substr(par.lastIndexOf('/') + 1);
            temp.FilePath = $scope.vm.No + '/' + temp.FileName;
            temp.JobType = $scope.vm.job.mast.DocType;
            temp.RefNo = $scope.vm.No;
            temp.FileType = 'excel';
            temp.CreateBy = SV_User.GetUserName();
            temp.FileNote = '';
            SV_PaymentDetail.Attachment_Add(temp);
        }
    }


    $scope.historyLog = {
        user: SV_User.GetUserName(),
        isOwn: function (row) {
            return row.Controller == $scope.historyLog.user;
        }
    }

    //================== modal,popover

    $scope.$on('$destroy', function () {
        SV_MasterData.OnDestory($scope);
        $scope.det.modal.remove();
        $scope.attachment.popover.remove();
    });

    $ionicModal.fromTemplateUrl('templates/modal_payment_item.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.det.modal = modal;
    });
    $ionicPopover.fromTemplateUrl('popover_attachments.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.attachment.popover = popover;
    });
})


.controller('Ctrl_DriverList', function ($scope, $state, $ionicModal, SV_DriverList, $ionicPopup) {
    SV_DriverList.Refresh();
    $scope.vm = SV_DriverList.GetData();

    $scope.action = {
        openDetail: function (row) {
            $state.go('menu.driver_detail', { No: row.Id })
        },
        addNew: function () {
            $state.go('menu.driver_detail', { No: '0' })
        }
    }
    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            $scope.search.closeModal();
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
})

.controller('Ctrl_DriverDetail', function ($scope, $ionicNavBarDelegate, SV_DriverList, $stateParams, $ionicPopup, SV_MasterData) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    //SV_MasterData.RefreshData();
    SV_MasterData.SetFix($scope);
    $scope.vm = SV_DriverList.GetData();
    ionic.DomUtil.ready(function () {
        if ($stateParams.No && $stateParams.No.length > 0) {
            SV_DriverList.RefreshDetail($stateParams.No);
        } else {
            $ionicPopup.alert({ title: 'Data Error' });
            $scope.GoBack();
        }
    });

    $scope.action = {
        towhead_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.action.towhead_select_callback, $scope.vm.detail.TowheaderCode, 'Towhead');
        },
        towhead_select_callback: function (par) {
            $scope.vm.detail.TowheaderCode = par.Code;
        },
        save: function () {
            if ($scope.vm.detail.Code.length == 0) {
                $ionicPopup.alert({ title: 'Request Code' });
                return;
            }
            SV_DriverList.Save($scope.action.save_callback);
        },
        save_callback: function (par) {
            var title = 'Save Error';
            if (par == "1") {
                title = 'Save Success';
            }
            $ionicPopup.alert({ title: title });
            if (par == "1") {
                $scope.GoBack();
            }
        }
    }
})


.controller('Ctrl_VehicleList', function ($scope, $state, $ionicModal, SV_VehicleList, $ionicPopup) {
    SV_VehicleList.Refresh();
    $scope.vm = SV_VehicleList.GetData();

    $scope.action = {
        openDetail: function (row) {
            $state.go('menu.vehicle_detail', { No: row.Id })
        },
        addNew: function () {
            $state.go('menu.vehicle_detail', { No: '0' })
        }
    }
    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            $scope.search.closeModal();
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
})

.controller('Ctrl_VehicleDetail', function ($scope, $ionicNavBarDelegate, SV_VehicleList, $stateParams, $ionicPopup, SV_MasterData) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }

    SV_MasterData.SetFix($scope);
    $scope.vm = SV_VehicleList.GetData();
    ionic.DomUtil.ready(function () {
        if ($stateParams.No && $stateParams.No.length > 0) {
            SV_VehicleList.RefreshDetail($stateParams.No);
        } else {
            $ionicPopup.alert({ title: 'Data Error' });
            $scope.GoBack();
        }
    });

    $scope.action = {
        towhead_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.action.towhead_select_callback, $scope.vm.detail.TowheaderCode, 'Towhead');
        },
        towhead_select_callback: function (par) {
            $scope.vm.detail.TowheaderCode = par.Code;
        },
        save: function () {
            if ($scope.vm.detail.Code.length == 0) {
                $ionicPopup.alert({ title: 'Request Code' });
                return;
            }
            SV_VehicleList.Save($scope.action.save_callback);
        },
        save_callback: function (par) {
            var title = 'Save Error';
            if (par == "1") {
                title = 'Save Success';
            }
            $ionicPopup.alert({ title: title });
            if (par == "1") {
                $scope.GoBack();
            }
        }
    }
})


.controller('Ctrl_ContainerSchedule', function ($scope, $state, SV_ContainerSchedule, $ionicPopup, $ionicTabsDelegate, $timeout) {
    $scope.vm = {};

    var tab = SV_ContainerSchedule.GetFocus();
    var tab_index = 0;
    if (tab) {
        switch (tab.name) {
            case 'Today':
                tab_index = 0;
                break;
            case 'This week':
                tab_index = 1;
                break;
            case 'Later':
                tab_index = 2;
                break;
            case 'Past':
                tab_index = 3;
                break;
        }
    }
    ionic.DomUtil.ready(function () {
        $scope.vm = SV_ContainerSchedule.GetData();
        //console.log($scope.vm);
        $timeout(function () {
            $ionicTabsDelegate.$getByHandle('handle_container_schedule1').select(tab_index);
        }, 100);
    });

    $scope.action = {
        onTabSelected: function (name, index) {
            SV_ContainerSchedule.SetFocus(name);
        },
        Refresh_DropDown: function (name) {
            SV_ContainerSchedule.Refresh_ByName(name, $scope.action.Refresh_DropDown_callback);

        },
        Refresh_DropDown_callback: function (re) {
            $scope.$broadcast('scroll.refreshComplete');
        },
        openJob: function (row) {
            $state.go('menu.jobdetail', { No: row.JobNo });
        },
    }
})


.controller('Ctrl_StockDetail', function ($scope, $ionicNavBarDelegate, $stateParams, SV_StockDetail, $ionicPopup) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    $scope.vm = SV_StockDetail.GetData();
    ionic.DomUtil.ready(function () {
        if ($stateParams.No && $stateParams.No.length > 0) {
            SV_StockDetail.Refresh($stateParams.No, function () {
                if (!$scope.vm.info.Id) {
                    $ionicPopup.alert({ title: 'Data Error' });
                    $scope.GoBack();
                }
                console.log($scope.vm);
            });
        } else {
            $ionicPopup.alert({ title: 'Data Error' });
            $scope.GoBack();
        }
    });
    $scope.info = {
        save: function () {
            SV_StockDetail.Save($scope.info.save_callback);
        },
        save_callback: function (par) {
            var title = "Save Error";
            if (par == "1") {
                title = "Save Success";
            }
            $ionicPopup.alert({ title: title });
        }
    }
})

.controller('Ctrl_StockBalanceList', function ($scope, $state, $ionicModal, SV_StockBalanceList, $ionicPopup) {

    $scope.vm = SV_StockBalanceList.GetData();

    $scope.action = {
        openDetail: function (row) {
            $state.go('menu.stock_detail', { No: row.Id })
        },
        search: function () {
            SV_StockBalanceList.Refresh();
        }
    }
    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            if ($scope.search.data.search.length > 0) {
                $scope.vm.search = $scope.search.data.search;
                $scope.action.search();
            }
            $scope.search.closeModal();
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
})

.controller('Ctrl_StockMovementList', function ($scope, $state, $ionicModal, SV_StockMovementList, $ionicPopup) {

    $scope.vm = SV_StockMovementList.GetData();

    $scope.action = {
        openDetail: function (row) {
            $state.go('menu.stock_detail', { No: row.Id })
        },
        search: function () {
            SV_StockMovementList.Refresh();
        }
    }
    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            if ($scope.search.data.search.length > 0) {
                $scope.vm.search = $scope.search.data.search;
                $scope.action.search();
            }
            $scope.search.closeModal();
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
})



.controller('Ctrl_CustomerList', function ($scope, $state, $ionicModal, SV_CustomerList, $ionicPopup) {
    SV_CustomerList.Refresh();
    $scope.vm = SV_CustomerList.GetData();

    $scope.action = {
        openDetail: function (row) {
            $state.go('menu.customer_detail', { No: row.SequenceId })
        },
        addNew: function () {
            $state.go('menu.customer_detail', { No: '0' })
        }
    }
    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            $scope.search.closeModal();
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
})

.controller('Ctrl_CustomerDetail', function ($scope, $ionicNavBarDelegate, SV_CustomerList, $stateParams, $ionicPopup, SV_MasterData) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }
    //SV_MasterData.RefreshData();
    SV_MasterData.SetFix($scope);
    $scope.vm = SV_CustomerList.GetData();
    ionic.DomUtil.ready(function () {
        if ($stateParams.No && $stateParams.No.length > 0) {
            SV_CustomerList.RefreshDetail($stateParams.No);
        } else {
            $ionicPopup.alert({ title: 'Data Error' });
            $scope.GoBack();
        }
    });

    $scope.action = {
        towhead_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.action.towhead_select_callback, $scope.vm.detail.TowheaderCode, 'Towhead');
        },
        towhead_select_callback: function (par) {
            $scope.vm.detail.TowheaderCode = par.Code;
        },
        save: function () {
            if ($scope.vm.detail.Code.length == 0) {
                $ionicPopup.alert({ title: 'Request Code' });
                return;
            }
            SV_CustomerList.Save($scope.action.save_callback);
        },
        save_callback: function (par) {
            var title = 'Save Error';
            if (par == "1") {
                title = 'Save Success';
            }
            $ionicPopup.alert({ title: title });
            if (par == "1") {
                $scope.GoBack();
            }
        }
    }
})


.controller('Ctrl_MapDriver', function ($scope, SV_MapDriver, SV_User, $ionicPopover) {

    $scope.vm = SV_MapDriver.GetData();
    $scope.User = SV_User.GetData();


    ionic.DomUtil.ready(function () {
        SV_MapDriver.SetCallback($scope.map.refresh_callback);
        $scope.map.refresh();
        console.log($scope.vm);
    });


    $scope.$on('$destroy', function () {
        SV_MapDriver.SetCallback(null);
        $scope.driver.popover.remove();
    });

    $scope.driver = {
        popover: null,
        openPopover: function ($event) {
            $scope.driver.popover.show($event);
        },
        closePopover: function () {
            $scope.driver.popover.hide();
        },
        SetCurrentDriver: function (row) {
            $scope.driver.closePopover();
            $scope.vm.driver.current = row.Code;
            $scope.map.refresh();
        }
    }

    $scope.map = {
        driver: '',
        poly: null,
        map: null,
        location: {},
        markers: [],
        refresh: function () {
            if (!google) { return; }
            var mapOptions = {
                center: new google.maps.LatLng(1.35, 103.83),
                zoom: 11,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"),
                mapOptions);


            // 线条设置
            //var polyOptions = {
            //    strokeColor: '#000000',    // 颜色
            //    strokeOpacity: 1.0,    // 透明度
            //    strokeWeight: 2    // 宽度
            //}
            //var poly = new google.maps.Polyline(polyOptions);
            //poly.setMap(map);    // 装载


            //$scope.map.poly = poly;
            $scope.map.map = map;
            //SV_Map.RefreshMap($scope.map.driver, $scope.map.refresh_callback);

            SV_MapDriver.RefreshData($scope.map.refresh_callback);
        },
        refresh_callback: function () {
            console.log('==========in refresh Map callback');
            if ($scope.map.markers) {
                $.each($scope.map.markers, function (index, d) {
                    //console.log(d);
                    d.setMap(null);
                })
            }
            $scope.map.markers.splice(0, $scope.map.markers.length);

            $scope.map.addMark();
        },
        addMark: function () {
            var loc = $scope.vm.list;
            var map = $scope.map.map;
            for (var i = 0; i < loc.length; i++) {
                //console.log(loc[i]);
                //marker = new google.maps.Marker({
                //    position: new google.maps.LatLng(loc[i].geo_latitude, loc[i].geo_longitude),
                //    map: map,
                //    title: "User:" + loc[i].user_login + "\nDate:" + loc[i].row_create_time
                //});

                var icon_ = "http://maps.gstatic.com/mapfiles/ridefinder-images/mm_20_orange.png";
                if (loc[i].IsOld == "0") {
                    //icon_ = "http://maps.gstatic.com/mapfiles/ridefinder-images/mm_20_green.png";
                    icon_ = "http://maps.gstatic.com/mapfiles/ridefinder-images/mm_20_blue.png";
                }
                var title_ = loc[i].u + " " + loc[i].date;
                var marker = new google.maps.Marker({ position: new google.maps.LatLng(loc[i].lat, loc[i].lng), map: map, icon: icon_, title: title_ });
                var content = "<div><table><tr><td><b>" + loc[i].u + "</b></td></tr><tr><td>" + loc[i].date + "</td></tr></table></div>";
                $scope.map.markers.push(marker);
                $scope.map.initInfoWindow(map, marker, content);
            }
        },
        initInfoWindow: function (map, marker, content) {
            var infowindow = new google.maps.InfoWindow({ content: content });
            infowindow.open(map, marker);
            google.maps.event.addListener(marker, 'click', function () { infowindow.open(map, marker); });
        }
    }

    $ionicPopover.fromTemplateUrl('popover_map_driver_driverlist.html', {
        scope: $scope,
    }).then(function (popover) {
        $scope.driver.popover = popover;
    });
})




.controller('Ctrl_TrailerList', function ($scope, $state, $ionicModal, SV_TrackerList, $ionicPopup) {
    SV_TrackerList.Refresh();
    $scope.vm = SV_TrackerList.GetData();

    $scope.action = {
        openDetail: function (row) {
            $state.go('menu.trailer_detail', { No: row.Id });
        },
        addNew: function () {
            $state.go('menu.trailer_detail', { No: '0' });
        }
    }
    $scope.search = {
        data: {
            search: '',
            list: []
        },
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            $scope.search.closeModal();
        }
    }
    //================== modal

    $scope.$on('$destroy', function () {

        $scope.search.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
})

.controller('Ctrl_TrailerDetail', function ($scope, $ionicNavBarDelegate, SV_TrackerList, $stateParams, $ionicPopup, SV_MasterData) {
    $scope.GoBack = function () {
        $ionicNavBarDelegate.back();
    }

    SV_MasterData.SetFix($scope);
    $scope.vm = SV_TrackerList.GetData();
    ionic.DomUtil.ready(function () {
        if ($stateParams.No && $stateParams.No.length > 0) {
            SV_TrackerList.RefreshDetail($stateParams.No);
        } else {
            $ionicPopup.alert({ title: 'Data Error' });
            $scope.GoBack();
        }
    });

    $scope.action = {
        towhead_select: function () {
            $scope.Fix_MasterData.MasterData.openModal($scope.action.towhead_select_callback, $scope.vm.detail.TowheaderCode, 'Towhead');
        },
        towhead_select_callback: function (par) {
            $scope.vm.detail.TowheaderCode = par.Code;
        },
        save: function () {
            if ($scope.vm.detail.Code.length == 0) {
                $ionicPopup.alert({ title: 'Request Code' });
                return;
            }
            SV_TrackerList.Save($scope.action.save_callback);
        },
        save_callback: function (par) {
            var title = 'Save Error';
            if (par == "1") {
                title = 'Save Success';
            }
            $ionicPopup.alert({ title: title });
            if (par == "1") {
                $scope.GoBack();
            }
        }
    }
})





.controller('Ctrl_DailyJobFCL', function ($scope, $state, SV_DailyJobFCL, $ionicPopup, $ionicTabsDelegate, $timeout, $ionicModal, SV_JobAdd) {
    $scope.vm = {};

    var tab = SV_DailyJobFCL.GetFocus();
    var tab_index = 0;
    if (tab) {
        switch (tab.name) {
            case 'Undelivered':
                tab_index = 0;
                break;
            case 'Completed':
                tab_index = 1;
                break;
            case 'Failed':
                tab_index = 2;
                break;
        }
    }
    ionic.DomUtil.ready(function () {
        $scope.vm = SV_DailyJobFCL.GetData();
        $scope.search.data = $scope.vm.search;
        //console.log($scope.vm);
        $timeout(function () {
            $ionicTabsDelegate.$getByHandle('handle_local_schedule1').select(tab_index);
        }, 100);
    });

    $scope.action = {
        onTabSelected: function (name, index) {
            SV_DailyJobFCL.SetFocus(name);
        },
        Refresh_DropDown: function (name) {
            SV_DailyJobFCL.Refresh_ByName(name, $scope.action.Refresh_DropDown_callback);

        },
        Refresh_DropDown_callback: function (re) {
            $scope.$broadcast('scroll.refreshComplete');
        },
        openJob: function (row) {
            $state.go('menu.jobdetail', { No: row.JobNo });
        },
        openTrip: function (row) {
            //console.log(row);
            $state.go('menu.scheduletrip', { No: row.Id });
        },
        addNew: function () {
            $scope.new.openModal();
        },
    }

    $scope.search = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.search.modal.hide();
        },
        openModal: function () {
            $scope.search.modal.show();
        },
        search: function () {
            //if ($scope.search.data.search.length > 0) {
            SV_DailyJobFCL.Search(function (re) {
                //console.log('========== in search re');
                $scope.search.data.list = re;
            });
            //}
        },
        search_changed: function () {
            if ($scope.search.data.search.length == 0) {
                $scope.search.data.list = [];
            }
        },
        openJob: function (row) {
            $state.go('menu.jobdetail', { No: row.JobNo });
        }
    }

    $scope.new = {
        data: {},
        modal: null,
        closeModal: function () {
            $scope.new.modal.hide();
        },
        openModal: function () {
            $scope.new.modal.show();
        },
        save: function () {
            SV_JobAdd.Save($scope.new.save_callback);
        },
        save_callback: function (par) {
            $scope.new.closeModal();
            //console.log(par);
            $state.go('menu.jobdetail', { No: par.context });
        }
    }
    $scope.new.data = SV_JobAdd.GetData();



    //================== modal

    $scope.$on('$destroy', function () {
        //SV_DailyJobFCL.SetFocus('');
        $scope.search.modal.remove();
        $scope.new.modal.remove();
    });
    $ionicModal.fromTemplateUrl('templates/modal_haulier_schedule_search.html', {
        scope: $scope,
        animation: 'slide-in-right'
    }).then(function (modal) {
        $scope.search.modal = modal;
    });
    $ionicModal.fromTemplateUrl('templates/modal_job_add.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.new.modal = modal;
    });
})
