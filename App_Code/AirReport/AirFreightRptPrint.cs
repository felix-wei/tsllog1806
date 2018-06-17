using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using C2;

/// <summary>
/// AirFreightRptPrint 的摘要说明
/// </summary>
public class AirFreightRptPrint
{
    #region account
    public static DataTable dsInvoiceByDate(string billType, string party, DateTime d1, DateTime d2, string sortFiled, string userName, string refType)
    {
        string sql = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, MastRefNo, MastType, inv.CurrencyId as Currency, 
cast(isnull(inv.ExRate,0) as numeric(21,6)) as ExRate, Term,DocAmt, LocAmt as Amt,
case '{0}' when 'CN' then 'Credit Note ' when 'DN' then 'Debit Note ' else 'Invoice ' end +'List From {1} to {2}' as PeriodStr,
ref.AirportCode0   as Pol,ref.AirportCode1 as Pod,
inv.MastRefNo as RefNo,'{3}' as UserId
FROM XAArInvoice as inv
left outer join air_ref as ref on ref.RefNo=inv.MastRefNo
where DocType='{0}' and DocDate>='{4}' and DocDate<'{5}' and ref.StatusCode<>'CNL' ", billType, d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), userName, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        if (refType.Trim().Length > 0)
        {
            sql += " and MastType='" + refType + "'";
        }
        if (party.Length > 0 && party != "null")
            sql += string.Format(" and PartyTo='{0}'", party);
        sql += " order by " + sortFiled;
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;

    }
    public static DataTable dsInvoiceByNo(string billType, string party, string docNo1, string docNo2, string sortFiled, string userName,string refType)
    {

        string sql = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, MastRefNo, MastType, inv.CurrencyId as Currency, 
cast(isnull(inv.ExRate,0) as numeric(21,6)) as ExRate, Term,DocAmt, LocAmt as Amt,
case '{0}' when 'CN' then 'Credit Note ' when 'DN' then 'Debit Note ' else 'Invoice ' end +'List From {1} to {2}' as PeriodStr,
ref.AirportCode0 as Pol,ref.AirportCode1 as Pod,
inv.MastRefNo as RefNo,'{3}' as UserId
FROM XAArInvoice as inv
left outer join air_ref as ref on ref.RefNo=inv.MastRefNo
where DocType='{0}' and DocNo>='{4}' and DocNo<'{5}' and ref.StatusCode<>'CNL' ", billType, docNo1, docNo2, userName, docNo1, docNo2);
        if (refType.Trim().Length > 0)
        {
            sql += " and MastType='" + refType + "'";
        }
        if (party.Length > 0 && party != "null")
            sql += string.Format(" and PartyTo='{0}'", party);
        sql += " order by " + sortFiled;
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;

    }
    public static DataTable dsVoucherByDate(string billType, string party, DateTime d1, DateTime d2, string sortFiled, string userName,string refType)
    {

        string sql = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, 
MastRefNo as RefNo, MastType, PayAble.CurrencyId as Currency, cast(isnull(PayAble.ExRate,0) as numeric(21,6)) as ExRate,ChqNo as Term,DocAmt, LocAmt as Amt,
'Voucher List From {0} to {1}' as PeriodStr,
ref.AirportCode0 as Pol,ref.AirportCode1 as Pod,'{2}' as UserId
FROM XAApPayable as PayAble
left outer join air_ref as ref on PayAble.MastRefNo=ref.RefNo 
where DocType='{3}' and DocDate>='{4}' and DocDate<'{5}'  and ref.StatusCode<>'CNL'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), userName, billType, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        if (refType.Trim().Length > 0)
        {
            sql += " and MastType='" + refType + "'";
        }
        if (party.Length > 0 && party != "null")
            sql += string.Format(" and PartyTo='{0}'", party);
        sql += " order by " + sortFiled;
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;

    }
    public static DataTable dsVoucherByNo(string billType, string party, string docNo1, string docNo2, string sortFiled, string userName,string refType)
    {

        string sql = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, 
MastRefNo as RefNo, MastType, PayAble.CurrencyId as Currency, cast(isnull(PayAble.ExRate,0) as numeric(21,6)) as ExRate,ChqNo as Term,DocAmt, LocAmt as Amt,
'Voucher List From {0} to {1}' as PeriodStr,ref.AirportCode0 as Pol,ref.AirportCode1 as Pod,'{2}' as UserId
FROM XAApPayable as PayAble
left outer join air_ref as ref on PayAble.MastRefNo=ref.RefNo 
where DocType='{3}' and DocNo>='{4}' and DocNo<'{5}' and ref.StatusCode<>'CNL'", docNo1, docNo2, userName, billType, docNo1, docNo2);
        if (refType.Trim().Length > 0)
        {
            sql += " and MastType='" + refType + "'";
        }
        if (party.Length > 0 && party != "null")
            sql += string.Format(" and PartyTo='{0}'", party);
        sql += " order by " + sortFiled;
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;

    }
    #endregion

    #region import/Export
    public static DataTable dsVolumeByDate(string refType, DateTime d1, DateTime d2, string userName)
    {
        string sql = string.Format(@"Select ref.RefNo,RefType,MAWB,CarrierBkgNo,EstCostAmt,EstSaleAmt,Weight,Qty,Volume,convert(nvarchar,FlightDate1,103) as Eta,convert(nvarchar,FlightDate0,103) as Etd, AirportCode0 as Port, 
AirportCode1, NvoccBlNO,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,Hbl.cnt as HblCnt,
'Date From {0} to {1}' as PeriodStr,'Import' as JobType,
case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent,
cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt,
cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt,
cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt 
FROM air_ref as ref
left outer join (select count(*) as cnt,refno from air_job where StatusCode='USE' group by refno) as Hbl on Hbl.refno=ref.refno
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{5}' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{5}' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='{5}' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM air_Costing where JobType='{5}' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE FlightDate0>='{2}' and FlightDate0<'{3}' and ref.StatusCode<>'CNL'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName, refType);
        if (refType.Length > 0)
            sql += string.Format(" and ref.RefType='{0}'", refType);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    public static DataTable dsVolumeByAgent(string refType, DateTime d1, DateTime d2, string agentId, string userName)
    {
        string sql = string.Format(@"Select ref.RefNo,RefType,MAWB,CarrierBkgNo,EstCostAmt,EstSaleAmt,Weight,Qty,Volume,convert(nvarchar,FlightDate1,103) as Eta,convert(nvarchar,FlightDate0,103) as Etd, AirportCode0 as Port, 
AirportCode1, NvoccBlNO,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,Hbl.cnt as HblCnt,
'Date From {0} to {1}' as PeriodStr,'Import' as JobType,
case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent,
cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt,
cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt,
cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt 
FROM air_ref as ref
left outer join (select count(*) as cnt,refno from air_job where StatusCode='USE' group by refno) as Hbl on Hbl.refno=ref.refno
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{5}' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{5}' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='{5}' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM air_Costing where JobType='{5}' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE FlightDate1>='{2}' and FlightDate1<'{3}' and ref.StatusCode<>'CNL'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName, refType);
        if (refType.Length > 0)
            sql += string.Format(" and ref.RefType='{0}'", refType);
        if (agentId.Length > 0 && agentId.ToLower() != "null" && agentId.ToLower() != "na")
            sql += string.Format(" and ref.AgentId='{0}'", agentId);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    public static DataTable dsVolumeByPort(string refType, DateTime d1, DateTime d2, string port, string userName)
    {
        string sql = string.Format(@"Select ref.RefNo,RefType,MAWB,CarrierBkgNo,EstCostAmt,EstSaleAmt,Weight,Qty,Volume,convert(nvarchar,FlightDate1,103) as Eta,convert(nvarchar,FlightDate0,103) as Etd, AirportCode0 as Port, 
AirportCode1, NvoccBlNO,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,Hbl.cnt as HblCnt,
'Date From {0} to {1}' as PeriodStr,'Import' as JobType,
case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent,'{4}' as UserId,
cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt,
cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt,
cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt 
FROM air_ref as ref
left outer join (select count(*) as cnt,refno from air_job where StatusCode='USE' group by refno) as Hbl on Hbl.refno=ref.refno
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{5}' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{5}' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='{5}' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM air_Costing where JobType='{5}' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE FlightDate0>='{2}' and FlightDate0<'{3}' and ref.StatusCode<>'CNL'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName, refType);
        if (refType.Length > 0)
            sql += string.Format(" and ref.RefType='{0}'", refType);
        if (port.Length > 0 && port.ToLower() != "null" && port.ToLower() != "na")
            sql += string.Format(" and ref.AirportCode0='{0}'", port);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    #endregion


    public static DataTable dsProfitBySales(string refType, DateTime d1, DateTime d2, string salesman, string userName)
    {

        string sql = "";
        string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"].ToUpper();
        string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
        if (refType != "E")
        {
            //0,1:d1,d2  2:role  3:ExceptSales   4:username   5:salesman  6:refType  7,8:where date
            sql = string.Format(@"SELECT ref.RefNo, ref.RefType as RefType, ref.AirportCode0 as Port, ref.CurrencyRate,convert(nvarchar,ref.FlightDate1,103) as Eta,convert(nvarchar,ref.FlightDate0,103) as Etd, job.JobNo, 
job.CustomerId, job.HAWB as Hbl, cast(isnull(job.Weight,0) as numeric(21,3)) as Wt, cast(isnull(job.Volume,0) as numeric(21,3)) as M3,ref.Volume as TotVolume,'Date From {0} to {1}' as PeriodStr,'Import' as ImpExpInd,
isnull(xxpPP.SalesmanId,'') as Salesman,case when len(isnull(job.CustomerId,''))>0 then '('+job.CustomerId+')'+isnull(xxpPP.Name,'')  else 'NA' end as Cust,
cast(isnull(invCN.Amt,0)+isnull(payable.Amt,0)*(case when isnull(ref.Volume,0)=0 and isnull(job.Volume,0)=0 then 1 else job.Volume end)/
(case when ISNULL(ref.Volume,0)=0 then 1 else ref.Volume end)  as numeric(20,2)) as CostAmt,
CAST(isnull(inv.Amt,0) as numeric(20,2)) as RevAmt,
CAST(isnull(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(payable.Amt,0)*(case when isnull(ref.Volume,0)=0 and isnull(job.Volume,0)=0 then 1 else job.Volume end)/
(case when ISNULL(ref.Volume,0)=0 then 1 else ref.Volume end)) as numeric(20,2)) as ProfitAmt,'{4}' as UserId
FROM air_job AS job INNER JOIN air_ref AS ref ON job.RefNo = ref.RefNo
left outer join XXParty as xxpPP on xxpPP.PartyId=job.CustomerId
left outer join (select MastRefNo,JobRefNo,sum(LocAmt) as Amt from XAArInvoice where doctype='CN' and MastType='{6}' group by MastRefNo,JobRefNo) as invCN on invCN.MastRefNo=ref.RefNo and invCN.JobRefNo=job.JobNo
left outer join (select MastRefNo,JobRefNo,sum(LocAmt) as Amt from XAArInvoice where doctype<>'CN' and MastType='{6}' group by MastRefNo,JobRefNo) as inv on inv.MastRefNo=ref.RefNo and inv.JobRefNo=job.JobNo
left outer join (select MastRefNo,SUM(case when doctype='SC' then -LocAmt else LocAmt end) as Amt from XAApPayable where MastType='{6}' group by MastRefNo) as payable on payable.MastRefNo=ref.RefNo
WHERE (ref.FlightDate1 >= '{7}') AND (ref.FlightDate1 <  '{8}') 
and (case when '{2}'='SALESMANAGER' and LEN('{3}')>0 and xxpPP.SalesmanId='{3}' then 0 
when '{2}'='SALESSTAFF' and xxpPP.SalesmanId<>'{4}' then 0
when '{5}'='All' then 1
when '{5}'='NA' then 1
when '{5}'=xxpPP.SalesmanId then 1
else 0 end)=1", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), role, exceptSales, userName, salesman, refType, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        }
        else
        {
            sql = string.Format(@"SELECT ref.RefNo, ref.RefType as RefType, ref.AirportCode1 as Port, ref.CurrencyRate,convert(nvarchar,ref.FlightDate1,103) as Eta,convert(nvarchar,ref.FlightDate0,103) as Etd, job.JobNo, 
job.CustomerId, job.HAWB  as Hbl, cast(isnull(job.Weight,0) as numeric(21,3)) as Wt, cast(isnull(job.Volume,0) as numeric(21,3)) as M3,ref.Volume as TotVolume,'Date From {0} to {1}' as PeriodStr,'Export' as ImpExpInd,
isnull(xxpPP.SalesmanId,'') as Salesman,case when len(isnull(job.CustomerId,''))>0 then '('+job.CustomerId+')'+isnull(xxpPP.Name,'')  else 'NA' end as Cust
FROM air_job AS job INNER JOIN air_ref AS ref ON job.RefNo = ref.RefNo
left outer join XXParty as xxpPP on xxpPP.PartyId=job.CustomerId
left outer join (select MastRefNo,JobRefNo,sum(LocAmt) as Amt from XAArInvoice where doctype='CN' and MastType='{6}' group by MastRefNo,JobRefNo) as invCN on invCN.MastRefNo=ref.RefNo and invCN.JobRefNo=job.JobNo
left outer join (select MastRefNo,JobRefNo,sum(LocAmt) as Amt from XAArInvoice where doctype<>'CN' and MastType='{6}' group by MastRefNo,JobRefNo) as inv on inv.MastRefNo=ref.RefNo and inv.JobRefNo=job.JobNo
left outer join (select MastRefNo,SUM(case when doctype='SC' then -LocAmt else LocAmt end) as Amt from XAApPayable where MastType='{6}' group by MastRefNo) as payable on payable.MastRefNo=ref.RefNo
WHERE (ref.FlightDate1 >= '{7}') AND (ref.FlightDate1 <  '{8}') 
and (case when '{2}'='SALESMANAGER' and LEN('{3}')>0 and xxpPP.SalesmanId='{3}' then 0 
when '{2}'='SALESSTAFF' and xxpPP.SalesmanId<>'{4}' then 0
when '{5}'='All' then 1
when '{5}'='NA' then 1
when '{5}'=xxpPP.SalesmanId then 1
else 0 end)=1", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), role, exceptSales, userName, salesman, refType, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        }
        sql += "  order by job.JobNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;



     
    }
    public static DataTable dsProfitByCust(string refType, DateTime d1, DateTime d2, string custId, string userName)
    {

        string sql = "";
        string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
        string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"].ToUpper();
        if (refType != "E")
        {
            sql = string.Format(@"SELECT ref.RefNo, ref.RefType as RefType, ref.AirportCode0 as Port, ref.CurrencyRate,convert(nvarchar,ref.FlightDate1,103) as Eta,convert(nvarchar,ref.FlightDate0,103) as Etd, 
job.JobNo, xxpPP.SalesmanId as Salesman, job.HAWB as Hbl, cast(isnull(job.Weight,0) as numeric(21,3)) as Wt, cast(isnull(job.Volume,0) as numeric(21,3)) as M3,ref.Volume as TotVolume,
'Date From {0} to {1}' as PeriodStr,'Import' as ImpExpInd,case when LEN(job.CustomerId)>0 then '('+job.CustomerId+')'+isnull(xxpPP.Name,'') else 'NA' end as Cust,
cast(isnull(invCN.Amt,0)+ISNULL(Payable.Amt,0)*(case when isnull(job.Volume,0)=0 and isnull(ref.Volume,0)=0 then 1 else job.Volume end)/
(case when ISNULL(ref.Volume,0)=0 then 1 else ref.Volume end) as numeric(20,2)) as CostAmt,
cast(isnull(inv.Amt,0) as numeric(20,2)) as RevAmt,cast(isnull(inv.Amt,0)-(isnull(invCN.Amt,0)+ISNULL(Payable.Amt,0)*(case when isnull(job.Volume,0)=0 and isnull(ref.Volume,0)=0 then 1 else job.Volume end)/
(case when ISNULL(ref.Volume,0)=0 then 1 else ref.Volume end)) as numeric(20,2)) as ProfitAmt,'{4}' as UserId
FROM air_job AS job INNER JOIN air_ref AS ref ON job.RefNo = ref.RefNo
left outer join XXParty as xxpPP on job.CustomerId=xxpPP.PartyId
left outer join (SELECT MastRefNo,JobRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE DocType='CN' and MastType='{5}' group by MastRefNo,JobRefNo) as invCN on invCN.MastRefNo=ref.RefNo and invCN.JobRefNo=job.JobNo 
left outer join (SELECT MastRefNo,JobRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE DocType<>'CN' and MastType='{5}' group by MastRefNo,JobRefNo) as inv on inv.MastRefNo=ref.RefNo and inv.JobRefNo=job.JobNo 
left outer join (select MastRefNo,SUM(case when doctype='SC' then -LocAmt else LocAmt end) as Amt from XAApPayable where MastType='{5}' group by MastRefNo,MastType) as Payable on Payable.MastRefNo=ref.RefNo
WHERE (ref.FlightDate1 >= '{6}') AND (ref.FlightDate1 < '{7}')
and (case when '{2}'='SALESMANAGER' and len('{3}')>0 and xxpPP.SalesmanId='{3}' then 0
when '{2}'='SALESSTAFF' and xxpPP.SalesmanId<>'{4}' then 0
else 1 end )=1", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), role, exceptSales, userName, refType, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        }
        else
        {
            sql = string.Format(@"SELECT ref.RefNo, ref.RefType as RefType, ref.AirportCode1 as Port, ref.CurrencyRate,convert(nvarchar,ref.FlightDate1,103) as Eta,convert(nvarchar,ref.FlightDate0,103) as Etd, 
job.JobNo, xxpPP.SalesmanId as Salesman, job.HAWB as Hbl, cast(isnull(job.Weight,0) as numeric(21,3)) as Wt, cast(isnull(job.Volume,0) as numeric(21,3)) as M3,ref.Volume as TotVolume,
'Date From {0} to {1}' as PeriodStr, 'Export' as ImpExpInd,case when LEN(job.CustomerId)>0 then '('+job.CustomerId+')'+isnull(xxpPP.Name,'') else 'NA' end as Cust
FROM air_job AS job INNER JOIN air_ref AS ref ON job.RefNo = ref.RefNo
left outer join XXParty as xxpPP on job.CustomerId=xxpPP.PartyId
left outer join (SELECT MastRefNo,JobRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE DocType='CN' and MastType='{5}' group by MastRefNo,JobRefNo) as invCN on invCN.MastRefNo=ref.RefNo and invCN.JobRefNo=job.JobNo 
left outer join (SELECT MastRefNo,JobRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE DocType<>'CN' and MastType='{5}' group by MastRefNo,JobRefNo) as inv on inv.MastRefNo=ref.RefNo and inv.JobRefNo=job.JobNo 
left outer join (select MastRefNo,SUM(case when doctype='SC' then -LocAmt else LocAmt end) as Amt from XAApPayable where MastType='{5}' group by MastRefNo,MastType) as Payable on Payable.MastRefNo=ref.RefNo
WHERE (ref.FlightDate0>= '{6}') AND (ref.FlightDate0 < '{7}')
and (case when '{2}'='SALESMANAGER' and len('{3}')>0 and xxpPP.SalesmanId='{3}' then 0
when '{2}'='SALESSTAFF' and xxpPP.SalesmanId<>'{4}' then 0
else 1 end )=1", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), role, exceptSales, userName, refType, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        }
        if (custId == "NA")
        {
        }
        else
            sql += " and Job.CustomerId='" + custId + "'";

        sql += "  order by job.JobNo";

        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;




    }

    public static DataTable dsProfit(DateTime d1, DateTime d2, string salesman, string userName)
    {
        DataTable det = new DataTable("Det");
        det.Columns.Add("PeriodStr");
        det.Columns.Add("ImpExpInd");
        det.Columns.Add("RefType");
        det.Columns.Add("Salesman");
        det.Columns.Add("Cnt");
        det.Columns.Add("RevAmt");
        det.Columns.Add("CostAmt");
        det.Columns.Add("ProfitAmt");
        det.Columns.Add("UserId");

        string sql = string.Format(@"SELECT ref.RefNo, ref.RefType, ref.AirportCode0 as Port, ref.CurrencyRate,ref.FlightDate1 as Eta,ref.FlightDate0 as Etd, job.JobNo, job.CustomerId, job.HAWB, job.Weight, job.Volume,ref.Volume as TotVolume
FROM air_job AS job INNER JOIN air_ref AS ref ON job.RefNo = ref.RefNo
WHERE (ref.Eta >= '{0}') AND (ref.Eta < '{1}')", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        sql += "  order by job.JobNo";
        string periodStr = string.Format("Date From {0} to {1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
        string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();

        DataTable tab1 = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            try
            {
                DataRow row = tab1.Rows[i];
                string partyId = SafeValue.SafeString(row["CustomerId"], "");
                string salesmanId = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("Select SalesmanId from XXParty where PartyId='{0}'", partyId)), "").ToUpper();
                salesman = salesman.ToUpper();

              
                if (salesman == "All" || salesman == "NA" || salesman == salesmanId)
                {
                    DataRow rptRow = det.NewRow();
                    rptRow["PeriodStr"] = periodStr;
                    rptRow["ImpExpInd"] = "Import";
                    string refNo = row["RefNo"].ToString();
                    string jobNo = row["JobNo"].ToString();
                    string refType = row["JobType"].ToString().ToUpper();
                    if (refType == "CONSOL")
                        continue;
                    rptRow["RefType"] = refType;
                    rptRow["Salesman"] = salesmanId;
                    rptRow["CostAmt"] = 0;
                    rptRow["RevAmt"] = 0;



                    string sql_Iv = string.Format("SELECT DocType, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{0}' and (MastRefNo = '{1}') and JobRefNo='{2}' group by DocType", refType, refNo, jobNo);
                    DataTable tab_Iv = ConnectSql.GetTab(sql_Iv);
                    for (int r = 0; r < tab_Iv.Rows.Count; r++)
                    {
                        string docType = tab_Iv.Rows[r]["DocType"].ToString();
                        decimal amt = SafeValue.SafeDecimal(tab_Iv.Rows[r]["Amt"], 0);
                        if (docType == "CN")
                        {
                            rptRow["CostAmt"] = (SafeValue.SafeDecimal(rptRow["CostAmt"], 0) + amt).ToString("0.00");
                        }
                        else
                        {
                            rptRow["RevAmt"] = (SafeValue.SafeDecimal(rptRow["RevAmt"], 0) + amt).ToString("0.00");
                        }
                    }
                    decimal jobM3 = SafeValue.SafeDecimal(row["Volume"], 0);
                    decimal totM3 = SafeValue.SafeDecimal(row["TotVolume"], 0);
                    if (totM3 == 0 && jobM3 == 0)
                    {
                        jobM3 = 1;
                        totM3 = 1;
                    }
                    else if (totM3 == 0)
                        totM3 = 1;
                    string sql_Vo = string.Format("SELECT DocType,sum(LocAmt) as Amt FROM XAApPayable WHERE MastType='{0}' and (MastRefNo = '{1}') group by DocType", refType, refNo);
                    DataTable tab_Vo = ConnectSql.GetTab(sql_Vo);
                    for (int r = 0; r < tab_Vo.Rows.Count; r++)
                    {
                        string docType = tab_Vo.Rows[r]["DocType"].ToString();
                        decimal amt = SafeValue.SafeDecimal(tab_Vo.Rows[r]["Amt"], 0);
                        if (docType == "SC")
                        {
                            rptRow["CostAmt"] = (SafeValue.SafeDecimal(rptRow["CostAmt"], 0) - SafeValue.ChinaRound(amt * jobM3 / totM3, 2)).ToString("0.00");
                        }
                        else
                        {
                            rptRow["CostAmt"] = (SafeValue.SafeDecimal(rptRow["CostAmt"], 0) + SafeValue.ChinaRound(amt * jobM3 / totM3, 2)).ToString("0.00");
                        }
                    }
                    rptRow["ProfitAmt"] = (SafeValue.SafeDecimal(rptRow["RevAmt"], 0) - SafeValue.SafeDecimal(rptRow["CostAmt"], 0)).ToString("0.00");
                    rptRow["UserId"] = userName;

                    det.Rows.Add(rptRow);
                }
            }
            catch { }
        }

        //EXPORT
        sql = string.Format(@"SELECT ref.RefNo, ref.RefType, ref.AirportCode1 Port, ref.CurrencyRate,ref.FlightDate1 as Eta,ref.FlightDate0 as Etd, job.JobNo, job.CustomerId, job.HAWB, job.Weight, job.Volume,ref.Volume as TotVolume
FROM air_job AS job INNER JOIN air_ref AS ref ON job.RefNo = ref.RefNo
WHERE (ref.Etd >= '{0}') AND (ref.Etd < '{1}')", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));

        sql += "  order by job.JobNo";

        tab1 = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            try
            {
                DataRow row = tab1.Rows[i];
                string partyId = SafeValue.SafeString(row["CustomerId"], "");
                string salesmanId = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("Select SalesmanId from XXParty where PartyId='{0}'", partyId)), "").ToUpper();
                salesman = salesman.ToUpper();


                if (salesman == "All" || salesman == "NA" || salesman == salesmanId)
                {
                    DataRow rptRow = det.NewRow();
                    rptRow["PeriodStr"] = periodStr;
                    rptRow["ImpExpInd"] = "Export";
                    string refNo = row["RefNo"].ToString();
                    string jobNo = row["JobNo"].ToString();
                    string refType = row["JobType"].ToString().ToUpper();
                    if (refType == "CONSOL")
                        continue;
                    rptRow["RefType"] = refType;
                    rptRow["Salesman"] = salesmanId;
                    rptRow["CostAmt"] = 0;
                    rptRow["RevAmt"] = 0;



                    string sql_Iv = string.Format("SELECT DocType, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{0}' and (MastRefNo = '{1}') and JobRefNo='{2}' group by DocType", refType, refNo, jobNo);
                    DataTable tab_Iv = ConnectSql.GetTab(sql_Iv);
                    for (int r = 0; r < tab_Iv.Rows.Count; r++)
                    {
                        string docType = tab_Iv.Rows[r]["DocType"].ToString();
                        decimal amt = SafeValue.SafeDecimal(tab_Iv.Rows[r]["Amt"], 0);
                        if (docType == "CN")
                        {
                            rptRow["CostAmt"] = (SafeValue.SafeDecimal(rptRow["CostAmt"], 0) + amt).ToString("0.00");
                        }
                        else
                        {
                            rptRow["RevAmt"] = (SafeValue.SafeDecimal(rptRow["RevAmt"], 0) + amt).ToString("0.00");
                        }
                    }
                    decimal jobM3 = SafeValue.SafeDecimal(row["Volume"], 0);
                    decimal totM3 = SafeValue.SafeDecimal(row["TotVolume"], 0);
                    if (totM3 == 0 && jobM3 == 0)
                    {
                        jobM3 = 1;
                        totM3 = 1;
                    }
                    else if (totM3 == 0)
                        totM3 = 1;
                    string sql_Vo = string.Format("SELECT DocType,sum(LocAmt) as Amt FROM XAApPayable WHERE MastType='{0}' and (MastRefNo = '{1}') group by DocType", refType, refNo);
                    DataTable tab_Vo = ConnectSql.GetTab(sql_Vo);
                    for (int r = 0; r < tab_Vo.Rows.Count; r++)
                    {
                        string docType = tab_Vo.Rows[r]["DocType"].ToString();
                        decimal amt = SafeValue.SafeDecimal(tab_Vo.Rows[r]["Amt"], 0);
                        if (docType == "SC")
                        {
                            rptRow["CostAmt"] = (SafeValue.SafeDecimal(rptRow["CostAmt"], 0) - SafeValue.ChinaRound(amt * jobM3 / totM3, 2)).ToString("0.00");
                        }
                        else
                        {
                            rptRow["CostAmt"] = (SafeValue.SafeDecimal(rptRow["CostAmt"], 0) + SafeValue.ChinaRound(amt * jobM3 / totM3, 2)).ToString("0.00");
                        }
                    }

                    decimal exRate = SafeValue.SafeDecimal(row["ExRate"], 0);
                    if (exRate == 0)
                        exRate = 1;
                    string sql_detail = string.Format("SELECT  Amount, Currency FROM SeaExportDetail where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                    DataTable tab_detail = ConnectSql.GetTab(sql_detail);
                    for (int r = 0; r < tab_detail.Rows.Count; r++)
                    {
                        string cur = tab_detail.Rows[r]["Currency"].ToString();
                        decimal amt = SafeValue.SafeDecimal(tab_detail.Rows[r]["Amt"], 0);
                        if (cur == "SGD")
                        {
                            rptRow["CostAmt"] = (SafeValue.SafeDecimal(rptRow["CostAmt"], 0) + amt).ToString("0.00");
                        }
                        else
                        {
                            rptRow["CostAmt"] = (SafeValue.SafeDecimal(rptRow["CostAmt"], 0) + amt * exRate).ToString("0.00");
                        }
                    }

                    rptRow["ProfitAmt"] = (SafeValue.SafeDecimal(rptRow["RevAmt"], 0) - SafeValue.SafeDecimal(rptRow["CostAmt"], 0)).ToString("0.00");
                    rptRow["UserId"] = userName;

                    det.Rows.Add(rptRow);
                }
            }
            catch { }
        }
        DataSetHelper help = new DataSetHelper();
        DataTable rptTab = help.SelectGroupByInto("rptTab", det, "PeriodStr,Salesman,ImpExpInd,RefType,count(Salesman) Cnt,sum(RevAmt) RevAmt,sum(CostAmt) CostAmt,sum(ProfitAmt) ProfitAmt", "",
            "PeriodStr,Salesman,ImpExpInd,RefType");


        return rptTab;
    }
}