﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using C2;

public static class SeaFreightRptPrint
{
    #region account
    public static DataTable dsInvoiceByDate(string billType, string party, DateTime d1, DateTime d2, string sortFiled, string userName,string refType)
    {
        string sql = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, MastRefNo, MastType, inv.CurrencyId as Currency, 
cast(isnull(inv.ExRate,0) as numeric(21,6)) as ExRate, Term,DocAmt, LocAmt as Amt,
case '{0}' when 'CN' then 'Credit Note ' when 'DN' then 'Debit Note ' else 'Invoice ' end +'List From {1} to {2}' as PeriodStr,
case when inv.MastType='SE' then ExRef.Pol else ImRef.Pol end as Pol,case when inv.MastType='SE' then ExRef.Pod else ImRef.Pod end as Pod,
inv.MastRefNo as RefNo,'{3}' as UserId
FROM XAArInvoice as inv
left outer join SeaExportRef as ExRef on ExRef.RefNo=inv.MastRefNo
left outer join SeaImportRef as ImRef on ImRef.RefNo=inv.MastRefNo
where DocType='{0}' and DocDate>='{4}' and DocDate<'{5}'  and ExRef.statuscode<>'CNL' and ImRef.statuscode<>'CNL'", billType, d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), userName, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        if (refType.Trim().Length > 0)
        {
            if (refType == "SI")
            {
                sql += " and MastType='" + refType + "'";
            }
            else
            {
                sql += " and ExRef.RefType like '" + refType + "%'";
            }
        }
        if (party.Length > 0 && party != "null")
            sql += string.Format(" and PartyTo='{0}'", party);
        sql += " order by " + sortFiled;
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;

    }
    public static DataTable dsInvoiceByNo(string billType, string party, string docNo1, string docNo2, string sortFiled, string userName, string refType)
    {
        string sql = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, MastRefNo, MastType, inv.CurrencyId as Currency, 
cast(isnull(inv.ExRate,0) as numeric(21,6)) as ExRate, Term,DocAmt, LocAmt as Amt,
case '{0}' when 'CN' then 'Credit Note ' when 'DN' then 'Debit Note ' else 'Invoice ' end +'List From {1} to {2}' as PeriodStr,
case when inv.MastType='SE' then ExRef.Pol else ImRef.Pol end as Pol,case when inv.MastType='SE' then ExRef.Pod else ImRef.Pod end as Pod,
inv.MastRefNo as RefNo,'{3}' as UserId
FROM XAArInvoice as inv
left outer join SeaExportRef as ExRef on ExRef.RefNo=inv.MastRefNo
left outer join SeaImportRef as ImRef on ImRef.RefNo=inv.MastRefNo
where DocType='{0}' and DocNo>='{4}' and DocNo<'{5}'  and ExRef.statuscode<>'CNL' and ImRef.statuscode<>'CNL'", billType, docNo1, docNo2, userName, docNo1, docNo2);

        if (refType.Trim().Length > 0)
        {
            if (refType == "SI")
            {
                sql += " and MastType='" + refType + "'";
            }
            else
            {
                sql += " and ExRef.RefType like '" + refType + "%'";
            }
        }
        if (party.Length > 0 && party != "null")
            sql += string.Format(" and PartyTo='{0}'", party);
        sql += " order by " + sortFiled;
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;


    }
    public static DataTable dsVoucherByDate(string billType, string party, DateTime d1, DateTime d2, string sortFiled, string userName, string refType)
    {

        string sql = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, 
MastRefNo as RefNo, MastType, PayAble.CurrencyId as Currency, cast(isnull(PayAble.ExRate,0) as numeric(21,6)) as ExRate,ChqNo as Term,DocAmt, LocAmt as Amt,
'Voucher List From {0} to {1}' as PeriodStr,case when PayAble.MastType='SE' then ExJob.Pol else ImJob.Pol end as Pol,
case when PayAble.MastType='SE' then ExJob.Pod else ImJob.Pod end as Pod,'{2}' as UserId
FROM XAApPayable as PayAble
left outer join SeaExportRef as ExJob on PayAble.MastRefNo=ExJob.RefNo
left outer join SeaImportRef as ImJob on PayAble.MastRefNo=ImJob.RefNo 
where DocType='{3}' and DocDate>='{4}' and DocDate<'{5}' and ExJob.statuscode<>'CNL' and ImJob.statuscode<>'CNL'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), userName, billType, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));

        if (refType.Trim().Length > 0)
        {
            if (refType == "SI")
            {
                sql += " and MastType='" + refType + "'";
            }
            else
            {
                sql += " and ExJob.RefType like '" + refType + "%'";
            }
        }
        if (party.Length > 0 && party != "null")
            sql += string.Format(" and PartyTo='{0}'", party);
        sql += " order by " + sortFiled;
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;

    }
    public static DataTable dsVoucherByNo(string billType, string party, string docNo1, string docNo2, string sortFiled, string userName, string refType)
    {
        string sql = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, 
MastRefNo as RefNo, MastType, PayAble.CurrencyId as Currency, cast(isnull(PayAble.ExRate,0) as numeric(21,6)) as ExRate,ChqNo as Term,DocAmt, LocAmt as Amt,
'Voucher List From {0} to {1}' as PeriodStr,case when PayAble.MastType='SE' then ExJob.Pol else ImJob.Pol end as Pol,
case when PayAble.MastType='SE' then ExJob.Pod else ImJob.Pod end as Pod,'{2}' as UserId
FROM XAApPayable as PayAble
left outer join SeaExportRef as ExJob on PayAble.MastRefNo=ExJob.RefNo 
left outer join SeaImportRef as ImJob on PayAble.MastRefNo=ImJob.RefNo 
where DocType='{3}' and DocNo>='{4}' and DocNo<'{5}' and ExJob.statuscode<>'CNL' and ImJob.statuscode<>'CNL'", docNo1, docNo2, userName, billType, docNo1, docNo2);
        if (refType.Trim().Length > 0)
        {
            if (refType == "SI")
            {
                sql += " and MastType='" + refType + "'";
            }
            else
            {
                sql += " and ExJob.RefType like '" + refType + "%'";
            }
        }
        if (party.Length > 0)
            sql += string.Format(" and PartyTo='{0}'", party);
        sql += " order by " + sortFiled;
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    #endregion

    #region import
    public static DataTable dsImportVolumeByDate(string refType, DateTime d1, DateTime d2, string userName)
    {
        string sql = string.Format(@"SELECT ref.RefNo,JobType as RefType, Vessel+'/'+Voyage as VesVoy, convert(nvarchar,Eta,103) as Eta,convert(nvarchar,Etd,103) as Etd, Pol as Port, 
Pod, OblNo,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,
'Date From {0} to {1}' as PeriodStr,'Import' as JobType,
case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent,
isnull(Cont20.Cnt,0) as Ft20,ISNULL(cont40.cnt,0) as Ft40,ISNULL(hblCnt.Amt,0) as HblCnt,
cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt,cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt,
cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt,'{4}' as UserId 
FROM SeaImportRef as ref
left outer join (select COUNT(*) as Cnt,RefNo from SeaImportMkg where StatusCode='USE' and MkgType='Cont' and ContainerType like '%20%' group by RefNo) as Cont20 on ref.RefNo=Cont20.RefNo
left outer join (select COUNT(*) as Cnt,RefNo from SeaImportMkg where StatusCode='USE' and MkgType='Cont' and ContainerType not like '%20%' group by RefNo) as Cont40 on ref.RefNo=Cont40.RefNo
left outer join (SELECT count(JobNo) as Amt,RefNo FROM SeaImport where StatusCode='USE' group by RefNo) as hblCnt on ref.RefNo=hblCnt.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SI' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SI' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='SI' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM SeaCosting where JobType='SI' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE ref.StatusCode<>'CNL' and Eta>='{2}' and Eta<'{3}'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName);
        if (refType.Length > 0)
            sql += string.Format(" and ref.JobType='{0}'", refType);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    public static DataTable dsImportVolumeByAgent(string refType, DateTime d1, DateTime d2, string agentId, string userName)
    {
        string sql = string.Format(@"SELECT ref.RefNo,JobType as RefType, Vessel+'/'+Voyage as VesVoy, convert(nvarchar,Eta,103) as Eta,convert(nvarchar,Etd,103) as Etd, Pol as Port, 
Pod, OblNo,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,
'Date From {0} to {1}' as PeriodStr,'Import' as JobType,
case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent,
isnull(Cont20.Cnt,0) as Ft20,ISNULL(cont40.cnt,0) as Ft40,ISNULL(hblCnt.Amt,0) as HblCnt,
cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt,cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt,
cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt,'{4}' as UserId 
FROM SeaImportRef as ref
left outer join (select COUNT(*) as Cnt,RefNo from SeaImportMkg where StatusCode='USE' and MkgType='Cont' and ContainerType like '%20%' group by RefNo) as Cont20 on ref.RefNo=Cont20.RefNo
left outer join (select COUNT(*) as Cnt,RefNo from SeaImportMkg where StatusCode='USE' and MkgType='Cont' and ContainerType not like '%20%' group by RefNo) as Cont40 on ref.RefNo=Cont40.RefNo
left outer join (SELECT count(JobNo) as Amt,RefNo FROM SeaImport where StatusCode='USE' group by RefNo) as hblCnt on ref.RefNo=hblCnt.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SI' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SI' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='SI' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM SeaCosting where JobType='SI' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE ref.StatusCode<>'CNL' and Eta>='{2}' and Eta<'{3}'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName);
        if (refType.Length > 0)
            sql += string.Format(" and ref.JobType='{0}'", refType);
        if (agentId.Length > 0 && agentId.ToLower() != "null" && agentId.ToLower() != "na")
            sql += string.Format(" and ref.AgentId='{0}'", agentId);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    public static DataTable dsImportVolumeByPort(string refType, DateTime d1, DateTime d2, string port, string userName)
    {
        string sql = string.Format(@"SELECT ref.RefNo,JobType as RefType, Vessel+'/'+Voyage as VesVoy, convert(nvarchar,Eta,103) as Eta,convert(nvarchar,Etd,103) as Etd, Pol as Port, 
Pod, OblNo,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,
'Date From {0} to {1}' as PeriodStr,'Import' as JobType,
case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent,
isnull(Cont20.Cnt,0) as Ft20,ISNULL(cont40.cnt,0) as Ft40,ISNULL(hblCnt.Amt,0) as HblCnt,
cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt,cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt,
cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt,'{4}' as UserId 
FROM SeaImportRef as ref
left outer join (select COUNT(*) as Cnt,RefNo from SeaImportMkg where StatusCode='USE' and MkgType='CONT' and ContainerType like '%20%' group by RefNo) as Cont20 on ref.RefNo=Cont20.RefNo
left outer join (select COUNT(*) as Cnt,RefNo from SeaImportMkg where StatusCode='USE' and MkgType='CONT' and ContainerType not like '%20%' group by RefNo) as Cont40 on ref.RefNo=Cont40.RefNo
left outer join (SELECT count(JobNo) as Amt,RefNo FROM SeaImport where StatusCode='USE' group by RefNo) as hblCnt on ref.RefNo=hblCnt.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SI' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SI' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='SI' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM SeaCosting where JobType='SI' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE ref.StatusCode<>'CNL' and Eta>='{2}' and Eta<'{3}'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName);
        if (refType.Length > 0)
            sql += string.Format(" and ref.JobType='{0}'", refType);
        if (port.Length > 0 && port.ToLower() != "null" && port.ToLower() != "na")
            sql += string.Format(" and ref.Pol='{0}'", port);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    #endregion

    #region Export
    public static DataTable dsExportVolumeByDate(string refType, string jobType, DateTime d1, DateTime d2, string userName)
    {
        string sql = string.Format(@"SELECT ref.RefNo,JobType as RefType, Vessel+'/'+Voyage as VesVoy, convert(nvarchar,Eta,103) as Eta,convert(nvarchar,Etd,103) as Etd, Pol as Port, 
Pod, OblNo,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,
'Date From {0} to {1}' as PeriodStr,'Export' as JobType,
case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent,
isnull(Cont20.Cnt,0) as Ft20,ISNULL(cont40.cnt,0) as Ft40,ISNULL(hblCnt.Amt,0) as HblCnt,
cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt,cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt,
cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt,'{4}' as UserId 
FROM SeaExportRef as ref
left outer join (select COUNT(*) as Cnt,RefNo from SeaExportMkg where StatusCode='USE' and MkgType='Cont' and ContainerType like '%20%' group by RefNo) as Cont20 on ref.RefNo=Cont20.RefNo
left outer join (select COUNT(*) as Cnt,RefNo from SeaExportMkg where StatusCode='USE' and MkgType='Cont' and ContainerType not like '%20%' group by RefNo) as Cont40 on ref.RefNo=Cont40.RefNo
left outer join (SELECT count(JobNo) as Amt,RefNo FROM SeaExport where StatusCode='USE' group by RefNo) as hblCnt on ref.RefNo=hblCnt.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SE' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SE' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='SE' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM SeaCosting where JobType='SE' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE ref.StatusCode<>'CNL' and Eta>='{2}' and Eta<'{3}'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName);
        if (refType != "null")
        {
            sql += string.Format(" and ref.RefType LIKE '{0}%'", refType);
        }
        if (jobType != "null")
            sql += string.Format(" and ref.JobType='{0}'", jobType);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    public static DataTable dsExportVolumeByAgent(string refType, string jobType, DateTime d1, DateTime d2, string agentId, string userName)
    {
        string sql = string.Format(@"SELECT ref.RefNo,ref.JobType as RefType, Vessel+'/'+Voyage as VesVoy, convert(nvarchar,Eta,103) as Eta,convert(nvarchar,Etd,103) as Etd,Pod as Port, 
Pod, OblNo,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,
'Date From {0} to {1}' as PeriodStr
,'Export' as JobType
,case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent
,isnull(Cont20.Cnt,0) as Ft20
,ISNULL(cont40.cnt,0) as Ft40
,ISNULL(hblCnt.Amt,0) as HblCnt
,cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt
,cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt
,cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt
,'{4}' as UserId 
FROM SeaExportRef as ref
left outer join (select COUNT(*) as Cnt,RefNo from SeaExportMkg where StatusCode='USE' and MkgType='Cont' and ContainerType like '%20%' group by RefNo) as Cont20 on ref.RefNo=Cont20.RefNo
left outer join (select COUNT(*) as Cnt,RefNo from SeaExportMkg where StatusCode='USE' and MkgType='Cont' and ContainerType not like '%20%' group by RefNo) as Cont40 on ref.RefNo=Cont40.RefNo
left outer join (SELECT count(*) as Amt,RefNo FROM SeaExport where StatusCode='USE' group by RefNo) as hblCnt on ref.RefNo=hblCnt.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SE' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SE' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='SE' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM SeaCosting where JobType='SE' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE ref.StatusCode<>'CNL' and Eta>='{2}' and Eta<'{3}'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName);
        if (refType != "null")
        {
            sql += string.Format(" and ref.RefType LIKE '{0}%'", refType);
        }
        if (jobType != "null")
            sql += string.Format(" and ref.JobType='{0}'", jobType);
        if (agentId.Length > 0 && agentId.ToLower() != "null" && agentId.ToLower() != "na")
            sql += string.Format(" and ref.AgentId='{0}'", agentId);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    public static DataTable dsExportVolumeByPort(string refType, string jobType, DateTime d1, DateTime d2, string port, string userName)
    {
        string sql = string.Format(@"SELECT ref.RefNo,JobType as RefType, Vessel+'/'+Voyage as VesVoy, convert(nvarchar,Eta,103) as Eta,convert(nvarchar,Etd,103) as Etd, Pol as Port, 
Pod, OblNo,cast(isnull(Volume,0) as numeric(21,3)) as M3, cast(isnull(Weight,0) as numeric(21,3)) as Wt,
'Date From {0} to {1}' as PeriodStr,'Export' as JobType,
case when isnull(AgentId,'')='' then 'NA' else '('+AgentId+')'+dbo.fun_GetPartyName(AgentId) end as Agent,
isnull(Cont20.Cnt,0) as Ft20,ISNULL(cont40.cnt,0) as Ft40,ISNULL(hblCnt.Amt,0) as HblCnt,
cast(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0) as numeric(20,2)) as CostAmt,cast(ISNULL(inv.Amt,0) as numeric(20,2)) as RevAmt,
cast(ISNULL(inv.Amt,0)-(isnull(invCN.Amt,0)+isnull(pay.Amt,0)+ISNULL(cost.Amt,0)) as numeric(20,2)) as ProfitAmt,'{4}' as UserId 
FROM SeaExportRef as ref
left outer join (select COUNT(*) as Cnt,RefNo from SeaExportMkg where StatusCode='USE' and MkgType='CONT' and ContainerType like '%20%' group by RefNo) as Cont20 on ref.RefNo=Cont20.RefNo
left outer join (select COUNT(*) as Cnt,RefNo from SeaExportMkg where StatusCode='USE' and MkgType='CONT' and ContainerType not like '%20%' group by RefNo) as Cont40 on ref.RefNo=Cont40.RefNo
left outer join (SELECT count(JobNo) as Amt,RefNo FROM SeaExport where StatusCode='USE' group by RefNo) as hblCnt on ref.RefNo=hblCnt.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SE' and DocType='CN' group by MastRefNo) as invCN on invCN.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='SE' and DocType<>'CN' group by MastRefNo) as inv on inv.MastRefNo=ref.RefNo
left outer join (SELECT MastRefNo,sum(case when DocType='sc' then -LocAmt else LocAmt end) Amt FROM XAApPayable WHERE MastType='SE' group by MastRefNo) as pay on pay.MastRefNo=ref.RefNo
left outer join (select sum(CostLocAmt) Amt,RefNo FROM SeaCosting where JobType='SE' group by RefNo) as cost on cost.RefNo=ref.RefNo
WHERE ref.StatusCode<>'CNL' and Eta>='{2}' and Eta<'{3}'", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), userName);
        if (refType != "null")
        {
            sql += string.Format(" and ref.RefType LIKE '{0}%'", refType);
        }
        if (jobType != "null")
            sql += string.Format(" and ref.JobType='{0}'", jobType);
        if (port.Length > 0 && port.ToLower() != "null" && port.ToLower() != "na")
            sql += string.Format(" and ref.Pol='{0}'", port);
        sql += "  order by ref.RefNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;
    }
    #endregion

    public static DataTable dsProfitBySales(string refType, string type, DateTime d1, DateTime d2, string salesman, string userName)
    {

        string sql = "";
        string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"].ToUpper();
        string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
        if (type != "E")
        {
            sql = string.Format(@"SELECT ref.RefNo, ref.JobType as RefType, ref.Pol as Port, ref.ExRate,convert(nvarchar,ref.Eta,103) as Eta,convert(nvarchar,ref.Etd,103) as Etd, job.JobNo, 
job.CustomerId, job.HblNo as Hbl, cast(isnull(job.Weight,0) as numeric(21,3)) as Wt, cast(isnull(job.Volume,0) as numeric(21,3)) as M3,ref.Volume as TotVolume,'Date From {0} to {1}' as PeriodStr,'Import' as ImpExpInd,
isnull(xxpPP.SalesmanId,'') as Salesman,case when len(isnull(job.CustomerId,''))>0 then '('+job.CustomerId+')'+isnull(xxpPP.Name,'')  else 'NA' end as Cust
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','CN')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','PL')-dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','SC')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','SD')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','VO') as CostAmt
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','IV')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','DN') as RevAmt
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','IV')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','DN')-(dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','CN')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','PL')-dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','SC')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','SD')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','VO')) as ProfitAmt
,'{4}' as UserId
FROM SeaImport AS job INNER JOIN SeaImportRef AS ref ON job.RefNo = ref.RefNo
left outer join XXParty as xxpPP on xxpPP.PartyId=job.CustomerId
WHERE ref.StatusCode<>'CNL' and (ref.Eta >= '{7}') AND (ref.Eta <  '{8}') 
and (case when '{2}'='SALESMANAGER' and LEN('{3}')>0 and xxpPP.SalesmanId='{3}' then 0 
when '{2}'='SALESSTAFF' and xxpPP.SalesmanId<>'{4}' then 0
when '{5}'='All' then 1
when '{5}'='NA' then 1
when '{5}'=xxpPP.SalesmanId then 1
else 0 end)=1", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), role, exceptSales, userName, salesman, type, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        }
        else
        {
            sql = string.Format(@"SELECT ref.RefNo, ref.JobType as RefType, ref.Pod as Port, ref.ExRate,convert(nvarchar,ref.Eta,103) as Eta,convert(nvarchar,ref.Etd,103) as Etd, job.JobNo, 
job.CustomerId, job.HblNo as Hbl, cast(isnull(job.Weight,0) as numeric(21,3)) as Wt, cast(isnull(job.Volume,0) as numeric(21,3)) as M3,ref.Volume as TotVolume,'Date From {0} to {1}' as PeriodStr,'Export' as ImpExpInd,
isnull(xxpPP.SalesmanId,'') as Salesman,case when len(isnull(job.CustomerId,''))>0 then '('+job.CustomerId+')'+isnull(xxpPP.Name,'')  else 'NA' end as Cust
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','CN')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','PL')-dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','SC')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','SD')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','VO') as CostAmt
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','IV')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','DN') as RevAmt
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','IV')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','DN')-(dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','CN')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','PL')-dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','SC')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','SD')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','VO')) as ProfitAmt
,'{4}' as UserId
FROM SeaExport AS job INNER JOIN SeaExportRef AS ref ON job.RefNo = ref.RefNo
left outer join XXParty as xxpPP on xxpPP.PartyId=job.CustomerId
WHERE ref.StatusCode<>'CNL' and (ref.Eta >= '{7}') AND (ref.Eta <  '{8}') 
and (case when '{2}'='SALESMANAGER' and LEN('{3}')>0 and xxpPP.SalesmanId='{3}' then 0 
when '{2}'='SALESSTAFF' and xxpPP.SalesmanId<>'{4}' then 0
when '{5}'='All' then 1
when '{5}'='NA' then 1
when '{5}'=xxpPP.SalesmanId then 1
else 0 end)=1 and (ref.RefType LIKE '{9}%')", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), role, exceptSales, userName, salesman, type, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), refType.Replace("null",""));
        }
        sql += "  order by job.JobNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;

    }
    public static DataTable dsProfitByCust(string refType, string type, DateTime d1, DateTime d2, string custId, string userName)
    {
        string role = EzshipHelper.GetUseRole();
        string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"].ToUpper();
        string sql = "";
        if (type != "E")
        {
            sql = string.Format(@"SELECT ref.RefNo, ref.JobType as RefType, ref.Pol as Port, ref.ExRate,convert(nvarchar,ref.Eta,103) as Eta,convert(nvarchar,ref.Etd,103) as Etd, 
job.JobNo, xxpPP.SalesmanId as Salesman, job.HblNo as Hbl, cast(isnull(job.Weight,0) as numeric(21,3)) as Wt, cast(isnull(job.Volume,0) as numeric(21,3)) as M3,ref.Volume as TotVolume,
'Date From {0} to {1}' as PeriodStr,'Import' as ImpExpInd,case when LEN(job.CustomerId)>0 then '('+job.CustomerId+')'+isnull(xxpPP.Name,'') else 'NA' end as Cust
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','CN')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','PL')- dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','SC')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','SD')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','VO') as CostAmt
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','IV')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','DN') as RevAmt
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','IV')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','DN')-(dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','CN')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','PL')-dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','SC')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','SD')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'si','VO')) as ProfitAmt
,'{4}' as UserId
FROM SeaImport AS job INNER JOIN SeaImportRef AS ref ON job.RefNo = ref.RefNo
left outer join XXParty as xxpPP on job.CustomerId=xxpPP.PartyId  
WHERE ref.StatusCode<>'CNL' and (ref.Eta >= '{6}') AND (ref.Eta < '{7}')
and (case when '{2}'='SALESMANAGER' and len('{3}')>0 and xxpPP.SalesmanId='{3}' then 0
when '{2}'='SALESSTAFF' and xxpPP.SalesmanId<>'{4}' then 0
else 1 end )=1", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), role, exceptSales, userName, type, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
        }
        else
        {
            sql = string.Format(@"SELECT ref.RefNo, ref.JobType as RefType, ref.Pod as Port, ref.ExRate,convert(nvarchar,ref.Eta,103) as Eta,convert(nvarchar,ref.Etd,103) as Etd, 
job.JobNo, xxpPP.SalesmanId as Salesman, job.HblNo as Hbl, cast(isnull(job.Weight,0) as numeric(21,3)) as Wt, cast(isnull(job.Volume,0) as numeric(21,3)) as M3,ref.Volume as TotVolume
,'Date From {0} to {1}' as PeriodStr, 'Export' as ImpExpInd,case when LEN(job.CustomerId)>0 then '('+job.CustomerId+')'+isnull(xxpPP.Name,'') else 'NA' end as Cust
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','CN')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','PL')- dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','SC')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','SD')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','VO') as CostAmt
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','IV')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','DN') as RevAmt
,dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','IV')+dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','DN')-(dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','CN')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','PL')- dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','SC')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','SD')+ dbo.Fun_GetBillAmt(job.RefNo,job.JobNo,'SE','VO')) as ProfitAmt
,'{4}' as UserId
FROM SeaExport AS job INNER JOIN SeaExportRef AS ref ON job.RefNo = ref.RefNo
left outer join XXParty as xxpPP on job.CustomerId=xxpPP.PartyId 
WHERE ref.StatusCode<>'CNL' and (ref.Etd >= '{6}') AND (ref.Etd < '{7}')and 
(case when '{2}'='SALESSTAFF' and '{4}'<>xxpPP.Salesmanid then 0
else 1 end )=1 and (ref.RefType LIKE '{8}%')", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"), role, exceptSales, userName, type, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), refType.Replace("null",""));
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

        string sql = string.Format(@"SELECT ref.RefNo, ref.JobType, ref.Pol as Port, ref.ExRate,ref.Eta as Eta,ref.Etd as Etd, job.JobNo, job.CustomerId, job.HblNo, job.Weight, job.Volume,ref.Volume as TotVolume
FROM SeaImport AS job INNER JOIN SeaImportRef AS ref ON job.RefNo = ref.RefNo
WHERE     (ref.Eta >= '{0}') AND (ref.Eta < '{1}')", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));
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

                //if (role == "SALESMANAGER")
                //{
                //    string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"].ToUpper();
                //    if (exceptSales.Length > 0 && salesmanId == exceptSales)
                //    {
                //        continue;
                //    }
                //}
                //else if (role == "SALESSTAFF")
                //{
                //    if (salesmanId != userName)
                //        continue;
                //}
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



                    string sql_Iv = string.Format("SELECT DocType, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{0}' and (MastRefNo = '{1}') and JobRefNo='{2}' group by DocType", "I", refNo, jobNo);
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
                    string sql_Vo = string.Format("SELECT DocType,sum(LocAmt) as Amt FROM XAApPayable WHERE MastType='{0}' and (MastRefNo = '{1}') group by DocType", "I", refNo);
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
        sql = string.Format(@"SELECT ref.RefNo, ref.JobType, ref.Pod AS Port, ref.ExRate,ref.Eta as Eta,ref.Etd as Etd, job.JobNo, job.CustomerId, job.HblNo, job.Weight, job.Volume,ref.Volume as TotVolume
FROM SeaExport AS job INNER JOIN SeaExportRef AS ref ON job.RefNo = ref.RefNo
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

                //if (role == "SALESMANAGER")
                //{
                //    string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"].ToUpper();
                //    if (exceptSales.Length > 0 && salesmanId == exceptSales)
                //    {
                //        continue;
                //    }
                //}
                //else if (role == "SALESSTAFF")
                //{
                //    if (salesmanId != userName)
                //        continue;
                //}
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



                    string sql_Iv = string.Format("SELECT DocType, sum(LocAmt) Amt FROM XAArInvoice WHERE MastType='{0}' and (MastRefNo = '{1}') and JobRefNo='{2}' group by DocType", "E", refNo, jobNo);
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
                    string sql_Vo = string.Format("SELECT DocType,sum(LocAmt) as Amt FROM XAApPayable WHERE MastType='{0}' and (MastRefNo = '{1}') group by DocType", "E", refNo);
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
        //det.Columns.Add("PeriodStr");
        //det.Columns.Add("ImpExpInd");
        //det.Columns.Add("RefType");
        //det.Columns.Add("Salesman");
        //det.Columns.Add("Cnt");
        //det.Columns.Add("RevAmt");
        //det.Columns.Add("CostAmt");
        //det.Columns.Add("ProfitAmt");
        //det.Columns.Add("UserId");
        //max(PeriodStr) as PeriodStr,max(UserId) as UserId,
        DataTable rptTab = help.SelectGroupByInto("rptTab", det, "PeriodStr,Salesman,ImpExpInd,RefType,count(Salesman) Cnt,sum(RevAmt) RevAmt,sum(CostAmt) CostAmt,sum(ProfitAmt) ProfitAmt", "",
            "PeriodStr,Salesman,ImpExpInd,RefType");


        return rptTab;
    }
}