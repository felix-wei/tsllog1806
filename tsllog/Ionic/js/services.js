angular.module('starter.services', [])


.factory('SV_Company', function ($http, $timeout, $ionicLoading) {
    var data = {
        list: [],
        company: {
            //Code: "gke",
            //Name: "Cargo Erp Pte Ltd",
            //ServerUri: "http://192.168.1.110:86",
            //WebSiteUri: "http://192.168.1.110:88"
        },
    }
    var vm = {
        initData: function () {
            $ionicLoading.show({
                template: 'Load...'
            });
            var pars = "";
            var func = "/Company_GetList";
            $http.jsonp(vm.GetWebServiceUrl_Company() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.list = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide()
                    });
        },
        GetData: function () {
            return data;
        },
        SetCompany: function (par, cb) {
            if (par && par != '') {
                //console.log('=========== in set company');
                if (data.list.length > 0) {
                    for (var i = 0; i < data.list.length; i++) {
                        if (data.list[i].Code == par) {
                            data.company = data.list[i];
                            console.log('=========== set company: ' + par, data.company);
                            break;
                        }
                    }
                    if (data.company.Code == par && cb) {
                        cb();
                    }
                } else {
                    $timeout(function () {
                        vm.SetCompany(par, cb);
                    }, 500);
                }
            }
        },
        SetCompany_ByInput: function (par, s_cb, f_cb) {
            if (!par || par.length == 0) {
                if (f_cb) {
                    f_cb();
                }
                return;
            }

            //====================== for developer
            if (par == 'test') {
                data.company = {
                    Code: par,
                    Name: par,
                    ServerUri: 'http://192.168.1.105:82/Mobile',
                    WebSiteUri: 'http://192.168.1.105:82'
                }
                console.log('=========== set company: ' + par, data.company);
                if (s_cb) {
                    s_cb();
                }
                return;
            }
            data.company = {
                Code: par,
                Name: par,
                ServerUri: 'http://' + par + '.cargoerp.com/Mobile',
                WebSiteUri: 'http://' + par + '.cargoerp.com'
            }
            console.log('=========== set company: ' + par, data.company);
            if (s_cb) {
                s_cb();
            }
        },
        GetServerUri: function () {
            if (data.company.ServerUri) {
                return data.company.ServerUri;
            } else {
                return '';
            }
        },
        GetWebSiteUri: function () {
            if (data.company.WebSiteUri) {
                return data.company.WebSiteUri;
            } else {
                return '';
            }
        },
        GetWebServiceUrl: function () {
            if (data.company.ServerUri) {
                return data.company.ServerUri + '/Connect.asmx';
            } else {
                return '';
            }
        },
        GetCompanyCode: function () {
            return data.company.Code;
        },
        GetWebServiceUrl_Company: function () {
            //return 'http://192.168.1.114:82/Mobile/Connect_Company.asmx';
            return 'http://demo.cargoerp.com/Mobile/Connect_Company.asmx';
        }
    }
    //vm.initData();
    return vm;
})


.factory('SV_DeviceData', function () {
    var data = {
        deviceId: '',
        platform: ''
    }

    //try {
    //    data.platform = ionic.Platform.platform();
    //    data.deviceId = device.uuid == null ? '' : device.uuid;
    //    console.log(data.platform + ' ====================');
    //}
    //catch (e) { }

    var vm = {
        GetData: function () {
            if (!data.deviceId || data.deviceId.length == 0 || !data.platform || data.platform.length == 0) {
                console.log('============== in device server, geting device or platform');
                try {
                    data.platform = ionic.Platform.platform();
                    data.deviceId = device.uuid == null ? '' : device.uuid;
                }
                catch (e) { }
            }
            return data;
        },
        LocalPath: function (par) {
            var re = par;
            if (data.platform == '') {
                try {
                    data.platform = ionic.Platform.platform();
                    data.deviceId = device.uuid == null ? '' : device.uuid;
                }
                catch (e) { }
            }
            if (data.platform == "android") {
                re = "/android_asset/www/" + par;
            }
            return re;
        },
        GetPlatForm: function () {
            return ionic.Platform.platform();
        }
    }
    return vm;
})

.factory('SV_AppStyle', function () {
    var data = {
        color_init: 'positive',//========== init status
        color: '',
        //============= light白，stable灰，positive蓝，calm浅蓝，balanced绿，energized黄，assertive红，royal紫，dark黑
        bar_list: {},
        tabs_list: {},
        style_list: {},
        color_list: []
    };
    var vm = {
        GetData: function () {
            return data;
        },
        SetFix: function (ss) {
            ss.Fix_Style = data;
        },
        //======> when the page have modal, need add the function to control bar color.
        SetColor: function (par) {
            var name = data.color_init;
            if (par) { name = par; }
            data.color = name;

            data.color_list
        },
        InitColor: function () {
            data.color_list.push({ name: 'light' });
            data.color_list.push({ name: 'stable' });
            data.color_list.push({ name: 'positive' });
            data.color_list.push({ name: 'calm' });
            data.color_list.push({ name: 'balanced' });
            data.color_list.push({ name: 'energized' });
            data.color_list.push({ name: 'assertive' });
            data.color_list.push({ name: 'royal' });
            data.color_list.push({ name: 'dark' });

            data.bar_list.light = 'bar-light';
            data.bar_list.stable = 'bar-stable';
            data.bar_list.positive = 'bar-positive';
            data.bar_list.calm = 'bar-calm';
            data.bar_list.balanced = 'bar-balanced';
            data.bar_list.energized = 'bar-energized';
            data.bar_list.assertive = 'bar-assertive';
            data.bar_list.royal = 'bar-royal';
            data.bar_list.dark = 'bar-dark';

            data.tabs_list.light = 'tabs-light';
            data.tabs_list.stable = 'tabs-stable';
            data.tabs_list.positive = 'tabs-positive';
            data.tabs_list.calm = 'tabs-calm';
            data.tabs_list.balanced = 'tabs-balanced';
            data.tabs_list.energized = 'tabs-energized';
            data.tabs_list.assertive = 'tabs-assertive';
            data.tabs_list.royal = 'tabs-royal';
            data.tabs_list.dark = 'tabs-dark';


            data.style_list.light = 'light';
            data.style_list.stable = 'stable';
            data.style_list.positive = 'positive';
            data.style_list.calm = 'calm';
            data.style_list.balanced = 'balanced';
            data.style_list.energized = 'energized';
            data.style_list.assertive = 'assertive';
            data.style_list.royal = 'royal';
            data.style_list.dark = 'dark';

            data.color_init = GetInitData_Scheme();
            vm.SetColor();

        }
    };
    vm.InitColor();
    return vm;
})

.factory('SV_Modal', function ($ionicModal, $ionicActionSheet, SV_Upload, $ionicPopup, $ionicBackdrop, SV_AppStyle, SV_Company, $http, SV_DeviceData) {
    var vm = {
        //========SV_Modal.Modal_Upload($scope);
        //==> use to upload image with actionSheet or direct open camera. 
        //==> And u can use the 'then' function variate to callback to controller when end upload.
        //==> And u can set the 'upload.path' to control the path will save, that under the '~/Upload/' content
        //==> And u can use the 'setSharpness' function to set the picture Sharpness.
        Modal_Upload: function (ss) {
            if (!ss.Fix_Style) {
                SV_AppStyle.SetFix(ss);
            }
            ss.Modal_Upload = {
                modal: null,
                openModal: function () {
                    ss.Modal_Upload.modal.show();
                },
                closeModal: function () {
                    ss.Modal_Upload.modal.hide();
                },
                menuButton: null,
                openMenuButton: function () {
                    if (ss.Modal_Upload.menuButton) {
                        ss.Modal_Upload.menuButton();
                    } else {
                        ss.Modal_Upload.menuButton = $ionicActionSheet.show({
                            buttons: [{ text: "<b>Open Camera</b>" }, { text: "<b>Take Picture</b>" }],
                            cancelText: "Cancel",
                            cancel: function () {
                                ss.Modal_Upload.menuButton = null;
                            },
                            buttonClicked: function (index) {
                                if (index == 0) {
                                    ss.Modal_Upload.openCamera();
                                }
                                if (index == 1) {
                                    ss.Modal_Upload.openAlbum();
                                }
                                ss.Modal_Upload.menuButton = null;
                                return true;
                            }
                        });
                    }
                },
                openCamera: function () {
                    navigator.camera.getPicture(function (imageURI) {
                        //console.log(imageURI);
                        ss.Modal_Upload.getPicture_cb(imageURI);
                    }, function (e) {
                        console.log('============= Camera error');
                    }, {
                        quality: ss.Modal_Upload.sharpness,
                        destinationType: Camera.DestinationType.FILE_URL
                    });
                },
                openAlbum: function () {
                    navigator.camera.getPicture(function (imageURI) {
                        ss.Modal_Upload.getPicture_cb(imageURI);
                    }, function (e) {
                        console.log('============= get Picture local error');
                    }, {
                        quality: ss.Modal_Upload.sharpness,
                        destinationType: navigator.camera.DestinationType.FILE_URI,
                        sourceType: navigator.camera.PictureSourceType.PHOTOLIBRARY
                    });
                },
                openChooser: function (type) {
                    fileChooser.open(function (url) {
                        //console.log('==========================' + url);
                        if (type == 'excel') {
                            if (url.substr(url.lastIndexOf('.') + 1).toLowerCase() != 'xls' && url.substr(url.lastIndexOf('.') + 1).toLowerCase() != 'xlsx') {
                                $ionicPopup.alert({ title: 'This file type is not ' + type + "!" });
                                return;
                            }
                        } else {
                            if (url.substr(url.lastIndexOf('.') + 1).toLowerCase() != type) {
                                $ionicPopup.alert({ title: 'This file type is not ' + type + "!" });
                                return;
                            }
                        }
                        ss.Modal_Upload.upload.uri = url;
                        ss.Modal_Upload.upload.upload();
                    }, function (e) { console.log('======= get ' + type + 'file error'); });
                },
                openAttach: function () {
                    fileChooser.open(function (url) {
                        console.log('==========================' + url);
                        ss.Modal_Upload.upload.uri = url;
                        ss.Modal_Upload.upload.upload();
                    }, function (e) { console.log('======= get file error'); });
                },
                getPicture_cb: function (imgUri) {
                    ss.Modal_Upload.upload.uri = imgUri;
                    ss.Modal_Upload.openModal();
                    //if (ss.Modal_Upload.then) {
                    //    ss.Modal_Upload.then(imgUri);
                    //}
                },
                then: null,
                sharpness: 10,
                setSharpness: function (par) {
                    if (par) {
                        ss.Modal_Upload.sharpness = par;
                    } else {
                        ss.Modal_Upload.sharpness = 10;
                    }
                },
                upload: {
                    uri: '',
                    path: '',
                    upload: function () {
                        //SV_Upload.UploadImage_noprogress(ss.Modal_Upload.upload.uri, ss.Modal_Upload.upload, ss.Modal_Upload.upload.path);
                        if (ss.Popup_Progress) {
                            SV_Upload.UploadImage(ss.Modal_Upload.upload.uri, ss.Modal_Upload.upload, ss.Modal_Upload.upload.path, ss);
                        } else {
                            SV_Upload.UploadImage_noprogress(ss.Modal_Upload.upload.uri, ss.Modal_Upload.upload, ss.Modal_Upload.upload.path);
                        }

                    },
                    success: function () {
                        if (ss.Modal_Upload.then) {
                            ss.Modal_Upload.then(ss.Modal_Upload.upload.uri);
                        }
                        //$ionicPopup.alert({
                        //    title: 'Upload Success',
                        //})
                        ss.Modal_Upload.closeModal();
                        ss.Modal_Upload.upload.finish();
                    },
                    error: function () {
                        $ionicPopup.alert({
                            title: 'Upload Error',
                        })
                        ss.Modal_Upload.closeModal();
                        ss.Modal_Upload.upload.finish();
                    },
                    finish: function () {
                        ss.Modal_Upload.upload.path = '';
                        ss.Modal_Upload.then = null;
                        ss.Modal_Upload.setSharpness();
                    }
                }
            }

            $ionicModal.fromTemplateUrl('templates/modal_upload_img.html', {
                scope: ss
            }).then(function (modal) {
                ss.Modal_Upload.modal = modal;
            });
        },
        //=======SV_Modal.Modal_FullScreen_Image($scope);
        //==> to show the full screen image ,or get Remote image uri.
        Modal_FullScreen_Image: function (ss) {
            ss.Modal_FullScreen_Image = {
                ImageUrl_Remote: function (filepath, filetype) {
                    //return GetRemoteImageUrl(par);
                    //var temp = ss.Modal_FullScreen_Image.Icon_Local(filetype);
                    //if (temp && temp.length > 0) {
                    //    return temp;
                    //}
                    if (!filepath || filepath == '') {
                        return 'img/logo.png';
                    }
                    var remoteUri = '';
                    var fp = '/Photos/' + filepath;
                    //console.log('==============', filepath, filetype);
                    remoteUri = SV_Company.GetWebSiteUri();
                    if (remoteUri.length > 0) {
                        return remoteUri + fp;
                    } else {
                        return 'img/logo.png';
                    }
                },
                IconUrl_Remote: function (par) {
                    var temp = "";
                    if (par && par != "") {
                        temp = par.substr(0, par.lastIndexOf('.')) + "_s200" + par.substr(par.lastIndexOf('.'));
                    }
                    return GetRemoteImageUrl(temp);
                },
                Icon_Local: function (type) {
                    //if (type.toLowerCase() == 'excel') {
                    //    return 'img/icon_excel.png';
                    //}
                    //if (type.toLowerCase() == 'pdf') {
                    //    return 'img/icon_pdf.png';
                    //}
                    return 'img/file_icon/' + type + '.png';
                },
                UserIcon_Remote: function (par) {
                    var re = SV_Company.GetWebSiteUri() + '/Mobile/UserIcon/' + par + '.jpg';
                    return re;
                },
                modal: null,
                data: {
                    uri: '',
                    name: '',
                },
                openModal: function (path, name) {
                    ss.Modal_FullScreen_Image.data.uri = path;
                    ss.Modal_FullScreen_Image.data.name = name;
                    ss.Modal_FullScreen_Image.modal.show();
                },
                closeModal: function () {
                    ss.Modal_FullScreen_Image.modal.hide();
                },
                openAttachFile: function (path) {
                    //console.log(path);
                    var pf = SV_DeviceData.GetPlatForm().toLowerCase();
                    if (pf == "android" || pf == "ios") {
                        //var filename = path.substr(path.lastIndexOf('/') + 1);
                        var ar_file = path.split('/');
                        var JobNo = '';
                        var filename = '';
                        //console.log('============' + path);
                        if (ar_file.length > 1) {
                            JobNo = ar_file[ar_file.length - 2];
                            filename = ar_file[ar_file.length - 1];
                            console.log(JobNo);
                            console.log(filename);
                        } else {
                            return;
                        }
                        vm.localFileIsExist('CargoERP/file/' + JobNo, filename, function (localfile) {
                            if (localfile.length > 0) {
                                window.plugins.fileOpener.open(localfile);
                            } else {
                                vm.Popup_Progress(ss, 'Download');
                                SV_Upload.Download([path], 'CargoERP/file/' + JobNo, ss, function (par) {
                                    //console.log(par);
                                    vm.Popup_Progress(ss, 'Upload');
                                    window.plugins.fileOpener.open(par);
                                });
                            }
                        });
                    }
                    //window.plugins.fileOpener.open(path);
                }
            };

            $ionicModal.fromTemplateUrl('templates/modal_fullscreen_img.html', {
                scope: ss
            }).then(function (modal) {
                ss.Modal_FullScreen_Image.modal = modal;
            });
        },
        //=======SV_Modal.Popup_Progress($scope);   
        //==> to control show Progress or not.
        Popup_Progress: function (ss, name) {
            ss.Popup_Progress = {
                popup: null,
                p_now: '0',
                p_max: '0',
                open: function () {
                    ss.Popup_Progress.popup = $ionicPopup.show({
                        template: '<div><i class="icon ion-loading-c"></i>{{Popup_Progress.p_now+"/"+Popup_Progress.p_max}}</div>',
                        title: name + '...',
                        //subTitle: 'Please use normal things',
                        scope: ss,
                    })
                },
                close: function () {
                    if (ss.Popup_Progress.popup) {
                        ss.Popup_Progress.popup.close();
                    }
                    //$ionicBackdrop.retain();
                    //$ionicBackdrop.release();
                },
                begin: function (max) {
                    //$ionicBackdrop.retain();
                    //$ionicBackdrop.release();
                    ss.Popup_Progress.p_now = "0";
                    ss.Popup_Progress.p_max = max;
                    ss.Popup_Progress.open();
                },
                setText: function (now, max) {
                    //console.log('==========' + now + '/' + max);
                    ss.Popup_Progress.p_now = now;
                    if (max) {
                        ss.Popup_Progress.p_max = max;
                    }
                },
                end: function () {
                    ss.Popup_Progress.close();
                },
                getText_now: function () {
                    return ss.Popup_Progress.p_now;
                }
            }
        },
        Modal_Signature: function (ss) {
            ss.Modal_Signature = {
                modal: null,
                data: {},
                then: null,
                openModal: function () {
                    ss.Modal_Signature.modal.show();
                    ss.Modal_Signature.data.sigCap = new SignatureCapture("signature");

                },
                closeModal: function () {
                    ss.Modal_Signature.then = null;
                    ss.Modal_Signature.modal.hide();
                },
                save: function () {
                    window.canvas2ImagePlugin.saveImageDataToLibrary(
                        function (path) {
                            //console.log(msg);
                            //$scope.signature.uploadImage(msg);
                            //if (ss.Modal_Signature.then) {
                            //    ss.Modal_Signature.then(path);
                            //}
                            ss.Modal_Signature.upload.uri = path;
                            ss.Modal_Signature.upload.upload();
                        },
                        function (msg) { console.log('false') },
                        document.getElementById('signature')
                    );
                },
                restart: function () {
                    ss.Modal_Signature.data.sigCap.clear();
                },
                destory: function () {
                    ss.Modal_Signature.modal.remove();
                },
                upload: {
                    uri: '',
                    path: '',
                    upload: function () {
                        if (ss.Popup_Progress) {
                            SV_Upload.UploadImage(ss.Modal_Signature.upload.uri, ss.Modal_Signature.upload, ss.Modal_Signature.upload.path, ss);
                        } else {
                            SV_Upload.UploadImage_noprogress(ss.Modal_Signature.upload.uri, ss.Modal_Signature.upload, ss.Modal_Signature.upload.path);
                        }
                    },
                    success: function () {
                        if (ss.Modal_Signature.then) {
                            ss.Modal_Signature.then(ss.Modal_Signature.upload.uri);
                        }
                        ss.Modal_Signature.closeModal();
                        ss.Modal_Signature.upload.finish();
                    },
                    error: function () {
                        $ionicPopup.alert({
                            title: 'Upload Error',
                        })
                        ss.Modal_Signature.closeModal();
                        ss.Modal_Signature.upload.finish();
                    },
                    finish: function () {
                        ss.Modal_Signature.upload.path = '';
                        ss.Modal_Signature.then = null;
                    }
                }
            }
            $ionicModal.fromTemplateUrl('templates/modal_drawing.html', {
                scope: ss
            }).then(function (modal) {
                ss.Modal_Signature.modal = modal;
            });
        },
        localFileIsExist: function (path, filename, cb) {
            var re = '';
            window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, function (fileSystem) {
                fileSystem.root.getDirectory(path, {
                    create: true,
                    exclusive: false
                }, function (path1) {
                    path1.getFile(filename, {
                        create: false,
                        exclusive: false
                    }, function (d) {
                        //console.log('======sss' + d.toURL());
                        re = d.toURL();
                        if (cb) {
                            cb(re);
                        }
                    }, function (d) {
                        //console.log('======ffff', d);
                        if (cb) {
                            cb('');
                        }
                    });
                })
            });
            //return re;
        }

    }
    return vm;
})

