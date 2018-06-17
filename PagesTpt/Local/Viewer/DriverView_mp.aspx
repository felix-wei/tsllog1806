<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverView_mp.aspx.cs" Inherits="PagesTpt_Local_Viewer_DriverView_mp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../../Script/jquery.js"></script>
    <style type="text/css">
        .div_Popup {
            position: fixed;
            _position: absolute;
            zoom: 1;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            overflow: hidden;
            background: url(/Images/hei.png) repeat;
            z-index: 99900;
            text-align: center;
        }
        .if_View {
            text-align:center;
            width:600px;
            height:500px;
            background-color:white;
        }
        .div_back {
            margin-left:600px;
        }
    </style>
    <script type="text/javascript">
        function get_data() {
            var driver = $("#search_driver").val();
            if (driver == null || driver == "") {
                alert("Request Driver");
                return;
            }
            var date1 = $("#search_date1").val();
            var date2 = $("#search_date2").val();
            if (date1 == null || date1 == "") {
                alert("Request Date from");
                return;
            }
            if (date2 == null || date2 == "") {
                alert("Request Date to");
                return;
            }
            var htmlobj = jQuery.ajax({ url: "DriverView_mp_Data.aspx?driver=" + driver + "&date1=" + date1 + "&date2=" + date2, async: false });
            $("#detail").html(htmlobj.responseText);
        }
        function AddTrip(jobNo, driver) {
            var date1 = $("#search_date1").val();
            var date2 = $("#search_date2").val();
            if (date1 == null || date1 == "") {
                alert("Request Date from");
                return;
            }
            if (date2 == null || date2 == "") {
                alert("Request Date to");
                return;
            }
            window.location = "DriverViewLog_mp.aspx?no=" + jobNo + "&driver=" + driver + "&date1=" + date1 + "&date2=" + date2;
        }
        function PopupPhotoView(jobno) {
            $("#div_Attachments").css("display", "");
            $("#if_View").attr("src", "Attachments.aspx?Type=Tpt&JobNo=" + jobno);
        }

        function getGps() {
            var gps = navigator.geolocation;
            if (gps) {
                gps.getCurrentPosition(showgps, function (error) {
                    alert("Got an error when get GPS,\n code: " + error.code + " message: " + error.message);
                    onStartGet();
                }, { maximumAge: 10000 });
            }
            else {
                showgps();
            }
        }
        function showgps(position) {
            if (position) {
                var latitude = position.coords.latitude;
                var longtitude = position.coords.longitude;
                var url = "../../../PagesContTrucking/GPSMonitor/gps.ashx?u=" + $("#search_driver").val() + "&lat=" + latitude + "&lng=" + longtitude;
                jQuery.ajax({
                    type: "POST",
                    url: url,
                    data: "",
                    cache: false
                });
                //document.getElementById("ifr").src = url;
                //alert("Latitude:" + latitude + "\nLongtitude:" + longtitude);
            }
            else {
                alert("position is null");
                window.clearTimeout(timeTemp);
            }
        }


        var timeTemp;
        function onStartGet() {
            timeTemp = setTimeout(getGps, 0);
        }

        function getSearchData() {

            var temp = jQuery.ajax({ url: "DriverView_mp_SearchData.aspx?Type=Driver", async: false });
            //alert(temp.responseText);
            var ar = temp.responseText.split("@#@");
            for (var i = 0; i < ar.length; i++) {
                $("#search_driver").append("<option>" + ar[i] + "</option>");
            }
            var temp1 = jQuery.ajax({ url: "DriverView_mp_SearchData.aspx?Type=Role", async: false });
            if (temp1.responseText == "Driver") {
                $("#div_exit").append('<input type="button" onclick="window.location = \'../../../Frames/Signout.ashx\';" value="Exit" style="width:60px" />');
            }
        }

        function bind_func() {
            $("#btn_popDown").bind("click", function () { $("#div_Attachments").css("display", "none"); });
            $("#btn_search").bind("click", function () { get_data(); });
        }

        $(document).ready(function () {
            //$("#search_date1").val("2014/01/10");
            getSearchData();
            getGps();
            bind_func();
        });
    </script>
</head>
<body>
    <table>
        <tr>
            <td>Driver:</td>
            <td>
                <select id="search_driver" style="width: 100px"></select></td>
            <td>Date&nbsp;From:</td>
            <td>
                <input type="date" id="search_date1" /></td>
            <td>To:</td>
            <td>
                <input type="date" id="search_date2" /></td>
            <td>
                <input type="button" id="btn_search" value="Retrieve" style="width: 60px" /></td>
            <td id="div_exit"></td>
            <%--<td>
                <input type="button" id="btn_getGPS" onclick="getGps();" value="Get&nbsp;GPS" /></td>--%>
        </tr>
    </table>
    <div id="detail"></div>
    <div id="div_Attachments" class="div_Popup" style="display: none">
        <div class="div_back">
            <input id="btn_popDown" type="image" src="/Images/close.png" value="缩小" /><%--/Icons/Cancel_NotActive.gif--%>
        </div>
        <iframe id="if_View" class="if_View" ></iframe>
        
    </div>
</body>
</html>
