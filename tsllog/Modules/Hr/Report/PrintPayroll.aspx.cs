using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ReportJob_PrintPayroll : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_Satrt.Date = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            this.date_End.Date = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.AddMonths(1).Day);
            btn_search_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            btn_search_Click(null, null);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

    }
    public DataTable GetCPFData() {
        string  dateFrom = date_Satrt.Date.ToString("yyyy-MM-dd");
        string  dateTo = date_End.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select * from(select mast.Id,mast.FromDate,p.IsCPF,mast.ToDate,mast.Amt,mast.StatusCode,p.Name,tab_bank.BankCode,tab_bank.AccNo,tab_amt1.Amt as Amt1,
tab_amt2.Amt as Amt2,tab_amt3.Amt as Amt3,tab_amt4.Amt as Amt4,tab_amt5.Amt as Amt5,tab_amt6.Amt as Amt6,tab_amt7.Amt as Amt7,tab_amt8.Amt as Amt8,tab_amt9.Amt as Amt9
,tab_amt10.Amt as Amt10,tab_amt11.Amt as Amt11,tab_amt12.Amt as Amt12,tab_amt13.Amt as Amt13,tab_amt14.Amt as Amt14,tab_amt15.Amt as Amt15,tab_amt16.Amt as Amt16,tab_amt17.Amt as Amt17,
tab_amt18.Amt as Amt18,tab_rate1.Rate as EmployerRate,tab_rate2.Rate as EmployeeRate,tab_leave.Days,(tab_leave.Days*tab_amt20.Amt) as LeaveAmt1,tab_leave1.Days as Days1,(tab_leave1.Days*tab_amt20.Amt) as LeaveAmt2
,0 as LeaveAmt3
from Hr_Payroll mast inner join Hr_Person p on mast.Person=p.Id
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
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{0}' and Date2<='{1}' and LeaveType!='PL') as tab_leave on tab_leave.Person=p.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{0}' and Date2<='{1}' and LeaveType='PL') as tab_leave1 on tab_leave1.Person=p.Id
left join (select top 1 CPF1,CPF2,Person from Hr_cpf order by Id asc) as tab_cpf on tab_cpf.Person=mast.Id 
left join (select top 1 ResignDate,Person,BeginDate from Hr_PersonDet1 order by Id asc) as tab_line on tab_line.Person=mast.Id 
left join (select top 1 BankCode,Person,AccNo from Hr_PersonDet3  where IsPayroll='1' order by Id ) as tab_bank on tab_bank.Person=p.Id
left join (select EmployeeRate,PayItem,Rate from Hr_Rate where FromDate>='{0}' and ToDate<='{1}' and PayItem='EmployerCPF') as tab_rate1 on tab_rate1.PayItem=tab_amt6.ChgCode
left join (select EmployeeRate,PayItem,Rate from Hr_Rate where FromDate>='{0}' and ToDate<='{1}' and PayItem='EmployeeCPF') as tab_rate2 on tab_rate2.PayItem=tab_amt2.ChgCode
) as tab where IsCPF='Yes' and (FromDate>='{0}' and ToDate<='{1}' )", dateFrom, dateTo);
       DataTable dt = ConnectSql.GetTab(sql);
       return dt;
    }
    public DataTable GetDirectorsData()
    {
        string dateFrom = date_Satrt.Date.ToString("yyyy-MM-dd");
        string dateTo = date_End.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select * from(select mast.Id,mast.FromDate,p.IsCPF,mast.ToDate,mast.Amt,mast.StatusCode,p.Name,tab_bank.BankCode,tab_bank.AccNo,tab_amt1.Amt as Amt1,
tab_amt2.Amt as Amt2,tab_amt3.Amt as Amt3,tab_amt4.Amt as Amt4,tab_amt5.Amt as Amt5,tab_amt6.Amt as Amt6,tab_amt7.Amt as Amt7,tab_amt8.Amt as Amt8,tab_amt9.Amt as Amt9
,tab_amt10.Amt as Amt10,tab_amt11.Amt as Amt11,tab_amt12.Amt as Amt12,tab_amt13.Amt as Amt13,tab_amt14.Amt as Amt14,tab_amt15.Amt as Amt15,tab_amt16.Amt as Amt16,tab_amt17.Amt as Amt17,
tab_amt18.Amt as Amt18,tab_rate1.Rate as EmployerRate,tab_rate2.Rate as EmployeeRate,tab_leave.Days,(tab_leave.Days*tab_amt20.Amt) as LeaveAmt1,tab_leave1.Days as Days1,(tab_leave1.Days*tab_amt20.Amt) as LeaveAmt2
,0 as LeaveAmt3
from Hr_Payroll mast inner join Hr_Person p on mast.Person=p.Id and p.HrRole='Directors'
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
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{0}' and Date2<='{1}' and LeaveType!='PL') as tab_leave on tab_leave.Person=p.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{0}' and Date2<='{1}' and LeaveType='PL') as tab_leave1 on tab_leave1.Person=p.Id
left join (select top 1 CPF1,CPF2,Person from Hr_cpf order by Id asc) as tab_cpf on tab_cpf.Person=mast.Id 
left join (select top 1 ResignDate,Person,BeginDate from Hr_PersonDet1 order by Id asc) as tab_line on tab_line.Person=mast.Id 
left join (select top 1 BankCode,Person,AccNo from Hr_PersonDet3  where IsPayroll='1' order by Id ) as tab_bank on tab_bank.Person=p.Id
left join (select EmployeeRate,PayItem,Rate from Hr_Rate where FromDate>='{0}' and ToDate<='{1}' and PayItem='EmployerCPF') as tab_rate1 on tab_rate1.PayItem=tab_amt6.ChgCode
left join (select EmployeeRate,PayItem,Rate from Hr_Rate where FromDate>='{0}' and ToDate<='{1}' and PayItem='EmployeeCPF') as tab_rate2 on tab_rate2.PayItem=tab_amt2.ChgCode
) as tab where IsCPF='Yes' and (FromDate>='{0}' and ToDate<='{1}' )", dateFrom, dateTo);
        DataTable dt = ConnectSql.GetTab(sql);
        return dt;
    }
    public DataTable GetNonCPFData()
    {
        string dateFrom = date_Satrt.Date.ToString("yyyy-MM-dd");
        string dateTo = date_End.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select * from(select mast.Id,mast.FromDate,p.IsCPF,mast.ToDate,mast.Amt,mast.StatusCode,p.Name,tab_bank.BankCode,tab_bank.AccNo,tab_amt1.Amt as Amt1,
tab_amt2.Amt as Amt2,tab_amt3.Amt as Amt3,tab_amt4.Amt as Amt4,tab_amt5.Amt as Amt5,tab_amt6.Amt as Amt6,tab_amt7.Amt as Amt7,tab_amt8.Amt as Amt8,tab_amt9.Amt as Amt9
,tab_amt10.Amt as Amt10,tab_amt11.Amt as Amt11,tab_amt12.Amt as Amt12,tab_amt13.Amt as Amt13,tab_amt14.Amt as Amt14,tab_amt15.Amt as Amt15,tab_amt16.Amt as Amt16,tab_amt17.Amt as Amt17,
tab_amt18.Amt as Amt18,tab_rate1.Rate as EmployerRate,tab_rate2.Rate as EmployeeRate,tab_leave.Days,(tab_leave.Days*tab_amt20.Amt) as LeaveAmt1,tab_leave1.Days as Days1,(tab_leave1.Days*tab_amt20.Amt) as LeaveAmt2
,0 as LeaveAmt3
from Hr_Payroll mast inner join Hr_Person p on mast.Person=p.Id
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
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{0}' and Date2<='{1}' and LeaveType!='PL') as tab_leave on tab_leave.Person=p.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{0}' and Date2<='{1}' and LeaveType='PL') as tab_leave1 on tab_leave1.Person=p.Id
left join (select top 1 CPF1,CPF2,Person from Hr_cpf order by Id asc) as tab_cpf on tab_cpf.Person=mast.Id 
left join (select top 1 ResignDate,Person,BeginDate from Hr_PersonDet1 order by Id asc) as tab_line on tab_line.Person=mast.Id 
left join (select top 1 BankCode,Person,AccNo from Hr_PersonDet3  where IsPayroll='1' order by Id ) as tab_bank on tab_bank.Person=p.Id
left join (select EmployeeRate,PayItem,Rate from Hr_Rate where FromDate>='{0}' and ToDate<='{1}' and PayItem='EmployerCPF') as tab_rate1 on tab_rate1.PayItem=tab_amt6.ChgCode
left join (select EmployeeRate,PayItem,Rate from Hr_Rate where FromDate>='{0}' and ToDate<='{1}' and PayItem='EmployeeCPF') as tab_rate2 on tab_rate2.PayItem=tab_amt2.ChgCode
) as tab where IsCPF='No' and (FromDate>='{0}' and ToDate<='{1}' )", dateFrom, dateTo);
        DataTable dt = ConnectSql.GetTab(sql);
        return dt;
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\Aspose.lic"));

        Workbook workbook = new Workbook();
        Worksheet worksheet = workbook.Worksheets[0];
        worksheet.Name = "EmployeeDatabase";

        #region cell style
        Cells cells = worksheet.Cells;
        Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
        Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()];
        Aspose.Cells.Style style2 = workbook.Styles[workbook.Styles.Add()];
        style.Font.Name = "Arial";//文字字体 ,宋体
        style.Font.Size = 10;//文字大小  
        style.Font.IsBold = true;//粗体
        style.Borders[BorderType.TopBorder].Color = Color.Black;
        style.Borders[BorderType.BottomBorder].Color = Color.Black;
        style.Borders[BorderType.LeftBorder].Color = Color.Black;
        style.Borders[BorderType.RightBorder].Color = Color.Black;
        style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style.HorizontalAlignment = TextAlignmentType.Center;//文字居中  

        style1.Font.Name = "Arial";//文字字体 ,宋体
        style1.Font.Size = 10;//文字大小  
        style1.Font.IsBold = false;//粗体
        style1.Borders[BorderType.TopBorder].Color = Color.Black;
        style1.Borders[BorderType.BottomBorder].Color = Color.Black;
        style1.Borders[BorderType.LeftBorder].Color = Color.Black;
        style1.Borders[BorderType.RightBorder].Color = Color.Black;
        style1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style1.HorizontalAlignment = TextAlignmentType.Center;//文字居中  


        style2.Font.Name = "Arial";//文字字体 ,宋体
        style2.Font.Size = 10;//文字大小  
        style2.Font.IsBold = true;//粗体
        style2.Borders[BorderType.TopBorder].Color = Color.Black;
        style2.Borders[BorderType.BottomBorder].Color = Color.Black;
        style2.Borders[BorderType.LeftBorder].Color = Color.Black;
        style2.Borders[BorderType.RightBorder].Color = Color.Black;
        style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中  

        #endregion

        cells[0, 0].PutValue("Payroll for "+PrintDate());

        #region cells 1
        cells[1, 0].PutValue("");
        Range range = worksheet.Cells.CreateRange(1, 0, 1, 5); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 5].PutValue("Deductions");
        range = worksheet.Cells.CreateRange(1, 5, 1, 4); //Merge the cells /合并单元格
        range.Merge();
        cells[1,9].PutValue("Additional");
        range = worksheet.Cells.CreateRange(1, 9, 1, 4); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 13].PutValue("Sun-Total");
        range = worksheet.Cells.CreateRange(1, 13, 3, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 14].PutValue("CPF Contributions");
        range = worksheet.Cells.CreateRange(1, 14, 1, 11); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 25].PutValue("Expenses Reimbursement");
        range = worksheet.Cells.CreateRange(1, 25, 1, 3); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 28].PutValue("Employee Gross Salary");
        range = worksheet.Cells.CreateRange(1, 28, 3, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 29].PutValue("Deductions");
        range= worksheet.Cells.CreateRange(1, 29, 1, 6); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 35].PutValue("Employee");
        range = worksheet.Cells.CreateRange(1, 35, 1, 2); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 37].PutValue("Employer Gross Pay");
        range= worksheet.Cells.CreateRange(1, 37, 3, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 38].PutValue("Payment Mode");
        range = worksheet.Cells.CreateRange(1, 38, 3, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 39].PutValue("NetAmount(GIRO)");
        range = worksheet.Cells.CreateRange(1, 39, 3, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[1, 40].PutValue("Net Amount(CHEQUE)");
        range = worksheet.Cells.CreateRange(1, 40, 3, 1); //Merge the cells /合并单元格
        range.Merge();

        #endregion
        
        for (int i = 0; i < 41;i++ )
        {
            cells[1, i].SetStyle(style);
            
        }

        
        #region cell 2
        cells[2, 0].PutValue("S/N");
        range = worksheet.Cells.CreateRange(2, 0, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 1].PutValue("Employee Name");
        range = worksheet.Cells.CreateRange(2, 1, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 2].PutValue("Bank");
        range = worksheet.Cells.CreateRange(2, 2, 1, 2); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 4].PutValue("Basic Pay");
        range = worksheet.Cells.CreateRange(2, 4, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 5].PutValue("Unpaid Leave/Absent");
        range = worksheet.Cells.CreateRange(2, 5, 1, 2); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 7].PutValue("Absent");
        range = worksheet.Cells.CreateRange(2, 7, 1, 2); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 9].PutValue("Leave Pay");
        range = worksheet.Cells.CreateRange(2, 9, 1, 2); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 11].PutValue("Pay-In-Lieu");
        range = worksheet.Cells.CreateRange(2, 11, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 12].PutValue("9Bonus");
        range = worksheet.Cells.CreateRange(2, 12, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 14].PutValue("Employee");
        range = worksheet.Cells.CreateRange(2, 14, 1, 5); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 19].PutValue("Employer");
        range = worksheet.Cells.CreateRange(2, 19, 1, 4); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 23].PutValue("Employer&Empolyee Total");
        range = worksheet.Cells.CreateRange(2, 23, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 24].PutValue("Ovarall Total");
        range = worksheet.Cells.CreateRange(2, 24, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 25].PutValue("Transort");
        range = worksheet.Cells.CreateRange(2, 25, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 26].PutValue("Laudry");
        range = worksheet.Cells.CreateRange(2, 26, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 27].PutValue("Accom");
        range = worksheet.Cells.CreateRange(2, 27, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 29].PutValue("Mid Month Salary");
        range = worksheet.Cells.CreateRange(2, 29, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 30].PutValue("Advance Salary");
        range = worksheet.Cells.CreateRange(2, 30, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 31].PutValue("NS Payment");
        range = worksheet.Cells.CreateRange(2, 31, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 32].PutValue("Withholding Tax");
        range = worksheet.Cells.CreateRange(2, 32, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 33].PutValue("Others");
        range = worksheet.Cells.CreateRange(2, 33, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 34].PutValue("Tel Overusage");
        range = worksheet.Cells.CreateRange(2, 34, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 35].PutValue("Nett Amount");
        range = worksheet.Cells.CreateRange(2, 35, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[2, 36].PutValue("Nett Amount Payable");
        range = worksheet.Cells.CreateRange(2, 36, 2, 1); //Merge the cells /合并单元格
        range.Merge();

        for (int i = 0; i < 41; i++)
        {
            cells[2, i].SetStyle(style);
        }
        #endregion

        #region cell 3
        cells[3, 2].PutValue("Name");
        cells[3, 3].PutValue("AC/No.");
        cells[3, 5].PutValue("No.of Days");
        cells[3, 6].PutValue("Amount");
        cells[3, 7].PutValue("No. of Days");
        cells[3, 8].PutValue("Amount");
        cells[3, 9].PutValue("No. of Days");
        cells[3, 10].PutValue("Amount");
        cells[3, 14].PutValue("Percentage");
        cells[3, 15].PutValue("CPF");
        cells[3, 16].PutValue("MBF");
        cells[3, 17].PutValue("Sinda");
        cells[3, 18].PutValue("CDAC");
        cells[3, 20].PutValue("CPF");
        cells[3, 21].PutValue("FWL");
        cells[3, 22].PutValue("SDL");
        cells[3, 25].PutValue("Transport");
        cells[3, 26].PutValue("Laundry");
        cells[3, 27].PutValue("Accom");
        for (int i = 0; i < 41; i++)
        {
            cells[3, i].SetStyle(style);
        }
        #endregion


        range = worksheet.Cells.CreateRange(4, 0, 1, 40); //Merge the cells /合并单元格
        range.Merge();
        #region cell 5
        cells[5, 0].PutValue("CPF Employee");
        range = worksheet.Cells.CreateRange(5, 0, 1, 40); //Merge the cells /合并单元格
        range.Merge();
        #endregion

        int a = 6;
        DataTable dt = GetCPFData();
        #region Set Column Width
        cells.SetColumnWidth(0, 5);
        cells.SetColumnWidth(1, 20);
        cells.SetColumnWidth(2, 8);
        cells.SetColumnWidth(3, 15);
        cells.SetColumnWidth(4, 10);
        cells.SetColumnWidth(5, 10);
        cells.SetColumnWidth(6, 15);
        cells.SetColumnWidth(7, 10);
        cells.SetColumnWidth(8, 15);
        cells.SetColumnWidth(9, 10);
        cells.SetColumnWidth(10, 15);
        cells.SetColumnWidth(11, 16);
        cells.SetColumnWidth(12, 16);
        cells.SetColumnWidth(13, 15);
        #endregion
       
        #region CPF Employee Data
        for (int n = 0; n < dt.Rows.Count; n++)
        {
            cells[n + a, 0].PutValue(n + 1);
            cells[n + a, 1].PutValue(SafeValue.SafeString(dt.Rows[n]["Name"]));
            cells[n + a, 2].PutValue(SafeValue.SafeString(dt.Rows[n]["BankCode"]));
            cells[n + a, 3].PutValue(SafeValue.SafeString(dt.Rows[n]["AccNo"]));
            cells[n + a, 4].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt"]));
            //cells[n + a, 5].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            //cells[n + a, 6].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            //cells[n + a, 7].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            //cells[n + a, 8].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            //cells[n + a, 9].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            //cells[n + a, 10].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            //cells[n + a, 11].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 12].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt1"]));
            //cells[n + a, 13].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 14].PutValue(SafeValue.SafeString(dt.Rows[n]["EmployeeRate"]));
            cells[n + a, 15].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt2"]));
            cells[n + a, 16].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt3"]));
            cells[n + a, 17].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt4"]));
            cells[n + a, 18].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt5"]));
            cells[n + a, 19].PutValue(SafeValue.SafeString(dt.Rows[n]["EmployerRate"]));
            cells[n + a, 20].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt6"]));
            cells[n + a, 21].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt7"]));
            cells[n + a, 22].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt8"]));
            cells[n + a, 23].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 24].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 25].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt9"]));
            cells[n + a, 26].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt10"]));
            cells[n + a, 27].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt11"]));
            cells[n + a, 28].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 29].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt12"]));
            cells[n + a, 30].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt13"]));
            cells[n + a, 31].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt14"]));
            cells[n + a, 32].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt15"]));
            cells[n + a, 33].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt16"]));
            cells[n + a, 34].PutValue(SafeValue.SafeString(dt.Rows[n]["Amt17"]));
            cells[n + a, 35].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 36].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 37].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 38].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 39].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            cells[n + a, 40].PutValue(SafeValue.SafeString(dt.Rows[n][""]));
            for (int i = 0; i < 41;i++ )
            {
                cells[n + a, i].SetStyle(style1);
            }
            a=a+n;
        }
        #endregion

        a = a + 1;
        cells[a, 0].PutValue(dt.Rows.Count);
        cells[a, 1].PutValue("Total Amount");
        for (int i = 0; i < 41; i++)
        {
            cells[a, i].SetStyle(style1);
        }
        #region cell a
        a = a + 2;
        cells[a, 0].PutValue("Directors");
        range = worksheet.Cells.CreateRange(a, 0, 1, 2); //Merge the cells /合并单元格
        range.Merge();
        #endregion

        DataTable dt1 = GetDirectorsData();
       a=a + 1;
       #region Directors Data
       for (int n = 0; n < dt1.Rows.Count; n++)
       {
           cells[n + a, 0].PutValue(n + 1);
           cells[n + a, 1].PutValue(SafeValue.SafeString(dt1.Rows[n]["Name"]));
           cells[n + a, 2].PutValue(SafeValue.SafeString(dt1.Rows[n]["BankCode"]));
           cells[n + a, 3].PutValue(SafeValue.SafeString(dt1.Rows[n]["AccNo"]));
           cells[n + a, 4].PutValue(SafeValue.SafeString(dt1.Rows[n]["Amt"]));
           //cells[n + a, 5].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 6].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 7].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 8].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 9].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 10].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 11].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 12].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 13].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 14].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 15].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 16].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 17].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 18].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 19].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 20].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 21].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 22].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 23].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 24].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 25].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 26].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 27].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 28].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 29].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 30].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 31].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 32].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 33].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 34].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 35].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 36].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 38].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 39].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           //cells[n + a, 40].PutValue(SafeValue.SafeString(dt1.Rows[n][""]));
           for (int i = 0; i < 41; i++)
           {
               cells[n + a, i].SetStyle(style1);
           }
           a = a + n;
       }
       #endregion
       a = a + 1;
       cells[a, 0].PutValue(dt1.Rows.Count);
       cells[a, 1].PutValue("Total Amount");
       for (int i = 0; i < 41; i++)
       {
           cells[a, i].SetStyle(style1);
       }

       #region cell a
       a = a + 2;
       cells[a, 0].PutValue("Non-CPF Employee");
       range = worksheet.Cells.CreateRange(a, 0, 1, 2); //Merge the cells /合并单元格
       range.Merge();
       #endregion

       DataTable dt2 = GetNonCPFData();
       #region Non-CPF Employee Data
       a = a + 1;
       for (int n = 0; n < dt2.Rows.Count; n++)
       {
           cells[n + a, 0].PutValue(n + 1);
           cells[n + a, 1].PutValue(SafeValue.SafeString(dt2.Rows[n]["Name"]));
           cells[n + a, 2].PutValue(SafeValue.SafeString(dt2.Rows[n]["BankCode"]));
           cells[n + a, 3].PutValue(SafeValue.SafeString(dt2.Rows[n]["AccNo"]));
           cells[n + a, 4].PutValue(SafeValue.SafeString(dt2.Rows[n]["Amt"]));
           //cells[n + a, 5].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 6].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 7].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 8].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 9].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 10].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 11].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 12].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 13].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 14].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 15].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 16].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 17].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 18].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 19].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 20].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 21].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 22].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 23].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 24].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 25].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 26].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 27].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 28].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 29].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 30].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 31].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 32].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 33].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 34].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 35].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 36].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 38].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 39].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           //cells[n + a, 40].PutValue(SafeValue.SafeString(dt2.Rows[n][""]));
           for (int i = 0; i < 41; i++)
           {
               cells[n + a, i].SetStyle(style1);
           }
           a = a + n;
       }
       #endregion
       a = a + 1;
       cells[a, 0].PutValue(dt2.Rows.Count);
       cells[a, 1].PutValue("Total Amount");
       for (int i = 0; i < 41; i++)
       {
           cells[a, i].SetStyle(style1);
       }

       #region cell a
       a = a + 2;
       cells[a, 0].PutValue(dt.Rows.Count+dt1.Rows.Count+dt2.Rows.Count);
       cells[a, 1].PutValue("Grand Total");
       for (int i = 0; i < 41; i++)
       {
           cells[a, i].SetStyle(style1);
       }
       #endregion
       string locaPath = MapPath("~/Excel");
       if (!Directory.Exists(locaPath))
           Directory.CreateDirectory(locaPath);
       string path0 = string.Format("~/Excel/Payroll_{0:yyyyMMdd}.xlsx",
       DateTime.Now.ToString("yyyyMMdd-HHmmss") ?? "01-01-2014"); //Request.QueryString["d"]
       string path = HttpContext.Current.Server.MapPath(path0); //POD_RECORD
       //workbook.Save(path);

       System.IO.MemoryStream ms = workbook.SaveToStream(); //生成数据流 
       byte[] bt = ms.ToArray();
       workbook.Save(path);
       Response.Redirect(path0.Substring(1));
    }
    public string PrintDate()
    {
        return date_Satrt.Date.ToString("MMMM yyyy");
    }
}