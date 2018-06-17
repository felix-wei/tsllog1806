function DispatchPlanner_TimeLine(WebServiceUrl) {

    var webServiceUrl = WebServiceUrl;

    var driver_trips_sum = new Array();
    var hours = 24;
    var hour_width = 159 + 1;
    var isSingular = true;

    //var minuteBucket_length = 15;
    var minuteBucket_width = hour_width / 60;// hour_width * minuteBucket_length / 60;
    var margin_vertical = 5;
    var float_view_height = 16 + 2 * 2;
    var border_width = 2 * 2;
    var trip_Toast_width = 274 + 10 * 2 + 3 * 2;
    var now_scroll_Left = hour_width * 6;

    ////=======更新auto
    //var canRefreshAuto = false;
    //var refresh_DelayTime = 60 * 1000;
    //var refreshAuto_Time;

    function init_scroll_left(par1) {
        now_scroll_Left = par1;
    }


    //刷新数据(SearchDate)
    function Refresh_Data(par_date) {
        try {
            var temp_date = new Date(par_date);
            search_date = par_date;
        } catch (e) {
            alert("Dispatch Date is Error");
            tc_layout_hid();
            return;
        }
        if (search_date == null || search_date == "") {
            alert("Dispatch Date is Empty");
            tc_layout_hid();
            return;
        }
        $.ajax({
            type: "post",
            contentType: "application/json",
            url: webServiceUrl + "/GetDriverTrip_ByDate",
            data: '{ "date": "' + search_date + '"}',
            dataType: "json",
            success: function (d) {
                clear_Data();
                $.each(d.d, function (index, data) {
                    //alert(data.Driver);
                    if ($("#td_" + data.Driver).length <= 0) {
                        init_driverline(data.Driver, data.Towhead);
                    }
                    create_DriverTrip_View(data, search_date);
                });
                tc_layout_hid();
                //====================初始移到6：00的位置,两次定位准确
                document.body.scrollLeft = now_scroll_Left - 1;
                document.body.scrollLeft = now_scroll_Left;
                //====================different color according to the current time
                var date_now = new Date();
                var title_row_time = getElementPosition(document.getElementById("title_row_time"));
                var tb_timeline = getElementPosition(document.getElementById("tb_timeline"));
                $("#span_current_hour").css("left", (title_row_time.x + title_row_time.width + hour_width * date_now.getHours()) + "px").css("top", tb_timeline.y + "px").css("height", tb_timeline.height);
            }
        });
    }
    function clear_Data() {
        isSingular = true;
        try {
            $("#tb_timeline").empty();
            $("#tb_timeline_float_driver").empty();
            $("#view_group").empty();
            init_table_title();
        } catch (e) {
            alert("Lack Document");
        }
    }
    function init_table_title() {
        var th_title = "<th style='width:160px'></th>";
        for (var i = 0; i < hours; i++) {
            var temp_int = 100 + i;
            var temp_str = temp_int.toString().substr(1, 2) + ":00";
            th_title += "<th>" + temp_str + "</th>";
        }
        th_title += "<th></th>";
        $("#tb_timeline").append("<tr class='timeline_table_tr_th'>" + th_title + "</tr>");

        //======init time row
        th_title = "<th id='title_row_time'></th>";
        for (var i = 0; i <= hours; i++) {
            th_title += "<th></th>";
        }
        $("#tb_timeline").append("<tr>" + th_title + "</tr>");

        //=====init driver column
        var title_row_time = getElementPosition(document.getElementById("tb_timeline"));
        var tb_search = getElementPosition(document.getElementById("tb_search"));
        var tb_driver_title = '<tr class="timeline_table_tr_th"><th style="height:20px">#</th></tr><tr><th style="height:20px"></th></tr>';
        $("#tb_timeline_float_driver").append(tb_driver_title);
        $("#tb_timeline_float_driver").css("top", (title_row_time.y) + "px").css("left", (tb_search.x + 1) + "px");

        //=====init search table
        $("#tb_search").css("top", "0px");
    }
    function init_driverline(name, Towhead) {
        var singular_css = isSingular ? " class='timeline_table_tr0'" : "";
        isSingular = !isSingular;
        var td_line = "<td id='td_" + name + "'>&nbsp;" + name + "&nbsp;[" + Towhead + "]</td>";
        for (var i = 0; i <= hours; i++) {
            td_line += "<td></td>";
        }
        $("#tb_timeline").append("<tr" + singular_css + ">" + td_line + "</tr>");

        //=====add float driver
        $("#tb_timeline_float_driver").append("<tr" + singular_css + "><th id='tb_driver_th_" + name + "'>&nbsp;" + name + "&nbsp;[" + Towhead + "]</th></tr>");
        driver_trips_sum[name] = 0;
    }
    function create_DriverTrip_View(data, search_date) {
        if (data.JobNo == null || data.JobNo == "") {
            return;
        }
        var td_temp = getElementPosition(document.getElementById("td_" + data.Driver));
        if (td_temp != null) {
            var temp_rows_height = margin_vertical + float_view_height;//每一个trip独占一行，公用一行设为0；
            var temp_top = td_temp.y + margin_vertical + temp_rows_height * driver_trips_sum[data.Driver];
            var temp_left = td_temp.x + td_temp.width;
            var temp_width = 0;

            try {
                var fromdate = new Date(data.FromDate);
                var todate = new Date(data.ToDate);
                var searchdate = new Date(search_date);
                var ar_fromtime = data.FromTime.toString().split(":");
                var ar_totime = data.ToTime.toString().split(":");
                //====修改 left
                if (fromdate >= searchdate) {
                    temp_left += parseInt(ar_fromtime[0]) * hour_width;
                    temp_left += ar_fromtime[1] * minuteBucket_width; //parseInt(parseInt(ar_fromtime[1]) / minuteBucket_length) * minuteBucket_width;
                }
                //====修改 width
                temp_width = todate.getYear() - fromdate.getYear();
                temp_width = temp_width * 12 + todate.getMonth() - fromdate.getMonth();
                temp_width = temp_width * 30 + todate.getDay() - fromdate.getDay();
                if (temp_width == 0) {
                    temp_width = temp_width * 24 + parseInt(ar_totime[0]) - parseInt(ar_fromtime[0]);
                    temp_width = temp_width * 60 + parseInt(ar_totime[1]) - parseInt(ar_fromtime[1]);
                    temp_width = temp_width * minuteBucket_width - border_width;
                } else {
                    var begin_time = 0;
                    var end_time = 0;
                    if (searchdate.getDay() > fromdate.getDay()) {
                        begin_time = 0;
                    } else {
                        begin_time = parseInt(ar_fromtime[0]) * 60 + parseInt(ar_fromtime[1]);
                    }
                    if (todate.getDay() > searchdate.getDay()) {
                        end_time = 24 * 60;
                    } else {
                        end_time = parseInt(ar_totime[0]) * 60 + parseInt(ar_totime[1]);
                    }
                    temp_width = (end_time - begin_time) * minuteBucket_width - border_width;
                }
            } catch (e) { }

            $("#view_group").append('<span id="span_' + data.Id + '" class="timeline_view" onmouseover="dp_timeline.view_mouseover(\'' + data.FromTime + '\',\'' + data.ToTime + '\',\'' + temp_top + '\',\'' + temp_left + '\',\'' + temp_width + '\',\'' + data.Id + '\');" onmouseout="dp_timeline.view_mouseout();"  onclick="popupOpen(\'' + data.ContainerNo + '\',\'' + data.JobNo + '\',\'' + data.Id + '\');">' + data.ContainerNo + '</span>');
            $("#span_" + data.Id).css("top", temp_top + "px").css("left", temp_left + "px").css("width", temp_width + "px");
            $("#td_" + data.Driver).css("height", (td_temp.height + (driver_trips_sum[data.Driver] > 0 ? (temp_rows_height) : 0)) + "px");
            $("#tb_driver_th_" + data.Driver).css("height", (td_temp.height + (driver_trips_sum[data.Driver] > 0 ? (temp_rows_height) : 0)) + "px");
            driver_trips_sum[data.Driver] = driver_trips_sum[data.Driver] + 1;
        }
    }


    function refresh_trip_toast(v_Id) {
        $.ajax({
            type: "post",
            contentType: "application/json",
            url: webServiceUrl+"/GetDriverTripToast_ById",
            data: '{ "Id": "' + v_Id + '"}',
            dataType: "json",
            success: function (d) {
                if (d.d.Id == null) {
                    $("#span_trip_toast").empty().append("can not find it");
                    return;
                }
                var toast_innerHtml = "<table><tr><td>JobNo:</td><td>" + d.d.JobNo + "&nbsp;[" + d.d.JobType + "]</td></tr><tr><td>ContNo:</td><td>" + d.d.ContainerNo + "&nbsp;[" + d.d.ContainerSize + "]</td></tr><tr><td>Driver:</td><td>" + d.d.Driver + "&nbsp;[" + d.d.Towhead + "]</td></tr><tr><td>Stage:</td><td>" + d.d.StageCode + "&nbsp;[" + d.d.StageStatus + "]</td></tr><tr><td>Load:</td><td>" + d.d.LoadCode + "</td></tr><tr><td>FromDate:</td><td>" + d.d.FromDate + "&nbsp;" + d.d.FromTime + "</td></tr><tr><td>ToDate:</td><td>" + d.d.ToDate + "&nbsp;" + d.d.ToTime + "</td></tr></table>";
                $("#span_trip_toast").empty().append(toast_innerHtml);
            }
        });
    }
    function view_mouseover(fromtime, totime, v_top, v_left, v_width, v_Id) {
        //alert("From:" + fromtime + " To:" + totime);
        var title_row_time = getElementPosition(document.getElementById("title_row_time"));
        var dashed_height = v_top - title_row_time.y;
        $("#span_dashed1").css("display", "block").css("top", title_row_time.y + "px").css("left", v_left + "px").css("width", (parseInt(v_width) + border_width - 1) + "px").css("height", dashed_height + "px");
        $("#span_timeline_fromtime").append("<h style='color:green'>" + fromtime + "</h>").css("top", title_row_time.y + "px").css("left", v_left + "px");
        var v_right = parseInt(v_left) + parseInt(v_width) + border_width;
        if (v_left >= v_right) {
            $("#span_timeline_totime").empty();
        } else {
            $("#span_timeline_totime").append("<h style='color:green'>" + totime + "</h>").css("top", title_row_time.y + "px").css("left", (parseInt(v_left) + parseInt(v_width) + border_width) + "px");
        }
        var view = document.getElementById("span_" + v_Id);
        var scrollLeft = document.documentElement.scrollLeft + document.body.scrollLeft;
        var view_toast_left = scrollLeft + 5;
        if (view.offsetLeft - scrollLeft <= trip_Toast_width) {
            view_toast_left += document.body.clientWidth - trip_Toast_width - 10;
        }
        var view_toast_top = document.documentElement.scrollTop + document.body.scrollTop + 5;
        $("#span_trip_toast").css("top", view_toast_top+"px").css("left", view_toast_left + "px").css("display", "block");
        refresh_trip_toast(v_Id);
    }
    function view_mouseout() {
        $("#span_dashed1").css("display", "none");
        $("#span_timeline_fromtime").empty();
        $("#span_timeline_totime").empty();
        $("#span_trip_toast").css("display", "none");
    }


    
    //滚动条刷新，固定driver位置
    function scroll_refresh() {
        var scrollLeft = document.documentElement.scrollLeft + document.body.scrollLeft;
        var float_driver = document.getElementById("tb_timeline_float_driver");
        var float_search = document.getElementById("tb_search");
        var pos = scrollLeft - float_driver.offsetLeft;
        pos = pos / 5;
        var moveLeft = pos > 0 ? Math.ceil(pos) : Math.floor(pos);
        if (moveLeft != 0) {
            float_driver.style.left = (float_driver.offsetLeft + moveLeft) + "px";
            float_search.style.left = (float_driver.offsetLeft + moveLeft) + "px";
            setTimeout(scroll_refresh, 200);
        }
        now_scroll_Left = scrollLeft;
    }





    var dp = {

        Refresh_Data: Refresh_Data,
        Clear_Data: clear_Data,
        view_mouseover: view_mouseover,
        view_mouseout: view_mouseout,
        scroll_refresh: scroll_refresh,
        init_scroll_left: init_scroll_left

    };
    return dp;
}