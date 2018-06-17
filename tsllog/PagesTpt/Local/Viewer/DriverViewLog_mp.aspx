<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverViewLog_mp.aspx.cs" Inherits="PagesTpt_Local_Viewer_DriverViewLog_mp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../../Script/jquery.js"></script>
    <script type="text/javascript">
        function getQueryString(name, url) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            //var r = window.location.search.substr(1).match(reg);
            var r = url.match(reg);
            if (r != null) return unescape(r[2]);
            return null;
        }
        function bind_func() {
            $("#btn_AddLog").bind("click", function () { $("#div_add").css("display", "block"); });
            $("#btn_cancel").bind("click", function () { $("#div_add").css("display", "none"); });
            $("#btn_save").bind("click", function () { uploadLog(); });
            $("#btn_GoBack").bind("click", function () { window.location = "DriverView_mp.aspx"; });
        }
        function uploadLog() {
            var url = "DriverViewLog_mp_Data.aspx";
            var jobNo = $("#txt_JobNo").val();
            if (jobNo == null || jobNo == "") {
                alert("Request JobNo");
                return;
            }
            var driver = $("#txt_Driver").val();
            if (driver == null || jobNo == "") {
                alert("Request Driver");
                return;
            }
            var status = $("#select_JobStatus").val();
            var logDateTime = $("#date_TptDate").val();
            if (logDateTime == null || logDateTime == "") {
                alert("Request Date Time");
                return;
            }
            var logDate = logDateTime.substr(0, 10);
            var logTime = logDateTime.substr(11, 5);
            var remark = $("#txt_remark").val();
            //alert(jobNo + "\n" + driver + "\n" + status + "\n" + logDate + "\n" + logTime + "\n" + remark);
            jQuery.ajax({
                type: "POST",
                url: url,
                data: {
                    Type: "UploadLog",
                    JobNo: jobNo,
                    Driver: driver,
                    Status: status,
                    LogDate: logDate,
                    LogTime: logTime,
                    Remark:remark
                },
                cache: false,
                success: function (data) {
                    alert(data);
                }
            });
            getLog();
            $("#div_add").css("display", "none");
        }
        function getLog() {
            var url = "DriverViewLog_mp_Data.aspx";
            jQuery.ajax({
                type: "POST",
                url: url,
                data: { Type: "GetLog", JobNo: $("#txt_JobNo").val(), Driver: $("#txt_Driver").val() },
                cache: false,
                success: function (data) {
                    var ar = data.toString().split("@;@");
                    $("#table_detail").children().remove();
                    for (var i = 0; i < ar.length; i++) {
                        if (i == 0 && ar[i].toString() == "") return;
                        var ar1 = ar[i].toString().split("@,@");
                        $("#table_detail").append("<tr><td><table style='border: 1px solid black; width: 600px'><tr><td style='width: 60px'>Status:</td><td style='width:130px'>" + ar1[0] + "</td><td style='width: 60px'>Date:</td><td>" + ar1[1] + "</td></tr><tr><td>Remark:</td><td colspan='3'>"+ar1[2]+"</td></tr></table></td></tr>");
                    }
                }
            });
        }

        $(document).ready(function () {
            var url = location.search.substr(1);
            $("#txt_JobNo").val(getQueryString("no", url));
            $("#txt_Driver").val(getQueryString("driver", url));
            $("#search_date1").val(getQueryString("date1", url));
            $("#search_date2").val(getQueryString("date2", url));
            bind_func();
            getLog();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none">
            <input id="search_date1" type="text" />
            <input id="search_date2" type="text" />
        </div>
        <div>
            <table>
                <tr>
                    <td>JobNo:</td>
                    <td>
                        <input id="txt_JobNo" type="text" readonly="true" style="width: 100px" /></td>
                    <td>Driver:</td>
                    <td>
                        <input id="txt_Driver" type="text" readonly="true" style="width: 100px" /></td>
                    <td>
                        <input type="button" id="btn_AddLog" style="width: 80px" value="Add&nbsp;Log" /></td>
                    <td>
                        <input type="button" id="btn_GoBack" style="width: 80px" value="Go&nbsp;Back" /></td>
                </tr>
            </table>
            <div id="div_add" style="display: none">
                <table style="width: 600px">
                    <tr>
                        <td>Status:</td>
                        <td>
                            <select id="select_JobStatus" style="width: 100px">
                                <option>Confirmed</option>
                                <option>Picked</option>
                                <option>Delivered</option>
                                <option>Completed</option>
                                <option>Canceled</option>
                            </select>
                        </td>
                        <td>Date Time:</td>
                        <td>
                            <input type="datetime-local" id="date_TptDate" />
                        </td>
                    </tr>
                    <tr>
                        <td>Remark:</td>
                        <td colspan="5">
                            <textarea id="txt_remark" rows="4" style="width: 100%"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                        <td style="text-align: right">
                            <input id="btn_save" type="button" value="Save" style="width: 80px" />
                        </td>
                        <td>
                            <input id="btn_cancel" type="button" value="Cancel" style="width: 80px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_detail">
                <table id="table_detail">
                    
                </table>
            </div>
        </div>
    </form>
</body>
</html>
