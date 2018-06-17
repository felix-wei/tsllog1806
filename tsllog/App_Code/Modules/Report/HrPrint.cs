using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Wilson.ORMapper;
using System.Collections;
using C2;
/// <summary>
/// HrReport 的摘要说明
/// </summary>
public class HrPrint
{
    public HrPrint()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static DataTable dsSummary(string billType, DateTime date1, DateTime date2, string userName)
    {
        string type = "Billing summary to Trucking-";
        string strsql = string.Format(@"select row_number() over(order by det.SequenceId) as No,'{2}'+'-'+(CONVERT(nvarchar(10),'{0}',103)+'-'+CONVERT(nvarchar(10),'{1}',103)) as Date
,(select Name from xxparty where PartyId=do.PartyId) as Customer,Price,Qty,det.LocAmt as Amt,CONVERT(nvarchar(10),DocDate,103) as DocDate,ChgCode,ChgDes1 as Description,det.DocNo from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId 
inner join CTM_Job do on mast.MastRefNo=do.JobNo
where DocDate between '{0}' and '{1}'", date1.ToString("yyyy-MM-dd"), date2.Date.AddDays(1).ToString("yyyy-MM-dd"), type);
        return ConnectSql.GetTab(strsql);
    }
    public static DataSet PrintHaulier(string orderNo, string jobType)
    {
        DataSet set = new DataSet();
        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return set;
        C2.CtmJob obj = objSet[0] as C2.CtmJob;

        ObjectQuery query1 = new ObjectQuery(typeof(C2.CtmJobDet1), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);


        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");

        mast.Columns.Add("JobNo");
        mast.Columns.Add("JobType");
        mast.Columns.Add("Haulier");
        mast.Columns.Add("Attn");
        mast.Columns.Add("DeliveryTo");
        mast.Columns.Add("OceanBl");
        mast.Columns.Add("Yard");
        mast.Columns.Add("Carrier");
        mast.Columns.Add("CrNo");
        mast.Columns.Add("Contact1");
        mast.Columns.Add("Contact2");
        mast.Columns.Add("Tel1");
        mast.Columns.Add("Fax1");
        mast.Columns.Add("Terminal");
        mast.Columns.Add("Permit");
        mast.Columns.Add("LastDate");
        mast.Columns.Add("Portnet");
        mast.Columns.Add("Ft20");
        mast.Columns.Add("Ft40");
        mast.Columns.Add("Ft45");

        mast.Columns.Add("Vessel");
        mast.Columns.Add("Voyage");
        mast.Columns.Add("Etd");
        mast.Columns.Add("Eta");
        mast.Columns.Add("Pol");
        mast.Columns.Add("ByWho");
        mast.Columns.Add("ClientRefNo");
        mast.Columns.Add("CustName");
        mast.Columns.Add("AgentName");
        mast.Columns.Add("UserData2");
        mast.Columns.Add("UserData3");
        mast.Columns.Add("UserData4");
        mast.Columns.Add("Outside");
        mast.Columns.Add("Note1");
        mast.Columns.Add("Note2");
        mast.Columns.Add("StampUser");
        mast.Columns.Add("Remark");
        mast.Columns.Add("SpecialInstruction");

        det.Columns.Add("Idx");
        det.Columns.Add("JobNo");
        det.Columns.Add("ContNo");
        det.Columns.Add("SealNo");
        det.Columns.Add("FtSize");
        det.Columns.Add("FtRemark");
        det.Columns.Add("FtType");
        det.Columns.Add("FtKgs");
        det.Columns.Add("FtQty");
        det.Columns.Add("FtPack");
        det.Columns.Add("FtTruckIn");
        det.Columns.Add("FtCbm");
        det.Columns.Add("FtNom");

        #endregion

        DataRow row = mast.NewRow();
        string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.ClientId + "'";
        DataTable tab_hauler = Helper.Sql.List(sql_haulier);
        row["Attn"] = obj.Terminalcode;
        row["DeliveryTo"] = obj.DeliveryTo;
        row["CrNo"] = "";
        row["Contact1"] = "";
        row["Contact2"] = "";
        row["Tel1"] = "";
        row["Fax1"] = "";
        if (tab_hauler.Rows.Count > 0)
        {
            row["Haulier"] = tab_hauler.Rows[0]["Name"];
            row["CrNo"] = tab_hauler.Rows[0]["CrNo"].ToString();
            row["Contact1"] = tab_hauler.Rows[0]["Contact1"].ToString();
            row["Contact2"] = tab_hauler.Rows[0]["Contact2"].ToString();
            row["Tel1"] = tab_hauler.Rows[0]["Tel1"].ToString();
            row["Fax1"] = tab_hauler.Rows[0]["Fax1"].ToString();
        }

        row["JobNo"] = orderNo;
        row["JobType"] = obj.JobType;
        row["Vessel"] = obj.Vessel;
        row["Voyage"] = obj.Voyage;


        string sql = "select name from xxparty where partyid='" + obj.ClientId + "'";
        row["CustName"] = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql), "");


