<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPaySlip.aspx.cs" Inherits="Modules_Hr_Report_PrintPaySlip" %>

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
    <div style="margin:0 auto;width: 210mm;">
       <table style="text-align: left; border-spacing: 0px;line-height:25px; width: 210mm; background-color: white; height: auto; font-family: Arial; font-size: 14px;">
            <tr>
                 <td width="70%">
                   <p> <font size="6"> <%= System.Configuration.ConfigurationManager.AppSettings["CompanyName"] %></font></p>
                    <p <font size="3">
                    <%= System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"]+System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"] %><br />
                     <%= System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"] %><br />
                    <%= System.Configuration.ConfigurationManager.AppSettings["CompanyAddress4"] %><br />
                    <%= System.Configuration.ConfigurationManager.AppSettings["CompanyAddress5"] %><br />
                   <%= System.Configuration.ConfigurationManager.AppSettings["CompanyAddress6"] %><br />
                       </font> </p>
                </td>
                <td width="20%"></td>
                <td width="10%">
                    <img  src='<%= System.Configuration.ConfigurationManager.AppSettings["Logo"] %>' />
                </td>
            </tr>
        </table>
 <table class="table_A4" style="margin-top:15px;">
            <tr style="text-align: center">
                <td width="30%"></td>
                <td style=" color:#000">
                    
                    <font size="4">PAYSLIP</font>
                </td>
                <td width="30%"></td>
            </tr>
        </table>

        <% 
            DataTable tab=GetData();
            for(int i=0;i<tab.Rows.Count;i++){
            %>
        <table class="table_A4" style="margin-top:10px">
            <tr style="vertical-align: top">
                <td width="120">Emp Name</td>

                <td width="120">:<%=tab.Rows[0]["Name"] %></td>
                
                <td width="120">Join Date:</td>
                <td width="120">:<%=Helper.Safe.SafeDateStr(tab.Rows[0]["BeginDate"]) %></td>
                <td width="120">
                    Month</td>
                <td width="120">:<%=string.Format("{0:MM/yyyy}", tab.Rows[0]["FromDate"]) %></td>
            </tr>
            <tr>
                <td>Bank Name</td>
                <td>:<%=tab.Rows[0]["BankName"] %></td>
                <td>Bank A/c No</td>
                <td>:<%=tab.Rows[0]["AccNo"] %></td>
                <td>Designation</td>
                <td>:<%=tab.Rows[0]["HrRole"] %></td>
            </tr>
            </table>
        
        <table class="table_A4_border" style="margin-top:50px;" cellspacing="0" cellpadding="0">
            <tr style="text-align:center"> 
                <td width="50%" height="30px" class="td_right"><font style="font-size:16px;font-weight:bold;">INCOME</font></td>
                <td><font style="font-size:16px;font-weight:bold;">DEDUCTIONS</font></td>
            </tr>
            <tr style=" vertical-align:top;height:auto">
                <td width="50%" class="td_bottom_right">
                    <table width="98%" cellspacing="0" cellpadding="0" style="margin-right:10px">
                        <% DataTable tab_det = GetDetData();
                           for (int j = 0; j < tab_det.Rows.Count; j++)
                           { %>
                        <tr style="vertical-align:top">
                            <td>&nbsp;&nbsp;<%=tab_det.Rows[j]["ChgCode"] %></td>
                            <td style="text-align:right"><%=tab_det.Rows[j]["Amt"] %></td>
                        </tr>
                        <%} %>
                         <% DataTable tab_det2 = GetDetData2(SafeValue.SafeInt(tab.Rows[0]["Id"],0));
                            for (int j = 0; j < tab_det2.Rows.Count; j++)
                           { %>
                         <tr>
                            <td>&nbsp;&nbsp;CPF</td>
                            <td style="text-align:right"><%=tab_det2.Rows[j]["Amt"] %></td>
                        </tr>
                        <%} %>
                    </table>
                </td>
                <td width="50%" class="td_bottom" height="200px">
                    <table width="98%" cellspacing="0" cellpadding="0" style="margin-right:10px">
                        <% DataTable tab_det1 = GetDetData1();
                           for (int j = 0; j < tab_det1.Rows.Count; j++)
                           { %>
                         <tr>
                            <td>&nbsp;&nbsp;<%=tab_det1.Rows[j]["ChgCode"] %></td>
                            <td style="text-align:right"><%=tab_det1.Rows[j]["Amt"] %></td>
                        </tr>
                        <%} %>
                       
                    </table>
                </td>
            </tr>
            <tr>
                <td width="50%" class="td_right" height="50px;">
                    <table width="98%" cellspacing="0" cellpadding="0" style="margin-top:10px" height="20px">
                        <tr>
                            <td width="80px"></td>
                            <td style="text-align: left"><font style="font-size: 12px; font-weight: bold;">&nbsp;&nbsp;TOTAL INCOME</font></td>
                            <td style="text-align: right"><font style="font-size: 12px; font-weight: bold;"><%=GetTotalAmt() %></font></td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="98%" cellspacing="0" cellpadding="0" style="margin-top:10px" height="20px">
                        <tr>
                            <td width="80px"></td>
                            <td><font style="font-size: 12px; font-weight: bold;">&nbsp;&nbsp;TOTAL DEDUCTIONS</font></td>
                            <td style="text-align: right"><font style="font-size: 12px; font-weight: bold;"><%=GetTotalAmt1() %></font></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="table_A4_border" style="margin-top:20px;height:100px" cellspacing="0" cellpadding="0">
            <tr style=" vertical-align:top;height:auto">
                <td width="50%">
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                     <table width="98%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td><font style="font-size: 12px; font-weight: bold;">&nbsp;&nbsp;NEIT PAY</font></td>
                            <td style="text-align:right"><%=GetTotalAmt2() %></td>

                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
        <table class="table_A4">
            <tr>
                <td colspan="2"  style="text-align:right">
                     <font style="font-size: 12px; font-weight: bold;">This is computer generated pay slip hence signature is not required.</font>
                </td>
            </tr>
        </table>
        <%} %>
    </div>
        <!--endprint-->
    </form>
</body>
</html>
