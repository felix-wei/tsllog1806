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

public partial class Modules_Hr_Report_PrintEmployeeSimplify : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_Employee.Date = DateTime.Today;
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

        string dateTo = "";
        if (date_Employee.Value != null)
        {
            dateTo = date_Employee.Date.ToString("yyyy-MM-dd");
        }
        if (dateTo.Length > 0)
        {

        }
        DataTable tab = GetData(dateTo);
    }
    public DataTable GetData(string date)
    {
        string sql = string.Format(@"select row_number() over(order by Id) as No,Name,IcNo,Country,Gender,Remark4,HrRole,
(case when CONVERT(varchar(100), Date2, 23)<='1900-01-01' then '' else datediff(day,getdate(),Date2) end) as Days,Remark2,
(dbo.fun_GetDate(BirthDay)) as BirthDay,
(CONVERT(nvarchar(100),(DATEDIFF(D,BirthDay,GETDATE())/365))+'years '+dbo.fun_GetBirthday('M',BirthDay,getDate())+'month '+
dbo.fun_GetBirthday('D',BirthDay,getDate())+'days') as Age,(dbo.fun_GetDate(tab_line.BeginDate)) as BeginDate,
((dbo.fun_GetBirthday('Y',BeginDate,ResignDate)+'years '+dbo.fun_GetBirthday('M',BeginDate,ResignDate)+'month '+
dbo.fun_GetBirthday('D',BeginDate,ResignDate)+'days')) as ServiceYears,(dbo.fun_GetDate(tab_line.ResignDate)) as ResignDate
,Address,tab_bank.BankCode,tab_bank.AccNo,('$ '+CONVERT(nvarchar(100),Amount1)) as Salary,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt1.Amt,0))) as Account1,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt2.Amt,0))) as Account2,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt3.Amt,0))) as Account3,
tab_amt8.Amt as CPF1,
tab_amt9.Amt as CPF2,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt4.Amt,0))) as Account4,
tab_amt5.Amt as Account5,
tab_amt6.Amt as Account6,
tab_total.TotalAmt
 from Hr_Person mast 
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='LaundryExpense') as tab_amt1 on tab_amt1.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='Accommodation') as tab_amt2 on tab_amt2.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='WitholdingTax') as tab_amt3 on tab_amt3.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='Levy') as tab_amt4 on tab_amt4.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='MBMF') as tab_amt5 on tab_amt5.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='SINDA') as tab_amt6 on tab_amt6.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='CDAC') as tab_amt7 on tab_amt7.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='EmployerCPF') as tab_amt8 on tab_amt8.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='EmployeeCPF') as tab_amt9 on tab_amt9.Person=mast.Id
left join(select top 1 ResignDate,Person,BeginDate from Hr_PersonDet1 order by Id asc) as tab_line on tab_line.Person=mast.Id 
left join(select sum(Amt) as TotalAmt,Person from Hr_Payroll where ToDate<='{0}' group by Person) as tab_total on tab_total.Person=mast.Id 
left join(select BankCode,Person,AccNo from Hr_PersonDet3  where IsPayroll='1') as tab_bank on tab_bank.Person=mast.Id", date);
        return ConnectSql.GetTab(sql);
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        //btn_search_Click(null, null);

        //gridExport.WriteXlsToResponse("EmployeeDatabase", true);
       string dateTo = date_Employee.Date.ToString("yyyy-MM-dd");
        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\Aspose.lic"));
        DataTable dtTableName = GetData(dateTo);
        Workbook workbook = new Workbook();
        Worksheet worksheet = workbook.Worksheets[0];
        worksheet.Name = "EmployeeDatabase";

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


        cells[0, 0].PutValue(DateTime.Today.Year+" IR8A");

        cells[1, 21].SetStyle(style);
        cells[1, 22].SetStyle(style);

        cells[2, 9].PutValue("Total");
        Range range = worksheet.Cells.CreateRange(2, 9, 3, 1); //Merge the cells /合并单元格
        range.Merge();


        cells[3, 0].PutValue("S/N");
        range = worksheet.Cells.CreateRange(3, 0, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[3, 1].PutValue("Employee Name");
        range = worksheet.Cells.CreateRange(3, 1, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[3, 2].PutValue("NRIC No./ FIN No.");
        range = worksheet.Cells.CreateRange(3, 2, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[3, 3].PutValue("Nationality");
        range = worksheet.Cells.CreateRange(3, 3, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[3, 4].PutValue("Sex");
        range = worksheet.Cells.CreateRange(3, 4, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[3, 5].PutValue("Designation");
        range = worksheet.Cells.CreateRange(3, 5, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[3, 6].PutValue("Date Joined");
        range = worksheet.Cells.CreateRange(3, 6, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[3, 7].PutValue("Date Left");
        range = worksheet.Cells.CreateRange(3, 7, 2, 1); //Merge the cells /合并单元格
        range.Merge();
        cells[3, 8].PutValue("Date of Birth");
        range = worksheet.Cells.CreateRange(3, 8, 2, 1); //Merge the cells /合并单元格
        range.Merge();

        cells[4, 10].PutValue("CPF");
        cells[4, 11].PutValue("MBMF");
        cells[4, 12].PutValue("SINDA");


        for (int i = 0; i < 13;i++ )
        {
            cells[2, i].SetStyle(style);
            cells[3, i].SetStyle(style);
            cells[4, i].SetStyle(style);
            cells.SetColumnWidth(i, 20);
        }

        for (int n = 0; n < dtTableName.Rows.Count; n++)
        {
            cells[n + 5, 0].PutValue(n + 1);
            cells[n + 5, 1].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Name"]));
            cells[n + 5, 2].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["IcNo"]));
            cells[n + 5, 3].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Country"]));
            cells[n + 5, 4].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Gender"]));
            cells[n + 5, 5].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Remark4"]));
            cells[n + 5, 6].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["BeginDate"]));
            cells[n + 5, 7].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["ResignDate"]));
            cells[n + 5, 8].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["BirthDay"]));
            cells[n + 5, 9].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["TotalAmt"]));
            cells[n + 5, 10].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["CPF2"]));
            cells[n + 5, 11].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Account5"]));
            cells[n + 5, 12].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Account6"]));

            cells[n + 5, 0].SetStyle(style1);
            cells[n + 5, 1].SetStyle(style1);
            cells[n + 5, 2].SetStyle(style1);
            cells[n + 5, 3].SetStyle(style1);
            cells[n + 5, 4].SetStyle(style1);
            cells[n + 5, 5].SetStyle(style1);
            cells[n + 5, 6].SetStyle(style1);
            cells[n + 5, 7].SetStyle(style1);
            cells[n + 5, 8].SetStyle(style1);
            cells[n + 5, 9].SetStyle(style1);
            cells[n + 5, 10].SetStyle(style1);
            cells[n + 5, 11].SetStyle(style1);
            cells[n + 5, 12].SetStyle(style1);
        }

        string locaPath = MapPath("~/Excel");
        if (!Directory.Exists(locaPath))
            Directory.CreateDirectory(locaPath);
        string path0 = string.Format("~/Excel/{1}_IR8A_{0:MMdd}.xlsx",
        DateTime.Now.ToString("MMdd-HHmmss") ?? "01-01-2014",DateTime.Today.Year); //Request.QueryString["d"]
        string path = HttpContext.Current.Server.MapPath(path0); //POD_RECORD
        //workbook.Save(path);

        System.IO.MemoryStream ms = workbook.SaveToStream(); //生成数据流 
        byte[] bt = ms.ToArray();
        workbook.Save(path);
        Response.Redirect(path0.Substring(1));

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    public string PrintDate()
    {
        return date_Employee.Date.ToString("yyyy");
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
            r = "-";
        if (d > 0)
            r = string.Format("{0:#,##0.00}", d);
        if (d < 0)
            r = string.Format("{0:#,##0.00}", -1 * d);
        return r;

    }
}