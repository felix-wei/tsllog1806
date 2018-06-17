<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowMap.aspx.cs" Inherits="PagesContTrucking_GPSMonitor_ShowMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAbt48ngIOe6nclahJyhkYrhT2yXp_ZAY8_ufC3CFXhHIE1NvwkxRjIODH2HtqT34H-m3I2Am3ouHfUQ"
        type="text/javascript"></script>
    <script type="text/javascript">
        var map = null;
        var geocoder = null;
        var baseIcon = null;
        var jobs = null;
        var truckNo = "";
        var date = "";
        function load() {
            if (GBrowserIsCompatible()) {
                map = new GMap2(document.getElementById("map"));
                ////add control scale and zoom
                //map.addControl(new GSmallMapControl()); //大小button
                map.addControl(new GLargeMapControl());
                map.addControl(new GScaleControl());    //比例
                map.addControl(new GMapTypeControl());  //卫星混合地图
                //map.addMapType(G_PHYSICAL_MAP);         //地形button
                var crossLayer = new GTileLayer(new GCopyrightCollection(""), 0, 15);
                crossLayer.getTileUrl = function (tile, zoom) {
                    return "./include/tile_crosshairs.png";
                }


                var GpsAll = txt_Gps.GetText();
                if (GpsAll.length > 0) {
                    var points = [];
                    var Gps = GpsAll.split(";");
                    var centerP_temp = Gps[Gps.length - 1].split(",");
                    var centerP = new GLatLng(centerP_temp[0], centerP_temp[1]);
                    map.setCenter(centerP, 12);


                    for (var i = 0; i < Gps.length; i++) {
                        var temp = Gps[i].split(",");
                        var point = new GLatLng(temp[0], temp[1]);
                        points.push(point);
                        if (i == 0) {
                            map.addOverlay(createMarker('', point, 'Start:' + temp[2]));
                        }
                        else {
                            if (i == Gps.length - 1) {
                                map.addOverlay(createMarker('0', point, 'End:' + temp[2]));
                            }
                            else {
                                map.addOverlay(createMarker('0', point, '' + temp[2]));
                            }
                        }
                    }
                    map.addOverlay(new GPolyline(points, '#000', 3));
                }
                else {
                    alert("Have no Start GPS");
                    this.window.close();
                }
            }
        }
        function createMarker(tid, point, remark) {
            //转变图标
            var marker;
            if (remark.indexOf('End') > -1) {
                marker = new GMarker(point);
            }
            else {
                var icon = new GIcon();
                icon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";
                icon.iconSize = new GSize(12, 20);
                icon.shadowSize = new GSize(22, 20);
                icon.iconAnchor = new GPoint(6, 20);
                icon.infoWindowAnchor = new GPoint(5, 1);
                if (remark.indexOf('Start') > -1) {// == 'Start Position') {
                    icon.image = "http://maps.gstatic.com/mapfiles/ridefinder-images/mm_20_yellow.png";
                    marker = new GMarker(point, icon);
                }
                else {
                    icon.image = "http://maps.gstatic.com/mapfiles/ridefinder-images/mm_20_green.png";
                    marker = new GMarker(point, icon);
                }
            }

            //var marker = new GMarker(point);
            GEvent.addListener(marker, "click",
            function () {
                marker.openInfoWindowHtml("<b>" + remark + "</b>");
                map.panTo(point, 1000);
            });
            return marker;
        }

        function getGps() {
            var gps = navigator.geolocation;
            if (gps) {
                gps.getCurrentPosition(showgps, function (error) { alert("Got an error, code: " + error.code + " message: " + error.message); }, { maximumAge: 10000 });
            }
            else {
                showgps();
            }
        }
        function showgps(position) {
            if (position) {
                var latitude = position.coords.latitude;
                var longtitude = position.coords.longitude;
                alert("Latitude:" + latitude + "\nLongtitude:" + longtitude);
            }
            else {
                alert("position is null");
            }
        }
    </script>
</head>
<body onload="load()" onunload="GUnload()">
    <form id="form1" runat="server">
        <div style="display: none">
            <dxe:ASPxLabel ID="txt_Gps" ClientInstanceName="txt_Gps" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lb_DriverCode" ClientInstanceName="lb_DriverCode" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxButton ID="btn_getGps" runat="server" Text="Get Gps" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){getGps();}" />
            </dxe:ASPxButton>
            <dxe:ASPxLabel ID="lb_date1" ClientInstanceName="lb_date1" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lb_date2" ClientInstanceName="lb_date2" runat="server"></dxe:ASPxLabel>
        </div>
        <div id="map" style="width: 100%; height: 580px">
        </div>
    </form>
</body>
</html>