.factory('SV_Upload', function ($ionicPopup, $q, SV_Company) {
    var vm = {
        Upload: function (imageURI, path) {
            var options = new FileUploadOptions();

            //用于设置参数，服务端的Request字串
            options.fileKey = "fileAddPic";
            options.fileName = path + imageURI.substr(imageURI.lastIndexOf('/') + 1);

            //console.log('====================='+options.fileName);
            //如果是图片格式，就用image/jpeg，其他文件格式上官网查API
            //options.mimeType = "image/jpeg";
            //这里的uri根据自己的需求设定，是一个接收上传图片的地址
            var url = encodeURI(SV_Company.GetServerUri() + "/uploadImage.ashx");
            var ft = new FileTransfer();



            ft.onprogress = function (progressEvent) {
            };

            ft.upload(imageURI, url, function () {
                console.log('upload success');
            }, function () {
                console.log('upload false');
            }, options);
        },
        UploadImage: function (imageURI, loading, path, ss) {
            var options = new FileUploadOptions();

            //用于设置参数，服务端的Request字串
            options.fileKey = "fileAddPic";
            options.fileName = path + imageURI.substr(imageURI.lastIndexOf('/') + 1);

            //console.log('====================='+options.fileName);
            //如果是图片格式，就用image/jpeg，其他文件格式上官网查API
            //options.mimeType = "image/jpeg";
            //这里的uri根据自己的需求设定，是一个接收上传图片的地址
            var url = encodeURI(SV_Company.GetServerUri() + "/uploadImage.ashx");
            var ft = new FileTransfer();



            ft.onprogress = function (progressEvent) {
                if (progressEvent.lengthComputable) {
                    //loading.setText(progressEvent.loaded + '/' + progressEvent.total);
                    ss.Popup_Progress.setText(progressEvent.loaded, progressEvent.total);
                }
            };
            ss.Popup_Progress.begin("1");

            ft.upload(imageURI, url, function () {
                ss.Popup_Progress.end();
                loading.success();
            }, function () {
                ss.Popup_Progress.end();
                loading.error();
            }, options);
        },
        UploadImage_noprogress: function (imageURI, loading, path) {
            var options = new FileUploadOptions();

            //用于设置参数，服务端的Request字串
            options.fileKey = "fileAddPic";
            options.fileName = path + imageURI.substr(imageURI.lastIndexOf('/') + 1);

            //console.log('====================='+options.fileName);
            //如果是图片格式，就用image/jpeg，其他文件格式上官网查API
            //options.mimeType = "image/jpeg";
            //这里的uri根据自己的需求设定，是一个接收上传图片的地址
            var url = encodeURI(GetUploadUrl() + "/uploadImage.ashx");
            var ft = new FileTransfer();

            ft.upload(imageURI, url, function () {
                loading.success();
            }, function () {
                loading.error();
            }, options);
        },
        Download_noprogress: function (files, path) {
            var defers = new Array();
            var promises = new Array();
            angular.forEach(files, function (file) {
                var t_d = $q.defer();
                defers.push(t_d);
                promises.push(t_d.promise);
            });
            $q.all(promises).then(function (d) {
                var temp_all = "All Success";
                var isAll = true;
                var temp = "";
                angular.forEach(d, function (dd) {
                    var t = dd.file;
                    switch (dd.status) {
                        case 1:
                            t += ' success';
                            break;
                        case 2:
                            isAll = false;
                            t += ' false';
                            break;
                        case 3:
                            isAll = false;
                            t += ' source error';
                            break;
                    }
                    temp += temp.length > 0 ? '<br/>' + t : t;
                })
                $ionicPopup.alert({
                    title: 'Download',
                    template: (isAll ? temp_all : temp)
                });
            }, function (e) {
                $ionicPopup.alert({
                    title: 'Download',
                    template: e
                });
            });
            //console.log(file);
            //--=====android  add in manifest.xml <uses-permission android:name="android.permission.INTERNET"/>  
            window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, function (fileSystem) {
                fileSystem.root.getDirectory(path, {
                    create: true,
                    exclusive: false
                }, function (path) {
                    angular.forEach(files, function (file, index) {
                        //console.log('read success');
                        //console.log($scope.action.selectList[0].note3.substr($scope.action.selectList[0].note3.lastIndexOf('/') + 1));
                        path.getFile(file.substr(file.lastIndexOf('/') + 1), {
                            create: true,
                            exclusive: false
                        }, function (newFile) {
                            console.log('======in download:' + newFile.toURL());
                            var filePath = newFile.toURL();
                            var url = encodeURI(file);
                            var fileTransfer = new FileTransfer();
                            //console.log('downloading..' + url);
                            //console.log('downloading..' + filePath);
                            fileTransfer.download(url, filePath, function (entry) {
                                console.log('download success!' + entry.fullpath);
                                defers[index].resolve({ file: filePath.substr(filePath.lastIndexOf('/') + 1), status: 1 });
                            }, function (error) {
                                defers[index].resolve({ file: filePath.substr(filePath.lastIndexOf('/') + 1), status: 2 });
                                console.log('download error!!');
                                //console.log("download error source " + error.source);
                                //console.log("download error target " + error.target);
                                //console.log("download error code " + error.code);
                                //console.log("download error exc " + error.exception);
                            });

                        }, function () {
                            console.log('File Error');
                            defers[index].resolve({ file: file.substr(file.lastIndexOf('/') + 1), status: 3 });
                        });
                    });

                }, function () {
                    console.log('Directory Error');
                    defers[0].reject('Directory Error');
                });
            }, function () {
                console.log('Device Error');
                defers[0].reject('Device Error');
                //$ionicPopup.alert({ title: 'Device Error' });
            });


        },
        Download_progress: function (files, path, ss) {
            var defers = new Array();
            var promises = new Array();
            angular.forEach(files, function (file) {
                var t_d = $q.defer();
                defers.push(t_d);
                var t_p = t_d.promise;
                t_p.then(function (d) {
                    var now = ss.Popup_Progress.getText_now();
                    ss.Popup_Progress.setText(parseInt(now, 10) + 1);
                    return d;
                })
                promises.push(t_p);
            });
            $q.all(promises).then(function (d) {
                var temp_all = "All Success";
                var isAll = true;
                var temp = "";
                angular.forEach(d, function (dd) {
                    var t = dd.file;
                    switch (dd.status) {
                        case 1:
                            t += ' success';
                            break;
                        case 2:
                            isAll = false;
                            t += ' false';
                            break;
                        case 3:
                            isAll = false;
                            t += ' source error';
                            break;
                    }
                    temp += temp.length > 0 ? '<br/>' + t : t;
                })
                //$ionicPopup.alert({
                //    title: 'Download',
                //    template: (isAll ? temp_all : temp)
                //});
                return isAll ? temp_all : temp;
            }, function (e) {
                //$ionicPopup.alert({
                //    title: 'Download',
                //    template: e
                //});
                return e;
            }).then(function (d) {
                ss.Popup_Progress.end();
                $ionicPopup.alert({
                    title: 'Download',
                    template: d
                });
                //$ionicBackdrop.retain();
                //$ionicBackdrop.release();
            });
            ss.Popup_Progress.begin(defers.length);
            //console.log(file);
            //--=====android  add in manifest.xml <uses-permission android:name="android.permission.INTERNET"/>  
            window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, function (fileSystem) {
                fileSystem.root.getDirectory(path, {
                    create: true,
                    exclusive: false
                }, function (path) {
                    angular.forEach(files, function (file, index) {
                        //console.log('read success');
                        //console.log($scope.action.selectList[0].note3.substr($scope.action.selectList[0].note3.lastIndexOf('/') + 1));
                        path.getFile(file.substr(file.lastIndexOf('/') + 1), {
                            create: true,
                            exclusive: false
                        }, function (newFile) {
                            console.log('======in download:' + newFile.toURL());
                            var filePath = newFile.toURL();
                            var url = encodeURI(file);
                            var fileTransfer = new FileTransfer();
                            //console.log('downloading..' + url);
                            //console.log('downloading..' + filePath);
                            fileTransfer.download(url, filePath, function (entry) {
                                console.log('download success!' + entry.fullpath);
                                defers[index].resolve({ file: filePath.substr(filePath.lastIndexOf('/') + 1), status: 1 });
                            }, function (error) {
                                defers[index].resolve({ file: filePath.substr(filePath.lastIndexOf('/') + 1), status: 2 });
                                console.log('download error!!');
                                //console.log("download error source " + error.source);
                                //console.log("download error target " + error.target);
                                //console.log("download error code " + error.code);
                                //console.log("download error exc " + error.exception);
                            });

                        }, function () {
                            console.log('File Error');
                            defers[index].resolve({ file: file.substr(file.lastIndexOf('/') + 1), status: 3 });
                        });
                    });

                }, function () {
                    console.log('Directory Error');
                    defers[0].reject('Directory Error');
                });
            }, function () {
                console.log('Device Error');
                defers[0].reject('Device Error');
                //$ionicPopup.alert({ title: 'Device Error' });
            });


        },
        Download_progress1: function (files, path, ss, cb) {
            var re_filepath = "";
            var defers = new Array();
            var promises = new Array();
            angular.forEach(files, function (file) {
                var t_d = $q.defer();
                defers.push(t_d);
                var t_p = t_d.promise;
                t_p.then(function (d) {
                    var now = ss.Popup_Progress.getText_now();
                    ss.Popup_Progress.setText(parseInt(now, 10) + 1);
                    return d;
                })
                promises.push(t_p);
            });
            $q.all(promises).then(function (d) {
                var temp_all = "All Success";
                var isAll = true;
                var temp = "";
                angular.forEach(d, function (dd) {
                    var t = dd.file;
                    switch (dd.status) {
                        case 1:
                            t += ' success';
                            break;
                        case 2:
                            isAll = false;
                            t += ' false';
                            break;
                        case 3:
                            isAll = false;
                            t += ' source error';
                            break;
                    }
                    temp += temp.length > 0 ? '<br/>' + t : t;
                })
                //$ionicPopup.alert({
                //    title: 'Download',
                //    template: (isAll ? temp_all : temp)
                //});
                return isAll ? temp_all : temp;
            }, function (e) {
                //$ionicPopup.alert({
                //    title: 'Download',
                //    template: e
                //});
                return e;
            }).then(function (d) {
                ss.Popup_Progress.end();
                if (cb) {
                    cb(re_filepath);
                } else {
                    $ionicPopup.alert({
                        title: 'Download',
                        template: d
                    });

                }
                //$ionicBackdrop.retain();
                //$ionicBackdrop.release();
            });
            ss.Popup_Progress.begin(defers.length);
            //console.log(file);
            //--=====android  add in manifest.xml <uses-permission android:name="android.permission.INTERNET"/>  
            window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, function (fileSystem) {
                fileSystem.root.getDirectory(path, {
                    create: true,
                    exclusive: false
                }, function (path) {
                    angular.forEach(files, function (file, index) {
                        //console.log('read success');
                        //console.log($scope.action.selectList[0].note3.substr($scope.action.selectList[0].note3.lastIndexOf('/') + 1));
                        path.getFile(file.substr(file.lastIndexOf('/') + 1), {
                            create: true,
                            exclusive: false
                        }, function (newFile) {
                            console.log('======in download:' + newFile.toURL());
                            var filePath = newFile.toURL();
                            re_filepath = filePath;
                            var url = encodeURI(file);
                            var fileTransfer = new FileTransfer();
                            //console.log('downloading..' + url);
                            //console.log('downloading..' + filePath);
                            fileTransfer.download(url, filePath, function (entry) {
                                console.log('download success!' + entry.fullpath);
                                defers[index].resolve({ file: filePath.substr(filePath.lastIndexOf('/') + 1), status: 1 });
                            }, function (error) {
                                defers[index].resolve({ file: filePath.substr(filePath.lastIndexOf('/') + 1), status: 2 });
                                console.log('download error!!');
                                //console.log("download error source " + error.source);
                                //console.log("download error target " + error.target);
                                //console.log("download error code " + error.code);
                                //console.log("download error exc " + error.exception);
                            });

                        }, function () {
                            console.log('File Error');
                            defers[index].resolve({ file: file.substr(file.lastIndexOf('/') + 1), status: 3 });
                        });
                    });

                }, function () {
                    console.log('Directory Error');
                    defers[0].reject('Directory Error');
                });
            }, function () {
                console.log('Device Error');
                defers[0].reject('Device Error');
                //$ionicPopup.alert({ title: 'Device Error' });
            });


        },
        Download: function (files, path, ss, cb) {
            if (ss.Popup_Progress) {
                vm.Download_progress1(files, path, ss, cb);
            } else {
                vm.Download_noprogress(files, path);
            }
        }
    }
    return vm;
})


.factory('SV_Firebase', function (SV_Company) {
    var data = {
        fb: null,
        server: {},
    }
    var vm = {
        init: function () {
            data.fb = new Firebase("https://zhaohui.firebaseio.com/CargoerpMove");
        },
        publish: function (channel, msg) {
            data.fb.child(channel).set(msg);
        },
        subscribe: function (channel, cb, par) {
            data.fb.child(channel).off("value");
            data.fb.child(channel).on('value', function (d) {
                var dv = d.val();
                if (dv) {
                    if (cb) {
                        cb(dv, par);
                    }
                }
            });
            console.log('connect ', channel);
        },
        publish_system: function (msg) {
            vm.publish(SV_Company.GetCompanyCode() + '&system', msg);
        },
        subscribe_system: function (JobList, Schedule, Map, Local_JobList, Local_Schedule, MasterData_Driver, MasterData_Towhead, ActivityLog, MapDriver, MasterData_Trailer, DailyJobFCL) {
            data.server.JobList = JobList;
            data.server.Schedule = Schedule;
            data.server.Local_JobList = Local_JobList;
            data.server.Local_Schedule = Local_Schedule;
            data.server.Map = Map;
            data.server.MapDriver = MapDriver;
            data.server.MasterData_Driver = MasterData_Driver;
            data.server.MasterData_Towhead = MasterData_Towhead;
            data.server.ActivityLog = ActivityLog;
            data.server.MasterData_Trailer = MasterData_Trailer;
            data.server.DailyJobFCL = DailyJobFCL;
            vm.subscribe(SV_Company.GetCompanyCode() + '&system', function (msg) {
                console.log('====== msg ', msg, msg.content);
                if (msg.type == "command") {
                    if (msg.content.target == "joblist") {
                        //data.server.JobList.system_msg_receive(msg);
                    }
                    if (msg.content.target == "map") {
                        data.server.Map.system_msg_receive(msg);
                        data.server.MapDriver.system_msg_receive(msg);
                    }
                    if (msg.content.target == "schedule") {
                        data.server.Schedule.system_msg_receive(msg);
                        data.server.DailyJobFCL.system_msg_receive(msg);
                    }
                    if (msg.content.target == "localjoblist") {
                        //data.server.Local_JobList.system_msg_receive(msg);
                        data.server.Local_Schedule.system_msg_receive(msg);
                    }
                    if (msg.content.target == "masterdata_driver_list") {
                        data.server.MasterData_Driver.system_msg_receive(msg);
                    }
                    if (msg.content.target == "masterdata_towhead_list") {
                        data.server.MasterData_Towhead.system_msg_receive(msg);
                    }
                    if (msg.content.target == "masterdata_trailer_list") {
                        data.server.MasterData_Trailer.system_msg_receive(msg);
                    }
                    if (msg.content.target == "activity") {
                        data.server.ActivityLog.system_msg_receive(msg);
                    }
                }
            });
        },
    };
    vm.init();
    return vm;
})

