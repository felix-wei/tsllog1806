<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintApplicationForLeave.aspx.cs" Inherits="PagesHr_Report_PrintApplicationForLeave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <style media="print">
        .Noprint {
            display: none;
        }

        .PageNext {
            page-break-after: always;
        }
    </style>
    <style type="text/css">
        .table_A4 {
            width: 210mm;
            background-color: white;
            height: auto;
            font-family: Arial;
            font-size: 14px;
            vertical-align: bottom;
        }


        .table_A4_border {
            border: solid 1px #000000;
            border-spacing: 0px;
            width: 210mm;
            background-color: white;
            height: auto;
            font-family: Arial;
            font-size: 12px;
        }

        .td_bottom_right {
            border-bottom: solid 1px #000000;
            border-right: solid 1px #000000;
        }

        .td_bottom {
            border-bottom: solid 1px #000000;
        }

        .td_right {
            border-right: solid 1px #000000;
        }

        .NOPRINT {
            font-family: "宋体";
            font-size: 9pt;
        }

        .table_A4 tr td {
            vertical-align: bottom;
        }

        .table_A4.font {
            font-size: 15px;
            font-family: Arial;
        }

        .table_A5 {
            width: 210mm;
            border: 1px solid #000;
        }

        table_A5_bottom {
            height: 40px;
            border-bottom: 1px solid #000;
        }

        .table_A5_right {
            border-right: 1px solid #000;
        }

        .table_A4_bottom {
            border-bottom: solid 1px #000000;
            margin-right: 20px;
        }
    </style>
    <script type="text/javascript">
        function doPrint() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            pagesetup_null();
            window.print();

        }
    </script>
    <script type="text/javascript">
        var hkey_root, hkey_path, hkey_key
        hkey_root = "HKEY_CURRENT_USER"
        hkey_path = "//Software//Microsoft//Internet Explorer//PageSetup//"
        //设置网页打印的页眉页脚为空
        function pagesetup_null() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell");
                hkey_key = "header";
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "");
                hkey_key = "footer";

                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "");
            } catch (e) { }
        }
        //设置网页打印的页眉页脚为默认值
        function pagesetup_default() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "footer";
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&u&b&d");
                hkey_key = "footer";
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&u&b&d")
            } catch (e) { }
        }
    </script>

