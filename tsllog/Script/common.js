$(document).ready(function () {

    // set layout
    layoutMain();

    tooltip();
    simpleTab("_main");

    $("#divLogoZone").load("/frames/zone_logo.aspx div#zone");
    $("#divUserZone").load("/frames/zone_user.aspx div#zone");
    $("#divFindZone").load("/frames/zone_find.aspx div#zone");
    $("#divMenuZone").load("/frames/zone_menu.aspx div#zone");

    initCommand();

    $('div.menuGroup').live('click', function () {
        toggleMenuGroup(this);
    });

    addSimpleTab("_main", "dashboard", "#DDDDFF", "/dashboard.aspx", "Dashboard", false);

    $('#search_button').live('click', function () {
        var typ = $("#search_type").val();
        var no = $("#search_no").val();
        switch (typ) {
            case "DN":
                exec("link", "/pageswarehouse/xw/receipt/ImpRecEdit.aspx?no=" + no, typ +  no, "DN:" + no, "#FFDB9D");
                break;
            case "IM":
                exec("link", "/pageswarehouse/xw/import/joborderedit.aspx?no=" + no, typ + no, "IMP:" + no, "#FFDB9D");
                break;
            case "EX":
                exec("link", "/pageswarehouse/xw/export/joborderedit.aspx?no=" + no, typ + no, "EXP:" + no, "#FFDB9D");
                break;
            case "CO":
                exec("link", "/pageswarehouse/xw/coload/joborderedit.aspx?no=" + no, typ + no, "COL:" + no, "#FFDB9D");
                break;
        }
        return false;
    });

    $('#new_button').live('click', function () {
        var typ = $("#search_type").val();
        var no = "New";
        switch (typ) {
            case "DN":
                exec("link", "/pageswarehouse/xw/receipt/ImpRecEdit.aspx?no=" + no, typ +  no, "DN:" + no, "#FEEFD0");
                break;
            case "IM":
                exec("link", "/pageswarehouse/xw/import/joborderedit.aspx?no=" + no, typ +  no, "IMP:" + no, "#FEEFD0");
                break;
            case "EX":
                exec("link", "/pageswarehouse/xw/export/joborderedit.aspx?no=" + no, typ +  no, "EXP:" + no, "#FEEFD0");
                break;
            case "CO":
                exec("link", "/pageswarehouse/xw/coload/joborderedit.aspx?no=" + no, typ +  no, "COL:" + no, "#FEEFD0");
                break;
        }
        return false;
    });


    $(window).resize(function () {
        layoutMain();
    });

});

function toggleMenuGroup(link) {
    var el = document.getElementById($(link).attr("id") + "-tree");
    if (el.style.display == 'none') {
        el.style.display = '';
    } else {
        el.style.display = 'none';
    }
}



var showMenu = 1;
function toggleMenu() {
    if (showMenu == 1) {
        $("#divMenu").hide();
        $("#aMenu").text(" >> ");
    } else {
        $("#divMenu").show();
        $("#aMenu").text(" << ");
    }
    showMenu = 1 - showMenu;
    layoutMain();
}

function layoutMain() {
    h = $(window).height();
    w = $(window).width();


    mw = 0;
    if (showMenu == 1)
        mw = 230;
    $("#main").css("height", h);
    $("#main").css("width", w);

    $("#divMenu").css("height", h - 20);
    $("#divMenu").css("width", 210);
    $("#divPage").css("height", h);
    $("#divPage").css("width", w - mw - 4);

    //$(".tabs").css("width", w - mw -30);
    $(".tabs").css("height", 30);
    $(".pages").css("width", w - mw - 4);
    $(".pages").css("height", h - 38);
}