.factory('SV_Hinter', function (SV_DeviceData, $timeout) {
    var data = {
        Ids: [],
        notice_error_times: 0,
    }
    var vm = {
        msg_sound: function () {
            new Media(SV_DeviceData.LocalPath('sound/sound_msg.mp3'), function () { }, function (e) { console.log(e.message); }).play();
        },
        GetNotificationDefault: function () {
            //console.log(angular.toJson(plugin.notification.local.getDefaults()));
            //var df = {
            //    "message": "",
            //    "title": "",
            //    "autoCancel": false,
            //    "badge": 0,
            //    "id": "0",
            //    "json": "",
            //    "repeat": "",
            //    "icon": "icon",
            //    "smallIcon": null,
            //    "ongoing": false,         //fix in topbar ,can not be remove
            //    "sound": "TYPE_NOTIFICATION"
            //};
            if (!data.df) {
                data.df = plugin.notification.local.getDefaults();
                data.df.autoCancel = true;
                //data.df.ongoing = true;
                //data.df.id = 0;
                //data.df.icon = "img/ionic.png";// SV_DeviceData.LocalPath('img/ionic.png');
                //data.df.smallIcon = "/assets/www/img/ionic.png"; //SV_DeviceData.LocalPath('img/ionic.png');
            }
            return data.df;
        },
        GetNotificationId: function (par) {
            var re = null;
            for (var i = 0; i < data.Ids.length; i++) {
                if (data.Ids[i].name == par) {
                    re = data.Ids[i];
                    break;
                }
            }
            if (!re) {
                data.Ids.push({ name: par, Id: data.Ids.length });
                re = data.Ids[data.Ids.length - 1];
            }
            return re.Id;
        },
        GetNotificationName_ById: function (Id) {
            var re = null;
            for (var i = 0; i < data.Ids.length; i++) {
                if (data.Ids[i].Id == Id) {
                    re = data.Ids[i];
                    break;
                }
            }
            return re ? re.name : null;
        },
        SetNotificationOnclick: function (cb) {
            window.plugin.notification.local.onclick = cb;
        },
        Notice: function (title, msg, type) {
            try {
                var df = vm.GetNotificationDefault();
                df.title = title;
                df.message = msg;
                var t_type = type ? type : '';
                df.id = vm.GetNotificationId(t_type);
                plugin.notification.local.add(df);
            } catch (e) { }
        },
        Notice_system: function (title, msg) {
            $timeout(function () {
                try {
                    var df = vm.GetNotificationDefault();
                    df = angular.copy(df);
                    df.autoCancel = false;
                    df.ongoing = true;

                    df.title = title;
                    df.message = msg;
                    df.id = vm.GetNotificationId('system');
                    plugin.notification.local.add(df);
                } catch (e) {
                    //console.log('=============== notice sysmte error');
                    if (data.notice_error_times < 10) {
                        data.notice_error_times++;
                        vm.Notice_system(title, msg);
                    } else {
                        data.notice_error_times = 0;
                    }
                }

            }, 1000);

        },
        Notice_system_schedule: function () {
            var temp = "";
            if (data.schedule_local && data.schedule_local.length > 0) {
                temp = "Local:" + data.schedule_local;
            }
            if (data.schedule_haulier && data.schedule_haulier.length > 0) {
                temp = temp.length > 0 ? temp + "; Haulier:" + data.schedule_haulier : "Haulier:" + data.schedule_haulier;
            }
            if (temp.length > 0) {
                vm.Notice_system('Schedule Jobs:', temp);
            }
        },
        Notice_system_schedule_local: function (msg) {
            if (data.schedule_local) {
                if (data.schedule_local == msg) {
                    return;
                }
            }
            data.schedule_local = msg;
            vm.Notice_system_schedule();
        },
        Notice_system_schedule_haulier: function (msg) {
            if (data.schedule_haulier) {
                if (data.schedule_haulier == msg) {
                    return;
                }
            }
            data.schedule_haulier = msg;
            vm.Notice_system_schedule();
        }
    };
    return vm;
})

.factory('SV_Print', function (SV_Company) {
    var vm = {
        print: function (url, cb) {
            //PrintPage(cb, "Print", GetRemoteUrl() + "/Print/" + url);

            var re = window.open(vm.getPrintUrl(url), '_blank', 'location=no');
            return re;
        },
        getPrintUrl: function (url) {
            return SV_Company.GetWebSiteUri() + "/" + url;
        }
    }
    return vm;
})


.factory('SV_Login', function ($http, SV_Company, $ionicLoading, $ionicPopup, SV_User, SV_MessageChat, SV_MessageGroup, $state, SV_Schedule, SV_Schedule1, SV_LocalSchedule, SV_LocalSchedule1, SV_JobList, SV_LocalJobList, SV_Firebase, SV_Position, SV_Map, SV_MyStatus, SV_Hinter, SV_DriverList, SV_VehicleList, SV_Activity, SV_MapDriver, SV_DailyJobFCL, SV_TrackerList) {
    var data = {
        user: {
            user_login: '',
            user_password: '',
            mobile_no: '',
            company_code: '',
        }
    }
    var vm = {
        Login: function (cb) {
            console.log('===== in login');
            $ionicLoading.show({
                template: 'Login...'
            });
            SV_Company.SetCompany_ByInput(data.user.company_code, null, function () { $state.go('menu.login'); });
            var info = angular.toJson(data.user);
            var pars = "&info=" + info;
            var func = "/User_Login";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            window.localStorage.setItem('mobile_no', data.user.mobile_no);
                            window.localStorage.setItem('user_password', data.user.user_password);
                            window.localStorage.setItem('company_code', data.user.company_code);
                        }
                        vm.Login_callback(d.result);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                        $ionicPopup.alert({
                            title: 'Error',
                            template: 'Internet error'
                        })
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Login_Auto: function () {
            if (SV_User.GetData().user.isLogin == true) { return; }
            console.log('===== in auto login');
            data.user.mobile_no = window.localStorage.getItem('mobile_no') == null ? '' : window.localStorage.getItem('mobile_no');
            data.user.user_password = window.localStorage.getItem('user_password') == null ? '' : window.localStorage.getItem('user_password');
            data.user.company_code = window.localStorage.getItem('company_code') == null ? '' : window.localStorage.getItem('company_code');

            //===================================
            //======================= for developer begin  (auto login admin)
            if (data.user.mobile_no == '' && data.user.user_password == '' && data.user.company_code == '') {
                var temp_login = GetInitData_Login();
                //console.log(temp_login);
                data.user.mobile_no = temp_login.un;
                data.user.user_password = temp_login.pw;
                data.user.company_code = temp_login.company;
            }
            //===============  for developer end

            if (data.user.mobile_no == '' || data.user.user_password == '' || data.user.company_code == '') {
                $state.go('menu.login');
                return;
            }
            SV_Company.SetCompany_ByInput(data.user.company_code, function () {
                $ionicLoading.show({
                    template: 'Login...'
                });
                var info = angular.toJson(data.user);
                var pars = "&info=" + info;
                var func = "/User_Login";
                $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                        .success(function (d) {
                            if (d.result == undefined || d.result == null) {
                                return;
                            }
                            if (d.result.status != "1") {
                                $state.go('menu.login');
                            }
                            vm.Login_callback(d.result);
                        })
                        .error(function (data, status, headers, config) {
                            console.log('Error', data, status, config);
                            $ionicPopup.alert({
                                title: 'Error',
                                template: 'Can not Login!'
                            })
                        })
                        .finally(function () {
                            $ionicLoading.hide();
                        });
            }, function () { $state.go('menu.login'); });
        },
        Login_callback: function (par) {
            if (par.status == "1") {
                SV_Firebase.subscribe_system(SV_JobList, SV_Schedule1, SV_Map, SV_LocalJobList, SV_LocalSchedule1, SV_DriverList, SV_VehicleList, SV_Activity, SV_MapDriver, SV_TrackerList, SV_DailyJobFCL);
                var msg = {
                    type: "notice",
                    company: SV_Company.GetCompanyCode(),
                    content: {
                        command: '',
                        target: '',
                        detail: par.context.Name + " Login",
                        date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                    }
                };
                SV_Hinter.Notice_system('Hello ' + par.context.Name, 'Welcome to Cargo Connect!');
                //console.log(msg);
                SV_MessageChat.setInit();
                SV_MessageGroup.setInit();
                SV_Firebase.publish_system(msg);
                //data.user = par.context;
                //console.log(data);
                SV_User.Login(par.context);
                SV_MessageChat.RefreshData();
                SV_MessageChat.addsubscribe_chat();
                SV_MessageGroup.RefreshData();
                SV_MessageGroup.addsubscribe_chat();
                //SV_Schedule.initData();
                //SV_Schedule.GetNoticeData();
                SV_Schedule1.InitData();
                SV_Schedule1.GetNoticeData();
                SV_DailyJobFCL.InitData();
                //SV_LocalSchedule.initData();
                SV_LocalSchedule1.InitData();
                SV_LocalSchedule1.GetNoticeData();
                SV_Position.WatchPosition();
                SV_MyStatus.RefreshData();
                SV_MapDriver.initData();
                $.getScript(SV_Company.GetServerUri() + '/Mobile_Remote_func.js', function () {
                    console.log('==================in login', MR_func1());
                    //MR_Login('zxcv', function (re) { console.log(re); });
                });

            }
        },
        GetData: function () {
            return data;
        },
        Logout: function () {
            console.log('==== in layout');
            SV_User.Logout();//GetData().user.isLogin = false;
            window.localStorage.setItem('mobile_no', '');
            window.localStorage.setItem('user_password', '');
            window.localStorage.setItem('company_code', '');
            SV_Position.WatchPosition_Clear();
        }
    }
    vm.Login_Auto();
    return vm;

})

.factory('SV_User', function ($http, SV_Company) {
    var data = {
        user: {},
        list: [],
        permission: [],
    };
    var vm = {
        Login: function (user) {
            console.log('============== login success', user);
            data.user = user;
            user.isLogin = true;
            vm.GetControl_ByRole(user.Role);

            var path = 'CargoERP';
            try {
                window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, function (fileSystem) {
                    fileSystem.root.getDirectory(path, {
                        create: true,
                        exclusive: false
                    }, function (path1) {
                        path = 'CargoERP/file';
                        window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, function (fileSystem) {
                            fileSystem.root.getDirectory(path, {
                                create: true,
                                exclusive: false
                            }, function (path1) {
                            })
                        });
                    })
                });
            } catch (e) { }
        },
        Logout: function () {
            data.permission = [];
            data.user.isLogin = false;
        },
        GetData: function () {
            return data;
        },
        GetUserName: function () {
            return data.user.Name;
        },
        GetControl_ByRole: function (Role) {
            var pars = "&Role=" + Role;
            var func = "/User_Login_GetControl";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.permission = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
        },
        Permis: function (code, type) {
            var re = false;
            for (var i = 0; i < data.permission.length; i++) {
                if (data.permission[i].Type == type && data.permission[i].Code == code) {
                    re = true;
                    break;
                }
            }
            return re;
        }
    };
    return vm;
})

