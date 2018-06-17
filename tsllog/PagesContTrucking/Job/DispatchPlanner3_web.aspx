<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner3_web.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner3_web" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Style/ConTrucking_planner.css" rel="stylesheet" />
    <script src="../../Script/jquery.js"></script>
    <script src="../../Script/ContTrucking/DispatchPlanner.js"></script>
    <script src="../../Script/ContTrucking/DispatchPlanner_TimeLine.js"></script>
    <script type="text/javascript">
        $(function () {
            date_searchDate.SetDate(new Date());
            dp_timeline = new DispatchPlanner_TimeLine('DispatchPlanner3_ws.asmx');
            dp_timeline.Refresh_Data(date_searchDate.GetText());
            window.onscroll = dp_timeline.scroll_refresh;
            myTime = setTimeout(TimeOut_RefreshData, delayTime);
        });
        var dp_timeline;
        var myTime;
        var delayTime = 60 * 1000;
        function popupOpen(contNo, JobNo, Det2Id) {
            clearTimeout(myTime);
            PopupChangeStage(contNo, JobNo, Det2Id);
        }
        function popupClose_RefreshData() {
            tc_layout_show();
            dp_timeline.Refresh_Data(date_searchDate.GetText());
            myTime = setTimeout(TimeOut_RefreshData, delayTime);
        }
        function TimeOut_RefreshData() {
            tc_layout_show();
            dp_timeline.Refresh_Data(date_searchDate.GetText());
            myTime = setTimeout(TimeOut_RefreshData, delayTime);
        }
        function Refresh_Date_By_Date() {
            dp_timeline.Refresh_Data(date_searchDate.GetText());
            tc_layout_show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div_tc" class="tc_layout">
            <img src="../../Images/loading_20140418.jpg" />
        </div>
        <div>
            <table id="tb_search" class="timeline_table_search">
                <tr>
                    <td>Dispatch Date:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_searchDate" ClientInstanceName="date_searchDate" runat="server" CalendarProperties-ShowClearButton="false" EditFormatString="yyyy/MM/dd"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Refresh" ClientInstanceName="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){}" />
                        </dxe:ASPxButton>

                    </td>
                </tr>
            </table>
            <table id="tb_timeline" class="timeline_table">
            </table>
            <div id="driver_group">
                <table id="tb_timeline_float_driver" class="timeline_table_driver">
                </table>
            </div>
            <div id="view_group"></div>
            <div>
                <span id="span_dashed1" class="timeline_view_dashed"></span>
                <span id="span_timeline_fromtime" class="timeline_view_timeline"></span>
                <span id="span_timeline_totime" class="timeline_view_timeline"></span>
                <span id="span_trip_toast" class="timeline_trip_Toast"></span>
                <span id="span_current_hour" class="timeline_current_hour"></span>
            </div>
        </div>

        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Planner2" AllowDragging="True" EnableAnimation="False" Height="550"
            Width="1100" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {popupClose_RefreshData();}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
