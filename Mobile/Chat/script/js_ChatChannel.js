function Ctrl_ChatChannel($scope, $http, $q, $timeout) {

    $scope.vm = {
        list: [],
    }
    $scope.action = {
        GoBack: function () {
            console.log('in ctrl');
            window.history.go(-1);
        },
        scroll_bottom: function (times) {
            $timeout(function () {
                console.log('in scroll bottom', times);
                document.getElementById('scroll').scrollTop = document.getElementById('scroll').scrollHeight;
                if (times) {
                    $scope.action.scroll_bottom(times - 1);
                }
            }, 100);

        },
        scroll_top: function (times) {
            //document.getElementById('scroll').scrollTop = 0;

            $timeout(function () {
                console.log('in scroll top', times);
                document.getElementById('scroll').scrollTop = 0;
                if (times) {
                    $scope.action.scroll_top(times - 1);
                }
            }, 100);
        },
        init: function () {
            sv.addsubscribe_chat();
            $scope.vm.user = lb_user.GetText();
            $scope.vm.chat = lb_chat.GetText();
            var chatName = sv.getChatName($scope.vm.user, $scope.vm.chat);
            $scope.vm.chatName = chatName;
            $scope.vm.haveHistory = true;
            sv.receiveMsg_toServer_All(chatName, function (par) {
                //chat.noRead = 0;
                $scope.action.scroll_bottom();
            });
            if ($scope.vm.list.length == 0) {
                sv.getHistoryMsg(0, chatName, function (list) {
                    angular.forEach(list, function (l) {
                        $scope.vm.list.push({
                            sender: l.speaker,
                            date: new Date(l.item_date).DateFormat('yyyy/MM/dd hh:mm:ss'),
                            msg: l.msg,
                            Id: l.Id
                        });
                    });
                    console.log($scope.vm.list, list);
                });
            }
        },
        sendMsg: function () {
            var msg = $scope.vm.msg_text;
            if (!msg || msg.length == 0) {
                return;
            }
            sv.sendMsg($scope.vm.user, $scope.vm.chat, msg);
        },
        send_callback: function () {
            $scope.vm.msg_text = '';
        },
        viewMore: function () {
            sv.toViewMore($scope.vm.user, $scope.vm.chat, $scope.action.viewMore_callback);
        },
        viewMore_callback: function () {
            //$scope.action.scroll_top();
        },
        isOwn: function (row) {
            return row.sender == $scope.vm.user;
        }
    }

    var sv = {
        data: {
            isInit: true,
        },
        getHistoryMsg: function (No, chat, cb) {
            var info = {
                No: No,
                chat: json2str_replace(chat),
                to: $scope.vm.user
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
                            $scope.vm.haveHistory = false;
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
        receiveMsg_toServer: function (content, cb) {
            if (!content.Id || content.Id == '' || content.Id == '0') { return; }
            if (content.from == $scope.vm.user) { return; }
            var info = {
                Id: content.Id,
                //chat: json2str_replace(vm.getChatName(content.from, content.to)),
                from: content.from,
                to: $scope.vm.user
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
                to: $scope.vm.user
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
        addsubscribe_chat: function () {
            SV_Firebase.subscribe(SV_Company.GetCompanyCode() + '&Chat', sv.addsubscribe_callback, null);
        },
        addsubscribe_callback: function (msg, par) {
            console.log(msg, msg.content.date, msg.content.text);
            if (sv.data.isInit) {
                sv.data.isInit = false;
                return;
            }
            if (msg.type == 'chat') {
                var content = msg.content;
                if (sv.getChatName(content.from, content.to) == $scope.vm.chatName) {
                    $scope.vm.list.push({
                        sender: content.from,
                        date: content.date,
                        msg: content.text,
                        Id: content.Id
                    });
                    //console.log($scope.vm.list);
                    $scope.action.scroll_bottom(1);
                    sv.receiveMsg_toServer(content);
                }
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
            sv.sendMsg_ToServer(msg.content, function (re) {
                msg.content.Id = re;
                SV_Firebase.publish(SV_Company.GetCompanyCode() + '&Chat', msg);
                $scope.action.send_callback();
            });
        },
        sendMsg_ToServer: function (content, cb) {
            var info = {
                chat: json2str_replace($scope.vm.chatName),
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
        toViewMore: function (from, to, cb) {
            var chatName = $scope.vm.chatName;
            var No = 0;
            if ($scope.vm.list.length > 0) {
                No = $scope.vm.list[0].Id;
            }
            sv.getHistoryMsg(No, chatName, function (list) {
                var templist = [];
                angular.forEach(list, function (l) {
                    templist.push({
                        sender: l.speaker,
                        date: new Date(l.item_date).DateFormat('yyyy/MM/dd hh:mm:ss'),
                        msg: l.msg,
                        Id: l.Id
                    });
                });
                $scope.vm.list = templist.concat($scope.vm.list);
                if (cb) {
                    cb();
                }
            });
        }
    }
    angular.element(document).ready(function () {
        $scope.action.init();
    });
}


function json2str_replace(s) {
    var r = s;
    r = r.toString().replace('&', '!AND!');
    r = r.toString().replace('?', '!WEN!');
    r = r.toString().replace('#', '!JING!');
    r = r.toString().replace('\'', '!DYIN!');
    r = r.toString().replace('-', '!HEN!');
    return r;
}