.factory('SV_MyStatus', function ($http, SV_Company, $ionicLoading, SV_User, SV_Position) {
    var data = {
        status: {},
        oldStatus: {},
    };
    var vm = {
        GetData: function () {
            return data;
        },
        RefreshData: function () {
            var pars = "&user=" + SV_User.GetUserName();
            var func = "/MyStatus_GetData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.status = d.result;
                        data.oldStatus = angular.copy(d.result);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        towhead_save: function (cb) {
            if (data.status.Towhead == data.oldStatus.Towhead) { if (cb) { cb(); } return; }
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&user=" + SV_User.GetUserName() + "&towhead=" + data.status.Towhead + "&old=" + data.oldStatus.Towhead + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/MyStatus_Towhead_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        vm.RefreshData();
                        if (cb) {
                            cb();
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        trail_save: function (cb) {
            if (data.status.Trail == data.oldStatus.Trail) { if (cb) { cb(); } return; }
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&user=" + SV_User.GetUserName() + "&trail=" + data.status.Trail + "&old=" + data.oldStatus.Trail + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/MyStatus_Trail_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        vm.RefreshData();
                        if (cb) {
                            cb();
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    }
    return vm;
})

.factory('SV_MasterData', function ($http, SV_Company, $ionicLoading, $ionicModal) {
    var data = {
        port: {
            search: '',
            model: '',
            list: []
        },
        party: {
            search: '',
            model: '',
            list: []
        },
        container: {
            search: '',
            model: {},
            list: [],
        },
        containerType: {
            search: '',
            model: {},
            list: []
        },
        packageType: {
            list: []
        },
        driver: {
            search: '',
            model: {},
            list: []
        },
        towhead: {
            search: '',
            model: {},
            list: []
        },
        trail: {
            search: '',
            model: {},
            list: []
        },
        trip: {
            search: '',
            model: {},
            list: []
        },
        term: {
            search: '',
            model: {},
            list: []
        },
        Invoice_Chg: {
            search: '',
            model: {},
            list: []
        },
        Invoice_GstType: {
            search: '',
            model: {},
            list: []
        },
        cb: null,
    };
    var vm = {
        GetData: function () {
            return data;
        },
        GetData_Port: function () {
            return data.port;
        },
        GetData_Party: function () {
            return data.party;
        },
        RefreshData: function () {
            if (data.port.list.length == 0) {
                vm.RefreshData_Port_all(0);
            }
            if (data.party.list.length == 0) {
                vm.RefreshData_Party_all(0);
            }
            if (data.container.list.length == 0) {
                vm.RefreshData_Container_all(0);
            }
            if (data.containerType.list.length == 0) {
                vm.RefreshData_ContainerType();
            }
            if (data.packageType.list.length == 0) {
                vm.RefreshData_PackageType();
            }
            if (data.driver.list.length == 0) {
                vm.RefreshData_Driver();
            }
            if (data.towhead.list.length == 0) {
                vm.RefreshData_Towhead();
            }
            if (data.trail.list.length == 0) {
                vm.RefreshData_Trail();
            }
            if (data.trip.list.length == 0) {
                vm.RefreshData_Trip();
            }
        },
        RefreshData_Invoice: function () {
            if (data.term.list.length == 0) {
                vm.RefreshData_Invoice_Term();
            }
            if (data.Invoice_Chg.list.length == 0) {
                vm.RefreshData_Invoice_Chg_part(0);
            }
            if (data.Invoice_GstType.list.length == 0) {
                vm.RefreshData_Invoice_GstType();
            }
        },
        RefreshData_only: function (par) {
            if (par && par.length > 0) {
                var temp = par.toLowerCase();
                //console.log(par,temp,data[temp]);
                if (data[temp]) {
                    vm['RefreshData_' + par]();
                }
            }
        },
        RefreshData_Port: function () {
            if (data.port.search.length == 0) { return; }
            var pars = "&search=" + data.port.search;
            var func = "/MasterData_GetData_Port";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.port.list = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Port_all: function (loaded) {
            var pars = "&loaded=" + loaded;
            var func = "/MasterData_GetData_Port_All";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result.list, function (dd) {
                            data.port.list.push(dd);
                        })
                        //console.log('==========', data.port.list.length, parseInt(d.result.count, 10));
                        //console.log(data.port.list);
                        if (data.port.list.length < parseInt(d.result.count, 10)) {
                            vm.RefreshData_Port_all(data.port.list.length);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Party_all: function (loaded) {
            var pars = "&loaded=" + loaded;
            var func = "/MasterData_GetData_Party_All";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result.list, function (dd) {
                            data.party.list.push(dd);
                        })
                        //console.log('==========', data.party.list.length, parseInt(d.result.count, 10));
                        //console.log(data.port.list);
                        if (data.party.list.length < parseInt(d.result.count, 10)) {
                            vm.RefreshData_Party_all(data.party.list.length);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Container_all: function (loaded) {
            var pars = "&loaded=" + loaded;
            var func = "/MasterData_GetData_Container_All";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result.list, function (dd) {
                            data.container.list.push(dd);
                        })
                        //console.log('==========', data.container.list.length, parseInt(d.result.count, 10));
                        //console.log(data.port.list);
                        if (data.container.list.length < parseInt(d.result.count, 10)) {
                            vm.RefreshData_Container_all(data.container.list.length);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_ContainerType: function () {
            var pars = "";
            var func = "/MasterData_GetData_ContainerType";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.containerType.list = d.result;
                        //console.log(data.containerType);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_PackageType: function () {
            var pars = "";
            var func = "/MasterData_GetData_PackageType";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.packageType.list = d.result;
                        //console.log(data.containerType);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Driver: function () {
            var pars = "";
            var func = "/MasterData_GetData_Driver";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.driver.list = d.result;
                        //console.log(data.containerType);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Towhead: function () {
            var pars = "";
            var func = "/MasterData_GetData_Towhead";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.towhead.list = d.result;
                        //console.log(data.containerType);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Trail: function () {
            var pars = "";
            var func = "/MasterData_GetData_Trail";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.trail.list = d.result;
                        //console.log(data.containerType);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Trip: function () {
            var pars = "";
            var func = "/MasterData_GetData_Trip";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.trip.list = d.result;
                        //console.log(data.containerType);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Invoice_Term: function () {
            var pars = "";
            var func = "/MasterData_Invoice_GetData_Term";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.term.list = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Invoice_Chg_part: function (loaded) {
            var pars = "&loaded=" + loaded;
            var func = "/MasterData_Invoice_GetData_Chg_Part";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result.list, function (dd) {
                            dd.Code = dd.ChgcodeId;
                            dd.Name = dd.ChgcodeDes;
                            data.Invoice_Chg.list.push(dd);
                        })
                        //console.log('==========', data.Invoice_Chg.list.length, parseInt(d.result.count, 10));
                        //console.log(data.Invoice_Chg.list);
                        if (data.Invoice_Chg.list.length < parseInt(d.result.count, 10)) {
                            vm.RefreshData_Invoice_Chg_part(data.Invoice_Chg.list.length);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        RefreshData_Invoice_GstType: function () {
            var pars = "";
            var func = "/MasterData_Invoice_GetData_GstType";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.Invoice_GstType.list = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        SetFix: function (ss) {
            ss.Fix_MasterData = {};
            vm.SetFix_Port(ss);
            vm.SetFix_Party(ss);
            vm.SetFix_Container(ss);
            vm.SetFix_ContainerType(ss);
            vm.SetFix_PackageType(ss);
            vm.SetFix_Term(ss);
            vm.SetFix_MasterData(ss);
            //console.log(ss.Fix_MasterData);
        },
        SetFix_Port: function (ss) {
            ss.Fix_MasterData.port = {
                data: data.port,
                modal: null,
                openModal: function (cb, par) {
                    if (ss.Fix_MasterData.port.modal && cb) {
                        data.cb = cb;
                        //data.port.model = par;
                        data.port.search = par;
                        //vm.RefreshData_Port();
                        ss.Fix_MasterData.port.modal.show();
                    }
                },
                closeModal: function () {
                    ss.Fix_MasterData.port.modal.hide();
                },
                selected: function () {
                    data.cb(data.port.model);
                    ss.Fix_MasterData.port.modal.hide();
                },
                searchData: function () {
                    vm.RefreshData_Port();
                }
            };

            $ionicModal.fromTemplateUrl('templates/modal_port.html', {
                scope: ss
            }).then(function (modal) {
                ss.Fix_MasterData.port.modal = modal;
            });
        },
        SetFix_Party: function (ss) {
            ss.Fix_MasterData.party = {
                data: data.party,
                modal: null,
                openModal: function (cb, par) {
                    if (ss.Fix_MasterData.party.modal && cb) {
                        data.cb = cb;
                        //data.party.model = par;
                        data.party.search = par;
                        //vm.RefreshData_Party();
                        ss.Fix_MasterData.party.modal.show();
                    }
                },
                closeModal: function () {
                    ss.Fix_MasterData.party.modal.hide();
                },
                selected: function () {
                    data.cb(data.party.model);
                    ss.Fix_MasterData.party.modal.hide();
                },
                searchData: function () {
                    vm.RefreshData_Party();
                }

            };

            $ionicModal.fromTemplateUrl('templates/modal_party.html', {
                scope: ss
            }).then(function (modal) {
                ss.Fix_MasterData.party.modal = modal;
            });
        },
        SetFix_Container: function (ss) {
            ss.Fix_MasterData.container = {
                data: data.container,
                modal: null,
                openModal: function (cb, par) {
                    if (ss.Fix_MasterData.container.modal && cb) {
                        data.cb = cb;
                        data.container.search = par;
                        ss.Fix_MasterData.container.modal.show();
                    }
                },
                closeModal: function () {
                    ss.Fix_MasterData.container.modal.hide();
                },
                selected: function () {
                    data.cb(data.container.model);
                    ss.Fix_MasterData.container.modal.hide();
                }
            };

            $ionicModal.fromTemplateUrl('templates/modal_container.html', {
                scope: ss
            }).then(function (modal) {
                ss.Fix_MasterData.container.modal = modal;
            });
        },
        SetFix_MasterData: function (ss) {
            ss.Fix_MasterData.MasterData = {
                data: {},
                dataType: '',
                modal: null,
                openModal: function (cb, par, datatype) {
                    if (ss.Fix_MasterData.MasterData.modal && cb && datatype && datatype != '') {
                        var IsOK = false;
                        switch (datatype) {
                            case 'Driver':
                                IsOK = true;
                                ss.Fix_MasterData.MasterData.dataType = datatype;
                                ss.Fix_MasterData.MasterData.data = data.driver;
                                break;
                            case 'Towhead':
                                IsOK = true;
                                ss.Fix_MasterData.MasterData.dataType = datatype;
                                ss.Fix_MasterData.MasterData.data = data.towhead;
                                break;
                            case 'Trail':
                                IsOK = true;
                                ss.Fix_MasterData.MasterData.dataType = datatype;
                                ss.Fix_MasterData.MasterData.data = data.trail;
                                break;
                            case 'Trip':
                                IsOK = true;
                                ss.Fix_MasterData.MasterData.dataType = datatype;
                                ss.Fix_MasterData.MasterData.data = data.trip;
                                break;
                            case 'Chg':
                                IsOK = true;
                                ss.Fix_MasterData.MasterData.dataType = datatype;
                                ss.Fix_MasterData.MasterData.data = data.Invoice_Chg;
                        }
                        if (!IsOK) { return; }
                        data.cb = cb;
                        ss.Fix_MasterData.MasterData.data.search = par;
                        ss.Fix_MasterData.MasterData.modal.show();
                    }
                },
                closeModal: function () {
                    ss.Fix_MasterData.MasterData.modal.hide();
                    ss.Fix_MasterData.MasterData.data.modal = '';
                    ss.Fix_MasterData.MasterData.dataType = '';
                },
                selected: function () {
                    var temp = angular.fromJson(ss.Fix_MasterData.MasterData.data.model);
                    data.cb(temp);
                    ss.Fix_MasterData.MasterData.modal.hide();
                    ss.Fix_MasterData.MasterData.data.modal = '';
                    ss.Fix_MasterData.MasterData.dataType = '';
                }
            };

            $ionicModal.fromTemplateUrl('templates/modal_masterdata.html', {
                scope: ss
            }).then(function (modal) {
                ss.Fix_MasterData.MasterData.modal = modal;
            });
        },
        //===== Data only
        SetFix_ContainerType: function (ss) {
            ss.Fix_MasterData.containerType = {
                data: data.containerType,
            }
        },
        SetFix_PackageType: function (ss) {
            ss.Fix_MasterData.packageType = {
                data: data.packageType,
            }
        },
        SetFix_Term: function (ss) {
            ss.Fix_MasterData.term = { data: data.term };
        },
        OnDestory: function (ss) {
            if (ss.Fix_MasterData) {
                var temp_s = ss.Fix_MasterData;
                if (temp_s.port && temp_s.port.modal) { temp_s.port.modal.remove(); }
                if (temp_s.party && temp_s.party.modal) { temp_s.party.modal.remove(); }
                if (temp_s.container && temp_s.container.modal) { temp_s.container.modal.remove(); }
                if (temp_s.MasterData && temp_s.MasterData.modal) { temp_s.MasterData.modal.remove(); }
            }
        }
    }
    //vm.RefreshData();
    return vm;
})



.factory('SV_JobList', function ($http, SV_Company, $ionicLoading, SV_Firebase) {
    var data = {
        search: '',
        list: []
    }
    var vm = {
        GetData: function () {
            return data;
        },
        RefreshData: function (cb) {
            if (data.search.length == 0) {
                return;
            }
            $ionicLoading.show({
                template: 'Load...'
            });
            var pars = "&search=" + data.search;
            var func = "/Job_GetDataList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.JobDate1 = new Date(dd.JobDate).DateFormat('yyyy/MM/dd');
                        });
                        data.list = d.result;
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                vm.RefreshData();
            }
        }
    };

    return vm;
})

.factory('SV_JobAdd', function ($http, SV_Company, $ionicLoading, SV_User, SV_Firebase, SV_Position) {
    var data = {
        job: {
            JobType: '',
        }
    };
    var vm = {
        GetData: function () {
            return data;
        },
        Save: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(data.job);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_New_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            vm.system_msg_send('refresh', 'joblist', 'new');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        }
    };
    return vm;
})

.factory('SV_JobDetail', function ($http, SV_Company, $ionicLoading, SV_Firebase, SV_User, SV_Position, SV_Activity) {
    var data = {
        job: {
            info: {},
            container: [],
            trip: [],
            attachment: [],
            signature: {},
            charge: [],
            billing: [],
            stock: [],
            activity: [],
        }
    };
    var vm = {
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        },
        GetData: function () {
            return data;
        },
        RefreshData: function (JobNo, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&JobNo=" + JobNo;
            var func = "/Job_Detail_GetData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.job.info = d.result.info;
                        data.job.info.EtdDate = new Date(data.job.info.EtdDate).DateFormat('yyyy-MM-dd');
                        data.job.info.EtdTime = data.job.info.EtdTime.length == 4 ? data.job.info.EtdTime.substr(0, 2) + ":" + data.job.info.EtdTime.substr(2) : "00:00";
                        data.job.info.EtaDate = new Date(data.job.info.EtaDate).DateFormat('yyyy-MM-dd');
                        data.job.info.EtaTime = data.job.info.EtaTime.length == 4 ? data.job.info.EtaTime.substr(0, 2) + ":" + data.job.info.EtaTime.substr(2) : "00:00";

                        data.job.container = d.result.container;
                        data.job.trip = d.result.trip;

                        angular.forEach(d.result.activity, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.activity = d.result.activity;
                        data.job.attachment = d.result.attachment;
                        data.job.signature = d.result.signature;
                        data.job.charge = d.result.charge;
                        data.job.billing = d.result.billing;
                        data.job.stock = d.result.stock;
                        console.log(data.job);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });
        },
        Info_Save: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(data.job.info);
            var pars = "&info=" + info;
            //console.log(pars);
            var func = "/Job_Detail_Info_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            vm.system_msg_send('refresh', 'joblist', data.job.info.JobNo);
                            SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Container_Save: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Container_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            data.job.container = d.result.context;
                            vm.Activity_Refresh();
                            SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Container_GetNew: function (cb) {
            $ionicLoading.show({
                template: 'Load...'
            });
            var pars = "";
            var func = "/Job_Detail_Container_GetNew";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        d.result.JobNo = data.job.info.JobNo;
                        d.result.Weight = 0;
                        d.result.Volume = 0;
                        d.result.QTY = 0;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Attachment_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Attachment_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            angular.forEach(d.result.context.activity, function (dd) {
                                var c_datetime = new Date(dd.CreateDateTime);
                                //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                                dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                                dd.CreateTime = c_datetime.DateFormat('hh:mm');
                            })
                            data.job.activity = d.result.context.activity;
                            data.job.attachment = d.result.context.attachment;
                            SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Activity_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Activity_Add_Text";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.activity = d.result;
                        SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');

                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Signature_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Signature_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            data.job.signature = d.result.context;
                            vm.Activity_Refresh();
                            SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Trip_Save: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Trip_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status != "0") {
                            data.job.trip = d.result.context;
                            var temp = {
                                controller: SV_User.GetUserName(),
                                driver: par.DriverCode,
                                no: d.result.status
                            }
                            vm.system_msg_send('refresh', 'schedule', angular.toJson(temp));
                            SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');

                            vm.Activity_Refresh();
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Trip_GetNew: function (cb) {
            $ionicLoading.show({
                template: 'Load...'
            });
            var pars = "";
            var func = "/Job_Detail_Trip_GetNew";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        d.result.JobNo = data.job.info.JobNo;
                        var date = new Date();
                        d.result.FromDate = date.DateFormat('yyyy-MM-dd');
                        d.result.FromTime = date.DateFormat('hh:mm');
                        d.result.ToDate = date.DateFormat('yyyy-MM-dd');
                        d.result.ToTime = date.DateFormat('hh:mm');
                        d.result.Statuscode = 'U';
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Charge_Add: function (cb) {
            $ionicLoading.show({
                template: 'Load...'
            });
            var pars = "&JobNo=" + data.job.info.JobNo + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Charge_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            data.job.charge = d.result.context;
                            vm.Activity_Refresh();
                            SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');

                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Charge_Save: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info;
            var func = "/Job_Detail_Charge_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result);
                        if (d.result.status == "1") {
                            data.job.charge = d.result.context;
                            SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Billing_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            par.User = SV_User.GetUserName();
            var pars = "&info=" + angular.toJson(par) + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Billing_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(data.list);
                        if (d.result.status == "1") {
                            //vm.Activity_Refresh();
                            SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });

        },
        Billing_refresh: function () {
            var pars = "&No=" + data.job.info.JobNo;
            var func = "/Job_Detail_Billing_Refresh";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.job.billing = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        Activity_Refresh: function () {
            var pars = "&No=" + data.job.info.JobNo;
            var func = "/Job_Detail_Activity_Refresh";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.attachment = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        Stock_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var pars = "&info=" + angular.toJson(par) + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Stock_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        SV_Activity.SendMsg(data.job.info.JobNo, 'fcl');
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }

    };
    return vm;
})

.factory('SV_Schedule', function ($http, SV_Company, $ionicLoading, SV_User, SV_Hinter) {
    var data = {
        days: [],
        jobs: {
            list: [],
            search: '',
            total: '',
            showAll: '0',
        },
        selectDay: new Date(),
        showAll: false,
        isInit: true
    };
    var vm = {
        initData: function () {
            for (var i = 0; i < 7; i++) {
                data.days.push({
                    Id: i,
                    day: i,
                    isToday: false,
                    date: ''
                })
            }
            vm.GetWeekdays();
        },
        GetData: function () {
            return data;
        },
        GetWeekdays: function (par, cb) {
            var date = new Date();
            if (par) {
                if (typeof (par) == "string") {
                    //===='2010-01-10'
                    date = new Date(par);
                }
                if (typeof (par) == "object") {
                    date = par;
                }
            }
            //console.log(date);
            $ionicLoading.show({ template: 'Loading...' });
            //=======================  js 中从8：00开始为整数，为准确算出js.Date.datediff
            var par = "&date=" + date.DateFormat('yyyyMMdd 08:00');
            var func = "/Schedule_GetWeekdays";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.isToday = dd.isToday == "1" ? true : false;
                            dd.date = new Date(dd.date);
                        });
                        data.days = d.result;
                        //console.log(d.result);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });
        },
        GetJobList_ByDate: function (date, cb) {
            if (par == '') { return; }
            if (!date || date == '') { return; }
            $ionicLoading.show({ template: 'Loading...' });
            //=======================  js 中从8：00开始为整数，为准确算出js.Date.datediff
            var par = "&date=" + date.DateFormat('yyyyMMdd');
            var func = "/Schedule_GetJobList_byDate";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.time = dd.EtaTime.length == 4 ? dd.EtaTime.substr(0, 2) + ':' + dd.EtaTime.substr(2) : "";
                        })
                        data.jobs.list = d.result;
                        //console.log(data.jobs);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });
        },
        RefeshScheduleList: function (date, cb) {
            if (SV_User.GetUserName() == '') { return; }
            if (!date || date == '') { return; }
            if (data.showAll) {
                vm.RefeshScheduleList_ALL(date, cb);
                return;
            }
            $ionicLoading.show({ template: 'Loading...' });
            //=======================  js 中从8：00开始为整数，为准确算出js.Date.datediff
            var par = "&date=" + date.DateFormat('yyyyMMdd') + "&user=" + SV_User.GetUserName();
            var func = "/Schedule_GetTripList_byUser";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.show_fromdate = new Date(dd.FromDate).DateFormat('MM/dd');
                            dd.show_todate = new Date(dd.ToDate).DateFormat('MM/dd');
                        })
                        data.jobs.list = d.result;
                        //console.log(data.jobs);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });

        },
        RefeshScheduleList_ALL: function (date, cb) {
            $ionicLoading.show({ template: 'Loading...' });
            //=======================  js 中从8：00开始为整数，为准确算出js.Date.datediff
            var par = "&date=" + date.DateFormat('yyyyMMdd') + "&user=" + SV_User.GetUserName();
            var func = "/Schedule_GetTripList_All";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.show_fromdate = new Date(dd.FromDate).DateFormat('MM/dd');
                            dd.show_todate = new Date(dd.ToDate).DateFormat('MM/dd');
                        })
                        data.jobs.list = d.result;
                        //console.log(data.jobs);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });
        },
        setSelectDay: function (par) {
            data.selectDay = par;
        },
        setInit: function () {
            data.isInit = true;
        },
        GetNoticeData: function () {
            var par = "&user=" + SV_User.GetUserName();
            var func = "/Schedule_GetNoticeData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result, tab);
                        var temp = "";
                        angular.forEach(d.result, function (dd) {
                            temp = temp.length > 0 ? temp + "," + dd.JobNo : dd.JobNo;
                        })
                        //console.log(temp);
                        SV_Hinter.Notice_system_schedule_haulier(temp);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                vm.RefeshScheduleList(data.selectDay);
                vm.GetNoticeData();
            }
        },
    };
    return vm;
})

