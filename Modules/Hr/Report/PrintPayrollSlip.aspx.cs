using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helper;

public partial class Modules_Hr_Report_PrintPayrollSlip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
    }
    public DataTable GetData()
    {
        string no ="0";
        string fromDate = "";
        string toDate = "";
        if (Request.QueryString["no"] != null && Request.QueryString["from"] != null && Request.QueryString["to"] != null)
        {
            no=SafeValue.SafeString(Request.QueryString["no"]);
            string dateFrom = Helper.Safe.SafeDateStr(Request.QueryString["from"]);
            string dateTo = Helper.Safe.SafeDateStr(Request.QueryString["to"]);
            DateTime from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            fromDate = from.Date.ToString("yyyy-MM-dd");
            toDate = to.Date.ToString("yyyy-MM-dd");
        }
        DateTime date = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.AddMonths(1).Day);
        string sql = string.Format(@"select mast.Person,mast.Id,p.Name,p.Remark4,mast.FromDate,p.IcNo,p.Department,p.HrRole,tab_bank.BankName,tab_begin.BeginDate,tab_bank.AccNo,tab_bank.BankCode,mast.FromDate,mast.ToDate
,month(mast.FromDate) as PayMonth,tab_det.Amt,tab_amt1.Amt as Amt1,tab_amt2.Amt as Amt2,tab_amt3.Amt as Amt3,tab_amt4.Amt as Amt4,tab_amt5.Amt as Amt5,((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0))*tab_rate.Rate*0.01) as Amt6,
tab_amt7.Amt as Amt7,tab_amt8.Amt as Amt8,tab_amt9.Amt as Amt9,tab_amt10.Amt as Amt10,tab_amt11.Amt as Amt11,tab_amt12.Amt as Amt12,tab_amt13.Amt as Amt13,tab_amt14.Amt as Amt14,
tab_amt15.Amt as Amt15,tab_amt16.Amt as Amt16,tab_amt17.Amt as Amt17,tab_amt18.Amt as Amt18,tab_amt19.Amt as Amt19,tab_amt20.Amt as Amt20,tab_amt21.Amt as Amt21,tab_amt22.Amt as Amt22,
(tab_leave.Days*tab_amt20.Amt) as LeaveAmt1,tab_leave1.Days as Days1,(tab_leave1.Days*tab_amt20.Amt) as LeaveAmt2,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))+ISNULL(tab_amt13.Amt,0)+isnull(tab_amt17.Amt,0)+isnull(tab_amt15.Amt,0)+isnull(tab_amt14.Amt,0)+((isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)))) as NettPayable,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))+isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as NettWage,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))) as GrossWage,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0))) as CPFWage,
(isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as TotalCPF
from Hr_Payroll mast 
left join Hr_Person p on mast.Person=p.Id
left join (select Amt,PayrollId,ChgCode from Hr_PayrollDet where ChgCode='Salary') as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Bonus') as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployeeCPF') as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MBMF') as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SINDA') as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='CDAC') as tab_amt5 on tab_amt5.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployerCPF') as tab_amt6 on tab_amt6.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='FWL') as tab_amt7 on tab_amt7.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SDL') as tab_amt8 on tab_amt8.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Transport') as tab_amt9 on tab_amt9.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='LaundryExpense') as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Accommodation') as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MinMonthSalary') as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='AdvanceSalary') as tab_amt13 on tab_amt13.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='NSPayment') as tab_amt14 on tab_amt14.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='WitholdingTax') as tab_amt15 on tab_amt15.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Others') as tab_amt16 on tab_amt16.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='TelParking') as tab_amt17 on tab_amt17.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Levy') as tab_amt18 on tab_amt18.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Claim') as tab_amt19 on tab_amt19.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Leave') as tab_amt20 on tab_amt20.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Allowances') as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Reimbursement') as tab_amt22 on tab_amt22.PayrollId=mast.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{1}' and Date2<='{2}' and LeaveType!='PL') as tab_leave on tab_leave.Person=p.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{1}' and Date2<='{2}' and LeaveType='PL') as tab_leave1 on tab_leave1.Person=p.Id
left join (select top 1 BankCode,BankName,Person,AccNo from Hr_PersonDet3 where IsPayroll='True' order by CreateDateTime desc) as tab_bank on tab_bank.Person=p.Id
left join (select PayItem,Rate,RateType,Age from Hr_Rate) as tab_rate on tab_rate.PayItem='EmployerCPF'
left join(select top 1 BeginDate,Person from Hr_PersonDet1  order by CreateDateTime desc) as tab_begin on tab_begin.Person=p.Id
left join(select sum(Amt) as TotalAmt,Person from Hr_Payroll group by Person) as tab_total on tab_total.Person=mast.Id 
where mast.Id={0} and FromDate>='{1}' and ToDate<='{2}'", no, fromDate, toDate);
       // throw new Exception(sql);
        return ConnectSql.GetTab(sql);
        
    }
    public static Decimal SafeDecimal(object s)
    {
        Decimal dec = 0;
        try
        {
            dec = Convert.ToDecimal(s);
        }
        catch
        {
            dec = 0;
        }
        return dec;
    }
    public static string SafeAccountSz(object s)
    {

        string r = "";
        decimal d = SafeDecimal(s);
        if (d == 0)
            r = "0.00";
        if (d > 0)
            r = string.Format("{0:#,##0.00}", d);
        if (d < 0)
            r = string.Format("{0:#,##0.00}", -1 * d);
        return r;

    }
    public string TotalNettPayable(string person) {
        string value = "";
        string dateFrom = Helper.Safe.SafeDateStr(Request.QueryString["from"]);
        string dateTo = Helper.Safe.SafeDateStr(Request.QueryString["to"]);
        DateTime from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string fromDate = from.AddMonths(-from.Month + 1).AddDays(-from.Day + 1).ToString("yyyy-MM-dd");
        string  toDate = to.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select sum(NettPayable) as TotalAmt,Person from(select Person,FromDate,ToDate,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)
+isnull(tab_amt22.Amt,0))+ISNULL(tab_amt13.Amt,0)+isnull(tab_amt17.Amt,0)+isnull(tab_amt15.Amt,0)
+isnull(tab_amt14.Amt,0)+((isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)
+isnull(tab_amt5.Amt,0)))) as NettPayable
from Hr_Payroll mast 
left join (select Amt,PayrollId,ChgCode from Hr_PayrollDet where ChgCode='Salary') as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Bonus') as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployeeCPF') as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MBMF') as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SINDA') as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='CDAC') as tab_amt5 on tab_amt5.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='LaundryExpense') as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Accommodation') as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MinMonthSalary') as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='AdvanceSalary') as tab_amt13 on tab_amt13.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='NSPayment') as tab_amt14 on tab_amt14.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='WitholdingTax') as tab_amt15 on tab_amt15.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='TelOverusage') as tab_amt17 on tab_amt17.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='PayInLieu') as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Reimbursement') as tab_amt22 on tab_amt22.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        value = SafeValue.SafeString(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public string TotalNettWage(string person)
    {
        string value = "";
        string dateFrom = Helper.Safe.SafeDateStr(Request.QueryString["from"]);
        string dateTo = Helper.Safe.SafeDateStr(Request.QueryString["to"]);
        DateTime from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string fromDate = from.AddMonths(-from.Month + 1).AddDays(-from.Day + 1).ToString("yyyy-MM-dd");
        string toDate = to.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select sum(NettWage) as TotalAmt,Person from(select Person,FromDate,ToDate,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))+isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as NettWage
