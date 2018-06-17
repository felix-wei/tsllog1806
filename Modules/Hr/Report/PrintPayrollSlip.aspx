<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPayrollSlip.aspx.cs" Inherits="Modules_Hr_Report_PrintPayrollSlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
         <style type="text/css">
        .table_A4 {
            width: 210mm;
            background-color: white;
            height: auto;
            font-family: Arial;
            font-size: 14px;
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
             .table_border {
                 border-left: solid 1px #000000;
                 border-top: solid 1px #000000;
                 border-spacing: 0px;
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
<body onload="doPrint()">
    <form id="form1" runat="server">
        <!--startprint-->
        <div style="margin: 0 auto; width: 210mm;">

            <% 
                DataTable tab = GetData();
            %>

            <table class="table_A4_border" style="margin-top: 50px;" cellspacing="0" cellpadding="0">
                <tr style="text-align: center">
                    <td width="50%" height="30px" class="td_right" colspan="2">
                        <table class="table_A4" style="margin-top: 10px">
                            <tr style="vertical-align: top; text-align: left">
                                <td width="120">Name:</td>
                                <td width="120"><%=tab.Rows[0]["Name"]  %></td>

                                <td width="120" style="text-align: right">NRIC/WP No. </td>
                                <td width="120"><%=tab.Rows[0]["IcNo"]  %></td>
                                <td width="120">Designation:</td>
                                <td width="120"><%=tab.Rows[0]["HrRole"]  %></td>
                            </tr>
                            <tr style="vertical-align: top; text-align: left">
                                <td width="120">&nbsp;</td>
                                <td width="120">&nbsp;</td>

                                <td width="120">&nbsp;</td>
                                <td width="120">&nbsp;</td>
                                <td width="120">&nbsp;</td>
                                <td width="120">&nbsp;</td>
                            </tr>
                            <tr style="vertical-align: top; text-align: left">
                                <td width="120" colspan="2" style="width: 240px; font-weight: bold"><%=System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToUpper() %></td>

                                <td width="150">Payslip for the Month</td>
                                <td width="120"><%=Helper.Safe.SafeDate(tab.Rows[0]["FromDate"]).ToString("MMMM yyyy") %></td>
                                <td width="120">Date Joined:</td>
                                <td width="120"><%=Helper.Safe.SafeDate(tab.Rows[0]["BeginDate"]).ToString("dd/MM/yyyy") %></td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr style="text-align: left">
                    <td width="40%" height="30px" style="text-align: left"><font style="font-size: 14px; font-weight: bold; text-decoration: underline">Earnings</font></td>
                    <td width="60%" style="text-align: left"><font style="font-size: 14px; font-weight: bold; text-decoration: underline">Reimbursement</font></td>
                </tr>
                <tr style="vertical-align: top; height: auto">
                    <td>
                        <table width="98%" cellspacing="0" cellpadding="0" style="margin-right: 10px; height: 450px">
                            <tr style="vertical-align: top; height: 100%">
                                <td colspan="2">
                                    <table style="width: 100%; line-height: 25px">
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td width="200px">Basic Salary</td>
                                            <td width="50px">:</td>
                                            <td width="50px">$</td>
                                            <td style="text-align: right; width: 200px"><%=SafeAccountSz(tab.Rows[0]["Amt"]) %></td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>Mid Month Salary</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;"><%=SafeAccountSz(tab.Rows[0]["Amt13"]) %></td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>Unpaid Leave/Absent</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;">(<%=SafeAccountSz(tab.Rows[0]["Amt20"]) %>)</td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>Leave Pay</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;"><%=SafeAccountSz(tab.Rows[0]["Amt20"]) %></td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>Pay-In-Lieu</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;"><%=SafeAccountSz(tab.Rows[0]["Amt21"]) %></td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>Bonus</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;"><%=SafeAccountSz(tab.Rows[0]["Amt1"]) %></td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>Advance Salary</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;"><%=SafeAccountSz(tab.Rows[0]["Amt13"]) %></td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>Telephone Overusage</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;"><%=SafeAccountSz(tab.Rows[0]["Amt17"]) %></td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>Witholding Tax</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;"><%=SafeAccountSz(tab.Rows[0]["Amt15"]) %></td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                            <td>NS</td>
                                            <td>:</td>
                                            <td>$</td>
                                            <td style="text-align: right;"><%=SafeAccountSz(tab.Rows[0]["Amt14"]) %></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="vertical-align: top; height: 20px">
                                <td colspan="2" class="td_bottom">&nbsp;</td>
                            </tr>
                            <tr style="vertical-align: bottom">
                                <td colspan="2">
                                    <table width="98%" cellspacing="0" cellpadding="0" style="height: 120px">
                                        <tr>
                                            <td width="20%"><font style="font-size: 14px; font-weight: bold;">Pay on</font></td>
                                            <td><font style="font-size: 14px; font-weight: bold;"><%=string.Format("{0:dd MMMM yyyy}", Helper.Safe.SafeDate(tab.Rows[0]["ToDate"])) %></font></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" height="90px"></td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" style="width: 100%; font-weight: bold; margin-bottom: 0px">
                                        <tr>
                                            <td width="50px">Bank:</td>
                                            <td width="60px"><font style="font-size: 13px; font-weight: bold;"><%=tab.Rows[0]["BankCode"] %></font></td>
                                            <td width="50px">A/C No.</td>
                                            <td><font style="font-size: 13px; font-weight: bold;"><%=tab.Rows[0]["AccNo"] %></font></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td>
                        <table width="100%" style="height: 220px; margin-bottom: 20px">
                            <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                <td width="120px">&nbsp;</td>
                                <td style="text-align: left">Laundry Expenses</td>
                                <td width="50px">:</td>
                                <td width="50px">$</td>
                                <td style="text-align: left" width="120px"><%=SafeAccountSz(tab.Rows[0]["Amt10"]) %></td>

                            </tr>
                            <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                <td width="120px">&nbsp;</td>
                                <td style="text-align: left">&nbsp;&nbsp;</td>
                                <td>:</td>
                                <td>$</td>
                                <td style="text-align: left"></td>

                            </tr>
                            <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                <td width="120px">&nbsp;</td>
                                <td style="text-align: left">Transport</td>
                                <td>:</td>
                                <td>$</td>
                                <td style="text-align: left"><%=SafeAccountSz(tab.Rows[0]["Amt9"]) %></td>

                            </tr>
                            <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                <td width="120px">&nbsp;</td>
                                <td style="text-align: left">Accomdation</td>
                                <td>:</td>
                                <td>$</td>
                                <td style="text-align: left"><%=SafeAccountSz(tab.Rows[0]["Amt11"]) %></td>

                            </tr>
                            <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                <td width="120px">&nbsp;</td>
                                <td style="text-align: left">Reimbursement</td>
                                <td>:</td>
                                <td>$</td>
                                <td style="text-align: left"><%=SafeAccountSz(tab.Rows[0]["Amt12"]) %></td>
                            </tr>
                            <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                <td width="120px"><font style="font-size: 14px; font-weight: bold; text-decoration: underline">CPF</font></td>
                                <td style="text-align: left">Employee</td>
                                <td width="50px">:</td>
                                <td width="50px">$</td>
                                <td style="text-align: left"><%=SafeAccountSz(tab.Rows[0]["Amt2"]) %></td>

                            </tr>
                            <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                <td width="120px">&nbsp;</td>
                                <td style="text-align: left">Employer&#39;s</td>
                                <td>:</td>
                                <td>$</td>
                                <td style="text-align: left"><%=SafeAccountSz(tab.Rows[0]["Amt6"]) %></td>

                            </tr>
                            <tr style="vertical-align: top; text-align: left; font-weight: bold">
                                <td width="120px">&nbsp;</td>
                                <td style="text-align: left">MBMF/SINDA/CDAC</td>
                                <td>:</td>
                                <td>$</td>
                                <td style="text-align: left"><%=SafeAccountSz(SafeValue.SafeDecimal(tab.Rows[0]["Amt3"])+SafeValue.SafeDecimal(tab.Rows[0]["Amt4"])+SafeValue.SafeDecimal(tab.Rows[0]["Amt5"])) %></td>
                            </tr>
                        </table>
                        <table class="table_border" width="100%" cellspacing="0" cellpadding="0" style="margin-right: 10px; height: 220px; text-align: left;">
                            <tr style="text-align: left;padding-left: 10px">
                                <td width="100px"></td>
                                <td width="20px"></td>
                                <td width="100px"></td>
                                <td width="100px"><font style="font-size: 14px; font-weight: bold; text-decoration: underline">Current</font></td>
                                <td width="50px">/</td>
                                <td width="200px"><font style="font-size: 14px; font-weight: bold; text-decoration: underline">Year-To-Date</font></td>
                            </tr>
                            <tr style="padding-left: 10px">
                                <td>Nett Payable</td>
                                <td>:</td>
                                <td>$</td>
                                <td><%=SafeAccountSz(tab.Rows[0]["NettPayable"]) %></td>
                                <td>/</td>
                                <td><%=SafeAccountSz(TotalNettPayable(tab.Rows[0]["Person"].ToString())) %></td>
                            </tr>
                            <tr style="padding-left: 10px">
                                <td>Nett Wage</td>
                                <td>:</td>
                                <td>$</td>
                                <td><%=SafeAccountSz(tab.Rows[0]["NettWage"]) %></td>
                                <td>/</td>
                                <td><%=SafeAccountSz(TotalNettWage(tab.Rows[0]["Person"].ToString())) %></td>
                            </tr>
                            <tr style="padding-left: 10px">
                                <td>Gross Wage</td>
                                <td>:</td>
                                <td>$</td>
                                <td><%=SafeAccountSz(tab.Rows[0]["GrossWage"]) %></td>
                                <td>/</td>
                                <td><%=SafeAccountSz(TotalGrossWage(tab.Rows[0]["Person"].ToString())) %></td>
                            </tr>
                            <tr style="padding-left: 10px">
                                <td>CPF Wage</td>
                                <td>:</td>
                                <td>$</td>
                                <td><%=SafeAccountSz(tab.Rows[0]["CPFWage"]) %></td>
                                <td>/</td>
                                <td><%=SafeAccountSz(TotalCPFWage(tab.Rows[0]["Person"].ToString())) %></td>
                            </tr>
                            <tr style="padding-left: 10px">
                                <td>Total CPF</td>
                                <td>:</td>
                                <td>$</td>
                                <td><%=SafeAccountSz(tab.Rows[0]["TotalCPF"]) %></td>
                                <td>/</td>
                                <td><%=SafeAccountSz(TotalCPF(tab.Rows[0]["Person"].ToString())) %></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <!--endprint-->
    </form>
</body>
</html>