.factory('SV_ScheduleTrip', function ($http, SV_Company, $ionicLoading, SV_User, SV_Firebase, SV_Position) {
    var data = {
        trip: {}
    };
    var vm = {
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        },
        GetData: function () {
            return data;
        },
        RefreshData: function (par, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&No=" + par;
            var func = "/Schedule_GetTripData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.trip = d.result;
                        data.trip.FromDate = new Date(data.trip.FromDate).DateFormat('yyyy-MM-dd');
                        data.trip.ToDate = new Date(data.trip.ToDate).DateFormat('yyyy-MM-dd');
                        data.trip.Statuscode_index = 0;
                        switch (data.trip.Statuscode) {
                            case 'U':
                                data.trip.Statuscode_show = 'Use';
                                data.trip.Statuscode_index = 1;
                                break;
                            case 'S':
                                data.trip.Statuscode_show = 'Start';
                                data.trip.Statuscode_index = 2;
                                break;
                            case 'D':
                                data.trip.Statuscode_show = 'Doing';
                                data.trip.Statuscode_index = 3;
                                break;
                            case 'W':
                                data.trip.Statuscode_show = 'Waiting';
                                data.trip.Statuscode_index = 4;
                                break;
                            case 'P':
                                data.trip.Statuscode_show = 'Pending';
                                data.trip.Statuscode_index = 5;
                                break;
                            case 'C':
                                data.trip.Statuscode_show = 'Completed';
                                data.trip.Statuscode_index = 6;
                                break;
                            case 'X':
                                data.trip.Statuscode_show = 'Cancel';
                                data.trip.Statuscode_index = 7;
                                break;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Status_Save: function (par, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Schedule_Trip_Status_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == 1) {
                            var temp = {
                                controller: SV_User.GetUserName(),
                                driver: par.Driver,
                                no: par.Id
                            }
                            vm.system_msg_send('refresh', 'schedule', angular.toJson(temp));
                            data.trip.Statuscode = par.Status;
                            data.trip.Statuscode_index = 0;
                            switch (data.trip.Statuscode) {
                                case 'U':
                                    data.trip.Statuscode_show = 'Use';
                                    data.trip.Statuscode_index = 1;
                                    break;
                                case 'S':
                                    data.trip.Statuscode_show = 'Start';
                                    data.trip.Statuscode_index = 2;
                                    break;
                                case 'D':
                                    data.trip.Statuscode_show = 'Doing';
                                    data.trip.Statuscode_index = 3;
                                    break;
                                case 'W':
                                    data.trip.Statuscode_show = 'Waiting';
                                    data.trip.Statuscode_index = 4;
                                    break;
                                case 'P':
                                    data.trip.Statuscode_show = 'Pending';
                                    data.trip.Statuscode_index = 5;
                                    break;
                                case 'C':
                                    data.trip.Statuscode_show = 'Completed';
                                    data.trip.Statuscode_index = 6;
                                    break;
                                case 'X':
                                    data.trip.Statuscode_show = 'Cancel';
                                    data.trip.Statuscode_index = 7;
                                    break;
                            }
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    }
    return vm;
})

.factory('SV_Schedule1', function ($http, SV_Company, $ionicLoading, SV_User, SV_Hinter) {
    var data = {
        schedule: [],
        search: {
            search: '',
            from: new Date().DateFormat('yyyy-MM-dd'),
            to: new Date().DateFormat('yyyy-MM-dd'),
            list: []
        }
    };
    var vm = {
        InitData: function () {
            data.schedule = [];
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Today'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'This week'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Later'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Past'
            });
            vm.Refresh_ByName('Today');
            vm.Refresh_ByName('This week');
            vm.Refresh_ByName('Later');
            vm.Refresh_ByName('Past');
        },
        GetData: function () {
            return data;
        },
        GetData_ByName: function (name) {
            var re = null;
            for (var i = 0; i < data.schedule.length; i++) {
                if (data.schedule[i].name == name) {
                    re = data.schedule[i];
                    break;
                }
            }
            return re;
        },
        SetFocus: function (name) {
            angular.forEach(data.schedule, function (dd) {
                if (dd.name == name) {
                    dd.isFocus = true;
                } else {
                    dd.isFocus = false;
                }
            })
        },
        GetFocus: function () {
            var re = null;
            for (var i = 0; i < data.schedule.length; i++) {
                if (data.schedule[i].isFocus) {
                    re = data.schedule[i];
                    break;
                }
            }
            return re;
        },
        Refresh_ByName: function (name, cb) {
            var tab = vm.GetData_ByName(name);
            if (!tab) { return; }
            $ionicLoading.show({ template: 'Loading...' });
            var par = "&tab=" + tab.name + "&user=" + SV_User.GetUserName();
            var func = "/Haulier_Schedule1_GetList_ByTab";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result, tab);
                        if (tab.name == d.result.tab) {
                            angular.forEach(d.result.list, function (dd) {
                                dd.show_fromdate = new Date(dd.FromDate).DateFormat('MM/dd');
                                dd.show_todate = new Date(dd.ToDate).DateFormat('MM/dd');
                            });
                            tab.list = d.result.list;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });

        },
        DelJobFromTabList_ByNo: function (No) {
            var re = null;
            //console.log(data.schedule);
            if (No) {
                for (var i = 0; i < data.schedule.length; i++) {
                    var tab = data.schedule[i];
                    for (var j = 0; j < tab.list.length; j++) {
                        //console.log(tab.list[j]);
                        if (tab.list[j].Id == No) {
                            re = tab.list.splice(j, 1);
                            break;
                        }
                    }
                    if (re && re.length > 0) {
                        break;
                    }
                }
            }
        },
        Refresh_ByMsg: function (det) {
            var No = det.no;
            vm.DelJobFromTabList_ByNo(No);
            //if (det.driver != SV_User.GetUserName()) { return; }
            var par = "&No=" + No + "&user=" + SV_User.GetUserName();
            var func = "/Haulier_Schedule1_GetList_ByNo";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result);
                        var tab = vm.GetData_ByName(d.result.tab);
                        if (tab) {
                            angular.forEach(d.result.list, function (dd) {
                                dd.show_fromdate = new Date(dd.FromDate).DateFormat('MM/dd');
                                dd.show_todate = new Date(dd.ToDate).DateFormat('MM/dd');
                            });
                            tab.list = d.result.list;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });

        },
        Search: function (cb) {
            $ionicLoading.show({ template: 'Loading...' });
            var par = "&search=" + data.search.search + "&from=" + data.search.from + "&to=" + data.search.to;
            var func = "/Haulier_Schedule1_Search";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.JobDate1 = new Date(dd.JobDate).DateFormat('yyyy/MM/dd');
                        });
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        GetNoticeData: function () {
            var par = "&user=" + SV_User.GetUserName();
            var func = "/Haulier_Schedule1_GetNoticeData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result, tab);
                        var temp = "";
                        angular.forEach(d.result, function (dd) {
                            temp = temp.length > 0 ? temp + "," + dd.JobNo : dd.JobNo;
                        })
                        //console.log(temp);
                        SV_Hinter.Notice_system_schedule_local(temp);

                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                var det = angular.fromJson(msg.content.detail);
                vm.Refresh_ByMsg(det);
                vm.GetNoticeData();
            }
        },
    };
    return vm;
})

.factory('SV_DailyJobFCL', function ($http, SV_Company, $ionicLoading, SV_User, SV_Hinter) {
    var data = {
        schedule: [],
        search: {
            search: '',
            from: new Date().DateFormat('yyyy-MM-dd'),
            to: new Date().DateFormat('yyyy-MM-dd'),
            list: []
        }
    };
    var vm = {
        InitData: function () {
            data.schedule = [];
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Undelivered'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Completed'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Failed'
            });
            //data.schedule.push({
            //    search: '',
            //    list: [],
            //    isFocus: false,
            //    name: 'Past'
            //});
            vm.Refresh_ByName('Undelivered');
            vm.Refresh_ByName('Completed');
            vm.Refresh_ByName('Failed');
            //vm.Refresh_ByName('Past');
        },
        GetData: function () {
            return data;
        },
        GetData_ByName: function (name) {
            var re = null;
            for (var i = 0; i < data.schedule.length; i++) {
                if (data.schedule[i].name == name) {
                    re = data.schedule[i];
                    break;
                }
            }
            return re;
        },
        SetFocus: function (name) {
            angular.forEach(data.schedule, function (dd) {
                if (dd.name == name) {
                    dd.isFocus = true;
                } else {
                    dd.isFocus = false;
                }
            })
        },
        GetFocus: function () {
            var re = null;
            for (var i = 0; i < data.schedule.length; i++) {
                if (data.schedule[i].isFocus) {
                    re = data.schedule[i];
                    break;
                }
            }
            return re;
        },
        Refresh_ByName: function (name, cb) {
            var tab = vm.GetData_ByName(name);
            if (!tab) { return; }
            $ionicLoading.show({ template: 'Loading...' });
            var par = "&tab=" + tab.name + "&user=" + SV_User.GetUserName();
            var func = "/Haulier_Daily_GetList_ByTab";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result, tab);
                        if (tab.name == d.result.tab) {
                            angular.forEach(d.result.list, function (dd) {
                                dd.show_fromdate = new Date(dd.FromDate).DateFormat('MM/dd');
                                dd.show_todate = new Date(dd.ToDate).DateFormat('MM/dd');
                            });
                            tab.list = d.result.list;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });

        },
        DelJobFromTabList_ByNo: function (No) {
            var re = null;
            //console.log(data.schedule);
            //console.log('================ in del job', No, data.schedule);
            if (No) {
                for (var i = 0; i < data.schedule.length; i++) {
                    var tab = data.schedule[i];
                    for (var j = 0; j < tab.list.length; j++) {
                        if (tab.list[j].Id == No) {
                            re = tab.list.splice(j, 1);
                            break;
                        }
                    }
                    if (re && re.length > 0) {
                        break;
                    }
                }
            }
        },
        Refresh_ByMsg: function (det) {
            var No = det.no;
            vm.DelJobFromTabList_ByNo(No);
            //if (det.driver != SV_User.GetUserName()) { return; }
            var par = "&No=" + No + "&user=" + SV_User.GetUserName();
            var func = "/Haulier_Daily_GetList_ByNo";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result);
                        var tab = vm.GetData_ByName(d.result.tab);
                        if (tab) {
                            angular.forEach(d.result.list, function (dd) {
                                dd.show_fromdate = new Date(dd.FromDate).DateFormat('MM/dd');
                                dd.show_todate = new Date(dd.ToDate).DateFormat('MM/dd');
                            });
                            tab.list = d.result.list;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });

        },
        Search: function (cb) {
            $ionicLoading.show({ template: 'Loading...' });
            var par = "&search=" + data.search.search + "&from=" + data.search.from + "&to=" + data.search.to;
            var func = "/Haulier_Schedule1_Search";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.JobDate1 = new Date(dd.JobDate).DateFormat('yyyy/MM/dd');
                        });
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                var det = angular.fromJson(msg.content.detail);
                vm.Refresh_ByMsg(det);
            }
        },
    };
    return vm;
})



.factory('SV_LocalJobAdd', function ($http, SV_Company, $ionicLoading, SV_User, SV_Firebase, SV_Position) {
    var data = {
        job: {
            JobType: '',
        }
    };
    var vm = {
        GetData: function () {
            return data;
        },
        Save: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(data.job);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Local_Job_New_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            var temp = {
                                controller: SV_User.GetUserName(),
                                driver: '',
                                no: d.result.context
                            }
                            vm.system_msg_send('refresh', 'localjoblist', angular.toJson(temp));
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        }
    };
    return vm;
})

.factory('SV_LocalJobList', function ($http, SV_Company, $ionicLoading, SV_Firebase) {
    var data = {
        search: '',
        list: []
    }
    var vm = {
        GetData: function () {
            return data;
        },
        RefreshData: function (cb) {
            if (data.search.length == 0) {
                return;
            }
            $ionicLoading.show({
                template: 'Load...'
            });
            var pars = "&search=" + data.search;
            var func = "/Local_Job_GetDataList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.JobDate1 = new Date(dd.JobDate).DateFormat('yyyy/MM/dd');
                        });
                        data.list = d.result;
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                vm.RefreshData();
            }
        }
    };

    return vm;
})

.factory('SV_LocalJobDetail', function ($http, SV_Company, $ionicLoading, SV_Firebase, SV_User, SV_Position, SV_Activity) {
    var data = {
        job: {
            info: {},
            attachment: [],
            billing: [],
            stock: [],
            activity: [],
            watch: [],
        },
        JobProgress: [
            { index: 0, name: '' },
            { index: 1, name: 'Assigned' },
            { index: 2, name: 'Confirmed' },
            { index: 3, name: 'Picked' },
            { index: 4, name: 'Delivered' },
            { index: 5, name: 'Completed' },
            { index: 6, name: 'Canceled' },
        ]
    };
    var vm = {
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        },
        GetData: function () {
            return data;
        },
        RefreshData: function (JobNo, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&JobNo=" + JobNo;
            var func = "/Local_Job_Detail_GetData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.job.info = d.result.info;
                        data.job.info.Etd = new Date(data.job.info.Etd).DateFormat('yyyy-MM-dd');
                        data.job.info.Eta = new Date(data.job.info.Eta).DateFormat('yyyy-MM-dd');
                        data.job.info.JobProgress_index = 0;
                        for (var i = 0; i < data.JobProgress.length; i++) {
                            if (data.JobProgress[i].name == data.job.info.JobProgress) {
                                data.job.info.JobProgress_index = data.JobProgress[i].index;
                                break;
                            }
                        }
                        data.job.info.Driver_isUser = (SV_User.GetUserName() == data.job.info.Driver);
                        //==========booking 
                        data.job.info.BkgDate = new Date(data.job.info.BkgDate).DateFormat('yyyy-MM-dd');
                        data.job.info.BkgWt = parseFloat(data.job.info.BkgWt, 10);
                        data.job.info.BkgM3 = parseFloat(data.job.info.BkgM3, 10);
                        data.job.info.BkgQty = parseFloat(data.job.info.BkgQty, 10);
                        //========== transport
                        data.job.info.TptDate = new Date(data.job.info.TptDate).DateFormat('yyyy-MM-dd');
                        data.job.info.Wt = parseFloat(data.job.info.Wt, 10);
                        data.job.info.M3 = parseFloat(data.job.info.M3, 10);
                        data.job.info.Qty = parseFloat(data.job.info.Qty, 10);
                        //========== fee
                        data.job.info.FeeTpt = parseFloat(data.job.info.FeeTpt, 10);
                        data.job.info.FeeLabour = parseFloat(data.job.info.FeeLabour, 10);
                        data.job.info.FeeOt = parseFloat(data.job.info.FeeOt, 10);
                        data.job.info.FeeAdmin = parseFloat(data.job.info.FeeAdmin, 10);
                        data.job.info.FeeReimberse = parseFloat(data.job.info.FeeReimberse, 10);
                        data.job.info.FeeMisc = parseFloat(data.job.info.FeeMisc, 10);

                        angular.forEach(d.result.activity, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.activity = d.result.activity;
                        data.job.attachment = d.result.attachment;
                        data.job.billing = d.result.billing;
                        data.job.stock = d.result.stock;
                        data.job.watch = d.result.watch;
                        console.log(data.job);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });
        },
        Info_Save: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(data.job.info);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();;
            //console.log(pars);
            var func = "/Local_Job_Detail_Info_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            var temp = {
                                controller: SV_User.GetUserName(),
                                driver: data.job.info.Driver,
                                no: data.job.info.JobNo
                            }
                            vm.system_msg_send('refresh', 'localjoblist', angular.toJson(temp));
                            data.job.info.JobProgress_index = 0;
                            for (var i = 0; i < data.JobProgress.length; i++) {
                                if (data.JobProgress[i].name == data.job.info.JobProgress) {
                                    data.job.info.JobProgress_index = data.JobProgress[i].index;
                                    break;
                                }
                            }
                            data.job.info.Driver_isUser = (SV_User.GetUserName() == data.job.info.Driver);
                            vm.Activity_Refresh();
                            SV_Activity.SendMsg(data.job.info.JobNo, 'lcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Attachment_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Local_Job_Detail_Attachment_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        //console.log(d.result);
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            angular.forEach(d.result.context.activity, function (dd) {
                                var c_datetime = new Date(dd.CreateDateTime);
                                //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                                dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                                dd.CreateTime = c_datetime.DateFormat('hh:mm');
                            })
                            data.job.activity = d.result.context.activity;
                            data.job.attachment = d.result.context.attachment;
                            //data.job.attachment = d.result.context;
                            SV_Activity.SendMsg(data.job.info.JobNo, 'lcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Activity_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Job_Detail_Activity_Add_Text";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.activity = d.result;
                        SV_Activity.SendMsg(data.job.info.JobNo, 'lcl');

                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Billing_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            par.User = SV_User.GetUserName();
            var pars = "&info=" + angular.toJson(par) + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Local_job_Detail_Billing_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(data.list);
                        if (d.result.status == "1") {
                            //vm.Billing_refresh();
                            SV_Activity.SendMsg(data.job.info.JobNo, 'lcl');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });

        },
        Billing_refresh: function () {
            var pars = "&No=" + data.job.info.JobNo;
            var func = "/Local_job_Detail_Billing_Refresh";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.job.billing = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        trip_status_save: function () {
            var t_info = data.job.info;
            var jp = "";
            for (var i = 0; i < data.JobProgress.length; i++) {
                if (data.job.info.JobProgress_index + 1 == data.JobProgress[i].index) {
                    jp = data.JobProgress[i].name;
                    break;
                }
            }
            var info = {
                Id: t_info.Id,
                JobNo: t_info.JobNo,
                Driver: t_info.Driver,
                Towhead: t_info.VehicleNo,
                JobProgress: jp,
            }
            info = angular.toJson(info);
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&info=" + info + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Local_Job_Detail_Status_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            data.job.info.JobProgress = jp;
                            data.job.info.JobProgress_index++;
                            var temp = {
                                controller: SV_User.GetUserName(),
                                driver: data.job.info.Driver,
                                no: data.job.info.JobNo
                            }
                            vm.system_msg_send('refresh', 'localjoblist', angular.toJson(temp));
                            vm.Activity_Refresh();
                            SV_Activity.SendMsg(data.job.info.JobNo, 'lcl');
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Activity_Refresh: function () {
            var pars = "&No=" + data.job.info.JobNo;
            var func = "/Local_Job_Detail_Activity_Refresh";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.attachment = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
        },
        GetNextProgress: function (par) {
            var ind = data.job.info.JobProgress_index;
            if (par) { ind = par; }
            ind++;
            var re = "";
            for (var i = 0; i < data.JobProgress.length; i++) {
                if (data.JobProgress[i].index == ind) {
                    re = data.JobProgress[i].name;
                    break;
                }
            }
            return re;
        },
        Stock_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var pars = "&info=" + angular.toJson(par) + "&user=" + SV_User.GetUserName() + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Local_job_Detail_Stock_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        SV_Activity.SendMsg(data.job.info.JobNo, 'lcl');
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    };
    return vm;
})

