
function func_company() {
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
                    ServerUri: 'http://192.168.1.118:82/Mobile',
                    WebSiteUri: 'http://192.168.1.118:82'
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
        SetCompany: function (par) {
            vm.SetCompany_ByInput(par);
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
    };
    return vm;
}



var SV_Company = new func_company();
SV_Company.SetCompany('test');