        row["Etd"] = obj.EtdDate.ToString("dd/MM/yyyy");
        row["Eta"] = obj.EtaDate.ToString("dd/MM/yyyy");
        sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pol, "") + "'";
        row["Pol"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        sql = "select name from xxparty where partyid='" + SafeValue.SafeInt(obj.CarrierId, 0) + "'";
        row["Carrier"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        row["OceanBl"] = "";
        row["Yard"] = obj.YardRef;
        row["ByWho"] = "";
        row["ClientRefNo"] = obj.ClientRefNo;
        row["UserData2"] = "";
        row["SpecialInstruction"] = obj.SpecialInstruction;
        row["Remark"] = obj.Remark;
        row["StampUser"] = HttpContext.Current.User.Identity.Name;
        int ft20 = 0;
        int ft40 = 0;
        int ft45 = 0;


        for (int i = 0; i < objSet1.Count; i++)
        {
            C2.CtmJobDet1 cont = objSet1[i] as C2.CtmJobDet1;
            DataRow row1 = det.NewRow();
            row1["Idx"] = i + 1;
            row1["JobNo"] = orderNo;
            row1["ContNo"] = cont.ContainerNo;
            row1["SealNo"] = cont.SealNo;
            string ftSize = "";
            //int index = cont.ContainerType.IndexOf("FT", 2);
            if (cont.ContainerType.IndexOf("20", 0) == 0)
                ft20++;
            if (cont.ContainerType.IndexOf("40", 0) == 0)
                ft40++;
            if (cont.ContainerType.IndexOf("45", 0) == 0)
                ft45++;
            row1["FtSize"] = ftSize;
            row1["FtType"] = cont.ContainerType;

            row1["FtQty"] = cont.Qty;
            row1["FtKgs"] = cont.Weight.ToString("0");
            row1["FtCbm"] = cont.Volume.ToString("#,##0.00");
            row1["FtTruckIn"] = "TRUCK-IN:" + cont.ScheduleDate.ToString("dd-MMM");
            det.Rows.Add(row1);
        }
        row["Ft20"] = ft20;
        row["Ft40"] = ft40;
        row["Ft45"] = ft45;
        mast.Rows.Add(row);

        set.Tables.Add(mast);
        set.Tables.Add(det);

        return set;
    }
    public static DataSet PrintPaySlip(string refNo, DateTime from, DateTime to)
    {
        DataSet set = new DataSet();
        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");
        DataTable det1 = new DataTable("Det1");
        mast.Columns.Add("Name");
        mast.Columns.Add("Department");
        mast.Columns.Add("BirthDay");
        mast.Columns.Add("BeginDate");
        mast.Columns.Add("Company");
        mast.Columns.Add("FromDate");
        mast.Columns.Add("Amt");
        mast.Columns.Add("Amt1");
        mast.Columns.Add("Amt2");
        mast.Columns.Add("Amt3");
        mast.Columns.Add("Amt4");
        mast.Columns.Add("Amt5");
        mast.Columns.Add("Amt6");
        mast.Columns.Add("Amt7");
        mast.Columns.Add("Amt8");
        mast.Columns.Add("Amt9");
        mast.Columns.Add("Amt10");
        mast.Columns.Add("Amt11");
        mast.Columns.Add("Amt12");
        mast.Columns.Add("Amt13");
        mast.Columns.Add("Amt14");
        mast.Columns.Add("Amt15");
        mast.Columns.Add("Amt16");
        mast.Columns.Add("Amt17");
        mast.Columns.Add("Amt18");
        mast.Columns.Add("Amt19");
        mast.Columns.Add("Amt20");
        mast.Columns.Add("Amt21");
        mast.Columns.Add("Amt22");
        mast.Columns.Add("Amt23");
        mast.Columns.Add("Amt24");
        mast.Columns.Add("Amt25");
        mast.Columns.Add("Amt26");
        mast.Columns.Add("SubTotal");
        mast.Columns.Add("SubTotal1");
        mast.Columns.Add("NettPayable");
        mast.Columns.Add("GrossWage");
        mast.Columns.Add("TotalAmt1");
        mast.Columns.Add("TotalAmt2");
        mast.Columns.Add("TotalAmt5");
        mast.Columns.Add("TotalAmt6");
        mast.Columns.Add("TotalAmt7");
        mast.Columns.Add("TotalAmt21");

        det.Columns.Add("Title");
        det.Columns.Add("Hours");
        det.Columns.Add("HoursRate");
        det.Columns.Add("Times");
        det.Columns.Add("TotalAmt");

        det1.Columns.Add("Title");
        det1.Columns.Add("Hours");
        det1.Columns.Add("HoursRate");
        det1.Columns.Add("Times");
        det1.Columns.Add("TotalAmt");

        #endregion


        string fromDate = from.Date.ToString("yyyy-MM-dd");
        string toDate = to.Date.ToString("yyyy-MM-dd");

        string sql = string.Format(@"select mast.Person,mast.Id,p.Name,p.Remark4,mast.FromDate,p.IcNo,p.Department,p.HrRole,tab_bank.BankName,tab_begin.BeginDate,tab_bank.AccNo,tab_bank.BankCode,mast.FromDate,mast.ToDate,p.BirthDay
,month(mast.FromDate) as PayMonth,tab_det.Amt,tab_amt1.Amt as Amt1,tab_amt2.Amt as Amt2,tab_amt3.Amt as Amt3,tab_amt4.Amt as Amt4,tab_amt5.Amt as Amt5,((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0))*tab_rate.Rate*0.01) as Amt6,
tab_amt7.Amt as Amt7,tab_amt8.Amt as Amt8,tab_amt9.Amt as Amt9,tab_amt10.Amt as Amt10,tab_amt11.Amt as Amt11,tab_amt12.Amt as Amt12,tab_amt13.Amt as Amt13,tab_amt14.Amt as Amt14,
tab_amt15.Amt as Amt15,tab_amt16.Amt as Amt16,tab_amt17.Amt as Amt17,tab_amt18.Amt as Amt18,tab_amt19.Amt as Amt19,tab_amt20.Amt as Amt20,tab_amt21.Amt as Amt21,tab_amt22.Amt as Amt22,tab_amt23.Amt as Amt23,tab_amt24.Amt as Amt24,tab_amt25.Amt as Amt25,
(tab_leave.Days*tab_amt20.Amt) as LeaveAmt1,tab_leave1.Days as Days1,(tab_leave1.Days*tab_amt20.Amt) as LeaveAmt2,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))+ISNULL(tab_amt13.Amt,0)+isnull(tab_amt17.Amt,0)+isnull(tab_amt15.Amt,0)+isnull(tab_amt14.Amt,0)+((isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)))) as NettPayable,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))+isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as NettWage,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))) as GrossWage,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0))) as CPFWage,
(isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as TotalCPF
from Hr_Payroll mast 
inner join Hr_Person p on mast.Person=p.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Basic%' or p.Description like '%Salary%') group by mast.ChgCode,mast.PayrollId) as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Bonus%' group by mast.ChgCode,mast.PayrollId) as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Employee CPF%' or p.Description like '%CPF Employee%') group by mast.ChgCode,mast.PayrollId) as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MBMF%' group by mast.ChgCode,mast.PayrollId) as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%SINDA%' group by mast.ChgCode,mast.PayrollId) as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%CDAC%' group by mast.ChgCode,mast.PayrollId) as tab_amt5 on tab_amt5.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Employer CPF%' or p.Description like '%CPF Employer%') group by mast.ChgCode,mast.PayrollId) as tab_amt6 on tab_amt6.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%FWL%' group by mast.ChgCode,mast.PayrollId) as tab_amt7 on tab_amt7.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%SDL%' group by mast.ChgCode,mast.PayrollId) as tab_amt8 on tab_amt8.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Transport%' group by mast.ChgCode,mast.PayrollId) as tab_amt9 on tab_amt9.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Laundry Expense%' group by mast.ChgCode,mast.PayrollId) as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Accommodation%' group by mast.ChgCode,mast.PayrollId) as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MinMonthSalary%' group by mast.ChgCode,mast.PayrollId) as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%AdvanceSalary%' group by mast.ChgCode,mast.PayrollId) as tab_amt13 on tab_amt13.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%NSPayment%' group by mast.ChgCode,mast.PayrollId) as tab_amt14 on tab_amt14.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%WitholdingTax%' group by mast.ChgCode,mast.PayrollId) as tab_amt15 on tab_amt15.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Others%'and Amt>0 group by mast.ChgCode,mast.PayrollId) as tab_amt16 on tab_amt16.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%TelParking%' group by mast.ChgCode,mast.PayrollId) as tab_amt17 on tab_amt17.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Levy%' group by mast.ChgCode,mast.PayrollId) as tab_amt18 on tab_amt18.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Claim%' group by mast.ChgCode,mast.PayrollId) as tab_amt19 on tab_amt19.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Leave%' group by mast.ChgCode,mast.PayrollId) as tab_amt20 on tab_amt20.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Allowance%' group by mast.ChgCode,mast.PayrollId) as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Reimbursement%' group by mast.ChgCode,mast.PayrollId) as tab_amt22 on tab_amt22.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Loan repayment%' group by mast.ChgCode,mast.PayrollId) as tab_amt23 on tab_amt23.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Others%' and Amt>0 group by mast.ChgCode,mast.PayrollId) as tab_amt24 on tab_amt24.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MonthlyVC%' group by mast.ChgCode,mast.PayrollId) as tab_amt25 on tab_amt25.PayrollId=mast.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{1}' and Date2<='{2}' and LeaveType!='PL') as tab_leave on tab_leave.Person=p.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{1}' and Date2<='{2}' and LeaveType='PL') as tab_leave1 on tab_leave1.Person=p.Id
left join (select top 1 BankCode,BankName,Person,AccNo from Hr_PersonDet3 where IsPayroll='True' order by CreateDateTime desc) as tab_bank on tab_bank.Person=p.Id
left join (select PayItem,Rate,RateType,Age from Hr_Rate) as tab_rate on tab_rate.PayItem='EmployerCPF'
left join(select top 1 BeginDate,Person from Hr_PersonDet1  order by CreateDateTime desc) as tab_begin on tab_begin.Person=p.Id
left join(select sum(Amt) as TotalAmt,Person from Hr_Payroll group by Person) as tab_total on tab_total.Person=mast.Id 
where mast.Id={0} and FromDate>='{1}' and ToDate<='{2}'", refNo, fromDate, toDate);

        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        DataRow row = mast.NewRow();
        int person = 0;
        decimal amt12 = 0;
        if (dt.Rows.Count > 0)
        {
            DataRow row_h = dt.Rows[0];
            person = SafeValue.SafeInt(row_h["Person"], 0);
            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToUpper();
            row["Name"] = row_h["Name"];
            row["Department"] = row_h["Department"];
            row["FromDate"] = Helper.Safe.SafeDate(row_h["FromDate"]).ToString("MMM-yyyy");
            row["BirthDay"] = Helper.Safe.SafeDate(row_h["BirthDay"]).ToString("dd-MMM-yyyy");
            row["BeginDate"] = Helper.Safe.SafeDate(row_h["BeginDate"]).ToString("dd-MMM-yyyy");
            row["Amt"] = Helper.Safe.AccountNz(row_h["Amt"]);
            row["Amt1"] = Helper.Safe.AccountNz(row_h["Amt1"]);
            row["Amt2"] = Helper.Safe.AccountNz(row_h["Amt2"]);
            row["Amt3"] = Helper.Safe.AccountNz(row_h["Amt3"]);
            row["Amt4"] = Helper.Safe.AccountNz(row_h["Amt4"]);
            row["Amt5"] = Helper.Safe.AccountNz(row_h["Amt5"]);
            row["Amt6"] = Helper.Safe.AccountNz(row_h["Amt6"]);
            row["Amt7"] = Helper.Safe.AccountNz(row_h["Amt7"]);
            row["Amt8"] = Helper.Safe.AccountNz(row_h["Amt8"]);
            row["Amt9"] = Helper.Safe.AccountNz(row_h["Amt9"]);
            row["Amt10"] = Helper.Safe.AccountNz(row_h["Amt10"]);
            row["Amt11"] = Helper.Safe.AccountNz(row_h["Amt11"]);
            amt12 = SafeValue.SafeDecimal(row_h["Amt25"]);
            if (amt12 > 0)
            {
                row["Amt12"] = amt12;
                amt12 = SafeValue.SafeDecimal(row_h["Amt25"]);
            }
            else
            {
                row["Amt12"] = Helper.Safe.AccountNz(row_h["Amt12"]);
                amt12 = SafeValue.SafeDecimal(row_h["Amt12"]);
            }
            row["Amt13"] = Helper.Safe.AccountNz(row_h["Amt13"]);
            row["Amt14"] = Helper.Safe.AccountNz(row_h["Amt14"]);
            row["Amt15"] = Helper.Safe.AccountNz(row_h["Amt15"]);
            row["Amt16"] = Helper.Safe.AccountNz(row_h["Amt16"]);
            row["Amt17"] = Helper.Safe.AccountNz(row_h["Amt17"]);
            row["Amt18"] = Helper.Safe.AccountNz(row_h["Amt18"]);
            row["Amt19"] = Helper.Safe.AccountNz(row_h["Amt19"]);

            row["Amt21"] = Helper.Safe.AccountNz(row_h["Amt21"]);
            row["Amt22"] = Helper.Safe.AccountNz(row_h["Amt22"]);
            row["Amt23"] = Helper.Safe.AccountNz(row_h["Amt23"]);
            row["Amt24"] = Helper.Safe.AccountNz(row_h["Amt24"]);

            decimal amt = SafeValue.SafeDecimal(row_h["Amt"]);
            decimal amt1 = SafeValue.SafeDecimal(row_h["Amt1"]);

            decimal amt16 = SafeValue.SafeDecimal(row_h["Amt16"]);
            decimal amt21 = SafeValue.SafeDecimal(row_h["Amt21"]);
            decimal subTotal = SafeValue.ChinaRound(amt + amt1 + amt12 + amt16 + amt21, 2);
            row["SubTotal"] = subTotal;





            fromDate = from.AddMonths(-from.Month + 1).AddDays(-from.Day + 1).ToString("yyyy-MM-dd");
            toDate = to.Date.ToString("yyyy-MM-dd");
            row["GrossWage"] = TotalGrossWage(person, fromDate, toDate);
            row["TotalAmt2"] = TotalEmployeeCPF(person, fromDate, toDate);
            row["TotalAmt1"] = TotalBonus(person, fromDate, toDate);
            row["TotalAmt5"] = TotalCDAC(person, fromDate, toDate) + TotalMBMF(person, fromDate, toDate) + TotalSINDA(person, fromDate, toDate);
            row["TotalAmt21"] = TotalAllowance(person, fromDate, toDate);
            row["TotalAmt6"] = TotalEmployerCPF(person, fromDate, toDate);

            mast.Rows.Add(row);

            fromDate = from.Date.ToString("yyyy-MM-dd");
            toDate = to.Date.ToString("yyyy-MM-dd");
            decimal hours = 0;
            decimal hoursRate = 0;
            decimal hours1 = 0;
            decimal hoursRate1 = 0;
            string sql_ot = string.Format(@"select * from hr_overtime where Person={0} and FromDate>='{1}' and ToDate<='{2}'", person, fromDate, toDate);
            DataTable dt_det = ConnectSql_mb.GetDataTable(sql_ot);
            if (dt_det.Rows.Count == 0)
            {
                sql_ot = string.Format(@"select top 1 * from hr_overtime where FromDate>='{1}' and ToDate<='{2}'", person, fromDate, toDate);
            }
            for (int i = 0; i < dt_det.Rows.Count; i++)
            {
                DataRow row_det = dt_det.Rows[i];
                DataRow row1 = det.NewRow();

                string type = row_det["TypeId"].ToString();

                decimal times = SafeValue.SafeDecimal(row_det["Times"]);
                if (type == "Overtime")
                {
                    hours = SafeValue.SafeDecimal(row_det["Hours"]);
                    hoursRate = SafeValue.SafeDecimal(row_det["HoursRate"]);
                    row1["Hours"] = hours;
                    row1["TotalAmt"] = SafeValue.ChinaRound(hours * hoursRate * times, 2);
                    row1["HoursRate"] = hoursRate;
                    row1["Times"] = times;

                    det.Rows.Add(row1);
                }
                if (type == "NoPay")
                {
                    hours1 = SafeValue.SafeDecimal(row_det["Hours"]);
                    hoursRate1 = SafeValue.SafeDecimal(row_det["HoursRate"]);
                }


            }

            int balDays = 0;
            int leaveDays = 0;
            decimal amt20 = 0;
            int year = DateTime.Now.Year;
            string sql_leave = string.Format(@"select * from Hr_LeaveTmp where Person={0} and Year='{1}'", person, year);
            DataTable dt_leave = ConnectSql_mb.GetDataTable(sql_leave);

            for (int i = 0; i < dt_leave.Rows.Count; i++)
            {
                string leaveType = SafeValue.SafeString(dt_leave.Rows[i]["LeaveType"]);
                int nowDays = SafeValue.SafeInt(dt_leave.Rows[i]["Days"], 0);
                sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", person, leaveType, year - 1);
                DataTable dt_day = ConnectSql_mb.GetDataTable(sql);
                for (int j = 0; j < dt_day.Rows.Count; j++)
                {
                    year = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                    string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                    if (approveStatus == "Approve")
                        leaveDays += SafeValue.SafeInt(dt_day.Rows[i]["Days"], 0);
                }
                sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", person, leaveType, year - 1);
                int totalDays = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
                balDays = totalDays - leaveDays;


                balDays = nowDays + balDays;//今年加上去年的假期还有多少天假
                int dayPerMonth = 0;
                if (balDays > 12)
                    dayPerMonth = balDays / 12;//每个月还有多少天假
                else
                    dayPerMonth = balDays;
                leaveDays = 0;
                sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and (Date1>='{2}' and Date2<='{3}')", person, leaveType, fromDate, toDate);
                dt_day = ConnectSql_mb.GetDataTable(sql);
                for (int j = 0; j < dt_day.Rows.Count; j++)
                {
                    year = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                    string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                    if (approveStatus == "Approve")
                        leaveDays += SafeValue.SafeInt(dt_day.Rows[i]["Days"], 0);
                }
                int n = dayPerMonth - leaveDays;
                if (hoursRate1 > 0)
                {
                    DataRow row2 = det1.NewRow();
                    row2["HoursRate"] = hoursRate1;
                    row2["Hours"] = Helper.Safe.AccountNz(n);
                    amt20 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(SafeValue.ChinaRound(n * hoursRate1, 2)));
                    row2["TotalAmt"] = Helper.Safe.AccountNz(SafeValue.ChinaRound(n * hoursRate1, 2));
                    row2["Times"] = 1;

                    det1.Rows.Add(row2);
                }
            }
            row["Amt20"] = amt20;
            decimal amt2 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(row_h["Amt2"]));
            decimal amt13 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(row_h["Amt13"]));
            decimal amt14 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(row_h["Amt14"]));
            decimal amt23 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(row_h["Amt23"]));
            decimal amt24 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(row_h["Amt24"]));
            decimal amt17 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(row_h["Amt17"]));
            decimal subTotal1 = SafeValue.ChinaRound(amt2 + amt13 + amt14 + amt17 + amt23 + amt24 + amt20, 2);
            row["SubTotal1"] = subTotal1;

            row["NettPayable"] = subTotal - subTotal1;

            set.Tables.Add(mast);
            set.Tables.Add(det);
            set.Tables.Add(det1);
        }
        return set;
    }
    public static decimal TotalGrossWage(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(GrossWage) as TotalAmt,Person from(select Person,FromDate,ToDate,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt12.Amt,0)+isnull(tab_amt22.Amt,0)+isnull(tab_amt25.Amt,0))) as GrossWage
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Basic%' or p.Description like '%Salary%') group by mast.ChgCode,mast.PayrollId) as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Bonus%' group by mast.ChgCode,mast.PayrollId) as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Laundry Expense%' group by mast.ChgCode,mast.PayrollId) as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Accommodation%' group by mast.ChgCode,mast.PayrollId) as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MinMonthSalary%' group by mast.ChgCode,mast.PayrollId) as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Leave%' group by mast.ChgCode,mast.PayrollId) as tab_amt20 on tab_amt20.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Allowance%' group by mast.ChgCode,mast.PayrollId) as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Reimbursement%' group by mast.ChgCode,mast.PayrollId) as tab_amt22 on tab_amt22.PayrollId=mast.Id
left join(select ChgCode,PayrollId,Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Code='MonthlyVC') as tab_amt25 on tab_amt25.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static decimal TotalCPFWage(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(CPFWage) as TotalAmt,Person from(select Person,FromDate,ToDate,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0))) as CPFWage
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Basic%' or p.Description like '%Salary%') group by mast.ChgCode,mast.PayrollId) as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Bonus%' group by mast.ChgCode,mast.PayrollId) as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%PayInLieu%' group by mast.ChgCode,mast.PayrollId) as tab_amt21 on tab_amt21.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static decimal TotalEmployeeCPF(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(TotalCPF) as TotalAmt,Person from(select Person,FromDate,ToDate,
(isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as TotalCPF
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Employee CPF%' or p.Description like '%CPF Employee%') group by mast.ChgCode,mast.PayrollId) as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MBMF%' group by mast.ChgCode,mast.PayrollId) as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%SINDA%' group by mast.ChgCode,mast.PayrollId) as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%CDAC%' group by mast.ChgCode,mast.PayrollId) as tab_amt5 on tab_amt5.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = -SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static decimal TotalBonus(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(TotalBonus) as TotalAmt,Person from(select Person,FromDate,ToDate,
isnull(tab_amt1.Amt,0) as TotalBonus
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Basic%' or p.Description like '%Salary%') group by mast.ChgCode,mast.PayrollId) as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Bonus%' group by mast.ChgCode,mast.PayrollId) as tab_amt1 on tab_amt1.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static decimal TotalCDAC(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(TotalAmt) as TotalAmt,Person from(select Person,FromDate,ToDate,
isnull(tab_amt.Amt,0) as TotalAmt
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%CDAC%' group by mast.ChgCode,mast.PayrollId) as tab_amt on tab_amt.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = -SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static decimal TotalMBMF(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(TotalAmt) as TotalAmt,Person from(select Person,FromDate,ToDate,
isnull(tab_amt.Amt,0) as TotalAmt
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MBMF%' group by mast.ChgCode,mast.PayrollId) as tab_amt on tab_amt.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = -SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static decimal TotalSINDA(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(TotalAmt) as TotalAmt,Person from(select Person,FromDate,ToDate,
isnull(tab_amt.Amt,0) as TotalAmt
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%SINDA%' group by mast.ChgCode,mast.PayrollId) as tab_amt on tab_amt.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = -SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static decimal TotalAllowance(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(TotalAmt) as TotalAmt,Person from(select Person,FromDate,ToDate,
isnull(tab_amt.Amt,0) as TotalAmt
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Allowance%' group by mast.ChgCode,mast.PayrollId) as tab_amt on tab_amt.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static decimal TotalEmployerCPF(int person, string fromDate, string toDate)
    {
        decimal value = 0;
        string sql = string.Format(@"select sum(TotalAmt) as TotalAmt,Person from(select Person,FromDate,ToDate,
isnull(tab_amt.Amt,0) as TotalAmt
from Hr_Payroll mast 
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Employer CPF%' or p.Description like '%CPF Employer%') group by mast.ChgCode,mast.PayrollId) as tab_amt on tab_amt.PayrollId=mast.Id
where Person={0} and FromDate>='{1}' and ToDate<='{2}') as tab group by Person", person, fromDate, toDate);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
            value = SafeValue.SafeDecimal(tab.Rows[0]["TotalAmt"]);
        return value;
    }
    public static DataSet PrintCPFContribution(DateTime from, DateTime to)
    {
        DataSet set = new DataSet();
        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");
        mast.Columns.Add("CSN");
        mast.Columns.Add("Company");
        mast.Columns.Add("Date");

        mast.Columns.Add("TotalAmt");
        mast.Columns.Add("TotalAmt1");
        mast.Columns.Add("TotalAmt2");
        mast.Columns.Add("TotalAmt3");
        mast.Columns.Add("TotalAmt4");
        mast.Columns.Add("TotalAmt5");
        mast.Columns.Add("TotalAmt6");

        det.Columns.Add("SN");
        det.Columns.Add("AccountNo");
        det.Columns.Add("Name");
        det.Columns.Add("TotalCPF");
        det.Columns.Add("EmployerCPF");
        det.Columns.Add("EmployeeCPF");
        det.Columns.Add("OrdinaryWages");
        det.Columns.Add("AdditionalWages");
        det.Columns.Add("SDL");
        det.Columns.Add("AgencyFund");

        #endregion


        string fromDate = from.Date.ToString("yyyy-MM-dd");
        string toDate = to.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"select mast.Person,mast.Id,p.Name,p.Remark4,mast.FromDate,p.IcNo,p.Department,p.HrRole,
tab_bank.BankName,tab_begin.BeginDate,(select top 1  AccNo from Hr_PersonDet3 where IsPayroll='True' and Person=mast.Person order by CreateDateTime desc) as AccNo,tab_bank.BankCode,mast.FromDate,mast.ToDate,p.BirthDay
,month(mast.FromDate) as PayMonth,tab_det.Amt,tab_amt1.Amt as Amt1,tab_amt2.Amt as Amt2,tab_amt3.Amt as Amt3,tab_amt4.Amt as Amt4,tab_amt5.Amt as Amt5,((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0))*tab_rate.Rate*0.01) as Amt6,
tab_amt7.Amt as Amt7,tab_amt8.Amt as Amt8,tab_amt9.Amt as Amt9,tab_amt10.Amt as Amt10,tab_amt11.Amt as Amt11,tab_amt12.Amt as Amt12,tab_amt13.Amt as Amt13,tab_amt14.Amt as Amt14,
tab_amt15.Amt as Amt15,tab_amt16.Amt as Amt16,tab_amt17.Amt as Amt17,tab_amt18.Amt as Amt18,tab_amt19.Amt as Amt19,tab_amt20.Amt as Amt20,tab_amt21.Amt as Amt21,tab_amt22.Amt as Amt22,tab_amt23.Amt as Amt23,tab_amt24.Amt as Amt24,tab_amt25.Amt as Amt25,
(tab_leave.Days*tab_amt20.Amt) as LeaveAmt1,tab_leave1.Days as Days1,(tab_leave1.Days*tab_amt20.Amt) as LeaveAmt2,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))+ISNULL(tab_amt13.Amt,0)+isnull(tab_amt17.Amt,0)+isnull(tab_amt15.Amt,0)+isnull(tab_amt14.Amt,0)+((isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)))) as NettPayable,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))+isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as NettWage,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0)+isnull(tab_amt10.Amt,0)+isnull(tab_amt11.Amt,0)+isnull(tab_amt22.Amt,0))) as GrossWage,
((isnull(tab_det.Amt,0)+ISNULL(tab_amt21.Amt,0)+isnull(tab_amt1.Amt,0))) as CPFWage,
(isnull(tab_amt2.Amt,0)+ISNULL(tab_amt3.Amt,0)+isnull(tab_amt4.Amt,0)+isnull(tab_amt5.Amt,0)) as TotalCPF
from Hr_Payroll mast 
left join Hr_Person p on mast.Person=p.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Basic%' or p.Description like '%Salary%') group by mast.ChgCode,mast.PayrollId) as tab_det on tab_det.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Bonus%' group by mast.ChgCode,mast.PayrollId) as tab_amt1 on tab_amt1.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Employee CPF%' or p.Description like '%CPF Employee%') group by mast.ChgCode,mast.PayrollId) as tab_amt2 on tab_amt2.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MBMF%' group by mast.ChgCode,mast.PayrollId) as tab_amt3 on tab_amt3.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%SINDA%' group by mast.ChgCode,mast.PayrollId) as tab_amt4 on tab_amt4.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%CDAC%' group by mast.ChgCode,mast.PayrollId) as tab_amt5 on tab_amt5.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where (p.Description like '%Employer CPF%' or p.Description like '%CPF Employer%') group by mast.ChgCode,mast.PayrollId) as tab_amt6 on tab_amt6.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%FWL%' group by mast.ChgCode,mast.PayrollId) as tab_amt7 on tab_amt7.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%SDL%' group by mast.ChgCode,mast.PayrollId) as tab_amt8 on tab_amt8.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Transport%' group by mast.ChgCode,mast.PayrollId) as tab_amt9 on tab_amt9.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Laundry Expense%' group by mast.ChgCode,mast.PayrollId) as tab_amt10 on tab_amt10.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Accommodation%' group by mast.ChgCode,mast.PayrollId) as tab_amt11 on tab_amt11.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MinMonthSalary%' group by mast.ChgCode,mast.PayrollId) as tab_amt12 on tab_amt12.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%AdvanceSalary%' group by mast.ChgCode,mast.PayrollId) as tab_amt13 on tab_amt13.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%NSPayment%' group by mast.ChgCode,mast.PayrollId) as tab_amt14 on tab_amt14.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%WitholdingTax%' group by mast.ChgCode,mast.PayrollId) as tab_amt15 on tab_amt15.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Others%'and Amt>0 group by mast.ChgCode,mast.PayrollId) as tab_amt16 on tab_amt16.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%TelParking%' group by mast.ChgCode,mast.PayrollId) as tab_amt17 on tab_amt17.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Levy%' group by mast.ChgCode,mast.PayrollId) as tab_amt18 on tab_amt18.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Claim%' group by mast.ChgCode,mast.PayrollId) as tab_amt19 on tab_amt19.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Leave%' group by mast.ChgCode,mast.PayrollId) as tab_amt20 on tab_amt20.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Allowance%' group by mast.ChgCode,mast.PayrollId) as tab_amt21 on tab_amt21.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Reimbursement%' group by mast.ChgCode,mast.PayrollId) as tab_amt22 on tab_amt22.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Loan repayment%' group by mast.ChgCode,mast.PayrollId) as tab_amt23 on tab_amt23.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%Others%' and Amt>0 group by mast.ChgCode,mast.PayrollId) as tab_amt24 on tab_amt24.PayrollId=mast.Id
left join(select ChgCode,PayrollId,sum(Amt) Amt from Hr_PayrollDet mast inner join Hr_PayItem p on mast.ChgCode=p.Code where p.Description like '%MonthlyVC%' group by mast.ChgCode,mast.PayrollId) as tab_amt25 on tab_amt25.PayrollId=mast.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{0}' and Date2<='{1}' and LeaveType!='PL') as tab_leave on tab_leave.Person=p.Id
left join(select Person,Date1,Date2,Days from Hr_Leave where Date1>='{0}' and Date2<='{1}' and LeaveType='PL') as tab_leave1 on tab_leave1.Person=p.Id
left join (select top 1 BankCode,BankName,Person,AccNo from Hr_PersonDet3 where IsPayroll='True' order by CreateDateTime desc) as tab_bank on tab_bank.Person=mast.Person
left join (select PayItem,Rate,RateType,Age from Hr_Rate) as tab_rate on tab_rate.PayItem='EmployerCPF'
left join(select top 1 BeginDate,Person from Hr_PersonDet1  order by CreateDateTime desc) as tab_begin on tab_begin.Person=p.Id
left join(select sum(Amt) as TotalAmt,Person from Hr_Payroll group by Person) as tab_total on tab_total.Person=mast.Id 
where FromDate>='{0}' and ToDate<='{1}'", fromDate, toDate);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        DataRow row = mast.NewRow();

        row["CSN"] = System.Configuration.ConfigurationManager.AppSettings["CSN"];
        row["Company"] = System.Configuration.ConfigurationManager.AppSettings["Company"];
        row["Date"] = from.ToString("MMM yyyy").ToUpper();
        decimal totalAmt = 0;
        decimal totalAmt1 = 0;
        decimal totalAmt2 = 0;
        decimal totalAmt3 = 0;
        decimal totalAmt4 = 0;
        decimal totalAmt5 = 0;
        decimal totalAmt6 = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row1 = det.NewRow();

            DataRow dt_row = dt.Rows[i];
            decimal amt12 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt25"]));
            if (amt12 < 0)
            {
                amt12 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt12"]));
            }
            decimal amt = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt"]));
            decimal amt1 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt1"]));
            decimal amt2 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt2"]));
            decimal amt3 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt3"]));
            decimal amt4 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt4"]));
            decimal amt5 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt5"]));
            decimal amt6 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt6"]));
            decimal amt7 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt7"]));
            decimal amt8 = SafeValue.SafeDecimal(Helper.Safe.AccountNz(dt_row["Amt8"]));
            decimal amt9 = SafeValue.SafeDecimal(dt_row["Amt9"]);
            decimal amt10 = SafeValue.SafeDecimal(dt_row["Amt10"]);
            decimal amt11 = SafeValue.SafeDecimal(dt_row["Amt11"]);
            decimal amt16 = SafeValue.SafeDecimal(dt_row["Amt16"]);
            decimal amt21 = SafeValue.SafeDecimal(dt_row["Amt21"]);
            decimal subTotal = SafeValue.ChinaRound(amt + amt12, 2);



            row1["SN"] = i + 1;
            row1["AccountNo"] = dt_row["AccNo"];
            row1["Name"] = dt_row["Name"];
            row1["TotalCPF"] = SafeValue.ChinaRound(amt2 + amt6, 2);
            row1["EmployerCPF"] = SafeValue.ChinaRound(amt6, 2);
            row1["EmployeeCPF"] = SafeValue.ChinaRound(amt2, 2);
            row1["OrdinaryWages"] = subTotal;
            row1["AdditionalWages"] = SafeValue.ChinaRound(amt1 + amt16 + amt21, 2);
            row1["SDL"] = SafeValue.ChinaRound(amt8, 2);
            row1["AgencyFund"] = SafeValue.ChinaRound(amt3 + amt4 + amt5, 2);

            det.Rows.Add(row1);

            totalAmt += SafeValue.ChinaRound(amt2 + amt6, 2);
            totalAmt1 += SafeValue.ChinaRound(amt6, 2);
            totalAmt2 += SafeValue.ChinaRound(amt2, 2);
            totalAmt3 += SafeValue.ChinaRound(subTotal, 2);
            totalAmt4 += SafeValue.ChinaRound(amt1 + amt16 + amt21, 2);
            totalAmt5 += SafeValue.ChinaRound(amt8, 2);
            totalAmt6 += SafeValue.ChinaRound(amt3 + amt4 + amt5, 2);
        }

        row["TotalAmt"] = totalAmt;
        row["TotalAmt1"] = totalAmt1;
        row["TotalAmt2"] = totalAmt2;
        row["TotalAmt3"] = totalAmt3;
        row["TotalAmt4"] = totalAmt4;
        row["TotalAmt5"] = totalAmt5;
        row["TotalAmt6"] = totalAmt6;
        mast.Rows.Add(row);


        set.Tables.Add(mast);
        set.Tables.Add(det);
        return set;

    }
}