.factory('SV_LocalSchedule', function ($http, SV_Company, $ionicLoading, SV_User) {
    var data = {
        days: [],
        jobs: {
            list: [],
            search: '',
            total: '',
            showAll: '0',
        },
        selectDay: new Date(),
        showAll: false,
        isInit: true
    };
    var vm = {
        initData: function () {
            for (var i = 0; i < 7; i++) {
                data.days.push({
                    Id: i,
                    day: i,
                    isToday: false,
                    date: ''
                })
            }
            vm.GetWeekdays();
        },
        GetData: function () {
            return data;
        },
        GetWeekdays: function (par, cb) {
            var date = new Date();
            if (par) {
                if (typeof (par) == "string") {
                    //===='2010-01-10'
                    date = new Date(par);
                }
                if (typeof (par) == "object") {
                    date = par;
                }
            }
            //console.log(date);
            $ionicLoading.show({ template: 'Loading...' });
            //=======================  js 中从8：00开始为整数，为准确算出js.Date.datediff
            var par = "&date=" + date.DateFormat('yyyyMMdd 08:00');
            var func = "/Schedule_GetWeekdays";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.isToday = dd.isToday == "1" ? true : false;
                            dd.date = new Date(dd.date);
                        });
                        data.days = d.result;
                        //console.log(d.result);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });
        },
        RefeshScheduleList: function (date, cb) {
            if (SV_User.GetUserName() == '') { return; }
            if (!date || date == '') { return; }
            if (data.showAll) {
                vm.RefeshScheduleList_ALL(date, cb);
                return;
            }
            $ionicLoading.show({ template: 'Loading...' });
            //=======================  js 中从8：00开始为整数，为准确算出js.Date.datediff
            var par = "&date=" + date.DateFormat('yyyyMMdd') + "&user=" + SV_User.GetUserName();
            var func = "/Local_Schedule_GetList_byUser";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.TptDate1 = new Date(dd.TptDate).DateFormat('MM/dd');
                        })
                        data.jobs.list = d.result;
                        //console.log(data.jobs);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });

        },
        RefeshScheduleList_ALL: function (date, cb) {
            $ionicLoading.show({ template: 'Loading...' });
            //=======================  js 中从8：00开始为整数，为准确算出js.Date.datediff
            var par = "&date=" + date.DateFormat('yyyyMMdd') + "&user=" + SV_User.GetUserName();
            var func = "/Local_Schedule_GetList_All";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.TptDate1 = new Date(dd.TptDate).DateFormat('MM/dd');
                        })
                        data.jobs.list = d.result;
                        //console.log(data.jobs);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });
        },
        setSelectDay: function (par) {
            data.selectDay = par;
        },
        setInit: function () {
            data.isInit = true;
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                vm.RefeshScheduleList(data.selectDay);
            }
        },
    };
    return vm;
})

.factory('SV_LocalSchedule1', function ($http, SV_Company, $ionicLoading, SV_User, SV_Hinter) {
    var data = {
        schedule: [],
        search: {
            search: '',
            from: new Date().DateFormat('yyyy-MM-dd'),
            to: new Date().DateFormat('yyyy-MM-dd'),
            list: []
        }
    };
    var vm = {
        InitData: function () {
            data.schedule = [];
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Today'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'This week'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Later'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Past'
            });
            vm.Refresh_ByName('Today');
            vm.Refresh_ByName('This week');
            vm.Refresh_ByName('Later');
            vm.Refresh_ByName('Past');
        },
        GetData: function () {
            return data;
        },
        GetData_ByName: function (name) {
            var re = null;
            for (var i = 0; i < data.schedule.length; i++) {
                if (data.schedule[i].name == name) {
                    re = data.schedule[i];
                    break;
                }
            }
            return re;
        },
        SetFocus: function (name) {
            angular.forEach(data.schedule, function (dd) {
                if (dd.name == name) {
                    dd.isFocus = true;
                } else {
                    dd.isFocus = false;
                }
            })
        },
        GetFocus: function () {
            var re = null;
            for (var i = 0; i < data.schedule.length; i++) {
                if (data.schedule[i].isFocus) {
                    re = data.schedule[i];
                    break;
                }
            }
            return re;
        },
        Refresh_ByName: function (name, cb) {
            var tab = vm.GetData_ByName(name);
            if (!tab) { return; }
            $ionicLoading.show({ template: 'Loading...' });
            var par = "&tab=" + tab.name + "&user=" + SV_User.GetUserName();
            var func = "/Local_Schedule1_GetList_ByTab";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result, tab);
                        if (tab.name == d.result.tab) {
                            angular.forEach(d.result.list, function (dd) {
                                dd.TptDate1 = new Date(dd.TptDate).DateFormat('MM/dd');
                            })
                            tab.list = d.result.list;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });

        },
        DelJobFromTabList_ByNo: function (No) {
            var re = null;
            //console.log(data.schedule);
            if (No) {
                for (var i = 0; i < data.schedule.length; i++) {
                    var tab = data.schedule[i];
                    for (var j = 0; j < tab.list.length; j++) {
                        //console.log(tab.list[j]);
                        if (tab.list[j].JobNo == No) {
                            re = tab.list.splice(j, 1);
                            break;
                        }
                    }
                    if (re && re.length > 0) {
                        break;
                    }
                }
            }
        },
        Refresh_ByMsg: function (det) {
            var No = det.no;
            vm.DelJobFromTabList_ByNo(No);
            //if (SV_User.GetUserName() != det.driver) { return; }
            var par = "&No=" + No + "&user=" + SV_User.GetUserName();
            var func = "/Local_Schedule1_GetList_ByNo";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        console.log(d.result);
                        var tab = vm.GetData_ByName(d.result.tab);
                        if (tab) {
                            angular.forEach(d.result.list, function (dd) {
                                dd.TptDate1 = new Date(dd.TptDate).DateFormat('MM/dd');
                            });
                            tab.list = d.result.list;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });

        },
        Search: function (cb) {
            $ionicLoading.show({ template: 'Loading...' });
            var par = "&search=" + data.search.search + "&from=" + data.search.from + "&to=" + data.search.to;
            var func = "/Local_Schedule1_Search";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.JobDate1 = new Date(dd.JobDate).DateFormat('yyyy/MM/dd');
                        });
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        GetNoticeData: function () {
            var par = "&user=" + SV_User.GetUserName();
            var func = "/Local_Schedule1_GetNoticeData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result, tab);
                        var temp = "";
                        angular.forEach(d.result, function (dd) {
                            temp = temp.length > 0 ? temp + "," + dd.JobNo : dd.JobNo;
                        })
                        //console.log(temp);
                        SV_Hinter.Notice_system_schedule_local(temp);

                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        system_msg_receive: function (msg) {
            //console.log('============= in SV_LocalSchedule1', msg);
            if (msg.content.command == "refresh") {
                var det = angular.fromJson(msg.content.detail);
                vm.Refresh_ByMsg(det);
                vm.GetNoticeData();
            }
        },
    };
    return vm;
})