from Hr_Payroll mast 
left join (select Amt,PayrollId,ChgCode from Hr_PayrollDet where ChgCode='Salary') as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Bonus') as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployeeCPF') as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MBMF') as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SINDA') as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='CDAC') as tab_amt5 on tab_amt5.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployerCPF') as tab_amt6 on tab_amt6.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='FWL') as tab_amt7 on tab_amt7.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SDL') as tab_amt8 on tab_amt8.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Transport') as tab_amt9 on tab_amt9.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='LaundryExpense') as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Accommodation') as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MinMonthSalary') as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='AdvanceSalary') as tab_amt13 on tab_amt13.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='NSPayment') as tab_amt14 on tab_amt14.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='WitholdingTax') as tab_amt15 on tab_amt15.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Others') as tab_amt16 on tab_amt16.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='TelOverusage') as tab_amt17 on tab_amt17.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Levy') as tab_amt18 on tab_amt18.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Claim') as tab_amt19 on tab_amt19.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Leave') as tab_amt20 on tab_amt20.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='PayInLieu') as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Reimbursement') as tab_amt22 on tab_amt22.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        value = SafeValue.SafeString(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public string TotalGrossWage(string person)
    {
        string value = "";
        string dateFrom = Helper.Safe.SafeDateStr(Request.QueryString["from"]);
        string dateTo = Helper.Safe.SafeDateStr(Request.QueryString["to"]);
        DateTime from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string fromDate = from.AddMonths(-from.Month + 1).AddDays(-from.Day + 1).ToString("yyyy-MM-dd");
        string toDate = to.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select sum(GrossWage) as TotalAmt,Person from(select Person,FromDate,ToDate,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))) as GrossWage
