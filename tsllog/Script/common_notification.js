
var commonNotication = null;
function notice(title, context, type) {
    notification_create(title, context, type);
}
function notification_create(title, context, type) {
    if (!commonNotication) {
        commonNotication = document.getElementById('common_notification');
    }
    var temp_div = document.createElement("div");

    var color = notification_getColor(type);
    temp_div.className = color;
    temp_div.setAttribute('style', 'display:none');

    var html = '';
    if (title && title.length > 0) {
        html += '<h2>' + title + '</h2>';
    }
    if (context && context.length > 0) {
        html += '<p>' + context + '</p>';
    }
    temp_div.innerHTML = html;
    commonNotication.appendChild(temp_div);

    notification_remove(temp_div, color);
}
function notification_getColor(type) {
    var res = 'blue';
    if (type) {
        switch (type.toLowerCase()) {
            case 'warn':
                res = 'red';
                break;
            case 'error':
                res = 'red';
                break;
            case 'success':
                res = 'green';
                break;
            case 'ok':
                res = 'green';
                break;
        }
    }
    return res;
}
function notification_remove(node, color) {
    var timeout = 3000;
    if (color) {
        switch (color) {
            case 'red':
                timeout: 4000;
                break;
        }
    }
    if (node) {
        $(node).show(300);
        setTimeout(function () {
            $(node).fadeOut(1000, function () { commonNotication.removeChild(node); });
        }, timeout);
    }
}

$(function () {
    var temp_div = document.createElement('div');
    temp_div.id = 'common_notification';
    //=========  notification, notification_rectangle
    temp_div.className = 'notification_dev';
    document.getElementsByTagName('body')[0].appendChild(temp_div);
})