.factory('SV_ContactList', function ($http, SV_Company, $ionicLoading) {
    var data = {
        list: []
    };
    var vm = {
        GetData: function () {
            return data;
        },
        RefreshData: function () {
            var pars = "";
            var func = "/Contact_GetDataList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.list = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        GetData_ByType: function (type, no, cb) {
            var pars = "&type=" + type + "&no=" + no;
            var func = "/Contact_GetData_ByType";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.temp = d.result;

                        if (cb) {
                            cb(data.temp);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        }
    }
    vm.RefreshData();
    return vm;
})

.factory('SV_MessageChat', function ($http, SV_Company, $ionicLoading, SV_User, SV_Firebase, SV_Hinter, $ionicBackdrop, $timeout) {
    var data = {
        UserList: [],
        send_callback: null,
        arrived_callback: function (par) {
            SV_Hinter.Notice('In Chat', 'Got new message', 'chat');
        },
        setNoRead_callback: null,
        isInit: true,
        haveHistory: true,
    };
    var vm = {
        GetData: function () {
            return data;
        },
        GetUser_ById: function (Id) {
            var re = null
            for (var i = 0; i < data.UserList.length; i++) {
                if (data.UserList[i].SequenceId == Id) {
                    re = data.UserList[i];
                    break;
                }
            }
            return re;
        },
        GetUser_ByChatName: function (name) {
            var re = null;
            for (var i = 0; i < data.UserList.length; i++) {
                if (data.UserList[i].chatName == name) {
                    re = data.UserList[i];
                    break;
                }
            }
            return re;
        },
        RefreshData: function () {
            var pars = "&user=" + SV_User.GetUserName();
            var func = "/Message_Chat_GetUserList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            dd.msgList = [];
                            dd.chatName = vm.getChatName(SV_User.GetUserName(), dd.Name);
                        });
                        data.UserList = d.result;
                        vm.GetNoReadMsg();
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        GetNoReadMsg: function () {
            var pars = "&user=" + SV_User.GetUserName();
            var func = "/Message_Chat_GetNoReadMsgNo";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        for (var i = 0; i < data.UserList.length; i++) {
                            var ul = data.UserList[i];
                            ul.noRead = 0;
                            for (var j = 0; j < d.result.length; j++) {
                                if (ul.chatName == d.result[j].item_chat) {
                                    ul.noRead = parseInt(d.result[j].c, 10);
                                }
                            }
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        getChatName: function (from, to) {
            var temp = "";
            var from1 = from ? from : "";
            var to1 = to ? to : '';
            if (from1 == "" && to1 == "") {
                return "";
            }
            if (from1 > to1) {
                temp = from1 + '&' + to1;
            } else {
                temp = to1 + '&' + from1;
            }
            return temp;
        },
        setCallback: function (sendcb, arrivedcb, setNoReadcb) {
            if (sendcb && typeof (sendcb) == 'function') {
                data.send_callback = sendcb;
            } else {
                data.send_callback = null;
            }
            if (arrivedcb && typeof (arrivedcb) == 'function') {
                data.arrived_callback = arrivedcb;
            } else {
                data.arrived_callback = function (par) {
                    SV_Hinter.Notice('In Chat', 'Got new message', 'chat');
                };
            }
            if (setNoReadcb && typeof (setNoReadcb) == 'function') {
                data.setNoRead_callback = setNoReadcb;
            } else {
                data.setNoRead_callback = null;
            }
        },
        openChat: function (from, to, chat) {
            var chatName = vm.getChatName(from, to);
            vm.receiveMsg_toServer_All(chatName, function (par) {
                chat.noRead = 0;
            });
            if (chat.msgList.length == 0) {
                vm.getHistoryMsg(0, chatName, function (list) {
                    angular.forEach(list, function (l) {
                        chat.msgList.push({
                            sender: l.speaker,
                            date: new Date(l.item_date).DateFormat('yyyy/MM/dd hh:mm:ss'),
                            msg: l.msg,
                            Id: l.Id
                        });
                    });
                    data.arrived_callback();
                });
            }
            data.haveHistory = true;
        },
        setInit: function () {
            data.isInit = true;
        },
        addsubscribe_chat: function () {
            SV_Firebase.subscribe(SV_Company.GetCompanyCode() + '&Chat', vm.addsubscribe_callback, null);
        },
        addsubscribe_callback: function (msg, par) {
            console.log(msg, msg.content.date, msg.content.text);
            if (data.isInit) {
                data.isInit = false;
                return;
            }
            if (msg.type == 'chat') {
                var content = msg.content;
                var chat = vm.GetUser_ByChatName(vm.getChatName(content.from, content.to));
                if (chat) {
                    chat.msgList.push({
                        sender: content.from,
                        date: content.date,
                        msg: content.text,
                        Id: content.Id
                    });
                    chat.noRead = chat.noRead + 1;
                    $ionicBackdrop.retain();
                    $timeout(function () {
                        $ionicBackdrop.release();
                    }, 100);
                    if (data.setNoRead_callback) {
                        data.setNoRead_callback();
                    }
                    if (data.arrived_callback) {
                        data.arrived_callback(content);
                    }
                }
                //if (content.from != SV_User.GetUserName()) {
                //    SV_Hinter.msg_sound();
                //}
                //console.log('===============', content.from, data.UserList);
            }
        },
        sendMsg: function (from, to, text) {
            var msg = {
                type: 'chat',
                company: SV_Company.GetCompanyCode(),
                content: {
                    from: from,
                    to: to,
                    text: text,
                    date: new Date().DateFormat('yyyy/MM/dd hh:mm:ss'),
                    Id: 0,
                }
            };
            vm.sendMsg_ToServer(msg.content, function (re) {
                msg.content.Id = re;
                SV_Firebase.publish(SV_Company.GetCompanyCode() + '&Chat', msg);
            });
            if (data.send_callback) {
                data.send_callback();
            }
        },
        sendMsg_ToServer: function (content, cb) {
            var info = {
                chat: json2str_replace(vm.getChatName(content.from, content.to)),
                from: content.from,
                to: content.to,
                text: content.text
            }
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Chat_AddMsg";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result && d.result != '0' && cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        receiveMsg_toServer: function (content, cb) {
            if (!content.Id || content.Id == '' || content.Id == '0') { return; }
            if (content.from == SV_User.GetUserName()) { return; }
            var info = {
                Id: content.Id,
                //chat: json2str_replace(vm.getChatName(content.from, content.to)),
                from: content.from,
                to: SV_User.GetUserName()
            }
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Chat_ReceiveMsg";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result && d.result != '0' && cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        receiveMsg_toServer_All: function (chat, cb) {
            var info = {
                chat: json2str_replace(chat),
                to: SV_User.GetUserName()
            }
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Chat_ReceiveMsg_All";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result);
                        if (cb) {
                            //if (d.result && d.result != '0' && cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        getHistoryMsg: function (No, chat, cb) {
            var info = {
                No: No,
                chat: json2str_replace(chat),
                to: SV_User.GetUserName()
            }
            console.log('============== in get history msg', info);
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Chat_GetHistoryMsg";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result);
                        if (d.result.length == 0) {
                            data.haveHistory = false;
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });

        },
        toViewMore: function (from, to, chat, cb) {
            var chatName = vm.getChatName(from, to);
            var No = 0;
            if (chat.msgList.length > 0) {
                No = chat.msgList[0].Id;
            }
            //console.log(chat.msgList);
            vm.getHistoryMsg(No, chatName, function (list) {
                var templist = [];
                angular.forEach(list, function (l) {
                    templist.push({
                        sender: l.speaker,
                        date: new Date(l.item_date).DateFormat('yyyy/MM/dd hh:mm:ss'),
                        msg: l.msg,
                        Id: l.Id
                    });
                });
                chat.msgList = templist.concat(chat.msgList);
                if (cb) {
                    cb();
                }
            });
        }
    };
    //vm.RefreshData();//============ when add refresh in user login ,it should be delete
    return vm;
})

.factory('SV_MessageGroup', function ($http, SV_Company, $ionicLoading, SV_User, SV_Firebase, $ionicBackdrop, $timeout, SV_Hinter) {
    var data = {
        GroupList: [],
        isInit: true,
        haveHistory: true,
        send_callback: null,
        arrived_callback: function (par) {
            SV_Hinter.Notice('In ChatGroup', 'Got new message', 'group');
        },
        setNoRead_callback: null,
    };
    var vm = {
        GetData: function () {
            return data;
        },
        GetGroup_ById: function (Id) {
            var list = data.GroupList;
            var re = null;
            for (var i = 0; i < list.length; i++) {
                if (Id == list[i].Id) {
                    re = list[i];
                    break;
                }
            }
            return re;
        },
        GetGroup_ByChat: function (name) {
            var re = null;
            for (var i = 0; i < data.GroupList.length; i++) {
                if (name == data.GroupList[i].chatName) {
                    re = data.GroupList[i];
                    break;
                }
            }
            return re;
        },
        RefreshData: function (cb) {
            var pars = "&user=" + SV_User.GetUserName();
            var func = "/Message_Group_GetGroupList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            dd.msgList = [];
                            dd.chatName = 'ChatGroup' + dd.Id;
                            dd.noRead = 0;
                        });
                        data.GroupList = d.result;
                        vm.GetNoReadMsg();
                        if (cb) {
                            cb();
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        GetNoReadMsg: function () {
            var pars = "&user=" + SV_User.GetUserName();
            var func = "/Message_Group_GetNoReadMsgNo";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        for (var i = 0; i < data.GroupList.length; i++) {
                            var ul = data.GroupList[i];
                            for (var j = 0; j < d.result.length; j++) {
                                if (ul.chatName == d.result[j].item_chat) {
                                    ul.noRead = parseInt(d.result[j].c, 10);
                                }
                            }
                        }
                        //console.log(data.GroupList);
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        setInit: function () {
            data.isInit = true;
        },
        addsubscribe_chat: function () {
            SV_Firebase.subscribe(SV_Company.GetCompanyCode() + '&ChatGroup', vm.addsubscribe_callback, null);
        },
        addsubscribe_callback: function (msg, par) {
            console.log(msg, msg.content.date, msg.content.text);
            if (data.isInit) {
                data.isInit = false;
                return;
            }
            if (msg.type == 'chat_group') {
                var content = msg.content;
                var chat = vm.GetGroup_ByChat(content.to);
                if (chat) {
                    chat.msgList.push({
                        sender: content.from,
                        date: content.date,
                        msg: content.text,
                        Id: content.Id
                    });
                    chat.noRead = chat.noRead + 1;
                    $ionicBackdrop.retain();
                    $timeout(function () {
                        $ionicBackdrop.release();
                    }, 100);
                    if (data.setNoRead_callback) {
                        data.setNoRead_callback();
                    }
                    if (data.arrived_callback) {
                        data.arrived_callback(content);
                    }
                }
            }
        },
        sendMsg: function (from, to, text) {
            var msg = {
                type: 'chat_group',
                company: SV_Company.GetCompanyCode(),
                content: {
                    from: from,
                    to: to,
                    text: text,
                    date: new Date().DateFormat('yyyy/MM/dd hh:mm:ss'),
                    Id: 0,
                }
            };
            vm.sendMsg_ToServer(msg.content, function (re) {
                msg.content.Id = re;
                SV_Firebase.publish(SV_Company.GetCompanyCode() + '&ChatGroup', msg);
            });
            if (data.send_callback) {
                data.send_callback();
            }
        },
        sendMsg_ToServer: function (content, cb) {
            var info = {
                chatId: content.to.replace('ChatGroup', ''),
                chat: content.to,
                from: content.from,
                text: content.text
            }
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Group_AddMsg";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result && d.result != '0' && cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        setCallback: function (sendcb, arrivedcb, setNoReadcb) {

            if (sendcb && typeof (sendcb) == 'function') {
                data.send_callback = sendcb;
            } else {
                data.send_callback = null;
            }
            if (arrivedcb && typeof (arrivedcb) == 'function') {
                data.arrived_callback = arrivedcb;
            } else {
                data.arrived_callback = function (par) {
                    SV_Hinter.Notice('In ChatGroup', 'Got new message', 'group');
                };
            }
            if (setNoReadcb && typeof (setNoReadcb) == 'function') {
                data.setNoRead_callback = setNoReadcb;
            } else {
                data.setNoRead_callback = null;
            }
        },
        receiveMsg_toServer: function (content, cb) {
            if (!content.Id || content.Id == '' || content.Id == '0') { return; }
            if (content.from == SV_User.GetUserName()) { return; }
            var info = {
                Id: content.Id,
                //chat: json2str_replace(vm.getChatName(content.from, content.to)),
                from: content.from,
                to: SV_User.GetUserName()
            }
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Group_ReceiveMsg";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result && d.result != '0' && cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        receiveMsg_toServer_All: function (chat, cb) {
            var info = {
                chat: json2str_replace(chat),
                to: SV_User.GetUserName()
            }
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Chat_ReceiveMsg_All";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result);
                        //if (d.result && d.result != '0' && cb) {
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        openChat: function (from, to, chat) {
            var chatName = to;
            vm.receiveMsg_toServer_All(chatName, function (par) {
                chat.noRead = 0;
            });
            if (chat.msgList.length == 0) {
                vm.getHistoryMsg(0, chatName, function (list) {
                    angular.forEach(list, function (l) {
                        chat.msgList.push({
                            sender: l.speaker,
                            date: new Date(l.item_date).DateFormat('yyyy/MM/dd hh:mm:ss'),
                            msg: l.msg,
                            Id: l.Id
                        });
                    });
                    data.arrived_callback();
                });
            }
            data.haveHistory = true;
        },
        getHistoryMsg: function (No, chat, cb) {
            var info = {
                No: No,
                chat: json2str_replace(chat),
                to: SV_User.GetUserName()
            }
            console.log('============== in get history msg', info);
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Group_GetHistoryMsg";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result);
                        if (d.result.length == 0) {
                            data.haveHistory = false;
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });

        },
        toViewMore: function (from, to, chat, cb) {
            var chatName = to;
            var No = 0;
            if (chat.msgList.length > 0) {
                No = chat.msgList[0].Id;
            }
            //console.log(chat.msgList);
            vm.getHistoryMsg(No, chatName, function (list) {
                var templist = [];
                angular.forEach(list, function (l) {
                    templist.push({
                        sender: l.speaker,
                        date: new Date(l.item_date).DateFormat('yyyy/MM/dd hh:mm:ss'),
                        msg: l.msg,
                        Id: l.Id
                    });
                });
                chat.msgList = templist.concat(chat.msgList);
                if (cb) {
                    cb();
                }
            });
        }
    };
    return vm;
})

.factory('SV_MessageGroupSetting', function ($http, SV_Company, $ionicLoading, SV_MessageGroup, SV_User) {
    var data = {
        group: {},
    };
    var vm = {
        GetData: function () {
            return data;
        },
        Refresh: function (Id) {
            data.group = SV_MessageGroup.GetGroup_ById(Id);
        },
        AddGroup: function (cb) {
            var info = {
                user: SV_User.GetUserName(),
                name: data.group.group_name
            }
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Group_Setting_AddGroup";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            SV_MessageGroup.RefreshData(function () {
                                if (cb) {
                                    cb(d.result);
                                }
                            });
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        ExitGroup: function (cb) {
            var info = {
                user: SV_User.GetUserName(),
                Id: data.group.Id
            }
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Group_Setting_ExitGroup";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            SV_MessageGroup.RefreshData(function () {
                                if (cb) {
                                    cb(d.result);
                                }
                            });
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        GetMembers: function (No, cb) {
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&No=" + No;
            var func = "/Message_Group_Setting_GetMombers";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        GetJoinUsers: function (cb) {
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&No=" + data.group.Id;
            var func = "/Message_Group_Setting_GetJoinUser";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            dd.Added = dd.Joined == "1" ? true : false;
                            dd.selected = false;
                        })
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        JoinToMombers: function (list, cb) {
            var info = {
                Id: data.group.Id,
                list: list
            }
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Group_Setting_JoinToMombers";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    };
    return vm;
})

.factory('SV_MessageGroupSearch', function ($http, SV_Company, $ionicLoading, SV_User, SV_MessageGroup) {
    var data = {
        list: [],
        search: '',
    };
    var vm = {
        GetData: function () { return data },
        Refresh: function (cb) {
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&user=" + SV_User.GetUserName() + "&search=" + data.search;
            var func = "/Message_Group_Search_GetGroupList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.list = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Join: function (group, cb) {
            var info = {
                user: SV_User.GetUserName(),
                Id: group.Id
            }
            $ionicLoading.show({ template: 'Loading...' });
            var pars = "&info=" + angular.toJson(info);
            var func = "/Message_Group_Search_Joinin";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            SV_MessageGroup.RefreshData(function () {
                                if (cb) {
                                    cb(d.result);
                                }
                            });
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    };
    return vm;
})



.factory('SV_Position', function ($ionicPopup, $ionicLoading, $http, SV_User, SV_DeviceData, SV_Company, SV_Firebase) {
    var data = {
        Longitude: '',
        Latitude: '',
        watchId: ''
    }
    var vm = {
        GetPosition: function (cb) {
            $ionicLoading.show({ template: 'Getting Location...' });
            navigator.geolocation.getCurrentPosition(function (position) {
                //$ionicPopup.alert({
                //    template: 'Latitude: ' + position.coords.latitude + '\n' +
                //      'Longitude: ' + position.coords.longitude + '\n' +
                //      'Altitude: ' + position.coords.altitude + '\n' +
                //      'Accuracy: ' + position.coords.accuracy + '\n' +
                //      'Altitude Accuracy: ' + position.coords.altitudeAccuracy + '\n' +
                //      'Heading: ' + position.coords.heading + '\n' +
                //      'Speed: ' + position.coords.speed + '\n' +
                //      'Timestamp: ' + new Date(position.timestamp) + '\n',
                //    title: 'Position'
                //});
                data.Longitude = position.coords.longitude;
                data.Latitude = position.coords.latitude;
                $ionicLoading.hide();
                if (cb) {
                    cb(data);
                }
            }, function onError(error) {
                $ionicLoading.hide();
                $ionicPopup.alert({
                    title: 'Get Location Time Out'
                });
                //cb(data);
            });//, { maximumAge: 3000, timeout: 5000, enableHighAccuracy: true });
            return data;
        },
        GetLocation_Json: function () {
            var temp = {
                Lat: data.Latitude,
                Lng: data.Longitude
            }
            return angular.toJson(temp);
        },
        GetData: function () {
            return data;
        },
        WatchPosition: function (cb) {
            data.watchId = navigator.geolocation.watchPosition(function (position) {
                data.Longitude = position.coords.longitude;
                data.Latitude = position.coords.latitude;
                //console.log("position", data);
                vm.UploadLocation(null, data.Latitude, data.Longitude);
                if (cb) {
                    cb(data);
                }
            }, function onError(error) {
                //$ionicPopup.alert({
                //    title: 'Get Location Time Out'
                //});
                console.log('Get Location Time Out');
            }, { timeout: 10 * 60 * 1000 });
        },
        WatchPosition_Clear: function (cb) {
            if (data.watchId != '') {
                navigator.geolocation.clearWatch(data.watchId);
                console.log('Position clear watch');
            }
            if (cb) {
                cb();
            }
        },
        UploadLocation: function (cb, lat, lng) {
            var user = SV_User.GetData();
            var device = SV_DeviceData.GetData();
            if (!lat || lat == '' || !lng || lng == '') {
                return;
            }
            if (user.user.isLogin) {
                var info = {
                    user: user.user.Name,
                    platform: device.platform,
                    deviceId: device.deviceId,
                    lat: lat,
                    lng: lng
                }
                var info_uri = angular.toJson(info);
                var pars = "&info=" + info_uri;
                var func = "/Location_Upload";
                $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                        .success(function (d) {
                            if (d.result == undefined || d.result == null) {
                                return;
                            }
                            if (d.result == "1") {
                                vm.system_msg_send('refresh', 'map', { from: user.user.Name });
                            }
                            if (cb) {
                                cb(d.result);
                            }
                        })
                        .error(function (data, status, headers, config) {
                            console.log('Error', data, status, config);
                        })
                        .finally(function () {
                        });
            }

        },
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        },

    };
    return vm;
})

.factory('SV_Map', function (SV_Company, $ionicLoading, $http) {
    var data = {
        list: [],
        date: new Date,
        callback: null
    };
    var vm = {
        GetData: function () {
            return data;
        },
        RefreshData: function (cb) {
            data.callback = cb;
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "";
            var func = "/Map_GetDataList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        var now = new Date();
                        angular.forEach(d.result, function (dd) {
                            dd.date = new Date(dd.date);
                            dd.IsOld = "1";
                            if (dd.date.dateDiff('n', now) < 30) {
                                dd.IsOld = "0";
                            }
                            dd.date = dd.date.DateFormat('yyyy/MM/dd hh:mm:ss');
                        })
                        data.list = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        RefreshData_noPopup: function (cb) {
            if (!data.callback) { return; }
            var pars = "";
            var func = "/Map_GetDataList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        var now = new Date();
                        angular.forEach(d.result, function (dd) {
                            dd.date = new Date(dd.date);
                            dd.IsOld = "1";
                            if (dd.date.dateDiff('n', now) < 30) {
                                dd.IsOld = "0";
                            }
                            dd.date = dd.date.DateFormat('yyyy/MM/dd hh:mm:ss');
                        })
                        data.list = d.result;
                        data.callback(d.result);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        system_msg_receive: function (msg) {
            if (msg.type == "command") {
                if (msg.content.target == "map" && msg.content.command == "refresh") {
                    var d = new Date();
                    //console.log(d, data.date, d.dateDiff('n', data.date));
                    if (d.dateDiff('n', data.date) < 0) {
                        data.date = d;
                        vm.RefreshData_noPopup();
                    }
                }
            }
        }
    };
    return vm;
})

.factory('SV_MapDriver', function (SV_Company, $ionicLoading, $http, SV_MasterData, SV_User) {
    var data = {
        list: [],
        driver: {
            data: [],
            current: '',
        },
        date: new Date,
        callback: null
    };
    var vm = {
        initData: function () {
            SV_MasterData.RefreshData_only('Driver');
            data.driver.data = SV_MasterData.GetData().driver;
            data.driver.current = SV_User.GetUserName();
        },
        GetData: function () {
            return data;
        },
        SetCallback: function (fun1) {
            if (fun1) {
                data.callback = fun1;
            } else {
                data.callback = null;
            }
        },
        RefreshData: function (cb) {
            //data.callback = cb;
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&user=" + data.driver.current;
            var func = "/MapDriver_GetCurrentLocation";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        var now = new Date();
                        angular.forEach(d.result, function (dd) {
                            dd.date = new Date(dd.date);
                            dd.IsOld = "1";
                            if (dd.date.dateDiff('n', now) < 30) {
                                dd.IsOld = "0";
                            }
                            dd.date = dd.date.DateFormat('yyyy/MM/dd hh:mm:ss');
                        })
                        data.list = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        RefreshData_noPopup: function (cb) {
            if (!data.callback) { return; }
            var pars = "&user=" + data.driver.current;
            var func = "/MapDriver_GetCurrentLocation";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        var now = new Date();
                        angular.forEach(d.result, function (dd) {
                            dd.date = new Date(dd.date);
                            dd.IsOld = "1";
                            if (dd.date.dateDiff('n', now) < 30) {
                                dd.IsOld = "0";
                            }
                            dd.date = dd.date.DateFormat('yyyy/MM/dd hh:mm:ss');
                        })
                        data.list = d.result;
                        data.callback(d.result);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        system_msg_receive: function (msg) {
            if (msg.type == "command") {
                if (msg.content.target == "map" && msg.content.command == "refresh") {
                    //console.log(msg.content.detail, SV_User.GetUserName());
                    if (msg.content.detail.from == data.driver.current) {
                        var d = new Date();
                        //console.log(d, data.date, d.dateDiff('n', data.date));
                        if (d.dateDiff('n', data.date) < 0) {
                            data.date = d;
                            vm.RefreshData_noPopup();
                        }
                    }
                }
            }
        }
    };
    return vm;
})



.factory('SV_InvoiceList', function (SV_Company, $ionicLoading, $http, SV_User, SV_Position) {
    var data = {
        list: [],
        search: '',
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function (cb) {
            if (data.search.length == 0) {
                return;
            }
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&search=" + data.search;
            var func = "/Invoice_GetDataList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.DocDate1 = new Date(dd.DocDate).DateFormat('yyyy/MM/dd');
                        });
                        data.list = d.result;
                        //console.log(data.list);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Save: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            par.User = SV_User.GetUserName();
            var pars = "&info=" + angular.toJson(par) + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Invoice_New_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(data.list);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    };
    return vm;
})

.factory('SV_InvoiceDetail', function (SV_Company, $ionicLoading, $http, SV_User, SV_Position) {
    var data = {
        job: {
            mast: {},
            det: [],
            log: [],
            attachment: []
        }
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function (No, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&No=" + No;
            var func = "/Invoice_Detail_GetData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.job.mast = d.result.mast;
                        data.job.mast.DocDate = new Date(data.job.mast.DocDate).DateFormat('yyyy-MM-dd');
                        angular.forEach(d.result.det, function (dd) {
                            dd.Qty = parseFloat(dd.Qty, 10);
                            dd.Price = parseFloat(dd.Price, 10);
                        })
                        data.job.det = d.result.det;
                        angular.forEach(d.result.log, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.log = d.result.log;
                        //data.job.attachment = d.result.attachment;
                        //console.log(data);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Refresh_Log: function (cb) {
            var pars = "&No=" + data.job.mast.DocNo;
            var func = "/Invoice_Detail_HisotryLog_GetList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.log = d.result;
                        //console.log(data);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });

        },
        Mast_Save: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(data.job.mast);
            var pars = "&info=" + info + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            //console.log(pars);
            var func = "/Invoice_Detail_Mast_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            vm.Refresh_Log();
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Item_GetNew: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var pars = "";
            //console.log(pars);
            var func = "/Invoice_Detail_Item_GetNew";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        d.result.DocId = data.job.mast.SequenceId;
                        d.result.DocNo = data.job.mast.DocNo;
                        d.result.DocType = data.job.mast.DocType;
                        d.result.DocLineNo = data.job.det.length + 1;
                        d.result.GstType = 'Z';
                        d.result.AcSource = 'CR';
                        d.result.ExRate = 1;
                        d.result.Qty = 1;
                        d.result.Price = 0;
                        d.result.Currency = data.job.mast.CurrencyId;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Item_Save: function (item, cb) {
            console.log(item);
            $ionicLoading.show({
                template: 'Save...'
            });
            var pars = "&info=" + angular.toJson(item) + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            var func = "/Invoice_Detail_Item_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            data.job.mast = d.result.context.mast;
                            data.job.mast.DocDate = new Date(data.job.mast.DocDate).DateFormat('yyyy-MM-dd');
                            angular.forEach(d.result.context.det, function (dd) {
                                dd.Qty = parseFloat(dd.Qty, 10);
                                dd.Price = parseFloat(dd.Price, 10);
                            })
                            data.job.det = d.result.context.det;
                            vm.Refresh_Log();
                        }
                        //console.log(data);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Attachment_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            var func = "/Invoice_Detail_Attachment_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            angular.forEach(d.result.context, function (dd) {
                                var c_datetime = new Date(dd.CreateDateTime);
                                //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                                dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                                dd.CreateTime = c_datetime.DateFormat('hh:mm');
                            })
                            data.job.log = d.result.context;
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
    };
    return vm;
})


.factory('SV_QuotationList', function (SV_Company, $ionicLoading, $http, SV_User, SV_Position) {
    var data = {
        list: [],
        search: '',
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function (cb) {
            if (data.search.length == 0) {
                return;
            }
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&search=" + data.search;
            var func = "/Quotation_GetDataList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.DocDate1 = new Date(dd.DocDate).DateFormat('yyyy/MM/dd');
                        });
                        data.list = d.result;
                        //console.log(data.list);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Save: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            par.User = SV_User.GetUserName();
            var pars = "&info=" + angular.toJson(par) + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Quotation_New_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(data.list);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    };
    return vm;
})

.factory('SV_QuotationDetail', function (SV_Company, $ionicLoading, $http, SV_User, SV_Position) {
    var data = {
        job: {
            mast: {},
            det: [],
            log: [],
            attachment: []
        }
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function (No, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&No=" + No;
            var func = "/Invoice_Detail_GetData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.job.mast = d.result.mast;
                        data.job.mast.DocDate = new Date(data.job.mast.DocDate).DateFormat('yyyy-MM-dd');
                        angular.forEach(d.result.det, function (dd) {
                            dd.Qty = parseFloat(dd.Qty, 10);
                            dd.Price = parseFloat(dd.Price, 10);
                        })
                        data.job.det = d.result.det;
                        angular.forEach(d.result.log, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.log = d.result.log;
                        //data.job.attachment = d.result.attachment;
                        //console.log(data);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Refresh_Log: function (cb) {
            var pars = "&No=" + data.job.mast.DocNo;
            var func = "/Invoice_Detail_HisotryLog_GetList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.log = d.result;
                        //console.log(data);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });

        },
        Mast_Save: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(data.job.mast);
            var pars = "&info=" + info + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            //console.log(pars);
            var func = "/Invoice_Detail_Mast_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            vm.Refresh_Log();
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Item_GetNew: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var pars = "";
            //console.log(pars);
            var func = "/Invoice_Detail_Item_GetNew";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        d.result.DocId = data.job.mast.SequenceId;
                        d.result.DocNo = data.job.mast.DocNo;
                        d.result.DocType = data.job.mast.DocType;
                        d.result.DocLineNo = data.job.det.length + 1;
                        d.result.GstType = 'Z';
                        d.result.AcSource = 'CR';
                        d.result.ExRate = 1;
                        d.result.Qty = 1;
                        d.result.Price = 0;
                        d.result.Currency = data.job.mast.CurrencyId;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Item_Save: function (item, cb) {
            console.log(item);
            $ionicLoading.show({
                template: 'Save...'
            });
            var pars = "&info=" + angular.toJson(item) + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            var func = "/Invoice_Detail_Item_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            data.job.mast = d.result.context.mast;
                            data.job.mast.DocDate = new Date(data.job.mast.DocDate).DateFormat('yyyy-MM-dd');
                            angular.forEach(d.result.context.det, function (dd) {
                                dd.Qty = parseFloat(dd.Qty, 10);
                                dd.Price = parseFloat(dd.Price, 10);
                            })
                            data.job.det = d.result.context.det;
                            vm.Refresh_Log();
                        }
                        //console.log(data);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Attachment_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            var func = "/Invoice_Detail_Attachment_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            angular.forEach(d.result.context, function (dd) {
                                var c_datetime = new Date(dd.CreateDateTime);
                                //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                                dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                                dd.CreateTime = c_datetime.DateFormat('hh:mm');
                            })
                            data.job.log = d.result.context;
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
    };
    return vm;
})


.factory('SV_PaymentList', function (SV_Company, $ionicLoading, $http, SV_User, SV_Position) {
    var data = {
        list: [],
        search: '',
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function (cb) {
            if (data.search.length == 0) {
                return;
            }
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&search=" + data.search;
            var func = "/Payment_GetDataList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        $.each(d.result, function (index, dd) {
                            dd.DocDate1 = new Date(dd.DocDate).DateFormat('yyyy/MM/dd');
                        });
                        data.list = d.result;
                        //console.log(data.list);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Save: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            par.User = SV_User.GetUserName();
            var pars = "&info=" + angular.toJson(par) + "&loc=" + SV_Position.GetLocation_Json();
            var func = "/Payment_New_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(data.list);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    };
    return vm;
})

.factory('SV_PaymentDetail', function (SV_Company, $ionicLoading, $http, SV_User, SV_Position) {
    var data = {
        job: {
            mast: {},
            det: [],
            log: [],
            attachment: []
        }
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function (No, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&No=" + No;
            var func = "/Invoice_Detail_GetData";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.job.mast = d.result.mast;
                        data.job.mast.DocDate = new Date(data.job.mast.DocDate).DateFormat('yyyy-MM-dd');
                        angular.forEach(d.result.det, function (dd) {
                            dd.Qty = parseFloat(dd.Qty, 10);
                            dd.Price = parseFloat(dd.Price, 10);
                        })
                        data.job.det = d.result.det;
                        angular.forEach(d.result.log, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.log = d.result.log;
                        //data.job.attachment = d.result.attachment;
                        //console.log(data);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Refresh_Log: function (cb) {
            var pars = "&No=" + data.job.mast.DocNo;
            var func = "/Invoice_Detail_HisotryLog_GetList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        angular.forEach(d.result, function (dd) {
                            var c_datetime = new Date(dd.CreateDateTime);
                            //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                            dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                            dd.CreateTime = c_datetime.DateFormat('hh:mm');
                        })
                        data.job.log = d.result;
                        //console.log(data);
                        if (cb) {
                            cb(data);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });

        },
        Mast_Save: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(data.job.mast);
            var pars = "&info=" + info + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            //console.log(pars);
            var func = "/Invoice_Detail_Mast_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            vm.Refresh_Log();
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Item_GetNew: function (cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var pars = "";
            //console.log(pars);
            var func = "/Invoice_Detail_Item_GetNew";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        d.result.DocId = data.job.mast.SequenceId;
                        d.result.DocNo = data.job.mast.DocNo;
                        d.result.DocType = data.job.mast.DocType;
                        d.result.DocLineNo = data.job.det.length + 1;
                        d.result.GstType = 'Z';
                        d.result.AcSource = 'CR';
                        d.result.ExRate = 1;
                        d.result.Qty = 1;
                        d.result.Price = 0;
                        d.result.Currency = data.job.mast.CurrencyId;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Item_Save: function (item, cb) {
            console.log(item);
            $ionicLoading.show({
                template: 'Save...'
            });
            var pars = "&info=" + angular.toJson(item) + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            var func = "/Invoice_Detail_Item_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            data.job.mast = d.result.context.mast;
                            data.job.mast.DocDate = new Date(data.job.mast.DocDate).DateFormat('yyyy-MM-dd');
                            angular.forEach(d.result.context.det, function (dd) {
                                dd.Qty = parseFloat(dd.Qty, 10);
                                dd.Price = parseFloat(dd.Price, 10);
                            })
                            data.job.det = d.result.context.det;
                            vm.Refresh_Log();
                        }
                        //console.log(data);
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Attachment_Add: function (par, cb) {
            $ionicLoading.show({
                template: 'Save...'
            });
            var info = angular.toJson(par);
            var pars = "&info=" + info + "&loc=" + SV_Position.GetLocation_Json() + "&user=" + SV_User.GetUserName();
            var func = "/Invoice_Detail_Attachment_Add";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            angular.forEach(d.result.context, function (dd) {
                                var c_datetime = new Date(dd.CreateDateTime);
                                //dd.CreateDateTime1 = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e() + ' ' + c_datetime.DateFormat('hh:mm');
                                dd.CreateDate = c_datetime.DateFormat('dd ') + c_datetime.getMonth_e();
                                dd.CreateTime = c_datetime.DateFormat('hh:mm');
                            })
                            data.job.log = d.result.context;
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
    };
    return vm;
})


.factory('SV_DriverList', function (SV_Company, $ionicLoading, $http, SV_MasterData, SV_Firebase) {
    var data = {
        driver: {},
        detail: {}
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function () {
            data.driver = SV_MasterData.GetData().driver;
            SV_MasterData.RefreshData();
        },
        RefreshDetail: function (No, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&No=" + No;
            var func = "/Setup_Driver_GetDateil";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.Id.length == 0) {
                            //==================== new
                            d.result.Id = 0;
                            d.result.Isstaff = 'Y';
                            d.result.ServiceLevel = 'Level1';
                            d.result.StatusCode = 'Active';
                        }
                        data.detail = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Save: function (cb) {
            var info = angular.toJson(data.detail);
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&info=" + info;
            var func = "/Setup_Driver_Detail_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            vm.system_msg_send('refresh', 'masterdata_driver_list', '');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });

        },
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                SV_MasterData.RefreshData_only('Driver');
            }
        }
    };
    return vm;
})

.factory('SV_VehicleList', function (SV_Company, $ionicLoading, $http, SV_MasterData, SV_Firebase) {
    var data = {
        towhead: {},
        detail: {}
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function () {
            data.towhead = SV_MasterData.GetData().towhead;
            SV_MasterData.RefreshData();
        },
        RefreshDetail: function (No, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&No=" + No;
            var func = "/Setup_Vehicle_GetDateil";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.Id.length == 0) {
                            //==================== new
                            d.result.Id = 0;
                            d.result.Type = 'towhead';
                        }
                        data.detail = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Save: function (cb) {
            var info = angular.toJson(data.detail);
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&info=" + info;
            var func = "/Setup_Vehicle_Detail_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            vm.system_msg_send('refresh', 'masterdata_towhead_list', '');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });

        },
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                SV_MasterData.RefreshData_only('Towhead');
            }
        }
    };
    return vm;
})

