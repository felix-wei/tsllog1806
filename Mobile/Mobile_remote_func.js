function MR_func1(){
    return {
        url: '192.168.1.102:82',
        from: 'Felix',
        company:'ZhaoHui'
    }
}
function MR_Login(par,callback) {
    var re = { value: par };
    if (callback) {
        callback(re);
    }
}