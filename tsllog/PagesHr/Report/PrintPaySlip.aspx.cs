using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helper;

public partial class PagesHr_Report_PrintPaySlip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
        GetDetData();
        GetDetData1();
    }
    public DataTable GetData()
    {
        string no ="0";
        if(SafeValue.SafeString(Request.QueryString["no"])!=null)
        {
            no=SafeValue.SafeString(Request.QueryString["no"]);
        }
        string sql = string.Format(@"select hr.Name,hr.Id,hr.Remark4,pay.FromDate,hr.IcNo,hr.Department,hr.HrRole,tab_bank.BankName,tab_begin.BeginDate,tab_bank.AccNo from Hr_Payroll pay 
left join Hr_Person hr on pay.Person=hr.Id
left join (select top 1 BankName,Person,AccNo from Hr_PersonDet3 where IsPayroll='True' order by CreateDateTime desc) as tab_bank on tab_bank.Person=hr.Id
left join(select top 1 BeginDate,Person from Hr_PersonDet1  order by CreateDateTime desc) as tab_begin on tab_begin.Person=hr.Id
where pay.Id={0}", no);
        return ConnectSql.GetTab(sql);
    }
    public string GetTotalAmt()
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = string.Format(@"select (sum(det.Amt)+sum(c.CPF1)+sum(c.CPF2)) as TotalAmt from Hr_PayrollDet det inner join Hr_Payroll mast on mast.Id=det.PayrollId
left join Hr_cpf c on c.Person=mast.Person
where det.PayrollId={0} and det.Amt>0", no);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    public string GetTotalAmt1()
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = string.Format(@"select (-sum(Amt)) as TotalAmt from Hr_PayrollDet where PayrollId={0} and Amt<0", no);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    public string GetTotalAmt2()
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = string.Format(@"select (sum(Amt)+sum(c.CPF1)+sum(c.CPF2)) as Amt from Hr_Payroll mast left join Hr_cpf c on c.Person=mast.Person where mast.Id={0}", no);
        decimal amt = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
        return SafeValue.SafeString(amt);
    }
    public DataTable GetDetData()
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = string.Format(@"select * from Hr_PayrollDet where PayrollId={0} and Amt>0", no);
        return ConnectSql.GetTab(sql);
    }
    public DataTable GetDetData1()
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = string.Format(@"select ChgCode,(-Amt) as Amt from Hr_PayrollDet where PayrollId={0} and Amt<0", no);
        return ConnectSql.GetTab(sql);
    }
    public DataTable GetDetData2(int person)
    {
        string sql = string.Format(@"select sum(CPF1+CPF2) as Amt from Hr_cpf where Person={0}", person);
        return ConnectSql.GetTab(sql);
    }
}