.factory('SV_TrackerList', function (SV_Company, $ionicLoading, $http, SV_MasterData, SV_Firebase) {
    var data = {
        trailer: {},
        detail: {}
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function () {
            data.trailer = SV_MasterData.GetData().trail;
            SV_MasterData.RefreshData();
        },
        RefreshDetail: function (No, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&No=" + No;
            var func = "/Setup_Vehicle_GetDateil";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.Id.length == 0) {
                            //==================== new
                            d.result.Id = 0;
                            d.result.Type = 'chessis';
                        }
                        data.detail = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
        Save: function (cb) {
            var info = angular.toJson(data.detail);
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&info=" + info;
            var func = "/Setup_Vehicle_Detail_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result == "1") {
                            vm.system_msg_send('refresh', 'masterdata_trailer_list', '');
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });

        },
        system_msg_send: function (par_command, par_target, par_detail) {
            var detail = par_detail ? par_detail : "";
            var company = SV_Company.GetCompanyCode();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: par_command,
                    target: par_target,
                    detail: detail,
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss')
                }
            };
            SV_Firebase.publish_system(msg);
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "refresh") {
                SV_MasterData.RefreshData_only('Trail');
            }
        }
    };
    return vm;
})


.factory('SV_ContainerSchedule', function ($http, SV_Company, $ionicLoading, SV_User) {

    var data = {
        schedule: []
    };
    var vm = {
        InitData: function () {
            data.schedule = [];
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Today'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'This week'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Later'
            });
            data.schedule.push({
                search: '',
                list: [],
                isFocus: false,
                name: 'Past'
            });
            vm.Refresh_ByName('Today');
            vm.Refresh_ByName('This week');
            vm.Refresh_ByName('Later');
            vm.Refresh_ByName('Past');
        },
        GetData: function () {
            return data;
        },
        GetData_ByName: function (name) {
            var re = null;
            for (var i = 0; i < data.schedule.length; i++) {
                if (data.schedule[i].name == name) {
                    re = data.schedule[i];
                    break;
                }
            }
            return re;
        },
        SetFocus: function (name) {
            angular.forEach(data.schedule, function (dd) {
                if (dd.name == name) {
                    dd.isFocus = true;
                } else {
                    dd.isFocus = false;
                }
            })
        },
        GetFocus: function () {
            var re = null;
            for (var i = 0; i < data.schedule.length; i++) {
                if (data.schedule[i].isFocus) {
                    re = data.schedule[i];
                    break;
                }
            }
            return re;
        },
        Refresh_ByName: function (name, cb) {
            var tab = vm.GetData_ByName(name);
            if (!tab) { return; }
            $ionicLoading.show({ template: 'Loading...' });
            var par = "&tab=" + tab.name + "&user=" + SV_User.GetUserName();
            var func = "/ContainerSchedule_GetList_ByTab";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        //console.log(d.result, tab);
                        if (tab.name == d.result.tab) {
                            angular.forEach(d.result.list, function (dd) {
                                dd.show_fromdate = new Date(dd.FromDate).DateFormat('MM/dd');
                                dd.show_todate = new Date(dd.ToDate).DateFormat('MM/dd');
                            });
                            tab.list = d.result.list;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });

        }
    };
    vm.InitData();
    return vm;
})


.factory('SV_StockDetail', function ($http, SV_Company, $ionicLoading, SV_User) {
    var data = {
        info: {}
    };
    var vm = {
        GetData: function () { return data },
        Refresh: function (par, cb) {
            $ionicLoading.show({ template: 'Loading...' });
            var par = "&No=" + par;
            var func = "/Stock_GetDetail";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.info = d.result;
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                        if (cb) {
                            cb();
                        }
                    });
        },
        Save: function (cb) {
            $ionicLoading.show({ template: 'Loading...' });
            var info = angular.toJson(data.info);
            var par = "&info=" + info;
            var func = "/Stock_Save";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + par)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });

        }
    }
    return vm;
})

.factory('SV_StockBalanceList', function (SV_Company, $ionicLoading, $http, SV_MasterData, SV_Firebase) {
    var data = {
        list: [],
        search: '',
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function (cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&search=" + data.search;
            var func = "/StockBalance_GetList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.list = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    };
    return vm;
})

.factory('SV_StockMovementList', function (SV_Company, $ionicLoading, $http, SV_MasterData, SV_Firebase) {
    var data = {
        list: [],
        search: '',
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function (cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&search=" + data.search;
            var func = "/StockMovement_GetList";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.list = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        }
    };
    return vm;
})




.factory('SV_CustomerList', function (SV_Company, $ionicLoading, $http, SV_MasterData, SV_Firebase) {
    var data = {
        party: {},
        detail: {}
    };
    var vm = {
        GetData: function () { return data; },
        Refresh: function () {
            data.party = SV_MasterData.GetData().party;
            SV_MasterData.RefreshData();
            console.log('===================', data);
        },
        RefreshDetail: function (No, cb) {
            $ionicLoading.show({
                template: 'Loading...'
            });
            var pars = "&No=" + No;
            var func = "/Setup_Party_GetDetail";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        data.detail = d.result;
                        if (cb) {
                            cb(d.result);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                        $ionicLoading.hide();
                    });
        },
    };
    return vm;
})


.factory('SV_Activity', function (SV_Company, $ionicLoading, $http, SV_User, SV_Firebase, SV_Hinter) {
    var data = {};
    var vm = {
        initData: function () {
            data.demo = {
                FileName: '',
                FilePath: '',
                JobType: '',
                RefNo: '',
                FileType: '',
                FileNote: '',
                CreateBy: '',
                Lat: '',
                Lng: '',
            }
        },
        GetDemo: function () {
            var re = angular.copy(data.demo);
            return re;
        },
        SendMsg: function (JobNo, Type) {
            var pars = "&JobNo=" + JobNo + "&Type=" + Type;
            var func = "/ActivityLog_GetData_ByJobNo";
            $http.jsonp(SV_Company.GetWebServiceUrl() + func + "?callback=JSON_CALLBACK" + pars)
                    .success(function (d) {
                        if (d.result == undefined || d.result == null) {
                            return;
                        }
                        if (d.result.status == "1") {
                            angular.forEach(d.result.context, function (dd) {
                                vm.system_msg_send(dd.JobNo, Type, dd.value, dd.Remark);
                            })
                        }
                    })
                    .error(function (data, status, headers, config) {
                        console.log('Error', data, status, config);
                    })
                    .finally(function () {
                    });
        },
        system_msg_receive: function (msg) {
            if (msg.content.command == "activity") {
                var detail = msg.content.detail;
                //console.log(detail);
                var user = SV_User.GetUserName();
                if (detail.from != user) {
                    if (detail.to == user) {
                        console.log('===================', detail);
                        var title = detail.no;
                        var remark = detail.remark;
                        var type = detail.type + "|" + detail.no;
                        SV_Hinter.Notice(title, remark, type);
                    }
                }
            }
        },
        system_msg_send: function (par_no, par_type, par_to, par_remark) {
            var company = SV_Company.GetCompanyCode();
            var user = SV_User.GetUserName();
            var msg = {
                type: "command",
                company: company,
                content: {
                    command: 'activity',
                    target: 'activity',
                    detail: {
                        from: user,
                        to: par_to,
                        no: par_no,
                        type: par_type,     //==========lcl,fcl
                        remark: par_remark
                    },
                    date: new Date().DateFormat('yyyy-MM-dd hh:mm:ss'),
                }
            };
            SV_Firebase.publish_system(msg);
        }

    }
    vm.initData();
    return vm;
})