from Hr_Payroll mast 
left join (select Amt,PayrollId,ChgCode from Hr_PayrollDet where ChgCode='Salary') as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Bonus') as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployeeCPF') as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MBMF') as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SINDA') as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='CDAC') as tab_amt5 on tab_amt5.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployerCPF') as tab_amt6 on tab_amt6.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='FWL') as tab_amt7 on tab_amt7.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SDL') as tab_amt8 on tab_amt8.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Transport') as tab_amt9 on tab_amt9.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='LaundryExpense') as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Accommodation') as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MinMonthSalary') as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='AdvanceSalary') as tab_amt13 on tab_amt13.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='NSPayment') as tab_amt14 on tab_amt14.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='WitholdingTax') as tab_amt15 on tab_amt15.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Others') as tab_amt16 on tab_amt16.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='TelOverusage') as tab_amt17 on tab_amt17.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Levy') as tab_amt18 on tab_amt18.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Claim') as tab_amt19 on tab_amt19.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Leave') as tab_amt20 on tab_amt20.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='PayInLieu') as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Reimbursement') as tab_amt22 on tab_amt22.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        value = SafeValue.SafeString(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public string TotalCPFWage(string person)
    {
        string value = "";
        string dateFrom = Helper.Safe.SafeDateStr(Request.QueryString["from"]);
        string dateTo = Helper.Safe.SafeDateStr(Request.QueryString["to"]);
        DateTime from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string fromDate = from.AddMonths(-from.Month + 1).AddDays(-from.Day + 1).ToString("yyyy-MM-dd");
        string toDate = to.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select sum(CPFWage) as TotalAmt,Person from(select Person,FromDate,ToDate,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0))) as CPFWage
from Hr_Payroll mast 
left join (select Amt,PayrollId,ChgCode from Hr_PayrollDet where ChgCode='Salary') as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Bonus') as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployeeCPF') as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MBMF') as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SINDA') as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='CDAC') as tab_amt5 on tab_amt5.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployerCPF') as tab_amt6 on tab_amt6.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='FWL') as tab_amt7 on tab_amt7.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SDL') as tab_amt8 on tab_amt8.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Transport') as tab_amt9 on tab_amt9.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='LaundryExpense') as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Accommodation') as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MinMonthSalary') as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='AdvanceSalary') as tab_amt13 on tab_amt13.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='NSPayment') as tab_amt14 on tab_amt14.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='WitholdingTax') as tab_amt15 on tab_amt15.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Others') as tab_amt16 on tab_amt16.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='TelOverusage') as tab_amt17 on tab_amt17.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Levy') as tab_amt18 on tab_amt18.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Claim') as tab_amt19 on tab_amt19.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Leave') as tab_amt20 on tab_amt20.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='PayInLieu') as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Reimbursement') as tab_amt22 on tab_amt22.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        value = SafeValue.SafeString(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public string TotalCPF(string person)
    {
        string value = "";
        string dateFrom = Helper.Safe.SafeDateStr(Request.QueryString["from"]);
        string dateTo = Helper.Safe.SafeDateStr(Request.QueryString["to"]);
        DateTime from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string fromDate = from.AddMonths(-from.Month + 1).AddDays(-from.Day + 1).ToString("yyyy-MM-dd");
        string toDate = to.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select sum(TotalCPF) as TotalAmt,Person from(select Person,FromDate,ToDate,
(isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as TotalCPF
from Hr_Payroll mast 
left join (select Amt,PayrollId,ChgCode from Hr_PayrollDet where ChgCode='Salary') as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Bonus') as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployeeCPF') as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MBMF') as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SINDA') as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='CDAC') as tab_amt5 on tab_amt5.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='EmployerCPF') as tab_amt6 on tab_amt6.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='FWL') as tab_amt7 on tab_amt7.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='SDL') as tab_amt8 on tab_amt8.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Transport') as tab_amt9 on tab_amt9.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='LaundryExpense') as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Accommodation') as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MinMonthSalary') as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='AdvanceSalary') as tab_amt13 on tab_amt13.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='NSPayment') as tab_amt14 on tab_amt14.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='WitholdingTax') as tab_amt15 on tab_amt15.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Others') as tab_amt16 on tab_amt16.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='TelOverusage') as tab_amt17 on tab_amt17.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Levy') as tab_amt18 on tab_amt18.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Claim') as tab_amt19 on tab_amt19.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Leave') as tab_amt20 on tab_amt20.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='PayInLieu') as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='Reimbursement') as tab_amt22 on tab_amt22.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        value = SafeValue.SafeString(tab.Rows[0]["TotalAmt"]);
        return value;
    }
}