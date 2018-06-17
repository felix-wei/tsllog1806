<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverView.aspx.cs" Inherits="PagesTpt_Job_DriverView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../../Script/jquery.js"></script>
    <script type="text/javascript">
        function AddTrip(jobNo, driver) {
            window.location = "DriverViewLog.aspx?no=" + jobNo + "&driver=" + driver + "&date1=" + search_From.GetText() + "&date2=" + search_To.GetText();
        }
        function PopupPhotoView(txt_JobNo) {
            popubCtr1.SetHeaderText('Attachment');
            popubCtr1.SetContentUrl('Attachments.aspx?Type=Tpt&JobNo=' + txt_JobNo);
            popubCtr1.Show();
        }

        function getGps() {
            var gps = navigator.geolocation;
            if (gps) {
                gps.getCurrentPosition(showgps, function (error) {
                    alert("Got an error, code: " + error.code + " message: " + error.message);
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
                var url = "../../../PagesContTrucking/GPSMonitor/gps.ashx?u=" + search_Driver.GetText() + "&lat=" + latitude + "&lng=" + longtitude;
                jQuery.ajax({
                    type: "POST",
                    url: url,
                    data: "",
                    cache: false
                });
                //document.getElementById("ifr").src = url;
                alert("Latitude:" + latitude + "\nLongtitude:" + longtitude);
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:right"><a href='../../../Frames/Signout.ashx' style="display:<%=user_Role%>">Exit</a></div>
        <div>
            <table>
                <tr>
                    <td>Driver</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_Driver" ClientInstanceName="search_Driver" Width="100px" runat="server" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Date From</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_From" ClientInstanceName="search_From" runat="server" Width="100px" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_To" ClientInstanceName="search_To" runat="server" Width="100px" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_getGps" runat="server" Text="Get Gps" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){getGps();}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <table style="border: 1px solid black; width: 600px">
                        <tr>
                            <td>JobNo:</td>
                            <td><%# Eval("JobNo") %></td>
                            <td>Status:</td>
                            <td><%# Eval("JobProgress") %></td>
                            <td>Date:</td>
                            <td><%# SafeValue.SafeDateStr(Eval("TptDate"))+" "+SafeValue.SafeString(Eval("TptTime")) %></td>
                        </tr>
                        <tr>
                            <td>Client:</td>
                            <td><%# Eval("Cust") %></td>
                            <td>PIC:</td>
                            <td><%# Eval("CustPic") %></td>
                        </tr>
                        <tr>
                            <td>Qty:</td>
                            <td><%# Eval("BkgQty") %>&nbsp;<%# Eval("BkgPkgType") %></td>
                            <td>Weight:</td>
                            <td><%# Eval("BkgWt") %></td>
                            <td>Volume:</td>
                            <td><%# Eval("BkgM3") %></td>
                        </tr>
                        <tr>
                            <td>From:</td>
                            <td colspan="5"><%# Eval("PickFrm1") %></td>
                        </tr>
                        <tr>
                            <td>To:</td>
                            <td colspan="5"><%# Eval("DeliveryTo1") %></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                            <td><a href="#" onclick='AddTrip("<%# Eval("JobNo") %>","<%# Eval("Driver") %>");'>Trip Log</a></td>
                            <td>
                                <a href='#' onclick='PopupPhotoView("<%# Eval("JobNo") %>");'>Attachments</a></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <dxpc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Attachments" AllowDragging="True" EnableAnimation="False" Height="550"
                AllowResize="True" Width="800" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
