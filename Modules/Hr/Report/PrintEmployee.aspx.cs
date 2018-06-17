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

public partial class ReportJob_PrintEmployee : System.Web.UI.Page
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
        
        string dateFrom = "";
        if (date_Employee.Value != null)
        {
            dateFrom = date_Employee.Date.ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0)
        {
            
        }
        DataTable tab = GetData();
        this.grid.DataSource = tab;
        this.grid.DataBind();
    }
    private DataTable GetData() {
        string sql = string.Format(@"select row_number() over(order by Id) as No,Name,IcNo,(dbo.fun_GetDate(Date2)) as ExpiryDate,Country,Department,IsCPF,
(case when CONVERT(varchar(100), Date2, 23)<='1900-01-01' then '' else datediff(day,getdate(),Date2) end) as Days,Remark2,
(dbo.fun_GetDate(BirthDay)) as BirthDay,
(CONVERT(nvarchar(100),(DATEDIFF(D,BirthDay,GETDATE())/365))+'years '+dbo.fun_GetBirthday('M',BirthDay,getDate())+'month '+
dbo.fun_GetBirthday('D',BirthDay,getDate())+'days') as Age,(dbo.fun_GetDate(tab_line.BeginDate)) as BeginDate,
((dbo.fun_GetBirthday('Y',BeginDate,ResignDate)+'years '+dbo.fun_GetBirthday('M',BeginDate,ResignDate)+'month '+
dbo.fun_GetBirthday('D',BeginDate,ResignDate)+'days')) as ServiceYears,(dbo.fun_GetDate(tab_line.ResignDate)) as ResignDate
,Address,tab_bank.BankCode,tab_bank.AccNo,('$ '+CONVERT(nvarchar(100),tab_amt0.Amt)) as Salary,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt1.Amt,0))) as Account1,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt2.Amt,0))) as Account2,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt3.Amt,0))) as Account3,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt8.Amt,0))) as CPF1,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt9.Amt,0))) as CPF2,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt4.Amt,0))) as Account4,
('$ '+CONVERT(nvarchar(100),isnull(tab_amt5.Amt,0)+isnull(tab_amt6.Amt,0)+isnull(tab_amt7.Amt,0))) as Account5
 from Hr_Person mast 
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='Salary') as tab_amt0 on tab_amt0.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='LaundryExpense') as tab_amt1 on tab_amt1.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='Accommodation') as tab_amt2 on tab_amt2.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='WitholdingTax') as tab_amt3 on tab_amt3.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='Levy') as tab_amt4 on tab_amt4.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='MBMF') as tab_amt5 on tab_amt5.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='SINDA') as tab_amt6 on tab_amt6.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='CDAC') as tab_amt7 on tab_amt7.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='EmployerCPF') as tab_amt8 on tab_amt8.Person=mast.Id
left join(select PayItem,Person,Amt from Hr_Quote mast inner join Hr_PayItem p on mast.PayItem=p.Code where p.Code='EmployeeCPF') as tab_amt9 on tab_amt9.Person=mast.Id
left join (select top 1 ResignDate,Person,BeginDate from Hr_PersonDet1 order by Id asc) as tab_line on tab_line.Person=mast.Id 
left join (select BankCode,Person,AccNo from Hr_PersonDet3  where IsPayroll='1') as tab_bank on tab_bank.Person=mast.Id");
        return ConnectSql.GetTab(sql);
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        //btn_search_Click(null, null);
        
        //gridExport.WriteXlsToResponse("EmployeeDatabase", true);
        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\Aspose.lic"));
        DataTable dtTableName = GetData();
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
       

        cells[0, 0].PutValue("Collin's Movers Employee Database");


        cells[1, 0].PutValue("Date           " + PrintDate());
        cells[1, 21].PutValue("CPF");
        cells[1, 21].SetStyle(style);
        cells[1, 22].SetStyle(style);
        Range range = worksheet.Cells.CreateRange(1, 21, 1, 2); //Merge the cells /合并单元格
        range.Merge(); 

        cells[2, 0].PutValue("S/N");
        cells[2, 1].PutValue("Employee Name");
        cells[2, 2].PutValue("NRIC No./ FIN No.");
        cells[2, 3].PutValue("Nationality");
        cells[2, 4].PutValue("Category");
        cells[2, 5].PutValue("Work Pass Expiry Date");
        cells[2, 6].PutValue("Days to Expiry");
        cells[2, 7].PutValue("Designation");
        cells[2, 8].PutValue("Date of Birth");
        cells[2, 9].PutValue("Age");
        cells[2, 10].PutValue("Date Jobined");
        cells[2, 11].PutValue("Year of Service");
        cells[2, 12].PutValue("Date Left");
        cells[2, 13].PutValue("CPF/Non-CPF");
        cells[2, 14].PutValue("Address");
        cells[2, 15].PutValue("Bank");
        cells[2, 16].PutValue("Account Number");
        cells[2, 17].PutValue("Basic Salary");
        cells[2, 18].PutValue("Laundry Expenses");
        cells[2, 19].PutValue("Accommodation");
        cells[2, 20].PutValue("Witholding Tax");
        cells[2, 21].PutValue("Employer");
        cells[2, 22].PutValue("Employee");
        cells[2, 23].PutValue("Levy");
        cells[2, 24].PutValue("MBMF/Sinda/CDAC");

       

        cells[2, 0].SetStyle(style);
        cells[2, 1].SetStyle(style);
        cells[2, 2].SetStyle(style);
        cells[2, 3].SetStyle(style);
        cells[2, 4].SetStyle(style);
        cells[2, 5].SetStyle(style);
        cells[2, 6].SetStyle(style);
        cells[2, 7].SetStyle(style);
        cells[2, 8].SetStyle(style);
        cells[2, 9].SetStyle(style);
        cells[2, 10].SetStyle(style);
        cells[2, 11].SetStyle(style);
        cells[2, 12].SetStyle(style);
        cells[2, 13].SetStyle(style);
        cells[2, 14].SetStyle(style);
        cells[2, 15].SetStyle(style);
        cells[2, 16].SetStyle(style);
        cells[2, 17].SetStyle(style);
        cells[2, 18].SetStyle(style);
        cells[2, 19].SetStyle(style);
        cells[2, 20].SetStyle(style);
        cells[2, 21].SetStyle(style);
        cells[2, 22].SetStyle(style);
        cells[2, 23].SetStyle(style);
        cells[2, 24].SetStyle(style);

        cells.SetColumnWidth(0, 5);
        cells.SetColumnWidth(1, 20);
        cells.SetColumnWidth(2, 20);
        cells.SetColumnWidth(3, 20);
        cells.SetColumnWidth(4, 20);
        cells.SetColumnWidth(5, 20);
        cells.SetColumnWidth(6, 20);
        cells.SetColumnWidth(7, 20);
        cells.SetColumnWidth(8, 20);
        cells.SetColumnWidth(9, 20);
        cells.SetColumnWidth(10, 20);
        cells.SetColumnWidth(11, 20);
        cells.SetColumnWidth(12, 20);
        cells.SetColumnWidth(13, 20);
        cells.SetColumnWidth(14, 20);
        cells.SetColumnWidth(15, 20);
        cells.SetColumnWidth(16, 20);
        cells.SetColumnWidth(17, 20);
        cells.SetColumnWidth(18, 20);
        cells.SetColumnWidth(19, 20);
        cells.SetColumnWidth(20, 20);
        cells.SetColumnWidth(21, 20);
        cells.SetColumnWidth(22, 20);
        cells.SetColumnWidth(23, 20);
        cells.SetColumnWidth(24, 20);
        for (int n = 0; n < dtTableName.Rows.Count; n++)
        {
            cells[n + 3, 0].PutValue(n + 1);
            cells[n + 3, 1].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Name"]));
            cells[n + 3, 2].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["IcNo"]));
            cells[n + 3,3].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Country"]));
            cells[n + 3,4].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Department"]));
            cells[n + 3,5].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["ExpiryDate"]));
            cells[n + 3,6].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Days"]));
            cells[n + 3,7].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Remark2"]));
            cells[n + 3,8].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["BirthDay"]));
            cells[n + 3,9].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Age"]));
            cells[n + 3,10].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["BeginDate"]));
            cells[n + 3,11].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["ServiceYears"]));
            cells[n + 3,12].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["ResignDate"]));
            cells[n + 3,13].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["IsCPF"]));
            cells[n + 3,14].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Address"]));
            cells[n + 3,15].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["BankCode"]));
            cells[n + 3,16].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["AccNo"]));
            cells[n + 3,17].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Salary"]));
            cells[n + 3,18].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Account1"]));
            cells[n + 3,19].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Account2"]));
            cells[n + 3,20].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Account3"]));
            cells[n + 3,21].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["CPF1"]));
            cells[n + 3,22].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["CPF2"]));
            cells[n + 3, 23].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Account4"]));
            cells[n + 3,24].PutValue(SafeValue.SafeString(dtTableName.Rows[n]["Account5"]));

            cells[n + 3, 0].SetStyle(style1);
            cells[n + 3, 1].SetStyle(style1);
            cells[n + 3, 2].SetStyle(style1);
            cells[n + 3, 3].SetStyle(style1);
            cells[n + 3, 4].SetStyle(style1);
            cells[n + 3, 5].SetStyle(style1);
            cells[n + 3, 6].SetStyle(style1);
            cells[n + 3, 7].SetStyle(style1);
            cells[n + 3, 8].SetStyle(style1);
            cells[n + 3, 9].SetStyle(style1);
            cells[n + 3, 10].SetStyle(style1);
            cells[n + 3, 11].SetStyle(style1);
            cells[n + 3, 12].SetStyle(style1);
            cells[n + 3, 13].SetStyle(style1);
            cells[n + 3, 14].SetStyle(style1);
            cells[n + 3, 15].SetStyle(style1);
            cells[n + 3, 16].SetStyle(style1);
            cells[n + 3, 17].SetStyle(style1);
            cells[n + 3, 18].SetStyle(style1);
            cells[n + 3, 19].SetStyle(style1);
            cells[n + 3, 20].SetStyle(style1);
            cells[n + 3, 21].SetStyle(style1);
            cells[n + 3, 22].SetStyle(style1);
            cells[n + 3, 23].SetStyle(style1);
            cells[n + 3, 24].SetStyle(style1);
        }

        string locaPath = MapPath("~/Excel");
        if (!Directory.Exists(locaPath))
            Directory.CreateDirectory(locaPath);
        string path0 = string.Format("~/Excel/Empolyee_{0:yyyyMMdd}.xlsx",
        DateTime.Now.ToString("yyyyMMdd-HHmmss") ?? "01-01-2014"); //Request.QueryString["d"]
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
    public string PrintDate() {
        return date_Employee.Date.ToString("ddd", new System.Globalization.CultureInfo("en")) +", "+ date_Employee.Date.ToString("dd MMMM yyyy");
    }
}