</head>
<body onload="doPrint();">
    <form id="form1" runat="server">
        <!--startprint-->
        <table class="table_A4" style="margin-top: 50px;">
            <tr style="text-align: center">
                <td>
                    <font size="6"><%=System.Configuration.ConfigurationManager.AppSettings["CompanyName"] %></font>
                </td>

            </tr>
            <tr style="text-align: center">
                <td>
                    <font size="4">APPLICATION FOR LEAVE</font>
                </td>
            </tr>
            <tr>
                <td style="height: 120px"></td>
            </tr>
        </table>
        <%
            DataTable tab = GetData();
            for (int i = 0; i < tab.Rows.Count; i++)
            {
        %>
        <table class="table_A4 font">
            <tr>
                <td><b>FISCAL YEAR:</b></td>
                <td class="table_A4_bottom" width="100px" colspan="2"><%=SafeValue.SafeDate(tab.Rows[i]["ApplyDateTime"],DateTime.Today).Year %></td>
                <td width="180px"><b>DATE OF APPLICATION:</b></td>
                <td class="table_A4_bottom"><%=SafeValue.SafeDate(tab.Rows[i]["ApplyDateTime"],DateTime.Today) %></td>
            </tr>
            <tr style="height:30px;">
                <td><b>NAME OF STAFF:</b></td>
                <td class="table_A4_bottom" colspan="4">
                <%=SafeValue.SafeString(tab.Rows[i]["Name"]) %>
            </tr>
            <tr style="height:30px;">
                <td width="150px"><b>NATURE OF LEAVE:</b></td>
                <td width="160px;" colspan="4">
                    <table cellpadding="0" cellspacing="0" width="100%" style="height: 41px">
                        <tr>
                            <td class="table_A4_bottom" width="45%"> <%=SafeValue.SafeString(tab.Rows[i]["LeaveType"]) %></td>
                            <td><b>SECTION:</b>
                            </td>
                            <td class="table_A4_bottom" width="45%"> <%=SafeValue.SafeString(tab.Rows[i]["Department"]) %></td>
                        </tr>
                    </table>
                </td>

            </tr>
            <tr style="height:30px;">
                <td><b>NUMBER OF DAYS:</b>
                </td>
                <td colspan="4">
                    <table cellpadding="0" cellspacing="0" width="100%" style="height:30px;">
                        <tr>
                            <td class="table_A4_bottom" width="120px"> <%=SafeValue.SafeString(tab.Rows[i]["Days"]) %></td>
                            <td width="60px"><b>FROM:</b></td>
                            <td class="table_A4_bottom"> <%=SafeValue.SafeDate(tab.Rows[i]["Date1"],DateTime.Today).ToString("dd/MM/yyyy") %>&nbsp;&nbsp; <%=SafeValue.SafeString(tab.Rows[i]["Time1"]) %> </td>
                            <td width="40px"><b>TO:</b></td>
                            <td class="table_A4_bottom"> <%=SafeValue.SafeDate(tab.Rows[i]["Date2"],DateTime.Today).ToString("dd/MM/yyyy") %>&nbsp;&nbsp; <%=SafeValue.SafeString(tab.Rows[i]["Time2"]) %></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 30px;vertical-align:text-top;">
                <td><b>REASON(IF ANY):</b>
                </td>
                <td colspan="4" rowspan="6" style="height: 300px;vertical-align:top;"> <%=SafeValue.SafeString(tab.Rows[i]["Remark"]) %></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td height="100px">
                    <hr align="right" size="0.5" />
                    <div align="center" style="padding-top: 2px; margin: 0px">SIGNATURE</div>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="50px"></td>
            </tr>
        </table>

        <table class="table_A5" style="table-layout: fixed; font-size: 15px; font-family: Arial; font-weight: bold" cellpadding="0" cellspacing="0">
            <tr style="text-align: center">
                <td rowspan="2" width="15%" class="td_bottom_right">Leave Record</td>
                <td width="15%" class="td_bottom_right">No.ofDays Entitled</td>
                <td width="15%" class="td_bottom_right">No.ofDays Taken</td>
                <td width="15%" class="td_bottom_right">When Taken</td>
                <td width="15%" class="td_bottom_right">Balance of Days</td>
                <td class="td_bottom">Remarks</td>
            </tr>
            <tr style="text-align: center" height="50px">

                <td width="15%" class="td_bottom_right"><%=SafeValue.SafeString(tab.Rows[i]["Entitled"]) %></td>
                <td width="15%" class="td_bottom_right"><%=SafeValue.SafeString(tab.Rows[i]["Days"]) %></td>
                <td width="15%" class="td_bottom_right"><%=SafeValue.SafeDate(tab.Rows[i]["Date1"],DateTime.Today).ToString("dd/MM/yyyy") %>&nbsp;&nbsp; <%=SafeValue.SafeString(tab.Rows[i]["Time1"]) %> </td>
                <td width="15%" class="td_bottom_right"><%=SafeValue.SafeString(tab.Rows[i]["BalDays"]) %></td>
                <td width="15%" class="td_bottom"><%=SafeValue.SafeString(tab.Rows[i]["TemRemark"]) %></td>

            </tr>
            <tr style="text-align: center">
                <td rowspan="2" class="td_bottom_right">Approved By</td>
                <td class="td_bottom_right">Deprive Supervisor</td>
                <td class="td_bottom_right">Deprive Manager</td>
                <td class="td_bottom_right">General Manager</td>
                <td class="td_bottom_right">Director</td>
                <td class="td_bottom">Remarks</td>
            </tr>
            <tr style="text-align: center" height="50px">
                <td height="50px" class="td_bottom_right"></td>
                <td class="td_bottom_right"></td>
                <td class="td_bottom_right"></td>
                <td class="td_bottom_right"></td>
                <td class="td_bottom"></td>
            </tr>
            <tr style="text-align: center">
                <td rowspan="2" class="td_right" height="80px">Disapproved By</td>
                <td colspan="5" style="text-align: left" heigth="50px">&nbsp;</td>
            </tr>
        </table>
        <%} %>
        <!--endprint-->
    </form>
</body>
</html>
