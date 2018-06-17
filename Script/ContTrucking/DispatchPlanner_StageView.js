function DispatchPlanner_StageView(WebServiceUrl) {
    var webServiceUrl = WebServiceUrl;
    //function refresh_Data(par_date) {
    //    var search_date = par_date;
    //    if (search_date == null || search_date == "") {
    //        alert("Search Date Error");
    //        tc_layout_hid();
    //        return;
    //    }
    //    $.ajax({
    //        type: "post",
    //        contentType: "application/json",
    //        url: webServiceUrl + "/GetContainerByDate",
    //        data: '{ "date": "' + search_date + '"}',
    //        dataType: "json",
    //        success: function (result) {
    //            clear_Data();
    //            $.each(result.d, function (index, data) {
    //                var table_css = "datalist_table_container_orange";
    //                switch (data.StageStatus) {
    //                    case "Pending":
    //                        break;
    //                    case "Intrantsit":
    //                        table_css = "datalist_table_container_blue";
    //                        break;
    //                    case "Completed":
    //                        table_css = "datalist_table_container_green";
    //                        break;
    //                    case "Cancel":
    //                        table_css = "datalist_table_container_red";
    //                        break;
    //                    default: break;
    //                }
    //                var load_html = "";
    //                switch (data.Load) {
    //                    case "E":
    //                        load_html = "<span style='border:solid 2px black'>&nbsp;E&nbsp;</span>";
    //                        break;
    //                    case "L":
    //                        load_html = "<span style='border:solid 2px black'>&nbsp;L&nbsp;</span>";
    //                    default: break;
    //                }
    //                $("#td_stage_" + data.StageCode).append("<table class='" + table_css + "' onclick='popupOpen(\"" + data.ContainerNo + "\",\"" + data.JobNo + "\",\"" + data.Det2Id + "\");'><tr><td rowspan='3' style='vertical-align:middle;width:0px;font-size:35px;padding-right:5px;'>" + data.JobType + "</td></tr><tr><th>" + data.ContainerNo + "(" + data.ContainerType + ")</th></tr><tr><td>" + data.Driver + "&nbsp;/" + data.Trailer + "&nbsp;&nbsp;" + data.Time + "&nbsp;" + load_html + "</td></tr></table>");
    //            });
    //            tc_layout_hid();
    //        }
            
    //    });
    //}

    function refresh_Stage(par_date) {
        $.ajax({
            type: "post",
            contentType: "application/json",
            url: webServiceUrl + "/GetStage",
            data: '',
            dataType: "json",
            success: function (result) {
                clear_Data();
                if (result.d.length > 0) {
                    var addHtml = "<tr id='tr_stage_Title0'></tr><tr id='tr_stage_Title1'></tr><tr id='tr_stage_detail'></tr>";
                    $("#tb_stageview").append(addHtml);
                    for (var i = 0; i < result.d.length; i++) {
                        var title0 = result.d[i];
                        if (title0.ParId != 0) {
                            break;
                        }
                        addHtml = "<th id='th_stage_Title0_" + title0.Id + "'>" + title0.Stage + "</th>";
                        var colspan_temp = 1;
                        var IsEmpty = true;
                        $("#tr_stage_Title0").append(addHtml);
                        for (var j = i; j < result.d.length; j++) {
                            var title1 = result.d[j];
                            if (title1.ParId == title0.Id) {
                                document.getElementById("th_stage_Title0_" + title1.ParId).colSpan = colspan_temp++;
                                addHtml = "<th><div>" + title1.Stage + "</div></th>";
                                $("#tr_stage_Title1").append(addHtml);
                                var css_temp = "";
                                switch (title1.Stage) {
                                    case "Pending":
                                        css_temp = "td_background_orange";
                                        break;
                                    case "Intrantsit":
                                        css_temp = "td_background_blue"
                                        break;
                                    case "Completed":
                                        css_temp = "td_background_green";
                                        break;
                                    case "Cancel":
                                        css_temp = "td_background_red";
                                        break;
                                    default: break;
                                }
                                css_temp = " class='" + css_temp + "'";
                                addHtml = "<td id='td_stage_detail_" + title0.Stage + "_" + title1.Stage + "'" + css_temp + "></td>";
                                $("#tr_stage_detail").append(addHtml);
                                IsEmpty = false;
                            } else {
                                if (parseInt(title1.ParId) > parseInt(title0.Id)) {
                                    break;
                                }
                            }
                        }
                        if (IsEmpty) {
                            $("#tr_stage_Title1").append("<th><div></div></th>");
                            addHtml = "<td id='td_stage_detail_" + title0.Stage + "_'></td>";
                            $("#tr_stage_detail").append(addHtml);
                        }
                    }
                    refresh_datalist(par_date);
                    
                }
            }
        });
    }
    function refresh_datalist(par_date) {
        var search_date = par_date;
        if (search_date == null || search_date == "") {
            alert("Search Date Error");
            tc_layout_hid();
            return;
        }
        $.ajax({
            type: "post",
            contentType: "application/json",
            url: webServiceUrl + "/GetContainerByDate",
            data: '{ "date": "' + search_date + '"}',
            dataType: "json",
            success: function (result) {
                $.each(result.d, function (index, data) {
                    
                    var load_html = "";
                    switch (data.Load) {
                        case "E":
                            load_html = "<span style='border:solid 2px black'>&nbsp;E&nbsp;</span>";
                            break;
                        case "L":
                            load_html = "<span style='border:solid 2px black'>&nbsp;L&nbsp;</span>";
                        default: break;
                    }
                    $("#td_stage_detail_" + data.StageCode + "_" + data.StageStatus).append("<table onclick='popupOpen(\"" + data.ContainerNo + "\",\"" + data.JobNo + "\",\"" + data.Det2Id + "\");'><tr><td rowspan='3' style='vertical-align:middle;font-size:35px;padding:1px;'><div style='width:15px;overflow:hidden;padding:1px'>" + data.JobType + "</div></td></tr><tr><th>" + data.ContainerNo + "(" + data.ContainerType + ")</th></tr><tr><td>" + data.Driver + "&nbsp;/" + data.Trailer + "&nbsp;&nbsp;" + data.Time + "&nbsp;" + load_html + "</td></tr></table>");
                });
                tc_layout_hid();
            }

        });
    }

    function clear_Data() {
        $("#tb_stageview").empty();

        //$("#td_stage_Pending").empty();
        //$("#td_stage_Port").empty();
        //$("#td_stage_Park1").empty();
        //$("#td_stage_Warehouse").empty();
        //$("#td_stage_Park2").empty();
        //$("#td_stage_Yard").empty();
        //$("#td_stage_Completed").empty();
    }

    var dp = {
        refresh_Data: refresh_Stage,
        clear_Data: clear_Data
    };
    return dp;
}