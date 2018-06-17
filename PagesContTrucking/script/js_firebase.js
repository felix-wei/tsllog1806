function func_FireBase() {

    var data = {
        fb: null,
        server: {},
        lastTime: null,
        link: 'https://booxview.firebaseio.com/www_cargoerp_com',
        aSystem_callback_list:[],
    }
    var vm = {
        init: function () {
            //data.fb = new Firebase(data.link);
            var temp = SV_Company.GetDomain();
            if (temp && temp.length > 0) {
                console.log('firebase domain:' + temp);
                temp = 'https://booxview.firebaseio.com/' + temp;
                data.link = temp;
            }
            data.fb = new Firebase(data.link);
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
            vm.publish('&system', msg);
        },
        subscribe_system: function (JobList, Schedule, Map, Local_JobList, Local_Schedule, MasterData_Driver, MasterData_Towhead) {
            vm.subscribe('&system', function (msg) {
                console.log('&system', msg);
                for (var i = 0; i < data.aSystem_callback_list.length; i++) {
                    data.aSystem_callback_list[i].callback(msg);
                }
            });
            //console.log('====== msg ', msg, msg.content);
            //data.server.JobList = JobList;
            //data.server.Schedule = Schedule;
            //data.server.Local_JobList = Local_JobList;
            //data.server.Local_Schedule = Local_Schedule;
            //data.server.Map = Map;
            //data.server.MasterData_Driver = MasterData_Driver;
            //data.server.MasterData_Towhead = MasterData_Towhead;
            vm.subscribe(SV_Company.GetCompanyCode() + '&system', function (msg) {
                console.log('====== msg ', msg, msg.content);
                //if (msg.type == "command") {
                //    if (msg.content.target == "joblist") {
                //        //data.server.JobList.system_msg_receive(msg);
                //    }
                //    if (msg.content.target == "map") {
                //        data.server.Map.system_msg_receive(msg);
                //    }
                //    if (msg.content.target == "schedule") {
                //        data.server.Schedule.system_msg_receive(msg);
                //    }
                //    if (msg.content.target == "localjoblist") {
                //        //data.server.Local_JobList.system_msg_receive(msg);
                //        data.server.Local_Schedule.system_msg_receive(msg);
                //    }
                //    if (msg.content.target == "masterdata_driver_list") {
                //        data.server.MasterData_Driver.system_msg_receive(msg);
                //    }
                //    if (msg.content.target == "masterdata_towhead_list") {
                //        data.server.MasterData_Towhead.system_msg_receive(msg);
                //    }
                //}
            });
        },
        publish_system_msg_send: function (par_command, par_target, par_detail) {
            //SV_Firebase.publish_system_msg_send('refresh', 'schedule', JSON.stringify({ controller: "BULL", driver: "BULL", no: "90" }));
            var nowTime = new Date().DateFormat('hh:mm:ss');
            if (data.lastTime && data.lastTime == nowTime) {
                return;
            }
            data.lastTime = nowTime;
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
            console.log('====='+data.link, msg);
            vm.publish_system(msg);
        },
        add_aSystem_callback: function (par) {
            data.aSystem_callback_list.push(par);
        }
    };
    vm.init();
    return vm;
}


Date.prototype.DateFormat = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

var SV_Firebase = new func_FireBase();
SV_Firebase.subscribe_system();
