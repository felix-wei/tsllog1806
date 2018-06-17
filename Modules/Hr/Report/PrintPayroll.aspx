<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPayroll.aspx.cs" Inherits="ReportJob_PrintPayroll" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
         <link href="/Modules/Hr/Css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">

        .td_top_left_bottom {
            border-top:1px solid #666;
            border-left:1px solid #666;
            border-bottom:1px solid #666;
            padding:0px;
            margin:0px;
        }
        .td_left_bottom {
            border-left:1px solid #666;
            border-bottom:1px solid #666;
            padding:0px;
            margin:0px;
        }
        .td_top_left_bottom_right {
            border-top:1px solid #666;
            border-left:1px solid #666;
            border-bottom:1px solid #666;
            border-right:1px solid #666;
            padding:0px;
            margin:0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table>
            <tr>
                <td>Date</td>
                <td>
                    <dxe:ASPxDateEdit ID="date_Satrt" Width="120" runat="server" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_End" Width="120" runat="server" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                     <dxe:ASPxButton ID="btn_export" Width="120" runat="server" Text="Save Excel" OnClick="btn_export_Click">
                        </dxe:ASPxButton>
                </td>
            </tr>
            
        </table>
        <div class="table">
            <table  cellpadding="0" cellspacing="0" style="margin-top:50px;width:4800px;" >
                <tr style="text-align: left; padding: 0;">
                    <td colspan="40">Payroll for &nbsp;<%=PrintDate() %></td>
                </tr>
                <tr>
                    <td height="40px" colspan="40"></td>
                </tr>
                <tr style="text-align: center;">
                    <td width="30px">&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="4" class="td_top_left_bottom">Deductions</td>
                    <td colspan="4" class="td_top_left_bottom">Additional</td>
                    <td rowspan="3" class="td_top_left_bottom" width="120px">Sub-Total</td>
                    <td colspan="11" class="td_top_left_bottom">CPF Contribution </td>
                    <td colspan="3" rowspan="2" class="td_top_left_bottom">Expenses Reimbursement </td>
                    <td rowspan="3" class="td_top_left_bottom" width="120px">Employee Gross Salary </td>
                    <td colspan="6" class="td_top_left_bottom">Deductions</td>
                    <td colspan="2" class="td_top_left_bottom">Employee</td>
                    <td rowspan="3" class="td_top_left_bottom" width="120px">Employer Gross Pay </td>
                    <td rowspan="3" class="td_top_left_bottom" width="120px">Payment Mode </td>
                    <td rowspan="3" class="td_top_left_bottom" width="120px">Net Amount(GIRO) </td>
                    <td rowspan="3" class="td_top_left_bottom_right" width="120px">Net Amount (CHEQUE) </td>
                </tr>
                <tr style="text-align: center;">
                    <td rowspan="2" class="td_top_left_bottom">S/N</td>
                    <td rowspan="2" class="td_top_left_bottom" width="180px">Employee</td>
                    <td colspan="2" class="td_top_left_bottom" width="240px">Bank</td>
                    <td rowspan="2" class="td_top_left_bottom" width="100px">Basic Pay </td>
                    <td colspan="2" class="td_left_bottom" width="390px">Unpaid Leave/Absent </td>
                    <td colspan="2" class="td_left_bottom" width="390px">Absent</td>
                    <td colspan="2" class="td_left_bottom" width="390px">Leave Pay </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Pay-In-Lieu</td>
                    <td rowspan="2" class="td_left_bottom" width="120px">2014 Bonus </td>
                    <td colspan="5" class="td_left_bottom">Employee</td>
                    <td colspan="4" class="td_left_bottom">Employer</td>
                    <td rowspan="2" class="td_left_bottom" width="100px">Employer&amp;Employer Total </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Overall Total </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Mid Month Salary </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Advance Salary </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">NS Payment </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Withholding Tax </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Others</td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Tel Overusage </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Nett Amount </td>
                    <td rowspan="2" class="td_left_bottom" width="120px">Nett Amount Payable </td>
                </tr>
                <tr style="text-align: center;">
                    <td class="td_left_bottom" width="80px">Name</td>
                    <td class="td_left_bottom" width="120px">A/C No.</td>
                    <td class="td_left_bottom">No. of Days </td>
                    <td class="td_left_bottom">Amount</td>
                    <td class="td_left_bottom">No. of Day</td>
                    <td class="td_left_bottom">Amount</td>
                    <td class="td_left_bottom">No. of Day</td>
                    <td class="td_left_bottom">Amount</td>
                    <td class="td_left_bottom" width="120px">%</td>
                    <td class="td_left_bottom" width="120px">CPF</td>
                    <td class="td_left_bottom" width="120px">MBMF</td>
                    <td class="td_left_bottom" width="120px">SINDA</td>
                    <td class="td_left_bottom" width="120px">CDAC</td>
                    <td class="td_left_bottom" width="60px">%</td>
                    <td class="td_left_bottom" width="120px">CPF</td>
                    <td class="td_left_bottom" width="120px">FWL</td>
                    <td class="td_left_bottom" width="120px">SDL</td>
                    <td class="td_left_bottom" width="120px">Transport</td>
                    <td class="td_left_bottom" width="120px">Laundry</td>
                    <td class="td_left_bottom" width="120px">Accom</td>
                </tr>
                <tr>
                    <td colspan="40" height="20px"></td>
                </tr>
                 <tr>
                    <td colspan="40">CPF Employee</td>
                </tr>
                <%
                    decimal grandSalary = 0;
                    decimal grandLeave1 = 0;
                    decimal grandLeave2 = 0;
                    decimal grandLeave3 = 0;
                    decimal subGrand = 0;
                    decimal grandBonus = 0;
                    decimal grandEmployeeCPF = 0;
                    decimal grandMBMF = 0;
                    decimal grandSINDA = 0;
                    decimal grandCDAC = 0;
                    decimal grandEmployerCPF = 0;
                    decimal grandFWL = 0;
                    decimal grandSDL = 0;
                    decimal grandTransport = 0;
                    decimal grandLaundry = 0;
                    decimal grandAccom = 0;
                    decimal grandMidMonth = 0;
                    decimal grandAdvance = 0;
                    decimal grandNSPayment = 0;
                    decimal grandTax = 0;
                    decimal grandOthers = 0;
                    decimal grandTel = 0;
                    decimal grandCPF = 0;
                    decimal grandOverall = 0;
                    decimal grandGrossSalary = 0;
                    decimal grandNet1 = 0;
                    decimal grandNet2 = 0;
                    decimal grandNet3 = 0;
                    decimal grandNet4 = 0;
                    decimal grandNet5 = 0;
                    decimal grandNet6 = 0;
                     %>
                <%
                    decimal totalCPF = 0;
                    decimal totalOverall = 0;
                    decimal grossSalary = 0;
                    decimal totalNet1 = 0;
                    decimal totalNet2 = 0;
                    decimal totalNet3 = 0;
                    decimal totalNet4 = 0;
                    decimal totalNet5 = 0;
                    decimal totalNet6 = 0;
                     %>
                <% 
                    
                    DataTable dt=GetCPFData();
                   
                    decimal totalSalary = 0;
                    decimal totalLeave1=0;
                    decimal totalLeave2 = 0;
                    decimal totalLeave3 = 0;
                    decimal subTotal = 0;
                    decimal totalBonus = 0;
                    decimal totalEmployeeCPF = 0;
                    decimal totalMBMF = 0;
                    decimal totalSINDA = 0;
                    decimal totalCDAC = 0;
                    decimal totalEmployerCPF = 0;
                    decimal totalFWL = 0;
                    decimal totalSDL= 0;
                    decimal totalTransport = 0;
                    decimal totalLaundry = 0;
                    decimal totalAccom = 0;
                    decimal totalMidMonth= 0;
                    decimal totalAdvance = 0;
                    decimal totalNSPayment = 0;
                    decimal totalTax = 0;
                    decimal totalOthers = 0;
                    decimal totalTel = 0;
                    
                    
                    for (int i=0;i<dt.Rows.Count;i++){
                         
                 %>
                <tr style="text-align: center;">
                    <td class="td_top_left_bottom" style="text-align:center"><%=i+1 %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Name"] %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["BankCode"] %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["AccNo"] %></td>
                    <td class="td_top_left_bottom"> <%=dt.Rows[i]["Amt"] %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Days"] %></td>
                    <td class="td_top_left_bottom"> <%=dt.Rows[i]["LeaveAmt1"] %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Days1"] %></td>
                    <td class="td_top_left_bottom"> -<%=dt.Rows[i]["LeaveAmt2"] %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["LeaveAmt3"] %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt1"]  %></td>
                    <td class="td_top_left_bottom"><%=SafeValue.SafeDecimal(dt.Rows[i]["LeaveAmt2"])+SafeValue.SafeDecimal(dt.Rows[i]["LeaveAmt3"])+SafeValue.SafeDecimal(dt.Rows[i]["Amt1"])  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["EmployeeRate"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt2"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt3"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt4"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt5"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["EmployerRate"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt6"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt7"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt8"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt9"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt10"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt11"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt12"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt13"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt14"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt15"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt16"]  %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Amt17"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom_right"></td>
                </tr>
                <%
                        totalSalary += SafeValue.SafeDecimal(dt.Rows[i]["Amt"]);
                        totalLeave1 += SafeValue.SafeDecimal(dt.Rows[i]["LeaveAmt1"]);
                        totalLeave2 -= SafeValue.SafeDecimal(dt.Rows[i]["LeaveAmt2"]);
                        totalLeave3 += SafeValue.SafeDecimal(dt.Rows[i]["LeaveAmt3"]);
                        totalBonus += SafeValue.SafeDecimal(dt.Rows[i]["Amt1"]);
                        subTotal += (SafeValue.SafeDecimal(dt.Rows[i]["LeaveAmt2"]) + SafeValue.SafeDecimal(dt.Rows[i]["LeaveAmt3"]) + SafeValue.SafeDecimal(dt.Rows[i]["Amt1"]));
                        totalEmployeeCPF += SafeValue.SafeDecimal(dt.Rows[i]["Amt2"]);
                        totalMBMF += SafeValue.SafeDecimal(dt.Rows[i]["Amt3"]);
                        totalSINDA += SafeValue.SafeDecimal(dt.Rows[i]["Amt4"]);
                        totalCDAC += SafeValue.SafeDecimal(dt.Rows[i]["Amt5"]);
                        totalEmployerCPF += SafeValue.SafeDecimal(dt.Rows[i]["Amt6"]);
                        totalFWL += SafeValue.SafeDecimal(dt.Rows[i]["Amt7"]);
                        totalSDL += SafeValue.SafeDecimal(dt.Rows[i]["Amt8"]);
                        totalTransport += SafeValue.SafeDecimal(dt.Rows[i]["Amt9"]);
                        totalLaundry += SafeValue.SafeDecimal(dt.Rows[i]["Amt10"]);
                        totalAccom += SafeValue.SafeDecimal(dt.Rows[i]["Amt11"]);
                        totalMidMonth += SafeValue.SafeDecimal(dt.Rows[i]["Amt12"]);
                        totalAdvance += SafeValue.SafeDecimal(dt.Rows[i]["Amt13"]);
                        totalNSPayment += SafeValue.SafeDecimal(dt.Rows[i]["Amt14"]);
                        totalTax += SafeValue.SafeDecimal(dt.Rows[i]["Amt15"]);
                        totalOthers += SafeValue.SafeDecimal(dt.Rows[i]["Amt16"]);
                        totalTel += SafeValue.SafeDecimal(dt.Rows[i]["Amt17"]);
                        
                }
                    grandSalary +=totalSalary;
                    grandLeave1 += totalLeave1;
                    grandLeave2 += totalLeave2;
                    grandLeave3 += totalLeave3;
                    grandBonus += totalBonus;
                    subGrand += subTotal;
                    grandEmployeeCPF += totalEmployeeCPF;
                    grandMBMF += totalMBMF;
                    grandSINDA += totalSINDA;
                    grandCDAC += totalCDAC;
                    grandEmployerCPF += totalEmployerCPF;
                    grandFWL += totalFWL;
                    grandSDL += totalSDL;
                    grandTransport += totalTransport;
                    grandLaundry += totalLaundry;
                    grandAccom += totalAccom;
                    grandMidMonth += totalMidMonth;
                    grandAdvance += totalAdvance;
                    grandNSPayment += totalNSPayment;
                    grandTax += totalTax;
                    grandOthers += totalOthers;
                    grandTel += totalTel;    
               %>
                <tr style="background-color:#9fa597;text-align: center;">
                    <td class="td_top_left_bottom"><%=dt.Rows.Count %></td>
                    <td class="td_top_left_bottom">Total Amount</td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"> <%=totalSalary %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave1 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave2 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave3 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalBonus %></td>
                    <td class="td_top_left_bottom"><%=subTotal %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalEmployeeCPF %></td>
                    <td class="td_top_left_bottom"><%=totalMBMF %></td>
                    <td class="td_top_left_bottom"><%=totalSINDA %></td>
                    <td class="td_top_left_bottom"><%=totalCDAC %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalEmployerCPF %></td>
                    <td class="td_top_left_bottom"><%=totalFWL %></td>
                    <td class="td_top_left_bottom"><%=totalSDL %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalTransport %></td>
                    <td class="td_top_left_bottom"><%=totalLaundry %></td>
                    <td class="td_top_left_bottom"><%=totalAccom %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalMidMonth %></td>
                    <td class="td_top_left_bottom"><%=totalAdvance %></td>
                    <td class="td_top_left_bottom"><%=totalNSPayment %></td>
                    <td class="td_top_left_bottom"><%=totalTax %></td>
                    <td class="td_top_left_bottom"><%=totalOthers %></td>
                    <td class="td_top_left_bottom"><%=totalTel %></td>
                     <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom_right"></td>
                </tr>
                 <tr>
                    <td colspan="40">Directors</td>
                </tr>
                <% 
                    DataTable dt1=GetDirectorsData();
                    totalSalary = 0;
                    totalLeave1 = 0;
                    totalLeave2 = 0;
                    totalLeave3 = 0;
                    subTotal = 0;
                    totalBonus = 0;
                    totalEmployeeCPF = 0;
                    totalMBMF = 0;
                    totalSINDA = 0;
                    totalCDAC = 0;
                    totalEmployerCPF = 0;
                    totalFWL = 0;
                    totalSDL = 0;
                    totalTransport = 0;
                    totalLaundry = 0;
                    totalAccom = 0;
                    totalMidMonth = 0;
                    totalAdvance = 0;
                    totalNSPayment = 0;
                    totalTax = 0;
                    totalOthers = 0;
                    totalTel = 0;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    { %>
                <tr style="text-align: center;">
                    <td class="td_top_left_bottom" style="text-align: center"><%=i+1 %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Name"] %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["BankCode"] %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["AccNo"] %></td>
                    <td class="td_top_left_bottom"> <%=dt1.Rows[i]["Amt"] %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Days"] %></td>
                    <td class="td_top_left_bottom"> <%=dt1.Rows[i]["LeaveAmt1"] %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Days1"] %></td>
                    <td class="td_top_left_bottom"> -<%=dt1.Rows[i]["LeaveAmt2"] %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["LeaveAmt3"] %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt1"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["EmployeeRate"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt2"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt3"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt4"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt5"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["EmployerRate"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt6"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt7"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt8"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt9"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt10"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt11"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt12"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt13"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt14"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt15"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt16"]  %></td>
                    <td class="td_top_left_bottom"><%=dt1.Rows[i]["Amt17"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom_right"></td>
                </tr>
                <%
                        totalSalary += SafeValue.SafeDecimal(dt1.Rows[i]["Amt"]);
                        totalLeave1 += SafeValue.SafeDecimal(dt1.Rows[i]["LeaveAmt1"]);
                        totalLeave2 -= SafeValue.SafeDecimal(dt1.Rows[i]["LeaveAmt2"]);
                        totalLeave3 += SafeValue.SafeDecimal(dt1.Rows[i]["LeaveAmt3"]);
                        totalBonus += SafeValue.SafeDecimal(dt1.Rows[i]["Amt1"]);
                        subTotal += (SafeValue.SafeDecimal(dt1.Rows[i]["LeaveAmt2"]) + SafeValue.SafeDecimal(dt1.Rows[i]["LeaveAmt3"]) + SafeValue.SafeDecimal(dt1.Rows[i]["Amt1"]));
                        totalEmployeeCPF += SafeValue.SafeDecimal(dt1.Rows[i]["Amt2"]);
                        totalMBMF += SafeValue.SafeDecimal(dt1.Rows[i]["Amt3"]);
                        totalSINDA += SafeValue.SafeDecimal(dt1.Rows[i]["Amt4"]);
                        totalCDAC += SafeValue.SafeDecimal(dt1.Rows[i]["Amt5"]);
                        totalEmployerCPF += SafeValue.SafeDecimal(dt1.Rows[i]["Amt6"]);
                        totalFWL += SafeValue.SafeDecimal(dt1.Rows[i]["Amt7"]);
                        totalSDL += SafeValue.SafeDecimal(dt1.Rows[i]["Amt8"]);
                        totalTransport += SafeValue.SafeDecimal(dt1.Rows[i]["Amt9"]);
                        totalLaundry += SafeValue.SafeDecimal(dt1.Rows[i]["Amt10"]);
                        totalAccom += SafeValue.SafeDecimal(dt1.Rows[i]["Amt11"]);
                        totalMidMonth += SafeValue.SafeDecimal(dt1.Rows[i]["Amt12"]);
                        totalAdvance += SafeValue.SafeDecimal(dt1.Rows[i]["Amt13"]);
                        totalNSPayment += SafeValue.SafeDecimal(dt1.Rows[i]["Amt14"]);
                        totalTax += SafeValue.SafeDecimal(dt1.Rows[i]["Amt15"]);
                        totalOthers += SafeValue.SafeDecimal(dt1.Rows[i]["Amt16"]);
                        totalTel += SafeValue.SafeDecimal(dt1.Rows[i]["Amt17"]);
                  }
                    grandSalary += totalSalary;
                    grandLeave1 += totalLeave1;
                    grandLeave2 += totalLeave2;
                    grandLeave3 += totalLeave3;
                    grandBonus += totalBonus;
                    subGrand += subTotal;
                    grandEmployeeCPF += totalEmployeeCPF;
                    grandMBMF += totalMBMF;
                    grandSINDA += totalSINDA;
                    grandCDAC += totalCDAC;
                    grandEmployerCPF += totalEmployerCPF;
                    grandFWL += totalFWL;
                    grandSDL += totalSDL;
                    grandTransport += totalTransport;
                    grandLaundry += totalLaundry;
                    grandAccom += totalAccom;
                    grandMidMonth += totalMidMonth;
                    grandAdvance += totalAdvance;
                    grandNSPayment += totalNSPayment;
                    grandTax += totalTax;
                    grandOthers += totalOthers;
                    grandTel += totalTel;   
                  %>
                <tr style="background-color:#9fa597;text-align: center;">
                    <td class="td_top_left_bottom"><%=dt1.Rows.Count %></td>
                    <td class="td_top_left_bottom">Total Amount</td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"> <%=totalSalary %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave1 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave2 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave3 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalBonus %></td>
                    <td class="td_top_left_bottom"><%=subTotal %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalEmployeeCPF %></td>
                    <td class="td_top_left_bottom"><%=totalMBMF %></td>
                    <td class="td_top_left_bottom"><%=totalSINDA %></td>
                    <td class="td_top_left_bottom"><%=totalCDAC %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalEmployerCPF %></td>
                    <td class="td_top_left_bottom"><%=totalFWL %></td>
                    <td class="td_top_left_bottom"><%=totalSDL %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalTransport %></td>
                    <td class="td_top_left_bottom"><%=totalLaundry %></td>
                    <td class="td_top_left_bottom"><%=totalAccom %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalMidMonth %></td>
                    <td class="td_top_left_bottom"><%=totalAdvance %></td>
                    <td class="td_top_left_bottom"><%=totalNSPayment %></td>
                    <td class="td_top_left_bottom"><%=totalTax %></td>
                    <td class="td_top_left_bottom"><%=totalOthers %></td>
                    <td class="td_top_left_bottom"><%=totalTel %></td>
                     <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom_right"></td>
                </tr>
                 <tr>
                    <td colspan="40">Non-CPF Employee</td>
                </tr>
                <% 
                     totalSalary = 0;
                     totalLeave1 = 0;
                     totalLeave2 = 0;
                     totalLeave3 = 0;
                     subTotal = 0;
                     totalBonus = 0;
                     totalEmployeeCPF = 0;
                     totalMBMF = 0;
                     totalSINDA = 0;
                     totalCDAC = 0;
                     totalEmployerCPF = 0;
                     totalFWL = 0;
                     totalSDL = 0;
                     totalTransport = 0;
                     totalLaundry = 0;
                     totalAccom = 0;
                     totalMidMonth = 0;
                     totalAdvance = 0;
                     totalNSPayment = 0;
                     totalTax = 0;
                     totalOthers = 0;
                     totalTel = 0;
                    DataTable dt2=GetNonCPFData();
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    { %>
                <tr style="text-align: center;">
                    <td class="td_top_left_bottom" style="text-align:center"><%=i+1 %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Name"] %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["BankCode"] %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["AccNo"] %></td>
                    <td class="td_top_left_bottom"> <%=dt2.Rows[i]["Amt"] %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Days"] %></td>
                    <td class="td_top_left_bottom"> <%=dt2.Rows[i]["LeaveAmt1"] %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Days1"] %></td>
                    <td class="td_top_left_bottom"> -<%=dt2.Rows[i]["LeaveAmt2"] %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["LeaveAmt3"] %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt1"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["EmployeeRate"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt2"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt3"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt4"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt5"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["EmployerRate"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt6"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt7"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt8"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt9"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt10"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt11"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt12"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt13"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt14"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt15"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt16"]  %></td>
                    <td class="td_top_left_bottom"><%=dt2.Rows[i]["Amt17"]  %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom_right"></td>
                </tr>
                <% 
                        totalSalary += SafeValue.SafeDecimal(dt2.Rows[i]["Amt"]);
                        totalLeave1 += SafeValue.SafeDecimal(dt2.Rows[i]["LeaveAmt1"]);
                        totalLeave2 -= SafeValue.SafeDecimal(dt2.Rows[i]["LeaveAmt2"]);
                        totalLeave3 += SafeValue.SafeDecimal(dt2.Rows[i]["LeaveAmt3"]);
                        totalBonus += SafeValue.SafeDecimal(dt2.Rows[i]["Amt1"]);
                        subTotal += (SafeValue.SafeDecimal(dt2.Rows[i]["LeaveAmt2"]) + SafeValue.SafeDecimal(dt2.Rows[i]["LeaveAmt3"]) + SafeValue.SafeDecimal(dt2.Rows[i]["Amt1"]));
                        totalEmployeeCPF += SafeValue.SafeDecimal(dt2.Rows[i]["Amt2"]);
                        totalMBMF += SafeValue.SafeDecimal(dt2.Rows[i]["Amt3"]);
                        totalSINDA += SafeValue.SafeDecimal(dt2.Rows[i]["Amt4"]);
                        totalCDAC += SafeValue.SafeDecimal(dt2.Rows[i]["Amt5"]);
                        totalEmployerCPF += SafeValue.SafeDecimal(dt2.Rows[i]["Amt6"]);
                        totalFWL += SafeValue.SafeDecimal(dt2.Rows[i]["Amt7"]);
                        totalSDL += SafeValue.SafeDecimal(dt2.Rows[i]["Amt8"]);
                        totalTransport += SafeValue.SafeDecimal(dt2.Rows[i]["Amt9"]);
                        totalLaundry += SafeValue.SafeDecimal(dt2.Rows[i]["Amt10"]);
                        totalAccom += SafeValue.SafeDecimal(dt2.Rows[i]["Amt11"]);
                        totalMidMonth += SafeValue.SafeDecimal(dt2.Rows[i]["Amt12"]);
                        totalAdvance += SafeValue.SafeDecimal(dt2.Rows[i]["Amt13"]);
                        totalNSPayment += SafeValue.SafeDecimal(dt2.Rows[i]["Amt14"]);
                        totalTax += SafeValue.SafeDecimal(dt2.Rows[i]["Amt15"]);
                        totalOthers += SafeValue.SafeDecimal(dt2.Rows[i]["Amt16"]);
                        totalTel += SafeValue.SafeDecimal(dt2.Rows[i]["Amt17"]);
                }
                    grandSalary += totalSalary;
                    grandLeave1 += totalLeave1;
                    grandLeave2 += totalLeave2;
                    grandLeave3 += totalLeave3;
                    grandBonus += totalBonus;
                    subGrand += subTotal;
                    grandEmployeeCPF += totalEmployeeCPF;
                    grandMBMF += totalMBMF;
                    grandSINDA += totalSINDA;
                    grandCDAC += totalCDAC;
                    grandEmployerCPF += totalEmployerCPF;
                    grandFWL += totalFWL;
                    grandSDL += totalSDL;
                    grandTransport += totalTransport;
                    grandLaundry += totalLaundry;
                    grandAccom += totalAccom;
                    grandMidMonth += totalMidMonth;
                    grandAdvance += totalAdvance;
                    grandNSPayment += totalNSPayment;
                    grandTax += totalTax;
                    grandOthers += totalOthers;
                    grandTel += totalTel;   
                %>
                <tr style="background-color:#9fa597;text-align: center;">
                    <td class="td_top_left_bottom"><%=dt2.Rows.Count %></td>
                    <td class="td_top_left_bottom">Total Amount</td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"> <%=totalSalary %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave1 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave2 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalLeave3 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalBonus %></td>
                    <td class="td_top_left_bottom"><%=subTotal %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalEmployeeCPF %></td>
                    <td class="td_top_left_bottom"><%=totalMBMF %></td>
                    <td class="td_top_left_bottom"><%=totalSINDA %></td>
                    <td class="td_top_left_bottom"><%=totalCDAC %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalEmployerCPF %></td>
                    <td class="td_top_left_bottom"><%=totalFWL %></td>
                    <td class="td_top_left_bottom"><%=totalSDL %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalTransport %></td>
                    <td class="td_top_left_bottom"><%=totalLaundry %></td>
                    <td class="td_top_left_bottom"><%=totalAccom %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=totalMidMonth %></td>
                    <td class="td_top_left_bottom"><%=totalAdvance %></td>
                    <td class="td_top_left_bottom"><%=totalNSPayment %></td>
                    <td class="td_top_left_bottom"><%=totalTax %></td>
                    <td class="td_top_left_bottom"><%=totalOthers %></td>
                    <td class="td_top_left_bottom"><%=totalTel %></td>
                     <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom_right"></td>
                </tr>
                 <tr>
                    <td colspan="40"></td>
                </tr>
                <tr style="background-color:#9fa597;text-align: center;">
                    <td class="td_top_left_bottom"><%=dt.Rows.Count + dt1.Rows.Count+dt2.Rows.Count %></td>
                    <td class="td_top_left_bottom">Grand Total</td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=grandSalary %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=grandLeave1 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=grandLeave2 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=grandLeave3 %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=grandBonus %></td>
                    <td class="td_top_left_bottom"><%=subGrand %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=grandEmployeeCPF %></td>
                    <td class="td_top_left_bottom"><%=grandMBMF %></td>
                    <td class="td_top_left_bottom"><%=grandSINDA %></td>
                    <td class="td_top_left_bottom"><%=grandCDAC %></td>
                    <td class="td_top_left_bottom"></td>
                    <td class="td_top_left_bottom"><%=grandEmployerCPF %></td>
                    <td class="td_top_left_bottom"><%=grandFWL %></td>
                    <td class="td_top_left_bottom"><%=grandSDL %></td>
                    <td class="td_top_left_bottom"><%=grandCPF %></td>
                    <td class="td_top_left_bottom"><%=grandOverall %></td>
                    <td class="td_top_left_bottom"><%=grandTransport %></td>
                    <td class="td_top_left_bottom"><%=grandLaundry %></td>
                    <td class="td_top_left_bottom"><%=grandAccom %></td>
                    <td class="td_top_left_bottom"><%=grandGrossSalary %></td>
                    <td class="td_top_left_bottom"><%=grandMidMonth %></td>
                    <td class="td_top_left_bottom"><%=grandAdvance %></td>
                    <td class="td_top_left_bottom"><%=grandNSPayment %></td>
                    <td class="td_top_left_bottom"><%=grandTax %></td>
                    <td class="td_top_left_bottom"><%=grandOthers %></td>
                    <td class="td_top_left_bottom"><%=grandTel %></td>
                    <td class="td_top_left_bottom"><%=grandNet1 %></td>
                    <td class="td_top_left_bottom"><%=grandNet2 %></td>
                    <td class="td_top_left_bottom"><%=grandNet3 %></td>
                    <td class="td_top_left_bottom"><%=grandNet4 %></td>
                    <td class="td_top_left_bottom"><%=grandNet5 %></td>
                    <td class="td_top_left_bottom_right"><%=grandNet6 %></td>
                </tr>
            </table>
        </div>
        
    </div>
    </form>
</body>
</html>
