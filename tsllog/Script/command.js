
function initCommand() {
    $('a.cmd').live('click', function () {
        // Get the tab name
        // linktype, link, id, title, color
        var cmd = $(this).attr("rel");
        var cmda = cmd.split('|');
        exec(cmda[0], cmda[1], cmda[2], cmda[3], cmda[4]);
    });

}

function exec(cmd,url,id,title,color) {
    var _grp = "_main";
    switch (cmd) {
        case "exit":
            if (confirm("Do you want to exit the system ?"))
                window.open("/frames/signout.ashx", "_parent");
            break;
        case "link":
            addSimpleTab(_grp, id, color, url, title, true);
            break;
        case "search":
            addSimpleTab(_grp, id, color, url, title, true);
            break;
        case "report":
            addSimpleTab(_grp, id, color, url, title, true);
            break;
        case "external":
            window.open(url, "_blank");
            break;
    }
}
